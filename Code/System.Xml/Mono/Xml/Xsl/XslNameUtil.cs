using System;
using System.Collections;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Mono.Xml.Xsl
{
	// Token: 0x0200007B RID: 123
	internal class XslNameUtil
	{
		// Token: 0x0600041E RID: 1054 RVA: 0x0001B2A8 File Offset: 0x000194A8
		public static XmlQualifiedName[] FromListString(string names, XPathNavigator current)
		{
			string[] array = names.Split(XmlChar.WhitespaceChars);
			int num = 0;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != string.Empty)
				{
					num++;
				}
			}
			XmlQualifiedName[] array2 = new XmlQualifiedName[num];
			num = 0;
			for (int j = 0; j < array.Length; j++)
			{
				if (array[j] != string.Empty)
				{
					array2[num++] = XslNameUtil.FromString(array[j], current, true);
				}
			}
			return array2;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0001B334 File Offset: 0x00019534
		public static XmlQualifiedName FromString(string name, XPathNavigator current)
		{
			return XslNameUtil.FromString(name, current, false);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0001B340 File Offset: 0x00019540
		public static XmlQualifiedName FromString(string name, XPathNavigator current, bool useDefaultXmlns)
		{
			if (current.NodeType == XPathNodeType.Attribute)
			{
				(current = current.Clone()).MoveToParent();
			}
			int num = name.IndexOf(':');
			if (num > 0)
			{
				return new XmlQualifiedName(name.Substring(num + 1), current.GetNamespace(name.Substring(0, num)));
			}
			if (num < 0)
			{
				return new XmlQualifiedName(name, (!useDefaultXmlns) ? string.Empty : current.GetNamespace(string.Empty));
			}
			throw new ArgumentException("Invalid name: " + name);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0001B3D0 File Offset: 0x000195D0
		public static XmlQualifiedName FromString(string name, Hashtable nsDecls)
		{
			int num = name.IndexOf(':');
			if (num > 0)
			{
				return new XmlQualifiedName(name.Substring(num + 1), nsDecls[name.Substring(0, num)] as string);
			}
			if (num < 0)
			{
				return new XmlQualifiedName(name, (!nsDecls.ContainsKey(string.Empty)) ? string.Empty : ((string)nsDecls[string.Empty]));
			}
			throw new ArgumentException("Invalid name: " + name);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0001B458 File Offset: 0x00019658
		public static XmlQualifiedName FromString(string name, IStaticXsltContext ctx)
		{
			int num = name.IndexOf(':');
			if (num > 0)
			{
				return new XmlQualifiedName(name.Substring(num + 1), ctx.LookupNamespace(name.Substring(0, num)));
			}
			if (num < 0)
			{
				return new XmlQualifiedName(name, string.Empty);
			}
			throw new ArgumentException("Invalid name: " + name);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0001B4B8 File Offset: 0x000196B8
		public static XmlQualifiedName FromString(string name, XmlNamespaceManager ctx)
		{
			int num = name.IndexOf(':');
			if (num > 0)
			{
				return new XmlQualifiedName(name.Substring(num + 1), ctx.LookupNamespace(name.Substring(0, num), false));
			}
			if (num < 0)
			{
				return new XmlQualifiedName(name, string.Empty);
			}
			throw new ArgumentException("Invalid name: " + name);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0001B518 File Offset: 0x00019718
		public static string LocalNameOf(string name)
		{
			int num = name.IndexOf(':');
			if (num > 0)
			{
				return name.Substring(num + 1);
			}
			if (num < 0)
			{
				return name;
			}
			throw new ArgumentException("Invalid name: " + name);
		}
	}
}
