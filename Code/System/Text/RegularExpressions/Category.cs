using System;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000470 RID: 1136
	internal enum Category : ushort
	{
		// Token: 0x0400192E RID: 6446
		None,
		// Token: 0x0400192F RID: 6447
		Any,
		// Token: 0x04001930 RID: 6448
		AnySingleline,
		// Token: 0x04001931 RID: 6449
		Word,
		// Token: 0x04001932 RID: 6450
		Digit,
		// Token: 0x04001933 RID: 6451
		WhiteSpace,
		// Token: 0x04001934 RID: 6452
		EcmaAny,
		// Token: 0x04001935 RID: 6453
		EcmaAnySingleline,
		// Token: 0x04001936 RID: 6454
		EcmaWord,
		// Token: 0x04001937 RID: 6455
		EcmaDigit,
		// Token: 0x04001938 RID: 6456
		EcmaWhiteSpace,
		// Token: 0x04001939 RID: 6457
		UnicodeL,
		// Token: 0x0400193A RID: 6458
		UnicodeM,
		// Token: 0x0400193B RID: 6459
		UnicodeN,
		// Token: 0x0400193C RID: 6460
		UnicodeZ,
		// Token: 0x0400193D RID: 6461
		UnicodeP,
		// Token: 0x0400193E RID: 6462
		UnicodeS,
		// Token: 0x0400193F RID: 6463
		UnicodeC,
		// Token: 0x04001940 RID: 6464
		UnicodeLu,
		// Token: 0x04001941 RID: 6465
		UnicodeLl,
		// Token: 0x04001942 RID: 6466
		UnicodeLt,
		// Token: 0x04001943 RID: 6467
		UnicodeLm,
		// Token: 0x04001944 RID: 6468
		UnicodeLo,
		// Token: 0x04001945 RID: 6469
		UnicodeMn,
		// Token: 0x04001946 RID: 6470
		UnicodeMe,
		// Token: 0x04001947 RID: 6471
		UnicodeMc,
		// Token: 0x04001948 RID: 6472
		UnicodeNd,
		// Token: 0x04001949 RID: 6473
		UnicodeNl,
		// Token: 0x0400194A RID: 6474
		UnicodeNo,
		// Token: 0x0400194B RID: 6475
		UnicodeZs,
		// Token: 0x0400194C RID: 6476
		UnicodeZl,
		// Token: 0x0400194D RID: 6477
		UnicodeZp,
		// Token: 0x0400194E RID: 6478
		UnicodePd,
		// Token: 0x0400194F RID: 6479
		UnicodePs,
		// Token: 0x04001950 RID: 6480
		UnicodePi,
		// Token: 0x04001951 RID: 6481
		UnicodePe,
		// Token: 0x04001952 RID: 6482
		UnicodePf,
		// Token: 0x04001953 RID: 6483
		UnicodePc,
		// Token: 0x04001954 RID: 6484
		UnicodePo,
		// Token: 0x04001955 RID: 6485
		UnicodeSm,
		// Token: 0x04001956 RID: 6486
		UnicodeSc,
		// Token: 0x04001957 RID: 6487
		UnicodeSk,
		// Token: 0x04001958 RID: 6488
		UnicodeSo,
		// Token: 0x04001959 RID: 6489
		UnicodeCc,
		// Token: 0x0400195A RID: 6490
		UnicodeCf,
		// Token: 0x0400195B RID: 6491
		UnicodeCo,
		// Token: 0x0400195C RID: 6492
		UnicodeCs,
		// Token: 0x0400195D RID: 6493
		UnicodeCn,
		// Token: 0x0400195E RID: 6494
		UnicodeBasicLatin,
		// Token: 0x0400195F RID: 6495
		UnicodeLatin1Supplement,
		// Token: 0x04001960 RID: 6496
		UnicodeLatinExtendedA,
		// Token: 0x04001961 RID: 6497
		UnicodeLatinExtendedB,
		// Token: 0x04001962 RID: 6498
		UnicodeIPAExtensions,
		// Token: 0x04001963 RID: 6499
		UnicodeSpacingModifierLetters,
		// Token: 0x04001964 RID: 6500
		UnicodeCombiningDiacriticalMarks,
		// Token: 0x04001965 RID: 6501
		UnicodeGreek,
		// Token: 0x04001966 RID: 6502
		UnicodeCyrillic,
		// Token: 0x04001967 RID: 6503
		UnicodeArmenian,
		// Token: 0x04001968 RID: 6504
		UnicodeHebrew,
		// Token: 0x04001969 RID: 6505
		UnicodeArabic,
		// Token: 0x0400196A RID: 6506
		UnicodeSyriac,
		// Token: 0x0400196B RID: 6507
		UnicodeThaana,
		// Token: 0x0400196C RID: 6508
		UnicodeDevanagari,
		// Token: 0x0400196D RID: 6509
		UnicodeBengali,
		// Token: 0x0400196E RID: 6510
		UnicodeGurmukhi,
		// Token: 0x0400196F RID: 6511
		UnicodeGujarati,
		// Token: 0x04001970 RID: 6512
		UnicodeOriya,
		// Token: 0x04001971 RID: 6513
		UnicodeTamil,
		// Token: 0x04001972 RID: 6514
		UnicodeTelugu,
		// Token: 0x04001973 RID: 6515
		UnicodeKannada,
		// Token: 0x04001974 RID: 6516
		UnicodeMalayalam,
		// Token: 0x04001975 RID: 6517
		UnicodeSinhala,
		// Token: 0x04001976 RID: 6518
		UnicodeThai,
		// Token: 0x04001977 RID: 6519
		UnicodeLao,
		// Token: 0x04001978 RID: 6520
		UnicodeTibetan,
		// Token: 0x04001979 RID: 6521
		UnicodeMyanmar,
		// Token: 0x0400197A RID: 6522
		UnicodeGeorgian,
		// Token: 0x0400197B RID: 6523
		UnicodeHangulJamo,
		// Token: 0x0400197C RID: 6524
		UnicodeEthiopic,
		// Token: 0x0400197D RID: 6525
		UnicodeCherokee,
		// Token: 0x0400197E RID: 6526
		UnicodeUnifiedCanadianAboriginalSyllabics,
		// Token: 0x0400197F RID: 6527
		UnicodeOgham,
		// Token: 0x04001980 RID: 6528
		UnicodeRunic,
		// Token: 0x04001981 RID: 6529
		UnicodeKhmer,
		// Token: 0x04001982 RID: 6530
		UnicodeMongolian,
		// Token: 0x04001983 RID: 6531
		UnicodeLatinExtendedAdditional,
		// Token: 0x04001984 RID: 6532
		UnicodeGreekExtended,
		// Token: 0x04001985 RID: 6533
		UnicodeGeneralPunctuation,
		// Token: 0x04001986 RID: 6534
		UnicodeSuperscriptsandSubscripts,
		// Token: 0x04001987 RID: 6535
		UnicodeCurrencySymbols,
		// Token: 0x04001988 RID: 6536
		UnicodeCombiningMarksforSymbols,
		// Token: 0x04001989 RID: 6537
		UnicodeLetterlikeSymbols,
		// Token: 0x0400198A RID: 6538
		UnicodeNumberForms,
		// Token: 0x0400198B RID: 6539
		UnicodeArrows,
		// Token: 0x0400198C RID: 6540
		UnicodeMathematicalOperators,
		// Token: 0x0400198D RID: 6541
		UnicodeMiscellaneousTechnical,
		// Token: 0x0400198E RID: 6542
		UnicodeControlPictures,
		// Token: 0x0400198F RID: 6543
		UnicodeOpticalCharacterRecognition,
		// Token: 0x04001990 RID: 6544
		UnicodeEnclosedAlphanumerics,
		// Token: 0x04001991 RID: 6545
		UnicodeBoxDrawing,
		// Token: 0x04001992 RID: 6546
		UnicodeBlockElements,
		// Token: 0x04001993 RID: 6547
		UnicodeGeometricShapes,
		// Token: 0x04001994 RID: 6548
		UnicodeMiscellaneousSymbols,
		// Token: 0x04001995 RID: 6549
		UnicodeDingbats,
		// Token: 0x04001996 RID: 6550
		UnicodeBraillePatterns,
		// Token: 0x04001997 RID: 6551
		UnicodeCJKRadicalsSupplement,
		// Token: 0x04001998 RID: 6552
		UnicodeKangxiRadicals,
		// Token: 0x04001999 RID: 6553
		UnicodeIdeographicDescriptionCharacters,
		// Token: 0x0400199A RID: 6554
		UnicodeCJKSymbolsandPunctuation,
		// Token: 0x0400199B RID: 6555
		UnicodeHiragana,
		// Token: 0x0400199C RID: 6556
		UnicodeKatakana,
		// Token: 0x0400199D RID: 6557
		UnicodeBopomofo,
		// Token: 0x0400199E RID: 6558
		UnicodeHangulCompatibilityJamo,
		// Token: 0x0400199F RID: 6559
		UnicodeKanbun,
		// Token: 0x040019A0 RID: 6560
		UnicodeBopomofoExtended,
		// Token: 0x040019A1 RID: 6561
		UnicodeEnclosedCJKLettersandMonths,
		// Token: 0x040019A2 RID: 6562
		UnicodeCJKCompatibility,
		// Token: 0x040019A3 RID: 6563
		UnicodeCJKUnifiedIdeographsExtensionA,
		// Token: 0x040019A4 RID: 6564
		UnicodeCJKUnifiedIdeographs,
		// Token: 0x040019A5 RID: 6565
		UnicodeYiSyllables,
		// Token: 0x040019A6 RID: 6566
		UnicodeYiRadicals,
		// Token: 0x040019A7 RID: 6567
		UnicodeHangulSyllables,
		// Token: 0x040019A8 RID: 6568
		UnicodeHighSurrogates,
		// Token: 0x040019A9 RID: 6569
		UnicodeHighPrivateUseSurrogates,
		// Token: 0x040019AA RID: 6570
		UnicodeLowSurrogates,
		// Token: 0x040019AB RID: 6571
		UnicodePrivateUse,
		// Token: 0x040019AC RID: 6572
		UnicodeCJKCompatibilityIdeographs,
		// Token: 0x040019AD RID: 6573
		UnicodeAlphabeticPresentationForms,
		// Token: 0x040019AE RID: 6574
		UnicodeArabicPresentationFormsA,
		// Token: 0x040019AF RID: 6575
		UnicodeCombiningHalfMarks,
		// Token: 0x040019B0 RID: 6576
		UnicodeCJKCompatibilityForms,
		// Token: 0x040019B1 RID: 6577
		UnicodeSmallFormVariants,
		// Token: 0x040019B2 RID: 6578
		UnicodeArabicPresentationFormsB,
		// Token: 0x040019B3 RID: 6579
		UnicodeSpecials,
		// Token: 0x040019B4 RID: 6580
		UnicodeHalfwidthandFullwidthForms,
		// Token: 0x040019B5 RID: 6581
		UnicodeOldItalic,
		// Token: 0x040019B6 RID: 6582
		UnicodeGothic,
		// Token: 0x040019B7 RID: 6583
		UnicodeDeseret,
		// Token: 0x040019B8 RID: 6584
		UnicodeByzantineMusicalSymbols,
		// Token: 0x040019B9 RID: 6585
		UnicodeMusicalSymbols,
		// Token: 0x040019BA RID: 6586
		UnicodeMathematicalAlphanumericSymbols,
		// Token: 0x040019BB RID: 6587
		UnicodeCJKUnifiedIdeographsExtensionB,
		// Token: 0x040019BC RID: 6588
		UnicodeCJKCompatibilityIdeographsSupplement,
		// Token: 0x040019BD RID: 6589
		UnicodeTags,
		// Token: 0x040019BE RID: 6590
		LastValue
	}
}
