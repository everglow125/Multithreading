using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace ConsoleApp1.Chart2
{
    /// <summary>
    /// Barrier 组织多个线程及时在某个时刻碰面
    /// </summary>
    public class Test207
    {
        static Barrier _barrier = new Barrier(2,
            b => Console.WriteLine("End of phase {0}", b.CurrentPhaseNumber + 1));
        static void PlayMusic(string name, string message, int seconds)
        {
            for (int i = 1; i < 3; i++)
            {
                Console.WriteLine("--------------------------------------------");
                Thread.Sleep(TimeSpan.FromSeconds(seconds));
                Console.WriteLine("{0} starts to {1}", name, message);
                Thread.Sleep(TimeSpan.FromSeconds(seconds));
                Console.WriteLine("{0} finishes to {1}", name, message);
                _barrier.SignalAndWait();
            }
        }
        public static void Excute()
        {
            var t1 = new Thread(() => PlayMusic("the guitarist", "play an amazing solo", 5));
            var t2 = new Thread(() => PlayMusic("the singer", "sing his song", 2));
            t1.Start();
            t2.Start();
            Console.ReadKey();
        }
    }
}
