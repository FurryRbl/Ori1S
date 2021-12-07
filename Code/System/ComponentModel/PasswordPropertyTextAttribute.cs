using System;

namespace System.ComponentModel
{
	/// <summary>Indicates that an object's text representation is obscured by characters such as asterisks. This class cannot be inherited.</summary>
	// Token: 0x02000193 RID: 403
	[AttributeUsage(AttributeTargets.All)]
	public sealed class PasswordPropertyTextAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" /> class. </summary>
		// Token: 0x06000E14 RID: 3604 RVA: 0x000244D8 File Offset: 0x000226D8
		public PasswordPropertyTextAttribute() : this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" /> class, optionally showing password text. </summary>
		/// <param name="password">true to indicate that the property should be shown as password text; otherwise, false. The default is false.</param>
		// Token: 0x06000E15 RID: 3605 RVA: 0x000244E4 File Offset: 0x000226E4
		public PasswordPropertyTextAttribute(bool password)
		{
			this._password = password;
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x000244F4 File Offset: 0x000226F4
		static PasswordPropertyTextAttribute()
		{
			PasswordPropertyTextAttribute.Default = PasswordPropertyTextAttribute.No;
		}

		/// <summary>Gets a value indicating if the property for which the <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" /> is defined should be shown as password text.</summary>
		/// <returns>true if the property should be shown as password text; otherwise, false.</returns>
		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000E17 RID: 3607 RVA: 0x00024524 File Offset: 0x00022724
		public bool Password
		{
			get
			{
				return this._password;
			}
		}

		/// <summary>Determines whether two <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" /> instances are equal.</summary>
		/// <returns>true if the specified <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" /> is equal to the current <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" />; otherwise, false.</returns>
		/// <param name="o">The <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" /> to compare with the current <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" />.</param>
		// Token: 0x06000E18 RID: 3608 RVA: 0x0002452C File Offset: 0x0002272C
		public override bool Equals(object o)
		{
			return o is PasswordPropertyTextAttribute && ((PasswordPropertyTextAttribute)o).Password == this.Password;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" />.</returns>
		// Token: 0x06000E19 RID: 3609 RVA: 0x0002455C File Offset: 0x0002275C
		public override int GetHashCode()
		{
			return this.Password.GetHashCode();
		}

		/// <summary>Returns an indication whether the value of this instance is the default value.</summary>
		/// <returns>true if this instance is the default attribute for the class; otherwise, false.</returns>
		// Token: 0x06000E1A RID: 3610 RVA: 0x00024578 File Offset: 0x00022778
		public override bool IsDefaultAttribute()
		{
			return PasswordPropertyTextAttribute.Default.Equals(this);
		}

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" />.</summary>
		// Token: 0x040003FC RID: 1020
		public static readonly PasswordPropertyTextAttribute Default;

		/// <summary>Specifies that a text property is not used as a password. This static (Shared in Visual Basic) field is read-only.</summary>
		// Token: 0x040003FD RID: 1021
		public static readonly PasswordPropertyTextAttribute No = new PasswordPropertyTextAttribute(false);

		/// <summary>Specifies that a text property is used as a password. This static (Shared in Visual Basic) field is read-only.</summary>
		// Token: 0x040003FE RID: 1022
		public static readonly PasswordPropertyTextAttribute Yes = new PasswordPropertyTextAttribute(true);

		// Token: 0x040003FF RID: 1023
		private bool _password;
	}
}
