using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Net.Cache;
using System.Net.Configuration;
using System.Net.Security;
using System.Runtime.Serialization;
using System.Security.Principal;

namespace System.Net
{
	/// <summary>Makes a request to a Uniform Resource Identifier (URI). This is an abstract class.</summary>
	// Token: 0x0200041F RID: 1055
	[Serializable]
	public abstract class WebRequest : MarshalByRefObject, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebRequest" /> class.</summary>
		// Token: 0x06002624 RID: 9764 RVA: 0x00076D4C File Offset: 0x00074F4C
		protected WebRequest()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebRequest" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains the information required to serialize the new <see cref="T:System.Net.WebRequest" /> instance. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that indicates the source of the serialized stream associated with the new <see cref="T:System.Net.WebRequest" /> instance. </param>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the constructor, when the constructor is not overridden in a descendant class. </exception>
		// Token: 0x06002625 RID: 9765 RVA: 0x00076D5C File Offset: 0x00074F5C
		protected WebRequest(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
		}

		// Token: 0x06002626 RID: 9766 RVA: 0x00076D6C File Offset: 0x00074F6C
		static WebRequest()
		{
			object section = ConfigurationManager.GetSection("system.net/webRequestModules");
			System.Net.Configuration.WebRequestModulesSection webRequestModulesSection = section as System.Net.Configuration.WebRequestModulesSection;
			if (webRequestModulesSection != null)
			{
				foreach (object obj in webRequestModulesSection.WebRequestModules)
				{
					System.Net.Configuration.WebRequestModuleElement webRequestModuleElement = (System.Net.Configuration.WebRequestModuleElement)obj;
					WebRequest.AddPrefix(webRequestModuleElement.Prefix, webRequestModuleElement.Type);
				}
				return;
			}
			System.Configuration.ConfigurationSettings.GetConfig("system.net/webRequestModules");
		}

		/// <summary>When overridden in a descendant class, populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data needed to serialize the <see cref="T:System.Net.WebRequest" />.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" />, which holds the serialized data for the <see cref="T:System.Net.WebRequest" />. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the destination of the serialized stream associated with the new <see cref="T:System.Net.WebRequest" />. </param>
		/// <exception cref="T:System.NotImplementedException">An attempt is made to serialize the object, when the interface is not overridden in a descendant class. </exception>
		// Token: 0x06002627 RID: 9767 RVA: 0x00076E20 File Offset: 0x00075020
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002628 RID: 9768 RVA: 0x00076E28 File Offset: 0x00075028
		private static void AddDynamicPrefix(string protocol, string implementor)
		{
			Type type = typeof(WebRequest).Assembly.GetType("System.Net." + implementor);
			if (type == null)
			{
				return;
			}
			WebRequest.AddPrefix(protocol, type);
		}

		// Token: 0x06002629 RID: 9769 RVA: 0x00076E64 File Offset: 0x00075064
		private static Exception GetMustImplement()
		{
			return new NotImplementedException("This method must be implemented in derived classes");
		}

		/// <summary>Gets or sets values indicating the level of authentication and impersonation used for this request.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Net.Security.AuthenticationLevel" /> values. The default value is <see cref="F:System.Net.Security.AuthenticationLevel.MutualAuthRequested" />.In mutual authentication, both the client and server present credentials to establish their identity. The <see cref="F:System.Net.Security.AuthenticationLevel.MutualAuthRequired" /> and <see cref="F:System.Net.Security.AuthenticationLevel.MutualAuthRequested" /> values are relevant for Kerberos authentication. Kerberos authentication can be supported directly, or can be used if the Negotiate security protocol is used to select the actual security protocol. For more information about authentication protocols, see Internet Authentication.To determine whether mutual authentication occurred, check the <see cref="P:System.Net.WebResponse.IsMutuallyAuthenticated" /> property. If you specify the <see cref="F:System.Net.Security.AuthenticationLevel.MutualAuthRequired" /> authentication flag value and mutual authentication does not occur, your application will receive an <see cref="T:System.IO.IOException" /> with a <see cref="T:System.Net.ProtocolViolationException" /> inner exception indicating that mutual authentication failed.</returns>
		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x0600262A RID: 9770 RVA: 0x00076E70 File Offset: 0x00075070
		// (set) Token: 0x0600262B RID: 9771 RVA: 0x00076E78 File Offset: 0x00075078
		public System.Net.Security.AuthenticationLevel AuthenticationLevel
		{
			get
			{
				return this.authentication_level;
			}
			set
			{
				this.authentication_level = value;
			}
		}

