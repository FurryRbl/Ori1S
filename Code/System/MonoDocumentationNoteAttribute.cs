using System;

namespace System
{
	// Token: 0x02000006 RID: 6
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoDocumentationNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x06000009 RID: 9 RVA: 0x0000213C File Offset: 0x0000033C
		public MonoDocumentationNoteAttribute(string comment) : base(comment)
		{
		}
	}
}
