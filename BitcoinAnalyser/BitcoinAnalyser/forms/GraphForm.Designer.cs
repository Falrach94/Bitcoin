namespace BitcoinAnalyser
{
	partial class GraphForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphForm));
			System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Custom Marker", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Trade Marker", System.Windows.Forms.HorizontalAlignment.Left);
			this.groupBox7 = new System.Windows.Forms.GroupBox();
			this.cb_autoReload = new System.Windows.Forms.CheckBox();
			this.cb_record = new System.Windows.Forms.CheckBox();
			this.cb_refreshJson = new System.Windows.Forms.CheckBox();
			this.button_refreshCourse = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.MonotonyLabel = new System.Windows.Forms.Label();
			this.AboveLabel = new System.Windows.Forms.Label();
			this.label29 = new System.Windows.Forms.Label();
			this.label33 = new System.Windows.Forms.Label();
			this.label36 = new System.Windows.Forms.Label();
			this.SpecStateLabel = new System.Windows.Forms.Label();
			this.ResStateLabel = new System.Windows.Forms.Label();
			this.MoreThanLabel = new System.Windows.Forms.Label();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.cb_max = new System.Windows.Forms.CheckBox();
			this.cb_bs = new System.Windows.Forms.CheckBox();
			this.cb_min = new System.Windows.Forms.CheckBox();
			this.cb_cur = new System.Windows.Forms.CheckBox();
			this.cb_peaks = new System.Windows.Forms.CheckBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.cb_12 = new System.Windows.Forms.CheckBox();
			this.cb_3 = new System.Windows.Forms.CheckBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.cb_buy = new System.Windows.Forms.CheckBox();
			this.cb_sell = new System.Windows.Forms.CheckBox();
			this.cb_av = new System.Windows.Forms.CheckBox();
			this.cb_peak = new System.Windows.Forms.CheckBox();
			this.cb_peakSmall = new System.Windows.Forms.CheckBox();
			this.drawPanel = new System.Windows.Forms.Panel();
			this.label22 = new System.Windows.Forms.Label();
			this.label35 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.GraphTimeBoxH = new System.Windows.Forms.TextBox();
			this.tb_padding = new System.Windows.Forms.TextBox();
			this.CourseBrowser = new System.Windows.Forms.WebBrowser();
			this.RefreshCourseGraph = new System.Windows.Forms.Timer(this.components);
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.label_time = new System.Windows.Forms.ToolStripStatusLabel();
			this.label_value = new System.Windows.Forms.ToolStripStatusLabel();
			this.groupBox9 = new System.Windows.Forms.GroupBox();
			this.label52 = new System.Windows.Forms.Label();
			this.num_smooth = new System.Windows.Forms.NumericUpDown();
			this.cb_hidePeak = new System.Windows.Forms.CheckBox();
			this.cb_smooth = new System.Windows.Forms.CheckBox();
			this.AutoReloadOfferData = new System.Windows.Forms.Timer(this.components);
			this.panel1 = new System.Windows.Forms.Panel();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.ScrollX = new System.Windows.Forms.ToolStripButton();
			this.ScrollY = new System.Windows.Forms.ToolStripButton();
			this.ZoomY = new System.Windows.Forms.ToolStripButton();
			this.ZoomX = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.CursorH = new System.Windows.Forms.ToolStripButton();
			this.CursorV = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.MarkTool = new System.Windows.Forms.ToolStripButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lv_marker = new System.Windows.Forms.ListView();
			this.cb_markerVisibility = new System.Windows.Forms.CheckBox();
			this.cb_1perc = new System.Windows.Forms.CheckBox();
			this.cb_0perc = new System.Windows.Forms.CheckBox();
			this.cb_priceLine = new System.Windows.Forms.CheckBox();
			this.button_saveMarker = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.cb_dateLine = new System.Windows.Forms.CheckBox();
			this.cb_snapToNow = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.tb_procPos = new System.Windows.Forms.TrackBar();
			this.panel2 = new System.Windows.Forms.Panel();
			this.tb_mixedScore = new System.Windows.Forms.TrackBar();
			this.cb_mixedScore = new System.Windows.Forms.CheckBox();
			this.tb_sellScore = new System.Windows.Forms.TrackBar();
			this.tb_buyScore = new System.Windows.Forms.TrackBar();
			this.tb_bds = new System.Windows.Forms.TrackBar();
			this.tb_score = new System.Windows.Forms.TrackBar();
			this.cb_gradBuyDivGradSell = new System.Windows.Forms.CheckBox();
			this.tb_gbdgs = new System.Windows.Forms.TrackBar();
			this.cb_gradSell = new System.Windows.Forms.CheckBox();
			this.tb_gs = new System.Windows.Forms.TrackBar();
			this.cb_sellScore = new System.Windows.Forms.CheckBox();
			this.cb_buyScore = new System.Windows.Forms.CheckBox();
			this.cb_score = new System.Windows.Forms.CheckBox();
			this.tb_gb = new System.Windows.Forms.TrackBar();
			this.cb_gradBuy = new System.Windows.Forms.CheckBox();
			this.cb_buyDivSell = new System.Windows.Forms.CheckBox();
			this.groupBox7.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.drawPanel.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.groupBox9.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.num_smooth)).BeginInit();
			this.panel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tb_procPos)).BeginInit();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tb_mixedScore)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tb_sellScore)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tb_buyScore)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tb_bds)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tb_score)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tb_gbdgs)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tb_gs)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tb_gb)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox7
			// 
			this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox7.Controls.Add(this.cb_autoReload);
			this.groupBox7.Controls.Add(this.cb_record);
			this.groupBox7.Controls.Add(this.cb_refreshJson);
			this.groupBox7.Controls.Add(this.button_refreshCourse);
			this.groupBox7.Controls.Add(this.button4);
			this.groupBox7.Controls.Add(this.button5);
			this.groupBox7.Location = new System.Drawing.Point(600, 582);
			this.groupBox7.Name = "groupBox7";
			this.groupBox7.Size = new System.Drawing.Size(166, 232);
			this.groupBox7.TabIndex = 28;
			this.groupBox7.TabStop = false;
			this.groupBox7.Text = "Settings";
			// 
			// cb_autoReload
			// 
			this.cb_autoReload.AutoSize = true;
			this.cb_autoReload.Checked = true;
			this.cb_autoReload.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cb_autoReload.Location = new System.Drawing.Point(20, 195);
			this.cb_autoReload.Name = "cb_autoReload";
			this.cb_autoReload.Size = new System.Drawing.Size(110, 21);
			this.cb_autoReload.TabIndex = 3;
			this.cb_autoReload.Text = "auto reaload";
			this.cb_autoReload.UseVisualStyleBackColor = true;
			this.cb_autoReload.CheckedChanged += new System.EventHandler(this.cb_autoReload_CheckedChanged);
			// 
			// cb_record
			// 
			this.cb_record.AutoSize = true;
			this.cb_record.Location = new System.Drawing.Point(16, 31);
			this.cb_record.Name = "cb_record";
			this.cb_record.Size = new System.Drawing.Size(111, 21);
			this.cb_record.TabIndex = 3;
			this.cb_record.Text = "record offers";
			this.cb_record.UseVisualStyleBackColor = true;
			this.cb_record.CheckedChanged += new System.EventHandler(this.cb_record_CheckedChanged);
			// 
			// cb_refreshJson
			// 
			this.cb_refreshJson.AutoSize = true;
			this.cb_refreshJson.Checked = true;
			this.cb_refreshJson.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cb_refreshJson.Location = new System.Drawing.Point(16, 57);
			this.cb_refreshJson.Name = "cb_refreshJson";
			this.cb_refreshJson.Size = new System.Drawing.Size(114, 21);
			this.cb_refreshJson.TabIndex = 3;
			this.cb_refreshJson.Text = "Refresh Json";
			this.cb_refreshJson.UseVisualStyleBackColor = true;
			this.cb_refreshJson.CheckedChanged += new System.EventHandler(this.cbRefreshJson_CheckedChanged);
			// 
			// button_refreshCourse
			// 
			this.button_refreshCourse.Location = new System.Drawing.Point(16, 108);
			this.button_refreshCourse.Name = "button_refreshCourse";
			this.button_refreshCourse.Size = new System.Drawing.Size(133, 23);
			this.button_refreshCourse.TabIndex = 8;
			this.button_refreshCourse.Text = "Refresh Course Data";
			this.button_refreshCourse.UseVisualStyleBackColor = true;
			this.button_refreshCourse.Click += new System.EventHandler(this.button_refreshCourse_Click);
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(16, 137);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(133, 23);
			this.button4.TabIndex = 8;
			this.button4.Text = "Repaint";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(16, 166);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(133, 23);
			this.button5.TabIndex = 8;
			this.button5.Text = "Reload Offer Data";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// groupBox6
			// 
			this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox6.Controls.Add(this.MonotonyLabel);
			this.groupBox6.Controls.Add(this.AboveLabel);
			this.groupBox6.Controls.Add(this.label29);
			this.groupBox6.Controls.Add(this.label33);
			this.groupBox6.Controls.Add(this.label36);
			this.groupBox6.Controls.Add(this.SpecStateLabel);
			this.groupBox6.Controls.Add(this.ResStateLabel);
			this.groupBox6.Controls.Add(this.MoreThanLabel);
			this.groupBox6.Location = new System.Drawing.Point(73, 255);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(281, 232);
			this.groupBox6.TabIndex = 27;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "auto buy info";
			this.groupBox6.Visible = false;
			// 
			// MonotonyLabel
			// 
			this.MonotonyLabel.AutoSize = true;
			this.MonotonyLabel.Location = new System.Drawing.Point(22, 29);
			this.MonotonyLabel.Name = "MonotonyLabel";
			this.MonotonyLabel.Size = new System.Drawing.Size(47, 17);
			this.MonotonyLabel.TabIndex = 5;
			this.MonotonyLabel.Text = "Rising";
			// 
			// AboveLabel
			// 
			this.AboveLabel.AutoSize = true;
			this.AboveLabel.Location = new System.Drawing.Point(22, 63);
			this.AboveLabel.Name = "AboveLabel";
			this.AboveLabel.Size = new System.Drawing.Size(76, 17);
			this.AboveLabel.TabIndex = 6;
			this.AboveLabel.Text = "Above 12h";
			// 
			// label29
			// 
			this.label29.AutoSize = true;
			this.label29.Location = new System.Drawing.Point(52, 80);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(104, 17);
			this.label29.TabIndex = 6;
			this.label29.Text = "more than 12h:";
			// 
			// label33
			// 
			this.label33.AutoSize = true;
			this.label33.Location = new System.Drawing.Point(22, 124);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(86, 17);
			this.label33.TabIndex = 6;
			this.label33.Text = "Speculation:";
			// 
			// label36
			// 
			this.label36.AutoSize = true;
			this.label36.Location = new System.Drawing.Point(22, 153);
			this.label36.Name = "label36";
			this.label36.Size = new System.Drawing.Size(65, 17);
			this.label36.TabIndex = 6;
			this.label36.Text = "Reserve:";
			// 
			// SpecStateLabel
			// 
			this.SpecStateLabel.AutoSize = true;
			this.SpecStateLabel.Location = new System.Drawing.Point(114, 124);
			this.SpecStateLabel.Name = "SpecStateLabel";
			this.SpecStateLabel.Size = new System.Drawing.Size(132, 17);
			this.SpecStateLabel.TabIndex = 6;
			this.SpecStateLabel.Text = "Searching buy offer";
			// 
			// ResStateLabel
			// 
			this.ResStateLabel.AutoSize = true;
			this.ResStateLabel.Location = new System.Drawing.Point(114, 153);
			this.ResStateLabel.Name = "ResStateLabel";
			this.ResStateLabel.Size = new System.Drawing.Size(132, 17);
			this.ResStateLabel.TabIndex = 6;
			this.ResStateLabel.Text = "Searching buy offer";
			// 
			// MoreThanLabel
			// 
			this.MoreThanLabel.AutoSize = true;
			this.MoreThanLabel.Location = new System.Drawing.Point(162, 80);
			this.MoreThanLabel.Name = "MoreThanLabel";
			this.MoreThanLabel.Size = new System.Drawing.Size(31, 17);
			this.MoreThanLabel.TabIndex = 6;
			this.MoreThanLabel.Text = "N/A";
			// 
			// groupBox5
			// 
			this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox5.Controls.Add(this.cb_max);
			this.groupBox5.Controls.Add(this.cb_bs);
			this.groupBox5.Controls.Add(this.cb_min);
			this.groupBox5.Controls.Add(this.cb_cur);
			this.groupBox5.Controls.Add(this.cb_peaks);
			this.groupBox5.Location = new System.Drawing.Point(438, 582);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(156, 232);
			this.groupBox5.TabIndex = 26;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "extra info";
			// 
			// cb_max
			// 
			this.cb_max.AutoSize = true;
			this.cb_max.Location = new System.Drawing.Point(23, 31);
			this.cb_max.Name = "cb_max";
			this.cb_max.Size = new System.Drawing.Size(97, 21);
			this.cb_max.TabIndex = 7;
			this.cb_max.Text = "max. value";
			this.cb_max.UseVisualStyleBackColor = true;
			this.cb_max.CheckedChanged += new System.EventHandler(this.drawSettingCheckbox_CheckedChanged);
			// 
			// cb_bs
			// 
			this.cb_bs.AutoSize = true;
			this.cb_bs.Checked = true;
			this.cb_bs.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cb_bs.Location = new System.Drawing.Point(23, 195);
			this.cb_bs.Name = "cb_bs";
			this.cb_bs.Size = new System.Drawing.Size(127, 21);
			this.cb_bs.TabIndex = 7;
			this.cb_bs.Text = "current buy/sell";
			this.cb_bs.UseVisualStyleBackColor = true;
			this.cb_bs.CheckedChanged += new System.EventHandler(this.drawSettingCheckbox_CheckedChanged);
			// 
			// cb_min
			// 
			this.cb_min.AutoSize = true;
			this.cb_min.Location = new System.Drawing.Point(23, 58);
			this.cb_min.Name = "cb_min";
			this.cb_min.Size = new System.Drawing.Size(94, 21);
			this.cb_min.TabIndex = 7;
			this.cb_min.Text = "min. value";
			this.cb_min.UseVisualStyleBackColor = true;
			this.cb_min.CheckedChanged += new System.EventHandler(this.drawSettingCheckbox_CheckedChanged);
			// 
			// cb_cur
			// 
			this.cb_cur.AutoSize = true;
			this.cb_cur.Checked = true;
			this.cb_cur.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cb_cur.Location = new System.Drawing.Point(23, 85);
			this.cb_cur.Name = "cb_cur";
			this.cb_cur.Size = new System.Drawing.Size(92, 21);
			this.cb_cur.TabIndex = 7;
			this.cb_cur.Text = "cur. value";
			this.cb_cur.UseVisualStyleBackColor = true;
			this.cb_cur.CheckedChanged += new System.EventHandler(this.drawSettingCheckbox_CheckedChanged);
			// 
			// cb_peaks
			// 
			this.cb_peaks.AutoSize = true;
			this.cb_peaks.Location = new System.Drawing.Point(23, 141);
			this.cb_peaks.Name = "cb_peaks";
			this.cb_peaks.Size = new System.Drawing.Size(68, 21);
			this.cb_peaks.TabIndex = 7;
			this.cb_peaks.Text = "peaks";
			this.cb_peaks.UseVisualStyleBackColor = true;
			this.cb_peaks.CheckedChanged += new System.EventHandler(this.drawSettingCheckbox_CheckedChanged);
			// 
			// groupBox4
			// 
			this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox4.Controls.Add(this.cb_12);
			this.groupBox4.Controls.Add(this.cb_3);
			this.groupBox4.Location = new System.Drawing.Point(20, 582);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(191, 58);
			this.groupBox4.TabIndex = 25;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "trades graph";
			// 
			// cb_12
			// 
			this.cb_12.AutoSize = true;
			this.cb_12.ForeColor = System.Drawing.Color.Red;
			this.cb_12.Location = new System.Drawing.Point(23, 21);
			this.cb_12.Name = "cb_12";
			this.cb_12.Size = new System.Drawing.Size(54, 21);
			this.cb_12.TabIndex = 7;
			this.cb_12.Text = "12h";
			this.cb_12.UseVisualStyleBackColor = true;
			this.cb_12.CheckedChanged += new System.EventHandler(this.drawSettingCheckbox_CheckedChanged);
			// 
			// cb_3
			// 
			this.cb_3.AutoSize = true;
			this.cb_3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.cb_3.Location = new System.Drawing.Point(122, 21);
			this.cb_3.Name = "cb_3";
			this.cb_3.Size = new System.Drawing.Size(46, 21);
			this.cb_3.TabIndex = 7;
			this.cb_3.Text = "3h";
			this.cb_3.UseVisualStyleBackColor = true;
			this.cb_3.CheckedChanged += new System.EventHandler(this.drawSettingCheckbox_CheckedChanged);
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox3.Controls.Add(this.cb_buy);
			this.groupBox3.Controls.Add(this.cb_sell);
			this.groupBox3.Controls.Add(this.cb_av);
			this.groupBox3.Controls.Add(this.cb_peak);
			this.groupBox3.Controls.Add(this.cb_peakSmall);
			this.groupBox3.Location = new System.Drawing.Point(21, 646);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(190, 168);
			this.groupBox3.TabIndex = 24;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "offer graph";
			// 
			// cb_buy
			// 
			this.cb_buy.AutoSize = true;
			this.cb_buy.Checked = true;
			this.cb_buy.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cb_buy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.cb_buy.Location = new System.Drawing.Point(22, 21);
			this.cb_buy.Name = "cb_buy";
			this.cb_buy.Size = new System.Drawing.Size(53, 21);
			this.cb_buy.TabIndex = 7;
			this.cb_buy.Text = "buy";
			this.cb_buy.UseVisualStyleBackColor = true;
			this.cb_buy.CheckedChanged += new System.EventHandler(this.drawSettingCheckbox_CheckedChanged);
			// 
			// cb_sell
			// 
			this.cb_sell.AutoSize = true;
			this.cb_sell.Checked = true;
			this.cb_sell.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cb_sell.ForeColor = System.Drawing.Color.Green;
			this.cb_sell.Location = new System.Drawing.Point(121, 21);
			this.cb_sell.Name = "cb_sell";
			this.cb_sell.Size = new System.Drawing.Size(51, 21);
			this.cb_sell.TabIndex = 7;
			this.cb_sell.Text = "sell";
			this.cb_sell.UseVisualStyleBackColor = true;
			this.cb_sell.CheckedChanged += new System.EventHandler(this.drawSettingCheckbox_CheckedChanged);
			// 
			// cb_av
			// 
			this.cb_av.AutoSize = true;
			this.cb_av.Checked = true;
			this.cb_av.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cb_av.Location = new System.Drawing.Point(22, 77);
			this.cb_av.Name = "cb_av";
			this.cb_av.Size = new System.Drawing.Size(82, 21);
			this.cb_av.TabIndex = 7;
			this.cb_av.Text = "average";
			this.cb_av.UseVisualStyleBackColor = true;
			this.cb_av.CheckedChanged += new System.EventHandler(this.drawSettingCheckbox_CheckedChanged);
			// 
			// cb_peak
			// 
			this.cb_peak.AutoSize = true;
			this.cb_peak.Location = new System.Drawing.Point(22, 104);
			this.cb_peak.Name = "cb_peak";
			this.cb_peak.Size = new System.Drawing.Size(61, 21);
			this.cb_peak.TabIndex = 7;
			this.cb_peak.Text = "peak";
			this.cb_peak.UseVisualStyleBackColor = true;
			this.cb_peak.CheckedChanged += new System.EventHandler(this.drawSettingCheckbox_CheckedChanged);
			// 
			// cb_peakSmall
			// 
			this.cb_peakSmall.AutoSize = true;
			this.cb_peakSmall.Location = new System.Drawing.Point(22, 131);
			this.cb_peakSmall.Name = "cb_peakSmall";
			this.cb_peakSmall.Size = new System.Drawing.Size(134, 21);
			this.cb_peakSmall.TabIndex = 7;
			this.cb_peakSmall.Text = "peak (0.01 BTC)";
			this.cb_peakSmall.UseVisualStyleBackColor = true;
			this.cb_peakSmall.CheckedChanged += new System.EventHandler(this.drawSettingCheckbox_CheckedChanged);
			// 
			// drawPanel
			// 
			this.drawPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.drawPanel.BackColor = System.Drawing.Color.White;
			this.drawPanel.Controls.Add(this.groupBox6);
			this.drawPanel.Cursor = System.Windows.Forms.Cursors.Cross;
			this.drawPanel.Location = new System.Drawing.Point(10, 86);
			this.drawPanel.Name = "drawPanel";
			this.drawPanel.Size = new System.Drawing.Size(1499, 490);
			this.drawPanel.TabIndex = 23;
			this.drawPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.drawPanel_Paint);
			this.drawPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.drawPanel_MouseClick);
			this.drawPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.drawPanel_MouseDown);
			this.drawPanel.MouseHover += new System.EventHandler(this.drawPanel_MouseHover);
			this.drawPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drawPanel_MouseMove);
			// 
			// label22
			// 
			this.label22.AutoSize = true;
			this.label22.Location = new System.Drawing.Point(204, 12);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(16, 17);
			this.label22.TabIndex = 19;
			this.label22.Text = "h";
			// 
			// label35
			// 
			this.label35.AutoSize = true;
			this.label35.Location = new System.Drawing.Point(645, 9);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(63, 17);
			this.label35.TabIndex = 20;
			this.label35.Text = "padding:";
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.Location = new System.Drawing.Point(27, 9);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(65, 17);
			this.label20.TabIndex = 22;
			this.label20.Text = "timespan";
			// 
			// GraphTimeBoxH
			// 
			this.GraphTimeBoxH.Location = new System.Drawing.Point(98, 9);
			this.GraphTimeBoxH.Name = "GraphTimeBoxH";
			this.GraphTimeBoxH.Size = new System.Drawing.Size(100, 22);
			this.GraphTimeBoxH.TabIndex = 16;
			this.GraphTimeBoxH.Text = "24";
			this.GraphTimeBoxH.TextChanged += new System.EventHandler(this.GraphTimeBoxH_TextChanged);
			// 
			// tb_padding
			// 
			this.tb_padding.Location = new System.Drawing.Point(710, 6);
			this.tb_padding.Name = "tb_padding";
			this.tb_padding.Size = new System.Drawing.Size(100, 22);
			this.tb_padding.TabIndex = 17;
			this.tb_padding.Text = "100";
			// 
			// CourseBrowser
			// 
			this.CourseBrowser.Dock = System.Windows.Forms.DockStyle.Right;
			this.CourseBrowser.Location = new System.Drawing.Point(1894, 0);
			this.CourseBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			this.CourseBrowser.Name = "CourseBrowser";
			this.CourseBrowser.Size = new System.Drawing.Size(20, 863);
			this.CourseBrowser.TabIndex = 15;
			this.CourseBrowser.Url = new System.Uri("", System.UriKind.Relative);
			this.CourseBrowser.Visible = false;
			// 
			// RefreshCourseGraph
			// 
			this.RefreshCourseGraph.Enabled = true;
			this.RefreshCourseGraph.Interval = 60000;
			this.RefreshCourseGraph.Tick += new System.EventHandler(this.RefreshCourseGraph_Tick);
			// 
			// statusStrip1
			// 
			this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.label_time,
            this.label_value});
			this.statusStrip1.Location = new System.Drawing.Point(0, 838);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(1894, 25);
			this.statusStrip1.TabIndex = 29;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// label_time
			// 
			this.label_time.Name = "label_time";
			this.label_time.Size = new System.Drawing.Size(78, 20);
			this.label_time.Text = "label_time";
			// 
			// label_value
			// 
			this.label_value.Name = "label_value";
			this.label_value.Size = new System.Drawing.Size(83, 20);
			this.label_value.Text = "label_value";
			// 
			// groupBox9
			// 
			this.groupBox9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox9.Controls.Add(this.label52);
			this.groupBox9.Controls.Add(this.num_smooth);
			this.groupBox9.Controls.Add(this.cb_hidePeak);
			this.groupBox9.Controls.Add(this.cb_smooth);
			this.groupBox9.Location = new System.Drawing.Point(217, 582);
			this.groupBox9.Name = "groupBox9";
			this.groupBox9.Size = new System.Drawing.Size(215, 232);
			this.groupBox9.TabIndex = 11;
			this.groupBox9.TabStop = false;
			this.groupBox9.Text = "presentation";
			// 
			// label52
			// 
			this.label52.AutoSize = true;
			this.label52.Location = new System.Drawing.Point(14, 59);
			this.label52.Name = "label52";
			this.label52.Size = new System.Drawing.Size(105, 17);
			this.label52.TabIndex = 9;
			this.label52.Text = "sample number";
			// 
			// num_smooth
			// 
			this.num_smooth.Location = new System.Drawing.Point(125, 57);
			this.num_smooth.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.num_smooth.Name = "num_smooth";
			this.num_smooth.Size = new System.Drawing.Size(67, 22);
			this.num_smooth.TabIndex = 8;
			this.num_smooth.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.num_smooth.ValueChanged += new System.EventHandler(this.drawSettingCheckbox_CheckedChanged);
			// 
			// cb_hidePeak
			// 
			this.cb_hidePeak.AutoSize = true;
			this.cb_hidePeak.Checked = true;
			this.cb_hidePeak.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cb_hidePeak.Location = new System.Drawing.Point(23, 120);
			this.cb_hidePeak.Name = "cb_hidePeak";
			this.cb_hidePeak.Size = new System.Drawing.Size(124, 21);
			this.cb_hidePeak.TabIndex = 7;
			this.cb_hidePeak.Text = "hide sell peaks";
			this.cb_hidePeak.UseVisualStyleBackColor = true;
			this.cb_hidePeak.CheckedChanged += new System.EventHandler(this.drawSettingCheckbox_CheckedChanged);
			// 
			// cb_smooth
			// 
			this.cb_smooth.AutoSize = true;
			this.cb_smooth.Checked = true;
			this.cb_smooth.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cb_smooth.Location = new System.Drawing.Point(23, 28);
			this.cb_smooth.Name = "cb_smooth";
			this.cb_smooth.Size = new System.Drawing.Size(169, 21);
			this.cb_smooth.TabIndex = 7;
			this.cb_smooth.Text = "smooth buy/sell graph";
			this.cb_smooth.UseVisualStyleBackColor = true;
			this.cb_smooth.CheckedChanged += new System.EventHandler(this.drawSettingCheckbox_CheckedChanged);
			// 
			// AutoReloadOfferData
			// 
			this.AutoReloadOfferData.Enabled = true;
			this.AutoReloadOfferData.Interval = 30000;
			this.AutoReloadOfferData.Tick += new System.EventHandler(this.AutoReloadOfferData_Tick);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.toolStrip1);
			this.panel1.Location = new System.Drawing.Point(10, 46);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(898, 34);
			this.panel1.TabIndex = 30;
			// 
			// toolStrip1
			// 
			this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ScrollX,
            this.ScrollY,
            this.ZoomY,
            this.ZoomX,
            this.toolStripSeparator1,
            this.CursorH,
            this.CursorV,
            this.toolStripSeparator2,
            this.MarkTool});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(898, 27);
			this.toolStrip1.Stretch = true;
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// ScrollX
			// 
			this.ScrollX.CheckOnClick = true;
			this.ScrollX.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ScrollX.Image = ((System.Drawing.Image)(resources.GetObject("ScrollX.Image")));
			this.ScrollX.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.ScrollX.Name = "ScrollX";
			this.ScrollX.Size = new System.Drawing.Size(24, 24);
			this.ScrollX.Text = "toolStripButton1";
			this.ScrollX.Click += new System.EventHandler(this.ScollX_Click);
			// 
			// ScrollY
			// 
			this.ScrollY.CheckOnClick = true;
			this.ScrollY.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ScrollY.Image = ((System.Drawing.Image)(resources.GetObject("ScrollY.Image")));
			this.ScrollY.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.ScrollY.Name = "ScrollY";
			this.ScrollY.Size = new System.Drawing.Size(24, 24);
			this.ScrollY.Text = "toolStripButton2";
			this.ScrollY.Click += new System.EventHandler(this.ScrollY_Click);
			// 
			// ZoomY
			// 
			this.ZoomY.Checked = true;
			this.ZoomY.CheckOnClick = true;
			this.ZoomY.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ZoomY.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ZoomY.Image = ((System.Drawing.Image)(resources.GetObject("ZoomY.Image")));
			this.ZoomY.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.ZoomY.Name = "ZoomY";
			this.ZoomY.Size = new System.Drawing.Size(24, 24);
			this.ZoomY.Text = "toolStripButton4";
			this.ZoomY.Click += new System.EventHandler(this.ZoomY_Click);
			// 
			// ZoomX
			// 
			this.ZoomX.CheckOnClick = true;
			this.ZoomX.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ZoomX.Image = ((System.Drawing.Image)(resources.GetObject("ZoomX.Image")));
			this.ZoomX.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.ZoomX.Name = "ZoomX";
			this.ZoomX.Size = new System.Drawing.Size(24, 24);
			this.ZoomX.Text = "toolStripButton3";
			this.ZoomX.Click += new System.EventHandler(this.ZoomX_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
			// 
			// CursorH
			// 
			this.CursorH.CheckOnClick = true;
			this.CursorH.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.CursorH.Image = ((System.Drawing.Image)(resources.GetObject("CursorH.Image")));
			this.CursorH.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.CursorH.Name = "CursorH";
			this.CursorH.Size = new System.Drawing.Size(24, 24);
			this.CursorH.Text = "CursorH";
			// 
			// CursorV
			// 
			this.CursorV.CheckOnClick = true;
			this.CursorV.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.CursorV.Image = ((System.Drawing.Image)(resources.GetObject("CursorV.Image")));
			this.CursorV.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.CursorV.Name = "CursorV";
			this.CursorV.Size = new System.Drawing.Size(24, 24);
			this.CursorV.Text = "CurV";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
			// 
			// MarkTool
			// 
			this.MarkTool.CheckOnClick = true;
			this.MarkTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.MarkTool.Image = ((System.Drawing.Image)(resources.GetObject("MarkTool.Image")));
			this.MarkTool.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.MarkTool.Name = "MarkTool";
			this.MarkTool.Size = new System.Drawing.Size(24, 24);
			this.MarkTool.Text = "place a marker";
			this.MarkTool.Click += new System.EventHandler(this.MarkTool_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox1.Controls.Add(this.lv_marker);
			this.groupBox1.Controls.Add(this.cb_markerVisibility);
			this.groupBox1.Controls.Add(this.cb_1perc);
			this.groupBox1.Controls.Add(this.cb_0perc);
			this.groupBox1.Controls.Add(this.cb_priceLine);
			this.groupBox1.Controls.Add(this.button_saveMarker);
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Controls.Add(this.cb_dateLine);
			this.groupBox1.Location = new System.Drawing.Point(772, 582);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(377, 232);
			this.groupBox1.TabIndex = 31;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Marker";
			// 
			// lv_marker
			// 
			this.lv_marker.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			listViewGroup1.Header = "Custom Marker";
			listViewGroup1.Name = "Custom";
			listViewGroup2.Header = "Trade Marker";
			listViewGroup2.Name = "Trades";
			this.lv_marker.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
			this.lv_marker.HideSelection = false;
			this.lv_marker.LabelWrap = false;
			this.lv_marker.Location = new System.Drawing.Point(6, 21);
			this.lv_marker.MultiSelect = false;
			this.lv_marker.Name = "lv_marker";
			this.lv_marker.Size = new System.Drawing.Size(187, 180);
			this.lv_marker.TabIndex = 0;
			this.lv_marker.UseCompatibleStateImageBehavior = false;
			this.lv_marker.View = System.Windows.Forms.View.SmallIcon;
			this.lv_marker.SelectedIndexChanged += new System.EventHandler(this.lv_marker_SelectedIndexChanged);
			this.lv_marker.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lv_marker_MouseDoubleClick);
			// 
			// cb_markerVisibility
			// 
			this.cb_markerVisibility.AutoSize = true;
			this.cb_markerVisibility.Location = new System.Drawing.Point(222, 168);
			this.cb_markerVisibility.Name = "cb_markerVisibility";
			this.cb_markerVisibility.Size = new System.Drawing.Size(117, 21);
			this.cb_markerVisibility.TabIndex = 3;
			this.cb_markerVisibility.Text = "marker visible";
			this.cb_markerVisibility.UseVisualStyleBackColor = true;
			this.cb_markerVisibility.CheckedChanged += new System.EventHandler(this.cb_dateLine_CheckedChanged);
			// 
			// cb_1perc
			// 
			this.cb_1perc.AutoSize = true;
			this.cb_1perc.Location = new System.Drawing.Point(222, 120);
			this.cb_1perc.Name = "cb_1perc";
			this.cb_1perc.Size = new System.Drawing.Size(101, 21);
			this.cb_1perc.TabIndex = 3;
			this.cb_1perc.Text = "1% sell line";
			this.cb_1perc.UseVisualStyleBackColor = true;
			this.cb_1perc.CheckedChanged += new System.EventHandler(this.cb_dateLine_CheckedChanged);
			// 
			// cb_0perc
			// 
			this.cb_0perc.AutoSize = true;
			this.cb_0perc.Location = new System.Drawing.Point(222, 93);
			this.cb_0perc.Name = "cb_0perc";
			this.cb_0perc.Size = new System.Drawing.Size(101, 21);
			this.cb_0perc.TabIndex = 3;
			this.cb_0perc.Text = "0% sell line";
			this.cb_0perc.UseVisualStyleBackColor = true;
			this.cb_0perc.CheckedChanged += new System.EventHandler(this.cb_dateLine_CheckedChanged);
			// 
			// cb_priceLine
			// 
			this.cb_priceLine.AutoSize = true;
			this.cb_priceLine.Location = new System.Drawing.Point(222, 58);
			this.cb_priceLine.Name = "cb_priceLine";
			this.cb_priceLine.Size = new System.Drawing.Size(84, 21);
			this.cb_priceLine.TabIndex = 3;
			this.cb_priceLine.Text = "date line";
			this.cb_priceLine.UseVisualStyleBackColor = true;
			this.cb_priceLine.CheckedChanged += new System.EventHandler(this.cb_dateLine_CheckedChanged);
			// 
			// button_saveMarker
			// 
			this.button_saveMarker.Location = new System.Drawing.Point(6, 203);
			this.button_saveMarker.Name = "button_saveMarker";
			this.button_saveMarker.Size = new System.Drawing.Size(187, 23);
			this.button_saveMarker.TabIndex = 8;
			this.button_saveMarker.Text = "Save";
			this.button_saveMarker.UseVisualStyleBackColor = true;
			this.button_saveMarker.Click += new System.EventHandler(this.button_saveMarker_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(222, 203);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(133, 23);
			this.button1.TabIndex = 8;
			this.button1.Text = "Delete";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// cb_dateLine
			// 
			this.cb_dateLine.AutoSize = true;
			this.cb_dateLine.Location = new System.Drawing.Point(222, 31);
			this.cb_dateLine.Name = "cb_dateLine";
			this.cb_dateLine.Size = new System.Drawing.Size(87, 21);
			this.cb_dateLine.TabIndex = 3;
			this.cb_dateLine.Text = "price line";
			this.cb_dateLine.UseVisualStyleBackColor = true;
			this.cb_dateLine.CheckedChanged += new System.EventHandler(this.cb_dateLine_CheckedChanged);
			// 
			// cb_snapToNow
			// 
			this.cb_snapToNow.AutoSize = true;
			this.cb_snapToNow.Checked = true;
			this.cb_snapToNow.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cb_snapToNow.Location = new System.Drawing.Point(239, 11);
			this.cb_snapToNow.Name = "cb_snapToNow";
			this.cb_snapToNow.Size = new System.Drawing.Size(106, 21);
			this.cb_snapToNow.TabIndex = 7;
			this.cb_snapToNow.Text = "snap to now";
			this.cb_snapToNow.UseVisualStyleBackColor = true;
			this.cb_snapToNow.CheckedChanged += new System.EventHandler(this.drawSettingCheckbox_CheckedChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox2.Controls.Add(this.tb_procPos);
			this.groupBox2.Controls.Add(this.panel2);
			this.groupBox2.Location = new System.Drawing.Point(1155, 582);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(354, 232);
			this.groupBox2.TabIndex = 27;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "processed data";
			// 
			// tb_procPos
			// 
			this.tb_procPos.Location = new System.Drawing.Point(314, 31);
			this.tb_procPos.Maximum = 1000;
			this.tb_procPos.Name = "tb_procPos";
			this.tb_procPos.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.tb_procPos.Size = new System.Drawing.Size(56, 195);
			this.tb_procPos.TabIndex = 9;
			this.tb_procPos.Value = 200;
			this.tb_procPos.Scroll += new System.EventHandler(this.drawSettingCheckbox_CheckedChanged);
			// 
			// panel2
			// 
			this.panel2.AutoScroll = true;
			this.panel2.Controls.Add(this.tb_mixedScore);
			this.panel2.Controls.Add(this.cb_mixedScore);
			this.panel2.Controls.Add(this.tb_sellScore);
			this.panel2.Controls.Add(this.tb_buyScore);
			this.panel2.Controls.Add(this.tb_bds);
			this.panel2.Controls.Add(this.tb_score);
			this.panel2.Controls.Add(this.cb_gradBuyDivGradSell);
			this.panel2.Controls.Add(this.tb_gbdgs);
			this.panel2.Controls.Add(this.cb_gradSell);
			this.panel2.Controls.Add(this.tb_gs);
			this.panel2.Controls.Add(this.cb_sellScore);
			this.panel2.Controls.Add(this.cb_buyScore);
			this.panel2.Controls.Add(this.cb_score);
			this.panel2.Controls.Add(this.tb_gb);
			this.panel2.Controls.Add(this.cb_gradBuy);
			this.panel2.Controls.Add(this.cb_buyDivSell);
			this.panel2.Location = new System.Drawing.Point(6, 21);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(302, 205);
			this.panel2.TabIndex = 28;
			// 
			// tb_mixedScore
			// 
			this.tb_mixedScore.Location = new System.Drawing.Point(3, 290);
			this.tb_mixedScore.Maximum = 1000;
			this.tb_mixedScore.Name = "tb_mixedScore";
			this.tb_mixedScore.Size = new System.Drawing.Size(104, 56);
			this.tb_mixedScore.TabIndex = 34;
			this.tb_mixedScore.Scroll += new System.EventHandler(this.drawSettingCheckbox_CheckedChanged);
			// 
			// cb_mixedScore
			// 
			this.cb_mixedScore.AutoSize = true;
			this.cb_mixedScore.Checked = true;
			this.cb_mixedScore.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cb_mixedScore.Location = new System.Drawing.Point(113, 290);
			this.cb_mixedScore.Name = "cb_mixedScore";
			this.cb_mixedScore.Size = new System.Drawing.Size(105, 21);
			this.cb_mixedScore.TabIndex = 33;
			this.cb_mixedScore.Text = "mixed score";
			this.cb_mixedScore.UseVisualStyleBackColor = true;
			// 
			// tb_sellScore
			// 
			this.tb_sellScore.Location = new System.Drawing.Point(3, 246);
			this.tb_sellScore.Maximum = 1000;
			this.tb_sellScore.Name = "tb_sellScore";
			this.tb_sellScore.Size = new System.Drawing.Size(104, 56);
			this.tb_sellScore.TabIndex = 9;
			this.tb_sellScore.Scroll += new System.EventHandler(this.drawSettingCheckbox_CheckedChanged);
			// 
			// tb_buyScore
			// 
			this.tb_buyScore.Location = new System.Drawing.Point(3, 198);
			this.tb_buyScore.Maximum = 1000;
			this.tb_buyScore.Name = "tb_buyScore";
			this.tb_buyScore.Size = new System.Drawing.Size(104, 56);
			this.tb_buyScore.TabIndex = 32;
			this.tb_buyScore.Scroll += new System.EventHandler(this.drawSettingCheckbox_CheckedChanged);
			// 
			// tb_bds
			// 
			this.tb_bds.Location = new System.Drawing.Point(3, 3);
			this.tb_bds.Maximum = 1000;
			this.tb_bds.Name = "tb_bds";
			this.tb_bds.Size = new System.Drawing.Size(104, 56);
			this.tb_bds.TabIndex = 8;
			this.tb_bds.Scroll += new System.EventHandler(this.drawSettingCheckbox_CheckedChanged);
			// 
			// tb_score
			// 
			this.tb_score.Location = new System.Drawing.Point(3, 155);
			this.tb_score.Maximum = 1000;
			this.tb_score.Name = "tb_score";
			this.tb_score.Size = new System.Drawing.Size(104, 56);
			this.tb_score.TabIndex = 8;
			this.tb_score.Scroll += new System.EventHandler(this.drawSettingCheckbox_CheckedChanged);
			// 
			// cb_gradBuyDivGradSell
			// 
			this.cb_gradBuyDivGradSell.AutoSize = true;
			this.cb_gradBuyDivGradSell.Location = new System.Drawing.Point(113, 120);
			this.cb_gradBuyDivGradSell.Name = "cb_gradBuyDivGradSell";
			this.cb_gradBuyDivGradSell.Size = new System.Drawing.Size(160, 21);
			this.cb_gradBuyDivGradSell.TabIndex = 7;
			this.cb_gradBuyDivGradSell.Text = "grad. buy / grad. sell";
			this.cb_gradBuyDivGradSell.UseVisualStyleBackColor = true;
			this.cb_gradBuyDivGradSell.CheckedChanged += new System.EventHandler(this.drawSettingCheckbox_CheckedChanged);
			// 
			// tb_gbdgs
			// 
			this.tb_gbdgs.Location = new System.Drawing.Point(3, 120);
			this.tb_gbdgs.Maximum = 1000;
			this.tb_gbdgs.Name = "tb_gbdgs";
			this.tb_gbdgs.Size = new System.Drawing.Size(104, 56);
			this.tb_gbdgs.TabIndex = 8;
			this.tb_gbdgs.Scroll += new System.EventHandler(this.drawSettingCheckbox_CheckedChanged);
			// 
			// cb_gradSell
			// 
			this.cb_gradSell.AutoSize = true;
			this.cb_gradSell.Location = new System.Drawing.Point(113, 85);
			this.cb_gradSell.Name = "cb_gradSell";
			this.cb_gradSell.Size = new System.Drawing.Size(88, 21);
			this.cb_gradSell.TabIndex = 7;
			this.cb_gradSell.Text = "grad. sell";
			this.cb_gradSell.UseVisualStyleBackColor = true;
			this.cb_gradSell.CheckedChanged += new System.EventHandler(this.drawSettingCheckbox_CheckedChanged);
			// 
			// tb_gs
			// 
			this.tb_gs.Location = new System.Drawing.Point(3, 83);
			this.tb_gs.Maximum = 1000;
			this.tb_gs.Name = "tb_gs";
			this.tb_gs.Size = new System.Drawing.Size(104, 56);
			this.tb_gs.TabIndex = 8;
			this.tb_gs.Scroll += new System.EventHandler(this.drawSettingCheckbox_CheckedChanged);
			// 
			// cb_sellScore
			// 
			this.cb_sellScore.AutoSize = true;
			this.cb_sellScore.Checked = true;
			this.cb_sellScore.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cb_sellScore.Location = new System.Drawing.Point(113, 244);
			this.cb_sellScore.Name = "cb_sellScore";
			this.cb_sellScore.Size = new System.Drawing.Size(90, 21);
			this.cb_sellScore.TabIndex = 7;
			this.cb_sellScore.Text = "sell score";
			this.cb_sellScore.UseVisualStyleBackColor = true;
			this.cb_sellScore.CheckedChanged += new System.EventHandler(this.drawSettingCheckbox_CheckedChanged);
			// 
			// cb_buyScore
			// 
			this.cb_buyScore.AutoSize = true;
			this.cb_buyScore.Checked = true;
			this.cb_buyScore.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cb_buyScore.Location = new System.Drawing.Point(113, 198);
			this.cb_buyScore.Name = "cb_buyScore";
			this.cb_buyScore.Size = new System.Drawing.Size(92, 21);
			this.cb_buyScore.TabIndex = 7;
			this.cb_buyScore.Text = "buy score";
			this.cb_buyScore.UseVisualStyleBackColor = true;
			this.cb_buyScore.CheckedChanged += new System.EventHandler(this.drawSettingCheckbox_CheckedChanged);
			// 
			// cb_score
			// 
			this.cb_score.AutoSize = true;
			this.cb_score.Checked = true;
			this.cb_score.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cb_score.Location = new System.Drawing.Point(113, 155);
			this.cb_score.Name = "cb_score";
			this.cb_score.Size = new System.Drawing.Size(65, 21);
			this.cb_score.TabIndex = 7;
			this.cb_score.Text = "score";
			this.cb_score.UseVisualStyleBackColor = true;
			this.cb_score.CheckedChanged += new System.EventHandler(this.drawSettingCheckbox_CheckedChanged);
			// 
			// tb_gb
			// 
			this.tb_gb.Location = new System.Drawing.Point(3, 43);
			this.tb_gb.Maximum = 1000;
			this.tb_gb.Name = "tb_gb";
			this.tb_gb.Size = new System.Drawing.Size(104, 56);
			this.tb_gb.TabIndex = 8;
			this.tb_gb.Scroll += new System.EventHandler(this.drawSettingCheckbox_CheckedChanged);
			// 
			// cb_gradBuy
			// 
			this.cb_gradBuy.AutoSize = true;
			this.cb_gradBuy.Location = new System.Drawing.Point(113, 43);
			this.cb_gradBuy.Name = "cb_gradBuy";
			this.cb_gradBuy.Size = new System.Drawing.Size(90, 21);
			this.cb_gradBuy.TabIndex = 7;
			this.cb_gradBuy.Text = "grad. buy";
			this.cb_gradBuy.UseVisualStyleBackColor = true;
			this.cb_gradBuy.CheckedChanged += new System.EventHandler(this.drawSettingCheckbox_CheckedChanged);
			// 
			// cb_buyDivSell
			// 
			this.cb_buyDivSell.AutoSize = true;
			this.cb_buyDivSell.Location = new System.Drawing.Point(113, 7);
			this.cb_buyDivSell.Name = "cb_buyDivSell";
			this.cb_buyDivSell.Size = new System.Drawing.Size(86, 21);
			this.cb_buyDivSell.TabIndex = 7;
			this.cb_buyDivSell.Text = "buy / sell";
			this.cb_buyDivSell.UseVisualStyleBackColor = true;
			this.cb_buyDivSell.CheckedChanged += new System.EventHandler(this.drawSettingCheckbox_CheckedChanged);
			// 
			// GraphForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(1914, 863);
			this.ControlBox = false;
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.cb_snapToNow);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.groupBox9);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.groupBox7);
			this.Controls.Add(this.groupBox5);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.drawPanel);
			this.Controls.Add(this.label22);
			this.Controls.Add(this.label35);
			this.Controls.Add(this.label20);
			this.Controls.Add(this.GraphTimeBoxH);
			this.Controls.Add(this.tb_padding);
			this.Controls.Add(this.CourseBrowser);
			this.Name = "GraphForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "GraphForm";
			this.Load += new System.EventHandler(this.GraphForm_Load);
			this.groupBox7.ResumeLayout(false);
			this.groupBox7.PerformLayout();
			this.groupBox6.ResumeLayout(false);
			this.groupBox6.PerformLayout();
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.drawPanel.ResumeLayout(false);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.groupBox9.ResumeLayout(false);
			this.groupBox9.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.num_smooth)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.tb_procPos)).EndInit();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.tb_mixedScore)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tb_sellScore)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tb_buyScore)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tb_bds)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tb_score)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tb_gbdgs)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tb_gs)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tb_gb)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox7;
		private System.Windows.Forms.CheckBox cb_record;
		private System.Windows.Forms.CheckBox cb_refreshJson;
		private System.Windows.Forms.Button button_refreshCourse;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.Label MonotonyLabel;
		private System.Windows.Forms.Label AboveLabel;
		private System.Windows.Forms.Label label29;
		private System.Windows.Forms.Label label33;
		private System.Windows.Forms.Label label36;
		private System.Windows.Forms.Label SpecStateLabel;
		private System.Windows.Forms.Label ResStateLabel;
		private System.Windows.Forms.Label MoreThanLabel;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.CheckBox cb_max;
		private System.Windows.Forms.CheckBox cb_bs;
		private System.Windows.Forms.CheckBox cb_min;
		private System.Windows.Forms.CheckBox cb_cur;
		private System.Windows.Forms.CheckBox cb_peaks;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.CheckBox cb_12;
		private System.Windows.Forms.CheckBox cb_3;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.CheckBox cb_buy;
		private System.Windows.Forms.CheckBox cb_sell;
		private System.Windows.Forms.CheckBox cb_av;
		private System.Windows.Forms.CheckBox cb_peak;
		private System.Windows.Forms.CheckBox cb_peakSmall;
		private System.Windows.Forms.Panel drawPanel;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.Label label35;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.TextBox GraphTimeBoxH;
		private System.Windows.Forms.TextBox tb_padding;
		private System.Windows.Forms.WebBrowser CourseBrowser;
		private System.Windows.Forms.Timer RefreshCourseGraph;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel label_time;
		private System.Windows.Forms.ToolStripStatusLabel label_value;
		private System.Windows.Forms.GroupBox groupBox9;
		private System.Windows.Forms.Label label52;
		private System.Windows.Forms.NumericUpDown num_smooth;
		private System.Windows.Forms.CheckBox cb_hidePeak;
		private System.Windows.Forms.CheckBox cb_smooth;
		private System.Windows.Forms.CheckBox cb_autoReload;
		private System.Windows.Forms.Timer AutoReloadOfferData;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton ScrollX;
		private System.Windows.Forms.ToolStripButton ScrollY;
		private System.Windows.Forms.ToolStripButton ZoomX;
		private System.Windows.Forms.ToolStripButton ZoomY;
		private System.Windows.Forms.ToolStripButton MarkTool;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ListView lv_marker;
		private System.Windows.Forms.CheckBox cb_0perc;
		private System.Windows.Forms.CheckBox cb_priceLine;
		private System.Windows.Forms.CheckBox cb_dateLine;
		private System.Windows.Forms.CheckBox cb_markerVisibility;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.CheckBox cb_1perc;
		private System.Windows.Forms.Button button_saveMarker;
		private System.Windows.Forms.CheckBox cb_snapToNow;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TrackBar tb_procPos;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.TrackBar tb_bds;
		private System.Windows.Forms.TrackBar tb_score;
		private System.Windows.Forms.CheckBox cb_gradBuyDivGradSell;
		private System.Windows.Forms.TrackBar tb_gbdgs;
		private System.Windows.Forms.CheckBox cb_gradSell;
		private System.Windows.Forms.TrackBar tb_gs;
		private System.Windows.Forms.CheckBox cb_score;
		private System.Windows.Forms.TrackBar tb_gb;
		private System.Windows.Forms.CheckBox cb_gradBuy;
		private System.Windows.Forms.CheckBox cb_buyDivSell;
		private System.Windows.Forms.TrackBar tb_sellScore;
		private System.Windows.Forms.TrackBar tb_buyScore;
		private System.Windows.Forms.CheckBox cb_sellScore;
		private System.Windows.Forms.CheckBox cb_buyScore;
		private System.Windows.Forms.TrackBar tb_mixedScore;
		private System.Windows.Forms.CheckBox cb_mixedScore;
		private System.Windows.Forms.ToolStripButton CursorH;
		private System.Windows.Forms.ToolStripButton CursorV;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
	}
}