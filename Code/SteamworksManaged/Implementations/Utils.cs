using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ManagedSteam.CallbackStructures;
using ManagedSteam.SteamTypes;
using ManagedSteam.Utility;

namespace ManagedSteam.Implementations
{
	// Token: 0x020000F8 RID: 248
	internal class Utils : SteamService, IUtils
	{
		// Token: 0x0600068B RID: 1675 RVA: 0x00009C6C File Offset: 0x00007E6C
		public Utils()
		{
			this.ipCountry = new List<IPCountry>();
			this.lowBatteryPower = new List<LowBatteryPower>();
			this.steamAPICallCompleted = new List<SteamAPICallCompleted>();
			this.steamShutdown = new List<SteamShutdown>();
			this.checkFileSignature = new List<CheckFileSignature>();
			this.gamepadTextInputDismissed = new List<GamepadTextInputDismissed>();
			SteamService.Callbacks[CallbackID.IPCountry] = delegate(IntPtr data, int size)
			{
				this.ipCountry.Add(ManagedSteam.CallbackStructures.IPCountry.Create(data, size));
			};
			SteamService.Callbacks[CallbackID.LowBatteryPower] = delegate(IntPtr data, int size)
			{
				this.lowBatteryPower.Add(ManagedSteam.CallbackStructures.LowBatteryPower.Create(data, size));
			};
			SteamService.Callbacks[CallbackID.SteamShutdown] = delegate(IntPtr data, int size)
			{
				this.steamShutdown.Add(ManagedSteam.CallbackStructures.SteamShutdown.Create(data, size));
			};
			SteamService.Callbacks[CallbackID.CheckFileSignature] = delegate(IntPtr data, int size)
			{
				this.checkFileSignature.Add(ManagedSteam.CallbackStructures.CheckFileSignature.Create(data, size));
			};
			SteamService.Callbacks[CallbackID.GamepadTextInputDismissed] = delegate(IntPtr data, int size)
			{
				this.gamepadTextInputDismissed.Add(ManagedSteam.CallbackStructures.GamepadTextInputDismissed.Create(data, size));
			};
		}

		// Token: 0x14000089 RID: 137
		// (add) Token: 0x0600068C RID: 1676 RVA: 0x00009D60 File Offset: 0x00007F60
		// (remove) Token: 0x0600068D RID: 1677 RVA: 0x00009D98 File Offset: 0x00007F98
		public event CallbackEvent<IPCountry> IPCountry;

		// Token: 0x1400008A RID: 138
		// (add) Token: 0x0600068E RID: 1678 RVA: 0x00009DD0 File Offset: 0x00007FD0
		// (remove) Token: 0x0600068F RID: 1679 RVA: 0x00009E08 File Offset: 0x00008008
		public event CallbackEvent<LowBatteryPower> LowBatteryPower;

		// Token: 0x1400008B RID: 139
		// (add) Token: 0x06000690 RID: 1680 RVA: 0x00009E40 File Offset: 0x00008040
		// (remove) Token: 0x06000691 RID: 1681 RVA: 0x00009E78 File Offset: 0x00008078
		public event CallbackEvent<SteamAPICallCompleted> SteamAPICallCompleted;

		// Token: 0x1400008C RID: 140
		// (add) Token: 0x06000692 RID: 1682 RVA: 0x00009EB0 File Offset: 0x000080B0
		// (remove) Token: 0x06000693 RID: 1683 RVA: 0x00009EE8 File Offset: 0x000080E8
		public event CallbackEvent<SteamShutdown> SteamShutdown;

		// Token: 0x1400008D RID: 141
		// (add) Token: 0x06000694 RID: 1684 RVA: 0x00009F20 File Offset: 0x00008120
		// (remove) Token: 0x06000695 RID: 1685 RVA: 0x00009F58 File Offset: 0x00008158
		public event CallbackEvent<CheckFileSignature> CheckFileSignature;

		// Token: 0x1400008E RID: 142
		// (add) Token: 0x06000696 RID: 1686 RVA: 0x00009F90 File Offset: 0x00008190
		// (remove) Token: 0x06000697 RID: 1687 RVA: 0x00009FC8 File Offset: 0x000081C8
		public event CallbackEvent<GamepadTextInputDismissed> GamepadTextInputDismissed;

