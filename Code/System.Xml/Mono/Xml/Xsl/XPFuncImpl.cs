using System;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Mono.Xml.Xsl
{
	// Token: 0x020000A3 RID: 163
	internal abstract class XPFuncImpl : IXsltContextFunction
	{
		// Token: 0x060005A0 RID: 1440 RVA: 0x00022E20 File Offset: 0x00021020
		public XPFuncImpl()
		{
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00022E28 File Offset: 0x00021028
		public XPFuncImpl(int minArgs, int maxArgs, XPathResultType returnType, XPathResultType[] argTypes)
		{
			this.Init(minArgs, maxArgs, returnType, argTypes);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00022E3C File Offset: 0x0002103C
		protected void Init(int minArgs, int maxArgs, XPathResultType returnType, XPathResultType[] argTypes)
		{
			this.minargs = minArgs;
			this.maxargs = maxArgs;
			this.returnType = returnType;
			this.argTypes = argTypes;
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x00022E5C File Offset: 0x0002105C
		public int Minargs
		{
			get
			{
				return this.minargs;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x00022E64 File Offset: 0x00021064
		public int Maxargs
		{
			get
			{
				return this.maxargs;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x00022E6C File Offset: 0x0002106C
		public XPathResultType ReturnType
		{
			get
			{
				return this.returnType;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x00022E74 File Offset: 0x00021074
		public XPathResultType[] ArgTypes
		{
			get
			{
				return this.argTypes;
			}
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00022E7C File Offset: 0x0002107C
		public object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
		{
			return this.Invoke((XsltCompiledContext)xsltContext, args, docContext);
		}

		// Token: 0x060005A8 RID: 1448
		public abstract object Invoke(XsltCompiledContext xsltContext, object[] args, XPathNavigator docContext);

		// Token: 0x060005A9 RID: 1449 RVA: 0x00022E8C File Offset: 0x0002108C
		public static XPathResultType GetXPathType(Type type, XPathNavigator node)
		{
			TypeCode typeCode = Type.GetTypeCode(type);
			switch (typeCode)
			{
			case TypeCode.Object:
				if (typeof(XPathNavigator).IsAssignableFrom(type) || typeof(IXPathNavigable).IsAssignableFrom(type))
				{
					return XPathResultType.Navigator;
				}
				if (typeof(XPathNodeIterator).IsAssignableFrom(type))
				{
					return XPathResultType.NodeSet;
				}
				return XPathResultType.Any;
			default:
				switch (typeCode)
				{
				case TypeCode.DateTime:
					throw new XsltException("Invalid type DateTime was specified.", null, node);
				case TypeCode.String:
					return XPathResultType.String;
				}
				return XPathResultType.Number;
			case TypeCode.Boolean:
				return XPathResultType.Boolean;
			}
		}

		// Token: 0x04000398 RID: 920
		private int minargs;

		// Token: 0x04000399 RID: 921
		private int maxargs;

		// Token: 0x0400039A RID: 922
		private XPathResultType returnType;

		// Token: 0x0400039B RID: 923
		private XPathResultType[] argTypes;
	}
}
