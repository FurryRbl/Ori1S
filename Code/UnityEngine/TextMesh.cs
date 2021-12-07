using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001DE RID: 478
	public sealed class TextMesh : Component
	{
		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06001CF3 RID: 7411
		// (set) Token: 0x06001CF4 RID: 7412
		public extern string text { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06001CF5 RID: 7413
		// (set) Token: 0x06001CF6 RID: 7414
		public extern Font font { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06001CF7 RID: 7415
		// (set) Token: 0x06001CF8 RID: 7416
		public extern int fontSize { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06001CF9 RID: 7417
		// (set) Token: 0x06001CFA RID: 7418
		public extern FontStyle fontStyle { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06001CFB RID: 7419
		// (set) Token: 0x06001CFC RID: 7420
		public extern float offsetZ { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06001CFD RID: 7421
		// (set) Token: 0x06001CFE RID: 7422
		public extern TextAlignment alignment { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06001CFF RID: 7423
		// (set) Token: 0x06001D00 RID: 7424
		public extern TextAnchor anchor { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06001D01 RID: 7425
		// (set) Token: 0x06001D02 RID: 7426
		public extern float characterSize { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06001D03 RID: 7427
		// (set) Token: 0x06001D04 RID: 7428
		public extern float lineSpacing { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06001D05 RID: 7429
		// (set) Token: 0x06001D06 RID: 7430
		public extern float tabSize { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06001D07 RID: 7431
		// (set) Token: 0x06001D08 RID: 7432
		public extern bool richText { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06001D09 RID: 7433 RVA: 0x0001B444 File Offset: 0x00019644
		// (set) Token: 0x06001D0A RID: 7434 RVA: 0x0001B45C File Offset: 0x0001965C
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

		// Token: 0x06001D0B RID: 7435
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_color(out Color value);

		// Token: 0x06001D0C RID: 7436
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_color(ref Color value);
	}
}
