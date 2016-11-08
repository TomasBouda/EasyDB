using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EasyDB
{
	public static class Syntax
	{
		public static readonly Style GreenStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
		public static readonly Style BlueStyle = new TextStyle(Brushes.Blue, null, FontStyle.Bold);
		public static readonly Style GrayStyle = new TextStyle(Brushes.Gray, null, FontStyle.Bold);
		public static readonly Style PurpleStyle = new TextStyle(Brushes.Purple, null, FontStyle.Bold);
		public static readonly Style Highlight = new TextStyle(null, Brushes.Orange, FontStyle.Regular);

		public static void SetSyntaxSql(this Range range)
		{
			range.ClearStyle(BlueStyle);
			range.ClearStyle(GreenStyle);
			range.ClearStyle(GrayStyle);
			range.ClearStyle(PurpleStyle);

			range.SetStyle(GreenStyle, @"--.*$", RegexOptions.Multiline);
			range.SetStyle(GreenStyle, @"/\*(.*?)\*/", RegexOptions.Multiline);
			range.SetStyle(BlueStyle, @"\b(select|update|create|as|join|alter|begin|declare|table|where|from|end|else|if|set|procedure|in|insert|into|order|by|desc|asc|left|right|inner|outer|case|when|delete|then|cast|exists|union|view)\b", RegexOptions.IgnoreCase);
			range.SetStyle(GrayStyle, @"\b(and|or)\b", RegexOptions.IgnoreCase);
			range.SetStyle(PurpleStyle, @"\b(isnull|getdate)\b", RegexOptions.IgnoreCase);
		}

		public static void HighlightText(this Range range, string text)
		{
			range.ClearStyle(Highlight);
			range.SetStyle(Highlight, text, RegexOptions.IgnoreCase | RegexOptions.Singleline);
		}
	}
}
