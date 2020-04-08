START KSQL CLIENT

./ksql http://192.168.0.3:8088


Step 4: create stream

KSQL> CREATE STREAM persons (firstName string, lastName string, birthDate string) WITH (Kafka_topic='persons', value_format='json');

Step 5: Ensure stream created 

KSQL> DESCRIBE persons;
OR

KSQL> DESCRIBE EXTENDED persons;

Name                 : PERSONS
 Field     | Type
---------------------------------------
 ROWTIME   | BIGINT           (system)
 ROWKEY    | VARCHAR(STRING)  (system)
 FIRSTNAME | VARCHAR(STRING)
 LASTNAME  | VARCHAR(STRING)
 BIRTHDATE | VARCHAR(STRING)
---------------------------------------

SQL> SHOW TOPICS;


Step 6: select/query

SET 'auto.offset.reset'='earliest';

SELECT * FROM persons;
SELECT firstName, lastName, birthDate, STRINGTOTIMESTAMP(birthDate,'yyyy-MM-dd''T''HH:mm:ss.SSSZ'), STRINGTOTIMESTAMP('2018-08-21','yyyy-MM-dd') FROM persons;

Step 7: write stream response to a topic

KSQL> CREATE STREAM persons_processed AS SELECT CONCAT(CONCAT(firstName, ' '), lastName) AS name, CAST((STRINGTOTIMESTAMP('2018-08-21','yyyy-MM-dd')-STRINGTOTIMESTAMP(birthDate,'yyyy-MM-dd''T''HH:mm:ss.SSSZ'))/365/86400/1000 AS string) AS age FROM persons PARTITION BY name;

KSQL> show streams;

CREATE STREAM ages WITH (Kafka_topic='ages', value_format='delimited') AS SELECT age FROM persons_processed;

NOTE: ./kafka-console-consumer --topic _confluent-ksql-default__command_topic --bootstrap-server 192.168.0.3:9092 --from-beginning


Step 7: KTable from a topic

CREATE TABLE User3 (EmpId VARCHAR, firstName VARCHAR, lastName VARCHAR) WITH (KAFKA_TOPIC = 'jsontest', VALUE_FORMAT='JSON', KEY = 'EmpId');

curl -X POST -H "Content-Type: application/vnd.kafka.json.v2+json" -H "Accept: application/vnd.kafka.v2+json" --data '{"records":[{"key":{"EmployeeId":"EMP1003"}, "value":{"EmpId":"EMP1002","firstName":"Anna","lastName":"Kumar","birthDate":"1997-10-22T04:09:35.696+0000"}}]}' "http://localhost:8082/topics/jsontest"


Lab: Stream Avro Message

START KSQL CLIENT

./ksql http://192.168.0.3:8088

Step 4: create stream

KSQL> CREATE STREAM registeredCustomers (CustomerId int, CustomerName string) WITH (Kafka_topic='customer-register-topic', value_format='AVRO', KEY='CustomerId');

Step 5: Ensure stream created 

KSQL> DESCRIBE registeredCustomers;
OR

KSQL> DESCRIBE EXTENDED persons;

Name                 : PERSONS
 Field     | Type
---------------------------------------
 ROWTIME   | BIGINT           (system)
 ROWKEY    | VARCHAR(STRING)  (system)
 FIRSTNAME | VARCHAR(STRING)
 LASTNAME  | VARCHAR(STRING)
 BIRTHDATE | VARCHAR(STRING)
---------------------------------------

SQL> SHOW TOPICS;

Step 6: select/query

SET 'auto.offset.reset'='earliest';

SELECT * FROM persons;

SELECT CustomerId, CustomerName FROM registeredCustomers;

CREATE STREAM PREMIUM_CUSTOMER AS SELECT CustomerId, CustomerName FROM registeredCustomers WHERE CustomerId >= 1000 AND CustomerId <= 2000;

SELECT * FROM PREMIUM_CUSTOMER

--- AVRO SCHEMA HAS TO BE PRESENT TO READ AT CONSUMER SIDE

