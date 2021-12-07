using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;
using ManagedSteam.Utility;

namespace ManagedSteam.Implementations
{
	// Token: 0x02000076 RID: 118
	internal class Hmd : IHmd
	{
		// Token: 0x060003EF RID: 1007 RVA: 0x000076E4 File Offset: 0x000058E4
		private void CheckIfUsable()
		{
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x000076E6 File Offset: 0x000058E6
		public HmdError Init()
		{
			this.CheckIfUsable();
			return (HmdError)NativeMethods.VR_Init();
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x000076F3 File Offset: 0x000058F3
		public void Shutdown()
		{
			this.CheckIfUsable();
			NativeMethods.VR_Shutdown();
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00007700 File Offset: 0x00005900
		public bool GetWindowBounds(out int X, out int Y, out uint Width, out uint Height)
		{
			this.CheckIfUsable();
			X = 0;
			Y = 0;
			Width = 0U;
			Height = 0U;
			return NativeMethods.VR_Hmd_GetWindowBounds(ref X, ref Y, ref Width, ref Height);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000771F File Offset: 0x0000591F
		public void GetRecommendedRenderTargetSize(out uint Width, out uint Height)
		{
			this.CheckIfUsable();
			Width = 0U;
			Height = 0U;
			NativeMethods.VR_Hmd_GetRecommendedRenderTargetSize(ref Width, ref Height);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00007734 File Offset: 0x00005934
		public void GetEyeOutputViewport(HmdEye Eye, GraphicsAPIConvention APIType, out uint X, out uint Y, out uint Width, out uint Height)
		{
			this.CheckIfUsable();
			X = 0U;
			Y = 0U;
			Width = 0U;
			Height = 0U;
			NativeMethods.VR_Hmd_GetEyeOutputViewport((int)Eye, (int)APIType, ref X, ref Y, ref Width, ref Height);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00007759 File Offset: 0x00005959
		public HmdMatrix44 GetProjectionMatrix(HmdEye Eye, float NearZ, float FarZ, GraphicsAPIConvention ProjType)
		{
			this.CheckIfUsable();
			return NativeHelpers.ConvertStructToClass<HmdMatrix44>(NativeMethods.VR_Hmd_GetProjectionMatrix((int)Eye, NearZ, FarZ, (int)ProjType), Marshal.SizeOf(typeof(HmdMatrix44)));
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000777F File Offset: 0x0000597F
		public void GetProjectionRaw(HmdEye Eye, out float Left, out float Right, out float Top, out float Bottom)
		{
			this.CheckIfUsable();
			Left = 0f;
			Right = 0f;
			Top = 0f;
			Bottom = 0f;
			NativeMethods.VR_Hmd_GetProjectionRaw((int)Eye, ref Left, ref Right, ref Top, ref Bottom);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x000077B1 File Offset: 0x000059B1
		public DistortionCoordinates ComputeDistortion(HmdEye Eye, float U, float V)
		{
			this.CheckIfUsable();
			return NativeHelpers.ConvertStructToClass<DistortionCoordinates>(NativeMethods.VR_Hmd_ComputeDistortion((int)Eye, U, V), Marshal.SizeOf(typeof(DistortionCoordinates)));
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x000077D5 File Offset: 0x000059D5
		public HmdMatrix44 GetEyeMatrix(HmdEye Eye)
		{
			this.CheckIfUsable();
			return NativeHelpers.ConvertStructToClass<HmdMatrix44>(NativeMethods.VR_Hmd_GetEyeMatrix((int)Eye), Marshal.SizeOf(typeof(HmdMatrix44)));
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x000077F8 File Offset: 0x000059F8
		public bool GetViewMatrix(float SecondsFromNow, out HmdMatrix44 MatLeftView, out HmdMatrix44 MatRightView, out HmdTrackingResult Result)
		{
			this.CheckIfUsable();
			bool result;
			using (NativeBuffer nativeBuffer = new NativeBuffer(Marshal.SizeOf(typeof(HmdMatrix44))))
			{
				using (NativeBuffer nativeBuffer2 = new NativeBuffer(Marshal.SizeOf(typeof(HmdMatrix44))))
				{
					int num = 0;
					bool flag = NativeMethods.VR_Hmd_GetViewMatrix(SecondsFromNow, nativeBuffer.UnmanagedMemory, nativeBuffer2.UnmanagedMemory, ref num);
					MatLeftView = NativeHelpers.ConvertStructToClass<HmdMatrix44>(nativeBuffer.UnmanagedMemory, nativeBuffer.UnmanagedSize);
					MatRightView = NativeHelpers.ConvertStructToClass<HmdMatrix44>(nativeBuffer2.UnmanagedMemory, nativeBuffer2.UnmanagedSize);
					Result = (HmdTrackingResult)num;
					result = flag;
				}
			}
			return result;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x000078B0 File Offset: 0x00005AB0
		public bool GetWorldFromHeadPose(float PredictedSecondsFromNow, out HmdMatrix34 Pose, out HmdTrackingResult Result)
		{
			this.CheckIfUsable();
			bool result;
			using (NativeBuffer nativeBuffer = new NativeBuffer(Marshal.SizeOf(typeof(HmdMatrix34))))
			{
				int num = 0;
				bool flag = NativeMethods.VR_Hmd_GetWorldFromHeadPose(PredictedSecondsFromNow, nativeBuffer.UnmanagedMemory, ref num);
				Pose = NativeHelpers.ConvertStructToClass<HmdMatrix34>(nativeBuffer.UnmanagedMemory, nativeBuffer.UnmanagedSize);
				Result = (HmdTrackingResult)num;
				result = flag;
			}
			return result;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00007920 File Offset: 0x00005B20
		public bool GetLastWorldFromHeadPose(out HmdMatrix34 Pose)
		{
			this.CheckIfUsable();
			bool result;
			using (NativeBuffer nativeBuffer = new NativeBuffer(Marshal.SizeOf(typeof(HmdMatrix34))))
			{
				bool flag = NativeMethods.VR_Hmd_GetLastWorldFromHeadPose(nativeBuffer.UnmanagedMemory);
				Pose = NativeHelpers.ConvertStructToClass<HmdMatrix34>(nativeBuffer.UnmanagedMemory, nativeBuffer.UnmanagedSize);
				result = flag;
			}
			return result;
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00007988 File Offset: 0x00005B88
		public bool WillDriftInYaw()
		{
			this.CheckIfUsable();
			return NativeMethods.VR_Hmd_WillDriftInYaw();
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00007995 File Offset: 0x00005B95
		public void ZeroTracker()
		{
			this.CheckIfUsable();
			NativeMethods.VR_Hmd_ZeroTracker();
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x000079A4 File Offset: 0x00005BA4
		public uint GetDriverId(out string Buffer)
		{
			this.CheckIfUsable();
			uint result;
			using (NativeBuffer nativeBuffer = new NativeBuffer(128))
			{
				uint num = NativeMethods.VR_Hmd_GetDriverId(nativeBuffer.UnmanagedMemory, (uint)nativeBuffer.UnmanagedSize);
				Buffer = NativeHelpers.ToStringAnsi(nativeBuffer.UnmanagedMemory);
				result = num;
			}
			return result;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00007A00 File Offset: 0x00005C00
		public uint GetDisplayId(out string Buffer)
		{
			this.CheckIfUsable();
			uint result;
			using (NativeBuffer nativeBuffer = new NativeBuffer(128))
			{
				uint num = NativeMethods.VR_Hmd_GetDisplayId(nativeBuffer.UnmanagedMemory, (uint)nativeBuffer.UnmanagedSize);
				Buffer = NativeHelpers.ToStringAnsi(nativeBuffer.UnmanagedMemory);
				result = num;
			}
			return result;
		}
	}
}
