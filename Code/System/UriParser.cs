using System;
using System.Collections;
using System.Globalization;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;

namespace System
{
	/// <summary>Parses a new URI scheme. This is an abstract class.</summary>
	// Token: 0x020004B9 RID: 1209
	public abstract class UriParser
	{
		// Token: 0x06002B93 RID: 11155 RVA: 0x00097E4C File Offset: 0x0009604C
		private static System.Text.RegularExpressions.Match ParseAuthority(System.Text.RegularExpressions.Group g)
		{
			return System.UriParser.auth_regex.Match(g.Value);
		}

		/// <summary>Gets the components from a URI.</summary>
		/// <returns>A string that contains the components.</returns>
		/// <param name="uri">The URI to parse.</param>
		/// <param name="components">The <see cref="T:System.UriComponents" /> to retrieve from <paramref name="uri" />.</param>
		/// <param name="format">One of the <see cref="T:System.UriFormat" /> values that controls how special characters are escaped.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="uriFormat" /> is invalid.- or -<paramref name="uriComponents" /> is not a combination of valid <see cref="T:System.UriComponents" /> values. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="uri" /> requires user-driven parsing- or -<paramref name="uri" /> is not an absolute URI. Relative URIs cannot be used with this method.</exception>
		// Token: 0x06002B94 RID: 11156 RVA: 0x00097E60 File Offset: 0x00096060
		protected internal virtual string GetComponents(System.Uri uri, System.UriComponents components, System.UriFormat format)
		{
			if (format < System.UriFormat.UriEscaped || format > System.UriFormat.SafeUnescaped)
			{
				throw new ArgumentOutOfRangeException("format");
			}
			System.Text.RegularExpressions.Match match = System.UriParser.uri_regex.Match(uri.OriginalString);
			string value = this.scheme_name;
			int defaultPort = this.default_port;
			if (value == null || value == "*")
			{
				value = match.Groups[2].Value;
				defaultPort = System.Uri.GetDefaultPort(value);
			}
			else if (string.Compare(value, match.Groups[2].Value, true) != 0)
			{
				throw new SystemException("URI Parser: scheme mismatch: " + value + " vs. " + match.Groups[2].Value);
			}
			System.UriComponents uriComponents = components;
			switch (uriComponents)
			{
			case System.UriComponents.Scheme:
				return value;
			case System.UriComponents.UserInfo:
				return System.UriParser.ParseAuthority(match.Groups[4]).Groups[2].Value;
			default:
			{
				if (uriComponents == System.UriComponents.Path)
				{
					return this.Format(this.IgnoreFirstCharIf(match.Groups[5].Value, '/'), format);
				}
				if (uriComponents == System.UriComponents.Query)
				{
					return this.Format(match.Groups[7].Value, format);
				}
				if (uriComponents == System.UriComponents.Fragment)
				{
					return this.Format(match.Groups[9].Value, format);
				}
				if (uriComponents != System.UriComponents.StrongPort)
				{
					if (uriComponents == System.UriComponents.SerializationInfoString)
					{
						components = System.UriComponents.AbsoluteUri;
					}
					System.Text.RegularExpressions.Match match2 = System.UriParser.ParseAuthority(match.Groups[4]);
					StringBuilder stringBuilder = new StringBuilder();
					if ((components & System.UriComponents.Scheme) != (System.UriComponents)0)
					{
						stringBuilder.Append(value);
						stringBuilder.Append(System.Uri.GetSchemeDelimiter(value));
					}
					if ((components & System.UriComponents.UserInfo) != (System.UriComponents)0)
					{
						stringBuilder.Append(match2.Groups[1].Value);
					}
					if ((components & System.UriComponents.Host) != (System.UriComponents)0)
					{
						stringBuilder.Append(match2.Groups[3].Value);
					}
					if ((components & System.UriComponents.StrongPort) != (System.UriComponents)0)
					{
						System.Text.RegularExpressions.Group group = match2.Groups[4];
						stringBuilder.Append((!group.Success) ? (":" + defaultPort) : group.Value);
					}
					if ((components & System.UriComponents.Port) != (System.UriComponents)0)
					{
						string value2 = match2.Groups[5].Value;
						if (value2 != null && value2 != string.Empty && value2 != defaultPort.ToString())
						{
							stringBuilder.Append(match2.Groups[4].Value);
						}
					}
					if ((components & System.UriComponents.Path) != (System.UriComponents)0)
					{
						stringBuilder.Append(match.Groups[5]);
					}
					if ((components & System.UriComponents.Query) != (System.UriComponents)0)
					{
						stringBuilder.Append(match.Groups[6]);
					}
					if ((components & System.UriComponents.Fragment) != (System.UriComponents)0)
					{
						stringBuilder.Append(match.Groups[8]);
					}
					return this.Format(stringBuilder.ToString(), format);
				}
				System.Text.RegularExpressions.Group group2 = System.UriParser.ParseAuthority(match.Groups[4]).Groups[5];
				return (!group2.Success) ? defaultPort.ToString() : group2.Value;
			}
			case System.UriComponents.Host:
				return System.UriParser.ParseAuthority(match.Groups[4]).Groups[3].Value;
			case System.UriComponents.Port:
			{
				string value3 = System.UriParser.ParseAuthority(match.Groups[4]).Groups[5].Value;
				if (value3 != null && value3 != string.Empty && value3 != defaultPort.ToString())
				{
					return value3;
				}
				return string.Empty;
			}
			}
		}

