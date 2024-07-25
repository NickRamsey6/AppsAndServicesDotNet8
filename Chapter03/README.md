# Chapter 3 Building Entity Models for SQL Server Using EF Core

## Key Concepts
* Executing simple queries and processing the results using the slower but more object-oriented EF Core
* Configuring and deciding between three mapping strategies for type hierarchies
* Implementing calculated properties on entity creation

## Practice Questions
1. What can the dotnet -ef tool be used for?  
**Answer: Performing design-time development tasks, like creating or applying migrations and generating a model from an existing database.**
2. What type would you use for the property that represents a table?  
**Answer: DbSet<<T>T> where T is the entity model type in the table.**
3. What type would you use for the property that represents a one-to-many relationship?  
**Answer: ICollection<<T>T> where T is the entity model type in the related table.**
4. What is the EF Core convention for primary keys?  
**Answer: ID, Id or ClassNameID or ClassNameId are assumed to be the primary key.**
5. Why might you choose the Fluent API in preference to annotation attributes?  
**Answer: You might choose Fluent API when you want to keep your entity classes free from extraneous code that is not needed in all scenarios.**
6. Why might you implement the IMaterializationInterceptor interface in an entity type?  
**Answer: EF Core interceptors enable interception, modification, and/or suppression of EF Core operations. The IMaterializationInterceptor interface allows interception before and after en entity class is created, and before and after properties of that instance are initialized. This enables setting properties or calling methods needed for validation, computed values, or flags, using a factory to create instances, and creating a different entity instance than EF would normally create, such as an instance from a cache.**