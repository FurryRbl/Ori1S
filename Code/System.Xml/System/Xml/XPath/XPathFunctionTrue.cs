using System;

namespace System.Xml.XPath
{
	// Token: 0x0200015B RID: 347
	internal class XPathFunctionTrue : XPathBooleanFunction
	{
		// Token: 0x06000F98 RID: 3992 RVA: 0x0004AB08 File Offset: 0x00048D08
		public XPathFunctionTrue(FunctionArguments args) : base(args)
		{
			if (args != null)
			{
				throw new XPathException("true takes 0 args");
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x0004AB24 File Offset: 0x00048D24
		public override bool HasStaticValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06000F9A RID: 3994 RVA: 0x0004AB28 File Offset: 0x00048D28
		public override bool StaticValueAsBoolean
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06000F9B RID: 3995 RVA: 0x0004AB2C File Offset: 0x00048D2C
		internal override bool Peer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x0004AB30 File Offset: 0x00048D30
		public override object Evaluate(BaseIterator iter)
		{
			return true;
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x0004AB38 File Offset: 0x00048D38
		public override string ToString()
		{
			return "true()";
		}
	}
}
