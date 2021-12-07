using System;

namespace J2i.Net.XInputWrapper
{
	// Token: 0x0200000D RID: 13
	public class XInputConstants
	{
		// Token: 0x04000047 RID: 71
		public const int XINPUT_DEVTYPE_GAMEPAD = 1;

		// Token: 0x04000048 RID: 72
		public const int XINPUT_DEVSUBTYPE_GAMEPAD = 1;

		// Token: 0x04000049 RID: 73
		public const int XINPUT_GAMEPAD_LEFT_THUMB_DEADZONE = 7849;

		// Token: 0x0400004A RID: 74
		public const int XINPUT_GAMEPAD_RIGHT_THUMB_DEADZONE = 8689;

		// Token: 0x0400004B RID: 75
		public const int XINPUT_GAMEPAD_TRIGGER_THRESHOLD = 30;

		// Token: 0x0400004C RID: 76
		public const int XINPUT_FLAG_GAMEPAD = 1;

		// Token: 0x0200000E RID: 14
		public enum CapabilityFlags
		{
			// Token: 0x0400004E RID: 78
			XINPUT_CAPS_VOICE_SUPPORTED = 4,
			// Token: 0x0400004F RID: 79
			XINPUT_CAPS_FFB_SUPPORTED = 1,
			// Token: 0x04000050 RID: 80
			XINPUT_CAPS_WIRELESS,
			// Token: 0x04000051 RID: 81
			XINPUT_CAPS_PMD_SUPPORTED = 8,
			// Token: 0x04000052 RID: 82
			XINPUT_CAPS_NO_NAVIGATION = 16
		}
	}
}