		// Token: 0x06000698 RID: 1688 RVA: 0x00009FFD File Offset: 0x000081FD
		internal override void CheckIfUsableInternal()
		{
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0000A000 File Offset: 0x00008200
		internal override void ReleaseManagedResources()
		{
			this.ipCountry = null;
			this.IPCountry = null;
			this.lowBatteryPower = null;
			this.LowBatteryPower = null;
			this.steamAPICallCompleted = null;
			this.SteamAPICallCompleted = null;
			this.steamShutdown = null;
			this.SteamShutdown = null;
			this.checkFileSignature = null;
			this.CheckFileSignature = null;
			this.gamepadTextInputDismissed = null;
			this.GamepadTextInputDismissed = null;
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0000A064 File Offset: 0x00008264
		internal override void InvokeEvents()
		{
			SteamService.InvokeEvents<IPCountry>(this.ipCountry, this.IPCountry);
			SteamService.InvokeEvents<LowBatteryPower>(this.lowBatteryPower, this.LowBatteryPower);
			SteamService.InvokeEvents<SteamAPICallCompleted>(this.steamAPICallCompleted, this.SteamAPICallCompleted);
			SteamService.InvokeEvents<SteamShutdown>(this.steamShutdown, this.SteamShutdown);
			SteamService.InvokeEvents<CheckFileSignature>(this.checkFileSignature, this.CheckFileSignature);
			SteamService.InvokeEvents<GamepadTextInputDismissed>(this.gamepadTextInputDismissed, this.GamepadTextInputDismissed);
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0000A0D7 File Offset: 0x000082D7
		public uint GetSecondsSinceAppActive()
		{
			base.CheckIfUsable();
			return NativeMethods.Utils_GetSecondsSinceAppActive();
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0000A0E4 File Offset: 0x000082E4
		public uint GetSecondsSinceComputerActive()
		{
			base.CheckIfUsable();
			return NativeMethods.Utils_GetSecondsSinceComputerActive();
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0000A0F1 File Offset: 0x000082F1
		public Universe GetConnectedUniverse()
		{
			return (Universe)NativeMethods.Utils_GetConnectedUniverse();
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0000A0F8 File Offset: 0x000082F8
		public uint GetServerRealTime()
		{
			base.CheckIfUsable();
			return NativeMethods.Utils_GetServerRealTime();
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0000A108 File Offset: 0x00008308
		public string GetIPCountry()
		{
			base.CheckIfUsable();
			IntPtr pointer = NativeMethods.Utils_GetIPCountry();
			return NativeHelpers.ToStringAnsi(pointer);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0000A127 File Offset: 0x00008327
		public bool GetImageSize(ImageHandle image, out uint width, out uint height)
		{
			base.CheckIfUsable();
			width = 0U;
			height = 0U;
			return NativeMethods.Utils_GetImageSize(image.AsInt32, ref width, ref height);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0000A144 File Offset: 0x00008344
		public UtilsGetImageSizeResult GetImageSize(ImageHandle image)
		{
			UtilsGetImageSizeResult result = default(UtilsGetImageSizeResult);
			result.Result = this.GetImageSize(image, out result.Width, out result.Height);
			return result;
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0000A176 File Offset: 0x00008376
		public bool GetImageRGBA(ImageHandle image, IntPtr buffer, int bufferSize)
		{
			base.CheckIfUsable();
			return NativeMethods.Utils_GetImageRGBA(image.AsInt32, buffer, bufferSize);
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0000A18C File Offset: 0x0000838C
		public bool GetCSERIPPort(out uint unIP, out ushort usPort)
		{
			base.CheckIfUsable();
			unIP = 0U;
			usPort = 0;
			return NativeMethods.Utils_GetCSERIPPort(ref unIP, ref usPort);
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0000A1A4 File Offset: 0x000083A4
		public UtilsGetCSERIPPortResult GetCSERIPPort()
		{
			UtilsGetCSERIPPortResult result = default(UtilsGetCSERIPPortResult);
			result.Result = this.GetCSERIPPort(out result.IP, out result.Port);
			return result;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0000A1D5 File Offset: 0x000083D5
		public int GetCurrentBatteryPower()
		{
			base.CheckIfUsable();
			return (int)NativeMethods.Utils_GetCurrentBatteryPower();
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0000A1E2 File Offset: 0x000083E2
		public void SetOverlayNotificationPosition(NotificationPosition notificationPosition)
		{
			NativeMethods.Utils_SetOverlayNotificationPosition((int)notificationPosition);
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0000A1EA File Offset: 0x000083EA
		public bool IsAPICallCompleted(SteamAPICall steamAPICall, out bool failed)
		{
			failed = false;
			return NativeMethods.Utils_IsAPICallCompleted(steamAPICall.AsUInt64, ref failed);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0000A1FC File Offset: 0x000083FC
		public bool GetAPICallResult<T>(SteamAPICall steamAPICall, ref T callback, out bool failed) where T : struct
		{
			failed = false;
			Marshal.SizeOf(typeof(T));
			if (Utils._CallResultBuffer == null)
			{
				Utils._CallResultBuffer = new byte[256];
			}
			GCHandle gchandle = GCHandle.Alloc(Utils._CallResultBuffer, GCHandleType.Pinned);
			bool result;
			try
			{
				IntPtr intPtr = Marshal.UnsafeAddrOfPinnedArrayElement(Utils._CallResultBuffer, 0);
				bool flag = NativeMethods.Utils_GetAPICallResult(steamAPICall.AsUInt64, intPtr, 256, 154, ref failed);
				callback = (T)((object)Marshal.PtrToStructure(intPtr, typeof(T)));
				result = flag;
			}
			finally
			{
				gchandle.Free();
			}
			return result;
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0000A29C File Offset: 0x0000849C
		public void RunFrame()
		{
			base.CheckIfUsable();
			NativeMethods.Utils_RunFrame();
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0000A2A9 File Offset: 0x000084A9
		public uint GetIPCCallCount()
		{
			base.CheckIfUsable();
			return NativeMethods.Utils_GetIPCCallCount();
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0000A2B6 File Offset: 0x000084B6
		public bool IsOverlayEnabled()
		{
			base.CheckIfUsable();
			return NativeMethods.Utils_IsOverlayEnabled();
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0000A2C3 File Offset: 0x000084C3
		public bool OverlayNeedsPresent()
		{
			base.CheckIfUsable();
			return NativeMethods.Utils_OverlayNeedsPresent();
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0000A2D0 File Offset: 0x000084D0
		public bool ShowGamepadTextInput(GamepadTextInputMode inputMode, GamepadTextInputLineMode lineInputMode, string description, uint charMax)
		{
			base.CheckIfUsable();
			return NativeMethods.Utils_ShowGamepadTextInput((int)inputMode, (int)lineInputMode, description, charMax);
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0000A2E2 File Offset: 0x000084E2
		public uint GetEnteredGamepadTextLength()
		{
			base.CheckIfUsable();
			return NativeMethods.Utils_GetEnteredGamepadTextLength();
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0000A2F0 File Offset: 0x000084F0
		public bool GetEnteredGamepadTextInput(out string pchText)
		{
			base.CheckIfUsable();
			bool result;
			using (NativeBuffer nativeBuffer = new NativeBuffer(256))
			{
				bool flag = NativeMethods.Utils_GetEnteredGamepadTextInput(nativeBuffer.UnmanagedMemory, (uint)nativeBuffer.UnmanagedSize);
				pchText = NativeHelpers.ToStringAnsi(nativeBuffer.UnmanagedMemory);
				result = flag;
			}
			return result;
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0000A34C File Offset: 0x0000854C
		public UtilsGetEnteredGamepadTextInputResult GetEnteredGamepadTextInput()
		{
			UtilsGetEnteredGamepadTextInputResult result = default(UtilsGetEnteredGamepadTextInputResult);
			result.Result = this.GetEnteredGamepadTextInput(out result.Text);
			return result;
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0000A376 File Offset: 0x00008576
		public bool IsSteamRunningInVR()
		{
			return NativeMethods.Utils_IsSteamRunningInVR();
		}

		// Token: 0x0400041F RID: 1055
		private List<IPCountry> ipCountry;

		// Token: 0x04000420 RID: 1056
		private List<LowBatteryPower> lowBatteryPower;

		// Token: 0x04000421 RID: 1057
		private List<SteamAPICallCompleted> steamAPICallCompleted;

		// Token: 0x04000422 RID: 1058
		private List<SteamShutdown> steamShutdown;

		// Token: 0x04000423 RID: 1059
		private List<CheckFileSignature> checkFileSignature;

		// Token: 0x04000424 RID: 1060
		private List<GamepadTextInputDismissed> gamepadTextInputDismissed;

		// Token: 0x0400042B RID: 1067
		[ThreadStatic]
		private static byte[] _CallResultBuffer;
	}
}
