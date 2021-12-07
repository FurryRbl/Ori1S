using System;

namespace UnityEngine.Networking
{
	// Token: 0x02000016 RID: 22
	public abstract class MessageBase
	{
		// Token: 0x06000091 RID: 145 RVA: 0x00004A84 File Offset: 0x00002C84
		public virtual void Deserialize(NetworkReader reader)
		{
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004A88 File Offset: 0x00002C88
		public virtual void Serialize(NetworkWriter writer)
		{
		}
	}
}
