#kafka-cat

kafkacat is a generic producer and consumer for Apache Kafka

brew install kafkacat

kafkacat -C -b 192.168.0.3:9092 -o beginning -e -t topic1 > message.txt
cat message.txt | kafkacat -P -b 192.168.0.3:9092 -t test2

