using System;
using System.Runtime.Serialization;

namespace ManagedSteam.Exceptions
{
	// Token: 0x02000114 RID: 276
	[Serializable]
	public class NotAvailableInLiteException : ManagedException
	{
		// Token: 0x06000803 RID: 2051 RVA: 0x0000BB21 File Offset: 0x00009D21
		internal NotAvailableInLiteException() : base(ErrorCodes.NotAvailableInLite, new object[0])
		{
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0000BB34 File Offset: 0x00009D34
		protected NotAvailableInLiteException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
