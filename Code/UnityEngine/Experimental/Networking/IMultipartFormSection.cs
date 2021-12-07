using System;

namespace UnityEngine.Experimental.Networking
{
	// Token: 0x0200021F RID: 543
	public interface IMultipartFormSection
	{
		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x060021C1 RID: 8641
		string sectionName { get; }

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x060021C2 RID: 8642
		byte[] sectionData { get; }

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x060021C3 RID: 8643
		string fileName { get; }

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x060021C4 RID: 8644
		string contentType { get; }
	}
}
