namespace LoLDamageStatCalculator
{
    partial class MainMenu
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
            this.btnChampions = new System.Windows.Forms.Button();
            this.btnItems = new System.Windows.Forms.Button();
            this.btnRunes = new System.Windows.Forms.Button();
            this.btnChampionBuilder = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbOffline = new System.Windows.Forms.RadioButton();
            this.rbOnline = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnChampions
            // 
            this.btnChampions.Location = new System.Drawing.Point(96, 216);
            this.btnChampions.Name = "btnChampions";
            this.btnChampions.Size = new System.Drawing.Size(100, 30);
            this.btnChampions.TabIndex = 0;
            this.btnChampions.Text = "Champions";
            this.btnChampions.UseVisualStyleBackColor = true;
            this.btnChampions.Click += new System.EventHandler(this.btnChampions_Click);
            // 
            // btnItems
            // 
            this.btnItems.Location = new System.Drawing.Point(96, 278);
            this.btnItems.Name = "btnItems";
            this.btnItems.Size = new System.Drawing.Size(100, 30);
            this.btnItems.TabIndex = 1;
            this.btnItems.Text = "Items";
            this.btnItems.UseVisualStyleBackColor = true;
            this.btnItems.Click += new System.EventHandler(this.btnItems_Click);
            // 
            // btnRunes
            // 
            this.btnRunes.Location = new System.Drawing.Point(96, 344);
            this.btnRunes.Name = "btnRunes";
            this.btnRunes.Size = new System.Drawing.Size(100, 30);
            this.btnRunes.TabIndex = 2;
            this.btnRunes.Text = "Runes";
            this.btnRunes.UseVisualStyleBackColor = true;
            this.btnRunes.Click += new System.EventHandler(this.btnRunes_Click);
            // 
            // btnChampionBuilder
            // 
            this.btnChampionBuilder.Location = new System.Drawing.Point(96, 156);
            this.btnChampionBuilder.Name = "btnChampionBuilder";
            this.btnChampionBuilder.Size = new System.Drawing.Size(100, 30);
            this.btnChampionBuilder.TabIndex = 3;
            this.btnChampionBuilder.Text = "Champion Builder";
            this.btnChampionBuilder.UseVisualStyleBackColor = true;
            this.btnChampionBuilder.Click += new System.EventHandler(this.btnChampionBuilder_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbOffline);
            this.groupBox1.Controls.Add(this.rbOnline);
            this.groupBox1.Location = new System.Drawing.Point(51, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 7;
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
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 412);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnChampionBuilder);
            this.Controls.Add(this.btnRunes);
            this.Controls.Add(this.btnItems);
            this.Controls.Add(this.btnChampions);
            this.Name = "MainMenu";
            this.Text = "Main Menu";
            this.Load += new System.EventHandler(this.MainMenu_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnChampions;
        private System.Windows.Forms.Button btnItems;
        private System.Windows.Forms.Button btnRunes;
        private System.Windows.Forms.Button btnChampionBuilder;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbOffline;
        private System.Windows.Forms.RadioButton rbOnline;
    }
}