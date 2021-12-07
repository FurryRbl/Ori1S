using System;
using System.Text.RegularExpressions;

namespace System.Net
{
	// Token: 0x0200041C RID: 1052
	internal class WebPermissionInfo
	{
		// Token: 0x060025EF RID: 9711 RVA: 0x00076120 File Offset: 0x00074320
		public WebPermissionInfo(WebPermissionInfoType type, string info)
		{
			this._type = type;
			this._info = info;
		}

		// Token: 0x060025F0 RID: 9712 RVA: 0x00076138 File Offset: 0x00074338
		public WebPermissionInfo(System.Text.RegularExpressions.Regex regex)
		{
			this._type = WebPermissionInfoType.InfoRegex;
			this._info = regex;
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x060025F1 RID: 9713 RVA: 0x00076150 File Offset: 0x00074350
		public string Info
		{
			get
			{
				if (this._type == WebPermissionInfoType.InfoRegex)
				{
					return null;
				}
				return (string)this._info;
			}
		}

		// Token: 0x04001771 RID: 6001
		private WebPermissionInfoType _type;

		// Token: 0x04001772 RID: 6002
		private object _info;
	}
}
