namespace ShockAnalyze
{
    partial class ReportForm
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
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tChartF_D = new Steema.TeeChart.TChart();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cbMinMax = new DevExpress.XtraEditors.ToggleSwitch();
            this.cbTemp = new DevExpress.XtraEditors.CheckEdit();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridControlResults = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControlTestInfo = new DevExpress.XtraGrid.GridControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.tChartF_V = new Steema.TeeChart.TChart();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.sbtnSubmit = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbMinMax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbTemp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlTestInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn1.Caption = "速度(m/s)";
            this.gridColumn1.FieldName = "speed";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 100;
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn6.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn6.Caption = "压缩力(N)";
            this.gridColumn6.FieldName = "Fmy";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 2;
            this.gridColumn6.Width = 212;
            // 
            // tChartF_D
            // 
            // 
            // 
            // 
            this.tChartF_D.Aspect.View3D = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_D.Axes.Bottom.AxisPen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_D.Axes.Bottom.AxisPen.Width = 1;
            // 
            // 
            // 
            this.tChartF_D.Axes.Bottom.Grid.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_D.Axes.Bottom.Grid.Style = System.Drawing.Drawing2D.DashStyle.Solid;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_D.Axes.Bottom.Labels.Font.Name = "Times New Roman";
            this.tChartF_D.Axes.Bottom.Labels.Font.Size = 10;
            this.tChartF_D.Axes.Bottom.Labels.Font.SizeFloat = 10F;
            // 
            // 
            // 
            this.tChartF_D.Axes.Bottom.MinorTicks.Visible = false;
            // 
            // 
            // 
            this.tChartF_D.Axes.Bottom.Ticks.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_D.Axes.Bottom.Ticks.Length = 2;
            // 
            // 
            // 
            this.tChartF_D.Axes.Bottom.TicksInner.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            // 
            // 
            // 
            this.tChartF_D.Axes.Bottom.Title.Caption = "位移(mm)";
            // 
            // 
            // 
            this.tChartF_D.Axes.Bottom.Title.Font.Name = "Times New Roman";
            this.tChartF_D.Axes.Bottom.Title.Lines = new string[] {
        "位移(mm)"};
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_D.Axes.Depth.AxisPen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_D.Axes.Depth.AxisPen.Width = 1;
            // 
            // 
            // 
            this.tChartF_D.Axes.Depth.Grid.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_D.Axes.Depth.Grid.Style = System.Drawing.Drawing2D.DashStyle.Solid;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_D.Axes.Depth.Labels.Font.Name = "Times New Roman";
            this.tChartF_D.Axes.Depth.Labels.Font.Size = 10;
            this.tChartF_D.Axes.Depth.Labels.Font.SizeFloat = 10F;
            // 
            // 
            // 
            this.tChartF_D.Axes.Depth.MinorTicks.Visible = false;
            // 
            // 
            // 
            this.tChartF_D.Axes.Depth.Ticks.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_D.Axes.Depth.Ticks.Length = 2;
            // 
            // 
            // 
            this.tChartF_D.Axes.Depth.TicksInner.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_D.Axes.Depth.Title.Font.Name = "Times New Roman";
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_D.Axes.DepthTop.AxisPen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_D.Axes.DepthTop.AxisPen.Width = 1;
            // 
            // 
            // 
            this.tChartF_D.Axes.DepthTop.Grid.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_D.Axes.DepthTop.Grid.Style = System.Drawing.Drawing2D.DashStyle.Solid;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_D.Axes.DepthTop.Labels.Font.Name = "Times New Roman";
            this.tChartF_D.Axes.DepthTop.Labels.Font.Size = 10;
            this.tChartF_D.Axes.DepthTop.Labels.Font.SizeFloat = 10F;
            // 
            // 
            // 
            this.tChartF_D.Axes.DepthTop.MinorTicks.Visible = false;
            // 
            // 
            // 
            this.tChartF_D.Axes.DepthTop.Ticks.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_D.Axes.DepthTop.Ticks.Length = 2;
            // 
            // 
            // 
            this.tChartF_D.Axes.DepthTop.TicksInner.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_D.Axes.DepthTop.Title.Font.Name = "Times New Roman";
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_D.Axes.Left.AxisPen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_D.Axes.Left.AxisPen.Width = 1;
            // 
            // 
            // 
            this.tChartF_D.Axes.Left.Grid.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_D.Axes.Left.Grid.Style = System.Drawing.Drawing2D.DashStyle.Solid;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_D.Axes.Left.Labels.Font.Name = "Times New Roman";
            this.tChartF_D.Axes.Left.Labels.Font.Size = 10;
            this.tChartF_D.Axes.Left.Labels.Font.SizeFloat = 10F;
            // 
            // 
            // 
            this.tChartF_D.Axes.Left.MinorTicks.Visible = false;
            // 
            // 
            // 
            this.tChartF_D.Axes.Left.Ticks.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_D.Axes.Left.Ticks.Length = 2;
            // 
            // 
            // 
            this.tChartF_D.Axes.Left.TicksInner.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            // 
            // 
            // 
            this.tChartF_D.Axes.Left.Title.Caption = "力(N)";
            // 
            // 
            // 
            this.tChartF_D.Axes.Left.Title.Font.Name = "Times New Roman";
            this.tChartF_D.Axes.Left.Title.Lines = new string[] {
        "力(N)"};
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_D.Axes.Right.AxisPen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_D.Axes.Right.AxisPen.Width = 1;
            // 
            // 
            // 
            this.tChartF_D.Axes.Right.Grid.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_D.Axes.Right.Grid.Style = System.Drawing.Drawing2D.DashStyle.Solid;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_D.Axes.Right.Labels.Font.Name = "Times New Roman";
            this.tChartF_D.Axes.Right.Labels.Font.Size = 10;
            this.tChartF_D.Axes.Right.Labels.Font.SizeFloat = 10F;
            // 
            // 
            // 
            this.tChartF_D.Axes.Right.MinorTicks.Visible = false;
            // 
            // 
            // 
            this.tChartF_D.Axes.Right.Ticks.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_D.Axes.Right.Ticks.Length = 2;
            // 
            // 
            // 
            this.tChartF_D.Axes.Right.TicksInner.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_D.Axes.Right.Title.Font.Name = "Times New Roman";
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_D.Axes.Top.AxisPen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_D.Axes.Top.AxisPen.Width = 1;
            // 
            // 
            // 
            this.tChartF_D.Axes.Top.Grid.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_D.Axes.Top.Grid.Style = System.Drawing.Drawing2D.DashStyle.Solid;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_D.Axes.Top.Labels.Font.Name = "Times New Roman";
            this.tChartF_D.Axes.Top.Labels.Font.Size = 10;
            this.tChartF_D.Axes.Top.Labels.Font.SizeFloat = 10F;
            // 
            // 
            // 
            this.tChartF_D.Axes.Top.MinorTicks.Visible = false;
            // 
            // 
            // 
            this.tChartF_D.Axes.Top.Ticks.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_D.Axes.Top.Ticks.Length = 2;
            // 
            // 
            // 
            this.tChartF_D.Axes.Top.TicksInner.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_D.Axes.Top.Title.Font.Name = "Times New Roman";
            this.tChartF_D.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tChartF_D.Enabled = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_D.Header.Brush.Gradient.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.tChartF_D.Header.Brush.Gradient.MiddleColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_D.Header.Brush.Gradient.SigmaFocus = 0F;
            this.tChartF_D.Header.Brush.Gradient.SigmaScale = 0F;
            this.tChartF_D.Header.Brush.Gradient.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_D.Header.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_D.Header.Font.Name = "Times New Roman";
            this.tChartF_D.Header.Font.Size = 12;
            this.tChartF_D.Header.Font.SizeFloat = 12F;
            this.tChartF_D.Header.Lines = new string[] {
        "位移-力"};
            // 
            // 
            // 
            this.tChartF_D.Header.Pen.Visible = false;
            // 
            // 
            // 
            this.tChartF_D.Header.Shadow.Height = 0;
            this.tChartF_D.Header.Shadow.Width = 0;
            this.tChartF_D.Header.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_D.Legend.Brush.Gradient.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.tChartF_D.Legend.Brush.Gradient.MiddleColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_D.Legend.Brush.Gradient.SigmaFocus = 0F;
            this.tChartF_D.Legend.Brush.Gradient.SigmaScale = 0F;
            this.tChartF_D.Legend.Brush.Gradient.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.tChartF_D.Legend.Font.Name = "Times New Roman";
            this.tChartF_D.Legend.Font.Size = 10;
            this.tChartF_D.Legend.Font.SizeFloat = 10F;
            this.tChartF_D.Legend.HorizMargin = 1;
            // 
            // 
            // 
            this.tChartF_D.Legend.Pen.Visible = false;
            // 
            // 
            // 
            this.tChartF_D.Legend.Shadow.Height = 0;
            this.tChartF_D.Legend.Shadow.Width = 0;
            // 
            // 
            // 
            this.tChartF_D.Legend.Symbol.Width = 25;
            this.tChartF_D.Legend.TopLeftPos = 1;
            this.tChartF_D.Legend.Transparent = true;
            this.tChartF_D.Legend.Visible = false;
            this.tChartF_D.Location = new System.Drawing.Point(2, 2);
            this.tChartF_D.Name = "tChartF_D";
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_D.Panel.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.tChartF_D.Panel.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.tChartF_D.Panel.Brush.Gradient.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.tChartF_D.Panel.Brush.Gradient.MiddleColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_D.Panel.Brush.Gradient.SigmaFocus = 0F;
            this.tChartF_D.Panel.Brush.Gradient.SigmaScale = 0F;
            this.tChartF_D.Panel.Brush.Gradient.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tChartF_D.Panel.Brush.Gradient.UseMiddle = false;
            this.tChartF_D.Panel.Brush.Gradient.Visible = false;
            // 
            // 
            // 
            this.tChartF_D.Panel.Shadow.Height = 0;
            this.tChartF_D.Panel.Shadow.Width = 0;
            this.tChartF_D.Size = new System.Drawing.Size(506, 306);
            this.tChartF_D.TabIndex = 13;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_D.Walls.Back.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.tChartF_D.Walls.Back.Brush.Gradient.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.tChartF_D.Walls.Back.Brush.Gradient.MiddleColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_D.Walls.Back.Brush.Gradient.SigmaFocus = 0F;
            this.tChartF_D.Walls.Back.Brush.Gradient.SigmaScale = 0F;
            this.tChartF_D.Walls.Back.Brush.Gradient.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tChartF_D.Walls.Back.Brush.Gradient.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_D.Walls.Bottom.Brush.Gradient.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.tChartF_D.Walls.Bottom.Brush.Gradient.MiddleColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_D.Walls.Bottom.Brush.Gradient.SigmaFocus = 0F;
            this.tChartF_D.Walls.Bottom.Brush.Gradient.SigmaScale = 0F;
            this.tChartF_D.Walls.Bottom.Brush.Gradient.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_D.Walls.Left.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.tChartF_D.Walls.Left.Brush.Gradient.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.tChartF_D.Walls.Left.Brush.Gradient.MiddleColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_D.Walls.Left.Brush.Gradient.SigmaFocus = 0F;
            this.tChartF_D.Walls.Left.Brush.Gradient.SigmaScale = 0F;
            this.tChartF_D.Walls.Left.Brush.Gradient.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_D.Walls.Right.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.tChartF_D.Walls.Right.Brush.Gradient.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.tChartF_D.Walls.Right.Brush.Gradient.MiddleColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_D.Walls.Right.Brush.Gradient.SigmaFocus = 0F;
            this.tChartF_D.Walls.Right.Brush.Gradient.SigmaScale = 0F;
            this.tChartF_D.Walls.Right.Brush.Gradient.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn2.Caption = "复原力(N)";
            this.gridColumn2.FieldName = "Fmf";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 210;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.cbMinMax);
            this.panelControl1.Controls.Add(this.cbTemp);
            this.panelControl1.Controls.Add(this.lblTitle);
            this.panelControl1.Location = new System.Drawing.Point(13, 16);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1033, 41);
            this.panelControl1.TabIndex = 115;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Location = new System.Drawing.Point(688, 18);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(133, 16);
            this.labelControl1.TabIndex = 105;
            this.labelControl1.Text = "零点/峰值输出:";
            // 
            // cbMinMax
            // 
            this.cbMinMax.Location = new System.Drawing.Point(827, 12);
            this.cbMinMax.Name = "cbMinMax";
            this.cbMinMax.Properties.OffText = "";
            this.cbMinMax.Properties.OnText = "";
            this.cbMinMax.Size = new System.Drawing.Size(72, 25);
            this.cbMinMax.TabIndex = 104;
            this.cbMinMax.Toggled += new System.EventHandler(this.cbMinMax_Toggled);
            // 
            // cbTemp
            // 
            this.cbTemp.Location = new System.Drawing.Point(905, 15);
            this.cbTemp.Name = "cbTemp";
            this.cbTemp.Properties.Caption = "目标对比";
            this.cbTemp.Size = new System.Drawing.Size(123, 19);
            this.cbTemp.TabIndex = 15;
            this.cbTemp.CheckedChanged += new System.EventHandler(this.cbTemp_CheckedChanged);
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblTitle.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Location = new System.Drawing.Point(2, 2);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1029, 37);
            this.lblTitle.TabIndex = 101;
            this.lblTitle.Text = "XXXXX";
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn6});
            this.gridView2.GridControl = this.gridControlResults;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.OptionsBehavior.KeepGroupExpandedOnSorting = false;
            this.gridView2.OptionsCustomization.AllowColumnMoving = false;
            this.gridView2.OptionsCustomization.AllowColumnResizing = false;
            this.gridView2.OptionsCustomization.AllowFilter = false;
            this.gridView2.OptionsCustomization.AllowGroup = false;
            this.gridView2.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridView2.OptionsCustomization.AllowSort = false;
            this.gridView2.OptionsDetail.ShowDetailTabs = false;
            this.gridView2.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.False;
            this.gridView2.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.False;
            this.gridView2.OptionsSelection.ShowCheckBoxSelectorInPrintExport = DevExpress.Utils.DefaultBoolean.False;
            this.gridView2.OptionsView.ShowDetailButtons = false;
            this.gridView2.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridView2.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            this.gridView2.OptionsView.ShowIndicator = false;
            this.gridView2.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridView2_CustomDrawCell);
            // 
            // gridControlResults
            // 
            this.gridControlResults.Location = new System.Drawing.Point(522, 365);
            this.gridControlResults.MainView = this.gridView2;
            this.gridControlResults.Name = "gridControlResults";
            this.gridControlResults.Size = new System.Drawing.Size(524, 289);
            this.gridControlResults.TabIndex = 110;
            this.gridControlResults.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn4,
            this.gridColumn5});
            this.gridView1.GridControl = this.gridControlTestInfo;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.KeepGroupExpandedOnSorting = false;
            this.gridView1.OptionsCustomization.AllowColumnMoving = false;
            this.gridView1.OptionsCustomization.AllowColumnResizing = false;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsCustomization.AllowQuickHideColumns = false;
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
            this.gridView1.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridView1_CustomDrawCell);
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn4.Caption = "参数";
            this.gridColumn4.FieldName = "key";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 0;
            this.gridColumn4.Width = 62;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn5.Caption = "数据";
            this.gridColumn5.FieldName = "value";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 1;
            this.gridColumn5.Width = 63;
            // 
            // gridControlTestInfo
            // 
            this.gridControlTestInfo.Location = new System.Drawing.Point(13, 365);
            this.gridControlTestInfo.MainView = this.gridView1;
            this.gridControlTestInfo.Name = "gridControlTestInfo";
            this.gridControlTestInfo.Size = new System.Drawing.Size(510, 289);
            this.gridControlTestInfo.TabIndex = 109;
            this.gridControlTestInfo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.tChartF_V);
            this.panelControl3.Location = new System.Drawing.Point(522, 56);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(524, 310);
            this.panelControl3.TabIndex = 112;
            // 
            // tChartF_V
            // 
            // 
            // 
            // 
            this.tChartF_V.Aspect.View3D = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_V.Axes.Bottom.AxisPen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_V.Axes.Bottom.AxisPen.Width = 1;
            // 
            // 
            // 
            this.tChartF_V.Axes.Bottom.Grid.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_V.Axes.Bottom.Grid.Style = System.Drawing.Drawing2D.DashStyle.Solid;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_V.Axes.Bottom.Labels.Font.Name = "Times New Roman";
            this.tChartF_V.Axes.Bottom.Labels.Font.Size = 10;
            this.tChartF_V.Axes.Bottom.Labels.Font.SizeFloat = 10F;
            // 
            // 
            // 
            this.tChartF_V.Axes.Bottom.MinorTicks.Visible = false;
            // 
            // 
            // 
            this.tChartF_V.Axes.Bottom.Ticks.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_V.Axes.Bottom.Ticks.Length = 2;
            // 
            // 
            // 
            this.tChartF_V.Axes.Bottom.TicksInner.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            // 
            // 
            // 
            this.tChartF_V.Axes.Bottom.Title.Caption = "速度(m/s)";
            // 
            // 
            // 
            this.tChartF_V.Axes.Bottom.Title.Font.Name = "Times New Roman";
            this.tChartF_V.Axes.Bottom.Title.Lines = new string[] {
        "速度(m/s)"};
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_V.Axes.Depth.AxisPen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_V.Axes.Depth.AxisPen.Width = 1;
            // 
            // 
            // 
            this.tChartF_V.Axes.Depth.Grid.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_V.Axes.Depth.Grid.Style = System.Drawing.Drawing2D.DashStyle.Solid;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_V.Axes.Depth.Labels.Font.Name = "Times New Roman";
            this.tChartF_V.Axes.Depth.Labels.Font.Size = 10;
            this.tChartF_V.Axes.Depth.Labels.Font.SizeFloat = 10F;
            // 
            // 
            // 
            this.tChartF_V.Axes.Depth.MinorTicks.Visible = false;
            // 
            // 
            // 
            this.tChartF_V.Axes.Depth.Ticks.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_V.Axes.Depth.Ticks.Length = 2;
            // 
            // 
            // 
            this.tChartF_V.Axes.Depth.TicksInner.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_V.Axes.Depth.Title.Font.Name = "Times New Roman";
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_V.Axes.DepthTop.AxisPen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_V.Axes.DepthTop.AxisPen.Width = 1;
            // 
            // 
            // 
            this.tChartF_V.Axes.DepthTop.Grid.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_V.Axes.DepthTop.Grid.Style = System.Drawing.Drawing2D.DashStyle.Solid;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_V.Axes.DepthTop.Labels.Font.Name = "Times New Roman";
            this.tChartF_V.Axes.DepthTop.Labels.Font.Size = 10;
            this.tChartF_V.Axes.DepthTop.Labels.Font.SizeFloat = 10F;
            // 
            // 
            // 
            this.tChartF_V.Axes.DepthTop.MinorTicks.Visible = false;
            // 
            // 
            // 
            this.tChartF_V.Axes.DepthTop.Ticks.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_V.Axes.DepthTop.Ticks.Length = 2;
            // 
            // 
            // 
            this.tChartF_V.Axes.DepthTop.TicksInner.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_V.Axes.DepthTop.Title.Font.Name = "Times New Roman";
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_V.Axes.Left.AxisPen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_V.Axes.Left.AxisPen.Width = 1;
            // 
            // 
            // 
            this.tChartF_V.Axes.Left.Grid.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_V.Axes.Left.Grid.Style = System.Drawing.Drawing2D.DashStyle.Solid;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_V.Axes.Left.Labels.Font.Name = "Times New Roman";
            this.tChartF_V.Axes.Left.Labels.Font.Size = 10;
            this.tChartF_V.Axes.Left.Labels.Font.SizeFloat = 10F;
            // 
            // 
            // 
            this.tChartF_V.Axes.Left.MinorTicks.Visible = false;
            // 
            // 
            // 
            this.tChartF_V.Axes.Left.Ticks.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_V.Axes.Left.Ticks.Length = 2;
            // 
            // 
            // 
            this.tChartF_V.Axes.Left.TicksInner.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            // 
            // 
            // 
            this.tChartF_V.Axes.Left.Title.Caption = "力(N)";
            // 
            // 
            // 
            this.tChartF_V.Axes.Left.Title.Font.Name = "Times New Roman";
            this.tChartF_V.Axes.Left.Title.Lines = new string[] {
        "力(N)"};
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_V.Axes.Right.AxisPen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_V.Axes.Right.AxisPen.Width = 1;
            // 
            // 
            // 
            this.tChartF_V.Axes.Right.Grid.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_V.Axes.Right.Grid.Style = System.Drawing.Drawing2D.DashStyle.Solid;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_V.Axes.Right.Labels.Font.Name = "Times New Roman";
            this.tChartF_V.Axes.Right.Labels.Font.Size = 10;
            this.tChartF_V.Axes.Right.Labels.Font.SizeFloat = 10F;
            // 
            // 
            // 
            this.tChartF_V.Axes.Right.MinorTicks.Visible = false;
            // 
            // 
            // 
            this.tChartF_V.Axes.Right.Ticks.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_V.Axes.Right.Ticks.Length = 2;
            // 
            // 
            // 
            this.tChartF_V.Axes.Right.TicksInner.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_V.Axes.Right.Title.Font.Name = "Times New Roman";
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_V.Axes.Top.AxisPen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_V.Axes.Top.AxisPen.Width = 1;
            // 
            // 
            // 
            this.tChartF_V.Axes.Top.Grid.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_V.Axes.Top.Grid.Style = System.Drawing.Drawing2D.DashStyle.Solid;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_V.Axes.Top.Labels.Font.Name = "Times New Roman";
            this.tChartF_V.Axes.Top.Labels.Font.Size = 10;
            this.tChartF_V.Axes.Top.Labels.Font.SizeFloat = 10F;
            // 
            // 
            // 
            this.tChartF_V.Axes.Top.MinorTicks.Visible = false;
            // 
            // 
            // 
            this.tChartF_V.Axes.Top.Ticks.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_V.Axes.Top.Ticks.Length = 2;
            // 
            // 
            // 
            this.tChartF_V.Axes.Top.TicksInner.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_V.Axes.Top.Title.Font.Name = "Times New Roman";
            this.tChartF_V.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tChartF_V.Enabled = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_V.Header.Brush.Gradient.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.tChartF_V.Header.Brush.Gradient.MiddleColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_V.Header.Brush.Gradient.SigmaFocus = 0F;
            this.tChartF_V.Header.Brush.Gradient.SigmaScale = 0F;
            this.tChartF_V.Header.Brush.Gradient.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_V.Header.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_V.Header.Font.Name = "Times New Roman";
            this.tChartF_V.Header.Font.Size = 12;
            this.tChartF_V.Header.Font.SizeFloat = 12F;
            this.tChartF_V.Header.Lines = new string[] {
        "速度-力"};
            // 
            // 
            // 
            this.tChartF_V.Header.Pen.Visible = false;
            // 
            // 
            // 
            this.tChartF_V.Header.Shadow.Height = 0;
            this.tChartF_V.Header.Shadow.Width = 0;
            this.tChartF_V.Header.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_V.Legend.Brush.Gradient.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.tChartF_V.Legend.Brush.Gradient.MiddleColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_V.Legend.Brush.Gradient.SigmaFocus = 0F;
            this.tChartF_V.Legend.Brush.Gradient.SigmaScale = 0F;
            this.tChartF_V.Legend.Brush.Gradient.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.tChartF_V.Legend.Font.Name = "Times New Roman";
            this.tChartF_V.Legend.Font.Size = 10;
            this.tChartF_V.Legend.Font.SizeFloat = 10F;
            this.tChartF_V.Legend.HorizMargin = 1;
            // 
            // 
            // 
            this.tChartF_V.Legend.Pen.Visible = false;
            // 
            // 
            // 
            this.tChartF_V.Legend.Shadow.Height = 0;
            this.tChartF_V.Legend.Shadow.Width = 0;
            // 
            // 
            // 
            this.tChartF_V.Legend.Symbol.Width = 25;
            this.tChartF_V.Legend.TopLeftPos = 1;
            this.tChartF_V.Legend.Transparent = true;
            this.tChartF_V.Legend.Visible = false;
            this.tChartF_V.Location = new System.Drawing.Point(2, 2);
            this.tChartF_V.Name = "tChartF_V";
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_V.Panel.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.tChartF_V.Panel.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.tChartF_V.Panel.Brush.Gradient.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.tChartF_V.Panel.Brush.Gradient.MiddleColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_V.Panel.Brush.Gradient.SigmaFocus = 0F;
            this.tChartF_V.Panel.Brush.Gradient.SigmaScale = 0F;
            this.tChartF_V.Panel.Brush.Gradient.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tChartF_V.Panel.Brush.Gradient.UseMiddle = false;
            this.tChartF_V.Panel.Brush.Gradient.Visible = false;
            // 
            // 
            // 
            this.tChartF_V.Panel.Shadow.Height = 0;
            this.tChartF_V.Panel.Shadow.Width = 0;
            this.tChartF_V.Size = new System.Drawing.Size(520, 306);
            this.tChartF_V.TabIndex = 14;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_V.Walls.Back.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.tChartF_V.Walls.Back.Brush.Gradient.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.tChartF_V.Walls.Back.Brush.Gradient.MiddleColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_V.Walls.Back.Brush.Gradient.SigmaFocus = 0F;
            this.tChartF_V.Walls.Back.Brush.Gradient.SigmaScale = 0F;
            this.tChartF_V.Walls.Back.Brush.Gradient.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tChartF_V.Walls.Back.Brush.Gradient.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_V.Walls.Bottom.Brush.Gradient.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.tChartF_V.Walls.Bottom.Brush.Gradient.MiddleColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_V.Walls.Bottom.Brush.Gradient.SigmaFocus = 0F;
            this.tChartF_V.Walls.Bottom.Brush.Gradient.SigmaScale = 0F;
            this.tChartF_V.Walls.Bottom.Brush.Gradient.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_V.Walls.Left.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.tChartF_V.Walls.Left.Brush.Gradient.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.tChartF_V.Walls.Left.Brush.Gradient.MiddleColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_V.Walls.Left.Brush.Gradient.SigmaFocus = 0F;
            this.tChartF_V.Walls.Left.Brush.Gradient.SigmaScale = 0F;
            this.tChartF_V.Walls.Left.Brush.Gradient.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartF_V.Walls.Right.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.tChartF_V.Walls.Right.Brush.Gradient.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.tChartF_V.Walls.Right.Brush.Gradient.MiddleColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartF_V.Walls.Right.Brush.Gradient.SigmaFocus = 0F;
            this.tChartF_V.Walls.Right.Brush.Gradient.SigmaScale = 0F;
            this.tChartF_V.Walls.Right.Brush.Gradient.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.tChartF_D);
            this.panelControl2.Location = new System.Drawing.Point(13, 56);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(510, 310);
            this.panelControl2.TabIndex = 111;
            // 
            // sbtnSubmit
            // 
            this.sbtnSubmit.Location = new System.Drawing.Point(419, 665);
            this.sbtnSubmit.Name = "sbtnSubmit";
            this.sbtnSubmit.Size = new System.Drawing.Size(101, 25);
            this.sbtnSubmit.TabIndex = 116;
            this.sbtnSubmit.Text = "生成报告";
            this.sbtnSubmit.Click += new System.EventHandler(this.sbtnSubmit_Click);
            // 
            // sbtnClose
            // 
            this.sbtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sbtnClose.Location = new System.Drawing.Point(526, 665);
            this.sbtnClose.Name = "sbtnClose";
            this.sbtnClose.Size = new System.Drawing.Size(75, 25);
            this.sbtnClose.TabIndex = 117;
            this.sbtnClose.Text = "关闭";
            this.sbtnClose.Click += new System.EventHandler(this.sbtnClose_Click);
            // 
            // ReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.sbtnClose;
            this.ClientSize = new System.Drawing.Size(1046, 703);
            this.ControlBox = false;
            this.Controls.Add(this.sbtnClose);
            this.Controls.Add(this.sbtnSubmit);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.gridControlTestInfo);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.gridControlResults);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReportForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "生成报告";
            this.Load += new System.EventHandler(this.ReportForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbMinMax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbTemp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlTestInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private Steema.TeeChart.TChart tChartF_D;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.GridControl gridControlResults;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.GridControl gridControlTestInfo;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private Steema.TeeChart.TChart tChartF_V;
        private DevExpress.XtraEditors.SimpleButton sbtnSubmit;
        private DevExpress.XtraEditors.SimpleButton sbtnClose;
        private DevExpress.XtraEditors.CheckEdit cbTemp;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ToggleSwitch cbMinMax;



    }
}