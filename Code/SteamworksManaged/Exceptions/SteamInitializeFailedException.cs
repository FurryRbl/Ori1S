using System;
using System.Runtime.Serialization;

namespace ManagedSteam.Exceptions
{
	// Token: 0x02000078 RID: 120
	[Serializable]
	public class SteamInitializeFailedException : NativeException
	{
		// Token: 0x06000403 RID: 1027 RVA: 0x00007A84 File Offset: 0x00005C84
		internal SteamInitializeFailedException(ErrorCodes code, params object[] args) : base(code, args)
		{
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00007A8E File Offset: 0x00005C8E
		internal SteamInitializeFailedException(StringID code, params object[] args) : base(code, args)
		{
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00007A98 File Offset: 0x00005C98
		protected SteamInitializeFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
