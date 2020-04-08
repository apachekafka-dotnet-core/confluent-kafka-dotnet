#kafka-consumer-perf-test

```sh
./kafka-consumer-perf-test --broker-list 192.168.0.3:9092 --threads 3 --topic test --messages 5000000 --print-metrics  --consumer.config ../etc/kafka/consumer.properties

```

Analyze the metrics

Example: 

```sh
start.time, end.time, data.consumed.in.MB, MB.sec, data.consumed.in.nMsg, nMsg.sec, rebalance.time.ms, fetch.time.ms, fetch.MB.sec, fetch.nMsg.sec
2019-10-13 12:32:12:521, 2019-10-13 12:32:15:209, 476.8372, 177.3948, 5000000, 1860119.0476, 19, 2669, 178.6576, 1873360.8093
```


