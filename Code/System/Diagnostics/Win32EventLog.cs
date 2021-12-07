using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Microsoft.Win32;

namespace System.Diagnostics
{
	// Token: 0x02000265 RID: 613
	internal class Win32EventLog : EventLogImpl
	{
		// Token: 0x060015C4 RID: 5572 RVA: 0x00039818 File Offset: 0x00037A18
		public Win32EventLog(EventLog coreEventLog) : base(coreEventLog)
		{
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x00039824 File Offset: 0x00037A24
		public override void BeginInit()
		{
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x00039828 File Offset: 0x00037A28
		public override void Clear()
		{
			int num = Win32EventLog.PInvoke.ClearEventLog(this.ReadHandle, null);
			if (num != 1)
			{
				throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x00039854 File Offset: 0x00037A54
		public override void Close()
		{
			if (this._readHandle != IntPtr.Zero)
			{
				this.CloseEventLog(this._readHandle);
				this._readHandle = IntPtr.Zero;
			}
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x00039890 File Offset: 0x00037A90
		public override void CreateEventSource(EventSourceCreationData sourceData)
		{
			using (RegistryKey eventLogKey = Win32EventLog.GetEventLogKey(sourceData.MachineName, true))
			{
				if (eventLogKey == null)
				{
					throw new InvalidOperationException("EventLog registry key is missing.");
				}
				bool flag = false;
				RegistryKey registryKey = null;
				try
				{
					registryKey = eventLogKey.OpenSubKey(sourceData.LogName, true);
					if (registryKey == null)
					{
						base.ValidateCustomerLogName(sourceData.LogName, sourceData.MachineName);
						registryKey = eventLogKey.CreateSubKey(sourceData.LogName);
						registryKey.SetValue("Sources", new string[]
						{
							sourceData.LogName,
							sourceData.Source
						});
						Win32EventLog.UpdateLogRegistry(registryKey);
						using (RegistryKey registryKey2 = registryKey.CreateSubKey(sourceData.LogName))
						{
							Win32EventLog.UpdateSourceRegistry(registryKey2, sourceData);
						}
						flag = true;
					}
					if (sourceData.LogName != sourceData.Source)
					{
						if (!flag)
						{
							string[] array = (string[])registryKey.GetValue("Sources");
							if (array == null)
							{
								registryKey.SetValue("Sources", new string[]
								{
									sourceData.LogName,
									sourceData.Source
								});
							}
							else
							{
								bool flag2 = false;
								for (int i = 0; i < array.Length; i++)
								{
									if (array[i] == sourceData.Source)
									{
										flag2 = true;
										break;
									}
								}
								if (!flag2)
								{
									string[] array2 = new string[array.Length + 1];
									Array.Copy(array, 0, array2, 0, array.Length);
									array2[array.Length] = sourceData.Source;
									registryKey.SetValue("Sources", array2);
								}
							}
						}
						using (RegistryKey registryKey3 = registryKey.CreateSubKey(sourceData.Source))
						{
							Win32EventLog.UpdateSourceRegistry(registryKey3, sourceData);
						}
					}
				}
				finally
				{
					if (registryKey != null)
					{
						registryKey.Close();
					}
				}
			}
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x00039AC8 File Offset: 0x00037CC8
		public override void Delete(string logName, string machineName)
		{
			using (RegistryKey eventLogKey = Win32EventLog.GetEventLogKey(machineName, true))
			{
				if (eventLogKey == null)
				{
					throw new InvalidOperationException("The event log key does not exist.");
				}
				using (RegistryKey registryKey = eventLogKey.OpenSubKey(logName, false))
				{
					if (registryKey == null)
					{
						throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Event Log '{0}' does not exist on computer '{1}'.", new object[]
						{
							logName,
							machineName
						}));
					}
					base.CoreEventLog.Clear();
					string text = (string)registryKey.GetValue("File");
					if (text != null)
					{
						try
						{
							File.Delete(text);
						}
						catch (Exception)
						{
						}
					}
				}
				eventLogKey.DeleteSubKeyTree(logName);
			}
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x00039BCC File Offset: 0x00037DCC
		public override void DeleteEventSource(string source, string machineName)
		{
			using (RegistryKey registryKey = Win32EventLog.FindLogKeyBySource(source, machineName, true))
			{
				if (registryKey == null)
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The source '{0}' is not registered on computer '{1}'.", new object[]
					{
						source,
						machineName
					}));
				}
				registryKey.DeleteSubKeyTree(source);
				string[] array = (string[])registryKey.GetValue("Sources");
				if (array != null)
				{
					ArrayList arrayList = new ArrayList();
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i] != source)
						{
							arrayList.Add(array[i]);
						}
					}
					string[] array2 = new string[arrayList.Count];
					arrayList.CopyTo(array2, 0);
					registryKey.SetValue("Sources", array2);
				}
			}
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x00039CAC File Offset: 0x00037EAC
		public override void Dispose(bool disposing)
		{
			this.Close();
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x00039CB4 File Offset: 0x00037EB4
		public override void EndInit()
		{
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x00039CB8 File Offset: 0x00037EB8
		public override bool Exists(string logName, string machineName)
		{
			bool result;
			using (RegistryKey registryKey = Win32EventLog.FindLogKeyByName(logName, machineName, false))
			{
				result = (registryKey != null);
			}
			return result;
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x00039D0C File Offset: 0x00037F0C
		[MonoTODO]
		protected override string FormatMessage(string source, uint messageID, string[] replacementStrings)
		{
			string text = null;
			string[] messageResourceDlls = this.GetMessageResourceDlls(source, "EventMessageFile");
			for (int i = 0; i < messageResourceDlls.Length; i++)
			{
				text = Win32EventLog.FetchMessage(messageResourceDlls[i], messageID, replacementStrings);
				if (text != null)
				{
					break;
				}
			}
			return (text == null) ? string.Join(", ", replacementStrings) : text;
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x00039D6C File Offset: 0x00037F6C
		private string FormatCategory(string source, int category)
		{
			string text = null;
			string[] messageResourceDlls = this.GetMessageResourceDlls(source, "CategoryMessageFile");
			for (int i = 0; i < messageResourceDlls.Length; i++)
			{
				text = Win32EventLog.FetchMessage(messageResourceDlls[i], (uint)category, new string[0]);
				if (text != null)
				{
					break;
				}
			}
			return (text == null) ? ("(" + category.ToString(CultureInfo.InvariantCulture) + ")") : text;
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x00039DE0 File Offset: 0x00037FE0
		protected override int GetEntryCount()
		{
			int result = 0;
			int numberOfEventLogRecords = Win32EventLog.PInvoke.GetNumberOfEventLogRecords(this.ReadHandle, ref result);
			if (numberOfEventLogRecords != 1)
			{
				throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
			}
			return result;
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x00039E10 File Offset: 0x00038010
		protected override EventLogEntry GetEntry(int index)
		{
			index += this.OldestEventLogEntry;
			int num = 0;
			int num2 = 0;
			byte[] buffer = new byte[524287];
			this.ReadEventLog(index, buffer, ref num, ref num2);
			MemoryStream memoryStream = new MemoryStream(buffer);
			BinaryReader binaryReader = new BinaryReader(memoryStream);
			binaryReader.ReadBytes(8);
			int index2 = binaryReader.ReadInt32();
			int num3 = binaryReader.ReadInt32();
			int num4 = binaryReader.ReadInt32();
			uint num5 = binaryReader.ReadUInt32();
			int eventID = EventLog.GetEventID((long)((ulong)num5));
			short entryType = binaryReader.ReadInt16();
			short num6 = binaryReader.ReadInt16();
			short num7 = binaryReader.ReadInt16();
			binaryReader.ReadInt16();
			binaryReader.ReadInt32();
			int num8 = binaryReader.ReadInt32();
			int num9 = binaryReader.ReadInt32();
			int num10 = binaryReader.ReadInt32();
			int num11 = binaryReader.ReadInt32();
			int num12 = binaryReader.ReadInt32();
			DateTime dateTime = new DateTime(1970, 1, 1);
			DateTime timeGenerated = dateTime.AddSeconds((double)num3);
			DateTime dateTime2 = new DateTime(1970, 1, 1);
			DateTime timeWritten = dateTime2.AddSeconds((double)num4);
			StringBuilder stringBuilder = new StringBuilder();
			while (binaryReader.PeekChar() != 0)
			{
				stringBuilder.Append(binaryReader.ReadChar());
			}
			binaryReader.ReadChar();
			string source = stringBuilder.ToString();
			stringBuilder.Length = 0;
			while (binaryReader.PeekChar() != 0)
			{
				stringBuilder.Append(binaryReader.ReadChar());
			}
			binaryReader.ReadChar();
			string machineName = stringBuilder.ToString();
			stringBuilder.Length = 0;
			while (binaryReader.PeekChar() != 0)
			{
				stringBuilder.Append(binaryReader.ReadChar());
			}
			binaryReader.ReadChar();
			string userName = null;
			if (num9 != 0)
			{
				memoryStream.Position = (long)num10;
				byte[] sid = binaryReader.ReadBytes(num9);
				userName = Win32EventLog.LookupAccountSid(machineName, sid);
			}
			memoryStream.Position = (long)num8;
			string[] array = new string[(int)num6];
			for (int i = 0; i < (int)num6; i++)
			{
				stringBuilder.Length = 0;
				while (binaryReader.PeekChar() != 0)
				{
					stringBuilder.Append(binaryReader.ReadChar());
				}
				binaryReader.ReadChar();
				array[i] = stringBuilder.ToString();
			}
			byte[] array2 = new byte[num11];
			memoryStream.Position = (long)num12;
			binaryReader.Read(array2, 0, num11);
			string message = this.FormatMessage(source, num5, array);
			string category = this.FormatCategory(source, (int)num7);
			return new EventLogEntry(category, num7, index2, eventID, source, message, userName, machineName, (EventLogEntryType)entryType, timeGenerated, timeWritten, array2, array, (long)((ulong)num5));
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x0003A0A0 File Offset: 0x000382A0
		[MonoTODO]
		protected override string GetLogDisplayName()
		{
			return base.CoreEventLog.Log;
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x0003A0B0 File Offset: 0x000382B0
		protected override string[] GetLogNames(string machineName)
		{
			string[] result;
			using (RegistryKey eventLogKey = Win32EventLog.GetEventLogKey(machineName, true))
			{
				if (eventLogKey == null)
				{
					result = new string[0];
				}
				else
				{
					result = eventLogKey.GetSubKeyNames();
				}
			}
			return result;
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x0003A114 File Offset: 0x00038314
		public override string LogNameFromSourceName(string source, string machineName)
		{
			string result;
			using (RegistryKey registryKey = Win32EventLog.FindLogKeyBySource(source, machineName, false))
			{
				if (registryKey == null)
				{
					result = string.Empty;
				}
				else
				{
					result = Win32EventLog.GetLogName(registryKey);
				}
			}
			return result;
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x0003A178 File Offset: 0x00038378
		public override bool SourceExists(string source, string machineName)
		{
			RegistryKey registryKey = Win32EventLog.FindLogKeyBySource(source, machineName, false);
			if (registryKey != null)
			{
				registryKey.Close();
				return true;
			}
			return false;
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x0003A1A0 File Offset: 0x000383A0
		public override void WriteEntry(string[] replacementStrings, EventLogEntryType type, uint instanceID, short category, byte[] rawData)
		{
			IntPtr intPtr = this.RegisterEventSource();
			try
			{
				int num = Win32EventLog.PInvoke.ReportEvent(intPtr, (ushort)type, (ushort)category, instanceID, IntPtr.Zero, (ushort)replacementStrings.Length, (uint)rawData.Length, replacementStrings, rawData);
				if (num != 1)
				{
					throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
				}
			}
			finally
			{
				this.DeregisterEventSource(intPtr);
			}
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x0003A20C File Offset: 0x0003840C
		private static void UpdateLogRegistry(RegistryKey logKey)
		{
			if (logKey.GetValue("File") == null)
			{
				string logName = Win32EventLog.GetLogName(logKey);
				string path;
				if (logName.Length > 8)
				{
					path = logName.Substring(0, 8) + ".evt";
				}
				else
				{
					path = logName + ".evt";
				}
				string path2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "config");
				logKey.SetValue("File", Path.Combine(path2, path));
			}
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x0003A284 File Offset: 0x00038484
		private static void UpdateSourceRegistry(RegistryKey sourceKey, EventSourceCreationData data)
		{
			if (data.CategoryCount > 0)
			{
				sourceKey.SetValue("CategoryCount", data.CategoryCount);
			}
			if (data.CategoryResourceFile != null && data.CategoryResourceFile.Length > 0)
			{
				sourceKey.SetValue("CategoryMessageFile", data.CategoryResourceFile);
			}
			if (data.MessageResourceFile != null && data.MessageResourceFile.Length > 0)
			{
				sourceKey.SetValue("EventMessageFile", data.MessageResourceFile);
			}
			if (data.ParameterResourceFile != null && data.ParameterResourceFile.Length > 0)
			{
				sourceKey.SetValue("ParameterMessageFile", data.ParameterResourceFile);
			}
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x0003A340 File Offset: 0x00038540
		private static string GetLogName(RegistryKey logKey)
		{
			string name = logKey.Name;
			return name.Substring(name.LastIndexOf("\\") + 1);
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x0003A368 File Offset: 0x00038568
		private void ReadEventLog(int index, byte[] buffer, ref int bytesRead, ref int minBufferNeeded)
		{
			for (int i = 0; i < 3; i++)
			{
				int num = Win32EventLog.PInvoke.ReadEventLog(this.ReadHandle, (Win32EventLog.ReadFlags)6, index, buffer, buffer.Length, ref bytesRead, ref minBufferNeeded);
				if (num != 1)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (i >= 2)
					{
						throw new System.ComponentModel.Win32Exception(lastWin32Error);
					}
					base.CoreEventLog.Reset();
				}
			}
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x0003A3C8 File Offset: 0x000385C8
		[MonoTODO("Support remote machines")]
		private static RegistryKey GetEventLogKey(string machineName, bool writable)
		{
			return Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\EventLog", writable);
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x0003A3DC File Offset: 0x000385DC
		private static RegistryKey FindSourceKeyByName(string source, string machineName, bool writable)
		{
			if (source == null || source.Length == 0)
			{
				return null;
			}
			RegistryKey registryKey = null;
			RegistryKey result;
			try
			{
				registryKey = Win32EventLog.GetEventLogKey(machineName, writable);
				if (registryKey == null)
				{
					result = null;
				}
				else
				{
					string[] subKeyNames = registryKey.GetSubKeyNames();
					for (int i = 0; i < subKeyNames.Length; i++)
					{
						using (RegistryKey registryKey2 = registryKey.OpenSubKey(subKeyNames[i], writable))
						{
							if (registryKey2 == null)
							{
								break;
							}
							RegistryKey registryKey3 = registryKey2.OpenSubKey(source, writable);
							if (registryKey3 != null)
							{
								return registryKey3;
							}
						}
					}
					result = null;
				}
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
			}
			return result;
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x0003A4C4 File Offset: 0x000386C4
		private static RegistryKey FindLogKeyByName(string logName, string machineName, bool writable)
		{
			RegistryKey result;
			using (RegistryKey eventLogKey = Win32EventLog.GetEventLogKey(machineName, writable))
			{
				if (eventLogKey == null)
				{
					result = null;
				}
				else
				{
					result = eventLogKey.OpenSubKey(logName, writable);
				}
			}
			return result;
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x0003A524 File Offset: 0x00038724
		private static RegistryKey FindLogKeyBySource(string source, string machineName, bool writable)
		{
			if (source == null || source.Length == 0)
			{
				return null;
			}
			RegistryKey registryKey = null;
			RegistryKey result;
			try
			{
				registryKey = Win32EventLog.GetEventLogKey(machineName, writable);
				if (registryKey == null)
				{
					result = null;
				}
				else
				{
					string[] subKeyNames = registryKey.GetSubKeyNames();
					for (int i = 0; i < subKeyNames.Length; i++)
					{
						RegistryKey registryKey2 = null;
						try
						{
							RegistryKey registryKey3 = registryKey.OpenSubKey(subKeyNames[i], writable);
							if (registryKey3 != null)
							{
								registryKey2 = registryKey3.OpenSubKey(source, writable);
								if (registryKey2 != null)
								{
									return registryKey3;
								}
							}
						}
						finally
						{
							if (registryKey2 != null)
							{
								registryKey2.Close();
							}
						}
					}
					result = null;
				}
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
			}
			return result;
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x060015DF RID: 5599 RVA: 0x0003A608 File Offset: 0x00038808
		private int OldestEventLogEntry
		{
			get
			{
				int result = 0;
				int oldestEventLogRecord = Win32EventLog.PInvoke.GetOldestEventLogRecord(this.ReadHandle, ref result);
				if (oldestEventLogRecord != 1)
				{
					throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
				}
				return result;
			}
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x0003A638 File Offset: 0x00038838
		private void CloseEventLog(IntPtr hEventLog)
		{
			int num = Win32EventLog.PInvoke.CloseEventLog(hEventLog);
			if (num != 1)
			{
				throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x0003A660 File Offset: 0x00038860
		private void DeregisterEventSource(IntPtr hEventLog)
		{
			int num = Win32EventLog.PInvoke.DeregisterEventSource(hEventLog);
			if (num != 1)
			{
				throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x0003A688 File Offset: 0x00038888
		private static string LookupAccountSid(string machineName, byte[] sid)
		{
			StringBuilder stringBuilder = new StringBuilder();
			uint capacity = (uint)stringBuilder.Capacity;
			StringBuilder stringBuilder2 = new StringBuilder();
			uint capacity2 = (uint)stringBuilder2.Capacity;
			string text = null;
			while (text == null)
			{
				Win32EventLog.SidNameUse sidNameUse;
				if (!Win32EventLog.PInvoke.LookupAccountSid(machineName, sid, stringBuilder, ref capacity, stringBuilder2, ref capacity2, out sidNameUse))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (lastWin32Error == 122)
					{
						stringBuilder.EnsureCapacity((int)capacity);
						stringBuilder2.EnsureCapacity((int)capacity2);
					}
					else
					{
						text = string.Empty;
					}
				}
				else
				{
					text = string.Format("{0}\\{1}", stringBuilder2.ToString(), stringBuilder.ToString());
				}
			}
			return text;
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x0003A724 File Offset: 0x00038924
		private static string FetchMessage(string msgDll, uint messageID, string[] replacementStrings)
		{
			IntPtr intPtr = Win32EventLog.PInvoke.LoadLibraryEx(msgDll, IntPtr.Zero, Win32EventLog.LoadFlags.LibraryAsDataFile);
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			IntPtr intPtr2 = IntPtr.Zero;
			IntPtr[] array = new IntPtr[replacementStrings.Length];
			try
			{
				for (int i = 0; i < replacementStrings.Length; i++)
				{
					array[i] = Marshal.StringToHGlobalAuto(replacementStrings[i]);
				}
				int num = Win32EventLog.PInvoke.FormatMessage(Win32EventLog.FormatMessageFlags.AllocateBuffer | Win32EventLog.FormatMessageFlags.FromHModule | Win32EventLog.FormatMessageFlags.ArgumentArray, intPtr, messageID, 0, ref intPtr2, 0, array);
				if (num != 0)
				{
					string text = Marshal.PtrToStringAuto(intPtr2);
					intPtr2 = Win32EventLog.PInvoke.LocalFree(intPtr2);
					return text.TrimEnd(null);
				}
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error == 317)
				{
				}
			}
			finally
			{
				foreach (IntPtr intPtr3 in array)
				{
					if (intPtr3 != IntPtr.Zero)
					{
						Marshal.FreeHGlobal(intPtr3);
					}
				}
				Win32EventLog.PInvoke.FreeLibrary(intPtr);
			}
			return null;
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x0003A83C File Offset: 0x00038A3C
		private string[] GetMessageResourceDlls(string source, string valueName)
		{
			RegistryKey registryKey = Win32EventLog.FindSourceKeyByName(source, base.CoreEventLog.MachineName, false);
			if (registryKey != null)
			{
				string text = registryKey.GetValue(valueName) as string;
				if (text != null)
				{
					return text.Split(new char[]
					{
						';'
					});
				}
			}
			return new string[0];
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060015E5 RID: 5605 RVA: 0x0003A890 File Offset: 0x00038A90
		private IntPtr ReadHandle
		{
			get
			{
				if (this._readHandle != IntPtr.Zero)
				{
					return this._readHandle;
				}
				string logName = base.CoreEventLog.GetLogName();
				this._readHandle = Win32EventLog.PInvoke.OpenEventLog(base.CoreEventLog.MachineName, logName);
				if (this._readHandle == IntPtr.Zero)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Event Log '{0}' on computer '{1}' cannot be opened.", new object[]
					{
						logName,
						base.CoreEventLog.MachineName
					}), new System.ComponentModel.Win32Exception());
				}
				return this._readHandle;
			}
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x0003A92C File Offset: 0x00038B2C
		private IntPtr RegisterEventSource()
		{
			IntPtr intPtr = Win32EventLog.PInvoke.RegisterEventSource(base.CoreEventLog.MachineName, base.CoreEventLog.Source);
			if (intPtr == IntPtr.Zero)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Event source '{0}' on computer '{1}' cannot be opened.", new object[]
				{
					base.CoreEventLog.Source,
					base.CoreEventLog.MachineName
				}), new System.ComponentModel.Win32Exception());
			}
			return intPtr;
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x0003A9A4 File Offset: 0x00038BA4
		public override void DisableNotification()
		{
			if (this._notifyResetEvent != null)
			{
				this._notifyResetEvent.Close();
				this._notifyResetEvent = null;
			}
			if (this._notifyThread != null)
			{
				if (this._notifyThread.ThreadState == ThreadState.Running)
				{
					this._notifyThread.Abort();
				}
				this._notifyThread = null;
			}
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x0003A9FC File Offset: 0x00038BFC
		public override void EnableNotification()
		{
			this._notifyResetEvent = new ManualResetEvent(false);
			this._lastEntryWritten = this.OldestEventLogEntry + base.EntryCount;
			if (Win32EventLog.PInvoke.NotifyChangeEventLog(this.ReadHandle, this._notifyResetEvent.Handle) == 0)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Unable to receive notifications for log '{0}' on computer '{1}'.", new object[]
				{
					base.CoreEventLog.GetLogName(),
					base.CoreEventLog.MachineName
				}), new System.ComponentModel.Win32Exception());
			}
			this._notifyThread = new Thread(new ThreadStart(this.NotifyEventThread));
			this._notifyThread.IsBackground = true;
			this._notifyThread.Start();
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x0003AAB0 File Offset: 0x00038CB0
		private void NotifyEventThread()
		{
			for (;;)
			{
				this._notifyResetEvent.WaitOne();
				lock (this)
				{
					if (this._notifying)
					{
						break;
					}
					this._notifying = true;
				}
				try
				{
					int oldestEventLogEntry = this.OldestEventLogEntry;
					if (this._lastEntryWritten < oldestEventLogEntry)
					{
						this._lastEntryWritten = oldestEventLogEntry;
					}
					int num = this._lastEntryWritten - oldestEventLogEntry;
					int num2 = base.EntryCount + oldestEventLogEntry;
					for (int i = num; i < num2 - 1; i++)
					{
						EventLogEntry entry = this.GetEntry(i);
						base.CoreEventLog.OnEntryWritten(entry);
					}
					this._lastEntryWritten = num2;
				}
				finally
				{
					lock (this)
					{
						this._notifying = false;
					}
				}
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060015EA RID: 5610 RVA: 0x0003ABCC File Offset: 0x00038DCC
		public override OverflowAction OverflowAction
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060015EB RID: 5611 RVA: 0x0003ABD4 File Offset: 0x00038DD4
		public override int MinimumRetentionDays
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x060015EC RID: 5612 RVA: 0x0003ABDC File Offset: 0x00038DDC
		// (set) Token: 0x060015ED RID: 5613 RVA: 0x0003ABE4 File Offset: 0x00038DE4
		public override long MaximumKilobytes
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x0003ABEC File Offset: 0x00038DEC
		public override void ModifyOverflowPolicy(OverflowAction action, int retentionDays)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x0003ABF4 File Offset: 0x00038DF4
		public override void RegisterDisplayName(string resourceFile, long resourceId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040006B6 RID: 1718
		private const int MESSAGE_NOT_FOUND = 317;

		// Token: 0x040006B7 RID: 1719
		private ManualResetEvent _notifyResetEvent;

		// Token: 0x040006B8 RID: 1720
		private IntPtr _readHandle;

		// Token: 0x040006B9 RID: 1721
		private Thread _notifyThread;

		// Token: 0x040006BA RID: 1722
		private int _lastEntryWritten;

		// Token: 0x040006BB RID: 1723
		private bool _notifying;

		// Token: 0x02000266 RID: 614
		private class PInvoke
		{
			// Token: 0x060015F1 RID: 5617
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern int ClearEventLog(IntPtr hEventLog, string lpBackupFileName);

			// Token: 0x060015F2 RID: 5618
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern int CloseEventLog(IntPtr hEventLog);

			// Token: 0x060015F3 RID: 5619
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern int DeregisterEventSource(IntPtr hEventLog);

			// Token: 0x060015F4 RID: 5620
			[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			public static extern int FormatMessage(Win32EventLog.FormatMessageFlags dwFlags, IntPtr lpSource, uint dwMessageId, int dwLanguageId, ref IntPtr lpBuffer, int nSize, IntPtr[] arguments);

			// Token: 0x060015F5 RID: 5621
			[DllImport("kernel32.dll", SetLastError = true)]
			public static extern bool FreeLibrary(IntPtr hModule);

			// Token: 0x060015F6 RID: 5622
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern int GetNumberOfEventLogRecords(IntPtr hEventLog, ref int NumberOfRecords);

			// Token: 0x060015F7 RID: 5623
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern int GetOldestEventLogRecord(IntPtr hEventLog, ref int OldestRecord);

			// Token: 0x060015F8 RID: 5624
			[DllImport("kernel32.dll", SetLastError = true)]
			public static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hFile, Win32EventLog.LoadFlags dwFlags);

			// Token: 0x060015F9 RID: 5625
			[DllImport("kernel32.dll", SetLastError = true)]
			public static extern IntPtr LocalFree(IntPtr hMem);

			// Token: 0x060015FA RID: 5626
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern bool LookupAccountSid(string lpSystemName, [MarshalAs(UnmanagedType.LPArray)] byte[] Sid, StringBuilder lpName, ref uint cchName, StringBuilder ReferencedDomainName, ref uint cchReferencedDomainName, out Win32EventLog.SidNameUse peUse);

			// Token: 0x060015FB RID: 5627
			[DllImport("Advapi32.dll", SetLastError = true)]
			public static extern int NotifyChangeEventLog(IntPtr hEventLog, IntPtr hEvent);

			// Token: 0x060015FC RID: 5628
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern IntPtr OpenEventLog(string machineName, string logName);

			// Token: 0x060015FD RID: 5629
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern IntPtr RegisterEventSource(string machineName, string sourceName);

			// Token: 0x060015FE RID: 5630
			[DllImport("Advapi32.dll", SetLastError = true)]
			public static extern int ReportEvent(IntPtr hHandle, ushort wType, ushort wCategory, uint dwEventID, IntPtr sid, ushort wNumStrings, uint dwDataSize, string[] lpStrings, byte[] lpRawData);

			// Token: 0x060015FF RID: 5631
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern int ReadEventLog(IntPtr hEventLog, Win32EventLog.ReadFlags dwReadFlags, int dwRecordOffset, byte[] buffer, int nNumberOfBytesToRead, ref int pnBytesRead, ref int pnMinNumberOfBytesNeeded);

			// Token: 0x040006BC RID: 1724
			public const int ERROR_INSUFFICIENT_BUFFER = 122;

			// Token: 0x040006BD RID: 1725
			public const int ERROR_EVENTLOG_FILE_CHANGED = 1503;
		}

		// Token: 0x02000267 RID: 615
		private enum ReadFlags
		{
			// Token: 0x040006BF RID: 1727
			Sequential = 1,
			// Token: 0x040006C0 RID: 1728
			Seek,
			// Token: 0x040006C1 RID: 1729
			ForwardsRead = 4,
			// Token: 0x040006C2 RID: 1730
			BackwardsRead = 8
		}

		// Token: 0x02000268 RID: 616
		private enum LoadFlags : uint
		{
			// Token: 0x040006C4 RID: 1732
			LibraryAsDataFile = 2U
		}

		// Token: 0x02000269 RID: 617
		[Flags]
		private enum FormatMessageFlags
		{
			// Token: 0x040006C6 RID: 1734
			AllocateBuffer = 256,
			// Token: 0x040006C7 RID: 1735
			IgnoreInserts = 512,
			// Token: 0x040006C8 RID: 1736
			FromHModule = 2048,
			// Token: 0x040006C9 RID: 1737
			FromSystem = 4096,
			// Token: 0x040006CA RID: 1738
			ArgumentArray = 8192
		}

		// Token: 0x0200026A RID: 618
		private enum SidNameUse
		{
			// Token: 0x040006CC RID: 1740
			User = 1,
			// Token: 0x040006CD RID: 1741
			Group,
			// Token: 0x040006CE RID: 1742
			Domain,
			// Token: 0x040006CF RID: 1743
			lias,
			// Token: 0x040006D0 RID: 1744
			WellKnownGroup,
			// Token: 0x040006D1 RID: 1745
			DeletedAccount,
			// Token: 0x040006D2 RID: 1746
			Invalid,
			// Token: 0x040006D3 RID: 1747
			Unknown,
			// Token: 0x040006D4 RID: 1748
			Computer
		}
	}
}
