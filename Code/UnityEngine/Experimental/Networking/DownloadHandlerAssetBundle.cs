using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine.Experimental.Networking
{
	// Token: 0x02000228 RID: 552
	[StructLayout(LayoutKind.Sequential)]
	public sealed class DownloadHandlerAssetBundle : DownloadHandler
	{
		// Token: 0x06002215 RID: 8725 RVA: 0x0002ABAC File Offset: 0x00028DAC
		public DownloadHandlerAssetBundle(string url, uint crc)
		{
			base.InternalCreateWebStream(url, crc);
		}

		// Token: 0x06002216 RID: 8726 RVA: 0x0002ABBC File Offset: 0x00028DBC
		public DownloadHandlerAssetBundle(string url, uint version, uint crc)
		{
			Hash128 hash = new Hash128(0U, 0U, 0U, version);
			base.InternalCreateWebStream(url, hash, crc);
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x0002ABE4 File Offset: 0x00028DE4
		public DownloadHandlerAssetBundle(string url, Hash128 hash, uint crc)
		{
			base.InternalCreateWebStream(url, hash, crc);
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x0002ABF8 File Offset: 0x00028DF8
		protected override byte[] GetData()
		{
			throw new NotSupportedException("Raw data access is not supported for asset bundles");
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x0002AC04 File Offset: 0x00028E04
		protected override string GetText()
		{
			throw new NotSupportedException("String access is not supported for asset bundles");
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x0600221A RID: 8730
		public extern AssetBundle assetBundle { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600221B RID: 8731 RVA: 0x0002AC10 File Offset: 0x00028E10
		public static AssetBundle GetContent(UnityWebRequest www)
		{
			return DownloadHandler.GetCheckedDownloader<DownloadHandlerAssetBundle>(www).assetBundle;
		}
	}
}
