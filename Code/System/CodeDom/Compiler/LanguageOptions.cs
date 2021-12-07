using System;

namespace System.CodeDom.Compiler
{
	/// <summary>Defines identifiers that indicate special features of a language.</summary>
	// Token: 0x0200008E RID: 142
	[Flags]
	[Serializable]
	public enum LanguageOptions
	{
		/// <summary>The language has default characteristics.</summary>
		// Token: 0x04000184 RID: 388
		None = 0,
		/// <summary>The language is case-insensitive.</summary>
		// Token: 0x04000185 RID: 389
		CaseInsensitive = 1
	}
}
