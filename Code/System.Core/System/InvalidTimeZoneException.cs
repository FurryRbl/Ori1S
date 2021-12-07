using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000011 RID: 17
	[Serializable]
	public class InvalidTimeZoneException : Exception
	{
		// Token: 0x060000FB RID: 251 RVA: 0x000067E4 File Offset: 0x000049E4
		public InvalidTimeZoneException()
		{
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000067EC File Offset: 0x000049EC
		public InvalidTimeZoneException(string message) : base(message)
		{
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000067F8 File Offset: 0x000049F8
		public InvalidTimeZoneException(string message, Exception e) : base(message, e)
		{
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00006804 File Offset: 0x00004A04
		protected InvalidTimeZoneException(SerializationInfo info, StreamingContext sc) : base(info, sc)
		{
		}
	}
}
