using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Represents a named argument of a custom attribute in the reflection-only context.</summary>
	// Token: 0x02000289 RID: 649
	[ComVisible(true)]
	[Serializable]
	public struct CustomAttributeNamedArgument
	{
		// Token: 0x0600213E RID: 8510 RVA: 0x00079C74 File Offset: 0x00077E74
		internal CustomAttributeNamedArgument(MemberInfo memberInfo, object typedArgument)
		{
			this.memberInfo = memberInfo;
			this.typedArgument = (CustomAttributeTypedArgument)typedArgument;
		}

		/// <summary>Gets the attribute member that would be used to set the named argument.</summary>
		/// <returns>A <see cref="T:System.Reflection.MemberInfo" /> representing the attribute member that would be used to set the named argument.</returns>
		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x0600213F RID: 8511 RVA: 0x00079C8C File Offset: 0x00077E8C
		public MemberInfo MemberInfo
		{
			get
			{
				return this.memberInfo;
			}
		}

		/// <summary>Gets a <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> structure that can be used to obtain the type and value of the current named argument.</summary>
		/// <returns>A <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> structure that can be used to obtain the type and value of the current named argument.</returns>
		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06002140 RID: 8512 RVA: 0x00079C94 File Offset: 0x00077E94
		public CustomAttributeTypedArgument TypedValue
		{
			get
			{
				return this.typedArgument;
			}
		}

		/// <summary>Returns a string consisting of the argument name, the equal sign, and a string representation of the argument value.</summary>
		/// <returns>A string consisting of the argument name, the equal sign, and a string representation of the argument value.</returns>
		// Token: 0x06002141 RID: 8513 RVA: 0x00079C9C File Offset: 0x00077E9C
		public override string ToString()
		{
			return this.memberInfo.Name + " = " + this.typedArgument.ToString();
		}

		/// <returns>true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, false.</returns>
		/// <param name="obj">Another object to compare to. </param>
		// Token: 0x06002142 RID: 8514 RVA: 0x00079CCC File Offset: 0x00077ECC
		public override bool Equals(object obj)
		{
			if (!(obj is CustomAttributeNamedArgument))
			{
				return false;
			}
			CustomAttributeNamedArgument customAttributeNamedArgument = (CustomAttributeNamedArgument)obj;
			return customAttributeNamedArgument.memberInfo == this.memberInfo && this.typedArgument.Equals(customAttributeNamedArgument.typedArgument);
		}

		/// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
		// Token: 0x06002143 RID: 8515 RVA: 0x00079D1C File Offset: 0x00077F1C
		public override int GetHashCode()
		{
			return (this.memberInfo.GetHashCode() << 16) + this.typedArgument.GetHashCode();
		}

		/// <summary>Tests whether two <see cref="T:System.Reflection.CustomAttributeNamedArgument" /> structures are equivalent.</summary>
		/// <returns>true if the two <see cref="T:System.Reflection.CustomAttributeNamedArgument" /> structures are equal; otherwise, false.</returns>
		/// <param name="left">The <see cref="T:System.Reflection.CustomAttributeNamedArgument" /> structure to the left of the equality operator.</param>
		/// <param name="right">The <see cref="T:System.Reflection.CustomAttributeNamedArgument" /> structure to the right of the equality operator.</param>
		// Token: 0x06002144 RID: 8516 RVA: 0x00079D38 File Offset: 0x00077F38
		public static bool operator ==(CustomAttributeNamedArgument left, CustomAttributeNamedArgument right)
		{
			return left.Equals(right);
		}

		/// <summary>Tests whether two <see cref="T:System.Reflection.CustomAttributeNamedArgument" /> structures are different.</summary>
		/// <returns>true if the two <see cref="T:System.Reflection.CustomAttributeNamedArgument" /> structures are different; otherwise, false.</returns>
		/// <param name="left">The <see cref="T:System.Reflection.CustomAttributeNamedArgument" /> structure to the left of the inequality operator.</param>
		/// <param name="right">The <see cref="T:System.Reflection.CustomAttributeNamedArgument" /> structure to the right of the inequality operator.</param>
		// Token: 0x06002145 RID: 8517 RVA: 0x00079D48 File Offset: 0x00077F48
		public static bool operator !=(CustomAttributeNamedArgument left, CustomAttributeNamedArgument right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000C43 RID: 3139
		private CustomAttributeTypedArgument typedArgument;

		// Token: 0x04000C44 RID: 3140
		private MemberInfo memberInfo;
	}
}
