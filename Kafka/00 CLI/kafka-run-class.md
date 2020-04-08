#kafka-run-class

##Lab: Get offsets for a topic

```sh
./kafka-run-class kafka.tools.GetOffsetShell --broker-list 192.168.0.3:9092 --topic test1

Example: messages in each partition

test1:0:9
test1:1:10
test1:2:9

```

##Lab: Get number of messages in a topic

```sh
./kafka-run-class kafka.tools.GetOffsetShell --broker-list 192.168.0.3:9092 --topic __consumer_offsets --time -1 --offsets 1 | awk -F ":" '{sum += $3} END {print sum}'

```

##Lab: Get the dump from the log segment file

```sh
./kafka-run-class kafka.tools.DumpLogSegments --deep-iteration --print-data-log --files /tmp/kafka-logs/test3-0/00000000000000000000.log
```

