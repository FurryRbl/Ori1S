using System;

namespace System.Configuration
{
	/// <summary>Contains metadata about an individual section within the configuration hierarchy. This class cannot be inherited.</summary>
	// Token: 0x0200006F RID: 111
	public sealed class SectionInformation
	{
		// Token: 0x060003D6 RID: 982 RVA: 0x0000B540 File Offset: 0x00009740
		[MonoTODO("default value for require_permission")]
		internal SectionInformation()
		{
			this.allow_definition = ConfigurationAllowDefinition.Everywhere;
			this.allow_location = true;
			this.allow_override = true;
			this.inherit_on_child_apps = true;
			this.restart_on_external_changes = true;
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x0000B590 File Offset: 0x00009790
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x0000B598 File Offset: 0x00009798
		internal string ConfigFilePath { get; set; }

		/// <summary>Gets or sets a value that indicates where in the configuration file hierarchy the associated configuration section can be defined. </summary>
		/// <returns>A value that indicates where in the configuration file hierarchy the associated <see cref="T:System.Configuration.ConfigurationSection" /> object can be declared.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The selected value conflicts with a value that is already defined.</exception>
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0000B5A4 File Offset: 0x000097A4
		// (set) Token: 0x060003DA RID: 986 RVA: 0x0000B5AC File Offset: 0x000097AC
		public ConfigurationAllowDefinition AllowDefinition
		{
			get
			{
				return this.allow_definition;
			}
			set
			{
				this.allow_definition = value;
			}
		}

		/// <summary>Gets or sets a value that indicates where in the configuration file hierarchy the associated configuration section can be declared.</summary>
		/// <returns>A value that indicates where in the configuration file hierarchy the associated <see cref="T:System.Configuration.ConfigurationSection" /> object can be declared for .exe files.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The selected value conflicts with a value that is already defined.</exception>
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0000B5B8 File Offset: 0x000097B8
		// (set) Token: 0x060003DC RID: 988 RVA: 0x0000B5C0 File Offset: 0x000097C0
		public ConfigurationAllowExeDefinition AllowExeDefinition
		{
			get
			{
				return this.allow_exe_definition;
			}
			set
			{
				this.allow_exe_definition = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the configuration section allows the location attribute.</summary>
		/// <returns>true if the location attribute is allowed; otherwise, false. The default is true.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The selected value conflicts with a value that is already defined.</exception>
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060003DD RID: 989 RVA: 0x0000B5CC File Offset: 0x000097CC
		// (set) Token: 0x060003DE RID: 990 RVA: 0x0000B5D4 File Offset: 0x000097D4
		public bool AllowLocation
		{
			get
			{
				return this.allow_location;
			}
			set
			{
				this.allow_location = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the associated configuration section can be overridden by lower-level configuration files.</summary>
		/// <returns>true if the section can be overridden; otherwise, false. The default is false.</returns>
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0000B5E0 File Offset: 0x000097E0
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x0000B5E8 File Offset: 0x000097E8
		public bool AllowOverride
		{
			get
			{
				return this.allow_override;
			}
			set
			{
				this.allow_override = value;
			}
		}

		/// <summary>Gets or sets the name of the include file in which the associated configuration section is defined, if such a file exists.</summary>
		/// <returns>The name of the include file in which the associated <see cref="T:System.Configuration.ConfigurationSection" /> is defined, if such a file exists; otherwise, an empty string ("").</returns>
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0000B5F4 File Offset: 0x000097F4
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x0000B5FC File Offset: 0x000097FC
		public string ConfigSource
		{
			get
			{
				return this.config_source;
			}
			set
			{
				this.config_source = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the associated configuration section will be saved even if it has not been modified.</summary>
		/// <returns>true if the associated <see cref="T:System.Configuration.ConfigurationSection" /> object will be saved even if it has not been modified; otherwise, false. The default is false.</returns>
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0000B608 File Offset: 0x00009808
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x0000B610 File Offset: 0x00009810
		public bool ForceSave
		{
			get
			{
				return this.force_update;
			}
			set
			{
				this.force_update = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the settings that are specified in the associated configuration section are inherited by applications that reside in a subdirectory of the relevant application.</summary>
		/// <returns>true if the settings specified in this <see cref="T:System.Configuration.ConfigurationSection" /> object are inherited by child applications; otherwise, false. The default is true.</returns>
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0000B61C File Offset: 0x0000981C
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x0000B624 File Offset: 0x00009824
		public bool InheritInChildApplications
		{
			get
			{
				return this.inherit_on_child_apps;
			}
			set
			{
				this.inherit_on_child_apps = value;
			}
		}

		/// <summary>Gets a value that indicates whether the configuration section must be declared in the configuration file.</summary>
		/// <returns>true if the associated <see cref="T:System.Configuration.ConfigurationSection" /> object must be declared in the configuration file; otherwise, false.</returns>
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000B630 File Offset: 0x00009830
		[MonoTODO]
		public bool IsDeclarationRequired
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value that indicates whether the associated configuration section is declared in the configuration file.</summary>
		/// <returns>true if this <see cref="T:System.Configuration.ConfigurationSection" /> is declared in the configuration file; otherwise, false. The default is true.</returns>
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0000B638 File Offset: 0x00009838
		[MonoTODO]
		public bool IsDeclared
		{
			get
			{
				return this.is_declared;
			}
		}

		/// <summary>Gets a value that indicates whether the associated configuration section is locked.</summary>
		/// <returns>true if the section is locked; otherwise, false. </returns>
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000B640 File Offset: 0x00009840
		[MonoTODO]
		public bool IsLocked
		{
			get
			{
				return this.is_locked;
			}
		}

		/// <summary>Gets a value that indicates whether the associated configuration section is protected.</summary>
		/// <returns>true if this <see cref="T:System.Configuration.ConfigurationSection" /> is protected; otherwise, false. The default is false.</returns>
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x0000B648 File Offset: 0x00009848
		public bool IsProtected
		{
			get
			{
				return this.protection_provider != null;
			}
		}

		/// <summary>Gets the name of the associated configuration section.</summary>
		/// <returns>The complete name of the configuration section.</returns>
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000B658 File Offset: 0x00009858
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the protected configuration provider for the associated configuration section.</summary>
		/// <returns>The protected configuration provider for this <see cref="T:System.Configuration.ConfigurationSection" /> object.</returns>
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x0000B660 File Offset: 0x00009860
		public ProtectedConfigurationProvider ProtectionProvider
		{
			get
			{
				return this.protection_provider;
			}
		}

		/// <summary>Gets a value that indicates whether the associated configuration section requires access permissions.</summary>
		/// <returns>true if the requirePermission attribute is set to true; otherwise, false. The default is true.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The selected value conflicts with a value that is already defined.</exception>
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000B668 File Offset: 0x00009868
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x0000B670 File Offset: 0x00009870
		[MonoTODO]
		public bool RequirePermission
		{
			get
			{
				return this.require_permission;
			}
			set
			{
				this.require_permission = value;
			}
		}

		/// <summary>Gets or sets a value that specifies whether a change in an external configuration include file requires an application restart.</summary>
		/// <returns>true if a change in an external configuration include file requires an application restart; otherwise, false. The default is true.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The selected value conflicts with a value that is already defined.</exception>
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000B67C File Offset: 0x0000987C
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x0000B684 File Offset: 0x00009884
		[MonoTODO]
		public bool RestartOnExternalChanges
		{
			get
			{
				return this.restart_on_external_changes;
			}
			set
			{
				this.restart_on_external_changes = value;
			}
		}

		/// <summary>Gets the name of the associated configuration section.</summary>
		/// <returns>The name of the associated <see cref="T:System.Configuration.ConfigurationSection" /> object.</returns>
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0000B690 File Offset: 0x00009890
		[MonoTODO]
		public string SectionName
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets or sets the section class name.</summary>
		/// <returns>The name of the class that is associated with this <see cref="T:System.Configuration.ConfigurationSection" /> section.</returns>
		/// <exception cref="T:System.ArgumentException">The selected value is null or an empty string ("").</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The selected value conflicts with a value that is already defined.</exception>
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0000B698 File Offset: 0x00009898
		// (set) Token: 0x060003F3 RID: 1011 RVA: 0x0000B6A0 File Offset: 0x000098A0
		public string Type
		{
			get
			{
				return this.type_name;
			}
			set
			{
				this.type_name = value;
			}
		}

		/// <summary>Gets the configuration section that contains the configuration section associated with this object.</summary>
		/// <returns>The configuration section that contains the <see cref="T:System.Configuration.ConfigurationSection" /> that is associated with this <see cref="T:System.Configuration.SectionInformation" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The method is invoked from a parent section.</exception>
		// Token: 0x060003F4 RID: 1012 RVA: 0x0000B6AC File Offset: 0x000098AC
		public ConfigurationSection GetParentSection()
		{
			return this.parent;
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000B6B4 File Offset: 0x000098B4
		internal void SetParentSection(ConfigurationSection parent)
		{
			this.parent = parent;
		}

		/// <summary>Returns an XML node object that represents the associated configuration-section object.</summary>
		/// <returns>The XML representation for this configuration section.</returns>
		/// <exception cref="T:System.InvalidOperationException">This configuration object is locked and cannot be edited.</exception>
		// Token: 0x060003F6 RID: 1014 RVA: 0x0000B6C0 File Offset: 0x000098C0
		public string GetRawXml()
		{
			return this.raw_xml;
		}

		/// <summary>Marks a configuration section for protection. </summary>
		/// <param name="protectionProvider">The name of the protection provider to use.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Configuration.SectionInformation.AllowLocation" /> property is set to false.- or -The target section is already a protected data section.</exception>
		// Token: 0x060003F7 RID: 1015 RVA: 0x0000B6C8 File Offset: 0x000098C8
		public void ProtectSection(string provider)
		{
			this.protection_provider = ProtectedConfiguration.GetProvider(provider, true);
		}

		/// <summary>Forces the associated configuration section to appear in the configuration file, or removes an existing section from the configuration file.</summary>
		/// <param name="force">true if the associated section should be written in the configuration file; otherwise, false.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">
		///   <paramref name="force" /> is true and the associated section cannot be exported to the child configuration file, or it is undeclared.</exception>
		// Token: 0x060003F8 RID: 1016 RVA: 0x0000B6D8 File Offset: 0x000098D8
		[MonoTODO]
		public void ForceDeclaration(bool require)
		{
		}

		/// <summary>Forces the associated configuration section to appear in the configuration file.</summary>
		// Token: 0x060003F9 RID: 1017 RVA: 0x0000B6DC File Offset: 0x000098DC
		public void ForceDeclaration()
		{
			this.ForceDeclaration(true);
		}

		/// <summary>Causes the associated configuration section to inherit all its values from the parent section.</summary>
		/// <exception cref="T:System.InvalidOperationException">This method cannot be called outside editing mode.</exception>
		// Token: 0x060003FA RID: 1018 RVA: 0x0000B6E8 File Offset: 0x000098E8
		[MonoTODO]
		public void RevertToParent()
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes the protected configuration encryption from the associated configuration section.</summary>
		// Token: 0x060003FB RID: 1019 RVA: 0x0000B6F0 File Offset: 0x000098F0
		public void UnprotectSection()
		{
			this.protection_provider = null;
		}

		/// <summary>Sets the object to an XML representation of the associated configuration section within the configuration file.</summary>
		/// <param name="rawXml">The XML to use.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rawXml" /> is null.</exception>
		// Token: 0x060003FC RID: 1020 RVA: 0x0000B6FC File Offset: 0x000098FC
		public void SetRawXml(string xml)
		{
			this.raw_xml = xml;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000B708 File Offset: 0x00009908
		[MonoTODO]
		internal void SetName(string name)
		{
			this.name = name;
		}

		// Token: 0x0400012D RID: 301
		private ConfigurationSection parent;

		// Token: 0x0400012E RID: 302
		private ConfigurationAllowDefinition allow_definition = ConfigurationAllowDefinition.Everywhere;

		// Token: 0x0400012F RID: 303
		private ConfigurationAllowExeDefinition allow_exe_definition = ConfigurationAllowExeDefinition.MachineToApplication;

		// Token: 0x04000130 RID: 304
		private bool allow_location;

		// Token: 0x04000131 RID: 305
		private bool allow_override;

		// Token: 0x04000132 RID: 306
		private bool inherit_on_child_apps;

		// Token: 0x04000133 RID: 307
		private bool restart_on_external_changes;

		// Token: 0x04000134 RID: 308
		private bool require_permission;

		// Token: 0x04000135 RID: 309
		private string config_source;

		// Token: 0x04000136 RID: 310
		private bool force_update;

		// Token: 0x04000137 RID: 311
		private bool is_declared;

		// Token: 0x04000138 RID: 312
		private bool is_locked;

		// Token: 0x04000139 RID: 313
		private string name;

		// Token: 0x0400013A RID: 314
		private string type_name;

		// Token: 0x0400013B RID: 315
		private string raw_xml;

		// Token: 0x0400013C RID: 316
		private ProtectedConfigurationProvider protection_provider;
	}
}
