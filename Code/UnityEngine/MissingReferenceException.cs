using System;
using System.Runtime.Serialization;

namespace UnityEngine
{
	// Token: 0x02000307 RID: 775
	[Serializable]
	public class MissingReferenceException : SystemException
	{
		// Token: 0x060026F9 RID: 9977 RVA: 0x00036E0C File Offset: 0x0003500C
		public MissingReferenceException() : base("A Unity Runtime error occurred!")
		{
			base.HResult = -2147467261;
		}

		// Token: 0x060026FA RID: 9978 RVA: 0x00036E24 File Offset: 0x00035024
		public MissingReferenceException(string message) : base(message)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x060026FB RID: 9979 RVA: 0x00036E38 File Offset: 0x00035038
		public MissingReferenceException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x060026FC RID: 9980 RVA: 0x00036E50 File Offset: 0x00035050
		protected MissingReferenceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04000C09 RID: 3081
		private const int Result = -2147467261;

		// Token: 0x04000C0A RID: 3082
		private string unityStackTrace;
	}
}
