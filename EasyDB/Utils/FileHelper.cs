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
		public static bool BackupScript(string scriptName, string sqlScript, out string output)
		{
			string dirName = GetDirForToday();
			output = $"{dirName}{scriptName}_{DateTime.Now.ToString("MMddyyyy_HHmmss")}.sql";

			try
			{
				File.WriteAllText(output, sqlScript, Encoding.UTF8);

				return true;
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
				output = ex.Message;
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
