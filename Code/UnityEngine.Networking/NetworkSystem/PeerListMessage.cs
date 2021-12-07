using System;

namespace UnityEngine.Networking.NetworkSystem
{
	// Token: 0x02000022 RID: 34
	public class PeerListMessage : MessageBase
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x00004E40 File Offset: 0x00003040
		public override void Deserialize(NetworkReader reader)
		{
			this.oldServerConnectionId = (int)reader.ReadPackedUInt32();
			int num = (int)reader.ReadUInt16();
			this.peers = new PeerInfoMessage[num];
			for (int i = 0; i < this.peers.Length; i++)
			{
				PeerInfoMessage peerInfoMessage = new PeerInfoMessage();
				peerInfoMessage.Deserialize(reader);
				this.peers[i] = peerInfoMessage;
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004E9C File Offset: 0x0000309C
		public override void Serialize(NetworkWriter writer)
		{
			writer.WritePackedUInt32((uint)this.oldServerConnectionId);
			writer.Write((ushort)this.peers.Length);
			foreach (PeerInfoMessage peerInfoMessage in this.peers)
			{
				peerInfoMessage.Serialize(writer);
			}
		}

		// Token: 0x04000064 RID: 100
		public PeerInfoMessage[] peers;

		// Token: 0x04000065 RID: 101
		public int oldServerConnectionId;
	}
}
