using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies a description for an assembly.</summary>
	// Token: 0x02000280 RID: 640
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class AssemblyTitleAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyTitleAttribute" /> class.</summary>
		/// <param name="title">The assembly title. </param>
		// Token: 0x06002100 RID: 8448 RVA: 0x00078700 File Offset: 0x00076900
		public AssemblyTitleAttribute(string title)
		{
			this.name = title;
		}

		/// <summary>Gets assembly title information.</summary>
		/// <returns>The assembly title.</returns>
		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06002101 RID: 8449 RVA: 0x00078710 File Offset: 0x00076910
		public string Title
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04000C20 RID: 3104
		private string name;
	}
}
