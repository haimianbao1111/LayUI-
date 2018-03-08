using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInterfaces
{
    public interface WinFormInterface
    {
        /// <summary>
        /// 说明
        /// </summary>
        String GetDirectons
        {
            get;
        }
        /// <summary>
        /// 显示窗体
        /// </summary>
        void ShowIt();
    }
}
