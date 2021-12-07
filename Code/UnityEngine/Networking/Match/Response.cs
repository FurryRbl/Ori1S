using System;
using System.Collections.Generic;

namespace UnityEngine.Networking.Match
{
	// Token: 0x02000230 RID: 560
	public abstract class Response : ResponseBase, IResponse
	{
		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06002253 RID: 8787 RVA: 0x0002B09C File Offset: 0x0002929C
		// (set) Token: 0x06002254 RID: 8788 RVA: 0x0002B0A4 File Offset: 0x000292A4
		public bool success { get; private set; }

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06002255 RID: 8789 RVA: 0x0002B0B0 File Offset: 0x000292B0
		// (set) Token: 0x06002256 RID: 8790 RVA: 0x0002B0B8 File Offset: 0x000292B8
		public string extendedInfo { get; private set; }

		// Token: 0x06002257 RID: 8791 RVA: 0x0002B0C4 File Offset: 0x000292C4
		public void SetSuccess()
		{
			this.success = true;
			this.extendedInfo = string.Empty;
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x0002B0D8 File Offset: 0x000292D8
		public void SetFailure(string info)
		{
			this.success = false;
			this.extendedInfo = info;
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x0002B0E8 File Offset: 0x000292E8
		public override string ToString()
		{
			return UnityString.Format("[{0}]-success:{1}-extendedInfo:{2}", new object[]
			{
				base.ToString(),
				this.success,
				this.extendedInfo
			});
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x0002B128 File Offset: 0x00029328
		public override void Parse(object obj)
		{
			IDictionary<string, object> dictionary = obj as IDictionary<string, object>;
			if (dictionary != null)
			{
				this.success = base.ParseJSONBool("success", obj, dictionary);
				this.extendedInfo = base.ParseJSONString("extendedInfo", obj, dictionary);
				if (!this.success)
				{
					throw new FormatException("FAILURE Returned from server: " + this.extendedInfo);
				}
			}
		}
	}
}
