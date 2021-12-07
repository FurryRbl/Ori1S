using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Defines a product name custom attribute for an assembly manifest.</summary>
	// Token: 0x0200027F RID: 639
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class AssemblyProductAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyProductAttribute" /> class.</summary>
		/// <param name="product">The product name information. </param>
		// Token: 0x060020FE RID: 8446 RVA: 0x000786E8 File Offset: 0x000768E8
		public AssemblyProductAttribute(string product)
		{
			this.name = product;
		}

		/// <summary>Gets product name information.</summary>
		/// <returns>A string containing the product name.</returns>
		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x060020FF RID: 8447 RVA: 0x000786F8 File Offset: 0x000768F8
		public string Product
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04000C1F RID: 3103
		private string name;
	}
}
