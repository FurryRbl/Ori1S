using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace System.IO
{
	// Token: 0x02000288 RID: 648
	internal class InotifyWatcher : IFileWatcher
	{
		// Token: 0x060016B5 RID: 5813 RVA: 0x0003DA74 File Offset: 0x0003BC74
		private InotifyWatcher()
		{
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x0003DA88 File Offset: 0x0003BC88
		public static bool GetInstance(out IFileWatcher watcher, bool gamin)
		{
			if (InotifyWatcher.failed)
			{
				watcher = null;
				return false;
			}
			if (InotifyWatcher.instance != null)
			{
				watcher = InotifyWatcher.instance;
				return true;
			}
			InotifyWatcher.FD = InotifyWatcher.GetInotifyInstance();
			if ((long)InotifyWatcher.FD == -1L)
			{
				InotifyWatcher.failed = true;
				watcher = null;
				return false;
			}
			InotifyWatcher.watches = Hashtable.Synchronized(new Hashtable());
			InotifyWatcher.requests = Hashtable.Synchronized(new Hashtable());
			InotifyWatcher.instance = new InotifyWatcher();
			watcher = InotifyWatcher.instance;
			return true;
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x0003DB10 File Offset: 0x0003BD10
		public void StartDispatching(FileSystemWatcher fsw)
		{
			ParentInotifyData parentInotifyData;
			lock (this)
			{
				if ((long)InotifyWatcher.FD == -1L)
				{
					InotifyWatcher.FD = InotifyWatcher.GetInotifyInstance();
				}
				if (InotifyWatcher.thread == null)
				{
					InotifyWatcher.thread = new Thread(new ThreadStart(this.Monitor));
					InotifyWatcher.thread.IsBackground = true;
					InotifyWatcher.thread.Start();
				}
				parentInotifyData = (ParentInotifyData)InotifyWatcher.watches[fsw];
			}
			if (parentInotifyData == null)
			{
				InotifyData inotifyData = new InotifyData();
				inotifyData.FSW = fsw;
				inotifyData.Directory = fsw.FullPath;
				parentInotifyData = new ParentInotifyData();
				parentInotifyData.IncludeSubdirs = fsw.IncludeSubdirectories;
				parentInotifyData.Enabled = true;
				parentInotifyData.children = new ArrayList();
				parentInotifyData.data = inotifyData;
				InotifyWatcher.watches[fsw] = parentInotifyData;
				try
				{
					InotifyWatcher.StartMonitoringDirectory(inotifyData, false);
					lock (this)
					{
						InotifyWatcher.AppendRequestData(inotifyData);
						InotifyWatcher.stop = false;
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x0003DC68 File Offset: 0x0003BE68
		private static void AppendRequestData(InotifyData data)
		{
			int watch = data.Watch;
			object obj = InotifyWatcher.requests[watch];
			if (obj == null)
			{
				InotifyWatcher.requests[data.Watch] = data;
			}
			else if (obj is InotifyData)
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(obj);
				arrayList.Add(data);
				InotifyWatcher.requests[data.Watch] = arrayList;
			}
			else
			{
				ArrayList arrayList = (ArrayList)obj;
				arrayList.Add(data);
			}
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x0003DCFC File Offset: 0x0003BEFC
		private static bool RemoveRequestData(InotifyData data)
		{
			int watch = data.Watch;
			object obj = InotifyWatcher.requests[watch];
			if (obj == null)
			{
				return true;
			}
			if (obj is InotifyData)
			{
				if (obj == data)
				{
					InotifyWatcher.requests.Remove(watch);
					return true;
				}
				return false;
			}
			else
			{
				ArrayList arrayList = (ArrayList)obj;
				arrayList.Remove(data);
				if (arrayList.Count == 0)
				{
					InotifyWatcher.requests.Remove(watch);
					return true;
				}
				return false;
			}
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x0003DD7C File Offset: 0x0003BF7C
		private static InotifyMask GetMaskFromFilters(NotifyFilters filters)
		{
			InotifyMask inotifyMask = InotifyMask.Create | InotifyMask.Delete | InotifyMask.DeleteSelf | InotifyMask.AddMask;
			if ((filters & NotifyFilters.Attributes) != (NotifyFilters)0)
			{
				inotifyMask |= InotifyMask.Attrib;
			}
			if ((filters & NotifyFilters.Security) != (NotifyFilters)0)
			{
				inotifyMask |= InotifyMask.Attrib;
			}
			if ((filters & NotifyFilters.Size) != (NotifyFilters)0)
			{
				inotifyMask |= InotifyMask.Attrib;
				inotifyMask |= InotifyMask.Modify;
			}
			if ((filters & NotifyFilters.LastAccess) != (NotifyFilters)0)
			{
				inotifyMask |= InotifyMask.Attrib;
				inotifyMask |= InotifyMask.Access;
				inotifyMask |= InotifyMask.Modify;
			}
			if ((filters & NotifyFilters.LastWrite) != (NotifyFilters)0)
			{
				inotifyMask |= InotifyMask.Attrib;
				inotifyMask |= InotifyMask.Modify;
			}
			if ((filters & NotifyFilters.FileName) != (NotifyFilters)0)
			{
				inotifyMask |= InotifyMask.MovedFrom;
				inotifyMask |= InotifyMask.MovedTo;
			}
			if ((filters & NotifyFilters.DirectoryName) != (NotifyFilters)0)
			{
				inotifyMask |= InotifyMask.MovedFrom;
				inotifyMask |= InotifyMask.MovedTo;
			}
			return inotifyMask;
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x0003DE0C File Offset: 0x0003C00C
		private static void StartMonitoringDirectory(InotifyData data, bool justcreated)
		{
			InotifyMask maskFromFilters = InotifyWatcher.GetMaskFromFilters(data.FSW.NotifyFilter);
			int num = InotifyWatcher.AddDirectoryWatch(InotifyWatcher.FD, data.Directory, maskFromFilters);
			if (num != -1)
			{
				FileSystemWatcher fsw = data.FSW;
				data.Watch = num;
				ParentInotifyData parentInotifyData = (ParentInotifyData)InotifyWatcher.watches[fsw];
				if (parentInotifyData.IncludeSubdirs)
				{
					foreach (string text in Directory.GetDirectories(data.Directory))
					{
						InotifyData inotifyData = new InotifyData();
						inotifyData.FSW = fsw;
						inotifyData.Directory = text;
						if (justcreated)
						{
							FileSystemWatcher obj = fsw;
							lock (obj)
							{
								RenamedEventArgs renamedEventArgs = null;
								if (fsw.Pattern.IsMatch(text))
								{
									fsw.DispatchEvents(FileAction.Added, text, ref renamedEventArgs);
									if (fsw.Waiting)
									{
										fsw.Waiting = false;
										System.Threading.Monitor.PulseAll(fsw);
									}
								}
							}
						}
						try
						{
							InotifyWatcher.StartMonitoringDirectory(inotifyData, justcreated);
							InotifyWatcher.AppendRequestData(inotifyData);
							parentInotifyData.children.Add(inotifyData);
						}
						catch
						{
						}
					}
				}
				if (justcreated)
				{
					foreach (string text2 in Directory.GetFiles(data.Directory))
					{
						FileSystemWatcher obj2 = fsw;
						lock (obj2)
						{
							RenamedEventArgs renamedEventArgs2 = null;
							if (fsw.Pattern.IsMatch(text2))
							{
								fsw.DispatchEvents(FileAction.Added, text2, ref renamedEventArgs2);
								fsw.DispatchEvents(FileAction.Modified, text2, ref renamedEventArgs2);
								if (fsw.Waiting)
								{
									fsw.Waiting = false;
									System.Threading.Monitor.PulseAll(fsw);
								}
							}
						}
					}
				}
				return;
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (lastWin32Error == 4)
			{
				string arg = "(unknown)";
				try
				{
					using (StreamReader streamReader = new StreamReader("/proc/sys/fs/inotify/max_user_watches"))
					{
						arg = streamReader.ReadLine();
					}
				}
				catch
				{
				}
				string message = string.Format("The per-user inotify watches limit of {0} has been reached. If you're experiencing problems with your application, increase that limit in /proc/sys/fs/inotify/max_user_watches.", arg);
				throw new System.ComponentModel.Win32Exception(lastWin32Error, message);
			}
			throw new System.ComponentModel.Win32Exception(lastWin32Error);
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x0003E0B0 File Offset: 0x0003C2B0
		public void StopDispatching(FileSystemWatcher fsw)
		{
			lock (this)
			{
				ParentInotifyData parentInotifyData = (ParentInotifyData)InotifyWatcher.watches[fsw];
				if (parentInotifyData != null)
				{
					if (InotifyWatcher.RemoveRequestData(parentInotifyData.data))
					{
						InotifyWatcher.StopMonitoringDirectory(parentInotifyData.data);
					}
					InotifyWatcher.watches.Remove(fsw);
					if (InotifyWatcher.watches.Count == 0)
					{
						InotifyWatcher.stop = true;
						IntPtr fd = InotifyWatcher.FD;
						InotifyWatcher.FD = (IntPtr)(-1);
						InotifyWatcher.Close(fd);
					}
					if (parentInotifyData.IncludeSubdirs)
					{
						foreach (object obj in parentInotifyData.children)
						{
							InotifyData data = (InotifyData)obj;
							if (InotifyWatcher.RemoveRequestData(data))
							{
								InotifyWatcher.StopMonitoringDirectory(data);
							}
						}
					}
				}
			}
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x0003E1DC File Offset: 0x0003C3DC
		private static void StopMonitoringDirectory(InotifyData data)
		{
			InotifyWatcher.RemoveWatch(InotifyWatcher.FD, data.Watch);
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x0003E1F0 File Offset: 0x0003C3F0
		private void Monitor()
		{
			byte[] array = new byte[4096];
			while (!InotifyWatcher.stop)
			{
				int num = InotifyWatcher.ReadFromFD(InotifyWatcher.FD, array, (IntPtr)array.Length);
				if (num != -1)
				{
					lock (this)
					{
						this.ProcessEvents(array, num);
					}
				}
			}
			lock (this)
			{
				InotifyWatcher.thread = null;
				InotifyWatcher.stop = false;
			}
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x0003E2A8 File Offset: 0x0003C4A8
		private static int ReadEvent(byte[] source, int off, int size, out InotifyEvent evt)
		{
			evt = default(InotifyEvent);
			if (size <= 0 || off > size - 16)
			{
				return -1;
			}
			int num;
			if (BitConverter.IsLittleEndian)
			{
				evt.WatchDescriptor = (int)source[off] + ((int)source[off + 1] << 8) + ((int)source[off + 2] << 16) + ((int)source[off + 3] << 24);
				evt.Mask = (InotifyMask)((int)source[off + 4] + ((int)source[off + 5] << 8) + ((int)source[off + 6] << 16) + ((int)source[off + 7] << 24));
				num = (int)source[off + 12] + ((int)source[off + 13] << 8) + ((int)source[off + 14] << 16) + ((int)source[off + 15] << 24);
			}
			else
			{
				evt.WatchDescriptor = (int)source[off + 3] + ((int)source[off + 2] << 8) + ((int)source[off + 1] << 16) + ((int)source[off] << 24);
				evt.Mask = (InotifyMask)((int)source[off + 7] + ((int)source[off + 6] << 8) + ((int)source[off + 5] << 16) + ((int)source[off + 4] << 24));
				num = (int)source[off + 15] + ((int)source[off + 14] << 8) + ((int)source[off + 13] << 16) + ((int)source[off + 12] << 24);
			}
			if (num > 0)
			{
				if (off > size - 16 - num)
				{
					return -1;
				}
				string @string = Encoding.UTF8.GetString(source, off + 16, num);
				evt.Name = @string.Trim(new char[1]);
			}
			else
			{
				evt.Name = null;
			}
			return 16 + num;
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x0003E400 File Offset: 0x0003C600
		private static IEnumerable GetEnumerator(object source)
		{
			if (source == null)
			{
				yield break;
			}
			if (source is InotifyData)
			{
				yield return source;
			}
			if (source is ArrayList)
			{
				ArrayList list = (ArrayList)source;
				for (int i = 0; i < list.Count; i++)
				{
					yield return list[i];
				}
			}
			yield break;
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x0003E42C File Offset: 0x0003C62C
		private void ProcessEvents(byte[] buffer, int length)
		{
			ArrayList arrayList = null;
			int num = 0;
			RenamedEventArgs renamedEventArgs = null;
			while (length > num)
			{
				InotifyEvent inotifyEvent;
				int num2 = InotifyWatcher.ReadEvent(buffer, num, length, out inotifyEvent);
				if (num2 <= 0)
				{
					break;
				}
				num += num2;
				InotifyMask inotifyMask = inotifyEvent.Mask;
				bool flag = (inotifyMask & InotifyMask.Directory) != (InotifyMask)0U;
				inotifyMask &= InotifyWatcher.Interesting;
				if (inotifyMask != (InotifyMask)0U)
				{
					foreach (object obj in InotifyWatcher.GetEnumerator(InotifyWatcher.requests[inotifyEvent.WatchDescriptor]))
					{
						InotifyData inotifyData = (InotifyData)obj;
						ParentInotifyData parentInotifyData = (ParentInotifyData)InotifyWatcher.watches[inotifyData.FSW];
						if (inotifyData != null && parentInotifyData.Enabled)
						{
							string directory = inotifyData.Directory;
							string text = inotifyEvent.Name;
							if (text == null)
							{
								text = directory;
							}
							FileSystemWatcher fsw = inotifyData.FSW;
							FileAction fileAction = (FileAction)0;
							if ((inotifyMask & (InotifyMask.Modify | InotifyMask.Attrib)) != (InotifyMask)0U)
							{
								fileAction = FileAction.Modified;
							}
							else if ((inotifyMask & InotifyMask.Create) != (InotifyMask)0U)
							{
								fileAction = FileAction.Added;
							}
							else if ((inotifyMask & InotifyMask.Delete) != (InotifyMask)0U)
							{
								fileAction = FileAction.Removed;
							}
							else if ((inotifyMask & InotifyMask.DeleteSelf) != (InotifyMask)0U)
							{
								if (inotifyData.Watch != parentInotifyData.data.Watch)
								{
									continue;
								}
								fileAction = FileAction.Removed;
							}
							else
							{
								if ((inotifyMask & InotifyMask.MoveSelf) != (InotifyMask)0U)
								{
									continue;
								}
								if ((inotifyMask & InotifyMask.MovedFrom) != (InotifyMask)0U)
								{
									InotifyEvent inotifyEvent2;
									int num3 = InotifyWatcher.ReadEvent(buffer, num, length, out inotifyEvent2);
									if (num3 == -1 || (inotifyEvent2.Mask & InotifyMask.MovedTo) == (InotifyMask)0U || inotifyEvent.WatchDescriptor != inotifyEvent2.WatchDescriptor)
									{
										fileAction = FileAction.Removed;
									}
									else
									{
										num += num3;
										fileAction = FileAction.RenamedNewName;
										renamedEventArgs = new RenamedEventArgs(WatcherChangeTypes.Renamed, inotifyData.Directory, inotifyEvent2.Name, inotifyEvent.Name);
										if (inotifyEvent.Name != inotifyData.Directory && !fsw.Pattern.IsMatch(inotifyEvent.Name))
										{
											text = inotifyEvent2.Name;
										}
									}
								}
								else if ((inotifyMask & InotifyMask.MovedTo) != (InotifyMask)0U)
								{
									fileAction = FileAction.Added;
								}
							}
							if (fsw.IncludeSubdirectories)
							{
								string fullPath = fsw.FullPath;
								string text2 = inotifyData.Directory;
								if (text2 != fullPath)
								{
									int length2 = fullPath.Length;
									int num4 = 1;
									if (length2 > 1 && fullPath[length2 - 1] == Path.DirectorySeparatorChar)
									{
										num4 = 0;
									}
									string path = text2.Substring(fullPath.Length + num4);
									text2 = Path.Combine(text2, text);
									text = Path.Combine(path, text);
								}
								else
								{
									text2 = Path.Combine(fullPath, text);
								}
								if (fileAction == FileAction.Added && flag)
								{
									if (arrayList == null)
									{
										arrayList = new ArrayList(2);
									}
									arrayList.Add(new InotifyData
									{
										FSW = fsw,
										Directory = text2
									});
								}
								if (fileAction == FileAction.RenamedNewName && flag)
								{
									string oldFullPath = renamedEventArgs.OldFullPath;
									string fullPath2 = renamedEventArgs.FullPath;
									int length3 = oldFullPath.Length;
									foreach (object obj2 in parentInotifyData.children)
									{
										InotifyData inotifyData2 = (InotifyData)obj2;
										if (inotifyData2.Directory.StartsWith(oldFullPath, StringComparison.Ordinal))
										{
											inotifyData2.Directory = fullPath2 + inotifyData2.Directory.Substring(length3);
										}
									}
								}
							}
							if (fileAction == FileAction.Removed && text == inotifyData.Directory)
							{
								int num5 = parentInotifyData.children.IndexOf(inotifyData);
								if (num5 != -1)
								{
									parentInotifyData.children.RemoveAt(num5);
									if (!fsw.Pattern.IsMatch(Path.GetFileName(text)))
									{
										continue;
									}
								}
							}
							if (!(text != inotifyData.Directory) || fsw.Pattern.IsMatch(Path.GetFileName(text)))
							{
								FileSystemWatcher obj3 = fsw;
								lock (obj3)
								{
									fsw.DispatchEvents(fileAction, text, ref renamedEventArgs);
									if (fileAction == FileAction.RenamedNewName)
									{
										renamedEventArgs = null;
									}
									if (fsw.Waiting)
									{
										fsw.Waiting = false;
										System.Threading.Monitor.PulseAll(fsw);
									}
								}
							}
						}
					}
				}
			}
			if (arrayList != null)
			{
				foreach (object obj4 in arrayList)
				{
					InotifyData inotifyData3 = (InotifyData)obj4;
					try
					{
						InotifyWatcher.StartMonitoringDirectory(inotifyData3, true);
						InotifyWatcher.AppendRequestData(inotifyData3);
						((ParentInotifyData)InotifyWatcher.watches[inotifyData3.FSW]).children.Add(inotifyData3);
					}
					catch
					{
					}
				}
				arrayList.Clear();
			}
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x0003E9D8 File Offset: 0x0003CBD8
		private static int AddDirectoryWatch(IntPtr fd, string directory, InotifyMask mask)
		{
			mask |= InotifyMask.Directory;
			return InotifyWatcher.AddWatch(fd, directory, mask);
		}

		// Token: 0x060016C4 RID: 5828
		[DllImport("libc", EntryPoint = "close")]
		internal static extern int Close(IntPtr fd);

		// Token: 0x060016C5 RID: 5829
		[DllImport("libc", EntryPoint = "read")]
		private static extern int ReadFromFD(IntPtr fd, byte[] buffer, IntPtr length);

		// Token: 0x060016C6 RID: 5830
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetInotifyInstance();

		// Token: 0x060016C7 RID: 5831
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int AddWatch(IntPtr fd, string name, InotifyMask mask);

		// Token: 0x060016C8 RID: 5832
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr RemoveWatch(IntPtr fd, int wd);

		// Token: 0x04000766 RID: 1894
		private static bool failed;

		// Token: 0x04000767 RID: 1895
		private static InotifyWatcher instance;

		// Token: 0x04000768 RID: 1896
		private static Hashtable watches;

		// Token: 0x04000769 RID: 1897
		private static Hashtable requests;

		// Token: 0x0400076A RID: 1898
		private static IntPtr FD;

		// Token: 0x0400076B RID: 1899
		private static Thread thread;

		// Token: 0x0400076C RID: 1900
		private static bool stop;

		// Token: 0x0400076D RID: 1901
		private static InotifyMask Interesting = InotifyMask.Modify | InotifyMask.Attrib | InotifyMask.MovedFrom | InotifyMask.MovedTo | InotifyMask.Create | InotifyMask.Delete | InotifyMask.DeleteSelf;
	}
}
