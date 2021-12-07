using System;

namespace System.Configuration
{
	/// <summary>Specifies special services for application settings properties. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000201 RID: 513
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class SettingsManageabilityAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsManageabilityAttribute" /> class.</summary>
		/// <param name="manageability">A <see cref="T:System.Configuration.SettingsManageability" /> value that enumerates the services being requested. </param>
		// Token: 0x06001178 RID: 4472 RVA: 0x0002E9D0 File Offset: 0x0002CBD0
		public SettingsManageabilityAttribute(SettingsManageability manageability)
		{
			this.manageability = manageability;
		}

		/// <summary>Gets the set of special services that have been requested.</summary>
		/// <returns>A value that results from using the logical OR operator to combine all the <see cref="T:System.Configuration.SettingsManageability" /> enumeration values corresponding to the requested services.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001179 RID: 4473 RVA: 0x0002E9E0 File Offset: 0x0002CBE0
		public SettingsManageability Manageability
		{
			get
			{
				return this.manageability;
			}
		}

		// Token: 0x040004FE RID: 1278
		private SettingsManageability manageability;
	}
}
