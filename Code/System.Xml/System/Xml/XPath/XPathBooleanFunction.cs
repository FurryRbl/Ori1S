using System;

namespace System.Xml.XPath
{
	// Token: 0x02000158 RID: 344
	internal abstract class XPathBooleanFunction : XPathFunction
	{
		// Token: 0x06000F8D RID: 3981 RVA: 0x0004A9A0 File Offset: 0x00048BA0
		public XPathBooleanFunction(FunctionArguments args) : base(args)
		{
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06000F8E RID: 3982 RVA: 0x0004A9AC File Offset: 0x00048BAC
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.Boolean;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06000F8F RID: 3983 RVA: 0x0004A9B0 File Offset: 0x00048BB0
		public override object StaticValue
		{
			get
			{
				return this.StaticValueAsBoolean;
			}
		}
	}
}
