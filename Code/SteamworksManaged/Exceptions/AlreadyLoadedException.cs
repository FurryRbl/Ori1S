using System;
using System.Runtime.Serialization;

namespace ManagedSteam.Exceptions
{
	// Token: 0x02000103 RID: 259
	[Serializable]
	public class AlreadyLoadedException : NativeException
	{
		// Token: 0x060007A6 RID: 1958 RVA: 0x0000B660 File Offset: 0x00009860
		internal AlreadyLoadedException(ErrorCodes code, params object[] args) : base(code, args)
		{
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0000B66A File Offset: 0x0000986A
		internal AlreadyLoadedException(StringID code, params object[] args) : base(code, args)
		{
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0000B674 File Offset: 0x00009874
		protected AlreadyLoadedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
