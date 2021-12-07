using System;

namespace System.Xml.XPath
{
	// Token: 0x0200015E RID: 350
	internal abstract class XPathNumericFunction : XPathFunction
	{
		// Token: 0x06000FAA RID: 4010 RVA: 0x0004AC60 File Offset: 0x00048E60
		internal XPathNumericFunction(FunctionArguments args) : base(args)
		{
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06000FAB RID: 4011 RVA: 0x0004AC6C File Offset: 0x00048E6C
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.Number;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000FAC RID: 4012 RVA: 0x0004AC70 File Offset: 0x00048E70
		public override object StaticValue
		{
			get
			{
				return this.StaticValueAsNumber;
			}
		}
	}
}
