using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020004A9 RID: 1193
	internal class MethodCallDictionary : MethodDictionary
	{
		// Token: 0x0600306E RID: 12398 RVA: 0x0009F698 File Offset: 0x0009D898
		public MethodCallDictionary(IMethodMessage message) : base(message)
		{
			base.MethodKeys = MethodCallDictionary.InternalKeys;
		}

		// Token: 0x04001483 RID: 5251
		public static string[] InternalKeys = new string[]
		{
			"__Uri",
			"__MethodName",
			"__TypeName",
			"__MethodSignature",
			"__Args",
			"__CallContext"
		};
	}
}
