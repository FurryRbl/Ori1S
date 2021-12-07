using System;

namespace UnityEngine.Networking
{
	// Token: 0x02000007 RID: 7
	[AttributeUsage(AttributeTargets.Class)]
	public class NetworkSettingsAttribute : Attribute
	{
		// Token: 0x04000032 RID: 50
		public int channel;

		// Token: 0x04000033 RID: 51
		public float sendInterval = 0.1f;
	}
}
