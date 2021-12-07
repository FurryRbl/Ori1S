using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies which culture the assembly supports.</summary>
	// Token: 0x02000049 RID: 73
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyCultureAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyCultureAttribute" /> class with the culture supported by the assembly being attributed.</summary>
		/// <param name="culture">The culture supported by the attributed assembly. </param>
		// Token: 0x0600066C RID: 1644 RVA: 0x00014B48 File Offset: 0x00012D48
		public AssemblyCultureAttribute(string culture)
		{
			this.name = culture;
		}

		/// <summary>Gets the supported culture of the attributed assembly.</summary>
		/// <returns>A string containing the name of the supported culture.</returns>
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x00014B58 File Offset: 0x00012D58
		public string Culture
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x0400009E RID: 158
		private string name;
	}
}
