#kafka-console-producer

## Lab: Start the consumer and producer with additional property to parse the key from the message


```sh

./kafka-console-producer --broker-list 192.168.0.3:9093 --topic employe-salary  --property parse.key=true  --property key.separator=,

./Kafka-console-consumer --topic employe-salary --bootstrap-server 192.168.0.3:9092 --from-beginning --property print.key=true --property key.seperator=,


```

##Lab: Enable debug log for producer, see what happens when you produce a message


```sh

Step 1: Open etc\kafka\log4j.properties 
Step 2: Make an entry at the end
log4j.logger.org.apache.kafka.clients.producer.KafkaProducer=ALL

Step 3: Restart Kafka
Step 4: ./confluent log Kafka -f 
Ste5 5: produce a message using console producer and see the steps

```

##Lab: Writes to the leader of a partition

```sh
./Kafka-console-producer --broker-list 192.168.0.3:9092 --topic test3
./Kafka-console-producer --broker-list 192.168.0.3:9092 --topic test3 --producer-property acks=all

./Kafka-console-producer --broker-list 192.168.0.3:9092 --topic test5 

warning - new topic LEADER_NOT_AVAILABLE
```

Note:
>	if topic not created, and try to broadcast message - producer create topic with default settings (server.properties = num.partitions),  automatically with the warning

>	Producer usually send data that is text based, if needed compression need to apply -compression configuration requires at producer end not on configuration end.


##Lab: Producer with the compressed message (compressed.topics)

