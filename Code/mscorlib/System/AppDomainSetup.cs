using System;
using System.IO;
using System.Runtime.Hosting;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Policy;

namespace System
{
	/// <summary>Represents assembly binding information that can be added to an instance of <see cref="T:System.AppDomain" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000FB RID: 251
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	[Serializable]
	public sealed class AppDomainSetup : IAppDomainSetup
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.AppDomainSetup" /> class.</summary>
		// Token: 0x06000D27 RID: 3367 RVA: 0x0003A35C File Offset: 0x0003855C
		public AppDomainSetup()
		{
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x0003A364 File Offset: 0x00038564
		internal AppDomainSetup(AppDomainSetup setup)
		{
			this.application_base = setup.application_base;
			this.application_name = setup.application_name;
			this.cache_path = setup.cache_path;
			this.configuration_file = setup.configuration_file;
			this.dynamic_base = setup.dynamic_base;
			this.license_file = setup.license_file;
			this.private_bin_path = setup.private_bin_path;
			this.private_bin_path_probe = setup.private_bin_path_probe;
			this.shadow_copy_directories = setup.shadow_copy_directories;
			this.shadow_copy_files = setup.shadow_copy_files;
			this.publisher_policy = setup.publisher_policy;
			this.path_changed = setup.path_changed;
			this.loader_optimization = setup.loader_optimization;
			this.disallow_binding_redirects = setup.disallow_binding_redirects;
			this.disallow_code_downloads = setup.disallow_code_downloads;
			this._activationArguments = setup._activationArguments;
			this.domain_initializer = setup.domain_initializer;
			this.domain_initializer_args = setup.domain_initializer_args;
			this.application_trust_xml = setup.application_trust_xml;
			this.disallow_appbase_probe = setup.disallow_appbase_probe;
			this.configuration_bytes = setup.configuration_bytes;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AppDomainSetup" /> class with the specified activation arguments required for manifest-based activation of an application domain.</summary>
		/// <param name="activationArguments">An <see cref="T:System.Runtime.Hosting.ActivationArguments" /> object that specifies information required for the manifest-based activation of a new application domain.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="activationArguments" /> is null.</exception>
		// Token: 0x06000D29 RID: 3369 RVA: 0x0003A474 File Offset: 0x00038674
		public AppDomainSetup(ActivationArguments activationArguments)
		{
			this._activationArguments = activationArguments;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AppDomainSetup" /> class with the specified activation context to use for manifest-based activation of an application domain.</summary>
		/// <param name="activationContext">An <see cref="T:System.ActivationContext" /> object that specifies the activation context to be used for an application domain.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="activationContext" /> is null.</exception>
		// Token: 0x06000D2A RID: 3370 RVA: 0x0003A484 File Offset: 0x00038684
		public AppDomainSetup(ActivationContext activationContext)
		{
			this._activationArguments = new ActivationArguments(activationContext);
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x0003A498 File Offset: 0x00038698
		private static string GetAppBase(string appBase)
		{
			if (appBase == null)
			{
				return null;
			}
			int length = appBase.Length;
			if (length >= 8 && appBase.ToLower().StartsWith("file://"))
			{
				appBase = appBase.Substring(7);
				if (Path.DirectorySeparatorChar != '/')
				{
					appBase = appBase.Replace('/', Path.DirectorySeparatorChar);
				}
				if (Environment.IsRunningOnWindows)
				{
					appBase = "//" + appBase;
				}
			}
			else
			{
				appBase = Path.GetFullPath(appBase);
			}
			return appBase;
		}

		/// <summary>Gets or sets the name of the directory containing the application.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the application base directory.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x0003A51C File Offset: 0x0003871C
		// (set) Token: 0x06000D2D RID: 3373 RVA: 0x0003A52C File Offset: 0x0003872C
		public string ApplicationBase
		{
			get
			{
				return AppDomainSetup.GetAppBase(this.application_base);
			}
			set
			{
				this.application_base = value;
			}
		}

		/// <summary>Gets or sets the name of the application.</summary>
		/// <returns>A <see cref="T:System.String" /> that is the name of the application.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x0003A538 File Offset: 0x00038738
		// (set) Token: 0x06000D2F RID: 3375 RVA: 0x0003A540 File Offset: 0x00038740
		public string ApplicationName
		{
			get
			{
				return this.application_name;
			}
			set
			{
				this.application_name = value;
			}
		}

		/// <summary>Gets or sets the name of an area specific to the application where files are shadow copied. </summary>
		/// <returns>A <see cref="T:System.String" /> that is the fully qualified name of the directory path and file name where files are shadow copied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000D30 RID: 3376 RVA: 0x0003A54C File Offset: 0x0003874C
		// (set) Token: 0x06000D31 RID: 3377 RVA: 0x0003A554 File Offset: 0x00038754
		public string CachePath
		{
			get
			{
				return this.cache_path;
			}
			set
			{
				this.cache_path = value;
			}
		}

		/// <summary>Gets or sets the name of the configuration file for an application domain.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the name of the configuration file.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000D32 RID: 3378 RVA: 0x0003A560 File Offset: 0x00038760
		// (set) Token: 0x06000D33 RID: 3379 RVA: 0x0003A5B8 File Offset: 0x000387B8
		public string ConfigurationFile
		{
			get
			{
				if (this.configuration_file == null)
				{
					return null;
				}
				if (Path.IsPathRooted(this.configuration_file))
				{
					return this.configuration_file;
				}
				if (this.ApplicationBase == null)
				{
					throw new MemberAccessException("The ApplicationBase must be set before retrieving this property.");
				}
				return Path.Combine(this.ApplicationBase, this.configuration_file);
			}
			set
			{
				this.configuration_file = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the publisher policy section of the configuration file is applied to an application domain.</summary>
		/// <returns>true if the publisherPolicy section of the configuration file for an application domain is ignored; otherwise, the declared publisher policy is honored.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000D34 RID: 3380 RVA: 0x0003A5C4 File Offset: 0x000387C4
		// (set) Token: 0x06000D35 RID: 3381 RVA: 0x0003A5CC File Offset: 0x000387CC
		public bool DisallowPublisherPolicy
		{
			get
			{
				return this.publisher_policy;
			}
			set
			{
				this.publisher_policy = value;
			}
		}

		/// <summary>Gets or sets the base directory where the directory for dynamically generated files is located.</summary>
		/// <returns>The directory where the <see cref="P:System.AppDomain.DynamicDirectory" /> is located.Note:The return value of this property is different from the value assigned. See the Remarks section.</returns>
		/// <exception cref="T:System.MemberAccessException">This property cannot be set because the application name on the application domain is null.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x0003A5D8 File Offset: 0x000387D8
		// (set) Token: 0x06000D37 RID: 3383 RVA: 0x0003A630 File Offset: 0x00038830
		public string DynamicBase
		{
			get
			{
				if (this.dynamic_base == null)
				{
					return null;
				}
				if (Path.IsPathRooted(this.dynamic_base))
				{
					return this.dynamic_base;
				}
				if (this.ApplicationBase == null)
				{
					throw new MemberAccessException("The ApplicationBase must be set before retrieving this property.");
				}
				return Path.Combine(this.ApplicationBase, this.dynamic_base);
			}
			set
			{
				if (this.application_name == null)
				{
					throw new MemberAccessException("ApplicationName must be set before the DynamicBase can be set.");
				}
				this.dynamic_base = Path.Combine(value, ((uint)this.application_name.GetHashCode()).ToString("x"));
			}
		}

		/// <summary>Gets or sets the location of the license file associated with this domain.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the name of the license file.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x0003A678 File Offset: 0x00038878
		// (set) Token: 0x06000D39 RID: 3385 RVA: 0x0003A680 File Offset: 0x00038880
		public string LicenseFile
		{
			get
			{
				return this.license_file;
			}
			set
			{
				this.license_file = value;
			}
		}

		/// <summary>Specifies the optimization policy used to load an executable.</summary>
		/// <returns>A <see cref="T:System.LoaderOptimization" /> enumerated constant used with the <see cref="T:System.LoaderOptimizationAttribute" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000D3A RID: 3386 RVA: 0x0003A68C File Offset: 0x0003888C
		// (set) Token: 0x06000D3B RID: 3387 RVA: 0x0003A694 File Offset: 0x00038894
		[MonoLimitation("In Mono this is controlled by the --share-code flag")]
		public LoaderOptimization LoaderOptimization
		{
			get
			{
				return this.loader_optimization;
			}
			set
			{
				this.loader_optimization = value;
			}
		}

		/// <summary>Gets or sets the list of directories under the application base directory that are probed for private assemblies.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a list of directory names, where the names are separated by semicolons.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000D3C RID: 3388 RVA: 0x0003A6A0 File Offset: 0x000388A0
		// (set) Token: 0x06000D3D RID: 3389 RVA: 0x0003A6A8 File Offset: 0x000388A8
		public string PrivateBinPath
		{
			get
			{
				return this.private_bin_path;
			}
			set
			{
				this.private_bin_path = value;
				this.path_changed = true;
			}
		}

		/// <summary>Gets or sets a string value that includes or excludes <see cref="P:System.AppDomainSetup.ApplicationBase" /> from the search path for the application, and searches only <see cref="P:System.AppDomainSetup.PrivateBinPath" />.</summary>
		/// <returns>A null reference (Nothing in Visual Basic) to include the application base path when searching for assemblies; any non-null string value to exclude the path. The default value is null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000D3E RID: 3390 RVA: 0x0003A6B8 File Offset: 0x000388B8
		// (set) Token: 0x06000D3F RID: 3391 RVA: 0x0003A6C0 File Offset: 0x000388C0
		public string PrivateBinPathProbe
		{
			get
			{
				return this.private_bin_path_probe;
			}
			set
			{
				this.private_bin_path_probe = value;
				this.path_changed = true;
			}
		}

		/// <summary>Gets or sets the names of the directories containing assemblies to be shadow copied.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a list of directory names, where each name is separated by a semicolon.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000D40 RID: 3392 RVA: 0x0003A6D0 File Offset: 0x000388D0
		// (set) Token: 0x06000D41 RID: 3393 RVA: 0x0003A6D8 File Offset: 0x000388D8
		public string ShadowCopyDirectories
		{
			get
			{
				return this.shadow_copy_directories;
			}
			set
			{
				this.shadow_copy_directories = value;
			}
		}

		/// <summary>Gets or sets a string that indicates whether shadow copying is turned on or off.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the string value "true" to indicate that shadow copying is turned on; or "false" to indicate that shadow copying is turned off.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x0003A6E4 File Offset: 0x000388E4
		// (set) Token: 0x06000D43 RID: 3395 RVA: 0x0003A6EC File Offset: 0x000388EC
		public string ShadowCopyFiles
		{
			get
			{
				return this.shadow_copy_files;
			}
			set
			{
				this.shadow_copy_files = value;
			}
		}

		/// <summary>Gets or sets a value indicating if an application domain allows assembly binding redirection.</summary>
		/// <returns>true if redirection of assemblies is disallowed; otherwise it is allowed.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x0003A6F8 File Offset: 0x000388F8
		// (set) Token: 0x06000D45 RID: 3397 RVA: 0x0003A700 File Offset: 0x00038900
		public bool DisallowBindingRedirects
		{
			get
			{
				return this.disallow_binding_redirects;
			}
			set
			{
				this.disallow_binding_redirects = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether HTTP download of assemblies is allowed for an application domain.</summary>
		/// <returns>true if HTTP download of assemblies is to be disallowed; otherwise, it is allowed.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x0003A70C File Offset: 0x0003890C
		// (set) Token: 0x06000D47 RID: 3399 RVA: 0x0003A714 File Offset: 0x00038914
		public bool DisallowCodeDownload
		{
			get
			{
				return this.disallow_code_downloads;
			}
			set
			{
				this.disallow_code_downloads = value;
			}
		}

		/// <summary>Gets or sets data about the activation of an application domain.</summary>
		/// <returns>An <see cref="T:System.Runtime.Hosting.ActivationArguments" /> object containing data about the activation of an application domain.</returns>
		/// <exception cref="T:System.InvalidOperationException">The property is set to an <see cref="T:System.Runtime.Hosting.ActivationArguments" /> object whose application identity does not match the application identity of the <see cref="T:System.Security.Policy.ApplicationTrust" /> object returned by the <see cref="P:System.AppDomainSetup.ApplicationTrust" /> property. No exception is thrown if the <see cref="P:System.AppDomainSetup.ApplicationTrust" /> property is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000D48 RID: 3400 RVA: 0x0003A720 File Offset: 0x00038920
		// (set) Token: 0x06000D49 RID: 3401 RVA: 0x0003A728 File Offset: 0x00038928
		public ActivationArguments ActivationArguments
		{
			get
			{
				return this._activationArguments;
			}
			set
			{
				this._activationArguments = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.AppDomainInitializer" /> delegate, which represents a callback method that is invoked when the application domain is initialized.</summary>
		/// <returns>An <see cref="T:System.AppDomainInitializer" /> delegate that represents a callback method that is invoked when the application domain is initialized.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000D4A RID: 3402 RVA: 0x0003A734 File Offset: 0x00038934
		// (set) Token: 0x06000D4B RID: 3403 RVA: 0x0003A73C File Offset: 0x0003893C
		[MonoLimitation("it needs to be invoked within the created domain")]
		public AppDomainInitializer AppDomainInitializer
		{
			get
			{
				return this.domain_initializer;
			}
			set
			{
				this.domain_initializer = value;
			}
		}

		/// <summary>Gets or sets the arguments passed to the callback method represented by the <see cref="T:System.AppDomainInitializer" /> delegate. The callback method is invoked when the application domain is initialized.</summary>
		/// <returns>An array of strings that is passed to the callback method represented by the <see cref="T:System.AppDomainInitializer" /> delegate, when the callback method is invoked during <see cref="T:System.AppDomain" /> initialization.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000D4C RID: 3404 RVA: 0x0003A748 File Offset: 0x00038948
		// (set) Token: 0x06000D4D RID: 3405 RVA: 0x0003A750 File Offset: 0x00038950
		[MonoLimitation("it needs to be used to invoke the initializer within the created domain")]
		public string[] AppDomainInitializerArguments
		{
			get
			{
				return this.domain_initializer_args;
			}
			set
			{
				this.domain_initializer_args = value;
			}
		}

		/// <summary>Gets or sets an object containing security and trust information.</summary>
		/// <returns>An <see cref="T:System.Security.Policy.ApplicationTrust" /> object representing security and trust information. </returns>
		/// <exception cref="T:System.InvalidOperationException">The property is set to an <see cref="T:System.Security.Policy.ApplicationTrust" /> object whose application identity does not match the application identity of the <see cref="T:System.Runtime.Hosting.ActivationArguments" /> object returned by the <see cref="P:System.AppDomainSetup.ActivationArguments" /> property. No exception is thrown if the <see cref="P:System.AppDomainSetup.ActivationArguments" /> property is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">The property is set to null.</exception>
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x0003A75C File Offset: 0x0003895C
		// (set) Token: 0x06000D4F RID: 3407 RVA: 0x0003A788 File Offset: 0x00038988
		[MonoNotSupported("This property exists but not considered.")]
		public ApplicationTrust ApplicationTrust
		{
			get
			{
				if (this.application_trust_xml == null)
				{
					return null;
				}
				if (this.application_trust == null)
				{
					this.application_trust = new ApplicationTrust();
				}
				return this.application_trust;
			}
			set
			{
				this.application_trust = value;
				if (value != null)
				{
					this.application_trust_xml = value.ToXml();
					this.application_trust.FromXml(this.application_trust_xml);
				}
				else
				{
					this.application_trust_xml = null;
				}
			}
		}

		/// <summary>Specifies whether the application base path and private binary path are probed when searching for assemblies to load.</summary>
		/// <returns>true if probing is not allowed; otherwise false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000D50 RID: 3408 RVA: 0x0003A7CC File Offset: 0x000389CC
		// (set) Token: 0x06000D51 RID: 3409 RVA: 0x0003A7D4 File Offset: 0x000389D4
		[MonoNotSupported("This property exists but not considered.")]
		public bool DisallowApplicationBaseProbing
		{
			get
			{
				return this.disallow_appbase_probe;
			}
			set
			{
				this.disallow_appbase_probe = value;
			}
		}

		/// <summary>Gets the XML configuration information set by the <see cref="M:System.AppDomainSetup.SetConfigurationBytes(System.Byte[])" /> method, which overrides the application's XML configuration information.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the XML configuration information that was set by the <see cref="M:System.AppDomainSetup.SetConfigurationBytes(System.Byte[])" /> method, or null if the <see cref="M:System.AppDomainSetup.SetConfigurationBytes(System.Byte[])" /> method has not been called.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D52 RID: 3410 RVA: 0x0003A7E0 File Offset: 0x000389E0
		[MonoNotSupported("This method exists but not considered.")]
		public byte[] GetConfigurationBytes()
		{
			return (this.configuration_bytes == null) ? null : (this.configuration_bytes.Clone() as byte[]);
		}

		/// <summary>Provides XML configuration information for the application domain, overriding the application's XML configuration information.</summary>
		/// <param name="value">A <see cref="T:System.Byte" /> array containing the XML configuration information to be used for the application domain.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D53 RID: 3411 RVA: 0x0003A804 File Offset: 0x00038A04
		[MonoNotSupported("This method exists but not considered.")]
		public void SetConfigurationBytes(byte[] value)
		{
			this.configuration_bytes = value;
		}

		// Token: 0x0400037F RID: 895
		private string application_base;

		// Token: 0x04000380 RID: 896
		private string application_name;

		// Token: 0x04000381 RID: 897
		private string cache_path;

		// Token: 0x04000382 RID: 898
		private string configuration_file;

		// Token: 0x04000383 RID: 899
		private string dynamic_base;

		// Token: 0x04000384 RID: 900
		private string license_file;

		// Token: 0x04000385 RID: 901
		private string private_bin_path;

		// Token: 0x04000386 RID: 902
		private string private_bin_path_probe;

		// Token: 0x04000387 RID: 903
		private string shadow_copy_directories;

		// Token: 0x04000388 RID: 904
		private string shadow_copy_files;

		// Token: 0x04000389 RID: 905
		private bool publisher_policy;

		// Token: 0x0400038A RID: 906
		private bool path_changed;

		// Token: 0x0400038B RID: 907
		private LoaderOptimization loader_optimization;

		// Token: 0x0400038C RID: 908
		private bool disallow_binding_redirects;

		// Token: 0x0400038D RID: 909
		private bool disallow_code_downloads;

		// Token: 0x0400038E RID: 910
		private ActivationArguments _activationArguments;

		// Token: 0x0400038F RID: 911
		private AppDomainInitializer domain_initializer;

		// Token: 0x04000390 RID: 912
		[NonSerialized]
		private ApplicationTrust application_trust;

		// Token: 0x04000391 RID: 913
		private string[] domain_initializer_args;

		// Token: 0x04000392 RID: 914
		private SecurityElement application_trust_xml;

		// Token: 0x04000393 RID: 915
		private bool disallow_appbase_probe;

		// Token: 0x04000394 RID: 916
		private byte[] configuration_bytes;
	}
}
