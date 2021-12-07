using System;

namespace System.ComponentModel
{
	/// <summary>Specifies values that succinctly describe the results of a masked text parsing operation.</summary>
	// Token: 0x02000189 RID: 393
	public enum MaskedTextResultHint
	{
		/// <summary>Operation did not succeed. The specified position is not in the range of the target string; typically it is either less than zero or greater then the length of the target string.</summary>
		// Token: 0x040003D8 RID: 984
		PositionOutOfRange = -55,
		/// <summary>Operation did not succeed. The current position in the formatted string is a literal character. </summary>
		// Token: 0x040003D9 RID: 985
		NonEditPosition,
		/// <summary>Operation did not succeed. There were not enough edit positions available to fulfill the request.</summary>
		// Token: 0x040003DA RID: 986
		UnavailableEditPosition,
		/// <summary>Operation did not succeed. The prompt character is not valid at input, perhaps because the <see cref="P:System.ComponentModel.MaskedTextProvider.AllowPromptAsInput" /> property is set to false. </summary>
		// Token: 0x040003DB RID: 987
		PromptCharNotAllowed,
		/// <summary>Operation did not succeed. The program encountered an  input character that was not valid. For more information about characters that are not valid, see the <see cref="M:System.ComponentModel.MaskedTextProvider.IsValidInputChar(System.Char)" /> method.</summary>
		// Token: 0x040003DC RID: 988
		InvalidInput,
		/// <summary>Operation did not succeed. An input character was encountered that was not a signed digit.</summary>
		// Token: 0x040003DD RID: 989
		SignedDigitExpected = -5,
		/// <summary>Operation did not succeed. An input character was encountered that was not a letter.</summary>
		// Token: 0x040003DE RID: 990
		LetterExpected,
		/// <summary>Operation did not succeed. An input character was encountered that was not a digit.</summary>
		// Token: 0x040003DF RID: 991
		DigitExpected,
		/// <summary>Operation did not succeed.An input character was encountered that was not alphanumeric. .</summary>
		// Token: 0x040003E0 RID: 992
		AlphanumericCharacterExpected,
		/// <summary>Operation did not succeed.An input character was encountered that was not a member of the ASCII character set.</summary>
		// Token: 0x040003E1 RID: 993
		AsciiCharacterExpected,
		/// <summary>Unknown. The result of the operation could not be determined.</summary>
		// Token: 0x040003E2 RID: 994
		Unknown,
		/// <summary>Success. The operation succeeded because a literal, prompt or space character was an escaped character. For more information about escaped characters, see the <see cref="M:System.ComponentModel.MaskedTextProvider.VerifyEscapeChar(System.Char,System.Int32)" /> method.</summary>
		// Token: 0x040003E3 RID: 995
		CharacterEscaped,
		/// <summary>Success. The primary operation was not performed because it was not needed; therefore, no side effect was produced.</summary>
		// Token: 0x040003E4 RID: 996
		NoEffect,
		/// <summary>Success. The primary operation was not performed because it was not needed, but the method produced a side effect. For example, the <see cref="Overload:System.ComponentModel.MaskedTextProvider.RemoveAt" /> method can delete an unassigned edit position, which causes left-shifting of subsequent characters in the formatted string. </summary>
		// Token: 0x040003E5 RID: 997
		SideEffect,
		/// <summary>Success. The primary operation succeeded.</summary>
		// Token: 0x040003E6 RID: 998
		Success
	}
}
