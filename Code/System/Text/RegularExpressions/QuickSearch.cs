using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000488 RID: 1160
	internal class QuickSearch
	{
		// Token: 0x06002989 RID: 10633 RVA: 0x0008ACEC File Offset: 0x00088EEC
		public QuickSearch(string str, bool ignore) : this(str, ignore, false)
		{
		}

		// Token: 0x0600298A RID: 10634 RVA: 0x0008ACF8 File Offset: 0x00088EF8
		public QuickSearch(string str, bool ignore, bool reverse)
		{
			this.str = str;
			this.len = str.Length;
			this.ignore = ignore;
			this.reverse = reverse;
			if (ignore)
			{
				str = str.ToLower();
			}
			if (this.len > QuickSearch.THRESHOLD)
			{
				this.SetupShiftTable();
			}
		}

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x0600298C RID: 10636 RVA: 0x0008AD58 File Offset: 0x00088F58
		public string String
		{
			get
			{
				return this.str;
			}
		}

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x0600298D RID: 10637 RVA: 0x0008AD60 File Offset: 0x00088F60
		public int Length
		{
			get
			{
				return this.len;
			}
		}

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x0600298E RID: 10638 RVA: 0x0008AD68 File Offset: 0x00088F68
		public bool IgnoreCase
		{
			get
			{
				return this.ignore;
			}
		}

		// Token: 0x0600298F RID: 10639 RVA: 0x0008AD70 File Offset: 0x00088F70
		public int Search(string text, int start, int end)
		{
			int i = start;
			if (this.reverse)
			{
				if (start < end)
				{
					return -1;
				}
				if (i > text.Length)
				{
					i = text.Length;
				}
				if (this.len == 1)
				{
					while (--i >= end)
					{
						if (this.str[0] == this.GetChar(text[i]))
						{
							return i;
						}
					}
					return -1;
				}
				if (end < this.len)
				{
					end = this.len - 1;
				}
				for (i--; i >= end; i -= this.GetShiftDistance(text[i - this.len]))
				{
					int num = this.len - 1;
					while (this.str[num] == this.GetChar(text[i - this.len + 1 + num]))
					{
						if (--num < 0)
						{
							return i - this.len + 1;
						}
					}
					if (i <= end)
					{
						break;
					}
				}
			}
			else
			{
				if (this.len == 1)
				{
					while (i <= end)
					{
						if (this.str[0] == this.GetChar(text[i]))
						{
							return i;
						}
						i++;
					}
					return -1;
				}
				if (end > text.Length - this.len)
				{
					end = text.Length - this.len;
				}
				while (i <= end)
				{
					int num2 = this.len - 1;
					while (this.str[num2] == this.GetChar(text[i + num2]))
					{
						if (--num2 < 0)
						{
							return i;
						}
					}
					if (i >= end)
					{
						break;
					}
					i += this.GetShiftDistance(text[i + this.len]);
				}
			}
			return -1;
		}

		// Token: 0x06002990 RID: 10640 RVA: 0x0008AF54 File Offset: 0x00089154
		private void SetupShiftTable()
		{
			bool flag = this.len > 254;
			byte b = 0;
			for (int i = 0; i < this.len; i++)
			{
				char c = this.str[i];
				if (c <= 'ÿ')
				{
					if ((byte)c > b)
					{
						b = (byte)c;
					}
				}
				else
				{
					flag = true;
				}
			}
			this.shift = new byte[(int)(b + 1)];
			if (flag)
			{
				this.shiftExtended = new Hashtable();
			}
			int j = 0;
			int num = this.len;
			while (j < this.len)
			{
				char c2 = this.str[this.reverse ? (num - 1) : j];
				if ((int)c2 >= this.shift.Length)
				{
					goto IL_DD;
				}
				if (num >= 255)
				{
					this.shift[(int)c2] = byte.MaxValue;
					goto IL_DD;
				}
				this.shift[(int)c2] = (byte)num;
				IL_F6:
				j++;
				num--;
				continue;
				IL_DD:
				this.shiftExtended[c2] = num;
				goto IL_F6;
			}
		}

		// Token: 0x06002991 RID: 10641 RVA: 0x0008B070 File Offset: 0x00089270
		private int GetShiftDistance(char c)
		{
			if (this.shift == null)
			{
				return 1;
			}
			c = this.GetChar(c);
			if ((int)c < this.shift.Length)
			{
				int num = (int)this.shift[(int)c];
				if (num == 0)
				{
					return this.len + 1;
				}
				if (num != 255)
				{
					return num;
				}
			}
			else if (c < 'ÿ')
			{
				return this.len + 1;
			}
			if (this.shiftExtended == null)
			{
				return this.len + 1;
			}
			object obj = this.shiftExtended[c];
			return (obj == null) ? (this.len + 1) : ((int)obj);
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x0008B120 File Offset: 0x00089320
		private char GetChar(char c)
		{
			return this.ignore ? char.ToLower(c) : c;
		}

		// Token: 0x04001A02 RID: 6658
		private string str;

		// Token: 0x04001A03 RID: 6659
		private int len;

		// Token: 0x04001A04 RID: 6660
		private bool ignore;

		// Token: 0x04001A05 RID: 6661
		private bool reverse;

		// Token: 0x04001A06 RID: 6662
		private byte[] shift;

		// Token: 0x04001A07 RID: 6663
		private Hashtable shiftExtended;

		// Token: 0x04001A08 RID: 6664
		private static readonly int THRESHOLD = 5;
	}
}
