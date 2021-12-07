using System;
using System.Runtime.InteropServices;

// Token: 0x02000120 RID: 288
// (Invoke) Token: 0x0600082F RID: 2095
[UnmanagedFunctionPointer(CallingConvention.StdCall)]
internal delegate void MatchmakingRulesResponse_RulesResponded(uint sender, IntPtr key, IntPtr value);
