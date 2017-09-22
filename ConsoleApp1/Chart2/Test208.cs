using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApp1.Chart2
{
    /// <summary>
    /// ReaderWriterLockSlim 在多线程中对一个集合进行读写操作
    /// </summary>
    public class Test208
    {
        static ReaderWriterLockSlim _rw = new ReaderWriterLockSlim();
        static Dictionary<int, int> _items = new Dictionary<int, int>();
        static void Read()
        {
            Console.WriteLine("Reading contents of a dictionary");
            while (true)
            {
                try
                {
                    _rw.EnterReadLock();
                    foreach (var key in _items.Keys)
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(0.1));
                    }
                }
                finally
                {
                    _rw.ExitReadLock();
                }
            }
        }
        static void Write(string threadName)
        {
            while (true)
            {
                try
                {
                    int newKey = new Random().Next(250);
                    _rw.EnterUpgradeableReadLock();
                    if (!_items.ContainsKey(newKey))
                    {
                        try
                        {
                            _rw.EnterWriteLock();
                            _items[newKey] = 1;
                            Console.WriteLine("New key {0} is added to a dictionary by a {1}", newKey, threadName);
                        }
                        finally
                        {
                            _rw.ExitWriteLock();
                        }
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(0.1));
                }
                finally
                {
                    _rw.ExitUpgradeableReadLock();
                }
            }

        }
        public static void Excute()
        {
            new Thread(Read) { IsBackground = true }.Start();
            new Thread(Read) { IsBackground = true }.Start();
            new Thread(Read) { IsBackground = true }.Start();

            new Thread(() => Write("Thread 1")) { IsBackground = true }.Start();
            new Thread(() => Write("Thread 2")) { IsBackground = true }.Start();
            Thread.Sleep(TimeSpan.FromSeconds(30));
            Console.ReadKey();
        }
    }
}
