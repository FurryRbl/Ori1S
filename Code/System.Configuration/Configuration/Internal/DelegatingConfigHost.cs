using System;
using System.IO;
using System.Security;

namespace System.Configuration.Internal
{
	/// <summary>Delegates all members of the <see cref="T:System.Configuration.Internal.IInternalConfigHost" /> interface to another instance of a host.</summary>
	// Token: 0x02000004 RID: 4
	public class DelegatingConfigHost : IInternalConfigHost
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.Internal.DelegatingConfigHost" /> class.</summary>
		// Token: 0x06000004 RID: 4 RVA: 0x00002104 File Offset: 0x00000304
		protected DelegatingConfigHost()
		{
		}

		/// <summary>Gets or sets the <see cref="T:System.Configuration.Internal.IInternalConfigHost" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.Internal.IInternalConfigHost" /> object.</returns>
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000210C File Offset: 0x0000030C
		// (set) Token: 0x06000006 RID: 6 RVA: 0x00002114 File Offset: 0x00000314
		protected IInternalConfigHost Host
		{
			get
			{
				return this.host;
			}
			set
			{
				this.host = value;
			}
		}

		/// <summary>Creates a new configuration context.</summary>
		/// <returns>A <see cref="T:System.Object" /> representing a new configuration context.</returns>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		/// <param name="locationSubPath">A string representing a location subpath.</param>
		// Token: 0x06000007 RID: 7 RVA: 0x00002120 File Offset: 0x00000320
		public virtual object CreateConfigurationContext(string configPath, string locationSubPath)
		{
			return this.host.CreateConfigurationContext(configPath, locationSubPath);
		}

		/// <summary>Creates a deprecated configuration context.</summary>
		/// <returns>A <see cref="T:System.Object" /> representing a deprecated configuration context.</returns>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		// Token: 0x06000008 RID: 8 RVA: 0x00002130 File Offset: 0x00000330
		public virtual object CreateDeprecatedConfigContext(string configPath)
		{
			return this.host.CreateDeprecatedConfigContext(configPath);
		}

		/// <summary>Decrypts an encrypted configuration section.</summary>
		/// <returns>A string representing a decrypted configuration section.</returns>
		/// <param name="encryptedXml">An encrypted section of a configuration file.</param>
		/// <param name="protectionProvider">A <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> object.</param>
		/// <param name="protectedConfigSection">A <see cref="T:System.Configuration.ProtectedConfigurationSection" /> object.</param>
		// Token: 0x06000009 RID: 9 RVA: 0x00002140 File Offset: 0x00000340
		public virtual string DecryptSection(string encryptedXml, ProtectedConfigurationProvider protectionProvider, ProtectedConfigurationSection protectedSection)
		{
			return this.host.DecryptSection(encryptedXml, protectionProvider, protectedSection);
		}

		/// <summary>Deletes the <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</summary>
		/// <param name="streamName">The name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</param>
		// Token: 0x0600000A RID: 10 RVA: 0x00002150 File Offset: 0x00000350
		public virtual void DeleteStream(string streamName)
		{
			this.host.DeleteStream(streamName);
		}

		/// <summary>Encrypts a section of a configuration object.</summary>
		/// <returns>A string representing an encrypted section of the configuration object.</returns>
		/// <param name="clearTextXml">A section of the configuration that is not encrypted.</param>
		/// <param name="protectionProvider">A <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> object.</param>
		/// <param name="protectedConfigSection">A <see cref="T:System.Configuration.ProtectedConfigurationSection" /> object.</param>
		// Token: 0x0600000B RID: 11 RVA: 0x00002160 File Offset: 0x00000360
		public virtual string EncryptSection(string encryptedXml, ProtectedConfigurationProvider protectionProvider, ProtectedConfigurationSection protectedSection)
		{
			return this.host.EncryptSection(encryptedXml, protectionProvider, protectedSection);
		}

