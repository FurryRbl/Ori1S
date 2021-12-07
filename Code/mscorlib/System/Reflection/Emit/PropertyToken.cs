using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>The PropertyToken struct is an opaque representation of the Token returned by the metadata to represent a property.</summary>
	// Token: 0x020002F9 RID: 761
	[ComVisible(true)]
	[Serializable]
	public struct PropertyToken
	{
		// Token: 0x06002703 RID: 9987 RVA: 0x0008AB2C File Offset: 0x00088D2C
		internal PropertyToken(int val)
		{
			this.tokValue = val;
		}

		/// <summary>Checks if the given object is an instance of PropertyToken and is equal to this instance.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of PropertyToken and equals the current instance; otherwise, false.</returns>
		/// <param name="obj">The object to this object. </param>
		// Token: 0x06002705 RID: 9989 RVA: 0x0008AB54 File Offset: 0x00088D54
		public override bool Equals(object obj)
		{
			bool flag = obj is PropertyToken;
			if (flag)
			{
				PropertyToken propertyToken = (PropertyToken)obj;
				flag = (this.tokValue == propertyToken.tokValue);
			}
			return flag;
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.PropertyToken" />.</summary>
		/// <returns>true if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.PropertyToken" /> to compare to the current instance.</param>
		// Token: 0x06002706 RID: 9990 RVA: 0x0008AB8C File Offset: 0x00088D8C
		public bool Equals(PropertyToken obj)
		{
			return this.tokValue == obj.tokValue;
		}

		/// <summary>Generates the hash code for this property.</summary>
		/// <returns>Returns the hash code for this property.</returns>
		// Token: 0x06002707 RID: 9991 RVA: 0x0008ABA0 File Offset: 0x00088DA0
		public override int GetHashCode()
		{
			return this.tokValue;
		}

		/// <summary>Retrieves the metadata token for this property.</summary>
		/// <returns>Read-only. Retrieves the metadata token for this instance.</returns>
		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06002708 RID: 9992 RVA: 0x0008ABA8 File Offset: 0x00088DA8
		public int Token
		{
			get
			{
				return this.tokValue;
			}
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.PropertyToken" /> structures are equal.</summary>
		/// <returns>true if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.PropertyToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.PropertyToken" /> to compare to <paramref name="a" />.</param>
		// Token: 0x06002709 RID: 9993 RVA: 0x0008ABB0 File Offset: 0x00088DB0
		public static bool operator ==(PropertyToken a, PropertyToken b)
		{
			return object.Equals(a, b);
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.PropertyToken" /> structures are not equal.</summary>
		/// <returns>true if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.PropertyToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.PropertyToken" /> to compare to <paramref name="a" />.</param>
		// Token: 0x0600270A RID: 9994 RVA: 0x0008ABC4 File Offset: 0x00088DC4
		public static bool operator !=(PropertyToken a, PropertyToken b)
		{
			return !object.Equals(a, b);
		}

		// Token: 0x04000FAC RID: 4012
		internal int tokValue;

		/// <summary>The default PropertyToken with <see cref="P:System.Reflection.Emit.PropertyToken.Token" /> value 0.</summary>
		// Token: 0x04000FAD RID: 4013
		public static readonly PropertyToken Empty = default(PropertyToken);
	}
}
