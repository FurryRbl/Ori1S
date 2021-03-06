using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Text
{
	/// <summary>Represents a mutable string of characters. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200068A RID: 1674
	[MonoTODO("Serialization format not compatible with .NET")]
	[ComVisible(true)]
	[Serializable]
	public sealed class StringBuilder : ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.StringBuilder" /> class from the specified substring and capacity.</summary>
		/// <param name="value">The string that contains the substring used to initialize the value of this instance. If <paramref name="value" /> is null, the new <see cref="T:System.Text.StringBuilder" /> will contain the empty string (that is, it contains <see cref="F:System.String.Empty" />). </param>
		/// <param name="startIndex">The position within <paramref name="value" /> where the substring begins. </param>
		/// <param name="length">The number of characters in the substring. </param>
		/// <param name="capacity">The suggested starting size of the <see cref="T:System.Text.StringBuilder" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.-or- <paramref name="startIndex" /> plus <paramref name="length" /> is not a position within <paramref name="value" />. </exception>
		// Token: 0x06003F8A RID: 16266 RVA: 0x000D9FFC File Offset: 0x000D81FC
		public StringBuilder(string value, int startIndex, int length, int capacity) : this(value, startIndex, length, capacity, int.MaxValue)
		{
		}

		// Token: 0x06003F8B RID: 16267 RVA: 0x000DA010 File Offset: 0x000D8210
		private StringBuilder(string value, int startIndex, int length, int capacity, int maxCapacity)
		{
			if (value == null)
			{
				value = string.Empty;
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");
			}
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", capacity, "capacity must be greater than zero.");
			}
			if (maxCapacity < 1)
			{
				throw new ArgumentOutOfRangeException("maxCapacity", "maxCapacity is less than one.");
			}
			if (capacity > maxCapacity)
			{
				throw new ArgumentOutOfRangeException("capacity", "Capacity exceeds maximum capacity.");
			}
			if (startIndex > value.Length - length)
			{
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex and length must refer to a location within the string.");
			}
			if (capacity == 0)
			{
				if (maxCapacity > 16)
				{
					capacity = 16;
				}
				else
				{
					this._str = (this._cached_str = string.Empty);
				}
			}
			this._maxCapacity = maxCapacity;
			if (this._str == null)
			{
				this._str = string.InternalAllocateStr((length <= capacity) ? capacity : length);
			}
			if (length > 0)
			{
				string.CharCopy(this._str, 0, value, startIndex, length);
			}
			this._length = length;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.StringBuilder" /> class.</summary>
		// Token: 0x06003F8C RID: 16268 RVA: 0x000DA158 File Offset: 0x000D8358
		public StringBuilder() : this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.StringBuilder" /> class using the specified capacity.</summary>
		/// <param name="capacity">The suggested starting size of this instance. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero. </exception>
		// Token: 0x06003F8D RID: 16269 RVA: 0x000DA164 File Offset: 0x000D8364
		public StringBuilder(int capacity) : this(string.Empty, 0, 0, capacity)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.StringBuilder" /> class that starts with a specified capacity and can grow to a specified maximum.</summary>
		/// <param name="capacity">The suggested starting size of the <see cref="T:System.Text.StringBuilder" />. </param>
		/// <param name="maxCapacity">The maximum number of characters the current string can contain. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maxCapacity" /> is less than one, <paramref name="capacity" /> is less than zero, or <paramref name="capacity" /> is greater than <paramref name="maxCapacity" />. </exception>
		// Token: 0x06003F8E RID: 16270 RVA: 0x000DA174 File Offset: 0x000D8374
		public StringBuilder(int capacity, int maxCapacity) : this(string.Empty, 0, 0, capacity, maxCapacity)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.StringBuilder" /> class using the specified string.</summary>
		/// <param name="value">The string used to initialize the value of the instance. If <paramref name="value" /> is null, the new <see cref="T:System.Text.StringBuilder" /> will contain the empty string (that is, it contains <see cref="F:System.String.Empty" />). </param>
		// Token: 0x06003F8F RID: 16271 RVA: 0x000DA188 File Offset: 0x000D8388
		public StringBuilder(string value)
		{
			if (value == null)
			{
				value = string.Empty;
			}
			this._length = value.Length;
			this._str = (this._cached_str = value);
			this._maxCapacity = int.MaxValue;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.StringBuilder" /> class using the specified string and capacity.</summary>
		/// <param name="value">The string used to initialize the value of the instance. If <paramref name="value" /> is null, the new <see cref="T:System.Text.StringBuilder" /> will contain the empty string (that is, it contains <see cref="F:System.String.Empty" />). </param>
		/// <param name="capacity">The suggested starting size of the <see cref="T:System.Text.StringBuilder" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero. </exception>
		// Token: 0x06003F90 RID: 16272 RVA: 0x000DA1D0 File Offset: 0x000D83D0
		public StringBuilder(string value, int capacity) : this((value != null) ? value : string.Empty, 0, (value != null) ? value.Length : 0, capacity)
		{
		}

		// Token: 0x06003F91 RID: 16273 RVA: 0x000DA208 File Offset: 0x000D8408
		private StringBuilder(SerializationInfo info, StreamingContext context)
		{
			string text = info.GetString("m_StringValue");
			if (text == null)
			{
				text = string.Empty;
			}
			this._length = text.Length;
			this._str = (this._cached_str = text);
			this._maxCapacity = info.GetInt32("m_MaxCapacity");
			if (this._maxCapacity < 0)
			{
				this._maxCapacity = int.MaxValue;
			}
			this.Capacity = info.GetInt32("Capacity");
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data necessary to deserialize the current <see cref="T:System.Text.StringBuilder" /> object.</summary>
		/// <param name="info">The object to populate with serialization information.</param>
		/// <param name="context">The place to store and retrieve serialized data. Reserved for future use.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		// Token: 0x06003F92 RID: 16274 RVA: 0x000DA288 File Offset: 0x000D8488
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("m_MaxCapacity", this._maxCapacity);
			info.AddValue("Capacity", this.Capacity);
			info.AddValue("m_StringValue", this.ToString());
			info.AddValue("m_currentThread", 0);
		}

		/// <summary>Gets the maximum capacity of this instance.</summary>
		/// <returns>The maximum number of characters this instance can hold.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x06003F93 RID: 16275 RVA: 0x000DA2D4 File Offset: 0x000D84D4
		public int MaxCapacity
		{
			get
			{
				return this._maxCapacity;
			}
		}

		/// <summary>Gets or sets the maximum number of characters that can be contained in the memory allocated by the current instance.</summary>
		/// <returns>The maximum number of characters that can be contained in the memory allocated by the current instance.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than the current length of this instance.-or- The value specified for a set operation is greater than the maximum capacity. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x06003F94 RID: 16276 RVA: 0x000DA2DC File Offset: 0x000D84DC
		// (set) Token: 0x06003F95 RID: 16277 RVA: 0x000DA308 File Offset: 0x000D8508
		public int Capacity
		{
			get
			{
				if (this._str.Length == 0)
				{
					return Math.Min(this._maxCapacity, 16);
				}
				return this._str.Length;
			}
			set
			{
				if (value < this._length)
				{
					throw new ArgumentException("Capacity must be larger than length");
				}
				if (value > this._maxCapacity)
				{
					throw new ArgumentOutOfRangeException("value", "Should be less than or equal to MaxCapacity");
				}
				this.InternalEnsureCapacity(value);
			}
		}

		/// <summary>Gets or sets the length of the current <see cref="T:System.Text.StringBuilder" /> object.</summary>
		/// <returns>The length of this instance.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than zero or greater than <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x06003F96 RID: 16278 RVA: 0x000DA350 File Offset: 0x000D8550
		// (set) Token: 0x06003F97 RID: 16279 RVA: 0x000DA358 File Offset: 0x000D8558
		public int Length
		{
			get
			{
				return this._length;
			}
			set
			{
				if (value < 0 || value > this._maxCapacity)
				{
					throw new ArgumentOutOfRangeException();
				}
				if (value == this._length)
				{
					return;
				}
				if (value < this._length)
				{
					this.InternalEnsureCapacity(value);
					this._length = value;
				}
				else
				{
					this.Append('\0', value - this._length);
				}
			}
		}

		/// <summary>Gets or sets the character at the specified character position in this instance.</summary>
		/// <returns>The Unicode character at position <paramref name="index" />.</returns>
		/// <param name="index">The position of the character. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the bounds of this instance while setting a character. </exception>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index" /> is outside the bounds of this instance while getting a character. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000C1A RID: 3098
		[IndexerName("Chars")]
		public char this[int index]
		{
			get
			{
				if (index >= this._length || index < 0)
				{
					throw new IndexOutOfRangeException();
				}
				return this._str[index];
			}
			set
			{
				if (index >= this._length || index < 0)
				{
					throw new IndexOutOfRangeException();
				}
				if (this._cached_str != null)
				{
					this.InternalEnsureCapacity(this._length);
				}
				this._str.InternalSetChar(index, value);
			}
		}

		/// <summary>Converts the value of this instance to a <see cref="T:System.String" />.</summary>
		/// <returns>A string whose value is the same as this instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F9A RID: 16282 RVA: 0x000DA424 File Offset: 0x000D8624
		public override string ToString()
		{
			if (this._length == 0)
			{
				return string.Empty;
			}
			if (this._cached_str != null)
			{
				return this._cached_str;
			}
			if (this._length < this._str.Length >> 1)
			{
				this._cached_str = this._str.SubstringUnchecked(0, this._length);
				return this._cached_str;
			}
			this._cached_str = this._str;
			this._str.InternalSetLength(this._length);
			return this._str;
		}

		/// <summary>Converts the value of a substring of this instance to a <see cref="T:System.String" />.</summary>
		/// <returns>A string whose value is the same as the specified substring of this instance.</returns>
		/// <param name="startIndex">The starting position of the substring in this instance. </param>
		/// <param name="length">The length of the substring. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> or <paramref name="length" /> is less than zero.-or- The sum of <paramref name="startIndex" /> and <paramref name="length" /> is greater than the length of the current instance. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F9B RID: 16283 RVA: 0x000DA4B0 File Offset: 0x000D86B0
		public string ToString(int startIndex, int length)
		{
			if (startIndex < 0 || length < 0 || startIndex > this._length - length)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (startIndex == 0 && length == this._length)
			{
				return this.ToString();
			}
			return this._str.SubstringUnchecked(startIndex, length);
		}

		/// <summary>Ensures that the capacity of this instance of <see cref="T:System.Text.StringBuilder" /> is at least the specified value.</summary>
		/// <returns>The new capacity of this instance.</returns>
		/// <param name="capacity">The minimum capacity to ensure. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003F9C RID: 16284 RVA: 0x000DA508 File Offset: 0x000D8708
		public int EnsureCapacity(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("Capacity must be greater than 0.");
			}
			if (capacity <= this._str.Length)
			{
				return this._str.Length;
			}
			this.InternalEnsureCapacity(capacity);
			return this._str.Length;
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <returns>true if this instance and <paramref name="sb" /> have equal string, <see cref="P:System.Text.StringBuilder.Capacity" />, and <see cref="P:System.Text.StringBuilder.MaxCapacity" /> values; otherwise, false.</returns>
		/// <param name="sb">An object to compare with this instance or null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003F9D RID: 16285 RVA: 0x000DA558 File Offset: 0x000D8758
		public bool Equals(StringBuilder sb)
		{
			return sb != null && (this._length == sb.Length && this._str == sb._str);
		}

		/// <summary>Removes the specified range of characters from this instance.</summary>
		/// <returns>A reference to this instance after the excise operation has completed.</returns>
		/// <param name="startIndex">The zero-based position in this instance where removal begins. </param>
		/// <param name="length">The number of characters to remove. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">If <paramref name="startIndex" /> or <paramref name="length" /> is less than zero, or <paramref name="startIndex" /> + <paramref name="length" /> is greater than the length of this instance. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F9E RID: 16286 RVA: 0x000DA598 File Offset: 0x000D8798
		public StringBuilder Remove(int startIndex, int length)
		{
			if (startIndex < 0 || length < 0 || startIndex > this._length - length)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (this._cached_str != null)
			{
				this.InternalEnsureCapacity(this._length);
			}
			if (this._length - (startIndex + length) > 0)
			{
				string.CharCopy(this._str, startIndex, this._str, startIndex + length, this._length - (startIndex + length));
			}
			this._length -= length;
			return this;
		}

		/// <summary>Replaces all occurrences of a specified character in this instance with another specified character.</summary>
		/// <returns>A reference to this instance with <paramref name="oldChar" /> replaced by <paramref name="newChar" />.</returns>
		/// <param name="oldChar">The character to replace. </param>
		/// <param name="newChar">The character that replaces <paramref name="oldChar" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F9F RID: 16287 RVA: 0x000DA61C File Offset: 0x000D881C
		public StringBuilder Replace(char oldChar, char newChar)
		{
			return this.Replace(oldChar, newChar, 0, this._length);
		}

		/// <summary>Replaces, within a substring of this instance, all occurrences of a specified character with another specified character.</summary>
		/// <returns>A reference to this instance with <paramref name="oldChar" /> replaced by <paramref name="newChar" /> in the range from <paramref name="startIndex" /> to <paramref name="startIndex" /> + <paramref name="count" /> -1.</returns>
		/// <param name="oldChar">The character to replace. </param>
		/// <param name="newChar">The character that replaces <paramref name="oldChar" />. </param>
		/// <param name="startIndex">The position in this instance where the substring begins. </param>
		/// <param name="count">The length of the substring. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> + <paramref name="count" /> is greater than the length of the value of this instance.-or- <paramref name="startIndex" /> or <paramref name="count" /> is less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FA0 RID: 16288 RVA: 0x000DA630 File Offset: 0x000D8830
		public StringBuilder Replace(char oldChar, char newChar, int startIndex, int count)
		{
			if (startIndex > this._length - count || startIndex < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (this._cached_str != null)
			{
				this.InternalEnsureCapacity(this._str.Length);
			}
			for (int i = startIndex; i < startIndex + count; i++)
			{
				if (this._str[i] == oldChar)
				{
					this._str.InternalSetChar(i, newChar);
				}
			}
			return this;
		}

		/// <summary>Replaces all occurrences of a specified string in this instance with another specified string.</summary>
		/// <returns>A reference to this instance with all instances of <paramref name="oldValue" /> replaced by <paramref name="newValue" />.</returns>
		/// <param name="oldValue">The string to replace. </param>
		/// <param name="newValue">The string that replaces <paramref name="oldValue" />, or null. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="oldValue" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="oldvalue" /> is zero. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FA1 RID: 16289 RVA: 0x000DA6B4 File Offset: 0x000D88B4
		public StringBuilder Replace(string oldValue, string newValue)
		{
			return this.Replace(oldValue, newValue, 0, this._length);
		}

		/// <summary>Replaces, within a substring of this instance, all occurrences of a specified string with another specified string.</summary>
		/// <returns>A reference to this instance with all instances of <paramref name="oldValue" /> replaced by <paramref name="newValue" /> in the range from <paramref name="startIndex" /> to <paramref name="startIndex" /> + <paramref name="count" /> - 1.</returns>
		/// <param name="oldValue">The string to replace. </param>
		/// <param name="newValue">The string that replaces <paramref name="oldValue" />, or null. </param>
		/// <param name="startIndex">The position in this instance where the substring begins. </param>
		/// <param name="count">The length of the substring. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="oldValue" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="oldvalue" /> is zero. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> or <paramref name="count" /> is less than zero.-or- <paramref name="startIndex" /> plus <paramref name="count" /> indicates a character position not within this instance.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FA2 RID: 16290 RVA: 0x000DA6C8 File Offset: 0x000D88C8
		public StringBuilder Replace(string oldValue, string newValue, int startIndex, int count)
		{
			if (oldValue == null)
			{
				throw new ArgumentNullException("The old value cannot be null.");
			}
			if (startIndex < 0 || count < 0 || startIndex > this._length - count)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (oldValue.Length == 0)
			{
				throw new ArgumentException("The old value cannot be zero length.");
			}
			string text = this._str.Substring(startIndex, count);
			string text2 = text.Replace(oldValue, newValue);
			if (text2 == text)
			{
				return this;
			}
			this.InternalEnsureCapacity(text2.Length + (this._length - count));
			if (text2.Length < count)
			{
				string.CharCopy(this._str, startIndex + text2.Length, this._str, startIndex + count, this._length - startIndex - count);
			}
			else if (text2.Length > count)
			{
				string.CharCopyReverse(this._str, startIndex + text2.Length, this._str, startIndex + count, this._length - startIndex - count);
			}
			string.CharCopy(this._str, startIndex, text2, 0, text2.Length);
			this._length = text2.Length + (this._length - count);
			return this;
		}

		/// <summary>Appends the string representation of the Unicode characters in a specified array to the end of this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The array of characters to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FA3 RID: 16291 RVA: 0x000DA7F4 File Offset: 0x000D89F4
		public StringBuilder Append(char[] value)
		{
			if (value == null)
			{
				return this;
			}
			int num = this._length + value.Length;
			if (this._cached_str != null || this._str.Length < num)
			{
				this.InternalEnsureCapacity(num);
			}
			string.CharCopy(this._str, this._length, value, 0, value.Length);
			this._length = num;
			return this;
		}

		/// <summary>Appends a copy of the specified string to the end of this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The string to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FA4 RID: 16292 RVA: 0x000DA858 File Offset: 0x000D8A58
		public StringBuilder Append(string value)
		{
			if (value == null)
			{
				return this;
			}
			if (this._length == 0 && value.Length < this._maxCapacity && value.Length > this._str.Length)
			{
				this._length = value.Length;
				this._cached_str = value;
				this._str = value;
				return this;
			}
			int num = this._length + value.Length;
			if (this._cached_str != null || this._str.Length < num)
			{
				this.InternalEnsureCapacity(num);
			}
			string.CharCopy(this._str, this._length, value, 0, value.Length);
			this._length = num;
			return this;
		}

		/// <summary>Appends the string representation of a specified Boolean value to the end of this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The Boolean value to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FA5 RID: 16293 RVA: 0x000DA910 File Offset: 0x000D8B10
		public StringBuilder Append(bool value)
		{
			return this.Append(value.ToString());
		}

		/// <summary>Appends the string representation of a specified 8-bit unsigned integer to the end of this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The value to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FA6 RID: 16294 RVA: 0x000DA920 File Offset: 0x000D8B20
		public StringBuilder Append(byte value)
		{
			return this.Append(value.ToString());
		}

		/// <summary>Appends the string representation of a specified decimal number to the end of this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The value to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FA7 RID: 16295 RVA: 0x000DA930 File Offset: 0x000D8B30
		public StringBuilder Append(decimal value)
		{
			return this.Append(value.ToString());
		}

		/// <summary>Appends the string representation of a specified double-precision floating-point number to the end of this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The value to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FA8 RID: 16296 RVA: 0x000DA940 File Offset: 0x000D8B40
		public StringBuilder Append(double value)
		{
			return this.Append(value.ToString());
		}

		/// <summary>Appends the string representation of a specified 16-bit signed integer to the end of this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The value to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FA9 RID: 16297 RVA: 0x000DA950 File Offset: 0x000D8B50
		public StringBuilder Append(short value)
		{
			return this.Append(value.ToString());
		}

		/// <summary>Appends the string representation of a specified 32-bit signed integer to the end of this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The value to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FAA RID: 16298 RVA: 0x000DA960 File Offset: 0x000D8B60
		public StringBuilder Append(int value)
		{
			return this.Append(value.ToString());
		}

		/// <summary>Appends the string representation of a specified 64-bit signed integer to the end of this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The value to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FAB RID: 16299 RVA: 0x000DA970 File Offset: 0x000D8B70
		public StringBuilder Append(long value)
		{
			return this.Append(value.ToString());
		}

		/// <summary>Appends the string representation of a specified object to the end of this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The object to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FAC RID: 16300 RVA: 0x000DA980 File Offset: 0x000D8B80
		public StringBuilder Append(object value)
		{
			if (value == null)
			{
				return this;
			}
			return this.Append(value.ToString());
		}

		/// <summary>Appends the string representation of a specified 8-bit signed integer to the end of this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The value to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FAD RID: 16301 RVA: 0x000DA998 File Offset: 0x000D8B98
		[CLSCompliant(false)]
		public StringBuilder Append(sbyte value)
		{
			return this.Append(value.ToString());
		}

		/// <summary>Appends the string representation of a specified single-precision floating-point number to the end of this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The value to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FAE RID: 16302 RVA: 0x000DA9A8 File Offset: 0x000D8BA8
		public StringBuilder Append(float value)
		{
			return this.Append(value.ToString());
		}

		/// <summary>Appends the string representation of a specified 16-bit unsigned integer to the end of this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The value to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FAF RID: 16303 RVA: 0x000DA9B8 File Offset: 0x000D8BB8
		[CLSCompliant(false)]
		public StringBuilder Append(ushort value)
		{
			return this.Append(value.ToString());
		}

		/// <summary>Appends the string representation of a specified 32-bit unsigned integer to the end of this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The value to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FB0 RID: 16304 RVA: 0x000DA9C8 File Offset: 0x000D8BC8
		[CLSCompliant(false)]
		public StringBuilder Append(uint value)
		{
			return this.Append(value.ToString());
		}

		/// <summary>Appends the string representation of a specified 64-bit unsigned integer to the end of this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The value to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FB1 RID: 16305 RVA: 0x000DA9D8 File Offset: 0x000D8BD8
		[CLSCompliant(false)]
		public StringBuilder Append(ulong value)
		{
			return this.Append(value.ToString());
		}

		/// <summary>Appends the string representation of a specified Unicode character to the end of this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The Unicode character to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FB2 RID: 16306 RVA: 0x000DA9E8 File Offset: 0x000D8BE8
		public StringBuilder Append(char value)
		{
			int num = this._length + 1;
			if (this._cached_str != null || this._str.Length < num)
			{
				this.InternalEnsureCapacity(num);
			}
			this._str.InternalSetChar(this._length, value);
			this._length = num;
			return this;
		}

		/// <summary>Appends a specified number of copies of the string representation of a Unicode character to the end of this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The character to append. </param>
		/// <param name="repeatCount">The number of times to append <paramref name="value" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="repeatCount" /> is less than zero.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <exception cref="T:System.OutOfMemoryException">Out of memory.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FB3 RID: 16307 RVA: 0x000DAA3C File Offset: 0x000D8C3C
		public StringBuilder Append(char value, int repeatCount)
		{
			if (repeatCount < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			this.InternalEnsureCapacity(this._length + repeatCount);
			for (int i = 0; i < repeatCount; i++)
			{
				this._str.InternalSetChar(this._length++, value);
			}
			return this;
		}

		/// <summary>Appends the string representation of a specified subarray of Unicode characters to the end of this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">A character array. </param>
		/// <param name="startIndex">The zero-based starting position in <paramref name="value" />. </param>
		/// <param name="charCount">The number of characters to append. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null, and <paramref name="startIndex" /> and <paramref name="charCount" /> are not zero. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="charCount" /> is less than zero.-or- <paramref name="startIndex" /> is less than zero.-or- <paramref name="startIndex" /> + <paramref name="charCount" /> is greater than the length of <paramref name="value" />.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FB4 RID: 16308 RVA: 0x000DAA94 File Offset: 0x000D8C94
		public StringBuilder Append(char[] value, int startIndex, int charCount)
		{
			if (value == null)
			{
				if (startIndex != 0 || charCount != 0)
				{
					throw new ArgumentNullException("value");
				}
				return this;
			}
			else
			{
				if (charCount < 0 || startIndex < 0 || startIndex > value.Length - charCount)
				{
					throw new ArgumentOutOfRangeException();
				}
				int num = this._length + charCount;
				this.InternalEnsureCapacity(num);
				string.CharCopy(this._str, this._length, value, startIndex, charCount);
				this._length = num;
				return this;
			}
		}

		/// <summary>Appends a copy of a specified substring to the end of this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The string that contains the substring to append. </param>
		/// <param name="startIndex">The zero-based starting position of the substring within <paramref name="value" />. </param>
		/// <param name="count">The number of characters in <paramref name="value" /> to append. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null, and <paramref name="startIndex" /> and <paramref name="count" /> are not zero. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> less than zero.-or- <paramref name="startIndex" /> less than zero.-or- <paramref name="startIndex" /> + <paramref name="count" /> is greater than the length of <paramref name="value" />.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FB5 RID: 16309 RVA: 0x000DAB0C File Offset: 0x000D8D0C
		public StringBuilder Append(string value, int startIndex, int count)
		{
			if (value == null)
			{
				if (startIndex != 0 && count != 0)
				{
					throw new ArgumentNullException("value");
				}
				return this;
			}
			else
			{
				if (count < 0 || startIndex < 0 || startIndex > value.Length - count)
				{
					throw new ArgumentOutOfRangeException();
				}
				int num = this._length + count;
				if (this._cached_str != null || this._str.Length < num)
				{
					this.InternalEnsureCapacity(num);
				}
				string.CharCopy(this._str, this._length, value, startIndex, count);
				this._length = num;
				return this;
			}
		}

		/// <summary>Appends the default line terminator to the end of the current <see cref="T:System.Text.StringBuilder" /> object.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FB6 RID: 16310 RVA: 0x000DABA4 File Offset: 0x000D8DA4
		[ComVisible(false)]
		public StringBuilder AppendLine()
		{
			return this.Append(Environment.NewLine);
		}

		/// <summary>Appends a copy of the specified string followed by the default line terminator to the end of the current <see cref="T:System.Text.StringBuilder" /> object.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The <see cref="T:System.String" /> to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FB7 RID: 16311 RVA: 0x000DABB4 File Offset: 0x000D8DB4
		[ComVisible(false)]
		public StringBuilder AppendLine(string value)
		{
			return this.Append(value).Append(Environment.NewLine);
		}

		/// <summary>Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance. Each format item is replaced by the string representation of a corresponding argument in a parameter array.</summary>
		/// <returns>A reference to this instance with <paramref name="format" /> appended. Each format item in <paramref name="format" /> is replaced by the string representation of the corresponding object argument.</returns>
		/// <param name="format">A composite format string (see Remarks). </param>
		/// <param name="args">An array of objects to format. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> or <paramref name="args" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid. -or-The index of a format item is less than 0 (zero), or greater than or equal to the length of the <paramref name="args" /> array.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of the expanded string would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003FB8 RID: 16312 RVA: 0x000DABC8 File Offset: 0x000D8DC8
		public StringBuilder AppendFormat(string format, params object[] args)
		{
			return this.AppendFormat(null, format, args);
		}

		/// <summary>Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance. Each format item is replaced by the string representation of a corresponding argument in a parameter array using a specified format provider.</summary>
		/// <returns>A reference to this instance after the append operation has completed. After the append operation, this instance contains any data that existed before the operation, suffixed by a copy of <paramref name="format" />, where each format item is replaced by the string representation of the corresponding object argument. </returns>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <param name="format">A composite format string (see Remarks). </param>
		/// <param name="args">An array of objects to format.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.-or-The index of a format item is less than 0 (zero), or greater than or equal to the length of the <paramref name="args" /> array. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of the expanded  string would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003FB9 RID: 16313 RVA: 0x000DABD4 File Offset: 0x000D8DD4
		public StringBuilder AppendFormat(IFormatProvider provider, string format, params object[] args)
		{
			string.FormatHelper(this, provider, format, args);
			return this;
		}

		/// <summary>Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance. Each format item is replaced by the string representation of a single argument.</summary>
		/// <returns>A reference to this instance with <paramref name="format" /> appended. Each format item in <paramref name="format" /> is replaced by the string representation of <paramref name="arg0" />.</returns>
		/// <param name="format">A composite format string (see Remarks). </param>
		/// <param name="arg0">An object to format. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid. -or-The index of a format item is less than 0 (zero), or greater than or equal to 1.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of the expanded string would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003FBA RID: 16314 RVA: 0x000DABE4 File Offset: 0x000D8DE4
		public StringBuilder AppendFormat(string format, object arg0)
		{
			return this.AppendFormat(null, format, new object[]
			{
				arg0
			});
		}

		/// <summary>Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance. Each format item is replaced by the string representation of either of two arguments.</summary>
		/// <returns>A reference to this instance with <paramref name="format" /> appended. Each format item in <paramref name="format" /> is replaced by the string representation of the corresponding object argument.</returns>
		/// <param name="format">A composite format string (see Remarks). </param>
		/// <param name="arg0">The first object to format. </param>
		/// <param name="arg1">The second object to format. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid. -or-The index of a format item is less than 0 (zero), or greater than or equal to 2.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of the expanded string would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003FBB RID: 16315 RVA: 0x000DABF8 File Offset: 0x000D8DF8
		public StringBuilder AppendFormat(string format, object arg0, object arg1)
		{
			return this.AppendFormat(null, format, new object[]
			{
				arg0,
				arg1
			});
		}

		/// <summary>Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance. Each format item is replaced by the string representation of either of three arguments.</summary>
		/// <returns>A reference to this instance with <paramref name="format" /> appended. Each format item in <paramref name="format" /> is replaced by the string representation of the corresponding object argument.</returns>
		/// <param name="format">A composite format string (see Remarks). </param>
		/// <param name="arg0">The first object to format. </param>
		/// <param name="arg1">The second object to format. </param>
		/// <param name="arg2">The third object to format. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid. -or-The index of a format item is less than 0 (zero), or greater than or equal to 3.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of the expanded string would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003FBC RID: 16316 RVA: 0x000DAC10 File Offset: 0x000D8E10
		public StringBuilder AppendFormat(string format, object arg0, object arg1, object arg2)
		{
			return this.AppendFormat(null, format, new object[]
			{
				arg0,
				arg1,
				arg2
			});
		}

		/// <summary>Inserts the string representation of a specified array of Unicode characters into this instance at the specified character position.</summary>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <param name="index">The position in this instance where insertion begins. </param>
		/// <param name="value">The character array to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FBD RID: 16317 RVA: 0x000DAC30 File Offset: 0x000D8E30
		public StringBuilder Insert(int index, char[] value)
		{
			return this.Insert(index, new string(value));
		}

		/// <summary>Inserts a string into this instance at the specified character position.</summary>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <param name="index">The position in this instance where insertion begins. </param>
		/// <param name="value">The string to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the current length of this instance. -or-The current length of this <see cref="T:System.Text.StringBuilder" /> object plus the length of <paramref name="value" /> exceeds <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FBE RID: 16318 RVA: 0x000DAC40 File Offset: 0x000D8E40
		public StringBuilder Insert(int index, string value)
		{
			if (index > this._length || index < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (value == null || value.Length == 0)
			{
				return this;
			}
			this.InternalEnsureCapacity(this._length + value.Length);
			string.CharCopyReverse(this._str, index + value.Length, this._str, index, this._length - index);
			string.CharCopy(this._str, index, value, 0, value.Length);
			this._length += value.Length;
			return this;
		}

		/// <summary>Inserts the string representation of a Boolean value into this instance at the specified character position.</summary>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <param name="index">The position in this instance where insertion begins. </param>
		/// <param name="value">The value to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FBF RID: 16319 RVA: 0x000DACD8 File Offset: 0x000D8ED8
		public StringBuilder Insert(int index, bool value)
		{
			return this.Insert(index, value.ToString());
		}

		/// <summary>Inserts the string representation of a specified 8-bit unsigned integer into this instance at the specified character position.</summary>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <param name="index">The position in this instance where insertion begins. </param>
		/// <param name="value">The value to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FC0 RID: 16320 RVA: 0x000DACE8 File Offset: 0x000D8EE8
		public StringBuilder Insert(int index, byte value)
		{
			return this.Insert(index, value.ToString());
		}

		/// <summary>Inserts the string representation of a specified Unicode character into this instance at the specified character position.</summary>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <param name="index">The position in this instance where insertion begins. </param>
		/// <param name="value">The value to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FC1 RID: 16321 RVA: 0x000DACF8 File Offset: 0x000D8EF8
		public StringBuilder Insert(int index, char value)
		{
			if (index > this._length || index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this.InternalEnsureCapacity(this._length + 1);
			string.CharCopyReverse(this._str, index + 1, this._str, index, this._length - index);
			this._str.InternalSetChar(index, value);
			this._length++;
			return this;
		}

		/// <summary>Inserts the string representation of a decimal number into this instance at the specified character position.</summary>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <param name="index">The position in this instance where insertion begins. </param>
		/// <param name="value">The value to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FC2 RID: 16322 RVA: 0x000DAD6C File Offset: 0x000D8F6C
		public StringBuilder Insert(int index, decimal value)
		{
			return this.Insert(index, value.ToString());
		}

		/// <summary>Inserts the string representation of a double-precision floating-point number into this instance at the specified character position.</summary>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <param name="index">The position in this instance where insertion begins. </param>
		/// <param name="value">The value to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FC3 RID: 16323 RVA: 0x000DAD7C File Offset: 0x000D8F7C
		public StringBuilder Insert(int index, double value)
		{
			return this.Insert(index, value.ToString());
		}

		/// <summary>Inserts the string representation of a specified 16-bit signed integer into this instance at the specified character position.</summary>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <param name="index">The position in this instance where insertion begins. </param>
		/// <param name="value">The value to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FC4 RID: 16324 RVA: 0x000DAD8C File Offset: 0x000D8F8C
		public StringBuilder Insert(int index, short value)
		{
			return this.Insert(index, value.ToString());
		}

		/// <summary>Inserts the string representation of a specified 32-bit signed integer into this instance at the specified character position.</summary>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <param name="index">The position in this instance where insertion begins. </param>
		/// <param name="value">The value to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FC5 RID: 16325 RVA: 0x000DAD9C File Offset: 0x000D8F9C
		public StringBuilder Insert(int index, int value)
		{
			return this.Insert(index, value.ToString());
		}

		/// <summary>Inserts the string representation of a 64-bit signed integer into this instance at the specified character position.</summary>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <param name="index">The position in this instance where insertion begins. </param>
		/// <param name="value">The value to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FC6 RID: 16326 RVA: 0x000DADAC File Offset: 0x000D8FAC
		public StringBuilder Insert(int index, long value)
		{
			return this.Insert(index, value.ToString());
		}

		/// <summary>Inserts the string representation of an object into this instance at the specified character position.</summary>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <param name="index">The position in this instance where insertion begins. </param>
		/// <param name="value">The object to insert or null. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FC7 RID: 16327 RVA: 0x000DADBC File Offset: 0x000D8FBC
		public StringBuilder Insert(int index, object value)
		{
			return this.Insert(index, value.ToString());
		}

		/// <summary>Inserts the string representation of a specified 8-bit signed integer into this instance at the specified character position.</summary>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <param name="index">The position in this instance where insertion begins. </param>
		/// <param name="value">The value to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FC8 RID: 16328 RVA: 0x000DADCC File Offset: 0x000D8FCC
		[CLSCompliant(false)]
		public StringBuilder Insert(int index, sbyte value)
		{
			return this.Insert(index, value.ToString());
		}

		/// <summary>Inserts the string representation of a single-precision floating point number into this instance at the specified character position.</summary>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <param name="index">The position in this instance where insertion begins. </param>
		/// <param name="value">The value to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FC9 RID: 16329 RVA: 0x000DADDC File Offset: 0x000D8FDC
		public StringBuilder Insert(int index, float value)
		{
			return this.Insert(index, value.ToString());
		}

		/// <summary>Inserts the string representation of a 16-bit unsigned integer into this instance at the specified character position.</summary>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <param name="index">The position in this instance where insertion begins. </param>
		/// <param name="value">The value to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FCA RID: 16330 RVA: 0x000DADEC File Offset: 0x000D8FEC
		[CLSCompliant(false)]
		public StringBuilder Insert(int index, ushort value)
		{
			return this.Insert(index, value.ToString());
		}

		/// <summary>Inserts the string representation of a 32-bit unsigned integer into this instance at the specified character position.</summary>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <param name="index">The position in this instance where insertion begins. </param>
		/// <param name="value">The value to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FCB RID: 16331 RVA: 0x000DADFC File Offset: 0x000D8FFC
		[CLSCompliant(false)]
		public StringBuilder Insert(int index, uint value)
		{
			return this.Insert(index, value.ToString());
		}

		/// <summary>Inserts the string representation of a 64-bit unsigned integer into this instance at the specified character position.</summary>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <param name="index">The position in this instance where insertion begins. </param>
		/// <param name="value">The value to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FCC RID: 16332 RVA: 0x000DAE0C File Offset: 0x000D900C
		[CLSCompliant(false)]
		public StringBuilder Insert(int index, ulong value)
		{
			return this.Insert(index, value.ToString());
		}

		/// <summary>Inserts one or more copies of a specified string into this instance at the specified character position.</summary>
		/// <returns>A reference to this instance after insertion has completed.</returns>
		/// <param name="index">The position in this instance where insertion begins. </param>
		/// <param name="value">The string to insert. </param>
		/// <param name="count">The number of times to insert <paramref name="value" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the current length of this instance.-or- <paramref name="count" /> is less than zero. -or-The current length of this <see cref="T:System.Text.StringBuilder" /> object plus the length of <paramref name="value" /> times <paramref name="count" /> exceeds <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FCD RID: 16333 RVA: 0x000DAE1C File Offset: 0x000D901C
		public StringBuilder Insert(int index, string value, int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (value != null && value != string.Empty)
			{
				for (int i = 0; i < count; i++)
				{
					this.Insert(index, value);
				}
			}
			return this;
		}

		/// <summary>Inserts the string representation of a specified subarray of Unicode characters into this instance at the specified character position.</summary>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <param name="index">The position in this instance where insertion begins. </param>
		/// <param name="value">A character array. </param>
		/// <param name="startIndex">The starting index within <paramref name="value" />. </param>
		/// <param name="charCount">The number of characters to insert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null, and <paramref name="startIndex" /> and <paramref name="charCount" /> are not zero. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" />, <paramref name="startIndex" />, or <paramref name="charCount" /> is less than zero.-or- <paramref name="index" /> is greater than the length of this instance.-or- <paramref name="startIndex" /> plus <paramref name="charCount" /> is not a position within <paramref name="value" />.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FCE RID: 16334 RVA: 0x000DAE68 File Offset: 0x000D9068
		public StringBuilder Insert(int index, char[] value, int startIndex, int charCount)
		{
			if (value == null)
			{
				if (startIndex == 0 && charCount == 0)
				{
					return this;
				}
				throw new ArgumentNullException("value");
			}
			else
			{
				if (charCount < 0 || startIndex < 0 || startIndex > value.Length - charCount)
				{
					throw new ArgumentOutOfRangeException();
				}
				return this.Insert(index, new string(value, startIndex, charCount));
			}
		}

		// Token: 0x06003FCF RID: 16335 RVA: 0x000DAEC8 File Offset: 0x000D90C8
		private void InternalEnsureCapacity(int size)
		{
			if (size > this._str.Length || this._cached_str == this._str)
			{
				int num = this._str.Length;
				if (size > num)
				{
					if (this._cached_str == this._str && num < 16)
					{
						num = 16;
					}
					num <<= 1;
					if (size > num)
					{
						num = size;
					}
					if (num >= 2147483647 || num < 0)
					{
						num = int.MaxValue;
					}
					if (num > this._maxCapacity && size <= this._maxCapacity)
					{
						num = this._maxCapacity;
					}
					if (num > this._maxCapacity)
					{
						throw new ArgumentOutOfRangeException("size", "capacity was less than the current size.");
					}
				}
				string text = string.InternalAllocateStr(num);
				if (this._length > 0)
				{
					string.CharCopy(text, 0, this._str, 0, this._length);
				}
				this._str = text;
			}
			this._cached_str = null;
		}

		/// <summary>Copies the characters from a specified segment of this instance to a specified segment of a destination <see cref="T:System.Char" /> array.</summary>
		/// <param name="sourceIndex">The starting position in this instance where characters will be copied from. The index is zero-based.</param>
		/// <param name="destination">The <see cref="T:System.Char" /> array where characters will be copied to.</param>
		/// <param name="destinationIndex">The starting position in <paramref name="destination" /> where characters will be copied to. The index is zero-based.</param>
		/// <param name="count">The number of characters to be copied.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destination" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="sourceIndex" />, <paramref name="destinationIndex" />, or <paramref name="count" />, is less than zero.-or-<paramref name="sourceIndex" /> is greater than the length of this instance.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="sourceIndex" /> + <paramref name="count" /> is greater than the length of this instance.-or-<paramref name="destinationIndex" /> + <paramref name="count" /> is greater than the length of <paramref name="destination" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FD0 RID: 16336 RVA: 0x000DAFBC File Offset: 0x000D91BC
		[ComVisible(false)]
		public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (this.Length - count < sourceIndex || destination.Length - count < destinationIndex || sourceIndex < 0 || destinationIndex < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			for (int i = 0; i < count; i++)
			{
				destination[destinationIndex + i] = this._str[sourceIndex + i];
			}
		}

		// Token: 0x04001B7C RID: 7036
		private const int constDefaultCapacity = 16;

		// Token: 0x04001B7D RID: 7037
		private int _length;

		// Token: 0x04001B7E RID: 7038
		private string _str;

		// Token: 0x04001B7F RID: 7039
		private string _cached_str;

		// Token: 0x04001B80 RID: 7040
		private int _maxCapacity;
	}
}