		/// <summary>Returns a configuration path based on a location subpath.</summary>
		/// <returns>A string representing a configuration path.</returns>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		/// <param name="locationSubPath">A string representing a location subpath.</param>
		// Token: 0x0600000C RID: 12 RVA: 0x00002170 File Offset: 0x00000370
		public virtual string GetConfigPathFromLocationSubPath(string configPath, string locatinSubPath)
		{
			return this.host.GetConfigPathFromLocationSubPath(configPath, locatinSubPath);
		}

		/// <summary>Returns a <see cref="T:System.Type" /> representing the type of the configuration.</summary>
		/// <returns>A <see cref="T:System.Type" /> representing the type of the configuration.</returns>
		/// <param name="typeName">A string representing the configuration type.</param>
		/// <param name="throwOnError">true if an exception should be thrown if an error is encountered; false if an exception should not be thrown if an error is encountered.</param>
		// Token: 0x0600000D RID: 13 RVA: 0x00002180 File Offset: 0x00000380
		public virtual Type GetConfigType(string typeName, bool throwOnError)
		{
			return this.host.GetConfigType(typeName, throwOnError);
		}

		/// <summary>Returns a string representing the type name of the configuration object.</summary>
		/// <returns>A string representing the type name of the configuration object.</returns>
		/// <param name="t">A <see cref="T:System.Type" /> object.</param>
		// Token: 0x0600000E RID: 14 RVA: 0x00002190 File Offset: 0x00000390
		public virtual string GetConfigTypeName(Type t)
		{
			return this.host.GetConfigTypeName(t);
		}

		/// <summary>Sets the specified permission set if available within the host object.</summary>
		/// <param name="configRecord">An <see cref="T:System.Configuration.Internal.IInternalConfigRecord" /> object.</param>
		/// <param name="permissionSet">A <see cref="T:System.Security.PermissionSet" /> object.</param>
		/// <param name="isHostReady">true if the host has finished initialization; otherwise, false.</param>
		// Token: 0x0600000F RID: 15 RVA: 0x000021A0 File Offset: 0x000003A0
		public virtual void GetRestrictedPermissions(IInternalConfigRecord configRecord, out PermissionSet permissionSet, out bool isHostReady)
		{
			this.host.GetRestrictedPermissions(configRecord, out permissionSet, out isHostReady);
		}

		/// <summary>Returns the name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</summary>
		/// <returns>A string representing the name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</returns>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		// Token: 0x06000010 RID: 16 RVA: 0x000021B0 File Offset: 0x000003B0
		public virtual string GetStreamName(string configPath)
		{
			return this.host.GetStreamName(configPath);
		}

		/// <summary>Returns the name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration source.</summary>
		/// <returns>A string representing the name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration source.</returns>
		/// <param name="streamName">The name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</param>
		/// <param name="configSource">A string representing the configuration source.</param>
		// Token: 0x06000011 RID: 17 RVA: 0x000021C0 File Offset: 0x000003C0
		public virtual string GetStreamNameForConfigSource(string streamName, string configSource)
		{
			return this.host.GetStreamNameForConfigSource(streamName, configSource);
		}

		/// <summary>Returns a <see cref="P:System.Diagnostics.FileVersionInfo.FileVersion" /> object representing the version of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</summary>
		/// <returns>A <see cref="P:System.Diagnostics.FileVersionInfo.FileVersion" /> object representing the version of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</returns>
		/// <param name="streamName">The name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</param>
		// Token: 0x06000012 RID: 18 RVA: 0x000021D0 File Offset: 0x000003D0
		public virtual object GetStreamVersion(string streamName)
		{
			return this.host.GetStreamVersion(streamName);
		}

		/// <summary>Instructs the host to impersonate and returns an <see cref="T:System.IDisposable" /> object required internally by the .NET Framework.</summary>
		/// <returns>An <see cref="T:System.IDisposable" /> value.</returns>
		// Token: 0x06000013 RID: 19 RVA: 0x000021E0 File Offset: 0x000003E0
		public virtual IDisposable Impersonate()
		{
			return this.host.Impersonate();
		}

