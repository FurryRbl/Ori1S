using System;
using System.Collections;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x020004A7 RID: 1191
	internal class BackslashNumber : Reference
	{
		// Token: 0x06002AC2 RID: 10946 RVA: 0x00093010 File Offset: 0x00091210
		public BackslashNumber(bool ignore, bool ecma) : base(ignore)
		{
			this.ecma = ecma;
		}

		// Token: 0x06002AC3 RID: 10947 RVA: 0x00093020 File Offset: 0x00091220
		public bool ResolveReference(string num_str, Hashtable groups)
		{
			if (this.ecma)
			{
				int num = 0;
				for (int i = 1; i < num_str.Length; i++)
				{
					if (groups[num_str.Substring(0, i)] != null)
					{
						num = i;
					}
				}
				if (num != 0)
				{
					base.CapturingGroup = (CapturingGroup)groups[num_str.Substring(0, num)];
					this.literal = num_str.Substring(num);
					return true;
				}
			}
			else if (num_str.Length == 1)
			{
				return false;
			}
			int num2 = 0;
			int num3 = Parser.ParseOctal(num_str, ref num2);
			if (num3 == -1)
			{
				return false;
			}
			if (num3 > 255 && this.ecma)
			{
				num3 /= 8;
				num2--;
			}
			num3 &= 255;
			this.literal = (char)num3 + num_str.Substring(num2);
			return true;
		}

		// Token: 0x06002AC4 RID: 10948 RVA: 0x000930FC File Offset: 0x000912FC
		public override void Compile(ICompiler cmp, bool reverse)
		{
			if (base.CapturingGroup != null)
			{
				base.Compile(cmp, reverse);
			}
			if (this.literal != null)
			{
				Literal.CompileLiteral(this.literal, cmp, base.IgnoreCase, reverse);
			}
		}

		// Token: 0x04001B0E RID: 6926
		private string literal;

		// Token: 0x04001B0F RID: 6927
		private bool ecma;
	}
}
