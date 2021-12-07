using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine.Experimental.Networking
{
	// Token: 0x02000222 RID: 546
	[StructLayout(LayoutKind.Sequential)]
	public class UploadHandler : IDisposable
	{
		// Token: 0x060021DB RID: 8667 RVA: 0x0002A920 File Offset: 0x00028B20
		internal UploadHandler()
		{
		}

		// Token: 0x060021DC RID: 8668
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void InternalCreateRaw(byte[] data);

		// Token: 0x060021DD RID: 8669
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalDestroy();

		// Token: 0x060021DE RID: 8670 RVA: 0x0002A928 File Offset: 0x00028B28
		~UploadHandler()
		{
			this.InternalDestroy();
		}

		// Token: 0x060021DF RID: 8671 RVA: 0x0002A964 File Offset: 0x00028B64
		public void Dispose()
		{
			this.InternalDestroy();
			GC.SuppressFinalize(this);
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x060021E0 RID: 8672 RVA: 0x0002A974 File Offset: 0x00028B74
		public byte[] data
		{
			get
			{
				return this.GetData();
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x060021E1 RID: 8673 RVA: 0x0002A97C File Offset: 0x00028B7C
		// (set) Token: 0x060021E2 RID: 8674 RVA: 0x0002A984 File Offset: 0x00028B84
		public string contentType
		{
			get
			{
				return this.GetContentType();
			}
			set
			{
				this.SetContentType(value);
			}
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x060021E3 RID: 8675 RVA: 0x0002A990 File Offset: 0x00028B90
		public float progress
		{
			get
			{
				return this.GetProgress();
			}
		}

		// Token: 0x060021E4 RID: 8676 RVA: 0x0002A998 File Offset: 0x00028B98
		internal virtual byte[] GetData()
		{
			return null;
		}

		// Token: 0x060021E5 RID: 8677 RVA: 0x0002A99C File Offset: 0x00028B9C
		internal virtual string GetContentType()
		{
			return "text/plain";
		}

		// Token: 0x060021E6 RID: 8678 RVA: 0x0002A9A4 File Offset: 0x00028BA4
		internal virtual void SetContentType(string newContentType)
		{
		}

		// Token: 0x060021E7 RID: 8679 RVA: 0x0002A9A8 File Offset: 0x00028BA8
		internal virtual float GetProgress()
		{
			return 0.5f;
		}

		// Token: 0x040008DF RID: 2271
		[NonSerialized]
		internal IntPtr m_Ptr;
	}
}
