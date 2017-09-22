using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace ConsoleApp1.Chart3
{
    public class Test302
    {
        static void AsyncOperation(object state)
        {
            Console.WriteLine("F Operation state:{0}", state ?? "(null)");
            Console.WriteLine("F Worker thread id:{0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(TimeSpan.FromSeconds(2));
        }
        public static void Excute()
        {
            const int x = 1;
            const int y = 2;
            const string lambdaState = "lambda state 2";
            ThreadPool.QueueUserWorkItem(AsyncOperation);
            Thread.Sleep(TimeSpan.FromSeconds(1));
            ThreadPool.QueueUserWorkItem(AsyncOperation, "async state");
            Thread.Sleep(TimeSpan.FromSeconds(1));

            ThreadPool.QueueUserWorkItem(state =>
            {
                Console.WriteLine("Q1 Operation state :{0}", state);
                Console.WriteLine("Q1 Worker thread id:{0}", Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(TimeSpan.FromSeconds(2));
            }, "lambda state");

            ThreadPool.QueueUserWorkItem(_ =>
            {
                Console.WriteLine("Q2 Operation state :{0},{1}", x + y, lambdaState);
                Console.WriteLine("Q2 Worker thread id:{0}", Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(TimeSpan.FromSeconds(2));
            }, "lambda state");
            Thread.Sleep(TimeSpan.FromSeconds(2));
            Console.ReadKey();
        }
    }
}