		/// <summary>Gets or sets the cache policy for this request.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.RequestCachePolicy" /> object that defines a cache policy.</returns>
		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x0600262C RID: 9772 RVA: 0x00076E84 File Offset: 0x00075084
		// (set) Token: 0x0600262D RID: 9773 RVA: 0x00076E8C File Offset: 0x0007508C
		public virtual System.Net.Cache.RequestCachePolicy CachePolicy
		{
			get
			{
				throw WebRequest.GetMustImplement();
			}
			set
			{
			}
		}

		/// <summary>When overridden in a descendant class, gets or sets the name of the connection group for the request.</summary>
		/// <returns>The name of the connection group for the request.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x0600262E RID: 9774 RVA: 0x00076E90 File Offset: 0x00075090
		// (set) Token: 0x0600262F RID: 9775 RVA: 0x00076E98 File Offset: 0x00075098
		public virtual string ConnectionGroupName
		{
			get
			{
				throw WebRequest.GetMustImplement();
			}
			set
			{
				throw WebRequest.GetMustImplement();
			}
		}

		/// <summary>When overridden in a descendant class, gets or sets the content length of the request data being sent.</summary>
		/// <returns>The number of bytes of request data being sent.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x06002630 RID: 9776 RVA: 0x00076EA0 File Offset: 0x000750A0
		// (set) Token: 0x06002631 RID: 9777 RVA: 0x00076EA8 File Offset: 0x000750A8
		public virtual long ContentLength
		{
			get
			{
				throw WebRequest.GetMustImplement();
			}
			set
			{
				throw WebRequest.GetMustImplement();
			}
		}

		/// <summary>When overridden in a descendant class, gets or sets the content type of the request data being sent.</summary>
		/// <returns>The content type of the request data.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x06002632 RID: 9778 RVA: 0x00076EB0 File Offset: 0x000750B0
		// (set) Token: 0x06002633 RID: 9779 RVA: 0x00076EB8 File Offset: 0x000750B8
		public virtual string ContentType
		{
			get
			{
				throw WebRequest.GetMustImplement();
			}
			set
			{
				throw WebRequest.GetMustImplement();
			}
		}

		/// <summary>When overridden in a descendant class, gets or sets the network credentials used for authenticating the request with the Internet resource.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentials" /> containing the authentication credentials associated with the request. The default is null.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x06002634 RID: 9780 RVA: 0x00076EC0 File Offset: 0x000750C0
		// (set) Token: 0x06002635 RID: 9781 RVA: 0x00076EC8 File Offset: 0x000750C8
		public virtual ICredentials Credentials
		{
			get
			{
				throw WebRequest.GetMustImplement();
			}
			set
			{
				throw WebRequest.GetMustImplement();
			}
		}

		/// <summary>Gets or sets the default cache policy for this request.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.HttpRequestCachePolicy" /> that specifies the cache policy in effect for this request when no other policy is applicable.</returns>
		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x06002636 RID: 9782 RVA: 0x00076ED0 File Offset: 0x000750D0
		// (set) Token: 0x06002637 RID: 9783 RVA: 0x00076ED8 File Offset: 0x000750D8
		public static System.Net.Cache.RequestCachePolicy DefaultCachePolicy
		{
			get
			{
				throw WebRequest.GetMustImplement();
			}
			set
			{
				throw WebRequest.GetMustImplement();
			}
		}

		/// <summary>When overridden in a descendant class, gets or sets the collection of header name/value pairs associated with the request.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> containing the header name/value pairs associated with this request.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x06002638 RID: 9784 RVA: 0x00076EE0 File Offset: 0x000750E0
		// (set) Token: 0x06002639 RID: 9785 RVA: 0x00076EE8 File Offset: 0x000750E8
		public virtual WebHeaderCollection Headers
		{
			get
			{
				throw WebRequest.GetMustImplement();
			}
			set
			{
				throw WebRequest.GetMustImplement();
			}
		}

