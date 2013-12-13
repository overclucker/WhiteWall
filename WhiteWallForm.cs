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
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

using BattleNET;
using MySql.Data.MySqlClient;

namespace WhiteWall
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		private static BattlEyeClient client;
		private static BattlEyeLoginCredentials creds;
		private static MySqlConnectionStringBuilder cs;
		private static string mode;
		private Thread beloopThread;
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			mode = ConfigurationManager.AppSettings["mode"];
			
			InitializeComponent();
			
			creds = new BattlEyeLoginCredentials();
			creds.Host = IPAddress.Parse(ConfigurationManager.AppSettings["behost"]);
			creds.Port = Convert.ToInt32(ConfigurationManager.AppSettings["beport"]);
			creds.Password = ConfigurationManager.AppSettings["bepass"];

			cs = new MySqlConnectionStringBuilder();
			cs.Server = ConfigurationManager.AppSettings["sqlhost"];
			cs.UserID = ConfigurationManager.AppSettings["sqluser"];
			cs.Password = ConfigurationManager.AppSettings["sqlpass"];
			cs.Database = ConfigurationManager.AppSettings["sqldatabase"];

			
			beloopThread = new Thread(beloop);
			beloopThread.IsBackground = true;
		}
		private static void beloop()
		{			
			client = new BattlEyeClient(creds);
			client.BattlEyeMessageReceived += BattlEyeMessageReceived;
			client.BattlEyeConnected += BattlEyeConnected;
			client.BattlEyeDisconnected += BattlEyeDisconnected;
			client.ReconnectOnPacketLoss = true;

			do
			{
				if(client.Connected == false)
					client.Connect();
				Thread.Sleep(1000 * 10);
			}
			while(true);
		}
		private static MySqlConnection connector()
		{
			MySqlConnectionStringBuilder cs = new MySqlConnectionStringBuilder();

			cs.Server = "localhost";
			cs.UserID = "root";
			cs.Password = "mysql";
			cs.Database = "whitewall";
			
			return new MySqlConnection(cs.ConnectionString);
		}
		private static void BattlEyeMessageReceived(BattlEyeMessageEventArgs args)
		{
			bool hasPlayer = false;
			if (args.Message != null)
			{
				if (args.Message.StartsWith("Player #") && args.Message.EndsWith("(unverified)"))
				{
					var player = MessageParse(args.Message);
					var mysql = connector();
					mysql.Open();
					string query = "SELECT COUNT(*) FROM Players WHERE guid = @GuID";
					Console.WriteLine(query);
					var command = new MySqlCommand(query, mysql);
					command.Parameters.AddWithValue("@GuID", player["guid"]);
					var result = command.ExecuteScalar();
					mysql.Close();
					if(result != null)
					{
						hasPlayer = Convert.ToBoolean(result);
					}

					if(hasPlayer == false)
					{
						Console.WriteLine(String.Format("{0} {1} added", player["name"], player["guid"]));
						if(AllowEntry)
						{
							mysql = connector();
							Console.WriteLine(query);
							query = "INSERT INTO `Players` (GuID, Name) VALUES(@GuID, @Name)";
							mysql.Open();
							command = new MySqlCommand(query, mysql);

							command.Parameters.AddWithValue("@GuID", player["guid"]);
							command.Parameters.AddWithValue("@Name", player["name"]);
							command.ExecuteNonQuery();
							mysql.Close();
						}
						else
						{
							client.SendCommand(BattlEyeCommand.Kick, player["id"] + " not in whitelist");
						}
					}
					else
					{
						Console.WriteLine(String.Format("{0} {1} exists", player["name"], player["guid"]));
					}
				}
			}
		}
		private static void BattlEyeConnected(BattlEyeConnectEventArgs args)
		{
			Console.WriteLine(args.Message);
		}
		private static void BattlEyeDisconnected(BattlEyeDisconnectEventArgs args)
		{
			Console.WriteLine(args.Message);
		}
		private static void Log(string message)
		{
			using(var logFile = File.AppendText("log.txt"))
			{
				logFile.WriteLine(message);
			}
		}
		public static Dictionary<string, string> MessageParse(string message)
		{
			Dictionary<string, string> player = new Dictionary<string, string>();
			
			string[] temp = message.TrimStart("Player #".ToCharArray()).TrimEnd(" (unverified)".ToCharArray()).Split(new string[] {" - GUID: "}, StringSplitOptions.None);
			
			player.Add("id", temp[0].Split()[0]);
			player.Add("name", temp[0].TrimStart(String.Format("{0} ", player["id"]).ToCharArray()));
			player.Add("guid", temp[1]);

			return player;
		}
		public static bool AllowEntry { get; private set; }
	}
}
