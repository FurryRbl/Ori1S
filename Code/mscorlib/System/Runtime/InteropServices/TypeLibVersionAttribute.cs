using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Specifies the version number of an exported type library.</summary>
	// Token: 0x020003CD RID: 973
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	public sealed class TypeLibVersionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.TypeLibVersionAttribute" /> class with the major and minor version numbers of the type library.</summary>
		/// <param name="major">The major version number of the type library. </param>
		/// <param name="minor">The minor version number of the type library. </param>
		// Token: 0x06002B8F RID: 11151 RVA: 0x00093CBC File Offset: 0x00091EBC
		public TypeLibVersionAttribute(int major, int minor)
		{
			this.major = major;
			this.minor = minor;
		}

		/// <summary>Gets the major version number of the type library.</summary>
		/// <returns>The major version number of the type library.</returns>
		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06002B90 RID: 11152 RVA: 0x00093CD4 File Offset: 0x00091ED4
		public int MajorVersion
		{
			get
			{
				return this.major;
			}
		}

		/// <summary>Gets the minor version number of the type library.</summary>
		/// <returns>The minor version number of the type library.</returns>
		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06002B91 RID: 11153 RVA: 0x00093CDC File Offset: 0x00091EDC
		public int MinorVersion
		{
			get
			{
				return this.minor;
			}
		}

		// Token: 0x04001215 RID: 4629
		private int major;

		// Token: 0x04001216 RID: 4630
		private int minor;
	}
}
