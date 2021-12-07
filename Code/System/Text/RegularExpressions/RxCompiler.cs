using System;
using System.Collections;
using System.Globalization;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000493 RID: 1171
	internal class RxCompiler : ICompiler
	{
		// Token: 0x06002A1B RID: 10779 RVA: 0x0009040C File Offset: 0x0008E60C
		private void MakeRoom(int bytes)
		{
			while (this.curpos + bytes > this.program.Length)
			{
				int num = this.program.Length * 2;
				byte[] dst = new byte[num];
				Buffer.BlockCopy(this.program, 0, dst, 0, this.program.Length);
				this.program = dst;
			}
		}

		// Token: 0x06002A1C RID: 10780 RVA: 0x00090464 File Offset: 0x0008E664
		private void Emit(byte val)
		{
			this.MakeRoom(1);
			this.program[this.curpos] = val;
			this.curpos++;
		}

		// Token: 0x06002A1D RID: 10781 RVA: 0x0009048C File Offset: 0x0008E68C
		private void Emit(RxOp opcode)
		{
			this.Emit((byte)opcode);
		}

		// Token: 0x06002A1E RID: 10782 RVA: 0x00090498 File Offset: 0x0008E698
		private void Emit(ushort val)
		{
			this.MakeRoom(2);
			this.program[this.curpos] = (byte)val;
			this.program[this.curpos + 1] = (byte)(val >> 8);
			this.curpos += 2;
		}

		// Token: 0x06002A1F RID: 10783 RVA: 0x000904D4 File Offset: 0x0008E6D4
		private void Emit(int val)
		{
			this.MakeRoom(4);
			this.program[this.curpos] = (byte)val;
			this.program[this.curpos + 1] = (byte)(val >> 8);
			this.program[this.curpos + 2] = (byte)(val >> 16);
			this.program[this.curpos + 3] = (byte)(val >> 24);
			this.curpos += 4;
		}

		// Token: 0x06002A20 RID: 10784 RVA: 0x00090540 File Offset: 0x0008E740
		private void BeginLink(LinkRef lref)
		{
			RxLinkRef rxLinkRef = lref as RxLinkRef;
			rxLinkRef.PushInstructionBase(this.curpos);
		}

		// Token: 0x06002A21 RID: 10785 RVA: 0x00090560 File Offset: 0x0008E760
		private void EmitLink(LinkRef lref)
		{
			RxLinkRef rxLinkRef = lref as RxLinkRef;
			rxLinkRef.PushOffsetPosition(this.curpos);
			this.Emit(0);
		}

		// Token: 0x06002A22 RID: 10786 RVA: 0x00090588 File Offset: 0x0008E788
		public void Reset()
		{
			this.curpos = 0;
		}

		// Token: 0x06002A23 RID: 10787 RVA: 0x00090594 File Offset: 0x0008E794
		public IMachineFactory GetMachineFactory()
		{
			byte[] dst = new byte[this.curpos];
			Buffer.BlockCopy(this.program, 0, dst, 0, this.curpos);
			return new RxInterpreterFactory(dst, null);
		}

		// Token: 0x06002A24 RID: 10788 RVA: 0x000905C8 File Offset: 0x0008E7C8
		public void EmitFalse()
		{
			this.Emit(RxOp.False);
		}

		// Token: 0x06002A25 RID: 10789 RVA: 0x000905D4 File Offset: 0x0008E7D4
		public void EmitTrue()
		{
			this.Emit(RxOp.True);
		}

		// Token: 0x06002A26 RID: 10790 RVA: 0x000905E0 File Offset: 0x0008E7E0
		public virtual void EmitOp(RxOp op, bool negate, bool ignore, bool reverse)
		{
			int num = 0;
			if (negate)
			{
				num++;
			}
			if (ignore)
			{
				num += 2;
			}
			if (reverse)
			{
				num += 4;
			}
			this.Emit(op + (byte)num);
		}

		// Token: 0x06002A27 RID: 10791 RVA: 0x00090618 File Offset: 0x0008E818
		public virtual void EmitOpIgnoreReverse(RxOp op, bool ignore, bool reverse)
		{
			int num = 0;
			if (ignore)
			{
				num++;
			}
			if (reverse)
			{
				num += 2;
			}
			this.Emit(op + (byte)num);
		}

		// Token: 0x06002A28 RID: 10792 RVA: 0x00090648 File Offset: 0x0008E848
		public virtual void EmitOpNegateReverse(RxOp op, bool negate, bool reverse)
		{
			int num = 0;
			if (negate)
			{
				num++;
			}
			if (reverse)
			{
				num += 2;
			}
			this.Emit(op + (byte)num);
		}

		// Token: 0x06002A29 RID: 10793 RVA: 0x00090678 File Offset: 0x0008E878
		public void EmitCharacter(char c, bool negate, bool ignore, bool reverse)
		{
			if (ignore)
			{
				c = char.ToLower(c);
			}
			if (c < 'Ā')
			{
				this.EmitOp(RxOp.Char, negate, ignore, reverse);
				this.Emit((byte)c);
			}
			else
			{
				this.EmitOp(RxOp.UnicodeChar, negate, ignore, reverse);
				this.Emit((ushort)c);
			}
		}

		// Token: 0x06002A2A RID: 10794 RVA: 0x000906CC File Offset: 0x0008E8CC
		private void EmitUniCat(UnicodeCategory cat, bool negate, bool reverse)
		{
			this.EmitOpNegateReverse(RxOp.CategoryUnicode, negate, reverse);
			this.Emit((byte)cat);
		}

		// Token: 0x06002A2B RID: 10795 RVA: 0x000906E0 File Offset: 0x0008E8E0
		private void EmitCatGeneral(Category cat, bool negate, bool reverse)
		{
			this.EmitOpNegateReverse(RxOp.CategoryGeneral, negate, reverse);
			this.Emit((byte)cat);
		}

		// Token: 0x06002A2C RID: 10796 RVA: 0x000906F8 File Offset: 0x0008E8F8
		public void EmitCategory(Category cat, bool negate, bool reverse)
		{
			switch (cat)
			{
			case Category.Any:
			case Category.EcmaAny:
				this.EmitOpNegateReverse(RxOp.CategoryAny, negate, reverse);
				return;
			case Category.AnySingleline:
				this.EmitOpNegateReverse(RxOp.CategoryAnySingleline, negate, reverse);
				return;
			case Category.Word:
				this.EmitOpNegateReverse(RxOp.CategoryWord, negate, reverse);
				return;
			case Category.Digit:
				this.EmitOpNegateReverse(RxOp.CategoryDigit, negate, reverse);
				return;
			case Category.WhiteSpace:
				this.EmitOpNegateReverse(RxOp.CategoryWhiteSpace, negate, reverse);
				return;
			case Category.EcmaWord:
				this.EmitOpNegateReverse(RxOp.CategoryEcmaWord, negate, reverse);
				return;
			case Category.EcmaDigit:
				this.EmitRange('0', '9', negate, false, reverse);
				return;
			case Category.EcmaWhiteSpace:
				this.EmitOpNegateReverse(RxOp.CategoryEcmaWhiteSpace, negate, reverse);
				return;
			case Category.UnicodeL:
			case Category.UnicodeM:
			case Category.UnicodeN:
			case Category.UnicodeZ:
			case Category.UnicodeP:
			case Category.UnicodeS:
			case Category.UnicodeC:
				this.EmitCatGeneral(cat, negate, reverse);
				return;
			case Category.UnicodeLu:
				this.EmitUniCat(UnicodeCategory.UppercaseLetter, negate, reverse);
				return;
			case Category.UnicodeLl:
				this.EmitUniCat(UnicodeCategory.LowercaseLetter, negate, reverse);
				return;
			case Category.UnicodeLt:
				this.EmitUniCat(UnicodeCategory.TitlecaseLetter, negate, reverse);
				return;
			case Category.UnicodeLm:
				this.EmitUniCat(UnicodeCategory.ModifierLetter, negate, reverse);
				return;
			case Category.UnicodeLo:
				this.EmitUniCat(UnicodeCategory.OtherLetter, negate, reverse);
				return;
			case Category.UnicodeMn:
				this.EmitUniCat(UnicodeCategory.NonSpacingMark, negate, reverse);
				return;
			case Category.UnicodeMe:
				this.EmitUniCat(UnicodeCategory.EnclosingMark, negate, reverse);
				return;
			case Category.UnicodeMc:
				this.EmitUniCat(UnicodeCategory.SpacingCombiningMark, negate, reverse);
				return;
			case Category.UnicodeNd:
				this.EmitUniCat(UnicodeCategory.DecimalDigitNumber, negate, reverse);
				return;
			case Category.UnicodeNl:
				this.EmitUniCat(UnicodeCategory.LetterNumber, negate, reverse);
				return;
			case Category.UnicodeNo:
				this.EmitUniCat(UnicodeCategory.OtherNumber, negate, reverse);
				return;
			case Category.UnicodeZs:
				this.EmitUniCat(UnicodeCategory.SpaceSeparator, negate, reverse);
				return;
			case Category.UnicodeZl:
				this.EmitUniCat(UnicodeCategory.LineSeparator, negate, reverse);
				return;
			case Category.UnicodeZp:
				this.EmitUniCat(UnicodeCategory.ParagraphSeparator, negate, reverse);
				return;
			case Category.UnicodePd:
				this.EmitUniCat(UnicodeCategory.DashPunctuation, negate, reverse);
				return;
			case Category.UnicodePs:
				this.EmitUniCat(UnicodeCategory.OpenPunctuation, negate, reverse);
				return;
			case Category.UnicodePi:
				this.EmitUniCat(UnicodeCategory.InitialQuotePunctuation, negate, reverse);
				return;
			case Category.UnicodePe:
				this.EmitUniCat(UnicodeCategory.ClosePunctuation, negate, reverse);
				return;
			case Category.UnicodePf:
				this.EmitUniCat(UnicodeCategory.FinalQuotePunctuation, negate, reverse);
				return;
			case Category.UnicodePc:
				this.EmitUniCat(UnicodeCategory.ConnectorPunctuation, negate, reverse);
				return;
			case Category.UnicodePo:
				this.EmitUniCat(UnicodeCategory.OtherPunctuation, negate, reverse);
				return;
			case Category.UnicodeSm:
				this.EmitUniCat(UnicodeCategory.MathSymbol, negate, reverse);
				return;
			case Category.UnicodeSc:
				this.EmitUniCat(UnicodeCategory.CurrencySymbol, negate, reverse);
				return;
			case Category.UnicodeSk:
				this.EmitUniCat(UnicodeCategory.ModifierSymbol, negate, reverse);
				return;
			case Category.UnicodeSo:
				this.EmitUniCat(UnicodeCategory.OtherSymbol, negate, reverse);
				return;
			case Category.UnicodeCc:
				this.EmitUniCat(UnicodeCategory.Control, negate, reverse);
				return;
			case Category.UnicodeCf:
				this.EmitUniCat(UnicodeCategory.Format, negate, reverse);
				return;
			case Category.UnicodeCo:
				this.EmitUniCat(UnicodeCategory.PrivateUse, negate, reverse);
				return;
			case Category.UnicodeCs:
				this.EmitUniCat(UnicodeCategory.Surrogate, negate, reverse);
				return;
			case Category.UnicodeCn:
				this.EmitUniCat(UnicodeCategory.OtherNotAssigned, negate, reverse);
				return;
			case Category.UnicodeBasicLatin:
				this.EmitRange('\0', '\u007f', negate, false, reverse);
				return;
			case Category.UnicodeLatin1Supplement:
				this.EmitRange('\u0080', 'ÿ', negate, false, reverse);
				return;
			case Category.UnicodeLatinExtendedA:
				this.EmitRange('Ā', 'ſ', negate, false, reverse);
				return;
			case Category.UnicodeLatinExtendedB:
				this.EmitRange('ƀ', 'ɏ', negate, false, reverse);
				return;
			case Category.UnicodeIPAExtensions:
				this.EmitRange('ɐ', 'ʯ', negate, false, reverse);
				return;
			case Category.UnicodeSpacingModifierLetters:
				this.EmitRange('ʰ', '˿', negate, false, reverse);
				return;
			case Category.UnicodeCombiningDiacriticalMarks:
				this.EmitRange('̀', 'ͯ', negate, false, reverse);
				return;
			case Category.UnicodeGreek:
				this.EmitRange('Ͱ', 'Ͽ', negate, false, reverse);
				return;
			case Category.UnicodeCyrillic:
				this.EmitRange('Ѐ', 'ӿ', negate, false, reverse);
				return;
			case Category.UnicodeArmenian:
				this.EmitRange('԰', '֏', negate, false, reverse);
				return;
			case Category.UnicodeHebrew:
				this.EmitRange('֐', '׿', negate, false, reverse);
				return;
			case Category.UnicodeArabic:
				this.EmitRange('؀', 'ۿ', negate, false, reverse);
				return;
			case Category.UnicodeSyriac:
				this.EmitRange('܀', 'ݏ', negate, false, reverse);
				return;
			case Category.UnicodeThaana:
				this.EmitRange('ހ', '޿', negate, false, reverse);
				return;
			case Category.UnicodeDevanagari:
				this.EmitRange('ऀ', 'ॿ', negate, false, reverse);
				return;
			case Category.UnicodeBengali:
				this.EmitRange('ঀ', '৿', negate, false, reverse);
				return;
			case Category.UnicodeGurmukhi:
				this.EmitRange('਀', '੿', negate, false, reverse);
				return;
			case Category.UnicodeGujarati:
				this.EmitRange('઀', '૿', negate, false, reverse);
				return;
			case Category.UnicodeOriya:
				this.EmitRange('଀', '୿', negate, false, reverse);
				return;
			case Category.UnicodeTamil:
				this.EmitRange('஀', '௿', negate, false, reverse);
				return;
			case Category.UnicodeTelugu:
				this.EmitRange('ఀ', '౿', negate, false, reverse);
				return;
			case Category.UnicodeKannada:
				this.EmitRange('ಀ', '೿', negate, false, reverse);
				return;
			case Category.UnicodeMalayalam:
				this.EmitRange('ഀ', 'ൿ', negate, false, reverse);
				return;
			case Category.UnicodeSinhala:
				this.EmitRange('඀', '෿', negate, false, reverse);
				return;
			case Category.UnicodeThai:
				this.EmitRange('฀', '๿', negate, false, reverse);
				return;
			case Category.UnicodeLao:
				this.EmitRange('຀', '໿', negate, false, reverse);
				return;
			case Category.UnicodeTibetan:
				this.EmitRange('ༀ', '࿿', negate, false, reverse);
				return;
			case Category.UnicodeMyanmar:
				this.EmitRange('က', '႟', negate, false, reverse);
				return;
			case Category.UnicodeGeorgian:
				this.EmitRange('Ⴀ', 'ჿ', negate, false, reverse);
				return;
			case Category.UnicodeHangulJamo:
				this.EmitRange('ᄀ', 'ᇿ', negate, false, reverse);
				return;
			case Category.UnicodeEthiopic:
				this.EmitRange('ሀ', '፿', negate, false, reverse);
				return;
			case Category.UnicodeCherokee:
				this.EmitRange('Ꭰ', '᏿', negate, false, reverse);
				return;
			case Category.UnicodeUnifiedCanadianAboriginalSyllabics:
				this.EmitRange('᐀', 'ᙿ', negate, false, reverse);
				return;
			case Category.UnicodeOgham:
				this.EmitRange('\u1680', '᚟', negate, false, reverse);
				return;
			case Category.UnicodeRunic:
				this.EmitRange('ᚠ', '᛿', negate, false, reverse);
				return;
			case Category.UnicodeKhmer:
				this.EmitRange('ក', '៿', negate, false, reverse);
				return;
			case Category.UnicodeMongolian:
				this.EmitRange('᠀', '᢯', negate, false, reverse);
				return;
			case Category.UnicodeLatinExtendedAdditional:
				this.EmitRange('Ḁ', 'ỿ', negate, false, reverse);
				return;
			case Category.UnicodeGreekExtended:
				this.EmitRange('ἀ', '῿', negate, false, reverse);
				return;
			case Category.UnicodeGeneralPunctuation:
				this.EmitRange('\u2000', '⁯', negate, false, reverse);
				return;
			case Category.UnicodeSuperscriptsandSubscripts:
				this.EmitRange('⁰', '₟', negate, false, reverse);
				return;
			case Category.UnicodeCurrencySymbols:
				this.EmitRange('₠', '⃏', negate, false, reverse);
				return;
			case Category.UnicodeCombiningMarksforSymbols:
				this.EmitRange('⃐', '⃿', negate, false, reverse);
				return;
			case Category.UnicodeLetterlikeSymbols:
				this.EmitRange('℀', '⅏', negate, false, reverse);
				return;
			case Category.UnicodeNumberForms:
				this.EmitRange('⅐', '↏', negate, false, reverse);
				return;
			case Category.UnicodeArrows:
				this.EmitRange('←', '⇿', negate, false, reverse);
				return;
			case Category.UnicodeMathematicalOperators:
				this.EmitRange('∀', '⋿', negate, false, reverse);
				return;
			case Category.UnicodeMiscellaneousTechnical:
				this.EmitRange('⌀', '⏿', negate, false, reverse);
				return;
			case Category.UnicodeControlPictures:
				this.EmitRange('␀', '␿', negate, false, reverse);
				return;
			case Category.UnicodeOpticalCharacterRecognition:
				this.EmitRange('⑀', '⑟', negate, false, reverse);
				return;
			case Category.UnicodeEnclosedAlphanumerics:
				this.EmitRange('①', '⓿', negate, false, reverse);
				return;
			case Category.UnicodeBoxDrawing:
				this.EmitRange('─', '╿', negate, false, reverse);
				return;
			case Category.UnicodeBlockElements:
				this.EmitRange('▀', '▟', negate, false, reverse);
				return;
			case Category.UnicodeGeometricShapes:
				this.EmitRange('■', '◿', negate, false, reverse);
				return;
			case Category.UnicodeMiscellaneousSymbols:
				this.EmitRange('☀', '⛿', negate, false, reverse);
				return;
			case Category.UnicodeDingbats:
				this.EmitRange('✀', '➿', negate, false, reverse);
				return;
			case Category.UnicodeBraillePatterns:
				this.EmitRange('⠀', '⣿', negate, false, reverse);
				return;
			case Category.UnicodeCJKRadicalsSupplement:
				this.EmitRange('⺀', '⻿', negate, false, reverse);
				return;
			case Category.UnicodeKangxiRadicals:
				this.EmitRange('⼀', '⿟', negate, false, reverse);
				return;
			case Category.UnicodeIdeographicDescriptionCharacters:
				this.EmitRange('⿰', '⿿', negate, false, reverse);
				return;
			case Category.UnicodeCJKSymbolsandPunctuation:
				this.EmitRange('\u3000', '〿', negate, false, reverse);
				return;
			case Category.UnicodeHiragana:
				this.EmitRange('぀', 'ゟ', negate, false, reverse);
				return;
			case Category.UnicodeKatakana:
				this.EmitRange('゠', 'ヿ', negate, false, reverse);
				return;
			case Category.UnicodeBopomofo:
				this.EmitRange('㄀', 'ㄯ', negate, false, reverse);
				return;
			case Category.UnicodeHangulCompatibilityJamo:
				this.EmitRange('㄰', '㆏', negate, false, reverse);
				return;
			case Category.UnicodeKanbun:
				this.EmitRange('㆐', '㆟', negate, false, reverse);
				return;
			case Category.UnicodeBopomofoExtended:
				this.EmitRange('ㆠ', 'ㆿ', negate, false, reverse);
				return;
			case Category.UnicodeEnclosedCJKLettersandMonths:
				this.EmitRange('㈀', '㋿', negate, false, reverse);
				return;
			case Category.UnicodeCJKCompatibility:
				this.EmitRange('㌀', '㏿', negate, false, reverse);
				return;
			case Category.UnicodeCJKUnifiedIdeographsExtensionA:
				this.EmitRange('㐀', '䶵', negate, false, reverse);
				return;
			case Category.UnicodeCJKUnifiedIdeographs:
				this.EmitRange('一', '鿿', negate, false, reverse);
				return;
			case Category.UnicodeYiSyllables:
				this.EmitRange('ꀀ', '꒏', negate, false, reverse);
				return;
			case Category.UnicodeYiRadicals:
				this.EmitRange('꒐', '꓏', negate, false, reverse);
				return;
			case Category.UnicodeHangulSyllables:
				this.EmitRange('가', '힣', negate, false, reverse);
				return;
			case Category.UnicodeHighSurrogates:
				this.EmitRange('\ud800', '\udb7f', negate, false, reverse);
				return;
			case Category.UnicodeHighPrivateUseSurrogates:
				this.EmitRange('\udb80', '\udbff', negate, false, reverse);
				return;
			case Category.UnicodeLowSurrogates:
				this.EmitRange('\udc00', '\udfff', negate, false, reverse);
				return;
			case Category.UnicodePrivateUse:
				this.EmitRange('', '', negate, false, reverse);
				return;
			case Category.UnicodeCJKCompatibilityIdeographs:
				this.EmitRange('豈', '﫿', negate, false, reverse);
				return;
			case Category.UnicodeAlphabeticPresentationForms:
				this.EmitRange('ﬀ', 'ﭏ', negate, false, reverse);
				return;
			case Category.UnicodeArabicPresentationFormsA:
				this.EmitRange('ﭐ', '﷿', negate, false, reverse);
				return;
			case Category.UnicodeCombiningHalfMarks:
				this.EmitRange('︠', '︯', negate, false, reverse);
				return;
			case Category.UnicodeCJKCompatibilityForms:
				this.EmitRange('︰', '﹏', negate, false, reverse);
				return;
			case Category.UnicodeSmallFormVariants:
				this.EmitRange('﹐', '﹯', negate, false, reverse);
				return;
			case Category.UnicodeArabicPresentationFormsB:
				this.EmitRange('ﹰ', '﻾', negate, false, reverse);
				return;
			case Category.UnicodeSpecials:
				this.EmitOpNegateReverse(RxOp.CategoryUnicodeSpecials, negate, reverse);
				return;
			case Category.UnicodeHalfwidthandFullwidthForms:
				this.EmitRange('＀', '￯', negate, false, reverse);
				return;
			}
			throw new NotImplementedException("Missing category: " + cat);
		}

		// Token: 0x06002A2D RID: 10797 RVA: 0x0009139C File Offset: 0x0008F59C
		public void EmitNotCategory(Category cat, bool negate, bool reverse)
		{
			if (negate)
			{
				this.EmitCategory(cat, false, reverse);
			}
			else
			{
				this.EmitCategory(cat, true, reverse);
			}
		}

		// Token: 0x06002A2E RID: 10798 RVA: 0x000913BC File Offset: 0x0008F5BC
		public void EmitRange(char lo, char hi, bool negate, bool ignore, bool reverse)
		{
			if (lo < 'Ā' && hi < 'Ā')
			{
				this.EmitOp(RxOp.Range, negate, ignore, reverse);
				this.Emit((byte)lo);
				this.Emit((byte)hi);
			}
			else
			{
				this.EmitOp(RxOp.UnicodeRange, negate, ignore, reverse);
				this.Emit((ushort)lo);
				this.Emit((ushort)hi);
			}
		}

		// Token: 0x06002A2F RID: 10799 RVA: 0x0009141C File Offset: 0x0008F61C
		public void EmitSet(char lo, BitArray set, bool negate, bool ignore, bool reverse)
		{
			int num = set.Length + 7 >> 3;
			if (lo < 'Ā' && num < 256)
			{
				this.EmitOp(RxOp.Bitmap, negate, ignore, reverse);
				this.Emit((byte)lo);
				this.Emit((byte)num);
			}
			else
			{
				this.EmitOp(RxOp.UnicodeBitmap, negate, ignore, reverse);
				this.Emit((ushort)lo);
				this.Emit((ushort)num);
			}
			int num2 = 0;
			while (num-- != 0)
			{
				int num3 = 0;
				for (int i = 0; i < 8; i++)
				{
					if (num2 >= set.Length)
					{
						break;
					}
					if (set[num2++])
					{
						num3 |= 1 << i;
					}
				}
				this.Emit((byte)num3);
			}
		}

		// Token: 0x06002A30 RID: 10800 RVA: 0x000914E4 File Offset: 0x0008F6E4
		public void EmitString(string str, bool ignore, bool reverse)
		{
			bool flag = false;
			int num = 0;
			if (ignore)
			{
				num++;
			}
			if (reverse)
			{
				num += 2;
			}
			if (ignore)
			{
				str = str.ToLower();
			}
			if (str.Length < 256)
			{
				flag = true;
				for (int i = 0; i < str.Length; i++)
				{
					if (str[i] >= 'Ā')
					{
						flag = false;
						break;
					}
				}
			}
			if (flag)
			{
				this.EmitOpIgnoreReverse(RxOp.String, ignore, reverse);
				this.Emit((byte)str.Length);
				for (int i = 0; i < str.Length; i++)
				{
					this.Emit((byte)str[i]);
				}
			}
			else
			{
				this.EmitOpIgnoreReverse(RxOp.UnicodeString, ignore, reverse);
				if (str.Length > 65535)
				{
					throw new NotSupportedException();
				}
				this.Emit((ushort)str.Length);
				for (int i = 0; i < str.Length; i++)
				{
					this.Emit((ushort)str[i]);
				}
			}
		}

		// Token: 0x06002A31 RID: 10801 RVA: 0x000915F0 File Offset: 0x0008F7F0
		public void EmitPosition(Position pos)
		{
			switch (pos)
			{
			case Position.Any:
				this.Emit(RxOp.AnyPosition);
				break;
			case Position.Start:
				this.Emit(RxOp.StartOfString);
				break;
			case Position.StartOfString:
				this.Emit(RxOp.StartOfString);
				break;
			case Position.StartOfLine:
				this.Emit(RxOp.StartOfLine);
				break;
			case Position.StartOfScan:
				this.Emit(RxOp.StartOfScan);
				break;
			case Position.End:
				this.Emit(RxOp.End);
				break;
			case Position.EndOfString:
				this.Emit(RxOp.EndOfString);
				break;
			case Position.EndOfLine:
				this.Emit(RxOp.EndOfLine);
				break;
			case Position.Boundary:
				this.Emit(RxOp.WordBoundary);
				break;
			case Position.NonBoundary:
				this.Emit(RxOp.NoWordBoundary);
				break;
			default:
				throw new NotSupportedException();
			}
		}

		// Token: 0x06002A32 RID: 10802 RVA: 0x000916B4 File Offset: 0x0008F8B4
		public void EmitOpen(int gid)
		{
			if (gid > 65535)
			{
				throw new NotSupportedException();
			}
			this.Emit(RxOp.OpenGroup);
			this.Emit((ushort)gid);
		}

		// Token: 0x06002A33 RID: 10803 RVA: 0x000916E8 File Offset: 0x0008F8E8
		public void EmitClose(int gid)
		{
			if (gid > 65535)
			{
				throw new NotSupportedException();
			}
			this.Emit(RxOp.CloseGroup);
			this.Emit((ushort)gid);
		}

		// Token: 0x06002A34 RID: 10804 RVA: 0x0009171C File Offset: 0x0008F91C
		public void EmitBalanceStart(int gid, int balance, bool capture, LinkRef tail)
		{
			this.BeginLink(tail);
			this.Emit(RxOp.BalanceStart);
			this.Emit((ushort)gid);
			this.Emit((ushort)balance);
			this.Emit((!capture) ? 0 : 1);
			this.EmitLink(tail);
		}

		// Token: 0x06002A35 RID: 10805 RVA: 0x00091768 File Offset: 0x0008F968
		public void EmitBalance()
		{
			this.Emit(RxOp.Balance);
		}

		// Token: 0x06002A36 RID: 10806 RVA: 0x00091778 File Offset: 0x0008F978
		public void EmitReference(int gid, bool ignore, bool reverse)
		{
			if (gid > 65535)
			{
				throw new NotSupportedException();
			}
			this.EmitOpIgnoreReverse(RxOp.Reference, ignore, reverse);
			this.Emit((ushort)gid);
		}

		// Token: 0x06002A37 RID: 10807 RVA: 0x000917AC File Offset: 0x0008F9AC
		public void EmitIfDefined(int gid, LinkRef tail)
		{
			if (gid > 65535)
			{
				throw new NotSupportedException();
			}
			this.BeginLink(tail);
			this.Emit(RxOp.IfDefined);
			this.EmitLink(tail);
			this.Emit((ushort)gid);
		}

		// Token: 0x06002A38 RID: 10808 RVA: 0x000917EC File Offset: 0x0008F9EC
		public void EmitSub(LinkRef tail)
		{
			this.BeginLink(tail);
			this.Emit(RxOp.SubExpression);
			this.EmitLink(tail);
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x00091808 File Offset: 0x0008FA08
		public void EmitTest(LinkRef yes, LinkRef tail)
		{
			this.BeginLink(yes);
			this.BeginLink(tail);
			this.Emit(RxOp.Test);
			this.EmitLink(yes);
			this.EmitLink(tail);
		}

		// Token: 0x06002A3A RID: 10810 RVA: 0x0009183C File Offset: 0x0008FA3C
		public void EmitBranch(LinkRef next)
		{
			this.BeginLink(next);
			this.Emit(RxOp.Branch);
			this.EmitLink(next);
		}

		// Token: 0x06002A3B RID: 10811 RVA: 0x00091858 File Offset: 0x0008FA58
		public void EmitJump(LinkRef target)
		{
			this.BeginLink(target);
			this.Emit(RxOp.Jump);
			this.EmitLink(target);
		}

		// Token: 0x06002A3C RID: 10812 RVA: 0x00091874 File Offset: 0x0008FA74
		public void EmitIn(LinkRef tail)
		{
			this.BeginLink(tail);
			this.Emit(RxOp.TestCharGroup);
			this.EmitLink(tail);
		}

		// Token: 0x06002A3D RID: 10813 RVA: 0x00091890 File Offset: 0x0008FA90
		public void EmitRepeat(int min, int max, bool lazy, LinkRef until)
		{
			this.BeginLink(until);
			this.Emit((!lazy) ? RxOp.Repeat : RxOp.RepeatLazy);
			this.EmitLink(until);
			this.Emit(min);
			this.Emit(max);
		}

		// Token: 0x06002A3E RID: 10814 RVA: 0x000918D8 File Offset: 0x0008FAD8
		public void EmitUntil(LinkRef repeat)
		{
			this.ResolveLink(repeat);
			this.Emit(RxOp.Until);
		}

		// Token: 0x06002A3F RID: 10815 RVA: 0x000918EC File Offset: 0x0008FAEC
		public void EmitInfo(int count, int min, int max)
		{
			this.Emit(RxOp.Info);
			if (count > 65535)
			{
				throw new NotSupportedException();
			}
			this.Emit((ushort)count);
			this.Emit(min);
			this.Emit(max);
		}

		// Token: 0x06002A40 RID: 10816 RVA: 0x00091928 File Offset: 0x0008FB28
		public void EmitFastRepeat(int min, int max, bool lazy, LinkRef tail)
		{
			this.BeginLink(tail);
			this.Emit((!lazy) ? RxOp.FastRepeat : RxOp.FastRepeatLazy);
			this.EmitLink(tail);
			this.Emit(min);
			this.Emit(max);
		}

		// Token: 0x06002A41 RID: 10817 RVA: 0x00091970 File Offset: 0x0008FB70
		public void EmitAnchor(bool reverse, int offset, LinkRef tail)
		{
			this.BeginLink(tail);
			if (reverse)
			{
				this.Emit(RxOp.AnchorReverse);
			}
			else
			{
				this.Emit(RxOp.Anchor);
			}
			this.EmitLink(tail);
			if (offset > 65535)
			{
				throw new NotSupportedException();
			}
			this.Emit((ushort)offset);
		}

		// Token: 0x06002A42 RID: 10818 RVA: 0x000919C8 File Offset: 0x0008FBC8
		public void EmitBranchEnd()
		{
		}

		// Token: 0x06002A43 RID: 10819 RVA: 0x000919CC File Offset: 0x0008FBCC
		public void EmitAlternationEnd()
		{
		}

		// Token: 0x06002A44 RID: 10820 RVA: 0x000919D0 File Offset: 0x0008FBD0
		public LinkRef NewLink()
		{
			return new RxLinkRef();
		}

		// Token: 0x06002A45 RID: 10821 RVA: 0x000919D8 File Offset: 0x0008FBD8
		public void ResolveLink(LinkRef link)
		{
			RxLinkRef rxLinkRef = link as RxLinkRef;
			for (int i = 0; i < rxLinkRef.current; i += 2)
			{
				int num = this.curpos - rxLinkRef.offsets[i];
				if (num > 65535)
				{
					throw new NotSupportedException();
				}
				int num2 = rxLinkRef.offsets[i + 1];
				this.program[num2] = (byte)num;
				this.program[num2 + 1] = (byte)(num >> 8);
			}
		}

		// Token: 0x04001A51 RID: 6737
		protected byte[] program = new byte[32];

		// Token: 0x04001A52 RID: 6738
		protected int curpos;
	}
}
