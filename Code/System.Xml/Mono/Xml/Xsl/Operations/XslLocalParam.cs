using System;

namespace Mono.Xml.Xsl.Operations
{
	// Token: 0x02000074 RID: 116
	internal class XslLocalParam : XslLocalVariable
	{
		// Token: 0x060003C8 RID: 968 RVA: 0x00019A70 File Offset: 0x00017C70
		public XslLocalParam(Compiler c) : base(c)
		{
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00019A7C File Offset: 0x00017C7C
		public override void Evaluate(XslTransformProcessor p)
		{
			if (p.Debugger != null)
			{
				p.Debugger.DebugExecute(p, base.DebugInput);
			}
			if (p.GetStackItem(this.slot) != null)
			{
				return;
			}
			if (p.Arguments != null && this.var.Select == null && this.var.Content == null)
			{
				object param = p.Arguments.GetParam(base.Name.Name, base.Name.Namespace);
				if (param != null)
				{
					this.Override(p, param);
					return;
				}
			}
			base.Evaluate(p);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00019B1C File Offset: 0x00017D1C
		public void Override(XslTransformProcessor p, object paramVal)
		{
			p.SetStackItem(this.slot, paramVal);
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060003CB RID: 971 RVA: 0x00019B2C File Offset: 0x00017D2C
		public override bool IsParam
		{
			get
			{
				return true;
			}
		}
	}
}
