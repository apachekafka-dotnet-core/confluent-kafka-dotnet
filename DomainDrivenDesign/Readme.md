DDD Architecture
----------------------------


###	Domain Driven Design
- Tackling complexity in heart of software (business problem)
- Dicovering the domain architecture more than organizing the business logic
- Domain model remains a valied pattern to organize the business logic but other pattern can be used as well
	. Object Oriendted Model
	. Functional Model
	. CQRS Model
	. etc. 	

Without DDD
> Make sense of requirements
> Build a (relational) data model
> Identify relevent tasks and data tables
> Build a user interface
> Close to what users wanted 



###	Ubiquitous Language
Primary goal of avoiding misunderstanding and making the languge of the business as done in the organization in day to day all form of communication.(user stories, Request for changes (RFC)

### Bounded Context
-	Any elements of the ubiquitous language
-	If language is same, context is same.
-	Each context has its own architecture and implementation
-	Remove duplication
-	Integrate component with-in system


###	Context Mapping --
Context map is the diagram that provides a comprenshive view of the system being designed. ex: Core Domain, BackOffice, Club Site, Weather Forecasts

Direction of relationship - Upstream (U)and downstream (D) - Upstream influnce or request downstream for change, vice versa not true.

###	Conformist
Downstream context dependes on upstreeeam context, no negotiation possible
-	Customer/Supplier - Customer context dependends on supplier context, chances to raise concerns and hame them addressed in some way (there is room to negotiate)
-	Partner - Mutual dependency between the two context
-	Shared Kernel - Shared model that can;t be changed without consulting teams in charge of contexts that depends on it.
-	Anti-corruption layer - Additional layer giving the downstream context a fixed interface no matter what happens in the upstream context.
- 	Event Storming (identifyng obserbable event and domain in business)
	- Setup for Event Storming (collabrative scoping sessoion between technical/development team(ability to question and get the clarity on requirement, tecnical team comeup with scenario business people has not thought of the problem we are trying to solve) and business people(different part of the business (domain experts)))
		- Physical output can used to scope microservices

		- Takes sticky notes and Pens and plot on the wall or horizontal space (virtual wall : miro.com)

		Step 1: Plotting Event (Events within the domain) - (that happens in existing system or to purposed system)
			- Idenity first event where journey starts, use past tense in sticky notes always
			- Avoid technical langugae, and use business people language(Domain language) to describe event
			- The event happens in same time line, stack it vertically or put it to left to right timeline of events.
			- Slowing and questions will leead to identify all existing domain event and events which will need into purposed solution - in the order
		
		Step 2: Plotting Definitions and Concerns

			- Defing all confusing words, plotting concerns, risks, issues, new terminology - make everything consistent and everybody in session are aligned with understanding
			
			- Use different sticky notes on top of event - where the confusion is  - or put it on top of table vertically

			- Concerns( different sticky notes) -for further tracking and tracing down or solutioning discssion -
				- Group it 
				- Prioritize it by voting
				- If something needs to handle immediatly, can be address outside the session


		Before start of the event storming session put the sticky legend cleary like
		-	Domain Event (Orange)
		
		Step 3: Plotting Commands
		Every step question previous steps and get it refined, implement learning

		-	Commands are normally paired with events, they normally represnt actions, interactions and decision that leads to events paired with.

		- Attach user attached with actions(Command) with separate sticky notes

		- Event - specially carried out by stystem/software not by specific user

		- 

		Step 4: Plotting External System

		Put the external system vertical on top of event where other system is consuming or their event getting consumed in proposed system, showing the direction using arrows.

		- show command received
		- show events emitted
		- separate system
		- third party or internal system


		Step 5: Plotting time triggered Events

		-	No user involved
		-	Not a software based decision
		-	Passing of time (example: if proces taking more than expected time)

		Step 6: Plotting Policies

		-	Sitting between command and event, which controls and software based command/set of commands to control behavior, reactive logic that may leed to another command event...like before happening this event set of command must to happens.

		-	Connect policy with event
		- 	Polices places inside the software

		Step 7: Plotting Read models

		-	Plot data requirement to carried next/actions or decision

		- 	Emphasis data requirement on screen to do the action

		Step 8: Plotting Aggregates

		-	Bring command and event data/the data which requires state
		- 	Hold many instances of data/data holder
		- 	Root object holding pairing of command and event

		-	for the aggregate name, think of nouns that command and event holds
		- 	Ensure aggregate names are unqiue that represents owenership of the data

		- 	Way of grouping commands and events

		-	Emits events in response

		-	Commands act upon the data

		Step 9: Drawing Boundaries

		-	Initial scope of microservice
		-	Now remove the timeline from events where its plotted

		-	Group the aggregate
		-	Identify the bounded context
		-	Remove duplice aggregate name
		-	Group based on provider(route or external system), brint own dataset and technoloies and query language(natural grouping)

		- Draw logical name for each group/boundry

		- Use boundary for microservice scope

		(how to scope microservices using bounded contexts)

		Step 10: Consolidating and Decomposing

		-	Scoping microserice usimg transaction boundries/consistency
		-	Further decomposing based on availabiliy and uses to gain performance benefits
		-	

	- Find what causes the event
		- User action ?
		- asynchornous event ?
		- Another event ?

DDD Layers
========================

#### Domain
	- Object Model 
		- Context mapping is paramount
		- Modeling the domain through objeects is just one of the possible options
	- Data agnostic
		- The object model must be easy to persist
		- Persistenc, through, should not be primary concern
		- Primary concern is making sense of the business domain
	- Ubiquitous Language
		- Understand the language to understand the business 
		- Keep of business in sync with code (working on exposing behvaiour of domain, how domain works)-it's all about behavior much more than data.

####	Business Layer Pattern
	- Transcation Script Pattern
		- Actions - Each procedure handles a single task
		- Logical Transcation - end-to -end 
	- Table Module Pattern
		- Business rules in db/module contains all methods that process the data
	- Domain Model Pattern
		- Aggregated objects (data and behavior)
		- Persistence agnositic 
		- Paired with domain services

####	Domain Layer
	- Logic invariant to use-cases
	- Domain Model (this classes expected to expose data and behaviour)
		- Models for the business domain - Object oriented entity model, functional model
		- Guidelines for classes in an entity model - DDD conventions (factories, value types)
		- Data and behavior
		- Anemic model - plain data containers, behavior moved into service class.
	- Domain Model (Great for command, Requires fixes for persistence, Expose behavior to presentation)
		Module(s) - 
		. Entities
			. Need an identity
			. a class with properties
			. made of data behavior
			. contain domain logic, but not persistence logic

		. Value Objects 
			. Collection Of individual values
			. Immutable
			. More precise and accurate 

		.Aggregates
			. Cluster of associateed objects treated as one for data changes
			. Aggregate roots
			. Preseervee transactional integrity
			. A few individual entities constantly referenced together.
			. Work with fewer objects and coarse grained and with fewer relationship
			. Protect as much as possible the graph of entitites from outsider access
			. Ensure the state of child entities is always consistent
			. Actual boundaries of aggregates are deteermined by business rules
			. Aggregate 
				if entity only remain part of one entity example: if address entity only part of Customer Entity

				Common Responsibilities associated with an aggregateed root

				- Ensure encapsulate objects are always in a consistent state
				- Take case of persistence of all encapsulated objects
				- Cascade updates and deletions through the encacpsulated objects
				- Acceess to encapsulateed objects must always happen by navigation

				- One reposistory per root


			. Distinct Aggregate - ex: Address entity is part of multiple entry, like Customer, Security etc.



		. Persistencee Model 
			. Object-oriendteed model 1:1 with underlying relational data
			. Donest include business logic (except constraints/validation)
		. Domain Model
			. Object-oriented model for business logic
			. Persistable Model
			. No persistence logic inside

		 . Two choice - resistence to change
	 	. Object as plain data containers
	 	. Business logic belongs to other components
	 	or
	 	. Data and behavior in the same object
	 	.Business logic expreessed as the combination of objects

	 	. Anemic Models (Great of queries, no business rules in the class, Risk of gettting into incongruent state)
	 		- Anti-pattern because it takes behavior away from domain objects, entities only made of properties and required logic placed in service components, that actually orchestrate the application logic
	 	

	> If a developer can use an API the wrong way, he will.


	> Domain Services - Repositories, Proxies

		- Implement the domain logic that desn't belong to a particular aggregate and most likely span over multiple entities

		- Coordinate the activity of aggregates and repositorties with the purpose of implementing a business action

		- May consume services from the infrastures ex, email.

		- Names used in domain are stricly part on ubiquitous language and approved by domain expert and fullfill business requirement.

		 > Repository - in DDD, a class that handles persistence on hehalf of entities and ideally aggregate roots

> Why should you consider events - in a domain layer?

	- Not strictly necessary
	- Just a more effective and resilent way to express the intricacy of some rela-world business domains.
	- if not, violations of ubiquitous language
	- the adverb "when" in requirements indicates what to do whenn a given "event" is observed

	> benefits raise an event for domain-relevant facts
		- no need to have all handling in code in one place
		- raise of the event distinct from handling the event
		- can be handle multiple places 


#### Behavior
>	The way in which one acts or connducts onseself, especially towards others
	
	- Methods that express business processes involving the objects
	- Methods that invoke business actions to perform on the objects
	- Methods that validate the state of the objects


####	CQRS -(Command and Query Responsibility Segaragation)

	- Distinct optimization
	- Scalability potential
	- Simplified design
	- Hastle free enhancement

##### Command | Tell the application to do the something - Actions upon the application

	- Serializable method calls (Command request model should not be used as DTO model, 
			DTO model is just data contract, Backward compatibility)
	- Alter state, trigger reaction, push model
	- Deosn't return data
	- Benefits
	- DOmain Model
	- Table Module
	- Transaction Script Model

##### Query | Ask the application about something

	- Returns data
	- Doesn't alter state
	- O/RM Choice (can be readonly wrapper)
	- LINQ
	- Database in use

##### Events | Inform other application 
	- Result of reaction
	- Pull model


###Differene between Event and Command?

Command (start with imperative) - action against the backend that user or other component has requested.

Event(end-with-the-past) - Notify something has happend, tracking events we never misses what has happend and when happend, can be replayed on multiple place for multiple purpose.
>It's not that you dont need event, you just don't need events yet.

### Event Sourcing

It's about ensuring that all channges made to the application state during the entire lifeetime of application are stored as a sequence of events. Serialized events become data source of the application, keeping the track of changes in the system.

####	The two options
- Option#1
	+ Store current state
	+ Use events to log relevant facts
- Option#2
	+ Store events
	+ Build relevant snapshots of facts from stream of events

###	How CQRS can help
                    
Command  | Queries
------------- | -------------
Event  | Orders
Stream  | Table 

###	Event: types of subscription

- Volatile -> Call back a function whenever an event is written to a given stream until the subscription is stopped
- Catch-up -> Call back afunction from a given position up to the end and then turns into volatile
- Persistence -> Multiple consumers are guaranteed to receive at least once notification of events written possibly more.


#####	Key facts

- An event is something that has happened in the past
- Events are expression of the ubiquitous language
- Events are not imperative and are named using past tense verbs
- Have a persistent store for events
- Append-only, no delete - one
	+ Once store, it's immutable
	+ Constant data	
- Replay the (related) events to get to the last know statee of an entity
	+ Replay from the begiinning or a known point (snapshot)
	+ Replaying the event doesn't mean repeating the behavior - track everyting that happend at the time it happened - regardless of the effects it produced.
	+ Replay is not about repeating commands that generated events, it's looking into the generated event and extract info.
	+ Replay can be used to form and project any kind of state from abstracted lower level eventsource


###	Command: CQRS

#### Command & Query Storage Synchronization

+	Synchronization (Automatically up-to-date, Every command triggers sync updates)
+	Asynchronous (Eventually up-to-date, Every command triggers async updates) 
+	Schedule (Controlled staleness, A job runs periodically and updates the read storage)
+	On-Demand (Controlled up-to-date, Updates triggerd by requests (if old enough))

- Types
+	Regular CQRS = [Post-Redirect-Get web pattern]
+	Premium CQRS = Separating by two separate commands (command and query)-Replicate db - and return query response from 
replicated db
+	Delux CQRS
	+	Message Bus


Pillars
=======
##### DDD Analysis
+ Ubiquitous Language
+ Bounded Context

##### Layers
+ Avoid tiers (each bounded context as layers, layers over tiers)

##### Top down design 
+ Task-based and starting from UX

##### CQRS
+ Distinct Stacks 



