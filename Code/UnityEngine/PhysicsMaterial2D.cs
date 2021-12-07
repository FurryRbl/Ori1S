using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000162 RID: 354
	public sealed class PhysicsMaterial2D : Object
	{
		// Token: 0x060016DD RID: 5853 RVA: 0x00017FF0 File Offset: 0x000161F0
		public PhysicsMaterial2D()
		{
			PhysicsMaterial2D.Internal_Create(this, null);
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x00018000 File Offset: 0x00016200
		public PhysicsMaterial2D(string name)
		{
			PhysicsMaterial2D.Internal_Create(this, name);
		}

		// Token: 0x060016DF RID: 5855
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] PhysicsMaterial2D mat, string name);

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x060016E0 RID: 5856
		// (set) Token: 0x060016E1 RID: 5857
		public extern float bounciness { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x060016E2 RID: 5858
		// (set) Token: 0x060016E3 RID: 5859
		public extern float friction { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