		/// <summary>Gets or sets the impersonation level for the current request.</summary>
		/// <returns>A <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> value.</returns>
		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x0600263A RID: 9786 RVA: 0x00076EF0 File Offset: 0x000750F0
		// (set) Token: 0x0600263B RID: 9787 RVA: 0x00076EF8 File Offset: 0x000750F8
		public TokenImpersonationLevel ImpersonationLevel
		{
			get
			{
				throw WebRequest.GetMustImplement();
			}
			set
			{
				throw WebRequest.GetMustImplement();
			}
		}

		/// <summary>When overridden in a descendant class, gets or sets the protocol method to use in this request.</summary>
		/// <returns>The protocol method to use in this request.</returns>
		/// <exception cref="T:System.NotImplementedException">If the property is not overridden in a descendant class, any attempt is made to get or set the property. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x0600263C RID: 9788 RVA: 0x00076F00 File Offset: 0x00075100
		// (set) Token: 0x0600263D RID: 9789 RVA: 0x00076F08 File Offset: 0x00075108
		public virtual string Method
		{
			get
			{
				throw WebRequest.GetMustImplement();
			}
			set
			{
				throw WebRequest.GetMustImplement();
			}
		}

		/// <summary>When overridden in a descendant class, indicates whether to pre-authenticate the request.</summary>
		/// <returns>true to pre-authenticate; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x0600263E RID: 9790 RVA: 0x00076F10 File Offset: 0x00075110
		// (set) Token: 0x0600263F RID: 9791 RVA: 0x00076F18 File Offset: 0x00075118
		public virtual bool PreAuthenticate
		{
			get
			{
				throw WebRequest.GetMustImplement();
			}
			set
			{
				throw WebRequest.GetMustImplement();
			}
		}

		/// <summary>When overridden in a descendant class, gets or sets the network proxy to use to access this Internet resource.</summary>
		/// <returns>The <see cref="T:System.Net.IWebProxy" /> to use to access the Internet resource.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x06002640 RID: 9792 RVA: 0x00076F20 File Offset: 0x00075120
		// (set) Token: 0x06002641 RID: 9793 RVA: 0x00076F28 File Offset: 0x00075128
		public virtual IWebProxy Proxy
		{
			get
			{
				throw WebRequest.GetMustImplement();
			}
			set
			{
				throw WebRequest.GetMustImplement();
			}
		}

		/// <summary>When overridden in a descendant class, gets the URI of the Internet resource associated with the request.</summary>
		/// <returns>A <see cref="T:System.Uri" /> representing the resource associated with the request </returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06002642 RID: 9794 RVA: 0x00076F30 File Offset: 0x00075130
		public virtual System.Uri RequestUri
		{
			get
			{
				throw WebRequest.GetMustImplement();
			}
		}

		/// <summary>Gets or sets the length of time, in milliseconds, before the request times out.</summary>
		/// <returns>The length of time, in milliseconds, until the request times out, or the value <see cref="F:System.Threading.Timeout.Infinite" /> to indicate that the request does not time out. The default value is defined by the descendant class.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06002643 RID: 9795 RVA: 0x00076F38 File Offset: 0x00075138
		// (set) Token: 0x06002644 RID: 9796 RVA: 0x00076F40 File Offset: 0x00075140
		public virtual int Timeout
		{
			get
			{
				throw WebRequest.GetMustImplement();
			}
			set
			{
				throw WebRequest.GetMustImplement();
			}
		}

		/// <summary>When overridden in a descendant class, gets or sets a <see cref="T:System.Boolean" /> value that controls whether <see cref="P:System.Net.CredentialCache.DefaultCredentials" /> are sent with requests.</summary>
		/// <returns>true if the default credentials are used; otherwise false. The default value is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">You attempted to set this property after the request was sent.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06002645 RID: 9797 RVA: 0x00076F48 File Offset: 0x00075148
		// (set) Token: 0x06002646 RID: 9798 RVA: 0x00076F50 File Offset: 0x00075150
		public virtual bool UseDefaultCredentials
		{
			get
			{
				throw WebRequest.GetMustImplement();
			}
			set
			{
				throw WebRequest.GetMustImplement();
			}
		}

