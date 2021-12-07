using System;

namespace Mono.Xml
{
	// Token: 0x020000BB RID: 187
	internal class DTDEmptyAutomata : DTDAutomata
	{
		// Token: 0x06000676 RID: 1654 RVA: 0x0002582C File Offset: 0x00023A2C
		public DTDEmptyAutomata(DTDObjectModel root) : base(root)
		{
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00025838 File Offset: 0x00023A38
		public override DTDAutomata TryEndElement()
		{
			return this;
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0002583C File Offset: 0x00023A3C
		public override DTDAutomata TryStartElement(string name)
		{
			return base.Root.Invalid;
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x0002584C File Offset: 0x00023A4C
		public override bool Emptiable
		{
			get
			{
				return true;
			}
		}
	}
}
