using System;

namespace System
{
	// Token: 0x020004D8 RID: 1240
	internal class UriData : IUriData
	{
		// Token: 0x06002C02 RID: 11266 RVA: 0x00098EAC File Offset: 0x000970AC
		public UriData(System.Uri uri, System.UriParser parser)
		{
			this.uri = uri;
			this.parser = parser;
		}

		// Token: 0x06002C03 RID: 11267 RVA: 0x00098EC4 File Offset: 0x000970C4
		private string Lookup(ref string cache, System.UriComponents components)
		{
			return this.Lookup(ref cache, components, (!this.uri.UserEscaped) ? System.UriFormat.UriEscaped : System.UriFormat.Unescaped);
		}

		// Token: 0x06002C04 RID: 11268 RVA: 0x00098EE8 File Offset: 0x000970E8
		private string Lookup(ref string cache, System.UriComponents components, System.UriFormat format)
		{
			if (cache == null)
			{
				cache = this.parser.GetComponents(this.uri, components, format);
			}
			return cache;
		}

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x06002C05 RID: 11269 RVA: 0x00098F08 File Offset: 0x00097108
		public string AbsolutePath
		{
			get
			{
				return this.Lookup(ref this.absolute_path, System.UriComponents.Path | System.UriComponents.KeepDelimiter);
			}
		}

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x06002C06 RID: 11270 RVA: 0x00098F1C File Offset: 0x0009711C
		public string AbsoluteUri
		{
			get
			{
				return this.Lookup(ref this.absolute_uri, System.UriComponents.AbsoluteUri);
			}
		}

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x06002C07 RID: 11271 RVA: 0x00098F2C File Offset: 0x0009712C
		public string AbsoluteUri_SafeUnescaped
		{
			get
			{
				return this.Lookup(ref this.absolute_uri_unescaped, System.UriComponents.AbsoluteUri, System.UriFormat.SafeUnescaped);
			}
		}

		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x06002C08 RID: 11272 RVA: 0x00098F40 File Offset: 0x00097140
		public string Authority
		{
			get
			{
				return this.Lookup(ref this.authority, System.UriComponents.Host | System.UriComponents.Port);
			}
		}

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x06002C09 RID: 11273 RVA: 0x00098F50 File Offset: 0x00097150
		public string Fragment
		{
			get
			{
				return this.Lookup(ref this.fragment, System.UriComponents.Fragment | System.UriComponents.KeepDelimiter);
			}
		}

		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x06002C0A RID: 11274 RVA: 0x00098F64 File Offset: 0x00097164
		public string Host
		{
			get
			{
				return this.Lookup(ref this.host, System.UriComponents.Host);
			}
		}

		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x06002C0B RID: 11275 RVA: 0x00098F74 File Offset: 0x00097174
		public string PathAndQuery
		{
			get
			{
				return this.Lookup(ref this.path_and_query, System.UriComponents.PathAndQuery);
			}
		}

		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x06002C0C RID: 11276 RVA: 0x00098F84 File Offset: 0x00097184
		public string StrongPort
		{
			get
			{
				return this.Lookup(ref this.strong_port, System.UriComponents.StrongPort);
			}
		}

		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x06002C0D RID: 11277 RVA: 0x00098F98 File Offset: 0x00097198
		public string Query
		{
			get
			{
				return this.Lookup(ref this.query, System.UriComponents.Query | System.UriComponents.KeepDelimiter);
			}
		}

		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x06002C0E RID: 11278 RVA: 0x00098FAC File Offset: 0x000971AC
		public string UserInfo
		{
			get
			{
				return this.Lookup(ref this.user_info, System.UriComponents.UserInfo);
			}
		}

		// Token: 0x04001BCA RID: 7114
		private System.Uri uri;

		// Token: 0x04001BCB RID: 7115
		private System.UriParser parser;

		// Token: 0x04001BCC RID: 7116
		private string absolute_path;

		// Token: 0x04001BCD RID: 7117
		private string absolute_uri;

		// Token: 0x04001BCE RID: 7118
		private string absolute_uri_unescaped;

		// Token: 0x04001BCF RID: 7119
		private string authority;

		// Token: 0x04001BD0 RID: 7120
		private string fragment;

		// Token: 0x04001BD1 RID: 7121
		private string host;

		// Token: 0x04001BD2 RID: 7122
		private string path_and_query;

		// Token: 0x04001BD3 RID: 7123
		private string strong_port;

		// Token: 0x04001BD4 RID: 7124
		private string query;

		// Token: 0x04001BD5 RID: 7125
		private string user_info;
	}
}
