using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001DD RID: 477
	public sealed class GUIText : GUIElement
	{
		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06001CD5 RID: 7381
		// (set) Token: 0x06001CD6 RID: 7382
		public extern string text { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06001CD7 RID: 7383
		// (set) Token: 0x06001CD8 RID: 7384
		public extern Material material { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001CD9 RID: 7385
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_GetPixelOffset(out Vector2 output);

		// Token: 0x06001CDA RID: 7386 RVA: 0x0001B3E8 File Offset: 0x000195E8
		private void Internal_SetPixelOffset(Vector2 p)
		{
			GUIText.INTERNAL_CALL_Internal_SetPixelOffset(this, ref p);
		}

		// Token: 0x06001CDB RID: 7387
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_SetPixelOffset(GUIText self, ref Vector2 p);

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06001CDC RID: 7388 RVA: 0x0001B3F4 File Offset: 0x000195F4
		// (set) Token: 0x06001CDD RID: 7389 RVA: 0x0001B40C File Offset: 0x0001960C
		public Vector2 pixelOffset
		{
			get
			{
				Vector2 result;
				this.Internal_GetPixelOffset(out result);
				return result;
			}
			set
			{
				this.Internal_SetPixelOffset(value);
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06001CDE RID: 7390
		// (set) Token: 0x06001CDF RID: 7391
		public extern Font font { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06001CE0 RID: 7392
		// (set) Token: 0x06001CE1 RID: 7393
		public extern TextAlignment alignment { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06001CE2 RID: 7394
		// (set) Token: 0x06001CE3 RID: 7395
		public extern TextAnchor anchor { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06001CE4 RID: 7396
		// (set) Token: 0x06001CE5 RID: 7397
		public extern float lineSpacing { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06001CE6 RID: 7398
		// (set) Token: 0x06001CE7 RID: 7399
		public extern float tabSize { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06001CE8 RID: 7400
		// (set) Token: 0x06001CE9 RID: 7401
		public extern int fontSize { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06001CEA RID: 7402
		// (set) Token: 0x06001CEB RID: 7403
		public extern FontStyle fontStyle { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06001CEC RID: 7404
		// (set) Token: 0x06001CED RID: 7405
		public extern bool richText { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06001CEE RID: 7406 RVA: 0x0001B418 File Offset: 0x00019618
		// (set) Token: 0x06001CEF RID: 7407 RVA: 0x0001B430 File Offset: 0x00019630
		public Color color
		{
			get
			{
				Color result;
				this.INTERNAL_get_color(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_color(ref value);
			}
		}

		// Token: 0x06001CF0 RID: 7408
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_color(out Color value);

		// Token: 0x06001CF1 RID: 7409
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_color(ref Color value);
	}
}
