using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 线程死锁
{
    class Program
    {

        DL d = new DL();

        static void Main(string[] args)
        {
            Program m = new Program();
            Thread t1 = new Thread(new ThreadStart(m.Run1));
            t1.Start();
            Thread t2 = new Thread(new ThreadStart(m.Run2));
            t2.Start();

            Console.ReadKey();
        }
        public void Run1()
        {
            this.d.First(10);
        }

        public void Run2()
        {
            this.d.Second(10);
        }
    }

    class DL
    {
        int field1 = 0;
        int field2 = 0;
        private object lock1 = new int[1];
        private object lock2 = new int[1];

        public void First(int val)
        {
            lock (lock1)
            {
                Console.WriteLine("First: Acquired lock 1: "
                    + Thread.CurrentThread.GetHashCode() + " Now Sleeping.");
                //Try commenting Thread.Sleep()
                Thread.Sleep(1000);
                Console.WriteLine("First: Acquired lock 1: "
                    + Thread.CurrentThread.GetHashCode() + " Now wants lock2.");

                lock (lock2)
                {
                    Console.WriteLine("First: Acquired lock 2: "
                        + Thread.CurrentThread.GetHashCode());
                    field1 = val;
                    field2 = val;
                }
            }
        }

        public void Second(int val)
        {
            lock (lock2)
            {
                Console.WriteLine("Second: Acquired lock 2: "
                    + Thread.CurrentThread.GetHashCode());

                lock (lock1)
                {
                    Console.WriteLine("Second: Acquired lock 1: "
                        + Thread.CurrentThread.GetHashCode());
                    field1 = val;
                    field2 = val;
                }
            }
        }
    }

    #region 死锁
        //在Deadlock.cs 中，线程t1调用First()方法获得lock1， 然后睡眠1秒钟。
        //同时线程t2调用Second()方法并获得lock2.然后在尝试在同一方法内获得lock1.但是线程1已经拥有lock1, 所以线程t2不得不等待线程1释放lock1.当线程t1醒来后，它尝试获得lock2。
        //而lock2已经被线程t2获得所以线程t1只能等待线程t2释放它。结果是发生了死锁而程序卡死。
        //把方法First()中的Thread.Sleep()方法注释掉不会导致死锁，至少是个临时解决方案。
        //因为线程t1会在线程t2之前获得lock2.但是在真实场景中，Thread.Sleep()时发生的可能是一个连接数据库操作，导致线程t2在线程t1之前获得lock2, 最终结果也是死锁。
        //这个例子告诉我们在任何多线程应用程序中设计出一个好的锁定方案是多么重要！
        //一个好的锁定方案可能通过让所有线程运行在一个定义好的行为中以合作方式获得锁。
        //在上面的例子中，线程t1不应该请求锁除非它被线程t2释放, 反之亦然。
        //这些决定取决于特定应用场景而不具有一般性。
        //测试各种锁定方案是同等重要的，因为死锁通常发生在那些缺少压力和功能性测试的系统中。
    #endregion
}
