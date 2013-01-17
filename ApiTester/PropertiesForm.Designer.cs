namespace WebApiInterface
{
    partial class PropertiesForm
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
            this.PropertiesList = new System.Windows.Forms.ListBox();
            this.btnAddProperty = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PropertiesList
            // 
            this.PropertiesList.FormattingEnabled = true;
            this.PropertiesList.Location = new System.Drawing.Point(12, 12);
            this.PropertiesList.Name = "PropertiesList";
            this.PropertiesList.Size = new System.Drawing.Size(180, 186);
            this.PropertiesList.TabIndex = 0;
            // 
            // btnAddProperty
            // 
            this.btnAddProperty.Location = new System.Drawing.Point(12, 204);
            this.btnAddProperty.Name = "btnAddProperty";
            this.btnAddProperty.Size = new System.Drawing.Size(81, 23);
            this.btnAddProperty.TabIndex = 1;
            this.btnAddProperty.Text = "Add Property";
            this.btnAddProperty.UseVisualStyleBackColor = true;
            this.btnAddProperty.Click += new System.EventHandler(this.btnAddProperty_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(111, 204);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(81, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // PropertiesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(201, 230);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAddProperty);
            this.Controls.Add(this.PropertiesList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "PropertiesForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Session Properties";
            this.Load += new System.EventHandler(this.PropertiesForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox PropertiesList;
        private System.Windows.Forms.Button btnAddProperty;
        private System.Windows.Forms.Button btnCancel;
    }
}