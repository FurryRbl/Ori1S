using System;

namespace Mono.Xml
{
	// Token: 0x020000BD RID: 189
	internal class DTDInvalidAutomata : DTDAutomata
	{
		// Token: 0x0600067E RID: 1662 RVA: 0x00025868 File Offset: 0x00023A68
		public DTDInvalidAutomata(DTDObjectModel root) : base(root)
		{
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00025874 File Offset: 0x00023A74
		public override DTDAutomata TryEndElement()
		{
			return this;
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00025878 File Offset: 0x00023A78
		public override DTDAutomata TryStartElement(string name)
		{
			return this;
		}
	}
}
