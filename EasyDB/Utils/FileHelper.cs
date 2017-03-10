using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDB.Utils
{
	public class FileHelper
	{
		public static bool BackupScript(string scriptName, string sqlScript, out Tuple<string, string> output)
		{
			string dirName = GetDirForToday();
			output = new Tuple<string, string>($"{dirName}{scriptName}_{DateTime.Now.ToString("MMddyyyy_HHmmss")}.sql", dirName);

			try
			{
				File.WriteAllText(output.Item1, sqlScript, Encoding.UTF8);

				return true;
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
				output = new Tuple<string, string>(ex.Message, ex.StackTrace);
				return false;
			}
		}

		private static string GetDirForToday()
		{
			string dirName = $"{AppDomain.CurrentDomain.BaseDirectory}SqlBak_{DateTime.Now.ToString("MMddyyyy")}\\";
			if (!Directory.Exists(dirName))
			{
				Directory.CreateDirectory(dirName);
			}

			return dirName;
		}
	}
}
