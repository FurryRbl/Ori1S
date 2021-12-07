using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	/// <summary>Stores the value of a <see cref="T:System.Decimal" /> constant in metadata. This class cannot be inherited.</summary>
	// Token: 0x02000057 RID: 87
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class DecimalConstantAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.DecimalConstantAttribute" /> class with the specified unsigned integer values.</summary>
		/// <param name="scale">The power of 10 scaling factor that indicates the number of digits to the right of the decimal point. Valid values are 0 through 28 inclusive. </param>
		/// <param name="sign">A value of 0 indicates a positive value, and a value of 1 indicates a negative value. </param>
		/// <param name="hi">The high 32 bits of the 96-bit <see cref="P:System.Runtime.CompilerServices.DecimalConstantAttribute.Value" />. </param>
		/// <param name="mid">The middle 32 bits of the 96-bit <see cref="P:System.Runtime.CompilerServices.DecimalConstantAttribute.Value" />. </param>
		/// <param name="low">The low 32 bits of the 96-bit <see cref="P:System.Runtime.CompilerServices.DecimalConstantAttribute.Value" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="scale" /> &gt; 28. </exception>
		// Token: 0x06000687 RID: 1671 RVA: 0x00014C78 File Offset: 0x00012E78
		[CLSCompliant(false)]
		public DecimalConstantAttribute(byte scale, byte sign, uint hi, uint mid, uint low)
		{
			this.scale = scale;
			this.sign = Convert.ToBoolean(sign);
			this.hi = (int)hi;
			this.mid = (int)mid;
			this.low = (int)low;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.DecimalConstantAttribute" /> class with the specified signed integer values. </summary>
		/// <param name="scale">The power of 10 scaling factor that indicates the number of digits to the right of the decimal point. Valid values are 0 through 28 inclusive.</param>
		/// <param name="sign">A value of 0 indicates a positive value, and a value of 1 indicates a negative value.</param>
		/// <param name="hi">The high 32 bits of the 96-bit <see cref="P:System.Runtime.CompilerServices.DecimalConstantAttribute.Value" />.</param>
		/// <param name="mid">The middle 32 bits of the 96-bit <see cref="P:System.Runtime.CompilerServices.DecimalConstantAttribute.Value" />.</param>
		/// <param name="low">The low 32 bits of the 96-bit <see cref="P:System.Runtime.CompilerServices.DecimalConstantAttribute.Value" />.</param>
		// Token: 0x06000688 RID: 1672 RVA: 0x00014CB8 File Offset: 0x00012EB8
		public DecimalConstantAttribute(byte scale, byte sign, int hi, int mid, int low)
		{
			this.scale = scale;
			this.sign = Convert.ToBoolean(sign);
			this.hi = hi;
			this.mid = mid;
			this.low = low;
		}

		/// <summary>Gets the decimal constant stored in this attribute.</summary>
		/// <returns>The decimal constant stored in this attribute.</returns>
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x00014CF8 File Offset: 0x00012EF8
		public decimal Value
		{
			get
			{
				return new decimal(this.low, this.mid, this.hi, this.sign, this.scale);
			}
		}

		// Token: 0x040000A9 RID: 169
		private byte scale;

		// Token: 0x040000AA RID: 170
		private bool sign;

		// Token: 0x040000AB RID: 171
		private int hi;

		// Token: 0x040000AC RID: 172
		private int mid;

		// Token: 0x040000AD RID: 173
		private int low;
	}
}
