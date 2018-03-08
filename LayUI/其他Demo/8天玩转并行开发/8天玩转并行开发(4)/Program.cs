using System.Collections.Concurrent;
using System.Threading.Tasks;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading;


namespace _8天玩转并行开发_4_
{
    class Program
    {
        #region 8天玩转并行开发——第四天 同步机制（上）
        //.net 4.0 数据同步问题
        //大体上分为二种：
        //①   并发集合类:           这个在先前的文章中也用到了，他们的出现不再让我们过多的关注同步细节。
        //②  轻量级同步机制:      相对于老版本中那些所谓的重量级同步机制而言,新的机制更加节省cpu的额外开销。
        #endregion

        //四个task执行
       
        static Barrier barrier = null;

        static SpinLock slock = new SpinLock(false);
        static int sum1 = 0;
        static int sum2 = 0;

        static void Main(string[] args)
        {
            Task[] tasks = new Task[4];
            #region 一： Barrier
            //超时机制
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken ct = cts.Token;
            barrier = new Barrier(tasks.Length, (i) =>
            {
                Console.WriteLine("**********************************************************");
                Console.WriteLine("\n屏障中当前阶段编号:{0}\n", i.CurrentPhaseNumber);
                Console.WriteLine("**********************************************************");
            });

            for (int j = 0; j < tasks.Length; j++)
            {
                tasks[j] = Task.Factory.StartNew((obj) =>
                {
                    var single = Convert.ToInt32(obj);

                    LoadUser(single);
                    if (!barrier.SignalAndWait(2000))
                    {
                        //抛出异常，取消后面加载的执行
                        throw new OperationCanceledException(string.Format("我是当前任务{0},我抛出异常了！", single), ct);
                    }

                    LoadProduct(single);
                    barrier.SignalAndWait();

                    LoadOrder(single);
                    barrier.SignalAndWait();
                }, j,ct);
            }

            Task.WaitAll(tasks,4000);
            try
            {
                for (int i = 0; i < tasks.Length; i++)
                {
                    if (tasks[i].Status == TaskStatus.Faulted)
                    {
                        //获取task中的异常
                        foreach (var single in tasks[i].Exception.InnerExceptions)
                        {
                            Console.WriteLine(single.Message);
                        }
                    }
                }

                barrier.Dispose();
            }
            catch (AggregateException e)
            {
                Console.WriteLine("我是总异常:{0}", e.Message);
            }
            Console.WriteLine("指定数据库中所有数据已经加载完毕！");
            #endregion

            #region 二：spinLock（自旋锁）

            Console.WriteLine("*************************************************************");

            Task[] tasks1 = new Task[100];

            for (int i = 1; i <= 100; i++)
            {
                tasks1[i - 1] = Task.Factory.StartNew((num) =>
                {
                    Add1((int)num);

                    Add2((int)num);

                }, i);
            }

            Task.WaitAll(tasks1);

            Console.WriteLine("Add1数字总和:{0}", sum1);

            Console.WriteLine("Add2数字总和:{0}", sum2);
            #endregion

            Console.Read();
        }

        #region Barrier
        static void LoadUser(int num)
        {
            Console.WriteLine("\n当前任务:{0}正在加载User部分数据！", num);

            #region 死锁
            if (num == 0)
            {
                //自旋转5s
                if (!SpinWait.SpinUntil(() => barrier.ParticipantsRemaining == 0, 5000))
                    return;
            }

            Console.WriteLine("当前任务:{0}正在加载User数据完毕！", num);
            #endregion
        }

        static void LoadProduct(int num)
        {
            Console.WriteLine("当前任务:{0}正在加载Product部分数据！", num);
        }

        static void LoadOrder(int num)
        {
            Console.WriteLine("当前任务:{0}正在加载Order部分数据！", num);
        }
        #endregion

        #region spinLock（自旋锁）
        //无锁
        static void Add1(int num)
        {
            Thread.Sleep(100);

            sum1 += num;
        }

        //自旋锁
        static void Add2(int num)
        {
            bool lockTaken = false;

            Thread.Sleep(100);

            try
            {
                slock.Enter(ref lockTaken);
                sum2 += num;
            }
            finally
            {
                if (lockTaken)
                    slock.Exit(false);
            }
        }

    }
    #endregion

}
