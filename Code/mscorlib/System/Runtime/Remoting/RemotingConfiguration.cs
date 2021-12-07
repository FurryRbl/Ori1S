using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;
using Mono.Xml;

namespace System.Runtime.Remoting
{
	/// <summary>Provides various static methods for configuring the remoting infrastructure.</summary>
	// Token: 0x02000423 RID: 1059
	[ComVisible(true)]
	public static class RemotingConfiguration
	{
		/// <summary>Gets the ID of the currently executing application.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the ID of the currently executing application.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06002CFC RID: 11516 RVA: 0x00094784 File Offset: 0x00092984
		public static string ApplicationId
		{
			get
			{
				RemotingConfiguration.applicationID = RemotingConfiguration.ApplicationName;
				return RemotingConfiguration.applicationID;
			}
		}

		/// <summary>Gets or sets the name of a remoting application.</summary>
		/// <returns>The name of a remoting application.</returns>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. This exception is thrown only when setting the property value. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06002CFD RID: 11517 RVA: 0x00094798 File Offset: 0x00092998
		// (set) Token: 0x06002CFE RID: 11518 RVA: 0x000947A0 File Offset: 0x000929A0
		public static string ApplicationName
		{
			get
			{
				return RemotingConfiguration.applicationName;
			}
			set
			{
				RemotingConfiguration.applicationName = value;
			}
		}

		/// <summary>Gets or sets value that indicates how custom errors are handled.</summary>
		/// <returns>A member of the <see cref="T:System.Runtime.Remoting.CustomErrorsModes" /> enumeration that indicates how custom errors are handled.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06002CFF RID: 11519 RVA: 0x000947A8 File Offset: 0x000929A8
		// (set) Token: 0x06002D00 RID: 11520 RVA: 0x000947B0 File Offset: 0x000929B0
		[MonoTODO]
		public static CustomErrorsModes CustomErrorsMode
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the ID of the currently executing process.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the ID of the currently executing process.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06002D01 RID: 11521 RVA: 0x000947B8 File Offset: 0x000929B8
		public static string ProcessId
		{
			get
			{
				if (RemotingConfiguration.processGuid == null)
				{
					RemotingConfiguration.processGuid = AppDomain.GetProcessGuid();
				}
				return RemotingConfiguration.processGuid;
			}
		}

		/// <summary>Reads the configuration file and configures the remoting infrastructure.</summary>
		/// <param name="filename">The name of the remoting configuration file. Can be null.</param>
		/// <param name="ensureSecurity">If set to true security is required. If set to false, security is not required but still may be used.</param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		// Token: 0x06002D02 RID: 11522 RVA: 0x000947D4 File Offset: 0x000929D4
		[MonoTODO("Implement ensureSecurity")]
		public static void Configure(string filename, bool ensureSecurity)
		{
			Hashtable obj = RemotingConfiguration.channelTemplates;
			lock (obj)
			{
				if (!RemotingConfiguration.defaultConfigRead)
				{
					RemotingConfiguration.ReadConfigFile(Environment.GetMachineConfigPath());
					RemotingConfiguration.defaultConfigRead = true;
				}
				if (filename != null)
				{
					RemotingConfiguration.ReadConfigFile(filename);
				}
			}
		}

		/// <summary>Reads the configuration file and configures the remoting infrastructure. <see cref="M:System.Runtime.Remoting.RemotingConfiguration.Configure(System.String)" /> is obsolete. Please use <see cref="M:System.Runtime.Remoting.RemotingConfiguration.Configure(System.String,System.Boolean)" /> instead.</summary>
		/// <param name="filename">The name of the remoting configuration file. Can be null. </param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06002D03 RID: 11523 RVA: 0x0009483C File Offset: 0x00092A3C
		[Obsolete("Use Configure(String,Boolean)")]
		public static void Configure(string filename)
		{
			RemotingConfiguration.Configure(filename, false);
		}

