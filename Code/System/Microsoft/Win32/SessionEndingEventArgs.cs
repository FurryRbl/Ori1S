using System;
using System.Security.Permissions;

namespace Microsoft.Win32
{
	/// <summary>Provides data for the <see cref="E:Microsoft.Win32.SystemEvents.SessionEnding" /> event.</summary>
	// Token: 0x02000016 RID: 22
	[PermissionSet((SecurityAction)15, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"\nUnrestricted=\"true\"/>\n")]
	[PermissionSet((SecurityAction)14, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"\nUnrestricted=\"true\"/>\n")]
	public class SessionEndingEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.SessionEndingEventArgs" /> class using the specified value indicating the type of session close event that is occurring.</summary>
		/// <param name="reason">One of the <see cref="T:Microsoft.Win32.SessionEndReasons" /> that specifies how the session ends. </param>
		// Token: 0x060000F6 RID: 246 RVA: 0x00009E58 File Offset: 0x00008058
		public SessionEndingEventArgs(SessionEndReasons reason)
		{
			this.myreason = reason;
		}

		/// <summary>Gets the reason the session is ending.</summary>
		/// <returns>One of the <see cref="T:Microsoft.Win32.SessionEndReasons" /> values that specifies how the session is ending.</returns>
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00009E68 File Offset: 0x00008068
		public SessionEndReasons Reason
		{
			get
			{
				return this.myreason;
			}
		}

		/// <summary>Gets or sets a value indicating whether to cancel the user request to end the session.</summary>
		/// <returns>true to cancel the user request to end the session; otherwise, false.</returns>
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00009E70 File Offset: 0x00008070
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x00009E78 File Offset: 0x00008078
		public bool Cancel
		{
			get
			{
				return this.mycancel;
			}
			set
			{
				this.mycancel = value;
			}
		}

		// Token: 0x04000039 RID: 57
		private SessionEndReasons myreason;

		// Token: 0x0400003A RID: 58
		private bool mycancel;
	}
}
