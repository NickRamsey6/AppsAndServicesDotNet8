# Chapter 5 Multitasking and Concurrency

## Key Concepts
* Defining and starting a task
* Waiting for one or more tasks to finish
* Controlling task completion order
* Synchronizing access to shared resources
* The magic behind async and await

## Task Class Wait Methods
| Method | Description |
|--------|-------------|
| t.Wait() | This waits for the task instance named t to complete execution. |
| Task.WaitAny(Task[]) | This waits for any of the tasks in the array to complete execution. |
| Task.WaitAll(Task[]) | This waits for all the tasks in the array to complete execution. |

## Methods to Create a Task in Various Scenarios
| Method | Description |
|--------|-------------|
| FromResult<T<T>>(TResult) | Creates a Task<TResult<T>> object whose Result property is the non-task result and whose Status property is RanToCompletion. |
| FromException<TResult<T>>(Exception) | Creates a Task<TResult<T>> that's completed with a specified exception. |
| FromCanceled<TResult<T>>(CancellationToken) | Creates a Task<TResult<T>> that's completed due to cancellation with a specified cancellation token. |

## Synchronization Types
| Type | Description |
|------|-------------|
| ReaderWriterLock, ReaderWriterLockSim | These allow multiple threads to be in **read mode**, one thread to be in **write mode** with exclusive ownership of the write lock, and one thread that was read access to be in **upgradeable read mode**, from which the thread can upgrade to write mode without having to relinquish its read access to the resource. |
| Mutex | Like Monitor, this provides exclusive access to a shared resource, except it is used for inter-process synchronization. |
| Semaphore, SemaphoreSlim | These limit the number of threads that can access a resource or pool of resources concurrently by defining slots. This is known as **resource throttling** rather than **resource locking**.
| AutoResetEvent, ManualResetEvent | Event wait handles allow threads to synchronize activities by signalling each other and by waiting for each other's signals. |

## Common Types that Support Multitasking
| Type | Methods |
|------|---------|
| DbContext<T<T>> | AddAsync, AddRangeAsync, FindAsync, and SaveChangesAsync |
| DbSet<T<T>> | AddAsync, AddRangeAsync, ForEachAsync, SumAsync, ToListAsync, ToDictionaryAsync, AverageAsync, and CountAsync |
| HttpClient | GetAsync, PostAsync, PutAsync, DeleteAsync, and SendAsync |
| StreamReader | ReadAsync, ReadLineAsync, and ReadToEndAsync |
| StreamWriter | WriteAsync, WriteLineAsync, and FlushAsync |

## Practice Questions
1. What information can you find out about a process?  
**Answer: The memory it uses and what threads are allocated to it.**
2. How accurate is the Stopwatch class?  
**Answer: Not very**
3. By convention, what suffix should be applied to a method that returns Task or Task<T<T>>?  
**Answer: Async**
4. To use the await keyword inside a method, what keyword must be applied to the method declaration?  
**Answer: Async**
5. How do you create a child task?  
**Answer: Call the Task.Factory.StartNew method with the TaskCreationOptions.AttachToParent option.**
6. Why should you avoid the lock keyword?  
**Answer: The lock keyword can cause deadlocks because it does not allow you to specify a timeout.**
7. When should you use the Interlocked class?  
**Answer: To modify integers and floating point numbers that are shared between multiple threads.**
8. When should you use the Mutex class instead of the Monitor class?  
**Answer: When you need to share a resource across process boundaries. Monitor only works on resources inside the current process.**
9. What is the benefit of using async and await in a website or web service?  
**Answer: Improved scalability.**
10. Can you cancel a task? If so, how?  
**Answer: Yes, by defining a cancellation method for your task.**
