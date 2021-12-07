using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Provides a text description for an assembly.</summary>
	// Token: 0x02000276 RID: 630
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class AssemblyDescriptionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyDescriptionAttribute" /> class.</summary>
		/// <param name="description">The assembly description. </param>
		// Token: 0x060020C4 RID: 8388 RVA: 0x00077CFC File Offset: 0x00075EFC
		public AssemblyDescriptionAttribute(string description)
		{
			this.name = description;
		}

		/// <summary>Gets assembly description information.</summary>
		/// <returns>A string containing the assembly description.</returns>
		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x060020C5 RID: 8389 RVA: 0x00077D0C File Offset: 0x00075F0C
		public string Description
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04000C04 RID: 3076
		private string name;
	}
}
