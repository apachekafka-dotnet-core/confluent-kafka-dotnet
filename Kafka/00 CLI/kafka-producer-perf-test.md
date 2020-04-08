#kafka-producer-perf-test

Step 1-create topic with replication factor and partitions – start brokers accordingly

For example: replication factor = brokers

```sh
./kafka-topics   --zookeeper 192.168.0.3:2181   --create   --topic test   --partitions 6 --replication-factor 3
```

Step 2:

```sh
./kafka-producer-perf-test --topic test --num-records 5000000 --print-metrics  --throughput -1 --record-size 100 --producer.config ../etc/kafka/producer.properties

```

Analyze the metrics

Example: 

3274096 records sent, 654688.3 records/sec (62.44 MB/sec), 5.6 ms avg latency, 230.0 max latency.
5000000 records sent, 751992.780869 records/sec (71.72 MB/sec), 7.00 ms avg latency, 230.00 ms max latency, 4 ms 50th, 29 ms 95th, 53 ms 99th, 68 ms 99.9th.
