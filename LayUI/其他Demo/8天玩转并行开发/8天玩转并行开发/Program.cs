using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace _8天玩转并行开发
{
    class Program
    {
        static void Main(string[] args)
        {
            //8天玩转并行开发——第一天 Parallel的使用
            #region Parallel.Invoke
            var watch = Stopwatch.StartNew();
            watch.Start();

            Run1();
            Run2();

            Console.WriteLine("我是串行开发，总共耗时:{0}\n", watch.ElapsedMilliseconds);
            watch.Restart();

            Parallel.Invoke(Run1, Run2);
            watch.Stop();
            Console.WriteLine("我是并行开发，总共耗时:{0}", watch.ElapsedMilliseconds);
            #endregion

            #region Parallel.for
            for (int j = 1; j < 4; j++)
            {
                Console.WriteLine("\n第{0}次比较", j);
                ConcurrentBag<int> bag = new ConcurrentBag<int>();
                watch = Stopwatch.StartNew();
                watch.Start();
                for (int i = 0; i < 20000000; i++)
                {
                    bag.Add(i);
                }
                Console.WriteLine("串行计算：集合有:{0},总共耗时：{1}", bag.Count, watch.ElapsedMilliseconds);
                GC.Collect();

                bag = new ConcurrentBag<int>();
                watch = Stopwatch.StartNew();
                watch.Start();
                Parallel.For(0, 20000000, i =>
                {
                    bag.Add(i);
                });
                Console.WriteLine("并行计算：集合有:{0},总共耗时：{1}", bag.Count, watch.ElapsedMilliseconds);
                GC.Collect();
            }
            #endregion

            #region Parallel.forEach
            for (int j = 1; j < 4; j++)
            {
                Console.WriteLine("\n第{0}次比较", j);
                ConcurrentBag<int> bag1 = new ConcurrentBag<int>();
                watch = Stopwatch.StartNew();
                watch.Start();

                for (int i = 0; i < 3000000; i++)
                {
                    bag1.Add(i);
                }
                Console.WriteLine("串行计算：集合有:{0},总共耗时：{1}", bag1.Count, watch.ElapsedMilliseconds);
                GC.Collect();

                bag1 = new ConcurrentBag<int>();
                watch = Stopwatch.StartNew();
                watch.Start();
                Parallel.ForEach(Partitioner.Create(0, 3000000, Environment.ProcessorCount), i =>
                {
                    for (int m = i.Item1; m < i.Item2; m++)
                    {
                        bag1.Add(m);
                    }
                });
                Console.WriteLine("获取到当前的硬件线程数:" + Environment.ProcessorCount);
                Console.WriteLine("并行计算：集合有:{0},总共耗时：{1}", bag1.Count, watch.ElapsedMilliseconds);
                GC.Collect();
            }
            #endregion

            #region 中断循环

            watch = Stopwatch.StartNew();
            watch.Start();
            int num = 0;
            ConcurrentBag<int> bag2 = new ConcurrentBag<int>();
            Parallel.For(0, 20000000, (i, state) =>
            {
                if (bag2.Count == 1000)
                {
                    state.Break();
                    return;
                }
                bag2.Add(i);
                num = Work(i);
            });

            Console.WriteLine(string.Format("当前集合有{0}个元素。", num.ToString()));


            #endregion

            #region 并行循环异常处理
            try
            {
                Parallel.Invoke(Run3, Run4);
            }
            catch (AggregateException ex)
            {
                foreach (var single in ex.InnerExceptions)
                {
                    Console.WriteLine(single.Message);
                }
            }
            #endregion

            #region 测量加速比
            ConcurrentBag<int> bag4 = new ConcurrentBag<int>();
            bag4 = new ConcurrentBag<int>();
            ParallelOptions options = new ParallelOptions();
            //指定使用的硬件线程数为1
            options.MaxDegreeOfParallelism = 1;

            Parallel.For(0, 300000, options, i =>
            {
                bag4.Add(i);
            });
            Console.WriteLine("并行计算：集合有:{0}", bag4.Count);
            #endregion

            Console.ReadKey();
        }
        //线程调用方法
        private static int Work(int TaskID)
        {
            return Interlocked.Increment(ref TaskID);
        }
        static void Run1()
        {
            Console.WriteLine("我是任务一，我跑了3秒。");
            Thread.Sleep(3000);
        }
        static void Run2()
        {
            Console.WriteLine("我是任务二，我跑了5秒。");
            Thread.Sleep(5000);
        }
        static void Run3()
        {
            Thread.Sleep(3000);
            throw new Exception("任务三跑出异常。");
        }
        static void Run4()
        {
            Thread.Sleep(5000);
            throw new Exception("任务四跑出异常。");
        }
    }
}
