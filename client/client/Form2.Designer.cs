
namespace client
{
    partial class Form2
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.messagegeneric = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.leaveButton = new System.Windows.Forms.Button();
            this.rejoinButton = new System.Windows.Forms.Button();
            this.rock = new System.Windows.Forms.Button();
            this.paper = new System.Windows.Forms.Button();
            this.scissors = new System.Windows.Forms.Button();
            this.user1_move = new System.Windows.Forms.Label();
            this.user3_move = new System.Windows.Forms.Label();
            this.user2_move = new System.Windows.Forms.Label();
            this.user4_move = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Red;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(136, 114);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(148, 110);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 0;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(136, 281);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(148, 110);
            this.panel2.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Aqua;
            this.panel3.Controls.Add(this.label4);
            this.panel3.Location = new System.Drawing.Point(504, 281);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(148, 110);
            this.panel3.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(53, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 13);
            this.label4.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.panel4.Controls.Add(this.label3);
            this.panel4.Location = new System.Drawing.Point(504, 114);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(148, 110);
            this.panel4.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(53, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 1;
            // 
            // messagegeneric
            // 
            this.messagegeneric.AutoSize = true;
            this.messagegeneric.Location = new System.Drawing.Point(335, 44);
            this.messagegeneric.Name = "messagegeneric";
            this.messagegeneric.Size = new System.Drawing.Size(121, 13);
            this.messagegeneric.TabIndex = 4;
            this.messagegeneric.Text = "Waiting for the players...";
            this.messagegeneric.Click += new System.EventHandler(this.label2_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label12);
            this.panel5.Controls.Add(this.label11);
            this.panel5.Controls.Add(this.label10);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Location = new System.Drawing.Point(770, 75);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(137, 316);
            this.panel5.TabIndex = 5;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(47, 267);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(0, 13);
            this.label12.TabIndex = 6;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(47, 226);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(0, 13);
            this.label11.TabIndex = 5;
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(47, 185);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(0, 13);
            this.label10.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(53, 146);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 13);
            this.label9.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(53, 106);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(0, 13);
            this.label8.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(53, 64);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 13);
            this.label7.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(53, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 13);
            this.label6.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(817, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Waiting Queue";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // leaveButton
            // 
            this.leaveButton.Location = new System.Drawing.Point(353, 379);
            this.leaveButton.Name = "leaveButton";
            this.leaveButton.Size = new System.Drawing.Size(91, 39);
            this.leaveButton.TabIndex = 1;
            this.leaveButton.Text = "Leave";
            this.leaveButton.UseVisualStyleBackColor = true;
            this.leaveButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // rejoinButton
            // 
            this.rejoinButton.Enabled = false;
            this.rejoinButton.Location = new System.Drawing.Point(353, 379);
            this.rejoinButton.Name = "rejoinButton";
            this.rejoinButton.Size = new System.Drawing.Size(91, 39);
            this.rejoinButton.TabIndex = 7;
            this.rejoinButton.Text = "Rejoin";
            this.rejoinButton.UseVisualStyleBackColor = true;
            this.rejoinButton.Visible = false;
            this.rejoinButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // rock
            // 
            this.rock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.rock.Enabled = false;
            this.rock.Location = new System.Drawing.Point(371, 114);
            this.rock.Name = "rock";
            this.rock.Size = new System.Drawing.Size(52, 48);
            this.rock.TabIndex = 1;
            this.rock.Text = "rock";
            this.rock.UseVisualStyleBackColor = false;
            this.rock.Visible = false;
            this.rock.Click += new System.EventHandler(this.rock_Click);
            // 
            // paper
            // 
            this.paper.BackColor = System.Drawing.Color.White;
            this.paper.Enabled = false;
            this.paper.Location = new System.Drawing.Point(371, 203);
            this.paper.Name = "paper";
            this.paper.Size = new System.Drawing.Size(52, 48);
            this.paper.TabIndex = 8;
            this.paper.Text = "paper";
            this.paper.UseVisualStyleBackColor = false;
            this.paper.Visible = false;
            this.paper.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // scissors
            // 
            this.scissors.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.scissors.Enabled = false;
            this.scissors.Location = new System.Drawing.Point(371, 295);
            this.scissors.Name = "scissors";
            this.scissors.Size = new System.Drawing.Size(52, 48);
            this.scissors.TabIndex = 9;
            this.scissors.Text = "scissors";
            this.scissors.UseVisualStyleBackColor = false;
            this.scissors.Visible = false;
            this.scissors.Click += new System.EventHandler(this.scissors_Click);
            // 
            // user1_move
            // 
            this.user1_move.AutoSize = true;
            this.user1_move.Location = new System.Drawing.Point(168, 227);
            this.user1_move.Name = "user1_move";
            this.user1_move.Size = new System.Drawing.Size(0, 13);
            this.user1_move.TabIndex = 10;
            // 
            // user3_move
            // 
            this.user3_move.AutoSize = true;
            this.user3_move.Location = new System.Drawing.Point(544, 227);
            this.user3_move.Name = "user3_move";
            this.user3_move.Size = new System.Drawing.Size(0, 13);
            this.user3_move.TabIndex = 11;
            // 
            // user2_move
            // 
            this.user2_move.AutoSize = true;
            this.user2_move.Location = new System.Drawing.Point(168, 392);
            this.user2_move.Name = "user2_move";
            this.user2_move.Size = new System.Drawing.Size(0, 13);
            this.user2_move.TabIndex = 12;
            this.user2_move.Click += new System.EventHandler(this.user2_move_Click);
            // 
            // user4_move
            // 
            this.user4_move.AutoSize = true;
            this.user4_move.Location = new System.Drawing.Point(544, 392);
            this.user4_move.Name = "user4_move";
            this.user4_move.Size = new System.Drawing.Size(0, 13);
            this.user4_move.TabIndex = 13;
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // timer3
            // 
            this.timer3.Interval = 1000;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(987, 450);
            this.Controls.Add(this.user4_move);
            this.Controls.Add(this.user2_move);
            this.Controls.Add(this.user3_move);
            this.Controls.Add(this.user1_move);
            this.Controls.Add(this.scissors);
            this.Controls.Add(this.paper);
            this.Controls.Add(this.rock);
            this.Controls.Add(this.rejoinButton);
            this.Controls.Add(this.leaveButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.messagegeneric);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label messagegeneric;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button leaveButton;
        private System.Windows.Forms.Button rejoinButton;
        private System.Windows.Forms.Button rock;
        private System.Windows.Forms.Button paper;
        private System.Windows.Forms.Button scissors;
        private System.Windows.Forms.Label user1_move;
        private System.Windows.Forms.Label user3_move;
        private System.Windows.Forms.Label user2_move;
        private System.Windows.Forms.Label user4_move;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Timer timer3;
    }
}