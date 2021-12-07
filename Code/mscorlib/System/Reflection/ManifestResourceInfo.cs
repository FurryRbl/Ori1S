using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Provides access to manifest resources, which are XML files that describe application dependencies.  </summary>
	// Token: 0x02000296 RID: 662
	[ComVisible(true)]
	public class ManifestResourceInfo
	{
		// Token: 0x0600219E RID: 8606 RVA: 0x0007A644 File Offset: 0x00078844
		internal ManifestResourceInfo()
		{
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x0007A64C File Offset: 0x0007884C
		internal ManifestResourceInfo(Assembly assembly, string filename, ResourceLocation location)
		{
			this._assembly = assembly;
			this._filename = filename;
			this._location = location;
		}

		/// <summary>Indicates the name of the file containing the manifest resource, if not the same as the manifest file. This property is read-only.</summary>
		/// <returns>A String that is the manifest resource's file name.</returns>
		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x060021A0 RID: 8608 RVA: 0x0007A66C File Offset: 0x0007886C
		public virtual string FileName
		{
			get
			{
				return this._filename;
			}
		}

		/// <summary>Indicates the containing assembly. This property is read-only.</summary>
		/// <returns>An <see cref="T:System.Reflection.Assembly" /> object representing the manifest resource's containing assembly.</returns>
		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x060021A1 RID: 8609 RVA: 0x0007A674 File Offset: 0x00078874
		public virtual Assembly ReferencedAssembly
		{
			get
			{
				return this._assembly;
			}
		}

		/// <summary>Indicates the manifest resource's location. This property is read-only.</summary>
		/// <returns>A combination of the <see cref="T:System.Reflection.ResourceLocation" /> flags.</returns>
		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x060021A2 RID: 8610 RVA: 0x0007A67C File Offset: 0x0007887C
		public virtual ResourceLocation ResourceLocation
		{
			get
			{
				return this._location;
			}
		}

		// Token: 0x04000C81 RID: 3201
		private Assembly _assembly;

		// Token: 0x04000C82 RID: 3202
		private string _filename;

		// Token: 0x04000C83 RID: 3203
		private ResourceLocation _location;
	}
}
