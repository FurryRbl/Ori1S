using System;

namespace Mono.Xml.Xsl.Operations
{
	// Token: 0x02000072 RID: 114
	internal class XslGlobalParam : XslGlobalVariable
	{
		// Token: 0x060003BF RID: 959 RVA: 0x000199C8 File Offset: 0x00017BC8
		public XslGlobalParam(Compiler c) : base(c)
		{
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x000199D4 File Offset: 0x00017BD4
		public void Override(XslTransformProcessor p, object paramVal)
		{
			p.globalVariableTable[this] = paramVal;
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x000199E4 File Offset: 0x00017BE4
		public override bool IsParam
		{
			get
			{
				return true;
			}
		}
	}
}