		/// <summary>Initializes the configuration host.</summary>
		/// <param name="configRoot">An <see cref="T:System.Configuration.Internal.IInternalConfigRoot" /> object.</param>
		/// <param name="hostInitParams">A parameter object containing the values used for initializing the configuration host.</param>
		// Token: 0x06000014 RID: 20 RVA: 0x000021F0 File Offset: 0x000003F0
		public virtual void Init(IInternalConfigRoot root, params object[] hostInitParams)
		{
			this.host.Init(root, hostInitParams);
		}

		/// <summary>Initializes the host for configuration.</summary>
		/// <param name="locationSubPath">A string representing a location subpath (passed by reference).</param>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		/// <param name="locationConfigPath">The location configuration path.</param>
		/// <param name="configRoot">The configuration root element.</param>
		/// <param name="hostInitConfigurationParams">A parameter object representing the parameters used to initialize the host.</param>
		// Token: 0x06000015 RID: 21 RVA: 0x00002200 File Offset: 0x00000400
		public virtual void InitForConfiguration(ref string locationSubPath, out string configPath, out string locationConfigPath, IInternalConfigRoot root, params object[] hostInitConfigurationParams)
		{
			this.host.InitForConfiguration(ref locationSubPath, out configPath, out locationConfigPath, root, hostInitConfigurationParams);
		}

		/// <summary>Returns a value indicating whether the configuration is above the application configuration in the configuration hierarchy.</summary>
		/// <returns>true if the configuration is above the application configuration in the configuration hierarchy; otherwise, false.</returns>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		// Token: 0x06000016 RID: 22 RVA: 0x00002214 File Offset: 0x00000414
		public virtual bool IsAboveApplication(string configPath)
		{
			return this.host.IsAboveApplication(configPath);
		}

		/// <summary>Returns a value indicating whether a configuration record is required for the host configuration initialization.</summary>
		/// <returns>true if a configuration record is required for the host configuration initialization; otherwise, false.</returns>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		// Token: 0x06000017 RID: 23 RVA: 0x00002224 File Offset: 0x00000424
		public virtual bool IsConfigRecordRequired(string configPath)
		{
			return this.host.IsConfigRecordRequired(configPath);
		}

		/// <summary>Restricts or allows definitions in the host configuration. </summary>
		/// <returns>true if the grant or restriction of definitions in the host configuration was successful; otherwise, false.</returns>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		/// <param name="allowDefinition">The <see cref="T:System.Configuration.ConfigurationAllowDefinition" /> object.</param>
		/// <param name="allowExeDefinition">The <see cref="T:System.Configuration.ConfigurationAllowExeDefinition" /> object.</param>
		// Token: 0x06000018 RID: 24 RVA: 0x00002234 File Offset: 0x00000434
		public virtual bool IsDefinitionAllowed(string configPath, ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition)
		{
			return this.host.IsDefinitionAllowed(configPath, allowDefinition, allowExeDefinition);
		}

		/// <summary>Returns a value indicating whether the initialization of a configuration object is considered delayed.</summary>
		/// <returns>true if the initialization of a configuration object is considered delayed; otherwise, false.</returns>
		/// <param name="configRecord">The <see cref="T:System.Configuration.Internal.IInternalConfigRecord" /> object.</param>
		// Token: 0x06000019 RID: 25 RVA: 0x00002244 File Offset: 0x00000444
		public virtual bool IsInitDelayed(IInternalConfigRecord configRecord)
		{
			return this.host.IsInitDelayed(configRecord);
		}

		/// <summary>Returns a value indicating whether the file path used by a <see cref="T:System.IO.Stream" /> object to read a configuration file is a valid path.</summary>
		/// <returns>true if the path used by a <see cref="T:System.IO.Stream" /> object to read a configuration file is a valid path; otherwise, false.</returns>
		/// <param name="streamName">The name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</param>
		// Token: 0x0600001A RID: 26 RVA: 0x00002254 File Offset: 0x00000454
		public virtual bool IsFile(string streamName)
		{
			return this.host.IsFile(streamName);
		}

