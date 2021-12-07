using System;

namespace System.Runtime.ConstrainedExecution
{
	/// <summary>Defines a contract for reliability between the author of some code, and the developers who have a dependency on that code.</summary>
	// Token: 0x0200034B RID: 843
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Interface, Inherited = false)]
	public sealed class ReliabilityContractAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.ConstrainedExecution.ReliabilityContractAttribute" /> class with the specified <see cref="T:System.Runtime.ConstrainedExecution.Consistency" /> guarantee and <see cref="T:System.Runtime.ConstrainedExecution.Cer" /> value.</summary>
		/// <param name="consistencyGuarantee">One of the <see cref="T:System.Runtime.ConstrainedExecution.Consistency" /> values. </param>
		/// <param name="cer">One of the <see cref="T:System.Runtime.ConstrainedExecution.Cer" /> values. </param>
		// Token: 0x060028B9 RID: 10425 RVA: 0x00091E94 File Offset: 0x00090094
		public ReliabilityContractAttribute(Consistency consistencyGuarantee, Cer cer)
		{
			this.consistency = consistencyGuarantee;
			this.cer = cer;
		}

		/// <summary>Gets the value that determines the behavior of a method, type, or assembly when called under a Constrained Execution Region (CER). </summary>
		/// <returns>One of the <see cref="T:System.Runtime.ConstrainedExecution.Cer" /> values.</returns>
		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x060028BA RID: 10426 RVA: 0x00091EAC File Offset: 0x000900AC
		public Cer Cer
		{
			get
			{
				return this.cer;
			}
		}

		/// <summary>Gets the value of the <see cref="T:System.Runtime.ConstrainedExecution.Consistency" /> reliability contract. </summary>
		/// <returns>One of the <see cref="T:System.Runtime.ConstrainedExecution.Consistency" /> values.</returns>
		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x060028BB RID: 10427 RVA: 0x00091EB4 File Offset: 0x000900B4
		public Consistency ConsistencyGuarantee
		{
			get
			{
				return this.consistency;
			}
		}

		// Token: 0x040010A6 RID: 4262
		private Consistency consistency;

		// Token: 0x040010A7 RID: 4263
		private Cer cer;
	}
}
