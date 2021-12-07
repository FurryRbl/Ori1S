using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace UnityEngine.Experimental.Networking
{
	// Token: 0x02000224 RID: 548
	[StructLayout(LayoutKind.Sequential)]
	public class DownloadHandler : IDisposable
	{
		// Token: 0x060021F1 RID: 8689 RVA: 0x0002A9E4 File Offset: 0x00028BE4
		internal DownloadHandler()
		{
		}

		// Token: 0x060021F2 RID: 8690
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void InternalCreateString();

		// Token: 0x060021F3 RID: 8691
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void InternalCreateScript();

		// Token: 0x060021F4 RID: 8692
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void InternalCreateTexture(bool readable);

		// Token: 0x060021F5 RID: 8693
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void InternalCreateWebStream(string url, uint crc);

		// Token: 0x060021F6 RID: 8694 RVA: 0x0002A9EC File Offset: 0x00028BEC
		internal void InternalCreateWebStream(string url, Hash128 hash, uint crc)
		{
			DownloadHandler.INTERNAL_CALL_InternalCreateWebStream(this, url, ref hash, crc);
		}

		// Token: 0x060021F7 RID: 8695
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_InternalCreateWebStream(DownloadHandler self, string url, ref Hash128 hash, uint crc);

		// Token: 0x060021F8 RID: 8696
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalDestroy();

		// Token: 0x060021F9 RID: 8697 RVA: 0x0002A9F8 File Offset: 0x00028BF8
		~DownloadHandler()
		{
			this.InternalDestroy();
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x0002AA34 File Offset: 0x00028C34
		public void Dispose()
		{
			this.InternalDestroy();
			GC.SuppressFinalize(this);
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x060021FB RID: 8699
		public extern bool isDone { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x060021FC RID: 8700 RVA: 0x0002AA44 File Offset: 0x00028C44
		public byte[] data
		{
			get
			{
				return this.GetData();
			}
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x060021FD RID: 8701 RVA: 0x0002AA4C File Offset: 0x00028C4C
		public string text
		{
			get
			{
				return this.GetText();
			}
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x0002AA54 File Offset: 0x00028C54
		protected virtual byte[] GetData()
		{
			return null;
		}

		// Token: 0x060021FF RID: 8703 RVA: 0x0002AA58 File Offset: 0x00028C58
		protected virtual string GetText()
		{
			byte[] data = this.GetData();
			if (data != null && data.Length > 0)
			{
				return Encoding.UTF8.GetString(data, 0, data.Length);
			}
			return string.Empty;
		}

		// Token: 0x06002200 RID: 8704 RVA: 0x0002AA90 File Offset: 0x00028C90
		protected virtual bool ReceiveData(byte[] data, int dataLength)
		{
			return true;
		}

		// Token: 0x06002201 RID: 8705 RVA: 0x0002AA94 File Offset: 0x00028C94
		protected virtual void ReceiveContentLength(int contentLength)
		{
		}

		// Token: 0x06002202 RID: 8706 RVA: 0x0002AA98 File Offset: 0x00028C98
		protected virtual void CompleteContent()
		{
		}

		// Token: 0x06002203 RID: 8707 RVA: 0x0002AA9C File Offset: 0x00028C9C
		protected virtual float GetProgress()
		{
			return 0.5f;
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x0002AAA4 File Offset: 0x00028CA4
		protected static T GetCheckedDownloader<T>(UnityWebRequest www) where T : DownloadHandler
		{
			if (www == null)
			{
				throw new NullReferenceException("Cannot get content from a null UnityWebRequest object");
			}
			if (!www.isDone)
			{
				throw new InvalidOperationException("Cannot get content from an unfinished UnityWebRequest object");
			}
			if (www.isError)
			{
				throw new InvalidOperationException(www.error);
			}
			return (T)((object)www.downloadHandler);
		}

		// Token: 0x040008E0 RID: 2272
		[NonSerialized]
		internal IntPtr m_Ptr;
	}
}
