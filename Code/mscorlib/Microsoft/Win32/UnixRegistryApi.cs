using System;
using System.Globalization;
using System.IO;

namespace Microsoft.Win32
{
	// Token: 0x0200006F RID: 111
	internal class UnixRegistryApi : IRegistryApi
	{
		// Token: 0x0600073D RID: 1853 RVA: 0x00016EF0 File Offset: 0x000150F0
		private static string ToUnix(string keyname)
		{
			if (keyname.IndexOf('\\') != -1)
			{
				keyname = keyname.Replace('\\', '/');
			}
			return keyname.ToLower();
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00016F20 File Offset: 0x00015120
		private static bool IsWellKnownKey(string parentKeyName, string keyname)
		{
			return (parentKeyName == Registry.CurrentUser.Name || parentKeyName == Registry.LocalMachine.Name) && 0 == string.Compare("software", keyname, true, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x00016F70 File Offset: 0x00015170
		public RegistryKey CreateSubKey(RegistryKey rkey, string keyname)
		{
			return this.CreateSubKey(rkey, keyname, true);
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x00016F7C File Offset: 0x0001517C
		public RegistryKey OpenRemoteBaseKey(RegistryHive hKey, string machineName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x00016F84 File Offset: 0x00015184
		public RegistryKey OpenSubKey(RegistryKey rkey, string keyname, bool writable)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, true);
			if (keyHandler == null)
			{
				return null;
			}
			RegistryKey registryKey = keyHandler.Probe(rkey, UnixRegistryApi.ToUnix(keyname), writable);
			if (registryKey == null && UnixRegistryApi.IsWellKnownKey(rkey.Name, keyname))
			{
				registryKey = this.CreateSubKey(rkey, keyname, writable);
			}
			return registryKey;
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00016FD4 File Offset: 0x000151D4
		public void Flush(RegistryKey rkey)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, false);
			if (keyHandler == null)
			{
				return;
			}
			keyHandler.Flush();
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00016FF8 File Offset: 0x000151F8
		public void Close(RegistryKey rkey)
		{
			KeyHandler.Drop(rkey);
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00017000 File Offset: 0x00015200
		public object GetValue(RegistryKey rkey, string name, object default_value, RegistryValueOptions options)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, true);
			if (keyHandler == null)
			{
				return default_value;
			}
			if (keyHandler.ValueExists(name))
			{
				return keyHandler.GetValue(name, options);
			}
			return default_value;
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x00017034 File Offset: 0x00015234
		public void SetValue(RegistryKey rkey, string name, object value)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, true);
			if (keyHandler == null)
			{
				throw RegistryKey.CreateMarkedForDeletionException();
			}
			keyHandler.SetValue(name, value);
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x00017060 File Offset: 0x00015260
		public void SetValue(RegistryKey rkey, string name, object value, RegistryValueKind valueKind)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, true);
			if (keyHandler == null)
			{
				throw RegistryKey.CreateMarkedForDeletionException();
			}
			keyHandler.SetValue(name, value, valueKind);
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0001708C File Offset: 0x0001528C
		public int SubKeyCount(RegistryKey rkey)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, true);
			if (keyHandler == null)
			{
				throw RegistryKey.CreateMarkedForDeletionException();
			}
			return Directory.GetDirectories(keyHandler.Dir).Length;
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x000170BC File Offset: 0x000152BC
		public int ValueCount(RegistryKey rkey)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, true);
			if (keyHandler == null)
			{
				throw RegistryKey.CreateMarkedForDeletionException();
			}
			return keyHandler.ValueCount;
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x000170E4 File Offset: 0x000152E4
		public void DeleteValue(RegistryKey rkey, string name, bool throw_if_missing)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, true);
			if (keyHandler == null)
			{
				return;
			}
			if (throw_if_missing && !keyHandler.ValueExists(name))
			{
				throw new ArgumentException("the given value does not exist");
			}
			keyHandler.RemoveValue(name);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00017124 File Offset: 0x00015324
		public void DeleteKey(RegistryKey rkey, string keyname, bool throw_if_missing)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, true);
			if (keyHandler != null)
			{
				string text = Path.Combine(keyHandler.Dir, UnixRegistryApi.ToUnix(keyname));
				if (Directory.Exists(text))
				{
					Directory.Delete(text, true);
					KeyHandler.Drop(text);
				}
				else if (throw_if_missing)
				{
					throw new ArgumentException("the given value does not exist");
				}
				return;
			}
			if (!throw_if_missing)
			{
				return;
			}
			throw new ArgumentException("the given value does not exist");
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00017194 File Offset: 0x00015394
		public string[] GetSubKeyNames(RegistryKey rkey)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, true);
			DirectoryInfo directoryInfo = new DirectoryInfo(keyHandler.Dir);
			DirectoryInfo[] directories = directoryInfo.GetDirectories();
			string[] array = new string[directories.Length];
			for (int i = 0; i < directories.Length; i++)
			{
				DirectoryInfo directoryInfo2 = directories[i];
				array[i] = directoryInfo2.Name;
			}
			return array;
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x000171F0 File Offset: 0x000153F0
		public string[] GetValueNames(RegistryKey rkey)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, true);
			if (keyHandler == null)
			{
				throw RegistryKey.CreateMarkedForDeletionException();
			}
			return keyHandler.GetValueNames();
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00017218 File Offset: 0x00015418
		public string ToString(RegistryKey rkey)
		{
			return rkey.Name;
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x00017220 File Offset: 0x00015420
		private RegistryKey CreateSubKey(RegistryKey rkey, string keyname, bool writable)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, true);
			if (keyHandler == null)
			{
				throw RegistryKey.CreateMarkedForDeletionException();
			}
			return keyHandler.Ensure(rkey, UnixRegistryApi.ToUnix(keyname), writable);
		}
	}
}
