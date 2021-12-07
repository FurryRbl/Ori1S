using System;
using System.Security.Permissions;

namespace Microsoft.Win32
{
	/// <summary>Provides data for the <see cref="E:Microsoft.Win32.SystemEvents.TimerElapsed" /> event.</summary>
	// Token: 0x0200001B RID: 27
	[PermissionSet((SecurityAction)15, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"\nUnrestricted=\"true\"/>\n")]
	[PermissionSet((SecurityAction)14, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"\nUnrestricted=\"true\"/>\n")]
	public class TimerElapsedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.TimerElapsedEventArgs" /> class.</summary>
		/// <param name="timerId">The ID number for the timer. </param>
		// Token: 0x0600011E RID: 286 RVA: 0x0000A020 File Offset: 0x00008220
		public TimerElapsedEventArgs(IntPtr timerId)
		{
			this.mytimerId = timerId;
		}

		/// <summary>Gets the ID number for the timer.</summary>
		/// <returns>The ID number for the timer.</returns>
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600011F RID: 287 RVA: 0x0000A030 File Offset: 0x00008230
		public IntPtr TimerId
		{
			get
			{
				return this.mytimerId;
			}
		}

		// Token: 0x0400004B RID: 75
		private IntPtr mytimerId;
	}
}
