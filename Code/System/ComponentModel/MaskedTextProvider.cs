using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;

namespace System.ComponentModel
{
	/// <summary>Represents a mask-parsing service that can be used by any number of controls that support masking, such as the <see cref="T:System.Windows.Forms.MaskedTextBox" /> control.</summary>
	// Token: 0x02000185 RID: 389
	public class MaskedTextProvider : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MaskedTextProvider" /> class using the specified mask.</summary>
		/// <param name="mask">A <see cref="T:System.String" /> that represents the input mask. </param>
		// Token: 0x06000D52 RID: 3410 RVA: 0x00021210 File Offset: 0x0001F410
		public MaskedTextProvider(string mask) : this(mask, null, true, MaskedTextProvider.default_prompt_char, MaskedTextProvider.default_password_char, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MaskedTextProvider" /> class using the specified mask and ASCII restriction value.</summary>
		/// <param name="mask">A <see cref="T:System.String" /> that represents the input mask. </param>
		/// <param name="restrictToAscii">true to restrict input to ASCII-compatible characters; otherwise false to allow the entire Unicode set. </param>
		// Token: 0x06000D53 RID: 3411 RVA: 0x00021228 File Offset: 0x0001F428
		public MaskedTextProvider(string mask, bool restrictToAscii) : this(mask, null, true, MaskedTextProvider.default_prompt_char, MaskedTextProvider.default_password_char, restrictToAscii)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MaskedTextProvider" /> class using the specified mask and culture.</summary>
		/// <param name="mask">A <see cref="T:System.String" /> that represents the input mask. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that is used to set region-sensitive separator characters.</param>
		// Token: 0x06000D54 RID: 3412 RVA: 0x00021240 File Offset: 0x0001F440
		public MaskedTextProvider(string mask, CultureInfo culture) : this(mask, culture, true, MaskedTextProvider.default_prompt_char, MaskedTextProvider.default_password_char, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MaskedTextProvider" /> class using the specified mask, password character, and prompt usage value.</summary>
		/// <param name="mask">A <see cref="T:System.String" /> that represents the input mask. </param>
		/// <param name="passwordChar">A <see cref="T:System.Char" /> that will be displayed for characters entered into a password string.</param>
		/// <param name="allowPromptAsInput">true to allow the prompt character as input; otherwise false. </param>
		// Token: 0x06000D55 RID: 3413 RVA: 0x00021258 File Offset: 0x0001F458
		public MaskedTextProvider(string mask, char passwordChar, bool allowPromptAsInput) : this(mask, null, allowPromptAsInput, MaskedTextProvider.default_prompt_char, passwordChar, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MaskedTextProvider" /> class using the specified mask, culture, and ASCII restriction value.</summary>
		/// <param name="mask">A <see cref="T:System.String" /> that represents the input mask. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that is used to set region-sensitive separator characters.</param>
		/// <param name="restrictToAscii">true to restrict input to ASCII-compatible characters; otherwise false to allow the entire Unicode set. </param>
		// Token: 0x06000D56 RID: 3414 RVA: 0x0002126C File Offset: 0x0001F46C
		public MaskedTextProvider(string mask, CultureInfo culture, bool restrictToAscii) : this(mask, culture, true, MaskedTextProvider.default_prompt_char, MaskedTextProvider.default_password_char, restrictToAscii)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MaskedTextProvider" /> class using the specified mask, culture, password character, and prompt usage value.</summary>
		/// <param name="mask">A <see cref="T:System.String" /> that represents the input mask. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that is used to set region-sensitive separator characters.</param>
		/// <param name="passwordChar">A <see cref="T:System.Char" /> that will be displayed for characters entered into a password string.</param>
		/// <param name="allowPromptAsInput">true to allow the prompt character as input; otherwise false. </param>
		// Token: 0x06000D57 RID: 3415 RVA: 0x00021284 File Offset: 0x0001F484
		public MaskedTextProvider(string mask, CultureInfo culture, char passwordChar, bool allowPromptAsInput) : this(mask, culture, allowPromptAsInput, MaskedTextProvider.default_prompt_char, passwordChar, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MaskedTextProvider" /> class using the specified mask, culture, prompt usage value, prompt character, password character, and ASCII restriction value.</summary>
		/// <param name="mask">A <see cref="T:System.String" /> that represents the input mask. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that is used to set region-sensitive separator characters.</param>
		/// <param name="allowPromptAsInput">A <see cref="T:System.Boolean" /> value that specifies whether the prompt character should be allowed as a valid input character. </param>
		/// <param name="promptChar">A <see cref="T:System.Char" /> that will be displayed as a placeholder for user input.</param>
		/// <param name="passwordChar">A <see cref="T:System.Char" /> that will be displayed for characters entered into a password string.</param>
		/// <param name="restrictToAscii">true to restrict input to ASCII-compatible characters; otherwise false to allow the entire Unicode set. </param>
		/// <exception cref="T:System.ArgumentException">The mask parameter is null or <see cref="F:System.String.Empty" />.-or-The mask contains one or more non-printable characters. </exception>
		// Token: 0x06000D58 RID: 3416 RVA: 0x00021298 File Offset: 0x0001F498
		public MaskedTextProvider(string mask, CultureInfo culture, bool allowPromptAsInput, char promptChar, char passwordChar, bool restrictToAscii)
		{
			this.SetMask(mask);
			if (culture == null)
			{
				this.culture = Thread.CurrentThread.CurrentCulture;
			}
			else
			{
				this.culture = culture;
			}
			this.allow_prompt_as_input = allowPromptAsInput;
			this.PromptChar = promptChar;
			this.PasswordChar = passwordChar;
			this.ascii_only = restrictToAscii;
			this.include_literals = true;
			this.reset_on_prompt = true;
			this.reset_on_space = true;
			this.skip_literals = true;
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x0002131C File Offset: 0x0001F51C
		private void SetMask(string mask)
		{
			if (mask == null || mask == string.Empty)
			{
				throw new ArgumentException("The Mask value cannot be null or empty.\r\nParameter name: mask");
			}
			this.mask = mask;
			List<MaskedTextProvider.EditPosition> list = new List<MaskedTextProvider.EditPosition>(mask.Length);
			MaskedTextProvider.EditState state = MaskedTextProvider.EditState.None;
			bool flag = false;
			for (int i = 0; i < mask.Length; i++)
			{
				if (flag)
				{
					list.Add(new MaskedTextProvider.EditPosition(this, MaskedTextProvider.EditType.Literal, state, mask[i]));
					flag = false;
				}
				else
				{
					char c = mask[i];
					switch (c)
					{
					case '#':
						list.Add(new MaskedTextProvider.EditPosition(this, MaskedTextProvider.EditType.DigitOrSpaceOptional_Blank, state, mask[i]));
						break;
					case '$':
						list.Add(new MaskedTextProvider.EditPosition(this, MaskedTextProvider.EditType.CurrencySymbol, state, mask[i]));
						break;
					default:
						switch (c)
						{
						case '9':
							list.Add(new MaskedTextProvider.EditPosition(this, MaskedTextProvider.EditType.DigitOrSpaceOptional, state, mask[i]));
							break;
						case ':':
							list.Add(new MaskedTextProvider.EditPosition(this, MaskedTextProvider.EditType.TimeSeparator, state, mask[i]));
							break;
						default:
							if (c != 'L')
							{
								if (c != '\\')
								{
									if (c != 'a')
									{
										if (c != '|')
										{
											list.Add(new MaskedTextProvider.EditPosition(this, MaskedTextProvider.EditType.Literal, state, mask[i]));
										}
										else
										{
											state = MaskedTextProvider.EditState.None;
										}
									}
									else
									{
										list.Add(new MaskedTextProvider.EditPosition(this, MaskedTextProvider.EditType.AlphanumericOptional, state, mask[i]));
									}
								}
								else
								{
									flag = true;
								}
							}
							else
							{
								list.Add(new MaskedTextProvider.EditPosition(this, MaskedTextProvider.EditType.LetterRequired, state, mask[i]));
							}
							break;
						case '<':
							state = MaskedTextProvider.EditState.LowerCase;
							break;
						case '>':
							state = MaskedTextProvider.EditState.UpperCase;
							break;
						case '?':
							list.Add(new MaskedTextProvider.EditPosition(this, MaskedTextProvider.EditType.LetterOptional, state, mask[i]));
							break;
						case 'A':
							list.Add(new MaskedTextProvider.EditPosition(this, MaskedTextProvider.EditType.AlphanumericRequired, state, mask[i]));
							break;
						case 'C':
							list.Add(new MaskedTextProvider.EditPosition(this, MaskedTextProvider.EditType.CharacterOptional, state, mask[i]));
							break;
						}
						break;
					case '&':
						list.Add(new MaskedTextProvider.EditPosition(this, MaskedTextProvider.EditType.CharacterRequired, state, mask[i]));
						break;
					case ',':
						list.Add(new MaskedTextProvider.EditPosition(this, MaskedTextProvider.EditType.ThousandsPlaceholder, state, mask[i]));
						break;
					case '.':
						list.Add(new MaskedTextProvider.EditPosition(this, MaskedTextProvider.EditType.DecimalPlaceholder, state, mask[i]));
						break;
					case '/':
						list.Add(new MaskedTextProvider.EditPosition(this, MaskedTextProvider.EditType.DateSeparator, state, mask[i]));
						break;
					case '0':
						list.Add(new MaskedTextProvider.EditPosition(this, MaskedTextProvider.EditType.DigitRequired, state, mask[i]));
						break;
					}
				}
			}
			this.edit_positions = list.ToArray();
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x000215FC File Offset: 0x0001F7FC
		private MaskedTextProvider.EditPosition[] ClonePositions()
		{
			MaskedTextProvider.EditPosition[] array = new MaskedTextProvider.EditPosition[this.edit_positions.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.edit_positions[i].Clone();
			}
			return array;
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x0002163C File Offset: 0x0001F83C
		private bool AddInternal(string str_input, out int testPosition, out MaskedTextResultHint resultHint, bool only_test)
		{
			MaskedTextProvider.EditPosition[] array;
			if (only_test)
			{
				array = this.ClonePositions();
			}
			else
			{
				array = this.edit_positions;
			}
			if (str_input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (str_input.Length == 0)
			{
				resultHint = MaskedTextResultHint.NoEffect;
				testPosition = this.LastAssignedPosition + 1;
				return true;
			}
			resultHint = MaskedTextResultHint.Unknown;
			testPosition = 0;
			int num = this.LastAssignedPosition;
			MaskedTextResultHint maskedTextResultHint = MaskedTextResultHint.Unknown;
			if (num >= array.Length)
			{
				testPosition = num;
				resultHint = MaskedTextResultHint.UnavailableEditPosition;
				return false;
			}
			foreach (char c in str_input)
			{
				num++;
				testPosition = num;
				if (maskedTextResultHint > resultHint)
				{
					resultHint = maskedTextResultHint;
				}
				if (this.VerifyEscapeChar(c, num))
				{
					maskedTextResultHint = MaskedTextResultHint.CharacterEscaped;
				}
				else
				{
					num = this.FindEditPositionFrom(num, true);
					testPosition = num;
					if (num == MaskedTextProvider.InvalidIndex)
					{
						testPosition = array.Length;
						resultHint = MaskedTextResultHint.UnavailableEditPosition;
						return false;
					}
					if (!MaskedTextProvider.IsValidInputChar(c))
					{
						testPosition = num;
						resultHint = MaskedTextResultHint.InvalidInput;
						return false;
					}
					if (!array[num].Match(c, out maskedTextResultHint, false))
					{
						testPosition = num;
						resultHint = maskedTextResultHint;
						return false;
					}
				}
			}
			if (maskedTextResultHint > resultHint)
			{
				resultHint = maskedTextResultHint;
			}
			return true;
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x00021754 File Offset: 0x0001F954
		private bool AddInternal(char input, out int testPosition, out MaskedTextResultHint resultHint, bool check_available_positions_first, bool check_escape_char_first)
		{
			testPosition = 0;
			int num = this.LastAssignedPosition + 1;
			if (check_available_positions_first)
			{
				int i = num;
				bool flag = false;
				while (i < this.edit_positions.Length)
				{
					if (this.edit_positions[i].Editable)
					{
						flag = true;
						break;
					}
					i++;
				}
				if (!flag)
				{
					testPosition = i;
					resultHint = MaskedTextResultHint.UnavailableEditPosition;
					return MaskedTextProvider.GetOperationResultFromHint(resultHint);
				}
			}
			if (check_escape_char_first && this.VerifyEscapeChar(input, num))
			{
				testPosition = num;
				resultHint = MaskedTextResultHint.CharacterEscaped;
				return true;
			}
			num = this.FindEditPositionFrom(num, true);
			if (num > this.edit_positions.Length - 1 || num == MaskedTextProvider.InvalidIndex)
			{
				testPosition = num;
				resultHint = MaskedTextResultHint.UnavailableEditPosition;
				return MaskedTextProvider.GetOperationResultFromHint(resultHint);
			}
			if (!MaskedTextProvider.IsValidInputChar(input))
			{
				testPosition = num;
				resultHint = MaskedTextResultHint.InvalidInput;
				return MaskedTextProvider.GetOperationResultFromHint(resultHint);
			}
			if (!this.edit_positions[num].Match(input, out resultHint, false))
			{
				testPosition = num;
				return MaskedTextProvider.GetOperationResultFromHint(resultHint);
			}
			testPosition = num;
			return MaskedTextProvider.GetOperationResultFromHint(resultHint);
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x00021850 File Offset: 0x0001FA50
		private bool VerifyStringInternal(string input, out int testPosition, out MaskedTextResultHint resultHint, int startIndex, bool only_test)
		{
			int num = startIndex;
			resultHint = MaskedTextResultHint.Unknown;
			for (int i = 0; i < input.Length; i++)
			{
				int num2 = this.FindEditPositionFrom(num, true);
				if (num2 == MaskedTextProvider.InvalidIndex)
				{
					testPosition = this.edit_positions.Length;
					resultHint = MaskedTextResultHint.UnavailableEditPosition;
					return false;
				}
				MaskedTextResultHint maskedTextResultHint;
				if (!this.VerifyCharInternal(input[i], num2, out maskedTextResultHint, only_test))
				{
					testPosition = num2;
					resultHint = maskedTextResultHint;
					return false;
				}
				if (maskedTextResultHint > resultHint)
				{
					resultHint = maskedTextResultHint;
				}
				num = num2 + 1;
			}
			if (!only_test)
			{
				for (num = this.FindEditPositionFrom(num, true); num != MaskedTextProvider.InvalidIndex; num = this.FindEditPositionFrom(num + 1, true))
				{
					if (this.edit_positions[num].FilledIn)
					{
						this.edit_positions[num].Reset();
						if (resultHint != MaskedTextResultHint.NoEffect)
						{
							resultHint = MaskedTextResultHint.Success;
						}
					}
				}
			}
			if (input.Length > 0)
			{
				testPosition = startIndex + input.Length - 1;
			}
			else
			{
				testPosition = startIndex;
				if (resultHint < MaskedTextResultHint.NoEffect)
				{
					resultHint = MaskedTextResultHint.NoEffect;
				}
			}
			return true;
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x00021950 File Offset: 0x0001FB50
		private bool VerifyCharInternal(char input, int position, out MaskedTextResultHint hint, bool only_test)
		{
			hint = MaskedTextResultHint.Unknown;
			if (position < 0 || position >= this.edit_positions.Length)
			{
				hint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			if (!MaskedTextProvider.IsValidInputChar(input))
			{
				hint = MaskedTextResultHint.InvalidInput;
				return false;
			}
			if (input == ' ' && this.ResetOnSpace && this.edit_positions[position].Editable && this.edit_positions[position].FilledIn)
			{
				if (!only_test)
				{
					this.edit_positions[position].Reset();
				}
				hint = MaskedTextResultHint.SideEffect;
				return true;
			}
			if (this.edit_positions[position].Editable && this.edit_positions[position].FilledIn && this.edit_positions[position].input == input)
			{
				hint = MaskedTextResultHint.NoEffect;
				return true;
			}
			if (this.SkipLiterals && !this.edit_positions[position].Editable && this.edit_positions[position].Text == input.ToString())
			{
				hint = MaskedTextResultHint.CharacterEscaped;
				return true;
			}
			return this.edit_positions[position].Match(input, out hint, only_test);
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x00021A6C File Offset: 0x0001FC6C
		private bool IsInsertableString(string str_input, int position, out int testPosition, out MaskedTextResultHint resultHint)
		{
			int num = position;
			resultHint = MaskedTextResultHint.UnavailableEditPosition;
			testPosition = MaskedTextProvider.InvalidIndex;
			foreach (char c in str_input)
			{
				int num2 = this.FindEditPositionFrom(num, true);
				if (num2 != MaskedTextProvider.InvalidIndex && this.VerifyEscapeChar(c, num2))
				{
					num = num2 + 1;
				}
				else if (this.VerifyEscapeChar(c, num))
				{
					num++;
				}
				else
				{
					if (num2 == MaskedTextProvider.InvalidIndex)
					{
						resultHint = MaskedTextResultHint.UnavailableEditPosition;
						testPosition = this.edit_positions.Length;
						return false;
					}
					testPosition = num2;
					if (!this.edit_positions[num2].Match(c, out resultHint, true))
					{
						return false;
					}
					num = num2 + 1;
				}
			}
			resultHint = MaskedTextResultHint.Success;
			return true;
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x00021B28 File Offset: 0x0001FD28
		private bool ShiftPositionsRight(MaskedTextProvider.EditPosition[] edit_positions, int start, out int testPosition, out MaskedTextResultHint resultHint)
		{
			int position = this.FindAssignedEditPositionFrom(edit_positions.Length, false);
			int i = this.FindUnassignedEditPositionFrom(position, true);
			testPosition = start;
			resultHint = MaskedTextResultHint.Unknown;
			if (i == MaskedTextProvider.InvalidIndex)
			{
				testPosition = edit_positions.Length;
				resultHint = MaskedTextResultHint.UnavailableEditPosition;
				return false;
			}
			while (i > start)
			{
				int num = this.FindEditPositionFrom(i - 1, false);
				char input = edit_positions[num].input;
				if (input == '\0')
				{
					edit_positions[i].input = input;
				}
				else if (!edit_positions[i].Match(input, out resultHint, false))
				{
					testPosition = i;
					return false;
				}
				i = num;
			}
			if (i != MaskedTextProvider.InvalidIndex)
			{
				edit_positions[i].Reset();
			}
			return true;
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x00021BCC File Offset: 0x0001FDCC
		private bool ReplaceInternal(string input, int startPosition, int endPosition, out int testPosition, out MaskedTextResultHint resultHint, bool only_test, bool dont_remove_at_end)
		{
			resultHint = MaskedTextResultHint.Unknown;
			MaskedTextProvider.EditPosition[] array;
			if (only_test)
			{
				array = this.ClonePositions();
			}
			else
			{
				array = this.edit_positions;
			}
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (endPosition >= array.Length)
			{
				testPosition = endPosition;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			if (startPosition < 0)
			{
				testPosition = startPosition;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			if (startPosition >= array.Length)
			{
				testPosition = startPosition;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			if (startPosition > endPosition)
			{
				testPosition = startPosition;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			if (input.Length == 0)
			{
				return this.RemoveAtInternal(startPosition, endPosition, out testPosition, out resultHint, only_test);
			}
			int num = startPosition;
			int num2 = num;
			MaskedTextResultHint maskedTextResultHint = MaskedTextResultHint.Unknown;
			testPosition = MaskedTextProvider.InvalidIndex;
			foreach (char c in input)
			{
				num2 = num;
				if (this.VerifyEscapeChar(c, num2))
				{
					if ((array[num2].FilledIn && array[num2].Editable && c == ' ' && this.ResetOnSpace) || (c == this.PromptChar && this.ResetOnPrompt))
					{
						array[num2].Reset();
						maskedTextResultHint = MaskedTextResultHint.SideEffect;
					}
					else
					{
						maskedTextResultHint = MaskedTextResultHint.CharacterEscaped;
					}
				}
				else if (num2 < array.Length && !array[num2].Editable && this.FindAssignedEditPositionInRange(num2, endPosition, true) == MaskedTextProvider.InvalidIndex)
				{
					num2 = this.FindEditPositionFrom(num2, true);
					if (num2 == MaskedTextProvider.InvalidIndex)
					{
						resultHint = MaskedTextResultHint.UnavailableEditPosition;
						testPosition = array.Length;
						return false;
					}
					if (!this.InsertAtInternal(c.ToString(), num2, out testPosition, out maskedTextResultHint, only_test))
					{
						resultHint = maskedTextResultHint;
						return false;
					}
				}
				else
				{
					num2 = this.FindEditPositionFrom(num2, true);
					if (num2 == MaskedTextProvider.InvalidIndex)
					{
						testPosition = array.Length;
						resultHint = MaskedTextResultHint.UnavailableEditPosition;
						return false;
					}
					if (!MaskedTextProvider.IsValidInputChar(c))
					{
						testPosition = num2;
						resultHint = MaskedTextResultHint.InvalidInput;
						return false;
					}
					if (!this.ReplaceInternal(array, c, num2, out testPosition, out maskedTextResultHint, false))
					{
						resultHint = maskedTextResultHint;
						return false;
					}
				}
				if (maskedTextResultHint > resultHint)
				{
					resultHint = maskedTextResultHint;
				}
				num = num2 + 1;
			}
			testPosition = num2;
			int num3;
			if (!dont_remove_at_end && num <= endPosition && !this.RemoveAtInternal(num, endPosition, out num3, out maskedTextResultHint, only_test))
			{
				testPosition = num3;
				resultHint = maskedTextResultHint;
				return false;
			}
			if (maskedTextResultHint == MaskedTextResultHint.Success && resultHint < MaskedTextResultHint.SideEffect)
			{
				resultHint = MaskedTextResultHint.SideEffect;
			}
			return true;
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x00021E24 File Offset: 0x00020024
		private bool ReplaceInternal(MaskedTextProvider.EditPosition[] edit_positions, char input, int position, out int testPosition, out MaskedTextResultHint resultHint, bool only_test)
		{
			testPosition = position;
			if (!MaskedTextProvider.IsValidInputChar(input))
			{
				resultHint = MaskedTextResultHint.InvalidInput;
				return false;
			}
			if (this.VerifyEscapeChar(input, position))
			{
				if ((edit_positions[position].FilledIn && edit_positions[position].Editable && input == ' ' && this.ResetOnSpace) || (input == this.PromptChar && this.ResetOnPrompt))
				{
					edit_positions[position].Reset();
					resultHint = MaskedTextResultHint.SideEffect;
				}
				else
				{
					resultHint = MaskedTextResultHint.CharacterEscaped;
				}
				testPosition = position;
				return true;
			}
			if (!edit_positions[position].Editable)
			{
				resultHint = MaskedTextResultHint.NonEditPosition;
				return false;
			}
			bool filledIn = edit_positions[position].FilledIn;
			if (filledIn && edit_positions[position].input == input)
			{
				if (this.VerifyEscapeChar(input, position))
				{
					resultHint = MaskedTextResultHint.CharacterEscaped;
				}
				else
				{
					resultHint = MaskedTextResultHint.NoEffect;
				}
			}
			else
			{
				if (input == ' ' && this.ResetOnSpace)
				{
					if (filledIn)
					{
						resultHint = MaskedTextResultHint.SideEffect;
						edit_positions[position].Reset();
					}
					else
					{
						resultHint = MaskedTextResultHint.CharacterEscaped;
					}
					return true;
				}
				if (this.VerifyEscapeChar(input, position))
				{
					resultHint = MaskedTextResultHint.SideEffect;
				}
				else
				{
					resultHint = MaskedTextResultHint.Success;
				}
			}
			MaskedTextResultHint maskedTextResultHint;
			if (!edit_positions[position].Match(input, out maskedTextResultHint, false))
			{
				resultHint = maskedTextResultHint;
				return false;
			}
			return true;
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x00021F68 File Offset: 0x00020168
		private bool RemoveAtInternal(int startPosition, int endPosition, out int testPosition, out MaskedTextResultHint resultHint, bool only_testing)
		{
			testPosition = -1;
			resultHint = MaskedTextResultHint.Unknown;
			MaskedTextProvider.EditPosition[] array;
			if (only_testing)
			{
				array = this.ClonePositions();
			}
			else
			{
				array = this.edit_positions;
			}
			if (endPosition < 0 || endPosition >= array.Length)
			{
				testPosition = endPosition;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			if (startPosition < 0 || startPosition >= array.Length)
			{
				testPosition = startPosition;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			if (startPosition > endPosition)
			{
				testPosition = startPosition;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			int num = 0;
			for (int i = startPosition; i <= endPosition; i++)
			{
				if (array[i].Editable)
				{
					num++;
				}
			}
			if (num == 0)
			{
				testPosition = startPosition;
				resultHint = MaskedTextResultHint.NoEffect;
				return true;
			}
			for (int num2 = this.FindEditPositionFrom(startPosition, true); num2 != MaskedTextProvider.InvalidIndex; num2 = this.FindEditPositionFrom(num2 + 1, true))
			{
				int num3 = this.FindEditPositionFrom(num2 + 1, true);
				int num4 = 1;
				while (num4 < num && num3 != MaskedTextProvider.InvalidIndex)
				{
					num3 = this.FindEditPositionFrom(num3 + 1, true);
					num4++;
				}
				if (num3 == MaskedTextProvider.InvalidIndex)
				{
					if (array[num2].FilledIn)
					{
						array[num2].Reset();
						resultHint = MaskedTextResultHint.Success;
					}
					else if (resultHint < MaskedTextResultHint.NoEffect)
					{
						resultHint = MaskedTextResultHint.NoEffect;
					}
				}
				else
				{
					if (!array[num3].FilledIn)
					{
						if (array[num2].FilledIn)
						{
							array[num2].Reset();
							resultHint = MaskedTextResultHint.Success;
						}
						else if (resultHint < MaskedTextResultHint.NoEffect)
						{
							resultHint = MaskedTextResultHint.NoEffect;
						}
					}
					else
					{
						MaskedTextResultHint maskedTextResultHint = MaskedTextResultHint.Unknown;
						if (array[num2].FilledIn)
						{
							resultHint = MaskedTextResultHint.Success;
						}
						else if (resultHint < MaskedTextResultHint.SideEffect)
						{
							resultHint = MaskedTextResultHint.SideEffect;
						}
						if (!array[num2].Match(array[num3].input, out maskedTextResultHint, false))
						{
							resultHint = maskedTextResultHint;
							testPosition = num2;
							return false;
						}
					}
					array[num3].Reset();
				}
			}
			if (resultHint == MaskedTextResultHint.Unknown)
			{
				resultHint = MaskedTextResultHint.NoEffect;
			}
			testPosition = startPosition;
			return true;
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x0002214C File Offset: 0x0002034C
		private bool InsertAtInternal(string str_input, int position, out int testPosition, out MaskedTextResultHint resultHint, bool only_testing)
		{
			testPosition = -1;
			resultHint = MaskedTextResultHint.Unknown;
			MaskedTextProvider.EditPosition[] array;
			if (only_testing)
			{
				array = this.ClonePositions();
			}
			else
			{
				array = this.edit_positions;
			}
			if (position < 0 || position >= array.Length)
			{
				testPosition = 0;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			if (!this.IsInsertableString(str_input, position, out testPosition, out resultHint))
			{
				return false;
			}
			resultHint = MaskedTextResultHint.Unknown;
			int num = position;
			int i = 0;
			while (i < str_input.Length)
			{
				char c = str_input[i];
				int num2 = this.FindEditPositionFrom(num, true);
				int num3 = this.FindUnassignedEditPositionFrom(num, true);
				bool flag = false;
				if (!this.VerifyEscapeChar(c, num))
				{
					goto IL_DF;
				}
				flag = true;
				if (!(c.ToString() == array[num].Text))
				{
					goto IL_DF;
				}
				if (this.FindAssignedEditPositionInRange(0, num - 1, true) != MaskedTextProvider.InvalidIndex && num3 == MaskedTextProvider.InvalidIndex)
				{
					resultHint = MaskedTextResultHint.UnavailableEditPosition;
					testPosition = array.Length;
					return false;
				}
				resultHint = MaskedTextResultHint.CharacterEscaped;
				testPosition = num;
				num++;
				IL_1FA:
				i++;
				continue;
				IL_DF:
				if (!flag && num2 == MaskedTextProvider.InvalidIndex)
				{
					testPosition = array.Length;
					resultHint = MaskedTextResultHint.UnavailableEditPosition;
					return false;
				}
				if (num2 == MaskedTextProvider.InvalidIndex)
				{
					num2 = num;
				}
				bool filledIn = array[num2].FilledIn;
				bool flag2 = filledIn;
				if (flag2 && !this.ShiftPositionsRight(array, num2, out testPosition, out resultHint))
				{
					return false;
				}
				testPosition = num2;
				if (flag)
				{
					if (filledIn)
					{
						resultHint = MaskedTextResultHint.Success;
					}
					else if (!array[num2].Editable && c.ToString() == array[num2].Text)
					{
						resultHint = MaskedTextResultHint.CharacterEscaped;
						testPosition = num;
					}
					else
					{
						int num4 = this.FindEditPositionFrom(num2, true);
						if (num4 == MaskedTextProvider.InvalidIndex)
						{
							resultHint = MaskedTextResultHint.UnavailableEditPosition;
							testPosition = array.Length;
							return false;
						}
						resultHint = MaskedTextResultHint.CharacterEscaped;
						if (c.ToString() == array[num].Text)
						{
							testPosition = num;
						}
					}
				}
				else
				{
					MaskedTextResultHint maskedTextResultHint;
					if (!array[num2].Match(c, out maskedTextResultHint, false))
					{
						resultHint = maskedTextResultHint;
						return false;
					}
					if (resultHint < maskedTextResultHint)
					{
						resultHint = maskedTextResultHint;
					}
				}
				num = num2 + 1;
				goto IL_1FA;
			}
			return true;
		}

		/// <summary>Adds the specified input character to the end of the formatted string.</summary>
		/// <returns>true if the input character was added successfully; otherwise false.</returns>
		/// <param name="input">A <see cref="T:System.Char" /> value to be appended to the formatted string. </param>
		// Token: 0x06000D66 RID: 3430 RVA: 0x00022364 File Offset: 0x00020564
		public bool Add(char input)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.Add(input, out num, out maskedTextResultHint);
		}

		/// <summary>Adds the characters in the specified input string to the end of the formatted string.</summary>
		/// <returns>true if all the characters from the input string were added successfully; otherwise false to indicate that no characters were added.</returns>
		/// <param name="input">A <see cref="T:System.String" /> containing character values to be appended to the formatted string. </param>
		/// <exception cref="T:System.ArgumentNullException">The<paramref name=" input" /> parameter is null.</exception>
		// Token: 0x06000D67 RID: 3431 RVA: 0x0002237C File Offset: 0x0002057C
		public bool Add(string input)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.Add(input, out num, out maskedTextResultHint);
		}

		/// <summary>Adds the specified input character to the end of the formatted string, and then outputs position and descriptive information.</summary>
		/// <returns>true if the input character was added successfully; otherwise false.</returns>
		/// <param name="input">A <see cref="T:System.Char" /> value to be appended to the formatted string.</param>
		/// <param name="testPosition">The zero-based position in the formatted string where the attempt was made to add the character. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the operation. An output parameter.</param>
		// Token: 0x06000D68 RID: 3432 RVA: 0x00022394 File Offset: 0x00020594
		public bool Add(char input, out int testPosition, out MaskedTextResultHint resultHint)
		{
			return this.AddInternal(input, out testPosition, out resultHint, true, false);
		}

		/// <summary>Adds the characters in the specified input string to the end of the formatted string, and then outputs position and descriptive information.</summary>
		/// <returns>true if all the characters from the input string were added successfully; otherwise false to indicate that no characters were added.</returns>
		/// <param name="input">A <see cref="T:System.String" /> containing character values to be appended to the formatted string. </param>
		/// <param name="testPosition">The zero-based position in the formatted string where the attempt was made to add the character. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the operation. An output parameter.</param>
		// Token: 0x06000D69 RID: 3433 RVA: 0x000223A4 File Offset: 0x000205A4
		public bool Add(string input, out int testPosition, out MaskedTextResultHint resultHint)
		{
			bool flag = this.AddInternal(input, out testPosition, out resultHint, true);
			if (flag)
			{
				flag = this.AddInternal(input, out testPosition, out resultHint, false);
			}
			return flag;
		}

		/// <summary>Clears all the editable input characters from the formatted string, replacing them with prompt characters.</summary>
		// Token: 0x06000D6A RID: 3434 RVA: 0x000223D0 File Offset: 0x000205D0
		public void Clear()
		{
			MaskedTextResultHint maskedTextResultHint;
			this.Clear(out maskedTextResultHint);
		}

		/// <summary>Clears all the editable input characters from the formatted string, replacing them with prompt characters, and then outputs descriptive information.</summary>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the operation. An output parameter. </param>
		// Token: 0x06000D6B RID: 3435 RVA: 0x000223E8 File Offset: 0x000205E8
		public void Clear(out MaskedTextResultHint resultHint)
		{
			resultHint = MaskedTextResultHint.NoEffect;
			for (int i = 0; i < this.edit_positions.Length; i++)
			{
				if (this.edit_positions[i].Editable && this.edit_positions[i].FilledIn)
				{
					this.edit_positions[i].Reset();
					resultHint = MaskedTextResultHint.Success;
				}
			}
		}

		/// <summary>Creates a copy of the current <see cref="T:System.ComponentModel.MaskedTextProvider" />.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.MaskedTextProvider" /> object this method creates, cast as an object.</returns>
		// Token: 0x06000D6C RID: 3436 RVA: 0x00022448 File Offset: 0x00020648
		public object Clone()
		{
			return new MaskedTextProvider(this.mask)
			{
				allow_prompt_as_input = this.allow_prompt_as_input,
				ascii_only = this.ascii_only,
				culture = this.culture,
				edit_positions = this.ClonePositions(),
				include_literals = this.include_literals,
				include_prompt = this.include_prompt,
				is_password = this.is_password,
				mask = this.mask,
				password_char = this.password_char,
				prompt_char = this.prompt_char,
				reset_on_prompt = this.reset_on_prompt,
				reset_on_space = this.reset_on_space,
				skip_literals = this.skip_literals
			};
		}

		/// <summary>Returns the position of the first assigned editable position after the specified position using the specified search direction.</summary>
		/// <returns>If successful, an <see cref="T:System.Int32" /> representing the zero-based position of the first assigned editable position encountered; otherwise <see cref="P:System.ComponentModel.MaskedTextProvider.InvalidIndex" />.</returns>
		/// <param name="position">The zero-based position in the formatted string to start the search.</param>
		/// <param name="direction">A <see cref="T:System.Boolean" /> indicating the search direction; either true to search forward or false to search backward.</param>
		// Token: 0x06000D6D RID: 3437 RVA: 0x00022500 File Offset: 0x00020700
		public int FindAssignedEditPositionFrom(int position, bool direction)
		{
			if (direction)
			{
				return this.FindAssignedEditPositionInRange(position, this.edit_positions.Length - 1, direction);
			}
			return this.FindAssignedEditPositionInRange(0, position, direction);
		}

		/// <summary>Returns the position of the first assigned editable position between the specified positions using the specified search direction.</summary>
		/// <returns>If successful, an <see cref="T:System.Int32" /> representing the zero-based position of the first assigned editable position encountered; otherwise <see cref="P:System.ComponentModel.MaskedTextProvider.InvalidIndex" />.</returns>
		/// <param name="startPosition">The zero-based position in the formatted string where the search starts.</param>
		/// <param name="endPosition">The zero-based position in the formatted string where the search ends.</param>
		/// <param name="direction">A <see cref="T:System.Boolean" /> indicating the search direction; either true to search forward or false to search backward.</param>
		// Token: 0x06000D6E RID: 3438 RVA: 0x00022530 File Offset: 0x00020730
		public int FindAssignedEditPositionInRange(int startPosition, int endPosition, bool direction)
		{
			if (startPosition < 0)
			{
				startPosition = 0;
			}
			if (endPosition >= this.edit_positions.Length)
			{
				endPosition = this.edit_positions.Length - 1;
			}
			if (startPosition > endPosition)
			{
				return MaskedTextProvider.InvalidIndex;
			}
			int num = (!direction) ? -1 : 1;
			int num2 = (!direction) ? endPosition : startPosition;
			int num3 = ((!direction) ? startPosition : endPosition) + num;
			for (int num4 = num2; num4 != num3; num4 += num)
			{
				if (this.edit_positions[num4].Editable && this.edit_positions[num4].FilledIn)
				{
					return num4;
				}
			}
			return MaskedTextProvider.InvalidIndex;
		}

		/// <summary>Returns the position of the first editable position after the specified position using the specified search direction.</summary>
		/// <returns>If successful, an <see cref="T:System.Int32" /> representing the zero-based position of the first editable position encountered; otherwise <see cref="P:System.ComponentModel.MaskedTextProvider.InvalidIndex" />.</returns>
		/// <param name="position">The zero-based position in the formatted string to start the search.</param>
		/// <param name="direction">A <see cref="T:System.Boolean" /> indicating the search direction; either true to search forward or false to search backward.</param>
		// Token: 0x06000D6F RID: 3439 RVA: 0x000225D8 File Offset: 0x000207D8
		public int FindEditPositionFrom(int position, bool direction)
		{
			if (direction)
			{
				return this.FindEditPositionInRange(position, this.edit_positions.Length - 1, direction);
			}
			return this.FindEditPositionInRange(0, position, direction);
		}

		/// <summary>Returns the position of the first editable position between the specified positions using the specified search direction.</summary>
		/// <returns>If successful, an <see cref="T:System.Int32" /> representing the zero-based position of the first editable position encountered; otherwise <see cref="P:System.ComponentModel.MaskedTextProvider.InvalidIndex" />.</returns>
		/// <param name="startPosition">The zero-based position in the formatted string where the search starts.</param>
		/// <param name="endPosition">The zero-based position in the formatted string where the search ends.</param>
		/// <param name="direction">A <see cref="T:System.Boolean" /> indicating the search direction; either true to search forward or false to search backward.</param>
		// Token: 0x06000D70 RID: 3440 RVA: 0x00022608 File Offset: 0x00020808
		public int FindEditPositionInRange(int startPosition, int endPosition, bool direction)
		{
			if (startPosition < 0)
			{
				startPosition = 0;
			}
			if (endPosition >= this.edit_positions.Length)
			{
				endPosition = this.edit_positions.Length - 1;
			}
			if (startPosition > endPosition)
			{
				return MaskedTextProvider.InvalidIndex;
			}
			int num = (!direction) ? -1 : 1;
			int num2 = (!direction) ? endPosition : startPosition;
			int num3 = ((!direction) ? startPosition : endPosition) + num;
			for (int num4 = num2; num4 != num3; num4 += num)
			{
				if (this.edit_positions[num4].Editable)
				{
					return num4;
				}
			}
			return MaskedTextProvider.InvalidIndex;
		}

		/// <summary>Returns the position of the first non-editable position after the specified position using the specified search direction.</summary>
		/// <returns>If successful, an <see cref="T:System.Int32" /> representing the zero-based position of the first literal position encountered; otherwise <see cref="P:System.ComponentModel.MaskedTextProvider.InvalidIndex" />.</returns>
		/// <param name="position">The zero-based position in the formatted string to start the search.</param>
		/// <param name="direction">A <see cref="T:System.Boolean" /> indicating the search direction; either true to search forward or false to search backward.</param>
		// Token: 0x06000D71 RID: 3441 RVA: 0x000226A0 File Offset: 0x000208A0
		public int FindNonEditPositionFrom(int position, bool direction)
		{
			if (direction)
			{
				return this.FindNonEditPositionInRange(position, this.edit_positions.Length - 1, direction);
			}
			return this.FindNonEditPositionInRange(0, position, direction);
		}

		/// <summary>Returns the position of the first non-editable position between the specified positions using the specified search direction.</summary>
		/// <returns>If successful, an <see cref="T:System.Int32" /> representing the zero-based position of the first literal position encountered; otherwise <see cref="P:System.ComponentModel.MaskedTextProvider.InvalidIndex" />.</returns>
		/// <param name="startPosition">The zero-based position in the formatted string where the search starts.</param>
		/// <param name="endPosition">The zero-based position in the formatted string where the search ends.</param>
		/// <param name="direction">A <see cref="T:System.Boolean" /> indicating the search direction; either true to search forward or false to search backward.</param>
		// Token: 0x06000D72 RID: 3442 RVA: 0x000226D0 File Offset: 0x000208D0
		public int FindNonEditPositionInRange(int startPosition, int endPosition, bool direction)
		{
			if (startPosition < 0)
			{
				startPosition = 0;
			}
			if (endPosition >= this.edit_positions.Length)
			{
				endPosition = this.edit_positions.Length - 1;
			}
			if (startPosition > endPosition)
			{
				return MaskedTextProvider.InvalidIndex;
			}
			int num = (!direction) ? -1 : 1;
			int num2 = (!direction) ? endPosition : startPosition;
			int num3 = ((!direction) ? startPosition : endPosition) + num;
			for (int num4 = num2; num4 != num3; num4 += num)
			{
				if (!this.edit_positions[num4].Editable)
				{
					return num4;
				}
			}
			return MaskedTextProvider.InvalidIndex;
		}

		/// <summary>Returns the position of the first unassigned editable position after the specified position using the specified search direction.</summary>
		/// <returns>If successful, an <see cref="T:System.Int32" /> representing the zero-based position of the first unassigned editable position encountered; otherwise <see cref="P:System.ComponentModel.MaskedTextProvider.InvalidIndex" />.</returns>
		/// <param name="position">The zero-based position in the formatted string to start the search.</param>
		/// <param name="direction">A <see cref="T:System.Boolean" /> indicating the search direction; either true to search forward or false to search backward.</param>
		// Token: 0x06000D73 RID: 3443 RVA: 0x00022768 File Offset: 0x00020968
		public int FindUnassignedEditPositionFrom(int position, bool direction)
		{
			if (direction)
			{
				return this.FindUnassignedEditPositionInRange(position, this.edit_positions.Length - 1, direction);
			}
			return this.FindUnassignedEditPositionInRange(0, position, direction);
		}

		/// <summary>Returns the position of the first unassigned editable position between the specified positions using the specified search direction.</summary>
		/// <returns>If successful, an <see cref="T:System.Int32" /> representing the zero-based position of the first unassigned editable position encountered; otherwise <see cref="P:System.ComponentModel.MaskedTextProvider.InvalidIndex" />.</returns>
		/// <param name="startPosition">The zero-based position in the formatted string where the search starts.</param>
		/// <param name="endPosition">The zero-based position in the formatted string where the search ends.</param>
		/// <param name="direction">A <see cref="T:System.Boolean" /> indicating the search direction; either true to search forward or false to search backward.</param>
		// Token: 0x06000D74 RID: 3444 RVA: 0x00022798 File Offset: 0x00020998
		public int FindUnassignedEditPositionInRange(int startPosition, int endPosition, bool direction)
		{
			if (startPosition < 0)
			{
				startPosition = 0;
			}
			if (endPosition >= this.edit_positions.Length)
			{
				endPosition = this.edit_positions.Length - 1;
			}
			if (startPosition > endPosition)
			{
				return MaskedTextProvider.InvalidIndex;
			}
			int num = (!direction) ? -1 : 1;
			int num2 = (!direction) ? endPosition : startPosition;
			int num3 = ((!direction) ? startPosition : endPosition) + num;
			for (int num4 = num2; num4 != num3; num4 += num)
			{
				if (this.edit_positions[num4].Editable && !this.edit_positions[num4].FilledIn)
				{
					return num4;
				}
			}
			return MaskedTextProvider.InvalidIndex;
		}

		/// <summary>Determines whether the specified <see cref="T:System.ComponentModel.MaskedTextResultHint" /> denotes success or failure.</summary>
		/// <returns>true if the specified <see cref="T:System.ComponentModel.MaskedTextResultHint" /> value represents a success; otherwise, false if it represents failure.</returns>
		/// <param name="hint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> value typically obtained as an output parameter from a previous operation. </param>
		// Token: 0x06000D75 RID: 3445 RVA: 0x00022840 File Offset: 0x00020A40
		public static bool GetOperationResultFromHint(MaskedTextResultHint hint)
		{
			return hint == MaskedTextResultHint.CharacterEscaped || hint == MaskedTextResultHint.NoEffect || hint == MaskedTextResultHint.SideEffect || hint == MaskedTextResultHint.Success;
		}

		/// <summary>Inserts the specified character at the specified position within the formatted string.</summary>
		/// <returns>true if the insertion was successful; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.Char" /> to be inserted. </param>
		/// <param name="position">The zero-based position in the formatted string to insert the character.</param>
		// Token: 0x06000D76 RID: 3446 RVA: 0x00022860 File Offset: 0x00020A60
		public bool InsertAt(char input, int position)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.InsertAt(input, position, out num, out maskedTextResultHint);
		}

		/// <summary>Inserts the specified string at a specified position within the formatted string. </summary>
		/// <returns>true if the insertion was successful; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.String" /> to be inserted. </param>
		/// <param name="position">The zero-based position in the formatted string to insert the input string.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> parameter is null.</exception>
		// Token: 0x06000D77 RID: 3447 RVA: 0x0002287C File Offset: 0x00020A7C
		public bool InsertAt(string input, int position)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.InsertAt(input, position, out num, out maskedTextResultHint);
		}

		/// <summary>Inserts the specified character at the specified position within the formatted string, returning the last insertion position and the status of the operation.</summary>
		/// <returns>true if the insertion was successful; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.Char" /> to be inserted. </param>
		/// <param name="position">The zero-based position in the formatted string to insert the character.</param>
		/// <param name="testPosition">If the method is successful, the last position where a character was inserted; otherwise, the first position where the insertion failed. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the insertion operation. An output parameter.</param>
		// Token: 0x06000D78 RID: 3448 RVA: 0x00022898 File Offset: 0x00020A98
		public bool InsertAt(char input, int position, out int testPosition, out MaskedTextResultHint resultHint)
		{
			return this.InsertAt(input.ToString(), position, out testPosition, out resultHint);
		}

		/// <summary>Inserts the specified string at a specified position within the formatted string, returning the last insertion position and the status of the operation. </summary>
		/// <returns>true if the insertion was successful; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.String" /> to be inserted. </param>
		/// <param name="position">The zero-based position in the formatted string to insert the input string.</param>
		/// <param name="testPosition">If the method is successful, the last position where a character was inserted; otherwise, the first position where the insertion failed. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the insertion operation. An output parameter.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> parameter is null.</exception>
		// Token: 0x06000D79 RID: 3449 RVA: 0x000228AC File Offset: 0x00020AAC
		public bool InsertAt(string input, int position, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (position >= this.edit_positions.Length)
			{
				testPosition = position;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			if (input == string.Empty)
			{
				testPosition = position;
				resultHint = MaskedTextResultHint.NoEffect;
				return true;
			}
			bool flag = this.InsertAtInternal(input, position, out testPosition, out resultHint, true);
			if (flag)
			{
				flag = this.InsertAtInternal(input, position, out testPosition, out resultHint, false);
			}
			return flag;
		}

		/// <summary>Determines whether the specified position is available for assignment.</summary>
		/// <returns>true if the specified position in the formatted string is editable and has not been assigned to yet; otherwise false.</returns>
		/// <param name="position">The zero-based position in the mask to test.</param>
		// Token: 0x06000D7A RID: 3450 RVA: 0x0002291C File Offset: 0x00020B1C
		public bool IsAvailablePosition(int position)
		{
			return position >= 0 && position < this.edit_positions.Length && this.edit_positions[position].Editable && !this.edit_positions[position].FilledIn;
		}

		/// <summary>Determines whether the specified position is editable. </summary>
		/// <returns>true if the specified position in the formatted string is editable; otherwise false.</returns>
		/// <param name="position">The zero-based position in the mask to test.</param>
		// Token: 0x06000D7B RID: 3451 RVA: 0x00022968 File Offset: 0x00020B68
		public bool IsEditPosition(int position)
		{
			return position >= 0 && position < this.edit_positions.Length && this.edit_positions[position].Editable;
		}

		/// <summary>Determines whether the specified character is a valid input character.</summary>
		/// <returns>true if the specified character contains a valid input value; otherwise false.</returns>
		/// <param name="c">The <see cref="T:System.Char" /> value to test.</param>
		// Token: 0x06000D7C RID: 3452 RVA: 0x0002299C File Offset: 0x00020B9C
		public static bool IsValidInputChar(char c)
		{
			return char.IsLetterOrDigit(c) || char.IsPunctuation(c) || char.IsSymbol(c) || c == ' ';
		}

		/// <summary>Determines whether the specified character is a valid mask character.</summary>
		/// <returns>true if the specified character contains a valid mask value; otherwise false.</returns>
		/// <param name="c">The <see cref="T:System.Char" /> value to test.</param>
		// Token: 0x06000D7D RID: 3453 RVA: 0x000229C8 File Offset: 0x00020BC8
		public static bool IsValidMaskChar(char c)
		{
			return char.IsLetterOrDigit(c) || char.IsPunctuation(c) || char.IsSymbol(c) || c == ' ';
		}

		/// <summary>Determines whether the specified character is a valid password character.</summary>
		/// <returns>true if the specified character contains a valid password value; otherwise false.</returns>
		/// <param name="c">The <see cref="T:System.Char" /> value to test.</param>
		// Token: 0x06000D7E RID: 3454 RVA: 0x000229F4 File Offset: 0x00020BF4
		public static bool IsValidPasswordChar(char c)
		{
			return char.IsLetterOrDigit(c) || char.IsPunctuation(c) || char.IsSymbol(c) || c == ' ' || c == '\0';
		}

		/// <summary>Removes the last assigned character from the formatted string.</summary>
		/// <returns>true if the character was successfully removed; otherwise, false.</returns>
		// Token: 0x06000D7F RID: 3455 RVA: 0x00022A34 File Offset: 0x00020C34
		public bool Remove()
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.Remove(out num, out maskedTextResultHint);
		}

		/// <summary>Removes the last assigned character from the formatted string, and then outputs the removal position and descriptive information.</summary>
		/// <returns>true if the character was successfully removed; otherwise, false.</returns>
		/// <param name="testPosition">The zero-based position in the formatted string where the character was actually removed. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the operation. An output parameter.</param>
		// Token: 0x06000D80 RID: 3456 RVA: 0x00022A4C File Offset: 0x00020C4C
		public bool Remove(out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (this.LastAssignedPosition == MaskedTextProvider.InvalidIndex)
			{
				resultHint = MaskedTextResultHint.NoEffect;
				testPosition = 0;
				return true;
			}
			testPosition = this.LastAssignedPosition;
			resultHint = MaskedTextResultHint.Success;
			this.edit_positions[this.LastAssignedPosition].input = '\0';
			return true;
		}

		/// <summary>Removes the assigned character at the specified position from the formatted string.</summary>
		/// <returns>true if the character was successfully removed; otherwise, false.</returns>
		/// <param name="position">The zero-based position of the assigned character to remove.</param>
		// Token: 0x06000D81 RID: 3457 RVA: 0x00022A90 File Offset: 0x00020C90
		public bool RemoveAt(int position)
		{
			return this.RemoveAt(position, position);
		}

		/// <summary>Removes the assigned characters between the specified positions from the formatted string.</summary>
		/// <returns>true if the character was successfully removed; otherwise, false.</returns>
		/// <param name="startPosition">The zero-based index of the first assigned character to remove.</param>
		/// <param name="endPosition">The zero-based index of the last assigned character to remove.</param>
		// Token: 0x06000D82 RID: 3458 RVA: 0x00022A9C File Offset: 0x00020C9C
		public bool RemoveAt(int startPosition, int endPosition)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.RemoveAt(startPosition, endPosition, out num, out maskedTextResultHint);
		}

		/// <summary>Removes the assigned characters between the specified positions from the formatted string, and then outputs the removal position and descriptive information.</summary>
		/// <returns>true if the character was successfully removed; otherwise, false.</returns>
		/// <param name="startPosition">The zero-based index of the first assigned character to remove.</param>
		/// <param name="endPosition">The zero-based index of the last assigned character to remove.</param>
		/// <param name="testPosition">If successful, the zero-based position in the formatted string of where the characters were actually removed; otherwise, the first position where the operation failed. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the operation. An output parameter.</param>
		// Token: 0x06000D83 RID: 3459 RVA: 0x00022AB8 File Offset: 0x00020CB8
		public bool RemoveAt(int startPosition, int endPosition, out int testPosition, out MaskedTextResultHint resultHint)
		{
			bool flag = this.RemoveAtInternal(startPosition, endPosition, out testPosition, out resultHint, true);
			if (flag)
			{
				flag = this.RemoveAtInternal(startPosition, endPosition, out testPosition, out resultHint, false);
			}
			return flag;
		}

		/// <summary>Replaces a single character at or beyond the specified position with the specified character value.</summary>
		/// <returns>true if the character was successfully replaced; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.Char" /> value that replaces the existing value.</param>
		/// <param name="position">The zero-based position to search for the first editable character to replace.</param>
		// Token: 0x06000D84 RID: 3460 RVA: 0x00022AE8 File Offset: 0x00020CE8
		public bool Replace(char input, int position)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.Replace(input, position, out num, out maskedTextResultHint);
		}

		/// <summary>Replaces a range of editable characters starting at the specified position with the specified string.</summary>
		/// <returns>true if all the characters were successfully replaced; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.String" /> value used to replace the existing editable characters.</param>
		/// <param name="position">The zero-based position to search for the first editable character to replace.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> parameter is null.</exception>
		// Token: 0x06000D85 RID: 3461 RVA: 0x00022B04 File Offset: 0x00020D04
		public bool Replace(string input, int position)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.Replace(input, position, out num, out maskedTextResultHint);
		}

		/// <summary>Replaces a single character at or beyond the specified position with the specified character value, and then outputs the removal position and descriptive information.</summary>
		/// <returns>true if the character was successfully replaced; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.Char" /> value that replaces the existing value.</param>
		/// <param name="position">The zero-based position to search for the first editable character to replace.</param>
		/// <param name="testPosition">If successful, the zero-based position in the formatted string where the last character was actually replaced; otherwise, the first position where the operation failed. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the replacement operation. An output parameter.</param>
		// Token: 0x06000D86 RID: 3462 RVA: 0x00022B20 File Offset: 0x00020D20
		public bool Replace(char input, int position, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (position < 0 || position >= this.edit_positions.Length)
			{
				testPosition = position;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			if (this.VerifyEscapeChar(input, position))
			{
				if ((this.edit_positions[position].FilledIn && this.edit_positions[position].Editable && input == ' ' && this.ResetOnSpace) || (input == this.PromptChar && this.ResetOnPrompt))
				{
					this.edit_positions[position].Reset();
					resultHint = MaskedTextResultHint.SideEffect;
				}
				else
				{
					resultHint = MaskedTextResultHint.CharacterEscaped;
				}
				testPosition = position;
				return true;
			}
			int num = this.FindEditPositionFrom(position, true);
			if (num == MaskedTextProvider.InvalidIndex)
			{
				testPosition = position;
				resultHint = MaskedTextResultHint.UnavailableEditPosition;
				return false;
			}
			if (!MaskedTextProvider.IsValidInputChar(input))
			{
				testPosition = num;
				resultHint = MaskedTextResultHint.InvalidInput;
				return false;
			}
			return this.ReplaceInternal(this.edit_positions, input, num, out testPosition, out resultHint, false);
		}

		/// <summary>Replaces a range of editable characters starting at the specified position with the specified string, and then outputs the removal position and descriptive information.</summary>
		/// <returns>true if all the characters were successfully replaced; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.String" /> value used to replace the existing editable characters.</param>
		/// <param name="position">The zero-based position to search for the first editable character to replace.</param>
		/// <param name="testPosition">If successful, the zero-based position in the formatted string where the last character was actually replaced; otherwise, the first position where the operation failed. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the replacement operation. An output parameter.</param>
		// Token: 0x06000D87 RID: 3463 RVA: 0x00022C0C File Offset: 0x00020E0C
		public bool Replace(string input, int position, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (position < 0 || position >= this.edit_positions.Length)
			{
				testPosition = position;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			if (input.Length == 0)
			{
				return this.RemoveAt(position, position, out testPosition, out resultHint);
			}
			bool flag = this.ReplaceInternal(input, position, this.edit_positions.Length - 1, out testPosition, out resultHint, true, true);
			if (flag)
			{
				flag = this.ReplaceInternal(input, position, this.edit_positions.Length - 1, out testPosition, out resultHint, false, true);
			}
			return flag;
		}

		/// <summary>Replaces a single character between the specified starting and ending positions with the specified character value, and then outputs the removal position and descriptive information.</summary>
		/// <returns>true if the character was successfully replaced; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.Char" /> value that replaces the existing value.</param>
		/// <param name="startPosition">The zero-based position in the formatted string where the replacement starts. </param>
		/// <param name="endPosition">The zero-based position in the formatted string where the replacement ends. </param>
		/// <param name="testPosition">If successful, the zero-based position in the formatted string where the last character was actually replaced; otherwise, the first position where the operation failed. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the replacement operation. An output parameter.</param>
		// Token: 0x06000D88 RID: 3464 RVA: 0x00022C98 File Offset: 0x00020E98
		public bool Replace(char input, int startPosition, int endPosition, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (endPosition >= this.edit_positions.Length)
			{
				testPosition = endPosition;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			if (startPosition < 0)
			{
				testPosition = startPosition;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			if (startPosition > endPosition)
			{
				testPosition = startPosition;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			if (startPosition == endPosition)
			{
				return this.ReplaceInternal(this.edit_positions, input, startPosition, out testPosition, out resultHint, false);
			}
			return this.Replace(input.ToString(), startPosition, endPosition, out testPosition, out resultHint);
		}

		/// <summary>Replaces a range of editable characters between the specified starting and ending positions with the specified string, and then outputs the removal position and descriptive information.</summary>
		/// <returns>true if all the characters were successfully replaced; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.String" /> value used to replace the existing editable characters.</param>
		/// <param name="startPosition">The zero-based position in the formatted string where the replacement starts. </param>
		/// <param name="endPosition">The zero-based position in the formatted string where the replacement ends. </param>
		/// <param name="testPosition">If successful, the zero-based position in the formatted string where the last character was actually replaced; otherwise, the first position where the operation failed. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the replacement operation. An output parameter.</param>
		// Token: 0x06000D89 RID: 3465 RVA: 0x00022D10 File Offset: 0x00020F10
		public bool Replace(string input, int startPosition, int endPosition, out int testPosition, out MaskedTextResultHint resultHint)
		{
			bool flag = this.ReplaceInternal(input, startPosition, endPosition, out testPosition, out resultHint, true, false);
			if (flag)
			{
				flag = this.ReplaceInternal(input, startPosition, endPosition, out testPosition, out resultHint, false, false);
			}
			return flag;
		}

		/// <summary>Sets the formatted string to the specified input string.</summary>
		/// <returns>true if all the characters were successfully set; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.String" /> value used to set the formatted string.</param>
		/// <exception cref="T:System.ArgumentNullException">The<paramref name=" input" /> parameter is null.</exception>
		// Token: 0x06000D8A RID: 3466 RVA: 0x00022D44 File Offset: 0x00020F44
		public bool Set(string input)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.Set(input, out num, out maskedTextResultHint);
		}

		/// <summary>Sets the formatted string to the specified input string, and then outputs the removal position and descriptive information.</summary>
		/// <returns>true if all the characters were successfully set; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.String" /> value used to set the formatted string.</param>
		/// <param name="testPosition">If successful, the zero-based position in the formatted string where the last character was actually set; otherwise, the first position where the operation failed. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the set operation. An output parameter.</param>
		/// <exception cref="T:System.ArgumentNullException">The<paramref name=" input" /> parameter is null.</exception>
		// Token: 0x06000D8B RID: 3467 RVA: 0x00022D5C File Offset: 0x00020F5C
		public bool Set(string input, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			bool flag = this.VerifyStringInternal(input, out testPosition, out resultHint, 0, true);
			if (flag)
			{
				flag = this.VerifyStringInternal(input, out testPosition, out resultHint, 0, false);
			}
			return flag;
		}

		/// <summary>Returns the formatted string in a displayable form.</summary>
		/// <returns>The formatted <see cref="T:System.String" /> that includes prompts and mask literals.</returns>
		// Token: 0x06000D8C RID: 3468 RVA: 0x00022D9C File Offset: 0x00020F9C
		public string ToDisplayString()
		{
			return this.ToString(false, true, true, 0, this.Length);
		}

		/// <summary>Returns the formatted string that includes all the assigned character values.</summary>
		/// <returns>The formatted <see cref="T:System.String" /> that includes all the assigned character values.</returns>
		// Token: 0x06000D8D RID: 3469 RVA: 0x00022DBC File Offset: 0x00020FBC
		public override string ToString()
		{
			return this.ToString(true, this.IncludePrompt, this.IncludeLiterals, 0, this.Length);
		}

		/// <summary>Returns the formatted string, optionally including password characters.</summary>
		/// <returns>The formatted <see cref="T:System.String" /> that includes literals, prompts, and optionally password characters.</returns>
		/// <param name="ignorePasswordChar">true to return the actual editable characters; otherwise, false to indicate that the <see cref="P:System.ComponentModel.MaskedTextProvider.PasswordChar" /> property is to be honored.</param>
		// Token: 0x06000D8E RID: 3470 RVA: 0x00022DE4 File Offset: 0x00020FE4
		public string ToString(bool ignorePasswordChar)
		{
			return this.ToString(ignorePasswordChar, this.IncludePrompt, this.IncludeLiterals, 0, this.Length);
		}

		/// <summary>Returns the formatted string, optionally including prompt and literal characters.</summary>
		/// <returns>The formatted <see cref="T:System.String" /> that includes all the assigned character values and optionally includes literals and prompts.</returns>
		/// <param name="includePrompt">true to include prompt characters in the return string; otherwise, false.</param>
		/// <param name="includeLiterals">true to include literal characters in the return string; otherwise, false.</param>
		// Token: 0x06000D8F RID: 3471 RVA: 0x00022E0C File Offset: 0x0002100C
		public string ToString(bool includePrompt, bool includeLiterals)
		{
			return this.ToString(true, includePrompt, includeLiterals, 0, this.Length);
		}

		/// <summary>Returns a substring of the formatted string.</summary>
		/// <returns>If successful, a substring of the formatted <see cref="T:System.String" />, which includes all the assigned character values; otherwise the <see cref="F:System.String.Empty" /> string.</returns>
		/// <param name="startPosition">The zero-based position in the formatted string where the output begins. </param>
		/// <param name="length">The number of characters to return.</param>
		// Token: 0x06000D90 RID: 3472 RVA: 0x00022E2C File Offset: 0x0002102C
		public string ToString(int startPosition, int length)
		{
			return this.ToString(true, this.IncludePrompt, this.IncludeLiterals, startPosition, length);
		}

		/// <summary>Returns a substring of the formatted string, optionally including password characters.</summary>
		/// <returns>If successful, a substring of the formatted <see cref="T:System.String" />, which includes literals, prompts, and optionally password characters; otherwise the <see cref="F:System.String.Empty" /> string.</returns>
		/// <param name="ignorePasswordChar">true to return the actual editable characters; otherwise, false to indicate that the <see cref="P:System.ComponentModel.MaskedTextProvider.PasswordChar" /> property is to be honored.</param>
		/// <param name="startPosition">The zero-based position in the formatted string where the output begins. </param>
		/// <param name="length">The number of characters to return.</param>
		// Token: 0x06000D91 RID: 3473 RVA: 0x00022E50 File Offset: 0x00021050
		public string ToString(bool ignorePasswordChar, int startPosition, int length)
		{
			return this.ToString(ignorePasswordChar, this.IncludePrompt, this.IncludeLiterals, startPosition, length);
		}

		/// <summary>Returns a substring of the formatted string, optionally including prompt and literal characters.</summary>
		/// <returns>If successful, a substring of the formatted <see cref="T:System.String" />, which includes all the assigned character values and optionally includes literals and prompts; otherwise the <see cref="F:System.String.Empty" /> string.</returns>
		/// <param name="includePrompt">true to include prompt characters in the return string; otherwise, false.</param>
		/// <param name="includeLiterals">true to include literal characters in the return string; otherwise, false.</param>
		/// <param name="startPosition">The zero-based position in the formatted string where the output begins. </param>
		/// <param name="length">The number of characters to return.</param>
		// Token: 0x06000D92 RID: 3474 RVA: 0x00022E74 File Offset: 0x00021074
		public string ToString(bool includePrompt, bool includeLiterals, int startPosition, int length)
		{
			return this.ToString(true, includePrompt, includeLiterals, startPosition, length);
		}

		/// <summary>Returns a substring of the formatted string, optionally including prompt, literal, and password characters.</summary>
		/// <returns>If successful, a substring of the formatted <see cref="T:System.String" />, which includes all the assigned character values and optionally includes literals, prompts, and password characters; otherwise the <see cref="F:System.String.Empty" /> string.</returns>
		/// <param name="ignorePasswordChar">true to return the actual editable characters; otherwise, false to indicate that the <see cref="P:System.ComponentModel.MaskedTextProvider.PasswordChar" /> property is to be honored.</param>
		/// <param name="includePrompt">true to include prompt characters in the return string; otherwise, false.</param>
		/// <param name="includeLiterals">true to return literal characters in the return string; otherwise, false.</param>
		/// <param name="startPosition">The zero-based position in the formatted string where the output begins. </param>
		/// <param name="length">The number of characters to return.</param>
		// Token: 0x06000D93 RID: 3475 RVA: 0x00022E84 File Offset: 0x00021084
		public string ToString(bool ignorePasswordChar, bool includePrompt, bool includeLiterals, int startPosition, int length)
		{
			if (startPosition < 0)
			{
				startPosition = 0;
			}
			if (length <= 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int num = startPosition;
			int num2 = startPosition + length - 1;
			if (num2 >= this.edit_positions.Length)
			{
				num2 = this.edit_positions.Length - 1;
			}
			int num3 = this.FindAssignedEditPositionInRange(num, num2, false);
			if (!includePrompt)
			{
				int num4 = this.FindNonEditPositionInRange(num, num2, false);
				if (includeLiterals)
				{
					num2 = ((num3 <= num4) ? num4 : num3);
				}
				else
				{
					num2 = num3;
				}
			}
			for (int i = num; i <= num2; i++)
			{
				MaskedTextProvider.EditPosition editPosition = this.edit_positions[i];
				if (editPosition.Type == MaskedTextProvider.EditType.Literal)
				{
					if (includeLiterals)
					{
						stringBuilder.Append(editPosition.Text);
					}
				}
				else if (editPosition.Editable)
				{
					if (this.IsPassword)
					{
						if (ignorePasswordChar)
						{
							if (!editPosition.FilledIn)
							{
								if (includePrompt)
								{
									stringBuilder.Append(this.PromptChar);
								}
								else
								{
									stringBuilder.Append(" ");
								}
							}
							else
							{
								stringBuilder.Append(editPosition.Input);
							}
						}
						else
						{
							stringBuilder.Append(this.PasswordChar);
						}
					}
					else if (!editPosition.FilledIn)
					{
						if (includePrompt)
						{
							stringBuilder.Append(this.PromptChar);
						}
						else if (includeLiterals)
						{
							stringBuilder.Append(" ");
						}
						else if (num3 != MaskedTextProvider.InvalidIndex && num3 > i)
						{
							stringBuilder.Append(" ");
						}
					}
					else
					{
						stringBuilder.Append(editPosition.Text);
					}
				}
				else if (includeLiterals)
				{
					stringBuilder.Append(editPosition.Text);
				}
			}
			return stringBuilder.ToString();
		}

		/// <summary>Tests whether the specified character could be set successfully at the specified position.</summary>
		/// <returns>true if the specified character is valid for the specified position; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.Char" /> value to test.</param>
		/// <param name="position">The position in the mask to test the input character against.</param>
		/// <param name="hint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the operation. An output parameter.</param>
		// Token: 0x06000D94 RID: 3476 RVA: 0x00023058 File Offset: 0x00021258
		public bool VerifyChar(char input, int position, out MaskedTextResultHint hint)
		{
			return this.VerifyCharInternal(input, position, out hint, true);
		}

		/// <summary>Tests whether the specified character would be escaped at the specified position.</summary>
		/// <returns>true if the specified character would be escaped at the specified position; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.Char" /> value to test.</param>
		/// <param name="position">The position in the mask to test the input character against.</param>
		// Token: 0x06000D95 RID: 3477 RVA: 0x00023064 File Offset: 0x00021264
		public bool VerifyEscapeChar(char input, int position)
		{
			if (position >= this.edit_positions.Length || position < 0)
			{
				return false;
			}
			if (!this.edit_positions[position].Editable)
			{
				return this.SkipLiterals && input.ToString() == this.edit_positions[position].Text;
			}
			return (this.ResetOnSpace && input == ' ') || (this.ResetOnPrompt && input == this.PromptChar);
		}

		/// <summary>Tests whether the specified string could be set successfully.</summary>
		/// <returns>true if the specified string represents valid input; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.String" /> value to test.</param>
		// Token: 0x06000D96 RID: 3478 RVA: 0x000230F0 File Offset: 0x000212F0
		public bool VerifyString(string input)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.VerifyString(input, out num, out maskedTextResultHint);
		}

		/// <summary>Tests whether the specified string could be set successfully, and then outputs position and descriptive information.</summary>
		/// <returns>true if the specified string represents valid input; otherwise, false. </returns>
		/// <param name="input">The <see cref="T:System.String" /> value to test.</param>
		/// <param name="testPosition">If successful, the zero-based position of the last character actually tested; otherwise, the first position where the test failed. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the test operation. An output parameter.</param>
		// Token: 0x06000D97 RID: 3479 RVA: 0x00023108 File Offset: 0x00021308
		public bool VerifyString(string input, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (input == null || input.Length == 0)
			{
				testPosition = 0;
				resultHint = MaskedTextResultHint.NoEffect;
				return true;
			}
			return this.VerifyStringInternal(input, out testPosition, out resultHint, 0, true);
		}

		/// <summary>Gets a value indicating whether the prompt character should be treated as a valid input character or not.</summary>
		/// <returns>true if the user can enter <see cref="P:System.ComponentModel.MaskedTextProvider.PromptChar" /> into the control; otherwise, false. The default is true. </returns>
		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x0002313C File Offset: 0x0002133C
		public bool AllowPromptAsInput
		{
			get
			{
				return this.allow_prompt_as_input;
			}
		}

		/// <summary>Gets a value indicating whether the mask accepts characters outside of the ASCII character set.</summary>
		/// <returns>true if only ASCII is accepted; false if <see cref="T:System.ComponentModel.MaskedTextProvider" /> can accept any arbitrary Unicode character. The default is false.</returns>
		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000D99 RID: 3481 RVA: 0x00023144 File Offset: 0x00021344
		public bool AsciiOnly
		{
			get
			{
				return this.ascii_only;
			}
		}

		/// <summary>Gets the number of editable character positions that have already been successfully assigned an input value.</summary>
		/// <returns>An <see cref="T:System.Int32" /> containing the number of editable character positions in the input mask that have already been assigned a character value in the formatted string.</returns>
		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000D9A RID: 3482 RVA: 0x0002314C File Offset: 0x0002134C
		public int AssignedEditPositionCount
		{
			get
			{
				int num = 0;
				for (int i = 0; i < this.edit_positions.Length; i++)
				{
					if (this.edit_positions[i].FilledIn)
					{
						num++;
					}
				}
				return num;
			}
		}

		/// <summary>Gets the number of editable character positions in the input mask that have not yet been assigned an input value.</summary>
		/// <returns>An <see cref="T:System.Int32" /> containing the number of editable character positions that not yet been assigned a character value.</returns>
		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000D9B RID: 3483 RVA: 0x0002318C File Offset: 0x0002138C
		public int AvailableEditPositionCount
		{
			get
			{
				int num = 0;
				foreach (MaskedTextProvider.EditPosition editPosition in this.edit_positions)
				{
					if (!editPosition.FilledIn && editPosition.Editable)
					{
						num++;
					}
				}
				return num;
			}
		}

		/// <summary>Gets the culture that determines the value of the localizable separators and placeholders in the input mask.</summary>
		/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> containing the culture information associated with the input mask.</returns>
		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000D9C RID: 3484 RVA: 0x000231D8 File Offset: 0x000213D8
		public CultureInfo Culture
		{
			get
			{
				return this.culture;
			}
		}

		/// <summary>Gets the default password character used obscure user input. </summary>
		/// <returns>A <see cref="T:System.Char" /> that represents the default password character.</returns>
		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000D9D RID: 3485 RVA: 0x000231E0 File Offset: 0x000213E0
		public static char DefaultPasswordChar
		{
			get
			{
				return '*';
			}
		}

		/// <summary>Gets the number of editable positions in the formatted string.</summary>
		/// <returns>An <see cref="T:System.Int32" /> containing the number of editable positions in the formatted string.</returns>
		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000D9E RID: 3486 RVA: 0x000231E4 File Offset: 0x000213E4
		public int EditPositionCount
		{
			get
			{
				int num = 0;
				foreach (MaskedTextProvider.EditPosition editPosition in this.edit_positions)
				{
					if (editPosition.Editable)
					{
						num++;
					}
				}
				return num;
			}
		}

		/// <summary>Gets a newly created enumerator for the editable positions in the formatted string. </summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that supports enumeration over the editable positions in the formatted string.</returns>
		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000D9F RID: 3487 RVA: 0x00023224 File Offset: 0x00021424
		public IEnumerator EditPositions
		{
			get
			{
				List<int> list = new List<int>();
				for (int i = 0; i < this.edit_positions.Length; i++)
				{
					if (this.edit_positions[i].Editable)
					{
						list.Add(i);
					}
				}
				return list.GetEnumerator();
			}
		}

		/// <summary>Gets or sets a value that indicates whether literal characters in the input mask should be included in the formatted string.</summary>
		/// <returns>true if literals are included; otherwise, false. The default is true. </returns>
		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000DA0 RID: 3488 RVA: 0x00023274 File Offset: 0x00021474
		// (set) Token: 0x06000DA1 RID: 3489 RVA: 0x0002327C File Offset: 0x0002147C
		public bool IncludeLiterals
		{
			get
			{
				return this.include_literals;
			}
			set
			{
				this.include_literals = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether <see cref="P:System.Windows.Forms.MaskedTextBox.PromptChar" /> is used to represent the absence of user input when displaying the formatted string. </summary>
		/// <returns>true if the prompt character is used to represent the positions where no user input was provided; otherwise, false. The default is true.</returns>
		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000DA2 RID: 3490 RVA: 0x00023288 File Offset: 0x00021488
		// (set) Token: 0x06000DA3 RID: 3491 RVA: 0x00023290 File Offset: 0x00021490
		public bool IncludePrompt
		{
			get
			{
				return this.include_prompt;
			}
			set
			{
				this.include_prompt = value;
			}
		}

		/// <summary>Gets the upper bound of the range of invalid indexes.</summary>
		/// <returns>A value representing the largest invalid index, as determined by the provider implementation. For example, if the lowest valid index is 0, this property will return -1.</returns>
		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000DA4 RID: 3492 RVA: 0x0002329C File Offset: 0x0002149C
		public static int InvalidIndex
		{
			get
			{
				return -1;
			}
		}

		/// <summary>Gets or sets a value that determines whether password protection should be applied to the formatted string.</summary>
		/// <returns>true if the input string is to be treated as a password string; otherwise, false. The default is false.</returns>
		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x000232A0 File Offset: 0x000214A0
		// (set) Token: 0x06000DA6 RID: 3494 RVA: 0x000232B0 File Offset: 0x000214B0
		public bool IsPassword
		{
			get
			{
				return this.password_char != '\0';
			}
			set
			{
				this.password_char = ((!value) ? '\0' : MaskedTextProvider.DefaultPasswordChar);
			}
		}

		/// <summary>Gets the element at the specified position in the formatted string.</summary>
		/// <returns>The <see cref="T:System.Char" /> at the specified position in the formatted string.</returns>
		/// <param name="index">A zero-based index of the element to retrieve. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than or equal to the <see cref="P:System.ComponentModel.MaskedTextProvider.Length" /> of the mask.</exception>
		// Token: 0x17000317 RID: 791
		public char this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Length)
				{
					throw new IndexOutOfRangeException(index.ToString());
				}
				return this.ToString(true, true, true, 0, this.edit_positions.Length)[index];
			}
		}

		/// <summary>Gets the index in the mask of the rightmost input character that has been assigned to the mask.</summary>
		/// <returns>If at least one input character has been assigned to the mask, an <see cref="T:System.Int32" /> containing the index of rightmost assigned position; otherwise, if no position has been assigned, <see cref="P:System.ComponentModel.MaskedTextProvider.InvalidIndex" />.</returns>
		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000DA8 RID: 3496 RVA: 0x00023314 File Offset: 0x00021514
		public int LastAssignedPosition
		{
			get
			{
				return this.FindAssignedEditPositionFrom(this.edit_positions.Length - 1, false);
			}
		}

		/// <summary>Gets the length of the mask, absent any mask modifier characters.</summary>
		/// <returns>An <see cref="T:System.Int32" /> containing the number of positions in the mask, excluding characters that modify mask input. </returns>
		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x00023328 File Offset: 0x00021528
		public int Length
		{
			get
			{
				int num = 0;
				for (int i = 0; i < this.edit_positions.Length; i++)
				{
					if (this.edit_positions[i].Visible)
					{
						num++;
					}
				}
				return num;
			}
		}

		/// <summary>Gets the input mask.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the full mask.</returns>
		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000DAA RID: 3498 RVA: 0x00023368 File Offset: 0x00021568
		public string Mask
		{
			get
			{
				return this.mask;
			}
		}

		/// <summary>Gets a value indicating whether all required inputs have been entered into the formatted string.</summary>
		/// <returns>true if all required input has been entered into the mask; otherwise, false.</returns>
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x00023370 File Offset: 0x00021570
		public bool MaskCompleted
		{
			get
			{
				for (int i = 0; i < this.edit_positions.Length; i++)
				{
					if (this.edit_positions[i].Required && !this.edit_positions[i].FilledIn)
					{
						return false;
					}
				}
				return true;
			}
		}

		/// <summary>Gets a value indicating whether all required and optional inputs have been entered into the formatted string. </summary>
		/// <returns>true if all required and optional inputs have been entered; otherwise, false. </returns>
		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x000233C0 File Offset: 0x000215C0
		public bool MaskFull
		{
			get
			{
				for (int i = 0; i < this.edit_positions.Length; i++)
				{
					if (this.edit_positions[i].Editable && !this.edit_positions[i].FilledIn)
					{
						return false;
					}
				}
				return true;
			}
		}

		/// <summary>Gets or sets the character to be substituted for the actual input characters.</summary>
		/// <returns>The <see cref="T:System.Char" /> value used as the password character.</returns>
		/// <exception cref="T:System.InvalidOperationException">The password character specified when setting this property is the same as the current prompt character, <see cref="P:System.ComponentModel.MaskedTextProvider.PromptChar" />. The two are required to be different.</exception>
		/// <exception cref="T:System.ArgumentException">The character specified when setting this property is not a valid password character, as determined by the <see cref="M:System.ComponentModel.MaskedTextProvider.IsValidPasswordChar(System.Char)" /> method.</exception>
		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000DAD RID: 3501 RVA: 0x00023410 File Offset: 0x00021610
		// (set) Token: 0x06000DAE RID: 3502 RVA: 0x00023418 File Offset: 0x00021618
		public char PasswordChar
		{
			get
			{
				return this.password_char;
			}
			set
			{
				this.password_char = value;
			}
		}

		/// <summary>Gets or sets the character used to represent the absence of user input for all available edit positions.</summary>
		/// <returns>The character used to prompt the user for input. The default is an underscore (_). </returns>
		/// <exception cref="T:System.InvalidOperationException">The prompt character specified when setting this property is the same as the current password character, <see cref="P:System.ComponentModel.MaskedTextProvider.PasswordChar" />. The two are required to be different.</exception>
		/// <exception cref="T:System.ArgumentException">The character specified when setting this property is not a valid password character, as determined by the <see cref="M:System.ComponentModel.MaskedTextProvider.IsValidPasswordChar(System.Char)" /> method.</exception>
		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000DAF RID: 3503 RVA: 0x00023424 File Offset: 0x00021624
		// (set) Token: 0x06000DB0 RID: 3504 RVA: 0x0002342C File Offset: 0x0002162C
		public char PromptChar
		{
			get
			{
				return this.prompt_char;
			}
			set
			{
				this.prompt_char = value;
			}
		}

		/// <summary>Gets or sets a value that determines how an input character that matches the prompt character should be handled.</summary>
		/// <returns>true if the prompt character entered as input causes the current editable position in the mask to be reset; otherwise, false to indicate that the prompt character is to be processed as a normal input character. The default is true.</returns>
		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x00023438 File Offset: 0x00021638
		// (set) Token: 0x06000DB2 RID: 3506 RVA: 0x00023440 File Offset: 0x00021640
		public bool ResetOnPrompt
		{
			get
			{
				return this.reset_on_prompt;
			}
			set
			{
				this.reset_on_prompt = value;
			}
		}

		/// <summary>Gets or sets a value that determines how a space input character should be handled.</summary>
		/// <returns>true if the space input character causes the current editable position in the mask to be reset; otherwise, false to indicate that it is to be processed as a normal input character. The default is true.</returns>
		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000DB3 RID: 3507 RVA: 0x0002344C File Offset: 0x0002164C
		// (set) Token: 0x06000DB4 RID: 3508 RVA: 0x00023454 File Offset: 0x00021654
		public bool ResetOnSpace
		{
			get
			{
				return this.reset_on_space;
			}
			set
			{
				this.reset_on_space = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether literal character positions in the mask can be overwritten by their same values.</summary>
		/// <returns>true to allow literals to be added back; otherwise, false to not allow the user to overwrite literal characters. The default is true.</returns>
		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000DB5 RID: 3509 RVA: 0x00023460 File Offset: 0x00021660
		// (set) Token: 0x06000DB6 RID: 3510 RVA: 0x00023468 File Offset: 0x00021668
		public bool SkipLiterals
		{
			get
			{
				return this.skip_literals;
			}
			set
			{
				this.skip_literals = value;
			}
		}

		// Token: 0x040003AF RID: 943
		private bool allow_prompt_as_input;

		// Token: 0x040003B0 RID: 944
		private bool ascii_only;

		// Token: 0x040003B1 RID: 945
		private CultureInfo culture;

		// Token: 0x040003B2 RID: 946
		private bool include_literals;

		// Token: 0x040003B3 RID: 947
		private bool include_prompt;

		// Token: 0x040003B4 RID: 948
		private bool is_password;

		// Token: 0x040003B5 RID: 949
		private string mask;

		// Token: 0x040003B6 RID: 950
		private char password_char;

		// Token: 0x040003B7 RID: 951
		private char prompt_char;

		// Token: 0x040003B8 RID: 952
		private bool reset_on_prompt;

		// Token: 0x040003B9 RID: 953
		private bool reset_on_space;

		// Token: 0x040003BA RID: 954
		private bool skip_literals;

		// Token: 0x040003BB RID: 955
		private MaskedTextProvider.EditPosition[] edit_positions;

		// Token: 0x040003BC RID: 956
		private static char default_prompt_char = '_';

		// Token: 0x040003BD RID: 957
		private static char default_password_char;

		// Token: 0x02000186 RID: 390
		private enum EditState
		{
			// Token: 0x040003BF RID: 959
			None,
			// Token: 0x040003C0 RID: 960
			UpperCase,
			// Token: 0x040003C1 RID: 961
			LowerCase
		}

		// Token: 0x02000187 RID: 391
		private enum EditType
		{
			// Token: 0x040003C3 RID: 963
			DigitRequired,
			// Token: 0x040003C4 RID: 964
			DigitOrSpaceOptional,
			// Token: 0x040003C5 RID: 965
			DigitOrSpaceOptional_Blank,
			// Token: 0x040003C6 RID: 966
			LetterRequired,
			// Token: 0x040003C7 RID: 967
			LetterOptional,
			// Token: 0x040003C8 RID: 968
			CharacterRequired,
			// Token: 0x040003C9 RID: 969
			CharacterOptional,
			// Token: 0x040003CA RID: 970
			AlphanumericRequired,
			// Token: 0x040003CB RID: 971
			AlphanumericOptional,
			// Token: 0x040003CC RID: 972
			DecimalPlaceholder,
			// Token: 0x040003CD RID: 973
			ThousandsPlaceholder,
			// Token: 0x040003CE RID: 974
			TimeSeparator,
			// Token: 0x040003CF RID: 975
			DateSeparator,
			// Token: 0x040003D0 RID: 976
			CurrencySymbol,
			// Token: 0x040003D1 RID: 977
			Literal
		}

		// Token: 0x02000188 RID: 392
		private class EditPosition
		{
			// Token: 0x06000DB7 RID: 3511 RVA: 0x00023474 File Offset: 0x00021674
			private EditPosition()
			{
			}

			// Token: 0x06000DB8 RID: 3512 RVA: 0x0002347C File Offset: 0x0002167C
			public EditPosition(MaskedTextProvider Parent, MaskedTextProvider.EditType Type, MaskedTextProvider.EditState State, char MaskCharacter)
			{
				this.Type = Type;
				this.Parent = Parent;
				this.State = State;
				this.MaskCharacter = MaskCharacter;
			}

			// Token: 0x06000DB9 RID: 3513 RVA: 0x000234A4 File Offset: 0x000216A4
			public void Reset()
			{
				this.input = '\0';
			}

			// Token: 0x06000DBA RID: 3514 RVA: 0x000234B0 File Offset: 0x000216B0
			internal MaskedTextProvider.EditPosition Clone()
			{
				return new MaskedTextProvider.EditPosition
				{
					Parent = this.Parent,
					Type = this.Type,
					State = this.State,
					MaskCharacter = this.MaskCharacter,
					input = this.input
				};
			}

			// Token: 0x17000322 RID: 802
			// (get) Token: 0x06000DBB RID: 3515 RVA: 0x00023500 File Offset: 0x00021700
			// (set) Token: 0x06000DBC RID: 3516 RVA: 0x00023508 File Offset: 0x00021708
			public char Input
			{
				get
				{
					return this.input;
				}
				set
				{
					MaskedTextProvider.EditState state = this.State;
					if (state != MaskedTextProvider.EditState.UpperCase)
					{
						if (state != MaskedTextProvider.EditState.LowerCase)
						{
							this.input = value;
						}
						else
						{
							this.input = char.ToLower(value, this.Parent.Culture);
						}
					}
					else
					{
						this.input = char.ToUpper(value, this.Parent.Culture);
					}
				}
			}

			// Token: 0x06000DBD RID: 3517 RVA: 0x00023574 File Offset: 0x00021774
			public bool IsAscii(char c)
			{
				return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
			}

			// Token: 0x06000DBE RID: 3518 RVA: 0x000235A8 File Offset: 0x000217A8
			public bool Match(char c, out MaskedTextResultHint resultHint, bool only_test)
			{
				if (!MaskedTextProvider.IsValidInputChar(c))
				{
					resultHint = MaskedTextResultHint.InvalidInput;
					return false;
				}
				if (this.Parent.ResetOnSpace && c == ' ' && this.Editable)
				{
					resultHint = MaskedTextResultHint.CharacterEscaped;
					if (this.FilledIn)
					{
						resultHint = MaskedTextResultHint.Success;
						if (!only_test && this.input != ' ')
						{
							switch (this.Type)
							{
							case MaskedTextProvider.EditType.CharacterRequired:
							case MaskedTextProvider.EditType.CharacterOptional:
							case MaskedTextProvider.EditType.AlphanumericRequired:
							case MaskedTextProvider.EditType.AlphanumericOptional:
								this.Input = c;
								break;
							default:
								this.Input = '\0';
								break;
							}
						}
					}
					return true;
				}
				if (this.Type == MaskedTextProvider.EditType.Literal && this.MaskCharacter == c && this.Parent.SkipLiterals)
				{
					resultHint = MaskedTextResultHint.Success;
					return true;
				}
				if (!this.Editable)
				{
					resultHint = MaskedTextResultHint.NonEditPosition;
					return false;
				}
				switch (this.Type)
				{
				case MaskedTextProvider.EditType.DigitRequired:
					if (char.IsDigit(c))
					{
						if (!only_test)
						{
							this.Input = c;
						}
						resultHint = MaskedTextResultHint.Success;
						return true;
					}
					resultHint = MaskedTextResultHint.DigitExpected;
					return false;
				case MaskedTextProvider.EditType.DigitOrSpaceOptional:
				case MaskedTextProvider.EditType.DigitOrSpaceOptional_Blank:
					if (char.IsDigit(c) || c == ' ')
					{
						if (!only_test)
						{
							this.Input = c;
						}
						resultHint = MaskedTextResultHint.Success;
						return true;
					}
					resultHint = MaskedTextResultHint.DigitExpected;
					return false;
				case MaskedTextProvider.EditType.LetterRequired:
				case MaskedTextProvider.EditType.LetterOptional:
					if (!char.IsLetter(c))
					{
						resultHint = MaskedTextResultHint.LetterExpected;
						return false;
					}
					if (this.Parent.AsciiOnly && !this.IsAscii(c))
					{
						resultHint = MaskedTextResultHint.LetterExpected;
						return false;
					}
					if (!only_test)
					{
						this.Input = c;
					}
					resultHint = MaskedTextResultHint.Success;
					return true;
				case MaskedTextProvider.EditType.CharacterRequired:
				case MaskedTextProvider.EditType.CharacterOptional:
					if (this.Parent.AsciiOnly && !this.IsAscii(c))
					{
						resultHint = MaskedTextResultHint.LetterExpected;
						return false;
					}
					if (!char.IsControl(c))
					{
						if (!only_test)
						{
							this.Input = c;
						}
						resultHint = MaskedTextResultHint.Success;
						return true;
					}
					resultHint = MaskedTextResultHint.LetterExpected;
					return false;
				case MaskedTextProvider.EditType.AlphanumericRequired:
				case MaskedTextProvider.EditType.AlphanumericOptional:
					if (!char.IsLetterOrDigit(c))
					{
						resultHint = MaskedTextResultHint.AlphanumericCharacterExpected;
						return false;
					}
					if (this.Parent.AsciiOnly && !this.IsAscii(c))
					{
						resultHint = MaskedTextResultHint.AsciiCharacterExpected;
						return false;
					}
					if (!only_test)
					{
						this.Input = c;
					}
					resultHint = MaskedTextResultHint.Success;
					return true;
				default:
					resultHint = MaskedTextResultHint.Unknown;
					return false;
				}
			}

			// Token: 0x17000323 RID: 803
			// (get) Token: 0x06000DBF RID: 3519 RVA: 0x000237E0 File Offset: 0x000219E0
			public bool FilledIn
			{
				get
				{
					return this.Input != '\0';
				}
			}

			// Token: 0x17000324 RID: 804
			// (get) Token: 0x06000DC0 RID: 3520 RVA: 0x000237F0 File Offset: 0x000219F0
			public bool Required
			{
				get
				{
					char maskCharacter = this.MaskCharacter;
					return maskCharacter == '&' || maskCharacter == '0' || maskCharacter == 'A' || maskCharacter == 'L';
				}
			}

			// Token: 0x17000325 RID: 805
			// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x0002382C File Offset: 0x00021A2C
			public bool Editable
			{
				get
				{
					char maskCharacter = this.MaskCharacter;
					switch (maskCharacter)
					{
					case '?':
					case 'A':
					case 'C':
						break;
					default:
						switch (maskCharacter)
						{
						case '#':
						case '&':
							break;
						default:
							if (maskCharacter != '0' && maskCharacter != '9' && maskCharacter != 'L' && maskCharacter != 'a')
							{
								return false;
							}
							break;
						}
						break;
					}
					return true;
				}
			}

			// Token: 0x17000326 RID: 806
			// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x000238A0 File Offset: 0x00021AA0
			public bool Visible
			{
				get
				{
					char maskCharacter = this.MaskCharacter;
					switch (maskCharacter)
					{
					case '<':
					case '>':
						break;
					default:
						if (maskCharacter != '|')
						{
							return true;
						}
						break;
					}
					return false;
				}
			}

			// Token: 0x17000327 RID: 807
			// (get) Token: 0x06000DC3 RID: 3523 RVA: 0x000238DC File Offset: 0x00021ADC
			public string Text
			{
				get
				{
					if (this.Type == MaskedTextProvider.EditType.Literal)
					{
						return this.MaskCharacter.ToString();
					}
					char maskCharacter = this.MaskCharacter;
					switch (maskCharacter)
					{
					case ',':
						return this.Parent.Culture.NumberFormat.NumberGroupSeparator;
					default:
						if (maskCharacter == '$')
						{
							return this.Parent.Culture.NumberFormat.CurrencySymbol;
						}
						if (maskCharacter != ':')
						{
							return (!this.FilledIn) ? this.Parent.PromptChar.ToString() : this.Input.ToString();
						}
						return this.Parent.Culture.DateTimeFormat.TimeSeparator;
					case '.':
						return this.Parent.Culture.NumberFormat.NumberDecimalSeparator;
					case '/':
						return this.Parent.Culture.DateTimeFormat.DateSeparator;
					}
				}
			}

			// Token: 0x040003D2 RID: 978
			public MaskedTextProvider Parent;

			// Token: 0x040003D3 RID: 979
			public MaskedTextProvider.EditType Type;

			// Token: 0x040003D4 RID: 980
			public MaskedTextProvider.EditState State;

			// Token: 0x040003D5 RID: 981
			public char MaskCharacter;

			// Token: 0x040003D6 RID: 982
			public char input;
		}
	}
}
