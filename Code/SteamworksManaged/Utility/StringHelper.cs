using System;
using System.Text;

namespace ManagedSteam.Utility
{
	// Token: 0x02000079 RID: 121
	public static class StringHelper
	{
		// Token: 0x06000406 RID: 1030 RVA: 0x00007AA2 File Offset: 0x00005CA2
		public static int GetByteCountUtf8(string value)
		{
			return Encoding.UTF8.GetByteCount(value);
		}
	}
}
