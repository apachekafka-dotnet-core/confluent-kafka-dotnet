Microservices
------------

2. Scoping microservice using bounded context
=============================================
>  Domain driven design (a design that models real world domains)
	- Domain consists of multiple bounded context (describe functions assocaited with-in domain), each bounded ccontext represents a domain function - a specific responsibility enforced by an explicit boundry

	- encourge to loose coupling and high cohesion with-in boundry

	- Starts of as a core domain concept
	- Internal models (supporting concepts/internal language)
	- Each bounded context has an explicit interface to interact with other bounded context, independed from other bounded context, internal models separate from external models  and internal change in models should not force external bounded context.
		- belong to a teeam
		- own repository and data store
		- contracts
		- Logical bounded context
		- Out of context
		- Ubiquitous (natural core language) Language (define key terms that domain expert and enginer uses to communicate)
	- Bounded Context 
		- Concepts from bounded context
		- Core concept forms ubiquitous language (rename concepts if needed)
		- Movee out of context concepts\models
		- Removing out of scope concepts
		- Single concepts indicate integration
			- integration with other bounded context

	- Identifying the core
		- 
	- Aggregation
		- Addition to bounded context
		- Combining services
		- Used in addition to decomposing
		- Reason for Reporting, Enhanced functionality, Usability for clients, Performane - when to decompose or aggregate		

	Unbounded approch 
		- overlaping language in the boundry

3. How to architect Asynchronous Microservics
============================================
> Event Based (Client <-.-> Message Broker <-.-> Service)
	. Competing workers pattern
	. Fanout pattern
	. Options
		> Transaction/action as an event
		> Messages using message brokers
		> Decouples client and service
		> Queuing Pattern
			> Competing workers Pattern
				- message queue listened by multiple service, each service competing for a message
			> Fanout Pattern
				- Each service listen by each service (message distributed to all other services, multiple task on same message)
 
> Async API call
	. Request acknowledge with callback
		> Incoming request with callback info.
		> Validate request
		> Return request accept status code
		> Start as background task
		> Return acknowledgement (return status using callback)


> Other Options
	- Hangfire (Dashboard)
	- 	


4. Architectural Patterns for Microservices
==========================================

Microservices principles
========================
> Independent Deployment
> Technology Agonstic
> Loosely Coupled
> Independent Changable
> Stateless API
> Support and honor contract 	


Architectrual Sytle
> Pragamatic REST (Extended unofficial version of REST)
	-- Not all about resource CRUD
	-- Action\Task based endpoints
	-- Verbs instead of nouns (ex: /api/offers/expirevouchers, noun: /api/offers)
	-- Query string or request body
	-- Response body for output
	-- Callback address for output
	-- Http Status code	

> HATEOS (True REST)
> SOAP
> RPC