{
  "type": "record",
  "name": "KsqlDataSourceSchema",
  "namespace": "io.confluent.ksql.avro_schemas",
  "fields": [
    {
      "name": "CUSTOMERID",
      "type": ["null", "int"],
      "default": null
    },
    {
      "name": "CUSTOMERNAME",
      "type": ["null", "string"],
      "default": null
    }
  ]
}


Lab: Join two Avro stream


START KSQL CLIENT

./ksql http://192.168.0.3:8088

Step 4: Create stream

KSQL> CREATE STREAM customerBalance (CustomerId int, Balance double) WITH (Kafka_topic='customer-credit-topic', value_format='AVRO', KEY='CustomerId');

Step 5: Ensure stream created 

KSQL> DESCRIBE registeredCustomers; // Provides a description of a function including input parameters and the return type.

KSQL> SET 'auto.offset.reset'='earliest';

//make sure earlier stream already there, if not - create before creating join below
SELECT CustomerId, CustomerName FROM registeredCustomers;

//create join
CREATE STREAM vip_users AS SELECT c.CustomerId, c.CustomerName, u.Balance FROM registeredCustomers2 c LEFT JOIN customerBalance u WITHIN 1 HOUR ON c.CustomerId = u.CustomerId;

SELECT * FROM vip_users


Lab: Aggregation and Join

curl -s "https://api.mockaroo.com/api/86f89de0?count=500&key=ff7856d0" | kafkacat -P -b 192.168.0.3:9092 -t orders

PRINT  'orders' FROM BEGINNING; // Print the contents of Kafka topics to the KSQL CLI.

[FROM BEGINNING]  - Print starting with the first message in the topic.
[INTERVAL interval] - Print every interval th message. The default is 1, meaning that every message is printed.
[LIMIT limit] - Stop printing after limit messages. The default value is unlimited

CREATE STREAM ORDERS9 (ORDER_ID INT, CUSTOMER_ID INT, ORDER_TS VARCHAR, ORDER_TOTAL_USD DOUBLE, MAKE VARCHAR) WITH (KAFKA_TOPIC='orders', VALUE_FORMAT='JSON', timestamp='ORDER_TS', timestamp_format='yyyy-MM-dd''T''HH:mm:ssX');

CREATE TABLE ORDERS_AGG8 AS SELECT MAKE, COUNT(*) AS ORDER_COUNT, MAX(ORDER_TOTAL_USD) AS MAX_ORDER_VALUE_USD, SUM(ORDER_TOTAL_USD) AS TOTAL_ORDER_VALUE_USD, SUM(ORDER_TOTAL_USD)/COUNT(*) AS AVG_ORDER_VALUE_USD FROM ORDERS8 GROUP BY MAKE;

curl -s "https://api.mockaroo.com/api/b410d0b0?count=500&key=ff7856d0" | kafkacat -P -b localhost -t shipments

CREATE STREAM SHIPMENTS (SHIPMENT_ID VARCHAR, ORDER_ID INT, SHIPMENT_PROVIDER VARCHAR, SHIPMENT_TS VARCHAR) WITH (KAFKA_TOPIC='shipments', VALUE_FORMAT='JSON' );

CREATE TABLE SHIPMENTS_AGG AS SELECT SHIPMENT_PROVIDER, COUNT(*) AS SHIPMENT_COUNT FROM SHIPMENTS WINDOW TUMBLING (SIZE 1 HOUR) GROUP BY SHIPMENT_PROVIDER;

CREATE STREAM ORDER_SHIPMENTS WITH (VALUE_FORMAT='AVRO') AS SELECT O.ORDER_ID AS ORDER_ID, O.MAKE AS MAKE, O.ORDER_TOTAL_USD AS ORDER_TOTAL_USD, S.SHIPMENT_ID AS SHIPMENT_ID, S.SHIPMENT_PROVIDER AS SHIPMENT_PROVIDER FROM ORDERS O LEFT OUTER JOIN SHIPMENTS S WITHIN (0 SECONDS, 1 HOUR) ON O.ORDER_ID=S.ORDER_ID;

