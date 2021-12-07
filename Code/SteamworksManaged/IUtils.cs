using System;
using ManagedSteam.CallbackStructures;
using ManagedSteam.SteamTypes;

namespace ManagedSteam
{
	// Token: 0x02000067 RID: 103
	public interface IUtils
	{
		// Token: 0x14000041 RID: 65
		// (add) Token: 0x0600034F RID: 847
		// (remove) Token: 0x06000350 RID: 848
		event CallbackEvent<IPCountry> IPCountry;

		// Token: 0x14000042 RID: 66
		// (add) Token: 0x06000351 RID: 849
		// (remove) Token: 0x06000352 RID: 850
		event CallbackEvent<LowBatteryPower> LowBatteryPower;

		// Token: 0x14000043 RID: 67
		// (add) Token: 0x06000353 RID: 851
		// (remove) Token: 0x06000354 RID: 852
		event CallbackEvent<SteamAPICallCompleted> SteamAPICallCompleted;

		// Token: 0x14000044 RID: 68
		// (add) Token: 0x06000355 RID: 853
		// (remove) Token: 0x06000356 RID: 854
		event CallbackEvent<SteamShutdown> SteamShutdown;

		// Token: 0x14000045 RID: 69
		// (add) Token: 0x06000357 RID: 855
		// (remove) Token: 0x06000358 RID: 856
		event CallbackEvent<CheckFileSignature> CheckFileSignature;

		// Token: 0x14000046 RID: 70
		// (add) Token: 0x06000359 RID: 857
		// (remove) Token: 0x0600035A RID: 858
		event CallbackEvent<GamepadTextInputDismissed> GamepadTextInputDismissed;

		// Token: 0x0600035B RID: 859
		uint GetSecondsSinceAppActive();

		// Token: 0x0600035C RID: 860
		uint GetSecondsSinceComputerActive();

		// Token: 0x0600035D RID: 861
		Universe GetConnectedUniverse();

		// Token: 0x0600035E RID: 862
		uint GetServerRealTime();

		// Token: 0x0600035F RID: 863
		string GetIPCountry();

		// Token: 0x06000360 RID: 864
		bool GetImageSize(ImageHandle image, out uint width, out uint height);

		// Token: 0x06000361 RID: 865
		UtilsGetImageSizeResult GetImageSize(ImageHandle image);

		// Token: 0x06000362 RID: 866
		bool GetImageRGBA(ImageHandle image, IntPtr buffer, int bufferSize);

		// Token: 0x06000363 RID: 867
		bool GetCSERIPPort(out uint unIP, out ushort usPort);

		// Token: 0x06000364 RID: 868
		UtilsGetCSERIPPortResult GetCSERIPPort();

		// Token: 0x06000365 RID: 869
		int GetCurrentBatteryPower();

		// Token: 0x06000366 RID: 870
		void SetOverlayNotificationPosition(NotificationPosition notificationPosition);

		// Token: 0x06000367 RID: 871
		bool IsAPICallCompleted(SteamAPICall steamAPICall, out bool failed);

		// Token: 0x06000368 RID: 872
		bool GetAPICallResult<T>(SteamAPICall steamAPICall, ref T callback, out bool failed) where T : struct;

		// Token: 0x06000369 RID: 873
		void RunFrame();

		// Token: 0x0600036A RID: 874
		uint GetIPCCallCount();

		// Token: 0x0600036B RID: 875
		bool IsOverlayEnabled();

		// Token: 0x0600036C RID: 876
		bool OverlayNeedsPresent();

		// Token: 0x0600036D RID: 877
		bool ShowGamepadTextInput(GamepadTextInputMode inputMode, GamepadTextInputLineMode lineInputMode, string description, uint charMax);

		// Token: 0x0600036E RID: 878
		uint GetEnteredGamepadTextLength();

		// Token: 0x0600036F RID: 879
		bool GetEnteredGamepadTextInput(out string pchText);

		// Token: 0x06000370 RID: 880
		UtilsGetEnteredGamepadTextInputResult GetEnteredGamepadTextInput();

		// Token: 0x06000371 RID: 881
		bool IsSteamRunningInVR();
	}
}
