using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000040 RID: 64
	[UsedByNativeCode]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class RectOffset
	{
		// Token: 0x06000326 RID: 806 RVA: 0x00003D74 File Offset: 0x00001F74
		public RectOffset()
		{
			this.Init();
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00003D84 File Offset: 0x00001F84
		internal RectOffset(GUIStyle sourceStyle, IntPtr source)
		{
			this.m_SourceStyle = sourceStyle;
			this.m_Ptr = source;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00003D9C File Offset: 0x00001F9C
		public RectOffset(int left, int right, int top, int bottom)
		{
			this.Init();
			this.left = left;
			this.right = right;
			this.top = top;
			this.bottom = bottom;
		}

		// Token: 0x06000329 RID: 809
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Init();

		// Token: 0x0600032A RID: 810
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Cleanup();

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600032B RID: 811
		// (set) Token: 0x0600032C RID: 812
		public extern int left { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600032D RID: 813
		// (set) Token: 0x0600032E RID: 814
		public extern int right { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600032F RID: 815
		// (set) Token: 0x06000330 RID: 816
		public extern int top { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000331 RID: 817
		// (set) Token: 0x06000332 RID: 818
		public extern int bottom { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000333 RID: 819
		public extern int horizontal { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000334 RID: 820
		public extern int vertical { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000335 RID: 821 RVA: 0x00003DD4 File Offset: 0x00001FD4
		public Rect Add(Rect rect)
		{
			Rect result;
			RectOffset.INTERNAL_CALL_Add(this, ref rect, out result);
			return result;
		}

		// Token: 0x06000336 RID: 822
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Add(RectOffset self, ref Rect rect, out Rect value);

		// Token: 0x06000337 RID: 823 RVA: 0x00003DEC File Offset: 0x00001FEC
		public Rect Remove(Rect rect)
		{
			Rect result;
			RectOffset.INTERNAL_CALL_Remove(this, ref rect, out result);
			return result;
		}

		// Token: 0x06000338 RID: 824
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Remove(RectOffset self, ref Rect rect, out Rect value);

		// Token: 0x06000339 RID: 825 RVA: 0x00003E04 File Offset: 0x00002004
		~RectOffset()
		{
			if (this.m_SourceStyle == null)
			{
				this.Cleanup();
			}
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00003E4C File Offset: 0x0000204C
		public override string ToString()
		{
			return UnityString.Format("RectOffset (l:{0} r:{1} t:{2} b:{3})", new object[]
			{
				this.left,
				this.right,
				this.top,
				this.bottom
			});
		}

		// Token: 0x040000AE RID: 174
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x040000AF RID: 175
		private readonly GUIStyle m_SourceStyle;
	}
}
