using System;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000495 RID: 1173
	internal enum RxOp : byte
	{
		// Token: 0x04001A59 RID: 6745
		Info,
		// Token: 0x04001A5A RID: 6746
		False,
		// Token: 0x04001A5B RID: 6747
		True,
		// Token: 0x04001A5C RID: 6748
		AnyPosition,
		// Token: 0x04001A5D RID: 6749
		StartOfString,
		// Token: 0x04001A5E RID: 6750
		StartOfLine,
		// Token: 0x04001A5F RID: 6751
		StartOfScan,
		// Token: 0x04001A60 RID: 6752
		EndOfString,
		// Token: 0x04001A61 RID: 6753
		EndOfLine,
		// Token: 0x04001A62 RID: 6754
		End,
		// Token: 0x04001A63 RID: 6755
		WordBoundary,
		// Token: 0x04001A64 RID: 6756
		NoWordBoundary,
		// Token: 0x04001A65 RID: 6757
		String,
		// Token: 0x04001A66 RID: 6758
		StringIgnoreCase,
		// Token: 0x04001A67 RID: 6759
		StringReverse,
		// Token: 0x04001A68 RID: 6760
		StringIgnoreCaseReverse,
		// Token: 0x04001A69 RID: 6761
		UnicodeString,
		// Token: 0x04001A6A RID: 6762
		UnicodeStringIgnoreCase,
		// Token: 0x04001A6B RID: 6763
		UnicodeStringReverse,
		// Token: 0x04001A6C RID: 6764
		UnicodeStringIgnoreCaseReverse,
		// Token: 0x04001A6D RID: 6765
		Char,
		// Token: 0x04001A6E RID: 6766
		NoChar,
		// Token: 0x04001A6F RID: 6767
		CharIgnoreCase,
		// Token: 0x04001A70 RID: 6768
		NoCharIgnoreCase,
		// Token: 0x04001A71 RID: 6769
		CharReverse,
		// Token: 0x04001A72 RID: 6770
		NoCharReverse,
		// Token: 0x04001A73 RID: 6771
		CharIgnoreCaseReverse,
		// Token: 0x04001A74 RID: 6772
		NoCharIgnoreCaseReverse,
		// Token: 0x04001A75 RID: 6773
		Range,
		// Token: 0x04001A76 RID: 6774
		NoRange,
		// Token: 0x04001A77 RID: 6775
		RangeIgnoreCase,
		// Token: 0x04001A78 RID: 6776
		NoRangeIgnoreCase,
		// Token: 0x04001A79 RID: 6777
		RangeReverse,
		// Token: 0x04001A7A RID: 6778
		NoRangeReverse,
		// Token: 0x04001A7B RID: 6779
		RangeIgnoreCaseReverse,
		// Token: 0x04001A7C RID: 6780
		NoRangeIgnoreCaseReverse,
		// Token: 0x04001A7D RID: 6781
		Bitmap,
		// Token: 0x04001A7E RID: 6782
		NoBitmap,
		// Token: 0x04001A7F RID: 6783
		BitmapIgnoreCase,
		// Token: 0x04001A80 RID: 6784
		NoBitmapIgnoreCase,
		// Token: 0x04001A81 RID: 6785
		BitmapReverse,
		// Token: 0x04001A82 RID: 6786
		NoBitmapReverse,
		// Token: 0x04001A83 RID: 6787
		BitmapIgnoreCaseReverse,
		// Token: 0x04001A84 RID: 6788
		NoBitmapIgnoreCaseReverse,
		// Token: 0x04001A85 RID: 6789
		UnicodeChar,
		// Token: 0x04001A86 RID: 6790
		NoUnicodeChar,
		// Token: 0x04001A87 RID: 6791
		UnicodeCharIgnoreCase,
		// Token: 0x04001A88 RID: 6792
		NoUnicodeCharIgnoreCase,
		// Token: 0x04001A89 RID: 6793
		UnicodeCharReverse,
		// Token: 0x04001A8A RID: 6794
		NoUnicodeCharReverse,
		// Token: 0x04001A8B RID: 6795
		UnicodeCharIgnoreCaseReverse,
		// Token: 0x04001A8C RID: 6796
		NoUnicodeCharIgnoreCaseReverse,
		// Token: 0x04001A8D RID: 6797
		UnicodeRange,
		// Token: 0x04001A8E RID: 6798
		NoUnicodeRange,
		// Token: 0x04001A8F RID: 6799
		UnicodeRangeIgnoreCase,
		// Token: 0x04001A90 RID: 6800
		NoUnicodeRangeIgnoreCase,
		// Token: 0x04001A91 RID: 6801
		UnicodeRangeReverse,
		// Token: 0x04001A92 RID: 6802
		NoUnicodeRangeReverse,
		// Token: 0x04001A93 RID: 6803
		UnicodeRangeIgnoreCaseReverse,
		// Token: 0x04001A94 RID: 6804
		NoUnicodeRangeIgnoreCaseReverse,
		// Token: 0x04001A95 RID: 6805
		UnicodeBitmap,
		// Token: 0x04001A96 RID: 6806
		NoUnicodeBitmap,
		// Token: 0x04001A97 RID: 6807
		UnicodeBitmapIgnoreCase,
		// Token: 0x04001A98 RID: 6808
		NoUnicodeBitmapIgnoreCase,
		// Token: 0x04001A99 RID: 6809
		UnicodeBitmapReverse,
		// Token: 0x04001A9A RID: 6810
		NoUnicodeBitmapReverse,
		// Token: 0x04001A9B RID: 6811
		UnicodeBitmapIgnoreCaseReverse,
		// Token: 0x04001A9C RID: 6812
		NoUnicodeBitmapIgnoreCaseReverse,
		// Token: 0x04001A9D RID: 6813
		CategoryAny,
		// Token: 0x04001A9E RID: 6814
		NoCategoryAny,
		// Token: 0x04001A9F RID: 6815
		CategoryAnyReverse,
		// Token: 0x04001AA0 RID: 6816
		NoCategoryAnyReverse,
		// Token: 0x04001AA1 RID: 6817
		CategoryAnySingleline,
		// Token: 0x04001AA2 RID: 6818
		NoCategoryAnySingleline,
		// Token: 0x04001AA3 RID: 6819
		CategoryAnySinglelineReverse,
		// Token: 0x04001AA4 RID: 6820
		NoCategoryAnySinglelineReverse,
		// Token: 0x04001AA5 RID: 6821
		CategoryDigit,
		// Token: 0x04001AA6 RID: 6822
		NoCategoryDigit,
		// Token: 0x04001AA7 RID: 6823
		CategoryDigitReverse,
		// Token: 0x04001AA8 RID: 6824
		NoCategoryDigitReverse,
		// Token: 0x04001AA9 RID: 6825
		CategoryWord,
		// Token: 0x04001AAA RID: 6826
		NoCategoryWord,
		// Token: 0x04001AAB RID: 6827
		CategoryWordReverse,
		// Token: 0x04001AAC RID: 6828
		NoCategoryWordReverse,
		// Token: 0x04001AAD RID: 6829
		CategoryWhiteSpace,
		// Token: 0x04001AAE RID: 6830
		NoCategoryWhiteSpace,
		// Token: 0x04001AAF RID: 6831
		CategoryWhiteSpaceReverse,
		// Token: 0x04001AB0 RID: 6832
		NoCategoryWhiteSpaceReverse,
		// Token: 0x04001AB1 RID: 6833
		CategoryEcmaWord,
		// Token: 0x04001AB2 RID: 6834
		NoCategoryEcmaWord,
		// Token: 0x04001AB3 RID: 6835
		CategoryEcmaWordReverse,
		// Token: 0x04001AB4 RID: 6836
		NoCategoryEcmaWordReverse,
		// Token: 0x04001AB5 RID: 6837
		CategoryEcmaWhiteSpace,
		// Token: 0x04001AB6 RID: 6838
		NoCategoryEcmaWhiteSpace,
		// Token: 0x04001AB7 RID: 6839
		CategoryEcmaWhiteSpaceReverse,
		// Token: 0x04001AB8 RID: 6840
		NoCategoryEcmaWhiteSpaceReverse,
		// Token: 0x04001AB9 RID: 6841
		CategoryUnicode,
		// Token: 0x04001ABA RID: 6842
		NoCategoryUnicode,
		// Token: 0x04001ABB RID: 6843
		CategoryUnicodeReverse,
		// Token: 0x04001ABC RID: 6844
		NoCategoryUnicodeReverse,
		// Token: 0x04001ABD RID: 6845
		CategoryUnicodeLetter,
		// Token: 0x04001ABE RID: 6846
		NoCategoryUnicodeLetter,
		// Token: 0x04001ABF RID: 6847
		CategoryUnicodeLetterReverse,
		// Token: 0x04001AC0 RID: 6848
		NoCategoryUnicodeLetterReverse,
		// Token: 0x04001AC1 RID: 6849
		CategoryUnicodeMark,
		// Token: 0x04001AC2 RID: 6850
		NoCategoryUnicodeMark,
		// Token: 0x04001AC3 RID: 6851
		CategoryUnicodeMarkReverse,
		// Token: 0x04001AC4 RID: 6852
		NoCategoryUnicodeMarkReverse,
		// Token: 0x04001AC5 RID: 6853
		CategoryUnicodeNumber,
		// Token: 0x04001AC6 RID: 6854
		NoCategoryUnicodeNumber,
		// Token: 0x04001AC7 RID: 6855
		CategoryUnicodeNumberReverse,
		// Token: 0x04001AC8 RID: 6856
		NoCategoryUnicodeNumberReverse,
		// Token: 0x04001AC9 RID: 6857
		CategoryUnicodeSeparator,
		// Token: 0x04001ACA RID: 6858
		NoCategoryUnicodeSeparator,
		// Token: 0x04001ACB RID: 6859
		CategoryUnicodeSeparatorReverse,
		// Token: 0x04001ACC RID: 6860
		NoCategoryUnicodeSeparatorReverse,
		// Token: 0x04001ACD RID: 6861
		CategoryUnicodePunctuation,
		// Token: 0x04001ACE RID: 6862
		NoCategoryUnicodePunctuation,
		// Token: 0x04001ACF RID: 6863
		CategoryUnicodePunctuationReverse,
		// Token: 0x04001AD0 RID: 6864
		NoCategoryUnicodePunctuationReverse,
		// Token: 0x04001AD1 RID: 6865
		CategoryUnicodeSymbol,
		// Token: 0x04001AD2 RID: 6866
		NoCategoryUnicodeSymbol,
		// Token: 0x04001AD3 RID: 6867
		CategoryUnicodeSymbolReverse,
		// Token: 0x04001AD4 RID: 6868
		NoCategoryUnicodeSymbolReverse,
		// Token: 0x04001AD5 RID: 6869
		CategoryUnicodeSpecials,
		// Token: 0x04001AD6 RID: 6870
		NoCategoryUnicodeSpecials,
		// Token: 0x04001AD7 RID: 6871
		CategoryUnicodeSpecialsReverse,
		// Token: 0x04001AD8 RID: 6872
		NoCategoryUnicodeSpecialsReverse,
		// Token: 0x04001AD9 RID: 6873
		CategoryUnicodeOther,
		// Token: 0x04001ADA RID: 6874
		NoCategoryUnicodeOther,
		// Token: 0x04001ADB RID: 6875
		CategoryUnicodeOtherReverse,
		// Token: 0x04001ADC RID: 6876
		NoCategoryUnicodeOtherReverse,
		// Token: 0x04001ADD RID: 6877
		CategoryGeneral,
		// Token: 0x04001ADE RID: 6878
		NoCategoryGeneral,
		// Token: 0x04001ADF RID: 6879
		CategoryGeneralReverse,
		// Token: 0x04001AE0 RID: 6880
		NoCategoryGeneralReverse,
		// Token: 0x04001AE1 RID: 6881
		Reference,
		// Token: 0x04001AE2 RID: 6882
		ReferenceIgnoreCase,
		// Token: 0x04001AE3 RID: 6883
		ReferenceReverse,
		// Token: 0x04001AE4 RID: 6884
		ReferenceIgnoreCaseReverse,
		// Token: 0x04001AE5 RID: 6885
		OpenGroup,
		// Token: 0x04001AE6 RID: 6886
		CloseGroup,
		// Token: 0x04001AE7 RID: 6887
		BalanceStart,
		// Token: 0x04001AE8 RID: 6888
		Balance,
		// Token: 0x04001AE9 RID: 6889
		IfDefined,
		// Token: 0x04001AEA RID: 6890
		Jump,
		// Token: 0x04001AEB RID: 6891
		SubExpression,
		// Token: 0x04001AEC RID: 6892
		Test,
		// Token: 0x04001AED RID: 6893
		Branch,
		// Token: 0x04001AEE RID: 6894
		TestCharGroup,
		// Token: 0x04001AEF RID: 6895
		Anchor,
		// Token: 0x04001AF0 RID: 6896
		AnchorReverse,
		// Token: 0x04001AF1 RID: 6897
		Repeat,
		// Token: 0x04001AF2 RID: 6898
		RepeatLazy,
		// Token: 0x04001AF3 RID: 6899
		Until,
		// Token: 0x04001AF4 RID: 6900
		FastRepeat,
		// Token: 0x04001AF5 RID: 6901
		FastRepeatLazy,
		// Token: 0x04001AF6 RID: 6902
		RepeatInfinite,
		// Token: 0x04001AF7 RID: 6903
		RepeatInfiniteLazy
	}
}
