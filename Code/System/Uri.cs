using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Text;

namespace System
{
	/// <summary>Provides an object representation of a uniform resource identifier (URI) and easy access to the parts of the URI.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020004B2 RID: 1202
	[System.ComponentModel.TypeConverter(typeof(System.UriTypeConverter))]
	[Serializable]
	public class Uri : ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Uri" /> class with the specified URI.</summary>
		/// <param name="uriString">A URI. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uriString" /> is null. </exception>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="uriString" /> is empty.-or- The scheme specified in <paramref name="uriString" /> is not correctly formed. See <see cref="M:System.Uri.CheckSchemeName(System.String)" />.-or- <paramref name="uriString" /> contains too many slashes.-or- The password specified in <paramref name="uriString" /> is not valid.-or- The host name specified in <paramref name="uriString" /> is not valid.-or- The file name specified in <paramref name="uriString" /> is not valid. -or- The user name specified in <paramref name="uriString" /> is not valid.-or- The host or authority name specified in <paramref name="uriString" /> cannot be terminated by backslashes.-or- The port number specified in <paramref name="uriString" /> is not valid or cannot be parsed.-or- The length of <paramref name="uriString" /> exceeds 65519 characters.-or- The length of the scheme specified in <paramref name="uriString" /> exceeds 1023 characters.-or- There is an invalid character sequence in <paramref name="uriString" />.-or- The MS-DOS path specified in <paramref name="uriString" /> must start with c:\\.</exception>
		// Token: 0x06002B2B RID: 11051 RVA: 0x000943A4 File Offset: 0x000925A4
		public Uri(string uriString) : this(uriString, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Uri" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">An instance of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> class containing the information required to serialize the new <see cref="T:System.Uri" /> instance. </param>
		/// <param name="streamingContext">An instance of the <see cref="T:System.Runtime.Serialization.StreamingContext" /> class containing the source of the serialized stream associated with the new <see cref="T:System.Uri" /> instance. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="serializationInfo" /> parameter contains a null URI. </exception>
		/// <exception cref="T:System.UriFormatException">The <paramref name="serializationInfo" /> parameter contains a URI that is empty.-or- The scheme specified is not correctly formed. See <see cref="M:System.Uri.CheckSchemeName(System.String)" />.-or- The URI contains too many slashes.-or- The password specified in the URI is not valid.-or- The host name specified in URI is not valid.-or- The file name specified in the URI is not valid. -or- The user name specified in the URI is not valid.-or- The host or authority name specified in the URI cannot be terminated by backslashes.-or- The port number specified in the URI is not valid or cannot be parsed.-or- The length of URI exceeds 65519 characters.-or- The length of the scheme specified in the URI exceeds 1023 characters.-or- There is an invalid character sequence in the URI.-or- The MS-DOS path specified in the URI must start with c:\\.</exception>
		// Token: 0x06002B2C RID: 11052 RVA: 0x000943B0 File Offset: 0x000925B0
		protected Uri(SerializationInfo serializationInfo, StreamingContext streamingContext) : this(serializationInfo.GetString("AbsoluteUri"), true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Uri" /> class with the specified URI. This constructor allows you to specify if the URI string is a relative URI, absolute URI, or is indeterminate.</summary>
		/// <param name="uriString">A string that identifies the resource to be represented by the <see cref="T:System.Uri" /> instance.</param>
		/// <param name="uriKind">Specifies whether the URI string is a relative URI, absolute URI, or is indeterminate.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="uriKind" /> is invalid. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uriString" /> is null. </exception>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="uriString" /> contains a relative URI and <paramref name="uriKind" /> is <see cref="F:System.UriKind.Absolute" />.or<paramref name="uriString" /> contains an absolute URI and <paramref name="uriKind" /> is <see cref="F:System.UriKind.Relative" />.or<paramref name="uriString" /> is empty.-or- The scheme specified in <paramref name="uriString" /> is not correctly formed. See <see cref="M:System.Uri.CheckSchemeName(System.String)" />.-or- <paramref name="uriString" /> contains too many slashes.-or- The password specified in <paramref name="uriString" /> is not valid.-or- The host name specified in <paramref name="uriString" /> is not valid.-or- The file name specified in <paramref name="uriString" /> is not valid. -or- The user name specified in <paramref name="uriString" /> is not valid.-or- The host or authority name specified in <paramref name="uriString" /> cannot be terminated by backslashes.-or- The port number specified in <paramref name="uriString" /> is not valid or cannot be parsed.-or- The length of <paramref name="uriString" /> exceeds 65519 characters.-or- The length of the scheme specified in <paramref name="uriString" /> exceeds 1023 characters.-or- There is an invalid character sequence in <paramref name="uriString" />.-or- The MS-DOS path specified in <paramref name="uriString" /> must start with c:\\.</exception>
		// Token: 0x06002B2D RID: 11053 RVA: 0x000943C4 File Offset: 0x000925C4
		public Uri(string uriString, System.UriKind uriKind)
		{
			this.scheme = string.Empty;
			this.host = string.Empty;
			this.port = -1;
			this.path = string.Empty;
			this.query = string.Empty;
			this.fragment = string.Empty;
			this.userinfo = string.Empty;
			this.isAbsoluteUri = true;
			base..ctor();
			this.source = uriString;
			this.ParseUri(uriKind);
			switch (uriKind)
			{
			case System.UriKind.RelativeOrAbsolute:
				break;
			case System.UriKind.Absolute:
				if (!this.IsAbsoluteUri)
				{
					throw new System.UriFormatException("Invalid URI: The format of the URI could not be determined.");
				}
				break;
			case System.UriKind.Relative:
				if (this.IsAbsoluteUri)
				{
					throw new System.UriFormatException("Invalid URI: The format of the URI could not be determined because the parameter 'uriString' represents an absolute URI.");
				}
				break;
			default:
			{
				string text = Locale.GetText("Invalid UriKind value '{0}'.", new object[]
				{
					uriKind
				});
				throw new ArgumentException(text);
			}
			}
		}

		// Token: 0x06002B2E RID: 11054 RVA: 0x000944AC File Offset: 0x000926AC
		private Uri(string uriString, System.UriKind uriKind, out bool success)
		{
			this.scheme = string.Empty;
			this.host = string.Empty;
			this.port = -1;
			this.path = string.Empty;
			this.query = string.Empty;
			this.fragment = string.Empty;
			this.userinfo = string.Empty;
			this.isAbsoluteUri = true;
			base..ctor();
			if (uriString == null)
			{
				success = false;
				return;
			}
			if (uriKind != System.UriKind.RelativeOrAbsolute && uriKind != System.UriKind.Absolute && uriKind != System.UriKind.Relative)
			{
				string text = Locale.GetText("Invalid UriKind value '{0}'.", new object[]
				{
					uriKind
				});
				throw new ArgumentException(text);
			}
			this.source = uriString;
			if (this.ParseNoExceptions(uriKind, uriString) != null)
			{
				success = false;
			}
			else
			{
				success = true;
				switch (uriKind)
				{
				case System.UriKind.RelativeOrAbsolute:
					break;
				case System.UriKind.Absolute:
					if (!this.IsAbsoluteUri)
					{
						success = false;
					}
					break;
				case System.UriKind.Relative:
					if (this.IsAbsoluteUri)
					{
						success = false;
					}
					break;
				default:
					success = false;
					break;
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Uri" /> class based on the combination of a specified base <see cref="T:System.Uri" /> instance and a relative <see cref="T:System.Uri" /> instance.</summary>
		/// <param name="baseUri">An absolute <see cref="T:System.Uri" /> that is the base for the new <see cref="T:System.Uri" /> instance. </param>
		/// <param name="relativeUri">A relative <see cref="T:System.Uri" /> instance that is combined with <paramref name="baseUri" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="baseUri" /> is null.-or- <paramref name="baseUri" /> is not an absolute <see cref="T:System.Uri" /> instance. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="baseUri" /> is not an absolute <see cref="T:System.Uri" /> instance. </exception>
		/// <exception cref="T:System.UriFormatException">The URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is empty or contains only spaces.-or- The scheme specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is not valid.-or- The URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> contains too many slashes.-or- The password specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is not valid.-or- The host name specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is not valid.-or- The file name specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is not valid. -or- The user name specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is not valid.-or- The host or authority name specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> cannot be terminated by backslashes.-or- The port number specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is not valid or cannot be parsed.-or- The length of the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> exceeds 65519 characters.-or- The length of the scheme specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> exceeds 1023 characters.-or- There is an invalid character sequence in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" />.-or- The MS-DOS path specified in <paramref name="uriString" /> must start with c:\\.</exception>
		// Token: 0x06002B2F RID: 11055 RVA: 0x000945BC File Offset: 0x000927BC
		public Uri(System.Uri baseUri, System.Uri relativeUri)
		{
			this.scheme = string.Empty;
			this.host = string.Empty;
			this.port = -1;
			this.path = string.Empty;
			this.query = string.Empty;
			this.fragment = string.Empty;
			this.userinfo = string.Empty;
			this.isAbsoluteUri = true;
			base..ctor();
			this.Merge(baseUri, (!(relativeUri == null)) ? relativeUri.OriginalString : string.Empty);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Uri" /> class with the specified URI, with explicit control of character escaping.</summary>
		/// <param name="uriString">The URI. </param>
		/// <param name="dontEscape">true if <paramref name="uriString" /> is completely escaped; otherwise, false. See Remarks. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uriString" /> is null. </exception>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="uriString" /> is empty or contains only spaces.-or- The scheme specified in <paramref name="uriString" /> is not valid.-or- <paramref name="uriString" /> contains too many slashes.-or- The password specified in <paramref name="uriString" /> is not valid.-or- The host name specified in <paramref name="uriString" /> is not valid.-or- The file name specified in <paramref name="uriString" /> is not valid. -or- The user name specified in <paramref name="uriString" /> is not valid.-or- The host or authority name specified in <paramref name="uriString" /> cannot be terminated by backslashes.-or- The port number specified in <paramref name="uriString" /> is not valid or cannot be parsed.-or- The length of <paramref name="uriString" /> exceeds 65519 characters.-or- The length of the scheme specified in <paramref name="uriString" /> exceeds 1023 characters.-or- There is an invalid character sequence in <paramref name="uriString" />.-or- The MS-DOS path specified in <paramref name="uriString" /> must start with c:\\.</exception>
		// Token: 0x06002B30 RID: 11056 RVA: 0x00094644 File Offset: 0x00092844
		[Obsolete]
		public Uri(string uriString, bool dontEscape)
		{
			this.scheme = string.Empty;
			this.host = string.Empty;
			this.port = -1;
			this.path = string.Empty;
			this.query = string.Empty;
			this.fragment = string.Empty;
			this.userinfo = string.Empty;
			this.isAbsoluteUri = true;
			base..ctor();
			this.userEscaped = dontEscape;
			this.source = uriString;
			this.ParseUri(System.UriKind.Absolute);
			if (!this.isAbsoluteUri)
			{
				throw new System.UriFormatException("Invalid URI: The format of the URI could not be determined: " + uriString);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Uri" /> class based on the specified base URI and relative URI string.</summary>
		/// <param name="baseUri">The base URI. </param>
		/// <param name="relativeUri">The relative URI to add to the base URI. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uriString" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="baseUri" /> is not an absolute <see cref="T:System.Uri" /> instance. </exception>
		/// <exception cref="T:System.UriFormatException">The URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is empty or contains only spaces.-or- The scheme specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is not valid.-or- The URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> contains too many slashes.-or- The password specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is not valid.-or- The host name specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is not valid.-or- The file name specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is not valid. -or- The user name specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is not valid.-or- The host or authority name specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> cannot be terminated by backslashes.-or- The port number specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is not valid or cannot be parsed.-or- The length of the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> exceeds 65519 characters.-or- The length of the scheme specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> exceeds 1023 characters.-or- There is an invalid character sequence in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" />.-or- The MS-DOS path specified in <paramref name="uriString" /> must start with c:\\.</exception>
		// Token: 0x06002B31 RID: 11057 RVA: 0x000946D8 File Offset: 0x000928D8
		public Uri(System.Uri baseUri, string relativeUri)
		{
			this.scheme = string.Empty;
			this.host = string.Empty;
			this.port = -1;
			this.path = string.Empty;
			this.query = string.Empty;
			this.fragment = string.Empty;
			this.userinfo = string.Empty;
			this.isAbsoluteUri = true;
			base..ctor();
			this.Merge(baseUri, relativeUri);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Uri" /> class based on the specified base and relative URIs, with explicit control of character escaping.</summary>
		/// <param name="baseUri">The base URI. </param>
		/// <param name="relativeUri">The relative URI to add to the base URI. </param>
		/// <param name="dontEscape">true if <paramref name="uriString" /> is completely escaped; otherwise, false. See Remarks. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uriString" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="baseUri" /> is not an absolute <see cref="T:System.Uri" /> instance. </exception>
		/// <exception cref="T:System.UriFormatException">The URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is empty or contains only spaces.-or- The scheme specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is not valid.-or- The URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> contains too many slashes.-or- The password specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is not valid.-or- The host name specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is not valid.-or- The file name specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is not valid. -or- The user name specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is not valid.-or- The host or authority name specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> cannot be terminated by backslashes.-or- The port number specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> is not valid or cannot be parsed.-or- The length of the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> exceeds 65519 characters.-or- The length of the scheme specified in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" /> exceeds 1023 characters.-or- There is an invalid character sequence in the URI formed by combining <paramref name="baseUri" /> and <paramref name="relativeUri" />.-or- The MS-DOS path specified in <paramref name="uriString" /> must start with c:\\.</exception>
		// Token: 0x06002B32 RID: 11058 RVA: 0x00094744 File Offset: 0x00092944
		[Obsolete("dontEscape is always false")]
		public Uri(System.Uri baseUri, string relativeUri, bool dontEscape)
		{
			this.scheme = string.Empty;
			this.host = string.Empty;
			this.port = -1;
			this.path = string.Empty;
			this.query = string.Empty;
			this.fragment = string.Empty;
			this.userinfo = string.Empty;
			this.isAbsoluteUri = true;
			base..ctor();
			this.userEscaped = dontEscape;
			this.Merge(baseUri, relativeUri);
		}

		/// <summary>Returns the data needed to serialize the current instance.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object containing the information required to serialize the <see cref="T:System.Uri" />.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object containing the source and destination of the serialized stream associated with the <see cref="T:System.Uri" />.</param>
		// Token: 0x06002B34 RID: 11060 RVA: 0x00094934 File Offset: 0x00092B34
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("AbsoluteUri", this.AbsoluteUri);
		}

		// Token: 0x06002B35 RID: 11061 RVA: 0x00094948 File Offset: 0x00092B48
		private void Merge(System.Uri baseUri, string relativeUri)
		{
			if (baseUri == null)
			{
				throw new ArgumentNullException("baseUri");
			}
			if (!baseUri.IsAbsoluteUri)
			{
				throw new ArgumentOutOfRangeException("baseUri");
			}
			if (relativeUri == null)
			{
				relativeUri = string.Empty;
			}
			if (relativeUri.Length >= 2 && relativeUri[0] == '\\' && relativeUri[1] == '\\')
			{
				this.source = relativeUri;
				this.ParseUri(System.UriKind.Absolute);
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
					if (string.CompareOrdinal(baseUri.Scheme, 0, relativeUri, 0, num) != 0 || !System.Uri.IsPredefinedScheme(baseUri.Scheme) || (relativeUri.Length > num + 1 && relativeUri[num + 1] == '/'))
					{
						this.source = relativeUri;
						this.ParseUri(System.UriKind.Absolute);
						return;
					}
					relativeUri = relativeUri.Substring(num + 1);
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
				if (this.userEscaped)
				{
					this.fragment = relativeUri.Substring(num);
				}
				else
				{
					this.fragment = "#" + System.Uri.EscapeString(relativeUri.Substring(num + 1));
				}
				relativeUri = relativeUri.Substring(0, num);
			}
			num = relativeUri.IndexOf('?');
			if (num != -1)
			{
				this.query = relativeUri.Substring(num);
				if (!this.userEscaped)
				{
					this.query = System.Uri.EscapeString(this.query);
				}
				relativeUri = relativeUri.Substring(0, num);
			}
			if (relativeUri.Length > 0 && relativeUri[0] == '/')
			{
				if (relativeUri.Length > 1 && relativeUri[1] == '/')
				{
					this.source = this.scheme + ':' + relativeUri;
					this.ParseUri(System.UriKind.Absolute);
					return;
				}
				this.path = relativeUri;
				if (!this.userEscaped)
				{
					this.path = System.Uri.EscapeString(this.path);
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
					this.path = System.Uri.EscapeString(this.path);
				}
				return;
			}
		}

		/// <summary>Gets the absolute path of the URI.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the absolute path to the resource.</returns>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this property is valid only for absolute URIs. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x06002B36 RID: 11062 RVA: 0x00094E94 File Offset: 0x00093094
		public string AbsolutePath
		{
			get
			{
				this.EnsureAbsoluteUri();
				string text = this.Scheme;
				if (text != null)
				{
					if (System.Uri.<>f__switch$map1C == null)
					{
						System.Uri.<>f__switch$map1C = new Dictionary<string, int>(2)
						{
							{
								"mailto",
								0
							},
							{
								"file",
								0
							}
						};
					}
					int num;
					if (System.Uri.<>f__switch$map1C.TryGetValue(text, out num))
					{
						if (num == 0)
						{
							return this.path;
						}
					}
				}
				if (this.path.Length != 0)
				{
					return this.path;
				}
				string value = this.Scheme + System.Uri.SchemeDelimiter;
				if (this.path.StartsWith(value))
				{
					return "/";
				}
				return string.Empty;
			}
		}

		/// <summary>Gets the absolute URI.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the entire URI.</returns>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this property is valid only for absolute URIs. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x06002B37 RID: 11063 RVA: 0x00094F4C File Offset: 0x0009314C
		public string AbsoluteUri
		{
			get
			{
				this.EnsureAbsoluteUri();
				if (this.cachedAbsoluteUri == null)
				{
					this.cachedAbsoluteUri = this.GetLeftPart(System.UriPartial.Path);
					if (this.query.Length > 0)
					{
						this.cachedAbsoluteUri += this.query;
					}
					if (this.fragment.Length > 0)
					{
						this.cachedAbsoluteUri += this.fragment;
					}
				}
				return this.cachedAbsoluteUri;
			}
		}

		/// <summary>Gets the Domain Name System (DNS) host name or IP address and the port number for a server.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the authority component of the URI represented by this instance.</returns>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this property is valid only for absolute URIs. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x06002B38 RID: 11064 RVA: 0x00094FD0 File Offset: 0x000931D0
		public string Authority
		{
			get
			{
				this.EnsureAbsoluteUri();
				return (System.Uri.GetDefaultPort(this.Scheme) != this.port) ? (this.host + ":" + this.port) : this.host;
			}
		}

		/// <summary>Gets the escaped URI fragment.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains any URI fragment information.</returns>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this property is valid only for absolute URIs. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x06002B39 RID: 11065 RVA: 0x00095020 File Offset: 0x00093220
		public string Fragment
		{
			get
			{
				this.EnsureAbsoluteUri();
				return this.fragment;
			}
		}

		/// <summary>Gets the host component of this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the host name. This is usually the DNS host name or IP address of the server.</returns>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this property is valid only for absolute URIs. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x06002B3A RID: 11066 RVA: 0x00095030 File Offset: 0x00093230
		public string Host
		{
			get
			{
				this.EnsureAbsoluteUri();
				return this.host;
			}
		}

		/// <summary>Gets the type of the host name specified in the URI.</summary>
		/// <returns>A member of the <see cref="T:System.UriHostNameType" /> enumeration.</returns>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this property is valid only for absolute URIs. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x06002B3B RID: 11067 RVA: 0x00095040 File Offset: 0x00093240
		public System.UriHostNameType HostNameType
		{
			get
			{
				this.EnsureAbsoluteUri();
				System.UriHostNameType uriHostNameType = System.Uri.CheckHostName(this.Host);
				if (uriHostNameType != System.UriHostNameType.Unknown)
				{
					return uriHostNameType;
				}
				string text = this.Scheme;
				if (text != null)
				{
					if (System.Uri.<>f__switch$map1D == null)
					{
						System.Uri.<>f__switch$map1D = new Dictionary<string, int>(1)
						{
							{
								"mailto",
								0
							}
						};
					}
					int num;
					if (System.Uri.<>f__switch$map1D.TryGetValue(text, out num))
					{
						if (num == 0)
						{
							return System.UriHostNameType.Basic;
						}
					}
				}
				return (!this.IsFile) ? uriHostNameType : System.UriHostNameType.Basic;
			}
		}

		/// <summary>Gets whether the port value of the URI is the default for this scheme.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that is true if the value in the <see cref="P:System.Uri.Port" /> property is the default port for this scheme; otherwise, false.</returns>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this property is valid only for absolute URIs. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x06002B3C RID: 11068 RVA: 0x000950C8 File Offset: 0x000932C8
		public bool IsDefaultPort
		{
			get
			{
				this.EnsureAbsoluteUri();
				return System.Uri.GetDefaultPort(this.Scheme) == this.port;
			}
		}

		/// <summary>Gets a value indicating whether the specified <see cref="T:System.Uri" /> is a file URI.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that is true if the <see cref="T:System.Uri" /> is a file URI; otherwise, false.</returns>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this property is valid only for absolute URIs. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x06002B3D RID: 11069 RVA: 0x000950E4 File Offset: 0x000932E4
		public bool IsFile
		{
			get
			{
				this.EnsureAbsoluteUri();
				return this.Scheme == System.Uri.UriSchemeFile;
			}
		}

		/// <summary>Gets whether the specified <see cref="T:System.Uri" /> references the local host.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that is true if this <see cref="T:System.Uri" /> references the local host; otherwise, false.</returns>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this property is valid only for absolute URIs. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x06002B3E RID: 11070 RVA: 0x000950FC File Offset: 0x000932FC
		public bool IsLoopback
		{
			get
			{
				this.EnsureAbsoluteUri();
				if (this.Host.Length == 0)
				{
					return this.IsFile;
				}
				System.Net.IPAddress other;
				IPv6Address addr;
				return this.host == "loopback" || this.host == "localhost" || (System.Net.IPAddress.TryParse(this.host, out other) && System.Net.IPAddress.Loopback.Equals(other)) || (IPv6Address.TryParse(this.host, out addr) && IPv6Address.IsLoopback(addr));
			}
		}

		/// <summary>Gets whether the specified <see cref="T:System.Uri" /> is a universal naming convention (UNC) path.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that is true if the <see cref="T:System.Uri" /> is a UNC path; otherwise, false.</returns>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this property is valid only for absolute URIs. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x06002B3F RID: 11071 RVA: 0x00095198 File Offset: 0x00093398
		public bool IsUnc
		{
			get
			{
				this.EnsureAbsoluteUri();
				return this.isUnc;
			}
		}

		/// <summary>Gets a local operating-system representation of a file name.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the local operating-system representation of a file name.</returns>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this property is valid only for absolute URIs. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x06002B40 RID: 11072 RVA: 0x000951A8 File Offset: 0x000933A8
		public string LocalPath
		{
			get
			{
				this.EnsureAbsoluteUri();
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
					bool flag2 = flag;
					if (flag2)
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
					string text2 = this.host;
					if (this.path.Length > 0 && (this.path.Length > 1 || this.path[0] != '/'))
					{
						text2 += this.path.Replace('/', '\\');
					}
					this.cachedLocalPath = "\\\\" + this.Unescape(text2);
				}
				else
				{
					this.cachedLocalPath = this.Unescape(this.path);
				}
				if (this.cachedLocalPath.Length == 0)
				{
					this.cachedLocalPath = Path.DirectorySeparatorChar.ToString();
				}
				return this.cachedLocalPath;
			}
		}

		/// <summary>Gets the <see cref="P:System.Uri.AbsolutePath" /> and <see cref="P:System.Uri.Query" /> properties separated by a question mark (?).</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the <see cref="P:System.Uri.AbsolutePath" /> and <see cref="P:System.Uri.Query" /> properties separated by a question mark (?).</returns>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this property is valid only for absolute URIs. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x06002B41 RID: 11073 RVA: 0x00095370 File Offset: 0x00093570
		public string PathAndQuery
		{
			get
			{
				this.EnsureAbsoluteUri();
				return this.path + this.Query;
			}
		}

		/// <summary>Gets the port number of this URI.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that contains the port number for this URI.</returns>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this property is valid only for absolute URIs. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x06002B42 RID: 11074 RVA: 0x0009538C File Offset: 0x0009358C
		public int Port
		{
			get
			{
				this.EnsureAbsoluteUri();
				return this.port;
			}
		}

		/// <summary>Gets any query information included in the specified URI.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains any query information included in the specified URI.</returns>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this property is valid only for absolute URIs. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x06002B43 RID: 11075 RVA: 0x0009539C File Offset: 0x0009359C
		public string Query
		{
			get
			{
				this.EnsureAbsoluteUri();
				return this.query;
			}
		}

		/// <summary>Gets the scheme name for this URI.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the scheme for this URI, converted to lowercase.</returns>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this property is valid only for absolute URIs. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x06002B44 RID: 11076 RVA: 0x000953AC File Offset: 0x000935AC
		public string Scheme
		{
			get
			{
				this.EnsureAbsoluteUri();
				return this.scheme;
			}
		}

		/// <summary>Gets an array containing the path segments that make up the specified URI.</summary>
		/// <returns>A <see cref="T:System.String" /> array that contains the path segments that make up the specified URI.</returns>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this property is valid only for absolute URIs. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x06002B45 RID: 11077 RVA: 0x000953BC File Offset: 0x000935BC
		public string[] Segments
		{
			get
			{
				this.EnsureAbsoluteUri();
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

		/// <summary>Indicates that the URI string was completely escaped before the <see cref="T:System.Uri" /> instance was created.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that is true if the <paramref name="dontEscape" /> parameter was set to true when the <see cref="T:System.Uri" /> instance was created; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x06002B46 RID: 11078 RVA: 0x00095514 File Offset: 0x00093714
		public bool UserEscaped
		{
			get
			{
				return this.userEscaped;
			}
		}

		/// <summary>Gets the user name, password, or other user-specific information associated with the specified URI.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the user information associated with the URI. The returned value does not include the '@' character reserved for delimiting the user information part of the URI.</returns>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this property is valid only for absolute URIs. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x06002B47 RID: 11079 RVA: 0x0009551C File Offset: 0x0009371C
		public string UserInfo
		{
			get
			{
				this.EnsureAbsoluteUri();
				return this.userinfo;
			}
		}

		/// <summary>Gets an unescaped host name that is safe to use for DNS resolution.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the unescaped host part of the URI that is suitable for DNS resolution; or the original unescaped host string, if it is already suitable for resolution.</returns>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this property is valid only for absolute URIs. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x06002B48 RID: 11080 RVA: 0x0009552C File Offset: 0x0009372C
		[MonoTODO("add support for IPv6 address")]
		public string DnsSafeHost
		{
			get
			{
				this.EnsureAbsoluteUri();
				return this.Unescape(this.Host);
			}
		}

		/// <summary>Gets whether the <see cref="T:System.Uri" /> instance is absolute.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that is true if the <see cref="T:System.Uri" /> instance is absolute; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x06002B49 RID: 11081 RVA: 0x00095540 File Offset: 0x00093740
		public bool IsAbsoluteUri
		{
			get
			{
				return this.isAbsoluteUri;
			}
		}

		/// <summary>Gets the original URI string that was passed to the <see cref="T:System.Uri" /> constructor.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the exact URI specified when this instance was constructed; otherwise, <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this property is valid only for absolute URIs. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x06002B4A RID: 11082 RVA: 0x00095548 File Offset: 0x00093748
		public string OriginalString
		{
			get
			{
				return (this.source == null) ? this.ToString() : this.source;
			}
		}

		/// <summary>Determines whether the specified host name is a valid DNS name.</summary>
		/// <returns>A <see cref="T:System.UriHostNameType" /> that indicates the type of the host name. If the type of the host name cannot be determined or if the host name is null or a zero-length string, this method returns <see cref="F:System.UriHostNameType.Unknown" />.</returns>
		/// <param name="name">The host name to validate. This can be an IPv4 or IPv6 address or an Internet host name. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002B4B RID: 11083 RVA: 0x00095568 File Offset: 0x00093768
		public static System.UriHostNameType CheckHostName(string name)
		{
			if (name == null || name.Length == 0)
			{
				return System.UriHostNameType.Unknown;
			}
			if (System.Uri.IsIPv4Address(name))
			{
				return System.UriHostNameType.IPv4;
			}
			if (System.Uri.IsDomainAddress(name))
			{
				return System.UriHostNameType.Dns;
			}
			IPv6Address pv6Address;
			if (IPv6Address.TryParse(name, out pv6Address))
			{
				return System.UriHostNameType.IPv6;
			}
			return System.UriHostNameType.Unknown;
		}

		// Token: 0x06002B4C RID: 11084 RVA: 0x000955B4 File Offset: 0x000937B4
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
				if (array[i].Length == 0)
				{
					return false;
				}
				uint num;
				if (!uint.TryParse(array[i], out num))
				{
					return false;
				}
				if (num > 255U)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002B4D RID: 11085 RVA: 0x00095620 File Offset: 0x00093820
		internal static bool IsDomainAddress(string name)
		{
			int length = name.Length;
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

		/// <summary>Converts the internally stored URI to canonical form.</summary>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this method is valid only for absolute URIs. </exception>
		/// <exception cref="T:System.UriFormatException">The URI is incorrectly formed.</exception>
		// Token: 0x06002B4E RID: 11086 RVA: 0x000956A4 File Offset: 0x000938A4
		[Obsolete("This method does nothing, it has been obsoleted")]
		protected virtual void Canonicalize()
		{
		}

		/// <summary>Calling this method has no effect.</summary>
		// Token: 0x06002B4F RID: 11087 RVA: 0x000956A8 File Offset: 0x000938A8
		[Obsolete]
		[MonoTODO("Find out what this should do")]
		protected virtual void CheckSecurity()
		{
		}

		/// <summary>Determines whether the specified scheme name is valid.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that is true if the scheme name is valid; otherwise, false.</returns>
		/// <param name="schemeName">The scheme name to validate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002B50 RID: 11088 RVA: 0x000956AC File Offset: 0x000938AC
		public static bool CheckSchemeName(string schemeName)
		{
			if (schemeName == null || schemeName.Length == 0)
			{
				return false;
			}
			if (!System.Uri.IsAlpha(schemeName[0]))
			{
				return false;
			}
			int length = schemeName.Length;
			for (int i = 1; i < length; i++)
			{
				char c = schemeName[i];
				if (!char.IsDigit(c) && !System.Uri.IsAlpha(c) && c != '.' && c != '+' && c != '-')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002B51 RID: 11089 RVA: 0x00095734 File Offset: 0x00093934
		private static bool IsAlpha(char c)
		{
			return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
		}

		/// <summary>Compares two <see cref="T:System.Uri" /> instances for equality.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that is true if the two instances represent the same URI; otherwise, false.</returns>
		/// <param name="comparand">The <see cref="T:System.Uri" /> instance or a URI identifier to compare with the current instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002B52 RID: 11090 RVA: 0x0009576C File Offset: 0x0009396C
		public override bool Equals(object comparant)
		{
			if (comparant == null)
			{
				return false;
			}
			System.Uri uri = comparant as System.Uri;
			if (uri == null)
			{
				string text = comparant as string;
				if (text == null)
				{
					return false;
				}
				uri = new System.Uri(text);
			}
			return this.InternalEquals(uri);
		}

		// Token: 0x06002B53 RID: 11091 RVA: 0x000957AC File Offset: 0x000939AC
		private bool InternalEquals(System.Uri uri)
		{
			if (this.isAbsoluteUri != uri.isAbsoluteUri)
			{
				return false;
			}
			if (!this.isAbsoluteUri)
			{
				return this.source == uri.source;
			}
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			return this.scheme.ToLower(invariantCulture) == uri.scheme.ToLower(invariantCulture) && this.host.ToLower(invariantCulture) == uri.host.ToLower(invariantCulture) && this.port == uri.port && this.query == uri.query && this.path == uri.path;
		}

		/// <summary>Gets the hash code for the URI.</summary>
		/// <returns>An <see cref="T:System.Int32" /> containing the hash value generated for this URI.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002B54 RID: 11092 RVA: 0x00095870 File Offset: 0x00093A70
		public override int GetHashCode()
		{
			if (this.cachedHashCode == 0)
			{
				CultureInfo invariantCulture = CultureInfo.InvariantCulture;
				if (this.isAbsoluteUri)
				{
					this.cachedHashCode = (this.scheme.ToLower(invariantCulture).GetHashCode() ^ this.host.ToLower(invariantCulture).GetHashCode() ^ this.port ^ this.query.GetHashCode() ^ this.path.GetHashCode());
				}
				else
				{
					this.cachedHashCode = this.source.GetHashCode();
				}
			}
			return this.cachedHashCode;
		}

		/// <summary>Gets the specified portion of a <see cref="T:System.Uri" /> instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the specified portion of the <see cref="T:System.Uri" /> instance.</returns>
		/// <param name="part">One of the <see cref="T:System.UriPartial" /> values that specifies the end of the URI portion to return. </param>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> instance is not an absolute instance. </exception>
		/// <exception cref="T:System.ArgumentException">The specified <paramref name="part" /> is not valid. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002B55 RID: 11093 RVA: 0x00095900 File Offset: 0x00093B00
		public string GetLeftPart(System.UriPartial part)
		{
			this.EnsureAbsoluteUri();
			switch (part)
			{
			case System.UriPartial.Scheme:
				return this.scheme + this.GetOpaqueWiseSchemeDelimiter();
			case System.UriPartial.Authority:
			{
				if (this.scheme == System.Uri.UriSchemeMailto || this.scheme == System.Uri.UriSchemeNews)
				{
					return string.Empty;
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(this.scheme);
				stringBuilder.Append(this.GetOpaqueWiseSchemeDelimiter());
				if (this.path.Length > 1 && this.path[1] == ':' && System.Uri.UriSchemeFile == this.scheme)
				{
					stringBuilder.Append('/');
				}
				if (this.userinfo.Length > 0)
				{
					stringBuilder.Append(this.userinfo).Append('@');
				}
				stringBuilder.Append(this.host);
				int defaultPort = System.Uri.GetDefaultPort(this.scheme);
				if (this.port != -1 && this.port != defaultPort)
				{
					stringBuilder.Append(':').Append(this.port);
				}
				return stringBuilder.ToString();
			}
			case System.UriPartial.Path:
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.Append(this.scheme);
				stringBuilder2.Append(this.GetOpaqueWiseSchemeDelimiter());
				if (this.path.Length > 1 && this.path[1] == ':' && System.Uri.UriSchemeFile == this.scheme)
				{
					stringBuilder2.Append('/');
				}
				if (this.userinfo.Length > 0)
				{
					stringBuilder2.Append(this.userinfo).Append('@');
				}
				stringBuilder2.Append(this.host);
				int defaultPort = System.Uri.GetDefaultPort(this.scheme);
				if (this.port != -1 && this.port != defaultPort)
				{
					stringBuilder2.Append(':').Append(this.port);
				}
				if (this.path.Length > 0)
				{
					string text = this.Scheme;
					if (text != null)
					{
						if (System.Uri.<>f__switch$map1E == null)
						{
							System.Uri.<>f__switch$map1E = new Dictionary<string, int>(2)
							{
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
						if (System.Uri.<>f__switch$map1E.TryGetValue(text, out num))
						{
							if (num == 0)
							{
								stringBuilder2.Append(this.path);
								goto IL_2A6;
							}
						}
					}
					stringBuilder2.Append(System.Uri.Reduce(this.path, System.Uri.CompactEscaped(this.Scheme)));
				}
				IL_2A6:
				return stringBuilder2.ToString();
			}
			default:
				return null;
			}
		}

		/// <summary>Gets the decimal value of a hexadecimal digit.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that contains a number from 0 to 15 that corresponds to the specified hexadecimal digit.</returns>
		/// <param name="digit">The hexadecimal digit (0-9, a-f, A-F) to convert. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="digit" /> is not a valid hexadecimal digit (0-9, a-f, A-F). </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002B56 RID: 11094 RVA: 0x00095BBC File Offset: 0x00093DBC
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

		/// <summary>Converts a specified character into its hexadecimal equivalent.</summary>
		/// <returns>The hexadecimal representation of the specified character.</returns>
		/// <param name="character">The character to convert to hexadecimal representation. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="character" /> is greater than 255. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002B57 RID: 11095 RVA: 0x00095C18 File Offset: 0x00093E18
		public static string HexEscape(char character)
		{
			if (character > 'ÿ')
			{
				throw new ArgumentOutOfRangeException("character");
			}
			return "%" + System.Uri.hexUpperChars[(int)((character & 'ð') >> 4)] + System.Uri.hexUpperChars[(int)(character & '\u000f')];
		}

		/// <summary>Converts a specified hexadecimal representation of a character to the character.</summary>
		/// <returns>The character represented by the hexadecimal encoding at position <paramref name="index" />. If the character at <paramref name="index" /> is not hexadecimal encoded, the character at <paramref name="index" /> is returned. The value of <paramref name="index" /> is incremented to point to the character following the one returned.</returns>
		/// <param name="pattern">The hexadecimal representation of a character. </param>
		/// <param name="index">The location in <paramref name="pattern" /> where the hexadecimal representation of a character begins. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0 or greater than or equal to the number of characters in <paramref name="pattern" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002B58 RID: 11096 RVA: 0x00095C70 File Offset: 0x00093E70
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
			if (!System.Uri.IsHexEncoding(pattern, index))
			{
				return pattern[index++];
			}
			index++;
			int num = System.Uri.FromHex(pattern[index++]);
			int num2 = System.Uri.FromHex(pattern[index++]);
			return (char)(num << 4 | num2);
		}

		/// <summary>Determines whether a specified character is a valid hexadecimal digit.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that is true if the character is a valid hexadecimal digit; otherwise false.</returns>
		/// <param name="character">The character to validate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002B59 RID: 11097 RVA: 0x00095D04 File Offset: 0x00093F04
		public static bool IsHexDigit(char digit)
		{
			return ('0' <= digit && digit <= '9') || ('a' <= digit && digit <= 'f') || ('A' <= digit && digit <= 'F');
		}

		/// <summary>Determines whether a character in a string is hexadecimal encoded.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that is true if <paramref name="pattern" /> is hexadecimal encoded at the specified location; otherwise, false.</returns>
		/// <param name="pattern">The string to check. </param>
		/// <param name="index">The location in <paramref name="pattern" /> to check for hexadecimal encoding. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002B5A RID: 11098 RVA: 0x00095D48 File Offset: 0x00093F48
		public static bool IsHexEncoding(string pattern, int index)
		{
			return index + 3 <= pattern.Length && (pattern[index++] == '%' && System.Uri.IsHexDigit(pattern[index++])) && System.Uri.IsHexDigit(pattern[index]);
		}

		/// <summary>Determines the difference between two <see cref="T:System.Uri" /> instances.</summary>
		/// <returns>If the hostname and scheme of this URI instance and <paramref name="toUri" /> are the same, then this method returns a relative <see cref="T:System.Uri" /> that, when appended to the current URI instance, yields <paramref name="toUri" />.If the hostname or scheme is different, then this method returns a <see cref="T:System.Uri" />  that represents the <paramref name="toUri" /> parameter.</returns>
		/// <param name="uri">The URI to compare to the current URI.</param>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this property is valid only for absolute URIs. </exception>
		// Token: 0x06002B5B RID: 11099 RVA: 0x00095DA0 File Offset: 0x00093FA0
		public System.Uri MakeRelativeUri(System.Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (this.Host != uri.Host || this.Scheme != uri.Scheme)
			{
				return uri;
			}
			string text = string.Empty;
			if (this.path != uri.path)
			{
				string[] array = this.Segments;
				string[] array2 = uri.Segments;
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
				for (int j = i + 1; j < array.Length; j++)
				{
					text += "../";
				}
				for (int k = i; k < array2.Length; k++)
				{
					text += array2[k];
				}
			}
			uri.AppendQueryAndFragment(ref text);
			return new System.Uri(text, System.UriKind.Relative);
		}

		/// <summary>Determines the difference between two <see cref="T:System.Uri" /> instances.</summary>
		/// <returns>If the hostname and scheme of this URI instance and <paramref name="toUri" /> are the same, then this method returns a <see cref="T:System.String" /> that represents a relative URI that, when appended to the current URI instance, yields the <paramref name="toUri" /> parameter.If the hostname or scheme is different, then this method returns a <see cref="T:System.String" /> that represents the <paramref name="toUri" /> parameter.</returns>
		/// <param name="toUri">The URI to compare to the current URI. </param>
		/// <exception cref="T:System.InvalidOperationException">This instance represents a relative URI, and this method is valid only for absolute URIs. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002B5C RID: 11100 RVA: 0x00095EAC File Offset: 0x000940AC
		[Obsolete("Use MakeRelativeUri(Uri uri) instead.")]
		public string MakeRelative(System.Uri toUri)
		{
			if (this.Scheme != toUri.Scheme || this.Authority != toUri.Authority)
			{
				return toUri.ToString();
			}
			string text = string.Empty;
			if (this.path != toUri.path)
			{
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
				for (int j = i + 1; j < array.Length; j++)
				{
					text += "../";
				}
				for (int k = i; k < array2.Length; k++)
				{
					text += array2[k];
				}
			}
			return text;
		}

		// Token: 0x06002B5D RID: 11101 RVA: 0x00095F98 File Offset: 0x00094198
		private void AppendQueryAndFragment(ref string result)
		{
			if (this.query.Length > 0)
			{
				string str = (this.query[0] != '?') ? System.Uri.Unescape(this.query, false) : ('?' + System.Uri.Unescape(this.query.Substring(1), false));
				result += str;
			}
			if (this.fragment.Length > 0)
			{
				result += this.fragment;
			}
		}

		/// <summary>Gets a canonical string representation for the specified <see cref="T:System.Uri" /> instance.</summary>
		/// <returns>A <see cref="T:System.String" /> instance that contains the unescaped canonical representation of the <see cref="T:System.Uri" /> instance. All characters are unescaped except #, ?, and %.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002B5E RID: 11102 RVA: 0x00096024 File Offset: 0x00094224
		public override string ToString()
		{
			if (this.cachedToString != null)
			{
				return this.cachedToString;
			}
			if (this.isAbsoluteUri)
			{
				this.cachedToString = System.Uri.Unescape(this.GetLeftPart(System.UriPartial.Path), true);
			}
			else
			{
				this.cachedToString = this.Unescape(this.path);
			}
			this.AppendQueryAndFragment(ref this.cachedToString);
			return this.cachedToString;
		}

		/// <summary>Returns the data needed to serialize the current instance.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object containing the information required to serialize the <see cref="T:System.Uri" />.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object containing the source and destination of the serialized stream associated with the <see cref="T:System.Uri" />.</param>
		// Token: 0x06002B5F RID: 11103 RVA: 0x0009608C File Offset: 0x0009428C
		protected void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("AbsoluteUri", this.AbsoluteUri);
		}

		/// <summary>Converts any unsafe or reserved characters in the path component to their hexadecimal character representations.</summary>
		/// <exception cref="T:System.UriFormatException">The URI passed from the constructor is invalid. This exception can occur if a URI has too many characters or the URI is relative.</exception>
		// Token: 0x06002B60 RID: 11104 RVA: 0x000960A0 File Offset: 0x000942A0
		[Obsolete]
		protected virtual void Escape()
		{
			this.path = System.Uri.EscapeString(this.path);
		}

		/// <summary>Converts a string to its escaped representation.</summary>
		/// <returns>The escaped representation of the string.</returns>
		/// <param name="str">The string to transform to its escaped representation. </param>
		// Token: 0x06002B61 RID: 11105 RVA: 0x000960B4 File Offset: 0x000942B4
		[Obsolete]
		protected static string EscapeString(string str)
		{
			return System.Uri.EscapeString(str, false, true, true);
		}

		// Token: 0x06002B62 RID: 11106 RVA: 0x000960C0 File Offset: 0x000942C0
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
				if (System.Uri.IsHexEncoding(str, i))
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
							stringBuilder.Append(System.Uri.HexEscape(c));
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

		/// <summary>Parses the URI of the current instance to ensure it contains all the parts required for a valid URI.</summary>
		/// <exception cref="T:System.UriFormatException">The Uri passed from the constructor is invalid. </exception>
		// Token: 0x06002B63 RID: 11107 RVA: 0x000961E0 File Offset: 0x000943E0
		[Obsolete("The method has been deprecated. It is not used by the system.")]
		protected virtual void Parse()
		{
		}

		// Token: 0x06002B64 RID: 11108 RVA: 0x000961E4 File Offset: 0x000943E4
		private void ParseUri(System.UriKind kind)
		{
			this.Parse(kind, this.source);
			if (this.userEscaped)
			{
				return;
			}
			this.host = System.Uri.EscapeString(this.host, false, true, false);
			if (this.host.Length > 1 && this.host[0] != '[' && this.host[this.host.Length - 1] != ']')
			{
				this.host = this.host.ToLower(CultureInfo.InvariantCulture);
			}
			if (this.path.Length > 0)
			{
				this.path = System.Uri.EscapeString(this.path);
			}
		}

		/// <summary>Converts the specified string by replacing any escape sequences with their unescaped representation.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the unescaped value of the <paramref name="path" /> parameter.</returns>
		/// <param name="path">The <see cref="T:System.String" /> to convert. </param>
		// Token: 0x06002B65 RID: 11109 RVA: 0x0009629C File Offset: 0x0009449C
		[Obsolete]
		protected virtual string Unescape(string str)
		{
			return System.Uri.Unescape(str, false);
		}

		// Token: 0x06002B66 RID: 11110 RVA: 0x000962A8 File Offset: 0x000944A8
		internal static string Unescape(string str, bool excludeSpecial)
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
					char c3;
					char c2 = System.Uri.HexUnescapeMultiByte(str, ref i, out c3);
					if (excludeSpecial && c2 == '#')
					{
						stringBuilder.Append("%23");
					}
					else if (excludeSpecial && c2 == '%')
					{
						stringBuilder.Append("%25");
					}
					else if (excludeSpecial && c2 == '?')
					{
						stringBuilder.Append("%3F");
					}
					else
					{
						stringBuilder.Append(c2);
						if (c3 != '\0')
						{
							stringBuilder.Append(c3);
						}
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

		// Token: 0x06002B67 RID: 11111 RVA: 0x0009638C File Offset: 0x0009458C
		private void ParseAsWindowsUNC(string uriString)
		{
			this.scheme = System.Uri.UriSchemeFile;
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

		// Token: 0x06002B68 RID: 11112 RVA: 0x00096438 File Offset: 0x00094638
		private string ParseAsWindowsAbsoluteFilePath(string uriString)
		{
			if (uriString.Length > 2 && uriString[2] != '\\' && uriString[2] != '/')
			{
				return "Relative file path is not allowed.";
			}
			this.scheme = System.Uri.UriSchemeFile;
			this.host = string.Empty;
			this.port = -1;
			this.path = uriString.Replace("\\", "/");
			this.fragment = string.Empty;
			this.query = string.Empty;
			return null;
		}

		// Token: 0x06002B69 RID: 11113 RVA: 0x000964C0 File Offset: 0x000946C0
		private void ParseAsUnixAbsoluteFilePath(string uriString)
		{
			this.isUnixFilePath = true;
			this.scheme = System.Uri.UriSchemeFile;
			this.port = -1;
			this.fragment = string.Empty;
			this.query = string.Empty;
			this.host = string.Empty;
			this.path = null;
			if (uriString.Length >= 2 && uriString[0] == '/' && uriString[1] == '/')
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

		// Token: 0x06002B6A RID: 11114 RVA: 0x00096570 File Offset: 0x00094770
		private void Parse(System.UriKind kind, string uriString)
		{
			if (uriString == null)
			{
				throw new ArgumentNullException("uriString");
			}
			string text = this.ParseNoExceptions(kind, uriString);
			if (text != null)
			{
				throw new System.UriFormatException(text);
			}
		}

		// Token: 0x06002B6B RID: 11115 RVA: 0x000965A4 File Offset: 0x000947A4
		private string ParseNoExceptions(System.UriKind kind, string uriString)
		{
			uriString = uriString.Trim();
			int length = uriString.Length;
			if (length == 0 && (kind == System.UriKind.Relative || kind == System.UriKind.RelativeOrAbsolute))
			{
				this.isAbsoluteUri = false;
				return null;
			}
			if (length <= 1 && kind != System.UriKind.Relative)
			{
				return "Absolute URI is too short";
			}
			int num = uriString.IndexOf(':');
			if (num == 0)
			{
				return "Invalid URI: The format of the URI could not be determined.";
			}
			if (num < 0)
			{
				if (uriString[0] == '/' && Path.DirectorySeparatorChar == '/')
				{
					this.ParseAsUnixAbsoluteFilePath(uriString);
					if (kind == System.UriKind.Relative)
					{
						this.isAbsoluteUri = false;
					}
				}
				else if (uriString.Length >= 2 && uriString[0] == '\\' && uriString[1] == '\\')
				{
					this.ParseAsWindowsUNC(uriString);
				}
				else
				{
					this.isAbsoluteUri = false;
					this.path = uriString;
				}
				return null;
			}
			if (num == 1)
			{
				if (!System.Uri.IsAlpha(uriString[0]))
				{
					return "URI scheme must start with a letter.";
				}
				string text = this.ParseAsWindowsAbsoluteFilePath(uriString);
				if (text != null)
				{
					return text;
				}
				return null;
			}
			else
			{
				this.scheme = uriString.Substring(0, num).ToLower(CultureInfo.InvariantCulture);
				if (!System.Uri.CheckSchemeName(this.scheme))
				{
					return Locale.GetText("URI scheme must start with a letter and must consist of one of alphabet, digits, '+', '-' or '.' character.");
				}
				int num2 = num + 1;
				int num3 = uriString.Length;
				num = uriString.IndexOf('#', num2);
				if (!this.IsUnc && num != -1)
				{
					if (this.userEscaped)
					{
						this.fragment = uriString.Substring(num);
					}
					else
					{
						this.fragment = "#" + System.Uri.EscapeString(uriString.Substring(num + 1));
					}
					num3 = num;
				}
				num = uriString.IndexOf('?', num2, num3 - num2);
				if (num != -1)
				{
					this.query = uriString.Substring(num, num3 - num);
					num3 = num;
					if (!this.userEscaped)
					{
						this.query = System.Uri.EscapeString(this.query);
					}
				}
				if (System.Uri.IsPredefinedScheme(this.scheme) && this.scheme != System.Uri.UriSchemeMailto && this.scheme != System.Uri.UriSchemeNews && (num3 - num2 < 2 || (num3 - num2 >= 2 && uriString[num2] == '/' && uriString[num2 + 1] != '/')))
				{
					return "Invalid URI: The Authority/Host could not be parsed.";
				}
				bool flag = num3 - num2 >= 2 && uriString[num2] == '/' && uriString[num2 + 1] == '/';
				bool flag2 = this.scheme == System.Uri.UriSchemeFile && flag && (num3 - num2 == 2 || uriString[num2 + 2] == '/');
				bool flag3 = false;
				if (flag)
				{
					if (kind == System.UriKind.Relative)
					{
						return "Absolute URI when we expected a relative one";
					}
					if (this.scheme != System.Uri.UriSchemeMailto && this.scheme != System.Uri.UriSchemeNews)
					{
						num2 += 2;
					}
					if (this.scheme == System.Uri.UriSchemeFile)
					{
						int num4 = 2;
						for (int i = num2; i < num3; i++)
						{
							if (uriString[i] != '/')
							{
								break;
							}
							num4++;
						}
						if (num4 >= 4)
						{
							flag2 = false;
							while (num2 < num3 && uriString[num2] == '/')
							{
								num2++;
							}
						}
						else if (num4 >= 3)
						{
							num2++;
						}
					}
					if (num3 - num2 > 1 && uriString[num2 + 1] == ':')
					{
						flag2 = false;
						flag3 = true;
					}
				}
				else if (!System.Uri.IsPredefinedScheme(this.scheme))
				{
					this.path = uriString.Substring(num2, num3 - num2);
					this.isOpaquePart = true;
					return null;
				}
				if (flag2)
				{
					num = -1;
				}
				else
				{
					num = uriString.IndexOf('/', num2, num3 - num2);
					if (num == -1 && flag3)
					{
						num = uriString.IndexOf('\\', num2, num3 - num2);
					}
				}
				if (num == -1)
				{
					if (this.scheme != System.Uri.UriSchemeMailto && this.scheme != System.Uri.UriSchemeNews)
					{
						this.path = "/";
					}
				}
				else
				{
					this.path = uriString.Substring(num, num3 - num);
					num3 = num;
				}
				if (flag2)
				{
					num = -1;
				}
				else
				{
					num = uriString.IndexOf('@', num2, num3 - num2);
				}
				if (num != -1)
				{
					this.userinfo = uriString.Substring(num2, num - num2);
					num2 = num + 1;
				}
				this.port = -1;
				if (flag2)
				{
					num = -1;
				}
				else
				{
					num = uriString.LastIndexOf(':', num3 - 1, num3 - num2);
				}
				if (num != -1 && num != num3 - 1)
				{
					string text2 = uriString.Substring(num + 1, num3 - (num + 1));
					if (text2.Length > 0 && text2[text2.Length - 1] != ']')
					{
						if (!int.TryParse(text2, NumberStyles.Integer, CultureInfo.InvariantCulture, out this.port) || this.port < 0 || this.port > 65535)
						{
							return "Invalid URI: Invalid port number";
						}
						num3 = num;
					}
					else if (this.port == -1)
					{
						this.port = System.Uri.GetDefaultPort(this.scheme);
					}
				}
				else if (this.port == -1)
				{
					this.port = System.Uri.GetDefaultPort(this.scheme);
				}
				uriString = uriString.Substring(num2, num3 - num2);
				this.host = uriString;
				if (flag2)
				{
					this.path = System.Uri.Reduce('/' + uriString, true);
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
				else if (this.scheme == System.Uri.UriSchemeFile)
				{
					this.isUnc = true;
				}
				else if (this.scheme == System.Uri.UriSchemeNews)
				{
					if (this.host.Length > 0)
					{
						this.path = this.host;
						this.host = string.Empty;
					}
				}
				else if (this.host.Length == 0 && (this.scheme == System.Uri.UriSchemeHttp || this.scheme == System.Uri.UriSchemeGopher || this.scheme == System.Uri.UriSchemeNntp || this.scheme == System.Uri.UriSchemeHttps || this.scheme == System.Uri.UriSchemeFtp))
				{
					return "Invalid URI: The hostname could not be parsed";
				}
				bool flag4 = this.host.Length > 0 && System.Uri.CheckHostName(this.host) == System.UriHostNameType.Unknown;
				if (!flag4 && this.host.Length > 1 && this.host[0] == '[' && this.host[this.host.Length - 1] == ']')
				{
					IPv6Address pv6Address;
					if (IPv6Address.TryParse(this.host, out pv6Address))
					{
						this.host = "[" + pv6Address.ToString(true) + "]";
					}
					else
					{
						flag4 = true;
					}
				}
				if (flag4 && (this.Parser is DefaultUriParser || this.Parser == null))
				{
					return Locale.GetText("Invalid URI: The hostname could not be parsed. (" + this.host + ")");
				}
				System.UriFormatException ex = null;
				if (this.Parser != null)
				{
					this.Parser.InitializeAndValidate(this, out ex);
				}
				if (ex != null)
				{
					return ex.Message;
				}
				if (this.scheme != System.Uri.UriSchemeMailto && this.scheme != System.Uri.UriSchemeNews && this.scheme != System.Uri.UriSchemeFile)
				{
					this.path = System.Uri.Reduce(this.path, System.Uri.CompactEscaped(this.scheme));
				}
				return null;
			}
		}

		// Token: 0x06002B6C RID: 11116 RVA: 0x00096E38 File Offset: 0x00095038
		private static bool CompactEscaped(string scheme)
		{
			if (scheme != null)
			{
				if (System.Uri.<>f__switch$map1F == null)
				{
					System.Uri.<>f__switch$map1F = new Dictionary<string, int>(5)
					{
						{
							"file",
							0
						},
						{
							"http",
							0
						},
						{
							"https",
							0
						},
						{
							"net.pipe",
							0
						},
						{
							"net.tcp",
							0
						}
					};
				}
				int num;
				if (System.Uri.<>f__switch$map1F.TryGetValue(scheme, out num))
				{
					if (num == 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002B6D RID: 11117 RVA: 0x00096EC0 File Offset: 0x000950C0
		private static string Reduce(string path, bool compact_escaped)
		{
			if (path == "/")
			{
				return path;
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (compact_escaped)
			{
				for (int i = 0; i < path.Length; i++)
				{
					char c = path[i];
					char c2 = c;
					if (c2 != '%')
					{
						if (c2 != '\\')
						{
							stringBuilder.Append(c);
						}
						else
						{
							stringBuilder.Append('/');
						}
					}
					else if (i < path.Length - 2)
					{
						char c3 = path[i + 1];
						char c4 = char.ToUpper(path[i + 2]);
						if ((c3 == '2' && c4 == 'F') || (c3 == '5' && c4 == 'C'))
						{
							stringBuilder.Append('/');
							i += 2;
						}
						else
						{
							stringBuilder.Append(c);
						}
					}
					else
					{
						stringBuilder.Append(c);
					}
				}
				path = stringBuilder.ToString();
			}
			else
			{
				path = path.Replace('\\', '/');
			}
			ArrayList arrayList = new ArrayList();
			int j = 0;
			while (j < path.Length)
			{
				int num = path.IndexOf('/', j);
				if (num == -1)
				{
					num = path.Length;
				}
				string text = path.Substring(j, num - j);
				j = num + 1;
				if (text.Length != 0 && !(text == "."))
				{
					if (text == "..")
					{
						int count = arrayList.Count;
						if (count != 0)
						{
							arrayList.RemoveAt(count - 1);
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
			stringBuilder.Length = 0;
			if (path[0] == '/')
			{
				stringBuilder.Append('/');
			}
			bool flag = true;
			foreach (object obj in arrayList)
			{
				string value = (string)obj;
				if (flag)
				{
					flag = false;
				}
				else
				{
					stringBuilder.Append('/');
				}
				stringBuilder.Append(value);
			}
			if (path.EndsWith("/"))
			{
				stringBuilder.Append('/');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002B6E RID: 11118 RVA: 0x00097150 File Offset: 0x00095350
		private static char HexUnescapeMultiByte(string pattern, ref int index, out char surrogate)
		{
			surrogate = '\0';
			if (pattern == null)
			{
				throw new ArgumentException("pattern");
			}
			if (index < 0 || index >= pattern.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (!System.Uri.IsHexEncoding(pattern, index))
			{
				return pattern[index++];
			}
			int num = index++;
			int num2 = System.Uri.FromHex(pattern[index++]);
			int num3 = System.Uri.FromHex(pattern[index++]);
			int num4 = num2;
			int num5 = 0;
			while ((num4 & 8) == 8)
			{
				num5++;
				num4 <<= 1;
			}
			if (num5 <= 1)
			{
				return (char)(num2 << 4 | num3);
			}
			byte[] array = new byte[num5];
			bool flag = false;
			array[0] = (byte)(num2 << 4 | num3);
			for (int i = 1; i < num5; i++)
			{
				if (!System.Uri.IsHexEncoding(pattern, index++))
				{
					flag = true;
					break;
				}
				int num6 = System.Uri.FromHex(pattern[index++]);
				if ((num6 & 12) != 8)
				{
					flag = true;
					break;
				}
				int num7 = System.Uri.FromHex(pattern[index++]);
				array[i] = (byte)(num6 << 4 | num7);
			}
			if (flag)
			{
				index = num + 3;
				return (char)array[0];
			}
			byte b = byte.MaxValue;
			b = (byte)(b >> num5 + 1);
			int num8 = (int)(array[0] & b);
			for (int j = 1; j < num5; j++)
			{
				num8 <<= 6;
				num8 |= (int)(array[j] & 63);
			}
			if (num8 <= 65535)
			{
				return (char)num8;
			}
			num8 -= 65536;
			surrogate = (char)((num8 & 1023) | 56320);
			return (char)(num8 >> 10 | 55296);
		}

		// Token: 0x06002B6F RID: 11119 RVA: 0x00097344 File Offset: 0x00095544
		internal static string GetSchemeDelimiter(string scheme)
		{
			for (int i = 0; i < System.Uri.schemes.Length; i++)
			{
				if (System.Uri.schemes[i].scheme == scheme)
				{
					return System.Uri.schemes[i].delimiter;
				}
			}
			return System.Uri.SchemeDelimiter;
		}

		// Token: 0x06002B70 RID: 11120 RVA: 0x0009739C File Offset: 0x0009559C
		internal static int GetDefaultPort(string scheme)
		{
			System.UriParser uriParser = System.UriParser.GetParser(scheme);
			if (uriParser == null)
			{
				return -1;
			}
			return uriParser.DefaultPort;
		}

		// Token: 0x06002B71 RID: 11121 RVA: 0x000973C0 File Offset: 0x000955C0
		private string GetOpaqueWiseSchemeDelimiter()
		{
			if (this.isOpaquePart)
			{
				return ":";
			}
			return System.Uri.GetSchemeDelimiter(this.scheme);
		}

		/// <summary>Gets whether a character is invalid in a file system name.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that is true if the specified character is invalid; otherwise false.</returns>
		/// <param name="character">The <see cref="T:System.Char" /> to test. </param>
		// Token: 0x06002B72 RID: 11122 RVA: 0x000973E0 File Offset: 0x000955E0
		[Obsolete]
		protected virtual bool IsBadFileSystemCharacter(char ch)
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

		/// <summary>Gets whether the specified character should be escaped.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that is true if the specified character should be escaped; otherwise, false.</returns>
		/// <param name="character">The <see cref="T:System.Char" /> to test. </param>
		// Token: 0x06002B73 RID: 11123 RVA: 0x00097468 File Offset: 0x00095668
		[Obsolete]
		protected static bool IsExcludedCharacter(char ch)
		{
			return ch <= ' ' || ch >= '\u007f' || (ch == '"' || ch == '#' || ch == '%' || ch == '<' || ch == '>' || ch == '[' || ch == '\\' || ch == ']' || ch == '^' || ch == '`' || ch == '{' || ch == '|' || ch == '}');
		}

		// Token: 0x06002B74 RID: 11124 RVA: 0x000974F4 File Offset: 0x000956F4
		internal static bool MaybeUri(string s)
		{
			int num = s.IndexOf(':');
			return num != -1 && num < 10 && System.Uri.IsPredefinedScheme(s.Substring(0, num));
		}

		// Token: 0x06002B75 RID: 11125 RVA: 0x0009752C File Offset: 0x0009572C
		private static bool IsPredefinedScheme(string scheme)
		{
			if (scheme != null)
			{
				if (System.Uri.<>f__switch$map20 == null)
				{
					System.Uri.<>f__switch$map20 = new Dictionary<string, int>(10)
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
						},
						{
							"net.pipe",
							0
						},
						{
							"net.tcp",
							0
						}
					};
				}
				int num;
				if (System.Uri.<>f__switch$map20.TryGetValue(scheme, out num))
				{
					if (num == 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>Gets whether the specified character is a reserved character.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that is true if the specified character is a reserved character otherwise, false.</returns>
		/// <param name="character">The <see cref="T:System.Char" /> to test. </param>
		// Token: 0x06002B76 RID: 11126 RVA: 0x000975F4 File Offset: 0x000957F4
		[Obsolete]
		protected virtual bool IsReservedCharacter(char ch)
		{
			return ch == '$' || ch == '&' || ch == '+' || ch == ',' || ch == '/' || ch == ':' || ch == ';' || ch == '=' || ch == '@';
		}

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x06002B77 RID: 11127 RVA: 0x0009764C File Offset: 0x0009584C
		// (set) Token: 0x06002B78 RID: 11128 RVA: 0x0009768C File Offset: 0x0009588C
		private System.UriParser Parser
		{
			get
			{
				if (this.parser == null)
				{
					this.parser = System.UriParser.GetParser(this.Scheme);
					if (this.parser == null)
					{
						this.parser = new DefaultUriParser("*");
					}
				}
				return this.parser;
			}
			set
			{
				this.parser = value;
			}
		}

		/// <summary>Gets the specified components of the current instance using the specified escaping for special characters.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the components.</returns>
		/// <param name="components">A bitwise combination of the <see cref="T:System.UriComponents" /> values that specifies which parts of the current instance to return to the caller.</param>
		/// <param name="format">One of the <see cref="T:System.UriFormat" /> values that controls how special characters are escaped. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="uriComponents" /> is not a combination of valid <see cref="T:System.UriComponents" /> values.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> is not an absolute URI. Relative URIs cannot be used with this method.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002B79 RID: 11129 RVA: 0x00097698 File Offset: 0x00095898
		public string GetComponents(System.UriComponents components, System.UriFormat format)
		{
			return this.Parser.GetComponents(this, components, format);
		}

		/// <summary>Determines whether the current <see cref="T:System.Uri" /> instance is a base of the specified <see cref="T:System.Uri" /> instance.</summary>
		/// <returns>true if the current <see cref="T:System.Uri" /> instance is a base of <paramref name="uri" />; otherwise, false.</returns>
		/// <param name="uri">The specified <see cref="T:System.Uri" /> instance to test. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002B7A RID: 11130 RVA: 0x000976A8 File Offset: 0x000958A8
		public bool IsBaseOf(System.Uri uri)
		{
			return this.Parser.IsBaseOf(this, uri);
		}

		/// <summary>Indicates whether the string used to construct this <see cref="T:System.Uri" /> was well-formed and is not required to be further escaped.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that is true if the string was well-formed; else false.</returns>
		// Token: 0x06002B7B RID: 11131 RVA: 0x000976B8 File Offset: 0x000958B8
		public bool IsWellFormedOriginalString()
		{
			return System.Uri.EscapeString(this.OriginalString) == this.OriginalString;
		}

		/// <summary>Compares the specified parts of two URIs using the specified comparison rules.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that indicates the lexical relationship between the compared <see cref="T:System.Uri" /> components.ValueMeaningLess than zero<paramref name="uri1" /> is less than <paramref name="uri2" />.Zero<paramref name="uri1" /> equals <paramref name="uri2" />.Greater than zero<paramref name="uri1" /> is greater than <paramref name="uri2" />.</returns>
		/// <param name="uri1">The first <see cref="T:System.Uri" />.</param>
		/// <param name="uri2">The second <see cref="T:System.Uri" />.</param>
		/// <param name="partsToCompare">A bitwise combination of the <see cref="T:System.UriComponents" /> values that specifies the parts of <paramref name="uri1" /> and <paramref name="uri2" /> to compare.</param>
		/// <param name="compareFormat">One of the <see cref="T:System.UriFormat" /> values that specifies the character escaping used when the URI components are compared.</param>
		/// <param name="comparisonType">One of the <see cref="T:System.StringComparison" /> values.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparisonType" /> is not a valid <see cref="T:System.StringComparison" /> value.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002B7C RID: 11132 RVA: 0x000976D0 File Offset: 0x000958D0
		public static int Compare(System.Uri uri1, System.Uri uri2, System.UriComponents partsToCompare, System.UriFormat compareFormat, StringComparison comparisonType)
		{
			if (comparisonType < StringComparison.CurrentCulture || comparisonType > StringComparison.OrdinalIgnoreCase)
			{
				string text = Locale.GetText("Invalid StringComparison value '{0}'", new object[]
				{
					comparisonType
				});
				throw new ArgumentException("comparisonType", text);
			}
			if (uri1 == null && uri2 == null)
			{
				return 0;
			}
			string components = uri1.GetComponents(partsToCompare, compareFormat);
			string components2 = uri2.GetComponents(partsToCompare, compareFormat);
			return string.Compare(components, components2, comparisonType);
		}

		// Token: 0x06002B7D RID: 11133 RVA: 0x0009774C File Offset: 0x0009594C
		private static bool NeedToEscapeDataChar(char b)
		{
			return (b < 'A' || b > 'Z') && (b < 'a' || b > 'z') && (b < '0' || b > '9') && b != '_' && b != '~' && b != '!' && b != '\'' && b != '(' && b != ')' && b != '*' && b != '-' && b != '.';
		}

		/// <summary>Converts a string to its escaped representation.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the escaped representation of <paramref name="stringToEscape" />.</returns>
		/// <param name="stringToEscape">The string to escape.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stringToEscape" /> is null. </exception>
		/// <exception cref="T:System.UriFormatException">The length of <paramref name="stringToEscape" /> exceeds 32766 characters.</exception>
		// Token: 0x06002B7E RID: 11134 RVA: 0x000977D4 File Offset: 0x000959D4
		public static string EscapeDataString(string stringToEscape)
		{
			if (stringToEscape == null)
			{
				throw new ArgumentNullException("stringToEscape");
			}
			if (stringToEscape.Length > 32766)
			{
				string text = Locale.GetText("Uri is longer than the maximum {0} characters.");
				throw new System.UriFormatException(text);
			}
			bool flag = false;
			foreach (char b in stringToEscape)
			{
				if (System.Uri.NeedToEscapeDataChar(b))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return stringToEscape;
			}
			StringBuilder stringBuilder = new StringBuilder();
			byte[] bytes = Encoding.UTF8.GetBytes(stringToEscape);
			foreach (byte b2 in bytes)
			{
				if (System.Uri.NeedToEscapeDataChar((char)b2))
				{
					stringBuilder.Append(System.Uri.HexEscape((char)b2));
				}
				else
				{
					stringBuilder.Append((char)b2);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002B7F RID: 11135 RVA: 0x000978C4 File Offset: 0x00095AC4
		private static bool NeedToEscapeUriChar(char b)
		{
			return (b < 'A' || b > 'Z') && (b < 'a' || b > 'z') && (b < '&' || b > ';') && b != '!' && b != '#' && b != '$' && b != '=' && b != '?' && b != '@' && b != '_' && b != '~';
		}

		/// <summary>Converts a URI string to its escaped representation.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the escaped representation of <paramref name="stringToEscape" />.</returns>
		/// <param name="stringToEscape">The string to escape.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stringToEscape" /> is null. </exception>
		/// <exception cref="T:System.UriFormatException">The length of <paramref name="stringToEscape" /> exceeds 32766 characters.</exception>
		// Token: 0x06002B80 RID: 11136 RVA: 0x00097944 File Offset: 0x00095B44
		public static string EscapeUriString(string stringToEscape)
		{
			if (stringToEscape == null)
			{
				throw new ArgumentNullException("stringToEscape");
			}
			if (stringToEscape.Length > 32766)
			{
				string text = Locale.GetText("Uri is longer than the maximum {0} characters.");
				throw new System.UriFormatException(text);
			}
			bool flag = false;
			foreach (char b in stringToEscape)
			{
				if (System.Uri.NeedToEscapeUriChar(b))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return stringToEscape;
			}
			StringBuilder stringBuilder = new StringBuilder();
			byte[] bytes = Encoding.UTF8.GetBytes(stringToEscape);
			foreach (byte b2 in bytes)
			{
				if (System.Uri.NeedToEscapeUriChar((char)b2))
				{
					stringBuilder.Append(System.Uri.HexEscape((char)b2));
				}
				else
				{
					stringBuilder.Append((char)b2);
				}
			}
			return stringBuilder.ToString();
		}

		/// <summary>Indicates whether the string is well-formed by attempting to construct a URI with the string and ensures that the string does not require further escaping.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that is true if the string was well-formed; else false.</returns>
		/// <param name="uriString">The string used to attempt to construct a <see cref="T:System.Uri" />.</param>
		/// <param name="uriKind">The type of the <see cref="T:System.Uri" /> in <paramref name="uriString" />.</param>
		// Token: 0x06002B81 RID: 11137 RVA: 0x00097A34 File Offset: 0x00095C34
		public static bool IsWellFormedUriString(string uriString, System.UriKind uriKind)
		{
			System.Uri uri;
			return uriString != null && System.Uri.TryCreate(uriString, uriKind, out uri) && uri.IsWellFormedOriginalString();
		}

		/// <summary>Creates a new <see cref="T:System.Uri" /> using the specified <see cref="T:System.String" /> instance and a <see cref="T:System.UriKind" />.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that is true if the <see cref="T:System.Uri" /> was successfully created; otherwise, false.</returns>
		/// <param name="uriString">The <see cref="T:System.String" /> representing the <see cref="T:System.Uri" />.</param>
		/// <param name="uriKind">The type of the Uri.</param>
		/// <param name="result">When this method returns, contains the constructed <see cref="T:System.Uri" />.</param>
		// Token: 0x06002B82 RID: 11138 RVA: 0x00097A60 File Offset: 0x00095C60
		public static bool TryCreate(string uriString, System.UriKind uriKind, out System.Uri result)
		{
			bool flag;
			System.Uri uri = new System.Uri(uriString, uriKind, ref flag);
			if (flag)
			{
				result = uri;
				return true;
			}
			result = null;
			return false;
		}

		/// <summary>Creates a new <see cref="T:System.Uri" /> using the specified base and relative <see cref="T:System.String" /> instances.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that is true if the <see cref="T:System.Uri" /> was successfully created; otherwise, false.</returns>
		/// <param name="baseUri">The base <see cref="T:System.Uri" />.</param>
		/// <param name="relativeUri">The relative <see cref="T:System.Uri" />, represented as a <see cref="T:System.String" />, to add to the base <see cref="T:System.Uri" />.</param>
		/// <param name="result">When this method returns, contains a <see cref="T:System.Uri" /> constructed from <paramref name="baseUri" /> and <paramref name="relativeUri" />. This parameter is passed uninitialized.</param>
		// Token: 0x06002B83 RID: 11139 RVA: 0x00097A88 File Offset: 0x00095C88
		public static bool TryCreate(System.Uri baseUri, string relativeUri, out System.Uri result)
		{
			bool result2;
			try
			{
				result = new System.Uri(baseUri, relativeUri);
				result2 = true;
			}
			catch (System.UriFormatException)
			{
				result = null;
				result2 = false;
			}
			return result2;
		}

		/// <summary>Creates a new <see cref="T:System.Uri" /> using the specified base and relative <see cref="T:System.Uri" /> instances.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that is true if the <see cref="T:System.Uri" /> was successfully created; otherwise, false.</returns>
		/// <param name="baseUri">The base <see cref="T:System.Uri" />. </param>
		/// <param name="relativeUri">The relative <see cref="T:System.Uri" /> to add to the base <see cref="T:System.Uri" />. </param>
		/// <param name="result">When this method returns, contains a <see cref="T:System.Uri" /> constructed from <paramref name="baseUri" /> and <paramref name="relativeUri" />. This parameter is passed uninitialized.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002B84 RID: 11140 RVA: 0x00097AD8 File Offset: 0x00095CD8
		public static bool TryCreate(System.Uri baseUri, System.Uri relativeUri, out System.Uri result)
		{
			bool result2;
			try
			{
				result = new System.Uri(baseUri, relativeUri.OriginalString);
				result2 = true;
			}
			catch (System.UriFormatException)
			{
				result = null;
				result2 = false;
			}
			return result2;
		}

		/// <summary>Converts a string to its unescaped representation.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the unescaped representation of <paramref name="stringToUnescape" />. </returns>
		/// <param name="stringToUnescape">The string to unescape.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stringToUnescape" /> is null. </exception>
		// Token: 0x06002B85 RID: 11141 RVA: 0x00097B2C File Offset: 0x00095D2C
		public static string UnescapeDataString(string stringToUnescape)
		{
			if (stringToUnescape == null)
			{
				throw new ArgumentNullException("stringToUnescape");
			}
			if (stringToUnescape.IndexOf('%') == -1 && stringToUnescape.IndexOf('+') == -1)
			{
				return stringToUnescape;
			}
			StringBuilder stringBuilder = new StringBuilder();
			long num = (long)stringToUnescape.Length;
			MemoryStream memoryStream = new MemoryStream();
			int num2 = 0;
			while ((long)num2 < num)
			{
				if (stringToUnescape[num2] == '%' && (long)(num2 + 2) < num && stringToUnescape[num2 + 1] != '%')
				{
					int @char;
					if (stringToUnescape[num2 + 1] == 'u' && (long)(num2 + 5) < num)
					{
						if (memoryStream.Length > 0L)
						{
							stringBuilder.Append(System.Uri.GetChars(memoryStream, Encoding.UTF8));
							memoryStream.SetLength(0L);
						}
						@char = System.Uri.GetChar(stringToUnescape, num2 + 2, 4);
						if (@char != -1)
						{
							stringBuilder.Append((char)@char);
							num2 += 5;
						}
						else
						{
							stringBuilder.Append('%');
						}
					}
					else if ((@char = System.Uri.GetChar(stringToUnescape, num2 + 1, 2)) != -1)
					{
						memoryStream.WriteByte((byte)@char);
						num2 += 2;
					}
					else
					{
						stringBuilder.Append('%');
					}
				}
				else
				{
					if (memoryStream.Length > 0L)
					{
						stringBuilder.Append(System.Uri.GetChars(memoryStream, Encoding.UTF8));
						memoryStream.SetLength(0L);
					}
					stringBuilder.Append(stringToUnescape[num2]);
				}
				num2++;
			}
			if (memoryStream.Length > 0L)
			{
				stringBuilder.Append(System.Uri.GetChars(memoryStream, Encoding.UTF8));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002B86 RID: 11142 RVA: 0x00097CC8 File Offset: 0x00095EC8
		private static int GetInt(byte b)
		{
			char c = (char)b;
			if (c >= '0' && c <= '9')
			{
				return (int)(c - '0');
			}
			if (c >= 'a' && c <= 'f')
			{
				return (int)(c - 'a' + '\n');
			}
			if (c >= 'A' && c <= 'F')
			{
				return (int)(c - 'A' + '\n');
			}
			return -1;
		}

		// Token: 0x06002B87 RID: 11143 RVA: 0x00097D20 File Offset: 0x00095F20
		private static int GetChar(string str, int offset, int length)
		{
			int num = 0;
			int num2 = length + offset;
			for (int i = offset; i < num2; i++)
			{
				char c = str[i];
				if (c > '\u007f')
				{
					return -1;
				}
				int @int = System.Uri.GetInt((byte)c);
				if (@int == -1)
				{
					return -1;
				}
				num = (num << 4) + @int;
			}
			return num;
		}

		// Token: 0x06002B88 RID: 11144 RVA: 0x00097D74 File Offset: 0x00095F74
		private static char[] GetChars(MemoryStream b, Encoding e)
		{
			return e.GetChars(b.GetBuffer(), 0, (int)b.Length);
		}

		// Token: 0x06002B89 RID: 11145 RVA: 0x00097D98 File Offset: 0x00095F98
		private void EnsureAbsoluteUri()
		{
			if (!this.IsAbsoluteUri)
			{
				throw new InvalidOperationException("This operation is not supported for a relative URI.");
			}
		}

		/// <summary>Determines whether two <see cref="T:System.Uri" /> instances have the same value.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that is true if the <see cref="T:System.Uri" /> instances are equivalent; otherwise, false.</returns>
		/// <param name="uri1">A <see cref="T:System.Uri" /> instance to compare with <paramref name="uri2" />. </param>
		/// <param name="uri2">A <see cref="T:System.Uri" /> instance to compare with <paramref name="uri1" />. </param>
		/// <filterpriority>3</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002B8A RID: 11146 RVA: 0x00097DB0 File Offset: 0x00095FB0
		public static bool operator ==(System.Uri u1, System.Uri u2)
		{
			return object.Equals(u1, u2);
		}

		/// <summary>Determines whether two <see cref="T:System.Uri" /> instances do not have the same value.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that is true if the two <see cref="T:System.Uri" /> instances are not equal; otherwise, false. If either parameter is null, this method returns true.</returns>
		/// <param name="uri1">A <see cref="T:System.Uri" /> instance to compare with <paramref name="uri2" />. </param>
		/// <param name="uri2">A <see cref="T:System.Uri" /> instance to compare with <paramref name="uri1" />. </param>
		/// <filterpriority>3</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002B8B RID: 11147 RVA: 0x00097DBC File Offset: 0x00095FBC
		public static bool operator !=(System.Uri u1, System.Uri u2)
		{
			return !(u1 == u2);
		}

		// Token: 0x04001B40 RID: 6976
		private const int MaxUriLength = 32766;

		// Token: 0x04001B41 RID: 6977
		private bool isUnixFilePath;

		// Token: 0x04001B42 RID: 6978
		private string source;

		// Token: 0x04001B43 RID: 6979
		private string scheme;

		// Token: 0x04001B44 RID: 6980
		private string host;

		// Token: 0x04001B45 RID: 6981
		private int port;

		// Token: 0x04001B46 RID: 6982
		private string path;

		// Token: 0x04001B47 RID: 6983
		private string query;

		// Token: 0x04001B48 RID: 6984
		private string fragment;

		// Token: 0x04001B49 RID: 6985
		private string userinfo;

		// Token: 0x04001B4A RID: 6986
		private bool isUnc;

		// Token: 0x04001B4B RID: 6987
		private bool isOpaquePart;

		// Token: 0x04001B4C RID: 6988
		private bool isAbsoluteUri;

		// Token: 0x04001B4D RID: 6989
		private string[] segments;

		// Token: 0x04001B4E RID: 6990
		private bool userEscaped;

		// Token: 0x04001B4F RID: 6991
		private string cachedAbsoluteUri;

		// Token: 0x04001B50 RID: 6992
		private string cachedToString;

		// Token: 0x04001B51 RID: 6993
		private string cachedLocalPath;

		// Token: 0x04001B52 RID: 6994
		private int cachedHashCode;

		// Token: 0x04001B53 RID: 6995
		private static readonly string hexUpperChars = "0123456789ABCDEF";

		/// <summary>Specifies the characters that separate the communication protocol scheme from the address portion of the URI. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04001B54 RID: 6996
		public static readonly string SchemeDelimiter = "://";

		/// <summary>Specifies that the URI is a pointer to a file. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04001B55 RID: 6997
		public static readonly string UriSchemeFile = "file";

		/// <summary>Specifies that the URI is accessed through the File Transfer Protocol (FTP). This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04001B56 RID: 6998
		public static readonly string UriSchemeFtp = "ftp";

		/// <summary>Specifies that the URI is accessed through the Gopher protocol. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04001B57 RID: 6999
		public static readonly string UriSchemeGopher = "gopher";

		/// <summary>Specifies that the URI is accessed through the Hypertext Transfer Protocol (HTTP). This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04001B58 RID: 7000
		public static readonly string UriSchemeHttp = "http";

		/// <summary>Specifies that the URI is accessed through the Secure Hypertext Transfer Protocol (HTTPS). This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04001B59 RID: 7001
		public static readonly string UriSchemeHttps = "https";

		/// <summary>Specifies that the URI is an e-mail address and is accessed through the Simple Mail Transport Protocol (SMTP). This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04001B5A RID: 7002
		public static readonly string UriSchemeMailto = "mailto";

		/// <summary>Specifies that the URI is an Internet news group and is accessed through the Network News Transport Protocol (NNTP). This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04001B5B RID: 7003
		public static readonly string UriSchemeNews = "news";

		/// <summary>Specifies that the URI is an Internet news group and is accessed through the Network News Transport Protocol (NNTP). This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04001B5C RID: 7004
		public static readonly string UriSchemeNntp = "nntp";

		/// <summary>Specifies that the URI is accessed through the NetPipe scheme used by Windows Communication Foundation (WCF). This field is read-only.</summary>
		// Token: 0x04001B5D RID: 7005
		public static readonly string UriSchemeNetPipe = "net.pipe";

		/// <summary>Specifies that the URI is accessed through the NetTcp scheme used by Windows Communication Foundation (WCF). This field is read-only.</summary>
		// Token: 0x04001B5E RID: 7006
		public static readonly string UriSchemeNetTcp = "net.tcp";

		// Token: 0x04001B5F RID: 7007
		private static System.Uri.UriScheme[] schemes = new System.Uri.UriScheme[]
		{
			new System.Uri.UriScheme(System.Uri.UriSchemeHttp, System.Uri.SchemeDelimiter, 80),
			new System.Uri.UriScheme(System.Uri.UriSchemeHttps, System.Uri.SchemeDelimiter, 443),
			new System.Uri.UriScheme(System.Uri.UriSchemeFtp, System.Uri.SchemeDelimiter, 21),
			new System.Uri.UriScheme(System.Uri.UriSchemeFile, System.Uri.SchemeDelimiter, -1),
			new System.Uri.UriScheme(System.Uri.UriSchemeMailto, ":", 25),
			new System.Uri.UriScheme(System.Uri.UriSchemeNews, ":", 119),
			new System.Uri.UriScheme(System.Uri.UriSchemeNntp, System.Uri.SchemeDelimiter, 119),
			new System.Uri.UriScheme(System.Uri.UriSchemeGopher, System.Uri.SchemeDelimiter, 70)
		};

		// Token: 0x04001B60 RID: 7008
		[NonSerialized]
		private System.UriParser parser;

		// Token: 0x020004B3 RID: 1203
		private struct UriScheme
		{
			// Token: 0x06002B8C RID: 11148 RVA: 0x00097DC8 File Offset: 0x00095FC8
			public UriScheme(string s, string d, int p)
			{
				this.scheme = s;
				this.delimiter = d;
				this.defaultPort = p;
			}

			// Token: 0x04001B66 RID: 7014
			public string scheme;

			// Token: 0x04001B67 RID: 7015
			public string delimiter;

			// Token: 0x04001B68 RID: 7016
			public int defaultPort;
		}
	}
}
