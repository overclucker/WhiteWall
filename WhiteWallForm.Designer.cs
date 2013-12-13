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
			this.toggleBtn = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// toggleBtn
			// 
			this.toggleBtn.Font = new System.Drawing.Font("Lucida Console", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.toggleBtn.Location = new System.Drawing.Point(32, 32);
			this.toggleBtn.Margin = new System.Windows.Forms.Padding(32);
			this.toggleBtn.Name = "toggleBtn";
			this.toggleBtn.Size = new System.Drawing.Size(128, 32);
			this.toggleBtn.TabIndex = 0;
			this.toggleBtn.Text = mode.ToUpper() == "OFF" ? "OFF" : "ON";
			this.toggleBtn.BackColor = mode.ToUpper() == "OFF" ? System.Drawing.Color.Red : System.Drawing.Color.GreenYellow;
			this.toggleBtn.UseVisualStyleBackColor = false;
			this.toggleBtn.Click += new System.EventHandler(this.ToggleClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(220, 109);
			this.Controls.Add(this.toggleBtn);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "WhiteWall";
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.ResumeLayout(false);
		}
		private bool toggle;
		
		void GroupBox1Enter(object sender, System.EventArgs e)
		{
			
		}
		
		void ToggleClick(object sender, System.EventArgs e)
		{
			toggle = toggle == false ? true : false;
			if(toggle)
			{
				toggleBtn.Text = "ON";
				toggleBtn.BackColor = System.Drawing.Color.GreenYellow;
				AllowEntry = false;
			}
			else
			{
				toggleBtn.Text = "OFF";
				toggleBtn.BackColor = System.Drawing.Color.Red;
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
