using System;

namespace System.IO.Pipes
{
	// Token: 0x02000085 RID: 133
	internal struct SecurityAttributesHack
	{
		// Token: 0x0600063E RID: 1598 RVA: 0x0001A350 File Offset: 0x00018550
		public SecurityAttributesHack(bool inheritable)
		{
			this.Length = 0;
			this.SecurityDescriptor = IntPtr.Zero;
			this.Inheritable = inheritable;
		}

		// Token: 0x040001BB RID: 443
		public readonly int Length;

		// Token: 0x040001BC RID: 444
		public readonly IntPtr SecurityDescriptor;

		// Token: 0x040001BD RID: 445
		public readonly bool Inheritable;
	}
}
