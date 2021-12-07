using System;

namespace Mono.Xml
{
	// Token: 0x020000BC RID: 188
	internal class DTDAnyAutomata : DTDAutomata
	{
		// Token: 0x0600067A RID: 1658 RVA: 0x00025850 File Offset: 0x00023A50
		public DTDAnyAutomata(DTDObjectModel root) : base(root)
		{
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0002585C File Offset: 0x00023A5C
		public override DTDAutomata TryEndElement()
		{
			return this;
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00025860 File Offset: 0x00023A60
		public override DTDAutomata TryStartElement(string name)
		{
			return this;
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x00025864 File Offset: 0x00023A64
		public override bool Emptiable
		{
			get
			{
				return true;
			}
		}
	}
}
