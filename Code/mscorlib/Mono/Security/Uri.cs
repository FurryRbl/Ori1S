using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Mono.Security
{
	// Token: 0x020000AE RID: 174
	internal class Uri
	{
		// Token: 0x060009B7 RID: 2487 RVA: 0x000260A4 File Offset: 0x000242A4
		public Uri(string uriString) : this(uriString, false)
		{
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x000260B0 File Offset: 0x000242B0
		public Uri(string uriString, bool dontEscape)
		{
			this.scheme = string.Empty;
			this.host = string.Empty;
			this.port = -1;
			this.path = string.Empty;
			this.query = string.Empty;
			this.fragment = string.Empty;
			this.userinfo = string.Empty;
			this.reduce = true;
			base..ctor();
			this.userEscaped = dontEscape;
			this.source = uriString;
			this.Parse();
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00026128 File Offset: 0x00024328
		public Uri(string uriString, bool dontEscape, bool reduce)
		{
			this.scheme = string.Empty;
			this.host = string.Empty;
			this.port = -1;
			this.path = string.Empty;
			this.query = string.Empty;
			this.fragment = string.Empty;
			this.userinfo = string.Empty;
			this.reduce = true;
			base..ctor();
			this.userEscaped = dontEscape;
			this.source = uriString;
			this.reduce = reduce;
			this.Parse();
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x000261A8 File Offset: 0x000243A8
		public Uri(Uri baseUri, string relativeUri) : this(baseUri, relativeUri, false)
		{
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x000261B4 File Offset: 0x000243B4
		public Uri(Uri baseUri, string relativeUri, bool dontEscape)
		{
			this.scheme = string.Empty;
			this.host = string.Empty;
			this.port = -1;
			this.path = string.Empty;
			this.query = string.Empty;
			this.fragment = string.Empty;
			this.userinfo = string.Empty;
			this.reduce = true;
			base..ctor();
			if (baseUri == null)
			{
				throw new NullReferenceException("baseUri");
			}
			this.userEscaped = dontEscape;
			if (relativeUri == null)
			{
				throw new NullReferenceException("relativeUri");
			}
			if (relativeUri.StartsWith("\\\\"))
			{
				this.source = relativeUri;
				this.Parse();
				return;
			}
			int num = relativeUri.IndexOf(':');
			if (num != -1)
			{
				int num2 = relativeUri.IndexOfAny(new char[]
				{
					'/',
					'\\',
					'?'
				});
				if (num2 > num || num2 < 0)
				{
					this.source = relativeUri;
					this.Parse();
					return;
				}
			}
			this.scheme = baseUri.scheme;
			this.host = baseUri.host;
			this.port = baseUri.port;
			this.userinfo = baseUri.userinfo;
			this.isUnc = baseUri.isUnc;
			this.isUnixFilePath = baseUri.isUnixFilePath;
			this.isOpaquePart = baseUri.isOpaquePart;
			if (relativeUri == string.Empty)
			{
				this.path = baseUri.path;
				this.query = baseUri.query;
				this.fragment = baseUri.fragment;
				return;
			}
			num = relativeUri.IndexOf('#');
			if (num != -1)
			{
				this.fragment = relativeUri.Substring(num);
				relativeUri = relativeUri.Substring(0, num);
			}
			num = relativeUri.IndexOf('?');
			if (num != -1)
			{
				this.query = relativeUri.Substring(num);
				if (!this.userEscaped)
				{
					this.query = Uri.EscapeString(this.query);
				}
				relativeUri = relativeUri.Substring(0, num);
			}
			if (relativeUri.Length > 0 && relativeUri[0] == '/')
			{
				if (relativeUri.Length > 1 && relativeUri[1] == '/')
				{
					this.source = this.scheme + ':' + relativeUri;
					this.Parse();
					return;
				}
				this.path = relativeUri;
				if (!this.userEscaped)
				{
					this.path = Uri.EscapeString(this.path);
				}
				return;
			}
			else
			{
				this.path = baseUri.path;
				if (relativeUri.Length > 0 || this.query.Length > 0)
				{
					num = this.path.LastIndexOf('/');
					if (num >= 0)
					{
						this.path = this.path.Substring(0, num + 1);
					}
				}
				if (relativeUri.Length == 0)
				{
					return;
				}
				this.path += relativeUri;
				int startIndex = 0;
				for (;;)
				{
					num = this.path.IndexOf("./", startIndex);
					if (num == -1)
					{
						break;
					}
					if (num == 0)
					{
						this.path = this.path.Remove(0, 2);
					}
					else if (this.path[num - 1] != '.')
					{
						this.path = this.path.Remove(num, 2);
					}
					else
					{
						startIndex = num + 1;
					}
				}
				if (this.path.Length > 1 && this.path[this.path.Length - 1] == '.' && this.path[this.path.Length - 2] == '/')
				{
					this.path = this.path.Remove(this.path.Length - 1, 1);
				}
				startIndex = 0;
				for (;;)
				{
					num = this.path.IndexOf("/../", startIndex);
					if (num == -1)
					{
						break;
					}
					if (num == 0)
					{
						startIndex = 3;
					}
					else
					{
						int num3 = this.path.LastIndexOf('/', num - 1);
						if (num3 == -1)
						{
							startIndex = num + 1;
						}
						else if (this.path.Substring(num3 + 1, num - num3 - 1) != "..")
						{
							this.path = this.path.Remove(num3 + 1, num - num3 + 3);
						}
						else
						{
							startIndex = num + 1;
						}
					}
				}
				if (this.path.Length > 3 && this.path.EndsWith("/.."))
				{
					num = this.path.LastIndexOf('/', this.path.Length - 4);
					if (num != -1 && this.path.Substring(num + 1, this.path.Length - num - 4) != "..")
					{
						this.path = this.path.Remove(num + 1, this.path.Length - num - 1);
					}
				}
				if (!this.userEscaped)
				{
					this.path = Uri.EscapeString(this.path);
				}
				return;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060009BD RID: 2493 RVA: 0x00026818 File Offset: 0x00024A18
		public string AbsolutePath
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x00026820 File Offset: 0x00024A20
		public string AbsoluteUri
		{
			get
			{
				if (this.cachedAbsoluteUri == null)
				{
					this.cachedAbsoluteUri = this.GetLeftPart(UriPartial.Path) + this.query + this.fragment;
				}
				return this.cachedAbsoluteUri;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x00026854 File Offset: 0x00024A54
		public string Authority
		{
			get
			{
				return (Uri.GetDefaultPort(this.scheme) != this.port) ? (this.host + ":" + this.port) : this.host;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x000268A0 File Offset: 0x00024AA0
		public string Fragment
		{
			get
			{
				return this.fragment;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x000268A8 File Offset: 0x00024AA8
		public string Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x000268B0 File Offset: 0x00024AB0
		public bool IsDefaultPort
		{
			get
			{
				return Uri.GetDefaultPort(this.scheme) == this.port;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060009C3 RID: 2499 RVA: 0x000268C8 File Offset: 0x00024AC8
		public bool IsFile
		{
			get
			{
				return this.scheme == Uri.UriSchemeFile;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x000268DC File Offset: 0x00024ADC
		public bool IsLoopback
		{
			get
			{
				return !(this.host == string.Empty) && (this.host == "loopback" || this.host == "localhost");
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x00026930 File Offset: 0x00024B30
		public bool IsUnc
		{
			get
			{
				return this.isUnc;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x00026938 File Offset: 0x00024B38
		public string LocalPath
		{
			get
			{
				if (this.cachedLocalPath != null)
				{
					return this.cachedLocalPath;
				}
				if (!this.IsFile)
				{
					return this.AbsolutePath;
				}
				bool flag = this.path.Length > 3 && this.path[1] == ':' && (this.path[2] == '\\' || this.path[2] == '/');
				if (!this.IsUnc)
				{
					string text = this.Unescape(this.path);
					if (Path.DirectorySeparatorChar == '\\' || flag)
					{
						this.cachedLocalPath = text.Replace('/', '\\');
					}
					else
					{
						this.cachedLocalPath = text;
					}
				}
				else if (this.path.Length > 1 && this.path[1] == ':')
				{
					this.cachedLocalPath = this.Unescape(this.path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar));
				}
				else if (Path.DirectorySeparatorChar == '\\')
				{
					this.cachedLocalPath = "\\\\" + this.Unescape(this.host + this.path.Replace('/', '\\'));
				}
				else
				{
					this.cachedLocalPath = this.Unescape(this.path);
				}
				if (this.cachedLocalPath == string.Empty)
				{
					this.cachedLocalPath = Path.DirectorySeparatorChar.ToString();
				}
				return this.cachedLocalPath;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x00026AD0 File Offset: 0x00024CD0
		public string PathAndQuery
		{
			get
			{
				return this.path + this.query;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060009C8 RID: 2504 RVA: 0x00026AE4 File Offset: 0x00024CE4
		public int Port
		{
			get
			{
				return this.port;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x00026AEC File Offset: 0x00024CEC
		public string Query
		{
			get
			{
				return this.query;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060009CA RID: 2506 RVA: 0x00026AF4 File Offset: 0x00024CF4
		public string Scheme
		{
			get
			{
				return this.scheme;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x00026AFC File Offset: 0x00024CFC
		public string[] Segments
		{
			get
			{
				if (this.segments != null)
				{
					return this.segments;
				}
				if (this.path.Length == 0)
				{
					this.segments = new string[0];
					return this.segments;
				}
				string[] array = this.path.Split(new char[]
				{
					'/'
				});
				this.segments = array;
				bool flag = this.path.EndsWith("/");
				if (array.Length > 0 && flag)
				{
					string[] array2 = new string[array.Length - 1];
					Array.Copy(array, 0, array2, 0, array.Length - 1);
					array = array2;
				}
				int i = 0;
				if (this.IsFile && this.path.Length > 1 && this.path[1] == ':')
				{
					string[] array3 = new string[array.Length + 1];
					Array.Copy(array, 1, array3, 2, array.Length - 1);
					array = array3;
					array[0] = this.path.Substring(0, 2);
					array[1] = string.Empty;
					i++;
				}
				int num = array.Length;
				while (i < num)
				{
					if (i != num - 1 || flag)
					{
						string[] array4 = array;
						int num2 = i;
						array4[num2] += '/';
					}
					i++;
				}
				this.segments = array;
				return this.segments;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x00026C4C File Offset: 0x00024E4C
		public bool UserEscaped
		{
			get
			{
				return this.userEscaped;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x00026C54 File Offset: 0x00024E54
		public string UserInfo
		{
			get
			{
				return this.userinfo;
			}
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x00026C5C File Offset: 0x00024E5C
		internal static bool IsIPv4Address(string name)
		{
			string[] array = name.Split(new char[]
			{
				'.'
			});
			if (array.Length != 4)
			{
				return false;
			}
			for (int i = 0; i < 4; i++)
			{
				try
				{
					int num = int.Parse(array[i], CultureInfo.InvariantCulture);
					if (num < 0 || num > 255)
					{
						return false;
					}
				}
				catch (Exception)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00026CF0 File Offset: 0x00024EF0
		internal static bool IsDomainAddress(string name)
		{
			int length = name.Length;
			if (name[length - 1] == '.')
			{
				return false;
			}
			int num = 0;
			for (int i = 0; i < length; i++)
			{
				char c = name[i];
				if (num == 0)
				{
					if (!char.IsLetterOrDigit(c))
					{
						return false;
					}
				}
				else if (c == '.')
				{
					num = 0;
				}
				else if (!char.IsLetterOrDigit(c) && c != '-' && c != '_')
				{
					return false;
				}
				if (++num == 64)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x00026D88 File Offset: 0x00024F88
		public static bool CheckSchemeName(string schemeName)
		{
			if (schemeName == null || schemeName.Length == 0)
			{
				return false;
			}
			if (!char.IsLetter(schemeName[0]))
			{
				return false;
			}
			int length = schemeName.Length;
			for (int i = 1; i < length; i++)
			{
				char c = schemeName[i];
				if (!char.IsLetterOrDigit(c) && c != '.' && c != '+' && c != '-')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x00026E04 File Offset: 0x00025004
		public override bool Equals(object comparant)
		{
			if (comparant == null)
			{
				return false;
			}
			Uri uri = comparant as Uri;
			if (uri == null)
			{
				string text = comparant as string;
				if (text == null)
				{
					return false;
				}
				uri = new Uri(text);
			}
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			return this.scheme.ToLower(invariantCulture) == uri.scheme.ToLower(invariantCulture) && this.userinfo.ToLower(invariantCulture) == uri.userinfo.ToLower(invariantCulture) && this.host.ToLower(invariantCulture) == uri.host.ToLower(invariantCulture) && this.port == uri.port && this.path == uri.path && this.query.ToLower(invariantCulture) == uri.query.ToLower(invariantCulture);
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x00026EF0 File Offset: 0x000250F0
		public override int GetHashCode()
		{
			if (this.cachedHashCode == 0)
			{
				this.cachedHashCode = this.scheme.GetHashCode() + this.userinfo.GetHashCode() + this.host.GetHashCode() + this.port + this.path.GetHashCode() + this.query.GetHashCode();
			}
			return this.cachedHashCode;
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x00026F58 File Offset: 0x00025158
		public string GetLeftPart(UriPartial part)
		{
			switch (part)
			{
			case UriPartial.Scheme:
				return this.scheme + this.GetOpaqueWiseSchemeDelimiter();
			case UriPartial.Authority:
			{
				if (this.host == string.Empty || this.scheme == Uri.UriSchemeMailto || this.scheme == Uri.UriSchemeNews)
				{
					return string.Empty;
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(this.scheme);
				stringBuilder.Append(this.GetOpaqueWiseSchemeDelimiter());
				if (this.path.Length > 1 && this.path[1] == ':' && Uri.UriSchemeFile == this.scheme)
				{
					stringBuilder.Append('/');
				}
				if (this.userinfo.Length > 0)
				{
					stringBuilder.Append(this.userinfo).Append('@');
				}
				stringBuilder.Append(this.host);
				int defaultPort = Uri.GetDefaultPort(this.scheme);
				if (this.port != -1 && this.port != defaultPort)
				{
					stringBuilder.Append(':').Append(this.port);
				}
				return stringBuilder.ToString();
			}
			case UriPartial.Path:
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.Append(this.scheme);
				stringBuilder2.Append(this.GetOpaqueWiseSchemeDelimiter());
				if (this.path.Length > 1 && this.path[1] == ':' && Uri.UriSchemeFile == this.scheme)
				{
					stringBuilder2.Append('/');
				}
				if (this.userinfo.Length > 0)
				{
					stringBuilder2.Append(this.userinfo).Append('@');
				}
				stringBuilder2.Append(this.host);
				int defaultPort = Uri.GetDefaultPort(this.scheme);
				if (this.port != -1 && this.port != defaultPort)
				{
					stringBuilder2.Append(':').Append(this.port);
				}
				stringBuilder2.Append(this.path);
				return stringBuilder2.ToString();
			}
			default:
				return null;
			}
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0002718C File Offset: 0x0002538C
		public static int FromHex(char digit)
		{
			if ('0' <= digit && digit <= '9')
			{
				return (int)(digit - '0');
			}
			if ('a' <= digit && digit <= 'f')
			{
				return (int)(digit - 'a' + '\n');
			}
			if ('A' <= digit && digit <= 'F')
			{
				return (int)(digit - 'A' + '\n');
			}
			throw new ArgumentException("digit");
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x000271E8 File Offset: 0x000253E8
		public static string HexEscape(char character)
		{
			if (character > 'ÿ')
			{
				throw new ArgumentOutOfRangeException("character");
			}
			return "%" + Uri.hexUpperChars[(int)((character & 'ð') >> 4)] + Uri.hexUpperChars[(int)(character & '\u000f')];
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x00027240 File Offset: 0x00025440
		public static char HexUnescape(string pattern, ref int index)
		{
			if (pattern == null)
			{
				throw new ArgumentException("pattern");
			}
			if (index < 0 || index >= pattern.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			int num = 0;
			int num2 = 0;
			while (index + 3 <= pattern.Length && pattern[index] == '%' && Uri.IsHexDigit(pattern[index + 1]) && Uri.IsHexDigit(pattern[index + 2]))
			{
				index++;
				int num3 = Uri.FromHex(pattern[index++]);
				int num4 = Uri.FromHex(pattern[index++]);
				int num5 = (num3 << 4) + num4;
				if (num == 0)
				{
					if (num5 < 192)
					{
						return (char)num5;
					}
					if (num5 < 224)
					{
						num2 = num5 - 192;
						num = 2;
					}
					else if (num5 < 240)
					{
						num2 = num5 - 224;
						num = 3;
					}
					else if (num5 < 248)
					{
						num2 = num5 - 240;
						num = 4;
					}
					else if (num5 < 251)
					{
						num2 = num5 - 248;
						num = 5;
					}
					else if (num5 < 254)
					{
						num2 = num5 - 252;
						num = 6;
					}
					num2 <<= (num - 1) * 6;
				}
				else
				{
					num2 += num5 - 128 << (num - 1) * 6;
				}
				num--;
				if (num <= 0)
				{
					IL_1A2:
					return (char)num2;
				}
			}
			if (num == 0)
			{
				return pattern[index++];
			}
			goto IL_1A2;
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x000273F4 File Offset: 0x000255F4
		public static bool IsHexDigit(char digit)
		{
			return ('0' <= digit && digit <= '9') || ('a' <= digit && digit <= 'f') || ('A' <= digit && digit <= 'F');
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x00027438 File Offset: 0x00025638
		public static bool IsHexEncoding(string pattern, int index)
		{
			return index + 3 <= pattern.Length && (pattern[index++] == '%' && Uri.IsHexDigit(pattern[index++])) && Uri.IsHexDigit(pattern[index]);
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x00027490 File Offset: 0x00025690
		public string MakeRelative(Uri toUri)
		{
			if (this.Scheme != toUri.Scheme || this.Authority != toUri.Authority)
			{
				return toUri.ToString();
			}
			if (this.path == toUri.path)
			{
				return string.Empty;
			}
			string[] array = this.Segments;
			string[] array2 = toUri.Segments;
			int i = 0;
			int num = Math.Min(array.Length, array2.Length);
			while (i < num)
			{
				if (array[i] != array2[i])
				{
					break;
				}
				i++;
			}
			string text = string.Empty;
			for (int j = i + 1; j < array.Length; j++)
			{
				text += "../";
			}
			for (int k = i; k < array2.Length; k++)
			{
				text += array2[k];
			}
			return text;
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x00027584 File Offset: 0x00025784
		public override string ToString()
		{
			if (this.cachedToString != null)
			{
				return this.cachedToString;
			}
			string str = (!this.query.StartsWith("?")) ? this.Unescape(this.query) : ('?' + this.Unescape(this.query.Substring(1)));
			this.cachedToString = this.Unescape(this.GetLeftPart(UriPartial.Path), true) + str + this.fragment;
			return this.cachedToString;
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x00027610 File Offset: 0x00025810
		protected void Escape()
		{
			this.path = Uri.EscapeString(this.path);
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x00027624 File Offset: 0x00025824
		protected static string EscapeString(string str)
		{
			return Uri.EscapeString(str, false, true, true);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x00027630 File Offset: 0x00025830
		internal static string EscapeString(string str, bool escapeReserved, bool escapeHex, bool escapeBrackets)
		{
			if (str == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int length = str.Length;
			for (int i = 0; i < length; i++)
			{
				if (Uri.IsHexEncoding(str, i))
				{
					stringBuilder.Append(str.Substring(i, 3));
					i += 2;
				}
				else
				{
					byte[] bytes = Encoding.UTF8.GetBytes(new char[]
					{
						str[i]
					});
					int num = bytes.Length;
					for (int j = 0; j < num; j++)
					{
						char c = (char)bytes[j];
						if (c <= ' ' || c >= '\u007f' || "<>%\"{}|\\^`".IndexOf(c) != -1 || (escapeHex && c == '#') || (escapeBrackets && (c == '[' || c == ']')) || (escapeReserved && ";/?:@&=+$,".IndexOf(c) != -1))
						{
							stringBuilder.Append(Uri.HexEscape(c));
						}
						else
						{
							stringBuilder.Append(c);
						}
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x00027750 File Offset: 0x00025950
		protected void Parse()
		{
			this.Parse(this.source);
			if (this.userEscaped)
			{
				return;
			}
			this.host = Uri.EscapeString(this.host, false, true, false);
			this.path = Uri.EscapeString(this.path);
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0002779C File Offset: 0x0002599C
		protected string Unescape(string str)
		{
			return this.Unescape(str, false);
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x000277A8 File Offset: 0x000259A8
		internal string Unescape(string str, bool excludeSharp)
		{
			if (str == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int length = str.Length;
			for (int i = 0; i < length; i++)
			{
				char c = str[i];
				if (c == '%')
				{
					char c2 = Uri.HexUnescape(str, ref i);
					if (excludeSharp && c2 == '#')
					{
						stringBuilder.Append("%23");
					}
					else
					{
						stringBuilder.Append(c2);
					}
					i--;
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0002783C File Offset: 0x00025A3C
		private void ParseAsWindowsUNC(string uriString)
		{
			this.scheme = Uri.UriSchemeFile;
			this.port = -1;
			this.fragment = string.Empty;
			this.query = string.Empty;
			this.isUnc = true;
			uriString = uriString.TrimStart(new char[]
			{
				'\\'
			});
			int num = uriString.IndexOf('\\');
			if (num > 0)
			{
				this.path = uriString.Substring(num);
				this.host = uriString.Substring(0, num);
			}
			else
			{
				this.host = uriString;
				this.path = string.Empty;
			}
			this.path = this.path.Replace("\\", "/");
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x000278E8 File Offset: 0x00025AE8
		private void ParseAsWindowsAbsoluteFilePath(string uriString)
		{
			if (uriString.Length > 2 && uriString[2] != '\\' && uriString[2] != '/')
			{
				throw new FormatException("Relative file path is not allowed.");
			}
			this.scheme = Uri.UriSchemeFile;
			this.host = string.Empty;
			this.port = -1;
			this.path = uriString.Replace("\\", "/");
			this.fragment = string.Empty;
			this.query = string.Empty;
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00027974 File Offset: 0x00025B74
		private void ParseAsUnixAbsoluteFilePath(string uriString)
		{
			this.isUnixFilePath = true;
			this.scheme = Uri.UriSchemeFile;
			this.port = -1;
			this.fragment = string.Empty;
			this.query = string.Empty;
			this.host = string.Empty;
			this.path = null;
			if (uriString.StartsWith("//"))
			{
				uriString = uriString.TrimStart(new char[]
				{
					'/'
				});
				this.path = '/' + uriString;
			}
			if (this.path == null)
			{
				this.path = uriString;
			}
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00027A0C File Offset: 0x00025C0C
		private void Parse(string uriString)
		{
			if (uriString == null)
			{
				throw new ArgumentNullException("uriString");
			}
			int length = uriString.Length;
			if (length <= 1)
			{
				throw new FormatException();
			}
			int num = uriString.IndexOf(':');
			if (num < 0)
			{
				if (uriString[0] == '/')
				{
					this.ParseAsUnixAbsoluteFilePath(uriString);
				}
				else
				{
					if (!uriString.StartsWith("\\\\"))
					{
						throw new FormatException("URI scheme was not recognized, nor input string is not recognized as an absolute file path.");
					}
					this.ParseAsWindowsUNC(uriString);
				}
				return;
			}
			if (num == 1)
			{
				if (!char.IsLetter(uriString[0]))
				{
					throw new FormatException("URI scheme must start with alphabet character.");
				}
				this.ParseAsWindowsAbsoluteFilePath(uriString);
				return;
			}
			else
			{
				this.scheme = uriString.Substring(0, num).ToLower(CultureInfo.InvariantCulture);
				if (!char.IsLetter(this.scheme[0]))
				{
					throw new FormatException("URI scheme must start with alphabet character.");
				}
				for (int i = 1; i < this.scheme.Length; i++)
				{
					if (!char.IsLetterOrDigit(this.scheme, i))
					{
						switch (this.scheme[i])
						{
						case '+':
						case '-':
						case '.':
							goto IL_132;
						}
						throw new FormatException("URI scheme must consist of one of alphabet, digits, '+', '-' or '.' character.");
					}
					IL_132:;
				}
				uriString = uriString.Substring(num + 1);
				num = uriString.IndexOf('#');
				if (!this.IsUnc && num != -1)
				{
					this.fragment = uriString.Substring(num);
					uriString = uriString.Substring(0, num);
				}
				num = uriString.IndexOf('?');
				if (num != -1)
				{
					this.query = uriString.Substring(num);
					uriString = uriString.Substring(0, num);
					if (!this.userEscaped)
					{
						this.query = Uri.EscapeString(this.query);
					}
				}
				bool flag = this.scheme == Uri.UriSchemeFile && uriString.StartsWith("///");
				if (uriString.StartsWith("//"))
				{
					if (uriString.StartsWith("////"))
					{
						flag = false;
					}
					uriString = uriString.TrimStart(new char[]
					{
						'/'
					});
					if (uriString.Length > 1 && uriString[1] == ':')
					{
						flag = false;
					}
				}
				else if (!Uri.IsPredefinedScheme(this.scheme))
				{
					this.path = uriString;
					this.isOpaquePart = true;
					return;
				}
				num = uriString.IndexOfAny(new char[]
				{
					'/'
				});
				if (flag)
				{
					num = -1;
				}
				if (num == -1)
				{
					if (this.scheme != Uri.UriSchemeMailto && this.scheme != Uri.UriSchemeNews && this.scheme != Uri.UriSchemeFile)
					{
						this.path = "/";
					}
				}
				else
				{
					this.path = uriString.Substring(num);
					uriString = uriString.Substring(0, num);
				}
				num = uriString.IndexOf("@");
				if (num != -1)
				{
					this.userinfo = uriString.Substring(0, num);
					uriString = uriString.Remove(0, num + 1);
				}
				this.port = -1;
				num = uriString.LastIndexOf(":");
				if (flag)
				{
					num = -1;
				}
				if (num != -1 && num != uriString.Length - 1)
				{
					string text = uriString.Remove(0, num + 1);
					if (text.Length > 1 && text[text.Length - 1] != ']')
					{
						try
						{
							this.port = (int)uint.Parse(text, CultureInfo.InvariantCulture);
							uriString = uriString.Substring(0, num);
						}
						catch (Exception)
						{
							throw new FormatException("Invalid URI: invalid port number");
						}
					}
				}
				if (this.port == -1)
				{
					this.port = Uri.GetDefaultPort(this.scheme);
				}
				this.host = uriString;
				if (flag)
				{
					this.path = '/' + uriString;
					this.host = string.Empty;
				}
				else if (this.host.Length == 2 && this.host[1] == ':')
				{
					this.path = this.host + this.path;
					this.host = string.Empty;
				}
				else if (this.isUnixFilePath)
				{
					uriString = "//" + uriString;
					this.host = string.Empty;
				}
				else
				{
					if (this.host.Length == 0)
					{
						throw new FormatException("Invalid URI: The hostname could not be parsed");
					}
					if (this.scheme == Uri.UriSchemeFile)
					{
						this.isUnc = true;
					}
				}
				if (this.scheme != Uri.UriSchemeMailto && this.scheme != Uri.UriSchemeNews && this.scheme != Uri.UriSchemeFile && this.reduce)
				{
					this.path = Uri.Reduce(this.path);
				}
				return;
			}
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x00027F30 File Offset: 0x00026130
		private static string Reduce(string path)
		{
			path = path.Replace('\\', '/');
			string[] array = path.Split(new char[]
			{
				'/'
			});
			ArrayList arrayList = new ArrayList();
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				string text = array[i];
				if (text.Length != 0 && !(text == "."))
				{
					if (text == "..")
					{
						if (arrayList.Count == 0)
						{
							if (i != 1)
							{
								throw new Exception("Invalid path.");
							}
						}
						else
						{
							arrayList.RemoveAt(arrayList.Count - 1);
						}
					}
					else
					{
						arrayList.Add(text);
					}
				}
			}
			if (arrayList.Count == 0)
			{
				return "/";
			}
			arrayList.Insert(0, string.Empty);
			string text2 = string.Join("/", (string[])arrayList.ToArray(typeof(string)));
			if (path.EndsWith("/"))
			{
				text2 += '/';
			}
			return text2;
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x00028050 File Offset: 0x00026250
		internal static string GetSchemeDelimiter(string scheme)
		{
			for (int i = 0; i < Uri.schemes.Length; i++)
			{
				if (Uri.schemes[i].scheme == scheme)
				{
					return Uri.schemes[i].delimiter;
				}
			}
			return Uri.SchemeDelimiter;
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x000280A8 File Offset: 0x000262A8
		internal static int GetDefaultPort(string scheme)
		{
			for (int i = 0; i < Uri.schemes.Length; i++)
			{
				if (Uri.schemes[i].scheme == scheme)
				{
					return Uri.schemes[i].defaultPort;
				}
			}
			return -1;
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x000280FC File Offset: 0x000262FC
		private string GetOpaqueWiseSchemeDelimiter()
		{
			if (this.isOpaquePart)
			{
				return ":";
			}
			return Uri.GetSchemeDelimiter(this.scheme);
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0002811C File Offset: 0x0002631C
		protected bool IsBadFileSystemCharacter(char ch)
		{
			if (ch < ' ' || (ch < '@' && ch > '9'))
			{
				return true;
			}
			switch (ch)
			{
			case '*':
			case ',':
			case '/':
				break;
			default:
				switch (ch)
				{
				case '\\':
				case '^':
					break;
				default:
					if (ch != '\0' && ch != '"' && ch != '&' && ch != '|')
					{
						return false;
					}
					break;
				}
				break;
			}
			return true;
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x000281A4 File Offset: 0x000263A4
		protected static bool IsExcludedCharacter(char ch)
		{
			return ch <= ' ' || ch >= '\u007f' || (ch == '"' || ch == '#' || ch == '%' || ch == '<' || ch == '>' || ch == '[' || ch == '\\' || ch == ']' || ch == '^' || ch == '`' || ch == '{' || ch == '|' || ch == '}');
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x00028230 File Offset: 0x00026430
		private static bool IsPredefinedScheme(string scheme)
		{
			if (scheme != null)
			{
				if (Uri.<>f__switch$map6 == null)
				{
					Uri.<>f__switch$map6 = new Dictionary<string, int>(8)
					{
						{
							"http",
							0
						},
						{
							"https",
							0
						},
						{
							"file",
							0
						},
						{
							"ftp",
							0
						},
						{
							"nntp",
							0
						},
						{
							"gopher",
							0
						},
						{
							"mailto",
							0
						},
						{
							"news",
							0
						}
					};
				}
				int num;
				if (Uri.<>f__switch$map6.TryGetValue(scheme, out num))
				{
					if (num == 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x000282DC File Offset: 0x000264DC
		protected bool IsReservedCharacter(char ch)
		{
			return ch == '$' || ch == '&' || ch == '+' || ch == ',' || ch == '/' || ch == ':' || ch == ';' || ch == '=' || ch == '@';
		}

		// Token: 0x04000211 RID: 529
		private bool isUnixFilePath;

		// Token: 0x04000212 RID: 530
		private string source;

		// Token: 0x04000213 RID: 531
		private string scheme;

		// Token: 0x04000214 RID: 532
		private string host;

		// Token: 0x04000215 RID: 533
		private int port;

		// Token: 0x04000216 RID: 534
		private string path;

		// Token: 0x04000217 RID: 535
		private string query;

		// Token: 0x04000218 RID: 536
		private string fragment;

		// Token: 0x04000219 RID: 537
		private string userinfo;

		// Token: 0x0400021A RID: 538
		private bool isUnc;

		// Token: 0x0400021B RID: 539
		private bool isOpaquePart;

		// Token: 0x0400021C RID: 540
		private string[] segments;

		// Token: 0x0400021D RID: 541
		private bool userEscaped;

		// Token: 0x0400021E RID: 542
		private string cachedAbsoluteUri;

		// Token: 0x0400021F RID: 543
		private string cachedToString;

		// Token: 0x04000220 RID: 544
		private string cachedLocalPath;

		// Token: 0x04000221 RID: 545
		private int cachedHashCode;

		// Token: 0x04000222 RID: 546
		private bool reduce;

		// Token: 0x04000223 RID: 547
		private static readonly string hexUpperChars = "0123456789ABCDEF";

		// Token: 0x04000224 RID: 548
		public static readonly string SchemeDelimiter = "://";

		// Token: 0x04000225 RID: 549
		public static readonly string UriSchemeFile = "file";

		// Token: 0x04000226 RID: 550
		public static readonly string UriSchemeFtp = "ftp";

		// Token: 0x04000227 RID: 551
		public static readonly string UriSchemeGopher = "gopher";

		// Token: 0x04000228 RID: 552
		public static readonly string UriSchemeHttp = "http";

		// Token: 0x04000229 RID: 553
		public static readonly string UriSchemeHttps = "https";

		// Token: 0x0400022A RID: 554
		public static readonly string UriSchemeMailto = "mailto";

		// Token: 0x0400022B RID: 555
		public static readonly string UriSchemeNews = "news";

		// Token: 0x0400022C RID: 556
		public static readonly string UriSchemeNntp = "nntp";

		// Token: 0x0400022D RID: 557
		private static Uri.UriScheme[] schemes = new Uri.UriScheme[]
		{
			new Uri.UriScheme(Uri.UriSchemeHttp, Uri.SchemeDelimiter, 80),
			new Uri.UriScheme(Uri.UriSchemeHttps, Uri.SchemeDelimiter, 443),
			new Uri.UriScheme(Uri.UriSchemeFtp, Uri.SchemeDelimiter, 21),
			new Uri.UriScheme(Uri.UriSchemeFile, Uri.SchemeDelimiter, -1),
			new Uri.UriScheme(Uri.UriSchemeMailto, ":", 25),
			new Uri.UriScheme(Uri.UriSchemeNews, ":", -1),
			new Uri.UriScheme(Uri.UriSchemeNntp, Uri.SchemeDelimiter, 119),
			new Uri.UriScheme(Uri.UriSchemeGopher, Uri.SchemeDelimiter, 70)
		};

		// Token: 0x020000AF RID: 175
		private struct UriScheme
		{
			// Token: 0x060009ED RID: 2541 RVA: 0x00028334 File Offset: 0x00026534
			public UriScheme(string s, string d, int p)
			{
				this.scheme = s;
				this.delimiter = d;
				this.defaultPort = p;
			}

			// Token: 0x0400022F RID: 559
			public string scheme;

			// Token: 0x04000230 RID: 560
			public string delimiter;

			// Token: 0x04000231 RID: 561
			public int defaultPort;
		}
	}
}
