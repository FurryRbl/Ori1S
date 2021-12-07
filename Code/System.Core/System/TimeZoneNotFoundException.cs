using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000016 RID: 22
	[Serializable]
	public class TimeZoneNotFoundException : Exception
	{
		// Token: 0x06000158 RID: 344 RVA: 0x000082E8 File Offset: 0x000064E8
		public TimeZoneNotFoundException()
		{
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000082F0 File Offset: 0x000064F0
		public TimeZoneNotFoundException(string message) : base(message)
		{
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000082FC File Offset: 0x000064FC
		public TimeZoneNotFoundException(string message, Exception e) : base(message, e)
		{
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00008308 File Offset: 0x00006508
		protected TimeZoneNotFoundException(SerializationInfo info, StreamingContext sc) : base(info, sc)
		{
		}
	}
}
