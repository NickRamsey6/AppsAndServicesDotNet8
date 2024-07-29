# Chapter 5 Multitasking and Concurrency

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

## Key Concepts

## Practice Questions
