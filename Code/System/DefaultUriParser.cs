using System;

namespace System
{
	// Token: 0x0200020B RID: 523
	internal class DefaultUriParser : System.UriParser
	{
		// Token: 0x0600118B RID: 4491 RVA: 0x0002EB7C File Offset: 0x0002CD7C
		public DefaultUriParser()
		{
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x0002EB84 File Offset: 0x0002CD84
		public DefaultUriParser(string scheme)
		{
			this.scheme_name = scheme;
		}
	}
}
