using System;

namespace System.IO
{
	// Token: 0x0200024F RID: 591
	internal class SearchPattern
	{
		// Token: 0x06001EB5 RID: 7861 RVA: 0x00071F60 File Offset: 0x00070160
		public SearchPattern(string pattern) : this(pattern, false)
		{
		}

		// Token: 0x06001EB6 RID: 7862 RVA: 0x00071F6C File Offset: 0x0007016C
		public SearchPattern(string pattern, bool ignore)
		{
			this.ignore = ignore;
			this.Compile(pattern);
		}

		// Token: 0x06001EB8 RID: 7864 RVA: 0x00071FC4 File Offset: 0x000701C4
		public bool IsMatch(string text)
		{
			return this.Match(this.ops, text, 0);
		}

		// Token: 0x06001EB9 RID: 7865 RVA: 0x00071FD4 File Offset: 0x000701D4
		private void Compile(string pattern)
		{
			if (pattern == null || pattern.IndexOfAny(SearchPattern.InvalidChars) >= 0)
			{
				throw new ArgumentException("Invalid search pattern.");
			}
			if (pattern == "*")
			{
				this.ops = new SearchPattern.Op(SearchPattern.OpCode.True);
				return;
			}
			this.ops = null;
			int i = 0;
			SearchPattern.Op op = null;
			while (i < pattern.Length)
			{
				char c = pattern[i];
				SearchPattern.Op op2;
				if (c != '*')
				{
					if (c != '?')
					{
						op2 = new SearchPattern.Op(SearchPattern.OpCode.ExactString);
						int num = pattern.IndexOfAny(SearchPattern.WildcardChars, i);
						if (num < 0)
						{
							num = pattern.Length;
						}
						op2.Argument = pattern.Substring(i, num - i);
						if (this.ignore)
						{
							op2.Argument = op2.Argument.ToLowerInvariant();
						}
						i = num;
					}
					else
					{
						op2 = new SearchPattern.Op(SearchPattern.OpCode.AnyChar);
						i++;
					}
				}
				else
				{
					op2 = new SearchPattern.Op(SearchPattern.OpCode.AnyString);
					i++;
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
				this.ops = new SearchPattern.Op(SearchPattern.OpCode.End);
			}
			else
			{
				op.Next = new SearchPattern.Op(SearchPattern.OpCode.End);
			}
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x00072110 File Offset: 0x00070310
		private bool Match(SearchPattern.Op op, string text, int ptr)
		{
			while (op != null)
			{
				switch (op.Code)
				{
				case SearchPattern.OpCode.ExactString:
				{
					int length = op.Argument.Length;
					if (ptr + length > text.Length)
					{
						return false;
					}
					string text2 = text.Substring(ptr, length);
					if (this.ignore)
					{
						text2 = text2.ToLowerInvariant();
					}
					if (text2 != op.Argument)
					{
						return false;
					}
					ptr += length;
					break;
				}
				case SearchPattern.OpCode.AnyChar:
					if (++ptr > text.Length)
					{
						return false;
					}
					break;
				case SearchPattern.OpCode.AnyString:
					while (ptr <= text.Length)
					{
						if (this.Match(op.Next, text, ptr))
						{
							return true;
						}
						ptr++;
					}
					return false;
				case SearchPattern.OpCode.End:
					return ptr == text.Length;
				case SearchPattern.OpCode.True:
					return true;
				}
				op = op.Next;
			}
			return true;
		}

		// Token: 0x04000B90 RID: 2960
		private SearchPattern.Op ops;

		// Token: 0x04000B91 RID: 2961
		private bool ignore;

		// Token: 0x04000B92 RID: 2962
		internal static readonly char[] WildcardChars = new char[]
		{
			'*',
			'?'
		};

		// Token: 0x04000B93 RID: 2963
		internal static readonly char[] InvalidChars = new char[]
		{
			Path.DirectorySeparatorChar,
			Path.AltDirectorySeparatorChar
		};

		// Token: 0x02000250 RID: 592
		private class Op
		{
			// Token: 0x06001EBB RID: 7867 RVA: 0x00072204 File Offset: 0x00070404
			public Op(SearchPattern.OpCode code)
			{
				this.Code = code;
				this.Argument = null;
				this.Next = null;
			}

			// Token: 0x04000B94 RID: 2964
			public SearchPattern.OpCode Code;

			// Token: 0x04000B95 RID: 2965
			public string Argument;

			// Token: 0x04000B96 RID: 2966
			public SearchPattern.Op Next;
		}

		// Token: 0x02000251 RID: 593
		private enum OpCode
		{
			// Token: 0x04000B98 RID: 2968
			ExactString,
			// Token: 0x04000B99 RID: 2969
			AnyChar,
			// Token: 0x04000B9A RID: 2970
			AnyString,
			// Token: 0x04000B9B RID: 2971
			End,
			// Token: 0x04000B9C RID: 2972
			True
		}
	}
}
