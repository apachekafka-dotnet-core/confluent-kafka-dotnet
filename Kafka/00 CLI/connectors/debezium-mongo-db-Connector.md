#debezium mongo db Connector

##Lab: debezium mongo db Connector

###Step1: download mongo server https://www.mongodb.com/dr/fastdl.mongodb.org/osx/mongodb-macos-x86_64-4.2.0.tgz/download

###Step2: unzip and start with 

mongod --port 27001 --smallfiles --oplogSize 50 --replSet test

###Step 3: 

```json
cfg = { _id: "test", members: [ {_id:0, host: "localhost:27001"} ] }
{
	"_id" : "test",
	"members" : [
		{
			"_id" : 0,
			"host" : "localhost:27001"
		}
	]
}
```

```sh
> use admin
switched to db admin
```

```sh
> rs.initiate(cfg)

```

###Step 4: kafka> path/connect-mongo-source.properties

name=mongodb-source-connector
connector.class=io.debezium.connector.mongodb.MongoDbConnector
mongodb.hosts=localhost:27001
initial.sync.max.threads=1
tasks.max=1
mongodb.name= testmanager

###Step 5: start connect

```sh
./connect-standalone ../etc/schema-registry/connect-avro-standalone.properties path/connect-mongo-source.properties
```

###Step 6: Insert new record in mongo db

Example:

//new record inserted

```json
{"after":{"string":"{\"_id\" : \"ac3\",\"name\" : \"AC3 Phone\",\"brand\" : \"ACME\",\"type\" : \"phone\",\"price\" : 200.0,\"rating\" : 3.8,\"warranty_years\" : 1.0,\"available\" : true}"},"patch":null,"source":{"version":{"string":"0.9.5.Final"},"connector":{"string":"mongodb"},"name":"nbs-mortgages-rio-affordability-manager","rs":"test","ns":"nbs-mortgages-rio-affordability-manager.Test","sec":1569703578,"ord":1,"h":{"long":745720102905626576},"initsync":{"boolean":true}},"op":{"string":"r"},"ts_ms":{"long":1569703581812}}
{"after":{"string":"{\"_id\" : \"ac1\",\"name\" : \"AC3 Phone\",\"brand\" : \"ACME\",\"type\" : \"phone\",\"price\" : 200.0,\"rating\" : 3.8,\"warranty_years\" : 1.0,\"available\" : true}"},"patch":null,"source":{"version":{"string":"0.9.5.Final"},"connector":{"string":"mongodb"},"name":"nbs-mortgages-rio-affordability-manager","rs":"test","ns":"nbs-mortgages-rio-affordability-manager.Test","sec":1569703606,"ord":1,"h":{"long":-8550136954992235550},"initsync":{"boolean":false}},"op":{"string":"c"},"ts_ms":{"long":1569703606433}}

//patch – warranty_years – value updated

{"after":null,"patch":{"string":"{\"$v\" : 1,\"$set\" : {\"warranty_years\" : 2.0}}"},"source":{"version":{"string":"0.9.5.Final"},"connector":{"string":"mongodb"},"name":"nbs-mortgages-rio-affordability-manager","rs":"test","ns":"nbs-mortgages-rio-affordability-manager.Test","sec":1569706369,"ord":1,"h":{"long":-6938885522743841623},"initsync":{"boolean":false}},"op":{"string":"u"},"ts_ms":{"long":1569706369166}}

//patch – null when record delete
{"after":null,"patch":null,"source":{"version":{"string":"0.9.5.Final"},"connector":{"string":"mongodb"},"name":"nbs-mortgages-rio-affordability-manager","rs":"test","ns":"nbs-mortgages-rio-affordability-manager.Test","sec":1569706401,"ord":1,"h":{"long":-4405656552751642254},"initsync":{"boolean":false}},"op":{"string":"d"},"ts_ms":{"long":1569706401998}}
```

###Step 6: run the consumer to validate messages are on topic

```sh
./kafka-avro-console-consumer --topic test --bootstrap-server 192.168.0.3:9092
```