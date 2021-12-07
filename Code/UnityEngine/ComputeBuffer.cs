using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace UnityEngine
{
	// Token: 0x020000B3 RID: 179
	public sealed class ComputeBuffer : IDisposable
	{
		// Token: 0x06000AB6 RID: 2742 RVA: 0x0000E9F8 File Offset: 0x0000CBF8
		public ComputeBuffer(int count, int stride) : this(count, stride, ComputeBufferType.Default)
		{
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0000EA04 File Offset: 0x0000CC04
		public ComputeBuffer(int count, int stride, ComputeBufferType type)
		{
			this.m_Ptr = IntPtr.Zero;
			ComputeBuffer.InitBuffer(this, count, stride, type);
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0000EA20 File Offset: 0x0000CC20
		~ComputeBuffer()
		{
			this.Dispose(false);
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0000EA5C File Offset: 0x0000CC5C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0000EA6C File Offset: 0x0000CC6C
		private void Dispose(bool disposing)
		{
			ComputeBuffer.DestroyBuffer(this);
			this.m_Ptr = IntPtr.Zero;
		}

		// Token: 0x06000ABB RID: 2747
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InitBuffer(ComputeBuffer buf, int count, int stride, ComputeBufferType type);

		// Token: 0x06000ABC RID: 2748
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DestroyBuffer(ComputeBuffer buf);

		// Token: 0x06000ABD RID: 2749 RVA: 0x0000EA80 File Offset: 0x0000CC80
		public void Release()
		{
			this.Dispose();
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000ABE RID: 2750
		public extern int count { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000ABF RID: 2751
		public extern int stride { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000AC0 RID: 2752 RVA: 0x0000EA88 File Offset: 0x0000CC88
		[SecuritySafeCritical]
		public void SetData(Array data)
		{
			this.InternalSetData(data, Marshal.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x06000AC1 RID: 2753
		[SecurityCritical]
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalSetData(Array data, int elemSize);

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0000EAAC File Offset: 0x0000CCAC
		[SecuritySafeCritical]
		public void GetData(Array data)
		{
			this.InternalGetData(data, Marshal.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x06000AC3 RID: 2755
		[WrapperlessIcall]
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalGetData(Array data, int elemSize);

		// Token: 0x06000AC4 RID: 2756
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CopyCount(ComputeBuffer src, ComputeBuffer dst, int dstOffset);

		// Token: 0x0400021C RID: 540
		internal IntPtr m_Ptr;
	}
}
