using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x020001DB RID: 475
	internal class CustomizableFileSettingsProvider : SettingsProvider, IApplicationSettingsProvider
	{
		// Token: 0x06001074 RID: 4212 RVA: 0x0002C1A0 File Offset: 0x0002A3A0
		public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
		{
			base.Initialize(name, config);
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06001075 RID: 4213 RVA: 0x0002C1AC File Offset: 0x0002A3AC
		internal static string UserRoamingFullPath
		{
			get
			{
				return Path.Combine(CustomizableFileSettingsProvider.userRoamingPath, CustomizableFileSettingsProvider.userRoamingName);
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x0002C1C0 File Offset: 0x0002A3C0
		internal static string UserLocalFullPath
		{
			get
			{
				return Path.Combine(CustomizableFileSettingsProvider.userLocalPath, CustomizableFileSettingsProvider.userLocalName);
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06001077 RID: 4215 RVA: 0x0002C1D4 File Offset: 0x0002A3D4
		public static string PrevUserRoamingFullPath
		{
			get
			{
				return Path.Combine(CustomizableFileSettingsProvider.userRoamingPathPrevVersion, CustomizableFileSettingsProvider.userRoamingName);
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06001078 RID: 4216 RVA: 0x0002C1E8 File Offset: 0x0002A3E8
		public static string PrevUserLocalFullPath
		{
			get
			{
				return Path.Combine(CustomizableFileSettingsProvider.userLocalPathPrevVersion, CustomizableFileSettingsProvider.userLocalName);
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06001079 RID: 4217 RVA: 0x0002C1FC File Offset: 0x0002A3FC
		public static string UserRoamingPath
		{
			get
			{
				return CustomizableFileSettingsProvider.userRoamingPath;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x0600107A RID: 4218 RVA: 0x0002C204 File Offset: 0x0002A404
		public static string UserLocalPath
		{
			get
			{
				return CustomizableFileSettingsProvider.userLocalPath;
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x0600107B RID: 4219 RVA: 0x0002C20C File Offset: 0x0002A40C
		public static string UserRoamingName
		{
			get
			{
				return CustomizableFileSettingsProvider.userRoamingName;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x0600107C RID: 4220 RVA: 0x0002C214 File Offset: 0x0002A414
		public static string UserLocalName
		{
			get
			{
				return CustomizableFileSettingsProvider.userLocalName;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x0600107D RID: 4221 RVA: 0x0002C21C File Offset: 0x0002A41C
		// (set) Token: 0x0600107E RID: 4222 RVA: 0x0002C224 File Offset: 0x0002A424
		public static UserConfigLocationOption UserConfigSelector
		{
			get
			{
				return CustomizableFileSettingsProvider.userConfig;
			}
			set
			{
				CustomizableFileSettingsProvider.userConfig = value;
				if ((CustomizableFileSettingsProvider.userConfig & UserConfigLocationOption.Other) != (UserConfigLocationOption)0U)
				{
					CustomizableFileSettingsProvider.isVersionMajor = false;
					CustomizableFileSettingsProvider.isVersionMinor = false;
					CustomizableFileSettingsProvider.isVersionBuild = false;
					CustomizableFileSettingsProvider.isVersionRevision = false;
					CustomizableFileSettingsProvider.isCompany = false;
					return;
				}
				CustomizableFileSettingsProvider.isVersionRevision = ((CustomizableFileSettingsProvider.userConfig & (UserConfigLocationOption)8U) != (UserConfigLocationOption)0U);
				CustomizableFileSettingsProvider.isVersionBuild = (CustomizableFileSettingsProvider.isVersionRevision | (CustomizableFileSettingsProvider.userConfig & (UserConfigLocationOption)4U) != (UserConfigLocationOption)0U);
				CustomizableFileSettingsProvider.isVersionMinor = (CustomizableFileSettingsProvider.isVersionBuild | (CustomizableFileSettingsProvider.userConfig & (UserConfigLocationOption)2U) != (UserConfigLocationOption)0U);
				CustomizableFileSettingsProvider.isVersionMajor = (CustomizableFileSettingsProvider.IsVersionMinor | (CustomizableFileSettingsProvider.userConfig & (UserConfigLocationOption)1U) != (UserConfigLocationOption)0U);
				CustomizableFileSettingsProvider.isCompany = ((CustomizableFileSettingsProvider.userConfig & (UserConfigLocationOption)16U) != (UserConfigLocationOption)0U);
				CustomizableFileSettingsProvider.isProduct = ((CustomizableFileSettingsProvider.userConfig & UserConfigLocationOption.Product) != (UserConfigLocationOption)0U);
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x0600107F RID: 4223 RVA: 0x0002C2E8 File Offset: 0x0002A4E8
		// (set) Token: 0x06001080 RID: 4224 RVA: 0x0002C2F0 File Offset: 0x0002A4F0
		public static bool IsVersionMajor
		{
			get
			{
				return CustomizableFileSettingsProvider.isVersionMajor;
			}
			set
			{
				CustomizableFileSettingsProvider.isVersionMajor = value;
				CustomizableFileSettingsProvider.isVersionMinor = false;
				CustomizableFileSettingsProvider.isVersionBuild = false;
				CustomizableFileSettingsProvider.isVersionRevision = false;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06001081 RID: 4225 RVA: 0x0002C30C File Offset: 0x0002A50C
		// (set) Token: 0x06001082 RID: 4226 RVA: 0x0002C314 File Offset: 0x0002A514
		public static bool IsVersionMinor
		{
			get
			{
				return CustomizableFileSettingsProvider.isVersionMinor;
			}
			set
			{
				CustomizableFileSettingsProvider.isVersionMinor = value;
				if (CustomizableFileSettingsProvider.isVersionMinor)
				{
					CustomizableFileSettingsProvider.isVersionMajor = true;
				}
				CustomizableFileSettingsProvider.isVersionBuild = false;
				CustomizableFileSettingsProvider.isVersionRevision = false;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06001083 RID: 4227 RVA: 0x0002C344 File Offset: 0x0002A544
		// (set) Token: 0x06001084 RID: 4228 RVA: 0x0002C34C File Offset: 0x0002A54C
		public static bool IsVersionBuild
		{
			get
			{
				return CustomizableFileSettingsProvider.isVersionBuild;
			}
			set
			{
				CustomizableFileSettingsProvider.isVersionBuild = value;
				if (CustomizableFileSettingsProvider.isVersionBuild)
				{
					CustomizableFileSettingsProvider.isVersionMajor = true;
					CustomizableFileSettingsProvider.isVersionMinor = true;
				}
				CustomizableFileSettingsProvider.isVersionRevision = false;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06001085 RID: 4229 RVA: 0x0002C37C File Offset: 0x0002A57C
		// (set) Token: 0x06001086 RID: 4230 RVA: 0x0002C384 File Offset: 0x0002A584
		public static bool IsVersionRevision
		{
			get
			{
				return CustomizableFileSettingsProvider.isVersionRevision;
			}
			set
			{
				CustomizableFileSettingsProvider.isVersionRevision = value;
				if (CustomizableFileSettingsProvider.isVersionRevision)
				{
					CustomizableFileSettingsProvider.isVersionMajor = true;
					CustomizableFileSettingsProvider.isVersionMinor = true;
					CustomizableFileSettingsProvider.isVersionBuild = true;
				}
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06001087 RID: 4231 RVA: 0x0002C3B4 File Offset: 0x0002A5B4
		// (set) Token: 0x06001088 RID: 4232 RVA: 0x0002C3BC File Offset: 0x0002A5BC
		public static bool IsCompany
		{
			get
			{
				return CustomizableFileSettingsProvider.isCompany;
			}
			set
			{
				CustomizableFileSettingsProvider.isCompany = value;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06001089 RID: 4233 RVA: 0x0002C3C4 File Offset: 0x0002A5C4
		// (set) Token: 0x0600108A RID: 4234 RVA: 0x0002C3CC File Offset: 0x0002A5CC
		public static bool IsEvidence
		{
			get
			{
				return CustomizableFileSettingsProvider.isEvidence;
			}
			set
			{
				CustomizableFileSettingsProvider.isEvidence = value;
			}
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x0002C3D4 File Offset: 0x0002A5D4
		private static string GetCompanyName()
		{
			Assembly assembly = Assembly.GetEntryAssembly();
			if (assembly == null)
			{
				assembly = Assembly.GetCallingAssembly();
			}
			AssemblyCompanyAttribute[] array = (AssemblyCompanyAttribute[])assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), true);
			if (array != null && array.Length > 0)
			{
				return array[0].Company;
			}
			MethodInfo entryPoint = assembly.EntryPoint;
			Type type = (entryPoint == null) ? null : entryPoint.DeclaringType;
			if (type != null && !string.IsNullOrEmpty(type.Namespace))
			{
				int num = type.Namespace.IndexOf('.');
				return (num >= 0) ? type.Namespace.Substring(0, num) : type.Namespace;
			}
			return "Program";
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x0002C488 File Offset: 0x0002A688
		private static string GetProductName()
		{
			Assembly assembly = Assembly.GetEntryAssembly();
			if (assembly == null)
			{
				assembly = Assembly.GetCallingAssembly();
			}
			byte[] publicKeyToken = assembly.GetName().GetPublicKeyToken();
			return string.Format("{0}_{1}_{2}", AppDomain.CurrentDomain.FriendlyName, (publicKeyToken == null) ? "Url" : "StrongName", CustomizableFileSettingsProvider.GetEvidenceHash());
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x0002C4E4 File Offset: 0x0002A6E4
		private static string GetEvidenceHash()
		{
			Assembly assembly = Assembly.GetEntryAssembly();
			if (assembly == null)
			{
				assembly = Assembly.GetCallingAssembly();
			}
			byte[] publicKeyToken = assembly.GetName().GetPublicKeyToken();
			byte[] inArray = SHA1.Create().ComputeHash((publicKeyToken == null) ? Encoding.UTF8.GetBytes(assembly.EscapedCodeBase) : publicKeyToken);
			return Convert.ToBase64String(inArray);
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x0002C53C File Offset: 0x0002A73C
		private static string GetProductVersion()
		{
			Assembly assembly = Assembly.GetEntryAssembly();
			if (assembly == null)
			{
				assembly = Assembly.GetCallingAssembly();
			}
			if (assembly == null)
			{
				return string.Empty;
			}
			return assembly.GetName().Version.ToString();
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x0002C578 File Offset: 0x0002A778
		private static void CreateUserConfigPath()
		{
			if (CustomizableFileSettingsProvider.userDefine)
			{
				return;
			}
			if (CustomizableFileSettingsProvider.ProductName == string.Empty)
			{
				CustomizableFileSettingsProvider.ProductName = CustomizableFileSettingsProvider.GetProductName();
			}
			if (CustomizableFileSettingsProvider.CompanyName == string.Empty)
			{
				CustomizableFileSettingsProvider.CompanyName = CustomizableFileSettingsProvider.GetCompanyName();
			}
			if (CustomizableFileSettingsProvider.ForceVersion == string.Empty)
			{
				CustomizableFileSettingsProvider.ProductVersion = CustomizableFileSettingsProvider.GetProductVersion().Split(new char[]
				{
					'.'
				});
			}
			if (CustomizableFileSettingsProvider.userRoamingBasePath == string.Empty)
			{
				CustomizableFileSettingsProvider.userRoamingPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			}
			else
			{
				CustomizableFileSettingsProvider.userRoamingPath = CustomizableFileSettingsProvider.userRoamingBasePath;
			}
			if (CustomizableFileSettingsProvider.userLocalBasePath == string.Empty)
			{
				CustomizableFileSettingsProvider.userLocalPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			}
			else
			{
				CustomizableFileSettingsProvider.userLocalPath = CustomizableFileSettingsProvider.userLocalBasePath;
			}
			if (CustomizableFileSettingsProvider.isCompany)
			{
				CustomizableFileSettingsProvider.userRoamingPath = Path.Combine(CustomizableFileSettingsProvider.userRoamingPath, CustomizableFileSettingsProvider.CompanyName);
				CustomizableFileSettingsProvider.userLocalPath = Path.Combine(CustomizableFileSettingsProvider.userLocalPath, CustomizableFileSettingsProvider.CompanyName);
			}
			if (CustomizableFileSettingsProvider.isProduct)
			{
				if (CustomizableFileSettingsProvider.isEvidence)
				{
					Assembly assembly = Assembly.GetEntryAssembly();
					if (assembly == null)
					{
						assembly = Assembly.GetCallingAssembly();
					}
					byte[] publicKeyToken = assembly.GetName().GetPublicKeyToken();
					CustomizableFileSettingsProvider.ProductName = string.Format("{0}_{1}_{2}", CustomizableFileSettingsProvider.ProductName, (publicKeyToken == null) ? "Url" : "StrongName", CustomizableFileSettingsProvider.GetEvidenceHash());
				}
				CustomizableFileSettingsProvider.userRoamingPath = Path.Combine(CustomizableFileSettingsProvider.userRoamingPath, CustomizableFileSettingsProvider.ProductName);
				CustomizableFileSettingsProvider.userLocalPath = Path.Combine(CustomizableFileSettingsProvider.userLocalPath, CustomizableFileSettingsProvider.ProductName);
			}
			string text;
			if (CustomizableFileSettingsProvider.ForceVersion == string.Empty)
			{
				if (CustomizableFileSettingsProvider.isVersionRevision)
				{
					text = string.Format("{0}.{1}.{2}.{3}", new object[]
					{
						CustomizableFileSettingsProvider.ProductVersion[0],
						CustomizableFileSettingsProvider.ProductVersion[1],
						CustomizableFileSettingsProvider.ProductVersion[2],
						CustomizableFileSettingsProvider.ProductVersion[3]
					});
				}
				else if (CustomizableFileSettingsProvider.isVersionBuild)
				{
					text = string.Format("{0}.{1}.{2}", CustomizableFileSettingsProvider.ProductVersion[0], CustomizableFileSettingsProvider.ProductVersion[1], CustomizableFileSettingsProvider.ProductVersion[2]);
				}
				else if (CustomizableFileSettingsProvider.isVersionMinor)
				{
					text = string.Format("{0}.{1}", CustomizableFileSettingsProvider.ProductVersion[0], CustomizableFileSettingsProvider.ProductVersion[1]);
				}
				else if (CustomizableFileSettingsProvider.isVersionMajor)
				{
					text = CustomizableFileSettingsProvider.ProductVersion[0];
				}
				else
				{
					text = string.Empty;
				}
			}
			else
			{
				text = CustomizableFileSettingsProvider.ForceVersion;
			}
			string text2 = CustomizableFileSettingsProvider.PrevVersionPath(CustomizableFileSettingsProvider.userRoamingPath, text);
			string text3 = CustomizableFileSettingsProvider.PrevVersionPath(CustomizableFileSettingsProvider.userLocalPath, text);
			CustomizableFileSettingsProvider.userRoamingPath = Path.Combine(CustomizableFileSettingsProvider.userRoamingPath, text);
			CustomizableFileSettingsProvider.userLocalPath = Path.Combine(CustomizableFileSettingsProvider.userLocalPath, text);
			if (text2 != string.Empty)
			{
				CustomizableFileSettingsProvider.userRoamingPathPrevVersion = Path.Combine(CustomizableFileSettingsProvider.userRoamingPath, text2);
			}
			if (text3 != string.Empty)
			{
				CustomizableFileSettingsProvider.userLocalPathPrevVersion = Path.Combine(CustomizableFileSettingsProvider.userLocalPath, text3);
			}
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x0002C868 File Offset: 0x0002AA68
		private static string PrevVersionPath(string dirName, string currentVersion)
		{
			string text = string.Empty;
			if (!Directory.Exists(dirName))
			{
				return text;
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(dirName);
			foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
			{
				if (string.Compare(currentVersion, directoryInfo2.Name, StringComparison.Ordinal) > 0 && string.Compare(text, directoryInfo2.Name, StringComparison.Ordinal) < 0)
				{
					text = directoryInfo2.Name;
				}
			}
			return text;
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x0002C8E4 File Offset: 0x0002AAE4
		public static bool SetUserRoamingPath(string configPath)
		{
			if (CustomizableFileSettingsProvider.CheckPath(configPath))
			{
				CustomizableFileSettingsProvider.userRoamingBasePath = configPath;
				return true;
			}
			return false;
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x0002C8FC File Offset: 0x0002AAFC
		public static bool SetUserLocalPath(string configPath)
		{
			if (CustomizableFileSettingsProvider.CheckPath(configPath))
			{
				CustomizableFileSettingsProvider.userLocalBasePath = configPath;
				return true;
			}
			return false;
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x0002C914 File Offset: 0x0002AB14
		private static bool CheckFileName(string configFile)
		{
			return configFile.IndexOfAny(Path.GetInvalidFileNameChars()) < 0;
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x0002C924 File Offset: 0x0002AB24
		public static bool SetUserRoamingFileName(string configFile)
		{
			if (CustomizableFileSettingsProvider.CheckFileName(configFile))
			{
				CustomizableFileSettingsProvider.userRoamingName = configFile;
				return true;
			}
			return false;
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x0002C93C File Offset: 0x0002AB3C
		public static bool SetUserLocalFileName(string configFile)
		{
			if (CustomizableFileSettingsProvider.CheckFileName(configFile))
			{
				CustomizableFileSettingsProvider.userLocalName = configFile;
				return true;
			}
			return false;
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x0002C954 File Offset: 0x0002AB54
		public static bool SetCompanyName(string companyName)
		{
			if (CustomizableFileSettingsProvider.CheckFileName(companyName))
			{
				CustomizableFileSettingsProvider.CompanyName = companyName;
				return true;
			}
			return false;
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x0002C96C File Offset: 0x0002AB6C
		public static bool SetProductName(string productName)
		{
			if (CustomizableFileSettingsProvider.CheckFileName(productName))
			{
				CustomizableFileSettingsProvider.ProductName = productName;
				return true;
			}
			return false;
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x0002C984 File Offset: 0x0002AB84
		public static bool SetVersion(int major)
		{
			CustomizableFileSettingsProvider.ForceVersion = string.Format("{0}", major);
			return true;
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x0002C99C File Offset: 0x0002AB9C
		public static bool SetVersion(int major, int minor)
		{
			CustomizableFileSettingsProvider.ForceVersion = string.Format("{0}.{1}", major, minor);
			return true;
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x0002C9BC File Offset: 0x0002ABBC
		public static bool SetVersion(int major, int minor, int build)
		{
			CustomizableFileSettingsProvider.ForceVersion = string.Format("{0}.{1}.{2}", major, minor, build);
			return true;
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x0002C9EC File Offset: 0x0002ABEC
		public static bool SetVersion(int major, int minor, int build, int revision)
		{
			CustomizableFileSettingsProvider.ForceVersion = string.Format("{0}.{1}.{2}.{3}", new object[]
			{
				major,
				minor,
				build,
				revision
			});
			return true;
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x0002CA34 File Offset: 0x0002AC34
		public static bool SetVersion(string forceVersion)
		{
			if (CustomizableFileSettingsProvider.CheckFileName(forceVersion))
			{
				CustomizableFileSettingsProvider.ForceVersion = forceVersion;
				return true;
			}
			return false;
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x0002CA4C File Offset: 0x0002AC4C
		private static bool CheckPath(string configPath)
		{
			char[] invalidPathChars = Path.GetInvalidPathChars();
			if (configPath.IndexOfAny(invalidPathChars) >= 0)
			{
				return false;
			}
			string path = configPath;
			string fileName;
			while ((fileName = Path.GetFileName(path)) != string.Empty)
			{
				if (!CustomizableFileSettingsProvider.CheckFileName(fileName))
				{
					return false;
				}
				path = Path.GetDirectoryName(path);
			}
			return true;
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x0600109E RID: 4254 RVA: 0x0002CAA4 File Offset: 0x0002ACA4
		public override string Name
		{
			get
			{
				return base.Name;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x0600109F RID: 4255 RVA: 0x0002CAAC File Offset: 0x0002ACAC
		// (set) Token: 0x060010A0 RID: 4256 RVA: 0x0002CAB4 File Offset: 0x0002ACB4
		public override string ApplicationName
		{
			get
			{
				return this.app_name;
			}
			set
			{
				this.app_name = value;
			}
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x0002CAC0 File Offset: 0x0002ACC0
		private void SaveProperties(ExeConfigurationFileMap exeMap, SettingsPropertyValueCollection collection, ConfigurationUserLevel level, SettingsContext context, bool checkUserLevel)
		{
			Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(exeMap, level);
			UserSettingsGroup userSettingsGroup = configuration.GetSectionGroup("userSettings") as UserSettingsGroup;
			bool flag = level == ConfigurationUserLevel.PerUserRoaming;
			if (userSettingsGroup == null)
			{
				userSettingsGroup = new UserSettingsGroup();
				configuration.SectionGroups.Add("userSettings", userSettingsGroup);
				ApplicationSettingsBase currentSettings = context.CurrentSettings;
				ClientSettingsSection section = new ClientSettingsSection();
				userSettingsGroup.Sections.Add(((currentSettings == null) ? typeof(ApplicationSettingsBase) : currentSettings.GetType()).FullName, section);
			}
			bool flag2 = false;
			foreach (object obj in userSettingsGroup.Sections)
			{
				ConfigurationSection configurationSection = (ConfigurationSection)obj;
				ClientSettingsSection clientSettingsSection = configurationSection as ClientSettingsSection;
				if (clientSettingsSection != null)
				{
					foreach (object obj2 in collection)
					{
						SettingsPropertyValue settingsPropertyValue = (SettingsPropertyValue)obj2;
						if (!checkUserLevel || settingsPropertyValue.Property.Attributes.Contains(typeof(SettingsManageabilityAttribute)) == flag)
						{
							flag2 = true;
							SettingElement settingElement = clientSettingsSection.Settings.Get(settingsPropertyValue.Name);
							if (settingElement == null)
							{
								settingElement = new SettingElement(settingsPropertyValue.Name, settingsPropertyValue.Property.SerializeAs);
								clientSettingsSection.Settings.Add(settingElement);
							}
							if (settingElement.Value.ValueXml == null)
							{
								settingElement.Value.ValueXml = new XmlDocument().CreateElement("value");
							}
							switch (settingsPropertyValue.Property.SerializeAs)
							{
							case SettingsSerializeAs.String:
								settingElement.Value.ValueXml.InnerText = (settingsPropertyValue.SerializedValue as string);
								break;
							case SettingsSerializeAs.Xml:
								settingElement.Value.ValueXml.InnerXml = ((settingsPropertyValue.SerializedValue as string) ?? string.Empty);
								break;
							case SettingsSerializeAs.Binary:
								settingElement.Value.ValueXml.InnerText = ((settingsPropertyValue.SerializedValue == null) ? string.Empty : Convert.ToBase64String(settingsPropertyValue.SerializedValue as byte[]));
								break;
							default:
								throw new NotImplementedException();
							}
						}
					}
				}
			}
			if (flag2)
			{
				configuration.Save(ConfigurationSaveMode.Minimal, true);
			}
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x0002CD84 File Offset: 0x0002AF84
		private void LoadPropertyValue(SettingsPropertyCollection collection, SettingElement element, bool allowOverwrite)
		{
			SettingsProperty settingsProperty = collection[element.Name];
			if (settingsProperty == null)
			{
				settingsProperty = new SettingsProperty(element.Name);
				collection.Add(settingsProperty);
			}
			SettingsPropertyValue settingsPropertyValue = new SettingsPropertyValue(settingsProperty);
			settingsPropertyValue.IsDirty = false;
			if (element.Value.ValueXml != null)
			{
				switch (settingsPropertyValue.Property.SerializeAs)
				{
				case SettingsSerializeAs.String:
					settingsPropertyValue.SerializedValue = element.Value.ValueXml.InnerText;
					break;
				case SettingsSerializeAs.Xml:
					settingsPropertyValue.SerializedValue = element.Value.ValueXml.InnerXml;
					break;
				case SettingsSerializeAs.Binary:
					settingsPropertyValue.SerializedValue = Convert.FromBase64String(element.Value.ValueXml.InnerText);
					break;
				}
			}
			else
			{
				settingsPropertyValue.SerializedValue = settingsProperty.DefaultValue;
			}
			try
			{
				if (allowOverwrite)
				{
					this.values.Remove(element.Name);
				}
				this.values.Add(settingsPropertyValue);
			}
			catch (ArgumentException inner)
			{
				throw new ConfigurationErrorsException(string.Format(CultureInfo.InvariantCulture, "Failed to load value for '{0}'.", new object[]
				{
					element.Name
				}), inner);
			}
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x0002CED0 File Offset: 0x0002B0D0
		private void LoadProperties(ExeConfigurationFileMap exeMap, SettingsPropertyCollection collection, ConfigurationUserLevel level, string sectionGroupName, bool allowOverwrite, string groupName)
		{
			Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(exeMap, level);
			ConfigurationSectionGroup sectionGroup = configuration.GetSectionGroup(sectionGroupName);
			if (sectionGroup != null)
			{
				foreach (object obj in sectionGroup.Sections)
				{
					ConfigurationSection configurationSection = (ConfigurationSection)obj;
					if (!(configurationSection.SectionInformation.Name != groupName))
					{
						ClientSettingsSection clientSettingsSection = configurationSection as ClientSettingsSection;
						if (clientSettingsSection != null)
						{
							foreach (object obj2 in clientSettingsSection.Settings)
							{
								SettingElement element = (SettingElement)obj2;
								this.LoadPropertyValue(collection, element, allowOverwrite);
							}
							break;
						}
					}
				}
			}
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x0002CFF4 File Offset: 0x0002B1F4
		public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection)
		{
			this.CreateExeMap();
			if (CustomizableFileSettingsProvider.UserLocalFullPath == CustomizableFileSettingsProvider.UserRoamingFullPath)
			{
				this.SaveProperties(this.exeMapCurrent, collection, ConfigurationUserLevel.PerUserRoaming, context, false);
			}
			else
			{
				this.SaveProperties(this.exeMapCurrent, collection, ConfigurationUserLevel.PerUserRoaming, context, true);
				this.SaveProperties(this.exeMapCurrent, collection, ConfigurationUserLevel.PerUserRoamingAndLocal, context, true);
			}
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x0002D054 File Offset: 0x0002B254
		public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection collection)
		{
			this.CreateExeMap();
			if (this.values == null)
			{
				this.values = new SettingsPropertyValueCollection();
				string groupName = context["GroupName"] as string;
				this.LoadProperties(this.exeMapCurrent, collection, ConfigurationUserLevel.None, "applicationSettings", false, groupName);
				this.LoadProperties(this.exeMapCurrent, collection, ConfigurationUserLevel.None, "userSettings", false, groupName);
				this.LoadProperties(this.exeMapCurrent, collection, ConfigurationUserLevel.PerUserRoaming, "userSettings", true, groupName);
				this.LoadProperties(this.exeMapCurrent, collection, ConfigurationUserLevel.PerUserRoamingAndLocal, "userSettings", true, groupName);
				foreach (object obj in collection)
				{
					SettingsProperty settingsProperty = (SettingsProperty)obj;
					if (this.values[settingsProperty.Name] == null)
					{
						this.values.Add(new SettingsPropertyValue(settingsProperty));
					}
				}
			}
			return this.values;
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x0002D168 File Offset: 0x0002B368
		private void CreateExeMap()
		{
			if (this.exeMapCurrent == null)
			{
				CustomizableFileSettingsProvider.CreateUserConfigPath();
				this.exeMapCurrent = new ExeConfigurationFileMap();
				Assembly assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
				this.exeMapCurrent.ExeConfigFilename = assembly.Location + ".config";
				this.exeMapCurrent.LocalUserConfigFilename = CustomizableFileSettingsProvider.UserLocalFullPath;
				this.exeMapCurrent.RoamingUserConfigFilename = CustomizableFileSettingsProvider.UserRoamingFullPath;
				if (CustomizableFileSettingsProvider.webConfigurationFileMapType != null && typeof(ConfigurationFileMap).IsAssignableFrom(CustomizableFileSettingsProvider.webConfigurationFileMapType))
				{
					try
					{
						ConfigurationFileMap configurationFileMap = Activator.CreateInstance(CustomizableFileSettingsProvider.webConfigurationFileMapType) as ConfigurationFileMap;
						if (configurationFileMap != null)
						{
							string machineConfigFilename = configurationFileMap.MachineConfigFilename;
							if (!string.IsNullOrEmpty(machineConfigFilename))
							{
								this.exeMapCurrent.ExeConfigFilename = machineConfigFilename;
							}
						}
					}
					catch
					{
					}
				}
				if (CustomizableFileSettingsProvider.PrevUserLocalFullPath != string.Empty && CustomizableFileSettingsProvider.PrevUserRoamingFullPath != string.Empty)
				{
					this.exeMapPrev = new ExeConfigurationFileMap();
					this.exeMapPrev.ExeConfigFilename = assembly.Location + ".config";
					this.exeMapPrev.LocalUserConfigFilename = CustomizableFileSettingsProvider.PrevUserLocalFullPath;
					this.exeMapPrev.RoamingUserConfigFilename = CustomizableFileSettingsProvider.PrevUserRoamingFullPath;
				}
			}
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x0002D2CC File Offset: 0x0002B4CC
		public SettingsPropertyValue GetPreviousVersion(SettingsContext context, SettingsProperty property)
		{
			return null;
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x0002D2D0 File Offset: 0x0002B4D0
		public void Reset(SettingsContext context)
		{
			SettingsPropertyCollection collection = new SettingsPropertyCollection();
			this.GetPropertyValues(context, collection);
			foreach (object obj in this.values)
			{
				SettingsPropertyValue settingsPropertyValue = (SettingsPropertyValue)obj;
				settingsPropertyValue.PropertyValue = settingsPropertyValue.Reset();
			}
			this.SetPropertyValues(context, this.values);
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x0002D360 File Offset: 0x0002B560
		public void Upgrade(SettingsContext context, SettingsPropertyCollection properties)
		{
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x0002D364 File Offset: 0x0002B564
		public static void setCreate()
		{
			CustomizableFileSettingsProvider.CreateUserConfigPath();
		}

		// Token: 0x040004B0 RID: 1200
		private static Type webConfigurationFileMapType;

		// Token: 0x040004B1 RID: 1201
		private static string userRoamingPath = string.Empty;

		// Token: 0x040004B2 RID: 1202
		private static string userLocalPath = string.Empty;

		// Token: 0x040004B3 RID: 1203
		private static string userRoamingPathPrevVersion = string.Empty;

		// Token: 0x040004B4 RID: 1204
		private static string userLocalPathPrevVersion = string.Empty;

		// Token: 0x040004B5 RID: 1205
		private static string userRoamingName = "user.config";

		// Token: 0x040004B6 RID: 1206
		private static string userLocalName = "user.config";

		// Token: 0x040004B7 RID: 1207
		private static string userRoamingBasePath = string.Empty;

		// Token: 0x040004B8 RID: 1208
		private static string userLocalBasePath = string.Empty;

		// Token: 0x040004B9 RID: 1209
		private static string CompanyName = string.Empty;

		// Token: 0x040004BA RID: 1210
		private static string ProductName = string.Empty;

		// Token: 0x040004BB RID: 1211
		private static string ForceVersion = string.Empty;

		// Token: 0x040004BC RID: 1212
		private static string[] ProductVersion;

		// Token: 0x040004BD RID: 1213
		private static bool isVersionMajor;

		// Token: 0x040004BE RID: 1214
		private static bool isVersionMinor;

		// Token: 0x040004BF RID: 1215
		private static bool isVersionBuild;

		// Token: 0x040004C0 RID: 1216
		private static bool isVersionRevision;

		// Token: 0x040004C1 RID: 1217
		private static bool isCompany = true;

		// Token: 0x040004C2 RID: 1218
		private static bool isProduct = true;

		// Token: 0x040004C3 RID: 1219
		private static bool isEvidence;

		// Token: 0x040004C4 RID: 1220
		private static bool userDefine;

		// Token: 0x040004C5 RID: 1221
		private static UserConfigLocationOption userConfig = UserConfigLocationOption.Company_Product;

		// Token: 0x040004C6 RID: 1222
		private string app_name = string.Empty;

		// Token: 0x040004C7 RID: 1223
		private ExeConfigurationFileMap exeMapCurrent;

		// Token: 0x040004C8 RID: 1224
		private ExeConfigurationFileMap exeMapPrev;

		// Token: 0x040004C9 RID: 1225
		private SettingsPropertyValueCollection values;
	}
}
