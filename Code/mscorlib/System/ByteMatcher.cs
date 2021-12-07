using System;
using System.Collections;

namespace System
{
	// Token: 0x0200017B RID: 379
	internal class ByteMatcher
	{
		// Token: 0x060013EF RID: 5103 RVA: 0x00050718 File Offset: 0x0004E918
		public void AddMapping(TermInfoStrings key, byte[] val)
		{
			if (val.Length == 0)
			{
				return;
			}
			this.map[val] = key;
			this.starts[(int)val[0]] = true;
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x0005075C File Offset: 0x0004E95C
		public void Sort()
		{
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x00050760 File Offset: 0x0004E960
		public bool StartsWith(int c)
		{
			return this.starts[c] != null;
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x0005077C File Offset: 0x0004E97C
		public TermInfoStrings Match(char[] buffer, int offset, int length, out int used)
		{
			foreach (object obj in this.map.Keys)
			{
				byte[] array = (byte[])obj;
				int num = 0;
				while (num < array.Length && num < length)
				{
					if ((char)array[num] != buffer[offset + num])
					{
						break;
					}
					if (array.Length - 1 == num)
					{
						used = array.Length;
						return (TermInfoStrings)((int)this.map[array]);
					}
					num++;
				}
			}
			used = 0;
			return (TermInfoStrings)(-1);
		}

		// Token: 0x04000613 RID: 1555
		private Hashtable map = new Hashtable();

		// Token: 0x04000614 RID: 1556
		private Hashtable starts = new Hashtable();
	}
}