		/// <summary>Returns a value indicating whether a configuration section requires a fully trusted code access security level and does not allow the <see cref="T:System.Security.AllowPartiallyTrustedCallersAttribute" /> attribute to disable implicit link demands.</summary>
		/// <returns>true if the configuration section requires a fully trusted code access security level and does not allow the <see cref="T:System.Security.AllowPartiallyTrustedCallersAttribute" /> attribute to disable implicit link demands; otherwise, false.</returns>
		/// <param name="configRecord">The <see cref="T:System.Configuration.Internal.IInternalConfigRecord" /> object.</param>
		// Token: 0x0600001B RID: 27 RVA: 0x00002264 File Offset: 0x00000464
		public virtual bool IsFullTrustSectionWithoutAptcaAllowed(IInternalConfigRecord configRecord)
		{
			return this.host.IsFullTrustSectionWithoutAptcaAllowed(configRecord);
		}

		/// <summary>Returns a value indicating whether the configuration object supports a location tag.</summary>
		/// <returns>true if the configuration object supports a location tag; otherwise, false.</returns>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		// Token: 0x0600001C RID: 28 RVA: 0x00002274 File Offset: 0x00000474
		public virtual bool IsLocationApplicable(string configPath)
		{
			return this.host.IsLocationApplicable(configPath);
		}

		/// <summary>Gets a value indicating whether the configuration is remote.</summary>
		/// <returns>true if the configuration is remote; otherwise, false.</returns>
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002284 File Offset: 0x00000484
		public virtual bool IsRemote
		{
			get
			{
				return this.host.IsRemote;
			}
		}

		/// <summary>Returns a value indicating whether a configuration path is to a configuration node whose contents should be treated as a root.</summary>
		/// <returns>true if the configuration path is to a configuration node whose contents should be treated as a root; otherwise, false.</returns>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		// Token: 0x0600001E RID: 30 RVA: 0x00002294 File Offset: 0x00000494
		public virtual bool IsSecondaryRoot(string configPath)
		{
			return this.host.IsSecondaryRoot(configPath);
		}

		/// <summary>Returns a value indicating whether the configuration path is trusted.</summary>
		/// <returns>true if the configuration path is trusted; otherwise, false.</returns>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		// Token: 0x0600001F RID: 31 RVA: 0x000022A4 File Offset: 0x000004A4
		public virtual bool IsTrustedConfigPath(string configPath)
		{
			return this.host.IsTrustedConfigPath(configPath);
		}

		/// <summary>Opens a <see cref="T:System.IO.Stream" /> object to read a configuration file.</summary>
		/// <returns>Returns the <see cref="T:System.IO.Stream" /> object specified by <paramref name="streamName" />.</returns>
		/// <param name="streamName">The name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</param>
		// Token: 0x06000020 RID: 32 RVA: 0x000022B4 File Offset: 0x000004B4
		public virtual Stream OpenStreamForRead(string streamName)
		{
			return this.host.OpenStreamForRead(streamName);
		}

		/// <summary>Opens a <see cref="T:System.IO.Stream" /> object to read a configuration file.</summary>
		/// <returns>Returns the <see cref="T:System.IO.Stream" /> object specified by <paramref name="streamName" />.</returns>
		/// <param name="streamName">The name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</param>
		/// <param name="assertPermissions">true to assert permissions; otherwise, false.</param>
		// Token: 0x06000021 RID: 33 RVA: 0x000022C4 File Offset: 0x000004C4
		public virtual Stream OpenStreamForRead(string streamName, bool assertPermissions)
		{
			return this.host.OpenStreamForRead(streamName, assertPermissions);
		}

		/// <summary>Opens a <see cref="T:System.IO.Stream" /> object for writing to a configuration file or for writing to a temporary file used to build a configuration file. Allows a <see cref="T:System.IO.Stream" /> object to be designated as a template for copying file attributes.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> object.</returns>
		/// <param name="streamName">The name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</param>
		/// <param name="templateStreamName">The name of a <see cref="T:System.IO.Stream" /> object from which file attributes are to be copied as a template.</param>
		/// <param name="writeContext">The write context of the <see cref="T:System.IO.Stream" /> object (passed by reference).</param>
		// Token: 0x06000022 RID: 34 RVA: 0x000022D4 File Offset: 0x000004D4
		public virtual Stream OpenStreamForWrite(string streamName, string templateStreamName, ref object writeContext)
		{
			return this.host.OpenStreamForWrite(streamName, templateStreamName, ref writeContext);
		}

