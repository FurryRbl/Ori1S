using System;
using System.Runtime.Serialization;

namespace UnityEngine
{
	// Token: 0x02000305 RID: 773
	[Serializable]
	public class MissingComponentException : SystemException
	{
		// Token: 0x060026F1 RID: 9969 RVA: 0x00036D6C File Offset: 0x00034F6C
		public MissingComponentException() : base("A Unity Runtime error occurred!")
		{
			base.HResult = -2147467261;
		}

		// Token: 0x060026F2 RID: 9970 RVA: 0x00036D84 File Offset: 0x00034F84
		public MissingComponentException(string message) : base(message)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x060026F3 RID: 9971 RVA: 0x00036D98 File Offset: 0x00034F98
		public MissingComponentException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x060026F4 RID: 9972 RVA: 0x00036DB0 File Offset: 0x00034FB0
		protected MissingComponentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04000C05 RID: 3077
		private const int Result = -2147467261;

		// Token: 0x04000C06 RID: 3078
		private string unityStackTrace;
	}
}
