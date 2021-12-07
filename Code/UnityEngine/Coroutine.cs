using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000014 RID: 20
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class Coroutine : YieldInstruction
	{
		// Token: 0x0600006E RID: 110 RVA: 0x000024DC File Offset: 0x000006DC
		private Coroutine()
		{
		}

		// Token: 0x0600006F RID: 111
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ReleaseCoroutine();

		// Token: 0x06000070 RID: 112 RVA: 0x000024E4 File Offset: 0x000006E4
		~Coroutine()
		{
			this.ReleaseCoroutine();
		}

		// Token: 0x0400006B RID: 107
		internal IntPtr m_Ptr;
	}
}
