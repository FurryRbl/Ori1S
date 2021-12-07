using System;
using System.Runtime.Serialization;

namespace System.Threading
{
	// Token: 0x02000063 RID: 99
	[Serializable]
	public class LockRecursionException : Exception
	{
		// Token: 0x0600055C RID: 1372 RVA: 0x000186DC File Offset: 0x000168DC
		public LockRecursionException()
		{
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x000186E4 File Offset: 0x000168E4
		public LockRecursionException(string message) : base(message)
		{
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x000186F0 File Offset: 0x000168F0
		public LockRecursionException(string message, Exception e) : base(message, e)
		{
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x000186FC File Offset: 0x000168FC
		protected LockRecursionException(SerializationInfo info, StreamingContext sc) : base(info, sc)
		{
		}
	}
}
