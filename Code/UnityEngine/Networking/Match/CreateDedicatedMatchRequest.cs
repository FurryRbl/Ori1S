using System;
using System.Collections.Generic;

namespace UnityEngine.Networking.Match
{
	// Token: 0x0200023E RID: 574
	public class CreateDedicatedMatchRequest : Request
	{
		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x060022E2 RID: 8930 RVA: 0x0002BE74 File Offset: 0x0002A074
		// (set) Token: 0x060022E3 RID: 8931 RVA: 0x0002BE7C File Offset: 0x0002A07C
		public string name { get; set; }

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x060022E4 RID: 8932 RVA: 0x0002BE88 File Offset: 0x0002A088
		// (set) Token: 0x060022E5 RID: 8933 RVA: 0x0002BE90 File Offset: 0x0002A090
		public uint size { get; set; }

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x060022E6 RID: 8934 RVA: 0x0002BE9C File Offset: 0x0002A09C
		// (set) Token: 0x060022E7 RID: 8935 RVA: 0x0002BEA4 File Offset: 0x0002A0A4
		public bool advertise { get; set; }

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x060022E8 RID: 8936 RVA: 0x0002BEB0 File Offset: 0x0002A0B0
		// (set) Token: 0x060022E9 RID: 8937 RVA: 0x0002BEB8 File Offset: 0x0002A0B8
		public string password { get; set; }

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x060022EA RID: 8938 RVA: 0x0002BEC4 File Offset: 0x0002A0C4
		// (set) Token: 0x060022EB RID: 8939 RVA: 0x0002BECC File Offset: 0x0002A0CC
		public string publicAddress { get; set; }

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x060022EC RID: 8940 RVA: 0x0002BED8 File Offset: 0x0002A0D8
		// (set) Token: 0x060022ED RID: 8941 RVA: 0x0002BEE0 File Offset: 0x0002A0E0
		public string privateAddress { get; set; }

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x060022EE RID: 8942 RVA: 0x0002BEEC File Offset: 0x0002A0EC
		// (set) Token: 0x060022EF RID: 8943 RVA: 0x0002BEF4 File Offset: 0x0002A0F4
		public int eloScore { get; set; }

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x060022F0 RID: 8944 RVA: 0x0002BF00 File Offset: 0x0002A100
		// (set) Token: 0x060022F1 RID: 8945 RVA: 0x0002BF08 File Offset: 0x0002A108
		public Dictionary<string, long> matchAttributes { get; set; }

		// Token: 0x060022F2 RID: 8946 RVA: 0x0002BF14 File Offset: 0x0002A114
		public override bool IsValid()
		{
			return this.matchAttributes == null || this.matchAttributes.Count <= 10;
		}
	}
}
