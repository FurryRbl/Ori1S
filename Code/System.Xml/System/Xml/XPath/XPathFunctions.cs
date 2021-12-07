using System;
using System.Globalization;

namespace System.Xml.XPath
{
	// Token: 0x02000145 RID: 325
	internal class XPathFunctions
	{
		// Token: 0x06000F29 RID: 3881 RVA: 0x00049480 File Offset: 0x00047680
		public static bool ToBoolean(object arg)
		{
			if (arg == null)
			{
				throw new ArgumentNullException();
			}
			if (arg is bool)
			{
				return (bool)arg;
			}
			if (arg is double)
			{
				double num = (double)arg;
				return num != 0.0 && !double.IsNaN(num);
			}
			if (arg is string)
			{
				return ((string)arg).Length != 0;
			}
			if (arg is XPathNodeIterator)
			{
				XPathNodeIterator xpathNodeIterator = (XPathNodeIterator)arg;
				return xpathNodeIterator.MoveNext();
			}
			if (arg is XPathNavigator)
			{
				return XPathFunctions.ToBoolean(((XPathNavigator)arg).SelectChildren(XPathNodeType.All));
			}
			throw new ArgumentException();
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x00049534 File Offset: 0x00047734
		public static bool ToBoolean(bool b)
		{
			return b;
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x00049538 File Offset: 0x00047738
		public static bool ToBoolean(double d)
		{
			return d != 0.0 && !double.IsNaN(d);
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x00049558 File Offset: 0x00047758
		public static bool ToBoolean(string s)
		{
			return s != null && s.Length > 0;
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x0004956C File Offset: 0x0004776C
		public static bool ToBoolean(BaseIterator iter)
		{
			return iter != null && iter.MoveNext();
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x00049580 File Offset: 0x00047780
		public static string ToString(object arg)
		{
			if (arg == null)
			{
				throw new ArgumentNullException();
			}
			if (arg is string)
			{
				return (string)arg;
			}
			if (arg is bool)
			{
				return (!(bool)arg) ? "false" : "true";
			}
			if (arg is double)
			{
				return XPathFunctions.ToString((double)arg);
			}
			if (arg is XPathNodeIterator)
			{
				XPathNodeIterator xpathNodeIterator = (XPathNodeIterator)arg;
				if (!xpathNodeIterator.MoveNext())
				{
					return string.Empty;
				}
				return xpathNodeIterator.Current.Value;
			}
			else
			{
				if (arg is XPathNavigator)
				{
					return ((XPathNavigator)arg).Value;
				}
				throw new ArgumentException();
			}
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x00049634 File Offset: 0x00047834
		public static string ToString(double d)
		{
			if (d == double.NegativeInfinity)
			{
				return "-Infinity";
			}
			if (d == double.PositiveInfinity)
			{
				return "Infinity";
			}
			return d.ToString("R", NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x00049674 File Offset: 0x00047874
		public static double ToNumber(object arg)
		{
			if (arg == null)
			{
				throw new ArgumentNullException();
			}
			if (arg is BaseIterator || arg is XPathNavigator)
			{
				arg = XPathFunctions.ToString(arg);
			}
			if (arg is string)
			{
				string arg2 = arg as string;
				return XPathFunctions.ToNumber(arg2);
			}
			if (arg is double)
			{
				return (double)arg;
			}
			if (arg is bool)
			{
				return Convert.ToDouble((bool)arg);
			}
			throw new ArgumentException();
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x000496F4 File Offset: 0x000478F4
		public static double ToNumber(string arg)
		{
			if (arg == null)
			{
				throw new ArgumentNullException();
			}
			string text = arg.Trim(XmlChar.WhitespaceChars);
			if (text.Length == 0)
			{
				return double.NaN;
			}
			double result;
			try
			{
				if (text[0] == '.')
				{
					text = '.' + text;
				}
				result = double.Parse(text, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo);
			}
			catch (OverflowException)
			{
				result = double.NaN;
			}
			catch (FormatException)
			{
				result = double.NaN;
			}
			return result;
		}
	}
}
