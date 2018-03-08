using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyWinForm
{
    public partial class MainForm : Form
    {

        /// <summary>
        /// 是否修改
        /// </summary>
        private bool Changge = false;
        List<String[]> ClassPaths = new List<String[]>();
        public MainForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 程序Load事件，把配置文件信息读入程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!File.Exists("配置.xml"))
            {
                return;
            }
            DataSet ds = new DataSet();
            try
            {
                ds.ReadXml("配置.xml");
            }
            catch
            {
                return;
            }
            DataTable dt;
            try
            {
                dt = ds.Tables[0];
            }
            catch
            {
                return;
            }
            for (Int32 i = 0; i < dt.Rows.Count; i++)
            {
                String[] Stemp = new String[3];
                Stemp[0] = dt.Rows[i]["ClassName"].ToString();
                Stemp[1] = dt.Rows[i]["FilePath"].ToString();
                Stemp[2] = dt.Rows[i]["Direction"].ToString();
                ClassPaths.Add(Stemp);
                listBox1.Items.Add(dt.Rows[i]["ClassName"]);
            }
        }
        /// <summary>
        /// 检测按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnJianCe_Click(object sender, EventArgs e)
        {
            StatusText.Text = "正在检测";
            String[] files = Directory.GetFiles(Application.StartupPath + "\\Plugins", "*.dll");
            Assembly Assly;
            Type[] Types;
            foreach (String ff in files)
            {
                try
                {
                    Assly = Assembly.LoadFile(ff);
                }
                catch
                {
                    continue;
                }
                Types = Assly.GetTypes();
                foreach (Type tp in Types)
                {
                    if (IsValidPlugin(tp))
                    {
                        if (IsContains(tp.FullName, ff))
                        {
                            continue;
                        }
                        String[] Stemp = new String[3];
                        Stemp[0] = tp.FullName;
                        Stemp[1] = ff;
                        Stemp[2] = "";
                        listBox1.Items.Add(tp.FullName);
                        ClassPaths.Add(Stemp);
                        Changge = true;
                    }
                }
            }
            StatusText.Text = "完成检测";
        }
        /// <summary>
        /// 判断类是否实现了接口
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private bool IsValidPlugin(Type t)
        {
            Type[] Interfaces = t.GetInterfaces();
            foreach (Type Ifs in Interfaces)
            {
                if (Ifs.FullName == "MyInterfaces.WinFormInterface")
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 判断是否有重复的类型和文件名
        /// </summary>
        /// <param name="classname"></param>
        /// <param name="filepath"></param>
        /// <returns></returns>
        private bool IsContains(String classname, String filepath)
        {
            foreach (String[] ss in ClassPaths)
            {
                if (ss[0] == classname && ss[1] == filepath)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 运行按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnYunXing_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                return;
            }
            Assembly asbly;
            try
            {
                asbly = Assembly.LoadFile((ClassPaths[listBox1.SelectedIndex])[1]);
            }
            catch
            {
                MessageBox.Show("系统找不到指定文件或者非有效的dll文件");
                //将文件内无效的项目移除
                ClassPaths.RemoveAt(listBox1.SelectedIndex);
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                Changge = true;
                return;
            }
            Object oo = asbly.CreateInstance((ClassPaths[listBox1.SelectedIndex])[0]);
            MyInterfaces.WinFormInterface ooshow = (MyInterfaces.WinFormInterface)oo;
            (ClassPaths[listBox1.SelectedIndex])[2] = ooshow.GetDirectons;
            ooshow.ShowIt();
        }
        /// <summary>
        /// 写配置文件
        /// </summary>
        private void WriteInfo()
        {
            if (ClassPaths.Count == 0)
            {
                try
                {
                    FileInfo fi = new FileInfo("配置.xml");
                    fi.Delete();
                    return;
                }
                catch
                {
                    return;
                }
            }
            FileStream FS = new FileStream("配置.xml", FileMode.Create);
            System.Xml.XmlTextWriter myXml = new System.Xml.XmlTextWriter(FS, System.Text.Encoding.Default);
            myXml.Formatting = System.Xml.Formatting.Indented;
            myXml.WriteStartDocument(true);
            myXml.WriteStartElement("ClassInfo");
            Int32 id = 0;
            foreach (String[] cp in ClassPaths)
            {
                myXml.WriteStartElement("Class");
                myXml.WriteAttributeString("ID", id.ToString());
                myXml.WriteElementString("ClassName", cp[0]);
                myXml.WriteElementString("FilePath", cp[1]);
                myXml.WriteElementString("Direction", cp[2]);
                myXml.WriteEndElement();
                id++;
            }

            myXml.WriteEndElement();
            myXml.WriteEndDocument();
            myXml.Close();
            FS.Close();
        }
        /// <summary>
        /// listBox1的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                return;
            }
            if ((ClassPaths[listBox1.SelectedIndex])[2] != "")
            {
                textBox1.Text = (ClassPaths[listBox1.SelectedIndex])[2];
                return;
            }
            Assembly asbly = Assembly.LoadFile((ClassPaths[listBox1.SelectedIndex])[1]);
            Object oo = asbly.CreateInstance((ClassPaths[listBox1.SelectedIndex])[0]);
            MyInterfaces.WinFormInterface ooshow = (MyInterfaces.WinFormInterface)oo;
            (ClassPaths[listBox1.SelectedIndex])[2] = ooshow.GetDirectons;
            textBox1.Text = ooshow.GetDirectons;
            Changge = true;
        }
    }
}
