using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	/// <summary>Defines a key/value pair that can be set or retrieved.</summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020006C7 RID: 1735
	[DebuggerDisplay("{value}", Name = "[{key}]")]
	[Serializable]
	public struct KeyValuePair<TKey, TValue>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.KeyValuePair`2" /> structure with the specified key and value.</summary>
		/// <param name="key">The object defined in each key/value pair.</param>
		/// <param name="value">The definition associated with <paramref name="key" />.</param>
		// Token: 0x06004271 RID: 17009 RVA: 0x000E3B54 File Offset: 0x000E1D54
		public KeyValuePair(TKey key, TValue value)
		{
			this.Key = key;
			this.Value = value;
		}

		/// <summary>Gets the key in the key/value pair.</summary>
		/// <returns>A <paramref name="TKey" /> that is the key of the <see cref="T:System.Collections.Generic.KeyValuePair`2" />. </returns>
		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x06004272 RID: 17010 RVA: 0x000E3B64 File Offset: 0x000E1D64
		// (set) Token: 0x06004273 RID: 17011 RVA: 0x000E3B6C File Offset: 0x000E1D6C
		public TKey Key
		{
			get
			{
				return this.key;
			}
			private set
			{
				this.key = value;
			}
		}

		/// <summary>Gets the value in the key/value pair.</summary>
		/// <returns>A <paramref name="TValue" /> that is the value of the <see cref="T:System.Collections.Generic.KeyValuePair`2" />. </returns>
		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x06004274 RID: 17012 RVA: 0x000E3B78 File Offset: 0x000E1D78
		// (set) Token: 0x06004275 RID: 17013 RVA: 0x000E3B80 File Offset: 0x000E1D80
		public TValue Value
		{
			get
			{
				return this.value;
			}
			private set
			{
				this.value = value;
			}
		}

		/// <summary>Returns a string representation of the <see cref="T:System.Collections.Generic.KeyValuePair`2" />, using the string representations of the key and value.</summary>
		/// <returns>A string representation of the <see cref="T:System.Collections.Generic.KeyValuePair`2" />, which includes the string representations of the key and value.</returns>
		// Token: 0x06004276 RID: 17014 RVA: 0x000E3B8C File Offset: 0x000E1D8C
		public override string ToString()
		{
			string[] array = new string[5];
			array[0] = "[";
			int num = 1;
			string text;
			if (this.Key != null)
			{
				TKey tkey = this.Key;
				text = tkey.ToString();
			}
			else
			{
				text = string.Empty;
			}
			array[num] = text;
			array[2] = ", ";
			int num2 = 3;
			string text2;
			if (this.Value != null)
			{
				TValue tvalue = this.Value;
				text2 = tvalue.ToString();
			}
			else
			{
				text2 = string.Empty;
			}
			array[num2] = text2;
			array[4] = "]";
			return string.Concat(array);
		}

		// Token: 0x04001C45 RID: 7237
		private TKey key;

		// Token: 0x04001C46 RID: 7238
		private TValue value;
	}
}
