#	kafka-consumer-groups

##	List all available consumer groups are active

```sh
./kafka-consumer-groups --list --bootstrap-server 192.168.0.3:9092
```

##	Lab: Verify if Kafka consumer consumed messages
 
```sh
./kafka-consumer-groups --bootstrap-server 192.168.0.3:9092 --group g1 –describe
```

##	Lab: Reset offset for consumer offset to replay already consumed messages

```sh
./kafka-consumer-groups --bootstrap-server 192.168.0.3:9092 --group g1 --topic test2 --reset-offsets --to-earliest --execute
```

##	Lab: Reset consumer group offset 

Note: Assignments can only be reset if the consumer group is inactive

```sh
./Kafka-consumer-groups --bootstrap-server 192.168.0.3:9092 --describe --group g1
```

Consumer group 'g1' has no active members. /// currently no active members from this group

TOPIC           PARTITION  CURRENT-OFFSET  LOG-END-OFFSET  LAG             CONSUMER-ID     HOST            CLIENT-ID
test1           0          8               9               1               -               -               -
test1           2          13              13              0               -               -               -
test1           1          11              12              1               -               -               -

Reset options

```sh
./Kafka-consumer-groups --bootstrap-server 192.168.0.3:9092 --reset-offsets --to-earliest --execute --group g1

./Kafka-consumer-groups --bootstrap-server 192.168.0.3:9092 --reset-offsets --to-earliest --execute --group g1 --topic test1

```

--to-earliest 
--shift-by

TOPIC                          PARTITION  NEW-OFFSET
test1                          2          0
test1                          1          0
test1                          0          0


```sh
./Kafka-consumer-groups --bootstrap-server 192.168.0.3:9092 --describe --group g1
Consumer group 'g1' has no active members.
```

TOPIC           PARTITION  CURRENT-OFFSET  LOG-END-OFFSET  LAG             CONSUMER-ID     HOST            CLIENT-ID
test1           0          0               10              10              -               -               -
test1           2          0               15              15              -               -               -
test1           1          0               13              13              -               -               -

or

```sh
Kafka-consumer-groups --bootstrap-server 192.168.0.3:9092 --reset-offsets --group g3 --to-datetime 2019-12-14T15:30:00.000 --execute --all-topics
```