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
            this.txtAPIKey = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSubmitAPIKey = new System.Windows.Forms.Button();
            this.btnGetData = new System.Windows.Forms.Button();
            this.cbxChampions = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtAPIKey
            // 
            this.txtAPIKey.Location = new System.Drawing.Point(201, 67);
            this.txtAPIKey.Name = "txtAPIKey";
            this.txtAPIKey.Size = new System.Drawing.Size(202, 20);
            this.txtAPIKey.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(125, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "API Key";
            // 
            // btnSubmitAPIKey
            // 
            this.btnSubmitAPIKey.Location = new System.Drawing.Point(419, 65);
            this.btnSubmitAPIKey.Name = "btnSubmitAPIKey";
            this.btnSubmitAPIKey.Size = new System.Drawing.Size(75, 23);
            this.btnSubmitAPIKey.TabIndex = 2;
            this.btnSubmitAPIKey.Text = "Submit";
            this.btnSubmitAPIKey.UseVisualStyleBackColor = true;
            this.btnSubmitAPIKey.Click += new System.EventHandler(this.btnSubmitAPIKey_Click);
            // 
            // btnGetData
            // 
            this.btnGetData.Location = new System.Drawing.Point(201, 113);
            this.btnGetData.Name = "btnGetData";
            this.btnGetData.Size = new System.Drawing.Size(202, 73);
            this.btnGetData.TabIndex = 3;
            this.btnGetData.Text = "Get Some Data!";
            this.btnGetData.UseVisualStyleBackColor = true;
            this.btnGetData.Click += new System.EventHandler(this.btnGetData_Click);
            // 
            // cbxChampions
            // 
            this.cbxChampions.FormattingEnabled = true;
            this.cbxChampions.Location = new System.Drawing.Point(128, 216);
            this.cbxChampions.Name = "cbxChampions";
            this.cbxChampions.Size = new System.Drawing.Size(121, 21);
            this.cbxChampions.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 219);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Champion";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbxChampions);
            this.Controls.Add(this.btnGetData);
            this.Controls.Add(this.btnSubmitAPIKey);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAPIKey);
            this.Name = "Main";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtAPIKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSubmitAPIKey;
        private System.Windows.Forms.Button btnGetData;
        private System.Windows.Forms.ComboBox cbxChampions;
        private System.Windows.Forms.Label label2;
    }
}

