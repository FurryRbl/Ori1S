using System;

namespace System
{
	// Token: 0x02000018 RID: 24
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoInternalNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00006A04 File Offset: 0x00004C04
		public MonoInternalNoteAttribute(string comment) : base(comment)
		{
		}
	}
}
