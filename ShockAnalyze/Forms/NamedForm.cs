using System;
using System.Drawing;
using System.Windows.Forms;

namespace ShockAnalyze
{
    public partial class NamedForm : DevExpress.XtraEditors.XtraForm
    {
        // 标题
        public string fileName = "";
        public string FileName
        {
            get { return bedtNamed.Text; }
            set { this.fileName = value; }
        }

        public NamedForm(Language language)
        {
            InitializeComponent();
            this.Size = new Size(265, 105);

            BindText(language);
        }

        private void BindText(Language language)
        {
            this.Text= language.FindText("重命名");
            labelControl1.Text = language.FindText("名称")+":";
            cbtnClose.Text= language.FindText("取消");
            cbtnSubmit.Text= language.FindText("确定");
        }

        // 窗体加载
        private void NamedForm_Load(object sender, EventArgs e)
        {
            bedtNamed.Text = fileName;
        }

        // 关闭
        private void cbtnClose_CheckedChanged(object sender, EventArgs e)
        {
            this.Close();
        }

        // 提交
        private void cbtnSubmit_CheckedChanged(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.OK;
        }

        // 清空文本按钮
        private void bedtNamed_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            bedtNamed.Text = "";
        }
    }
}
