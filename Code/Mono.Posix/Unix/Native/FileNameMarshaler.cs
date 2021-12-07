using System;
using System.Runtime.InteropServices;

namespace Mono.Unix.Native
{
	// Token: 0x02000026 RID: 38
	internal class FileNameMarshaler : ICustomMarshaler
	{
		// Token: 0x06000203 RID: 515 RVA: 0x000087FC File Offset: 0x000069FC
		public static ICustomMarshaler GetInstance(string s)
		{
			return FileNameMarshaler.Instance;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00008804 File Offset: 0x00006A04
		public void CleanUpManagedData(object o)
		{
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00008808 File Offset: 0x00006A08
		public void CleanUpNativeData(IntPtr pNativeData)
		{
			UnixMarshal.FreeHeap(pNativeData);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00008810 File Offset: 0x00006A10
		public int GetNativeDataSize()
		{
			return IntPtr.Size;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00008818 File Offset: 0x00006A18
		public IntPtr MarshalManagedToNative(object obj)
		{
			string text = obj as string;
			if (text == null)
			{
				return IntPtr.Zero;
			}
			return UnixMarshal.StringToHeap(text, UnixEncoding.Instance);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00008848 File Offset: 0x00006A48
		public object MarshalNativeToManaged(IntPtr pNativeData)
		{
			return UnixMarshal.PtrToString(pNativeData, UnixEncoding.Instance);
		}

		// Token: 0x04000092 RID: 146
		private static FileNameMarshaler Instance = new FileNameMarshaler();
	}
}