>Facade Pattern (a single interface to sub systems, api request honured by facade)
>Proxy Pattern (a placeholder of another object, used to control access to other object, other object could be external api, doesn't contain business logic instead provide wrapper around Transformation, Validation, Security or Simplification)
>Stateless service pattern (not keeping state within service/server end, client maintain state, client send as request if required to process, benefits scalability, performance and availibility)

5. How to compose microservics?

Composition Patterns

	> Broker - Message brokers 
	> Aggregate Pattern-A service communicate with other servicee and aggregate response and return to client 
	> Chained Pattern (One service call another service, make it synchronous call)
	> Proxy Pattern (API Gateway)
	> Branch Pattern (branching and grouping services and returing response), could be one half is asynchronous and another is chained

6. How to achieve data consistency(ACID) across microservices.
	> Two phase commit pattern (ACID is mandatory,CAP Theorem: Choosing consistency)
		- Pattern for distributed architecture
			- Transaction manager manages transaction
		- Prepare phase
			- Transaction manager ask to prepare
		- Voting phase
			- Transaction manager receive votes
			- Issues a commit on all yes votes
			- Issues a rollback on any no vote
		- Caveats
			> Reliane on a transaction manager
			> No voting response
			> Commit failure after successful vote
			> Pending transactions lock resources
			> Avoid custom implementation
			> Has scaling out issues
			> Reduce througput
			> Anti-pattern
	> Saga Pattern (Trading atomicity over availability and consistency)
			> Split transaction into many requests
			> Track each request (Saga log, Saga execution coordinator (SEC)
			.--SEC--
				. Interprets and writes to the log
				. Sends out saga requests
				. Sends out saga compensation requests
				. Recovery Safe state vs unsafe state
				. Send on failure for all completed requets
				. Idempotent (easy with REST), each one sent one or many times

			> Failure management pattern
			> Compensate requets
		- Caveats
			> ACID: Compromise atomocity 
	> Eventual consistency pattern (Compromise ACID, Choosing availability)
			> Data will eventually be consistent based on BASE model
			> BASE vs ACID model
			> Avoid resource locking
			> Ideal for long running tasks
			> Prepared of inconsistency
			> Race conditions
			> Data Replication
			> Event based

07. Centralize access to microservices using an API Gateway
 	Why and how we need centralize access?
 	> API Gateway - why ?
 		- Since microservice is distributed architeture wheere it comes with lots of feature and benefits, there are some complexivty comes inheritnly that we can resolve with some pattern and practices.. some complexitity like
 		- Distributed functionality
 		- Distributed data
 		- Distributed security
 		- Complexity for client applications
 			- Consolicating API data
 			- Consolidatinng API functionalities
 			- Managing security
 		- Complicated customer experience
 			- APIs as a product
 	> API Gateway - Component to access APIs
 		- An API iteself
 		- Restful API
 		- Security/Authentication - take away complexity from backend complexity
 		- Traffic control
 		- Load balancing
 		- Tranformations
 		- Circuit Breaker/RetryPolicy/ Resilence/Caching/Logging and Monitoring
 		- Service Discovering/Reegistry
 		- Mask versioning /compaitability/migrating traffic in gradulway (control load on new release/ip control/block/restriction)
 		- 
 	Open source - api gateway -> Konghq.com

08. Split monolitihic database across microservices
	> Monolithic database
		- Provide ability to share daya easily
	> Why we should avoid
		- independently change
		- Independently deploy
		- Tight coupling
		- Harder to scaleout
		- Performance bottelneck

	> Microdatabase how?
		- Avoid data first design (Traditional approch, anti-pattern for microservices, Anemic CRUD based services, exposing internal data structures)	
		- Incorporate existing assets as service
		- Put legacy assets behind a facade and connect it to the core application 	
		- Function first design for loose coupling
			- Aim is not just to share data
			- APIs interfaces that provide functions, function defined by the scope of the service
			- application and its finction comes first use bounadd conteexts tool to define the boundry of the service
		- Microservice represents function
			- Code first approch to database design
			- Frameworks to support code first
			- Function of microservice defines data store technology

		- Pattern for database design
			- Sharing data using events
				- interested parties/subscriber store data locally
				- Local for data of interest
				- Data for joins
				- Avoids anemic CRUD services
				- Push is better than pull
				- Asynchronus communication/use of message brokers/Decoupled architecture

			- Store data as events
				-  alternative approch to store final state of the data
				-  State is stored as a series of events
				- An event record states what has changed
				- Replay events to get current state
				- Why? - use event sourcing for microservices
					- data is shared using event notifications
					- join back to the correct state of the record
				- Challenges
					- Regular snapshots to increase performance
					- Regular snapshot to decrease storage

			- Separate write model from the query model
				- CQRS - Why? - separtating the writting from reading
					- separating command and query (separating responsibilities, event notifications handled by command, reporting/functions handled by query, may using different models)

				- Separation of technologies
					- Service and storage

				- Challenges
					- Command and query database syncing


		- Greenfield database scenario (when there is no database)
			- Bounded context and code first tools
			- Avoiding modelling data first
			- Functions and ctracts over internal models
			- Approach and design patterns
			- CQRS to further split service and its database
				- Microservice for events(COMMAND)
				- Microservice for data (Query)
		- Brownfield databbase scenario
			- Migration strategy (monolothic to micro)
				Step 1: Indentify and pickout table havily shared across applications/services/functions, develop a service for the table which connect data from the table to the rest of the architecture using RESTful APIs.
				Step 2: Refactor application services to use endpoints instead of directly connecting to database directly
				Step 3: Move that table and move it into separate database/micro db for new microservice.
				Step 4: Follow on same steps for each tables in monolotihic db
			- Challenges
				- Event sourcing and CQRS (may need to wait until monolothic db converted into microservice)
				- Slow need strateegic planning
				- Refactoring existing applications and services
				- New service contracts
				- Use patterns to sheild table migration
				- Proxy class to fetch data from service
				- Replacing table joins
				- Multiple service calls
				- Local cache for events of interest to a join
				- Static data 
					- move to shared libraries

09. Resilency
	- Need for resiliency
		- Distributed Architecture
		- Fault tolerance 
		- Avoid cascading failure
		- Avoid exhausting resoure pools
		- Communication over a network
	- Types of failure
		- Hardware
		- Infrasture goes down
		- Communication layer
		- Dependencies 
		- Microservices

	- Patterns and approach
		- Timeouts
			. Instead of waiting forever, design must allow to configure timeout after waiting certain time and frees up calling thread.
			. Do not rely on default timeout values
			. Can be used with retries 
			. Allows to handle call failure
		- Circuit Breaker
			. Encourage fail fast, monitors for failure in downstream system, trips after reaching threashold.
				- Circuit is open
				- Returns an error without making call
				- Different threashold for diffrent issues
				- Reset after a number of tries
				- Way to default of degrade functionality
				- Avoids cascading failures
				- Always report: monitor and log state
				Tool: polly
		- Retry 
			. Momentary loss of connectivity
			. Temporary unnavailability of service
			. Timesout when service is busy
			. Service throttles accepted requested
			. Faults selft correct
			...Strategies
				. Retry after delay
				. Cancel
				. Log and monitor
				. Work with conjunction with circuit breakers

		- Bulkheads Design pattern (separate part of the system which may hinder critical component - as design principle to isolate microservice, ceiling of potential problem that system might face)

			. High level design pattern
				- Way of sealing off failure
				- Shipping bulkhead analogy
				- Avoid cascading failure
				- Separate from failed component
				- Ensure degraded functionality
			How -
				- Separation by criticality
				- Isolated microservices
				- Redundancy
				- Circuit breakeers for all synchonous calls
				- Rejecting calls using sheading 

		- Design for known failure
			- Look at previous failure causes
			- Architect away single point of failure
			- Use builhead pattern
		- Embrace failure
			- Circuit breakers/retries with monitoring
		- Fail fast
			- Fail fast for incoming request
			- Using timeout on outbounnd calls
		- Degrade or default functionalty
			- Circuit Breakers
			- Use caches


10. Microservice Comptaible
	
	USPs
	- Independently changeable
	- Independently deployable

	Need for compatibility 
	- Backward compataibility
	- Forward Compatibility

	Compatibility With
	- Client applications
	- Client services
	- Overall architecture
	- Third parties

	Incompatible Changes
	- Renaming operation/resourcees
	- Removing operation/resourcees
	- Optional data becomes mandatory
	- Adding a new validation/exception
	- Changing data constraints and types
	- Changing endpoints
	- Changinng security
	- Changing a standard
	- Renaming or removing data
	- Changing data format and types

	Comptaible Changes
	- Adding an operation/resources
	- Adding optional data
	- Making mandatory data optional
	- Reducing the constraints on data
	- Implementing an existing operation/resource
	- Redirection response codes
	- Adding additional data to the schema

	Versioning Strategies
	- Shared model and Separation of concern
		- API Layer have separate Contract model, UI aligned with this contract model
		- Api interacting domain layer using adapter and domain has their separate internal models
		- Domain layer interacting with data layer using Adapter, and data layer has thier own separate data models.
	- Version Strategy (consecutive numbers for endpoints/date/semantic version-major, mionr and patch)
		- Version identification pattern
		- Canonical versioning pattern
			- Http headers to sepcify versions
				. Accept header used for the request (client uses)
				. Content-type heaer used for the response (server response)
				. alternatively - use custom app specific headers
			- Request path - consecutive numbers within request path
				ex: myapi.net/v2/order
				ex: myapi.net/v20190402/order (alternative)
				ex: myapi.net/order?version=2 (alternative)

	- Unit testing layers (adapters) -> Integration Testing -> Contract testing -> Component testing - > Automated End to end testing
	-	

11. Define and document microservice contract
	> Consumer driven contract, taken consumer application as requirement based on types of request to fulfill the use case., Documented as computer driven language so that unit test and validate contract between both parties.
	
	> Resource based microservice, restful style where every endpoint considered as managing resource using http verb.

	> Action based microservice - Restful style microservice using actions(starting with verb on top of resource). ex: myapi.org/orders/cancelorder/{ordeerid}

	> Interface defination langugage -  way to document microservice contracts (before or after)
		. Computer readable format
		. Platform and language independent
		. Tools - generate test suits, mock APIs and documentation.
	> Swagger - based on open api specification, it's implementation of interface defination langugage




12. Implement micro services centralized logging

	> Distributed architecture but centralized logging
	. Consistent format (json/xml)
		- Shared libraries
		- Importance of transcation transperency using Structured format
			> Consistent metadata
				> Correlation Id
				> Date and Time
				> Host and app information
			> Logs (problem solving data)-> Logging API -> UI or Portal (Problem solving assistent)
			> Logging Levels
				. Info
					-Non-technical descriptive message
					- Simple enough for entire business
					- Key transaction milestones
				. Debug
					- Techinical milestones
					- calls and parameters
					- Response times and timeout stats
				. Error level
					- information on exceptions raised
					- Error information and number
					- call stack
			> Message
			> Decoupled from implementation, shared libraries
			> Avoid log buffering, log in sequence date to support sequence
			> Performance test logging frequence
			-- Making Secured access --
			> Controlled acceess to database and portal
			> HTTPS logging connections
			> In-line with data retention policies

13. Reporting from distributed microservice data
	Solution options for central reporting
	> Reporting service - calls other services and pull required data and store into local db for further reporting or process
	
	> Data push application - data push application work with each service and push to common db after some transformation for reporting, ensuring right data picked and converted into right format and pushed to right location. (converting transaction data into reporting data)

	> Reporting event subscriber - send reporting data to reporting system as series of event, microservice raises change event to message system and reporting service listen and store data into central reporting system (event sourcing) -> transform the data -> save into central reporting database.

	> Reporting events via Gateway
	- Client App/Api => API Gateway => Message Broker => Data Push App=> Event Sourcing=> Reporting Service => Central reporting database

 	> Using backup imports for reporting
 	- Backup database -> data import/jobs/app -> Reporting service -> central reporting database

 	> ETL and Data-warehouses
 	Orders+Customer => ETL Process (Extract => Transform => Load into datawarehouse) => Reporting service => central database

14. Cloud based APIs
	> High cohesion, autonomous, business domain, resilience, obserable and automation

	What are we designing

	Approch- 

	> IAAS (Infrastructure as a service)
		. Outsource Infrastructre
		. Cloud provider is the owner
		. Cloud provider responsible for maintenance and management. operations of the resources
		. Save time and money, control OS and above
		. Fireup extra environment
		. Migrate existing
		. Meet hardware capacity spikes


	> PAAS (Platform as a service)
		. Ideal for developers
		. On Demand delivery environment
		. Cloud provider responsible for maintenance and management
		. Tools and environment are setup
		. Managed using web browser
		. RAD at low cost
		. Can mix with IAAS
		. Focus on shipping
		
	> SAAS(Software as a service)		. 

	> Applications
	> Data
	> Runtime
	> Middleware
	> O/S
	> Vistualizztion
	> Servers
	> Storage
	> Netowrking


16. Managing microservices configuration
	> Parameters to affect behavior
	> Endpoint information
	> Connection string
	> Authentication information
	> Service configuration

	Solution options
		> Deployment servers
			. Central configuration that overrides config
			. Different for each enviornment
			. Configuration values as variables
				- Key, value pairs
				- Substituted in your app configuration files
				- Available in deployment definitions
			. Inteellignet configuration definitions
				- Scope for variables
				- Full deployment tool support
				- Library sets

		> Externalized configuration pattern
			. External storage
			. COnfiguration store interface
			. When to use - Share configuration, Zero downtime for configuration changes, Flexibility to store multiple versions, Easier management and control
			. Considerations - Security and handling errors, format of the data, caching expiration policy and use of caching
		> Configuration management tools
			. Infrastructure as code (IAC)
			. Chef, Puppet, Zookeeper, Consul etc.
		> Containers/Orchestration engines


17. Manage microservices registration and discovery
	> Client side discovery
		.  Calling client application responsible
			> Discovery: Finding service instance location
			> Load balancing requests across instances	
		. Client application uses service registry database
			> Use to retrieve a location of an instance
			> Location is registered on instance startup
			> Location is deregistered if an instance is removed
			> Periodically health/check instance existence
		. Drawbacks
			> Client applications are reliant of service registry
			> Client application must implement client side dicovery

	> Service side discovery
			. Client makes service requests via load balancer
				> Load balancer routes requests to a service
				> Load balancer uses a service registry database
			. Load balancer uses service registry database
				> Use to retrieve a location of an instance
				> Location is registered on instance startup
				> Location is deregistered if an innstance is removed
				> Periodically check instance existence
			. Advantage
				> Removes seervices discovery logic from client
			. Drawbacks
				> Load balancer another component to manage  
	> Service registration
			. Self registration pattern
			. Third-party registration pattern
	> Registration and discovery tools
			. Kubernetes
			. Docker swarm
			. Zookeeper
			. Consul
			. etc.		


18. Monitoring Microservices
	> Why the need to monitoring
		. Microservices complexity
		. High availability is a requirement
	> Causes of microservices outages
		. Deployment Issues
		. Delayed and long repair
		. Downstream dependency issues
	> Monitoring is the proactive solutionn
		. Know the state of all components
		. Know the state at eveery stage
		. Collect all statistics using metrics
	...Monitor Key Metrics...
	> Host and infrastrucres metrics => Monitoring Dashboard
		. State of hosts and infrastructure
			(ammount of resourcee available or consumed, state of resource like CPU, bandwidth, memory , disk)
		. State affects microservices
	> Monitor microservice metrics
		. Availability (health, response rate, average request/response/ success or failure of endpoints metrics)
		. Metrics around Errors, exception and timeouts
		.Endpoints exposing metrics data
		.Health of dependencies (metrics - central monitoring system of third party APIs)
		.Metrics at different stages
	>Business Metrics
		. Hidden issues and failure
		. Sudden dip or change in end user behaviour=> must feed into metrics or dashboard
		. Business metrics/stats around business
	>Monitor SLA metrics at all levels (Without them, you cannot know if your system is reliable, available or even useful)
		. Service level (throguput and uptime)
		. Service endpoint level (integration points)
		. Concurrent clients
		. Average response times
		. Impact of dependencies on SLAs
			- Monitor dependencies
			- Actionable alerts to fix SLA issues		
			- Communicate SLA issues early
	>Monitoring Dashboards and Alerting 
		. Display and collect metrics
		. Real-time
		. Centralized, accessible and standardized
		. Good dashboard-help into decision, showing alert associated with the metrics, and same can be used for testing and monitoring lower enviorment. - would be used tp correlate mmetrics to events in the system.
		. Bad dashboard - hard to interpret data
		. Alert - if significant change in metrics, up or down or passed configured threshold.
		. Alert - Configurable
		. Actionable - alerting (avoid non-actionable alert, alert should not for information purpose, alert should link document or url to solve/helping this issue or next step, and metrics link of monitoring dashboard snf used for threashold configuration (historical data, use for performance testing and load testing, expected trafffic for normal thresholds or larger than expecte traffic) .. help engineer to resolve problem

			- Normal state threshold
			- Warning state threshold (prevent outages)
			- Critical state threshold
	> Worth reading SRE (site reliability engineering)
		> Service-Level Objective (SLO)
			..prerequisite to success is availability
			..set a precise numerical target for system availability
			..Every service should have an availability SLO
			..keep servers at the right availability level and move the load where its most needed
		> Service-Level Agreement (SLA)
			..An SLA normally involves a promise to someone using your service that its availability
		> Service-Level Indicator (SLI)
			..the frequency of successful probes of our system. 
	> Monitoring Tools
		. StatsD
		. Graphite
		. New Relic
		. etc.

------

Data Management Patterns for Microservices
==========================================
> Database Per Service
In this pattern, each microservice manages its own data. What this implies is that no other microservice can access that data directly. Communication or exchange of data can only happen using a set of well-defined APIs. The success of this pattern hinges on effectively defining the bounded contexts in your application. For a new application or system, it is easier to do so. But for large and existing monolithic systems, it is troublesome.

>Shared Database
Shared database could be a viable option if the challenges surrounding Database Per Service become too tough to handle for your team.


>Saga Pattern
A Saga is basically a sequence of local transactions. For every transaction performed within a Saga, the service performing the transaction publishes an event. The subsequent transaction is triggered based on the output of the previous transaction. And if one of the transactions in this chain fails, the Saga executes a series of compensating transactions to undo the impact of all the previous transactions.

>API Composition
In this pattern, an API Composer invokes other microservices in the required order. And after fetching the results it performs an in-memory join of the data before providing it to the consumer.

>CQRS

An application listens to domain events from other microservices and updates the view or query database. You can serve complex aggregation queries from this database. You could optimize the performance and scale up the query microservices accordingly.

>Event Sourcing
In event sourcing, you store the state of the entity or the aggregate as a sequence of state changing events. A new event is created whenever there is an update or an insert. The event store is used to store the events.


What is preferred way of communication between microservices?
> Synchronous - Request/Response based
> Asynchronous - Event Based


What is the difference between Orchestration and Choreography in Microservices architecture? 

In Orchestration, we rely on a central system to control and call other Microservices to complete a task. 
Orchestration is a tightly coupled approach for integrating Microservices.
Orchestration is often implemented by synchronous calls.

In Choreography, each Microservice works like a State Machine and reacts based on the input from other parts.
Whereas, Choreography is a loose coupling approach. Choreography based systems are more flexible and easy to change than Orchestration based systems. Choreography is implemented by asynchronous calls. The synchronous calls are much simpler compared to asynchronous communication.

What are the issues in using REST over HTTP for Microservices? 

In general, REST over HTTP is one of the most popular implementations of Microservices. Some of the issues in using REST over HTTP for Microservices are as follows: In REST over HTTP, it is difficult to generate a client stub.  Some Web-Servers do not support all the HTTP verbs like- GET, PUT, POST, DELETE etc.   Due to JSON or plain text in response, performance of   over HTTP is better than SOAP. But it is not as good as plain binary communication.  There is an overhead of HTTP in each request for communication.  HTTP is not well suited for low-latency communication.  There is more work in consumption of payload. There may be overhead of serialization, deserialization in HTTP.

Can we create Microservices as State Machines? Yes, Microservices are independent entities that serve a specific context. For that context, a Microservice can work as a State Machine. In a State Machine, there are lifecycle events that cause change in the state of the system.

What is Reactive Extensions? Reactive Extensions is a design approach in which we collect results by calling multiple services and then compile a combined response. These calls can be synchronous or asynchronous, blocking or non-blocking. It is also known as Rx. Rx works opposite to legacy flows. It is very popular in distributed systems. You just react to the response received from another Microservice. Rx is based on a combination of Observer pattern, Iterator pattern and functional programming design approach. Some people call it functional reactive programming. But it can be used in Object Oriented programming as well.


What is a Consumer Driven Contract (CDC)? Consumer Driven Contract (CDC) is a pattern for developing Microservices that are used by external systems. In a Microservice, there is a provider that builds it, and there are one or more consumers that use the Microservice. Microservice. General approach is for Provider to specify its interfaces in an XML like document. But in Consumer Driven Contract, each consumer of a service provides and documents the interface it expects from Provider. In this way, a Provider can never introduce a breaking change if it keeps adhering to the Consumer Driven Contract. CDC has following characteristics: 
> Closed and complete: It represents the mandatory functionality that a consumer expects from the provider.  
> Union: These are union of the expectations from multiple consumers.  
> Stable and Immutable: It is a stable contract that cannot change without prior communication with the consumers. So it is considered immutable.

What is PACT? PACT is an open source tool to allow testing interactions between service providers and consumers in isolation against a contract. It is a tool to implement Consumer Driven Contract in Microservices. By using PACT you can test the consumer driven contracts between consumer and provider of a Microservice. It is an automated suite to test this interaction. It increases the overall reliability of Microservices integration in a complex system. Nowadays, Spring Cloud Contract is another tool for implementing Consumer driven contracts.

Why do we use Correlation IDs in Microservices architecture? In Microservices architecture, if there is a failure in one Microservice, then we may have to go to upstream Microservice to find the source of failure. Or we may have to go to downstream Microservice to find the impact of the failure. Each Microservice is an independent entity. Therefore, we need some relation between the error in a service as well as the error in its upstream/downstream services. To resolve this issue, we can use Correlation IDs. These are unique IDs like GUID that are passed from one service to another service during an API call. By using a GUID and failure message associated with it we can find the source of failure as well as the impact of failure.


How does HTTPS authentication work in Microservices? HTTPS is also known as HTTP Secure. This protocol ensures that all communication between Microservices is secure and encrypted. HTTPS is used for handling the transfer of confidential information between Microservices. In HTTPS, the client is assured that the Server it is talking to is a genuine server. To do so, HTTPS uses SSL certificates. Also encrypting the request and response helps in avoiding eavesdropping. SSL ensures that all communication is encrypted and decrypted in a secure way. The drawback of encryption is that it cannot be used in proxy caching.

What are Client certificates? Client certificate is another form of security that is implemented at Transport Layer instead of Application layer. Each client installs a X.509 certificate locally. This ensures that server can rely on the validity of client, if it has the right certificate. It is more complex than SSL, since it has to manage certificates at all clients.

What is Confused Deputy Problem in security context? In Microservices world, a Confused Deputy is a computer security problem. In this case one service acts as deputy and accepts authenticated requests from other clients to perform an action. Sometimes, one malicious client can hack the system and send a falsely authenticated request. Since the request is authenticated, deputy will do the same action for malicious client. This can breach the security of the system. E.g. Let say a service depends on barcode to get the price. If a client provides incorrect barcode, the service will provide incorrect price, since it relies on the authenticity of barcode. This can lead to incorrect billing for a customer.

What are the different points to consider for security in Microservices architecture? Implementing security in Microservices architecture has its own challenges. Whenever there are multiple Microservices and many interfaces between them, it is important to consider following aspects to implement correct security in Microservices architecture based system: Data in Motion,  Data at Rest.  Firewalls,  Operating system,  Logging,  Intrusion Monitoring, Network segregation.

What are the important Cross-Functional Requirements to consider during the design of a Microservice? Some of the important cross-functional requirements for design consideration are: Availability,  Durability of data,  Extensibility,  Fault Tolerance  Latency,  Maintainability,  Performance,  Resilience,  Robustness, Scalability,  Security Compliance,  Testability,  Usability  Integration Support.

How will you implement Caching in Microservice?

Caching is a performance improvement technique for getting query results from a service. It helps us in minimizing the calls to database while fetching results. In the Internet, HTTP protocol also uses caching to serve web pages faster with increased load. In Microservices, we can implement Caching in three ways. 

Client Side: In this case, information is cached at client side and service has to inform the client when the information needs to be replaced in cache. There is very less network traffic in this case.  

Proxy Side: In this case, there is a proxy server between client and server that maintains the cache. It can be used to cache results for multiple services with less overhead.  

Server Side: In this case, server provides caching with tools like Memcache or Redis. It is very opaque to clients. With multiple clients, this is the best strategy for caching.



How will you implement Service Discovery in Microservices architecture? In Microservices architecture, there are multiple Microservices that provide APIs for different purposes. At times, it is difficult for a Developer to find whether an API already exists or not. If there is already an API provided by another another Microservice, then there is no need to rewrite it. Here comes the need for Service Discovery. Following are some ways to implement Service Discovery: 

DNS: We can simply use Domain Naming System in such a way that it is intuitive to find a service. You can either use subdomains or a domain-naming template that provides convention for naming services.  

Apache Zookeeper: It is an open source software from Apache foundation for maintaining configuration information, naming, providing distributed synchronization and providing group services. It has a hierarchical namespace for storing information about services and their configuration.  

Consul: It is another REST based tool for dynamic service registry. You can use it to register a new service, add health-checks for a service etc.   

Eureka: Netflix has provided an open source system known as Eureka that provides load-balancing capabilities in addition to service discovery. It is preferred in a Java environment in AWS cloud.


What is an API Gateway? API Gateway is a service from Amazon in AWS cloud. This is used for creating, publishing, monitoring and maintaining secure APIs in cloud. It is an important part of Microservices architecture in AWS. This works as a "front door" for
applications to access resources in AWS cloud. API Gateway supports services at a large scale as well as concurrent API calls.  We can also create REST endpoints for existing services by using API Gateway.  It supports request throttling, traffic monitoring etc features to manage Microservices architecture very well.

How will you attain high availability with Microservice API gateway? There are many options in Amazon Web Services (AWS) to build a Microservices based system with high availability. We can use API Gateway to build the APIs. But these options are very helpful in creating a Highly Available service. Some of these options are: I. Elastic IPs: We can failover gracefully by using Elastic IPs in AWS. An Elastic IP is a static IP that is dynamically re-mappable. We can quickly remap and failover to another set of servers so that application traffic is routed to the new set of servers. It is also very useful when we want to upgrade from old to new version of software. II. Availability Zones: We can use multiple Availability Zones to introduce resiliency in AWS system. An Availability Zone is like a logical datacenter. By deploying application in multiple availability zones, we can ensure highly availability of a Microservice. III. Amazon Machine Image: We can maintain an Amazon Machine Image for a Microservice to restore and clone environments easily in a different Availability Zone. We can use multiple Database slaves across Availability Zones and setup hot replication with these Machine images. IV. Amazon CloudWatch: This is a real-time open source monitoring tool in AWS that provides visibility of a service in AWS cloud. We can take appropriate actions in case of hardware failure or performance degradation by setting alerts on CloudWatch. V. Auto scaling: We can maintain an auto-scaling group to maintain a fixed number of servers for a Microservice. In case of failure or performance degradation unhealthy Amazon EC2 instances are replaced by new ones.


What is the right approach to test a Microservice component that calls a legacy system?

First of all we have to determine, how are we calling legacy system from a Microservice. Is it a synchronous call or an asynchronous call? If we are calling it asynchronously, then it should not be an issue. We can simply stub the interface with legacy system and test the Microservice. If we are calling it synchronously, then we need to find the right testing strategy. Generally, it is not a good practice to call a legacy system from a Microservice synchronously. Calling a legacy system asynchronously is the preferred option. In case of synchronous communication we generally create an Adaptor service to communicate with the legacy system. This Adaptor service provides mock APIs that are called by Microservice. With an Adaptor, it is easier to test the legacy system, without worrying about any changes to it. Also it is a quick method of testing.

How will you store data in Microservice architecture? In Microservice architecture, each Microservice stores its data within itself. But that does not mean that each instance of same Microservice will have its own data store. For performance improvement, if we implement Sharding then each instance of same Microservice maintains its own shard. The data specific to a Microservice instance is stored in its shard. There are scenarios when we have to keep a common data-store between Microservices. This is generally done for Microservices that are computation intensive. In such a scenario, we implement a Microservice to cache the data that does not get updated very often.

What is a Circuit Breaker pattern in the context of Microservice? Circuit Breaker is a software design pattern similar to the one used in electrical devices. In an electrical device, we use circuit breaker to break the flow of current in an unforeseen situation. This ensures that the electrical device is not damaged due to an unexpected issue. In the context of Microservices, it can be implemented in various scenarios. If the number of requests to a Microservice increases beyond a threshold then circuit breaker becomes active and blocks extra calls. If the number of failed requests to a Microservice reach a threshold then circuit breaker becomes active and the requests are throttled for a period of time. After that circuit breaker sends a few requests to check if Microservice has recovered or not. In Microservices, sometimes circuit breaker stores and queues up the failed requests for retry at a later time. In certain cases, it is better to fail fast and give immediate response to clients for a synchronous request. Circuit breaker can also be used to implement Bulkhead design pattern in Microservices.

What is Bulkhead design pattern? Bulkhead is a software design pattern to create a stable and fault tolerant system. In this pattern, if one part of a system fails, then other parts keep functioning normally rather than bringing the whole system down. While designing a system, we create separate components in a watertight compartment like manner. If one component fails, we just close access to that component and try to fix it. Rest of the system keeps working business as usual (BAU). Netflix’s Hysterix library provides two ways to implement Bulkhead design pattern. Thread Isolation: In this case, if calls to a component fail then these are directed to thread pool with fixed number of threads.  Semaphore Isolation:  In this case, all callers acquire a permit before making a request. If caller doesn’t get permit then it means the component is down.  Currently Hysterix library is in maintenance mode. Nowadays, resilience4j is the open source library that provides fault tolerance functionality similar to Hysterix.

What is Idempotency of a Microservice operation? Idempotency refers to getting same result even after doing same operation multiple times in a Microservice. E.g. Let say, we have to send a reminder email to a customer for an upcoming bill payment on the due date. Our Microservice provides an idempotent Reminder oHow will you store data in Microservice architecture? In Microservice architecture, each Microservice stores its data within itself. But that does not mean that each instance of same Microservice

Powerhouse, Knowledge. Top 50 Microservices Interview Questions & Answers: Good Collection of Questions Faced in Architect Level Technical Interviews (updated 2020 version) . Kindle Edition. peration, so that even if we call this operation multiple times on the due date, it sends only one email per day. This will give better experience to customer instead of sending multiple reminders for same payment on same day.


