using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApp1.Chart2
{
    /// <summary>
    /// 使用Mutex类 只对一个线程授予共享资源的独占访问
    /// </summary>
    public class Test202
    {
        public static void Excute()
        {
            const string MutexName = "CSharpThreadingCookbook";
            using (var m = new Mutex(false, MutexName))
            {
                if (!m.WaitOne(TimeSpan.FromSeconds(5), false))
                {
                    Console.WriteLine("Second instance is running");
                }
                else
                {
                    Console.WriteLine("Running");
                    Console.ReadLine();
                    m.ReleaseMutex();
                }
            }
            Console.ReadKey();
        }
    }
}
