# Chapter 1 Introducing Apps and Services with .NET

## Key Concepts
* Intro to the app and service technologies that you will learn about in this book
* Setup dev environment
* Where to look for help

## Pros and Cons of Common Service Technologies
| Feature | Web API | OData | GraphQL | gRPC | SignalR |
|---------|---------|-------|---------|------|---------|
| Clients can request just the data they need. | No | Yes | Yes | No | No |
| Minimum HTTP version | 1.1 | 1.1 | 1.1 | 2.0 | 1.1 |
| Browser support | Yes | Yes | Yes | No | Yes |
| Data format | XML, JSON | XML, JSON | GraphQL | Binary | Varies |
| Service documentation | Swagger | Swagger | No | No | No |
| Code generation | Third-party | Third-party | Third-party | Google | Microsoft |
| Caching | Easy | Easy | Hard | Hard | Hard |

## Service Scenarios and the Recommended Implementation Technology
| Scenario | Recommendation |
|----------|----------------|
| Public service | HTTP/1.1-based services are best for services that need to be publicly accessible, especially if they need to be called from a browser or mobile device. |
| Public data service | OData and GraphQL are both good choices for exposing complex hierarchical datasets that could come from different data stores. OData is designed and supported by Microsoft via official .NET packages. GraphQL is designed by Facebook and supported by third party packages. |
| Service-to-service | gRPC is designed for low-latency and high throughput communication. gRPC is great for lightweight internal microservices where efficiency is critical. |
| Point-to-point real time communication | gRPC has excellent support for bidirectional streaming. gRPC services can push messages in real time without polling. SignalR is designed for real-time communication of many kinds, so it tends to be easier to implement than gRPC although it is less efficient. |
| Broadcast real-time communication | SignalR has great support for broadcasting real-time communication to many clients. |
| Polygot environment | gRPC tooling supports all popular development languages, making gRPC a good choice for multi-language and platform environments. |
| Network-bandwidth-constrained environment | gRPC messages are serialized with Protobuf, a lightweight message format. A gRPC message is always smaller than an equivalent JSON message. |
| Serverless nanoservice | Azure Functions do not need to be hosted 24/7 so they are a good choice for nanoservices that usually do not need to be running constantly. Amazon Web Services (AWS) Lambdas are an alternative. |

## Practice Questions
1. Why is it good practice to add the following setting to your project files? And when should you not set it?  
**Answer: Enabling this forces the developer to fix any issues in their code that are giving warnings before the code will compile and run. Do not use with .NET7.** 
2. Which service technology requires a minimum HTTP version of 2?  
**Answer: gRPC**
3. In 2010, your organization created a service using .NET Framework and WCF. What is the best technology to migrate it to and why?  
**Answer: Two options: use the Core WCF open-source project or re-implement the service and client using gRPC.**
4. Which code editor or IDE should you install for .NET development?  
**Answer: VS or VS Code or whatever else you want.**
5. What should you be aware of when creating Azure resources?  
**Answer: Price**