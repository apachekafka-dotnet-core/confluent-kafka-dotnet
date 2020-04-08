#kafka-console-consumer

## Lab: Consuming json message

```sh

./kafka-console-consumer --topic test2 --bootstrap-server 192.168.0.3:9092 --from-beginning

./kafka-console-consumer --topic test2 --bootstrap-server 192.168.0.3:9092 --group g1 --from-beginning

./kafka-console-consumer --topic test-topic4 --bootstrap-server 192.168.0.3:9092 --property print.key=true --from-beginning --key-deserializer=org.apache.kafka.common.serialization.StringDeserializer

./kafka-console-consumer --bootstrap-server 192.168.0.3:9092 --topic jsontest --from-beginning --group g1 > backup-jsontest-15-Sep.txt

```

Note:

--fetch-size => amount of data to be fetched in a single request, its size in bytes follow this argument.

--max-messages => maximum number of messages to consume before exiting. If it’s not set, the consumption is unlimited.

--skip-message-on-error => just skip the current message, if any error while processing message

--autocommit.interval.ms => specify the time interval in which the current offset is saved in ms.

## Lab: While consuming message and print message timestamp (time when message produced)

```sh
kafka-console-consumer --bootstrap-server localhost:9092 --topic test5 --group g2 --from-beginning --property print.timestamp=true
```

## Lab: Read topic message and format binary encoded data 

```sh
kafka-console-consumer --bootstrap-server localhost:9092   --topic __consumer_offsets --formatter "kafka.coordinator.group.GroupMetadataManager\$OffsetsMessageFormatter"
```

