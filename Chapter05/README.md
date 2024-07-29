# Chapter 5 Multitasking and Concurrency

## Key Concepts

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

## Practice Questions
