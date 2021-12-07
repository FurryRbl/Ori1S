using System;

namespace UnityEngine.Networking
{
	// Token: 0x02000009 RID: 9
	[AttributeUsage(AttributeTargets.Method)]
	public class CommandAttribute : Attribute
	{
		// Token: 0x04000035 RID: 53
		public int channel;
	}
}
