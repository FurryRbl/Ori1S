using System;
using System.Collections.Generic;
using System.Security.Permissions;

namespace System.Security.Policy
{
	// Token: 0x02000637 RID: 1591
	internal static class DefaultPolicies
	{
		// Token: 0x06003C9D RID: 15517 RVA: 0x000D0258 File Offset: 0x000CE458
		// Note: this type is marked as 'beforefieldinit'.
		static DefaultPolicies()
		{
			byte[] array = new byte[16];
			array[8] = 4;
			DefaultPolicies._ecmaKey = array;
			DefaultPolicies._msFinalKey = new byte[]
			{
				0,
				36,
				0,
				0,
				4,
				128,
				0,
				0,
				148,
				0,
				0,
				0,
				6,
				2,
				0,
				0,
				0,
				36,
				0,
				0,
				82,
				83,
				65,
				49,
				0,
				4,
				0,
				0,
				1,
				0,
				1,
				0,
				7,
				209,
				250,
				87,
				196,
				174,
				217,
				240,
				163,
				46,
				132,
				170,
				15,
				174,
				253,
				13,
				233,
				232,
				253,
				106,
				236,
				143,
				135,
				251,
				3,
				118,
				108,
				131,
				76,
				153,
				146,
				30,
				178,
				59,
				231,
				154,
				217,
				213,
				220,
				193,
				221,
				154,
				210,
				54,
				19,
				33,
				2,
				144,
				11,
				114,
				60,
				249,
				128,
				149,
				127,
				196,
				225,
				119,
				16,
				143,
				198,
				7,
				119,
				79,
				41,
				232,
				50,
				14,
				146,
				234,
				5,
				236,
				228,
				232,
				33,
				192,
				165,
				239,
				232,
				241,
				100,
				92,
				76,
				12,
				147,
				193,
				171,
				153,
				40,
				93,
				98,
				44,
				170,
				101,
				44,
				29,
				250,
				214,
				61,
				116,
				93,
				111,
				45,
				229,
				241,
				126,
				94,
				175,
				15,
				196,
				150,
				61,
				38,
				28,
				138,
				18,
				67,
				101,
				24,
				32,
				109,
				192,
				147,
				52,
				77,
				90,
				210,
				147
			};
		}

