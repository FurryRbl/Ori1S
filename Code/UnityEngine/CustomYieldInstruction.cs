using System;
using System.Collections;

namespace UnityEngine
{
	// Token: 0x02000011 RID: 17
	public abstract class CustomYieldInstruction : IEnumerator
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000066 RID: 102
		public abstract bool keepWaiting { get; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000067 RID: 103 RVA: 0x0000248C File Offset: 0x0000068C
		public object Current
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002490 File Offset: 0x00000690
		public bool MoveNext()
		{
			return this.keepWaiting;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002498 File Offset: 0x00000698
		public void Reset()
		{
		}
	}
}
