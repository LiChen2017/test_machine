using System;
using System.Drawing;
using Microsoft.Office.Interop.Word;
using Steema.TeeChart;
using System.IO;

namespace ShockAnalyze
{
    public class Report
    {
        #region 调用方法

        // 生成报告
        public void OutputReport(string testName, string sourcePath, string savePath, TChart tChartF_D, TChart tChartF_V, ResultData resultData, TestSetting testSetting, bool isMinMax)
        {
            //创建Word
            string TemPath = GlobalData.BasePath + "\\TempDoc\\模板1.doc";
            if (testSetting.isNeedTemp && isMinMax) { TemPath = GlobalData.BasePath + "\\TempDoc\\模板2.doc"; }

            bool openState = CreateNewDocument(TemPath); //模板路径
            if (!openState) { return; }

            //2张图表
            ChartManager.ImageCopyToClipboard(tChartF_V);
            PastePicture("Picture");//F-V
            //InsertValue("Picture", "     ");//图片中间空格
            ChartManager.ImageCopyToClipboard(tChartF_D);
            PastePicture("Picture");//F-D

            //减震器信息
            ShockInfo shockInfo = resultData.ShockInf;
            InsertValue("Test_Name", testName);
            InsertValue("Shock_Name", shockInfo.Shock_Name);
            InsertValue("Shock_ID", shockInfo.Shock_Num);
            InsertValue("Vehicle", shockInfo.Vehicle);
            InsertValue("Location", shockInfo.Location);
            InsertValue("Compression_Valving", shockInfo.Compression_Valving);
            InsertValue("Rebound_Valving", shockInfo.Rebound_Valving);
            InsertValue("Diameter", shockInfo.Diameter);
            InsertValue("Installation_Position", shockInfo.Installation_Position);
            InsertValue("Compression_Setting", shockInfo.Compression_Setting);
            InsertValue("Rebound_Setting", shockInfo.Rebound_Setting);
            InsertValue("Preload_Setting", shockInfo.Preload_Setting);
            InsertValue("Notes", shockInfo.Notes);

            //每个速度点信息
            int rowCount = resultData.AnalyzeDatas.Count;

            //剩下的行
            int idx = 0;
            double Fc = Math.Abs(double.Parse(resultData.Fc));
            for (int i = 0; i < rowCount; i++)
            {
                Color textColor = tChartF_D.Series.Count > i ? tChartF_D.Series[i].Color : Color.Black;//颜色
                bool isNext = i == rowCount - 1 ? false : true;//是否有下一行

                //获取最高速
                double curSpeed = resultData.AnalyzeDatas[i].ActSpeed;

                //数据
                string FmyStr = (resultData.AnalyzeDatas[i].CompForce).ToString("F2");
                string FmfStr = (resultData.AnalyzeDatas[i].ReboundForce).ToString("F2");

                if (isMinMax)
                {
                    FmyStr = (resultData.AnalyzeDatas[i].MinForce + Fc).ToString("F2");
                    FmfStr = (resultData.AnalyzeDatas[i].MaxForce + Fc).ToString("F2");
                }

                if (testSetting.isNeedTemp && testSetting.Temps1.Count > idx)
                {
                    Temp temp1 = testSetting.Temps1[idx];
                    Temp temp2 = testSetting.Temps2[idx];
                    if (resultData.AnalyzeDatas[i].Speed == temp1.Speed)
                    {

                        FmyStr = FmyStr + "(" + temp1.Fmy + "," + temp2.Fmy + ")";
                        FmfStr = FmfStr + "(" + temp1.Fmf + "," + temp2.Fmf + ")";
                        idx++;
                    }
                }

                InsertValueBySelection("Compression_Velocity", i, "-" + curSpeed.ToString("F2"), textColor, isNext);
                InsertValueBySelection("Compression_Force", i, FmyStr, textColor, isNext);
                InsertValueBySelection("Rebound_Velocity", i, curSpeed.ToString("F2"), textColor, isNext);
                InsertValueBySelection("Rebound_Force", i, FmfStr, textColor, isNext);
            }

            //分析结果
            InsertValue("Date", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            string initial_Temp = resultData.Initial_Temp == Constant.EMPTY_STR ? resultData.Initial_Temp : resultData.Initial_Temp + "℃";
            InsertValue("InitTemp", initial_Temp);
            InsertValue("Fm", GetForceValue(resultData.Fm));
            InsertValue("Fc", GetForceValue(resultData.Fc));

            InsertValue("mz", "MZ Shockabosorber Test System");
            InsertValue("OutType", isMinMax ? "Peak Value" : "Max Velocity Point");

            //保存
            SaveDocument(sourcePath, savePath); //文档路径
            killWinWordProcess();
        }

        // 转换力值数据
        private string GetForceValue(string oldValueStr)
        {
            string force = oldValueStr == Constant.EMPTY_STR ? oldValueStr : double.Parse(oldValueStr) + "N";
            return force;
        }

        #endregion

        #region Word 操作

        private _Application wordApp = null;
        private _Document wordDoc = null;
        public _Application Application
        {
            get
            {
                return wordApp;
            }
            set
            {
                wordApp = value;
            }
        }
        public _Document Document
        {
            get
            {
                return wordDoc;
            }
            set
            {
                wordDoc = value;
            }
        }

        //通过模板创建新文档
        public bool CreateNewDocument(string filePath)
        {
            killWinWordProcess();
            wordApp = new Application();
            wordApp.DisplayAlerts = WdAlertLevel.wdAlertsNone;
            wordApp.Visible = false;
            object missing = System.Reflection.Missing.Value;
            object templateName = filePath;
            try
            {
                wordDoc = wordApp.Documents.Open(ref templateName, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Report:" + ex.ToString());
                //System.Windows.Forms.MessageBox.Show("生成失败！");
                return false;
            }
            wordApp.Selection.Font.Name = "宋体";
            return true;
        }

        //保存新文件
        public void SaveDocument(string sourcePath, string filePath)
        {
            object fileName = sourcePath;
            object format = WdSaveFormat.wdFormatDocument;//保存格式
            object miss = System.Reflection.Missing.Value;
            wordDoc.SaveAs(ref fileName, ref format, ref miss,
                ref miss, ref miss, ref miss, ref miss,
                ref miss, ref miss, ref miss, ref miss,
                ref miss, ref miss, ref miss, ref miss,
                ref miss);

            wordDoc.ExportAsFixedFormat(filePath, WdExportFormat.wdExportFormatPDF);// 转换成PDF格式

            //关闭wordDoc，wordApp对象
            object SaveChanges = WdSaveOptions.wdSaveChanges;
            object OriginalFormat = WdOriginalFormat.wdOriginalDocumentFormat;
            object RouteDocument = false;
            wordDoc.Close(ref SaveChanges, ref OriginalFormat, ref RouteDocument);
            wordApp.Quit(ref SaveChanges, ref OriginalFormat, ref RouteDocument);

            // 转换好,删除原来的Word
            try
            {
                File.Delete(sourcePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        //打开文档
        public void OpenWordFile(string FileName)
        {
            wordApp = new Application();
            wordApp.Visible = true;
            object missing = System.Reflection.Missing.Value;
            Object filename = @FileName;
            wordDoc = wordApp.Documents.Open(ref filename, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
        }

        //在书签处插入值
        public void InsertValue(string bookmark, string value)
        {
            object bkObj = bookmark;
            if (wordApp.ActiveDocument.Bookmarks.Exists(bookmark))
            {
                wordApp.ActiveDocument.Bookmarks.get_Item(ref bkObj).Select();
                wordApp.Selection.TypeText(value);
            }
        }

        public void InsertValueBySelection(string bookmark, int rowIndex, string value, System.Drawing.Color textColor, bool isNext)
        {
            object bkObj = bookmark;
            object count = rowIndex;
            object missing = System.Reflection.Missing.Value;
            if (wordApp.ActiveDocument.Bookmarks.Exists(bookmark))
            {
                wordApp.ActiveDocument.Bookmarks.get_Item(ref bkObj).Select();//找到书签
                object WdLine = Microsoft.Office.Interop.Word.WdUnits.wdLine;//换一行;
                wordApp.Selection.MoveDown(ref WdLine, ref count, ref missing);//移动焦点

                //文字颜色
                if (textColor != System.Drawing.Color.Empty)
                {
                    //颜色转换
                    UInt32 R = 0x1, G = 0x100, B = 0x10000;
                    WdColor wordColor = (Microsoft.Office.Interop.Word.WdColor)(R * textColor.R + G * textColor.G + B * textColor.B);
                    wordApp.Selection.Font.Color = wordColor;
                }

                string text = isNext ? value + Environment.NewLine : value;
                wordApp.Selection.TypeText(text);//插入文字
            }
        }

        //插入表格,bookmark书签
        public Table InsertTable(string bookmark, int rows, int columns, float width)
        {
            object miss = System.Reflection.Missing.Value;
            object oStart = bookmark;
            Range range = wordDoc.Bookmarks.get_Item(ref oStart).Range;//表格插入位置
            Table newTable = wordDoc.Tables.Add(range, rows, columns, ref miss, ref miss);
            //设置表的格式
            newTable.Borders.Enable = 1;  //允许有边框，默认没有边框(为0时报错，1为实线边框，2、3为虚线边框，以后的数字没试过)
            newTable.Borders.OutsideLineWidth = WdLineWidth.wdLineWidth050pt;//边框宽度
            if (width != 0)
            {
                newTable.PreferredWidth = width;//表格宽度
            }

            newTable.AllowPageBreaks = false;
            return newTable;
        }

        //合并单元格 表名,开始行号,开始列号,结束行号,结束列号
        public void MergeCell(Microsoft.Office.Interop.Word.Table table, int row1, int column1, int row2, int column2)
        {
            table.Cell(row1, column1).Merge(table.Cell(row2, column2));
        }

        //设置表格内容对齐方式 Align水平方向，Vertical垂直方向(左对齐，居中对齐，右对齐分别对应Align和Vertical的值为-1,0,1)
        public void SetParagraph_Table(Microsoft.Office.Interop.Word.Table table, int Align, int Vertical)
        {
            switch (Align)
            {
                case -1: table.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft; break;//左对齐
                case 0: table.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter; break;//水平居中
                case 1: table.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight; break;//右对齐
            }
            switch (Vertical)
            {
                case -1: table.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalTop; break;//顶端对齐
                case 0: table.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter; break;//垂直居中
                case 1: table.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalBottom; break;//底端对齐
            }
        }

        //设置表格字体
        public void SetFont_Table(Microsoft.Office.Interop.Word.Table table, string fontName, double size)
        {
            if (size != 0)
            {
                table.Range.Font.Size = Convert.ToSingle(size);
            }
            if (fontName != "")
            {
                table.Range.Font.Name = fontName;
            }
        }

        //是否使用边框,n表格的序号,use是或否
        public void UseBorder(int n, bool use)
        {
            if (use)
            {
                wordDoc.Content.Tables[n].Borders.Enable = 1;  //允许有边框，默认没有边框(为0时报错，1为实线边框，2、3为虚线边框，以后的数字没试过)
            }
            else
            {
                wordDoc.Content.Tables[n].Borders.Enable = 2;  //允许有边框，默认没有边框(为0时报错，1为实线边框，2、3为虚线边框，以后的数字没试过)
            }
        }

        //给表格插入一行,n表格的序号从1开始记
        public void AddRow(int n)
        {
            object miss = System.Reflection.Missing.Value;
            wordDoc.Content.Tables[n].Rows.Add(ref miss);
        }

        //给表格添加一行
        public void AddRow(Microsoft.Office.Interop.Word.Table table)
        {
            object miss = System.Reflection.Missing.Value;
            table.Rows.Add(ref miss);
        }

        //给表格插入rows行,n为表格的序号
        public void AddRow(int n, int rows)
        {
            object miss = System.Reflection.Missing.Value;
            Microsoft.Office.Interop.Word.Table table = wordDoc.Content.Tables[n];
            for (int i = 0; i < rows; i++)
            {
                table.Rows.Add(ref miss);
            }
        }

        //给表格中单元格插入元素，table所在表格，row行号，column列号，value插入的元素
        public void InsertCell(Microsoft.Office.Interop.Word.Table table, int row, int column, string value, System.Drawing.Color textColor)
        {
            //颜色转换
            UInt32 R = 0x1, G = 0x100, B = 0x10000;
            WdColor wordColor = (Microsoft.Office.Interop.Word.WdColor)(R * textColor.R + G * textColor.G + B * textColor.B);

            //插入表格
            table.Cell(row, column).Range.Text = value;
            table.Cell(row, column).Range.Font.Color = wordColor;
        }

        public WdColor GetWordColor(System.Drawing.Color c)
        {
            UInt32 R = 0x1, G = 0x100, B = 0x10000;
            return (Microsoft.Office.Interop.Word.WdColor)(R * c.R + G * c.G + B * c.B);
        }

        //给表格中单元格插入元素，n表格的序号从1开始记，row行号，column列号，value插入的元素
        public void InsertCell(int n, int row, int column, string value)
        {
            wordDoc.Content.Tables[n].Cell(row, column).Range.Text = value;
        }

        //给表格插入一行数据，n为表格的序号，row行号，columns列数，values插入的值
        public void InsertCell(int n, int row, int columns, string[] values)
        {
            Microsoft.Office.Interop.Word.Table table = wordDoc.Content.Tables[n];
            for (int i = 0; i < columns; i++)
            {
                table.Cell(row, i + 1).Range.Text = values[i];
            }
        }

        //插入图片
        public void PastePicture(string bookmark)//, string picturePath
        {

            object miss = System.Reflection.Missing.Value;
            object oStart = bookmark;
            Object linkToFile = false;       //图片是否为外部链接
            Object saveWithDocument = true;  //图片是否随文档一起保存 
            if (wordApp.ActiveDocument.Bookmarks.Exists(bookmark))
            {
                //object range = wordDoc.Bookmarks.get_Item(ref oStart).Range;//图片插入位置
                //wordDoc.InlineShapes.AddPicture(picturePath, ref linkToFile, ref saveWithDocument, ref range);

                try
                {
                    wordDoc.Bookmarks.get_Item(ref oStart).Range.Paste();
                    System.Windows.Forms.Clipboard.Clear();//清空剪切板内容
                    //wordDoc.Application.ActiveDocument.InlineShapes[1].Width = width;   //设置图片宽度
                    //wordDoc.Application.ActiveDocument.InlineShapes[1].Height = hight;  //设置图片高度
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.Clipboard.Clear();//清空剪切板内容
                    Console.WriteLine(ex.Message);
                    MessageDxUtil.ShowError("Generate Report Error");
                }

            }
        }

        //插入一段文字,text为文字内容
        public void InsertText(string bookmark, string text)
        {
            object oStart = bookmark;
            object range = wordDoc.Bookmarks.get_Item(ref oStart).Range;
            Paragraph wp = wordDoc.Content.Paragraphs.Add(ref range);
            wp.Format.SpaceBefore = 6;
            wp.Range.Text = text;
            wp.Format.SpaceAfter = 24;
            wp.Range.InsertParagraphAfter();
            wordDoc.Paragraphs.Last.Range.Text = "\n";
        }

        // 杀掉winword.exe进程
        public void killWinWordProcess()
        {
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcessesByName("WINWORD");
            foreach (System.Diagnostics.Process process in processes)
            {
                bool b = process.MainWindowTitle == "";
                if (process.MainWindowTitle == "")
                {
                    process.Kill();
                }
            }
        }

        #endregion
    }
}
