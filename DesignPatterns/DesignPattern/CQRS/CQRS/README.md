README
======

- Command handler should not be initiating another command (Dont reuse command handlers), if needed extrct common behaviour put it in doamin service and raise two separate commands.
- Put command and query handlers inside respective commands and handlers
- Separation of domain model
	- It is impossible to created an optimal soluton for searching and processing of transactions utilizing a single model
	- Loose coupling
	- Both reads and writes benifits from separation
	- Writes benefit from removing code from the domain model that is not used for data modification
	- Reads benefit because you are able to optimize the data retrieval 
	- Sharding to scale commands (rare requirement)	

###The Read Model and the Onion Architecture

####Command 	 Query
Entities
Aggregates
Value Objects
Domain Events
Pure Domain Service

###Separation at the domain model level###
Repositories
Impure Domain Service
Queries model are not longer part of this
No need for encapsulation in the read

####DTOs go here####
Application Service
UI


### Seeparate databases for reads (always eventual consistency-partitionability-when scalabiliity is important, give-up full consistency) and writes (full consistency, give-up partition tolerance, when consistent changes are important	)

https://www.enterpriseintegrationpatterns.com/ramblings/18_starbucks.html

 -  if versioning, always keep the version in communication
-	One master node for write and multiple replicas node to read
- 	Index view 
-	Database replication
-	Elasticsearch
- 	High normal forms are good for commands; low normal forms are good for queries. (3rd Normal form is for commands; the 1st form for queryies)

### Designed the database for read 

- Denormalized and thus adujsted it to the nesds of the read model
- Minimized the number of joins and the amount of post processing
- Might need a seprate read database for each client

- Projections
	-	Event Driven 
		- Domain evetns drive the changes
			(Instead of any flag/IsSyncRequired glag - all sync happens using event that occurs in the system, benefits scales really well, can use message bus,)
		- Without event sourcing
			- State-driven projection
		- With event sourcing
			- Event-driven projection


	-	State driven
		-	Sync driven (database trigger) - (applcation does all the jobs, increases processing time, benefits - read and write consistently immediately, more db added increases the time(consider)- scaling-up become challenging)

		- 	(Note: to rebuild the read database, raise the flag for all records)
		-	In-Sync driven (without-blocking main thread)
			- Introduce the flags in the domain model (whenever changes happens mark the record dirty/IsSyncRequired=true until it updates/replciated)
			More: http://bit.ly/ef-vs-nh

###	Evolutionary Design

Ensure the benefits outweigh the costs before applyinng a pattern.

Doen't apply all techniques at once start by Bounded context one by one/task based interface.

<!-- <Sequence> -->
sequenceDiagram
Client->>Write model: Command
Write model -->>Event: Somthing happend
Event -->>Read Model(State): State
Write model -->> Client: Acknowledgement
Client->>Read Model(State): QUERY?
Read Model(State)-->>Client: sum of all the events
<!-- </Sequence> -->



