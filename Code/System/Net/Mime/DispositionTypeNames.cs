﻿using System;

namespace System.Net.Mime
{
	/// <summary>Supplies the strings used to specify the disposition type for an e-mail attachment.</summary>
	// Token: 0x02000350 RID: 848
	public static class DispositionTypeNames
	{
		/// <summary>Specifies that the attachment is to be displayed as a file attached to the e-mail message.</summary>
		// Token: 0x040012B5 RID: 4789
		public const string Attachment = "attachment";

		/// <summary>Specifies that the attachment is to be displayed as part of the e-mail message body.</summary>
		// Token: 0x040012B6 RID: 4790
		public const string Inline = "inline";
	}
}
