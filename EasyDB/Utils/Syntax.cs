using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EasyDB.Utils
{
	public static class Syntax
	{
		public static readonly Style GreenStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
		public static readonly Style BlueStyle = new TextStyle(Brushes.Blue, null, FontStyle.Bold);
		public static readonly Style GrayStyle = new TextStyle(Brushes.DarkGray, null, FontStyle.Bold);
		public static readonly Style PurpleStyle = new TextStyle(Brushes.DeepPink, null, FontStyle.Regular);
		public static readonly Style RedStyle = new TextStyle(Brushes.Red, null, FontStyle.Regular);
		public static readonly Style LimeStyle = new TextStyle(Brushes.LimeGreen, null, FontStyle.Regular);

		public static readonly Style Highlight = new TextStyle(null, Brushes.Orange, FontStyle.Regular);

		public static void SetSyntaxSql(this Range range)
		{
			range.ClearStyle(BlueStyle);
			range.ClearStyle(GreenStyle);
			range.ClearStyle(GrayStyle);
			range.ClearStyle(PurpleStyle);

			range.SetStyle(GreenStyle, @"--.*$", RegexOptions.Multiline);
			range.SetStyle(GreenStyle, @"/\*(.*?)\*/", RegexOptions.Multiline);
			range.SetStyle(RedStyle, @"'(.*?)'", RegexOptions.IgnoreCase);
			range.SetStyle(BlueStyle, @"\b(select|create|as|alter|begin|declare|table|where|from|end|else|if|set|procedure|in|insert|into|order|by|desc|asc|case|when|delete|then|cast|union|view|on|group|int|varchar|truncate|return|values|decimal|exec|char)\b", RegexOptions.IgnoreCase);
			range.SetStyle(GrayStyle, @"\b(and|or|join|left|right|inner|outer|exists|not)\b", RegexOptions.IgnoreCase);
			range.SetStyle(PurpleStyle, @"\b(isnull|getdate|update|count|sum)\b", RegexOptions.IgnoreCase);
			range.SetStyle(LimeStyle, @"\b(INFORMATION_SCHEMA|TABLES)\b", RegexOptions.IgnoreCase);
		}

		public static void HighlightText(this Range range, string text)
		{
			range.ClearStyle(Highlight);
			range.SetStyle(Highlight, text, RegexOptions.IgnoreCase | RegexOptions.Singleline);
		}
	}
}
