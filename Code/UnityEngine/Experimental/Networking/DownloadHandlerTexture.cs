using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine.Experimental.Networking
{
	// Token: 0x02000227 RID: 551
	[StructLayout(LayoutKind.Sequential)]
	public sealed class DownloadHandlerTexture : DownloadHandler
	{
		// Token: 0x0600220E RID: 8718 RVA: 0x0002AB6C File Offset: 0x00028D6C
		public DownloadHandlerTexture()
		{
			base.InternalCreateTexture(true);
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x0002AB7C File Offset: 0x00028D7C
		public DownloadHandlerTexture(bool readable)
		{
			base.InternalCreateTexture(readable);
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x0002AB8C File Offset: 0x00028D8C
		protected override byte[] GetData()
		{
			return this.InternalGetData();
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06002211 RID: 8721 RVA: 0x0002AB94 File Offset: 0x00028D94
		public Texture2D texture
		{
			get
			{
				return this.InternalGetTexture();
			}
		}

		// Token: 0x06002212 RID: 8722
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Texture2D InternalGetTexture();

		// Token: 0x06002213 RID: 8723
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern byte[] InternalGetData();

		// Token: 0x06002214 RID: 8724 RVA: 0x0002AB9C File Offset: 0x00028D9C
		public static Texture2D GetContent(UnityWebRequest www)
		{
			return DownloadHandler.GetCheckedDownloader<DownloadHandlerTexture>(www).texture;
		}
	}
}
