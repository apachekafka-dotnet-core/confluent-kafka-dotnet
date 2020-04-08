#kafka-preferred-replica-election

## Lab: restore the leadership balance between the brokers in the cluster

This guarantees that the leadership load across the brokers in a cluster are evenly balanced. sometime the leadership load could get imbalanced due to broker shutdowns (caused by controlled shutdown, crashes, machine failures etc). 

```sh
kafka-preferred-replica-election --zookeeper localhost:2181
```
What happens behind the scene.

>	1. The tool updates the zookeeper path "/admin/preferred_replica_election" with the list of topic partitions whose leader needs to be moved to the preferred replica.

>	2. The controller listens to the path above. When a data change update is triggered, the controller reads the list of topic partitions from zookeeper.

>	3. For each topic partition, the controller gets the preferred replica (the first replica in the assigned replicas list). If the preferred replica is not already the leader and it is present in the isr, the controller issues a request to the broker that owns the preferred replica to become the leader for the partition.


## Lab: Assign partition leader among the servers

Example json file (This is optional. This can be specified to move the leader to the preferred replica for specific topic partitions)

```sh
topic-parition-across-brokers.json

{
 "partitions":
  [
    {"topic": "topic1", "partition": 0},
    {"topic": "topic1", "partition": 1},
    {"topic": "topic1", "partition": 2},
    {"topic": "topic2", "partition": 0},
    {"topic": "topic2", "partition": 1}
  ]
}

kafka-preferred-replica-election --zookeeper localhost:2181 --path-to-json-file topic-parition-across-brokers.json

```

Note: if preferred replica is not in-sync, it fails in order to avoid any data loss.

