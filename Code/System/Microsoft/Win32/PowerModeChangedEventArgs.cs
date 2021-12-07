using System;
using System.Security.Permissions;

namespace Microsoft.Win32
{
	/// <summary>Provides data for the <see cref="E:Microsoft.Win32.SystemEvents.PowerModeChanged" /> event.</summary>
	// Token: 0x02000013 RID: 19
	[PermissionSet((SecurityAction)14, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"\nUnrestricted=\"true\"/>\n")]
	[PermissionSet((SecurityAction)15, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"\nUnrestricted=\"true\"/>\n")]
	public class PowerModeChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.PowerModeChangedEventArgs" /> class using the specified power mode event type.</summary>
		/// <param name="mode">One of the <see cref="T:Microsoft.Win32.PowerModes" /> values that represents the type of power mode event. </param>
		// Token: 0x060000F2 RID: 242 RVA: 0x00009E28 File Offset: 0x00008028
		public PowerModeChangedEventArgs(PowerModes mode)
		{
			this.mymode = mode;
		}

		/// <summary>Gets an identifier that indicates the type of the power mode event that has occurred.</summary>
		/// <returns>One of the <see cref="T:Microsoft.Win32.PowerModes" /> values.</returns>
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00009E38 File Offset: 0x00008038
		public PowerModes Mode
		{
			get
			{
				return this.mymode;
			}
		}

		// Token: 0x04000033 RID: 51
		private PowerModes mymode;
	}
}
