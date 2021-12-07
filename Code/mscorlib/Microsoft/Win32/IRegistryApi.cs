using System;

namespace Microsoft.Win32
{
	// Token: 0x02000066 RID: 102
	internal interface IRegistryApi
	{
		// Token: 0x060006DC RID: 1756
		RegistryKey CreateSubKey(RegistryKey rkey, string keyname);

		// Token: 0x060006DD RID: 1757
		RegistryKey OpenRemoteBaseKey(RegistryHive hKey, string machineName);

		// Token: 0x060006DE RID: 1758
		RegistryKey OpenSubKey(RegistryKey rkey, string keyname, bool writtable);

		// Token: 0x060006DF RID: 1759
		void Flush(RegistryKey rkey);

		// Token: 0x060006E0 RID: 1760
		void Close(RegistryKey rkey);

		// Token: 0x060006E1 RID: 1761
		object GetValue(RegistryKey rkey, string name, object default_value, RegistryValueOptions options);

		// Token: 0x060006E2 RID: 1762
		void SetValue(RegistryKey rkey, string name, object value);

		// Token: 0x060006E3 RID: 1763
		int SubKeyCount(RegistryKey rkey);

		// Token: 0x060006E4 RID: 1764
		int ValueCount(RegistryKey rkey);

		// Token: 0x060006E5 RID: 1765
		void DeleteValue(RegistryKey rkey, string value, bool throw_if_missing);

		// Token: 0x060006E6 RID: 1766
		void DeleteKey(RegistryKey rkey, string keyName, bool throw_if_missing);

		// Token: 0x060006E7 RID: 1767
		string[] GetSubKeyNames(RegistryKey rkey);

		// Token: 0x060006E8 RID: 1768
		string[] GetValueNames(RegistryKey rkey);

		// Token: 0x060006E9 RID: 1769
		string ToString(RegistryKey rkey);

		// Token: 0x060006EA RID: 1770
		void SetValue(RegistryKey rkey, string name, object value, RegistryValueKind valueKind);
	}
}
