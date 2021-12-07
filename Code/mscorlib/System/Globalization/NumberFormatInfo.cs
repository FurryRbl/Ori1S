using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Globalization
{
	/// <summary>Defines how numeric values are formatted and displayed, depending on the culture.</summary>
	// Token: 0x02000222 RID: 546
	[ComVisible(true)]
	[Serializable]
	public sealed class NumberFormatInfo : ICloneable, IFormatProvider
	{
		// Token: 0x06001BB2 RID: 7090 RVA: 0x0006665C File Offset: 0x0006485C
		internal NumberFormatInfo(int lcid, bool read_only)
		{
			this.isReadOnly = read_only;
			if (lcid != 127)
			{
				lcid = 127;
			}
			int num = lcid;
			if (num == 127)
			{
				this.isReadOnly = false;
				this.currencyDecimalDigits = 2;
				this.currencyDecimalSeparator = ".";
				this.currencyGroupSeparator = ",";
				this.currencyGroupSizes = new int[]
				{
					3
				};
				this.currencyNegativePattern = 0;
				this.currencyPositivePattern = 0;
				this.currencySymbol = "$";
				this.nanSymbol = "NaN";
				this.negativeInfinitySymbol = "-Infinity";
				this.negativeSign = "-";
				this.numberDecimalDigits = 2;
				this.numberDecimalSeparator = ".";
				this.numberGroupSeparator = ",";
				this.numberGroupSizes = new int[]
				{
					3
				};
				this.numberNegativePattern = 1;
				this.percentDecimalDigits = 2;
				this.percentDecimalSeparator = ".";
				this.percentGroupSeparator = ",";
				this.percentGroupSizes = new int[]
				{
					3
				};
				this.percentNegativePattern = 0;
				this.percentPositivePattern = 0;
				this.percentSymbol = "%";
				this.perMilleSymbol = "‰";
				this.positiveInfinitySymbol = "Infinity";
				this.positiveSign = "+";
			}
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x000667C8 File Offset: 0x000649C8
		internal NumberFormatInfo(bool read_only) : this(127, read_only)
		{
		}

		/// <summary>Initializes a new writable instance of the <see cref="T:System.Globalization.NumberFormatInfo" /> class that is culture-independent (invariant).</summary>
		// Token: 0x06001BB4 RID: 7092 RVA: 0x000667D4 File Offset: 0x000649D4
		public NumberFormatInfo() : this(false)
		{
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x0006684C File Offset: 0x00064A4C
		private void InitPatterns()
		{
			string[] array = this.decimalFormats.Split(new char[]
			{
				';'
			}, 2);
			string[] array2;
			if (array.Length == 2)
			{
				array2 = array[0].Split(new char[]
				{
					'.'
				}, 2);
				if (array2.Length == 2)
				{
					this.numberDecimalDigits = 0;
					for (int i = 0; i < array2[1].Length; i++)
					{
						if (array2[1][i] != this.digitPattern[0])
						{
							break;
						}
						this.numberDecimalDigits++;
					}
					string[] array3 = array2[0].Split(new char[]
					{
						','
					});
					if (array3.Length > 1)
					{
						this.numberGroupSizes = new int[array3.Length - 1];
						for (int j = 0; j < this.numberGroupSizes.Length; j++)
						{
							string text = array3[j + 1];
							this.numberGroupSizes[j] = text.Length;
						}
					}
					else
					{
						this.numberGroupSizes = new int[1];
					}
					if (array[1].StartsWith("(") && array[1].EndsWith(")"))
					{
						this.numberNegativePattern = 0;
					}
					else if (array[1].StartsWith("- "))
					{
						this.numberNegativePattern = 2;
					}
					else if (array[1].StartsWith("-"))
					{
						this.numberNegativePattern = 1;
					}
					else if (array[1].EndsWith(" -"))
					{
						this.numberNegativePattern = 4;
					}
					else if (array[1].EndsWith("-"))
					{
						this.numberNegativePattern = 3;
					}
					else
					{
						this.numberNegativePattern = 1;
					}
				}
			}
			array = this.currencyFormats.Split(new char[]
			{
				';'
			}, 2);
			if (array.Length == 2)
			{
				array2 = array[0].Split(new char[]
				{
					'.'
				}, 2);
				if (array2.Length == 2)
				{
					this.currencyDecimalDigits = 0;
					for (int k = 0; k < array2[1].Length; k++)
					{
						if (array2[1][k] != this.zeroPattern[0])
						{
							break;
						}
						this.currencyDecimalDigits++;
					}
					string[] array3 = array2[0].Split(new char[]
					{
						','
					});
					if (array3.Length > 1)
					{
						this.currencyGroupSizes = new int[array3.Length - 1];
						for (int l = 0; l < this.currencyGroupSizes.Length; l++)
						{
							string text2 = array3[l + 1];
							this.currencyGroupSizes[l] = text2.Length;
						}
					}
					else
					{
						this.currencyGroupSizes = new int[1];
					}
					if (array[1].StartsWith("(¤ ") && array[1].EndsWith(")"))
					{
						this.currencyNegativePattern = 14;
					}
					else if (array[1].StartsWith("(¤") && array[1].EndsWith(")"))
					{
						this.currencyNegativePattern = 0;
					}
					else if (array[1].StartsWith("¤ ") && array[1].EndsWith("-"))
					{
						this.currencyNegativePattern = 11;
					}
					else if (array[1].StartsWith("¤") && array[1].EndsWith("-"))
					{
						this.currencyNegativePattern = 3;
					}
					else if (array[1].StartsWith("(") && array[1].EndsWith(" ¤"))
					{
						this.currencyNegativePattern = 15;
					}
					else if (array[1].StartsWith("(") && array[1].EndsWith("¤"))
					{
						this.currencyNegativePattern = 4;
					}
					else if (array[1].StartsWith("-") && array[1].EndsWith(" ¤"))
					{
						this.currencyNegativePattern = 8;
					}
					else if (array[1].StartsWith("-") && array[1].EndsWith("¤"))
					{
						this.currencyNegativePattern = 5;
					}
					else if (array[1].StartsWith("-¤ "))
					{
						this.currencyNegativePattern = 9;
					}
					else if (array[1].StartsWith("-¤"))
					{
						this.currencyNegativePattern = 1;
					}
					else if (array[1].StartsWith("¤ -"))
					{
						this.currencyNegativePattern = 12;
					}
					else if (array[1].StartsWith("¤-"))
					{
						this.currencyNegativePattern = 2;
					}
					else if (array[1].EndsWith(" ¤-"))
					{
						this.currencyNegativePattern = 10;
					}
					else if (array[1].EndsWith("¤-"))
					{
						this.currencyNegativePattern = 7;
					}
					else if (array[1].EndsWith("- ¤"))
					{
						this.currencyNegativePattern = 13;
					}
					else if (array[1].EndsWith("-¤"))
					{
						this.currencyNegativePattern = 6;
					}
					else
					{
						this.currencyNegativePattern = 0;
					}
					if (array[0].StartsWith("¤ "))
					{
						this.currencyPositivePattern = 2;
					}
					else if (array[0].StartsWith("¤"))
					{
						this.currencyPositivePattern = 0;
					}
					else if (array[0].EndsWith(" ¤"))
					{
						this.currencyPositivePattern = 3;
					}
					else if (array[0].EndsWith("¤"))
					{
						this.currencyPositivePattern = 1;
					}
					else
					{
						this.currencyPositivePattern = 0;
					}
				}
			}
			if (this.percentFormats.StartsWith("%"))
			{
				this.percentPositivePattern = 2;
				this.percentNegativePattern = 2;
			}
			else if (this.percentFormats.EndsWith(" %"))
			{
				this.percentPositivePattern = 0;
				this.percentNegativePattern = 0;
			}
			else if (this.percentFormats.EndsWith("%"))
			{
				this.percentPositivePattern = 1;
				this.percentNegativePattern = 1;
			}
			else
			{
				this.percentPositivePattern = 0;
				this.percentNegativePattern = 0;
			}
			array2 = this.percentFormats.Split(new char[]
			{
				'.'
			}, 2);
			if (array2.Length == 2)
			{
				this.percentDecimalDigits = 0;
				for (int m = 0; m < array2[1].Length; m++)
				{
					if (array2[1][m] != this.digitPattern[0])
					{
						break;
					}
					this.percentDecimalDigits++;
				}
				string[] array3 = array2[0].Split(new char[]
				{
					','
				});
				if (array3.Length > 1)
				{
					this.percentGroupSizes = new int[array3.Length - 1];
					for (int n = 0; n < this.percentGroupSizes.Length; n++)
					{
						string text3 = array3[n + 1];
						this.percentGroupSizes[n] = text3.Length;
					}
				}
				else
				{
					this.percentGroupSizes = new int[1];
				}
			}
		}

		/// <summary>Gets or sets the number of decimal places to use in currency values.</summary>
		/// <returns>The number of decimal places to use in currency values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 2.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is less than 0 or greater than 99. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> is read-only. </exception>
		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06001BB7 RID: 7095 RVA: 0x00066F84 File Offset: 0x00065184
		// (set) Token: 0x06001BB8 RID: 7096 RVA: 0x00066F8C File Offset: 0x0006518C
		public int CurrencyDecimalDigits
		{
			get
			{
				return this.currencyDecimalDigits;
			}
			set
			{
				if (value < 0 || value > 99)
				{
					throw new ArgumentOutOfRangeException("The value specified for the property is less than 0 or greater than 99");
				}
				if (this.isReadOnly)
				{
					throw new InvalidOperationException("The current instance is read-only and a set operation was attempted");
				}
				this.currencyDecimalDigits = value;
			}
		}

		/// <summary>Gets or sets the string to use as the decimal separator in currency values.</summary>
		/// <returns>The string to use as the decimal separator in currency values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is ".".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> is read-only. </exception>
		/// <exception cref="T:System.ArgumentException">The property is being set to an empty string.</exception>
		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001BB9 RID: 7097 RVA: 0x00066FC8 File Offset: 0x000651C8
		// (set) Token: 0x06001BBA RID: 7098 RVA: 0x00066FD0 File Offset: 0x000651D0
		public string CurrencyDecimalSeparator
		{
			get
			{
				return this.currencyDecimalSeparator;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("The value specified for the property is a null reference");
				}
				if (this.isReadOnly)
				{
					throw new InvalidOperationException("The current instance is read-only and a set operation was attempted");
				}
				this.currencyDecimalSeparator = value;
			}
		}

		/// <summary>Gets or sets the string that separates groups of digits to the left of the decimal in currency values.</summary>
		/// <returns>The string that separates groups of digits to the left of the decimal in currency values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is ",".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> is read-only. </exception>
		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06001BBB RID: 7099 RVA: 0x0006700C File Offset: 0x0006520C
		// (set) Token: 0x06001BBC RID: 7100 RVA: 0x00067014 File Offset: 0x00065214
		public string CurrencyGroupSeparator
		{
			get
			{
				return this.currencyGroupSeparator;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("The value specified for the property is a null reference");
				}
				if (this.isReadOnly)
				{
					throw new InvalidOperationException("The current instance is read-only and a set operation was attempted");
				}
				this.currencyGroupSeparator = value;
			}
		}

		/// <summary>Gets or sets the number of digits in each group to the left of the decimal in currency values.</summary>
		/// <returns>The number of digits in each group to the left of the decimal in currency values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is a one-dimensional array with only one element, which is set to 3.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.ArgumentException">The property is being set and the array contains an entry that is less than 0 or greater than 9.-or- The property is being set and the array contains an entry, other than the last entry, that is set to 0. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> is read-only. </exception>
		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06001BBD RID: 7101 RVA: 0x00067050 File Offset: 0x00065250
		// (set) Token: 0x06001BBE RID: 7102 RVA: 0x00067064 File Offset: 0x00065264
		public int[] CurrencyGroupSizes
		{
			get
			{
				return (int[])this.RawCurrencyGroupSizes.Clone();
			}
			set
			{
				this.RawCurrencyGroupSizes = value;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06001BBF RID: 7103 RVA: 0x00067070 File Offset: 0x00065270
		// (set) Token: 0x06001BC0 RID: 7104 RVA: 0x00067078 File Offset: 0x00065278
		internal int[] RawCurrencyGroupSizes
		{
			get
			{
				return this.currencyGroupSizes;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("The value specified for the property is a null reference");
				}
				if (this.isReadOnly)
				{
					throw new InvalidOperationException("The current instance is read-only and a set operation was attempted");
				}
				if (value.Length == 0)
				{
					this.currencyGroupSizes = new int[0];
					return;
				}
				int num = value.Length - 1;
				for (int i = 0; i < num; i++)
				{
					if (value[i] < 1 || value[i] > 9)
					{
						throw new ArgumentOutOfRangeException("One of the elements in the array specified is not between 1 and 9");
					}
				}
				if (value[num] < 0 || value[num] > 9)
				{
					throw new ArgumentOutOfRangeException("Last element in the array specified is not between 0 and 9");
				}
				this.currencyGroupSizes = (int[])value.Clone();
			}
		}

		/// <summary>Gets or sets the format pattern for negative currency values.</summary>
		/// <returns>The format pattern for negative currency values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 0, which represents "($n)", where "$" is the <see cref="P:System.Globalization.NumberFormatInfo.CurrencySymbol" /> and <paramref name="n" /> is a number.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is less than 0 or greater than 15. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> is read-only. </exception>
		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06001BC1 RID: 7105 RVA: 0x00067128 File Offset: 0x00065328
		// (set) Token: 0x06001BC2 RID: 7106 RVA: 0x00067130 File Offset: 0x00065330
		public int CurrencyNegativePattern
		{
			get
			{
				return this.currencyNegativePattern;
			}
			set
			{
				if (value < 0 || value > 15)
				{
					throw new ArgumentOutOfRangeException("The value specified for the property is less than 0 or greater than 15");
				}
				if (this.isReadOnly)
				{
					throw new InvalidOperationException("The current instance is read-only and a set operation was attempted");
				}
				this.currencyNegativePattern = value;
			}
		}

		/// <summary>Gets or sets the format pattern for positive currency values.</summary>
		/// <returns>The format pattern for positive currency values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 0, which represents "$n", where "$" is the <see cref="P:System.Globalization.NumberFormatInfo.CurrencySymbol" /> and <paramref name="n" /> is a number.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is less than 0 or greater than 3. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> is read-only. </exception>
		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06001BC3 RID: 7107 RVA: 0x0006716C File Offset: 0x0006536C
		// (set) Token: 0x06001BC4 RID: 7108 RVA: 0x00067174 File Offset: 0x00065374
		public int CurrencyPositivePattern
		{
			get
			{
				return this.currencyPositivePattern;
			}
			set
			{
				if (value < 0 || value > 3)
				{
					throw new ArgumentOutOfRangeException("The value specified for the property is less than 0 or greater than 3");
				}
				if (this.isReadOnly)
				{
					throw new InvalidOperationException("The current instance is read-only and a set operation was attempted");
				}
				this.currencyPositivePattern = value;
			}
		}

		/// <summary>Gets or sets the string to use as the currency symbol.</summary>
		/// <returns>The string to use as the currency symbol. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "¤".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> is read-only. </exception>
		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001BC5 RID: 7109 RVA: 0x000671B8 File Offset: 0x000653B8
		// (set) Token: 0x06001BC6 RID: 7110 RVA: 0x000671C0 File Offset: 0x000653C0
		public string CurrencySymbol
		{
			get
			{
				return this.currencySymbol;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("The value specified for the property is a null reference");
				}
				if (this.isReadOnly)
				{
					throw new InvalidOperationException("The current instance is read-only and a set operation was attempted");
				}
				this.currencySymbol = value;
			}
		}

		/// <summary>Gets a read-only <see cref="T:System.Globalization.NumberFormatInfo" /> that formats values based on the current culture.</summary>
		/// <returns>A read-only <see cref="T:System.Globalization.NumberFormatInfo" /> based on the <see cref="T:System.Globalization.CultureInfo" /> of the current thread.</returns>
		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06001BC7 RID: 7111 RVA: 0x000671FC File Offset: 0x000653FC
		public static NumberFormatInfo CurrentInfo
		{
			get
			{
				NumberFormatInfo numberFormat = Thread.CurrentThread.CurrentCulture.NumberFormat;
				numberFormat.isReadOnly = true;
				return numberFormat;
			}
		}

		/// <summary>Gets the default read-only <see cref="T:System.Globalization.NumberFormatInfo" /> that is culture-independent (invariant).</summary>
		/// <returns>The default read-only <see cref="T:System.Globalization.NumberFormatInfo" /> that is culture-independent (invariant).</returns>
		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06001BC8 RID: 7112 RVA: 0x00067224 File Offset: 0x00065424
		public static NumberFormatInfo InvariantInfo
		{
			get
			{
				return new NumberFormatInfo
				{
					NumberNegativePattern = 1,
					isReadOnly = true
				};
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Globalization.NumberFormatInfo" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Globalization.NumberFormatInfo" /> is read-only; otherwise, false.</returns>
		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06001BC9 RID: 7113 RVA: 0x00067248 File Offset: 0x00065448
		public bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		/// <summary>Gets or sets the string that represents the IEEE NaN (not a number) value.</summary>
		/// <returns>The string that represents the IEEE NaN (not a number) value. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "NaN".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> is read-only. </exception>
		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06001BCA RID: 7114 RVA: 0x00067250 File Offset: 0x00065450
		// (set) Token: 0x06001BCB RID: 7115 RVA: 0x00067258 File Offset: 0x00065458
		public string NaNSymbol
		{
			get
			{
				return this.nanSymbol;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("The value specified for the property is a null reference");
				}
				if (this.isReadOnly)
				{
					throw new InvalidOperationException("The current instance is read-only and a set operation was attempted");
				}
				this.nanSymbol = value;
			}
		}

		/// <summary>Gets or sets a string array of native digits equivalent to the Western digits 0 through 9.</summary>
		/// <returns>An array of type <see cref="T:System.String" /> that contains the native equivalent of the Western digits 0 through 9. The default is an array having the elements "0", "1", "2", "3", "4", "5", "6", "7", "8", and "9".</returns>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		/// <exception cref="T:System.ArgumentNullException">In a set operation, the value is null.-or-In a set operation, an element of the value array is null.</exception>
		/// <exception cref="T:System.ArgumentException">In a set operation, the value array does not contain 10 elements.-or-In a set operation, an element of the value array does not contain either a single <see cref="T:System.Char" /> object or a pair of <see cref="T:System.Char" /> objects that comprise a surrogate pair.-or-In a set operation, an element of the value array is not a number digit as defined by the Unicode standard. That is, the digit in the array element does not have the Unicode Number, Decimal Digit (Nd) General Category value.-or-In a set operation, the numeric value of an element in the value array does not correspond to the element's position in the array. That is, the element at index 0, which is the first element of the array, does not have a numeric value of 0, or the element at index 1 does not have a numeric value of 1. </exception>
		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06001BCC RID: 7116 RVA: 0x00067294 File Offset: 0x00065494
		// (set) Token: 0x06001BCD RID: 7117 RVA: 0x0006729C File Offset: 0x0006549C
		[ComVisible(false)]
		[MonoNotSupported("We don't have native digit info")]
		public string[] NativeDigits
		{
			get
			{
				return this.nativeDigits;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length != 10)
				{
					throw new ArgumentException("Argument array length must be 10");
				}
				for (int i = 0; i < value.Length; i++)
				{
					string value2 = value[i];
					if (string.IsNullOrEmpty(value2))
					{
						throw new ArgumentException("Argument array contains one or more null strings");
					}
				}
				this.nativeDigits = value;
			}
		}

		/// <summary>Gets or sets a value that specifies how the graphical user interface displays the shape of a digit.</summary>
		/// <returns>One of the <see cref="T:System.Globalization.DigitShapes" /> values.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		/// <exception cref="T:System.ArgumentException">The value in a set operation is not a defined <see cref="T:System.Globalization.DigitShapes" /> value.</exception>
		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06001BCE RID: 7118 RVA: 0x00067308 File Offset: 0x00065508
		// (set) Token: 0x06001BCF RID: 7119 RVA: 0x00067310 File Offset: 0x00065510
		[MonoNotSupported("We don't have native digit info")]
		[ComVisible(false)]
		public DigitShapes DigitSubstitution
		{
			get
			{
				return (DigitShapes)this.digitSubstitution;
			}
			set
			{
				this.digitSubstitution = (int)value;
			}
		}

		/// <summary>Gets or sets the string that represents negative infinity.</summary>
		/// <returns>The string that represents negative infinity. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "-Infinity".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> is read-only. </exception>
		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06001BD0 RID: 7120 RVA: 0x0006731C File Offset: 0x0006551C
		// (set) Token: 0x06001BD1 RID: 7121 RVA: 0x00067324 File Offset: 0x00065524
		public string NegativeInfinitySymbol
		{
			get
			{
				return this.negativeInfinitySymbol;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("The value specified for the property is a null reference");
				}
				if (this.isReadOnly)
				{
					throw new InvalidOperationException("The current instance is read-only and a set operation was attempted");
				}
				this.negativeInfinitySymbol = value;
			}
		}

		/// <summary>Gets or sets the string that denotes that the associated number is negative.</summary>
		/// <returns>The string that denotes that the associated number is negative. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "-".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> is read-only. </exception>
		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06001BD2 RID: 7122 RVA: 0x00067360 File Offset: 0x00065560
		// (set) Token: 0x06001BD3 RID: 7123 RVA: 0x00067368 File Offset: 0x00065568
		public string NegativeSign
		{
			get
			{
				return this.negativeSign;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("The value specified for the property is a null reference");
				}
				if (this.isReadOnly)
				{
					throw new InvalidOperationException("The current instance is read-only and a set operation was attempted");
				}
				this.negativeSign = value;
			}
		}

		/// <summary>Gets or sets the number of decimal places to use in numeric values.</summary>
		/// <returns>The number of decimal places to use in numeric values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 2.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is less than 0 or greater than 99. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> is read-only. </exception>
		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06001BD4 RID: 7124 RVA: 0x000673A4 File Offset: 0x000655A4
		// (set) Token: 0x06001BD5 RID: 7125 RVA: 0x000673AC File Offset: 0x000655AC
		public int NumberDecimalDigits
		{
			get
			{
				return this.numberDecimalDigits;
			}
			set
			{
				if (value < 0 || value > 99)
				{
					throw new ArgumentOutOfRangeException("The value specified for the property is less than 0 or greater than 99");
				}
				if (this.isReadOnly)
				{
					throw new InvalidOperationException("The current instance is read-only and a set operation was attempted");
				}
				this.numberDecimalDigits = value;
			}
		}

		/// <summary>Gets or sets the string to use as the decimal separator in numeric values.</summary>
		/// <returns>The string to use as the decimal separator in numeric values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is ".".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> is read-only. </exception>
		/// <exception cref="T:System.ArgumentException">The property is being set to an empty string.</exception>
		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001BD6 RID: 7126 RVA: 0x000673E8 File Offset: 0x000655E8
		// (set) Token: 0x06001BD7 RID: 7127 RVA: 0x000673F0 File Offset: 0x000655F0
		public string NumberDecimalSeparator
		{
			get
			{
				return this.numberDecimalSeparator;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("The value specified for the property is a null reference");
				}
				if (this.isReadOnly)
				{
					throw new InvalidOperationException("The current instance is read-only and a set operation was attempted");
				}
				this.numberDecimalSeparator = value;
			}
		}

		/// <summary>Gets or sets the string that separates groups of digits to the left of the decimal in numeric values.</summary>
		/// <returns>The string that separates groups of digits to the left of the decimal in numeric values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is ",".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> is read-only. </exception>
		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001BD8 RID: 7128 RVA: 0x0006742C File Offset: 0x0006562C
		// (set) Token: 0x06001BD9 RID: 7129 RVA: 0x00067434 File Offset: 0x00065634
		public string NumberGroupSeparator
		{
			get
			{
				return this.numberGroupSeparator;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("The value specified for the property is a null reference");
				}
				if (this.isReadOnly)
				{
					throw new InvalidOperationException("The current instance is read-only and a set operation was attempted");
				}
				this.numberGroupSeparator = value;
			}
		}

		/// <summary>Gets or sets the number of digits in each group to the left of the decimal in numeric values.</summary>
		/// <returns>The number of digits in each group to the left of the decimal in numeric values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is a one-dimensional array with only one element, which is set to 3.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.ArgumentException">The property is being set and the array contains an entry that is less than 0 or greater than 9.-or- The property is being set and the array contains an entry, other than the last entry, that is set to 0. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> is read-only. </exception>
		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001BDA RID: 7130 RVA: 0x00067470 File Offset: 0x00065670
		// (set) Token: 0x06001BDB RID: 7131 RVA: 0x00067484 File Offset: 0x00065684
		public int[] NumberGroupSizes
		{
			get
			{
				return (int[])this.RawNumberGroupSizes.Clone();
			}
			set
			{
				this.RawNumberGroupSizes = value;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001BDC RID: 7132 RVA: 0x00067490 File Offset: 0x00065690
		// (set) Token: 0x06001BDD RID: 7133 RVA: 0x00067498 File Offset: 0x00065698
		internal int[] RawNumberGroupSizes
		{
			get
			{
				return this.numberGroupSizes;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("The value specified for the property is a null reference");
				}
				if (this.isReadOnly)
				{
					throw new InvalidOperationException("The current instance is read-only and a set operation was attempted");
				}
				if (value.Length == 0)
				{
					this.numberGroupSizes = new int[0];
					return;
				}
				int num = value.Length - 1;
				for (int i = 0; i < num; i++)
				{
					if (value[i] < 1 || value[i] > 9)
					{
						throw new ArgumentOutOfRangeException("One of the elements in the array specified is not between 1 and 9");
					}
				}
				if (value[num] < 0 || value[num] > 9)
				{
					throw new ArgumentOutOfRangeException("Last element in the array specified is not between 0 and 9");
				}
				this.numberGroupSizes = (int[])value.Clone();
			}
		}

		/// <summary>Gets or sets the format pattern for negative numeric values.</summary>
		/// <returns>The format pattern for negative numeric values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 1, which represents "-n", where <paramref name="n" /> is a number.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is less than 0 or greater than 4. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> is read-only. </exception>
		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001BDE RID: 7134 RVA: 0x00067548 File Offset: 0x00065748
		// (set) Token: 0x06001BDF RID: 7135 RVA: 0x00067550 File Offset: 0x00065750
		public int NumberNegativePattern
		{
			get
			{
				return this.numberNegativePattern;
			}
			set
			{
				if (value < 0 || value > 4)
				{
					throw new ArgumentOutOfRangeException("The value specified for the property is less than 0 or greater than 15");
				}
				if (this.isReadOnly)
				{
					throw new InvalidOperationException("The current instance is read-only and a set operation was attempted");
				}
				this.numberNegativePattern = value;
			}
		}

		/// <summary>Gets or sets the number of decimal places to use in percent values. </summary>
		/// <returns>The number of decimal places to use in percent values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 2.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is less than 0 or greater than 99. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> is read-only. </exception>
		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001BE0 RID: 7136 RVA: 0x00067594 File Offset: 0x00065794
		// (set) Token: 0x06001BE1 RID: 7137 RVA: 0x0006759C File Offset: 0x0006579C
		public int PercentDecimalDigits
		{
			get
			{
				return this.percentDecimalDigits;
			}
			set
			{
				if (value < 0 || value > 99)
				{
					throw new ArgumentOutOfRangeException("The value specified for the property is less than 0 or greater than 99");
				}
				if (this.isReadOnly)
				{
					throw new InvalidOperationException("The current instance is read-only and a set operation was attempted");
				}
				this.percentDecimalDigits = value;
			}
		}

		/// <summary>Gets or sets the string to use as the decimal separator in percent values. </summary>
		/// <returns>The string to use as the decimal separator in percent values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is ".".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> is read-only. </exception>
		/// <exception cref="T:System.ArgumentException">The property is being set to an empty string.</exception>
		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06001BE2 RID: 7138 RVA: 0x000675D8 File Offset: 0x000657D8
		// (set) Token: 0x06001BE3 RID: 7139 RVA: 0x000675E0 File Offset: 0x000657E0
		public string PercentDecimalSeparator
		{
			get
			{
				return this.percentDecimalSeparator;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("The value specified for the property is a null reference");
				}
				if (this.isReadOnly)
				{
					throw new InvalidOperationException("The current instance is read-only and a set operation was attempted");
				}
				this.percentDecimalSeparator = value;
			}
		}

		/// <summary>Gets or sets the string that separates groups of digits to the left of the decimal in percent values. </summary>
		/// <returns>The string that separates groups of digits to the left of the decimal in percent values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is ",".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> is read-only. </exception>
		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001BE4 RID: 7140 RVA: 0x0006761C File Offset: 0x0006581C
		// (set) Token: 0x06001BE5 RID: 7141 RVA: 0x00067624 File Offset: 0x00065824
		public string PercentGroupSeparator
		{
			get
			{
				return this.percentGroupSeparator;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("The value specified for the property is a null reference");
				}
				if (this.isReadOnly)
				{
					throw new InvalidOperationException("The current instance is read-only and a set operation was attempted");
				}
				this.percentGroupSeparator = value;
			}
		}

		/// <summary>Gets or sets the number of digits in each group to the left of the decimal in percent values. </summary>
		/// <returns>The number of digits in each group to the left of the decimal in percent values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is a one-dimensional array with only one element, which is set to 3.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.ArgumentException">The property is being set and the array contains an entry that is less than 0 or greater than 9.-or- The property is being set and the array contains an entry, other than the last entry, that is set to 0. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> is read-only. </exception>
		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001BE6 RID: 7142 RVA: 0x00067660 File Offset: 0x00065860
		// (set) Token: 0x06001BE7 RID: 7143 RVA: 0x00067674 File Offset: 0x00065874
		public int[] PercentGroupSizes
		{
			get
			{
				return (int[])this.RawPercentGroupSizes.Clone();
			}
			set
			{
				this.RawPercentGroupSizes = value;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06001BE8 RID: 7144 RVA: 0x00067680 File Offset: 0x00065880
		// (set) Token: 0x06001BE9 RID: 7145 RVA: 0x00067688 File Offset: 0x00065888
		internal int[] RawPercentGroupSizes
		{
			get
			{
				return this.percentGroupSizes;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("The value specified for the property is a null reference");
				}
				if (this.isReadOnly)
				{
					throw new InvalidOperationException("The current instance is read-only and a set operation was attempted");
				}
				if (this == CultureInfo.CurrentCulture.NumberFormat)
				{
					throw new Exception("HERE the value was modified");
				}
				if (value.Length == 0)
				{
					this.percentGroupSizes = new int[0];
					return;
				}
				int num = value.Length - 1;
				for (int i = 0; i < num; i++)
				{
					if (value[i] < 1 || value[i] > 9)
					{
						throw new ArgumentOutOfRangeException("One of the elements in the array specified is not between 1 and 9");
					}
				}
				if (value[num] < 0 || value[num] > 9)
				{
					throw new ArgumentOutOfRangeException("Last element in the array specified is not between 0 and 9");
				}
				this.percentGroupSizes = (int[])value.Clone();
			}
		}

		/// <summary>Gets or sets the format pattern for negative percent values.</summary>
		/// <returns>The format pattern for negative percent values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 0, which represents "-n %", where "%" is the <see cref="P:System.Globalization.NumberFormatInfo.PercentSymbol" /> and <paramref name="n" /> is a number.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is less than 0 or greater than 11. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> is read-only. </exception>
		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06001BEA RID: 7146 RVA: 0x00067754 File Offset: 0x00065954
		// (set) Token: 0x06001BEB RID: 7147 RVA: 0x0006775C File Offset: 0x0006595C
		public int PercentNegativePattern
		{
			get
			{
				return this.percentNegativePattern;
			}
			set
			{
				if (value < 0 || value > 2)
				{
					throw new ArgumentOutOfRangeException("The value specified for the property is less than 0 or greater than 15");
				}
				if (this.isReadOnly)
				{
					throw new InvalidOperationException("The current instance is read-only and a set operation was attempted");
				}
				this.percentNegativePattern = value;
			}
		}

		/// <summary>Gets or sets the format pattern for positive percent values.</summary>
		/// <returns>The format pattern for positive percent values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 0, which represents "n %", where "%" is the <see cref="P:System.Globalization.NumberFormatInfo.PercentSymbol" /> and <paramref name="n" /> is a number.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is less than 0 or greater than 3. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> is read-only. </exception>
		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06001BEC RID: 7148 RVA: 0x000677A0 File Offset: 0x000659A0
		// (set) Token: 0x06001BED RID: 7149 RVA: 0x000677A8 File Offset: 0x000659A8
		public int PercentPositivePattern
		{
			get
			{
				return this.percentPositivePattern;
			}
			set
			{
				if (value < 0 || value > 2)
				{
					throw new ArgumentOutOfRangeException("The value specified for the property is less than 0 or greater than 3");
				}
				if (this.isReadOnly)
				{
					throw new InvalidOperationException("The current instance is read-only and a set operation was attempted");
				}
				this.percentPositivePattern = value;
			}
		}

		/// <summary>Gets or sets the string to use as the percent symbol.</summary>
		/// <returns>The string to use as the percent symbol. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "%".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> is read-only. </exception>
		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001BEE RID: 7150 RVA: 0x000677EC File Offset: 0x000659EC
		// (set) Token: 0x06001BEF RID: 7151 RVA: 0x000677F4 File Offset: 0x000659F4
		public string PercentSymbol
		{
			get
			{
				return this.percentSymbol;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("The value specified for the property is a null reference");
				}
				if (this.isReadOnly)
				{
					throw new InvalidOperationException("The current instance is read-only and a set operation was attempted");
				}
				this.percentSymbol = value;
			}
		}

		/// <summary>Gets or sets the string to use as the per mille symbol.</summary>
		/// <returns>The string to use as the per mille symbol. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "‰", which is the Unicode character U+2030.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> is read-only. </exception>
		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001BF0 RID: 7152 RVA: 0x00067830 File Offset: 0x00065A30
		// (set) Token: 0x06001BF1 RID: 7153 RVA: 0x00067838 File Offset: 0x00065A38
		public string PerMilleSymbol
		{
			get
			{
				return this.perMilleSymbol;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("The value specified for the property is a null reference");
				}
				if (this.isReadOnly)
				{
					throw new InvalidOperationException("The current instance is read-only and a set operation was attempted");
				}
				this.perMilleSymbol = value;
			}
		}

		/// <summary>Gets or sets the string that represents positive infinity.</summary>
		/// <returns>The string that represents positive infinity. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "Infinity".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> is read-only. </exception>
		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001BF2 RID: 7154 RVA: 0x00067874 File Offset: 0x00065A74
		// (set) Token: 0x06001BF3 RID: 7155 RVA: 0x0006787C File Offset: 0x00065A7C
		public string PositiveInfinitySymbol
		{
			get
			{
				return this.positiveInfinitySymbol;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("The value specified for the property is a null reference");
				}
				if (this.isReadOnly)
				{
					throw new InvalidOperationException("The current instance is read-only and a set operation was attempted");
				}
				this.positiveInfinitySymbol = value;
			}
		}

		/// <summary>Gets or sets the string that denotes that the associated number is positive.</summary>
		/// <returns>The string that denotes that the associated number is positive. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "+".</returns>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> is read-only. </exception>
		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001BF4 RID: 7156 RVA: 0x000678B8 File Offset: 0x00065AB8
		// (set) Token: 0x06001BF5 RID: 7157 RVA: 0x000678C0 File Offset: 0x00065AC0
		public string PositiveSign
		{
			get
			{
				return this.positiveSign;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("The value specified for the property is a null reference");
				}
				if (this.isReadOnly)
				{
					throw new InvalidOperationException("The current instance is read-only and a set operation was attempted");
				}
				this.positiveSign = value;
			}
		}

		/// <summary>Gets an object of the specified type that provides a number formatting service.</summary>
		/// <returns>The current <see cref="T:System.Globalization.NumberFormatInfo" />, if <paramref name="formatType" /> is the same as the type of the current <see cref="T:System.Globalization.NumberFormatInfo" />; otherwise, null.</returns>
		/// <param name="formatType">The <see cref="T:System.Type" /> of the required formatting service. </param>
		// Token: 0x06001BF6 RID: 7158 RVA: 0x000678FC File Offset: 0x00065AFC
		public object GetFormat(Type formatType)
		{
			return (formatType != typeof(NumberFormatInfo)) ? null : this;
		}

		/// <summary>Creates a shallow copy of the <see cref="T:System.Globalization.NumberFormatInfo" />.</summary>
		/// <returns>A new <see cref="T:System.Globalization.NumberFormatInfo" /> copied from the original <see cref="T:System.Globalization.NumberFormatInfo" />.</returns>
		// Token: 0x06001BF7 RID: 7159 RVA: 0x00067918 File Offset: 0x00065B18
		public object Clone()
		{
			NumberFormatInfo numberFormatInfo = (NumberFormatInfo)base.MemberwiseClone();
			numberFormatInfo.isReadOnly = false;
			return numberFormatInfo;
		}

		/// <summary>Returns a read-only <see cref="T:System.Globalization.NumberFormatInfo" /> wrapper.</summary>
		/// <returns>A read-only <see cref="T:System.Globalization.NumberFormatInfo" /> wrapper around <paramref name="nfi" />.</returns>
		/// <param name="nfi">The <see cref="T:System.Globalization.NumberFormatInfo" /> to wrap. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="nfi" /> is null. </exception>
		// Token: 0x06001BF8 RID: 7160 RVA: 0x0006793C File Offset: 0x00065B3C
		public static NumberFormatInfo ReadOnly(NumberFormatInfo nfi)
		{
			NumberFormatInfo numberFormatInfo = (NumberFormatInfo)nfi.Clone();
			numberFormatInfo.isReadOnly = true;
			return numberFormatInfo;
		}

		/// <summary>Gets the <see cref="T:System.Globalization.NumberFormatInfo" /> associated with the specified <see cref="T:System.IFormatProvider" />.</summary>
		/// <returns>The <see cref="T:System.Globalization.NumberFormatInfo" /> associated with the specified <see cref="T:System.IFormatProvider" />.</returns>
		/// <param name="formatProvider">The <see cref="T:System.IFormatProvider" /> used to get the <see cref="T:System.Globalization.NumberFormatInfo" />.-or- null to get <see cref="P:System.Globalization.NumberFormatInfo.CurrentInfo" />. </param>
		// Token: 0x06001BF9 RID: 7161 RVA: 0x00067960 File Offset: 0x00065B60
		public static NumberFormatInfo GetInstance(IFormatProvider formatProvider)
		{
			if (formatProvider != null)
			{
				NumberFormatInfo numberFormatInfo = (NumberFormatInfo)formatProvider.GetFormat(typeof(NumberFormatInfo));
				if (numberFormatInfo != null)
				{
					return numberFormatInfo;
				}
			}
			return NumberFormatInfo.CurrentInfo;
		}

		// Token: 0x04000A4C RID: 2636
		private bool isReadOnly;

		// Token: 0x04000A4D RID: 2637
		private string decimalFormats;

		// Token: 0x04000A4E RID: 2638
		private string currencyFormats;

		// Token: 0x04000A4F RID: 2639
		private string percentFormats;

		// Token: 0x04000A50 RID: 2640
		private string digitPattern = "#";

		// Token: 0x04000A51 RID: 2641
		private string zeroPattern = "0";

		// Token: 0x04000A52 RID: 2642
		private int currencyDecimalDigits;

		// Token: 0x04000A53 RID: 2643
		private string currencyDecimalSeparator;

		// Token: 0x04000A54 RID: 2644
		private string currencyGroupSeparator;

		// Token: 0x04000A55 RID: 2645
		private int[] currencyGroupSizes;

		// Token: 0x04000A56 RID: 2646
		private int currencyNegativePattern;

		// Token: 0x04000A57 RID: 2647
		private int currencyPositivePattern;

		// Token: 0x04000A58 RID: 2648
		private string currencySymbol;

		// Token: 0x04000A59 RID: 2649
		private string nanSymbol;

		// Token: 0x04000A5A RID: 2650
		private string negativeInfinitySymbol;

		// Token: 0x04000A5B RID: 2651
		private string negativeSign;

		// Token: 0x04000A5C RID: 2652
		private int numberDecimalDigits;

		// Token: 0x04000A5D RID: 2653
		private string numberDecimalSeparator;

		// Token: 0x04000A5E RID: 2654
		private string numberGroupSeparator;

		// Token: 0x04000A5F RID: 2655
		private int[] numberGroupSizes;

		// Token: 0x04000A60 RID: 2656
		private int numberNegativePattern;

		// Token: 0x04000A61 RID: 2657
		private int percentDecimalDigits;

		// Token: 0x04000A62 RID: 2658
		private string percentDecimalSeparator;

		// Token: 0x04000A63 RID: 2659
		private string percentGroupSeparator;

		// Token: 0x04000A64 RID: 2660
		private int[] percentGroupSizes;

		// Token: 0x04000A65 RID: 2661
		private int percentNegativePattern;

		// Token: 0x04000A66 RID: 2662
		private int percentPositivePattern;

		// Token: 0x04000A67 RID: 2663
		private string percentSymbol;

		// Token: 0x04000A68 RID: 2664
		private string perMilleSymbol;

		// Token: 0x04000A69 RID: 2665
		private string positiveInfinitySymbol;

		// Token: 0x04000A6A RID: 2666
		private string positiveSign;

		// Token: 0x04000A6B RID: 2667
		private string ansiCurrencySymbol;

		// Token: 0x04000A6C RID: 2668
		private int m_dataItem;

		// Token: 0x04000A6D RID: 2669
		private bool m_useUserOverride;

		// Token: 0x04000A6E RID: 2670
		private bool validForParseAsNumber;

		// Token: 0x04000A6F RID: 2671
		private bool validForParseAsCurrency;

		// Token: 0x04000A70 RID: 2672
		private string[] nativeDigits = NumberFormatInfo.invariantNativeDigits;

		// Token: 0x04000A71 RID: 2673
		private int digitSubstitution = 1;

		// Token: 0x04000A72 RID: 2674
		private static readonly string[] invariantNativeDigits = new string[]
		{
			"0",
			"1",
			"2",
			"3",
			"4",
			"5",
			"6",
			"7",
			"8",
			"9"
		};
	}
}
