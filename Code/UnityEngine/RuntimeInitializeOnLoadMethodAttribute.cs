using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020002FF RID: 767
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class RuntimeInitializeOnLoadMethodAttribute : PreserveAttribute
	{
		// Token: 0x060026DE RID: 9950 RVA: 0x0003656C File Offset: 0x0003476C
		public RuntimeInitializeOnLoadMethodAttribute()
		{
			this.loadType = RuntimeInitializeLoadType.AfterSceneLoad;
		}

		// Token: 0x060026DF RID: 9951 RVA: 0x0003657C File Offset: 0x0003477C
		public RuntimeInitializeOnLoadMethodAttribute(RuntimeInitializeLoadType loadType)
		{
			this.loadType = loadType;
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x060026E0 RID: 9952 RVA: 0x0003658C File Offset: 0x0003478C
		// (set) Token: 0x060026E1 RID: 9953 RVA: 0x00036594 File Offset: 0x00034794
		public RuntimeInitializeLoadType loadType { get; private set; }
	}
}
