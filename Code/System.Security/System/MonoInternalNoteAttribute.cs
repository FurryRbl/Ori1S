using System;

namespace System
{
	// Token: 0x0200006E RID: 110
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoInternalNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x06000343 RID: 835 RVA: 0x0000EBD8 File Offset: 0x0000CDD8
		public MonoInternalNoteAttribute(string comment) : base(comment)
		{
		}
	}
}
