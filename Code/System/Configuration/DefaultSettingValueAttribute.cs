using System;

namespace System.Configuration
{
	/// <summary>Specifies the default value for an application settings property.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001DC RID: 476
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class DefaultSettingValueAttribute : Attribute
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.DefaultSettingValueAttribute" /> class.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that represents the default value for the property. </param>
		// Token: 0x060010AB RID: 4267 RVA: 0x0002D36C File Offset: 0x0002B56C
		public DefaultSettingValueAttribute(string value)
		{
			this.value = value;
		}

		/// <summary>Gets the default value for the application settings property.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the default value for the property.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003BB RID: 955
		// (get) Token: 0x060010AC RID: 4268 RVA: 0x0002D37C File Offset: 0x0002B57C
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x040004CA RID: 1226
		private string value;
	}
}