Lab: Stream json(structured data)


Create a file with below structure (i.e. car-sale.json)

{"order_id":1,"customer_id":197,"order_ts":"2019-03-24T09:01:04Z","order_total_usd":"56770.40","make":"Audi","registration":{"name":"ABC","address":"Paramount 1245","city":"Swindon","county":"Wiltz","postcode":"NS112"}}


Ksql > cat car-sale.json | kafkacat -P -b 192.168.0.3:9092 -t orders

PRINT  'orders' FROM BEGINNING; // Print the contents of Kafka topics to the KSQL CLI.

CREATE STREAM ORDERS (ORDER_ID INT, CUSTOMER_ID INT, ORDER_TS VARCHAR, ORDER_TOTAL_USD DOUBLE, MAKE VARCHAR, REGISTRATION STRUCT<"name" VARCHAR,"address" VARCHAR,"city" VARCHAR,"county" VARCHAR, "postcode" VARCHAR>) WITH (KAFKA_TOPIC='orders', VALUE_FORMAT='JSON');

create stream paramount_orders as select ORDER_ID, registration -> "address", make from ORDERS where registration -> "address" like 'Paramount%';


CREATE TABLE TB1 AS SELECT CUSTOMER_ID, MAKE FROM ORDERS GROUP BY CUSTOMER_ID,MAKE

More:
https://www.confluent.io/stream-processing-cookbook/ksql-recipes
https://github.com/confluentinc/demo-scene

Lab: Convert Avro Stream to CSV

1. Define the source topic’s schema:
CREATE STREAM source_avro \
WITH (KAFKA_TOPIC='mysql_users_avro', VALUE_FORMAT='AVRO');

2. Create a derived topic in delimited (CSV) format:
CREATE STREAM target_delim WITH (KAFKA_TOPIC='mysql_users_delim',VALUE_FORMAT='DELIMITED') AS \
SELECT * FROM source_avro;

You will get the resulting data:
$ kafkacat -C -b localhost:9092 -t mysql_users_delim
1,Cliff,en_US,St Louis,P

Lab: Convert CSV Stream to AVRO

Step 1: Make sure topic has data in csv format

$ kafkacat -b localhost:9092 -t testdata-csv -C
1,Rick Astley,Never Gonna Give You Up
2,Johnny Cash,Ring of Fire

Step 2: Create stream and columns

CREATE STREAM TESTDATA_CSV (ID INT, ARTIST VARCHAR, SONG VARCHAR) \
WITH (KAFKA_TOPIC='testdata-csv', VALUE_FORMAT='DELIMITED');

DESCRIBE TESTDATA_CSV;

Name                 : TESTDATA_CSV
 Field   | Type
-------------------------------------
 ROWTIME | BIGINT (system)
 ROWKEY  | VARCHAR(STRING) (system)
 ID      | INTEGER
 ARTIST  | VARCHAR(STRING)
 SONG    | VARCHAR(STRING)

Step 3: Create stream and columns

SET 'auto.offset.reset' = 'earliest';
SELECT ID, ARTIST, SONG FROM TESTDATA_CSV;


1 | Rick Astley | Never Gonna Give You Up
2 | Johnny Cash | Ring of Fire


Step 4: Create stream that writes into avro format

CREATE STREAM TESTDATA WITH (VALUE_FORMAT='AVRO') AS SELECT * FROM TESTDATA_CSV;

Step 5: Ensure avro console consumer listen the message 

kafka-avro-console-consumer --bootstrap-server localhost:9092 \
                                --property schema.registry.url=http://localhost:8081 \
                                --topic TESTDATA \
                                --from-beginning | \
                                jq '.'

Lab: Terminate a query
terminate query "query_name"

Lab: Run a ksql script from cli
run script "./path/to/file.ksql"

Lab: Print a topic from beginning
print 'topicname' from beginning

Lab: Stream from beginning
set 'auto.offset.reset'='earliest';
select * from stream;
