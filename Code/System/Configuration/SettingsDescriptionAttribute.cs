using System;

namespace System.Configuration
{
	/// <summary>Provides a string that describes an individual configuration property. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000203 RID: 515
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class SettingsDescriptionAttribute : Attribute
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.SettingsDescriptionAttribute" /> class.</summary>
		/// <param name="description">The <see cref="T:System.String" /> used as descriptive text.</param>
		// Token: 0x0600117C RID: 4476 RVA: 0x0002EA00 File Offset: 0x0002CC00
		public SettingsDescriptionAttribute(string description)
		{
			this.desc = description;
		}

		/// <summary>Gets the descriptive text for the associated configuration property.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the descriptive text for the associated configuration property.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x0600117D RID: 4477 RVA: 0x0002EA10 File Offset: 0x0002CC10
		public string Description
		{
			get
			{
				return this.desc;
			}
		}

		// Token: 0x04000500 RID: 1280
		private string desc;
	}
}