		// Token: 0x06002D04 RID: 11524 RVA: 0x00094848 File Offset: 0x00092A48
		private static void ReadConfigFile(string filename)
		{
			try
			{
				SmallXmlParser smallXmlParser = new SmallXmlParser();
				using (TextReader textReader = new StreamReader(filename))
				{
					ConfigHandler handler = new ConfigHandler(false);
					smallXmlParser.Parse(textReader, handler);
				}
			}
			catch (Exception ex)
			{
				throw new RemotingException("Configuration file '" + filename + "' could not be loaded: " + ex.Message, ex);
			}
		}

		// Token: 0x06002D05 RID: 11525 RVA: 0x000948E0 File Offset: 0x00092AE0
		internal static void LoadDefaultDelayedChannels()
		{
			Hashtable obj = RemotingConfiguration.channelTemplates;
			lock (obj)
			{
				if (!RemotingConfiguration.defaultDelayedConfigRead && !RemotingConfiguration.defaultConfigRead)
				{
					SmallXmlParser smallXmlParser = new SmallXmlParser();
					using (TextReader textReader = new StreamReader(Environment.GetMachineConfigPath()))
					{
						ConfigHandler handler = new ConfigHandler(true);
						smallXmlParser.Parse(textReader, handler);
					}
					RemotingConfiguration.defaultDelayedConfigRead = true;
				}
			}
		}

		/// <summary>Retrieves an array of object types registered on the client as types that will be activated remotely.</summary>
		/// <returns>An array of object types registered on the client as types that will be activated remotely.</returns>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06002D06 RID: 11526 RVA: 0x0009498C File Offset: 0x00092B8C
		public static ActivatedClientTypeEntry[] GetRegisteredActivatedClientTypes()
		{
			Hashtable obj = RemotingConfiguration.channelTemplates;
			ActivatedClientTypeEntry[] result;
			lock (obj)
			{
				ActivatedClientTypeEntry[] array = new ActivatedClientTypeEntry[RemotingConfiguration.activatedClientEntries.Count];
				RemotingConfiguration.activatedClientEntries.Values.CopyTo(array, 0);
				result = array;
			}
			return result;
		}

		/// <summary>Retrieves an array of object types registered on the service end that can be activated on request from a client.</summary>
		/// <returns>An array of object types registered on the service end that can be activated on request from a client.</returns>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06002D07 RID: 11527 RVA: 0x000949F8 File Offset: 0x00092BF8
		public static ActivatedServiceTypeEntry[] GetRegisteredActivatedServiceTypes()
		{
			Hashtable obj = RemotingConfiguration.channelTemplates;
			ActivatedServiceTypeEntry[] result;
			lock (obj)
			{
				ActivatedServiceTypeEntry[] array = new ActivatedServiceTypeEntry[RemotingConfiguration.activatedServiceEntries.Count];
				RemotingConfiguration.activatedServiceEntries.Values.CopyTo(array, 0);
				result = array;
			}
			return result;
		}

		/// <summary>Retrieves an array of object types registered on the client end as well-known types.</summary>
		/// <returns>An array of object types registered on the client end as well-known types.</returns>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06002D08 RID: 11528 RVA: 0x00094A64 File Offset: 0x00092C64
		public static WellKnownClientTypeEntry[] GetRegisteredWellKnownClientTypes()
		{
			Hashtable obj = RemotingConfiguration.channelTemplates;
			WellKnownClientTypeEntry[] result;
			lock (obj)
			{
				WellKnownClientTypeEntry[] array = new WellKnownClientTypeEntry[RemotingConfiguration.wellKnownClientEntries.Count];
				RemotingConfiguration.wellKnownClientEntries.Values.CopyTo(array, 0);
				result = array;
			}
			return result;
		}

		/// <summary>Retrieves an array of object types registered on the service end as well-known types.</summary>
		/// <returns>An array of object types registered on the service end as well-known types.</returns>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06002D09 RID: 11529 RVA: 0x00094AD0 File Offset: 0x00092CD0
		public static WellKnownServiceTypeEntry[] GetRegisteredWellKnownServiceTypes()
		{
			Hashtable obj = RemotingConfiguration.channelTemplates;
			WellKnownServiceTypeEntry[] result;
			lock (obj)
			{
				WellKnownServiceTypeEntry[] array = new WellKnownServiceTypeEntry[RemotingConfiguration.wellKnownServiceEntries.Count];
				RemotingConfiguration.wellKnownServiceEntries.Values.CopyTo(array, 0);
				result = array;
			}
			return result;
		}

