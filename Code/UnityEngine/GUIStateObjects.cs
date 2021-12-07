using System;
using System.Collections.Generic;
using System.Security;

namespace UnityEngine
{
	// Token: 0x020002C9 RID: 713
	internal class GUIStateObjects
	{
		// Token: 0x060025CE RID: 9678 RVA: 0x00034738 File Offset: 0x00032938
		[SecuritySafeCritical]
		internal static object GetStateObject(Type t, int controlID)
		{
			object obj;
			if (!GUIStateObjects.s_StateCache.TryGetValue(controlID, out obj) || obj.GetType() != t)
			{
				obj = Activator.CreateInstance(t);
				GUIStateObjects.s_StateCache[controlID] = obj;
			}
			return obj;
		}

		// Token: 0x060025CF RID: 9679 RVA: 0x00034778 File Offset: 0x00032978
		internal static object QueryStateObject(Type t, int controlID)
		{
			object obj = GUIStateObjects.s_StateCache[controlID];
			if (t.IsInstanceOfType(obj))
			{
				return obj;
			}
			return null;
		}

		// Token: 0x04000B8D RID: 2957
		private static Dictionary<int, object> s_StateCache = new Dictionary<int, object>();
	}
}
