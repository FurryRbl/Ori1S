using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine.Experimental.Networking
{
	// Token: 0x02000223 RID: 547
	[StructLayout(LayoutKind.Sequential)]
	public sealed class UploadHandlerRaw : UploadHandler
	{
		// Token: 0x060021E8 RID: 8680 RVA: 0x0002A9B0 File Offset: 0x00028BB0
		public UploadHandlerRaw(byte[] data)
		{
			base.InternalCreateRaw(data);
		}

		// Token: 0x060021E9 RID: 8681
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string InternalGetContentType();

		// Token: 0x060021EA RID: 8682
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalSetContentType(string newContentType);

		// Token: 0x060021EB RID: 8683
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern byte[] InternalGetData();

		// Token: 0x060021EC RID: 8684
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float InternalGetProgress();

		// Token: 0x060021ED RID: 8685 RVA: 0x0002A9C0 File Offset: 0x00028BC0
		internal override string GetContentType()
		{
			return this.InternalGetContentType();
		}

		// Token: 0x060021EE RID: 8686 RVA: 0x0002A9C8 File Offset: 0x00028BC8
		internal override void SetContentType(string newContentType)
		{
			this.InternalSetContentType(newContentType);
		}

		// Token: 0x060021EF RID: 8687 RVA: 0x0002A9D4 File Offset: 0x00028BD4
		internal override byte[] GetData()
		{
			return this.InternalGetData();
		}

		// Token: 0x060021F0 RID: 8688 RVA: 0x0002A9DC File Offset: 0x00028BDC
		internal override float GetProgress()
		{
			return this.InternalGetProgress();
		}
	}
}
