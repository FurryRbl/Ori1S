using System;
using System.Configuration.Internal;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System.Configuration
{
	// Token: 0x02000051 RID: 81
	internal abstract class InternalConfigurationHost : IInternalConfigHost
	{
		// Token: 0x060002C9 RID: 713 RVA: 0x00008890 File Offset: 0x00006A90
		string IInternalConfigHost.DecryptSection(string encryptedXml, ProtectedConfigurationProvider protectionProvider, ProtectedConfigurationSection protectedSection)
		{
			return protectedSection.DecryptSection(encryptedXml, protectionProvider);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000889C File Offset: 0x00006A9C
		string IInternalConfigHost.EncryptSection(string clearXml, ProtectedConfigurationProvider protectionProvider, ProtectedConfigurationSection protectedSection)
		{
			return protectedSection.EncryptSection(clearXml, protectionProvider);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x000088A8 File Offset: 0x00006AA8
		public virtual object CreateConfigurationContext(string configPath, string locationSubPath)
		{
			return null;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x000088AC File Offset: 0x00006AAC
		public virtual object CreateDeprecatedConfigContext(string configPath)
		{
			return null;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x000088B0 File Offset: 0x00006AB0
		public virtual void DeleteStream(string streamName)
		{
			File.Delete(streamName);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x000088B8 File Offset: 0x00006AB8
		public virtual string GetConfigPathFromLocationSubPath(string configPath, string locationSubPath)
		{
			return configPath;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x000088BC File Offset: 0x00006ABC
		public virtual Type GetConfigType(string typeName, bool throwOnError)
		{
			Type type = Type.GetType(typeName);
			if (type == null && throwOnError)
			{
				throw new ConfigurationErrorsException("Type '" + typeName + "' not found.");
			}
			return type;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x000088F4 File Offset: 0x00006AF4
		public virtual string GetConfigTypeName(Type t)
		{
			return t.AssemblyQualifiedName;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x000088FC File Offset: 0x00006AFC
		public virtual void GetRestrictedPermissions(IInternalConfigRecord configRecord, out PermissionSet permissionSet, out bool isHostReady)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002D2 RID: 722
		public abstract string GetStreamName(string configPath);

		// Token: 0x060002D3 RID: 723
		public abstract void Init(IInternalConfigRoot root, params object[] hostInitParams);

		// Token: 0x060002D4 RID: 724
		public abstract void InitForConfiguration(ref string locationSubPath, out string configPath, out string locationConfigPath, IInternalConfigRoot root, params object[] hostInitConfigurationParams);

		// Token: 0x060002D5 RID: 725 RVA: 0x00008904 File Offset: 0x00006B04
		[MonoNotSupported("mono does not support remote configuration")]
		public virtual string GetStreamNameForConfigSource(string streamName, string configSource)
		{
			throw new NotSupportedException("mono does not support remote configuration");
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00008910 File Offset: 0x00006B10
		public virtual object GetStreamVersion(string streamName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00008918 File Offset: 0x00006B18
		public virtual IDisposable Impersonate()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00008920 File Offset: 0x00006B20
		public virtual bool IsAboveApplication(string configPath)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00008928 File Offset: 0x00006B28
		public virtual bool IsConfigRecordRequired(string configPath)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00008930 File Offset: 0x00006B30
		public virtual bool IsDefinitionAllowed(string configPath, ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition)
		{
			if (allowDefinition != ConfigurationAllowDefinition.MachineOnly)
			{
				return allowDefinition != ConfigurationAllowDefinition.MachineToApplication || configPath == "machine" || configPath == "exe";
			}
			return configPath == "machine";
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00008984 File Offset: 0x00006B84
		public virtual bool IsFile(string streamName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000898C File Offset: 0x00006B8C
		public virtual bool IsFullTrustSectionWithoutAptcaAllowed(IInternalConfigRecord configRecord)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00008994 File Offset: 0x00006B94
		public virtual bool IsInitDelayed(IInternalConfigRecord configRecord)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000899C File Offset: 0x00006B9C
		public virtual bool IsLocationApplicable(string configPath)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002DF RID: 735 RVA: 0x000089A4 File Offset: 0x00006BA4
		public virtual bool IsRemote
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x000089AC File Offset: 0x00006BAC
		public virtual bool IsSecondaryRoot(string configPath)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x000089B4 File Offset: 0x00006BB4
		public virtual bool IsTrustedConfigPath(string configPath)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002E2 RID: 738
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string get_bundled_machine_config();

		// Token: 0x060002E3 RID: 739 RVA: 0x000089BC File Offset: 0x00006BBC
		public virtual Stream OpenStreamForRead(string streamName)
		{
			if (string.CompareOrdinal(streamName, RuntimeEnvironment.SystemConfigurationFile) == 0)
			{
				string bundled_machine_config = InternalConfigurationHost.get_bundled_machine_config();
				if (bundled_machine_config != null)
				{
					return new MemoryStream(Encoding.UTF8.GetBytes(bundled_machine_config));
				}
			}
			if (!File.Exists(streamName))
			{
				throw new ConfigurationException("File '" + streamName + "' not found");
			}
			return new FileStream(streamName, FileMode.Open, FileAccess.Read);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00008A20 File Offset: 0x00006C20
		public virtual Stream OpenStreamForRead(string streamName, bool assertPermissions)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00008A28 File Offset: 0x00006C28
		public virtual Stream OpenStreamForWrite(string streamName, string templateStreamName, ref object writeContext)
		{
			string directoryName = Path.GetDirectoryName(streamName);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			return new FileStream(streamName, FileMode.Create, FileAccess.Write);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00008A58 File Offset: 0x00006C58
		public virtual Stream OpenStreamForWrite(string streamName, string templateStreamName, ref object writeContext, bool assertPermissions)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00008A60 File Offset: 0x00006C60
		public virtual bool PrefetchAll(string configPath, string streamName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00008A68 File Offset: 0x00006C68
		public virtual bool PrefetchSection(string sectionGroupName, string sectionName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00008A70 File Offset: 0x00006C70
		public virtual void RequireCompleteInit(IInternalConfigRecord configRecord)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00008A78 File Offset: 0x00006C78
		public virtual object StartMonitoringStreamForChanges(string streamName, StreamChangeCallback callback)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00008A80 File Offset: 0x00006C80
		public virtual void StopMonitoringStreamForChanges(string streamName, StreamChangeCallback callback)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00008A88 File Offset: 0x00006C88
		public virtual void VerifyDefinitionAllowed(string configPath, ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition, IConfigErrorInfo errorInfo)
		{
			if (!this.IsDefinitionAllowed(configPath, allowDefinition, allowExeDefinition))
			{
				throw new ConfigurationErrorsException("The section can't be defined in this file (the allowed definition context is '" + allowDefinition + "').", errorInfo.Filename, errorInfo.LineNumber);
			}
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00008ACC File Offset: 0x00006CCC
		public virtual void WriteCompleted(string streamName, bool success, object writeContext)
		{
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00008AD0 File Offset: 0x00006CD0
		public virtual void WriteCompleted(string streamName, bool success, object writeContext, bool assertPermissions)
		{
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002EF RID: 751 RVA: 0x00008AD4 File Offset: 0x00006CD4
		public virtual bool SupportsChangeNotifications
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x00008AD8 File Offset: 0x00006CD8
		public virtual bool SupportsLocation
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x00008ADC File Offset: 0x00006CDC
		public virtual bool SupportsPath
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x00008AE0 File Offset: 0x00006CE0
		public virtual bool SupportsRefresh
		{
			get
			{
				return false;
			}
		}
	}
}
