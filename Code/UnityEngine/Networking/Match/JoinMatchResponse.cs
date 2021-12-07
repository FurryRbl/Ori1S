using System;
using System.Collections.Generic;
using UnityEngine.Networking.Types;

namespace UnityEngine.Networking.Match
{
	// Token: 0x02000237 RID: 567
	public class JoinMatchResponse : BasicResponse
	{
		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06002290 RID: 8848 RVA: 0x0002B5E8 File Offset: 0x000297E8
		// (set) Token: 0x06002291 RID: 8849 RVA: 0x0002B5F0 File Offset: 0x000297F0
		public string address { get; set; }

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06002292 RID: 8850 RVA: 0x0002B5FC File Offset: 0x000297FC
		// (set) Token: 0x06002293 RID: 8851 RVA: 0x0002B604 File Offset: 0x00029804
		public int port { get; set; }

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06002294 RID: 8852 RVA: 0x0002B610 File Offset: 0x00029810
		// (set) Token: 0x06002295 RID: 8853 RVA: 0x0002B618 File Offset: 0x00029818
		public NetworkID networkId { get; set; }

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06002296 RID: 8854 RVA: 0x0002B624 File Offset: 0x00029824
		// (set) Token: 0x06002297 RID: 8855 RVA: 0x0002B62C File Offset: 0x0002982C
		public string accessTokenString { get; set; }

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06002298 RID: 8856 RVA: 0x0002B638 File Offset: 0x00029838
		// (set) Token: 0x06002299 RID: 8857 RVA: 0x0002B640 File Offset: 0x00029840
		public NodeID nodeId { get; set; }

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x0600229A RID: 8858 RVA: 0x0002B64C File Offset: 0x0002984C
		// (set) Token: 0x0600229B RID: 8859 RVA: 0x0002B654 File Offset: 0x00029854
		public bool usingRelay { get; set; }

		// Token: 0x0600229C RID: 8860 RVA: 0x0002B660 File Offset: 0x00029860
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

		// Token: 0x0600229D RID: 8861 RVA: 0x0002B6DC File Offset: 0x000298DC
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
