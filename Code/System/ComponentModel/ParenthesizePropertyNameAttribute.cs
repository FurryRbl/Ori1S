using System;

namespace System.ComponentModel
{
	/// <summary>Indicates whether the name of the associated property is displayed with parentheses in the Properties window. This class cannot be inherited.</summary>
	// Token: 0x02000192 RID: 402
	[AttributeUsage(AttributeTargets.All)]
	public sealed class ParenthesizePropertyNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ParenthesizePropertyNameAttribute" /> class that indicates that the associated property should not be shown with parentheses.</summary>
		// Token: 0x06000E0D RID: 3597 RVA: 0x00024454 File Offset: 0x00022654
		public ParenthesizePropertyNameAttribute()
		{
			this.parenthesis = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ParenthesizePropertyNameAttribute" /> class, using the specified value to indicate whether the attribute is displayed with parentheses.</summary>
		/// <param name="needParenthesis">true if the name should be enclosed in parentheses; otherwise, false. </param>
		// Token: 0x06000E0E RID: 3598 RVA: 0x00024464 File Offset: 0x00022664
		public ParenthesizePropertyNameAttribute(bool needParenthesis)
		{
			this.parenthesis = needParenthesis;
		}

		/// <summary>Gets a value indicating whether the Properties window displays the name of the property in parentheses in the Properties window.</summary>
		/// <returns>true if the property is displayed with parentheses; otherwise, false.</returns>
		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000E10 RID: 3600 RVA: 0x00024480 File Offset: 0x00022680
		public bool NeedParenthesis
		{
			get
			{
				return this.parenthesis;
			}
		}

		/// <summary>Compares the specified object to this object and tests for equality.</summary>
		/// <returns>true if equal; otherwise, false.</returns>
		/// <param name="o">The object to be compared. </param>
		// Token: 0x06000E11 RID: 3601 RVA: 0x00024488 File Offset: 0x00022688
		public override bool Equals(object o)
		{
			return o is ParenthesizePropertyNameAttribute && (o == this || ((ParenthesizePropertyNameAttribute)o).NeedParenthesis == this.parenthesis);
		}

		/// <summary>Gets the hash code for this object.</summary>
		/// <returns>The hash code for the object the attribute belongs to.</returns>
		// Token: 0x06000E12 RID: 3602 RVA: 0x000244B4 File Offset: 0x000226B4
		public override int GetHashCode()
		{
			return this.parenthesis.GetHashCode();
		}

		/// <summary>Gets a value indicating whether the current value of the attribute is the default value for the attribute.</summary>
		/// <returns>true if the current value of the attribute is the default value of the attribute; otherwise, false.</returns>
		// Token: 0x06000E13 RID: 3603 RVA: 0x000244C4 File Offset: 0x000226C4
		public override bool IsDefaultAttribute()
		{
			return this.parenthesis == ParenthesizePropertyNameAttribute.Default.NeedParenthesis;
		}

		// Token: 0x040003FA RID: 1018
		private bool parenthesis;

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ParenthesizePropertyNameAttribute" /> class with a default value that indicates that the associated property should not be shown with parentheses. This field is read-only.</summary>
		// Token: 0x040003FB RID: 1019
		public static readonly ParenthesizePropertyNameAttribute Default = new ParenthesizePropertyNameAttribute();
	}
}
