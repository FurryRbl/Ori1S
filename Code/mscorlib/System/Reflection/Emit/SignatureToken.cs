using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Represents the Token returned by the metadata to represent a signature.</summary>
	// Token: 0x020002FC RID: 764
	[ComVisible(true)]
	public struct SignatureToken
	{
		// Token: 0x0600272C RID: 10028 RVA: 0x0008B270 File Offset: 0x00089470
		internal SignatureToken(int val)
		{
			this.tokValue = val;
		}

		/// <summary>Checks if the given object is an instance of SignatureToken and is equal to this instance.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of SignatureToken and is equal to this object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with this SignatureToken. </param>
		// Token: 0x0600272E RID: 10030 RVA: 0x0008B298 File Offset: 0x00089498
		public override bool Equals(object obj)
		{
			bool flag = obj is SignatureToken;
			if (flag)
			{
				SignatureToken signatureToken = (SignatureToken)obj;
				flag = (this.tokValue == signatureToken.tokValue);
			}
			return flag;
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.SignatureToken" />.</summary>
		/// <returns>true if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.SignatureToken" /> to compare to the current instance.</param>
		// Token: 0x0600272F RID: 10031 RVA: 0x0008B2D0 File Offset: 0x000894D0
		public bool Equals(SignatureToken obj)
		{
			return this.tokValue == obj.tokValue;
		}

		/// <summary>Generates the hash code for this signature.</summary>
		/// <returns>Returns the hash code for this signature.</returns>
		// Token: 0x06002730 RID: 10032 RVA: 0x0008B2E4 File Offset: 0x000894E4
		public override int GetHashCode()
		{
			return this.tokValue;
		}

		/// <summary>Retrieves the metadata token for the local variable signature for this method.</summary>
		/// <returns>Read-only. Retrieves the metadata token of this signature.</returns>
		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06002731 RID: 10033 RVA: 0x0008B2EC File Offset: 0x000894EC
		public int Token
		{
			get
			{
				return this.tokValue;
			}
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.SignatureToken" /> structures are equal.</summary>
		/// <returns>true if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.SignatureToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.SignatureToken" /> to compare to <paramref name="a" />.</param>
		// Token: 0x06002732 RID: 10034 RVA: 0x0008B2F4 File Offset: 0x000894F4
		public static bool operator ==(SignatureToken a, SignatureToken b)
		{
			return object.Equals(a, b);
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.SignatureToken" /> structures are not equal.</summary>
		/// <returns>true if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.SignatureToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.SignatureToken" /> to compare to <paramref name="a" />.</param>
		// Token: 0x06002733 RID: 10035 RVA: 0x0008B308 File Offset: 0x00089508
		public static bool operator !=(SignatureToken a, SignatureToken b)
		{
			return !object.Equals(a, b);
		}

		// Token: 0x04000FBB RID: 4027
		internal int tokValue;

		/// <summary>The default SignatureToken with <see cref="P:System.Reflection.Emit.SignatureToken.Token" /> value 0.</summary>
		// Token: 0x04000FBC RID: 4028
		public static readonly SignatureToken Empty = default(SignatureToken);
	}
}
