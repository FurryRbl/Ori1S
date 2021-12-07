using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Microsoft.Win32
{
	// Token: 0x02000070 RID: 112
	internal class Win32RegistryApi : IRegistryApi
	{
		// Token: 0x06000750 RID: 1872
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegCreateKey(IntPtr keyBase, string keyName, out IntPtr keyHandle);

		// Token: 0x06000751 RID: 1873
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegCloseKey(IntPtr keyHandle);

		// Token: 0x06000752 RID: 1874
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegConnectRegistry(string machineName, IntPtr hKey, out IntPtr keyHandle);

		// Token: 0x06000753 RID: 1875
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegFlushKey(IntPtr keyHandle);

		// Token: 0x06000754 RID: 1876
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegOpenKeyEx(IntPtr keyBase, string keyName, IntPtr reserved, int access, out IntPtr keyHandle);

		// Token: 0x06000755 RID: 1877
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegDeleteKey(IntPtr keyHandle, string valueName);

		// Token: 0x06000756 RID: 1878
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegDeleteValue(IntPtr keyHandle, string valueName);

		// Token: 0x06000757 RID: 1879
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegEnumKey(IntPtr keyBase, int index, StringBuilder nameBuffer, int bufferLength);

		// Token: 0x06000758 RID: 1880
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegEnumValue(IntPtr keyBase, int index, StringBuilder nameBuffer, ref int nameLength, IntPtr reserved, ref RegistryValueKind type, IntPtr data, IntPtr dataLength);

		// Token: 0x06000759 RID: 1881
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegSetValueEx(IntPtr keyBase, string valueName, IntPtr reserved, RegistryValueKind type, string data, int rawDataLength);

		// Token: 0x0600075A RID: 1882
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegSetValueEx(IntPtr keyBase, string valueName, IntPtr reserved, RegistryValueKind type, byte[] rawData, int rawDataLength);

		// Token: 0x0600075B RID: 1883
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegSetValueEx(IntPtr keyBase, string valueName, IntPtr reserved, RegistryValueKind type, ref int data, int rawDataLength);

		// Token: 0x0600075C RID: 1884
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegQueryValueEx(IntPtr keyBase, string valueName, IntPtr reserved, ref RegistryValueKind type, IntPtr zero, ref int dataSize);

		// Token: 0x0600075D RID: 1885
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegQueryValueEx(IntPtr keyBase, string valueName, IntPtr reserved, ref RegistryValueKind type, [Out] byte[] data, ref int dataSize);

		// Token: 0x0600075E RID: 1886
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegQueryValueEx(IntPtr keyBase, string valueName, IntPtr reserved, ref RegistryValueKind type, ref int data, ref int dataSize);

		// Token: 0x0600075F RID: 1887 RVA: 0x00017264 File Offset: 0x00015464
		private static IntPtr GetHandle(RegistryKey key)
		{
			return (IntPtr)key.Handle;
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00017274 File Offset: 0x00015474
		private static bool IsHandleValid(RegistryKey key)
		{
			return key.Handle != null;
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x00017284 File Offset: 0x00015484
		public object GetValue(RegistryKey rkey, string name, object defaultValue, RegistryValueOptions options)
		{
			RegistryValueKind registryValueKind = RegistryValueKind.Unknown;
			int size = 0;
			IntPtr handle = Win32RegistryApi.GetHandle(rkey);
			int num = Win32RegistryApi.RegQueryValueEx(handle, name, IntPtr.Zero, ref registryValueKind, IntPtr.Zero, ref size);
			if (num == 2 || num == 1018)
			{
				return defaultValue;
			}
			if (num != 234 && num != 0)
			{
				this.GenerateException(num);
			}
			object obj;
			if (registryValueKind == RegistryValueKind.String)
			{
				byte[] data;
				num = this.GetBinaryValue(rkey, name, registryValueKind, out data, size);
				obj = RegistryKey.DecodeString(data);
			}
			else if (registryValueKind == RegistryValueKind.ExpandString)
			{
				byte[] data2;
				num = this.GetBinaryValue(rkey, name, registryValueKind, out data2, size);
				obj = RegistryKey.DecodeString(data2);
				if ((options & RegistryValueOptions.DoNotExpandEnvironmentNames) == RegistryValueOptions.None)
				{
					obj = Environment.ExpandEnvironmentVariables((string)obj);
				}
			}
			else if (registryValueKind == RegistryValueKind.DWord)
			{
				int num2 = 0;
				num = Win32RegistryApi.RegQueryValueEx(handle, name, IntPtr.Zero, ref registryValueKind, ref num2, ref size);
				obj = num2;
			}
			else if (registryValueKind == RegistryValueKind.Binary)
			{
				byte[] array;
				num = this.GetBinaryValue(rkey, name, registryValueKind, out array, size);
				obj = array;
			}
			else
			{
				if (registryValueKind != RegistryValueKind.MultiString)
				{
					throw new SystemException();
				}
				obj = null;
				byte[] data3;
				num = this.GetBinaryValue(rkey, name, registryValueKind, out data3, size);
				if (num == 0)
				{
					obj = RegistryKey.DecodeString(data3).Split(new char[1]);
				}
			}
			if (num != 0)
			{
				this.GenerateException(num);
			}
			return obj;
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x000173D4 File Offset: 0x000155D4
		public void SetValue(RegistryKey rkey, string name, object value, RegistryValueKind valueKind)
		{
			Type type = value.GetType();
			IntPtr handle = Win32RegistryApi.GetHandle(rkey);
			int num2;
			if (valueKind == RegistryValueKind.DWord && type == typeof(int))
			{
				int num = (int)value;
				num2 = Win32RegistryApi.RegSetValueEx(handle, name, IntPtr.Zero, RegistryValueKind.DWord, ref num, 4);
			}
			else if (valueKind == RegistryValueKind.Binary && type == typeof(byte[]))
			{
				byte[] array = (byte[])value;
				num2 = Win32RegistryApi.RegSetValueEx(handle, name, IntPtr.Zero, RegistryValueKind.Binary, array, array.Length);
			}
			else if (valueKind == RegistryValueKind.MultiString && type == typeof(string[]))
			{
				string[] array2 = (string[])value;
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string value2 in array2)
				{
					stringBuilder.Append(value2);
					stringBuilder.Append('\0');
				}
				stringBuilder.Append('\0');
				byte[] bytes = Encoding.Unicode.GetBytes(stringBuilder.ToString());
				num2 = Win32RegistryApi.RegSetValueEx(handle, name, IntPtr.Zero, RegistryValueKind.MultiString, bytes, bytes.Length);
			}
			else if ((valueKind == RegistryValueKind.String || valueKind == RegistryValueKind.ExpandString) && type == typeof(string))
			{
				string text = string.Format("{0}{1}", value, '\0');
				num2 = Win32RegistryApi.RegSetValueEx(handle, name, IntPtr.Zero, valueKind, text, text.Length * this.NativeBytesPerCharacter);
			}
			else
			{
				if (type.IsArray)
				{
					throw new ArgumentException("Only string and byte arrays can written as registry values");
				}
				throw new ArgumentException("Type does not match the valueKind");
			}
			if (num2 != 0)
			{
				this.GenerateException(num2);
			}
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x00017578 File Offset: 0x00015778
		public void SetValue(RegistryKey rkey, string name, object value)
		{
			Type type = value.GetType();
			IntPtr handle = Win32RegistryApi.GetHandle(rkey);
			int num2;
			if (type == typeof(int))
			{
				int num = (int)value;
				num2 = Win32RegistryApi.RegSetValueEx(handle, name, IntPtr.Zero, RegistryValueKind.DWord, ref num, 4);
			}
			else if (type == typeof(byte[]))
			{
				byte[] array = (byte[])value;
				num2 = Win32RegistryApi.RegSetValueEx(handle, name, IntPtr.Zero, RegistryValueKind.Binary, array, array.Length);
			}
			else if (type == typeof(string[]))
			{
				string[] array2 = (string[])value;
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string value2 in array2)
				{
					stringBuilder.Append(value2);
					stringBuilder.Append('\0');
				}
				stringBuilder.Append('\0');
				byte[] bytes = Encoding.Unicode.GetBytes(stringBuilder.ToString());
				num2 = Win32RegistryApi.RegSetValueEx(handle, name, IntPtr.Zero, RegistryValueKind.MultiString, bytes, bytes.Length);
			}
			else
			{
				if (type.IsArray)
				{
					throw new ArgumentException("Only string and byte arrays can written as registry values");
				}
				string text = string.Format("{0}{1}", value, '\0');
				num2 = Win32RegistryApi.RegSetValueEx(handle, name, IntPtr.Zero, RegistryValueKind.String, text, text.Length * this.NativeBytesPerCharacter);
			}
			if (num2 == 1018)
			{
				throw RegistryKey.CreateMarkedForDeletionException();
			}
			if (num2 != 0)
			{
				this.GenerateException(num2);
			}
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x000176E4 File Offset: 0x000158E4
		private int GetBinaryValue(RegistryKey rkey, string name, RegistryValueKind type, out byte[] data, int size)
		{
			byte[] array = new byte[size];
			IntPtr handle = Win32RegistryApi.GetHandle(rkey);
			int result = Win32RegistryApi.RegQueryValueEx(handle, name, IntPtr.Zero, ref type, array, ref size);
			data = array;
			return result;
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00017718 File Offset: 0x00015918
		public int SubKeyCount(RegistryKey rkey)
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			IntPtr handle = Win32RegistryApi.GetHandle(rkey);
			int num = 0;
			for (;;)
			{
				int num2 = Win32RegistryApi.RegEnumKey(handle, num, stringBuilder, stringBuilder.Capacity);
				if (num2 == 1018)
				{
					break;
				}
				if (num2 != 0)
				{
					if (num2 == 259)
					{
						return num;
					}
					this.GenerateException(num2);
				}
				num++;
			}
			throw RegistryKey.CreateMarkedForDeletionException();
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0001778C File Offset: 0x0001598C
		public int ValueCount(RegistryKey rkey)
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			IntPtr handle = Win32RegistryApi.GetHandle(rkey);
			int num = 0;
			for (;;)
			{
				RegistryValueKind registryValueKind = RegistryValueKind.Unknown;
				int capacity = stringBuilder.Capacity;
				int num2 = Win32RegistryApi.RegEnumValue(handle, num, stringBuilder, ref capacity, IntPtr.Zero, ref registryValueKind, IntPtr.Zero, IntPtr.Zero);
				if (num2 == 1018)
				{
					break;
				}
				if (num2 != 0 && num2 != 234)
				{
					if (num2 == 259)
					{
						return num;
					}
					this.GenerateException(num2);
				}
				num++;
			}
			throw RegistryKey.CreateMarkedForDeletionException();
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00017824 File Offset: 0x00015A24
		public RegistryKey OpenRemoteBaseKey(RegistryHive hKey, string machineName)
		{
			IntPtr hKey2 = new IntPtr((int)hKey);
			IntPtr keyHandle;
			int num = Win32RegistryApi.RegConnectRegistry(machineName, hKey2, out keyHandle);
			if (num != 0)
			{
				this.GenerateException(num);
			}
			return new RegistryKey(hKey, keyHandle, true);
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00017858 File Offset: 0x00015A58
		public RegistryKey OpenSubKey(RegistryKey rkey, string keyName, bool writable)
		{
			int num = 131097;
			if (writable)
			{
				num |= 131078;
			}
			IntPtr handle = Win32RegistryApi.GetHandle(rkey);
			IntPtr intPtr;
			int num2 = Win32RegistryApi.RegOpenKeyEx(handle, keyName, IntPtr.Zero, num, out intPtr);
			if (num2 == 2 || num2 == 1018)
			{
				return null;
			}
			if (num2 != 0)
			{
				this.GenerateException(num2);
			}
			return new RegistryKey(intPtr, Win32RegistryApi.CombineName(rkey, keyName), writable);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x000178C4 File Offset: 0x00015AC4
		public void Flush(RegistryKey rkey)
		{
			if (!Win32RegistryApi.IsHandleValid(rkey))
			{
				return;
			}
			IntPtr handle = Win32RegistryApi.GetHandle(rkey);
			Win32RegistryApi.RegFlushKey(handle);
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x000178EC File Offset: 0x00015AEC
		public void Close(RegistryKey rkey)
		{
			if (!Win32RegistryApi.IsHandleValid(rkey))
			{
				return;
			}
			IntPtr handle = Win32RegistryApi.GetHandle(rkey);
			Win32RegistryApi.RegCloseKey(handle);
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x00017914 File Offset: 0x00015B14
		public RegistryKey CreateSubKey(RegistryKey rkey, string keyName)
		{
			IntPtr handle = Win32RegistryApi.GetHandle(rkey);
			IntPtr intPtr;
			int num = Win32RegistryApi.RegCreateKey(handle, keyName, out intPtr);
			if (num == 1018)
			{
				throw RegistryKey.CreateMarkedForDeletionException();
			}
			if (num != 0)
			{
				this.GenerateException(num);
			}
			return new RegistryKey(intPtr, Win32RegistryApi.CombineName(rkey, keyName), true);
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00017964 File Offset: 0x00015B64
		public void DeleteKey(RegistryKey rkey, string keyName, bool shouldThrowWhenKeyMissing)
		{
			IntPtr handle = Win32RegistryApi.GetHandle(rkey);
			int num = Win32RegistryApi.RegDeleteKey(handle, keyName);
			if (num != 2)
			{
				if (num != 0)
				{
					this.GenerateException(num);
				}
				return;
			}
			if (shouldThrowWhenKeyMissing)
			{
				throw new ArgumentException("key " + keyName);
			}
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x000179AC File Offset: 0x00015BAC
		public void DeleteValue(RegistryKey rkey, string value, bool shouldThrowWhenKeyMissing)
		{
			IntPtr handle = Win32RegistryApi.GetHandle(rkey);
			int num = Win32RegistryApi.RegDeleteValue(handle, value);
			if (num == 1018)
			{
				return;
			}
			if (num != 2)
			{
				if (num != 0)
				{
					this.GenerateException(num);
				}
				return;
			}
			if (shouldThrowWhenKeyMissing)
			{
				throw new ArgumentException("value " + value);
			}
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00017A00 File Offset: 0x00015C00
		public string[] GetSubKeyNames(RegistryKey rkey)
		{
			IntPtr handle = Win32RegistryApi.GetHandle(rkey);
			StringBuilder stringBuilder = new StringBuilder(1024);
			ArrayList arrayList = new ArrayList();
			int num = 0;
			for (;;)
			{
				int num2 = Win32RegistryApi.RegEnumKey(handle, num, stringBuilder, stringBuilder.Capacity);
				if (num2 == 0)
				{
					arrayList.Add(stringBuilder.ToString());
					stringBuilder.Length = 0;
				}
				else
				{
					if (num2 == 259)
					{
						break;
					}
					this.GenerateException(num2);
				}
				num++;
			}
			return (string[])arrayList.ToArray(typeof(string));
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00017A94 File Offset: 0x00015C94
		public string[] GetValueNames(RegistryKey rkey)
		{
			IntPtr handle = Win32RegistryApi.GetHandle(rkey);
			ArrayList arrayList = new ArrayList();
			int num = 0;
			for (;;)
			{
				StringBuilder stringBuilder = new StringBuilder(1024);
				int capacity = stringBuilder.Capacity;
				RegistryValueKind registryValueKind = RegistryValueKind.Unknown;
				int num2 = Win32RegistryApi.RegEnumValue(handle, num, stringBuilder, ref capacity, IntPtr.Zero, ref registryValueKind, IntPtr.Zero, IntPtr.Zero);
				if (num2 == 0 || num2 == 234)
				{
					arrayList.Add(stringBuilder.ToString());
				}
				else
				{
					if (num2 == 259)
					{
						break;
					}
					if (num2 == 1018)
					{
						goto Block_3;
					}
					this.GenerateException(num2);
				}
				num++;
			}
			return (string[])arrayList.ToArray(typeof(string));
			Block_3:
			throw RegistryKey.CreateMarkedForDeletionException();
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00017B58 File Offset: 0x00015D58
		private void GenerateException(int errorCode)
		{
			switch (errorCode)
			{
			case 2:
				break;
			default:
				if (errorCode == 53)
				{
					throw new IOException("The network path was not found.");
				}
				if (errorCode != 87)
				{
					throw new SystemException();
				}
				break;
			case 5:
				throw new SecurityException();
			}
			throw new ArgumentException();
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00017BB0 File Offset: 0x00015DB0
		public string ToString(RegistryKey rkey)
		{
			return rkey.Name;
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00017BB8 File Offset: 0x00015DB8
		internal static string CombineName(RegistryKey rkey, string localName)
		{
			return rkey.Name + "\\" + localName;
		}

		// Token: 0x04000108 RID: 264
		private const int OpenRegKeyRead = 131097;

		// Token: 0x04000109 RID: 265
		private const int OpenRegKeyWrite = 131078;

		// Token: 0x0400010A RID: 266
		private const int Int32ByteSize = 4;

		// Token: 0x0400010B RID: 267
		private const int BufferMaxLength = 1024;

		// Token: 0x0400010C RID: 268
		private readonly int NativeBytesPerCharacter = Marshal.SystemDefaultCharSize;
	}
}
