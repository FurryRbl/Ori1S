using System;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Policy;

namespace System.CodeDom.Compiler
{
	/// <summary>Represents the parameters used to invoke a compiler.</summary>
	// Token: 0x02000082 RID: 130
	[PermissionSet((SecurityAction)15, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"\nUnrestricted=\"true\"/>\n")]
	[PermissionSet((SecurityAction)14, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"\nUnrestricted=\"true\"/>\n")]
	[Serializable]
	public class CompilerParameters
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> class.</summary>
		// Token: 0x06000555 RID: 1365 RVA: 0x00011330 File Offset: 0x0000F530
		public CompilerParameters()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> class using the specified assembly names.</summary>
		/// <param name="assemblyNames">The names of the assemblies to reference. </param>
		// Token: 0x06000556 RID: 1366 RVA: 0x0001134C File Offset: 0x0000F54C
		public CompilerParameters(string[] assemblyNames)
		{
			this.referencedAssemblies = new System.Collections.Specialized.StringCollection();
			this.referencedAssemblies.AddRange(assemblyNames);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> class using the specified assembly names and output file name.</summary>
		/// <param name="assemblyNames">The names of the assemblies to reference. </param>
		/// <param name="outputName">The output file name. </param>
		// Token: 0x06000557 RID: 1367 RVA: 0x00011380 File Offset: 0x0000F580
		public CompilerParameters(string[] assemblyNames, string output)
		{
			this.referencedAssemblies = new System.Collections.Specialized.StringCollection();
			this.referencedAssemblies.AddRange(assemblyNames);
			this.outputAssembly = output;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> class using the specified assembly names, output name, and a value indicating whether to include debug information.</summary>
		/// <param name="assemblyNames">The names of the assemblies to reference. </param>
		/// <param name="outputName">The output file name. </param>
		/// <param name="includeDebugInformation">true if debug information should be included; false if debug information should be excluded. </param>
		// Token: 0x06000558 RID: 1368 RVA: 0x000113C4 File Offset: 0x0000F5C4
		public CompilerParameters(string[] assemblyNames, string output, bool includeDebugInfo)
		{
			this.referencedAssemblies = new System.Collections.Specialized.StringCollection();
			this.referencedAssemblies.AddRange(assemblyNames);
			this.outputAssembly = output;
			this.includeDebugInformation = includeDebugInfo;
		}

		/// <summary>Gets or sets the optional additional-command line arguments string to use when invoking the compiler.</summary>
		/// <returns>Any additional command line arguments for the compiler.</returns>
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x00011404 File Offset: 0x0000F604
		// (set) Token: 0x0600055A RID: 1370 RVA: 0x0001140C File Offset: 0x0000F60C
		public string CompilerOptions
		{
			get
			{
				return this.compilerOptions;
			}
			set
			{
				this.compilerOptions = value;
			}
		}

		/// <summary>Specifies an evidence object that represents the security policy permissions to grant the compiled assembly.</summary>
		/// <returns>An <see cref="T:System.Security.Policy.Evidence" /> object that represents the security policy permissions to grant the compiled assembly.</returns>
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x00011418 File Offset: 0x0000F618
		// (set) Token: 0x0600055C RID: 1372 RVA: 0x00011420 File Offset: 0x0000F620
		public Evidence Evidence
		{
			get
			{
				return this.evidence;
			}
			[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"ControlEvidence\"/>\n</PermissionSet>\n")]
			set
			{
				this.evidence = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to generate an executable.</summary>
		/// <returns>true if an executable should be generated; otherwise, false.</returns>
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x0001142C File Offset: 0x0000F62C
		// (set) Token: 0x0600055E RID: 1374 RVA: 0x00011434 File Offset: 0x0000F634
		public bool GenerateExecutable
		{
			get
			{
				return this.generateExecutable;
			}
			set
			{
				this.generateExecutable = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to generate the output in memory.</summary>
		/// <returns>true if the compiler should generate the output in memory; otherwise, false.</returns>
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x00011440 File Offset: 0x0000F640
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x00011448 File Offset: 0x0000F648
		public bool GenerateInMemory
		{
			get
			{
				return this.generateInMemory;
			}
			set
			{
				this.generateInMemory = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to include debug information in the compiled executable.</summary>
		/// <returns>true if debug information should be generated; otherwise, false.</returns>
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x00011454 File Offset: 0x0000F654
		// (set) Token: 0x06000562 RID: 1378 RVA: 0x0001145C File Offset: 0x0000F65C
		public bool IncludeDebugInformation
		{
			get
			{
				return this.includeDebugInformation;
			}
			set
			{
				this.includeDebugInformation = value;
			}
		}

		/// <summary>Gets or sets the name of the main class.</summary>
		/// <returns>The name of the main class.</returns>
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x00011468 File Offset: 0x0000F668
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x00011470 File Offset: 0x0000F670
		public string MainClass
		{
			get
			{
				return this.mainClass;
			}
			set
			{
				this.mainClass = value;
			}
		}

		/// <summary>Gets or sets the name of the output assembly.</summary>
		/// <returns>The name of the output assembly.</returns>
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x0001147C File Offset: 0x0000F67C
		// (set) Token: 0x06000566 RID: 1382 RVA: 0x00011484 File Offset: 0x0000F684
		public string OutputAssembly
		{
			get
			{
				return this.outputAssembly;
			}
			set
			{
				this.outputAssembly = value;
			}
		}

		/// <summary>Gets the assemblies referenced by the current project.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> that contains the assembly names that are referenced by the source to compile.</returns>
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x00011490 File Offset: 0x0000F690
		public System.Collections.Specialized.StringCollection ReferencedAssemblies
		{
			get
			{
				if (this.referencedAssemblies == null)
				{
					this.referencedAssemblies = new System.Collections.Specialized.StringCollection();
				}
				return this.referencedAssemblies;
			}
		}

		/// <summary>Gets or sets the collection that contains the temporary files.</summary>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> that contains the temporary files.</returns>
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x000114B0 File Offset: 0x0000F6B0
		// (set) Token: 0x06000569 RID: 1385 RVA: 0x000114D0 File Offset: 0x0000F6D0
		public TempFileCollection TempFiles
		{
			get
			{
				if (this.tempFiles == null)
				{
					this.tempFiles = new TempFileCollection();
				}
				return this.tempFiles;
			}
			set
			{
				this.tempFiles = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to treat warnings as errors.</summary>
		/// <returns>true if warnings should be treated as errors; otherwise, false.</returns>
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x000114DC File Offset: 0x0000F6DC
		// (set) Token: 0x0600056B RID: 1387 RVA: 0x000114E4 File Offset: 0x0000F6E4
		public bool TreatWarningsAsErrors
		{
			get
			{
				return this.treatWarningsAsErrors;
			}
			set
			{
				this.treatWarningsAsErrors = value;
			}
		}

		/// <summary>Gets or sets the user token to use when creating the compiler process.</summary>
		/// <returns>The user token to use.</returns>
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x000114F0 File Offset: 0x0000F6F0
		// (set) Token: 0x0600056D RID: 1389 RVA: 0x000114F8 File Offset: 0x0000F6F8
		public IntPtr UserToken
		{
			get
			{
				return this.userToken;
			}
			set
			{
				this.userToken = value;
			}
		}

		/// <summary>Gets or sets the warning level at which the compiler aborts compilation.</summary>
		/// <returns>The warning level at which the compiler aborts compilation.</returns>
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x00011504 File Offset: 0x0000F704
		// (set) Token: 0x0600056F RID: 1391 RVA: 0x0001150C File Offset: 0x0000F70C
		public int WarningLevel
		{
			get
			{
				return this.warningLevel;
			}
			set
			{
				this.warningLevel = value;
			}
		}

		/// <summary>Gets or sets the file name of a Win32 resource file to link into the compiled assembly.</summary>
		/// <returns>A Win32 resource file that will be linked into the compiled assembly.</returns>
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x00011518 File Offset: 0x0000F718
		// (set) Token: 0x06000571 RID: 1393 RVA: 0x00011520 File Offset: 0x0000F720
		public string Win32Resource
		{
			get
			{
				return this.win32Resource;
			}
			set
			{
				this.win32Resource = value;
			}
		}

		/// <summary>Gets the .NET Framework resource files to include when compiling the assembly output.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> containing the file paths of .NET Framework resources to include in the generated assembly.</returns>
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x0001152C File Offset: 0x0000F72C
		[ComVisible(false)]
		public System.Collections.Specialized.StringCollection EmbeddedResources
		{
			get
			{
				if (this.embedded_resources == null)
				{
					this.embedded_resources = new System.Collections.Specialized.StringCollection();
				}
				return this.embedded_resources;
			}
		}

		/// <summary>Gets the .NET Framework resource files that are referenced in the current source.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> containing the file paths of .NET Framework resources that are referenced by the source.</returns>
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x0001154C File Offset: 0x0000F74C
		[ComVisible(false)]
		public System.Collections.Specialized.StringCollection LinkedResources
		{
			get
			{
				if (this.linked_resources == null)
				{
					this.linked_resources = new System.Collections.Specialized.StringCollection();
				}
				return this.linked_resources;
			}
		}

		// Token: 0x04000145 RID: 325
		private string compilerOptions;

		// Token: 0x04000146 RID: 326
		private Evidence evidence;

		// Token: 0x04000147 RID: 327
		private bool generateExecutable;

		// Token: 0x04000148 RID: 328
		private bool generateInMemory;

		// Token: 0x04000149 RID: 329
		private bool includeDebugInformation;

		// Token: 0x0400014A RID: 330
		private string mainClass;

		// Token: 0x0400014B RID: 331
		private string outputAssembly;

		// Token: 0x0400014C RID: 332
		private System.Collections.Specialized.StringCollection referencedAssemblies;

		// Token: 0x0400014D RID: 333
		private TempFileCollection tempFiles;

		// Token: 0x0400014E RID: 334
		private bool treatWarningsAsErrors;

		// Token: 0x0400014F RID: 335
		private IntPtr userToken = IntPtr.Zero;

		// Token: 0x04000150 RID: 336
		private int warningLevel = -1;

		// Token: 0x04000151 RID: 337
		private string win32Resource;

		// Token: 0x04000152 RID: 338
		private System.Collections.Specialized.StringCollection embedded_resources;

		// Token: 0x04000153 RID: 339
		private System.Collections.Specialized.StringCollection linked_resources;
	}
}
