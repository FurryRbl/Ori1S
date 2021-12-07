using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Threading;

namespace System.IO
{
	/// <summary>Listens to the file system change notifications and raises events when a directory, or file in a directory, changes.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000281 RID: 641
	[System.ComponentModel.DefaultEvent("Changed")]
	[IODescription("")]
	public class FileSystemWatcher : System.ComponentModel.Component, System.ComponentModel.ISupportInitialize
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileSystemWatcher" /> class.</summary>
		// Token: 0x0600167A RID: 5754 RVA: 0x0003CFEC File Offset: 0x0003B1EC
		public FileSystemWatcher()
		{
			this.notifyFilter = (NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastWrite);
			this.enableRaisingEvents = false;
			this.filter = "*.*";
			this.includeSubdirectories = false;
			this.internalBufferSize = 8192;
			this.path = string.Empty;
			this.InitWatcher();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileSystemWatcher" /> class, given the specified directory to monitor.</summary>
		/// <param name="path">The directory to monitor, in standard or Universal Naming Convention (UNC) notation. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> parameter is an empty string ("").-or- The path specified through the <paramref name="path" /> parameter does not exist. </exception>
		// Token: 0x0600167B RID: 5755 RVA: 0x0003D03C File Offset: 0x0003B23C
		public FileSystemWatcher(string path) : this(path, "*.*")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileSystemWatcher" /> class, given the specified directory and type of files to monitor.</summary>
		/// <param name="path">The directory to monitor, in standard or Universal Naming Convention (UNC) notation. </param>
		/// <param name="filter">The type of files to watch. For example, "*.txt" watches for changes to all text files. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is null.-or- The <paramref name="filter" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> parameter is an empty string ("").-or- The path specified through the <paramref name="path" /> parameter does not exist. </exception>
		// Token: 0x0600167C RID: 5756 RVA: 0x0003D04C File Offset: 0x0003B24C
		public FileSystemWatcher(string path, string filter)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (filter == null)
			{
				throw new ArgumentNullException("filter");
			}
			if (path == string.Empty)
			{
				throw new ArgumentException("Empty path", "path");
			}
			if (!Directory.Exists(path))
			{
				throw new ArgumentException("Directory does not exists", "path");
			}
			this.enableRaisingEvents = false;
			this.filter = filter;
			this.includeSubdirectories = false;
			this.internalBufferSize = 8192;
			this.notifyFilter = (NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastWrite);
			this.path = path;
			this.synchronizingObject = null;
			this.InitWatcher();
		}

		/// <summary>Occurs when a file or directory in the specified <see cref="P:System.IO.FileSystemWatcher.Path" /> is changed.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000043 RID: 67
		// (add) Token: 0x0600167E RID: 5758 RVA: 0x0003D104 File Offset: 0x0003B304
		// (remove) Token: 0x0600167F RID: 5759 RVA: 0x0003D120 File Offset: 0x0003B320
		[IODescription("Occurs when a file/directory change matches the filter")]
		public event FileSystemEventHandler Changed;

		/// <summary>Occurs when a file or directory in the specified <see cref="P:System.IO.FileSystemWatcher.Path" /> is created.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000044 RID: 68
		// (add) Token: 0x06001680 RID: 5760 RVA: 0x0003D13C File Offset: 0x0003B33C
		// (remove) Token: 0x06001681 RID: 5761 RVA: 0x0003D158 File Offset: 0x0003B358
		[IODescription("Occurs when a file/directory creation matches the filter")]
		public event FileSystemEventHandler Created;

		/// <summary>Occurs when a file or directory in the specified <see cref="P:System.IO.FileSystemWatcher.Path" /> is deleted.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000045 RID: 69
		// (add) Token: 0x06001682 RID: 5762 RVA: 0x0003D174 File Offset: 0x0003B374
		// (remove) Token: 0x06001683 RID: 5763 RVA: 0x0003D190 File Offset: 0x0003B390
		[IODescription("Occurs when a file/directory deletion matches the filter")]
		public event FileSystemEventHandler Deleted;

		/// <summary>Occurs when the internal buffer overflows.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000046 RID: 70
		// (add) Token: 0x06001684 RID: 5764 RVA: 0x0003D1AC File Offset: 0x0003B3AC
		// (remove) Token: 0x06001685 RID: 5765 RVA: 0x0003D1C8 File Offset: 0x0003B3C8
		[System.ComponentModel.Browsable(false)]
		public event ErrorEventHandler Error;

