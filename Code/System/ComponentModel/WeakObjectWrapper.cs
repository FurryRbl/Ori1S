using System;

namespace System.ComponentModel
{
	// Token: 0x020001C3 RID: 451
	internal sealed class WeakObjectWrapper
	{
		// Token: 0x06000FD4 RID: 4052 RVA: 0x0002990C File Offset: 0x00027B0C
		public WeakObjectWrapper(object target)
		{
			this.TargetHashCode = target.GetHashCode();
			this.Weak = new WeakReference(target);
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x00029938 File Offset: 0x00027B38
		// (set) Token: 0x06000FD6 RID: 4054 RVA: 0x00029940 File Offset: 0x00027B40
		public int TargetHashCode { get; private set; }

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x0002994C File Offset: 0x00027B4C
		// (set) Token: 0x06000FD8 RID: 4056 RVA: 0x00029954 File Offset: 0x00027B54
		public WeakReference Weak { get; private set; }
	}
}
