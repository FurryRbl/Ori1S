using System;

namespace System
{
	// Token: 0x02000017 RID: 23
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoExtensionAttribute : MonoTODOAttribute
	{
		// Token: 0x06000079 RID: 121 RVA: 0x000069F8 File Offset: 0x00004BF8
		public MonoExtensionAttribute(string comment) : base(comment)
		{
		}
	}
}
