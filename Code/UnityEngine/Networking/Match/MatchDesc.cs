using System;
using System.Collections.Generic;
using UnityEngine.Networking.Types;

namespace UnityEngine.Networking.Match
{
	// Token: 0x0200023C RID: 572
	public class MatchDesc : ResponseBase
	{
		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x060022C7 RID: 8903 RVA: 0x0002BBA0 File Offset: 0x00029DA0
		// (set) Token: 0x060022C8 RID: 8904 RVA: 0x0002BBA8 File Offset: 0x00029DA8
		public NetworkID networkId { get; set; }

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x060022C9 RID: 8905 RVA: 0x0002BBB4 File Offset: 0x00029DB4
		// (set) Token: 0x060022CA RID: 8906 RVA: 0x0002BBBC File Offset: 0x00029DBC
		public string name { get; set; }

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x060022CB RID: 8907 RVA: 0x0002BBC8 File Offset: 0x00029DC8
		// (set) Token: 0x060022CC RID: 8908 RVA: 0x0002BBD0 File Offset: 0x00029DD0
		public int averageEloScore { get; set; }

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x060022CD RID: 8909 RVA: 0x0002BBDC File Offset: 0x00029DDC
		// (set) Token: 0x060022CE RID: 8910 RVA: 0x0002BBE4 File Offset: 0x00029DE4
		public int maxSize { get; set; }

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x060022CF RID: 8911 RVA: 0x0002BBF0 File Offset: 0x00029DF0
		// (set) Token: 0x060022D0 RID: 8912 RVA: 0x0002BBF8 File Offset: 0x00029DF8
		public int currentSize { get; set; }

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x060022D1 RID: 8913 RVA: 0x0002BC04 File Offset: 0x00029E04
		// (set) Token: 0x060022D2 RID: 8914 RVA: 0x0002BC0C File Offset: 0x00029E0C
		public bool isPrivate { get; set; }

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x060022D3 RID: 8915 RVA: 0x0002BC18 File Offset: 0x00029E18
		// (set) Token: 0x060022D4 RID: 8916 RVA: 0x0002BC20 File Offset: 0x00029E20
		public Dictionary<string, long> matchAttributes { get; set; }

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x060022D5 RID: 8917 RVA: 0x0002BC2C File Offset: 0x00029E2C
		// (set) Token: 0x060022D6 RID: 8918 RVA: 0x0002BC34 File Offset: 0x00029E34
		public NodeID hostNodeId { get; set; }

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x060022D7 RID: 8919 RVA: 0x0002BC40 File Offset: 0x00029E40
		// (set) Token: 0x060022D8 RID: 8920 RVA: 0x0002BC48 File Offset: 0x00029E48
		public List<MatchDirectConnectInfo> directConnectInfos { get; set; }

		// Token: 0x060022D9 RID: 8921 RVA: 0x0002BC54 File Offset: 0x00029E54
		public override string ToString()
		{
			return UnityString.Format("[{0}]-networkId:0x{1},name:{2},averageEloScore:{3},maxSize:{4},currentSize:{5},isPrivate:{6},matchAttributes.Count:{7},directConnectInfos.Count:{8}", new object[]
			{
				base.ToString(),
				this.networkId.ToString("X"),
				this.name,
				this.averageEloScore,
				this.maxSize,
				this.currentSize,
				this.isPrivate,
				(this.matchAttributes != null) ? this.matchAttributes.Count : 0,
				this.directConnectInfos.Count
			});
		}

		// Token: 0x060022DA RID: 8922 RVA: 0x0002BD0C File Offset: 0x00029F0C
		public override void Parse(object obj)
		{
			IDictionary<string, object> dictionary = obj as IDictionary<string, object>;
			if (dictionary != null)
			{
				this.networkId = (NetworkID)base.ParseJSONUInt64("networkId", obj, dictionary);
				this.name = base.ParseJSONString("name", obj, dictionary);
				this.maxSize = base.ParseJSONInt32("maxSize", obj, dictionary);
				this.currentSize = base.ParseJSONInt32("currentSize", obj, dictionary);
				this.isPrivate = base.ParseJSONBool("isPrivate", obj, dictionary);
				this.directConnectInfos = base.ParseJSONList<MatchDirectConnectInfo>("directConnectInfos", obj, dictionary);
				return;
			}
			throw new FormatException("While parsing JSON response, found obj is not of type IDictionary<string,object>:" + obj.ToString());
		}
	}
}
