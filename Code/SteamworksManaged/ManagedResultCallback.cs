using System;
using System.Runtime.InteropServices;

// Token: 0x02000117 RID: 279
// (Invoke) Token: 0x0600080B RID: 2059
[UnmanagedFunctionPointer(CallingConvention.StdCall)]
internal delegate void ManagedResultCallback(int id, IntPtr data, int dataSize, [MarshalAs(UnmanagedType.I1)] bool ioFailure);
