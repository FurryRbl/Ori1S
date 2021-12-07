using System;
using System.Runtime.Serialization;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000304 RID: 772
	[RequiredByNativeCode]
	[Serializable]
	public class UnityException : SystemException
	{
		// Token: 0x060026ED RID: 9965 RVA: 0x00036D1C File Offset: 0x00034F1C
		public UnityException() : base("A Unity Runtime error occurred!")
		{
			base.HResult = -2147467261;
		}

		// Token: 0x060026EE RID: 9966 RVA: 0x00036D34 File Offset: 0x00034F34
		public UnityException(string message) : base(message)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x060026EF RID: 9967 RVA: 0x00036D48 File Offset: 0x00034F48
		public UnityException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x060026F0 RID: 9968 RVA: 0x00036D60 File Offset: 0x00034F60
		protected UnityException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04000C03 RID: 3075
		private const int Result = -2147467261;

		// Token: 0x04000C04 RID: 3076
		private string unityStackTrace;
	}
}
