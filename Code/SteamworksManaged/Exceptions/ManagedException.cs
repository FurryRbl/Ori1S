using System;
using System.Runtime.Serialization;

namespace ManagedSteam.Exceptions
{
	// Token: 0x02000059 RID: 89
	[Serializable]
	public class ManagedException : Exception
	{
		// Token: 0x0600031B RID: 795 RVA: 0x00006B66 File Offset: 0x00004D66
		internal ManagedException(ErrorCodes code, params object[] args) : base(StringMap.GetString(code, args))
		{
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00006B75 File Offset: 0x00004D75
		internal ManagedException(StringID code, params object[] args) : base(StringMap.GetString(code, args))
		{
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00006B84 File Offset: 0x00004D84
		protected ManagedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
