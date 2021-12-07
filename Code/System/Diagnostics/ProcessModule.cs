using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	/// <summary>Represents a.dll or .exe file that is loaded into a particular process.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000247 RID: 583
	[System.ComponentModel.Designer("System.Diagnostics.Design.ProcessModuleDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class ProcessModule : System.ComponentModel.Component
	{
		// Token: 0x06001474 RID: 5236 RVA: 0x00036A74 File Offset: 0x00034C74
		internal ProcessModule(IntPtr baseaddr, IntPtr entryaddr, string filename, FileVersionInfo version_info, int memory_size, string modulename)
		{
			this.baseaddr = baseaddr;
			this.entryaddr = entryaddr;
			this.filename = filename;
			this.version_info = version_info;
			this.memory_size = memory_size;
			this.modulename = modulename;
		}

		/// <summary>Gets the memory address where the module was loaded.</summary>
		/// <returns>The load address of the module.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001475 RID: 5237 RVA: 0x00036AAC File Offset: 0x00034CAC
		[MonitoringDescription("The base memory address of this module")]
		public IntPtr BaseAddress
		{
			get
			{
				return this.baseaddr;
			}
		}

		/// <summary>Gets the memory address for the function that runs when the system loads and runs the module.</summary>
		/// <returns>The entry point of the module.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001476 RID: 5238 RVA: 0x00036AB4 File Offset: 0x00034CB4
		[MonitoringDescription("The base memory address of the entry point of this module")]
		public IntPtr EntryPointAddress
		{
			get
			{
				return this.entryaddr;
			}
		}

		/// <summary>Gets the full path to the module.</summary>
		/// <returns>The fully qualified path that defines the location of the module.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001477 RID: 5239 RVA: 0x00036ABC File Offset: 0x00034CBC
		[MonitoringDescription("The file name of this module")]
		public string FileName
		{
			get
			{
				return this.filename;
			}
		}

		/// <summary>Gets version information about the module.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.FileVersionInfo" /> that contains the module's version information.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001478 RID: 5240 RVA: 0x00036AC4 File Offset: 0x00034CC4
		[System.ComponentModel.Browsable(false)]
		public FileVersionInfo FileVersionInfo
		{
			get
			{
				return this.version_info;
			}
		}

		/// <summary>Gets the amount of memory that is required to load the module.</summary>
		/// <returns>The size, in bytes, of the memory that the module occupies.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001479 RID: 5241 RVA: 0x00036ACC File Offset: 0x00034CCC
		[MonitoringDescription("The memory needed by this module")]
		public int ModuleMemorySize
		{
			get
			{
				return this.memory_size;
			}
		}

		/// <summary>Gets the name of the process module.</summary>
		/// <returns>The name of the module.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x0600147A RID: 5242 RVA: 0x00036AD4 File Offset: 0x00034CD4
		[MonitoringDescription("The name of this module")]
		public string ModuleName
		{
			get
			{
				return this.modulename;
			}
		}

		/// <summary>Converts the name of the module to a string.</summary>
		/// <returns>The value of the <see cref="P:System.Diagnostics.ProcessModule.ModuleName" /> property.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600147B RID: 5243 RVA: 0x00036ADC File Offset: 0x00034CDC
		public override string ToString()
		{
			return this.ModuleName;
		}

		// Token: 0x0400061C RID: 1564
		private IntPtr baseaddr;

		// Token: 0x0400061D RID: 1565
		private IntPtr entryaddr;

		// Token: 0x0400061E RID: 1566
		private string filename;

		// Token: 0x0400061F RID: 1567
		private FileVersionInfo version_info;

		// Token: 0x04000620 RID: 1568
		private int memory_size;

		// Token: 0x04000621 RID: 1569
		private string modulename;
	}
}
