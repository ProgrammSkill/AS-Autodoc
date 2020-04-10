namespace AS_Autodoc
{
    partial class Menu
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
            this.enter = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.справочникToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.маркиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.моделиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.производителиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.странаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.городToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.улицаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.автозапчастиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // enter
            // 
            this.enter.Location = new System.Drawing.Point(32, 64);
            this.enter.Name = "enter";
            this.enter.Size = new System.Drawing.Size(162, 29);
            this.enter.TabIndex = 1;
            this.enter.Text = "Поставщики";
            this.enter.UseVisualStyleBackColor = true;
            this.enter.Click += new System.EventHandler(this.Enter_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справочникToolStripMenuItem,
            this.автозапчастиToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // справочникToolStripMenuItem
            // 
            this.справочникToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.маркиToolStripMenuItem,
            this.моделиToolStripMenuItem,
            this.производителиToolStripMenuItem,
            this.странаToolStripMenuItem,
            this.городToolStripMenuItem,
            this.улицаToolStripMenuItem});
            this.справочникToolStripMenuItem.Name = "справочникToolStripMenuItem";
            this.справочникToolStripMenuItem.Size = new System.Drawing.Size(87, 20);
            this.справочникToolStripMenuItem.Text = "Справочник";
            // 
            // маркиToolStripMenuItem
            // 
            this.маркиToolStripMenuItem.Name = "маркиToolStripMenuItem";
            this.маркиToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.маркиToolStripMenuItem.Text = "Марки";
            this.маркиToolStripMenuItem.Click += new System.EventHandler(this.МаркиToolStripMenuItem_Click);
            // 
            // моделиToolStripMenuItem
            // 
            this.моделиToolStripMenuItem.Name = "моделиToolStripMenuItem";
            this.моделиToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.моделиToolStripMenuItem.Text = "Модели";
            this.моделиToolStripMenuItem.Click += new System.EventHandler(this.МоделиToolStripMenuItem_Click);
            // 
            // производителиToolStripMenuItem
            // 
            this.производителиToolStripMenuItem.Name = "производителиToolStripMenuItem";
            this.производителиToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.производителиToolStripMenuItem.Text = "Производители";
            this.производителиToolStripMenuItem.Click += new System.EventHandler(this.ПроизводителиToolStripMenuItem_Click);
            // 
            // странаToolStripMenuItem
            // 
            this.странаToolStripMenuItem.Name = "странаToolStripMenuItem";
            this.странаToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.странаToolStripMenuItem.Text = "Страна";
            this.странаToolStripMenuItem.Click += new System.EventHandler(this.СтранаToolStripMenuItem_Click);
            // 
            // городToolStripMenuItem
            // 
            this.городToolStripMenuItem.Name = "городToolStripMenuItem";
            this.городToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.городToolStripMenuItem.Text = "Город";
            this.городToolStripMenuItem.Click += new System.EventHandler(this.ГородToolStripMenuItem_Click);
            // 
            // улицаToolStripMenuItem
            // 
            this.улицаToolStripMenuItem.Name = "улицаToolStripMenuItem";
            this.улицаToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.улицаToolStripMenuItem.Text = "Улица";
            this.улицаToolStripMenuItem.Click += new System.EventHandler(this.УлицаToolStripMenuItem_Click);
            // 
            // автозапчастиToolStripMenuItem
            // 
            this.автозапчастиToolStripMenuItem.Name = "автозапчастиToolStripMenuItem";
            this.автозапчастиToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.автозапчастиToolStripMenuItem.Text = "Автозапчасти";
            this.автозапчастиToolStripMenuItem.Click += new System.EventHandler(this.АвтозапчастиToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(32, 113);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(162, 29);
            this.button1.TabIndex = 3;
            this.button1.Text = "Марки и модели";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(32, 162);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(162, 29);
            this.button2.TabIndex = 4;
            this.button2.Text = "Автозапчасти";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(32, 212);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(162, 29);
            this.button3.TabIndex = 5;
            this.button3.Text = "Поставка";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.enter);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Menu";
            this.Text = "Меню";
            this.Load += new System.EventHandler(this.Menu_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button enter;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem справочникToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem странаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem городToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem улицаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem маркиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem моделиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem производителиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem автозапчастиToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}