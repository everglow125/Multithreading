using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace ConsoleApp1.Chart3
{
    /// <summary>
    /// 在线程池中使用等待事件处理器及超时
    /// </summary>
    public class Test305
    {
        static void RunOperations(TimeSpan workerOperationTimeout)
        {
            using (var evt = new ManualResetEvent(false))
            using (var cts = new CancellationTokenSource())
            {
                Console.WriteLine("Registering timeout operations...");
                var worker = ThreadPool.RegisterWaitForSingleObject(
                    evt, (state, isTimedOut) =>
                    WorkerOperationWait(cts, isTimedOut), null, workerOperationTimeout, true);
                Console.WriteLine("Starting long running operation...");
                ThreadPool.QueueUserWorkItem(_ => WorkerOperation(cts.Token, evt));
                Thread.Sleep(workerOperationTimeout.Add(TimeSpan.FromSeconds(2)));
                worker.Unregister(evt);
            }
        }

        static void WorkerOperation(CancellationToken token, ManualResetEvent evt)
        {
            for (int i = 0; i < 6; i++)
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            evt.Set();
        }

        static void WorkerOperationWait(CancellationTokenSource cts, bool isTimeOut)
        {
            if (isTimeOut)
            {
                cts.Cancel();
                Console.WriteLine("Worker operation timed out and was canceled");
            }
            else
            {
                Console.WriteLine("Worker operation succeded");
            }
        }

        public static void Excute()
        {
            RunOperations(TimeSpan.FromSeconds(5));
            RunOperations(TimeSpan.FromSeconds(7));
            Console.ReadKey();
        }
    }
}
