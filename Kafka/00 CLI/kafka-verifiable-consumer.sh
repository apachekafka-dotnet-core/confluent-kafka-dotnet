#kafka-verifiable-consumer

```sh
./kafka-verifiable-consumer --broker-list 192.168.0.3:9092 --topic mytopic --max-messages 1000000 --group-id g1  --assignment-strategy org.apache.kafka.clients.consumer.RoundRobinAssignor --consumer.config ../etc/kafka/consumer.properties

```

