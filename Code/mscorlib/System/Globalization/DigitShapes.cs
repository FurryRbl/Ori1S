using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>Specifies the culture-specific display of digits.</summary>
	// Token: 0x02000214 RID: 532
	[ComVisible(true)]
	[Serializable]
	public enum DigitShapes
	{
		/// <summary>The digit shape depends on the previous text in the same output. European digits follow Latin scripts; Arabic-Indic digits follow Arabic text; and Thai digits follow Thai text.</summary>
		// Token: 0x04000A14 RID: 2580
		Context,
		/// <summary>The digit shape is not changed. Full Unicode compatibility is maintained.</summary>
		// Token: 0x04000A15 RID: 2581
		None,
		/// <summary>The digit shape is the native equivalent of the digits from 0 through 9. ASCII digits from 0 through 9 are replaced by equivalent native national digits.</summary>
		// Token: 0x04000A16 RID: 2582
		NativeNational
	}
}
