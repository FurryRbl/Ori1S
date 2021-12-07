using System;

namespace System
{
	// Token: 0x0200018A RID: 394
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoInternalNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x06001459 RID: 5209 RVA: 0x00051F50 File Offset: 0x00050150
		public MonoInternalNoteAttribute(string comment) : base(comment)
		{
		}
	}
}
