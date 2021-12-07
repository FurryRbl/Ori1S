using System;

namespace System.ComponentModel
{
	/// <summary>Specifies that the property can be used as an application setting.</summary>
	// Token: 0x0200019C RID: 412
	[Obsolete("Use SettingsBindableAttribute instead of RecommendedAsConfigurableAttribute")]
	[AttributeUsage(AttributeTargets.Property)]
	public class RecommendedAsConfigurableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.RecommendedAsConfigurableAttribute" /> class.</summary>
		/// <param name="recommendedAsConfigurable">true if the property this attribute is bound to can be used as an application setting; otherwise, false. </param>
		// Token: 0x06000E8C RID: 3724 RVA: 0x000255B0 File Offset: 0x000237B0
		public RecommendedAsConfigurableAttribute(bool recommendedAsConfigurable)
		{
			this.recommendedAsConfigurable = recommendedAsConfigurable;
		}

		/// <summary>Gets a value indicating whether the property this attribute is bound to can be used as an application setting.</summary>
		/// <returns>true if the property this attribute is bound to can be used as an application setting; otherwise, false.</returns>
		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000E8E RID: 3726 RVA: 0x000255E4 File Offset: 0x000237E4
		public bool RecommendedAsConfigurable
		{
			get
			{
				return this.recommendedAsConfigurable;
			}
		}

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <returns>true if <paramref name="obj" /> is equal to this instance; otherwise, false.</returns>
		/// <param name="obj">Another object to compare to. </param>
		// Token: 0x06000E8F RID: 3727 RVA: 0x000255EC File Offset: 0x000237EC
		public override bool Equals(object obj)
		{
			return obj is RecommendedAsConfigurableAttribute && ((RecommendedAsConfigurableAttribute)obj).RecommendedAsConfigurable == this.recommendedAsConfigurable;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.RecommendedAsConfigurableAttribute" />.</returns>
		// Token: 0x06000E90 RID: 3728 RVA: 0x0002561C File Offset: 0x0002381C
		public override int GetHashCode()
		{
			return this.recommendedAsConfigurable.GetHashCode();
		}

		/// <summary>Indicates whether the value of this instance is the default value for the class.</summary>
		/// <returns>true if this instance is the default attribute for the class; otherwise, false.</returns>
		// Token: 0x06000E91 RID: 3729 RVA: 0x0002562C File Offset: 0x0002382C
		public override bool IsDefaultAttribute()
		{
			return this.recommendedAsConfigurable == RecommendedAsConfigurableAttribute.Default.RecommendedAsConfigurable;
		}

		// Token: 0x04000414 RID: 1044
		private bool recommendedAsConfigurable;

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.RecommendedAsConfigurableAttribute" />, which is <see cref="F:System.ComponentModel.RecommendedAsConfigurableAttribute.No" />. This static field is read-only.</summary>
		// Token: 0x04000415 RID: 1045
		public static readonly RecommendedAsConfigurableAttribute Default = new RecommendedAsConfigurableAttribute(false);

		/// <summary>Specifies that a property cannot be used as an application setting. This static field is read-only.</summary>
		// Token: 0x04000416 RID: 1046
		public static readonly RecommendedAsConfigurableAttribute No = new RecommendedAsConfigurableAttribute(false);

		/// <summary>Specifies that a property can be used as an application setting. This static field is read-only.</summary>
		// Token: 0x04000417 RID: 1047
		public static readonly RecommendedAsConfigurableAttribute Yes = new RecommendedAsConfigurableAttribute(true);
	}
}
