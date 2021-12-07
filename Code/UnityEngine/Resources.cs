using System;
using System.Runtime.CompilerServices;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x02000084 RID: 132
	public sealed class Resources
	{
		// Token: 0x06000810 RID: 2064 RVA: 0x0000B630 File Offset: 0x00009830
		internal static T[] ConvertObjects<T>(Object[] rawObjects) where T : Object
		{
			if (rawObjects == null)
			{
				return null;
			}
			T[] array = new T[rawObjects.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (T)((object)rawObjects[i]);
			}
			return array;
		}

		// Token: 0x06000811 RID: 2065
		[WrapperlessIcall]
		[TypeInferenceRule(TypeInferenceRules.ArrayOfTypeReferencedByFirstArgument)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object[] FindObjectsOfTypeAll(Type type);

		// Token: 0x06000812 RID: 2066 RVA: 0x0000B674 File Offset: 0x00009874
		public static T[] FindObjectsOfTypeAll<T>() where T : Object
		{
			return Resources.ConvertObjects<T>(Resources.FindObjectsOfTypeAll(typeof(T)));
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0000B68C File Offset: 0x0000988C
		public static Object Load(string path)
		{
			return Resources.Load(path, typeof(Object));
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0000B6A0 File Offset: 0x000098A0
		public static T Load<T>(string path) where T : Object
		{
			return (T)((object)Resources.Load(path, typeof(T)));
		}

		// Token: 0x06000815 RID: 2069
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedBySecondArgument)]
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object Load(string path, Type systemTypeInstance);

		// Token: 0x06000816 RID: 2070 RVA: 0x0000B6B8 File Offset: 0x000098B8
		public static ResourceRequest LoadAsync(string path)
		{
			return Resources.LoadAsync(path, typeof(Object));
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0000B6CC File Offset: 0x000098CC
		public static ResourceRequest LoadAsync<T>(string path) where T : Object
		{
			return Resources.LoadAsync(path, typeof(T));
		}

		// Token: 0x06000818 RID: 2072
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ResourceRequest LoadAsync(string path, Type type);

		// Token: 0x06000819 RID: 2073
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object[] LoadAll(string path, Type systemTypeInstance);

		// Token: 0x0600081A RID: 2074 RVA: 0x0000B6E0 File Offset: 0x000098E0
		public static Object[] LoadAll(string path)
		{
			return Resources.LoadAll(path, typeof(Object));
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0000B6F4 File Offset: 0x000098F4
		public static T[] LoadAll<T>(string path) where T : Object
		{
			return Resources.ConvertObjects<T>(Resources.LoadAll(path, typeof(T)));
		}

		// Token: 0x0600081C RID: 2076
		[WrapperlessIcall]
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object GetBuiltinResource(Type type, string path);

		// Token: 0x0600081D RID: 2077 RVA: 0x0000B70C File Offset: 0x0000990C
		public static T GetBuiltinResource<T>(string path) where T : Object
		{
			return (T)((object)Resources.GetBuiltinResource(typeof(T), path));
		}

		// Token: 0x0600081E RID: 2078
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void UnloadAsset(Object assetToUnload);

		// Token: 0x0600081F RID: 2079
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern AsyncOperation UnloadUnusedAssets();
	}
}
