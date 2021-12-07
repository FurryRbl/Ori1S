using System;

namespace System.Net
{
	// Token: 0x020002F9 RID: 761
	internal class DigestHeaderParser
	{
		// Token: 0x06001A06 RID: 6662 RVA: 0x00048074 File Offset: 0x00046274
		public DigestHeaderParser(string header)
		{
			this.header = header.Trim();
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001A08 RID: 6664 RVA: 0x000480E0 File Offset: 0x000462E0
		public string Realm
		{
			get
			{
				return this.values[0];
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001A09 RID: 6665 RVA: 0x000480EC File Offset: 0x000462EC
		public string Opaque
		{
			get
			{
				return this.values[1];
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001A0A RID: 6666 RVA: 0x000480F8 File Offset: 0x000462F8
		public string Nonce
		{
			get
			{
				return this.values[2];
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06001A0B RID: 6667 RVA: 0x00048104 File Offset: 0x00046304
		public string Algorithm
		{
			get
			{
				return this.values[3];
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06001A0C RID: 6668 RVA: 0x00048110 File Offset: 0x00046310
		public string QOP
		{
			get
			{
				return this.values[4];
			}
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x0004811C File Offset: 0x0004631C
		public bool Parse()
		{
			if (!this.header.ToLower().StartsWith("digest "))
			{
				return false;
			}
			this.pos = 6;
			this.length = this.header.Length;
			while (this.pos < this.length)
			{
				string value;
				string text;
				if (!this.GetKeywordAndValue(out value, out text))
				{
					return false;
				}
				this.SkipWhitespace();
				if (this.pos < this.length && this.header[this.pos] == ',')
				{
					this.pos++;
				}
				int num = Array.IndexOf<string>(DigestHeaderParser.keywords, value);
				if (num != -1)
				{
					if (this.values[num] != null)
					{
						return false;
					}
					this.values[num] = text;
				}
			}
			return this.Realm != null && this.Nonce != null;
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x0004820C File Offset: 0x0004640C
		private void SkipWhitespace()
		{
			char c = ' ';
			while (this.pos < this.length && (c == ' ' || c == '\t' || c == '\r' || c == '\n'))
			{
				c = this.header[this.pos++];
			}
			this.pos--;
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x00048280 File Offset: 0x00046480
		private string GetKey()
		{
			this.SkipWhitespace();
			int num = this.pos;
			while (this.pos < this.length && this.header[this.pos] != '=')
			{
				this.pos++;
			}
			return this.header.Substring(num, this.pos - num).Trim().ToLower();
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x000482F8 File Offset: 0x000464F8
		private bool GetKeywordAndValue(out string key, out string value)
		{
			key = null;
			value = null;
			key = this.GetKey();
			if (this.pos >= this.length)
			{
				return false;
			}
			this.SkipWhitespace();
			if (this.pos + 1 >= this.length || this.header[this.pos++] != '=')
			{
				return false;
			}
			this.SkipWhitespace();
			if (this.pos + 1 >= this.length)
			{
				return false;
			}
			bool flag = false;
			if (this.header[this.pos] == '"')
			{
				this.pos++;
				flag = true;
			}
			int num = this.pos;
			if (flag)
			{
				this.pos = this.header.IndexOf('"', this.pos);
				if (this.pos == -1)
				{
					return false;
				}
			}
			else
			{
				do
				{
					char c = this.header[this.pos];
					if (c == ',' || c == ' ' || c == '\t' || c == '\r' || c == '\n')
					{
						break;
					}
				}
				while (++this.pos < this.length);
				if (this.pos >= this.length && num == this.pos)
				{
					return false;
				}
			}
			value = this.header.Substring(num, this.pos - num);
			this.pos += 2;
			return true;
		}

		// Token: 0x0400103A RID: 4154
		private string header;

		// Token: 0x0400103B RID: 4155
		private int length;

		// Token: 0x0400103C RID: 4156
		private int pos;

		// Token: 0x0400103D RID: 4157
		private static string[] keywords = new string[]
		{
			"realm",
			"opaque",
			"nonce",
			"algorithm",
			"qop"
		};

		// Token: 0x0400103E RID: 4158
		private string[] values = new string[DigestHeaderParser.keywords.Length];
	}
}
