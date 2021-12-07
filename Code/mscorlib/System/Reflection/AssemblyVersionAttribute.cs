using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies the version of the assembly being attributed.</summary>
	// Token: 0x0200004A RID: 74
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class AssemblyVersionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the AssemblyVersionAttribute class with the version number of the assembly being attributed.</summary>
		/// <param name="version">The version number of the attributed assembly. </param>
		// Token: 0x0600066E RID: 1646 RVA: 0x00014B60 File Offset: 0x00012D60
		public AssemblyVersionAttribute(string version)
		{
			this.name = version;
		}

		/// <summary>Gets the version number of the attributed assembly.</summary>
		/// <returns>A string containing the assembly version number.</returns>
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x00014B70 File Offset: 0x00012D70
		public string Version
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x0400009F RID: 159
		private string name;
	}
}
