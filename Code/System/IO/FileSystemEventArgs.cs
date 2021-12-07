using System;

namespace System.IO
{
	/// <summary>Provides data for the directory events: <see cref="E:System.IO.FileSystemWatcher.Changed" />, <see cref="E:System.IO.FileSystemWatcher.Created" />, <see cref="E:System.IO.FileSystemWatcher.Deleted" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000280 RID: 640
	public class FileSystemEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileSystemEventArgs" /> class.</summary>
		/// <param name="changeType">One of the <see cref="T:System.IO.WatcherChangeTypes" /> values, which represents the kind of change detected in the file system. </param>
		/// <param name="directory">The root directory of the affected file or directory. </param>
		/// <param name="name">The name of the affected file or directory. </param>
		// Token: 0x06001675 RID: 5749 RVA: 0x0003CF9C File Offset: 0x0003B19C
		public FileSystemEventArgs(WatcherChangeTypes changeType, string directory, string name)
		{
			this.changeType = changeType;
			this.directory = directory;
			this.name = name;
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x0003CFBC File Offset: 0x0003B1BC
		internal void SetName(string name)
		{
			this.name = name;
		}

		/// <summary>Gets the type of directory event that occurred.</summary>
		/// <returns>One of the <see cref="T:System.IO.WatcherChangeTypes" /> values that represents the kind of change detected in the file system.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001677 RID: 5751 RVA: 0x0003CFC8 File Offset: 0x0003B1C8
		public WatcherChangeTypes ChangeType
		{
			get
			{
				return this.changeType;
			}
		}

		/// <summary>Gets the fully qualifed path of the affected file or directory.</summary>
		/// <returns>The path of the affected file or directory.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001678 RID: 5752 RVA: 0x0003CFD0 File Offset: 0x0003B1D0
		public string FullPath
		{
			get
			{
				return Path.Combine(this.directory, this.name);
			}
		}

		/// <summary>Gets the name of the affected file or directory.</summary>
		/// <returns>The name of the affected file or directory.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001679 RID: 5753 RVA: 0x0003CFE4 File Offset: 0x0003B1E4
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x0400072A RID: 1834
		private WatcherChangeTypes changeType;

		// Token: 0x0400072B RID: 1835
		private string directory;

		// Token: 0x0400072C RID: 1836
		private string name;
	}
}
