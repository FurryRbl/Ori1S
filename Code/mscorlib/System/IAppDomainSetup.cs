using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Represents assembly binding information that can be added to an instance of <see cref="T:System.AppDomain" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200013F RID: 319
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("27FFF232-A7A8-40DD-8D4A-734AD59fCD41")]
	[ComVisible(true)]
	public interface IAppDomainSetup
	{
		/// <summary>Gets or sets the name of the directory containing the application.</summary>
		/// <returns>A <see cref="T:System.String" /> containg the name of the application base directory.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700026D RID: 621
		// (get) Token: 0x0600116F RID: 4463
		// (set) Token: 0x06001170 RID: 4464
		string ApplicationBase { get; set; }

		/// <summary>Gets or sets the name of the application.</summary>
		/// <returns>A <see cref="T:System.String" /> that is the name of the application.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06001171 RID: 4465
		// (set) Token: 0x06001172 RID: 4466
		string ApplicationName { get; set; }

		/// <summary>Gets and sets the name of an area specific to the application where files are shadow copied.</summary>
		/// <returns>A <see cref="T:System.String" /> that is the fully-qualified name of the directory path and file name where files are shadow copied.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06001173 RID: 4467
		// (set) Token: 0x06001174 RID: 4468
		string CachePath { get; set; }

		/// <summary>Gets and sets the name of the configuration file for an application domain.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the name of the configuration file.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06001175 RID: 4469
		// (set) Token: 0x06001176 RID: 4470
		string ConfigurationFile { get; set; }

		/// <summary>Gets or sets the directory where dynamically generated files are stored and accessed.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the directory containing dynamic assemblies.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06001177 RID: 4471
		// (set) Token: 0x06001178 RID: 4472
		string DynamicBase { get; set; }

		/// <summary>Gets or sets the location of the license file associated with this domain.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the name of the license file.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06001179 RID: 4473
		// (set) Token: 0x0600117A RID: 4474
		string LicenseFile { get; set; }

		/// <summary>Gets or sets the list of directories that is combined with the <see cref="P:System.AppDomainSetup.ApplicationBase" /> directory to probe for private assemblies.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a list of directory names, where each name is separated by a semicolon.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000273 RID: 627
		// (get) Token: 0x0600117B RID: 4475
		// (set) Token: 0x0600117C RID: 4476
		string PrivateBinPath { get; set; }

		/// <summary>Gets or sets the private binary directory path used to locate an application.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a list of directory names, where each name is separated by a semicolon.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000274 RID: 628
		// (get) Token: 0x0600117D RID: 4477
		// (set) Token: 0x0600117E RID: 4478
		string PrivateBinPathProbe { get; set; }

		/// <summary>Gets or sets the names of the directories containing assemblies to be shadow copied.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a list of directory names, where each name is separated by a semicolon.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000275 RID: 629
		// (get) Token: 0x0600117F RID: 4479
		// (set) Token: 0x06001180 RID: 4480
		string ShadowCopyDirectories { get; set; }

		/// <summary>Gets or sets a string that indicates whether shadow copying is turned on or off.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the value "true" to indicate that shadow copying is turned on; or "false" to indicate that shadow copying is turned off.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06001181 RID: 4481
		// (set) Token: 0x06001182 RID: 4482
		string ShadowCopyFiles { get; set; }
	}
}
