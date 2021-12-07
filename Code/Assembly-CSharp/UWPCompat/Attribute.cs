using System;
using System.Reflection;

namespace UWPCompat
{
	// Token: 0x02000869 RID: 2153
	public static class Attribute
	{
		// Token: 0x0600309E RID: 12446 RVA: 0x000CEB9F File Offset: 0x000CCD9F
		public static bool IsDefined(FieldInfo field, Type type)
		{
			return Attribute.IsDefined(field, type);
		}
	}
}
