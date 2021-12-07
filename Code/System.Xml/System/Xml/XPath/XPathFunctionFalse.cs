using System;

namespace System.Xml.XPath
{
	// Token: 0x0200015C RID: 348
	internal class XPathFunctionFalse : XPathBooleanFunction
	{
		// Token: 0x06000F9E RID: 3998 RVA: 0x0004AB40 File Offset: 0x00048D40
		public XPathFunctionFalse(FunctionArguments args) : base(args)
		{
			if (args != null)
			{
				throw new XPathException("false takes 0 args");
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x0004AB5C File Offset: 0x00048D5C
		public override bool HasStaticValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06000FA0 RID: 4000 RVA: 0x0004AB60 File Offset: 0x00048D60
		public override bool StaticValueAsBoolean
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x0004AB64 File Offset: 0x00048D64
		internal override bool Peer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x0004AB68 File Offset: 0x00048D68
		public override object Evaluate(BaseIterator iter)
		{
			return false;
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x0004AB70 File Offset: 0x00048D70
		public override string ToString()
		{
			return "false()";
		}
	}
}
