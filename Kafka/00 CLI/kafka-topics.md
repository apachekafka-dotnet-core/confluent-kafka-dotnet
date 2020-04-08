# kafka-topics


## Lab: Check what is committed in log after producing some message

```sh

./Kafka-run-class Kafka.tools.DumpLogSegments --deep-iteration --files /tmp/Kafka-logs-2/test1-1/00000000000000000000.log

Dumping /tmp/Kafka-logs-2/test1-1/00000000000000000000.log
Starting offset: 0
offset: 0 position: 0 CreateTime: 1558178444430 isvalid: true keysize: -1 valuesize: 11 magic: 2 compresscodec: NONE producerId: -1 producerEpoch: -1 sequence: -1 isTransactional: false headerKeys: []
offset: 1 position: 79 CreateTime: 1558193735202 isvalid: true keysize: -1 valuesize: 10 magic: 2 compresscodec: NONE producerId: -1 producerEpoch: -1 sequence: -1 isTransactional: false headerKeys: []

```


## Lab: Create a topic, with custom configs

```sh
./kafka-topics --zookeeper 127.0.0.1:2181 --create --topic employe-salary --partitions 1 --replication-factor 1 --config cleanup.policy=compact --config min.cleanable.dirty.ratio=0.001 --config segment.ms=5000

cleanup.policy=compact //policy to let Kafka enable compacting policy on topic
min.cleanable.dirty.ratio=0.001 // ensure log compaction happens in this interval of time
segment.ms=5000 // every five seconds new segment will be created
```

Ensure config are there with the topic
```sh
Topic:employe-salary  PartitionCount:1  ReplicationFactor:1  Configs:min.cleanable.dirty.ratio=0.001,cleanup.policy=compact,segment.ms=5000
Topic: employe-salary  Partition: 0  Leader: 0  Replicas: 0  Isr: 0
```

## Lab: How to show partitions whose leader are not available


```sh
./kafka-topics --bootstrap-server localhost:9092 --describe --unavailable-partitions
```

## Lab: Log clean-up policy

Log cleanup happens on partition segments

Policy=1 

>	Delete data based on age of data (default is a week)
>	Delete data based on max size (default is -1, i.e. infinite)

```sh
log.cleanup.policy = delete //default for all topics
```

Policy=2

>	Delete based on key of the message
>	Will delete old duplicate keys after the active segment is committed infinite time and space

Note: If log cleanup trigger too often thus more CPU and RAM resources, that shouldn't.  see log.cleaner.backoff.ms setting.


```sh
log.cleanup.policy = compact (Kafka default for topic __consumer_offsets)
```


## Lab: Delete a topic

Topic Deletion is a feature of Kafka that allows for deleting topics. TopicDeletionManager is responsible for topic deletion and controlled by delete.topic.enable Kafka property that turns it on when true and will only delete a topic if the topic’s leader broker is available.

Step 1: Start the broker in one terminal window (assuming zookeeper is running)

```sh
./kafka-server-start ../etc/kafka/server.properties --override delete.topic.enable=true --override broker.id=100 --override log.dirs=/tmp/kafka-logs-100 --override port=9192
```


Step 2: Create a topic
```sh
kafka-topics.sh --zookeeper localhost:2181 --create --topic remove-me --partitions 1 --replication-factor 1
```

Step 3: Make sure topic is created and broker 100 is the leader

```sh
kafka-topics --zookeeper localhost:2181 –-describe
```

Step 4: Stop the broker.id 100 and start with new broker with id 200

```sh
./kafka-server-start ../etc/kafka/server.properties --override delete.topic.enable=true --override broker.id=200 --override log.dirs=/tmp/kafka-logs-200 --override port=9192
```

Step 4: Delete the topic, as you can see topic marked with deletion and will remain in this state until broker 100 is up.

```sh
./kafka-topics --zookeeper localhost:2181 --delete --topic remove-me

Log----- It’s marked for deletion
[2019-12-10 19:23:14,970] INFO Log for partition remove-me-0 is renamed to /tmp/kafka-logs-100/remove-me-0.5d224c2961324dbc9a9edb00948a9a28-delete and is scheduled for deletion (kafka.log.LogManager)
```

Also check with zookeeper with this state

```sh
./zookeeper-shell localhost:2181
ls /admin/delete_topics
[remove-me]
```

Step 4: Start the broker.id=100 and check the log, topic should not be there.  

