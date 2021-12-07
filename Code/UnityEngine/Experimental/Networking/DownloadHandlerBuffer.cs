using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine.Experimental.Networking
{
	// Token: 0x02000225 RID: 549
	[StructLayout(LayoutKind.Sequential)]
	public sealed class DownloadHandlerBuffer : DownloadHandler
	{
		// Token: 0x06002205 RID: 8709 RVA: 0x0002AAFC File Offset: 0x00028CFC
		public DownloadHandlerBuffer()
		{
			base.InternalCreateString();
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x0002AB0C File Offset: 0x00028D0C
		protected override byte[] GetData()
		{
			return this.InternalGetData();
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x0002AB14 File Offset: 0x00028D14
		protected override string GetText()
		{
			return this.InternalGetText();
		}

		// Token: 0x06002208 RID: 8712
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern byte[] InternalGetData();

		// Token: 0x06002209 RID: 8713
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string InternalGetText();

		// Token: 0x0600220A RID: 8714 RVA: 0x0002AB1C File Offset: 0x00028D1C
		public static string GetContent(UnityWebRequest www)
		{
			return DownloadHandler.GetCheckedDownloader<DownloadHandlerBuffer>(www).text;
		}
	}
}
