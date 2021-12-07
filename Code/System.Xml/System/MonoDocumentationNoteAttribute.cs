using System;

namespace System
{
	// Token: 0x02000016 RID: 22
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoDocumentationNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x06000078 RID: 120 RVA: 0x000069EC File Offset: 0x00004BEC
		public MonoDocumentationNoteAttribute(string comment) : base(comment)
		{
		}
	}
}
