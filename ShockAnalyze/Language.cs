using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace ShockAnalyze
{
    public class Language
    {
        // 语言字典
        private Dictionary<string, string> dicLanguage = new Dictionary<string, string>();
        private string languageType= "Chinese";

        // 读取语言数据
        public Dictionary<string, string> ReadLanguage(int type)
        {
            languageType = type == 1 ? "English" : "Chinese";
            string floderPath = Application.StartupPath + "\\Languages\\";
            if (!Directory.Exists(floderPath)) { Directory.CreateDirectory(floderPath); }
            ReadXmlFile(floderPath);

            return dicLanguage;
        }

        public void ChangeLanguage(string languageType)
        {
            this.languageType = languageType;
        }

        // 获取语言文字
        public string FindText(string key)
        {
            string text = key; // 默认中文
            if (languageType.Equals("English") && dicLanguage.ContainsKey(key))
            {
                text = dicLanguage[key];
            }

            return text;
        }

        // 清空语言数据
        public void ClearLanguageItem()
        {
            dicLanguage.Clear();
        }

        // 添加语言项
        public void AddLanguageItem(string chinese, string english)
        {
            if (!dicLanguage.ContainsKey(chinese))
            {
                dicLanguage.Add(chinese, english);
            }
        }

        // 打开语言文件
        public void OpenLanguageFile()
        {
            string filePath = Application.StartupPath + "\\Languages\\Language.xml";
            if (File.Exists(filePath))
            {
                System.Diagnostics.Process.Start(filePath);
            }
        }

        // 保存语言文件
        public void SaveLanguage()
        {
            string floderPath = Application.StartupPath + "\\Languages\\";
            if (!Directory.Exists(floderPath)) { Directory.CreateDirectory(floderPath); }
            CreateXmlFile(floderPath);
        }

        // 创建xml
        private void CreateXmlFile(string floderPath)
        {
            string filePath = floderPath + "Language.xml";
            if (!File.Exists(filePath)) { return; }
            XmlTextWriter xtw = new XmlTextWriter(filePath, Encoding.UTF8);
            xtw.Formatting = Formatting.Indented; //这个属性说明xml文件里面的内容是按级别缩进的。

            // 写入根节点
            xtw.WriteStartDocument();
            xtw.WriteStartElement("Sentances");

            // 写入子节点
            foreach (string key in dicLanguage.Keys)
            {
                xtw.WriteStartElement("Sentence");
                xtw.WriteAttributeString("Chinese", key); // 中文Key
                xtw.WriteValue(dicLanguage[key]); // 英语Value
                xtw.WriteEndElement();
            }

            xtw.WriteEndElement();
            xtw.WriteEndDocument();
            xtw.Close();　//完成xml文件的输出，关闭
        }

        // XmlTextReader 读文件
        private void ReadXmlFile(string floderPath)
        {
            string filePath = floderPath + "Language.xml";
            if (!File.Exists(filePath)) { return; }

            dicLanguage.Clear();
            XmlTextReader readerXml = new XmlTextReader(filePath);
            while (readerXml.Read())
            {
                if (readerXml.NodeType == XmlNodeType.Element &&
                    readerXml.Name == "Sentence" &&
                    readerXml.AttributeCount == 1)
                {
                    string key = readerXml.GetAttribute(0);
                    string value = readerXml.ReadElementString().Trim();
                    if (!dicLanguage.ContainsKey(key))
                        dicLanguage.Add(key, value);
                    //Console.WriteLine("key={0},value={1}", key, value);
                }
            }

            readerXml.Close();
        }

    }
}
