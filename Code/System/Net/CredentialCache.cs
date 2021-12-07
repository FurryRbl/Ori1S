using System;
using System.Collections;

namespace System.Net
{
	/// <summary>Provides storage for multiple credentials.</summary>
	// Token: 0x020002F4 RID: 756
	public class CredentialCache : IEnumerable, ICredentials, ICredentialsByHost
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Net.CredentialCache" /> class.</summary>
		// Token: 0x060019EA RID: 6634 RVA: 0x00047B78 File Offset: 0x00045D78
		public CredentialCache()
		{
			this.cache = new Hashtable();
			this.cacheForHost = new Hashtable();
		}

		/// <summary>Gets the system credentials of the application.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentials" /> that represents the system credentials of the application.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="USERNAME" />
		/// </PermissionSet>
		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x060019EC RID: 6636 RVA: 0x00047BB4 File Offset: 0x00045DB4
		[MonoTODO("Need EnvironmentPermission implementation first")]
		public static ICredentials DefaultCredentials
		{
			get
			{
				return CredentialCache.empty;
			}
		}

		/// <summary>Gets the network credentials of the current security context.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkCredential" /> that represents the network credentials of the current user or application.</returns>
		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x060019ED RID: 6637 RVA: 0x00047BBC File Offset: 0x00045DBC
		public static NetworkCredential DefaultNetworkCredentials
		{
			get
			{
				return CredentialCache.empty;
			}
		}