		/// <summary>Initialize the state of the parser and validate the URI.</summary>
		/// <param name="uri">The T:System.Uri to validate.</param>
		/// <param name="parsingError">Validation errors, if any.</param>
		// Token: 0x06002B95 RID: 11157 RVA: 0x00098244 File Offset: 0x00096444
		protected internal virtual void InitializeAndValidate(System.Uri uri, out System.UriFormatException parsingError)
		{
			if (uri.Scheme != this.scheme_name && this.scheme_name != "*")
			{
				parsingError = new System.UriFormatException("The argument Uri's scheme does not match");
			}
			else
			{
				parsingError = null;
			}
		}

		/// <summary>Determines whether <paramref name="baseUri" /> is a base URI for <paramref name="relativeUri" />.</summary>
		/// <returns>true if <paramref name="baseUri" /> is a base URI for <paramref name="relativeUri" />; otherwise, false.</returns>
		/// <param name="baseUri">The base URI.</param>
		/// <param name="relativeUri">The URI to test.</param>
		// Token: 0x06002B96 RID: 11158 RVA: 0x00098290 File Offset: 0x00096490
		protected internal virtual bool IsBaseOf(System.Uri baseUri, System.Uri relativeUri)
		{
			if (System.Uri.Compare(baseUri, relativeUri, System.UriComponents.Scheme | System.UriComponents.UserInfo | System.UriComponents.Host | System.UriComponents.Port, System.UriFormat.Unescaped, StringComparison.InvariantCultureIgnoreCase) != 0)
			{
				return false;
			}
			string localPath = baseUri.LocalPath;
			int length = localPath.LastIndexOf('/') + 1;
			return string.Compare(localPath, 0, relativeUri.LocalPath, 0, length, StringComparison.InvariantCultureIgnoreCase) == 0;
		}

		/// <summary>Indicates whether a URI is well-formed.</summary>
		/// <returns>true if <paramref name="uri" /> is well-formed; otherwise, false.</returns>
		/// <param name="uri">The URI to check.</param>
		// Token: 0x06002B97 RID: 11159 RVA: 0x000982D4 File Offset: 0x000964D4
		protected internal virtual bool IsWellFormedOriginalString(System.Uri uri)
		{
			return uri.IsWellFormedOriginalString();
		}

		/// <summary>Invoked by a <see cref="T:System.Uri" /> constructor to get a <see cref="T:System.UriParser" /> instance</summary>
		/// <returns>A <see cref="T:System.UriParser" /> for the constructed <see cref="T:System.Uri" />.</returns>
		// Token: 0x06002B98 RID: 11160 RVA: 0x000982DC File Offset: 0x000964DC
		protected internal virtual System.UriParser OnNewUri()
		{
			return this;
		}

		/// <summary>Invoked by the Framework when a <see cref="T:System.UriParser" /> method is registered.</summary>
		/// <param name="schemeName">The scheme that is associated with this <see cref="T:System.UriParser" />.</param>
		/// <param name="defaultPort">The port number of the scheme.</param>
		// Token: 0x06002B99 RID: 11161 RVA: 0x000982E0 File Offset: 0x000964E0
		[MonoTODO]
		protected virtual void OnRegister(string schemeName, int defaultPort)
		{
		}

