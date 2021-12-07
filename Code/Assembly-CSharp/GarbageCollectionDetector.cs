using System;

// Token: 0x02000748 RID: 1864
public class GarbageCollectionDetector
{
	// Token: 0x06002BBF RID: 11199 RVA: 0x000BB654 File Offset: 0x000B9854
	~GarbageCollectionDetector()
	{
		FramePerformanceMonitor.GarbageCollectionFlag = true;
	}
}
