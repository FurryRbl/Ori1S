using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000042 RID: 66
	public sealed class GUITexture : GUIElement
	{
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00003F0C File Offset: 0x0000210C
		// (set) Token: 0x06000344 RID: 836 RVA: 0x00003F24 File Offset: 0x00002124
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

		// Token: 0x06000345 RID: 837
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_color(out Color value);

		// Token: 0x06000346 RID: 838
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_color(ref Color value);

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000347 RID: 839
		// (set) Token: 0x06000348 RID: 840
		public extern Texture texture { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000349 RID: 841 RVA: 0x00003F30 File Offset: 0x00002130
		// (set) Token: 0x0600034A RID: 842 RVA: 0x00003F48 File Offset: 0x00002148
		public Rect pixelInset
		{
			get
			{
				Rect result;
				this.INTERNAL_get_pixelInset(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_pixelInset(ref value);
			}
		}

		// Token: 0x0600034B RID: 843
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_pixelInset(out Rect value);

		// Token: 0x0600034C RID: 844
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_pixelInset(ref Rect value);

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600034D RID: 845
		// (set) Token: 0x0600034E RID: 846
		public extern RectOffset border { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
