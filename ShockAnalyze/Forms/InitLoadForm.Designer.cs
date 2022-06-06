namespace ShockAnalyze
{
    partial class InitLoadForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabNavigationPage2 = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.sbtnPrevious = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnRating = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.tedtForce = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.tabPane1 = new DevExpress.XtraBars.Navigation.TabPane();
            this.tabNavigationPage1 = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.sbtnNext = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.sbtnClose = new DevExpress.XtraEditors.SimpleButton();
            this.timerShow = new System.Windows.Forms.Timer(this.components);
            this.tabNavigationPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tedtForce.Properties)).BeginInit();
            this.tabPane1.SuspendLayout();
            this.tabNavigationPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabNavigationPage2
            // 
            this.tabNavigationPage2.Caption = "步骤二";
            this.tabNavigationPage2.Controls.Add(this.sbtnPrevious);
            this.tabNavigationPage2.Controls.Add(this.sbtnRating);
            this.tabNavigationPage2.Controls.Add(this.labelControl6);
            this.tabNavigationPage2.Controls.Add(this.labelControl7);
            this.tabNavigationPage2.Controls.Add(this.labelControl8);
            this.tabNavigationPage2.Controls.Add(this.labelControl9);
            this.tabNavigationPage2.Name = "tabNavigationPage2";
            this.tabNavigationPage2.Size = new System.Drawing.Size(502, 194);
            // 
            // sbtnPrevious
            // 
            this.sbtnPrevious.Location = new System.Drawing.Point(140, 141);
            this.sbtnPrevious.Name = "sbtnPrevious";
            this.sbtnPrevious.Size = new System.Drawing.Size(75, 30);
            this.sbtnPrevious.TabIndex = 14;
            this.sbtnPrevious.Text = "上一步";
            this.sbtnPrevious.Click += new System.EventHandler(this.sbtnPrevious_Click);
            // 
            // sbtnRating
            // 
            this.sbtnRating.Location = new System.Drawing.Point(353, 141);
            this.sbtnRating.Name = "sbtnRating";
            this.sbtnRating.Size = new System.Drawing.Size(75, 30);
            this.sbtnRating.TabIndex = 13;
            this.sbtnRating.Text = "标定";
            this.sbtnRating.Click += new System.EventHandler(this.sbtnRating_Click);
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.BackColor = System.Drawing.Color.LightSteelBlue;
            this.labelControl6.Appearance.ForeColor = System.Drawing.Color.Transparent;
            this.labelControl6.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl6.Location = new System.Drawing.Point(140, 68);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(288, 2);
            this.labelControl6.TabIndex = 11;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.labelControl7.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.labelControl7.Location = new System.Drawing.Point(140, 90);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(362, 14);
            this.labelControl7.TabIndex = 10;
            this.labelControl7.Text = "下方标示栏为当前力传感器测量值，请在稳定状态标定载荷单元值。";
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl8.Appearance.BackColor2 = System.Drawing.Color.Transparent;
            this.labelControl8.Appearance.BorderColor = System.Drawing.Color.Transparent;
            this.labelControl8.Appearance.Image = global::ShockAnalyze.Properties.Resources.loadcell2;
            this.labelControl8.AppearanceDisabled.BackColor = System.Drawing.Color.Transparent;
            this.labelControl8.AppearanceDisabled.BackColor2 = System.Drawing.Color.Transparent;
            this.labelControl8.AppearanceDisabled.BorderColor = System.Drawing.Color.Transparent;
            this.labelControl8.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl8.Location = new System.Drawing.Point(19, 8);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(80, 180);
            this.labelControl8.TabIndex = 9;
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.labelControl9.Location = new System.Drawing.Point(140, 29);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(63, 24);
            this.labelControl9.TabIndex = 8;
            this.labelControl9.Text = "步骤二";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl1.Location = new System.Drawing.Point(167, 267);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(11, 19);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "N";
            // 
            // tedtForce
            // 
            this.tedtForce.EditValue = "0.00";
            this.tedtForce.Location = new System.Drawing.Point(40, 249);
            this.tedtForce.Name = "tedtForce";
            this.tedtForce.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.tedtForce.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.tedtForce.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.tedtForce.Properties.Appearance.Options.UseBackColor = true;
            this.tedtForce.Properties.Appearance.Options.UseFont = true;
            this.tedtForce.Properties.Appearance.Options.UseForeColor = true;
            this.tedtForce.Properties.Appearance.Options.UseTextOptions = true;
            this.tedtForce.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tedtForce.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.tedtForce.Properties.AutoHeight = false;
            this.tedtForce.Properties.ReadOnly = true;
            this.tedtForce.Size = new System.Drawing.Size(121, 38);
            this.tedtForce.TabIndex = 5;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Location = new System.Drawing.Point(140, 29);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(63, 24);
            this.labelControl2.TabIndex = 8;
            this.labelControl2.Text = "步骤一";
            // 
            // tabPane1
            // 
            this.tabPane1.Controls.Add(this.tabNavigationPage1);
            this.tabPane1.Controls.Add(this.tabNavigationPage2);
            this.tabPane1.Location = new System.Drawing.Point(12, 12);
            this.tabPane1.Name = "tabPane1";
            this.tabPane1.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] {
            this.tabNavigationPage1,
            this.tabNavigationPage2});
            this.tabPane1.RegularSize = new System.Drawing.Size(520, 240);
            this.tabPane1.SelectedPage = this.tabNavigationPage1;
            this.tabPane1.SelectedPageIndex = 0;
            this.tabPane1.Size = new System.Drawing.Size(520, 240);
            this.tabPane1.TabIndex = 4;
            this.tabPane1.Text = "tabPane1";
            // 
            // tabNavigationPage1
            // 
            this.tabNavigationPage1.Caption = "步骤一";
            this.tabNavigationPage1.Controls.Add(this.sbtnNext);
            this.tabNavigationPage1.Controls.Add(this.labelControl5);
            this.tabNavigationPage1.Controls.Add(this.labelControl4);
            this.tabNavigationPage1.Controls.Add(this.labelControl3);
            this.tabNavigationPage1.Controls.Add(this.labelControl2);
            this.tabNavigationPage1.Name = "tabNavigationPage1";
            this.tabNavigationPage1.Size = new System.Drawing.Size(502, 194);
            // 
            // sbtnNext
            // 
            this.sbtnNext.Location = new System.Drawing.Point(353, 136);
            this.sbtnNext.Name = "sbtnNext";
            this.sbtnNext.Size = new System.Drawing.Size(75, 30);
            this.sbtnNext.TabIndex = 12;
            this.sbtnNext.Text = "下一步";
            this.sbtnNext.Click += new System.EventHandler(this.sbtnNext_Click);
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.BackColor = System.Drawing.Color.LightSteelBlue;
            this.labelControl5.Appearance.ForeColor = System.Drawing.Color.Transparent;
            this.labelControl5.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl5.Location = new System.Drawing.Point(140, 68);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(288, 2);
            this.labelControl5.TabIndex = 11;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.labelControl4.Location = new System.Drawing.Point(140, 97);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(288, 14);
            this.labelControl4.TabIndex = 10;
            this.labelControl4.Text = "请保证您的载荷单元处于空载状态，即减震器未装夹。";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl3.Appearance.BackColor2 = System.Drawing.Color.Transparent;
            this.labelControl3.Appearance.BorderColor = System.Drawing.Color.Transparent;
            this.labelControl3.Appearance.Image = global::ShockAnalyze.Properties.Resources.loadcell1;
            this.labelControl3.AppearanceDisabled.BackColor = System.Drawing.Color.Transparent;
            this.labelControl3.AppearanceDisabled.BackColor2 = System.Drawing.Color.Transparent;
            this.labelControl3.AppearanceDisabled.BorderColor = System.Drawing.Color.Transparent;
            this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl3.Location = new System.Drawing.Point(32, 29);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(56, 120);
            this.labelControl3.TabIndex = 9;
            // 
            // sbtnClose
            // 
            this.sbtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sbtnClose.Location = new System.Drawing.Point(448, 257);
            this.sbtnClose.Name = "sbtnClose";
            this.sbtnClose.Size = new System.Drawing.Size(75, 30);
            this.sbtnClose.TabIndex = 14;
            this.sbtnClose.Text = "关闭";
            this.sbtnClose.Click += new System.EventHandler(this.sbtnClose_Click);
            // 
            // timerShow
            // 
            this.timerShow.Enabled = true;
            this.timerShow.Tick += new System.EventHandler(this.timerShow_Tick);
            // 
            // InitLoadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.sbtnClose;
            this.ClientSize = new System.Drawing.Size(536, 294);
            this.ControlBox = false;
            this.Controls.Add(this.sbtnClose);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.tedtForce);
            this.Controls.Add(this.tabPane1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InitLoadForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "初始载荷";
            this.tabNavigationPage2.ResumeLayout(false);
            this.tabNavigationPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tedtForce.Properties)).EndInit();
            this.tabPane1.ResumeLayout(false);
            this.tabNavigationPage1.ResumeLayout(false);
            this.tabNavigationPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Navigation.TabNavigationPage tabNavigationPage2;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit tedtForce;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraBars.Navigation.TabPane tabPane1;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabNavigationPage1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton sbtnRating;
        private DevExpress.XtraEditors.SimpleButton sbtnNext;
        private DevExpress.XtraEditors.SimpleButton sbtnPrevious;
        private DevExpress.XtraEditors.SimpleButton sbtnClose;
        private System.Windows.Forms.Timer timerShow;

    }
}