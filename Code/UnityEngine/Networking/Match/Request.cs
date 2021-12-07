using System;
using UnityEngine.Networking.Types;

namespace UnityEngine.Networking.Match
{
	// Token: 0x0200022D RID: 557
	public abstract class Request
	{
		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06002237 RID: 8759 RVA: 0x0002ACAC File Offset: 0x00028EAC
		// (set) Token: 0x06002238 RID: 8760 RVA: 0x0002ACB4 File Offset: 0x00028EB4
		public SourceID sourceId { get; set; }

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06002239 RID: 8761 RVA: 0x0002ACC0 File Offset: 0x00028EC0
		// (set) Token: 0x0600223A RID: 8762 RVA: 0x0002ACC8 File Offset: 0x00028EC8
		public string projectId { get; set; }

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x0600223B RID: 8763 RVA: 0x0002ACD4 File Offset: 0x00028ED4
		// (set) Token: 0x0600223C RID: 8764 RVA: 0x0002ACDC File Offset: 0x00028EDC
		public AppID appId { get; set; }

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x0600223D RID: 8765 RVA: 0x0002ACE8 File Offset: 0x00028EE8
		// (set) Token: 0x0600223E RID: 8766 RVA: 0x0002ACF0 File Offset: 0x00028EF0
		public string accessTokenString { get; set; }

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x0600223F RID: 8767 RVA: 0x0002ACFC File Offset: 0x00028EFC
		// (set) Token: 0x06002240 RID: 8768 RVA: 0x0002AD04 File Offset: 0x00028F04
		public int domain { get; set; }

		// Token: 0x06002241 RID: 8769 RVA: 0x0002AD10 File Offset: 0x00028F10
		public virtual bool IsValid()
		{
			return this.appId != AppID.Invalid && this.sourceId != SourceID.Invalid;
		}

		// Token: 0x06002242 RID: 8770 RVA: 0x0002AD30 File Offset: 0x00028F30
		public override string ToString()
		{
			return UnityString.Format("[{0}]-SourceID:0x{1},AppID:0x{2},domain:{3}", new object[]
			{
				base.ToString(),
				this.sourceId.ToString("X"),
				this.appId.ToString("X"),
				this.domain
			});
		}

		// Token: 0x040008E6 RID: 2278
		public int version = 2;
	}
}
