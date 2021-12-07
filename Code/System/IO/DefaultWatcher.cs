using System;
using System.Collections;
using System.Threading;

namespace System.IO
{
	// Token: 0x02000277 RID: 631
	internal class DefaultWatcher : IFileWatcher
	{
		// Token: 0x0600164B RID: 5707 RVA: 0x0003BBAC File Offset: 0x00039DAC
		private DefaultWatcher()
		{
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x0003BBC4 File Offset: 0x00039DC4
		public static bool GetInstance(out IFileWatcher watcher)
		{
			if (DefaultWatcher.instance != null)
			{
				watcher = DefaultWatcher.instance;
				return true;
			}
			DefaultWatcher.instance = new DefaultWatcher();
			watcher = DefaultWatcher.instance;
			return true;
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x0003BBEC File Offset: 0x00039DEC
		public void StartDispatching(FileSystemWatcher fsw)
		{
			lock (this)
			{
				if (DefaultWatcher.watches == null)
				{
					DefaultWatcher.watches = new Hashtable();
				}
				if (DefaultWatcher.thread == null)
				{
					DefaultWatcher.thread = new Thread(new ThreadStart(this.Monitor));
					DefaultWatcher.thread.IsBackground = true;
					DefaultWatcher.thread.Start();
				}
			}
			Hashtable obj = DefaultWatcher.watches;
			lock (obj)
			{
				DefaultWatcherData defaultWatcherData = (DefaultWatcherData)DefaultWatcher.watches[fsw];
				if (defaultWatcherData == null)
				{
					defaultWatcherData = new DefaultWatcherData();
					defaultWatcherData.Files = new Hashtable();
					DefaultWatcher.watches[fsw] = defaultWatcherData;
				}
				defaultWatcherData.FSW = fsw;
				defaultWatcherData.Directory = fsw.FullPath;
				defaultWatcherData.NoWildcards = !fsw.Pattern.HasWildcard;
				if (defaultWatcherData.NoWildcards)
				{
					defaultWatcherData.FileMask = Path.Combine(defaultWatcherData.Directory, fsw.MangledFilter);
				}
				else
				{
					defaultWatcherData.FileMask = fsw.MangledFilter;
				}
				defaultWatcherData.IncludeSubdirs = fsw.IncludeSubdirectories;
				defaultWatcherData.Enabled = true;
				defaultWatcherData.DisabledTime = DateTime.MaxValue;
				this.UpdateDataAndDispatch(defaultWatcherData, false);
			}
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x0003BD58 File Offset: 0x00039F58
		public void StopDispatching(FileSystemWatcher fsw)
		{
			lock (this)
			{
				if (DefaultWatcher.watches == null)
				{
					return;
				}
			}
			Hashtable obj = DefaultWatcher.watches;
			lock (obj)
			{
				DefaultWatcherData defaultWatcherData = (DefaultWatcherData)DefaultWatcher.watches[fsw];
				if (defaultWatcherData != null)
				{
					defaultWatcherData.Enabled = false;
					defaultWatcherData.DisabledTime = DateTime.Now;
				}
			}
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x0003BE00 File Offset: 0x0003A000
		private void Monitor()
		{
			int num = 0;
			for (;;)
			{
				Thread.Sleep(750);
				Hashtable obj = DefaultWatcher.watches;
				Hashtable hashtable;
				lock (obj)
				{
					if (DefaultWatcher.watches.Count == 0)
					{
						if (++num == 20)
						{
							break;
						}
						continue;
					}
					else
					{
						hashtable = (Hashtable)DefaultWatcher.watches.Clone();
					}
				}
				if (hashtable.Count != 0)
				{
					num = 0;
					foreach (object obj2 in hashtable.Values)
					{
						DefaultWatcherData defaultWatcherData = (DefaultWatcherData)obj2;
						bool flag = this.UpdateDataAndDispatch(defaultWatcherData, true);
						if (flag)
						{
							Hashtable obj3 = DefaultWatcher.watches;
							lock (obj3)
							{
								DefaultWatcher.watches.Remove(defaultWatcherData.FSW);
							}
						}
					}
				}
			}
			lock (this)
			{
				DefaultWatcher.thread = null;
			}
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x0003BF7C File Offset: 0x0003A17C
		private bool UpdateDataAndDispatch(DefaultWatcherData data, bool dispatch)
		{
			if (!data.Enabled)
			{
				return data.DisabledTime != DateTime.MaxValue && (DateTime.Now - data.DisabledTime).TotalSeconds > 5.0;
			}
			this.DoFiles(data, data.Directory, dispatch);
			return false;
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x0003BFE0 File Offset: 0x0003A1E0
		private static void DispatchEvents(FileSystemWatcher fsw, FileAction action, string filename)
		{
			RenamedEventArgs renamedEventArgs = null;
			lock (fsw)
			{
				fsw.DispatchEvents(action, filename, ref renamedEventArgs);
				if (fsw.Waiting)
				{
					fsw.Waiting = false;
					System.Threading.Monitor.PulseAll(fsw);
				}
			}
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x0003C044 File Offset: 0x0003A244
		private void DoFiles(DefaultWatcherData data, string directory, bool dispatch)
		{
			bool flag = Directory.Exists(directory);
			if (flag && data.IncludeSubdirs)
			{
				foreach (string directory2 in Directory.GetDirectories(directory))
				{
					this.DoFiles(data, directory2, dispatch);
				}
			}
			string[] array = null;
			if (!flag)
			{
				array = DefaultWatcher.NoStringsArray;
			}
			else if (!data.NoWildcards)
			{
				array = Directory.GetFileSystemEntries(directory, data.FileMask);
			}
			else if (File.Exists(data.FileMask) || Directory.Exists(data.FileMask))
			{
				array = new string[]
				{
					data.FileMask
				};
			}
			else
			{
				array = DefaultWatcher.NoStringsArray;
			}
			foreach (object obj in data.Files.Keys)
			{
				string key = (string)obj;
				FileData fileData = (FileData)data.Files[key];
				if (fileData.Directory == directory)
				{
					fileData.NotExists = true;
				}
			}
			foreach (string text in array)
			{
				FileData fileData2 = (FileData)data.Files[text];
				if (fileData2 == null)
				{
					try
					{
						data.Files.Add(text, DefaultWatcher.CreateFileData(directory, text));
					}
					catch
					{
						data.Files.Remove(text);
						goto IL_1BD;
					}
					if (dispatch)
					{
						DefaultWatcher.DispatchEvents(data.FSW, FileAction.Added, text);
					}
				}
				else if (fileData2.Directory == directory)
				{
					fileData2.NotExists = false;
				}
				IL_1BD:;
			}
			if (!dispatch)
			{
				return;
			}
			ArrayList arrayList = null;
			foreach (object obj2 in data.Files.Keys)
			{
				string text2 = (string)obj2;
				FileData fileData3 = (FileData)data.Files[text2];
				if (fileData3.NotExists)
				{
					if (arrayList == null)
					{
						arrayList = new ArrayList();
					}
					arrayList.Add(text2);
					DefaultWatcher.DispatchEvents(data.FSW, FileAction.Removed, text2);
				}
			}
			if (arrayList != null)
			{
				foreach (object obj3 in arrayList)
				{
					string key2 = (string)obj3;
					data.Files.Remove(key2);
				}
				arrayList = null;
			}
			foreach (object obj4 in data.Files.Keys)
			{
				string text3 = (string)obj4;
				FileData fileData4 = (FileData)data.Files[text3];
				DateTime creationTime;
				DateTime lastWriteTime;
				try
				{
					creationTime = File.GetCreationTime(text3);
					lastWriteTime = File.GetLastWriteTime(text3);
				}
				catch
				{
					if (arrayList == null)
					{
						arrayList = new ArrayList();
					}
					arrayList.Add(text3);
					DefaultWatcher.DispatchEvents(data.FSW, FileAction.Removed, text3);
					continue;
				}
				if (creationTime != fileData4.CreationTime || lastWriteTime != fileData4.LastWriteTime)
				{
					fileData4.CreationTime = creationTime;
					fileData4.LastWriteTime = lastWriteTime;
					DefaultWatcher.DispatchEvents(data.FSW, FileAction.Modified, text3);
				}
			}
			if (arrayList != null)
			{
				foreach (object obj5 in arrayList)
				{
					string key3 = (string)obj5;
					data.Files.Remove(key3);
				}
			}
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x0003C508 File Offset: 0x0003A708
		private static FileData CreateFileData(string directory, string filename)
		{
			FileData fileData = new FileData();
			string path = Path.Combine(directory, filename);
			fileData.Directory = directory;
			fileData.Attributes = File.GetAttributes(path);
			fileData.CreationTime = File.GetCreationTime(path);
			fileData.LastWriteTime = File.GetLastWriteTime(path);
			return fileData;
		}

		// Token: 0x04000701 RID: 1793
		private static DefaultWatcher instance;

		// Token: 0x04000702 RID: 1794
		private static Thread thread;

		// Token: 0x04000703 RID: 1795
		private static Hashtable watches;

		// Token: 0x04000704 RID: 1796
		private static string[] NoStringsArray = new string[0];
	}
}
