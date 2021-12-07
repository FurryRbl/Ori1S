using System;
using System.Runtime.Serialization;

namespace ManagedSteam.Exceptions
{
	// Token: 0x0200008D RID: 141
	[Serializable]
	public class UsageAfterAPIShutdownException : ManagedException
	{
		// Token: 0x0600045F RID: 1119 RVA: 0x000081AD File Offset: 0x000063AD
		internal UsageAfterAPIShutdownException(ErrorCodes code, params object[] args) : base(code, args)
		{
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x000081B7 File Offset: 0x000063B7
		internal UsageAfterAPIShutdownException(StringID code, params object[] args) : base(code, args)
		{
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x000081C1 File Offset: 0x000063C1
		protected UsageAfterAPIShutdownException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
