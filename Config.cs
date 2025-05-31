using System;
using System.IO;
public class Config
{
	// input your domain in the domain.txt file
	// domain_format: http://your-domain.com:port 
	public static string domain = File.ReadAllText("domain.txt");
  
}
