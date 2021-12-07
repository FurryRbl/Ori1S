using System;

namespace System.Configuration
{
	/// <summary>Indicates that an application settings property has a special significance. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000207 RID: 519
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class SpecialSettingAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SpecialSettingAttribute" /> class.</summary>
		/// <param name="specialSetting">A <see cref="T:System.Configuration.SpecialSetting" /> enumeration value defining the category of the application settings property.</param>
		// Token: 0x06001182 RID: 4482 RVA: 0x0002EAB0 File Offset: 0x0002CCB0
		public SpecialSettingAttribute(SpecialSetting setting)
		{
			this.setting = setting;
		}

		/// <summary>Gets the value describing the special setting category of the application settings property.</summary>
		/// <returns>A <see cref="T:System.Configuration.SpecialSetting" /> enumeration value defining the category of the application settings property.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001183 RID: 4483 RVA: 0x0002EAC0 File Offset: 0x0002CCC0
		public SpecialSetting SpecialSetting
		{
			get
			{
				return this.setting;
			}
		}

		// Token: 0x04000505 RID: 1285
		private SpecialSetting setting;
	}
}
