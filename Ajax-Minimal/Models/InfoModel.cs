﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Ajax_Minimal.Models
{
	public class InfoModel
	{
		private static InfoModel s_instace = null;

		public static InfoModel Instance
		{
			get
			{
				if (s_instace == null)
				{
					s_instace = new InfoModel();
				}
				return s_instace;
			}
		}
		// members
		public Location Location { get; private set; }
		public string ip { get; set; }
		public string port { get; set; }
		public int time { get; set; }
		public List<Location> locList = new List<Location>();


		public Location LocationReader()
		{
			TcpClient client = new TcpClient(ip, int.Parse(port));
			client.ReceiveTimeout = 5000;
			StreamReader reader = new StreamReader(client.GetStream(), Encoding.ASCII);
			StreamWriter writer = new StreamWriter(client.GetStream(), Encoding.ASCII);
			// writing proccess
			string lonStr = "get /position/longitude-deg\r\n";
			string latStr = "get /position/latitude-deg\r\n";
			//reading process
			writer.Write(lonStr);
			writer.Flush();
			//Debug.Print("Sent");
			string lon = reader.ReadLine().Split(new string[] { "'" }, StringSplitOptions.None)[1];
			//Debug.Print(lon);
			writer.Write(latStr);
			writer.Flush();
			//Debug.Print("Sent");
			string lat = reader.ReadLine().Split(new string[] { "'" }, StringSplitOptions.None)[1];
			//Debug.Print(lat);
			//close
			Location result = new Location(double.Parse(lon), double.Parse(lat));
			client.Close();
			locList.Add(result);
			return result;
		}
		//public const string SCENARIO_FILE = "~/App_Data/{0}.txt";           // The Path of the Secnario

		//public void ReadData(string name)
		//{
		//	string path = HttpContext.Current.Server.MapPath(String.Format(SCENARIO_FILE, name));
		//	if (!File.Exists(path))
		//	{
		//		Employee.FirstName = name;
		//		Employee.LastName = name;
		//		Employee.Salary = 500;

		//		using (StreamWriter file = new StreamWriter(path, true))
		//		{
		//			file.WriteLine(Employee.FirstName);
		//			file.WriteLine(Employee.LastName);
		//			file.WriteLine(Employee.Salary);
		//		}
		//	}
		//	else
		//	{
		//		string[] lines = File.ReadAllLines(path);        // reading all the lines of the file
		//		Employee.FirstName = lines[0];
		//		Employee.LastName = lines[1];
		//		Employee.Salary = int.Parse(lines[2]);
		//	}
		//}

	}
}