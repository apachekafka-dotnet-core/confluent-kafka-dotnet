#kafka-reassign-partitions

##Lab: Migrate topic leader and replicas

This tool is similar to kafka-preferred-replica-election but just only updating leader it also allows to assign replica of a parition. This tool only updates the zookeeper path and exits and controller reassign the replicas for the partitions asynchronously.

###Step 1: create topics-to-move.json file with all topics that you want to migrate or assign new leader or replicas


topics-to-move.json

```json 

{"topics":  [
		{"topic": "mytopic1"},
                {"topic": "mytopic2"}
	    ],
 "version":1
}

```



###Step 2: see current assignment and proposed reassignment

```sh
./kafka-reassign-partitions --topics-to-move-json-file topics-to-move.json --broker-list "0,1,2" --generate --zookeeper localhost:2181

Current partition replica assignment (or create a json file in below format)

{"version":1,"partitions":[{"topic":"test2","partition":2,"replicas":[1,2,0],"log_dirs":["any","any","any"]},{"topic":"test2","partition":1,"replicas":[0,1,2],"log_dirs":["any","any","any"]},{"topic":"test2","partition":0,"replicas":[2,0,1],"log_dirs":["any","any","any"]}]}

```

Proposed partition reassignment configuration

```json
{"version":1,"partitions":[{"topic":"test2","partition":2,"replicas":[0,1,2],"log_dirs":["any","any","any"]},{"topic":"test2","partition":1,"replicas":[2,0,1],"log_dirs":["any","any","any"]},{"topic":"test2","partition":0,"replicas":[1,2,0],"log_dirs":["any","any","any"]}]}

```

###Step 3: Make the amendment in proposed configuration, save and execute.

```sh
./kafka-reassign-partitions  --zookeeper localhost:2181  --reassignment-json-file expand-cluster-reassignment.json --execute
```
Status of partition reassignment:

Reassignment of partition test3-0 completed successfully
Reassignment of partition test3-1 completed successfully


```sh

--verify option in kafka-reassign-partitions to check the status of your partitions.

./kafka-reassign-partitions  --zookeeper localhost:2181  --reassignment-json-file expand-cluster-reassignment.json --verify
```
