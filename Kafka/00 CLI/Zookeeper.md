
#Zookeeper

##Lab: Get broker details from Zookeeper

```sh
./Zookeeper-shell localhost:2181

Or

./Zookeeper-shell 192.168.0.3:2181 ls /brokers/ids
./Zookeeper-shell 192.168.0.3:2181 get /brokers/ids/0

ls /brokers/ids   ----- List down all broker

- details out broker details
get /brokers/ids/1
```

```json
{"listener_security_protocol_map":{"PLAINTEXT":"PLAINTEXT"},"endpoints":["PLAINTEXT://192.168.0.3:9093"],"jmx_port":-1,"host":"192.168.0.3","timestamp":"1558195991588","port":9093,"version":4}
```


Zookeeper
./Zookeeper-server-start ../etc/kafka/Zookeeper.properties
./Zookeeper-server-stop

./Zookeeper-shell 192.168.0.3:2181 ls /
./Zookeeper-shell 192.168.0.3:2181 get /topics/test
./Zookeeper-shell 192.168.0.3:2181 get /brokers/ids/1

./bin/Zookeeper-security-migration
