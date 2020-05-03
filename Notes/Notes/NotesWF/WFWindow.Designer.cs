namespace NotesWF
{
    partial class WFWindow
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
            this.addRecord = new System.Windows.Forms.Button();
            this.up = new System.Windows.Forms.Button();
            this.down = new System.Windows.Forms.Button();
            this.removeRecord = new System.Windows.Forms.Button();
            this.writeRecords = new System.Windows.Forms.Button();
            this.table = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.table)).BeginInit();
            this.SuspendLayout();
            // 
            // addRecord
            // 
            this.addRecord.Location = new System.Drawing.Point(12, 12);
            this.addRecord.Name = "addRecord";
            this.addRecord.Size = new System.Drawing.Size(75, 23);
            this.addRecord.TabIndex = 0;
            this.addRecord.Text = "AddRecord";
            this.addRecord.UseVisualStyleBackColor = true;
            // 
            // up
            // 
            this.up.Location = new System.Drawing.Point(94, 12);
            this.up.Name = "up";
            this.up.Size = new System.Drawing.Size(57, 23);
            this.up.TabIndex = 1;
            this.up.Text = "Up";
            this.up.UseVisualStyleBackColor = true;
            // 
            // down
            // 
            this.down.Location = new System.Drawing.Point(158, 12);
            this.down.Name = "down";
            this.down.Size = new System.Drawing.Size(75, 23);
            this.down.TabIndex = 2;
            this.down.Text = "Down";
            this.down.UseVisualStyleBackColor = true;
            // 
            // removeRecord
            // 
            this.removeRecord.Location = new System.Drawing.Point(293, 12);
            this.removeRecord.Name = "removeRecord";
            this.removeRecord.Size = new System.Drawing.Size(91, 23);
            this.removeRecord.TabIndex = 3;
            this.removeRecord.Text = "RemoveRecord";
            this.removeRecord.UseVisualStyleBackColor = true;
            // 
            // writeRecords
            // 
            this.writeRecords.Location = new System.Drawing.Point(401, 12);
            this.writeRecords.Name = "writeRecords";
            this.writeRecords.Size = new System.Drawing.Size(93, 23);
            this.writeRecords.TabIndex = 4;
            this.writeRecords.Text = "WriteRecords";
            this.writeRecords.UseVisualStyleBackColor = true;
            // 
            // table
            // 
            this.table.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.table.AutoGenerateColumns = false;
            this.table.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.table.Location = new System.Drawing.Point(12, 41);
            this.table.Name = "table";
            this.table.RowHeadersVisible = false;
            this.table.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.table.Size = new System.Drawing.Size(482, 210);
            this.table.TabIndex = 5;
            this.table.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.table_DataError);
            // 
            // WFWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 263);
            this.Controls.Add(this.table);
            this.Controls.Add(this.writeRecords);
            this.Controls.Add(this.removeRecord);
            this.Controls.Add(this.down);
            this.Controls.Add(this.up);
            this.Controls.Add(this.addRecord);
            this.Name = "WFWindow";
            ((System.ComponentModel.ISupportInitialize)(this.table)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button addRecord;
        private System.Windows.Forms.Button up;
        private System.Windows.Forms.Button down;
        private System.Windows.Forms.Button removeRecord;
        private System.Windows.Forms.Button writeRecords;
        private System.Windows.Forms.DataGridView table;
    }
}