using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates that the attributed assembly is a primary interop assembly.</summary>
	// Token: 0x020003B1 RID: 945
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = true)]
	public sealed class PrimaryInteropAssemblyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.PrimaryInteropAssemblyAttribute" /> class with the major and minor version numbers of the type library for which this assembly is the primary interop assembly.</summary>
		/// <param name="major">The major version of the type library for which this assembly is the primary interop assembly. </param>
		/// <param name="minor">The minor version of the type library for which this assembly is the primary interop assembly. </param>
		// Token: 0x06002B4F RID: 11087 RVA: 0x0009376C File Offset: 0x0009196C
		public PrimaryInteropAssemblyAttribute(int major, int minor)
		{
			this.major = major;
			this.minor = minor;
		}

		/// <summary>Gets the major version number of the type library for which this assembly is the primary interop assembly.</summary>
		/// <returns>The major version number of the type library for which this assembly is the primary interop assembly.</returns>
		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06002B50 RID: 11088 RVA: 0x00093784 File Offset: 0x00091984
		public int MajorVersion
		{
			get
			{
				return this.major;
			}
		}

		/// <summary>Gets the minor version number of the type library for which this assembly is the primary interop assembly.</summary>
		/// <returns>The minor version number of the type library for which this assembly is the primary interop assembly.</returns>
		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06002B51 RID: 11089 RVA: 0x0009378C File Offset: 0x0009198C
		public int MinorVersion
		{
			get
			{
				return this.minor;
			}
		}

		// Token: 0x0400116C RID: 4460
		private int major;

		// Token: 0x0400116D RID: 4461
		private int minor;
	}
}
