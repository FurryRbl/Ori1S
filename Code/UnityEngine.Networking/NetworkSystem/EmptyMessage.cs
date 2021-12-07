using System;

namespace UnityEngine.Networking.NetworkSystem
{
	// Token: 0x02000019 RID: 25
	public class EmptyMessage : MessageBase
	{
		// Token: 0x0600009C RID: 156 RVA: 0x00004B04 File Offset: 0x00002D04
		public override void Deserialize(NetworkReader reader)
		{
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004B08 File Offset: 0x00002D08
		public override void Serialize(NetworkWriter writer)
		{
		}
	}
}
