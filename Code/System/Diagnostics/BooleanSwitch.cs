using System;

namespace System.Diagnostics
{
	/// <summary>Provides a simple on/off switch that controls debugging and tracing output.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200020D RID: 525
	[SwitchLevel(typeof(bool))]
	public class BooleanSwitch : Switch
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.BooleanSwitch" /> class with the specified display name and description.</summary>
		/// <param name="displayName">The name to display on a user interface. </param>
		/// <param name="description">The description of the switch. </param>
		// Token: 0x0600118F RID: 4495 RVA: 0x0002EBA8 File Offset: 0x0002CDA8
		public BooleanSwitch(string displayName, string description) : base(displayName, description)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.BooleanSwitch" /> class with the specified display name, description, and default switch value. </summary>
		/// <param name="displayName">The name to display on the user interface. </param>
		/// <param name="description">The description of the switch. </param>
		/// <param name="defaultSwitchValue">The default value of the switch.</param>
		// Token: 0x06001190 RID: 4496 RVA: 0x0002EBB4 File Offset: 0x0002CDB4
		public BooleanSwitch(string displayName, string description, string defaultSwitchValue) : base(displayName, description, defaultSwitchValue)
		{
		}

		/// <summary>Gets or sets a value indicating whether the switch is enabled or disabled.</summary>
		/// <returns>true if the switch is enabled; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permission.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06001191 RID: 4497 RVA: 0x0002EBC0 File Offset: 0x0002CDC0
		// (set) Token: 0x06001192 RID: 4498 RVA: 0x0002EBD0 File Offset: 0x0002CDD0
		public bool Enabled
		{
			get
			{
				return base.SwitchSetting != 0;
			}
			set
			{
				base.SwitchSetting = Convert.ToInt32(value);
			}
		}

		/// <summary>Determines whether the new value of the <see cref="P:System.Diagnostics.Switch.Value" /> property can be parsed as a Boolean value.</summary>
		// Token: 0x06001193 RID: 4499 RVA: 0x0002EBE0 File Offset: 0x0002CDE0
		protected override void OnValueChanged()
		{
			int num;
			if (int.TryParse(base.Value, out num))
			{
				this.Enabled = (num != 0);
			}
			else
			{
				this.Enabled = Convert.ToBoolean(base.Value);
			}
		}
	}
}
