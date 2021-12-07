using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001B3 RID: 435
	public sealed class AnimatorUtility
	{
		// Token: 0x06001AFC RID: 6908
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void OptimizeTransformHierarchy(GameObject go, string[] exposedTransforms);

		// Token: 0x06001AFD RID: 6909
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DeoptimizeTransformHierarchy(GameObject go);
	}
}
