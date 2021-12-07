using System;

namespace System
{
	// Token: 0x0200006C RID: 108
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoDocumentationNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x06000341 RID: 833 RVA: 0x0000EBC0 File Offset: 0x0000CDC0
		public MonoDocumentationNoteAttribute(string comment) : base(comment)
		{
		}
	}
}
