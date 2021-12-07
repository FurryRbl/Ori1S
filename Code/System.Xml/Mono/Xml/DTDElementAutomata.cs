using System;

namespace Mono.Xml
{
	// Token: 0x020000B7 RID: 183
	internal class DTDElementAutomata : DTDAutomata
	{
		// Token: 0x06000663 RID: 1635 RVA: 0x0002556C File Offset: 0x0002376C
		public DTDElementAutomata(DTDObjectModel root, string name) : base(root)
		{
			this.name = name;
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x0002557C File Offset: 0x0002377C
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00025584 File Offset: 0x00023784
		public override DTDAutomata TryStartElement(string name)
		{
			if (name == this.Name)
			{
				return base.Root.Empty;
			}
			return base.Root.Invalid;
		}

		// Token: 0x040003D0 RID: 976
		private string name;
	}
}
