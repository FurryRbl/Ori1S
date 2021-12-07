using System;
using System.Runtime.Serialization;

namespace ManagedSteam.Exceptions
{
	// Token: 0x0200013F RID: 319
	[Serializable]
	public class InvalidInterfaceVersionException : ManagedException
	{
		// Token: 0x06000B6E RID: 2926 RVA: 0x0000F9E8 File Offset: 0x0000DBE8
		internal InvalidInterfaceVersionException(ErrorCodes code, params object[] args) : base(code, args)
		{
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0000F9F2 File Offset: 0x0000DBF2
		internal InvalidInterfaceVersionException(StringID code, params object[] args) : base(code, args)
		{
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0000F9FC File Offset: 0x0000DBFC
		protected InvalidInterfaceVersionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
