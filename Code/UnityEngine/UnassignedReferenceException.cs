using System;
using System.Runtime.Serialization;

namespace UnityEngine
{
	// Token: 0x02000306 RID: 774
	[Serializable]
	public class UnassignedReferenceException : SystemException
	{
		// Token: 0x060026F5 RID: 9973 RVA: 0x00036DBC File Offset: 0x00034FBC
		public UnassignedReferenceException() : base("A Unity Runtime error occurred!")
		{
			base.HResult = -2147467261;
		}

		// Token: 0x060026F6 RID: 9974 RVA: 0x00036DD4 File Offset: 0x00034FD4
		public UnassignedReferenceException(string message) : base(message)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x060026F7 RID: 9975 RVA: 0x00036DE8 File Offset: 0x00034FE8
		public UnassignedReferenceException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x060026F8 RID: 9976 RVA: 0x00036E00 File Offset: 0x00035000
		protected UnassignedReferenceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04000C07 RID: 3079
		private const int Result = -2147467261;

		// Token: 0x04000C08 RID: 3080
		private string unityStackTrace;
	}
}
