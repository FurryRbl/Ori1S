using System;
using System.Collections.Generic;

namespace UnityEngine.Networking.Match
{
	// Token: 0x0200023D RID: 573
	public class ListMatchResponse : BasicResponse
	{
		// Token: 0x060022DB RID: 8923 RVA: 0x0002BDB4 File Offset: 0x00029FB4
		public ListMatchResponse()
		{
		}

		// Token: 0x060022DC RID: 8924 RVA: 0x0002BDBC File Offset: 0x00029FBC
		public ListMatchResponse(List<MatchDesc> otherMatches)
		{
			this.matches = otherMatches;
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x060022DD RID: 8925 RVA: 0x0002BDCC File Offset: 0x00029FCC
		// (set) Token: 0x060022DE RID: 8926 RVA: 0x0002BDD4 File Offset: 0x00029FD4
		public List<MatchDesc> matches { get; set; }

		// Token: 0x060022DF RID: 8927 RVA: 0x0002BDE0 File Offset: 0x00029FE0
		public override string ToString()
		{
			return UnityString.Format("[{0}]-matches.Count:{1}", new object[]
			{
				base.ToString(),
				this.matches.Count
			});
		}

		// Token: 0x060022E0 RID: 8928 RVA: 0x0002BE1C File Offset: 0x0002A01C
		public override void Parse(object obj)
		{
			base.Parse(obj);
			IDictionary<string, object> dictionary = obj as IDictionary<string, object>;
			if (dictionary != null)
			{
				this.matches = base.ParseJSONList<MatchDesc>("matches", obj, dictionary);
				return;
			}
			throw new FormatException("While parsing JSON response, found obj is not of type IDictionary<string,object>:" + obj.ToString());
		}
	}
}
