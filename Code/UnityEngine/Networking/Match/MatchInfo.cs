using System;
using UnityEngine.Networking.Types;

namespace UnityEngine.Networking.Match
{
	// Token: 0x02000248 RID: 584
	public class MatchInfo
	{
		// Token: 0x0600230F RID: 8975 RVA: 0x0002C204 File Offset: 0x0002A404
		public MatchInfo(CreateMatchResponse matchResponse)
		{
			this.address = matchResponse.address;
			this.port = matchResponse.port;
			this.networkId = matchResponse.networkId;
			this.accessToken = new NetworkAccessToken(matchResponse.accessTokenString);
			this.nodeId = matchResponse.nodeId;
			this.usingRelay = matchResponse.usingRelay;
		}

		// Token: 0x06002310 RID: 8976 RVA: 0x0002C264 File Offset: 0x0002A464
		public MatchInfo(JoinMatchResponse matchResponse)
		{
			this.address = matchResponse.address;
			this.port = matchResponse.port;
			this.networkId = matchResponse.networkId;
			this.accessToken = new NetworkAccessToken(matchResponse.accessTokenString);
			this.nodeId = matchResponse.nodeId;
			this.usingRelay = matchResponse.usingRelay;
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06002311 RID: 8977 RVA: 0x0002C2C4 File Offset: 0x0002A4C4
		// (set) Token: 0x06002312 RID: 8978 RVA: 0x0002C2CC File Offset: 0x0002A4CC
		public string address { get; private set; }

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06002313 RID: 8979 RVA: 0x0002C2D8 File Offset: 0x0002A4D8
		// (set) Token: 0x06002314 RID: 8980 RVA: 0x0002C2E0 File Offset: 0x0002A4E0
		public int port { get; private set; }

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06002315 RID: 8981 RVA: 0x0002C2EC File Offset: 0x0002A4EC
		// (set) Token: 0x06002316 RID: 8982 RVA: 0x0002C2F4 File Offset: 0x0002A4F4
		public NetworkID networkId { get; private set; }

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06002317 RID: 8983 RVA: 0x0002C300 File Offset: 0x0002A500
		// (set) Token: 0x06002318 RID: 8984 RVA: 0x0002C308 File Offset: 0x0002A508
		public NetworkAccessToken accessToken { get; private set; }

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06002319 RID: 8985 RVA: 0x0002C314 File Offset: 0x0002A514
		// (set) Token: 0x0600231A RID: 8986 RVA: 0x0002C31C File Offset: 0x0002A51C
		public NodeID nodeId { get; private set; }

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x0600231B RID: 8987 RVA: 0x0002C328 File Offset: 0x0002A528
		// (set) Token: 0x0600231C RID: 8988 RVA: 0x0002C330 File Offset: 0x0002A530
		public bool usingRelay { get; private set; }

		// Token: 0x0600231D RID: 8989 RVA: 0x0002C33C File Offset: 0x0002A53C
		public override string ToString()
		{
			return UnityString.Format("{0} @ {1}:{2} [{3},{4}]", new object[]
			{
				this.networkId,
				this.address,
				this.port,
				this.nodeId,
				this.usingRelay
			});
		}
	}
}
