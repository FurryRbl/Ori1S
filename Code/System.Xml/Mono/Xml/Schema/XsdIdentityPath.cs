using System;

namespace Mono.Xml.Schema
{
	// Token: 0x0200001E RID: 30
	internal class XsdIdentityPath
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00007BF0 File Offset: 0x00005DF0
		public bool IsAttribute
		{
			get
			{
				return this.OrderedSteps.Length != 0 && this.OrderedSteps[this.OrderedSteps.Length - 1].IsAttribute;
			}
		}

		// Token: 0x040000EF RID: 239
		public XsdIdentityStep[] OrderedSteps;

		// Token: 0x040000F0 RID: 240
		public bool Descendants;
	}
}