		// Token: 0x06003C9E RID: 15518 RVA: 0x000D0290 File Offset: 0x000CE490
		public static PermissionSet GetSpecialPermissionSet(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			switch (name)
			{
			case "FullTrust":
				return DefaultPolicies.FullTrust;
			case "LocalIntranet":
				return DefaultPolicies.LocalIntranet;
			case "Internet":
				return DefaultPolicies.Internet;
			case "SkipVerification":
				return DefaultPolicies.SkipVerification;
			case "Execution":
				return DefaultPolicies.Execution;
			case "Nothing":
				return DefaultPolicies.Nothing;
			case "Everything":
				return DefaultPolicies.Everything;
			}
			return null;
		}

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x06003C9F RID: 15519 RVA: 0x000D0388 File Offset: 0x000CE588
		public static PermissionSet FullTrust
		{
			get
			{
				if (DefaultPolicies._fullTrust == null)
				{
					DefaultPolicies._fullTrust = DefaultPolicies.BuildFullTrust();
				}
				return DefaultPolicies._fullTrust;
			}
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x06003CA0 RID: 15520 RVA: 0x000D03A4 File Offset: 0x000CE5A4
		public static PermissionSet LocalIntranet
		{
			get
			{
				if (DefaultPolicies._localIntranet == null)
				{
					DefaultPolicies._localIntranet = DefaultPolicies.BuildLocalIntranet();
				}
				return DefaultPolicies._localIntranet;
			}
		}

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x06003CA1 RID: 15521 RVA: 0x000D03C0 File Offset: 0x000CE5C0
		public static PermissionSet Internet
		{
			get
			{
				if (DefaultPolicies._internet == null)
				{
					DefaultPolicies._internet = DefaultPolicies.BuildInternet();
				}
				return DefaultPolicies._internet;
			}
		}

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x06003CA2 RID: 15522 RVA: 0x000D03DC File Offset: 0x000CE5DC
		public static PermissionSet SkipVerification
		{
			get
			{
				if (DefaultPolicies._skipVerification == null)
				{
					DefaultPolicies._skipVerification = DefaultPolicies.BuildSkipVerification();
				}
				return DefaultPolicies._skipVerification;
			}
		}

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x06003CA3 RID: 15523 RVA: 0x000D03F8 File Offset: 0x000CE5F8
		public static PermissionSet Execution
		{
			get
			{
				if (DefaultPolicies._execution == null)
				{
					DefaultPolicies._execution = DefaultPolicies.BuildExecution();
				}
				return DefaultPolicies._execution;
			}
		}

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x06003CA4 RID: 15524 RVA: 0x000D0414 File Offset: 0x000CE614
		public static PermissionSet Nothing
		{
			get
			{
				if (DefaultPolicies._nothing == null)
				{
					DefaultPolicies._nothing = DefaultPolicies.BuildNothing();
				}
				return DefaultPolicies._nothing;
			}
		}

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x06003CA5 RID: 15525 RVA: 0x000D0430 File Offset: 0x000CE630
		public static PermissionSet Everything
		{
			get
			{
				if (DefaultPolicies._everything == null)
				{
					DefaultPolicies._everything = DefaultPolicies.BuildEverything();
				}
				return DefaultPolicies._everything;
			}
		}

		// Token: 0x06003CA6 RID: 15526 RVA: 0x000D044C File Offset: 0x000CE64C
		public static StrongNameMembershipCondition FullTrustMembership(string name, DefaultPolicies.Key key)
		{
			StrongNamePublicKeyBlob blob = null;
			if (key != DefaultPolicies.Key.Ecma)
			{
				if (key == DefaultPolicies.Key.MsFinal)
				{
					if (DefaultPolicies._msFinal == null)
					{
						DefaultPolicies._msFinal = new StrongNamePublicKeyBlob(DefaultPolicies._msFinalKey);
					}
					blob = DefaultPolicies._msFinal;
				}
			}
			else
			{
				if (DefaultPolicies._ecma == null)
				{
					DefaultPolicies._ecma = new StrongNamePublicKeyBlob(DefaultPolicies._ecmaKey);
				}
				blob = DefaultPolicies._ecma;
			}
			if (DefaultPolicies._fxVersion == null)
			{
				DefaultPolicies._fxVersion = new Version("2.0.0.0");
			}
			return new StrongNameMembershipCondition(blob, name, DefaultPolicies._fxVersion);
		}

		// Token: 0x06003CA7 RID: 15527 RVA: 0x000D04E4 File Offset: 0x000CE6E4
		private static NamedPermissionSet BuildFullTrust()
		{
			return new NamedPermissionSet("FullTrust", PermissionState.Unrestricted);
		}

		// Token: 0x06003CA8 RID: 15528 RVA: 0x000D04F4 File Offset: 0x000CE6F4
		private static NamedPermissionSet BuildLocalIntranet()
		{
			NamedPermissionSet namedPermissionSet = new NamedPermissionSet("LocalIntranet", PermissionState.None);
			namedPermissionSet.AddPermission(new EnvironmentPermission(EnvironmentPermissionAccess.Read, "USERNAME;USER"));
			namedPermissionSet.AddPermission(new FileDialogPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new IsolatedStorageFilePermission(PermissionState.None)
			{
				UsageAllowed = IsolatedStorageContainment.AssemblyIsolationByUser,
				UserQuota = long.MaxValue
			});
			namedPermissionSet.AddPermission(new ReflectionPermission(ReflectionPermissionFlag.ReflectionEmit));
			SecurityPermissionFlag flag = SecurityPermissionFlag.Assertion | SecurityPermissionFlag.Execution;
			namedPermissionSet.AddPermission(new SecurityPermission(flag));
			namedPermissionSet.AddPermission(new UIPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Net.DnsPermission, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create(DefaultPolicies.PrintingPermission("SafePrinting")));
			return namedPermissionSet;
		}

		// Token: 0x06003CA9 RID: 15529 RVA: 0x000D05A8 File Offset: 0x000CE7A8
		private static NamedPermissionSet BuildInternet()
		{
			NamedPermissionSet namedPermissionSet = new NamedPermissionSet("Internet", PermissionState.None);
			namedPermissionSet.AddPermission(new FileDialogPermission(FileDialogPermissionAccess.Open));
			namedPermissionSet.AddPermission(new IsolatedStorageFilePermission(PermissionState.None)
			{
				UsageAllowed = IsolatedStorageContainment.DomainIsolationByUser,
				UserQuota = 512000L
			});
			namedPermissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
			namedPermissionSet.AddPermission(new UIPermission(UIPermissionWindow.SafeTopLevelWindows, UIPermissionClipboard.OwnClipboard));
			namedPermissionSet.AddPermission(PermissionBuilder.Create(DefaultPolicies.PrintingPermission("SafePrinting")));
			return namedPermissionSet;
		}

		// Token: 0x06003CAA RID: 15530 RVA: 0x000D0624 File Offset: 0x000CE824
		private static NamedPermissionSet BuildSkipVerification()
		{
			NamedPermissionSet namedPermissionSet = new NamedPermissionSet("SkipVerification", PermissionState.None);
			namedPermissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.SkipVerification));
			return namedPermissionSet;
		}

