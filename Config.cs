using System;
using System.IO;
public class Config
{
	// input your domain in the domain.txt file
	// format: http://your-domain.com:port
	public static string domain = File.ReadAllText("domain.txt");
  
}
	//public static string domain = "http://127.0.0.1:8001";

