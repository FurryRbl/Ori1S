using System;
using System.Runtime.InteropServices;

// Token: 0x02000116 RID: 278
// (Invoke) Token: 0x06000807 RID: 2055
[UnmanagedFunctionPointer(CallingConvention.StdCall)]
internal delegate void ManagedCallback(int id, IntPtr data, int dataSize);
