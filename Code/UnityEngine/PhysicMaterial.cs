using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200013E RID: 318
	public sealed class PhysicMaterial : Object
	{
		// Token: 0x0600147E RID: 5246 RVA: 0x00016650 File Offset: 0x00014850
		public PhysicMaterial()
		{
			PhysicMaterial.Internal_CreateDynamicsMaterial(this, null);
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x00016660 File Offset: 0x00014860
		public PhysicMaterial(string name)
		{
			PhysicMaterial.Internal_CreateDynamicsMaterial(this, name);
		}

		// Token: 0x06001480 RID: 5248
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateDynamicsMaterial([Writable] PhysicMaterial mat, string name);

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06001481 RID: 5249
		// (set) Token: 0x06001482 RID: 5250
		public extern float dynamicFriction { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06001483 RID: 5251
		// (set) Token: 0x06001484 RID: 5252
		public extern float staticFriction { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001485 RID: 5253
		// (set) Token: 0x06001486 RID: 5254
		public extern float bounciness { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001487 RID: 5255 RVA: 0x00016670 File Offset: 0x00014870
		// (set) Token: 0x06001488 RID: 5256 RVA: 0x00016678 File Offset: 0x00014878
		[Obsolete("Use PhysicMaterial.bounciness instead", true)]
		public float bouncyness
		{
			get
			{
				return this.bounciness;
			}
			set
			{
				this.bounciness = value;
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001489 RID: 5257 RVA: 0x00016684 File Offset: 0x00014884
		// (set) Token: 0x0600148A RID: 5258 RVA: 0x0001668C File Offset: 0x0001488C
		[Obsolete("Anisotropic friction is no longer supported since Unity 5.0.", true)]
		public Vector3 frictionDirection2
		{
			get
			{
				return Vector3.zero;
			}
			set
			{
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x0600148B RID: 5259
		// (set) Token: 0x0600148C RID: 5260
		[Obsolete("Anisotropic friction is no longer supported since Unity 5.0.", true)]
		public extern float dynamicFriction2 { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x0600148D RID: 5261
		// (set) Token: 0x0600148E RID: 5262
		[Obsolete("Anisotropic friction is no longer supported since Unity 5.0.", true)]
		public extern float staticFriction2 { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x0600148F RID: 5263
		// (set) Token: 0x06001490 RID: 5264
		public extern PhysicMaterialCombine frictionCombine { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06001491 RID: 5265
		// (set) Token: 0x06001492 RID: 5266
		public extern PhysicMaterialCombine bounceCombine { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06001493 RID: 5267 RVA: 0x00016690 File Offset: 0x00014890
		// (set) Token: 0x06001494 RID: 5268 RVA: 0x00016698 File Offset: 0x00014898
		[Obsolete("Anisotropic friction is no longer supported since Unity 5.0.", true)]
		public Vector3 frictionDirection
		{
			get
			{
				return Vector3.zero;
			}
			set
			{
			}
		}
	}
}
