using System;

namespace System.Diagnostics
{
	/// <summary>Provides a multilevel switch to control tracing and debug output without recompiling your code.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200024F RID: 591
	public class SourceSwitch : Switch
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.SourceSwitch" /> class, specifying the name of the source.</summary>
		/// <param name="name">The name of the source.</param>
		// Token: 0x060014C7 RID: 5319 RVA: 0x000370D4 File Offset: 0x000352D4
		public SourceSwitch(string displayName) : this(displayName, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.SourceSwitch" /> class, specifying the display name and the default value for the source switch.</summary>
		/// <param name="displayName">The name of the source switch. </param>
		/// <param name="defaultSwitchValue">The default value for the switch. </param>
		// Token: 0x060014C8 RID: 5320 RVA: 0x000370E0 File Offset: 0x000352E0
		public SourceSwitch(string displayName, string defaultSwitchValue) : base(displayName, "Source switch.", defaultSwitchValue)
		{
		}

		/// <summary>Gets or sets the level of the switch.</summary>
		/// <returns>One of the <see cref="T:System.Diagnostics.SourceLevels" /> values that represents the event level of the switch.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x060014C9 RID: 5321 RVA: 0x000370F0 File Offset: 0x000352F0
		// (set) Token: 0x060014CA RID: 5322 RVA: 0x000370F8 File Offset: 0x000352F8
		public SourceLevels Level
		{
			get
			{
				return (SourceLevels)base.SwitchSetting;
			}
			set
			{
				base.SwitchSetting = (int)value;
			}
		}

		/// <summary>Determines if trace listeners should be called, based on the trace event type.</summary>
		/// <returns>True if the trace listeners should be called; otherwise, false.</returns>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060014CB RID: 5323 RVA: 0x00037104 File Offset: 0x00035304
		public bool ShouldTrace(TraceEventType eventType)
		{
			switch (eventType)
			{
			case TraceEventType.Critical:
				return (this.Level & SourceLevels.Critical) != SourceLevels.Off;
			case TraceEventType.Error:
				return (this.Level & SourceLevels.Error) != SourceLevels.Off;
			default:
				if (eventType != TraceEventType.Verbose)
				{
					if (eventType != TraceEventType.Start && eventType != TraceEventType.Stop && eventType != TraceEventType.Suspend && eventType != TraceEventType.Resume && eventType != TraceEventType.Transfer)
					{
					}
					return (this.Level & SourceLevels.ActivityTracing) != SourceLevels.Off;
				}
				return (this.Level & SourceLevels.Verbose) != SourceLevels.Off;
			case TraceEventType.Warning:
				return (this.Level & SourceLevels.Warning) != SourceLevels.Off;
			case TraceEventType.Information:
				return (this.Level & SourceLevels.Information) != SourceLevels.Off;
			}
		}

		/// <summary>Invoked when the value of the <see cref="P:System.Diagnostics.Switch.Value" /> property changes.</summary>
		/// <exception cref="T:System.ArgumentException">The new value of <see cref="P:System.Diagnostics.Switch.Value" /> is not one of the <see cref="T:System.Diagnostics.SourceLevels" /> values.</exception>
		// Token: 0x060014CC RID: 5324 RVA: 0x000371E0 File Offset: 0x000353E0
		protected override void OnValueChanged()
		{
			base.SwitchSetting = (int)Enum.Parse(typeof(SourceLevels), base.Value, true);
		}

		// Token: 0x0400064C RID: 1612
		private const string description = "Source switch.";
	}
}
