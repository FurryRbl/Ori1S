﻿using System;

namespace System
{
	// Token: 0x02000008 RID: 8
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoInternalNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002154 File Offset: 0x00000354
		public MonoInternalNoteAttribute(string comment) : base(comment)
		{
		}
	}
}
