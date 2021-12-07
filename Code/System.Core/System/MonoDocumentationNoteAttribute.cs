using System;

namespace System
{
	// Token: 0x02000009 RID: 9
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoDocumentationNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x00005878 File Offset: 0x00003A78
		public MonoDocumentationNoteAttribute(string comment) : base(comment)
		{
		}
	}
}
