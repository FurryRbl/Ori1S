using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Represents a token that represents a string.</summary>
	// Token: 0x020002FE RID: 766
	[ComVisible(true)]
	[Serializable]
	public struct StringToken
	{
		// Token: 0x06002734 RID: 10036 RVA: 0x0008B320 File Offset: 0x00089520
		internal StringToken(int val)
		{
			this.tokValue = val;
		}

		/// <summary>Checks if the given object is an instance of StringToken and is equal to this instance.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of StringToken and is equal to this object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with this StringToken. </param>
		// Token: 0x06002736 RID: 10038 RVA: 0x0008B330 File Offset: 0x00089530
		public override bool Equals(object obj)
		{
			bool flag = obj is StringToken;
			if (flag)
			{
				StringToken stringToken = (StringToken)obj;
				flag = (this.tokValue == stringToken.tokValue);
			}
			return flag;
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.StringToken" />.</summary>
		/// <returns>true if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.StringToken" /> to compare to the current instance.</param>
		// Token: 0x06002737 RID: 10039 RVA: 0x0008B368 File Offset: 0x00089568
		public bool Equals(StringToken obj)
		{
			return this.tokValue == obj.tokValue;
		}

		/// <summary>Returns the hash code for this string.</summary>
		/// <returns>Returns the underlying string token.</returns>
		// Token: 0x06002738 RID: 10040 RVA: 0x0008B37C File Offset: 0x0008957C
		public override int GetHashCode()
		{
			return this.tokValue;
		}

		/// <summary>Retrieves the metadata token for this string.</summary>
		/// <returns>Read-only. Retrieves the metadata token of this string.</returns>
		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06002739 RID: 10041 RVA: 0x0008B384 File Offset: 0x00089584
		public int Token
		{
			get
			{
				return this.tokValue;
			}
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.StringToken" /> structures are equal.</summary>
		/// <returns>true if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.StringToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.StringToken" /> to compare to <paramref name="a" />.</param>
		// Token: 0x0600273A RID: 10042 RVA: 0x0008B38C File Offset: 0x0008958C
		public static bool operator ==(StringToken a, StringToken b)
		{
			return object.Equals(a, b);
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.StringToken" /> structures are not equal.</summary>
		/// <returns>true if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.StringToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.StringToken" /> to compare to <paramref name="a" />.</param>
		// Token: 0x0600273B RID: 10043 RVA: 0x0008B3A0 File Offset: 0x000895A0
		public static bool operator !=(StringToken a, StringToken b)
		{
			return !object.Equals(a, b);
		}

		// Token: 0x04000FDB RID: 4059
		internal int tokValue;
	}
}
