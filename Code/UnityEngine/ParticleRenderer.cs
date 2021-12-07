using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000116 RID: 278
	public sealed class ParticleRenderer : Renderer
	{
		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06001192 RID: 4498
		// (set) Token: 0x06001193 RID: 4499
		public extern ParticleRenderMode particleRenderMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06001194 RID: 4500
		// (set) Token: 0x06001195 RID: 4501
		public extern float lengthScale { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001196 RID: 4502
		// (set) Token: 0x06001197 RID: 4503
		public extern float velocityScale { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001198 RID: 4504
		// (set) Token: 0x06001199 RID: 4505
		public extern float cameraVelocityScale { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x0600119A RID: 4506
		// (set) Token: 0x0600119B RID: 4507
		public extern float maxParticleSize { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x0600119C RID: 4508
		// (set) Token: 0x0600119D RID: 4509
		public extern int uvAnimationXTile { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x0600119E RID: 4510
		// (set) Token: 0x0600119F RID: 4511
		public extern int uvAnimationYTile { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x060011A0 RID: 4512
		// (set) Token: 0x060011A1 RID: 4513
		public extern float uvAnimationCycles { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x060011A2 RID: 4514 RVA: 0x000143B8 File Offset: 0x000125B8
		// (set) Token: 0x060011A3 RID: 4515 RVA: 0x000143C0 File Offset: 0x000125C0
		[Obsolete("animatedTextureCount has been replaced by uvAnimationXTile and uvAnimationYTile.")]
		public int animatedTextureCount
		{
			get
			{
				return this.uvAnimationXTile;
			}
			set
			{
				this.uvAnimationXTile = value;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x060011A4 RID: 4516 RVA: 0x000143CC File Offset: 0x000125CC
		// (set) Token: 0x060011A5 RID: 4517 RVA: 0x000143D4 File Offset: 0x000125D4
		public float maxPartileSize
		{
			get
			{
				return this.maxParticleSize;
			}
			set
			{
				this.maxParticleSize = value;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x060011A6 RID: 4518
		// (set) Token: 0x060011A7 RID: 4519
		public extern Rect[] uvTiles { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x060011A8 RID: 4520 RVA: 0x000143E0 File Offset: 0x000125E0
		// (set) Token: 0x060011A9 RID: 4521 RVA: 0x000143E4 File Offset: 0x000125E4
		[Obsolete("This function has been removed.", true)]
		public AnimationCurve widthCurve
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x060011AA RID: 4522 RVA: 0x000143E8 File Offset: 0x000125E8
		// (set) Token: 0x060011AB RID: 4523 RVA: 0x000143EC File Offset: 0x000125EC
		[Obsolete("This function has been removed.", true)]
		public AnimationCurve heightCurve
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x060011AC RID: 4524 RVA: 0x000143F0 File Offset: 0x000125F0
		// (set) Token: 0x060011AD RID: 4525 RVA: 0x000143F4 File Offset: 0x000125F4
		[Obsolete("This function has been removed.", true)]
		public AnimationCurve rotationCurve
		{
			get
			{
				return null;
			}
			set
			{
			}
		}
	}
}
