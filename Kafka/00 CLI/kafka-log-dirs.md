#kafka-log-dirs

##Lab: Know where the topic data being committed or segment files are 
```sh
kafka-log-dirs --bootstrap-server 192.168.0.3:9092 --describe --topic-list test1

or

kafka-log-dirs --describe --bootstrap-server hostname:port --broker-list broker 1, broker 2 --topic-list topic 1, topic 2

```

