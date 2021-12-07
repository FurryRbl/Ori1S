using System;
using System.Collections.Generic;
using UnityEngine.Networking.Types;

namespace UnityEngine.Networking.Match
{
	// Token: 0x0200023B RID: 571
	public class MatchDirectConnectInfo : ResponseBase
	{
		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x060022BE RID: 8894 RVA: 0x0002BAA4 File Offset: 0x00029CA4
		// (set) Token: 0x060022BF RID: 8895 RVA: 0x0002BAAC File Offset: 0x00029CAC
		public NodeID nodeId { get; set; }

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x060022C0 RID: 8896 RVA: 0x0002BAB8 File Offset: 0x00029CB8
		// (set) Token: 0x060022C1 RID: 8897 RVA: 0x0002BAC0 File Offset: 0x00029CC0
		public string publicAddress { get; set; }

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x060022C2 RID: 8898 RVA: 0x0002BACC File Offset: 0x00029CCC
		// (set) Token: 0x060022C3 RID: 8899 RVA: 0x0002BAD4 File Offset: 0x00029CD4
		public string privateAddress { get; set; }

		// Token: 0x060022C4 RID: 8900 RVA: 0x0002BAE0 File Offset: 0x00029CE0
		public override string ToString()
		{
			return UnityString.Format("[{0}]-nodeId:{1},publicAddress:{2},privateAddress:{3}", new object[]
			{
				base.ToString(),
				this.nodeId,
				this.publicAddress,
				this.privateAddress
			});
		}

		// Token: 0x060022C5 RID: 8901 RVA: 0x0002BB28 File Offset: 0x00029D28
		public override void Parse(object obj)
		{
			IDictionary<string, object> dictionary = obj as IDictionary<string, object>;
			if (dictionary != null)
			{
				this.nodeId = (NodeID)base.ParseJSONUInt16("nodeId", obj, dictionary);
				this.publicAddress = base.ParseJSONString("publicAddress", obj, dictionary);
				this.privateAddress = base.ParseJSONString("privateAddress", obj, dictionary);
				return;
			}
			throw new FormatException("While parsing JSON response, found obj is not of type IDictionary<string,object>:" + obj.ToString());
		}
	}
}
