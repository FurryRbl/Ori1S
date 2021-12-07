using System;
using System.Security.Permissions;

namespace Microsoft.Win32
{
	/// <summary>Provides data for the <see cref="E:Microsoft.Win32.SystemEvents.SessionEnded" /> event.</summary>
	// Token: 0x02000015 RID: 21
	[PermissionSet((SecurityAction)15, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"\nUnrestricted=\"true\"/>\n")]
	[PermissionSet((SecurityAction)14, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"\nUnrestricted=\"true\"/>\n")]
	public class SessionEndedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.SessionEndedEventArgs" /> class.</summary>
		/// <param name="reason">One of the <see cref="T:Microsoft.Win32.SessionEndReasons" /> values indicating how the session ended. </param>
		// Token: 0x060000F4 RID: 244 RVA: 0x00009E40 File Offset: 0x00008040
		public SessionEndedEventArgs(SessionEndReasons reason)
		{
			this.myreason = reason;
		}

		/// <summary>Gets an identifier that indicates how the session ended.</summary>
		/// <returns>One of the <see cref="T:Microsoft.Win32.SessionEndReasons" /> values that indicates how the session ended.</returns>
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00009E50 File Offset: 0x00008050
		public SessionEndReasons Reason
		{
			get
			{
				return this.myreason;
			}
		}

		// Token: 0x04000038 RID: 56
		private SessionEndReasons myreason;
	}
}
