using System;

namespace System.IO
{
	/// <summary>Provides data for the <see cref="E:System.IO.FileSystemWatcher.Renamed" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002A6 RID: 678
	public class RenamedEventArgs : FileSystemEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.RenamedEventArgs" /> class.</summary>
		/// <param name="changeType">One of the <see cref="T:System.IO.WatcherChangeTypes" /> values. </param>
		/// <param name="directory">The name of the affected file or directory. </param>
		/// <param name="name">The name of the affected file or directory. </param>
		/// <param name="oldName">The old name of the affected file or directory. </param>
		// Token: 0x060017B1 RID: 6065 RVA: 0x000410C8 File Offset: 0x0003F2C8
		public RenamedEventArgs(WatcherChangeTypes changeType, string directory, string name, string oldName) : base(changeType, directory, name)
		{
			this.oldName = oldName;
			this.oldFullPath = Path.Combine(directory, oldName);
		}

		/// <summary>Gets the previous fully qualified path of the affected file or directory.</summary>
		/// <returns>The previous fully qualified path of the affected file or directory.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x060017B2 RID: 6066 RVA: 0x000410EC File Offset: 0x0003F2EC
		public string OldFullPath
		{
			get
			{
				return this.oldFullPath;
			}
		}

		/// <summary>Gets the old name of the affected file or directory.</summary>
		/// <returns>The previous name of the affected file or directory.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x060017B3 RID: 6067 RVA: 0x000410F4 File Offset: 0x0003F2F4
		public string OldName
		{
			get
			{
				return this.oldName;
			}
		}

		// Token: 0x04000F0D RID: 3853
		private string oldName;

		// Token: 0x04000F0E RID: 3854
		private string oldFullPath;
	}
}
