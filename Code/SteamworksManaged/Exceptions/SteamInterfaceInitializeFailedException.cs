using System;
using System.Runtime.Serialization;

namespace ManagedSteam.Exceptions
{
	// Token: 0x02000102 RID: 258
	[Serializable]
	public class SteamInterfaceInitializeFailedException : NativeException
	{
		// Token: 0x060007A3 RID: 1955 RVA: 0x0000B642 File Offset: 0x00009842
		internal SteamInterfaceInitializeFailedException(ErrorCodes code, params object[] args) : base(code, args)
		{
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0000B64C File Offset: 0x0000984C
		internal SteamInterfaceInitializeFailedException(StringID code, params object[] args) : base(code, args)
		{
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0000B656 File Offset: 0x00009856
		protected SteamInterfaceInitializeFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