		// Token: 0x06003CAB RID: 15531 RVA: 0x000D064C File Offset: 0x000CE84C
		private static NamedPermissionSet BuildExecution()
		{
			NamedPermissionSet namedPermissionSet = new NamedPermissionSet("Execution", PermissionState.None);
			namedPermissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
			return namedPermissionSet;
		}

		// Token: 0x06003CAC RID: 15532 RVA: 0x000D0674 File Offset: 0x000CE874
		private static NamedPermissionSet BuildNothing()
		{
			return new NamedPermissionSet("Nothing", PermissionState.None);
		}

		// Token: 0x06003CAD RID: 15533 RVA: 0x000D0684 File Offset: 0x000CE884
		private static NamedPermissionSet BuildEverything()
		{
			NamedPermissionSet namedPermissionSet = new NamedPermissionSet("Everything", PermissionState.None);
			namedPermissionSet.AddPermission(new EnvironmentPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new FileDialogPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new FileIOPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new IsolatedStorageFilePermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new ReflectionPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new RegistryPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new KeyContainerPermission(PermissionState.Unrestricted));
			SecurityPermissionFlag securityPermissionFlag = SecurityPermissionFlag.AllFlags;
			securityPermissionFlag &= ~SecurityPermissionFlag.SkipVerification;
			namedPermissionSet.AddPermission(new SecurityPermission(securityPermissionFlag));
			namedPermissionSet.AddPermission(new UIPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Net.DnsPermission, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Drawing.Printing.PrintingPermission, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Diagnostics.EventLogPermission, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Net.SocketPermission, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Net.WebPermission, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.DirectoryServices.DirectoryServicesPermission, System.DirectoryServices, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.ServiceProcess.ServiceControllerPermission, System.ServiceProcess, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Data.OleDb.OleDbPermission, System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Data.SqlClient.SqlClientPermission, System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			return namedPermissionSet;
		}

		// Token: 0x06003CAE RID: 15534 RVA: 0x000D07E4 File Offset: 0x000CE9E4
		private static SecurityElement PrintingPermission(string level)
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", "System.Drawing.Printing.PrintingPermission, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			securityElement.AddAttribute("version", "1");
			securityElement.AddAttribute("Level", level);
			return securityElement;
		}

