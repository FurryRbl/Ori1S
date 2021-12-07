using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates the COM alias for a parameter or field type.</summary>
	// Token: 0x02000376 RID: 886
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	public sealed class ComAliasNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ComAliasNameAttribute" /> class with the alias for the attributed field or parameter.</summary>
		/// <param name="alias">The alias for the field or parameter as found in the type library when it was imported. </param>
		// Token: 0x06002A20 RID: 10784 RVA: 0x000921E8 File Offset: 0x000903E8
		public ComAliasNameAttribute(string alias)
		{
			this.val = alias;
		}

		/// <summary>Gets the alias for the field or parameter as found in the type library when it was imported.</summary>
		/// <returns>The alias for the field or parameter as found in the type library when it was imported.</returns>
		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06002A21 RID: 10785 RVA: 0x000921F8 File Offset: 0x000903F8
		public string Value
		{
			get
			{
				return this.val;
			}
		}

		// Token: 0x040010D6 RID: 4310
		private string val;
	}
}