		/// <summary>Returns a Boolean value that indicates whether the specified <see cref="T:System.Type" /> is allowed to be client activated.</summary>
		/// <returns>true if the specified <see cref="T:System.Type" /> is allowed to be client activated; otherwise, false.</returns>
		/// <param name="svrType">The object <see cref="T:System.Type" /> to check. </param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06002D0A RID: 11530 RVA: 0x00094B3C File Offset: 0x00092D3C
		public static bool IsActivationAllowed(Type svrType)
		{
			Hashtable obj = RemotingConfiguration.channelTemplates;
			bool result;
			lock (obj)
			{
				result = RemotingConfiguration.activatedServiceEntries.ContainsKey(svrType);
			}
			return result;
		}

		/// <summary>Checks whether the specified object <see cref="T:System.Type" /> is registered as a remotely activated client type.</summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.ActivatedClientTypeEntry" /> that corresponds to the specified object type.</returns>
		/// <param name="svrType">The object type to check. </param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06002D0B RID: 11531 RVA: 0x00094B90 File Offset: 0x00092D90
		public static ActivatedClientTypeEntry IsRemotelyActivatedClientType(Type svrType)
		{
			Hashtable obj = RemotingConfiguration.channelTemplates;
			ActivatedClientTypeEntry result;
			lock (obj)
			{
				result = (RemotingConfiguration.activatedClientEntries[svrType] as ActivatedClientTypeEntry);
			}
			return result;
		}

		/// <summary>Checks whether the object specified by its type name and assembly name is registered as a remotely activated client type.</summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.ActivatedClientTypeEntry" /> that corresponds to the specified object type.</returns>
		/// <param name="typeName">The type name of the object to check. </param>
		/// <param name="assemblyName">The assembly name of the object to check. </param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06002D0C RID: 11532 RVA: 0x00094BE8 File Offset: 0x00092DE8
		public static ActivatedClientTypeEntry IsRemotelyActivatedClientType(string typeName, string assemblyName)
		{
			return RemotingConfiguration.IsRemotelyActivatedClientType(Assembly.Load(assemblyName).GetType(typeName));
		}

		/// <summary>Checks whether the specified object <see cref="T:System.Type" /> is registered as a well-known client type.</summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.WellKnownClientTypeEntry" /> that corresponds to the specified object type.</returns>
		/// <param name="svrType">The object <see cref="T:System.Type" /> to check. </param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06002D0D RID: 11533 RVA: 0x00094BFC File Offset: 0x00092DFC
		public static WellKnownClientTypeEntry IsWellKnownClientType(Type svrType)
		{
			Hashtable obj = RemotingConfiguration.channelTemplates;
			WellKnownClientTypeEntry result;
			lock (obj)
			{
				result = (RemotingConfiguration.wellKnownClientEntries[svrType] as WellKnownClientTypeEntry);
			}
			return result;
		}

		/// <summary>Checks whether the object specified by its type name and assembly name is registered as a well-known client type.</summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.WellKnownClientTypeEntry" /> that corresponds to the specified object type.</returns>
		/// <param name="typeName">The type name of the object to check. </param>
		/// <param name="assemblyName">The assembly name of the object to check. </param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06002D0E RID: 11534 RVA: 0x00094C54 File Offset: 0x00092E54
		public static WellKnownClientTypeEntry IsWellKnownClientType(string typeName, string assemblyName)
		{
			return RemotingConfiguration.IsWellKnownClientType(Assembly.Load(assemblyName).GetType(typeName));
		}

