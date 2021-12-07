using System;
using System.Collections.Generic;
using UnityEngine.Networking.Types;

namespace UnityEngine.Networking.Match
{
	// Token: 0x0200023F RID: 575
	public class CreateDedicatedMatchResponse : BasicResponse
	{
		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x060022F4 RID: 8948 RVA: 0x0002BF4C File Offset: 0x0002A14C
		// (set) Token: 0x060022F5 RID: 8949 RVA: 0x0002BF54 File Offset: 0x0002A154
		public NetworkID networkId { get; set; }

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x060022F6 RID: 8950 RVA: 0x0002BF60 File Offset: 0x0002A160
		// (set) Token: 0x060022F7 RID: 8951 RVA: 0x0002BF68 File Offset: 0x0002A168
		public NodeID nodeId { get; set; }

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x060022F8 RID: 8952 RVA: 0x0002BF74 File Offset: 0x0002A174
		// (set) Token: 0x060022F9 RID: 8953 RVA: 0x0002BF7C File Offset: 0x0002A17C
		public string address { get; set; }

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x060022FA RID: 8954 RVA: 0x0002BF88 File Offset: 0x0002A188
		// (set) Token: 0x060022FB RID: 8955 RVA: 0x0002BF90 File Offset: 0x0002A190
		public int port { get; set; }

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x060022FC RID: 8956 RVA: 0x0002BF9C File Offset: 0x0002A19C
		// (set) Token: 0x060022FD RID: 8957 RVA: 0x0002BFA4 File Offset: 0x0002A1A4
		public string accessTokenString { get; set; }

		// Token: 0x060022FE RID: 8958 RVA: 0x0002BFB0 File Offset: 0x0002A1B0
		public override void Parse(object obj)
		{
			base.Parse(obj);
			IDictionary<string, object> dictionary = obj as IDictionary<string, object>;
			if (dictionary != null)
			{
				this.address = base.ParseJSONString("address", obj, dictionary);
				this.port = base.ParseJSONInt32("port", obj, dictionary);
				this.accessTokenString = base.ParseJSONString("accessTokenString", obj, dictionary);
				this.networkId = (NetworkID)base.ParseJSONUInt64("networkId", obj, dictionary);
				this.nodeId = (NodeID)base.ParseJSONUInt16("nodeId", obj, dictionary);
				return;
			}
			throw new FormatException("While parsing JSON response, found obj is not of type IDictionary<string,object>:" + obj.ToString());
		}
	}
}