		/// <summary>Called by <see cref="T:System.Uri" /> constructors and <see cref="Overload:System.Uri.TryCreate" /> to resolve a relative URI.</summary>
		/// <returns>The string of the resolved relative <see cref="T:System.Uri" />.</returns>
		/// <param name="baseUri">A base URI.</param>
		/// <param name="relativeUri">A relative URI.</param>
		/// <param name="parsingError">Errors during the resolve process, if any.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="baseUri" /> parameter is not an absolute <see cref="T:System.Uri" />- or -<paramref name="baseUri" /> parameter requires user-driven parsing.</exception>
		// Token: 0x06002B9A RID: 11162 RVA: 0x000982E4 File Offset: 0x000964E4
		[MonoTODO]
		protected internal virtual string Resolve(System.Uri baseUri, System.Uri relativeUri, out System.UriFormatException parsingError)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x06002B9B RID: 11163 RVA: 0x000982EC File Offset: 0x000964EC
		// (set) Token: 0x06002B9C RID: 11164 RVA: 0x000982F4 File Offset: 0x000964F4
		internal string SchemeName
		{
			get
			{
				return this.scheme_name;
			}
			set
			{
				this.scheme_name = value;
			}
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x06002B9D RID: 11165 RVA: 0x00098300 File Offset: 0x00096500
		// (set) Token: 0x06002B9E RID: 11166 RVA: 0x00098308 File Offset: 0x00096508
		internal int DefaultPort
		{
			get
			{
				return this.default_port;
			}
			set
			{
				this.default_port = value;
			}
		}

		// Token: 0x06002B9F RID: 11167 RVA: 0x00098314 File Offset: 0x00096514
		private string IgnoreFirstCharIf(string s, char c)
		{
			if (s.Length == 0)
			{
				return string.Empty;
			}
			if (s[0] == c)
			{
				return s.Substring(1);
			}
			return s;
		}

		// Token: 0x06002BA0 RID: 11168 RVA: 0x00098348 File Offset: 0x00096548
		private string Format(string s, System.UriFormat format)
		{
			if (s.Length == 0)
			{
				return string.Empty;
			}
			switch (format)
			{
			case System.UriFormat.UriEscaped:
				return System.Uri.EscapeString(s, false, true, true);
			case System.UriFormat.Unescaped:
				return System.Uri.Unescape(s, false);
			case System.UriFormat.SafeUnescaped:
				s = System.Uri.Unescape(s, false);
				return s;
			default:
				throw new ArgumentOutOfRangeException("format");
			}
		}

		// Token: 0x06002BA1 RID: 11169 RVA: 0x000983A8 File Offset: 0x000965A8
		private static void CreateDefaults()
		{
			if (System.UriParser.table != null)
			{
				return;
			}
			Hashtable hashtable = new Hashtable();
			System.UriParser.InternalRegister(hashtable, new DefaultUriParser(), System.Uri.UriSchemeFile, -1);
			System.UriParser.InternalRegister(hashtable, new DefaultUriParser(), System.Uri.UriSchemeFtp, 21);
			System.UriParser.InternalRegister(hashtable, new DefaultUriParser(), System.Uri.UriSchemeGopher, 70);
			System.UriParser.InternalRegister(hashtable, new DefaultUriParser(), System.Uri.UriSchemeHttp, 80);
			System.UriParser.InternalRegister(hashtable, new DefaultUriParser(), System.Uri.UriSchemeHttps, 443);
			System.UriParser.InternalRegister(hashtable, new DefaultUriParser(), System.Uri.UriSchemeMailto, 25);
			System.UriParser.InternalRegister(hashtable, new DefaultUriParser(), System.Uri.UriSchemeNetPipe, -1);
			System.UriParser.InternalRegister(hashtable, new DefaultUriParser(), System.Uri.UriSchemeNetTcp, -1);
			System.UriParser.InternalRegister(hashtable, new DefaultUriParser(), System.Uri.UriSchemeNews, 119);
			System.UriParser.InternalRegister(hashtable, new DefaultUriParser(), System.Uri.UriSchemeNntp, 119);
			System.UriParser.InternalRegister(hashtable, new DefaultUriParser(), "ldap", 389);
			object obj = System.UriParser.lock_object;
			lock (obj)
			{
				if (System.UriParser.table == null)
				{
					System.UriParser.table = hashtable;
				}
			}
		}

		/// <summary>Indicates whether the parser for a scheme is registered.</summary>
		/// <returns>true if <paramref name="schemeName" /> has been registered; otherwise, false.</returns>
		/// <param name="schemeName">The scheme name to check.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="schemeName" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="schemeName" /> parameter is not valid. </exception>
		// Token: 0x06002BA2 RID: 11170 RVA: 0x000984DC File Offset: 0x000966DC
		public static bool IsKnownScheme(string schemeName)
		{
			if (schemeName == null)
			{
				throw new ArgumentNullException("schemeName");
			}
			if (schemeName.Length == 0)
			{
				throw new ArgumentOutOfRangeException("schemeName");
			}
			System.UriParser.CreateDefaults();
			string key = schemeName.ToLower(CultureInfo.InvariantCulture);
			return System.UriParser.table[key] != null;
		}

		// Token: 0x06002BA3 RID: 11171 RVA: 0x00098534 File Offset: 0x00096734
		private static void InternalRegister(Hashtable table, System.UriParser uriParser, string schemeName, int defaultPort)
		{
			uriParser.SchemeName = schemeName;
			uriParser.DefaultPort = defaultPort;
			if (uriParser is System.GenericUriParser)
			{
				table.Add(schemeName, uriParser);
			}
			else
			{
				table.Add(schemeName, new DefaultUriParser
				{
					SchemeName = schemeName,
					DefaultPort = defaultPort
				});
			}
			uriParser.OnRegister(schemeName, defaultPort);
		}

		/// <summary>Associates a scheme and port number with a <see cref="T:System.UriParser" />.</summary>
		/// <param name="uriParser">The URI parser to register.</param>
		/// <param name="schemeName">The name of the scheme that is associated with this parser.</param>
		/// <param name="defaultPort">The default port number for the specified scheme.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uriParser" /> parameter is null- or -<paramref name="schemeName" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="schemeName" /> parameter is not valid- or -<paramref name="defaultPort" /> parameter is not valid. The <paramref name="defaultPort" /> parameter must be not be less than zero or greater than 65,534.</exception>
		// Token: 0x06002BA4 RID: 11172 RVA: 0x0009858C File Offset: 0x0009678C
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"Infrastructure\"/>\n</PermissionSet>\n")]
		public static void Register(System.UriParser uriParser, string schemeName, int defaultPort)
		{
			if (uriParser == null)
			{
				throw new ArgumentNullException("uriParser");
			}
			if (schemeName == null)
			{
				throw new ArgumentNullException("schemeName");
			}
			if (defaultPort < -1 || defaultPort >= 65535)
			{
				throw new ArgumentOutOfRangeException("defaultPort");
			}
			System.UriParser.CreateDefaults();
			string text = schemeName.ToLower(CultureInfo.InvariantCulture);
			if (System.UriParser.table[text] != null)
			{
				string text2 = Locale.GetText("Scheme '{0}' is already registred.");
				throw new InvalidOperationException(text2);
			}
			System.UriParser.InternalRegister(System.UriParser.table, uriParser, text, defaultPort);
		}

