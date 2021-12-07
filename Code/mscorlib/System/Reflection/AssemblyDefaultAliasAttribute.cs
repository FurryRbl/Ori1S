using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Defines a friendly default alias for an assembly manifest.</summary>
	// Token: 0x02000274 RID: 628
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyDefaultAliasAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyDefaultAliasAttribute" /> class.</summary>
		/// <param name="defaultAlias">The assembly default alias information. </param>
		// Token: 0x060020C0 RID: 8384 RVA: 0x00077CCC File Offset: 0x00075ECC
		public AssemblyDefaultAliasAttribute(string defaultAlias)
		{
			this.name = defaultAlias;
		}

		/// <summary>Gets default alias information.</summary>
		/// <returns>A string containing the default alias information.</returns>
		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x060020C1 RID: 8385 RVA: 0x00077CDC File Offset: 0x00075EDC
		public string DefaultAlias
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04000C02 RID: 3074
		private string name;
	}
}
