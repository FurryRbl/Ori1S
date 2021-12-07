using System;
using System.Runtime.Remoting.Messaging;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x0200007C RID: 124
	internal class MethodCallHeaderHandler
	{
		// Token: 0x06000596 RID: 1430 RVA: 0x0000E0F8 File Offset: 0x0000C2F8
		public MethodCallHeaderHandler(string uri)
		{
			this._uri = uri;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0000E108 File Offset: 0x0000C308
		public object HandleHeaders(Header[] headers)
		{
			return this._uri;
		}

		// Token: 0x0400042B RID: 1067
		private string _uri;
	}
}
