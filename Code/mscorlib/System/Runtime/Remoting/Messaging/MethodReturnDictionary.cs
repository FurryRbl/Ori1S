using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020004AC RID: 1196
	internal class MethodReturnDictionary : MethodDictionary
	{
		// Token: 0x06003092 RID: 12434 RVA: 0x0009FFA4 File Offset: 0x0009E1A4
		public MethodReturnDictionary(IMethodReturnMessage message) : base(message)
		{
			if (message.Exception == null)
			{
				base.MethodKeys = MethodReturnDictionary.InternalReturnKeys;
			}
			else
			{
				base.MethodKeys = MethodReturnDictionary.InternalExceptionKeys;
			}
		}

		// Token: 0x0400148D RID: 5261
		public static string[] InternalReturnKeys = new string[]
		{
			"__Uri",
			"__MethodName",
			"__TypeName",
			"__MethodSignature",
			"__OutArgs",
			"__Return",
			"__CallContext"
		};

		// Token: 0x0400148E RID: 5262
		public static string[] InternalExceptionKeys = new string[]
		{
			"__CallContext"
		};
	}
}
