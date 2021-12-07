using System;

namespace UnityEngine
{
	// Token: 0x02000009 RID: 9
	public enum RuntimePlatform
	{
		// Token: 0x04000012 RID: 18
		OSXEditor,
		// Token: 0x04000013 RID: 19
		OSXPlayer,
		// Token: 0x04000014 RID: 20
		WindowsPlayer,
		// Token: 0x04000015 RID: 21
		OSXWebPlayer,
		// Token: 0x04000016 RID: 22
		OSXDashboardPlayer,
		// Token: 0x04000017 RID: 23
		WindowsWebPlayer,
		// Token: 0x04000018 RID: 24
		WindowsEditor = 7,
		// Token: 0x04000019 RID: 25
		IPhonePlayer,
		// Token: 0x0400001A RID: 26
		XBOX360 = 10,
		// Token: 0x0400001B RID: 27
		PS3 = 9,
		// Token: 0x0400001C RID: 28
		Android = 11,
		// Token: 0x0400001D RID: 29
		[Obsolete("NaCl export is no longer supported in Unity 5.0+.")]
		NaCl,
		// Token: 0x0400001E RID: 30
		[Obsolete("FlashPlayer export is no longer supported in Unity 5.0+.")]
		FlashPlayer = 15,
		// Token: 0x0400001F RID: 31
		LinuxPlayer = 13,
		// Token: 0x04000020 RID: 32
		WebGLPlayer = 17,
		// Token: 0x04000021 RID: 33
		[Obsolete("Use WSAPlayerX86 instead")]
		MetroPlayerX86,
		// Token: 0x04000022 RID: 34
		WSAPlayerX86 = 18,
		// Token: 0x04000023 RID: 35
		[Obsolete("Use WSAPlayerX64 instead")]
		MetroPlayerX64,
		// Token: 0x04000024 RID: 36
		WSAPlayerX64 = 19,
		// Token: 0x04000025 RID: 37
		[Obsolete("Use WSAPlayerARM instead")]
		MetroPlayerARM,
		// Token: 0x04000026 RID: 38
		WSAPlayerARM = 20,
		// Token: 0x04000027 RID: 39
		WP8Player,
		// Token: 0x04000028 RID: 40
		BlackBerryPlayer,
		// Token: 0x04000029 RID: 41
		TizenPlayer,
		// Token: 0x0400002A RID: 42
		PSP2,
		// Token: 0x0400002B RID: 43
		PS4,
		// Token: 0x0400002C RID: 44
		PSM,
		// Token: 0x0400002D RID: 45
		XboxOne,
		// Token: 0x0400002E RID: 46
		SamsungTVPlayer,
		// Token: 0x0400002F RID: 47
		WiiU = 30,
		// Token: 0x04000030 RID: 48
		tvOS
	}
}
