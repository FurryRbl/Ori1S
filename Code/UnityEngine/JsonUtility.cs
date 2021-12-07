using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200022C RID: 556
	public static class JsonUtility
	{
		// Token: 0x06002231 RID: 8753 RVA: 0x0002AC78 File Offset: 0x00028E78
		public static string ToJson(object obj)
		{
			return JsonUtility.ToJson(obj, false);
		}

		// Token: 0x06002232 RID: 8754
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string ToJson(object obj, bool prettyPrint);

		// Token: 0x06002233 RID: 8755 RVA: 0x0002AC84 File Offset: 0x00028E84
		public static T FromJson<T>(string json)
		{
			return (T)((object)JsonUtility.FromJson(json, typeof(T)));
		}

		// Token: 0x06002234 RID: 8756
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern object FromJson(string json, Type type);

		// Token: 0x06002235 RID: 8757
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void FromJsonOverwrite(string json, object objectToOverwrite);
	}
}
