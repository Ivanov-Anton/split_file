using System.ComponentModel;
using System.Windows.Forms;

namespace XlsSplitterWin7
{
    partial class MainForm
    {
        private IContainer components = null;
        private Label selectedFileLabel;
        private TextBox selectedFileTextBox;
        private Button openButton;
        private Button splitButton;
        private Label statusLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.selectedFileLabel = new System.Windows.Forms.Label();
            this.selectedFileTextBox = new System.Windows.Forms.TextBox();
            this.openButton = new System.Windows.Forms.Button();
            this.splitButton = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // selectedFileLabel
            // 
            this.selectedFileLabel.AutoSize = true;
            this.selectedFileLabel.Location = new System.Drawing.Point(12, 15);
            this.selectedFileLabel.Name = "selectedFileLabel";
            this.selectedFileLabel.Size = new System.Drawing.Size(76, 13);
            this.selectedFileLabel.TabIndex = 0;
            this.selectedFileLabel.Text = "Selected file:";
            // 
            // selectedFileTextBox
            // 
            this.selectedFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedFileTextBox.Location = new System.Drawing.Point(94, 12);
            this.selectedFileTextBox.Name = "selectedFileTextBox";
            this.selectedFileTextBox.ReadOnly = true;
            this.selectedFileTextBox.Size = new System.Drawing.Size(394, 20);
            this.selectedFileTextBox.TabIndex = 1;
            // 
            // openButton
            // 
            this.openButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.openButton.Location = new System.Drawing.Point(494, 10);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(94, 23);
            this.openButton.TabIndex = 2;
            this.openButton.Text = "Open XLS...";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // splitButton
            // 
            this.splitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.splitButton.Enabled = false;
            this.splitButton.Location = new System.Drawing.Point(494, 39);
            this.splitButton.Name = "splitButton";
            this.splitButton.Size = new System.Drawing.Size(94, 23);
            this.splitButton.TabIndex = 3;
            this.splitButton.Text = "Split";
            this.splitButton.UseVisualStyleBackColor = true;
            this.splitButton.Click += new System.EventHandler(this.SplitButton_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(12, 44);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(79, 13);
            this.statusLabel.TabIndex = 4;
            this.statusLabel.Text = "No file selected.";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 80);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.splitButton);
            this.Controls.Add(this.openButton);
            this.Controls.Add(this.selectedFileTextBox);
            this.Controls.Add(this.selectedFileLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(616, 119);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XLS Splitter (Win7)";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
