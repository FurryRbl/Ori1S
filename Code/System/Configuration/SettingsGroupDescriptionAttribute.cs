using System;

namespace System.Configuration
{
	/// <summary>Provides a string that describes an application settings property group. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000202 RID: 514
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class SettingsGroupDescriptionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsGroupDescriptionAttribute" /> class.</summary>
		/// <param name="description">A <see cref="T:System.String" /> containing the descriptive text for the application settings group.</param>
		// Token: 0x0600117A RID: 4474 RVA: 0x0002E9E8 File Offset: 0x0002CBE8
		public SettingsGroupDescriptionAttribute(string description)
		{
			this.desc = description;
		}

		/// <summary>The descriptive text for the application settings properties group.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the descriptive text for the application settings group.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x0600117B RID: 4475 RVA: 0x0002E9F8 File Offset: 0x0002CBF8
		public string Description
		{
			get
			{
				return this.desc;
			}
		}

		// Token: 0x040004FF RID: 1279
		private string desc;
	}
}
