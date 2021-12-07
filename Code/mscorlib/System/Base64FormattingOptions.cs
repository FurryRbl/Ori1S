using System;

namespace System
{
	/// <summary>Specifies whether relevant <see cref="Overload:System.Convert.ToBase64CharArray" /> and <see cref="Overload:System.Convert.ToBase64String" /> methods insert line breaks in their output. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000108 RID: 264
	[Flags]
	public enum Base64FormattingOptions
	{
		/// <summary>Inserts line breaks after every 76 characters in the string representation.</summary>
		// Token: 0x040003BB RID: 955
		InsertLineBreaks = 1,
		/// <summary>Does not insert line breaks after every 76 characters in the string representation.</summary>
		// Token: 0x040003BC RID: 956
		None = 0
	}
}
