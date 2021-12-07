using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	/// <summary>Specifies a description for a property or event.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000232 RID: 562
	[AttributeUsage(AttributeTargets.All)]
	public class MonitoringDescriptionAttribute : System.ComponentModel.DescriptionAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.MonitoringDescriptionAttribute" /> class, using the specified description.</summary>
		/// <param name="description">The application-defined description text. </param>
		// Token: 0x06001353 RID: 4947 RVA: 0x00033E10 File Offset: 0x00032010
		public MonitoringDescriptionAttribute(string description) : base(description)
		{
		}

		/// <summary>Gets description text associated with the item monitored.</summary>
		/// <returns>An application-defined description.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001354 RID: 4948 RVA: 0x00033E1C File Offset: 0x0003201C
		public override string Description
		{
			get
			{
				return base.Description;
			}
		}
	}
}
