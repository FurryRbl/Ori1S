using System;

namespace Mono.Xml.Xsl.Operations
{
	// Token: 0x02000073 RID: 115
	internal class XslLocalVariable : XslGeneralVariable
	{
		// Token: 0x060003C2 RID: 962 RVA: 0x000199E8 File Offset: 0x00017BE8
		public XslLocalVariable(Compiler c) : base(c)
		{
			this.slot = c.AddVariable(this);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00019A00 File Offset: 0x00017C00
		public override void Evaluate(XslTransformProcessor p)
		{
			if (p.Debugger != null)
			{
				p.Debugger.DebugExecute(p, base.DebugInput);
			}
			p.SetStackItem(this.slot, this.var.Evaluate(p));
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00019A44 File Offset: 0x00017C44
		protected override object GetValue(XslTransformProcessor p)
		{
			return p.GetStackItem(this.slot);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00019A54 File Offset: 0x00017C54
		public bool IsEvaluated(XslTransformProcessor p)
		{
			return p.GetStackItem(this.slot) != null;
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x00019A68 File Offset: 0x00017C68
		public override bool IsLocal
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x00019A6C File Offset: 0x00017C6C
		public override bool IsParam
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040002A4 RID: 676
		protected int slot;
	}
}
