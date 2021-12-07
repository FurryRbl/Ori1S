using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies the build configuration, such as retail or debug, for an assembly.</summary>
	// Token: 0x02000272 RID: 626
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class AssemblyConfigurationAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyConfigurationAttribute" /> class.</summary>
		/// <param name="configuration">The assembly configuration. </param>
		// Token: 0x060020BC RID: 8380 RVA: 0x00077C9C File Offset: 0x00075E9C
		public AssemblyConfigurationAttribute(string configuration)
		{
			this.name = configuration;
		}

		/// <summary>Gets assembly configuration information.</summary>
		/// <returns>A string containing the assembly configuration information.</returns>
		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x060020BD RID: 8381 RVA: 0x00077CAC File Offset: 0x00075EAC
		public string Configuration
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04000C00 RID: 3072
		private string name;
	}
}
