using System;

namespace UnityEngine.Networking
{
	// Token: 0x0200000A RID: 10
	[AttributeUsage(AttributeTargets.Method)]
	public class ClientRpcAttribute : Attribute
	{
		// Token: 0x04000036 RID: 54
		public int channel;
	}
}
