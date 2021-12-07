using System;

namespace System.Xml
{
	/// <summary>Specifies how to handle line breaks.</summary>
	// Token: 0x020000E1 RID: 225
	public enum NewLineHandling
	{
		/// <summary>New line characters are replaced to match the character specified in the <see cref="P:System.Xml.XmlWriterSettings.NewLineChars" />  property.</summary>
		// Token: 0x04000483 RID: 1155
		Replace,
		/// <summary>New line characters are entitized. This setting preserves all characters when the output is read by a normalizing <see cref="T:System.Xml.XmlReader" />.</summary>
		// Token: 0x04000484 RID: 1156
		Entitize,
		/// <summary>The new line characters are unchanged. The output is the same as the input.</summary>
		// Token: 0x04000485 RID: 1157
		None
	}
}
