#ksql-datagen

##Lab: KSQL - ksql-datagen

```sh
./ksql-datagen quickstart=users format=json topic=users maxInterval=100 bootstrap-server=192.168.0.3:9092

./ksql-datagen quickstart=pageviews format=delimited topic=pageviews maxInterval=500 bootstrap-server=192.168.0.3:9092
```

##Lab: KSQL - Sample data generation as per given avro schema

```sh
./ksql-datagen schema=avroschema.avsc  format=avro topic=pageviews maxInterval=500 bootstrap-server=192.168.0.3:9092 key=username
```

Ex: avroschema.avsc
```json

{
    "type" : "record",
    "name" : "userInfo",
    "namespace" : "my.example",
    "fields" : [{"name" : "username", "type" : "string"}]
}

```

