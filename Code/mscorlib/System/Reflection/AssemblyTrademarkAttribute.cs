using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Defines a trademark custom attribute for an assembly manifest.</summary>
	// Token: 0x02000281 RID: 641
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyTrademarkAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyTrademarkAttribute" /> class.</summary>
		/// <param name="trademark">The trademark information. </param>
		// Token: 0x06002102 RID: 8450 RVA: 0x00078718 File Offset: 0x00076918
		public AssemblyTrademarkAttribute(string trademark)
		{
			this.name = trademark;
		}

		/// <summary>Gets trademark information.</summary>
		/// <returns>A String containing trademark information.</returns>
		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06002103 RID: 8451 RVA: 0x00078728 File Offset: 0x00076928
		public string Trademark
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04000C21 RID: 3105
		private string name;
	}
}
