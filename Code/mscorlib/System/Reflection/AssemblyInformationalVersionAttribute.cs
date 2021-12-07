using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Defines additional version information for an assembly manifest.</summary>
	// Token: 0x02000279 RID: 633
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class AssemblyInformationalVersionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyInformationalVersionAttribute" /> class.</summary>
		/// <param name="informationalVersion">The assembly version information. </param>
		// Token: 0x060020CD RID: 8397 RVA: 0x00077D7C File Offset: 0x00075F7C
		public AssemblyInformationalVersionAttribute(string informationalVersion)
		{
			this.name = informationalVersion;
		}

		/// <summary>Gets version information.</summary>
		/// <returns>A string containing the version information.</returns>
		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x060020CE RID: 8398 RVA: 0x00077D8C File Offset: 0x00075F8C
		public string InformationalVersion
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04000C07 RID: 3079
		private string name;
	}
}
