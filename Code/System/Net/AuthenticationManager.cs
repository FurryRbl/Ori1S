using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Net.Configuration;

namespace System.Net
{
	/// <summary>Manages the authentication modules called during the client authentication process.</summary>
	// Token: 0x020002B9 RID: 697
	public class AuthenticationManager
	{
		// Token: 0x06001823 RID: 6179 RVA: 0x000424F0 File Offset: 0x000406F0
		private AuthenticationManager()
		{
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x0004250C File Offset: 0x0004070C
		private static void EnsureModules()
		{
			object obj = AuthenticationManager.locker;
			lock (obj)
			{
				if (AuthenticationManager.modules == null)
				{
					AuthenticationManager.modules = new ArrayList();
					object section = ConfigurationManager.GetSection("system.net/authenticationModules");
					System.Net.Configuration.AuthenticationModulesSection authenticationModulesSection = section as System.Net.Configuration.AuthenticationModulesSection;
					if (authenticationModulesSection != null)
					{
						foreach (object obj2 in authenticationModulesSection.AuthenticationModules)
						{
							System.Net.Configuration.AuthenticationModuleElement authenticationModuleElement = (System.Net.Configuration.AuthenticationModuleElement)obj2;
							IAuthenticationModule value = null;
							try
							{
								Type type = Type.GetType(authenticationModuleElement.Type, true);
								value = (IAuthenticationModule)Activator.CreateInstance(type);
							}
							catch
							{
							}
							AuthenticationManager.modules.Add(value);
						}
					}
				}
			}
		}

		/// <summary>Gets or sets the credential policy to be used for resource requests made using the <see cref="T:System.Net.HttpWebRequest" /> class.</summary>
		/// <returns>An object that implements the <see cref="T:System.Net.ICredentialPolicy" /> interface that determines whether credentials are sent with requests. The default value is null.</returns>
		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001826 RID: 6182 RVA: 0x0004262C File Offset: 0x0004082C
		// (set) Token: 0x06001827 RID: 6183 RVA: 0x00042634 File Offset: 0x00040834
		public static ICredentialPolicy CredentialPolicy
		{
			get
			{
				return AuthenticationManager.credential_policy;
			}
			set
			{
				AuthenticationManager.credential_policy = value;
			}
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x0004263C File Offset: 0x0004083C
		private static Exception GetMustImplement()
		{
			return new NotImplementedException();
		}

		/// <summary>Gets the dictionary that contains Service Principal Names (SPNs) that are used to identify hosts during Kerberos authentication for requests made using <see cref="T:System.Net.WebRequest" /> and its derived classes.</summary>
		/// <returns>A writable <see cref="T:System.Collections.Specialized.StringDictionary" /> that contains the SPN values for keys composed of host information. </returns>
		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001829 RID: 6185 RVA: 0x00042644 File Offset: 0x00040844
		[MonoTODO]
		public static System.Collections.Specialized.StringDictionary CustomTargetNameDictionary
		{
			get
			{
				throw AuthenticationManager.GetMustImplement();
			}
		}

		/// <summary>Gets a list of authentication modules that are registered with the authentication manager.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that enables the registered authentication modules to be read.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x0600182A RID: 6186 RVA: 0x0004264C File Offset: 0x0004084C
		public static IEnumerator RegisteredModules
		{
			get
			{
				AuthenticationManager.EnsureModules();
				return AuthenticationManager.modules.GetEnumerator();
			}
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x00042660 File Offset: 0x00040860
		internal static void Clear()
		{
			AuthenticationManager.EnsureModules();
			ArrayList obj = AuthenticationManager.modules;
			lock (obj)
			{
				AuthenticationManager.modules.Clear();
			}
		}

		/// <summary>Calls each registered authentication module to find the first module that can respond to the authentication request.</summary>
		/// <returns>An instance of the <see cref="T:System.Net.Authorization" /> class containing the result of the authorization attempt. If there is no authentication module to respond to the challenge, this method returns null.</returns>
		/// <param name="challenge">The challenge returned by the Internet resource. </param>
		/// <param name="request">The <see cref="T:System.Net.WebRequest" /> that initiated the authentication challenge. </param>
		/// <param name="credentials">The <see cref="T:System.Net.ICredentials" /> associated with this request. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="challenge" /> is null.-or- <paramref name="request" /> is null.-or- <paramref name="credentials" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600182C RID: 6188 RVA: 0x000426B0 File Offset: 0x000408B0
		public static Authorization Authenticate(string challenge, WebRequest request, ICredentials credentials)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			if (credentials == null)
			{
				throw new ArgumentNullException("credentials");
			}
			if (challenge == null)
			{
				throw new ArgumentNullException("challenge");
			}
			return AuthenticationManager.DoAuthenticate(challenge, request, credentials);
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x000426F0 File Offset: 0x000408F0
		private static Authorization DoAuthenticate(string challenge, WebRequest request, ICredentials credentials)
		{
			AuthenticationManager.EnsureModules();
			ArrayList obj = AuthenticationManager.modules;
			lock (obj)
			{
				foreach (object obj2 in AuthenticationManager.modules)
				{
					IAuthenticationModule authenticationModule = (IAuthenticationModule)obj2;
					Authorization authorization = authenticationModule.Authenticate(challenge, request, credentials);
					if (authorization != null)
					{
						authorization.Module = authenticationModule;
						return authorization;
					}
				}
			}
			return null;
		}

		/// <summary>Preauthenticates a request.</summary>
		/// <returns>An instance of the <see cref="T:System.Net.Authorization" /> class if the request can be preauthenticated; otherwise, null. If <paramref name="credentials" /> is null, this method returns null.</returns>
		/// <param name="request">A <see cref="T:System.Net.WebRequest" /> to an Internet resource. </param>
		/// <param name="credentials">The <see cref="T:System.Net.ICredentials" /> associated with the request. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="request" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600182E RID: 6190 RVA: 0x000427B8 File Offset: 0x000409B8
		public static Authorization PreAuthenticate(WebRequest request, ICredentials credentials)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			if (credentials == null)
			{
				return null;
			}
			AuthenticationManager.EnsureModules();
			ArrayList obj = AuthenticationManager.modules;
			lock (obj)
			{
				foreach (object obj2 in AuthenticationManager.modules)
				{
					IAuthenticationModule authenticationModule = (IAuthenticationModule)obj2;
					Authorization authorization = authenticationModule.PreAuthenticate(request, credentials);
					if (authorization != null)
					{
						authorization.Module = authenticationModule;
						return authorization;
					}
				}
			}
			return null;
		}

		/// <summary>Registers an authentication module with the authentication manager.</summary>
		/// <param name="authenticationModule">The <see cref="T:System.Net.IAuthenticationModule" /> to register with the authentication manager. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="authenticationModule" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600182F RID: 6191 RVA: 0x00042898 File Offset: 0x00040A98
		public static void Register(IAuthenticationModule authenticationModule)
		{
			if (authenticationModule == null)
			{
				throw new ArgumentNullException("authenticationModule");
			}
			AuthenticationManager.DoUnregister(authenticationModule.AuthenticationType, false);
			ArrayList obj = AuthenticationManager.modules;
			lock (obj)
			{
				AuthenticationManager.modules.Add(authenticationModule);
			}
		}

		/// <summary>Removes the specified authentication module from the list of registered modules.</summary>
		/// <param name="authenticationModule">The <see cref="T:System.Net.IAuthenticationModule" /> to remove from the list of registered modules. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="authenticationModule" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The specified <see cref="T:System.Net.IAuthenticationModule" /> is not registered. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001830 RID: 6192 RVA: 0x00042904 File Offset: 0x00040B04
		public static void Unregister(IAuthenticationModule authenticationModule)
		{
			if (authenticationModule == null)
			{
				throw new ArgumentNullException("authenticationModule");
			}
			AuthenticationManager.DoUnregister(authenticationModule.AuthenticationType, true);
		}

		/// <summary>Removes authentication modules with the specified authentication scheme from the list of registered modules.</summary>
		/// <param name="authenticationScheme">The authentication scheme of the module to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="authenticationScheme" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">A module for this authentication scheme is not registered. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001831 RID: 6193 RVA: 0x00042924 File Offset: 0x00040B24
		public static void Unregister(string authenticationScheme)
		{
			if (authenticationScheme == null)
			{
				throw new ArgumentNullException("authenticationScheme");
			}
			AuthenticationManager.DoUnregister(authenticationScheme, true);
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x00042940 File Offset: 0x00040B40
		private static void DoUnregister(string authenticationScheme, bool throwEx)
		{
			AuthenticationManager.EnsureModules();
			ArrayList obj = AuthenticationManager.modules;
			lock (obj)
			{
				IAuthenticationModule authenticationModule = null;
				foreach (object obj2 in AuthenticationManager.modules)
				{
					IAuthenticationModule authenticationModule2 = (IAuthenticationModule)obj2;
					string authenticationType = authenticationModule2.AuthenticationType;
					if (string.Compare(authenticationType, authenticationScheme, true) == 0)
					{
						authenticationModule = authenticationModule2;
						break;
					}
				}
				if (authenticationModule == null)
				{
					if (throwEx)
					{
						throw new InvalidOperationException("Scheme not registered.");
					}
				}
				else
				{
					AuthenticationManager.modules.Remove(authenticationModule);
				}
			}
		}

		// Token: 0x04000F69 RID: 3945
		private static ArrayList modules;

		// Token: 0x04000F6A RID: 3946
		private static object locker = new object();

		// Token: 0x04000F6B RID: 3947
		private static ICredentialPolicy credential_policy = null;
	}
}
