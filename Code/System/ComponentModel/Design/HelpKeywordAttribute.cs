using System;

namespace System.ComponentModel.Design
{
	/// <summary>Specifies the context keyword for a class or member. This class cannot be inherited.</summary>
	// Token: 0x0200010A RID: 266
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
	[Serializable]
	public sealed class HelpKeywordAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" /> class. </summary>
		// Token: 0x06000A9B RID: 2715 RVA: 0x0001D900 File Offset: 0x0001BB00
		public HelpKeywordAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" /> class. </summary>
		/// <param name="keyword">The Help keyword value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> is null.</exception>
		// Token: 0x06000A9C RID: 2716 RVA: 0x0001D908 File Offset: 0x0001BB08
		public HelpKeywordAttribute(string keyword)
		{
			this.contextKeyword = keyword;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" /> class from the given type. </summary>
		/// <param name="t">The type from which the Help keyword will be taken.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="t" /> is null.</exception>
		// Token: 0x06000A9D RID: 2717 RVA: 0x0001D918 File Offset: 0x0001BB18
		public HelpKeywordAttribute(Type t)
		{
			if (t == null)
			{
				throw new ArgumentNullException("t");
			}
			this.contextKeyword = t.FullName;
		}

		/// <summary>Determines whether two <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" /> instances are equal.</summary>
		/// <returns>true if the specified <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" /> is equal to the current <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" /> to compare with the current <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" />.</param>
		// Token: 0x06000A9F RID: 2719 RVA: 0x0001D944 File Offset: 0x0001BB44
		public override bool Equals(object other)
		{
			if (other == null)
			{
				return false;
			}
			HelpKeywordAttribute helpKeywordAttribute = other as HelpKeywordAttribute;
			return helpKeywordAttribute != null && helpKeywordAttribute.contextKeyword == this.contextKeyword;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" />.</returns>
		// Token: 0x06000AA0 RID: 2720 RVA: 0x0001D97C File Offset: 0x0001BB7C
		public override int GetHashCode()
		{
			return (this.contextKeyword == null) ? 0 : this.contextKeyword.GetHashCode();
		}

		/// <summary>Determines whether the Help keyword is null.</summary>
		/// <returns>true if the Help keyword is null; otherwise, false.</returns>
		// Token: 0x06000AA1 RID: 2721 RVA: 0x0001D99C File Offset: 0x0001BB9C
		public override bool IsDefaultAttribute()
		{
			return this.contextKeyword == null;
		}

		/// <summary>Gets the Help keyword supplied by this attribute.</summary>
		/// <returns>The Help keyword supplied by this attribute.</returns>
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x0001D9A8 File Offset: 0x0001BBA8
		public string HelpKeyword
		{
			get
			{
				return this.contextKeyword;
			}
		}

		/// <summary>Represents the default value for <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" />. This field is read-only.</summary>
		// Token: 0x040002DE RID: 734
		public static readonly HelpKeywordAttribute Default;

		// Token: 0x040002DF RID: 735
		private string contextKeyword;
	}
}
