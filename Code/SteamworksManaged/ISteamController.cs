using System;
using ManagedSteam.SteamTypes;

namespace ManagedSteam
{
	// Token: 0x020000F6 RID: 246
	public interface ISteamController
	{
		// Token: 0x06000685 RID: 1669
		bool Init(string absolutPathToControllerConfigVDF);

		// Token: 0x06000686 RID: 1670
		bool Shutdown();

		// Token: 0x06000687 RID: 1671
		void RunFrame();

		// Token: 0x06000688 RID: 1672
		bool GetControllerState(uint controllerIndex, out SteamControllerState state);

		// Token: 0x06000689 RID: 1673
		void TriggerHapticPulse(uint controllerIndex, SteamControllerPad targetPad, ushort durationMicroSec);

		// Token: 0x0600068A RID: 1674
		void SetOverrideMode(string mode);
	}
}
