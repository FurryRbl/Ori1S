using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000085 RID: 133
	internal sealed class RuntimeUndo
	{
		// Token: 0x06000821 RID: 2081
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetTransformParent(Transform transform, Transform newParent, string name);

		// Token: 0x06000822 RID: 2082
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void RecordObject(Object objectToUndo, string name);

		// Token: 0x06000823 RID: 2083
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void RecordObjects(Object[] objectsToUndo, string name);
	}
}
