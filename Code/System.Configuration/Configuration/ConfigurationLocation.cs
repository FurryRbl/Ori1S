using System;
using System.IO;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Represents a location element within a configuration file.</summary>
	// Token: 0x02000029 RID: 41
	public class ConfigurationLocation
	{
		// Token: 0x060001A2 RID: 418 RVA: 0x00005FA0 File Offset: 0x000041A0
		internal ConfigurationLocation()
		{
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00005FA8 File Offset: 0x000041A8
		internal ConfigurationLocation(string path, string xmlContent, Configuration parent, bool allowOverride)
		{
			if (!string.IsNullOrEmpty(path))
			{
				char c = path[0];
				if (c == '.' || c == '/' || c == ' ' || c == '\\')
				{
					throw new ConfigurationErrorsException("<location> path attribute must be a relative virtual path.  It cannot start with any of ' ' '.' '/' or '\\'.");
				}
				path = path.TrimEnd(ConfigurationLocation.pathTrimChars);
			}
			this.path = path;
			this.xmlContent = xmlContent;
			this.parent = parent;
			this.allowOverride = allowOverride;
		}

		/// <summary>Gets the relative path to the resource whose configuration settings are represented by this <see cref="T:System.Configuration.ConfigurationLocation" /> object.</summary>
		/// <returns>The relative path to the resource whose configuration settings are represented by this <see cref="T:System.Configuration.ConfigurationLocation" />.</returns>
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000603C File Offset: 0x0000423C
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00006044 File Offset: 0x00004244
		internal bool AllowOverride
		{
			get
			{
				return this.allowOverride;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x0000604C File Offset: 0x0000424C
		internal string XmlContent
		{
			get
			{
				return this.xmlContent;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00006054 File Offset: 0x00004254
		internal Configuration OpenedConfiguration
		{
			get
			{
				return this.configuration;
			}
		}

		/// <summary>Creates an instance of a Configuration object.</summary>
		/// <returns>A Configuration object.</returns>
		// Token: 0x060001A9 RID: 425 RVA: 0x0000605C File Offset: 0x0000425C
		public Configuration OpenConfiguration()
		{
			if (this.configuration == null)
			{
				if (!this.parentResolved)
				{
					Configuration parentWithFile = this.parent.GetParentWithFile();
					if (parentWithFile != null)
					{
						string configPathFromLocationSubPath = this.parent.ConfigHost.GetConfigPathFromLocationSubPath(this.parent.LocationConfigPath, this.path);
						this.parent = parentWithFile.FindLocationConfiguration(configPathFromLocationSubPath, this.parent);
					}
				}
				this.configuration = new Configuration(this.parent, this.path);
				using (XmlTextReader xmlTextReader = new ConfigXmlTextReader(new StringReader(this.xmlContent), this.path))
				{
					this.configuration.ReadData(xmlTextReader, this.allowOverride);
				}
				this.xmlContent = null;
			}
			return this.configuration;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00006144 File Offset: 0x00004344
		internal void SetParentConfiguration(Configuration parent)
		{
			this.parentResolved = true;
			this.parent = parent;
			if (this.configuration != null)
			{
				this.configuration.Parent = parent;
			}
		}

		// Token: 0x0400007D RID: 125
		private static readonly char[] pathTrimChars = new char[]
		{
			'/'
		};

		// Token: 0x0400007E RID: 126
		private string path;

		// Token: 0x0400007F RID: 127
		private Configuration configuration;

		// Token: 0x04000080 RID: 128
		private Configuration parent;

		// Token: 0x04000081 RID: 129
		private string xmlContent;

		// Token: 0x04000082 RID: 130
		private bool parentResolved;

		// Token: 0x04000083 RID: 131
		private bool allowOverride;
	}
}
