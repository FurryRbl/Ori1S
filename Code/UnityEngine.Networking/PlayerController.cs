using System;

namespace UnityEngine.Networking
{
	// Token: 0x0200005D RID: 93
	public class PlayerController
	{
		// Token: 0x060004E0 RID: 1248 RVA: 0x00019E98 File Offset: 0x00018098
		public PlayerController()
		{
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00019EA8 File Offset: 0x000180A8
		internal PlayerController(GameObject go, short playerControllerId)
		{
			this.gameObject = go;
			this.unetView = go.GetComponent<NetworkIdentity>();
			this.playerControllerId = playerControllerId;
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x00019ED4 File Offset: 0x000180D4
		public bool IsValid
		{
			get
			{
				return this.playerControllerId != -1;
			}
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00019EE4 File Offset: 0x000180E4
		public override string ToString()
		{
			return string.Format("ID={0} NetworkIdentity NetID={1} Player={2}", new object[]
			{
				this.playerControllerId,
				(!(this.unetView != null)) ? "null" : this.unetView.netId.ToString(),
				(!(this.gameObject != null)) ? "null" : this.gameObject.name
			});
		}

		// Token: 0x040001FE RID: 510
		internal const short kMaxLocalPlayers = 8;

		// Token: 0x040001FF RID: 511
		public const int MaxPlayersPerClient = 32;

		// Token: 0x04000200 RID: 512
		public short playerControllerId = -1;

		// Token: 0x04000201 RID: 513
		public NetworkIdentity unetView;

		// Token: 0x04000202 RID: 514
		public GameObject gameObject;
	}
}
