using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Defines a company name custom attribute for an assembly manifest.</summary>
	// Token: 0x02000271 RID: 625
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class AssemblyCompanyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyCompanyAttribute" /> class.</summary>
		/// <param name="company">The company name information. </param>
		// Token: 0x060020BA RID: 8378 RVA: 0x00077C84 File Offset: 0x00075E84
		public AssemblyCompanyAttribute(string company)
		{
			this.name = company;
		}

		/// <summary>Gets company name information.</summary>
		/// <returns>A string containing the company name.</returns>
		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x060020BB RID: 8379 RVA: 0x00077C94 File Offset: 0x00075E94
		public string Company
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04000BFF RID: 3071
		private string name;
	}
}
