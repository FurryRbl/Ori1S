using System;

namespace System
{
	// Token: 0x0200000B RID: 11
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoInternalNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x00005890 File Offset: 0x00003A90
		public MonoInternalNoteAttribute(string comment) : base(comment)
		{
		}
	}
}
