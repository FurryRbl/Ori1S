using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Represents the Token returned by the metadata to represent a type.</summary>
	// Token: 0x02000300 RID: 768
	[ComVisible(true)]
	[Serializable]
	public struct TypeToken
	{
		// Token: 0x060027BF RID: 10175 RVA: 0x0008DC10 File Offset: 0x0008BE10
		internal TypeToken(int val)
		{
			this.tokValue = val;
		}

		/// <summary>Checks if the given object is an instance of TypeToken and is equal to this instance.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of TypeToken and is equal to this object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with this TypeToken. </param>
		// Token: 0x060027C1 RID: 10177 RVA: 0x0008DC38 File Offset: 0x0008BE38
		public override bool Equals(object obj)
		{
			bool flag = obj is TypeToken;
			if (flag)
			{
				TypeToken typeToken = (TypeToken)obj;
				flag = (this.tokValue == typeToken.tokValue);
			}
			return flag;
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.TypeToken" />.</summary>
		/// <returns>true if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.TypeToken" /> to compare to the current instance.</param>
		// Token: 0x060027C2 RID: 10178 RVA: 0x0008DC70 File Offset: 0x0008BE70
		public bool Equals(TypeToken obj)
		{
			return this.tokValue == obj.tokValue;
		}

		/// <summary>Generates the hash code for this type.</summary>
		/// <returns>Returns the hash code for this type.</returns>
		// Token: 0x060027C3 RID: 10179 RVA: 0x0008DC84 File Offset: 0x0008BE84
		public override int GetHashCode()
		{
			return this.tokValue;
		}

		/// <summary>Retrieves the metadata token for this class.</summary>
		/// <returns>Read-only. Retrieves the metadata token of this type.</returns>
		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x060027C4 RID: 10180 RVA: 0x0008DC8C File Offset: 0x0008BE8C
		public int Token
		{
			get
			{
				return this.tokValue;
			}
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.TypeToken" /> structures are equal.</summary>
		/// <returns>true if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.TypeToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.TypeToken" /> to compare to <paramref name="a" />.</param>
		// Token: 0x060027C5 RID: 10181 RVA: 0x0008DC94 File Offset: 0x0008BE94
		public static bool operator ==(TypeToken a, TypeToken b)
		{
			return object.Equals(a, b);
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.TypeToken" /> structures are not equal.</summary>
		/// <returns>true if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.TypeToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.TypeToken" /> to compare to <paramref name="a" />.</param>
		// Token: 0x060027C6 RID: 10182 RVA: 0x0008DCA8 File Offset: 0x0008BEA8
		public static bool operator !=(TypeToken a, TypeToken b)
		{
			return !object.Equals(a, b);
		}

		// Token: 0x04000FF8 RID: 4088
		internal int tokValue;

		/// <summary>The default TypeToken with <see cref="P:System.Reflection.Emit.TypeToken.Token" /> value 0.</summary>
		// Token: 0x04000FF9 RID: 4089
		public static readonly TypeToken Empty = default(TypeToken);
	}
}
