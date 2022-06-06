namespace ShockAnalyze
{
    partial class NamedForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NamedForm));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.cbtnClose = new DevExpress.XtraEditors.CheckButton();
            this.cbtnSubmit = new DevExpress.XtraEditors.CheckButton();
            this.bedtNamed = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.bedtNamed.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // cbtnClose
            // 
            this.cbtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cbtnClose.Location = new System.Drawing.Point(46, 41);
            this.cbtnClose.Name = "cbtnClose";
            this.cbtnClose.Size = new System.Drawing.Size(75, 25);
            this.cbtnClose.TabIndex = 128;
            this.cbtnClose.Text = "取消";
            this.cbtnClose.CheckedChanged += new System.EventHandler(this.cbtnClose_CheckedChanged);
            // 
            // cbtnSubmit
            // 
            this.cbtnSubmit.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cbtnSubmit.Location = new System.Drawing.Point(171, 41);
            this.cbtnSubmit.Name = "cbtnSubmit";
            this.cbtnSubmit.Size = new System.Drawing.Size(75, 25);
            this.cbtnSubmit.TabIndex = 127;
            this.cbtnSubmit.Text = "确定";
            this.cbtnSubmit.CheckedChanged += new System.EventHandler(this.cbtnSubmit_CheckedChanged);
            // 
            // bedtNamed
            // 
            this.bedtNamed.Location = new System.Drawing.Point(46, 10);
            this.bedtNamed.Name = "bedtNamed";
            this.bedtNamed.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("bedtNamed.Properties.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.bedtNamed.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bedtNamed_Properties_ButtonClick);
            this.bedtNamed.Size = new System.Drawing.Size(200, 22);
            this.bedtNamed.TabIndex = 126;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(10, 11);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(28, 14);
            this.labelControl1.TabIndex = 125;
            this.labelControl1.Text = "名称:";
            // 
            // NamedForm
            // 
            this.AcceptButton = this.cbtnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cbtnClose;
            this.ClientSize = new System.Drawing.Size(251, 69);
            this.ControlBox = false;
            this.Controls.Add(this.cbtnClose);
            this.Controls.Add(this.cbtnSubmit);
            this.Controls.Add(this.bedtNamed);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NamedForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "重命名";
            this.Load += new System.EventHandler(this.NamedForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bedtNamed.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.CheckButton cbtnClose;
        private DevExpress.XtraEditors.CheckButton cbtnSubmit;
        private DevExpress.XtraEditors.ButtonEdit bedtNamed;
        private DevExpress.XtraEditors.LabelControl labelControl1;



    }
}