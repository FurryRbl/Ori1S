using System;
using System.Collections.Generic;

namespace UnityEngine.Networking.Match
{
	// Token: 0x0200023A RID: 570
	public class ListMatchRequest : Request
	{
		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x060022AB RID: 8875 RVA: 0x0002B8CC File Offset: 0x00029ACC
		// (set) Token: 0x060022AC RID: 8876 RVA: 0x0002B8D4 File Offset: 0x00029AD4
		public int pageSize { get; set; }

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x060022AD RID: 8877 RVA: 0x0002B8E0 File Offset: 0x00029AE0
		// (set) Token: 0x060022AE RID: 8878 RVA: 0x0002B8E8 File Offset: 0x00029AE8
		public int pageNum { get; set; }

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x060022AF RID: 8879 RVA: 0x0002B8F4 File Offset: 0x00029AF4
		// (set) Token: 0x060022B0 RID: 8880 RVA: 0x0002B8FC File Offset: 0x00029AFC
		public string nameFilter { get; set; }

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x060022B1 RID: 8881 RVA: 0x0002B908 File Offset: 0x00029B08
		// (set) Token: 0x060022B2 RID: 8882 RVA: 0x0002B910 File Offset: 0x00029B10
		public bool includePasswordMatches { get; set; }

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x060022B3 RID: 8883 RVA: 0x0002B91C File Offset: 0x00029B1C
		// (set) Token: 0x060022B4 RID: 8884 RVA: 0x0002B924 File Offset: 0x00029B24
		public int eloScore { get; set; }

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x060022B5 RID: 8885 RVA: 0x0002B930 File Offset: 0x00029B30
		// (set) Token: 0x060022B6 RID: 8886 RVA: 0x0002B938 File Offset: 0x00029B38
		public Dictionary<string, long> matchAttributeFilterLessThan { get; set; }

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x060022B7 RID: 8887 RVA: 0x0002B944 File Offset: 0x00029B44
		// (set) Token: 0x060022B8 RID: 8888 RVA: 0x0002B94C File Offset: 0x00029B4C
		public Dictionary<string, long> matchAttributeFilterEqualTo { get; set; }

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x060022B9 RID: 8889 RVA: 0x0002B958 File Offset: 0x00029B58
		// (set) Token: 0x060022BA RID: 8890 RVA: 0x0002B960 File Offset: 0x00029B60
		public Dictionary<string, long> matchAttributeFilterGreaterThan { get; set; }

		// Token: 0x060022BB RID: 8891 RVA: 0x0002B96C File Offset: 0x00029B6C
		public override string ToString()
		{
			return UnityString.Format("[{0}]-pageSize:{1},pageNum:{2},nameFilter:{3},matchAttributeFilterLessThan.Count:{4}, matchAttributeFilterGreaterThan.Count:{5}", new object[]
			{
				base.ToString(),
				this.pageSize,
				this.pageNum,
				this.nameFilter,
				(this.matchAttributeFilterLessThan != null) ? this.matchAttributeFilterLessThan.Count : 0,
				(this.matchAttributeFilterGreaterThan != null) ? this.matchAttributeFilterGreaterThan.Count : 0
			});
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x0002BA00 File Offset: 0x00029C00
		public override bool IsValid()
		{
			int num = (this.matchAttributeFilterLessThan != null) ? this.matchAttributeFilterLessThan.Count : 0;
			num += ((this.matchAttributeFilterEqualTo != null) ? this.matchAttributeFilterEqualTo.Count : 0);
			num += ((this.matchAttributeFilterGreaterThan != null) ? this.matchAttributeFilterGreaterThan.Count : 0);
			return base.IsValid() && (this.pageSize >= 1 || this.pageSize <= 1000) && num <= 10;
		}
	}
}
