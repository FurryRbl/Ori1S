using System;
using UnityEngine.Networking.Types;

namespace UnityEngine.Networking.Match
{
	// Token: 0x02000236 RID: 566
	public class JoinMatchRequest : Request
	{
		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06002283 RID: 8835 RVA: 0x0002B4F4 File Offset: 0x000296F4
		// (set) Token: 0x06002284 RID: 8836 RVA: 0x0002B4FC File Offset: 0x000296FC
		public NetworkID networkId { get; set; }

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06002285 RID: 8837 RVA: 0x0002B508 File Offset: 0x00029708
		// (set) Token: 0x06002286 RID: 8838 RVA: 0x0002B510 File Offset: 0x00029710
		public string publicAddress { get; set; }

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06002287 RID: 8839 RVA: 0x0002B51C File Offset: 0x0002971C
		// (set) Token: 0x06002288 RID: 8840 RVA: 0x0002B524 File Offset: 0x00029724
		public string privateAddress { get; set; }

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06002289 RID: 8841 RVA: 0x0002B530 File Offset: 0x00029730
		// (set) Token: 0x0600228A RID: 8842 RVA: 0x0002B538 File Offset: 0x00029738
		public int eloScore { get; set; }

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x0600228B RID: 8843 RVA: 0x0002B544 File Offset: 0x00029744
		// (set) Token: 0x0600228C RID: 8844 RVA: 0x0002B54C File Offset: 0x0002974C
		public string password { get; set; }

		// Token: 0x0600228D RID: 8845 RVA: 0x0002B558 File Offset: 0x00029758
		public override string ToString()
		{
			return UnityString.Format("[{0}]-networkId:0x{1},HasPassword:{2}", new object[]
			{
				base.ToString(),
				this.networkId.ToString("X"),
				(!(this.password == string.Empty)) ? "YES" : "NO"
			});
		}

		// Token: 0x0600228E RID: 8846 RVA: 0x0002B5C0 File Offset: 0x000297C0
		public override bool IsValid()
		{
			return base.IsValid() && this.networkId != NetworkID.Invalid;
		}
	}
}
