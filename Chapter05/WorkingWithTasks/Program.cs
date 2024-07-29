using System.Diagnostics; // To use stopwatch

OutputThreadInfo();
Stopwatch timer = Stopwatch.StartNew();
#region Running methods synchronously on one thread
//SectionTitle("Running methods synchronously on one thread.");
//MethodA();
//MethodB();
//MethodC();

#endregion

#region Running methods asynchronously on multiple threads
//SectionTitle("Running methods asynchronously on multiple threads.");

//Task taskA = new(MethodA);
//taskA.Start();
//Task taskB = Task.Factory.StartNew(MethodB);
//Task taskC = Task.Run(MethodC);

//Task[] tasks = { taskA, taskB, taskC };
//Task.WaitAll(tasks);

#endregion


#region Passing the result of one task as an input into another
//SectionTitle("Passing the result of one task as an input into another.");

//Task<string> taskServiceThenSProc = Task.Factory
//    .StartNew(CallWebService) // returns Task<decimal>
//    .ContinueWith(previousTask => // returns Task<string>
//        CallStoredProcedure(previousTask.Result));

//WriteLine($"Result: {taskServiceThenSProc.Result}");

//WriteLine($"{timer.ElapsedMilliseconds:#,##0}ms elapsed.");

#endregion

#region Nested and child tasks
SectionTitle("Nested and child tasks");

Task outerTask = Task.Factory.StartNew(OuterMethod);
outerTask.Wait();
WriteLine("Console app is stopping");

#endregion