using System;

namespace System.IO
{
	// Token: 0x02000285 RID: 645
	internal struct InotifyEvent
	{
		// Token: 0x060016B2 RID: 5810 RVA: 0x0003DA30 File Offset: 0x0003BC30
		public override string ToString()
		{
			return string.Format("[Descriptor: {0} Mask: {1} Name: {2}]", this.WatchDescriptor, this.Mask, this.Name);
		}

		// Token: 0x0400075B RID: 1883
		public static readonly InotifyEvent Default = default(InotifyEvent);

		// Token: 0x0400075C RID: 1884
		public int WatchDescriptor;

		// Token: 0x0400075D RID: 1885
		public InotifyMask Mask;

		// Token: 0x0400075E RID: 1886
		public string Name;
	}
}
