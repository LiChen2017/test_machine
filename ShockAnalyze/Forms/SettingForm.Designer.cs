namespace ShockAnalyze
{
    partial class SettingForm
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
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl38 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl39 = new DevExpress.XtraEditors.LabelControl();
            this.bedtProjectDirectory = new DevExpress.XtraEditors.ButtonEdit();
            this.tedtPrefixNamed = new DevExpress.XtraEditors.TextEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControlChannels = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cbtnClose = new DevExpress.XtraEditors.CheckButton();
            this.cbtnSubmit = new DevExpress.XtraEditors.CheckButton();
            this.cbLanguages = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.bedtProjectDirectory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tedtPrefixNamed.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlChannels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbLanguages.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.BackColor = System.Drawing.Color.LightSteelBlue;
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Transparent;
            this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl3.Location = new System.Drawing.Point(22, 164);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(400, 2);
            this.labelControl3.TabIndex = 109;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl4.Location = new System.Drawing.Point(22, 144);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(78, 14);
            this.labelControl4.TabIndex = 108;
            this.labelControl4.Text = "数采通道设置";
            // 
            // labelControl38
            // 
            this.labelControl38.Appearance.BackColor = System.Drawing.Color.LightSteelBlue;
            this.labelControl38.Appearance.ForeColor = System.Drawing.Color.Transparent;
            this.labelControl38.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl38.Location = new System.Drawing.Point(22, 39);
            this.labelControl38.Name = "labelControl38";
            this.labelControl38.Size = new System.Drawing.Size(400, 2);
            this.labelControl38.TabIndex = 107;
            // 
            // labelControl39
            // 
            this.labelControl39.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl39.Location = new System.Drawing.Point(22, 19);
            this.labelControl39.Name = "labelControl39";
            this.labelControl39.Size = new System.Drawing.Size(52, 14);
            this.labelControl39.TabIndex = 106;
            this.labelControl39.Text = "基本设置";
            // 
            // bedtProjectDirectory
            // 
            this.bedtProjectDirectory.EditValue = "XXX//XXX//XXX";
            this.bedtProjectDirectory.Location = new System.Drawing.Point(126, 84);
            this.bedtProjectDirectory.Name = "bedtProjectDirectory";
            this.bedtProjectDirectory.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.bedtProjectDirectory.Properties.Appearance.Options.UseBackColor = true;
            this.bedtProjectDirectory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.bedtProjectDirectory.Properties.ReadOnly = true;
            this.bedtProjectDirectory.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bedtProjectDirectory_Properties_ButtonClick);
            this.bedtProjectDirectory.Size = new System.Drawing.Size(258, 20);
            this.bedtProjectDirectory.TabIndex = 105;
            // 
            // tedtPrefixNamed
            // 
            this.tedtPrefixNamed.EditValue = "XXXX";
            this.tedtPrefixNamed.Location = new System.Drawing.Point(126, 51);
            this.tedtPrefixNamed.Name = "tedtPrefixNamed";
            this.tedtPrefixNamed.Size = new System.Drawing.Size(258, 20);
            this.tedtPrefixNamed.TabIndex = 104;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn1.Caption = "偏移量";
            this.gridColumn1.FieldName = "offset";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 4;
            this.gridColumn1.Width = 89;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn5.Caption = "比例系数";
            this.gridColumn5.FieldName = "rate";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 3;
            this.gridColumn5.Width = 103;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn4.Caption = "单位";
            this.gridColumn4.FieldName = "unit";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            this.gridColumn4.Width = 66;
            // 
            // gridControlChannels
            // 
            this.gridControlChannels.Location = new System.Drawing.Point(22, 175);
            this.gridControlChannels.MainView = this.gridView1;
            this.gridControlChannels.Name = "gridControlChannels";
            this.gridControlChannels.Size = new System.Drawing.Size(400, 180);
            this.gridControlChannels.TabIndex = 103;
            this.gridControlChannels.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn1});
            this.gridView1.GridControl = this.gridControlChannels;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.KeepGroupExpandedOnSorting = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsDetail.ShowDetailTabs = false;
            this.gridView1.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsSelection.ShowCheckBoxSelectorInPrintExport = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsView.ShowDetailButtons = false;
            this.gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridView1.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn2.Caption = "序号";
            this.gridColumn2.FieldName = "index";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 50;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn3.Caption = "名称";
            this.gridColumn3.FieldName = "name";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 90;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl2.Location = new System.Drawing.Point(22, 87);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(100, 14);
            this.labelControl2.TabIndex = 102;
            this.labelControl2.Text = "项目目录:";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Location = new System.Drawing.Point(22, 54);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(100, 14);
            this.labelControl1.TabIndex = 101;
            this.labelControl1.Text = "命名前缀:";
            // 
            // cbtnClose
            // 
            this.cbtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cbtnClose.Location = new System.Drawing.Point(225, 366);
            this.cbtnClose.Name = "cbtnClose";
            this.cbtnClose.Size = new System.Drawing.Size(75, 25);
            this.cbtnClose.TabIndex = 113;
            this.cbtnClose.Text = "关闭";
            this.cbtnClose.CheckedChanged += new System.EventHandler(this.cbtnClose_CheckedChanged);
            // 
            // cbtnSubmit
            // 
            this.cbtnSubmit.Location = new System.Drawing.Point(144, 366);
            this.cbtnSubmit.Name = "cbtnSubmit";
            this.cbtnSubmit.Size = new System.Drawing.Size(75, 25);
            this.cbtnSubmit.TabIndex = 112;
            this.cbtnSubmit.Text = "确定";
            this.cbtnSubmit.CheckedChanged += new System.EventHandler(this.cbtnSubmit_CheckedChanged);
            // 
            // cbLanguages
            // 
            this.cbLanguages.Location = new System.Drawing.Point(127, 113);
            this.cbLanguages.Name = "cbLanguages";
            this.cbLanguages.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbLanguages.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbLanguages.Size = new System.Drawing.Size(257, 20);
            this.cbLanguages.TabIndex = 115;
            // 
            // labelControl11
            // 
            this.labelControl11.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(59)))), ((int)(((byte)(59)))));
            this.labelControl11.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl11.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl11.Location = new System.Drawing.Point(1, 116);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(121, 14);
            this.labelControl11.TabIndex = 114;
            this.labelControl11.Text = "语言选择:";
            // 
            // SettingForm
            // 
            this.AcceptButton = this.cbtnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cbtnClose;
            this.ClientSize = new System.Drawing.Size(428, 402);
            this.ControlBox = false;
            this.Controls.Add(this.cbLanguages);
            this.Controls.Add(this.labelControl11);
            this.Controls.Add(this.cbtnClose);
            this.Controls.Add(this.cbtnSubmit);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl38);
            this.Controls.Add(this.labelControl39);
            this.Controls.Add(this.bedtProjectDirectory);
            this.Controls.Add(this.tedtPrefixNamed);
            this.Controls.Add(this.gridControlChannels);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设置";
            this.Load += new System.EventHandler(this.SettingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bedtProjectDirectory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tedtPrefixNamed.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlChannels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbLanguages.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl38;
        private DevExpress.XtraEditors.LabelControl labelControl39;
        private DevExpress.XtraEditors.ButtonEdit bedtProjectDirectory;
        private DevExpress.XtraEditors.TextEdit tedtPrefixNamed;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.GridControl gridControlChannels;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.CheckButton cbtnClose;
        private DevExpress.XtraEditors.CheckButton cbtnSubmit;
        private DevExpress.XtraEditors.ComboBoxEdit cbLanguages;
        private DevExpress.XtraEditors.LabelControl labelControl11;
    }
}