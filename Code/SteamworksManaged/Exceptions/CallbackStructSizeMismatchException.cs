using System;
using System.Runtime.Serialization;

namespace ManagedSteam.Exceptions
{
	// Token: 0x0200005A RID: 90
	[Serializable]
	public class CallbackStructSizeMismatchException : ManagedException
	{
		// Token: 0x0600031E RID: 798 RVA: 0x00006B8E File Offset: 0x00004D8E
		internal CallbackStructSizeMismatchException(ErrorCodes code, params object[] args) : base(code, args)
		{
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00006B98 File Offset: 0x00004D98
		internal CallbackStructSizeMismatchException(StringID code, params object[] args) : base(code, args)
		{
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00006BA2 File Offset: 0x00004DA2
		protected CallbackStructSizeMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
