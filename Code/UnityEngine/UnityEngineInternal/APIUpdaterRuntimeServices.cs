using System;
using UnityEngine;

namespace UnityEngineInternal
{
	// Token: 0x02000339 RID: 825
	public sealed class APIUpdaterRuntimeServices
	{
		// Token: 0x0600284A RID: 10314 RVA: 0x0003A008 File Offset: 0x00038208
		[Obsolete("Method is not meant to be used at runtime. Please, replace this call with GameObject.AddComponent<T>()/GameObject.AddComponent(Type).", true)]
		public static Component AddComponent(GameObject go, string sourceInfo, string name)
		{
			throw new Exception();
		}
	}
}
