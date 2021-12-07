using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000D6 RID: 214
	public class Motion : Object
	{
		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000DF8 RID: 3576
		public extern float averageDuration { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000DF9 RID: 3577
		public extern float averageAngularSpeed { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000DFA RID: 3578 RVA: 0x00011E14 File Offset: 0x00010014
		public Vector3 averageSpeed
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_averageSpeed(out result);
				return result;
			}
		}

		// Token: 0x06000DFB RID: 3579
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_averageSpeed(out Vector3 value);

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000DFC RID: 3580
		public extern float apparentSpeed { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000DFD RID: 3581
		public extern bool isLooping { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000DFE RID: 3582
		public extern bool legacy { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000DFF RID: 3583
		public extern bool isHumanMotion { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000E00 RID: 3584
		[Obsolete("ValidateIfRetargetable is not supported anymore. Use isHumanMotion instead.", true)]
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool ValidateIfRetargetable(bool val);

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000E01 RID: 3585
		[Obsolete("isAnimatorMotion is not supported anymore. Use !legacy instead.", true)]
		public extern bool isAnimatorMotion { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }
	}
}
