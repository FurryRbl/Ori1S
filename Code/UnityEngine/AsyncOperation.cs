using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000A9 RID: 169
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class AsyncOperation : YieldInstruction
	{
		// Token: 0x0600099B RID: 2459
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalDestroy();

		// Token: 0x0600099C RID: 2460 RVA: 0x0000E0AC File Offset: 0x0000C2AC
		~AsyncOperation()
		{
			this.InternalDestroy();
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x0600099D RID: 2461
		public extern bool isDone { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x0600099E RID: 2462
		public extern float progress { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x0600099F RID: 2463
		// (set) Token: 0x060009A0 RID: 2464
		public extern int priority { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060009A1 RID: 2465
		// (set) Token: 0x060009A2 RID: 2466
		public extern bool allowSceneActivation { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x04000201 RID: 513
		internal IntPtr m_Ptr;
	}
}
