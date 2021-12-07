using System;
using System.Text;

namespace Microsoft.Win32
{
	// Token: 0x0200006D RID: 109
	internal class ExpandString
	{
		// Token: 0x0600071F RID: 1823 RVA: 0x00015D10 File Offset: 0x00013F10
		public ExpandString(string s)
		{
			this.value = s;
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00015D20 File Offset: 0x00013F20
		public override string ToString()
		{
			return this.value;
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00015D28 File Offset: 0x00013F28
		public string Expand()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.value.Length; i++)
			{
				if (this.value[i] == '%')
				{
					int j;
					for (j = i + 1; j < this.value.Length; j++)
					{
						if (this.value[j] == '%')
						{
							string variable = this.value.Substring(i + 1, j - i - 1);
							stringBuilder.Append(Environment.GetEnvironmentVariable(variable));
							i += j;
							break;
						}
					}
					if (j == this.value.Length)
					{
						stringBuilder.Append('%');
					}
				}
				else
				{
					stringBuilder.Append(this.value[i]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000100 RID: 256
		private string value;
	}
}
