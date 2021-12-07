using System;
using System.Collections.Generic;

namespace UnityEngine.Networking.NetworkSystem
{
	// Token: 0x02000021 RID: 33
	public class PeerInfoMessage : MessageBase
	{
		// Token: 0x060000AD RID: 173 RVA: 0x00004C5C File Offset: 0x00002E5C
		public override void Deserialize(NetworkReader reader)
		{
			this.connectionId = (int)reader.ReadPackedUInt32();
			this.address = reader.ReadString();
			this.port = (int)reader.ReadPackedUInt32();
			this.isHost = reader.ReadBoolean();
			this.isYou = reader.ReadBoolean();
			uint num = reader.ReadPackedUInt32();
			if (num > 0U)
			{
				List<PeerInfoPlayer> list = new List<PeerInfoPlayer>();
				for (uint num2 = 0U; num2 < num; num2 += 1U)
				{
					PeerInfoPlayer item;
					item.netId = reader.ReadNetworkId();
					item.playerControllerId = (short)reader.ReadPackedUInt32();
					list.Add(item);
				}
				this.playerIds = list.ToArray();
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004CFC File Offset: 0x00002EFC
		public override void Serialize(NetworkWriter writer)
		{
			writer.WritePackedUInt32((uint)this.connectionId);
			writer.Write(this.address);
			writer.WritePackedUInt32((uint)this.port);
			writer.Write(this.isHost);
			writer.Write(this.isYou);
			if (this.playerIds == null)
			{
				writer.WritePackedUInt32(0U);
			}
			else
			{
				writer.WritePackedUInt32((uint)this.playerIds.Length);
				for (int i = 0; i < this.playerIds.Length; i++)
				{
					writer.Write(this.playerIds[i].netId);
					writer.WritePackedUInt32((uint)this.playerIds[i].playerControllerId);
				}
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004DB4 File Offset: 0x00002FB4
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"PeerInfo conn:",
				this.connectionId,
				" addr:",
				this.address,
				":",
				this.port,
				" host:",
				this.isHost,
				" isYou:",
				this.isYou
			});
		}

		// Token: 0x0400005E RID: 94
		public int connectionId;

		// Token: 0x0400005F RID: 95
		public string address;

		// Token: 0x04000060 RID: 96
		public int port;

		// Token: 0x04000061 RID: 97
		public bool isHost;

		// Token: 0x04000062 RID: 98
		public bool isYou;

		// Token: 0x04000063 RID: 99
		public PeerInfoPlayer[] playerIds;
	}
}
