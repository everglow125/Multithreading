using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace ConsoleApp1.Chart1
{
    /// <summary>
    /// 处理异常
    /// </summary>
    public class Test11
    {
        static void BadFaultyThread()
        {
            Console.WriteLine("Starting a faulty thread...");
            Thread.Sleep(TimeSpan.FromSeconds(2));
            throw new Exception("Boom");
        }
        static void FaultyThread()
        {
            try
            {
                Console.WriteLine("Starting a faulty thread...");
                Thread.Sleep(TimeSpan.FromSeconds(1));
                throw new Exception("Boom!");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception handled :{0}", ex.Message);
            }
        }
        public static void Excute()
        {
            var t = new Thread(FaultyThread);
            t.Start();
            t.Join();
            try
            {
                t = new Thread(BadFaultyThread);
                t.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("We won't get here!");
            }
            Console.ReadKey();
        }
    }
}