		/// <summary>Returns the <see cref="T:System.Net.NetworkCredential" /> instance associated with the specified Uniform Resource Identifier (URI) and authentication type.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkCredential" /> or, if there is no matching credential in the cache, null.</returns>
		/// <param name="uriPrefix">A <see cref="T:System.Uri" /> that specifies the URI prefix of the resources that the credential grants access to. </param>
		/// <param name="authType">The authentication scheme used by the resource named in <paramref name="uriPrefix" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uriPrefix" /> or <paramref name="authType" /> is null. </exception>
		// Token: 0x060019EE RID: 6638 RVA: 0x00047BC4 File Offset: 0x00045DC4
		public NetworkCredential GetCredential(System.Uri uriPrefix, string authType)
		{
			int num = -1;
			NetworkCredential result = null;
			if (uriPrefix == null || authType == null)
			{
				return null;
			}
			string text = uriPrefix.AbsolutePath;
			text = text.Substring(0, text.LastIndexOf('/'));
			IDictionaryEnumerator enumerator = this.cache.GetEnumerator();
			while (enumerator.MoveNext())
			{
				CredentialCache.CredentialCacheKey credentialCacheKey = enumerator.Key as CredentialCache.CredentialCacheKey;
				if (credentialCacheKey.Length > num)
				{
					if (string.Compare(credentialCacheKey.AuthType, authType, true) == 0)
					{
						System.Uri uriPrefix2 = credentialCacheKey.UriPrefix;
						if (!(uriPrefix2.Scheme != uriPrefix.Scheme))
						{
							if (uriPrefix2.Port == uriPrefix.Port)
							{
								if (!(uriPrefix2.Host != uriPrefix.Host))
								{
									if (text.StartsWith(credentialCacheKey.AbsPath))
									{
										num = credentialCacheKey.Length;
										result = (NetworkCredential)enumerator.Value;
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		/// <summary>Returns an enumerator that can iterate through the <see cref="T:System.Net.CredentialCache" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Net.CredentialCache" />.</returns>
		// Token: 0x060019EF RID: 6639 RVA: 0x00047CD8 File Offset: 0x00045ED8
		public IEnumerator GetEnumerator()
		{
			return this.cache.Values.GetEnumerator();
		}

		/// <summary>Adds a <see cref="T:System.Net.NetworkCredential" /> instance to the credential cache for use with protocols other than SMTP and associates it with a Uniform Resource Identifier (URI) prefix and authentication protocol. </summary>
		/// <param name="uriPrefix">A <see cref="T:System.Uri" /> that specifies the URI prefix of the resources that the credential grants access to. </param>
		/// <param name="authType">The authentication scheme used by the resource named in <paramref name="uriPrefix" />. </param>
		/// <param name="cred">The <see cref="T:System.Net.NetworkCredential" /> to add to the credential cache. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uriPrefix" /> is null. -or- <paramref name="authType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The same credentials are added more than once. </exception>
		// Token: 0x060019F0 RID: 6640 RVA: 0x00047CEC File Offset: 0x00045EEC
		public void Add(System.Uri uriPrefix, string authType, NetworkCredential cred)
		{
			if (uriPrefix == null)
			{
				throw new ArgumentNullException("uriPrefix");
			}
			if (authType == null)
			{
				throw new ArgumentNullException("authType");
			}
			this.cache.Add(new CredentialCache.CredentialCacheKey(uriPrefix, authType), cred);
		}

		/// <summary>Deletes a <see cref="T:System.Net.NetworkCredential" /> instance from the cache if it is associated with the specified Uniform Resource Identifier (URI) prefix and authentication protocol.</summary>
		/// <param name="uriPrefix">A <see cref="T:System.Uri" /> that specifies the URI prefix of the resources that the credential is used for. </param>
		/// <param name="authType">The authentication scheme used by the host named in <paramref name="uriPrefix" />. </param>
		// Token: 0x060019F1 RID: 6641 RVA: 0x00047D2C File Offset: 0x00045F2C
		public void Remove(System.Uri uriPrefix, string authType)
		{
			if (uriPrefix == null)
			{
				throw new ArgumentNullException("uriPrefix");
			}
			if (authType == null)
			{
				throw new ArgumentNullException("authType");
			}
			this.cache.Remove(new CredentialCache.CredentialCacheKey(uriPrefix, authType));
		}

		/// <summary>Returns the <see cref="T:System.Net.NetworkCredential" /> instance associated with the specified host, port, and authentication protocol.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkCredential" /> or, if there is no matching credential in the cache, null.</returns>
		/// <param name="host">A <see cref="T:System.String" /> that identifies the host computer.</param>
		/// <param name="port">A <see cref="T:System.Int32" /> that specifies the port to connect to on <paramref name="host" />.</param>
		/// <param name="authenticationType">A <see cref="T:System.String" /> that identifies the authentication scheme used when connecting to <paramref name="host" />. See Remarks.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="host" /> is null. -or- <paramref name="authType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="authType" /> not an accepted value. See Remarks. -or-<paramref name="host" /> is equal to the empty string ("").</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is less than zero.</exception>
		// Token: 0x060019F2 RID: 6642 RVA: 0x00047D74 File Offset: 0x00045F74
		public NetworkCredential GetCredential(string host, int port, string authenticationType)
		{
			NetworkCredential result = null;
			if (host == null || port < 0 || authenticationType == null)
			{
				return null;
			}
			IDictionaryEnumerator enumerator = this.cacheForHost.GetEnumerator();
			while (enumerator.MoveNext())
			{
				CredentialCache.CredentialCacheForHostKey credentialCacheForHostKey = enumerator.Key as CredentialCache.CredentialCacheForHostKey;
				if (string.Compare(credentialCacheForHostKey.AuthType, authenticationType, true) == 0)
				{
					if (!(credentialCacheForHostKey.Host != host))
					{
						if (credentialCacheForHostKey.Port == port)
						{
							result = (NetworkCredential)enumerator.Value;
						}
					}
				}
			}
			return result;
		}

		/// <summary>Adds a <see cref="T:System.Net.NetworkCredential" /> instance for use with SMTP to the credential cache and associates it with a host computer, port, and authentication protocol. Credentials added using this method are valid for SMTP only. This method does not work for HTTP or FTP requests.</summary>
		/// <param name="host">A <see cref="T:System.String" /> that identifies the host computer.</param>
		/// <param name="port">A <see cref="T:System.Int32" /> that specifies the port to connect to on <paramref name="host" />.</param>
		/// <param name="authenticationType">A <see cref="T:System.String" /> that identifies the authentication scheme used when connecting to <paramref name="host" /> using <paramref name="cred" />. See Remarks.</param>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> to add to the credential cache. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="host" /> is null. -or-<paramref name="authType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="authType" /> not an accepted value. See Remarks. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is less than zero.</exception>
		// Token: 0x060019F3 RID: 6643 RVA: 0x00047E0C File Offset: 0x0004600C
		public void Add(string host, int port, string authenticationType, NetworkCredential credential)
		{
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			if (port < 0)
			{
				throw new ArgumentOutOfRangeException("port");
			}
			if (authenticationType == null)
			{
				throw new ArgumentOutOfRangeException("authenticationType");
			}
			this.cacheForHost.Add(new CredentialCache.CredentialCacheForHostKey(host, port, authenticationType), credential);
		}

		/// <summary>Deletes a <see cref="T:System.Net.NetworkCredential" /> instance from the cache if it is associated with the specified host, port, and authentication protocol.</summary>
		/// <param name="host">A <see cref="T:System.String" /> that identifies the host computer.</param>
		/// <param name="port">A <see cref="T:System.Int32" /> that specifies the port to connect to on <paramref name="host" />.</param>
		/// <param name="authenticationType">A <see cref="T:System.String" /> that identifies the authentication scheme used when connecting to <paramref name="host" />. See Remarks.</param>
		// Token: 0x060019F4 RID: 6644 RVA: 0x00047E64 File Offset: 0x00046064
		public void Remove(string host, int port, string authenticationType)
		{
			if (host == null)
			{
				return;
			}
			if (authenticationType == null)
			{
				return;
			}
			this.cacheForHost.Remove(new CredentialCache.CredentialCacheForHostKey(host, port, authenticationType));
		}

		// Token: 0x0400102A RID: 4138
		private static NetworkCredential empty = new NetworkCredential(string.Empty, string.Empty, string.Empty);

		// Token: 0x0400102B RID: 4139
		private Hashtable cache;

		// Token: 0x0400102C RID: 4140
		private Hashtable cacheForHost;

		// Token: 0x020002F5 RID: 757
		private class CredentialCacheKey
		{
			// Token: 0x060019F5 RID: 6645 RVA: 0x00047E88 File Offset: 0x00046088
			internal CredentialCacheKey(System.Uri uriPrefix, string authType)
			{
				this.uriPrefix = uriPrefix;
				this.authType = authType;
				this.absPath = uriPrefix.AbsolutePath;
				this.absPath = this.absPath.Substring(0, this.absPath.LastIndexOf('/'));
				this.len = uriPrefix.AbsoluteUri.Length;
				this.hash = uriPrefix.GetHashCode() + authType.GetHashCode();
			}

			// Token: 0x17000642 RID: 1602
			// (get) Token: 0x060019F6 RID: 6646 RVA: 0x00047EF8 File Offset: 0x000460F8
			public int Length
			{
				get
				{
					return this.len;
				}
			}

			// Token: 0x17000643 RID: 1603
			// (get) Token: 0x060019F7 RID: 6647 RVA: 0x00047F00 File Offset: 0x00046100
			public string AbsPath
			{
				get
				{
					return this.absPath;
				}
			}

			// Token: 0x17000644 RID: 1604
			// (get) Token: 0x060019F8 RID: 6648 RVA: 0x00047F08 File Offset: 0x00046108
			public System.Uri UriPrefix
			{
				get
				{
					return this.uriPrefix;
				}
			}

			// Token: 0x17000645 RID: 1605
			// (get) Token: 0x060019F9 RID: 6649 RVA: 0x00047F10 File Offset: 0x00046110
			public string AuthType
			{
				get
				{
					return this.authType;
				}
			}

			// Token: 0x060019FA RID: 6650 RVA: 0x00047F18 File Offset: 0x00046118
			public override int GetHashCode()
			{
				return this.hash;
			}

			// Token: 0x060019FB RID: 6651 RVA: 0x00047F20 File Offset: 0x00046120
			public override bool Equals(object obj)
			{
				CredentialCache.CredentialCacheKey credentialCacheKey = obj as CredentialCache.CredentialCacheKey;
				return credentialCacheKey != null && this.hash == credentialCacheKey.hash;
			}

			// Token: 0x060019FC RID: 6652 RVA: 0x00047F4C File Offset: 0x0004614C
			public override string ToString()
			{
				return string.Concat(new object[]
				{
					this.absPath,
					" : ",
					this.authType,
					" : len=",
					this.len
				});
			}

			// Token: 0x0400102D RID: 4141
			private System.Uri uriPrefix;

			// Token: 0x0400102E RID: 4142
			private string authType;

			// Token: 0x0400102F RID: 4143
			private string absPath;

			// Token: 0x04001030 RID: 4144
			private int len;

			// Token: 0x04001031 RID: 4145
			private int hash;
		}

		// Token: 0x020002F6 RID: 758
		private class CredentialCacheForHostKey
		{
			// Token: 0x060019FD RID: 6653 RVA: 0x00047F8C File Offset: 0x0004618C
			internal CredentialCacheForHostKey(string host, int port, string authType)
			{
				this.host = host;
				this.port = port;
				this.authType = authType;
				this.hash = host.GetHashCode() + port.GetHashCode() + authType.GetHashCode();
			}

			// Token: 0x17000646 RID: 1606
			// (get) Token: 0x060019FE RID: 6654 RVA: 0x00047FD0 File Offset: 0x000461D0
			public string Host
			{
				get
				{
					return this.host;
				}
			}

			// Token: 0x17000647 RID: 1607
			// (get) Token: 0x060019FF RID: 6655 RVA: 0x00047FD8 File Offset: 0x000461D8
			public int Port
			{
				get
				{
					return this.port;
				}
			}

			// Token: 0x17000648 RID: 1608
			// (get) Token: 0x06001A00 RID: 6656 RVA: 0x00047FE0 File Offset: 0x000461E0
			public string AuthType
			{
				get
				{
					return this.authType;
				}
			}

			// Token: 0x06001A01 RID: 6657 RVA: 0x00047FE8 File Offset: 0x000461E8
			public override int GetHashCode()
			{
				return this.hash;
			}

			// Token: 0x06001A02 RID: 6658 RVA: 0x00047FF0 File Offset: 0x000461F0
			public override bool Equals(object obj)
			{
				CredentialCache.CredentialCacheForHostKey credentialCacheForHostKey = obj as CredentialCache.CredentialCacheForHostKey;
				return credentialCacheForHostKey != null && this.hash == credentialCacheForHostKey.hash;
			}

			// Token: 0x06001A03 RID: 6659 RVA: 0x0004801C File Offset: 0x0004621C
			public override string ToString()
			{
				return this.host + " : " + this.authType;
			}

			// Token: 0x04001032 RID: 4146
			private string host;

			// Token: 0x04001033 RID: 4147
			private int port;

			// Token: 0x04001034 RID: 4148
			private string authType;

			// Token: 0x04001035 RID: 4149
			private int hash;
		}
	}
}
