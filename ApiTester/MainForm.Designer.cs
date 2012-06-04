namespace WebApiInterface
{
    partial class MainForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtClientID = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtQuality = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNetworkType = new System.Windows.Forms.TextBox();
            this.txtCallerID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtIvrID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSessionID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCallID = new System.Windows.Forms.TextBox();
            this.btnStartEndSession = new System.Windows.Forms.Button();
            this.btnGetProperties = new System.Windows.Forms.Button();
            this.btnSendUrl = new System.Windows.Forms.Button();
            this.btnSendText = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Client ID:";
            // 
            // txtClientID
            // 
            this.txtClientID.Location = new System.Drawing.Point(16, 30);
            this.txtClientID.Name = "txtClientID";
            this.txtClientID.Size = new System.Drawing.Size(100, 20);
            this.txtClientID.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtQuality);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtStatus);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtNetworkType);
            this.groupBox1.Controls.Add(this.txtCallerID);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtIvrID);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtSessionID);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(16, 114);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(338, 112);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Session Information";
            // 
            // txtQuality
            // 
            this.txtQuality.Enabled = false;
            this.txtQuality.Location = new System.Drawing.Point(230, 79);
            this.txtQuality.Name = "txtQuality";
            this.txtQuality.Size = new System.Drawing.Size(100, 20);
            this.txtQuality.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(227, 63);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Network Quality:";
            // 
            // txtStatus
            // 
            this.txtStatus.Enabled = false;
            this.txtStatus.Location = new System.Drawing.Point(121, 79);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(100, 20);
            this.txtStatus.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(118, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Connection Status:";
            // 
            // txtNetworkType
            // 
            this.txtNetworkType.Enabled = false;
            this.txtNetworkType.Location = new System.Drawing.Point(6, 80);
            this.txtNetworkType.Name = "txtNetworkType";
            this.txtNetworkType.Size = new System.Drawing.Size(100, 20);
            this.txtNetworkType.TabIndex = 7;
            // 
            // txtCallerID
            // 
            this.txtCallerID.Enabled = false;
            this.txtCallerID.Location = new System.Drawing.Point(230, 36);
            this.txtCallerID.Name = "txtCallerID";
            this.txtCallerID.Size = new System.Drawing.Size(100, 20);
            this.txtCallerID.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(227, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Caller ID:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Network Type:";
            // 
            // txtIvrID
            // 
            this.txtIvrID.Enabled = false;
            this.txtIvrID.Location = new System.Drawing.Point(121, 36);
            this.txtIvrID.Name = "txtIvrID";
            this.txtIvrID.Size = new System.Drawing.Size(100, 20);
            this.txtIvrID.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(118, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "IVR ID:";
            // 
            // txtSessionID
            // 
            this.txtSessionID.Enabled = false;
            this.txtSessionID.Location = new System.Drawing.Point(7, 36);
            this.txtSessionID.Name = "txtSessionID";
            this.txtSessionID.Size = new System.Drawing.Size(100, 20);
            this.txtSessionID.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Session ID:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 57);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Call ID:";
            // 
            // txtCallID
            // 
            this.txtCallID.Location = new System.Drawing.Point(16, 74);
            this.txtCallID.Name = "txtCallID";
            this.txtCallID.Size = new System.Drawing.Size(100, 20);
            this.txtCallID.TabIndex = 4;
            // 
            // btnStartEndSession
            // 
            this.btnStartEndSession.Location = new System.Drawing.Point(137, 30);
            this.btnStartEndSession.Name = "btnStartEndSession";
            this.btnStartEndSession.Size = new System.Drawing.Size(100, 23);
            this.btnStartEndSession.TabIndex = 5;
            this.btnStartEndSession.Text = "Start Session";
            this.btnStartEndSession.UseVisualStyleBackColor = true;
            this.btnStartEndSession.Click += new System.EventHandler(this.btnStartEndSession_Click);
            // 
            // btnGetProperties
            // 
            this.btnGetProperties.Location = new System.Drawing.Point(137, 70);
            this.btnGetProperties.Name = "btnGetProperties";
            this.btnGetProperties.Size = new System.Drawing.Size(100, 23);
            this.btnGetProperties.TabIndex = 6;
            this.btnGetProperties.Text = "Get Properties";
            this.btnGetProperties.UseVisualStyleBackColor = true;
            this.btnGetProperties.Click += new System.EventHandler(this.btnGetProperties_Click);
            // 
            // btnSendUrl
            // 
            this.btnSendUrl.Location = new System.Drawing.Point(246, 30);
            this.btnSendUrl.Name = "btnSendUrl";
            this.btnSendUrl.Size = new System.Drawing.Size(100, 23);
            this.btnSendUrl.TabIndex = 7;
            this.btnSendUrl.Text = "Send Url";
            this.btnSendUrl.UseVisualStyleBackColor = true;
            this.btnSendUrl.Click += new System.EventHandler(this.btnSendUrl_Click);
            // 
            // btnSendText
            // 
            this.btnSendText.Location = new System.Drawing.Point(246, 70);
            this.btnSendText.Name = "btnSendText";
            this.btnSendText.Size = new System.Drawing.Size(100, 23);
            this.btnSendText.TabIndex = 8;
            this.btnSendText.Text = "Send Text";
            this.btnSendText.UseVisualStyleBackColor = true;
            this.btnSendText.Click += new System.EventHandler(this.btnSendText_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 15000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 240);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(367, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(71, 17);
            this.toolStripStatusLabel1.Text = "No updates.";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 262);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnSendText);
            this.Controls.Add(this.btnSendUrl);
            this.Controls.Add(this.btnGetProperties);
            this.Controls.Add(this.btnStartEndSession);
            this.Controls.Add(this.txtCallID);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtClientID);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Web API Tester";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtClientID;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtQuality;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNetworkType;
        private System.Windows.Forms.TextBox txtCallerID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtIvrID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSessionID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtCallID;
        private System.Windows.Forms.Button btnStartEndSession;
        private System.Windows.Forms.Button btnGetProperties;
        private System.Windows.Forms.Button btnSendUrl;
        private System.Windows.Forms.Button btnSendText;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}