		/// <summary>Gets or sets the global HTTP proxy.</summary>
		/// <returns>An <see cref="T:System.Net.IWebProxy" /> used by every call to instances of <see cref="T:System.Net.WebRequest" />.</returns>
		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x06002647 RID: 9799 RVA: 0x00076F58 File Offset: 0x00075158
		// (set) Token: 0x06002648 RID: 9800 RVA: 0x00076FBC File Offset: 0x000751BC
		public static IWebProxy DefaultWebProxy
		{
			get
			{
				if (!WebRequest.isDefaultWebProxySet)
				{
					object obj = WebRequest.lockobj;
					lock (obj)
					{
						if (WebRequest.defaultWebProxy == null)
						{
							WebRequest.defaultWebProxy = WebRequest.GetDefaultWebProxy();
						}
					}
				}
				return WebRequest.defaultWebProxy;
			}
			set
			{
				WebRequest.defaultWebProxy = value;
				WebRequest.isDefaultWebProxySet = true;
			}
		}

		// Token: 0x06002649 RID: 9801 RVA: 0x00076FCC File Offset: 0x000751CC
		[MonoTODO("Needs to respect Module, Proxy.AutoDetect, and Proxy.ScriptLocation config settings")]
		private static IWebProxy GetDefaultWebProxy()
		{
			System.Net.Configuration.DefaultProxySection defaultProxySection = ConfigurationManager.GetSection("system.net/defaultProxy") as System.Net.Configuration.DefaultProxySection;
			if (defaultProxySection == null)
			{
				return WebRequest.GetSystemWebProxy();
			}
			System.Net.Configuration.ProxyElement proxy = defaultProxySection.Proxy;
			WebProxy webProxy;
			if (proxy.UseSystemDefault != System.Net.Configuration.ProxyElement.UseSystemDefaultValues.False && proxy.ProxyAddress == null)
			{
				webProxy = (WebProxy)WebRequest.GetSystemWebProxy();
			}
			else
			{
				webProxy = new WebProxy();
			}
			if (proxy.ProxyAddress != null)
			{
				webProxy.Address = proxy.ProxyAddress;
			}
			if (proxy.BypassOnLocal != System.Net.Configuration.ProxyElement.BypassOnLocalValues.Unspecified)
			{
				webProxy.BypassProxyOnLocal = (proxy.BypassOnLocal == System.Net.Configuration.ProxyElement.BypassOnLocalValues.True);
			}
			return webProxy;
		}

		/// <summary>Aborts the Request </summary>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the method, when the method is not overridden in a descendant class. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600264A RID: 9802 RVA: 0x0007706C File Offset: 0x0007526C
		public virtual void Abort()
		{
			throw WebRequest.GetMustImplement();
		}

