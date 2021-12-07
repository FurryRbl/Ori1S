using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Security;
using System.Text;
using System.Threading;

namespace System.Diagnostics
{
	// Token: 0x02000231 RID: 561
	internal class LocalFileEventLog : EventLogImpl
	{
		// Token: 0x06001331 RID: 4913 RVA: 0x00033258 File Offset: 0x00031458
		public LocalFileEventLog(EventLog coreEventLog) : base(coreEventLog)
		{
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x00033270 File Offset: 0x00031470
		public override void BeginInit()
		{
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x00033274 File Offset: 0x00031474
		public override void Clear()
		{
			string path = this.FindLogStore(base.CoreEventLog.Log);
			if (!Directory.Exists(path))
			{
				return;
			}
			foreach (string path2 in Directory.GetFiles(path, "*.log"))
			{
				File.Delete(path2);
			}
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x000332CC File Offset: 0x000314CC
		public override void Close()
		{
			if (this.file_watcher != null)
			{
				this.file_watcher.EnableRaisingEvents = false;
				this.file_watcher = null;
			}
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x000332EC File Offset: 0x000314EC
		public override void CreateEventSource(EventSourceCreationData sourceData)
		{
			string text = this.FindLogStore(sourceData.LogName);
			if (!Directory.Exists(text))
			{
				base.ValidateCustomerLogName(sourceData.LogName, sourceData.MachineName);
				Directory.CreateDirectory(text);
				Directory.CreateDirectory(Path.Combine(text, sourceData.LogName));
				if (this.RunningOnUnix)
				{
					LocalFileEventLog.ModifyAccessPermissions(text, "777");
					LocalFileEventLog.ModifyAccessPermissions(text, "+t");
				}
			}
			string path = Path.Combine(text, sourceData.Source);
			Directory.CreateDirectory(path);
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x00033374 File Offset: 0x00031574
		public override void Delete(string logName, string machineName)
		{
			string path = this.FindLogStore(logName);
			if (!Directory.Exists(path))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Event Log '{0}' does not exist on computer '{1}'.", new object[]
				{
					logName,
					machineName
				}));
			}
			Directory.Delete(path, true);
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x000333C0 File Offset: 0x000315C0
		public override void DeleteEventSource(string source, string machineName)
		{
			if (!Directory.Exists(this.EventLogStore))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The source '{0}' is not registered on computer '{1}'.", new object[]
				{
					source,
					machineName
				}));
			}
			string text = this.FindSourceDirectory(source);
			if (text == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The source '{0}' is not registered on computer '{1}'.", new object[]
				{
					source,
					machineName
				}));
			}
			Directory.Delete(text);
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x00033438 File Offset: 0x00031638
		public override void Dispose(bool disposing)
		{
			this.Close();
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x00033440 File Offset: 0x00031640
		public override void DisableNotification()
		{
			if (this.file_watcher == null)
			{
				return;
			}
			this.file_watcher.EnableRaisingEvents = false;
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x0003345C File Offset: 0x0003165C
		public override void EnableNotification()
		{
			if (this.file_watcher == null)
			{
				string path = this.FindLogStore(base.CoreEventLog.Log);
				if (!Directory.Exists(path))
				{
					Directory.CreateDirectory(path);
				}
				this.file_watcher = new System.IO.FileSystemWatcher();
				this.file_watcher.Path = path;
				this.file_watcher.Created += delegate(object o, System.IO.FileSystemEventArgs e)
				{
					lock (this)
					{
						if (this._notifying)
						{
							return;
						}
						this._notifying = true;
					}
					Thread.Sleep(100);
					try
					{
						while (this.GetLatestIndex() > this.last_notification_index)
						{
							try
							{
								base.CoreEventLog.OnEntryWritten(this.GetEntry(this.last_notification_index++));
							}
							catch (Exception ex)
							{
							}
						}
					}
					finally
					{
						lock (this)
						{
							this._notifying = false;
						}
					}
				};
			}
			this.last_notification_index = this.GetLatestIndex();
			this.file_watcher.EnableRaisingEvents = true;
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x000334E0 File Offset: 0x000316E0
		public override void EndInit()
		{
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x000334E4 File Offset: 0x000316E4
		public override bool Exists(string logName, string machineName)
		{
			string path = this.FindLogStore(logName);
			return Directory.Exists(path);
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x00033500 File Offset: 0x00031700
		[MonoTODO("Use MessageTable from PE for lookup")]
		protected override string FormatMessage(string source, uint eventID, string[] replacementStrings)
		{
			return string.Join(", ", replacementStrings);
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x00033510 File Offset: 0x00031710
		protected override int GetEntryCount()
		{
			string path = this.FindLogStore(base.CoreEventLog.Log);
			if (!Directory.Exists(path))
			{
				return 0;
			}
			string[] files = Directory.GetFiles(path, "*.log");
			return files.Length;
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x0003354C File Offset: 0x0003174C
		protected override EventLogEntry GetEntry(int index)
		{
			string path = this.FindLogStore(base.CoreEventLog.Log);
			string path2 = Path.Combine(path, (index + 1).ToString(CultureInfo.InvariantCulture) + ".log");
			EventLogEntry result;
			using (TextReader textReader = File.OpenText(path2))
			{
				int index2 = int.Parse(Path.GetFileNameWithoutExtension(path2), CultureInfo.InvariantCulture);
				uint num = uint.Parse(textReader.ReadLine().Substring(12), CultureInfo.InvariantCulture);
				EventLogEntryType entryType = (EventLogEntryType)((int)Enum.Parse(typeof(EventLogEntryType), textReader.ReadLine().Substring(11)));
				string source = textReader.ReadLine().Substring(8);
				string text = textReader.ReadLine().Substring(10);
				short categoryNumber = short.Parse(text, CultureInfo.InvariantCulture);
				string category = "(" + text + ")";
				DateTime timeGenerated = DateTime.ParseExact(textReader.ReadLine().Substring(15), "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);
				DateTime lastWriteTime = File.GetLastWriteTime(path2);
				int num2 = int.Parse(textReader.ReadLine().Substring(20));
				ArrayList arrayList = new ArrayList();
				StringBuilder stringBuilder = new StringBuilder();
				while (arrayList.Count < num2)
				{
					char c = (char)textReader.Read();
					if (c == '\0')
					{
						arrayList.Add(stringBuilder.ToString());
						stringBuilder.Length = 0;
					}
					else
					{
						stringBuilder.Append(c);
					}
				}
				string[] array = new string[arrayList.Count];
				arrayList.CopyTo(array, 0);
				string message = this.FormatMessage(source, num, array);
				int eventID = EventLog.GetEventID((long)((ulong)num));
				byte[] data = Convert.FromBase64String(textReader.ReadToEnd());
				result = new EventLogEntry(category, categoryNumber, index2, eventID, source, message, null, Environment.MachineName, entryType, timeGenerated, lastWriteTime, data, array, (long)((ulong)num));
			}
			return result;
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x00033748 File Offset: 0x00031948
		[MonoTODO]
		protected override string GetLogDisplayName()
		{
			return base.CoreEventLog.Log;
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x00033758 File Offset: 0x00031958
		protected override string[] GetLogNames(string machineName)
		{
			if (!Directory.Exists(this.EventLogStore))
			{
				return new string[0];
			}
			string[] directories = Directory.GetDirectories(this.EventLogStore, "*");
			string[] array = new string[directories.Length];
			for (int i = 0; i < directories.Length; i++)
			{
				array[i] = Path.GetFileName(directories[i]);
			}
			return array;
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x000337B8 File Offset: 0x000319B8
		public override string LogNameFromSourceName(string source, string machineName)
		{
			if (!Directory.Exists(this.EventLogStore))
			{
				return string.Empty;
			}
			string text = this.FindSourceDirectory(source);
			if (text == null)
			{
				return string.Empty;
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(text);
			return directoryInfo.Parent.Name;
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x00033804 File Offset: 0x00031A04
		public override bool SourceExists(string source, string machineName)
		{
			if (!Directory.Exists(this.EventLogStore))
			{
				return false;
			}
			string text = this.FindSourceDirectory(source);
			return text != null;
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x00033834 File Offset: 0x00031A34
		public override void WriteEntry(string[] replacementStrings, EventLogEntryType type, uint instanceID, short category, byte[] rawData)
		{
			object obj = LocalFileEventLog.lockObject;
			lock (obj)
			{
				string path = this.FindLogStore(base.CoreEventLog.Log);
				string path2 = Path.Combine(path, (this.GetLatestIndex() + 1).ToString(CultureInfo.InvariantCulture) + ".log");
				try
				{
					using (TextWriter textWriter = File.CreateText(path2))
					{
						textWriter.WriteLine("InstanceID: {0}", instanceID.ToString(CultureInfo.InvariantCulture));
						textWriter.WriteLine("EntryType: {0}", (int)type);
						textWriter.WriteLine("Source: {0}", base.CoreEventLog.Source);
						textWriter.WriteLine("Category: {0}", category.ToString(CultureInfo.InvariantCulture));
						textWriter.WriteLine("TimeGenerated: {0}", DateTime.Now.ToString("yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
						textWriter.WriteLine("ReplacementStrings: {0}", replacementStrings.Length.ToString(CultureInfo.InvariantCulture));
						StringBuilder stringBuilder = new StringBuilder();
						foreach (string value in replacementStrings)
						{
							stringBuilder.Append(value);
							stringBuilder.Append('\0');
						}
						textWriter.Write(stringBuilder.ToString());
						textWriter.Write(Convert.ToBase64String(rawData));
					}
				}
				catch (IOException)
				{
					File.Delete(path2);
				}
			}
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x000339F8 File Offset: 0x00031BF8
		private string FindSourceDirectory(string source)
		{
			string result = null;
			string[] directories = Directory.GetDirectories(this.EventLogStore, "*");
			for (int i = 0; i < directories.Length; i++)
			{
				string[] directories2 = Directory.GetDirectories(directories[i], "*");
				for (int j = 0; j < directories2.Length; j++)
				{
					string fileName = Path.GetFileName(directories2[j]);
					if (string.Compare(fileName, source, true, CultureInfo.InvariantCulture) == 0)
					{
						result = directories2[j];
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001347 RID: 4935 RVA: 0x00033A7C File Offset: 0x00031C7C
		private bool RunningOnUnix
		{
			get
			{
				int platform = (int)Environment.OSVersion.Platform;
				return platform == 4 || platform == 128 || platform == 6;
			}
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x00033AB0 File Offset: 0x00031CB0
		private string FindLogStore(string logName)
		{
			if (!Directory.Exists(this.EventLogStore))
			{
				return Path.Combine(this.EventLogStore, logName);
			}
			string[] directories = Directory.GetDirectories(this.EventLogStore, "*");
			for (int i = 0; i < directories.Length; i++)
			{
				string fileName = Path.GetFileName(directories[i]);
				if (string.Compare(fileName, logName, true, CultureInfo.InvariantCulture) == 0)
				{
					return directories[i];
				}
			}
			return Path.Combine(this.EventLogStore, logName);
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001349 RID: 4937 RVA: 0x00033B2C File Offset: 0x00031D2C
		private string EventLogStore
		{
			get
			{
				string environmentVariable = Environment.GetEnvironmentVariable("MONO_EVENTLOG_TYPE");
				if (environmentVariable != null && environmentVariable.Length > "local".Length + 1)
				{
					return environmentVariable.Substring("local".Length + 1);
				}
				if (this.RunningOnUnix)
				{
					return "/var/lib/mono/eventlog";
				}
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "mono\\eventlog");
			}
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x00033B98 File Offset: 0x00031D98
		private int GetLatestIndex()
		{
			int num = 0;
			string[] files = Directory.GetFiles(this.FindLogStore(base.CoreEventLog.Log), "*.log");
			for (int i = 0; i < files.Length; i++)
			{
				try
				{
					string path = files[i];
					int num2 = int.Parse(Path.GetFileNameWithoutExtension(path), CultureInfo.InvariantCulture);
					if (num2 > num)
					{
						num = num2;
					}
				}
				catch
				{
				}
			}
			return num;
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x00033C20 File Offset: 0x00031E20
		private static void ModifyAccessPermissions(string path, string permissions)
		{
			ProcessStartInfo processStartInfo = new ProcessStartInfo();
			processStartInfo.FileName = "chmod";
			processStartInfo.RedirectStandardOutput = true;
			processStartInfo.RedirectStandardError = true;
			processStartInfo.UseShellExecute = false;
			processStartInfo.Arguments = string.Format("{0} \"{1}\"", permissions, path);
			Process process = null;
			try
			{
				process = Process.Start(processStartInfo);
			}
			catch (Exception inner)
			{
				throw new SecurityException("Access permissions could not be modified.", inner);
			}
			process.WaitForExit();
			if (process.ExitCode != 0)
			{
				process.Close();
				throw new SecurityException("Access permissions could not be modified.");
			}
			process.Close();
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x0600134C RID: 4940 RVA: 0x00033CCC File Offset: 0x00031ECC
		public override OverflowAction OverflowAction
		{
			get
			{
				return OverflowAction.DoNotOverwrite;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x0600134D RID: 4941 RVA: 0x00033CD0 File Offset: 0x00031ED0
		public override int MinimumRetentionDays
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x0600134E RID: 4942 RVA: 0x00033CD8 File Offset: 0x00031ED8
		// (set) Token: 0x0600134F RID: 4943 RVA: 0x00033CE4 File Offset: 0x00031EE4
		public override long MaximumKilobytes
		{
			get
			{
				return long.MaxValue;
			}
			set
			{
				throw new NotSupportedException("This EventLog implementation does not support setting max kilobytes policy");
			}
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x00033CF0 File Offset: 0x00031EF0
		public override void ModifyOverflowPolicy(OverflowAction action, int retentionDays)
		{
			throw new NotSupportedException("This EventLog implementation does not support modifying overflow policy");
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x00033CFC File Offset: 0x00031EFC
		public override void RegisterDisplayName(string resourceFile, long resourceId)
		{
			throw new NotSupportedException("This EventLog implementation does not support registering display name");
		}

		// Token: 0x0400058C RID: 1420
		private const string DateFormat = "yyyyMMddHHmmssfff";

		// Token: 0x0400058D RID: 1421
		private static readonly object lockObject = new object();

		// Token: 0x0400058E RID: 1422
		private System.IO.FileSystemWatcher file_watcher;

		// Token: 0x0400058F RID: 1423
		private int last_notification_index;

		// Token: 0x04000590 RID: 1424
		private bool _notifying;
	}
}