		/// <summary>Registers an object <see cref="T:System.Type" /> recorded in the provided <see cref="T:System.Runtime.Remoting.ActivatedClientTypeEntry" /> on the client end as a type that can be activated on the server.</summary>
		/// <param name="entry">Configuration settings for the client-activated type. </param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06002D0F RID: 11535 RVA: 0x00094C68 File Offset: 0x00092E68
		public static void RegisterActivatedClientType(ActivatedClientTypeEntry entry)
		{
			Hashtable obj = RemotingConfiguration.channelTemplates;
			lock (obj)
			{
				if (RemotingConfiguration.wellKnownClientEntries.ContainsKey(entry.ObjectType) || RemotingConfiguration.activatedClientEntries.ContainsKey(entry.ObjectType))
				{
					throw new RemotingException("Attempt to redirect activation of type '" + entry.ObjectType.FullName + "' which is already redirected.");
				}
				RemotingConfiguration.activatedClientEntries[entry.ObjectType] = entry;
				ActivationServices.EnableProxyActivation(entry.ObjectType, true);
			}
		}

		/// <summary>Registers an object <see cref="T:System.Type" /> on the client end as a type that can be activated on the server, using the given parameters to initialize a new instance of the <see cref="T:System.Runtime.Remoting.ActivatedClientTypeEntry" /> class.</summary>
		/// <param name="type">The object <see cref="T:System.Type" />. </param>
		/// <param name="appUrl">URL of the application where this type is activated. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="typeName" /> or <paramref name="URI" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06002D10 RID: 11536 RVA: 0x00094D10 File Offset: 0x00092F10
		public static void RegisterActivatedClientType(Type type, string appUrl)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (appUrl == null)
			{
				throw new ArgumentNullException("appUrl");
			}
			RemotingConfiguration.RegisterActivatedClientType(new ActivatedClientTypeEntry(type, appUrl));
		}

		/// <summary>Registers an object type recorded in the provided <see cref="T:System.Runtime.Remoting.ActivatedServiceTypeEntry" /> on the service end as one that can be activated on request from a client.</summary>
		/// <param name="entry">Configuration settings for the client-activated type. </param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06002D11 RID: 11537 RVA: 0x00094D4C File Offset: 0x00092F4C
		public static void RegisterActivatedServiceType(ActivatedServiceTypeEntry entry)
		{
			Hashtable obj = RemotingConfiguration.channelTemplates;
			lock (obj)
			{
				RemotingConfiguration.activatedServiceEntries.Add(entry.ObjectType, entry);
			}
		}

		/// <summary>Registers a specified object type on the service end as a type that can be activated on request from a client.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of object to register. </param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06002D12 RID: 11538 RVA: 0x00094DA0 File Offset: 0x00092FA0
		public static void RegisterActivatedServiceType(Type type)
		{
			RemotingConfiguration.RegisterActivatedServiceType(new ActivatedServiceTypeEntry(type));
		}

