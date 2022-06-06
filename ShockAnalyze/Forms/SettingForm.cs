using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ShockAnalyze
{
    public partial class SettingForm : DevExpress.XtraEditors.XtraForm
    {
        // 通道表格
        DataTable dtChannels;
        // 测试参数设置
        public SyetemSetting syetemSetting = new SyetemSetting();
        public SyetemSetting Syetem_Setting
        {
            get { return this.syetemSetting; }
            set { this.syetemSetting = value; }
        }
        Language language;
        public SettingForm(Language language,int type)
        {
            InitializeComponent();

            this.Size = new Size(450, 440);

            //创建表
            dtChannels = new DataTable();
            //添加列
            dtChannels.Columns.Add("index", typeof(string));
            dtChannels.Columns.Add("name", typeof(string));
            dtChannels.Columns.Add("unit", typeof(string));
            dtChannels.Columns.Add("rate", typeof(string));
            dtChannels.Columns.Add("offset", typeof(string));

            this.language = language;
            BindText(language, type);
        }

        private void BindText(Language language, int type)
        {
            this.Text = language.FindText("设置");
            labelControl39.Text = language.FindText("基本设置");
            labelControl1.Text= language.FindText("命名前缀")+":";
            labelControl2.Text = language.FindText("项目目录") + ":";

            labelControl4.Text = language.FindText("数采通道设置");

            gridColumn2.Caption = language.FindText("序号");
            gridColumn3.Caption = language.FindText("名称");
            gridColumn4.Caption = language.FindText("单位");
            gridColumn5.Caption = language.FindText("比例系数");
            gridColumn1.Caption = language.FindText("偏移量");

            labelControl11.Text= language.FindText("语言选择");
            cbLanguages.Properties.Items.Clear();
            cbLanguages.Properties.Items.Add(language.FindText("中文"));
            cbLanguages.Properties.Items.Add(language.FindText("英语"));
            cbLanguages.SelectedIndex = type;

            cbtnSubmit.Text = language.FindText("确定");
            cbtnClose.Text = language.FindText("关闭");

        }

        private void SettingForm_Load(object sender, System.EventArgs e)
        {
            ShowSystemSetting();
        }

        // 关闭
        private void cbtnClose_CheckedChanged(object sender, System.EventArgs e)
        {
            this.Close();
        }

        // 提交
        private void cbtnSubmit_CheckedChanged(object sender, System.EventArgs e)
        {
            SetSystemSetting();
            this.DialogResult = DialogResult.OK;
        }

        // 显示系统设置
        private void ShowSystemSetting()
        {
            tedtPrefixNamed.Text = syetemSetting.PrefixNamed;
            bedtProjectDirectory.Text = syetemSetting.ProjectDirectory;

            for (int i = 0; i < syetemSetting.Channels.Count; i++)
            {
                Channel channel = syetemSetting.Channels[i];
                DataRow row = dtChannels.NewRow();
                row[0] = (i + 1) + "";
                row[1] =language.FindText(channel.Name);
                row[2] = channel.Unit;
                row[3] = channel.Rate;
                row[4] = channel.Offset;
                dtChannels.Rows.Add(row);
            }

            gridControlChannels.DataSource = dtChannels;
        }

        // 赋值系统设置
        private void SetSystemSetting()
        {
            syetemSetting.PrefixNamed=tedtPrefixNamed.Text;
            syetemSetting.ProjectDirectory=bedtProjectDirectory.Text;
            syetemSetting.LanguageType = cbLanguages.SelectedIndex;

            syetemSetting.Channels.Clear();
            for (int i = 0; i < dtChannels.Rows.Count; i++)
            {
                DataRow row = dtChannels.Rows[i];
                Channel channel = new Channel();
                channel.Name = row[1].ToString();
                channel.Unit = row[2].ToString();
                channel.Rate = double.Parse(row[3].ToString());
                channel.Offset = double.Parse(row[4].ToString());
                syetemSetting.Channels.Add(channel);
            }
        }
        
        // 文件夹浏览选择
        private void bedtProjectDirectory_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.SelectedPath = bedtProjectDirectory.Text;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                bedtProjectDirectory.Text = folderBrowserDialog.SelectedPath;
            }
        }

    }
}
