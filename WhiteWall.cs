﻿/* WhiteWall is intended to be an hassle free solution for blocking repeated attacks on servers using BattlEye.
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
using System;
using System.Windows.Forms;

namespace WhiteWall
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
		
	}
}
