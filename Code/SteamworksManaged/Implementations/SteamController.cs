using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;
using ManagedSteam.Utility;

namespace ManagedSteam.Implementations
{
	// Token: 0x02000101 RID: 257
	internal class SteamController : SteamService, ISteamController
	{
		// Token: 0x06000799 RID: 1945 RVA: 0x0000B521 File Offset: 0x00009721
		internal override void CheckIfUsableInternal()
		{
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0000B523 File Offset: 0x00009723
		internal override void InvokeEvents()
		{
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0000B525 File Offset: 0x00009725
		internal override void ReleaseManagedResources()
		{
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0000B527 File Offset: 0x00009727
		public bool Init(string absolutPathToControllerConfigVDF)
		{
			base.CheckIfUsable();
			return NativeMethods.SteamController_Init(absolutPathToControllerConfigVDF);
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0000B535 File Offset: 0x00009735
		public bool Shutdown()
		{
			base.CheckIfUsable();
			return NativeMethods.SteamController_Shutdown();
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0000B542 File Offset: 0x00009742
		public void RunFrame()
		{
			base.CheckIfUsable();
			NativeMethods.SteamController_RunFrame();
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0000B550 File Offset: 0x00009750
		public bool GetControllerState(uint controllerIndex, out SteamControllerState state)
		{
			base.CheckIfUsable();
			bool result;
			using (NativeBuffer nativeBuffer = new NativeBuffer(Marshal.SizeOf(typeof(SteamControllerState))))
			{
				bool flag = NativeMethods.SteamController_GetControllerState(controllerIndex, nativeBuffer.UnmanagedMemory);
				state = NativeHelpers.ConvertStruct<SteamControllerState>(nativeBuffer.UnmanagedMemory, nativeBuffer.UnmanagedSize);
				result = flag;
			}
			return result;
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0000B5BC File Offset: 0x000097BC
		public SteamControllerGetControllerStateResult GetControllerState(uint controllerIndex)
		{
			base.CheckIfUsable();
			SteamControllerGetControllerStateResult result;
			using (NativeBuffer nativeBuffer = new NativeBuffer(Marshal.SizeOf(typeof(SteamControllerState))))
			{
				SteamControllerGetControllerStateResult steamControllerGetControllerStateResult;
				steamControllerGetControllerStateResult.Result = NativeMethods.SteamController_GetControllerState(controllerIndex, nativeBuffer.UnmanagedMemory);
				steamControllerGetControllerStateResult.State = NativeHelpers.ConvertStruct<SteamControllerState>(nativeBuffer.UnmanagedMemory, nativeBuffer.UnmanagedSize);
				result = steamControllerGetControllerStateResult;
			}
			return result;
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0000B630 File Offset: 0x00009830
		public void TriggerHapticPulse(uint controllerIndex, SteamControllerPad targetPad, ushort durationMicroSec)
		{
			NativeMethods.SteamController_TriggerHapticPulse(controllerIndex, (int)targetPad, durationMicroSec);
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0000B63A File Offset: 0x0000983A
		public void SetOverrideMode(string mode)
		{
			NativeMethods.SteamController_SetOverrideMode(mode);
		}
	}
}
