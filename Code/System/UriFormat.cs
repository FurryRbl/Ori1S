using System;

namespace System
{
	/// <summary>Controls how URI information is escaped.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020004B4 RID: 1204
	public enum UriFormat
	{
		/// <summary>Escaping is performed according to the rules in RFC 2396.</summary>
		// Token: 0x04001B6A RID: 7018
		UriEscaped = 1,
		/// <summary>No escaping is performed.</summary>
		// Token: 0x04001B6B RID: 7019
		Unescaped,
		/// <summary>Characters that have a reserved meaning in the requested URI components remain escaped. All others are not escaped. See Remarks.</summary>
		// Token: 0x04001B6C RID: 7020
		SafeUnescaped
	}
}
