using System;
using System.Collections;

namespace System.Xml.XPath
{
	// Token: 0x0200014A RID: 330
	internal class XPathFunctionId : XPathFunction
	{
		// Token: 0x06000F45 RID: 3909 RVA: 0x0004990C File Offset: 0x00047B0C
		public XPathFunctionId(FunctionArguments args) : base(args)
		{
			if (args == null || args.Tail != null)
			{
				throw new XPathException("id takes 1 arg");
			}
			this.arg0 = args.Arg;
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000F47 RID: 3911 RVA: 0x00049958 File Offset: 0x00047B58
		public Expression Id
		{
			get
			{
				return this.arg0;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000F48 RID: 3912 RVA: 0x00049960 File Offset: 0x00047B60
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000F49 RID: 3913 RVA: 0x00049964 File Offset: 0x00047B64
		internal override bool Peer
		{
			get
			{
				return this.arg0.Peer;
			}
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x00049974 File Offset: 0x00047B74
		public override object Evaluate(BaseIterator iter)
		{
			object obj = this.arg0.Evaluate(iter);
			XPathNodeIterator xpathNodeIterator = obj as XPathNodeIterator;
			string text;
			if (xpathNodeIterator != null)
			{
				text = string.Empty;
				while (xpathNodeIterator.MoveNext())
				{
					XPathNavigator xpathNavigator = xpathNodeIterator.Current;
					text = text + xpathNavigator.Value + " ";
				}
			}
			else
			{
				text = XPathFunctions.ToString(obj);
			}
			XPathNavigator xpathNavigator2 = iter.Current.Clone();
			ArrayList arrayList = new ArrayList();
			string[] array = text.Split(XPathFunctionId.rgchWhitespace);
			for (int i = 0; i < array.Length; i++)
			{
				if (xpathNavigator2.MoveToId(array[i]))
				{
					arrayList.Add(xpathNavigator2.Clone());
				}
			}
			arrayList.Sort(XPathNavigatorComparer.Instance);
			return new ListIterator(iter, arrayList);
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x00049A40 File Offset: 0x00047C40
		public override string ToString()
		{
			return "id(" + this.arg0.ToString() + ")";
		}

		// Token: 0x040006B0 RID: 1712
		private Expression arg0;

		// Token: 0x040006B1 RID: 1713
		private static char[] rgchWhitespace = new char[]
		{
			' ',
			'\t',
			'\r',
			'\n'
		};
	}
}
