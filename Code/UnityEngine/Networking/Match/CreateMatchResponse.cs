using System;
using System.Collections.Generic;
using UnityEngine.Networking.Types;

namespace UnityEngine.Networking.Match
{
	// Token: 0x02000235 RID: 565
	public class CreateMatchResponse : BasicResponse
	{
		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06002274 RID: 8820 RVA: 0x0002B348 File Offset: 0x00029548
		// (set) Token: 0x06002275 RID: 8821 RVA: 0x0002B350 File Offset: 0x00029550
		public string address { get; set; }

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06002276 RID: 8822 RVA: 0x0002B35C File Offset: 0x0002955C
		// (set) Token: 0x06002277 RID: 8823 RVA: 0x0002B364 File Offset: 0x00029564
		public int port { get; set; }

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06002278 RID: 8824 RVA: 0x0002B370 File Offset: 0x00029570
		// (set) Token: 0x06002279 RID: 8825 RVA: 0x0002B378 File Offset: 0x00029578
		public NetworkID networkId { get; set; }

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x0600227A RID: 8826 RVA: 0x0002B384 File Offset: 0x00029584
		// (set) Token: 0x0600227B RID: 8827 RVA: 0x0002B38C File Offset: 0x0002958C
		public string accessTokenString { get; set; }

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x0600227C RID: 8828 RVA: 0x0002B398 File Offset: 0x00029598
		// (set) Token: 0x0600227D RID: 8829 RVA: 0x0002B3A0 File Offset: 0x000295A0
		public NodeID nodeId { get; set; }

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x0600227E RID: 8830 RVA: 0x0002B3AC File Offset: 0x000295AC
		// (set) Token: 0x0600227F RID: 8831 RVA: 0x0002B3B4 File Offset: 0x000295B4
		public bool usingRelay { get; set; }

		// Token: 0x06002280 RID: 8832 RVA: 0x0002B3C0 File Offset: 0x000295C0
		public override string ToString()
		{
			return UnityString.Format("[{0}]-address:{1},port:{2},networkId:0x{3},nodeId:0x{4},usingRelay:{5}", new object[]
			{
				base.ToString(),
				this.address,
				this.port,
				this.networkId.ToString("X"),
				this.nodeId.ToString("X"),
				this.usingRelay
			});
		}

		// Token: 0x06002281 RID: 8833 RVA: 0x0002B43C File Offset: 0x0002963C
		public override void Parse(object obj)
		{
			base.Parse(obj);
			IDictionary<string, object> dictionary = obj as IDictionary<string, object>;
			if (dictionary != null)
			{
				this.address = base.ParseJSONString("address", obj, dictionary);
				this.port = base.ParseJSONInt32("port", obj, dictionary);
				this.networkId = (NetworkID)base.ParseJSONUInt64("networkId", obj, dictionary);
				this.accessTokenString = base.ParseJSONString("accessTokenString", obj, dictionary);
				this.nodeId = (NodeID)base.ParseJSONUInt16("nodeId", obj, dictionary);
				this.usingRelay = base.ParseJSONBool("usingRelay", obj, dictionary);
				return;
			}
			throw new FormatException("While parsing JSON response, found obj is not of type IDictionary<string,object>:" + obj.ToString());
		}
	}
}
