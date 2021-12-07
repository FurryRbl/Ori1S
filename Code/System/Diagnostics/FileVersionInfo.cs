using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.Diagnostics
{
	/// <summary>Provides version information for a physical file on disk.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200022C RID: 556
	[PermissionSet((SecurityAction)14, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"\nUnrestricted=\"true\"/>\n")]
	public sealed class FileVersionInfo
	{
		// Token: 0x060012FC RID: 4860 RVA: 0x00032CF4 File Offset: 0x00030EF4
		private FileVersionInfo()
		{
			this.comments = null;
			this.companyname = null;
			this.filedescription = null;
			this.filename = null;
			this.fileversion = null;
			this.internalname = null;
			this.language = null;
			this.legalcopyright = null;
			this.legaltrademarks = null;
			this.originalfilename = null;
			this.privatebuild = null;
			this.productname = null;
			this.productversion = null;
			this.specialbuild = null;
			this.isdebug = false;
			this.ispatched = false;
			this.isprerelease = false;
			this.isprivatebuild = false;
			this.isspecialbuild = false;
			this.filemajorpart = 0;
			this.fileminorpart = 0;
			this.filebuildpart = 0;
			this.fileprivatepart = 0;
			this.productmajorpart = 0;
			this.productminorpart = 0;
			this.productbuildpart = 0;
			this.productprivatepart = 0;
		}

		/// <summary>Gets the comments associated with the file.</summary>
		/// <returns>The comments associated with the file or null if the file did not contain version information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x060012FD RID: 4861 RVA: 0x00032DC4 File Offset: 0x00030FC4
		public string Comments
		{
			get
			{
				return this.comments;
			}
		}

		/// <summary>Gets the name of the company that produced the file.</summary>
		/// <returns>The name of the company that produced the file or null if the file did not contain version information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x060012FE RID: 4862 RVA: 0x00032DCC File Offset: 0x00030FCC
		public string CompanyName
		{
			get
			{
				return this.companyname;
			}
		}

		/// <summary>Gets the build number of the file.</summary>
		/// <returns>A value representing the build number of the file or null if the file did not contain version information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x060012FF RID: 4863 RVA: 0x00032DD4 File Offset: 0x00030FD4
		public int FileBuildPart
		{
			get
			{
				return this.filebuildpart;
			}
		}

		/// <summary>Gets the description of the file.</summary>
		/// <returns>The description of the file or null if the file did not contain version information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06001300 RID: 4864 RVA: 0x00032DDC File Offset: 0x00030FDC
		public string FileDescription
		{
			get
			{
				return this.filedescription;
			}
		}

		/// <summary>Gets the major part of the version number.</summary>
		/// <returns>A value representing the major part of the version number or 0 (zero) if the file did not contain version information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06001301 RID: 4865 RVA: 0x00032DE4 File Offset: 0x00030FE4
		public int FileMajorPart
		{
			get
			{
				return this.filemajorpart;
			}
		}

		/// <summary>Gets the minor part of the version number of the file.</summary>
		/// <returns>A value representing the minor part of the version number of the file or 0 (zero) if the file did not contain version information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06001302 RID: 4866 RVA: 0x00032DEC File Offset: 0x00030FEC
		public int FileMinorPart
		{
			get
			{
				return this.fileminorpart;
			}
		}

		/// <summary>Gets the name of the file that this instance of <see cref="T:System.Diagnostics.FileVersionInfo" /> describes.</summary>
		/// <returns>The name of the file described by this instance of <see cref="T:System.Diagnostics.FileVersionInfo" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001303 RID: 4867 RVA: 0x00032DF4 File Offset: 0x00030FF4
		public string FileName
		{
			get
			{
				if (SecurityManager.SecurityEnabled)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.filename).Demand();
				}
				return this.filename;
			}
		}

		/// <summary>Gets the file private part number.</summary>
		/// <returns>A value representing the file private part number or null if the file did not contain version information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06001304 RID: 4868 RVA: 0x00032E18 File Offset: 0x00031018
		public int FilePrivatePart
		{
			get
			{
				return this.fileprivatepart;
			}
		}

		/// <summary>Gets the file version number.</summary>
		/// <returns>The version number of the file or null if the file did not contain version information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06001305 RID: 4869 RVA: 0x00032E20 File Offset: 0x00031020
		public string FileVersion
		{
			get
			{
				return this.fileversion;
			}
		}

		/// <summary>Gets the internal name of the file, if one exists.</summary>
		/// <returns>The internal name of the file. If none exists, this property will contain the original name of the file without the extension.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001306 RID: 4870 RVA: 0x00032E28 File Offset: 0x00031028
		public string InternalName
		{
			get
			{
				return this.internalname;
			}
		}

		/// <summary>Gets a value that specifies whether the file contains debugging information or is compiled with debugging features enabled.</summary>
		/// <returns>true if the file contains debugging information or is compiled with debugging features enabled; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001307 RID: 4871 RVA: 0x00032E30 File Offset: 0x00031030
		public bool IsDebug
		{
			get
			{
				return this.isdebug;
			}
		}

		/// <summary>Gets a value that specifies whether the file has been modified and is not identical to the original shipping file of the same version number.</summary>
		/// <returns>true if the file is patched; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001308 RID: 4872 RVA: 0x00032E38 File Offset: 0x00031038
		public bool IsPatched
		{
			get
			{
				return this.ispatched;
			}
		}

		/// <summary>Gets a value that specifies whether the file is a development version, rather than a commercially released product.</summary>
		/// <returns>true if the file is prerelease; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06001309 RID: 4873 RVA: 0x00032E40 File Offset: 0x00031040
		public bool IsPreRelease
		{
			get
			{
				return this.isprerelease;
			}
		}

		/// <summary>Gets a value that specifies whether the file was built using standard release procedures.</summary>
		/// <returns>true if the file is a private build; false if the file was built using standard release procedures or if the file did not contain version information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x0600130A RID: 4874 RVA: 0x00032E48 File Offset: 0x00031048
		public bool IsPrivateBuild
		{
			get
			{
				return this.isprivatebuild;
			}
		}

		/// <summary>Gets a value that specifies whether the file is a special build.</summary>
		/// <returns>true if the file is a special build; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x0600130B RID: 4875 RVA: 0x00032E50 File Offset: 0x00031050
		public bool IsSpecialBuild
		{
			get
			{
				return this.isspecialbuild;
			}
		}

		/// <summary>Gets the default language string for the version info block.</summary>
		/// <returns>The description string for the Microsoft Language Identifier in the version resource or null if the file did not contain version information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x0600130C RID: 4876 RVA: 0x00032E58 File Offset: 0x00031058
		public string Language
		{
			get
			{
				return this.language;
			}
		}

		/// <summary>Gets all copyright notices that apply to the specified file.</summary>
		/// <returns>The copyright notices that apply to the specified file.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x0600130D RID: 4877 RVA: 0x00032E60 File Offset: 0x00031060
		public string LegalCopyright
		{
			get
			{
				return this.legalcopyright;
			}
		}

		/// <summary>Gets the trademarks and registered trademarks that apply to the file.</summary>
		/// <returns>The trademarks and registered trademarks that apply to the file or null if the file did not contain version information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x0600130E RID: 4878 RVA: 0x00032E68 File Offset: 0x00031068
		public string LegalTrademarks
		{
			get
			{
				return this.legaltrademarks;
			}
		}

		/// <summary>Gets the name the file was created with.</summary>
		/// <returns>The name the file was created with or null if the file did not contain version information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x0600130F RID: 4879 RVA: 0x00032E70 File Offset: 0x00031070
		public string OriginalFilename
		{
			get
			{
				return this.originalfilename;
			}
		}

		/// <summary>Gets information about a private version of the file.</summary>
		/// <returns>Information about a private version of the file or null if the file did not contain version information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001310 RID: 4880 RVA: 0x00032E78 File Offset: 0x00031078
		public string PrivateBuild
		{
			get
			{
				return this.privatebuild;
			}
		}

		/// <summary>Gets the build number of the product this file is associated with.</summary>
		/// <returns>A value representing the build number of the product this file is associated with or null if the file did not contain version information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001311 RID: 4881 RVA: 0x00032E80 File Offset: 0x00031080
		public int ProductBuildPart
		{
			get
			{
				return this.productbuildpart;
			}
		}

		/// <summary>Gets the major part of the version number for the product this file is associated with.</summary>
		/// <returns>A value representing the major part of the product version number or null if the file did not contain version information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06001312 RID: 4882 RVA: 0x00032E88 File Offset: 0x00031088
		public int ProductMajorPart
		{
			get
			{
				return this.productmajorpart;
			}
		}

		/// <summary>Gets the minor part of the version number for the product the file is associated with.</summary>
		/// <returns>A value representing the minor part of the product version number or null if the file did not contain version information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06001313 RID: 4883 RVA: 0x00032E90 File Offset: 0x00031090
		public int ProductMinorPart
		{
			get
			{
				return this.productminorpart;
			}
		}

		/// <summary>Gets the name of the product this file is distributed with.</summary>
		/// <returns>The name of the product this file is distributed with or null if the file did not contain version information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06001314 RID: 4884 RVA: 0x00032E98 File Offset: 0x00031098
		public string ProductName
		{
			get
			{
				return this.productname;
			}
		}

		/// <summary>Gets the private part number of the product this file is associated with.</summary>
		/// <returns>A value representing the private part number of the product this file is associated with or null if the file did not contain version information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06001315 RID: 4885 RVA: 0x00032EA0 File Offset: 0x000310A0
		public int ProductPrivatePart
		{
			get
			{
				return this.productprivatepart;
			}
		}

		/// <summary>Gets the version of the product this file is distributed with.</summary>
		/// <returns>The version of the product this file is distributed with or null if the file did not contain version information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001316 RID: 4886 RVA: 0x00032EA8 File Offset: 0x000310A8
		public string ProductVersion
		{
			get
			{
				return this.productversion;
			}
		}

		/// <summary>Gets the special build information for the file.</summary>
		/// <returns>The special build information for the file or null if the file did not contain version information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06001317 RID: 4887 RVA: 0x00032EB0 File Offset: 0x000310B0
		public string SpecialBuild
		{
			get
			{
				return this.specialbuild;
			}
		}

		// Token: 0x06001318 RID: 4888
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetVersionInfo_internal(string fileName);

		/// <summary>Returns a <see cref="T:System.Diagnostics.FileVersionInfo" /> representing the version information associated with the specified file.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.FileVersionInfo" /> containing information about the file. If the file did not contain version information, the <see cref="T:System.Diagnostics.FileVersionInfo" /> contains only the name of the file requested.</returns>
		/// <param name="fileName">The fully qualified path and name of the file to retrieve the version information for. </param>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified cannot be found. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001319 RID: 4889 RVA: 0x00032EB8 File Offset: 0x000310B8
		public static FileVersionInfo GetVersionInfo(string fileName)
		{
			if (SecurityManager.SecurityEnabled)
			{
				new FileIOPermission(FileIOPermissionAccess.Read, fileName).Demand();
			}
			string fullPath = Path.GetFullPath(fileName);
			if (!File.Exists(fullPath))
			{
				throw new FileNotFoundException(fileName);
			}
			FileVersionInfo fileVersionInfo = new FileVersionInfo();
			fileVersionInfo.GetVersionInfo_internal(fileName);
			return fileVersionInfo;
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x00032F04 File Offset: 0x00031104
		private static void AppendFormat(StringBuilder sb, string format, params object[] args)
		{
			sb.AppendFormat(format, args);
		}

		/// <summary>Returns a partial list of properties in the <see cref="T:System.Diagnostics.FileVersionInfo" /> and their values.</summary>
		/// <returns>A list of the following properties in this class and their values: <see cref="P:System.Diagnostics.FileVersionInfo.FileName" />, <see cref="P:System.Diagnostics.FileVersionInfo.InternalName" />, <see cref="P:System.Diagnostics.FileVersionInfo.OriginalFilename" />, <see cref="P:System.Diagnostics.FileVersionInfo.FileVersion" />, <see cref="P:System.Diagnostics.FileVersionInfo.FileDescription" />, <see cref="P:System.Diagnostics.FileVersionInfo.ProductName" />, <see cref="P:System.Diagnostics.FileVersionInfo.ProductVersion" />, <see cref="P:System.Diagnostics.FileVersionInfo.IsDebug" />, <see cref="P:System.Diagnostics.FileVersionInfo.IsPatched" />, <see cref="P:System.Diagnostics.FileVersionInfo.IsPreRelease" />, <see cref="P:System.Diagnostics.FileVersionInfo.IsPrivateBuild" />, <see cref="P:System.Diagnostics.FileVersionInfo.IsSpecialBuild" />,<see cref="P:System.Diagnostics.FileVersionInfo.Language" />.If the file did not contain version information, this list will contain only the name of the requested file. Boolean values will be false, and all other entries will be null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600131B RID: 4891 RVA: 0x00032F10 File Offset: 0x00031110
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			FileVersionInfo.AppendFormat(stringBuilder, "File:             {0}{1}", new object[]
			{
				this.FileName,
				Environment.NewLine
			});
			FileVersionInfo.AppendFormat(stringBuilder, "InternalName:     {0}{1}", new object[]
			{
				this.internalname,
				Environment.NewLine
			});
			FileVersionInfo.AppendFormat(stringBuilder, "OriginalFilename: {0}{1}", new object[]
			{
				this.originalfilename,
				Environment.NewLine
			});
			FileVersionInfo.AppendFormat(stringBuilder, "FileVersion:      {0}{1}", new object[]
			{
				this.fileversion,
				Environment.NewLine
			});
			FileVersionInfo.AppendFormat(stringBuilder, "FileDescription:  {0}{1}", new object[]
			{
				this.filedescription,
				Environment.NewLine
			});
			FileVersionInfo.AppendFormat(stringBuilder, "Product:          {0}{1}", new object[]
			{
				this.productname,
				Environment.NewLine
			});
			FileVersionInfo.AppendFormat(stringBuilder, "ProductVersion:   {0}{1}", new object[]
			{
				this.productversion,
				Environment.NewLine
			});
			FileVersionInfo.AppendFormat(stringBuilder, "Debug:            {0}{1}", new object[]
			{
				this.isdebug,
				Environment.NewLine
			});
			FileVersionInfo.AppendFormat(stringBuilder, "Patched:          {0}{1}", new object[]
			{
				this.ispatched,
				Environment.NewLine
			});
			FileVersionInfo.AppendFormat(stringBuilder, "PreRelease:       {0}{1}", new object[]
			{
				this.isprerelease,
				Environment.NewLine
			});
			FileVersionInfo.AppendFormat(stringBuilder, "PrivateBuild:     {0}{1}", new object[]
			{
				this.isprivatebuild,
				Environment.NewLine
			});
			FileVersionInfo.AppendFormat(stringBuilder, "SpecialBuild:     {0}{1}", new object[]
			{
				this.isspecialbuild,
				Environment.NewLine
			});
			FileVersionInfo.AppendFormat(stringBuilder, "Language          {0}{1}", new object[]
			{
				this.language,
				Environment.NewLine
			});
			return stringBuilder.ToString();
		}

		// Token: 0x0400056E RID: 1390
		private string comments;

		// Token: 0x0400056F RID: 1391
		private string companyname;

		// Token: 0x04000570 RID: 1392
		private string filedescription;

		// Token: 0x04000571 RID: 1393
		private string filename;

		// Token: 0x04000572 RID: 1394
		private string fileversion;

		// Token: 0x04000573 RID: 1395
		private string internalname;

		// Token: 0x04000574 RID: 1396
		private string language;

		// Token: 0x04000575 RID: 1397
		private string legalcopyright;

		// Token: 0x04000576 RID: 1398
		private string legaltrademarks;

		// Token: 0x04000577 RID: 1399
		private string originalfilename;

		// Token: 0x04000578 RID: 1400
		private string privatebuild;

		// Token: 0x04000579 RID: 1401
		private string productname;

		// Token: 0x0400057A RID: 1402
		private string productversion;

		// Token: 0x0400057B RID: 1403
		private string specialbuild;

		// Token: 0x0400057C RID: 1404
		private bool isdebug;

		// Token: 0x0400057D RID: 1405
		private bool ispatched;

		// Token: 0x0400057E RID: 1406
		private bool isprerelease;

		// Token: 0x0400057F RID: 1407
		private bool isprivatebuild;

		// Token: 0x04000580 RID: 1408
		private bool isspecialbuild;

		// Token: 0x04000581 RID: 1409
		private int filemajorpart;

		// Token: 0x04000582 RID: 1410
		private int fileminorpart;

		// Token: 0x04000583 RID: 1411
		private int filebuildpart;

		// Token: 0x04000584 RID: 1412
		private int fileprivatepart;

		// Token: 0x04000585 RID: 1413
		private int productmajorpart;

		// Token: 0x04000586 RID: 1414
		private int productminorpart;

		// Token: 0x04000587 RID: 1415
		private int productbuildpart;

		// Token: 0x04000588 RID: 1416
		private int productprivatepart;
	}
}
