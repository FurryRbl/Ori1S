using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>The ParameterToken struct is an opaque representation of the token returned by the metadata to represent a parameter.</summary>
	// Token: 0x020002F5 RID: 757
	[ComVisible(true)]
	[Serializable]
	public struct ParameterToken
	{
		// Token: 0x060026C9 RID: 9929 RVA: 0x0008A674 File Offset: 0x00088874
		internal ParameterToken(int val)
		{
			this.tokValue = val;
		}

		/// <summary>Checks if the given object is an instance of ParameterToken and is equal to this instance.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of ParameterToken and equals the current instance; otherwise, false.</returns>
		/// <param name="obj">The object to compare to this object. </param>
		// Token: 0x060026CB RID: 9931 RVA: 0x0008A69C File Offset: 0x0008889C
		public override bool Equals(object obj)
		{
			bool flag = obj is ParameterToken;
			if (flag)
			{
				ParameterToken parameterToken = (ParameterToken)obj;
				flag = (this.tokValue == parameterToken.tokValue);
			}
			return flag;
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.ParameterToken" />.</summary>
		/// <returns>true if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.ParameterToken" /> to compare to the current instance.</param>
		// Token: 0x060026CC RID: 9932 RVA: 0x0008A6D4 File Offset: 0x000888D4
		public bool Equals(ParameterToken obj)
		{
			return this.tokValue == obj.tokValue;
		}

		/// <summary>Generates the hash code for this parameter.</summary>
		/// <returns>Returns the hash code for this parameter.</returns>
		// Token: 0x060026CD RID: 9933 RVA: 0x0008A6E8 File Offset: 0x000888E8
		public override int GetHashCode()
		{
			return this.tokValue;
		}

		/// <summary>Retrieves the metadata token for this parameter.</summary>
		/// <returns>Read-only. Retrieves the metadata token for this parameter.</returns>
		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x060026CE RID: 9934 RVA: 0x0008A6F0 File Offset: 0x000888F0
		public int Token
		{
			get
			{
				return this.tokValue;
			}
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.ParameterToken" /> structures are equal.</summary>
		/// <returns>true if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.ParameterToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.ParameterToken" /> to compare to <paramref name="a" />.</param>
		// Token: 0x060026CF RID: 9935 RVA: 0x0008A6F8 File Offset: 0x000888F8
		public static bool operator ==(ParameterToken a, ParameterToken b)
		{
			return object.Equals(a, b);
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.ParameterToken" /> structures are not equal.</summary>
		/// <returns>true if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.ParameterToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.ParameterToken" /> to compare to <paramref name="a" />.</param>
		// Token: 0x060026D0 RID: 9936 RVA: 0x0008A70C File Offset: 0x0008890C
		public static bool operator !=(ParameterToken a, ParameterToken b)
		{
			return !object.Equals(a, b);
		}

		// Token: 0x04000F96 RID: 3990
		internal int tokValue;

		/// <summary>The default ParameterToken with <see cref="P:System.Reflection.Emit.ParameterToken.Token" /> value 0.</summary>
		// Token: 0x04000F97 RID: 3991
		public static readonly ParameterToken Empty = default(ParameterToken);
	}
}