		/// <summary>Occurs when a file or directory in the specified <see cref="P:System.IO.FileSystemWatcher.Path" /> is renamed.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000047 RID: 71
		// (add) Token: 0x06001686 RID: 5766 RVA: 0x0003D1E4 File Offset: 0x0003B3E4
		// (remove) Token: 0x06001687 RID: 5767 RVA: 0x0003D200 File Offset: 0x0003B400
		[IODescription("Occurs when a file/directory rename matches the filter")]
		public event RenamedEventHandler Renamed;

		// Token: 0x06001688 RID: 5768 RVA: 0x0003D21C File Offset: 0x0003B41C
		[PermissionSet(SecurityAction.Assert, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nRead=\"MONO_MANAGED_WATCHER\"\nWrite=\"\"/>\n</PermissionSet>\n")]
		private void InitWatcher()
		{
			object obj = FileSystemWatcher.lockobj;
			lock (obj)
			{
				if (FileSystemWatcher.watcher == null)
				{
					string environmentVariable = Environment.GetEnvironmentVariable("MONO_MANAGED_WATCHER");
					int num = 0;
					if (environmentVariable == null)
					{
						num = FileSystemWatcher.InternalSupportsFSW();
					}
					bool flag = false;
					switch (num)
					{
					case 1:
						flag = DefaultWatcher.GetInstance(out FileSystemWatcher.watcher);
						break;
					case 2:
						flag = FAMWatcher.GetInstance(out FileSystemWatcher.watcher, false);
						break;
					case 3:
						flag = KeventWatcher.GetInstance(out FileSystemWatcher.watcher);
						break;
					case 4:
						flag = FAMWatcher.GetInstance(out FileSystemWatcher.watcher, true);
						break;
					case 5:
						flag = InotifyWatcher.GetInstance(out FileSystemWatcher.watcher, true);
						break;
					}
					if (num == 0 || !flag)
					{
						if (string.Compare(environmentVariable, "disabled", true) == 0)
						{
							NullFileWatcher.GetInstance(out FileSystemWatcher.watcher);
						}
						else
						{
							DefaultWatcher.GetInstance(out FileSystemWatcher.watcher);
						}
					}
				}
			}
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x0003D338 File Offset: 0x0003B538
		[Conditional("DEBUG")]
		[Conditional("TRACE")]
		private void ShowWatcherInfo()
		{
			Console.WriteLine("Watcher implementation: {0}", (FileSystemWatcher.watcher == null) ? "<none>" : FileSystemWatcher.watcher.GetType().ToString());
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x0600168A RID: 5770 RVA: 0x0003D368 File Offset: 0x0003B568
		// (set) Token: 0x0600168B RID: 5771 RVA: 0x0003D370 File Offset: 0x0003B570
		internal bool Waiting
		{
			get
			{
				return this.waiting;
			}
			set
			{
				this.waiting = value;
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x0600168C RID: 5772 RVA: 0x0003D37C File Offset: 0x0003B57C
		internal string MangledFilter
		{
			get
			{
				if (this.filter != "*.*")
				{
					return this.filter;
				}
				if (this.mangledFilter != null)
				{
					return this.mangledFilter;
				}
				string result = "*.*";
				if (FileSystemWatcher.watcher.GetType() != typeof(WindowsWatcher))
				{
					result = "*";
				}
				return result;
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x0600168D RID: 5773 RVA: 0x0003D3E0 File Offset: 0x0003B5E0
		internal SearchPattern2 Pattern
		{
			get
			{
				if (this.pattern == null)
				{
					this.pattern = new SearchPattern2(this.MangledFilter);
				}
				return this.pattern;
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x0600168E RID: 5774 RVA: 0x0003D410 File Offset: 0x0003B610
		internal string FullPath
		{
			get
			{
				if (this.fullpath == null)
				{
					if (this.path == null || this.path == string.Empty)
					{
						this.fullpath = Environment.CurrentDirectory;
					}
					else
					{
						this.fullpath = System.IO.Path.GetFullPath(this.path);
					}
				}
				return this.fullpath;
			}
		}

		/// <summary>Gets or sets a value indicating whether the component is enabled.</summary>
		/// <returns>true if the component is enabled; otherwise, false. The default is false. If you are using the component on a designer in Visual Studio 2005, the default is true.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.FileSystemWatcher" /> object has been disposed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The directory specified in <see cref="P:System.IO.FileSystemWatcher.Path" /> could not be found.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.IO.FileSystemWatcher.Path" /> has not been set or is invalid.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x0600168F RID: 5775 RVA: 0x0003D470 File Offset: 0x0003B670
		// (set) Token: 0x06001690 RID: 5776 RVA: 0x0003D478 File Offset: 0x0003B678
		[IODescription("Flag to indicate if this instance is active")]
		[System.ComponentModel.DefaultValue(false)]
		public bool EnableRaisingEvents
		{
			get
			{
				return this.enableRaisingEvents;
			}
			set
			{
				if (value == this.enableRaisingEvents)
				{
					return;
				}
				this.enableRaisingEvents = value;
				if (value)
				{
					this.Start();
				}
				else
				{
					this.Stop();
				}
			}
		}

		/// <summary>Gets or sets the filter string used to determine what files are monitored in a directory.</summary>
		/// <returns>The filter string. The default is "*.*" (Watches all files.) </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001691 RID: 5777 RVA: 0x0003D4A8 File Offset: 0x0003B6A8
		// (set) Token: 0x06001692 RID: 5778 RVA: 0x0003D4B0 File Offset: 0x0003B6B0
		[System.ComponentModel.RecommendedAsConfigurable(true)]
		[System.ComponentModel.TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[System.ComponentModel.DefaultValue("*.*")]
		[IODescription("File name filter pattern")]
		public string Filter
		{
			get
			{
				return this.filter;
			}
			set
			{
				if (value == null || value == string.Empty)
				{
					value = "*.*";
				}
				if (this.filter != value)
				{
					this.filter = value;
					this.pattern = null;
					this.mangledFilter = null;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether subdirectories within the specified path should be monitored.</summary>
		/// <returns>true if you want to monitor subdirectories; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001693 RID: 5779 RVA: 0x0003D500 File Offset: 0x0003B700
		// (set) Token: 0x06001694 RID: 5780 RVA: 0x0003D508 File Offset: 0x0003B708
		[System.ComponentModel.DefaultValue(false)]
		[IODescription("Flag to indicate we want to watch subdirectories")]
		public bool IncludeSubdirectories
		{
			get
			{
				return this.includeSubdirectories;
			}
			set
			{
				if (this.includeSubdirectories == value)
				{
					return;
				}
				this.includeSubdirectories = value;
				if (value && this.enableRaisingEvents)
				{
					this.Stop();
					this.Start();
				}
			}
		}

		/// <summary>Gets or sets the size of the internal buffer.</summary>
		/// <returns>The internal buffer size. The default is 8192 (8 KB).</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001695 RID: 5781 RVA: 0x0003D53C File Offset: 0x0003B73C
		// (set) Token: 0x06001696 RID: 5782 RVA: 0x0003D544 File Offset: 0x0003B744
		[System.ComponentModel.DefaultValue(8192)]
		[System.ComponentModel.Browsable(false)]
		public int InternalBufferSize
		{
			get
			{
				return this.internalBufferSize;
			}
			set
			{
				if (this.internalBufferSize == value)
				{
					return;
				}
				if (value < 4196)
				{
					value = 4196;
				}
				this.internalBufferSize = value;
				if (this.enableRaisingEvents)
				{
					this.Stop();
					this.Start();
				}
			}
		}

		/// <summary>Gets or sets the type of changes to watch for.</summary>
		/// <returns>One of the <see cref="T:System.IO.NotifyFilters" /> values. The default is the bitwise OR combination of LastWrite, FileName, and DirectoryName.</returns>
		/// <exception cref="T:System.ArgumentException">The value is not a valid bitwise OR combination of the <see cref="T:System.IO.NotifyFilters" /> values. </exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value that is being set is not valid.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001697 RID: 5783 RVA: 0x0003D584 File Offset: 0x0003B784
		// (set) Token: 0x06001698 RID: 5784 RVA: 0x0003D58C File Offset: 0x0003B78C
		[System.ComponentModel.DefaultValue(NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastWrite)]
		[IODescription("Flag to indicate which change event we want to monitor")]
		public NotifyFilters NotifyFilter
		{
			get
			{
				return this.notifyFilter;
			}
			set
			{
				if (this.notifyFilter == value)
				{
					return;
				}
				this.notifyFilter = value;
				if (this.enableRaisingEvents)
				{
					this.Stop();
					this.Start();
				}
			}
		}

		/// <summary>Gets or sets the path of the directory to watch.</summary>
		/// <returns>The path to monitor. The default is an empty string ("").</returns>
		/// <exception cref="T:System.ArgumentException">The specified path does not exist or could not be found.-or- The specified path contains wildcard characters.-or- The specified path contains invalid path characters.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001699 RID: 5785 RVA: 0x0003D5BC File Offset: 0x0003B7BC
		// (set) Token: 0x0600169A RID: 5786 RVA: 0x0003D5C4 File Offset: 0x0003B7C4
		[System.ComponentModel.DefaultValue("")]
		[System.ComponentModel.RecommendedAsConfigurable(true)]
		[System.ComponentModel.Editor("System.Diagnostics.Design.FSWPathEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[System.ComponentModel.TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[IODescription("The directory to monitor")]
		public string Path
		{
			get
			{
				return this.path;
			}
			set
			{
				if (this.path == value)
				{
					return;
				}
				bool flag = false;
				Exception ex = null;
				try
				{
					flag = Directory.Exists(value);
				}
				catch (Exception ex2)
				{
					ex = ex2;
				}
				if (ex != null)
				{
					throw new ArgumentException("Invalid directory name", "value", ex);
				}
				if (!flag)
				{
					throw new ArgumentException("Directory does not exists", "value");
				}
				this.path = value;
				this.fullpath = null;
				if (this.enableRaisingEvents)
				{
					this.Stop();
					this.Start();
				}
			}
		}

		/// <summary>Gets or sets an <see cref="T:System.ComponentModel.ISite" /> for the <see cref="T:System.IO.FileSystemWatcher" />.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ISite" /> for the <see cref="T:System.IO.FileSystemWatcher" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x0600169B RID: 5787 RVA: 0x0003D66C File Offset: 0x0003B86C
		// (set) Token: 0x0600169C RID: 5788 RVA: 0x0003D674 File Offset: 0x0003B874
		[System.ComponentModel.Browsable(false)]
		public override System.ComponentModel.ISite Site
		{
			get
			{
				return base.Site;
			}
			set
			{
				base.Site = value;
			}
		}

		/// <summary>Gets or sets the object used to marshal the event handler calls issued as a result of a directory change.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISynchronizeInvoke" /> that represents the object used to marshal the event handler calls issued as a result of a directory change. The default is null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x0600169D RID: 5789 RVA: 0x0003D680 File Offset: 0x0003B880
		// (set) Token: 0x0600169E RID: 5790 RVA: 0x0003D688 File Offset: 0x0003B888
		[System.ComponentModel.Browsable(false)]
		[System.ComponentModel.DefaultValue(null)]
		[IODescription("The object used to marshal the event handler calls resulting from a directory change")]
		public System.ComponentModel.ISynchronizeInvoke SynchronizingObject
		{
			get
			{
				return this.synchronizingObject;
			}
			set
			{
				this.synchronizingObject = value;
			}
		}

		/// <summary>Begins the initialization of a <see cref="T:System.IO.FileSystemWatcher" /> used on a form or used by another component. The initialization occurs at run time.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600169F RID: 5791 RVA: 0x0003D694 File Offset: 0x0003B894
		public void BeginInit()
		{
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.FileSystemWatcher" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x060016A0 RID: 5792 RVA: 0x0003D698 File Offset: 0x0003B898
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				this.disposed = true;
				this.Stop();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x0003D6BC File Offset: 0x0003B8BC
		~FileSystemWatcher()
		{
			this.disposed = true;
			this.Stop();
		}

		/// <summary>Ends the initialization of a <see cref="T:System.IO.FileSystemWatcher" /> used on a form or used by another component. The initialization occurs at run time.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060016A2 RID: 5794 RVA: 0x0003D700 File Offset: 0x0003B900
		public void EndInit()
		{
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x0003D704 File Offset: 0x0003B904
		private void RaiseEvent(Delegate ev, EventArgs arg, FileSystemWatcher.EventType evtype)
		{
			if (ev == null)
			{
				return;
			}
			if (this.synchronizingObject == null)
			{
				switch (evtype)
				{
				case FileSystemWatcher.EventType.FileSystemEvent:
					((FileSystemEventHandler)ev).BeginInvoke(this, (FileSystemEventArgs)arg, null, null);
					break;
				case FileSystemWatcher.EventType.ErrorEvent:
					((ErrorEventHandler)ev).BeginInvoke(this, (ErrorEventArgs)arg, null, null);
					break;
				case FileSystemWatcher.EventType.RenameEvent:
					((RenamedEventHandler)ev).BeginInvoke(this, (RenamedEventArgs)arg, null, null);
					break;
				}
				return;
			}
			this.synchronizingObject.BeginInvoke(ev, new object[]
			{
				this,
				arg
			});
		}

		/// <summary>Raises the <see cref="E:System.IO.FileSystemWatcher.Changed" /> event.</summary>
		/// <param name="e">A <see cref="T:System.IO.FileSystemEventArgs" /> that contains the event data. </param>
		// Token: 0x060016A4 RID: 5796 RVA: 0x0003D7A8 File Offset: 0x0003B9A8
		protected void OnChanged(FileSystemEventArgs e)
		{
			this.RaiseEvent(this.Changed, e, FileSystemWatcher.EventType.FileSystemEvent);
		}

		/// <summary>Raises the <see cref="E:System.IO.FileSystemWatcher.Created" /> event.</summary>
		/// <param name="e">A <see cref="T:System.IO.FileSystemEventArgs" /> that contains the event data. </param>
		// Token: 0x060016A5 RID: 5797 RVA: 0x0003D7B8 File Offset: 0x0003B9B8
		protected void OnCreated(FileSystemEventArgs e)
		{
			this.RaiseEvent(this.Created, e, FileSystemWatcher.EventType.FileSystemEvent);
		}

		/// <summary>Raises the <see cref="E:System.IO.FileSystemWatcher.Deleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.IO.FileSystemEventArgs" /> that contains the event data. </param>
		// Token: 0x060016A6 RID: 5798 RVA: 0x0003D7C8 File Offset: 0x0003B9C8
		protected void OnDeleted(FileSystemEventArgs e)
		{
			this.RaiseEvent(this.Deleted, e, FileSystemWatcher.EventType.FileSystemEvent);
		}

		/// <summary>Raises the <see cref="E:System.IO.FileSystemWatcher.Error" /> event.</summary>
		/// <param name="e">An <see cref="T:System.IO.ErrorEventArgs" /> that contains the event data. </param>
		// Token: 0x060016A7 RID: 5799 RVA: 0x0003D7D8 File Offset: 0x0003B9D8
		protected void OnError(ErrorEventArgs e)
		{
			this.RaiseEvent(this.Error, e, FileSystemWatcher.EventType.ErrorEvent);
		}

		/// <summary>Raises the <see cref="E:System.IO.FileSystemWatcher.Renamed" /> event.</summary>
		/// <param name="e">A <see cref="T:System.IO.RenamedEventArgs" /> that contains the event data. </param>
		// Token: 0x060016A8 RID: 5800 RVA: 0x0003D7E8 File Offset: 0x0003B9E8
		protected void OnRenamed(RenamedEventArgs e)
		{
			this.RaiseEvent(this.Renamed, e, FileSystemWatcher.EventType.RenameEvent);
		}

		/// <summary>A synchronous method that returns a structure that contains specific information on the change that occurred, given the type of change you want to monitor.</summary>
		/// <returns>A <see cref="T:System.IO.WaitForChangedResult" /> that contains specific information on the change that occurred.</returns>
		/// <param name="changeType">The <see cref="T:System.IO.WatcherChangeTypes" /> to watch for. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060016A9 RID: 5801 RVA: 0x0003D7F8 File Offset: 0x0003B9F8
		public WaitForChangedResult WaitForChanged(WatcherChangeTypes changeType)
		{
			return this.WaitForChanged(changeType, -1);
		}

		/// <summary>A synchronous method that returns a structure that contains specific information on the change that occurred, given the type of change you want to monitor and the time (in milliseconds) to wait before timing out.</summary>
		/// <returns>A <see cref="T:System.IO.WaitForChangedResult" /> that contains specific information on the change that occurred.</returns>
		/// <param name="changeType">The <see cref="T:System.IO.WatcherChangeTypes" /> to watch for. </param>
		/// <param name="timeout">The time (in milliseconds) to wait before timing out. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060016AA RID: 5802 RVA: 0x0003D804 File Offset: 0x0003BA04
		public WaitForChangedResult WaitForChanged(WatcherChangeTypes changeType, int timeout)
		{
			WaitForChangedResult result = default(WaitForChangedResult);
			bool flag = this.EnableRaisingEvents;
			if (!flag)
			{
				this.EnableRaisingEvents = true;
			}
			bool flag2;
			lock (this)
			{
				this.waiting = true;
				flag2 = Monitor.Wait(this, timeout);
				if (flag2)
				{
					result = this.lastData;
				}
			}
			this.EnableRaisingEvents = flag;
			if (!flag2)
			{
				result.TimedOut = true;
			}
			return result;
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x0003D890 File Offset: 0x0003BA90
		internal void DispatchEvents(FileAction act, string filename, ref RenamedEventArgs renamed)
		{
			if (this.waiting)
			{
				this.lastData = default(WaitForChangedResult);
			}
			switch (act)
			{
			case FileAction.Added:
				this.lastData.Name = filename;
				this.lastData.ChangeType = WatcherChangeTypes.Created;
				this.OnCreated(new FileSystemEventArgs(WatcherChangeTypes.Created, this.path, filename));
				break;
			case FileAction.Removed:
				this.lastData.Name = filename;
				this.lastData.ChangeType = WatcherChangeTypes.Deleted;
				this.OnDeleted(new FileSystemEventArgs(WatcherChangeTypes.Deleted, this.path, filename));
				break;
			case FileAction.Modified:
				this.lastData.Name = filename;
				this.lastData.ChangeType = WatcherChangeTypes.Changed;
				this.OnChanged(new FileSystemEventArgs(WatcherChangeTypes.Changed, this.path, filename));
				break;
			case FileAction.RenamedOldName:
				if (renamed != null)
				{
					this.OnRenamed(renamed);
				}
				this.lastData.OldName = filename;
				this.lastData.ChangeType = WatcherChangeTypes.Renamed;
				renamed = new RenamedEventArgs(WatcherChangeTypes.Renamed, this.path, filename, string.Empty);
				break;
			case FileAction.RenamedNewName:
				this.lastData.Name = filename;
				this.lastData.ChangeType = WatcherChangeTypes.Renamed;
				if (renamed == null)
				{
					renamed = new RenamedEventArgs(WatcherChangeTypes.Renamed, this.path, string.Empty, filename);
				}
				this.OnRenamed(renamed);
				renamed = null;
				break;
			}
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x0003D9F4 File Offset: 0x0003BBF4
		private void Start()
		{
			FileSystemWatcher.watcher.StartDispatching(this);
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x0003DA04 File Offset: 0x0003BC04
		private void Stop()
		{
			FileSystemWatcher.watcher.StopDispatching(this);
		}

		// Token: 0x060016AE RID: 5806
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int InternalSupportsFSW();

		// Token: 0x0400072D RID: 1837
		private bool enableRaisingEvents;

		// Token: 0x0400072E RID: 1838
		private string filter;

		// Token: 0x0400072F RID: 1839
		private bool includeSubdirectories;

		// Token: 0x04000730 RID: 1840
		private int internalBufferSize;

		// Token: 0x04000731 RID: 1841
		private NotifyFilters notifyFilter;

		// Token: 0x04000732 RID: 1842
		private string path;

		// Token: 0x04000733 RID: 1843
		private string fullpath;

		// Token: 0x04000734 RID: 1844
		private System.ComponentModel.ISynchronizeInvoke synchronizingObject;

		// Token: 0x04000735 RID: 1845
		private WaitForChangedResult lastData;

		// Token: 0x04000736 RID: 1846
		private bool waiting;

		// Token: 0x04000737 RID: 1847
		private SearchPattern2 pattern;

		// Token: 0x04000738 RID: 1848
		private bool disposed;

		// Token: 0x04000739 RID: 1849
		private string mangledFilter;

		// Token: 0x0400073A RID: 1850
		private static IFileWatcher watcher;

		// Token: 0x0400073B RID: 1851
		private static object lockobj = new object();

		// Token: 0x02000282 RID: 642
		private enum EventType
		{
			// Token: 0x04000742 RID: 1858
			FileSystemEvent,
			// Token: 0x04000743 RID: 1859
			ErrorEvent,
			// Token: 0x04000744 RID: 1860
			RenameEvent
		}
	}
}
