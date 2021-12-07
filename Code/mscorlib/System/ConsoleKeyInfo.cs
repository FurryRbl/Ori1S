using System;

namespace System
{
	/// <summary>Describes the console key that was pressed, including the character represented by the console key and the state of the SHIFT, ALT, and CTRL modifier keys.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000115 RID: 277
	[Serializable]
	public struct ConsoleKeyInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ConsoleKeyInfo" /> structure using the specified character, console key, and modifier keys.</summary>
		/// <param name="keyChar">The Unicode character that corresponds to the <paramref name="key" /> parameter. </param>
		/// <param name="key">The console key that corresponds to the <paramref name="keyChar" /> parameter. </param>
		/// <param name="shift">true to indicate that a SHIFT key was pressed; otherwise, false. </param>
		/// <param name="alt">true to indicate that an ALT key was pressed; otherwise, false. </param>
		/// <param name="control">true to indicate that a CTRL key was pressed; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The numeric value of the <paramref name="key" /> parameter is less than 0 or greater than 255.</exception>
		// Token: 0x06000E86 RID: 3718 RVA: 0x0003CD84 File Offset: 0x0003AF84
		public ConsoleKeyInfo(char keyChar, ConsoleKey key, bool shift, bool alt, bool control)
		{
			this.key = key;
			this.keychar = keyChar;
			this.modifiers = (ConsoleModifiers)0;
			this.SetModifiers(shift, alt, control);
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x0003CDB4 File Offset: 0x0003AFB4
		internal ConsoleKeyInfo(ConsoleKeyInfo other)
		{
			this.key = other.key;
			this.keychar = other.keychar;
			this.modifiers = other.modifiers;
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x0003CDF4 File Offset: 0x0003AFF4
		internal void SetKey(ConsoleKey key)
		{
			this.key = key;
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x0003CE00 File Offset: 0x0003B000
		internal void SetKeyChar(char keyChar)
		{
			this.keychar = keyChar;
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x0003CE0C File Offset: 0x0003B00C
		internal void SetModifiers(bool shift, bool alt, bool control)
		{
			this.modifiers = ((!shift) ? ((ConsoleModifiers)0) : ConsoleModifiers.Shift);
			this.modifiers |= ((!alt) ? ((ConsoleModifiers)0) : ConsoleModifiers.Alt);
			this.modifiers |= ((!control) ? ((ConsoleModifiers)0) : ConsoleModifiers.Control);
		}

		/// <summary>Gets the console key represented by the current <see cref="T:System.ConsoleKeyInfo" /> object.</summary>
		/// <returns>A <see cref="T:System.ConsoleKey" /> value that identifies the console key that was pressed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000E8C RID: 3724 RVA: 0x0003CE60 File Offset: 0x0003B060
		public ConsoleKey Key
		{
			get
			{
				return this.key;
			}
		}

		/// <summary>Gets the Unicode character represented by the current <see cref="T:System.ConsoleKeyInfo" /> object.</summary>
		/// <returns>A <see cref="T:System.Char" /> object that corresponds to the console key represented by the current <see cref="T:System.ConsoleKeyInfo" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000E8D RID: 3725 RVA: 0x0003CE68 File Offset: 0x0003B068
		public char KeyChar
		{
			get
			{
				return this.keychar;
			}
		}

		/// <summary>Gets a bitwise combination of <see cref="T:System.ConsoleModifiers" /> values that specifies one or more modifier keys pressed simultaneously with the console key.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.ConsoleModifiers" /> values. There is no default value.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000E8E RID: 3726 RVA: 0x0003CE70 File Offset: 0x0003B070
		public ConsoleModifiers Modifiers
		{
			get
			{
				return this.modifiers;
			}
		}

		/// <summary>Gets a value indicating whether the specified object is equal to the current <see cref="T:System.ConsoleKeyInfo" /> object.</summary>
		/// <returns>true if <paramref name="value" /> is a <see cref="T:System.ConsoleKeyInfo" /> object and is equal to the current <see cref="T:System.ConsoleKeyInfo" /> object; otherwise, false.</returns>
		/// <param name="value">An object to compare to the current <see cref="T:System.ConsoleKeyInfo" /> object.</param>
		// Token: 0x06000E8F RID: 3727 RVA: 0x0003CE78 File Offset: 0x0003B078
		public override bool Equals(object value)
		{
			return value is ConsoleKeyInfo && this.Equals((ConsoleKeyInfo)value);
		}

		/// <summary>Gets a value indicating whether the specified <see cref="T:System.ConsoleKeyInfo" /> object is equal to the current <see cref="T:System.ConsoleKeyInfo" /> object.</summary>
		/// <returns>true if <paramref name="obj" /> is equal to the current <see cref="T:System.ConsoleKeyInfo" /> object; otherwise, false.</returns>
		/// <param name="obj">A <see cref="T:System.ConsoleKeyInfo" /> object to compare to the current <see cref="T:System.ConsoleKeyInfo" /> object.</param>
		// Token: 0x06000E90 RID: 3728 RVA: 0x0003CE94 File Offset: 0x0003B094
		public bool Equals(ConsoleKeyInfo obj)
		{
			return this.key == obj.key && obj.keychar == this.keychar && obj.modifiers == this.modifiers;
		}

		/// <summary>Returns the hash code for the current <see cref="T:System.ConsoleKeyInfo" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000E91 RID: 3729 RVA: 0x0003CED8 File Offset: 0x0003B0D8
		public override int GetHashCode()
		{
			return this.key.GetHashCode() ^ this.keychar.GetHashCode() ^ this.modifiers.GetHashCode();
		}

		/// <summary>Indicates whether the specified <see cref="T:System.ConsoleKeyInfo" /> objects are equal.</summary>
		/// <returns>true if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">A <see cref="T:System.ConsoleKeyInfo" /> object.</param>
		/// <param name="b">A <see cref="T:System.ConsoleKeyInfo" /> object.</param>
		// Token: 0x06000E92 RID: 3730 RVA: 0x0003CF14 File Offset: 0x0003B114
		public static bool operator ==(ConsoleKeyInfo a, ConsoleKeyInfo b)
		{
			return a.Equals(b);
		}

		/// <summary>Indicates whether the specified <see cref="T:System.ConsoleKeyInfo" /> objects are not equal.</summary>
		/// <returns>true if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">A <see cref="T:System.ConsoleKeyInfo" /> object.</param>
		/// <param name="b">A <see cref="T:System.ConsoleKeyInfo" /> object.</param>
		// Token: 0x06000E93 RID: 3731 RVA: 0x0003CF20 File Offset: 0x0003B120
		public static bool operator !=(ConsoleKeyInfo a, ConsoleKeyInfo b)
		{
			return !a.Equals(b);
		}

		// Token: 0x04000473 RID: 1139
		internal static ConsoleKeyInfo Empty = new ConsoleKeyInfo('\0', (ConsoleKey)0, false, false, false);

		// Token: 0x04000474 RID: 1140
		private ConsoleKey key;

		// Token: 0x04000475 RID: 1141
		private char keychar;

		// Token: 0x04000476 RID: 1142
		private ConsoleModifiers modifiers;
	}
}