		/// <summary>When overridden in a descendant class, provides an asynchronous version of the <see cref="M:System.Net.WebRequest.GetRequestStream" /> method.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous request.</returns>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate. </param>
		/// <param name="state">An object containing state information for this asynchronous request. </param>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the method, when the method is not overridden in a descendant class. </exception>
		// Token: 0x0600264B RID: 9803 RVA: 0x00077074 File Offset: 0x00075274
		public virtual IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state)
		{
			throw WebRequest.GetMustImplement();
		}

		/// <summary>When overridden in a descendant class, begins an asynchronous request for an Internet resource.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous request.</returns>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate. </param>
		/// <param name="state">An object containing state information for this asynchronous request. </param>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the method, when the method is not overridden in a descendant class. </exception>
		// Token: 0x0600264C RID: 9804 RVA: 0x0007707C File Offset: 0x0007527C
		public virtual IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
		{
			throw WebRequest.GetMustImplement();
		}

		/// <summary>Initializes a new <see cref="T:System.Net.WebRequest" /> instance for the specified URI scheme.</summary>
		/// <returns>A <see cref="T:System.Net.WebRequest" /> descendant for the specific URI scheme.</returns>
		/// <param name="requestUriString">The URI that identifies the Internet resource. </param>
		/// <exception cref="T:System.NotSupportedException">The request scheme specified in <paramref name="requestUriString" /> has not been registered. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="requestUriString" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have permission to connect to the requested URI or a URI that the request is redirected to. </exception>
		/// <exception cref="T:System.UriFormatException">The URI specified in <paramref name="requestUriString" /> is not a valid URI. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600264D RID: 9805 RVA: 0x00077084 File Offset: 0x00075284
		public static WebRequest Create(string requestUriString)
		{
			if (requestUriString == null)
			{
				throw new ArgumentNullException("requestUriString");
			}
			return WebRequest.Create(new System.Uri(requestUriString));
		}

		/// <summary>Initializes a new <see cref="T:System.Net.WebRequest" /> instance for the specified URI scheme.</summary>
		/// <returns>A <see cref="T:System.Net.WebRequest" /> descendant for the specified URI scheme.</returns>
		/// <param name="requestUri">A <see cref="T:System.Uri" /> containing the URI of the requested resource. </param>
		/// <exception cref="T:System.NotSupportedException">The request scheme specified in <paramref name="requestUri" /> is not registered. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="requestUri" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have permission to connect to the requested URI or a URI that the request is redirected to. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600264E RID: 9806 RVA: 0x000770A4 File Offset: 0x000752A4
		public static WebRequest Create(System.Uri requestUri)
		{
			if (requestUri == null)
			{
				throw new ArgumentNullException("requestUri");
			}
			return WebRequest.GetCreator(requestUri.AbsoluteUri).Create(requestUri);
		}

		/// <summary>Initializes a new <see cref="T:System.Net.WebRequest" /> instance for the specified URI scheme.</summary>
		/// <returns>A <see cref="T:System.Net.WebRequest" /> descendant for the specified URI scheme.</returns>
		/// <param name="requestUri">A <see cref="T:System.Uri" /> containing the URI of the requested resource. </param>
		/// <exception cref="T:System.NotSupportedException">The request scheme specified in <paramref name="requestUri" /> is not registered. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="requestUri" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have permission to connect to the requested URI or a URI that the request is redirected to. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600264F RID: 9807 RVA: 0x000770DC File Offset: 0x000752DC
		public static WebRequest CreateDefault(System.Uri requestUri)
		{
			if (requestUri == null)
			{
				throw new ArgumentNullException("requestUri");
			}
			return WebRequest.GetCreator(requestUri.Scheme).Create(requestUri);
		}

		/// <summary>When overridden in a descendant class, returns a <see cref="T:System.IO.Stream" /> for writing data to the Internet resource.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> to write data to.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that references a pending request for a stream. </param>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the method, when the method is not overridden in a descendant class. </exception>
		// Token: 0x06002650 RID: 9808 RVA: 0x00077114 File Offset: 0x00075314
		public virtual Stream EndGetRequestStream(IAsyncResult asyncResult)
		{
			throw WebRequest.GetMustImplement();
		}

		/// <summary>When overridden in a descendant class, returns a <see cref="T:System.Net.WebResponse" />.</summary>
		/// <returns>A <see cref="T:System.Net.WebResponse" /> that contains a response to the Internet request.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that references a pending request for a response. </param>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the method, when the method is not overridden in a descendant class. </exception>
		// Token: 0x06002651 RID: 9809 RVA: 0x0007711C File Offset: 0x0007531C
		public virtual WebResponse EndGetResponse(IAsyncResult asyncResult)
		{
			throw WebRequest.GetMustImplement();
		}

		/// <summary>When overridden in a descendant class, returns a <see cref="T:System.IO.Stream" /> for writing data to the Internet resource.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> for writing data to the Internet resource.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the method, when the method is not overridden in a descendant class. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002652 RID: 9810 RVA: 0x00077124 File Offset: 0x00075324
		public virtual Stream GetRequestStream()
		{
			throw WebRequest.GetMustImplement();
		}

		/// <summary>When overridden in a descendant class, returns a response to an Internet request.</summary>
		/// <returns>A <see cref="T:System.Net.WebResponse" /> containing the response to the Internet request.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the method, when the method is not overridden in a descendant class. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002653 RID: 9811 RVA: 0x0007712C File Offset: 0x0007532C
		public virtual WebResponse GetResponse()
		{
			throw WebRequest.GetMustImplement();
		}

		/// <summary>Returns a proxy configured with the Internet Explorer settings of the currently impersonated user.</summary>
		/// <returns>An <see cref="T:System.Net.IWebProxy" /> used by every call to instances of <see cref="T:System.Net.WebRequest" />.</returns>
		// Token: 0x06002654 RID: 9812 RVA: 0x00077134 File Offset: 0x00075334
		[MonoTODO("Look in other places for proxy config info")]
		public static IWebProxy GetSystemWebProxy()
		{
			string text = Environment.GetEnvironmentVariable("http_proxy");
			if (text == null)
			{
				text = Environment.GetEnvironmentVariable("HTTP_PROXY");
			}
			if (text != null)
			{
				try
				{
					if (!text.StartsWith("http://"))
					{
						text = "http://" + text;
					}
					System.Uri uri = new System.Uri(text);
					IPAddress other;
					if (IPAddress.TryParse(uri.Host, out other))
					{
						if (IPAddress.Any.Equals(other))
						{
							uri = new System.UriBuilder(uri)
							{
								Host = "127.0.0.1"
							}.Uri;
						}
						else if (IPAddress.IPv6Any.Equals(other))
						{
							uri = new System.UriBuilder(uri)
							{
								Host = "[::1]"
							}.Uri;
						}
					}
					return new WebProxy(uri);
				}
				catch (System.UriFormatException)
				{
				}
			}
			return new WebProxy();
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x06002655 RID: 9813 RVA: 0x00077230 File Offset: 0x00075430
		protected virtual void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			throw WebRequest.GetMustImplement();
		}

		/// <summary>Registers a <see cref="T:System.Net.WebRequest" /> descendant for the specified URI.</summary>
		/// <returns>true if registration is successful; otherwise, false.</returns>
		/// <param name="prefix">The complete URI or URI prefix that the <see cref="T:System.Net.WebRequest" /> descendant services. </param>
		/// <param name="creator">The create method that the <see cref="T:System.Net.WebRequest" /> calls to create the <see cref="T:System.Net.WebRequest" /> descendant. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="prefix" /> is null-or- <paramref name="creator" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002656 RID: 9814 RVA: 0x00077238 File Offset: 0x00075438
		public static bool RegisterPrefix(string prefix, IWebRequestCreate creator)
		{
			if (prefix == null)
			{
				throw new ArgumentNullException("prefix");
			}
			if (creator == null)
			{
				throw new ArgumentNullException("creator");
			}
			object syncRoot = WebRequest.prefixes.SyncRoot;
			lock (syncRoot)
			{
				string key = prefix.ToLower(CultureInfo.InvariantCulture);
				if (WebRequest.prefixes.Contains(key))
				{
					return false;
				}
				WebRequest.prefixes.Add(key, creator);
			}
			return true;
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x000772D4 File Offset: 0x000754D4
		private static IWebRequestCreate GetCreator(string prefix)
		{
			int num = -1;
			IWebRequestCreate webRequestCreate = null;
			prefix = prefix.ToLower(CultureInfo.InvariantCulture);
			IDictionaryEnumerator enumerator = WebRequest.prefixes.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string text = enumerator.Key as string;
				if (text.Length > num)
				{
					if (prefix.StartsWith(text))
					{
						num = text.Length;
						webRequestCreate = (IWebRequestCreate)enumerator.Value;
					}
				}
			}
			if (webRequestCreate == null)
			{
				throw new NotSupportedException(prefix);
			}
			return webRequestCreate;
		}

		// Token: 0x06002658 RID: 9816 RVA: 0x0007735C File Offset: 0x0007555C
		internal static void ClearPrefixes()
		{
			WebRequest.prefixes.Clear();
		}

		// Token: 0x06002659 RID: 9817 RVA: 0x00077368 File Offset: 0x00075568
		internal static void RemovePrefix(string prefix)
		{
			WebRequest.prefixes.Remove(prefix);
		}

		// Token: 0x0600265A RID: 9818 RVA: 0x00077378 File Offset: 0x00075578
		internal static void AddPrefix(string prefix, string typeName)
		{
			Type type = Type.GetType(typeName);
			if (type == null)
			{
				throw new System.Configuration.ConfigurationException(string.Format("Type {0} not found", typeName));
			}
			WebRequest.AddPrefix(prefix, type);
		}

		// Token: 0x0600265B RID: 9819 RVA: 0x000773AC File Offset: 0x000755AC
		internal static void AddPrefix(string prefix, Type type)
		{
			object value = Activator.CreateInstance(type, true);
			WebRequest.prefixes[prefix] = value;
		}

		// Token: 0x0400177B RID: 6011
		private static System.Collections.Specialized.HybridDictionary prefixes = new System.Collections.Specialized.HybridDictionary();

		// Token: 0x0400177C RID: 6012
		private static bool isDefaultWebProxySet;

		// Token: 0x0400177D RID: 6013
		private static IWebProxy defaultWebProxy;

		// Token: 0x0400177E RID: 6014
		private System.Net.Security.AuthenticationLevel authentication_level = System.Net.Security.AuthenticationLevel.MutualAuthRequested;

		// Token: 0x0400177F RID: 6015
		private static readonly object lockobj = new object();
	}
}
