using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Provides a hint to the common language runtime (CLR) indicating how likely a dependency is to be loaded. This class is used in a dependent assembly to indicate what hint should be used when the parent does not specify the <see cref="T:System.Runtime.CompilerServices.DependencyAttribute" /> attribute.  This class cannot be inherited. </summary>
	// Token: 0x0200032B RID: 811
	[AttributeUsage(AttributeTargets.Assembly)]
	[Serializable]
	public sealed class DefaultDependencyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.DefaultDependencyAttribute" /> class with the specified <see cref="T:System.Runtime.CompilerServices.LoadHint" /> binding. </summary>
		/// <param name="loadHintArgument">One of the <see cref="T:System.Runtime.CompilerServices.LoadHint" /> values that indicates the default binding preference.</param>
		// Token: 0x060028A2 RID: 10402 RVA: 0x00091D88 File Offset: 0x0008FF88
		public DefaultDependencyAttribute(LoadHint loadHintArgument)
		{
			this.hint = loadHintArgument;
		}

		/// <summary>Gets the <see cref="T:System.Runtime.CompilerServices.LoadHint" /> value that indicates when an assembly loads a dependency.</summary>
		/// <returns>One of the <see cref="T:System.Runtime.CompilerServices.LoadHint" /> values.</returns>
		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x060028A3 RID: 10403 RVA: 0x00091D98 File Offset: 0x0008FF98
		public LoadHint LoadHint
		{
			get
			{
				return this.hint;
			}
		}

		// Token: 0x04001088 RID: 4232
		private LoadHint hint;
	}
}
