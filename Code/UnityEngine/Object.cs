using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x020000C5 RID: 197
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class Object
	{
		// Token: 0x06000BAE RID: 2990
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Object Internal_CloneSingle(Object data);

		// Token: 0x06000BAF RID: 2991 RVA: 0x0000F484 File Offset: 0x0000D684
		private static Object Internal_InstantiateSingle(Object data, Vector3 pos, Quaternion rot)
		{
			return Object.INTERNAL_CALL_Internal_InstantiateSingle(data, ref pos, ref rot);
		}

		// Token: 0x06000BB0 RID: 2992
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Object INTERNAL_CALL_Internal_InstantiateSingle(Object data, ref Vector3 pos, ref Quaternion rot);

		// Token: 0x06000BB1 RID: 2993
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Destroy(Object obj, [DefaultValue("0.0F")] float t);

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0000F490 File Offset: 0x0000D690
		[ExcludeFromDocs]
		public static void Destroy(Object obj)
		{
			float t = 0f;
			Object.Destroy(obj, t);
		}

		// Token: 0x06000BB3 RID: 2995
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DestroyImmediate(Object obj, [DefaultValue("false")] bool allowDestroyingAssets);

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0000F4AC File Offset: 0x0000D6AC
		[ExcludeFromDocs]
		public static void DestroyImmediate(Object obj)
		{
			bool allowDestroyingAssets = false;
			Object.DestroyImmediate(obj, allowDestroyingAssets);
		}

		// Token: 0x06000BB5 RID: 2997
		[TypeInferenceRule(TypeInferenceRules.ArrayOfTypeReferencedByFirstArgument)]
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object[] FindObjectsOfType(Type type);

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000BB6 RID: 2998
		// (set) Token: 0x06000BB7 RID: 2999
		public extern string name { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000BB8 RID: 3000
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DontDestroyOnLoad(Object target);

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000BB9 RID: 3001
		// (set) Token: 0x06000BBA RID: 3002
		public extern HideFlags hideFlags { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000BBB RID: 3003
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DestroyObject(Object obj, [DefaultValue("0.0F")] float t);

		// Token: 0x06000BBC RID: 3004 RVA: 0x0000F4C4 File Offset: 0x0000D6C4
		[ExcludeFromDocs]
		public static void DestroyObject(Object obj)
		{
			float t = 0f;
			Object.DestroyObject(obj, t);
		}

		// Token: 0x06000BBD RID: 3005
		[WrapperlessIcall]
		[Obsolete("use Object.FindObjectsOfType instead.")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object[] FindSceneObjectsOfType(Type type);

		// Token: 0x06000BBE RID: 3006
		[WrapperlessIcall]
		[Obsolete("use Resources.FindObjectsOfTypeAll instead.")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object[] FindObjectsOfTypeIncludingAssets(Type type);

		// Token: 0x06000BBF RID: 3007 RVA: 0x0000F4E0 File Offset: 0x0000D6E0
		[Obsolete("Please use Resources.FindObjectsOfTypeAll instead")]
		public static Object[] FindObjectsOfTypeAll(Type type)
		{
			return Resources.FindObjectsOfTypeAll(type);
		}

		// Token: 0x06000BC0 RID: 3008
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public override extern string ToString();

		// Token: 0x06000BC1 RID: 3009
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool DoesObjectWithInstanceIDExist(int instanceID);

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0000F4E8 File Offset: 0x0000D6E8
		public override bool Equals(object o)
		{
			return Object.CompareBaseObjects(this, o as Object);
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0000F4F8 File Offset: 0x0000D6F8
		public override int GetHashCode()
		{
			return this.GetInstanceID();
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0000F500 File Offset: 0x0000D700
		private static bool CompareBaseObjects(Object lhs, Object rhs)
		{
			bool flag = lhs == null;
			bool flag2 = rhs == null;
			if (flag2 && flag)
			{
				return true;
			}
			if (flag2)
			{
				return !Object.IsNativeObjectAlive(lhs);
			}
			if (flag)
			{
				return !Object.IsNativeObjectAlive(rhs);
			}
			return lhs.m_InstanceID == rhs.m_InstanceID;
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0000F554 File Offset: 0x0000D754
		private static bool IsNativeObjectAlive(Object o)
		{
			return o.GetCachedPtr() != IntPtr.Zero;
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0000F568 File Offset: 0x0000D768
		public int GetInstanceID()
		{
			return this.m_InstanceID;
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0000F570 File Offset: 0x0000D770
		private IntPtr GetCachedPtr()
		{
			return this.m_CachedPtr;
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0000F578 File Offset: 0x0000D778
		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object original, Vector3 position, Quaternion rotation)
		{
			Object.CheckNullArgument(original, "The thing you want to instantiate is null.");
			return Object.Internal_InstantiateSingle(original, position, rotation);
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0000F590 File Offset: 0x0000D790
		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object original)
		{
			Object.CheckNullArgument(original, "The thing you want to instantiate is null.");
			return Object.Internal_CloneSingle(original);
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x0000F5A4 File Offset: 0x0000D7A4
		public static T Instantiate<T>(T original) where T : Object
		{
			Object.CheckNullArgument(original, "The thing you want to instantiate is null.");
			return (T)((object)Object.Internal_CloneSingle(original));
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x0000F5D4 File Offset: 0x0000D7D4
		private static void CheckNullArgument(object arg, string message)
		{
			if (arg == null)
			{
				throw new ArgumentException(message);
			}
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x0000F5E4 File Offset: 0x0000D7E4
		public static T[] FindObjectsOfType<T>() where T : Object
		{
			return Resources.ConvertObjects<T>(Object.FindObjectsOfType(typeof(T)));
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0000F5FC File Offset: 0x0000D7FC
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public static Object FindObjectOfType(Type type)
		{
			Object[] array = Object.FindObjectsOfType(type);
			if (array.Length > 0)
			{
				return array[0];
			}
			return null;
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0000F620 File Offset: 0x0000D820
		public static T FindObjectOfType<T>() where T : Object
		{
			return (T)((object)Object.FindObjectOfType(typeof(T)));
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x0000F638 File Offset: 0x0000D838
		public static implicit operator bool(Object exists)
		{
			return !Object.CompareBaseObjects(exists, null);
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x0000F644 File Offset: 0x0000D844
		public static bool operator ==(Object x, Object y)
		{
			return Object.CompareBaseObjects(x, y);
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x0000F650 File Offset: 0x0000D850
		public static bool operator !=(Object x, Object y)
		{
			return !Object.CompareBaseObjects(x, y);
		}

		// Token: 0x04000263 RID: 611
		private int m_InstanceID;

		// Token: 0x04000264 RID: 612
		private IntPtr m_CachedPtr;
	}
}
