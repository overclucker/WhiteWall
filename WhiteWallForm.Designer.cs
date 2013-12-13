/* WhiteWall is intended to be an hassle free solution for blocking repeated server attacks.
 * Copyright 2013 Robert Rose
 *
 * This file is part of WhiteWall.
 *
 * WhiteWall is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * WhiteWall is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with WhiteWall.  If not, see <http://www.gnu.org/licenses/>.
 *
 * email: <overclucker@gmail.com>
 *
 */
namespace WhiteWall
{
	partial class MainForm
	{
		private System.Windows.Forms.Button toggleBtn;
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if(client.Connected)
			{
				client.Disconnect();
			}

			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.statusTbx = new System.Windows.Forms.TextBox();
			this.toggleBtn = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.AutoSize = true;
			this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.groupBox1.Controls.Add(this.statusTbx);
			this.groupBox1.Controls.Add(this.toggleBtn);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(193, 62);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Whitelist Toggle";
			this.groupBox1.Enter += new System.EventHandler(this.GroupBox1Enter);
			// 
			// statusTbx
			// 
			this.statusTbx.BackColor = System.Drawing.Color.Red;
			this.statusTbx.Location = new System.Drawing.Point(106, 22);
			this.statusTbx.Name = "statusTbx";
			this.statusTbx.Size = new System.Drawing.Size(81, 20);
			this.statusTbx.TabIndex = 1;
			this.statusTbx.Text = "OFF";
			this.statusTbx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// toggleBtn
			// 
			this.toggleBtn.Location = new System.Drawing.Point(7, 20);
			this.toggleBtn.Name = "toggleBtn";
			this.toggleBtn.Size = new System.Drawing.Size(92, 23);
			this.toggleBtn.TabIndex = 0;
			this.toggleBtn.Text = "ON/OFF";
			this.toggleBtn.UseVisualStyleBackColor = true;
			this.toggleBtn.Click += new System.EventHandler(this.ToggleClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(221, 89);
			this.Controls.Add(this.groupBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "WhiteWall";
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private bool toggle;
		private System.Windows.Forms.TextBox statusTbx;
		private System.Windows.Forms.GroupBox groupBox1;
		
		void GroupBox1Enter(object sender, System.EventArgs e)
		{
			
		}
		
		void ToggleClick(object sender, System.EventArgs e)
		{
			toggle = toggle == false ? true : false;
			if(toggle)
			{
				statusTbx.Text = "ON";
				statusTbx.BackColor = System.Drawing.Color.GreenYellow;
				AllowEntry = false;
			}
			else
			{
				statusTbx.Text = "OFF";
				statusTbx.BackColor = System.Drawing.Color.Red;
				AllowEntry = true;
			}
		}
		
		void MainFormLoad(object sender, System.EventArgs e)
		{
			AllowEntry = true;
			
			beloopThread.Start();
		}
	}
}
