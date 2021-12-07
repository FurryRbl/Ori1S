using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200009F RID: 159
	public sealed class SpriteRenderer : Renderer
	{
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x0000CC78 File Offset: 0x0000AE78
		// (set) Token: 0x06000919 RID: 2329 RVA: 0x0000CC80 File Offset: 0x0000AE80
		public Sprite sprite
		{
			get
			{
				return this.GetSprite_INTERNAL();
			}
			set
			{
				this.SetSprite_INTERNAL(value);
			}
		}

		// Token: 0x0600091A RID: 2330
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Sprite GetSprite_INTERNAL();

		// Token: 0x0600091B RID: 2331
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetSprite_INTERNAL(Sprite sprite);

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x0000CC8C File Offset: 0x0000AE8C
		// (set) Token: 0x0600091D RID: 2333 RVA: 0x0000CCA4 File Offset: 0x0000AEA4
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

		// Token: 0x0600091E RID: 2334
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_color(out Color value);

		// Token: 0x0600091F RID: 2335
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_color(ref Color value);

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000920 RID: 2336
		// (set) Token: 0x06000921 RID: 2337
		public extern bool flipX { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000922 RID: 2338
		// (set) Token: 0x06000923 RID: 2339
		public extern bool flipY { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
