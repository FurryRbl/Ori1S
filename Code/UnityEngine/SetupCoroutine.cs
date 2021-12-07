using System;
using System.Reflection;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000281 RID: 641
	[RequiredByNativeCode]
	internal class SetupCoroutine
	{
		// Token: 0x0600259B RID: 9627 RVA: 0x00034344 File Offset: 0x00032544
		[RequiredByNativeCode]
		public static object InvokeMember(object behaviour, string name, object variable)
		{
			object[] args = null;
			if (variable != null)
			{
				args = new object[]
				{
					variable
				};
			}
			return behaviour.GetType().InvokeMember(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, behaviour, args, null, null, null);
		}

		// Token: 0x0600259C RID: 9628 RVA: 0x0003437C File Offset: 0x0003257C
		public static object InvokeStatic(Type klass, string name, object variable)
		{
			object[] args = null;
			if (variable != null)
			{
				args = new object[]
				{
					variable
				};
			}
			return klass.InvokeMember(name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, null, args, null, null, null);
		}
	}
}
