using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Instructs a compiler to use a specific version number for the Win32 file version resource. The Win32 file version is not required to be the same as the assembly's version number.</summary>
	// Token: 0x02000277 RID: 631
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyFileVersionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyFileVersionAttribute" /> class, specifying the file version.</summary>
		/// <param name="version">The file version. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="version" /> is null. </exception>
		// Token: 0x060020C6 RID: 8390 RVA: 0x00077D14 File Offset: 0x00075F14
		public AssemblyFileVersionAttribute(string version)
		{
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			this.name = version;
		}

		/// <summary>Gets the Win32 file version resource name.</summary>
		/// <returns>A string containing the file version resource name.</returns>
		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x060020C7 RID: 8391 RVA: 0x00077D34 File Offset: 0x00075F34
		public string Version
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04000C05 RID: 3077
		private string name;
	}
}
