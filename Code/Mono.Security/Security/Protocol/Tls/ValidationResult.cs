using System;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x02000098 RID: 152
	public class ValidationResult
	{
		// Token: 0x06000577 RID: 1399 RVA: 0x0001FB50 File Offset: 0x0001DD50
		public ValidationResult(bool trusted, bool user_denied, int error_code)
		{
			this.trusted = trusted;
			this.user_denied = user_denied;
			this.error_code = error_code;
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x0001FB70 File Offset: 0x0001DD70
		public bool Trusted
		{
			get
			{
				return this.trusted;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x0001FB78 File Offset: 0x0001DD78
		public bool UserDenied
		{
			get
			{
				return this.user_denied;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x0001FB80 File Offset: 0x0001DD80
		public int ErrorCode
		{
			get
			{
				return this.error_code;
			}
		}

		// Token: 0x040002BF RID: 703
		private bool trusted;

		// Token: 0x040002C0 RID: 704
		private bool user_denied;

		// Token: 0x040002C1 RID: 705
		private int error_code;
	}
}