		// Token: 0x06002BA5 RID: 11173 RVA: 0x00098618 File Offset: 0x00096818
		internal static System.UriParser GetParser(string schemeName)
		{
			if (schemeName == null)
			{
				return null;
			}
			System.UriParser.CreateDefaults();
			string key = schemeName.ToLower(CultureInfo.InvariantCulture);
			return (System.UriParser)System.UriParser.table[key];
		}

		// Token: 0x04001B7B RID: 7035
		private static object lock_object = new object();

		// Token: 0x04001B7C RID: 7036
		private static Hashtable table;

		// Token: 0x04001B7D RID: 7037
		internal string scheme_name;

		// Token: 0x04001B7E RID: 7038
		private int default_port;

		// Token: 0x04001B7F RID: 7039
		private static readonly System.Text.RegularExpressions.Regex uri_regex = new System.Text.RegularExpressions.Regex("^(([^:/?#]+):)?(//([^/?#]*))?([^?#]*)(\\?([^#]*))?(#(.*))?", System.Text.RegularExpressions.RegexOptions.Compiled);

		// Token: 0x04001B80 RID: 7040
		private static readonly System.Text.RegularExpressions.Regex auth_regex = new System.Text.RegularExpressions.Regex("^(([^@]+)@)?(.*?)(:([0-9]+))?$");
	}
}
