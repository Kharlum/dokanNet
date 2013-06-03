namespace course.gui
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.mounter_button = new System.Windows.Forms.Button();
            this.rtb = new System.Windows.Forms.RichTextBox();
            this.selecter = new System.Windows.Forms.ComboBox();
            this.folder_selector = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.button_Folder_selecter = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mounter_button
            // 
            this.mounter_button.Location = new System.Drawing.Point(352, 92);
            this.mounter_button.Name = "mounter_button";
            this.mounter_button.Size = new System.Drawing.Size(121, 23);
            this.mounter_button.TabIndex = 0;
            this.mounter_button.Text = "mount";
            this.mounter_button.UseVisualStyleBackColor = true;
            this.mounter_button.Click += new System.EventHandler(this.mounter_button_Click);
            // 
            // rtb
            // 
            this.rtb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtb.Location = new System.Drawing.Point(12, 121);
            this.rtb.Name = "rtb";
            this.rtb.ReadOnly = true;
            this.rtb.Size = new System.Drawing.Size(471, 207);
            this.rtb.TabIndex = 1;
            this.rtb.Text = "";
            // 
            // selecter
            // 
            this.selecter.FormattingEnabled = true;
            this.selecter.Location = new System.Drawing.Point(12, 12);
            this.selecter.Name = "selecter";
            this.selecter.Size = new System.Drawing.Size(121, 21);
            this.selecter.TabIndex = 2;
            // 
            // folder_selector
            // 
            this.folder_selector.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.folder_selector.SelectedPath = "C:\\files";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(154, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(236, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Выберите букву для монтирования драйвера";
            // 
            // button_Folder_selecter
            // 
            this.button_Folder_selecter.Location = new System.Drawing.Point(12, 39);
            this.button_Folder_selecter.Name = "button_Folder_selecter";
            this.button_Folder_selecter.Size = new System.Drawing.Size(121, 23);
            this.button_Folder_selecter.TabIndex = 4;
            this.button_Folder_selecter.Text = "Выбрать";
            this.button_Folder_selecter.UseVisualStyleBackColor = true;
            this.button_Folder_selecter.Click += new System.EventHandler(this.button_Folder_selecter_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(154, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Выберите папку для хранения данных";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(12, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "По умолчанию ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 340);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_Folder_selecter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.selecter);
            this.Controls.Add(this.rtb);
            this.Controls.Add(this.mounter_button);
            this.Name = "Form1";
            this.Text = "Курсовая . монтирование ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button mounter_button;
        private System.Windows.Forms.RichTextBox rtb;
        private System.Windows.Forms.ComboBox selecter;
        private System.Windows.Forms.FolderBrowserDialog folder_selector;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_Folder_selecter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

