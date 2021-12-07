using System;
using System.Collections;
using System.Globalization;
using System.Text;

namespace System.Net
{
	/// <summary>Provides a set of properties and methods that are used to manage cookies. This class cannot be inherited.</summary>
	// Token: 0x020002F2 RID: 754
	[Serializable]
	public sealed class Cookie
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cookie" /> class.</summary>
		// Token: 0x060019B8 RID: 6584 RVA: 0x00047298 File Offset: 0x00045498
		public Cookie()
		{
			this.expires = DateTime.MinValue;
			this.timestamp = DateTime.Now;
			this.domain = string.Empty;
			this.name = string.Empty;
			this.val = string.Empty;
			this.comment = string.Empty;
			this.port = string.Empty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cookie" /> class with a specified <see cref="P:System.Net.Cookie.Name" /> and <see cref="P:System.Net.Cookie.Value" />.</summary>
		/// <param name="name">The name of a <see cref="T:System.Net.Cookie" />. The following characters must not be used inside <paramref name="name" />: equal sign, semicolon, comma, newline (\n), return (\r), tab (\t), and space character. The dollar sign character ("$") cannot be the first character. </param>
		/// <param name="value">The value of a <see cref="T:System.Net.Cookie" />. The following characters must not be used inside <paramref name="value" />: semicolon, comma. </param>
		/// <exception cref="T:System.Net.CookieException">The <paramref name="name" /> parameter is null. -or- The <paramref name="name" /> parameter is of zero length. -or- The <paramref name="name" /> parameter contains an invalid character.-or- The <paramref name="value" /> parameter is null .-or - The <paramref name="value" /> parameter contains a string not enclosed in quotes that contains an invalid character. </exception>
		// Token: 0x060019B9 RID: 6585 RVA: 0x000472F8 File Offset: 0x000454F8
		public Cookie(string name, string value) : this()
		{
			this.Name = name;
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cookie" /> class with a specified <see cref="P:System.Net.Cookie.Name" />, <see cref="P:System.Net.Cookie.Value" />, and <see cref="P:System.Net.Cookie.Path" />.</summary>
		/// <param name="name">The name of a <see cref="T:System.Net.Cookie" />. The following characters must not be used inside <paramref name="name" />: equal sign, semicolon, comma, newline (\n), return (\r), tab (\t), and space character. The dollar sign character ("$") cannot be the first character. </param>
		/// <param name="value">The value of a <see cref="T:System.Net.Cookie" />. The following characters must not be used inside <paramref name="value" />: semicolon, comma. </param>
		/// <param name="path">The subset of URIs on the origin server to which this <see cref="T:System.Net.Cookie" /> applies. The default value is "/". </param>
		/// <exception cref="T:System.Net.CookieException">The <paramref name="name" /> parameter is null. -or- The <paramref name="name" /> parameter is of zero length. -or- The <paramref name="name" /> parameter contains an invalid character.-or- The <paramref name="value" /> parameter is null .-or - The <paramref name="value" /> parameter contains a string not enclosed in quotes that contains an invalid character.</exception>
		// Token: 0x060019BA RID: 6586 RVA: 0x00047310 File Offset: 0x00045510
		public Cookie(string name, string value, string path) : this(name, value)
		{
			this.Path = path;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cookie" /> class with a specified <see cref="P:System.Net.Cookie.Name" />, <see cref="P:System.Net.Cookie.Value" />, <see cref="P:System.Net.Cookie.Path" />, and <see cref="P:System.Net.Cookie.Domain" />.</summary>
		/// <param name="name">The name of a <see cref="T:System.Net.Cookie" />. The following characters must not be used inside <paramref name="name" />: equal sign, semicolon, comma, newline (\n), return (\r), tab (\t), and space character. The dollar sign character ("$") cannot be the first character. </param>
		/// <param name="value">The value of a <see cref="T:System.Net.Cookie" /> object. The following characters must not be used inside <paramref name="value" />: semicolon, comma. </param>
		/// <param name="path">The subset of URIs on the origin server to which this <see cref="T:System.Net.Cookie" /> applies. The default value is "/". </param>
		/// <param name="domain">The optional internet domain for which this <see cref="T:System.Net.Cookie" /> is valid. The default value is the host this <see cref="T:System.Net.Cookie" /> has been received from. </param>
		/// <exception cref="T:System.Net.CookieException">The <paramref name="name" /> parameter is null. -or- The <paramref name="name" /> parameter is of zero length. -or- The <paramref name="name" /> parameter contains an invalid character.-or- The <paramref name="value" /> parameter is null .-or - The <paramref name="value" /> parameter contains a string not enclosed in quotes that contains an invalid character.</exception>
		// Token: 0x060019BB RID: 6587 RVA: 0x00047324 File Offset: 0x00045524
		public Cookie(string name, string value, string path, string domain) : this(name, value, path)
		{
			this.Domain = domain;
		}

		/// <summary>Gets or sets a comment that the server can add to a <see cref="T:System.Net.Cookie" />.</summary>
		/// <returns>An optional comment to document intended usage for this <see cref="T:System.Net.Cookie" />.</returns>
		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x060019BD RID: 6589 RVA: 0x00047370 File Offset: 0x00045570
		// (set) Token: 0x060019BE RID: 6590 RVA: 0x00047378 File Offset: 0x00045578
		public string Comment
		{
			get
			{
				return this.comment;
			}
			set
			{
				this.comment = ((value != null) ? value : string.Empty);
			}
		}

		/// <summary>Gets or sets a URI comment that the server can provide with a <see cref="T:System.Net.Cookie" />.</summary>
		/// <returns>An optional comment that represents the intended usage of the URI reference for this <see cref="T:System.Net.Cookie" />. The value must conform to URI format.</returns>
		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x060019BF RID: 6591 RVA: 0x00047394 File Offset: 0x00045594
		// (set) Token: 0x060019C0 RID: 6592 RVA: 0x0004739C File Offset: 0x0004559C
		public System.Uri CommentUri
		{
			get
			{
				return this.commentUri;
			}
			set
			{
				this.commentUri = value;
			}
		}

		/// <summary>Gets or sets the discard flag set by the server.</summary>
		/// <returns>true if the client is to discard the <see cref="T:System.Net.Cookie" /> at the end of the current session; otherwise, false. The default is false.</returns>
		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x060019C1 RID: 6593 RVA: 0x000473A8 File Offset: 0x000455A8
		// (set) Token: 0x060019C2 RID: 6594 RVA: 0x000473B0 File Offset: 0x000455B0
		public bool Discard
		{
			get
			{
				return this.discard;
			}
			set
			{
				this.discard = value;
			}
		}

		/// <summary>Gets or sets the URI for which the <see cref="T:System.Net.Cookie" /> is valid.</summary>
		/// <returns>The URI for which the <see cref="T:System.Net.Cookie" /> is valid.</returns>
		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x060019C3 RID: 6595 RVA: 0x000473BC File Offset: 0x000455BC
		// (set) Token: 0x060019C4 RID: 6596 RVA: 0x000473C4 File Offset: 0x000455C4
		public string Domain
		{
			get
			{
				return this.domain;
			}
			set
			{
				if (Cookie.IsNullOrEmpty(value))
				{
					this.domain = string.Empty;
					this.ExactDomain = true;
				}
				else
				{
					this.domain = value;
					this.ExactDomain = (value[0] != '.');
				}
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x060019C5 RID: 6597 RVA: 0x00047410 File Offset: 0x00045610
		// (set) Token: 0x060019C6 RID: 6598 RVA: 0x00047418 File Offset: 0x00045618
		internal bool ExactDomain
		{
			get
			{
				return this.exact_domain;
			}
			set
			{
				this.exact_domain = value;
			}
		}

		/// <summary>Gets or sets the current state of the <see cref="T:System.Net.Cookie" />.</summary>
		/// <returns>true if the <see cref="T:System.Net.Cookie" /> has expired; otherwise, false. The default is false.</returns>
		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x060019C7 RID: 6599 RVA: 0x00047424 File Offset: 0x00045624
		// (set) Token: 0x060019C8 RID: 6600 RVA: 0x0004745C File Offset: 0x0004565C
		public bool Expired
		{
			get
			{
				return this.expires <= DateTime.Now && this.expires != DateTime.MinValue;
			}
			set
			{
				if (value)
				{
					this.expires = DateTime.Now;
				}
			}
		}

		/// <summary>Gets or sets the expiration date and time for the <see cref="T:System.Net.Cookie" /> as a <see cref="T:System.DateTime" />.</summary>
		/// <returns>The expiration date and time for the <see cref="T:System.Net.Cookie" /> as a <see cref="T:System.DateTime" /> instance.</returns>
		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x060019C9 RID: 6601 RVA: 0x00047470 File Offset: 0x00045670
		// (set) Token: 0x060019CA RID: 6602 RVA: 0x00047478 File Offset: 0x00045678
		public DateTime Expires
		{
			get
			{
				return this.expires;
			}
			set
			{
				this.expires = value;
			}
		}

		/// <summary>Determines whether a page script or other active content can access this cookie.</summary>
		/// <returns>Boolean value that determines whether a page script or other active content can access this cookie.</returns>
		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x060019CB RID: 6603 RVA: 0x00047484 File Offset: 0x00045684
		// (set) Token: 0x060019CC RID: 6604 RVA: 0x0004748C File Offset: 0x0004568C
		public bool HttpOnly
		{
			get
			{
				return this.httpOnly;
			}
			set
			{
				this.httpOnly = value;
			}
		}

		/// <summary>Gets or sets the name for the <see cref="T:System.Net.Cookie" />.</summary>
		/// <returns>The name for the <see cref="T:System.Net.Cookie" />.</returns>
		/// <exception cref="T:System.Net.CookieException">The value specified for a set operation is null or the empty string- or -The value specified for a set operation contained an illegal character. The following characters must not be used inside the <see cref="P:System.Net.Cookie.Name" /> property: equal sign, semicolon, comma, newline (\n), return (\r), tab (\t), and space character. The dollar sign character ("$") cannot be the first character.</exception>
		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x060019CD RID: 6605 RVA: 0x00047498 File Offset: 0x00045698
		// (set) Token: 0x060019CE RID: 6606 RVA: 0x000474A0 File Offset: 0x000456A0
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				if (Cookie.IsNullOrEmpty(value))
				{
					throw new CookieException("Name cannot be empty");
				}
				if (value[0] == '$' || value.IndexOfAny(Cookie.reservedCharsName) != -1)
				{
					this.name = string.Empty;
					throw new CookieException("Name contains invalid characters");
				}
				this.name = value;
			}
		}

		/// <summary>Gets or sets the URIs to which the <see cref="T:System.Net.Cookie" /> applies.</summary>
		/// <returns>The URIs to which the <see cref="T:System.Net.Cookie" /> applies.</returns>
		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x060019CF RID: 6607 RVA: 0x00047500 File Offset: 0x00045700
		// (set) Token: 0x060019D0 RID: 6608 RVA: 0x00047520 File Offset: 0x00045720
		public string Path
		{
			get
			{
				return (this.path != null) ? this.path : string.Empty;
			}
			set
			{
				this.path = ((value != null) ? value : string.Empty);
			}
		}

		/// <summary>Gets or sets a list of TCP ports that the <see cref="T:System.Net.Cookie" /> applies to.</summary>
		/// <returns>The list of TCP ports that the <see cref="T:System.Net.Cookie" /> applies to.</returns>
		/// <exception cref="T:System.Net.CookieException">The value specified for a set operation could not be parsed or is not enclosed in double quotes. </exception>
		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x060019D1 RID: 6609 RVA: 0x0004753C File Offset: 0x0004573C
		// (set) Token: 0x060019D2 RID: 6610 RVA: 0x00047544 File Offset: 0x00045744
		public string Port
		{
			get
			{
				return this.port;
			}
			set
			{
				if (Cookie.IsNullOrEmpty(value))
				{
					this.port = string.Empty;
					return;
				}
				if (value[0] != '"' || value[value.Length - 1] != '"')
				{
					throw new CookieException("The 'Port'='" + value + "' part of the cookie is invalid. Port must be enclosed by double quotes.");
				}
				this.port = value;
				string[] array = this.port.Split(Cookie.portSeparators);
				this.ports = new int[array.Length];
				for (int i = 0; i < this.ports.Length; i++)
				{
					this.ports[i] = int.MinValue;
					if (array[i].Length != 0)
					{
						try
						{
							this.ports[i] = int.Parse(array[i]);
						}
						catch (Exception e)
						{
							throw new CookieException("The 'Port'='" + value + "' part of the cookie is invalid. Invalid value: " + array[i], e);
						}
					}
				}
				this.Version = 1;
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x060019D3 RID: 6611 RVA: 0x00047658 File Offset: 0x00045858
		internal int[] Ports
		{
			get
			{
				return this.ports;
			}
		}

		/// <summary>Gets or sets the security level of a <see cref="T:System.Net.Cookie" />.</summary>
		/// <returns>true if the client is only to return the cookie in subsequent requests if those requests use Secure Hypertext Transfer Protocol (HTTPS); otherwise, false. The default is false.</returns>
		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x060019D4 RID: 6612 RVA: 0x00047660 File Offset: 0x00045860
		// (set) Token: 0x060019D5 RID: 6613 RVA: 0x00047668 File Offset: 0x00045868
		public bool Secure
		{
			get
			{
				return this.secure;
			}
			set
			{
				this.secure = value;
			}
		}

		/// <summary>Gets the time when the cookie was issued as a <see cref="T:System.DateTime" />.</summary>
		/// <returns>The time when the cookie was issued as a <see cref="T:System.DateTime" />.</returns>
		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x060019D6 RID: 6614 RVA: 0x00047674 File Offset: 0x00045874
		public DateTime TimeStamp
		{
			get
			{
				return this.timestamp;
			}
		}

		/// <summary>Gets or sets the <see cref="P:System.Net.Cookie.Value" /> for the <see cref="T:System.Net.Cookie" />.</summary>
		/// <returns>The <see cref="P:System.Net.Cookie.Value" /> for the <see cref="T:System.Net.Cookie" />.</returns>
		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x060019D7 RID: 6615 RVA: 0x0004767C File Offset: 0x0004587C
		// (set) Token: 0x060019D8 RID: 6616 RVA: 0x00047684 File Offset: 0x00045884
		public string Value
		{
			get
			{
				return this.val;
			}
			set
			{
				if (value == null)
				{
					this.val = string.Empty;
					return;
				}
				this.val = value;
			}
		}

		/// <summary>Gets or sets the version of HTTP state maintenance to which the cookie conforms.</summary>
		/// <returns>The version of HTTP state maintenance to which the cookie conforms.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a version is not allowed. </exception>
		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x060019D9 RID: 6617 RVA: 0x000476A0 File Offset: 0x000458A0
		// (set) Token: 0x060019DA RID: 6618 RVA: 0x000476A8 File Offset: 0x000458A8
		public int Version
		{
			get
			{
				return this.version;
			}
			set
			{
				if (value < 0 || value > 10)
				{
					this.version = 0;
				}
				else
				{
					this.version = value;
				}
			}
		}

		/// <summary>Overrides the <see cref="M:System.Object.Equals(System.Object)" /> method.</summary>
		/// <returns>Returns true if the <see cref="T:System.Net.Cookie" /> is equal to <paramref name="comparand" />. Two <see cref="T:System.Net.Cookie" /> instances are equal if their <see cref="P:System.Net.Cookie.Name" />, <see cref="P:System.Net.Cookie.Value" />, <see cref="P:System.Net.Cookie.Path" />, <see cref="P:System.Net.Cookie.Domain" />, and <see cref="P:System.Net.Cookie.Version" /> properties are equal. <see cref="P:System.Net.Cookie.Name" /> and <see cref="P:System.Net.Cookie.Domain" /> string comparisons are case-insensitive.</returns>
		/// <param name="comparand">A reference to a <see cref="T:System.Net.Cookie" />. </param>
		// Token: 0x060019DB RID: 6619 RVA: 0x000476D8 File Offset: 0x000458D8
		public override bool Equals(object obj)
		{
			Cookie cookie = obj as Cookie;
			return cookie != null && string.Compare(this.name, cookie.name, true, CultureInfo.InvariantCulture) == 0 && string.Compare(this.val, cookie.val, false, CultureInfo.InvariantCulture) == 0 && string.Compare(this.Path, cookie.Path, false, CultureInfo.InvariantCulture) == 0 && string.Compare(this.domain, cookie.domain, true, CultureInfo.InvariantCulture) == 0 && this.version == cookie.version;
		}

		/// <summary>Overrides the <see cref="M:System.Object.GetHashCode" /> method.</summary>
		/// <returns>The 32-bit signed integer hash code for this instance.</returns>
		// Token: 0x060019DC RID: 6620 RVA: 0x00047774 File Offset: 0x00045974
		public override int GetHashCode()
		{
			return Cookie.hash(CaseInsensitiveHashCodeProvider.DefaultInvariant.GetHashCode(this.name), this.val.GetHashCode(), this.Path.GetHashCode(), CaseInsensitiveHashCodeProvider.DefaultInvariant.GetHashCode(this.domain), this.version);
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x000477C4 File Offset: 0x000459C4
		private static int hash(int i, int j, int k, int l, int m)
		{
			return i ^ (j << 13 | j >> 19) ^ (k << 26 | k >> 6) ^ (l << 7 | l >> 25) ^ (m << 20 | m >> 12);
		}

		/// <summary>Overrides the <see cref="M:System.Object.ToString" /> method.</summary>
		/// <returns>Returns a string representation of this <see cref="T:System.Net.Cookie" /> object that is suitable for including in a HTTP Cookie: request header.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060019DE RID: 6622 RVA: 0x000477F0 File Offset: 0x000459F0
		public override string ToString()
		{
			return this.ToString(null);
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x000477FC File Offset: 0x000459FC
		internal string ToString(System.Uri uri)
		{
			if (this.name.Length == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(64);
			if (this.version > 0)
			{
				stringBuilder.Append("$Version=").Append(this.version).Append("; ");
			}
			stringBuilder.Append(this.name).Append("=").Append(this.val);
			if (this.version == 0)
			{
				return stringBuilder.ToString();
			}
			if (!Cookie.IsNullOrEmpty(this.path))
			{
				stringBuilder.Append("; $Path=").Append(this.path);
			}
			else if (uri != null)
			{
				stringBuilder.Append("; $Path=/").Append(this.path);
			}
			bool flag = uri == null || uri.Host != this.domain;
			if (flag && !Cookie.IsNullOrEmpty(this.domain))
			{
				stringBuilder.Append("; $Domain=").Append(this.domain);
			}
			if (this.port != null && this.port.Length != 0)
			{
				stringBuilder.Append("; $Port=").Append(this.port);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x00047960 File Offset: 0x00045B60
		internal string ToClientString()
		{
			if (this.name.Length == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(64);
			if (this.version > 0)
			{
				stringBuilder.Append("Version=").Append(this.version).Append(";");
			}
			stringBuilder.Append(this.name).Append("=").Append(this.val);
			if (this.path != null && this.path.Length != 0)
			{
				stringBuilder.Append(";Path=").Append(this.QuotedString(this.path));
			}
			if (this.domain != null && this.domain.Length != 0)
			{
				stringBuilder.Append(";Domain=").Append(this.QuotedString(this.domain));
			}
			if (this.port != null && this.port.Length != 0)
			{
				stringBuilder.Append(";Port=").Append(this.port);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x00047A84 File Offset: 0x00045C84
		private string QuotedString(string value)
		{
			if (this.version == 0 || this.IsToken(value))
			{
				return value;
			}
			return "\"" + value.Replace("\"", "\\\"") + "\"";
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x00047ACC File Offset: 0x00045CCC
		private bool IsToken(string value)
		{
			int length = value.Length;
			for (int i = 0; i < length; i++)
			{
				char c = value[i];
				if (c < ' ' || c >= '\u007f' || Cookie.tspecials.IndexOf(c) != -1)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x00047B20 File Offset: 0x00045D20
		private static bool IsNullOrEmpty(string s)
		{
			return s == null || s.Length == 0;
		}

		// Token: 0x04001018 RID: 4120
		private string comment;

		// Token: 0x04001019 RID: 4121
		private System.Uri commentUri;

		// Token: 0x0400101A RID: 4122
		private bool discard;

		// Token: 0x0400101B RID: 4123
		private string domain;

		// Token: 0x0400101C RID: 4124
		private DateTime expires;

		// Token: 0x0400101D RID: 4125
		private bool httpOnly;

		// Token: 0x0400101E RID: 4126
		private string name;

		// Token: 0x0400101F RID: 4127
		private string path;

		// Token: 0x04001020 RID: 4128
		private string port;

		// Token: 0x04001021 RID: 4129
		private int[] ports;

		// Token: 0x04001022 RID: 4130
		private bool secure;

		// Token: 0x04001023 RID: 4131
		private DateTime timestamp;

		// Token: 0x04001024 RID: 4132
		private string val;

		// Token: 0x04001025 RID: 4133
		private int version;

		// Token: 0x04001026 RID: 4134
		private static char[] reservedCharsName = new char[]
		{
			' ',
			'=',
			';',
			',',
			'\n',
			'\r',
			'\t'
		};

		// Token: 0x04001027 RID: 4135
		private static char[] portSeparators = new char[]
		{
			'"',
			','
		};

		// Token: 0x04001028 RID: 4136
		private static string tspecials = "()<>@,;:\\\"/[]?={} \t";

		// Token: 0x04001029 RID: 4137
		private bool exact_domain;
	}
}
