using System;

namespace System
{
	// Token: 0x02000188 RID: 392
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoDocumentationNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x06001457 RID: 5207 RVA: 0x00051F38 File Offset: 0x00050138
		public MonoDocumentationNoteAttribute(string comment) : base(comment)
		{
		}
	}
}