```sh
./kafka-topics --zookeeper localhost:2181 –-describe
```

## Lab: add/update config after topic is created

  
Step 1: Create topic and assign some additional properties
  
```sh
$ ./kafka-topics --zookeeper localhost:2181 --create --topic test1 --partitions 1 --replication-factor 1

$ ./kafka-configs --zookeeper 127.0.0.1:2181 --entity-type topics --entity-name test1 --add-config min.insync.replicas=1 --alter

Completed Updating config for entity: topic 'test1'.

 ```
 
Step 2: Verify what are additional properties are assigned to the topic
  
```sh
$ ./kafka-configs --zookeeper 127.0.0.1:2181 --entity-type topics --entity-name test1 --describe

Configs for topic 'test1' are min.insync.replicas=1

  ```

```sh
$ ./kafka-topics --zookeeper 127.0.0.1:2181 --describe --topic test1

Topic:test1  PartitionCount:3  ReplicationFactor:3  Configs:min.insync.replicas=1
Topic: test1  Partition: 0  Leader: 1  Replicas: 2,1,0  Isr: 1,0,2
Topic: test1  Partition: 1  Leader: 1  Replicas: 0,2,1  Isr: 1,0,2
Topic: test1  Partition: 2  Leader: 1  Replicas: 1,0,2  Isr: 1,2,0

  ```

Also check the state in zookeeper

```sh
[zk: localhost:2181(CONNECTED) 16] get /config/topics/test1

{"version":1,"config":{"min.insync.replicas":"1"}}
  ```



##	Lab: Get the replicas status and partition leader of a topic 

In my case three brokers are started and replica setting is three for test1 topic.

```sh
./Kafka-topics --describe test1 --zookeeper 192.168.0.3


Topic:__confluent.support.metrics	PartitionCount:1	ReplicationFactor:1	Configs:retention.ms=31536000000
	Topic: __confluent.support.metrics	Partition: 0	Leader: 0	Replicas: 0	Isr: 0

Topic:test1	PartitionCount:3	ReplicationFactor:3	Configs:
	Topic: test1	Partition: 0	Leader: 2	Replicas: 2,1,0	Isr: 2,1,0
	Topic: test1	Partition: 1	Leader: 0	Replicas: 0,2,1	Isr: 0,1,2
	Topic: test1	Partition: 2	Leader: 1	Replicas: 1,0,2	Isr: 1,0,2

```

### Topic

Topic can have one or more partition.
It is not possible to delete a partition of topic once created.
Order is guaranteed within the partition and once data is written into partition, it’s immutable!
If producer writes at 1 GB/sec and consumer consumes at 250MB/sec then requires 4 partition!


### Partition

Partitions is the actual unit of storage in Kafka, immutable collection of messages that stores in multiple segments files.

### Leader 

The node responsible for all reads and writes for the given partition. Each node will be the leader for a randomly selected portion of the partitions. In above table Leader:2 means broker 2 is leader of this partition.

### Replicas

It is the list of nodes that replicate the log for this partition regardless of whether they are the leader or even if they are currently alive.

### isr 

It is the set of "in-sync" replicas. This is the subset of the replicas list that is currently alive and caught-up to the leader.

###Configs

no specific configs


Brokers message locations (Kafka-logs-2)// driven from server.properties 

log.dirs= /tmp/Kafka-logs-2 //A comma separated list of directories under which to store log files

```sh
Kafka-logs-2
$ [   0]  cleaner-offset-checkpoint
[   4]  log-start-offset-checkpoint
[  54]  meta.properties
[  34]  recovery-point-offset-checkpoint
[  34]  replication-offset-checkpoint
[ 204]  test1-0
‚îÇ¬†¬† [ 10M]  00000000000000000000.index
‚îÇ¬†¬† [  81]  00000000000000000000.log
‚îÇ¬†¬† [10.0M]  00000000000000000000.timeindex
‚îÇ¬†¬† ‚îî‚îÄ‚îÄ [   8]  leader-epoch-checkpoint
```

Each message in partition (.log file) gets an ID which is incremental  also called as the offset so the first message offsets 0, the second 1 and so on.


## Lab: In sync replicas and replication factor

```sh
./kafka-topics --create --zookeeper 192.168.0.3:2181 --topic test2 --config min.insync.replicas=3 --partitions 3 --replication-factor 3

./kafka-topics --create --zookeeper 192.168.0.3:2181 --topic test1 --partitions 3 --replication-factor 3

```


