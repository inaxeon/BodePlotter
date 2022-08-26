namespace BodePlotter
{
    partial class SettingsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ddlMeasurementDevice = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ddlSourceDevice = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnBackgroundColor = new System.Windows.Forms.Button();
            this.btnGridColor = new System.Windows.Forms.Button();
            this.btnFontColor = new System.Windows.Forms.Button();
            this.btnRefColor = new System.Windows.Forms.Button();
            this.btnActualColor = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnAxisFont = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRefPlot = new System.Windows.Forms.TextBox();
            this.txtActualPlot = new System.Windows.Forms.TextBox();
            this.btnDefault = new System.Windows.Forms.Button();
            this.btnRescan = new System.Windows.Forms.Button();
            this.txtFillOpacity = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source device:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ddlMeasurementDevice);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.ddlSourceDevice);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(444, 91);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Hardware setup";
            // 
            // ddlMeasurementDevice
            // 
            this.ddlMeasurementDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlMeasurementDevice.FormattingEnabled = true;
            this.ddlMeasurementDevice.Location = new System.Drawing.Point(130, 55);
            this.ddlMeasurementDevice.Name = "ddlMeasurementDevice";
            this.ddlMeasurementDevice.Size = new System.Drawing.Size(291, 21);
            this.ddlMeasurementDevice.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Measurement device:";
            // 
            // ddlSourceDevice
            // 
            this.ddlSourceDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlSourceDevice.FormattingEnabled = true;
            this.ddlSourceDevice.Location = new System.Drawing.Point(130, 24);
            this.ddlSourceDevice.Name = "ddlSourceDevice";
            this.ddlSourceDevice.Size = new System.Drawing.Size(291, 21);
            this.ddlSourceDevice.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(366, 285);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(90, 29);
            this.btnOK.TabIndex = 14;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(270, 285);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 29);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtFillOpacity);
            this.groupBox2.Controls.Add(this.btnBackgroundColor);
            this.groupBox2.Controls.Add(this.btnGridColor);
            this.groupBox2.Controls.Add(this.btnFontColor);
            this.groupBox2.Controls.Add(this.btnRefColor);
            this.groupBox2.Controls.Add(this.btnActualColor);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.btnAxisFont);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtRefPlot);
            this.groupBox2.Controls.Add(this.txtActualPlot);
            this.groupBox2.Location = new System.Drawing.Point(13, 109);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(443, 170);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Graph configuration";
            // 
            // btnBackgroundColor
            // 
            this.btnBackgroundColor.Location = new System.Drawing.Point(375, 105);
            this.btnBackgroundColor.Name = "btnBackgroundColor";
            this.btnBackgroundColor.Size = new System.Drawing.Size(45, 22);
            this.btnBackgroundColor.TabIndex = 9;
            this.btnBackgroundColor.UseVisualStyleBackColor = true;
            this.btnBackgroundColor.Click += new System.EventHandler(this.btnBackgroundColor_Click);
            // 
            // btnGridColor
            // 
            this.btnGridColor.Location = new System.Drawing.Point(243, 105);
            this.btnGridColor.Name = "btnGridColor";
            this.btnGridColor.Size = new System.Drawing.Size(45, 22);
            this.btnGridColor.TabIndex = 8;
            this.btnGridColor.UseVisualStyleBackColor = true;
            this.btnGridColor.Click += new System.EventHandler(this.btnGridColor_Click);
            // 
            // btnFontColor
            // 
            this.btnFontColor.Location = new System.Drawing.Point(128, 105);
            this.btnFontColor.Name = "btnFontColor";
            this.btnFontColor.Size = new System.Drawing.Size(45, 22);
            this.btnFontColor.TabIndex = 7;
            this.btnFontColor.UseVisualStyleBackColor = true;
            this.btnFontColor.Click += new System.EventHandler(this.btnFontColor_Click);
            // 
            // btnRefColor
            // 
            this.btnRefColor.Location = new System.Drawing.Point(388, 49);
            this.btnRefColor.Name = "btnRefColor";
            this.btnRefColor.Size = new System.Drawing.Size(32, 22);
            this.btnRefColor.TabIndex = 5;
            this.btnRefColor.UseVisualStyleBackColor = true;
            this.btnRefColor.Click += new System.EventHandler(this.btnRefColor_Click);
            // 
            // btnActualColor
            // 
            this.btnActualColor.Location = new System.Drawing.Point(388, 19);
            this.btnActualColor.Name = "btnActualColor";
            this.btnActualColor.Size = new System.Drawing.Size(32, 22);
            this.btnActualColor.TabIndex = 3;
            this.btnActualColor.UseVisualStyleBackColor = true;
            this.btnActualColor.Click += new System.EventHandler(this.btnActualColor_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(301, 110);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(68, 13);
            this.label11.TabIndex = 13;
            this.label11.Text = "Background:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(179, 110);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "Grid color:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 109);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Front color:";
            // 
            // btnAxisFont
            // 
            this.btnAxisFont.Location = new System.Drawing.Point(128, 77);
            this.btnAxisFont.Name = "btnAxisFont";
            this.btnAxisFont.Size = new System.Drawing.Size(75, 23);
            this.btnAxisFont.TabIndex = 6;
            this.btnAxisFont.Text = "Select...";
            this.btnAxisFont.UseVisualStyleBackColor = true;
            this.btnAxisFont.Click += new System.EventHandler(this.btnAxisFont_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Axis font:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Reference plot label:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Actual plot label:";
            // 
            // txtRefPlot
            // 
            this.txtRefPlot.Location = new System.Drawing.Point(129, 50);
            this.txtRefPlot.Name = "txtRefPlot";
            this.txtRefPlot.Size = new System.Drawing.Size(253, 20);
            this.txtRefPlot.TabIndex = 4;
            // 
            // txtActualPlot
            // 
            this.txtActualPlot.Location = new System.Drawing.Point(129, 20);
            this.txtActualPlot.Name = "txtActualPlot";
            this.txtActualPlot.Size = new System.Drawing.Size(253, 20);
            this.txtActualPlot.TabIndex = 2;
            // 
            // btnDefault
            // 
            this.btnDefault.Location = new System.Drawing.Point(174, 285);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(90, 29);
            this.btnDefault.TabIndex = 12;
            this.btnDefault.Text = "Default";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // btnRescan
            // 
            this.btnRescan.Location = new System.Drawing.Point(78, 285);
            this.btnRescan.Name = "btnRescan";
            this.btnRescan.Size = new System.Drawing.Size(90, 29);
            this.btnRescan.TabIndex = 11;
            this.btnRescan.Text = "Re-scan";
            this.btnRescan.UseVisualStyleBackColor = true;
            this.btnRescan.Click += new System.EventHandler(this.btnRescan_Click);
            // 
            // txtFillOpacity
            // 
            this.txtFillOpacity.Location = new System.Drawing.Point(129, 133);
            this.txtFillOpacity.Name = "txtFillOpacity";
            this.txtFillOpacity.Size = new System.Drawing.Size(44, 20);
            this.txtFillOpacity.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 136);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Fill opacity (%):";
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(468, 326);
            this.Controls.Add(this.btnRescan);
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox ddlMeasurementDevice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ddlSourceDevice;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRefPlot;
        private System.Windows.Forms.TextBox txtActualPlot;
        private System.Windows.Forms.Button btnAxisFont;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnDefault;
        private System.Windows.Forms.Button btnRescan;
        private System.Windows.Forms.Button btnActualColor;
        private System.Windows.Forms.Button btnRefColor;
        private System.Windows.Forms.Button btnBackgroundColor;
        private System.Windows.Forms.Button btnGridColor;
        private System.Windows.Forms.Button btnFontColor;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtFillOpacity;
    }
}