#Lab (Connect source): Use Kafka Connect to import/export data from a topic

###step 1:
```sh
echo -e "foo\nbar" > test.txt
```
###step 2:

```sh
./connect-standalone config/connect-standalone.properties config/connect-file-source.properties config/connect-file-sink.properties
```

###step 3:

```sh
./Kafka-console-consumer --bootstrap-server localhost:9092 --topic connect-test --from-beginning
```

##Lab(Connect source):  Text file to topic in real time

```sh
./connect-standalone ../etc/kafka/connect-standalone.properties ../etc/kafka/connect-file-source.properties
```

Start the consumer in separate terminal window

```sh
./kafka-console-consumer --bootstrap-server localhost:9092 --topic connect-test --from-beginning
```

#Lab(Connect Sink):  Topic to text file in real time

##Step 1: create separate worker.properties file to process String line rather json

### my-standalone.properties worker config file

###bootstrap kafka servers
bootstrap.servers=localhost:9092

### specify input data format
key.converter=org.apache.kafka.connect.storage.StringConverter
value.converter=org.apache.kafka.connect.storage.StringConverter

### The internal converter used for offsets, most will always want to use the built-in default
internal.key.converter=org.apache.kafka.connect.json.JsonConverter
internal.value.converter=org.apache.kafka.connect.json.JsonConverter
internal.key.converter.schemas.enable=false
internal.value.converter.schemas.enable=false

### local file storing offsets and config data
offset.storage.file.filename=/tmp/connect.offsets
rest.port=8084
plugin.path=share/java

Connect-file-sink-properties

name=test-file-sink1
connector.class=FileStreamSink
tasks.max=1
file=/Users/ajachoud/Downloads/confluent-5.2.1/bin/test.sink.txt
topics=test-file-topic2

### Run-

```sh
/connect-standalone ../etc/kafka/connect-standalone-string.properties ../etc/kafka/connect-file-sink.properties
```

