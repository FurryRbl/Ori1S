using System;
using System.Diagnostics;
using System.Globalization;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Mono.Xml.Xsl
{
	// Token: 0x0200007C RID: 124
	internal class Debug
	{
		// Token: 0x06000426 RID: 1062 RVA: 0x0001B560 File Offset: 0x00019760
		[Conditional("_DEBUG")]
		internal static void TraceContext(XPathNavigator context)
		{
			if (context != null)
			{
				context = context.Clone();
				XPathNodeType nodeType = context.NodeType;
				if (nodeType == XPathNodeType.Element)
				{
					string str = string.Format("<{0}:{1}", context.Prefix, context.LocalName);
					bool flag = context.MoveToFirstAttribute();
					while (flag)
					{
						str += string.Format(CultureInfo.InvariantCulture, " {0}:{1}={2}", new object[]
						{
							context.Prefix,
							context.LocalName,
							context.Value
						});
						flag = context.MoveToNextAttribute();
					}
					str += ">";
				}
			}
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0001B614 File Offset: 0x00019814
		[Conditional("DEBUG")]
		internal static void Assert(bool condition, string message)
		{
			if (!condition)
			{
				throw new XsltException(message, null);
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0001B624 File Offset: 0x00019824
		[Conditional("_DEBUG")]
		internal static void WriteLine(object value)
		{
			Console.Error.WriteLine(value);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0001B634 File Offset: 0x00019834
		[Conditional("_DEBUG")]
		internal static void WriteLine(string message)
		{
			Console.Error.WriteLine(message);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0001B644 File Offset: 0x00019844
		[Conditional("DEBUG")]
		internal static void EnterNavigator(Compiler c)
		{
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0001B648 File Offset: 0x00019848
		[Conditional("DEBUG")]
		internal static void ExitNavigator(Compiler c)
		{
		}
	}
}
