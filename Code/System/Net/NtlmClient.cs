using System;
using Mono.Http;

namespace System.Net
{
	// Token: 0x020003D8 RID: 984
	internal class NtlmClient : IAuthenticationModule
	{
		// Token: 0x060021A7 RID: 8615 RVA: 0x0006293C File Offset: 0x00060B3C
		public NtlmClient()
		{
			this.authObject = new NtlmClient();
		}

		// Token: 0x060021A8 RID: 8616 RVA: 0x00062950 File Offset: 0x00060B50
		public Authorization Authenticate(string challenge, WebRequest webRequest, ICredentials credentials)
		{
			if (this.authObject == null)
			{
				return null;
			}
			return this.authObject.Authenticate(challenge, webRequest, credentials);
		}

		// Token: 0x060021A9 RID: 8617 RVA: 0x00062970 File Offset: 0x00060B70
		public Authorization PreAuthenticate(WebRequest webRequest, ICredentials credentials)
		{
			return null;
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x060021AA RID: 8618 RVA: 0x00062974 File Offset: 0x00060B74
		public string AuthenticationType
		{
			get
			{
				return "NTLM";
			}
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x060021AB RID: 8619 RVA: 0x0006297C File Offset: 0x00060B7C
		public bool CanPreAuthenticate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040014E6 RID: 5350
		private IAuthenticationModule authObject;
	}
}
