using System;

namespace System.Configuration
{
	/// <summary>Represents a group of related sections within a configuration file.</summary>
	// Token: 0x02000038 RID: 56
	public class ConfigurationSectionGroup
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000231 RID: 561 RVA: 0x000078AC File Offset: 0x00005AAC
		private Configuration Config
		{
			get
			{
				if (this.config == null)
				{
					throw new InvalidOperationException("ConfigurationSectionGroup cannot be edited until it is added to a Configuration instance as its descendant");
				}
				return this.config;
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x000078CC File Offset: 0x00005ACC
		internal void Initialize(Configuration config, SectionGroupInfo group)
		{
			if (this.initialized)
			{
				throw new SystemException("INTERNAL ERROR: this configuration section is being initialized twice: " + base.GetType());
			}
			this.initialized = true;
			this.config = config;
			this.group = group;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00007910 File Offset: 0x00005B10
		internal void SetName(string name)
		{
			this.name = name;
		}

		/// <summary>Forces the declaration for this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object.</summary>
		/// <param name="force">true if the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object must be written to the file; otherwise, false.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object is the root section group.- or -The <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object has a location.</exception>
		// Token: 0x06000234 RID: 564 RVA: 0x0000791C File Offset: 0x00005B1C
		[MonoTODO]
		public void ForceDeclaration(bool require)
		{
			this.require_declaration = require;
		}

		/// <summary>Forces the declaration for this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object.</summary>
		// Token: 0x06000235 RID: 565 RVA: 0x00007928 File Offset: 0x00005B28
		public void ForceDeclaration()
		{
			this.ForceDeclaration(true);
		}

		/// <summary>Gets a value that indicates whether this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object is declared.</summary>
		/// <returns>true if this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> is declared; otherwise, false. The default is false.</returns>
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00007934 File Offset: 0x00005B34
		public bool IsDeclared
		{
			get
			{
				return this.declared;
			}
		}

		/// <summary>Gets a value that indicates whether this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object declaration is required. </summary>
		/// <returns>true if this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> declaration is required; otherwise, false.</returns>
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000793C File Offset: 0x00005B3C
		[MonoTODO]
		public bool IsDeclarationRequired
		{
			get
			{
				return this.require_declaration;
			}
		}

		/// <summary>Gets the name property of this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object.</summary>
		/// <returns>The name property of this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object.</returns>
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00007944 File Offset: 0x00005B44
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the section group name associated with this <see cref="T:System.Configuration.ConfigurationSectionGroup" />.</summary>
		/// <returns>The section group name of this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object.</returns>
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000794C File Offset: 0x00005B4C
		[MonoInternalNote("Check if this is correct")]
		public string SectionGroupName
		{
			get
			{
				return this.group.XPath;
			}
		}

		/// <summary>Gets a <see cref="T:System.Configuration.ConfigurationSectionGroupCollection" /> object that contains all the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> objects that are children of this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConfigurationSectionGroupCollection" /> object that contains all the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> objects that are children of this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object.</returns>
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000795C File Offset: 0x00005B5C
		public ConfigurationSectionGroupCollection SectionGroups
		{
			get
			{
				if (this.groups == null)
				{
					this.groups = new ConfigurationSectionGroupCollection(this.Config, this.group);
				}
				return this.groups;
			}
		}

		/// <summary>Gets a <see cref="T:System.Configuration.ConfigurationSectionCollection" /> object that contains all of <see cref="T:System.Configuration.ConfigurationSection" /> objects within this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConfigurationSectionCollection" /> object that contains all the <see cref="T:System.Configuration.ConfigurationSection" /> objects within this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object.</returns>
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00007994 File Offset: 0x00005B94
		public ConfigurationSectionCollection Sections
		{
			get
			{
				if (this.sections == null)
				{
					this.sections = new ConfigurationSectionCollection(this.Config, this.group);
				}
				return this.sections;
			}
		}

		/// <summary>Gets or sets the type for this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object.</summary>
		/// <returns>The type of this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object is the root section group.- or -The <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object has a location.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The section or group is already defined at another level.</exception>
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600023C RID: 572 RVA: 0x000079CC File Offset: 0x00005BCC
		// (set) Token: 0x0600023D RID: 573 RVA: 0x000079D4 File Offset: 0x00005BD4
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

		// Token: 0x040000B4 RID: 180
		private bool require_declaration;

		// Token: 0x040000B5 RID: 181
		private bool declared;

		// Token: 0x040000B6 RID: 182
		private string name;

		// Token: 0x040000B7 RID: 183
		private string type_name;

		// Token: 0x040000B8 RID: 184
		private ConfigurationSectionCollection sections;

		// Token: 0x040000B9 RID: 185
		private ConfigurationSectionGroupCollection groups;

		// Token: 0x040000BA RID: 186
		private Configuration config;

		// Token: 0x040000BB RID: 187
		private SectionGroupInfo group;

		// Token: 0x040000BC RID: 188
		private bool initialized;
	}
}
