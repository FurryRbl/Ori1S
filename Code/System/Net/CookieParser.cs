using System;

namespace System.Net
{
	// Token: 0x02000324 RID: 804
	internal class CookieParser
	{
		// Token: 0x06001C96 RID: 7318 RVA: 0x00054140 File Offset: 0x00052340
		public CookieParser(string header) : this(header, 0)
		{
		}

		// Token: 0x06001C97 RID: 7319 RVA: 0x0005414C File Offset: 0x0005234C
		public CookieParser(string header, int position)
		{
			this.header = header;
			this.pos = position;
			this.length = header.Length;
		}

		// Token: 0x06001C98 RID: 7320 RVA: 0x0005417C File Offset: 0x0005237C
		public bool GetNextNameValue(out string name, out string val)
		{
			name = null;
			val = null;
			if (this.pos >= this.length)
			{
				return false;
			}
			name = this.GetCookieName();
			if (this.pos < this.header.Length && this.header[this.pos] == '=')
			{
				this.pos++;
				val = this.GetCookieValue();
			}
			if (this.pos < this.length && this.header[this.pos] == ';')
			{
				this.pos++;
			}
			return true;
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x00054228 File Offset: 0x00052428
		private string GetCookieName()
		{
			int num = this.pos;
			while (num < this.length && char.IsWhiteSpace(this.header[num]))
			{
				num++;
			}
			int num2 = num;
			while (num < this.length && this.header[num] != ';' && this.header[num] != '=')
			{
				num++;
			}
			this.pos = num;
			return this.header.Substring(num2, num - num2).Trim();
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x000542C0 File Offset: 0x000524C0
		private string GetCookieValue()
		{
			if (this.pos >= this.length)
			{
				return null;
			}
			int num = this.pos;
			while (num < this.length && char.IsWhiteSpace(this.header[num]))
			{
				num++;
			}
			int num2;
			if (this.header[num] == '"')
			{
				num = (num2 = num + 1);
				while (num < this.length && this.header[num] != '"')
				{
					num++;
				}
				int num3 = num;
				while (num3 < this.length && this.header[num3] != ';')
				{
					num3++;
				}
				this.pos = num3;
			}
			else
			{
				num2 = num;
				while (num < this.length && this.header[num] != ';')
				{
					num++;
				}
				this.pos = num;
			}
			return this.header.Substring(num2, num - num2).Trim();
		}

		// Token: 0x04001201 RID: 4609
		private string header;

		// Token: 0x04001202 RID: 4610
		private int pos;

		// Token: 0x04001203 RID: 4611
		private int length;
	}
}
