using System.Diagnostics; // To use stopwatch

OutputThreadInfo();
Stopwatch timer = Stopwatch.StartNew();

//SectionTitle("Running methods synchronously on one thread.");
//MethodA();
//MethodB();
//MethodC();

SectionTitle("Running methods asynchronously on multiple threads.");

Task taskA = new(MethodA);
taskA.Start();
Task taskB = Task.Factory.StartNew(MethodB);
Task taskC = Task.Run(MethodC);

WriteLine($"{timer.ElapsedMilliseconds:#,##0}ms elapsed.");