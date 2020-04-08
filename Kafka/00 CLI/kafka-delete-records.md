#kafka-delete-records

##Lab: delete logs from topic

Option #1 – Not recommended
```sh
rm -rf /tmp/kafka-logs/test2*
```

Option #2 – Idle way

```sh
Step 1: Create a json file i.e. test0-delete-record.json

{
  "partitions": [
    {
      "topic": "test1",
      "partition": 0,
      "offset": 1
    }
],
  "version": 1
}

Step2: 

./kafka-delete-records --offset-json-file test0-delete-record.json --bootstrap-server 192.168.0.3:9092

```

