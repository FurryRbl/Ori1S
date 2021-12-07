using System;

namespace JetBrains.Annotations
{
	// Token: 0x020002CF RID: 719
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	public sealed class ContractAnnotationAttribute : Attribute
	{
		// Token: 0x060025DA RID: 9690 RVA: 0x00034808 File Offset: 0x00032A08
		public ContractAnnotationAttribute([NotNull] string contract) : this(contract, false)
		{
		}

		// Token: 0x060025DB RID: 9691 RVA: 0x00034814 File Offset: 0x00032A14
		public ContractAnnotationAttribute([NotNull] string contract, bool forceFullStates)
		{
			this.Contract = contract;
			this.ForceFullStates = forceFullStates;
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x060025DC RID: 9692 RVA: 0x0003482C File Offset: 0x00032A2C
		// (set) Token: 0x060025DD RID: 9693 RVA: 0x00034834 File Offset: 0x00032A34
		public string Contract { get; private set; }

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x060025DE RID: 9694 RVA: 0x00034840 File Offset: 0x00032A40
		// (set) Token: 0x060025DF RID: 9695 RVA: 0x00034848 File Offset: 0x00032A48
		public bool ForceFullStates { get; private set; }
	}
}
