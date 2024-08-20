# Chapter 10 Building Serverless Nanoservices Using Azure Functions

## Key Concepts
* Azure Functions
* Building Serverless Services Using Azure Functions
* Responding to HTTP, Timer and Queue Triggers
* Binding to Queue and Blob Storage
* Deploying Azure Functions to the cloud

## Common Triggers for Azure Functions
* **HTTP**: This trigger responds to an incoming HTTP request, typically GET or POST.
* **Azure SQL**: This trigger responds when a change is detected on a SQL table.
* **Cosmos DB**: This trigger uses the Cosmos DB Change Feed to listen for inserts and updates.
* **Timer**: This trigger responds to a scheduled time occurring. It does not retry if a function fails. The function is not called again until the next time on the schedule.
* **SignalR**: This trigger responds to messages sent from Azure SignalR Service.
* **Queue** and **RabbitMQ**: These triggers respond to a message arriving in a queue ready for processing.
* **Blob Storage**: This trigger responds to a new or updated **binary language object (Blob)**. 
* **Event Grid** and **Event Hub**: These triggers respond when a predefined event occurs.

## Common Bindings for Azure Functions
* **Azure SQL**: Read or Write to a table in a SQL Server database.
* **Blob Storage**: Read or Write to any file stored as a BLOB.
* **Cosmos DB**: Read or Write documents to a cloud-scale data store.
* **SignalR**: Receive or make remote method calls.
* **HTTP**: Make an HTTP request and receive the response.
* **Queue** and **RabbitMQ**: Write a message to a queue or read a message from a queue.
* **SendGrid**: Send an email message.
* **Twillio**: Send an SMS message.
* **IoT hub**: Write to an internet-connected device.

## NCRONTAB Expressions
| Expression | Description |
|------------|-------------|
| 0 5 * * * * | Once every hour of the day at minute 5 of each hour. |
| 0 0,10,30,40 * * * * | Four times and hour - at minutes 0, 10, 30 and 40 during every hour. |
| * * */2 * * * | Every 2 hours. |
| 0, 15 * * * * * | At 0 and 15 seconds every minute. |
| 0/15 * * * * * | At 0, 15, 30, and 45 seconds every minute, aka every 15 seconds. |
| 0-15 * * * * * | At 0, 1, 2, 3, and so on up to 15 seconds past each minute, but not 16 to 59 seconds past each minute. |
| 0 30 9-16 * * * | Eight times a day - at hours 9:30 AM, 10:30 AM and so on up to 4:30 PM. |
| 0 */5 * * * * | 12 times an hour - at second 0 of every 5th minute of every hour. |
| 0 0 */4 * * * | 6 times a day - at second 0 of every 4th hour of every day. |
| 0 30 9 * * * | 9:30 AM every day. |
| 0 30 9 * * 1-5 | 9:30 AM every work day. |
| 0 30 9 * * Mon-Fri | 9:30 AM every work day. |
| 0 30 9 * Jan Mon | 9:30 AM every Monday in January. |

## Practice Questions
1. What is the difference between the in-process and isolated worker models for Azure Functions?  
**Answer: Isolated functions run in their own process and give you full control over the Main entry point, and additional features like invocation middleware. In-process hosting model requires your Azure function be loaded alongside other code to target a predefined version of an LTS release. Isolated can use any version of .NET**
2. What attribute do you use to cause a function to trigger when a message arrives in a queue?  
**Answer: [QueueTrigger]**
```
public static async Task Run(
    [QueueTrigger("checksQueue")] QueueMessage message, ILogger log)
```
3. What attribute do you use to make a queue available to send messages to?  
**Answer:[Queue]**
```
public static async Task<IActionResult> Run(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, [Queue("checksQueue")] ICollector<string> collector, ILogger log)
```
4. What schedule does the following NCRONTAB expression define? 0 0 */6 * 66  
**Answer: Staurdays (the last 6 digit) during June (the second to last 6 digit) on any day of the month at zero minutes and seconds (first and second 0's) of every 6 hours (*/6)**
5. How can you configure a dependency service for use in a function?  
**Answer: Create a class that inherits from FunctionStartup class and override its Configure method. Add the [FunctionsStartup] assembly attribute to specify the class name registered for startup. Add services to the IFunctionsHostBuilder instance passed to the method**
