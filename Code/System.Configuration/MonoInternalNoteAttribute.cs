using System;

namespace System
{
	// Token: 0x02000073 RID: 115
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoInternalNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x06000403 RID: 1027 RVA: 0x0000B74C File Offset: 0x0000994C
		public MonoInternalNoteAttribute(string comment) : base(comment)
		{
		}
	}
}
