using System;

namespace System.Xml
{
	/// <summary>Specifies how white space is handled.</summary>
	// Token: 0x020000E4 RID: 228
	public enum WhitespaceHandling
	{
		/// <summary>Return Whitespace and SignificantWhitespace nodes. This is the default.</summary>
		// Token: 0x04000493 RID: 1171
		All,
		/// <summary>Return SignificantWhitespace nodes only.</summary>
		// Token: 0x04000494 RID: 1172
		Significant,
		/// <summary>Return no Whitespace and no SignificantWhitespace nodes.</summary>
		// Token: 0x04000495 RID: 1173
		None
	}
}
