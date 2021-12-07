using System;

namespace System
{
	// Token: 0x020004D7 RID: 1239
	internal interface IUriData
	{
		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x06002BF8 RID: 11256
		string AbsolutePath { get; }

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06002BF9 RID: 11257
		string AbsoluteUri { get; }

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x06002BFA RID: 11258
		string AbsoluteUri_SafeUnescaped { get; }

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x06002BFB RID: 11259
		string Authority { get; }

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x06002BFC RID: 11260
		string Fragment { get; }

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x06002BFD RID: 11261
		string Host { get; }

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x06002BFE RID: 11262
		string PathAndQuery { get; }

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x06002BFF RID: 11263
		string StrongPort { get; }

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x06002C00 RID: 11264
		string Query { get; }

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x06002C01 RID: 11265
		string UserInfo { get; }
	}
}
