using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 构建高质量的C项代码
{
    class MathDemo
    {
        static void Main(string[] args)
        {
            #region 计算俩点间的距离
            //点一坐标：
            double x1 = 100D, y1 = 100D;
            //点二坐标：
            double x2 = 200D, y2 = 200D;
            //计算距离
            double interval = Math.Sqrt(Math.Pow(x2-x1,2D)+Math.Pow(y2-y1,2D));
            //显示结果
            Console.WriteLine("俩点距离：{0}",interval);
            #endregion

            #region Round()方法获取指定小数位
            //在Round()方法中采用的是银行家舍入法。基本原则是：转换后的数据将是最接近原值的数据。
            //例如：为4时，就舍去它；为6时，就向前进位；如果为5，则一般会转换最接近的偶数
            // double 数据保留指定位数
            Console.WriteLine(Math.Round(1.214,2));
            Console.WriteLine(Math.Round(1.216,2));
            Console.WriteLine(Math.Round(1.215,2));
            Console.WriteLine(Math.Round(1.225,2));
            Console.WriteLine(Math.Round(1.2225,3));
            #endregion


            Console.ReadKey();
        }
    }
}
