using System;

namespace System.IO
{
	// Token: 0x02000284 RID: 644
	[Flags]
	internal enum InotifyMask : uint
	{
		// Token: 0x04000746 RID: 1862
		Access = 1U,
		// Token: 0x04000747 RID: 1863
		Modify = 2U,
		// Token: 0x04000748 RID: 1864
		Attrib = 4U,
		// Token: 0x04000749 RID: 1865
		CloseWrite = 8U,
		// Token: 0x0400074A RID: 1866
		CloseNoWrite = 16U,
		// Token: 0x0400074B RID: 1867
		Open = 32U,
		// Token: 0x0400074C RID: 1868
		MovedFrom = 64U,
		// Token: 0x0400074D RID: 1869
		MovedTo = 128U,
		// Token: 0x0400074E RID: 1870
		Create = 256U,
		// Token: 0x0400074F RID: 1871
		Delete = 512U,
		// Token: 0x04000750 RID: 1872
		DeleteSelf = 1024U,
		// Token: 0x04000751 RID: 1873
		MoveSelf = 2048U,
		// Token: 0x04000752 RID: 1874
		BaseEvents = 4095U,
		// Token: 0x04000753 RID: 1875
		Umount = 8192U,
		// Token: 0x04000754 RID: 1876
		Overflow = 16384U,
		// Token: 0x04000755 RID: 1877
		Ignored = 32768U,
		// Token: 0x04000756 RID: 1878
		OnlyDir = 16777216U,
		// Token: 0x04000757 RID: 1879
		DontFollow = 33554432U,
		// Token: 0x04000758 RID: 1880
		AddMask = 536870912U,
		// Token: 0x04000759 RID: 1881
		Directory = 1073741824U,
		// Token: 0x0400075A RID: 1882
		OneShot = 2147483648U
	}
}
