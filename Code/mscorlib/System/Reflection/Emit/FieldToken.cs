using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>The FieldToken struct is an object representation of a token that represents a field.</summary>
	// Token: 0x020002D9 RID: 729
	[ComVisible(true)]
	[Serializable]
	public struct FieldToken
	{
		// Token: 0x06002524 RID: 9508 RVA: 0x000835D0 File Offset: 0x000817D0
		internal FieldToken(int val)
		{
			this.tokValue = val;
		}

		/// <summary>Determines if an object is an instance of FieldToken and is equal to this instance.</summary>
		/// <returns>Returns true if <paramref name="obj" /> is an instance of FieldToken and is equal to this object; otherwise, false.</returns>
		/// <param name="obj">The object to compare to this FieldToken. </param>
		// Token: 0x06002526 RID: 9510 RVA: 0x000835F8 File Offset: 0x000817F8
		public override bool Equals(object obj)
		{
			bool flag = obj is FieldToken;
			if (flag)
			{
				FieldToken fieldToken = (FieldToken)obj;
				flag = (this.tokValue == fieldToken.tokValue);
			}
			return flag;
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.FieldToken" />.</summary>
		/// <returns>true if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.FieldToken" /> to compare to the current instance.</param>
		// Token: 0x06002527 RID: 9511 RVA: 0x00083630 File Offset: 0x00081830
		public bool Equals(FieldToken obj)
		{
			return this.tokValue == obj.tokValue;
		}

		/// <summary>Generates the hash code for this field.</summary>
		/// <returns>Returns the hash code for this instance.</returns>
		// Token: 0x06002528 RID: 9512 RVA: 0x00083644 File Offset: 0x00081844
		public override int GetHashCode()
		{
			return this.tokValue;
		}

		/// <summary>Retrieves the metadata token for this field.</summary>
		/// <returns>Read-only. Retrieves the metadata token of this field.</returns>
		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06002529 RID: 9513 RVA: 0x0008364C File Offset: 0x0008184C
		public int Token
		{
			get
			{
				return this.tokValue;
			}
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.FieldToken" /> structures are equal.</summary>
		/// <returns>true if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.FieldToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.FieldToken" /> to compare to <paramref name="a" />.</param>
		// Token: 0x0600252A RID: 9514 RVA: 0x00083654 File Offset: 0x00081854
		public static bool operator ==(FieldToken a, FieldToken b)
		{
			return object.Equals(a, b);
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.FieldToken" /> structures are not equal.</summary>
		/// <returns>true if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.FieldToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.FieldToken" /> to compare to <paramref name="a" />.</param>
		// Token: 0x0600252B RID: 9515 RVA: 0x00083668 File Offset: 0x00081868
		public static bool operator !=(FieldToken a, FieldToken b)
		{
			return !object.Equals(a, b);
		}

		// Token: 0x04000DF1 RID: 3569
		internal int tokValue;

		/// <summary>The default FieldToken with <see cref="P:System.Reflection.Emit.FieldToken.Token" /> value 0.</summary>
		// Token: 0x04000DF2 RID: 3570
		public static readonly FieldToken Empty = default(FieldToken);
	}
}
