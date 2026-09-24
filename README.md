🏨 Hotel Reservation System
![Image](https://img.shields.io/badge/TypeScript-5.7-blue?logo=typescript)
![Image](https://img.shields.io/badge/Node.js-22.x-green?logo=node.js)
![Image](https://img.shields.io/badge/Express-4.21-lightgrey?logo=express)
![Image](https://img.shields.io/badge/Swagger-OpenAPI%203.0-brightgreen?logo=swagger)
![Image](https://img.shields.io/badge/Auth-JWT%20Bearer-orange?logo=jsonwebtokens)
![Image](https://img.shields.io/badge/License-MIT-blue.svg)
A production-grade Hotel Reservation Management System engineered for hotel chains, multi-building hospitality complexes, room asset tracking, customer bookings, and role-based access control.
📑 Table of Contents
Overview
Key Features
Real Project Interface & Previews
System Architecture & UML Design
Database Design & Data Models
1. Conceptual ERD (Chen Notation)
2. Relational Schema (ER-to-Relational Mapping)
3. Data Dictionary
REST API Reference & Swagger
Authentication & Security Model
Technology Stack
Getting Started & Local Setup
👥 Project Team & Contributors
License
🌟 Overview
The Hotel Reservation System is an end-to-end hotel operations and booking platform. Originally architected in ASP.NET Core (Entity Framework Core) and migrated to Node.js & TypeScript, it manages the complete hierarchy of hospitality properties:
Hotels & Locations: Multi-branch support across major destinations (Nile, Red Sea, Alexandria, Luxor, Aswan, etc.).
Buildings & Wings: Physical architectural breakdown of hotel properties into distinct wings, towers, and beachfront blocks.
Room Inventory & Categorization: Fine-grained distinction between Standard Rooms and VIP Luxury Suites with specialized amenities.
Amenities & Components: Many-to-Many inventory matrix binding rooms with air conditioning, Wi-Fi 6, Jacuzzis, minibars, sound systems, and private balconies.
Reservation Lifecycle: Automated availability checking, conflict prevention, check-in/check-out date tracking, and cancellation handling.
Role-Based Security: Cryptographically signed JSON Web Tokens (JWT) securing administration and customer reservation operations.
⚡ Key Features
Multi-Property Organization: Manage multiple hotels and their individual building complexes or wings.
Dynamic Availability Engine: Instant verification of room states (Available vs Booked) preventing double bookings.
Room Type Specialization:
Standard Rooms: Optimized for corporate travelers, single guests, and leisure stays with essential utilities.
VIP Suites: Premium rooms featuring extended living areas, dedicated room service, and optional late checkout requests with fee calculations.
Interactive Swagger Documentation: Live, interactive OpenAPI 3.0 specification available at /swagger for real-time testing.
Web Management Dashboard: Built-in responsive web portal at / with real-time statistics, room filter filters, customer selection, and booking dispatch.
🖥 Real Project Interface & Previews
1. Web Management Dashboard
The operational portal allows administrators and front-desk personnel to monitor hotel capacity, inspect rooms with their equipped amenities, filter by hotel or category, and dispatch customer reservations in real time.
![Image](docs/portal-dashboard.svg)
2. Interactive Swagger / OpenAPI Specification
All REST API endpoints are self-documenting with interactive request execution, schema contracts, and Bearer Token authorization at /swagger.
![Image](docs/swagger-preview.svg)
🏗 System Architecture & UML Design
The system implements a strong Object-Oriented design pattern with separation of concerns:
IRoom Interface: Establishes common contracts for room number, pricing, cleaning routines, availability status, and check-in/check-out lifecycles.
Polymorphism: standardRoom and vipRoom implement IRoom. vipRoom extends capabilities with RoomServices(), LateCheckOut(), and LivingArea().
Inherited Reservations: standardReservation and vipReservation derive from the base Reservation class to encapsulate VIP-specific services.
Multiplicity & Aggregations: A Hotel aggregates multiple rooms and active reservations; a Building maps directly to its parent hotel.
UML Class Diagram (Design Specification)
![Image](docs/uml-class-diagram.svg)
code
Mermaid
classDiagram
    direction TB

    class Hotel {
        +int Id
        +string Name
        +List~IRoom~ rooms
        +List~Reservation~ reserv
        +method(type): type
    }

    class IRoom {
        <<Interface>>
        +int roomId
        +int RoomNumber
        +List~Component~ components
        +Building building
        +decimal price
        +isAvailable: bool
        +Clean(): void
        +CheckIn(): void
        +CheckOut(): void
    }

    class StandardRoom {
        +field: type
        +method(type): type
    }

    class VipRoom {
        +field: type
        +RoomServices(type): type
        +LateCheckOut(type): type
        +LivingArea(type): type
    }

    class Component {
        +int id
        +string name
        +string type
    }

    class Building {
        +int Id
        +string Location
    }

    class Customer {
        +string Name
        +string Username
        +string password
        +string Location
        +string Email
        +string Phone
        +method(type): type
    }

    class Reservation {
        +int id
        +DateTime FromDate
        +DateTime ToDate
        +IRoom room
        +Customer customer
        +Create(type): type
        +Edit(type): type
        +Cancel(type): type
    }

    class StandardReservation {
        -StandardRoom standardRoom
        +method(type): type
    }

    class VipReservation {
        -VipRoom vipRoom
        +VipServices: type
    }

    Hotel "1" --> "*" IRoom : offers
    Hotel "1" --> "*" Reservation : hosts
    IRoom <|.. StandardRoom : implements
    IRoom <|.. VipRoom : implements
    IRoom "1" o-- "*" Component : includes
    Building "0..1" --> "*" IRoom : contains
    Reservation <|-- StandardReservation : extends
    Reservation <|-- VipReservation : extends
    Customer "1" --> "*" Reservation : makes
    IRoom "1" --> "*" Reservation : assigned to
🗄 Database Design & Data Models
1. Conceptual ERD (Chen Notation)
The conceptual model captures the core business entities, their attributes, primary keys (underlined), and cardinalities.
![Image](docs/database-erd.svg)
Entity & Relationship Breakdown:
Relationship	Between Entities	Cardinality	Business Meaning
has	Hotel → Building	1 : N	A hotel possesses one or more distinct buildings or wings.
offers	Hotel → Room	1 : N	A hotel offers multiple accommodation rooms.
locatedIn	Room → Building	N : 1	Every room is situated within a specific building block.
includes	Room → Component	M : N	Rooms can have multiple amenities; amenities are shared across rooms.
hosts	Hotel → Reservation	1 : N	A hotel hosts reservations booked by guests.
makes	Customer → Reservation	1 : N	A customer places one or more bookings.
for	Room → Reservation	1 : N	A specific room is assigned to reservations across time windows.
isVIP	Room → VipRoom	1 : 1	Specialty 1-to-1 extension storing VIP suite attributes.
requestedFor	VipRoom → LateCheckOutRequest	1 : N	VIP suites allow guests to log late checkout requests.
2. Relational Schema (ER-to-Relational Mapping)
The normalized relational schema illustrates the primary key (PK) and foreign key (FK) dependencies ensuring referential integrity across the system.
![Image](docs/relational-schema.svg)
code
Mermaid
erDiagram
    HOTEL ||--o{ BUILDING : "has (HotelId)"
    HOTEL ||--o{ ROOM : "offers (HotelId)"
    BUILDING ||--o{ ROOM : "located in (BuildingId)"
    ROOM ||--o{ ROOM_COMPONENT : "has"
    COMPONENT ||--o{ ROOM_COMPONENT : "belongs to"
    HOTEL ||--o{ RESERVATION : "hosts (HotelId)"
    ROOM ||--o{ RESERVATION : "booked for (RoomId)"
    CUSTOMER ||--o{ RESERVATION : "makes (CustomerId)"

    HOTEL {
        int Id PK
        string Name
    }

    BUILDING {
        int Id PK
        string Location
        int HotelId FK
    }

    ROOM {
        int Id PK
        int RoomNumber
        int Price
        boolean IsAvailable
        string CheckIn
        string CheckOut
        int HotelId FK
        int BuildingId FK
    }

    COMPONENT {
        int Id PK
        string Name
        string Type
    }

    ROOM_COMPONENT {
        int RoomId PK,FK
        int ComponentId PK,FK
    }

    CUSTOMER {
        int Id PK
        string Name
        string Username
        string Password
        string Location
        string Email
        string Phone
    }

    RESERVATION {
        int Id PK
        string FromDate
        string ToDate
        string Status
        int HotelId FK
        int RoomId FK
        int CustomerId FK
    }
3. Data Dictionary
Table: Hotel
Column	Type	Constraints	Description
Id	INT	PK, IDENTITY	Unique identifier for the hotel
Name	NVARCHAR(100)	NOT NULL	Display name of the hotel
Table: Building
Column	Type	Constraints	Description
Id	INT	PK, IDENTITY	Unique identifier for the building/wing
Location	NVARCHAR(150)	NOT NULL	Physical location or block description
HotelId	INT	FK -> Hotel(Id)	Associated parent hotel
Table: Room
Column	Type	Constraints	Description
Id	INT	PK, IDENTITY	Unique identifier for the room
RoomNumber	INT	NOT NULL	Room number (e.g., 101, 202)
Price	INT	NOT NULL	Price per night in local currency
IsAvailable	BIT	NOT NULL, DEFAULT 1	Real-time booking availability flag
CheckIn	DATE	NULL	Scheduled check-in date
CheckOut	DATE	NULL	Scheduled check-out date
HotelId	INT	FK -> Hotel(Id)	Parent hotel reference
BuildingId	INT	FK -> Building(Id)	Building wing reference
RoomType	NVARCHAR(20)	CHECK ('Standard', 'VIP')	Category of the room
Table: Component (Amenities)
Column	Type	Constraints	Description
Id	INT	PK, IDENTITY	Unique identifier for the component/amenity
Name	NVARCHAR(100)	NOT NULL	Name (e.g., Air Conditioner, Jacuzzi)
Type	NVARCHAR(50)	NOT NULL	Category (Cooling, Luxury, Amenity)
Table: RoomComponent (Junction Table)
Column	Type	Constraints	Description
RoomId	INT	PK, FK -> Room(Id)	Room reference
ComponentId	INT	PK, FK -> Component(Id)	Component reference
Table: Customer
Column	Type	Constraints	Description
Id	INT	PK, IDENTITY	Unique identifier for the customer
Name	NVARCHAR(100)	NOT NULL	Full name of the customer
Username	NVARCHAR(50)	NOT NULL, UNIQUE	Login handle
Password	NVARCHAR(255)	NOT NULL	Encrypted credential
Location	NVARCHAR(100)	NULL	City or region of the customer
Email	NVARCHAR(100)	NOT NULL, UNIQUE	Contact email address
Phone	NVARCHAR(20)	NOT NULL	Contact telephone number
Table: Reservation
Column	Type	Constraints	Description
Id	INT	PK, IDENTITY	Unique identifier for the reservation
FromDate	DATE	NOT NULL	Start date of stay
ToDate	DATE	NOT NULL	End date of stay
Status	NVARCHAR(30)	CHECK ('Confirmed','Pending','Cancelled')	Current booking state
HotelId	INT	FK -> Hotel(Id)	Target hotel
RoomId	INT	FK -> Room(Id)	Target room
CustomerId	INT	FK -> Customer(Id)	Booking customer
📡 REST API Reference & Swagger
The API conforms to REST principles and provides both JSON responses and OpenAPI 3.0 documentation.
Method	Endpoint	Description	Auth Required
POST	/api/auth/authenticate	Authenticate user credentials and issue signed JWT token	❌
GET	/api/hotels	Retrieve list of all registered hotels	❌
GET	/api/hotels/:id	Get detailed hotel profile including attached wings & rooms	❌
GET	/api/buildings	Query all buildings (optional filter: ?hotelId=1)	❌
GET	/api/rooms	Retrieve all rooms with filters (hotelId, type, availableOnly)	❌
GET	/api/rooms/:id	Get detailed room information with all associated amenities	❌
GET	/api/standardrooms	Query exclusively Standard rooms	❌
GET	/api/viprooms	Query exclusively VIP luxury suites	❌
GET	/api/components	Retrieve catalog of all room components and amenities	❌
GET	/api/customers	Query list of customers	❌
GET	/api/reservations	Retrieve reservations (optional filter: hotelId, customerId)	❌
POST	/api/reservations	Create a new room booking (validates room availability)	❌
DELETE	/api/reservations/:id	Cancel an existing reservation and release room availability	❌
🔐 Authentication & Security Model
The system incorporates JWT (JSON Web Token) authentication based on symmetric key cryptography:
Token Claims: nameid (User ID), unique_name (Username), role (Admin or Customer).
Token Validity: 8 hours with configured Issuer and Audience checks.
Seed Test Accounts:
Administrator: admin / Admin@123 (Full system permissions)
Customer User: ahmed.salah / Pass@123 (Standard reservation operations)
💻 Technology Stack
Backend Runtime: Node.js 22 LTS with TypeScript 5.7
Web Framework: Express.js
API Documentation: Swagger UI (swagger-ui-express & OpenAPI 3.0)
Authentication: JWT (jsonwebtoken)
Data Architecture: Relational Entity Mapping (seeded with real Egyptian hospitality properties)
Frontend / Portal: Vanilla HTML5, modern CSS3 variables, and ES6 asynchronous fetch clients
🚀 Getting Started & Local Setup
1. Prerequisites
Node.js (v18.0.0 or higher recommended)
npm (comes with Node.js)
2. Installation
Clone the repository and install project dependencies:
code
Bash
git clone https://github.com/Gemy21/Hotel_Reservation_System.git
cd Hotel_Reservation_System
npm install
3. Environment Configuration
Create a .env file from the provided example:
code
Bash
cp .env.example .env
Default configuration:
code
Env
PORT=3000
HOST=0.0.0.0
JWT_SIGN_KEY=vI5KTQ78ohYriuvWKHY6COtZWXexHGLllxksOdZuya8
JWT_ISSUER=https://localhost:5001
JWT_AUDIENCE=https://localhost:5001
4. Running the Application
Start the development server with live reload:
code
Bash
npm run dev
Or build and run in production mode:
code
Bash
npm run build
npm start
5. Accessing the Application
Interactive Web Portal: http://localhost:3000
Interactive Swagger Documentation: http://localhost:3000/swagger
👥 Project Team & Contributors
This system was collaboratively designed, engineered, and delivered as a team project.
Avatar	Team Member	Primary Role & Responsibilities	GitHub Profile
🧑‍💻	Gamal Sameh (Team Lead)	System Architecture, Database Design & Backend API	@Gemy21
🧑‍💻	[Team Member Name]	[e.g., Database Modeling & SQL Schema]	@username
🧑‍💻	[Team Member Name]	[e.g., UML Design & Entity Relationships]	@username
🧑‍💻	[Team Member Name]	[e.g., API Controller Development & DTOs]	@username
🧑‍💻	[Team Member Name]	[e.g., Frontend Interface & Integration Testing]	@username
Tip: You can easily update this section with your teammates' names, assigned tasks, and GitHub profiles whenever you're ready!
📄 License
This project is licensed under the MIT License — feel free to use, modify, and distribute it for academic or commercial purposes.
