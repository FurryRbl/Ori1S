using System;
using System.Collections.Generic;

namespace UnityEngine.Networking.Match
{
	// Token: 0x02000234 RID: 564
	public class CreateMatchRequest : Request
	{
		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06002261 RID: 8801 RVA: 0x0002B1C0 File Offset: 0x000293C0
		// (set) Token: 0x06002262 RID: 8802 RVA: 0x0002B1C8 File Offset: 0x000293C8
		public string name { get; set; }

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06002263 RID: 8803 RVA: 0x0002B1D4 File Offset: 0x000293D4
		// (set) Token: 0x06002264 RID: 8804 RVA: 0x0002B1DC File Offset: 0x000293DC
		public uint size { get; set; }

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06002265 RID: 8805 RVA: 0x0002B1E8 File Offset: 0x000293E8
		// (set) Token: 0x06002266 RID: 8806 RVA: 0x0002B1F0 File Offset: 0x000293F0
		public string publicAddress { get; set; }

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06002267 RID: 8807 RVA: 0x0002B1FC File Offset: 0x000293FC
		// (set) Token: 0x06002268 RID: 8808 RVA: 0x0002B204 File Offset: 0x00029404
		public string privateAddress { get; set; }

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06002269 RID: 8809 RVA: 0x0002B210 File Offset: 0x00029410
		// (set) Token: 0x0600226A RID: 8810 RVA: 0x0002B218 File Offset: 0x00029418
		public int eloScore { get; set; }

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x0600226B RID: 8811 RVA: 0x0002B224 File Offset: 0x00029424
		// (set) Token: 0x0600226C RID: 8812 RVA: 0x0002B22C File Offset: 0x0002942C
		public bool advertise { get; set; }

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x0600226D RID: 8813 RVA: 0x0002B238 File Offset: 0x00029438
		// (set) Token: 0x0600226E RID: 8814 RVA: 0x0002B240 File Offset: 0x00029440
		public string password { get; set; }

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x0600226F RID: 8815 RVA: 0x0002B24C File Offset: 0x0002944C
		// (set) Token: 0x06002270 RID: 8816 RVA: 0x0002B254 File Offset: 0x00029454
		public Dictionary<string, long> matchAttributes { get; set; }

		// Token: 0x06002271 RID: 8817 RVA: 0x0002B260 File Offset: 0x00029460
		public override string ToString()
		{
			return UnityString.Format("[{0}]-name:{1},size:{2},advertise:{3},HasPassword:{4},matchAttributes.Count:{5}", new object[]
			{
				base.ToString(),
				this.name,
				this.size,
				this.advertise,
				(!(this.password == string.Empty)) ? "YES" : "NO",
				(this.matchAttributes != null) ? this.matchAttributes.Count : 0
			});
		}

		// Token: 0x06002272 RID: 8818 RVA: 0x0002B2F8 File Offset: 0x000294F8
		public override bool IsValid()
		{
			return (base.IsValid() && this.size >= 2U && this.matchAttributes == null) || this.matchAttributes.Count <= 10;
		}
	}
}