		// Token: 0x04001A44 RID: 6724
		private const string DnsPermissionClass = "System.Net.DnsPermission, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04001A45 RID: 6725
		private const string EventLogPermissionClass = "System.Diagnostics.EventLogPermission, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04001A46 RID: 6726
		private const string PrintingPermissionClass = "System.Drawing.Printing.PrintingPermission, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04001A47 RID: 6727
		private const string SocketPermissionClass = "System.Net.SocketPermission, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04001A48 RID: 6728
		private const string WebPermissionClass = "System.Net.WebPermission, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04001A49 RID: 6729
		private const string PerformanceCounterPermissionClass = "System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04001A4A RID: 6730
		private const string DirectoryServicesPermissionClass = "System.DirectoryServices.DirectoryServicesPermission, System.DirectoryServices, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04001A4B RID: 6731
		private const string MessageQueuePermissionClass = "System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04001A4C RID: 6732
		private const string ServiceControllerPermissionClass = "System.ServiceProcess.ServiceControllerPermission, System.ServiceProcess, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04001A4D RID: 6733
		private const string OleDbPermissionClass = "System.Data.OleDb.OleDbPermission, System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04001A4E RID: 6734
		private const string SqlClientPermissionClass = "System.Data.SqlClient.SqlClientPermission, System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04001A4F RID: 6735
		private const string DataProtectionPermissionClass = "System.Security.Permissions.DataProtectionPermission, System.Security, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04001A50 RID: 6736
		private const string StorePermissionClass = "System.Security.Permissions.StorePermission, System.Security, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04001A51 RID: 6737
		private static Version _fxVersion;

		// Token: 0x04001A52 RID: 6738
		private static byte[] _ecmaKey;

		// Token: 0x04001A53 RID: 6739
		private static StrongNamePublicKeyBlob _ecma;

		// Token: 0x04001A54 RID: 6740
		private static byte[] _msFinalKey;

		// Token: 0x04001A55 RID: 6741
		private static StrongNamePublicKeyBlob _msFinal;

		// Token: 0x04001A56 RID: 6742
		private static NamedPermissionSet _fullTrust;

		// Token: 0x04001A57 RID: 6743
		private static NamedPermissionSet _localIntranet;

		// Token: 0x04001A58 RID: 6744
		private static NamedPermissionSet _internet;

		// Token: 0x04001A59 RID: 6745
		private static NamedPermissionSet _skipVerification;

		// Token: 0x04001A5A RID: 6746
		private static NamedPermissionSet _execution;

		// Token: 0x04001A5B RID: 6747
		private static NamedPermissionSet _nothing;

		// Token: 0x04001A5C RID: 6748
		private static NamedPermissionSet _everything;

		// Token: 0x02000638 RID: 1592
		public static class ReservedNames
		{
			// Token: 0x06003CAF RID: 15535 RVA: 0x000D082C File Offset: 0x000CEA2C
			public static bool IsReserved(string name)
			{
				if (name != null)
				{
					if (DefaultPolicies.ReservedNames.<>f__switch$map30 == null)
					{
						DefaultPolicies.ReservedNames.<>f__switch$map30 = new Dictionary<string, int>(7)
						{
							{
								"FullTrust",
								0
							},
							{
								"LocalIntranet",
								0
							},
							{
								"Internet",
								0
							},
							{
								"SkipVerification",
								0
							},
							{
								"Execution",
								0
							},
							{
								"Nothing",
								0
							},
							{
								"Everything",
								0
							}
						};
					}
					int num;
					if (DefaultPolicies.ReservedNames.<>f__switch$map30.TryGetValue(name, out num))
					{
						if (num == 0)
						{
							return true;
						}
					}
				}
				return false;
			}

			// Token: 0x04001A5E RID: 6750
			public const string FullTrust = "FullTrust";

			// Token: 0x04001A5F RID: 6751
			public const string LocalIntranet = "LocalIntranet";

			// Token: 0x04001A60 RID: 6752
			public const string Internet = "Internet";

			// Token: 0x04001A61 RID: 6753
			public const string SkipVerification = "SkipVerification";

			// Token: 0x04001A62 RID: 6754
			public const string Execution = "Execution";

			// Token: 0x04001A63 RID: 6755
			public const string Nothing = "Nothing";

			// Token: 0x04001A64 RID: 6756
			public const string Everything = "Everything";
		}

		// Token: 0x02000639 RID: 1593
		public enum Key
		{
			// Token: 0x04001A67 RID: 6759
			Ecma,
			// Token: 0x04001A68 RID: 6760
			MsFinal
		}
	}
}
