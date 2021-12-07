using System;
using System.Runtime.Serialization;

namespace ManagedSteam.Exceptions
{
	// Token: 0x02000077 RID: 119
	[Serializable]
	public class NativeException : Exception
	{
		// Token: 0x06000400 RID: 1024 RVA: 0x00007A5C File Offset: 0x00005C5C
		internal NativeException(ErrorCodes code, params object[] args) : base(StringMap.GetString(code, args))
		{
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00007A6B File Offset: 0x00005C6B
		internal NativeException(StringID code, params object[] args) : base(StringMap.GetString(code, args))
		{
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00007A7A File Offset: 0x00005C7A
		protected NativeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