		/// <summary>Opens a <see cref="T:System.IO.Stream" /> object for writing to a configuration file. Allows a <see cref="T:System.IO.Stream" /> object to be designated as a template for copying file attributes.</summary>
		/// <returns>Returns the <see cref="T:System.IO.Stream" /> object specified by the <paramref name="streamName" /> parameter.</returns>
		/// <param name="streamName">The name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</param>
		/// <param name="templateStreamName">The name of a <see cref="T:System.IO.Stream" /> object from which file attributes are to be copied as a template.</param>
		/// <param name="writeContext">The write context of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file (passed by reference).</param>
		/// <param name="assertPermissions">true to assert permissions; otherwise, false.</param>
		// Token: 0x06000023 RID: 35 RVA: 0x000022E4 File Offset: 0x000004E4
		public virtual Stream OpenStreamForWrite(string streamName, string templateStreamName, ref object writeContext, bool assertPermissions)
		{
			return this.host.OpenStreamForWrite(streamName, templateStreamName, ref writeContext, assertPermissions);
		}

		/// <summary>Returns a value indicating whether the entire configuration file could be read by a designated <see cref="T:System.IO.Stream" /> object.</summary>
		/// <returns>true if the entire configuration file could be read by the <see cref="T:System.IO.Stream" /> object designated by <paramref name="streamName" />; otherwise, false.</returns>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		/// <param name="streamName">The name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</param>
		// Token: 0x06000024 RID: 36 RVA: 0x000022F8 File Offset: 0x000004F8
		public virtual bool PrefetchAll(string configPath, string streamName)
		{
			return this.host.PrefetchAll(configPath, streamName);
		}

		/// <summary>Instructs the <see cref="T:System.Configuration.Internal.IInternalConfigHost" /> object to read a designated section of its associated configuration file.</summary>
		/// <returns>true if a section of the configuration file designated by the <paramref name="sectionGroupName" /> and <paramref name="sectionName" /> parameters can be read by a <see cref="T:System.IO.Stream" /> object; otherwise, false.</returns>
		/// <param name="sectionGroupName">A string representing the name of a section group in the configuration file.</param>
		/// <param name="sectionName">A string representing the name of a section in the configuration file.</param>
		// Token: 0x06000025 RID: 37 RVA: 0x00002308 File Offset: 0x00000508
		public virtual bool PrefetchSection(string sectionGroupName, string sectionName)
		{
			return this.host.PrefetchSection(sectionGroupName, sectionName);
		}

		/// <summary>Indicates that a new configuration record requires a complete initialization.</summary>
		/// <param name="configRecord">An <see cref="T:System.Configuration.Internal.IInternalConfigRecord" /> object.</param>
		// Token: 0x06000026 RID: 38 RVA: 0x00002318 File Offset: 0x00000518
		public virtual void RequireCompleteInit(IInternalConfigRecord configRecord)
		{
			this.host.RequireCompleteInit(configRecord);
		}

		/// <summary>Instructs the host to monitor an associated <see cref="T:System.IO.Stream" /> object for changes in a configuration file.</summary>
		/// <returns>An <see cref="T:System.Object" /> instance containing changed configuration settings.</returns>
		/// <param name="streamName">The name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</param>
		/// <param name="callback">A <see cref="T:System.Configuration.Internal.StreamChangeCallback" /> object to receive the returned data representing the changes in the configuration file.</param>
		// Token: 0x06000027 RID: 39 RVA: 0x00002328 File Offset: 0x00000528
		public virtual object StartMonitoringStreamForChanges(string streamName, StreamChangeCallback callback)
		{
			return this.host.StartMonitoringStreamForChanges(streamName, callback);
		}

