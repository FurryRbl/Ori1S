using System;

namespace System
{
	// Token: 0x02000071 RID: 113
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoDocumentationNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x06000401 RID: 1025 RVA: 0x0000B734 File Offset: 0x00009934
		public MonoDocumentationNoteAttribute(string comment) : base(comment)
		{
		}
	}
}
