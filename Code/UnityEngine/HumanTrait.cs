using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001C4 RID: 452
	public sealed class HumanTrait
	{
		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06001B34 RID: 6964
		public static extern int MuscleCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06001B35 RID: 6965
		public static extern string[] MuscleName { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06001B36 RID: 6966
		public static extern int BoneCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06001B37 RID: 6967
		public static extern string[] BoneName { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001B38 RID: 6968
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int MuscleFromBone(int i, int dofIndex);

		// Token: 0x06001B39 RID: 6969
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int BoneFromMuscle(int i);

		// Token: 0x06001B3A RID: 6970
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool RequiredBone(int i);

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06001B3B RID: 6971
		public static extern int RequiredBoneCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001B3C RID: 6972
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool HasCollider(Avatar avatar, int i);

		// Token: 0x06001B3D RID: 6973
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetMuscleDefaultMin(int i);

		// Token: 0x06001B3E RID: 6974
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetMuscleDefaultMax(int i);

		// Token: 0x06001B3F RID: 6975
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetParentBone(int i);
	}
}
