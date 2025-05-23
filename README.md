# Order Management System

#Project Setup
The solution is broken down into several small projects.
- OrgerMgt.API (main API project)
- OrderMgt.DatabaseUpdater (an app for inserting data into the database)
- OrderMgt.IntegrationTests (Integration tests for the project)
- OrderMgt.Model (DB entities/models)
- OrderMgt.UnitTests (Unit tests for the project)

#Installation
The API project has a connection string in the appsettings, which must be updated before running the migrations.
The DatabaseUpdater project also has a connection string, this must be pointed to your new DB before it can insert data into the database.

## User Accounts
  Two user accounts will be created after the data is inserted: user@demo.com and admin@demo.com, both using the password "test1234".
## Security
  The API has been implemented with JWT therefore, a token has to be generated using the AuthenticationController -> Login(user@demo.com,test1234).
  Swagger has been configured to accept the JWT token, allowing you to access the endpoints with it.
  Exception handling has been implemented globally in the API.

#Approach to API creation
1. Define the API scope and establish expected data flows and interactions.
2. Design the data models, repositories, services, and API endpoints.
3. Implement security (JWT) - Authorization on the endpoints just checks if a user is authenticated, and no role or policy-based authorization has been implemented.
4. API Optimization consideration - for a production-level API, caching mechanisms like Redis is recommended, indexing all database tables, and implementing pagination for end points that retrieve large datasets.
5. Testing and Documentation - API documentation using Swagger has been implemented, and some controllers have been fully commented; all services and entities have also been commented. Test projects have been added for Units tests and Integration tests respectively.

