namespace EF.GenericRepository
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
            this.gbConnection = new System.Windows.Forms.GroupBox();
            this.btnGenerateInfoLog = new System.Windows.Forms.Button();
            this.gbCriteria = new System.Windows.Forms.GroupBox();
            this.lMessage = new System.Windows.Forms.Label();
            this.lLevel = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.gbPaging = new System.Windows.Forms.GroupBox();
            this.lTotalCountValue = new System.Windows.Forms.Label();
            this.lTotalCount = new System.Windows.Forms.Label();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSortDesc = new System.Windows.Forms.TextBox();
            this.lPageSize = new System.Windows.Forms.Label();
            this.txtPageSize = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lPageIndex = new System.Windows.Forms.Label();
            this.txtSortAsc = new System.Windows.Forms.TextBox();
            this.txtPageIndex = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.cbLevel = new System.Windows.Forms.ComboBox();
            this.gbConnection.SuspendLayout();
            this.gbCriteria.SuspendLayout();
            this.gbPaging.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // gbConnection
            // 
            this.gbConnection.Controls.Add(this.btnGenerateInfoLog);
            this.gbConnection.Location = new System.Drawing.Point(17, 13);
            this.gbConnection.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbConnection.Name = "gbConnection";
            this.gbConnection.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbConnection.Size = new System.Drawing.Size(200, 85);
            this.gbConnection.TabIndex = 4;
            this.gbConnection.TabStop = false;
            this.gbConnection.Text = "Database";
            // 
            // btnGenerateInfoLog
            // 
            this.btnGenerateInfoLog.Location = new System.Drawing.Point(20, 33);
            this.btnGenerateInfoLog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGenerateInfoLog.Name = "btnGenerateInfoLog";
            this.btnGenerateInfoLog.Size = new System.Drawing.Size(147, 31);
            this.btnGenerateInfoLog.TabIndex = 7;
            this.btnGenerateInfoLog.Text = "Generate Info Logs";
            this.btnGenerateInfoLog.UseVisualStyleBackColor = true;
            this.btnGenerateInfoLog.Click += new System.EventHandler(this.btnGenerateInfoLog_Click);
            // 
            // gbCriteria
            // 
            this.gbCriteria.Controls.Add(this.cbLevel);
            this.gbCriteria.Controls.Add(this.lMessage);
            this.gbCriteria.Controls.Add(this.lLevel);
            this.gbCriteria.Controls.Add(this.txtMessage);
            this.gbCriteria.Location = new System.Drawing.Point(224, 13);
            this.gbCriteria.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbCriteria.Name = "gbCriteria";
            this.gbCriteria.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbCriteria.Size = new System.Drawing.Size(461, 85);
            this.gbCriteria.TabIndex = 5;
            this.gbCriteria.TabStop = false;
            this.gbCriteria.Text = "Criteria";
            // 
            // lMessage
            // 
            this.lMessage.AutoSize = true;
            this.lMessage.Location = new System.Drawing.Point(233, 44);
            this.lMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lMessage.Name = "lMessage";
            this.lMessage.Size = new System.Drawing.Size(69, 17);
            this.lMessage.TabIndex = 3;
            this.lMessage.Text = "Message:";
            // 
            // lLevel
            // 
            this.lLevel.AutoSize = true;
            this.lLevel.Location = new System.Drawing.Point(16, 44);
            this.lLevel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lLevel.Name = "lLevel";
            this.lLevel.Size = new System.Drawing.Size(46, 17);
            this.lLevel.TabIndex = 3;
            this.lLevel.Text = "Level:";
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(316, 40);
            this.txtMessage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(132, 22);
            this.txtMessage.TabIndex = 3;
            // 
            // gbPaging
            // 
            this.gbPaging.Controls.Add(this.lTotalCountValue);
            this.gbPaging.Controls.Add(this.lTotalCount);
            this.gbPaging.Controls.Add(this.btnPrevious);
            this.gbPaging.Controls.Add(this.btnNext);
            this.gbPaging.Controls.Add(this.label2);
            this.gbPaging.Controls.Add(this.txtSortDesc);
            this.gbPaging.Controls.Add(this.lPageSize);
            this.gbPaging.Controls.Add(this.txtPageSize);
            this.gbPaging.Controls.Add(this.label1);
            this.gbPaging.Controls.Add(this.lPageIndex);
            this.gbPaging.Controls.Add(this.txtSortAsc);
            this.gbPaging.Controls.Add(this.txtPageIndex);
            this.gbPaging.Location = new System.Drawing.Point(693, 13);
            this.gbPaging.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbPaging.Name = "gbPaging";
            this.gbPaging.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbPaging.Size = new System.Drawing.Size(657, 85);
            this.gbPaging.TabIndex = 6;
            this.gbPaging.TabStop = false;
            this.gbPaging.Text = "Paging";
            // 
            // lTotalCountValue
            // 
            this.lTotalCountValue.AutoSize = true;
            this.lTotalCountValue.Location = new System.Drawing.Point(109, 55);
            this.lTotalCountValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lTotalCountValue.Name = "lTotalCountValue";
            this.lTotalCountValue.Size = new System.Drawing.Size(0, 17);
            this.lTotalCountValue.TabIndex = 15;
            // 
            // lTotalCount
            // 
            this.lTotalCount.AutoSize = true;
            this.lTotalCount.Location = new System.Drawing.Point(13, 55);
            this.lTotalCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lTotalCount.Name = "lTotalCount";
            this.lTotalCount.Size = new System.Drawing.Size(85, 17);
            this.lTotalCount.TabIndex = 14;
            this.lTotalCount.Text = "Total Count:";
            // 
            // btnPrevious
            // 
            this.btnPrevious.Location = new System.Drawing.Point(415, 52);
            this.btnPrevious.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(113, 28);
            this.btnPrevious.TabIndex = 13;
            this.btnPrevious.Text = "Previous Page";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(536, 52);
            this.btnNext.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(113, 28);
            this.btnNext.TabIndex = 13;
            this.btnNext.Text = "Next Page";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(453, 27);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Sort Desc:";
            // 
            // txtSortDesc
            // 
            this.txtSortDesc.Location = new System.Drawing.Point(536, 23);
            this.txtSortDesc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSortDesc.Name = "txtSortDesc";
            this.txtSortDesc.Size = new System.Drawing.Size(112, 22);
            this.txtSortDesc.TabIndex = 5;
            // 
            // lPageSize
            // 
            this.lPageSize.AutoSize = true;
            this.lPageSize.Location = new System.Drawing.Point(156, 27);
            this.lPageSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lPageSize.Name = "lPageSize";
            this.lPageSize.Size = new System.Drawing.Size(76, 17);
            this.lPageSize.TabIndex = 3;
            this.lPageSize.Text = "Page Size:";
            // 
            // txtPageSize
            // 
            this.txtPageSize.Location = new System.Drawing.Point(241, 23);
            this.txtPageSize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPageSize.Name = "txtPageSize";
            this.txtPageSize.Size = new System.Drawing.Size(48, 22);
            this.txtPageSize.TabIndex = 3;
            this.txtPageSize.Text = "10";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(293, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Sort Asc:";
            // 
            // lPageIndex
            // 
            this.lPageIndex.AutoSize = true;
            this.lPageIndex.Location = new System.Drawing.Point(13, 27);
            this.lPageIndex.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lPageIndex.Name = "lPageIndex";
            this.lPageIndex.Size = new System.Drawing.Size(82, 17);
            this.lPageIndex.TabIndex = 3;
            this.lPageIndex.Text = "Page Index:";
            // 
            // txtSortAsc
            // 
            this.txtSortAsc.Location = new System.Drawing.Point(363, 23);
            this.txtSortAsc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSortAsc.Name = "txtSortAsc";
            this.txtSortAsc.Size = new System.Drawing.Size(81, 22);
            this.txtSortAsc.TabIndex = 3;
            this.txtSortAsc.Text = "CreateTime";
            // 
            // txtPageIndex
            // 
            this.txtPageIndex.Location = new System.Drawing.Point(107, 23);
            this.txtPageIndex.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPageIndex.Name = "txtPageIndex";
            this.txtPageIndex.Size = new System.Drawing.Size(40, 22);
            this.txtPageIndex.TabIndex = 3;
            this.txtPageIndex.Text = "0";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(1141, 106);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(200, 44);
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dgvData
            // 
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(17, 158);
            this.dgvData.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvData.Name = "dgvData";
            this.dgvData.Size = new System.Drawing.Size(1333, 430);
            this.dgvData.TabIndex = 9;
            // 
            // cbLevel
            // 
            this.cbLevel.FormattingEnabled = true;
            this.cbLevel.Items.AddRange(new object[] {
            "INFO",
            "WARN",
            "ERROR"});
            this.cbLevel.Location = new System.Drawing.Point(80, 41);
            this.cbLevel.Name = "cbLevel";
            this.cbLevel.Size = new System.Drawing.Size(132, 24);
            this.cbLevel.TabIndex = 4;
            this.cbLevel.Text = "INFO";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1371, 599);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.gbPaging);
            this.Controls.Add(this.gbCriteria);
            this.Controls.Add(this.gbConnection);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Main";
            this.Text = "EF Generic Repository Sample";
            this.gbConnection.ResumeLayout(false);
            this.gbCriteria.ResumeLayout(false);
            this.gbCriteria.PerformLayout();
            this.gbPaging.ResumeLayout(false);
            this.gbPaging.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gbConnection;
        private System.Windows.Forms.GroupBox gbCriteria;
        private System.Windows.Forms.Label lLevel;
        private System.Windows.Forms.Label lMessage;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.GroupBox gbPaging;
        private System.Windows.Forms.Label lPageIndex;
        private System.Windows.Forms.Label lPageSize;
        private System.Windows.Forms.TextBox txtPageIndex;
        private System.Windows.Forms.TextBox txtPageSize;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSortAsc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSortDesc;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lTotalCount;
        private System.Windows.Forms.Label lTotalCountValue;
        private System.Windows.Forms.Button btnGenerateInfoLog;
        private System.Windows.Forms.ComboBox cbLevel;
    }
}

