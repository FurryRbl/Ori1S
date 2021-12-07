using System;

namespace System.IO
{
	// Token: 0x020002A7 RID: 679
	internal class SearchPattern2
	{
		// Token: 0x060017B4 RID: 6068 RVA: 0x000410FC File Offset: 0x0003F2FC
		public SearchPattern2(string pattern) : this(pattern, false)
		{
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x00041108 File Offset: 0x0003F308
		public SearchPattern2(string pattern, bool ignore)
		{
			this.ignore = ignore;
			this.pattern = pattern;
			this.Compile(pattern);
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x00041168 File Offset: 0x0003F368
		public bool IsMatch(string text, bool ignorecase)
		{
			if (this.hasWildcard)
			{
				return this.Match(this.ops, text, 0);
			}
			bool flag = string.Compare(this.pattern, text, ignorecase) == 0;
			if (flag)
			{
				return true;
			}
			int num = text.LastIndexOf('/');
			if (num == -1)
			{
				return false;
			}
			num++;
			return num != text.Length && string.Compare(this.pattern, text.Substring(num), ignorecase) == 0;
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x000411E4 File Offset: 0x0003F3E4
		public bool IsMatch(string text)
		{
			return this.IsMatch(text, this.ignore);
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x060017B9 RID: 6073 RVA: 0x000411F4 File Offset: 0x0003F3F4
		public bool HasWildcard
		{
			get
			{
				return this.hasWildcard;
			}
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x000411FC File Offset: 0x0003F3FC
		private void Compile(string pattern)
		{
			if (pattern == null || pattern.IndexOfAny(SearchPattern2.InvalidChars) >= 0)
			{
				throw new ArgumentException("Invalid search pattern: '" + pattern + "'");
			}
			if (pattern == "*")
			{
				this.ops = new SearchPattern2.Op(SearchPattern2.OpCode.True);
				this.hasWildcard = true;
				return;
			}
			this.ops = null;
			int i = 0;
			SearchPattern2.Op op = null;
			while (i < pattern.Length)
			{
				char c = pattern[i];
				SearchPattern2.Op op2;
				if (c != '*')
				{
					if (c != '?')
					{
						op2 = new SearchPattern2.Op(SearchPattern2.OpCode.ExactString);
						int num = pattern.IndexOfAny(SearchPattern2.WildcardChars, i);
						if (num < 0)
						{
							num = pattern.Length;
						}
						op2.Argument = pattern.Substring(i, num - i);
						if (this.ignore)
						{
							op2.Argument = op2.Argument.ToLower();
						}
						i = num;
					}
					else
					{
						op2 = new SearchPattern2.Op(SearchPattern2.OpCode.AnyChar);
						i++;
						this.hasWildcard = true;
					}
				}
				else
				{
					op2 = new SearchPattern2.Op(SearchPattern2.OpCode.AnyString);
					i++;
					this.hasWildcard = true;
				}
				if (op == null)
				{
					this.ops = op2;
				}
				else
				{
					op.Next = op2;
				}
				op = op2;
			}
			if (op == null)
			{
				this.ops = new SearchPattern2.Op(SearchPattern2.OpCode.End);
			}
			else
			{
				op.Next = new SearchPattern2.Op(SearchPattern2.OpCode.End);
			}
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x00041358 File Offset: 0x0003F558
		private bool Match(SearchPattern2.Op op, string text, int ptr)
		{
			while (op != null)
			{
				switch (op.Code)
				{
				case SearchPattern2.OpCode.ExactString:
				{
					int length = op.Argument.Length;
					if (ptr + length > text.Length)
					{
						return false;
					}
					string text2 = text.Substring(ptr, length);
					if (this.ignore)
					{
						text2 = text2.ToLower();
					}
					if (text2 != op.Argument)
					{
						return false;
					}
					ptr += length;
					break;
				}
				case SearchPattern2.OpCode.AnyChar:
					if (++ptr > text.Length)
					{
						return false;
					}
					break;
				case SearchPattern2.OpCode.AnyString:
					while (ptr <= text.Length)
					{
						if (this.Match(op.Next, text, ptr))
						{
							return true;
						}
						ptr++;
					}
					return false;
				case SearchPattern2.OpCode.End:
					return ptr == text.Length;
				case SearchPattern2.OpCode.True:
					return true;
				}
				op = op.Next;
			}
			return true;
		}

		// Token: 0x04000F0F RID: 3855
		private SearchPattern2.Op ops;

		// Token: 0x04000F10 RID: 3856
		private bool ignore;

		// Token: 0x04000F11 RID: 3857
		private bool hasWildcard;

		// Token: 0x04000F12 RID: 3858
		private string pattern;

		// Token: 0x04000F13 RID: 3859
		internal static readonly char[] WildcardChars = new char[]
		{
			'*',
			'?'
		};

		// Token: 0x04000F14 RID: 3860
		internal static readonly char[] InvalidChars = new char[]
		{
			Path.DirectorySeparatorChar,
			Path.AltDirectorySeparatorChar
		};

		// Token: 0x020002A8 RID: 680
		private class Op
		{
			// Token: 0x060017BC RID: 6076 RVA: 0x0004144C File Offset: 0x0003F64C
			public Op(SearchPattern2.OpCode code)
			{
				this.Code = code;
				this.Argument = null;
				this.Next = null;
			}

			// Token: 0x04000F15 RID: 3861
			public SearchPattern2.OpCode Code;

			// Token: 0x04000F16 RID: 3862
			public string Argument;

			// Token: 0x04000F17 RID: 3863
			public SearchPattern2.Op Next;
		}

		// Token: 0x020002A9 RID: 681
		private enum OpCode
		{
			// Token: 0x04000F19 RID: 3865
			ExactString,
			// Token: 0x04000F1A RID: 3866
			AnyChar,
			// Token: 0x04000F1B RID: 3867
			AnyString,
			// Token: 0x04000F1C RID: 3868
			End,
			// Token: 0x04000F1D RID: 3869
			True
		}
	}
}
