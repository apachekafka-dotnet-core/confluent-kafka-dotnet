#kafka-verifiable-producer

##Lab: Testing the cluster settings(threshold)

In order to verify how the cluster will behave on several situations, such as when brokers goes down – with partition leaderships or not -, new brokers goes in, etc.

These two utilities is basic interface of produce and consume functionality which helps to test with integer value.

In this example I am producing and consuming 1 million message and see how the end result looks based on configuration setup you might have done.


```sh
./kafka-verifiable-producer --topic mytopic --max-messages 1000000 --broker-list 192.168.0.3:9092 --repeating-keys "3" --producer.config ../etc/kafka/producer.properties

End result-
{"timestamp":1570962520565,"name":"tool_data","sent":1000000,"acked":1000000,"target_throughput":-1,"avg_throughput":66476.10184138801}
```