		/// <summary>Instructs the host object to stop monitoring an associated <see cref="T:System.IO.Stream" /> object for changes in a configuration file.</summary>
		/// <param name="streamName">The name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</param>
		/// <param name="callback">A <see cref="T:System.Configuration.Internal.StreamChangeCallback" /> object.</param>
		// Token: 0x06000028 RID: 40 RVA: 0x00002338 File Offset: 0x00000538
		public virtual void StopMonitoringStreamForChanges(string streamName, StreamChangeCallback callback)
		{
			this.host.StopMonitoringStreamForChanges(streamName, callback);
		}

		/// <summary>Verifies that a configuration definition is allowed for a configuration record.</summary>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		/// <param name="allowDefinition">An <see cref="P:System.Configuration.SectionInformation.AllowDefinition" /> object.</param>
		/// <param name="allowExeDefinition">A <see cref="T:System.Configuration.ConfigurationAllowExeDefinition" /> object</param>
		/// <param name="errorInfo">An <see cref="T:System.Configuration.Internal.IConfigErrorInfo" /> object.</param>
		// Token: 0x06000029 RID: 41 RVA: 0x00002348 File Offset: 0x00000548
		public virtual void VerifyDefinitionAllowed(string configPath, ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition, IConfigErrorInfo errorInfo)
		{
			this.host.VerifyDefinitionAllowed(configPath, allowDefinition, allowExeDefinition, errorInfo);
		}

		/// <summary>Indicates that all writing to the configuration file has completed.</summary>
		/// <param name="streamName">The name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</param>
		/// <param name="success">true if writing to the configuration file completed successfully; otherwise, false.</param>
		/// <param name="writeContext">The write context of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
		// Token: 0x0600002A RID: 42 RVA: 0x0000235C File Offset: 0x0000055C
		public virtual void WriteCompleted(string streamName, bool success, object writeContext)
		{
			this.host.WriteCompleted(streamName, success, writeContext);
		}

		/// <summary>Indicates that all writing to the configuration file has completed and specifies whether permissions should be asserted.</summary>
		/// <param name="streamName">The name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</param>
		/// <param name="success">true to indicate that writing was completed successfully; otherwise, false.</param>
		/// <param name="writeContext">The write context of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
		/// <param name="assertPermissions">true to assert permissions; otherwise, false.</param>
		// Token: 0x0600002B RID: 43 RVA: 0x0000236C File Offset: 0x0000056C
		public virtual void WriteCompleted(string streamName, bool success, object writeContext, bool assertPermissions)
		{
			this.host.WriteCompleted(streamName, success, writeContext, assertPermissions);
		}

		/// <summary>Gets a value indicating whether the host configuration supports change notifications.</summary>
		/// <returns>true if the host supports change notifications; otherwise, false. </returns>
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002380 File Offset: 0x00000580
		public virtual bool SupportsChangeNotifications
		{
			get
			{
				return this.host.SupportsChangeNotifications;
			}
		}

		/// <summary>Gets a value indicating whether the host configuration supports location tags.</summary>
		/// <returns>true if the host supports location tags; otherwise, false.</returns>
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002390 File Offset: 0x00000590
		public virtual bool SupportsLocation
		{
			get
			{
				return this.host.SupportsLocation;
			}
		}

		/// <summary>Gets a value indicating whether the host configuration has path support.</summary>
		/// <returns>true if the host configuration has path support; otherwise, false.</returns>
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000023A0 File Offset: 0x000005A0
		public virtual bool SupportsPath
		{
			get
			{
				return this.host.SupportsPath;
			}
		}

		/// <summary>Gets a value indicating whether the host configuration supports refresh.</summary>
		/// <returns>true if the host configuration supports refresh; otherwise, false.</returns>
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000023B0 File Offset: 0x000005B0
		public virtual bool SupportsRefresh
		{
			get
			{
				return this.host.SupportsRefresh;
			}
		}

		// Token: 0x0400001E RID: 30
		private IInternalConfigHost host;
	}
}
