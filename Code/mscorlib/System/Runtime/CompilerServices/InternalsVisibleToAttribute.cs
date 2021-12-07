using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Specifies that types that are ordinarily visible only within the current assembly are visible to another assembly.</summary>
	// Token: 0x02000052 RID: 82
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
	public sealed class InternalsVisibleToAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.InternalsVisibleToAttribute" /> class with the name of the specified friend assembly. </summary>
		/// <param name="assemblyName">The name of a friend assembly.</param>
		// Token: 0x0600067C RID: 1660 RVA: 0x00014C00 File Offset: 0x00012E00
		public InternalsVisibleToAttribute(string assemblyName)
		{
			this.assemblyName = assemblyName;
		}

		/// <summary>Gets the name of the friend assembly to which all types and type members that are marked with the internal keyword are to be made visible. </summary>
		/// <returns>A string that represents the name of the friend assembly.</returns>
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x00014C18 File Offset: 0x00012E18
		public string AssemblyName
		{
			get
			{
				return this.assemblyName;
			}
		}

		/// <summary>This property is not implemented.</summary>
		/// <returns>This property does not return a value.</returns>
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x00014C20 File Offset: 0x00012E20
		// (set) Token: 0x0600067F RID: 1663 RVA: 0x00014C28 File Offset: 0x00012E28
		public bool AllInternalsVisible
		{
			get
			{
				return this.all_visible;
			}
			set
			{
				this.all_visible = value;
			}
		}

		// Token: 0x040000A5 RID: 165
		private string assemblyName;

		// Token: 0x040000A6 RID: 166
		private bool all_visible = true;
	}
}
