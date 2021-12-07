using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Security.Principal
{
	/// <summary>Represents a Windows user.</summary>
	// Token: 0x0200066B RID: 1643
	[ComVisible(true)]
	[Serializable]
	public class WindowsIdentity : IDisposable, ISerializable, IDeserializationCallback, IIdentity
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.WindowsIdentity" /> class for the user represented by the specified Windows account token.</summary>
		/// <param name="userToken">The account token for the user on whose behalf the code is running. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="userToken" /> is 0.-or-<paramref name="userToken" /> is duplicated and invalid for impersonation.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions. -or-A Win32 error occurred.</exception>
		// Token: 0x06003E50 RID: 15952 RVA: 0x000D608C File Offset: 0x000D428C
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlPrincipal\"/>\n</PermissionSet>\n")]
		public WindowsIdentity(IntPtr userToken) : this(userToken, null, WindowsAccountType.Normal, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.WindowsIdentity" /> class for the user represented by the specified Windows account token and the specified authentication type.</summary>
		/// <param name="userToken">The account token for the user on whose behalf the code is running. </param>
		/// <param name="type">(Informational) The type of authentication used to identify the user. For more information, see Remarks.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="userToken" /> is 0.-or-<paramref name="userToken" /> is duplicated and invalid for impersonation.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions. -or-A Win32 error occurred.</exception>
		// Token: 0x06003E51 RID: 15953 RVA: 0x000D6098 File Offset: 0x000D4298
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlPrincipal\"/>\n</PermissionSet>\n")]
		public WindowsIdentity(IntPtr userToken, string type) : this(userToken, type, WindowsAccountType.Normal, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.WindowsIdentity" /> class for the user represented by the specified Windows account token, the specified authentication type, and the specified Windows account type.</summary>
		/// <param name="userToken">The account token for the user on whose behalf the code is running. </param>
		/// <param name="type">(Informational) The type of authentication used to identify the user. For more information, see Remarks.</param>
		/// <param name="acctType">One of the <see cref="T:System.Security.Principal.WindowsAccountType" /> values. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="userToken" /> is 0.-or-<paramref name="userToken" /> is duplicated and invalid for impersonation.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions. -or-A Win32 error occurred.</exception>
		// Token: 0x06003E52 RID: 15954 RVA: 0x000D60A4 File Offset: 0x000D42A4
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlPrincipal\"/>\n</PermissionSet>\n")]
		public WindowsIdentity(IntPtr userToken, string type, WindowsAccountType acctType) : this(userToken, type, acctType, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.WindowsIdentity" /> class for the user represented by the specified Windows account token, the specified authentication type, the specified Windows account type, and the specified authentication status.</summary>
		/// <param name="userToken">The account token for the user on whose behalf the code is running. </param>
		/// <param name="type">(Informational) The type of authentication used to identify the user. For more information, see Remarks.</param>
		/// <param name="acctType">One of the <see cref="T:System.Security.Principal.WindowsAccountType" /> values. </param>
		/// <param name="isAuthenticated">true to indicate that the user is authenticated; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="userToken" /> is 0.-or-<paramref name="userToken" /> is duplicated and invalid for impersonation.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions. -or-A Win32 error occurred.</exception>
		// Token: 0x06003E53 RID: 15955 RVA: 0x000D60B0 File Offset: 0x000D42B0
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlPrincipal\"/>\n</PermissionSet>\n")]
		public WindowsIdentity(IntPtr userToken, string type, WindowsAccountType acctType, bool isAuthenticated)
		{
			this._type = type;
			this._account = acctType;
			this._authenticated = isAuthenticated;
			this._name = null;
			this.SetToken(userToken);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.WindowsIdentity" /> class for the user represented by the specified User Principal Name (UPN).</summary>
		/// <param name="sUserPrincipalName">The UPN for the user on whose behalf the code is running. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">Windows returned the Windows NT status code STATUS_ACCESS_DENIED.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions. </exception>
		// Token: 0x06003E54 RID: 15956 RVA: 0x000D60E8 File Offset: 0x000D42E8
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlPrincipal\"/>\n</PermissionSet>\n")]
		public WindowsIdentity(string sUserPrincipalName) : this(sUserPrincipalName, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.WindowsIdentity" /> class for the user represented by the specified User Principal Name (UPN) and the specified authentication type.</summary>
		/// <param name="sUserPrincipalName">The UPN for the user on whose behalf the code is running. </param>
		/// <param name="type">(Informational) The type of authentication used to identify the user. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">Windows returned the Windows NT status code STATUS_ACCESS_DENIED.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions. </exception>
		// Token: 0x06003E55 RID: 15957 RVA: 0x000D60F4 File Offset: 0x000D42F4
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlPrincipal\"/>\n</PermissionSet>\n")]
		public WindowsIdentity(string sUserPrincipalName, string type)
		{
			if (sUserPrincipalName == null)
			{
				throw new NullReferenceException("sUserPrincipalName");
			}
			IntPtr userToken = WindowsIdentity.GetUserToken(sUserPrincipalName);
			if (!WindowsIdentity.IsPosix && userToken == IntPtr.Zero)
			{
				throw new ArgumentException("only for Windows Server 2003 +");
			}
			this._authenticated = true;
			this._account = WindowsAccountType.Normal;
			this._type = type;
			this.SetToken(userToken);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.WindowsIdentity" /> class for the user represented by information in a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> stream.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> containing the account information for the user. </param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that indicates the stream characteristics. </param>
		/// <exception cref="T:System.NotSupportedException">A <see cref="T:System.Security.Principal.WindowsIdentity" /> cannot be serialized across processes. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions. -or-A Win32 error occurred.</exception>
		// Token: 0x06003E56 RID: 15958 RVA: 0x000D6160 File Offset: 0x000D4360
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlPrincipal\"/>\n</PermissionSet>\n")]
		public WindowsIdentity(SerializationInfo info, StreamingContext context)
		{
			this._info = info;
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and is called back by the deserialization event when deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event. </param>
		// Token: 0x06003E58 RID: 15960 RVA: 0x000D617C File Offset: 0x000D437C
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			this._token = (IntPtr)this._info.GetValue("m_userToken", typeof(IntPtr));
			this._name = this._info.GetString("m_name");
			if (this._name != null)
			{
				string tokenName = WindowsIdentity.GetTokenName(this._token);
				if (tokenName != this._name)
				{
					throw new SerializationException("Token-Name mismatch.");
				}
			}
			else
			{
				this._name = WindowsIdentity.GetTokenName(this._token);
				if (this._name == string.Empty || this._name == null)
				{
					throw new SerializationException("Token doesn't match a user.");
				}
			}
			this._type = this._info.GetString("m_type");
			this._account = (WindowsAccountType)((int)this._info.GetValue("m_acctType", typeof(WindowsAccountType)));
			this._authenticated = this._info.GetBoolean("m_isAuthenticated");
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the logical context information needed to recreate an instance of this execution context.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object containing the information required to serialize the <see cref="T:System.Collections.Hashtable" />. </param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object containing the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Hashtable" />. </param>
		// Token: 0x06003E59 RID: 15961 RVA: 0x000D628C File Offset: 0x000D448C
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("m_userToken", this._token);
			info.AddValue("m_name", this._name);
			info.AddValue("m_type", this._type);
			info.AddValue("m_acctType", this._account);
			info.AddValue("m_isAuthenticated", this._authenticated);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Security.Principal.WindowsIdentity" />. </summary>
		// Token: 0x06003E5A RID: 15962 RVA: 0x000D62F8 File Offset: 0x000D44F8
		[ComVisible(false)]
		public void Dispose()
		{
			this._token = IntPtr.Zero;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Principal.WindowsIdentity" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06003E5B RID: 15963 RVA: 0x000D6308 File Offset: 0x000D4508
		[ComVisible(false)]
		protected virtual void Dispose(bool disposing)
		{
			this._token = IntPtr.Zero;
		}

		/// <summary>Returns a <see cref="T:System.Security.Principal.WindowsIdentity" /> object that represents an anonymous user.</summary>
		/// <returns>A <see cref="T:System.Security.Principal.WindowsIdentity" /> object that represents an anonymous user.</returns>
		// Token: 0x06003E5C RID: 15964 RVA: 0x000D6318 File Offset: 0x000D4518
		public static WindowsIdentity GetAnonymous()
		{
			WindowsIdentity windowsIdentity;
			if (WindowsIdentity.IsPosix)
			{
				windowsIdentity = new WindowsIdentity("nobody");
				windowsIdentity._account = WindowsAccountType.Anonymous;
				windowsIdentity._authenticated = false;
				windowsIdentity._type = string.Empty;
			}
			else
			{
				windowsIdentity = new WindowsIdentity(IntPtr.Zero, string.Empty, WindowsAccountType.Anonymous, false);
				windowsIdentity._name = string.Empty;
			}
			return windowsIdentity;
		}

		/// <summary>Returns a <see cref="T:System.Security.Principal.WindowsIdentity" /> object that represents the current Windows user.</summary>
		/// <returns>A <see cref="T:System.Security.Principal.WindowsIdentity" /> object that represents the current user.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPrincipal" />
		/// </PermissionSet>
		// Token: 0x06003E5D RID: 15965 RVA: 0x000D6378 File Offset: 0x000D4578
		public static WindowsIdentity GetCurrent()
		{
			return new WindowsIdentity(WindowsIdentity.GetCurrentToken(), null, WindowsAccountType.Normal, true);
		}

		/// <summary>Returns a <see cref="T:System.Security.Principal.WindowsIdentity" /> object that represents the Windows identity for either the thread or the process, depending on the value of the <paramref name="ifImpersonating" /> parameter.</summary>
		/// <returns>A <see cref="T:System.Security.Principal.WindowsIdentity" /> object that represents a Windows user.</returns>
		/// <param name="ifImpersonating">true to return the <see cref="T:System.Security.Principal.WindowsIdentity" /> only if the thread is currently impersonating; false to return the <see cref="T:System.Security.Principal.WindowsIdentity" />   of the thread if it is impersonating or the <see cref="T:System.Security.Principal.WindowsIdentity" /> of the process if the thread is not currently impersonating.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPrincipal" />
		/// </PermissionSet>
		// Token: 0x06003E5E RID: 15966 RVA: 0x000D6388 File Offset: 0x000D4588
		[MonoTODO("need icall changes")]
		public static WindowsIdentity GetCurrent(bool ifImpersonating)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a <see cref="T:System.Security.Principal.WindowsIdentity" /> object that represents the current Windows user, using the specified desired token access level.</summary>
		/// <returns>A <see cref="T:System.Security.Principal.WindowsIdentity" /> object that represents the current user.</returns>
		/// <param name="desiredAccess">A bitwise combination of the <see cref="T:System.Security.Principal.TokenAccessLevels" /> values. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPrincipal" />
		/// </PermissionSet>
		// Token: 0x06003E5F RID: 15967 RVA: 0x000D6390 File Offset: 0x000D4590
		[MonoTODO("need icall changes")]
		public static WindowsIdentity GetCurrent(TokenAccessLevels desiredAccess)
		{
			throw new NotImplementedException();
		}

		/// <summary>Impersonates the user represented by the <see cref="T:System.Security.Principal.WindowsIdentity" /> object.</summary>
		/// <returns>A <see cref="T:System.Security.Principal.WindowsImpersonationContext" /> object that represents the Windows user prior to impersonation; this can be used to revert to the original user's context.</returns>
		/// <exception cref="T:System.InvalidOperationException">An anonymous identity attempted to perform an impersonation.</exception>
		/// <exception cref="T:System.Security.SecurityException">A Win32 error occurred.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPrincipal" />
		/// </PermissionSet>
		// Token: 0x06003E60 RID: 15968 RVA: 0x000D6398 File Offset: 0x000D4598
		public virtual WindowsImpersonationContext Impersonate()
		{
			return new WindowsImpersonationContext(this._token);
		}

		/// <summary>Impersonates the user represented by the specified user token.</summary>
		/// <returns>A <see cref="T:System.Security.Principal.WindowsImpersonationContext" /> object that represents the Windows user prior to impersonation; this object can be used to revert to the original user's context.</returns>
		/// <param name="userToken">The handle of a Windows account token. This token is usually retrieved through a call to unmanaged code, such as a call to the Win32 API LogonUser function. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">Windows returned the Windows NT status code STATUS_ACCESS_DENIED.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPrincipal" />
		/// </PermissionSet>
		// Token: 0x06003E61 RID: 15969 RVA: 0x000D63A8 File Offset: 0x000D45A8
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlPrincipal\"/>\n</PermissionSet>\n")]
		public static WindowsImpersonationContext Impersonate(IntPtr userToken)
		{
			return new WindowsImpersonationContext(userToken);
		}

		/// <summary>Gets the type of authentication used to identify the user.</summary>
		/// <returns>The type of authentication used to identify the user.</returns>
		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x06003E62 RID: 15970 RVA: 0x000D63B0 File Offset: 0x000D45B0
		public string AuthenticationType
		{
			get
			{
				return this._type;
			}
		}

		/// <summary>Gets a value indicating whether the user account is identified as an anonymous account by the system.</summary>
		/// <returns>true if the user account is an anonymous account; otherwise, false.</returns>
		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x06003E63 RID: 15971 RVA: 0x000D63B8 File Offset: 0x000D45B8
		public virtual bool IsAnonymous
		{
			get
			{
				return this._account == WindowsAccountType.Anonymous;
			}
		}

		/// <summary>Gets a value indicating whether the user has been authenticated by Windows.</summary>
		/// <returns>true if the user was authenticated; otherwise, false.</returns>
		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x06003E64 RID: 15972 RVA: 0x000D63C4 File Offset: 0x000D45C4
		public virtual bool IsAuthenticated
		{
			get
			{
				return this._authenticated;
			}
		}

		/// <summary>Gets a value indicating whether the user account is identified as a <see cref="F:System.Security.Principal.WindowsAccountType.Guest" /> account by the system.</summary>
		/// <returns>true if the user account is a <see cref="F:System.Security.Principal.WindowsAccountType.Guest" /> account; otherwise, false.</returns>
		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x06003E65 RID: 15973 RVA: 0x000D63CC File Offset: 0x000D45CC
		public virtual bool IsGuest
		{
			get
			{
				return this._account == WindowsAccountType.Guest;
			}
		}

		/// <summary>Gets a value indicating whether the user account is identified as a <see cref="F:System.Security.Principal.WindowsAccountType.System" /> account by the system.</summary>
		/// <returns>true if the user account is a <see cref="F:System.Security.Principal.WindowsAccountType.System" /> account; otherwise, false.</returns>
		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x06003E66 RID: 15974 RVA: 0x000D63D8 File Offset: 0x000D45D8
		public virtual bool IsSystem
		{
			get
			{
				return this._account == WindowsAccountType.System;
			}
		}

		/// <summary>Gets the user's Windows logon name.</summary>
		/// <returns>The Windows logon name of the user on whose behalf the code is being run.</returns>
		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x06003E67 RID: 15975 RVA: 0x000D63E4 File Offset: 0x000D45E4
		public virtual string Name
		{
			get
			{
				if (this._name == null)
				{
					this._name = WindowsIdentity.GetTokenName(this._token);
				}
				return this._name;
			}
		}

		/// <summary>Gets the Windows account token for the user.</summary>
		/// <returns>The handle of the access token associated with the current execution thread.</returns>
		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x06003E68 RID: 15976 RVA: 0x000D6414 File Offset: 0x000D4614
		public virtual IntPtr Token
		{
			get
			{
				return this._token;
			}
		}

		/// <summary>Gets the groups the current Windows user belongs to.</summary>
		/// <returns>An <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> object representing the groups the current Windows user belongs to.</returns>
		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x06003E69 RID: 15977 RVA: 0x000D641C File Offset: 0x000D461C
		[MonoTODO("not implemented")]
		public IdentityReferenceCollection Groups
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the impersonation level for the user.</summary>
		/// <returns>One of the <see cref="T:System.Management.ImpersonationLevel" /> values. </returns>
		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x06003E6A RID: 15978 RVA: 0x000D6424 File Offset: 0x000D4624
		[MonoTODO("not implemented")]
		[ComVisible(false)]
		public TokenImpersonationLevel ImpersonationLevel
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the security identifier (SID) for the token owner.</summary>
		/// <returns>A <see cref="T:System.Security.Principal.SecurityIdentifier" /> object for the token owner.</returns>
		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x06003E6B RID: 15979 RVA: 0x000D642C File Offset: 0x000D462C
		[MonoTODO("not implemented")]
		[ComVisible(false)]
		public SecurityIdentifier Owner
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the security identifier (SID) for the user.</summary>
		/// <returns>A <see cref="T:System.Security.Principal.SecurityIdentifier" /> object for the user.</returns>
		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x06003E6C RID: 15980 RVA: 0x000D6434 File Offset: 0x000D4634
		[ComVisible(false)]
		[MonoTODO("not implemented")]
		public SecurityIdentifier User
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x06003E6D RID: 15981 RVA: 0x000D643C File Offset: 0x000D463C
		private static bool IsPosix
		{
			get
			{
				int platform = (int)Environment.Platform;
				return platform == 128 || platform == 4 || platform == 6;
			}
		}

		// Token: 0x06003E6E RID: 15982 RVA: 0x000D6468 File Offset: 0x000D4668
		private void SetToken(IntPtr token)
		{
			if (WindowsIdentity.IsPosix)
			{
				this._token = token;
				if (this._type == null)
				{
					this._type = "POSIX";
				}
				if (this._token == IntPtr.Zero)
				{
					this._account = WindowsAccountType.System;
				}
			}
			else
			{
				if (token == WindowsIdentity.invalidWindows && this._account != WindowsAccountType.Anonymous)
				{
					throw new ArgumentException("Invalid token");
				}
				this._token = token;
				if (this._type == null)
				{
					this._type = "NTLM";
				}
			}
		}

		// Token: 0x06003E6F RID: 15983
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string[] _GetRoles(IntPtr token);

		// Token: 0x06003E70 RID: 15984
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetCurrentToken();

		// Token: 0x06003E71 RID: 15985
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetTokenName(IntPtr token);

		// Token: 0x06003E72 RID: 15986
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetUserToken(string username);

		// Token: 0x04001B1F RID: 6943
		private IntPtr _token;

		// Token: 0x04001B20 RID: 6944
		private string _type;

		// Token: 0x04001B21 RID: 6945
		private WindowsAccountType _account;

		// Token: 0x04001B22 RID: 6946
		private bool _authenticated;

		// Token: 0x04001B23 RID: 6947
		private string _name;

		// Token: 0x04001B24 RID: 6948
		private SerializationInfo _info;

		// Token: 0x04001B25 RID: 6949
		private static IntPtr invalidWindows = IntPtr.Zero;
	}
}
