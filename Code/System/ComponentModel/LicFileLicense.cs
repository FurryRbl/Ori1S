using System;

namespace System.ComponentModel
{
	// Token: 0x0200017B RID: 379
	internal class LicFileLicense : License
	{
		// Token: 0x06000D0B RID: 3339 RVA: 0x00020BA4 File Offset: 0x0001EDA4
		public LicFileLicense(string key)
		{
			this._key = key;
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x00020BB4 File Offset: 0x0001EDB4
		public override string LicenseKey
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x00020BBC File Offset: 0x0001EDBC
		public override void Dispose()
		{
		}

		// Token: 0x0400038B RID: 907
		private string _key;
	}
}
