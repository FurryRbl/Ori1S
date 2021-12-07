using System;
using ManagedSteam.SteamTypes;

namespace ManagedSteam
{
	// Token: 0x02000075 RID: 117
	public interface IHmd
	{
		// Token: 0x060003DE RID: 990
		HmdError Init();

		// Token: 0x060003DF RID: 991
		void Shutdown();

		// Token: 0x060003E0 RID: 992
		bool GetWindowBounds(out int X, out int Y, out uint Width, out uint Height);

		// Token: 0x060003E1 RID: 993
		void GetRecommendedRenderTargetSize(out uint Width, out uint Height);

		// Token: 0x060003E2 RID: 994
		void GetEyeOutputViewport(HmdEye Eye, GraphicsAPIConvention APIType, out uint X, out uint Y, out uint Width, out uint Height);

		// Token: 0x060003E3 RID: 995
		HmdMatrix44 GetProjectionMatrix(HmdEye Eye, float NearZ, float FarZ, GraphicsAPIConvention ProjType);

		// Token: 0x060003E4 RID: 996
		void GetProjectionRaw(HmdEye Eye, out float Left, out float Right, out float Top, out float Bottom);

		// Token: 0x060003E5 RID: 997
		DistortionCoordinates ComputeDistortion(HmdEye Eye, float U, float V);

		// Token: 0x060003E6 RID: 998
		HmdMatrix44 GetEyeMatrix(HmdEye Eye);

		// Token: 0x060003E7 RID: 999
		bool GetViewMatrix(float SecondsFromNow, out HmdMatrix44 MatLeftView, out HmdMatrix44 MatRightView, out HmdTrackingResult Result);

		// Token: 0x060003E8 RID: 1000
		bool GetWorldFromHeadPose(float PredictedSecondsFromNow, out HmdMatrix34 Pose, out HmdTrackingResult Result);

		// Token: 0x060003E9 RID: 1001
		bool GetLastWorldFromHeadPose(out HmdMatrix34 Pose);

		// Token: 0x060003EA RID: 1002
		bool WillDriftInYaw();

		// Token: 0x060003EB RID: 1003
		void ZeroTracker();

		// Token: 0x060003EC RID: 1004
		uint GetDriverId(out string Buffer);

		// Token: 0x060003ED RID: 1005
		uint GetDisplayId(out string Buffer);
	}
}
