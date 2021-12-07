using System;

namespace UnityEngine
{
	// Token: 0x020002F4 RID: 756
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public abstract class PropertyAttribute : Attribute
	{
		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x060026CF RID: 9935 RVA: 0x000363EC File Offset: 0x000345EC
		// (set) Token: 0x060026D0 RID: 9936 RVA: 0x000363F4 File Offset: 0x000345F4
		public int order { get; set; }
	}
}