		/// <summary>Registers an object <see cref="T:System.Type" /> on the client end as a well-known type that can be activated on the server, using the given parameters to initialize a new instance of the <see cref="T:System.Runtime.Remoting.WellKnownClientTypeEntry" /> class.</summary>
		/// <param name="type">The object <see cref="T:System.Type" />. </param>
		/// <param name="objectUrl">URL of a well-known client object. </param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06002D13 RID: 11539 RVA: 0x00094DB0 File Offset: 0x00092FB0
		public static void RegisterWellKnownClientType(Type type, string objectUrl)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (objectUrl == null)
			{
				throw new ArgumentNullException("objectUrl");
			}
			RemotingConfiguration.RegisterWellKnownClientType(new WellKnownClientTypeEntry(type, objectUrl));
		}

		/// <summary>Registers an object <see cref="T:System.Type" /> recorded in the provided <see cref="T:System.Runtime.Remoting.WellKnownClientTypeEntry" /> on the client end as a well-known type that can be activated on the server.</summary>
		/// <param name="entry">Configuration settings for the well-known type. </param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06002D14 RID: 11540 RVA: 0x00094DEC File Offset: 0x00092FEC
		public static void RegisterWellKnownClientType(WellKnownClientTypeEntry entry)
		{
			Hashtable obj = RemotingConfiguration.channelTemplates;
			lock (obj)
			{
				if (RemotingConfiguration.wellKnownClientEntries.ContainsKey(entry.ObjectType) || RemotingConfiguration.activatedClientEntries.ContainsKey(entry.ObjectType))
				{
					throw new RemotingException("Attempt to redirect activation of type '" + entry.ObjectType.FullName + "' which is already redirected.");
				}
				RemotingConfiguration.wellKnownClientEntries[entry.ObjectType] = entry;
				ActivationServices.EnableProxyActivation(entry.ObjectType, true);
			}
		}

		/// <summary>Registers an object <see cref="T:System.Type" /> on the service end as a well-known type, using the given parameters to initialize a new instance of <see cref="T:System.Runtime.Remoting.WellKnownServiceTypeEntry" />.</summary>
		/// <param name="type">The object <see cref="T:System.Type" />. </param>
		/// <param name="objectUri">The object URI. </param>
		/// <param name="mode">The activation mode of the well-known object type being registered. (See <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" />.) </param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06002D15 RID: 11541 RVA: 0x00094E94 File Offset: 0x00093094
		public static void RegisterWellKnownServiceType(Type type, string objectUri, WellKnownObjectMode mode)
		{
			RemotingConfiguration.RegisterWellKnownServiceType(new WellKnownServiceTypeEntry(type, objectUri, mode));
		}

		/// <summary>Registers an object <see cref="T:System.Type" /> recorded in the provided <see cref="T:System.Runtime.Remoting.WellKnownServiceTypeEntry" /> on the service end as a well-known type.</summary>
		/// <param name="entry">Configuration settings for the well-known type. </param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06002D16 RID: 11542 RVA: 0x00094EA4 File Offset: 0x000930A4
		public static void RegisterWellKnownServiceType(WellKnownServiceTypeEntry entry)
		{
			Hashtable obj = RemotingConfiguration.channelTemplates;
			lock (obj)
			{
				RemotingConfiguration.wellKnownServiceEntries[entry.ObjectUri] = entry;
				RemotingServices.CreateWellKnownServerIdentity(entry.ObjectType, entry.ObjectUri, entry.Mode);
			}
		}

		// Token: 0x06002D17 RID: 11543 RVA: 0x00094F10 File Offset: 0x00093110
		internal static void RegisterChannelTemplate(ChannelData channel)
		{
			RemotingConfiguration.channelTemplates[channel.Id] = channel;
		}

		// Token: 0x06002D18 RID: 11544 RVA: 0x00094F24 File Offset: 0x00093124
		internal static void RegisterClientProviderTemplate(ProviderData prov)
		{
			RemotingConfiguration.clientProviderTemplates[prov.Id] = prov;
		}

		// Token: 0x06002D19 RID: 11545 RVA: 0x00094F38 File Offset: 0x00093138
		internal static void RegisterServerProviderTemplate(ProviderData prov)
		{
			RemotingConfiguration.serverProviderTemplates[prov.Id] = prov;
		}

		// Token: 0x06002D1A RID: 11546 RVA: 0x00094F4C File Offset: 0x0009314C
		internal static void RegisterChannels(ArrayList channels, bool onlyDelayed)
		{
			foreach (object obj in channels)
			{
				ChannelData channelData = (ChannelData)obj;
				if (!onlyDelayed || !(channelData.DelayLoadAsClientChannel != "true"))
				{
					if (!RemotingConfiguration.defaultDelayedConfigRead || !(channelData.DelayLoadAsClientChannel == "true"))
					{
						if (channelData.Ref != null)
						{
							ChannelData channelData2 = (ChannelData)RemotingConfiguration.channelTemplates[channelData.Ref];
							if (channelData2 == null)
							{
								throw new RemotingException("Channel template '" + channelData.Ref + "' not found");
							}
							channelData.CopyFrom(channelData2);
						}
						foreach (object obj2 in channelData.ServerProviders)
						{
							ProviderData providerData = (ProviderData)obj2;
							if (providerData.Ref != null)
							{
								ProviderData providerData2 = (ProviderData)RemotingConfiguration.serverProviderTemplates[providerData.Ref];
								if (providerData2 == null)
								{
									throw new RemotingException("Provider template '" + providerData.Ref + "' not found");
								}
								providerData.CopyFrom(providerData2);
							}
						}
						foreach (object obj3 in channelData.ClientProviders)
						{
							ProviderData providerData3 = (ProviderData)obj3;
							if (providerData3.Ref != null)
							{
								ProviderData providerData4 = (ProviderData)RemotingConfiguration.clientProviderTemplates[providerData3.Ref];
								if (providerData4 == null)
								{
									throw new RemotingException("Provider template '" + providerData3.Ref + "' not found");
								}
								providerData3.CopyFrom(providerData4);
							}
						}
						ChannelServices.RegisterChannelConfig(channelData);
					}
				}
			}
		}

		// Token: 0x06002D1B RID: 11547 RVA: 0x000951A0 File Offset: 0x000933A0
		internal static void RegisterTypes(ArrayList types)
		{
			foreach (object obj in types)
			{
				TypeEntry typeEntry = (TypeEntry)obj;
				if (typeEntry is ActivatedClientTypeEntry)
				{
					RemotingConfiguration.RegisterActivatedClientType((ActivatedClientTypeEntry)typeEntry);
				}
				else if (typeEntry is ActivatedServiceTypeEntry)
				{
					RemotingConfiguration.RegisterActivatedServiceType((ActivatedServiceTypeEntry)typeEntry);
				}
				else if (typeEntry is WellKnownClientTypeEntry)
				{
					RemotingConfiguration.RegisterWellKnownClientType((WellKnownClientTypeEntry)typeEntry);
				}
				else if (typeEntry is WellKnownServiceTypeEntry)
				{
					RemotingConfiguration.RegisterWellKnownServiceType((WellKnownServiceTypeEntry)typeEntry);
				}
			}
		}

		/// <summary>Indicates whether the server channels in this application domain return filtered or complete exception information to local or remote callers.</summary>
		/// <returns>true if only filtered exception information is returned to local or remote callers, as specified by the <paramref name="isLocalRequest" /> parameter; false if complete exception information is returned.</returns>
		/// <param name="isLocalRequest">true to specify local callers; false to specify remote callers. </param>
		// Token: 0x06002D1C RID: 11548 RVA: 0x0009526C File Offset: 0x0009346C
		public static bool CustomErrorsEnabled(bool isLocalRequest)
		{
			return !(RemotingConfiguration._errorMode == "off") && (RemotingConfiguration._errorMode == "on" || !isLocalRequest);
		}

		// Token: 0x06002D1D RID: 11549 RVA: 0x000952AC File Offset: 0x000934AC
		internal static void SetCustomErrorsMode(string mode)
		{
			if (mode == null)
			{
				throw new RemotingException("mode attribute is required");
			}
			string text = mode.ToLower();
			if (text != "on" && text != "off" && text != "remoteonly")
			{
				throw new RemotingException("Invalid custom error mode: " + mode);
			}
			RemotingConfiguration._errorMode = text;
		}

		// Token: 0x0400136F RID: 4975
		private static string applicationID = null;

		// Token: 0x04001370 RID: 4976
		private static string applicationName = null;

		// Token: 0x04001371 RID: 4977
		private static string processGuid = null;

		// Token: 0x04001372 RID: 4978
		private static bool defaultConfigRead = false;

		// Token: 0x04001373 RID: 4979
		private static bool defaultDelayedConfigRead = false;

		// Token: 0x04001374 RID: 4980
		private static string _errorMode;

		// Token: 0x04001375 RID: 4981
		private static Hashtable wellKnownClientEntries = new Hashtable();

		// Token: 0x04001376 RID: 4982
		private static Hashtable activatedClientEntries = new Hashtable();

		// Token: 0x04001377 RID: 4983
		private static Hashtable wellKnownServiceEntries = new Hashtable();

		// Token: 0x04001378 RID: 4984
		private static Hashtable activatedServiceEntries = new Hashtable();

		// Token: 0x04001379 RID: 4985
		private static Hashtable channelTemplates = new Hashtable();

		// Token: 0x0400137A RID: 4986
		private static Hashtable clientProviderTemplates = new Hashtable();

		// Token: 0x0400137B RID: 4987
		private static Hashtable serverProviderTemplates = new Hashtable();
	}
}
