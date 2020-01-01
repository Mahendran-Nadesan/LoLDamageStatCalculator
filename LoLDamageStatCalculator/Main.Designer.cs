namespace LoLDamageStatCalculator
{
    partial class Main
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
            this.cbxChampions = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbOffline = new System.Windows.Forms.RadioButton();
            this.rbOnline = new System.Windows.Forms.RadioButton();
            this.cbxLevels = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnQ = new System.Windows.Forms.Button();
            this.btnW = new System.Windows.Forms.Button();
            this.btnE = new System.Windows.Forms.Button();
            this.btnR = new System.Windows.Forms.Button();
            this.txtSummary = new System.Windows.Forms.TextBox();
            this.btnPassive = new System.Windows.Forms.Button();
            this.webStats = new System.Windows.Forms.WebBrowser();
            this.cbxQLevel = new System.Windows.Forms.ComboBox();
            this.cbxWLevel = new System.Windows.Forms.ComboBox();
            this.cbxELevel = new System.Windows.Forms.ComboBox();
            this.cbxRLevel = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbxChampions
            // 
            this.cbxChampions.FormattingEnabled = true;
            this.cbxChampions.Location = new System.Drawing.Point(77, 61);
            this.cbxChampions.Name = "cbxChampions";
            this.cbxChampions.Size = new System.Drawing.Size(121, 21);
            this.cbxChampions.TabIndex = 4;
            this.cbxChampions.SelectedIndexChanged += new System.EventHandler(this.cbxChampions_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Champion";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbOffline);
            this.groupBox1.Controls.Add(this.rbOnline);
            this.groupBox1.Location = new System.Drawing.Point(405, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mode";
            // 
            // rbOffline
            // 
            this.rbOffline.AutoSize = true;
            this.rbOffline.Location = new System.Drawing.Point(37, 53);
            this.rbOffline.Name = "rbOffline";
            this.rbOffline.Size = new System.Drawing.Size(55, 17);
            this.rbOffline.TabIndex = 1;
            this.rbOffline.TabStop = true;
            this.rbOffline.Text = "Offline";
            this.rbOffline.UseVisualStyleBackColor = true;
            this.rbOffline.CheckedChanged += new System.EventHandler(this.rbOffline_CheckedChanged);
            // 
            // rbOnline
            // 
            this.rbOnline.AutoSize = true;
            this.rbOnline.Location = new System.Drawing.Point(37, 29);
            this.rbOnline.Name = "rbOnline";
            this.rbOnline.Size = new System.Drawing.Size(55, 17);
            this.rbOnline.TabIndex = 0;
            this.rbOnline.TabStop = true;
            this.rbOnline.Text = "Online";
            this.rbOnline.UseVisualStyleBackColor = true;
            this.rbOnline.CheckedChanged += new System.EventHandler(this.rbOnline_CheckedChanged);
            // 
            // cbxLevels
            // 
            this.cbxLevels.FormattingEnabled = true;
            this.cbxLevels.Location = new System.Drawing.Point(261, 61);
            this.cbxLevels.Name = "cbxLevels";
            this.cbxLevels.Size = new System.Drawing.Size(121, 21);
            this.cbxLevels.TabIndex = 7;
            this.cbxLevels.SelectedIndexChanged += new System.EventHandler(this.cbxLevels_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(225, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Level";
            // 
            // btnQ
            // 
            this.btnQ.Location = new System.Drawing.Point(217, 387);
            this.btnQ.Name = "btnQ";
            this.btnQ.Size = new System.Drawing.Size(50, 50);
            this.btnQ.TabIndex = 9;
            this.btnQ.Text = "Q";
            this.btnQ.UseVisualStyleBackColor = true;
            this.btnQ.Click += new System.EventHandler(this.btnQ_Click);
            // 
            // btnW
            // 
            this.btnW.Location = new System.Drawing.Point(306, 387);
            this.btnW.Name = "btnW";
            this.btnW.Size = new System.Drawing.Size(50, 50);
            this.btnW.TabIndex = 10;
            this.btnW.Text = "W";
            this.btnW.UseVisualStyleBackColor = true;
            this.btnW.Click += new System.EventHandler(this.btnW_Click);
            // 
            // btnE
            // 
            this.btnE.Location = new System.Drawing.Point(393, 387);
            this.btnE.Name = "btnE";
            this.btnE.Size = new System.Drawing.Size(50, 50);
            this.btnE.TabIndex = 11;
            this.btnE.Text = "E";
            this.btnE.UseVisualStyleBackColor = true;
            this.btnE.Click += new System.EventHandler(this.btnE_Click);
            // 
            // btnR
            // 
            this.btnR.Location = new System.Drawing.Point(471, 387);
            this.btnR.Name = "btnR";
            this.btnR.Size = new System.Drawing.Size(50, 50);
            this.btnR.TabIndex = 12;
            this.btnR.Text = "R";
            this.btnR.UseVisualStyleBackColor = true;
            this.btnR.Click += new System.EventHandler(this.btnR_Click);
            // 
            // txtSummary
            // 
            this.txtSummary.Enabled = false;
            this.txtSummary.Location = new System.Drawing.Point(23, 151);
            this.txtSummary.Multiline = true;
            this.txtSummary.Name = "txtSummary";
            this.txtSummary.Size = new System.Drawing.Size(582, 212);
            this.txtSummary.TabIndex = 13;
            // 
            // btnPassive
            // 
            this.btnPassive.Location = new System.Drawing.Point(77, 387);
            this.btnPassive.Name = "btnPassive";
            this.btnPassive.Size = new System.Drawing.Size(50, 50);
            this.btnPassive.TabIndex = 14;
            this.btnPassive.Text = "P";
            this.btnPassive.UseVisualStyleBackColor = true;
            this.btnPassive.Click += new System.EventHandler(this.btnPassive_Click);
            // 
            // webStats
            // 
            this.webStats.Location = new System.Drawing.Point(23, 492);
            this.webStats.MinimumSize = new System.Drawing.Size(20, 20);
            this.webStats.Name = "webStats";
            this.webStats.ScrollBarsEnabled = false;
            this.webStats.Size = new System.Drawing.Size(582, 240);
            this.webStats.TabIndex = 16;
            // 
            // cbxQLevel
            // 
            this.cbxQLevel.FormattingEnabled = true;
            this.cbxQLevel.Location = new System.Drawing.Point(217, 453);
            this.cbxQLevel.Name = "cbxQLevel";
            this.cbxQLevel.Size = new System.Drawing.Size(50, 21);
            this.cbxQLevel.TabIndex = 17;
            // 
            // cbxWLevel
            // 
            this.cbxWLevel.FormattingEnabled = true;
            this.cbxWLevel.Location = new System.Drawing.Point(306, 453);
            this.cbxWLevel.Name = "cbxWLevel";
            this.cbxWLevel.Size = new System.Drawing.Size(50, 21);
            this.cbxWLevel.TabIndex = 18;
            // 
            // cbxELevel
            // 
            this.cbxELevel.FormattingEnabled = true;
            this.cbxELevel.Location = new System.Drawing.Point(393, 453);
            this.cbxELevel.Name = "cbxELevel";
            this.cbxELevel.Size = new System.Drawing.Size(50, 21);
            this.cbxELevel.TabIndex = 19;
            // 
            // cbxRLevel
            // 
            this.cbxRLevel.FormattingEnabled = true;
            this.cbxRLevel.Location = new System.Drawing.Point(471, 453);
            this.cbxRLevel.Name = "cbxRLevel";
            this.cbxRLevel.Size = new System.Drawing.Size(50, 21);
            this.cbxRLevel.TabIndex = 20;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 865);
            this.Controls.Add(this.cbxRLevel);
            this.Controls.Add(this.cbxELevel);
            this.Controls.Add(this.cbxWLevel);
            this.Controls.Add(this.cbxQLevel);
            this.Controls.Add(this.webStats);
            this.Controls.Add(this.btnPassive);
            this.Controls.Add(this.txtSummary);
            this.Controls.Add(this.btnR);
            this.Controls.Add(this.btnE);
            this.Controls.Add(this.btnW);
            this.Controls.Add(this.btnQ);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbxLevels);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbxChampions);
            this.Name = "Main";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Main_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cbxChampions;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbOffline;
        private System.Windows.Forms.RadioButton rbOnline;
        private System.Windows.Forms.ComboBox cbxLevels;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnQ;
        private System.Windows.Forms.Button btnW;
        private System.Windows.Forms.Button btnE;
        private System.Windows.Forms.Button btnR;
        private System.Windows.Forms.TextBox txtSummary;
        private System.Windows.Forms.Button btnPassive;
        private System.Windows.Forms.WebBrowser webStats;
        private System.Windows.Forms.ComboBox cbxQLevel;
        private System.Windows.Forms.ComboBox cbxWLevel;
        private System.Windows.Forms.ComboBox cbxELevel;
        private System.Windows.Forms.ComboBox cbxRLevel;
    }
}

