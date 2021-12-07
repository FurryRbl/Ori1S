using System;

namespace UnityEngine
{
	// Token: 0x02000277 RID: 631
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public sealed class RequireComponent : Attribute
	{
		// Token: 0x0600255A RID: 9562 RVA: 0x00033674 File Offset: 0x00031874
		public RequireComponent(Type requiredComponent)
		{
			this.m_Type0 = requiredComponent;
		}

		// Token: 0x0600255B RID: 9563 RVA: 0x00033684 File Offset: 0x00031884
		public RequireComponent(Type requiredComponent, Type requiredComponent2)
		{
			this.m_Type0 = requiredComponent;
			this.m_Type1 = requiredComponent2;
		}

		// Token: 0x0600255C RID: 9564 RVA: 0x0003369C File Offset: 0x0003189C
		public RequireComponent(Type requiredComponent, Type requiredComponent2, Type requiredComponent3)
		{
			this.m_Type0 = requiredComponent;
			this.m_Type1 = requiredComponent2;
			this.m_Type2 = requiredComponent3;
		}

		// Token: 0x040009D3 RID: 2515
		public Type m_Type0;

		// Token: 0x040009D4 RID: 2516
		public Type m_Type1;

		// Token: 0x040009D5 RID: 2517
		public Type m_Type2;
	}
}
