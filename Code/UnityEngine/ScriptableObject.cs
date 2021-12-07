using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000015 RID: 21
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class ScriptableObject : Object
	{
		// Token: 0x06000071 RID: 113 RVA: 0x00002520 File Offset: 0x00000720
		public ScriptableObject()
		{
			ScriptableObject.Internal_CreateScriptableObject(this);
		}

		// Token: 0x06000072 RID: 114
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateScriptableObject([Writable] ScriptableObject self);

		// Token: 0x06000073 RID: 115 RVA: 0x00002530 File Offset: 0x00000730
		[Obsolete("Use EditorUtility.SetDirty instead")]
		public void SetDirty()
		{
			ScriptableObject.INTERNAL_CALL_SetDirty(this);
		}

		// Token: 0x06000074 RID: 116
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetDirty(ScriptableObject self);

		// Token: 0x06000075 RID: 117
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ScriptableObject CreateInstance(string className);

		// Token: 0x06000076 RID: 118 RVA: 0x00002538 File Offset: 0x00000738
		public static ScriptableObject CreateInstance(Type type)
		{
			return ScriptableObject.CreateInstanceFromType(type);
		}

		// Token: 0x06000077 RID: 119
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ScriptableObject CreateInstanceFromType(Type type);

		// Token: 0x06000078 RID: 120 RVA: 0x00002540 File Offset: 0x00000740
		public static T CreateInstance<T>() where T : ScriptableObject
		{
			return (T)((object)ScriptableObject.CreateInstance(typeof(T)));
		}
	}
}
