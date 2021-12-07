using System;
using System.Collections;
using System.IO;
using System.Security;
using System.Threading;

namespace Microsoft.Win32
{
	// Token: 0x0200006E RID: 110
	internal class KeyHandler
	{
		// Token: 0x06000722 RID: 1826 RVA: 0x00015E00 File Offset: 0x00014000
		private KeyHandler(RegistryKey rkey, string basedir)
		{
			if (!Directory.Exists(basedir))
			{
				try
				{
					Directory.CreateDirectory(basedir);
				}
				catch (UnauthorizedAccessException)
				{
					throw new SecurityException("No access to the given key");
				}
			}
			this.Dir = basedir;
			this.file = Path.Combine(this.Dir, "values.xml");
			this.Load();
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00015E9C File Offset: 0x0001409C
		public void Load()
		{
			this.values = new Hashtable();
			if (!File.Exists(this.file))
			{
				return;
			}
			try
			{
				using (FileStream fileStream = File.OpenRead(this.file))
				{
					StreamReader streamReader = new StreamReader(fileStream);
					string text = streamReader.ReadToEnd();
					if (text.Length != 0)
					{
						SecurityElement securityElement = SecurityElement.FromString(text);
						if (securityElement.Tag == "values" && securityElement.Children != null)
						{
							foreach (object obj in securityElement.Children)
							{
								SecurityElement securityElement2 = (SecurityElement)obj;
								if (securityElement2.Tag == "value")
								{
									this.LoadKey(securityElement2);
								}
							}
						}
					}
				}
			}
			catch (UnauthorizedAccessException)
			{
				this.values.Clear();
				throw new SecurityException("No access to the given key");
			}
			catch (Exception arg)
			{
				Console.Error.WriteLine("While loading registry key at {0}: {1}", this.file, arg);
				this.values.Clear();
			}
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0001603C File Offset: 0x0001423C
		private void LoadKey(SecurityElement se)
		{
			Hashtable attributes = se.Attributes;
			try
			{
				string text = (string)attributes["name"];
				if (text != null)
				{
					string text2 = (string)attributes["type"];
					if (text2 != null)
					{
						string text3 = text2;
						switch (text3)
						{
						case "int":
							this.values[text] = int.Parse(se.Text);
							break;
						case "bytearray":
							this.values[text] = Convert.FromBase64String(se.Text);
							break;
						case "string":
							this.values[text] = se.Text;
							break;
						case "expand":
							this.values[text] = new ExpandString(se.Text);
							break;
						case "qword":
							this.values[text] = long.Parse(se.Text);
							break;
						case "string-array":
						{
							ArrayList arrayList = new ArrayList();
							if (se.Children != null)
							{
								foreach (object obj in se.Children)
								{
									SecurityElement securityElement = (SecurityElement)obj;
									arrayList.Add(securityElement.Text);
								}
							}
							this.values[text] = arrayList.ToArray(typeof(string));
							break;
						}
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00016288 File Offset: 0x00014488
		public RegistryKey Ensure(RegistryKey rkey, string extra, bool writable)
		{
			Type typeFromHandle = typeof(KeyHandler);
			RegistryKey result;
			lock (typeFromHandle)
			{
				string text = Path.Combine(this.Dir, extra);
				KeyHandler keyHandler = (KeyHandler)KeyHandler.dir_to_handler[text];
				if (keyHandler == null)
				{
					keyHandler = new KeyHandler(rkey, text);
				}
				RegistryKey registryKey = new RegistryKey(keyHandler, KeyHandler.CombineName(rkey, extra), writable);
				KeyHandler.key_to_handler[registryKey] = keyHandler;
				KeyHandler.dir_to_handler[text] = keyHandler;
				result = registryKey;
			}
			return result;
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0001632C File Offset: 0x0001452C
		public RegistryKey Probe(RegistryKey rkey, string extra, bool writable)
		{
			RegistryKey registryKey = null;
			Type typeFromHandle = typeof(KeyHandler);
			RegistryKey result;
			lock (typeFromHandle)
			{
				string text = Path.Combine(this.Dir, extra);
				KeyHandler keyHandler = (KeyHandler)KeyHandler.dir_to_handler[text];
				if (keyHandler != null)
				{
					registryKey = new RegistryKey(keyHandler, KeyHandler.CombineName(rkey, extra), writable);
					KeyHandler.key_to_handler[registryKey] = keyHandler;
				}
				else if (Directory.Exists(text))
				{
					keyHandler = new KeyHandler(rkey, text);
					registryKey = new RegistryKey(keyHandler, KeyHandler.CombineName(rkey, extra), writable);
					KeyHandler.dir_to_handler[text] = keyHandler;
					KeyHandler.key_to_handler[registryKey] = keyHandler;
				}
				result = registryKey;
			}
			return result;
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x000163FC File Offset: 0x000145FC
		private static string CombineName(RegistryKey rkey, string extra)
		{
			if (extra.IndexOf('/') != -1)
			{
				extra = extra.Replace('/', '\\');
			}
			return rkey.Name + "\\" + extra;
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00016434 File Offset: 0x00014634
		public static KeyHandler Lookup(RegistryKey rkey, bool createNonExisting)
		{
			Type typeFromHandle = typeof(KeyHandler);
			KeyHandler result;
			lock (typeFromHandle)
			{
				KeyHandler keyHandler = (KeyHandler)KeyHandler.key_to_handler[rkey];
				if (keyHandler != null)
				{
					result = keyHandler;
				}
				else if (!rkey.IsRoot || !createNonExisting)
				{
					result = null;
				}
				else
				{
					RegistryHive hive = rkey.Hive;
					RegistryHive registryHive = hive;
					switch (registryHive + -2147483648)
					{
					case (RegistryHive)0:
					case (RegistryHive)2:
					case (RegistryHive)3:
					case (RegistryHive)4:
					case (RegistryHive)5:
					case (RegistryHive)6:
					{
						string text = Path.Combine(KeyHandler.MachineStore, hive.ToString());
						keyHandler = new KeyHandler(rkey, text);
						KeyHandler.dir_to_handler[text] = keyHandler;
						break;
					}
					case (RegistryHive)1:
					{
						string text2 = Path.Combine(KeyHandler.UserStore, hive.ToString());
						keyHandler = new KeyHandler(rkey, text2);
						KeyHandler.dir_to_handler[text2] = keyHandler;
						break;
					}
					default:
						throw new Exception("Unknown RegistryHive");
					}
					KeyHandler.key_to_handler[rkey] = keyHandler;
					result = keyHandler;
				}
			}
			return result;
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0001656C File Offset: 0x0001476C
		public static void Drop(RegistryKey rkey)
		{
			Type typeFromHandle = typeof(KeyHandler);
			lock (typeFromHandle)
			{
				KeyHandler keyHandler = (KeyHandler)KeyHandler.key_to_handler[rkey];
				if (keyHandler != null)
				{
					KeyHandler.key_to_handler.Remove(rkey);
					int num = 0;
					foreach (object obj in KeyHandler.key_to_handler)
					{
						if (((DictionaryEntry)obj).Value == keyHandler)
						{
							num++;
						}
					}
					if (num == 0)
					{
						KeyHandler.dir_to_handler.Remove(keyHandler.Dir);
					}
				}
			}
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00016660 File Offset: 0x00014860
		public static void Drop(string dir)
		{
			Type typeFromHandle = typeof(KeyHandler);
			lock (typeFromHandle)
			{
				KeyHandler keyHandler = (KeyHandler)KeyHandler.dir_to_handler[dir];
				if (keyHandler != null)
				{
					KeyHandler.dir_to_handler.Remove(dir);
					ArrayList arrayList = new ArrayList();
					foreach (object obj in KeyHandler.key_to_handler)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						if (dictionaryEntry.Value == keyHandler)
						{
							arrayList.Add(dictionaryEntry.Key);
						}
					}
					foreach (object key in arrayList)
					{
						KeyHandler.key_to_handler.Remove(key);
					}
				}
			}
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x000167AC File Offset: 0x000149AC
		public object GetValue(string name, RegistryValueOptions options)
		{
			if (this.IsMarkedForDeletion)
			{
				return null;
			}
			if (name == null)
			{
				name = string.Empty;
			}
			object obj = this.values[name];
			ExpandString expandString = obj as ExpandString;
			if (expandString == null)
			{
				return obj;
			}
			if ((options & RegistryValueOptions.DoNotExpandEnvironmentNames) == RegistryValueOptions.None)
			{
				return expandString.Expand();
			}
			return expandString.ToString();
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00016804 File Offset: 0x00014A04
		public void SetValue(string name, object value)
		{
			this.AssertNotMarkedForDeletion();
			if (name == null)
			{
				name = string.Empty;
			}
			if (value is int || value is string || value is byte[] || value is string[])
			{
				this.values[name] = value;
			}
			else
			{
				this.values[name] = value.ToString();
			}
			this.SetDirty();
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0001687C File Offset: 0x00014A7C
		public string[] GetValueNames()
		{
			this.AssertNotMarkedForDeletion();
			ICollection keys = this.values.Keys;
			string[] array = new string[keys.Count];
			keys.CopyTo(array, 0);
			return array;
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x000168B0 File Offset: 0x00014AB0
		public void SetValue(string name, object value, RegistryValueKind valueKind)
		{
			this.SetDirty();
			if (name == null)
			{
				name = string.Empty;
			}
			switch (valueKind)
			{
			case RegistryValueKind.String:
				if (value is string)
				{
					this.values[name] = value;
					return;
				}
				goto IL_186;
			case RegistryValueKind.ExpandString:
				if (value is string)
				{
					this.values[name] = new ExpandString((string)value);
					return;
				}
				goto IL_186;
			case RegistryValueKind.Binary:
				if (value is byte[])
				{
					this.values[name] = value;
					return;
				}
				goto IL_186;
			case RegistryValueKind.DWord:
				if (value is long && (long)value < 2147483647L && (long)value > -2147483648L)
				{
					this.values[name] = (int)((long)value);
					return;
				}
				if (value is int)
				{
					this.values[name] = value;
					return;
				}
				goto IL_186;
			case RegistryValueKind.MultiString:
				if (value is string[])
				{
					this.values[name] = value;
					return;
				}
				goto IL_186;
			case RegistryValueKind.QWord:
				if (value is int)
				{
					this.values[name] = (long)((int)value);
					return;
				}
				if (value is long)
				{
					this.values[name] = value;
					return;
				}
				goto IL_186;
			}
			throw new ArgumentException("unknown value", "valueKind");
			IL_186:
			throw new ArgumentException("Value could not be converted to specified type", "valueKind");
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00016A54 File Offset: 0x00014C54
		private void SetDirty()
		{
			Type typeFromHandle = typeof(KeyHandler);
			lock (typeFromHandle)
			{
				if (!this.dirty)
				{
					this.dirty = true;
					new Timer(new TimerCallback(this.DirtyTimeout), null, 3000, -1);
				}
			}
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00016ACC File Offset: 0x00014CCC
		public void DirtyTimeout(object state)
		{
			this.Flush();
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x00016AD4 File Offset: 0x00014CD4
		public void Flush()
		{
			Type typeFromHandle = typeof(KeyHandler);
			lock (typeFromHandle)
			{
				if (this.dirty)
				{
					this.Save();
					this.dirty = false;
				}
			}
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00016B34 File Offset: 0x00014D34
		public bool ValueExists(string name)
		{
			if (name == null)
			{
				name = string.Empty;
			}
			return this.values.Contains(name);
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x00016B50 File Offset: 0x00014D50
		public int ValueCount
		{
			get
			{
				return this.values.Keys.Count;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x00016B64 File Offset: 0x00014D64
		public bool IsMarkedForDeletion
		{
			get
			{
				return !KeyHandler.dir_to_handler.Contains(this.Dir);
			}
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x00016B7C File Offset: 0x00014D7C
		public void RemoveValue(string name)
		{
			this.AssertNotMarkedForDeletion();
			this.values.Remove(name);
			this.SetDirty();
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x00016BA4 File Offset: 0x00014DA4
		~KeyHandler()
		{
			this.Flush();
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00016BE0 File Offset: 0x00014DE0
		private void Save()
		{
			if (this.IsMarkedForDeletion)
			{
				return;
			}
			if (!File.Exists(this.file) && this.values.Count == 0)
			{
				return;
			}
			SecurityElement securityElement = new SecurityElement("values");
			foreach (object obj in this.values)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				object value = dictionaryEntry.Value;
				SecurityElement securityElement2 = new SecurityElement("value");
				securityElement2.AddAttribute("name", SecurityElement.Escape((string)dictionaryEntry.Key));
				if (value is string)
				{
					securityElement2.AddAttribute("type", "string");
					securityElement2.Text = SecurityElement.Escape((string)value);
				}
				else if (value is int)
				{
					securityElement2.AddAttribute("type", "int");
					securityElement2.Text = value.ToString();
				}
				else if (value is long)
				{
					securityElement2.AddAttribute("type", "qword");
					securityElement2.Text = value.ToString();
				}
				else if (value is byte[])
				{
					securityElement2.AddAttribute("type", "bytearray");
					securityElement2.Text = Convert.ToBase64String((byte[])value);
				}
				else if (value is ExpandString)
				{
					securityElement2.AddAttribute("type", "expand");
					securityElement2.Text = SecurityElement.Escape(value.ToString());
				}
				else if (value is string[])
				{
					securityElement2.AddAttribute("type", "string-array");
					foreach (string str in (string[])value)
					{
						securityElement2.AddChild(new SecurityElement("string")
						{
							Text = SecurityElement.Escape(str)
						});
					}
				}
				securityElement.AddChild(securityElement2);
			}
			using (FileStream fileStream = File.Create(this.file))
			{
				StreamWriter streamWriter = new StreamWriter(fileStream);
				streamWriter.Write(securityElement.ToString());
				streamWriter.Flush();
			}
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x00016E70 File Offset: 0x00015070
		private void AssertNotMarkedForDeletion()
		{
			if (this.IsMarkedForDeletion)
			{
				throw RegistryKey.CreateMarkedForDeletionException();
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x00016E84 File Offset: 0x00015084
		private static string UserStore
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), ".mono/registry");
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x00016E98 File Offset: 0x00015098
		private static string MachineStore
		{
			get
			{
				string text = Environment.GetEnvironmentVariable("MONO_REGISTRY_PATH");
				if (text != null)
				{
					return text;
				}
				text = Environment.GetMachineConfigPath();
				int num = text.IndexOf("machine.config");
				return Path.Combine(Path.Combine(text.Substring(0, num - 1), ".."), "registry");
			}
		}

		// Token: 0x04000101 RID: 257
		private static Hashtable key_to_handler = new Hashtable();

		// Token: 0x04000102 RID: 258
		private static Hashtable dir_to_handler = new Hashtable(new CaseInsensitiveHashCodeProvider(), new CaseInsensitiveComparer());

		// Token: 0x04000103 RID: 259
		public string Dir;

		// Token: 0x04000104 RID: 260
		private Hashtable values;

		// Token: 0x04000105 RID: 261
		private string file;

		// Token: 0x04000106 RID: 262
		private bool dirty;
	}
}
