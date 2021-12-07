using System;
using System.Runtime.Serialization;

namespace ManagedSteam.Exceptions
{
	// Token: 0x0200000C RID: 12
	[Serializable]
	public class UnexpectedException : Exception
	{
		// Token: 0x0600001E RID: 30 RVA: 0x0000253F File Offset: 0x0000073F
		internal UnexpectedException(ErrorCodes code, params object[] args) : base(StringMap.GetString(code, args))
		{
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000254E File Offset: 0x0000074E
		internal UnexpectedException(StringID code, params object[] args) : base(StringMap.GetString(code, args))
		{
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000255D File Offset: 0x0000075D
		protected UnexpectedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
