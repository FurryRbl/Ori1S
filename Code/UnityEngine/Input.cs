using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000C3 RID: 195
	public sealed class Input
	{
		// Token: 0x06000B6A RID: 2922
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int mainGyroIndex_Internal();

		// Token: 0x06000B6B RID: 2923
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetKeyInt(int key);

		// Token: 0x06000B6C RID: 2924
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetKeyString(string name);

		// Token: 0x06000B6D RID: 2925
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetKeyUpInt(int key);

		// Token: 0x06000B6E RID: 2926
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetKeyUpString(string name);

		// Token: 0x06000B6F RID: 2927
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetKeyDownInt(int key);

		// Token: 0x06000B70 RID: 2928
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetKeyDownString(string name);

		// Token: 0x06000B71 RID: 2929
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetAxis(string axisName);

		// Token: 0x06000B72 RID: 2930
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetAxisRaw(string axisName);

		// Token: 0x06000B73 RID: 2931
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetButton(string buttonName);

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000B74 RID: 2932
		// (set) Token: 0x06000B75 RID: 2933
		public static extern bool compensateSensors { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000B76 RID: 2934
		[Obsolete("isGyroAvailable property is deprecated. Please use SystemInfo.supportsGyroscope instead.")]
		public static extern bool isGyroAvailable { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x0000F2F4 File Offset: 0x0000D4F4
		public static Gyroscope gyro
		{
			get
			{
				if (Input.m_MainGyro == null)
				{
					Input.m_MainGyro = new Gyroscope(Input.mainGyroIndex_Internal());
				}
				return Input.m_MainGyro;
			}
		}

		// Token: 0x06000B78 RID: 2936
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetButtonDown(string buttonName);

		// Token: 0x06000B79 RID: 2937
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetButtonUp(string buttonName);

		// Token: 0x06000B7A RID: 2938 RVA: 0x0000F314 File Offset: 0x0000D514
		public static bool GetKey(string name)
		{
			return Input.GetKeyString(name);
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0000F31C File Offset: 0x0000D51C
		public static bool GetKey(KeyCode key)
		{
			return Input.GetKeyInt((int)key);
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0000F324 File Offset: 0x0000D524
		public static bool GetKeyDown(string name)
		{
			return Input.GetKeyDownString(name);
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0000F32C File Offset: 0x0000D52C
		public static bool GetKeyDown(KeyCode key)
		{
			return Input.GetKeyDownInt((int)key);
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0000F334 File Offset: 0x0000D534
		public static bool GetKeyUp(string name)
		{
			return Input.GetKeyUpString(name);
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0000F33C File Offset: 0x0000D53C
		public static bool GetKeyUp(KeyCode key)
		{
			return Input.GetKeyUpInt((int)key);
		}

		// Token: 0x06000B80 RID: 2944
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string[] GetJoystickNames();

		// Token: 0x06000B81 RID: 2945
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetMouseButton(int button);

		// Token: 0x06000B82 RID: 2946
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetMouseButtonDown(int button);

		// Token: 0x06000B83 RID: 2947
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetMouseButtonUp(int button);

		// Token: 0x06000B84 RID: 2948
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ResetInputAxes();

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000B85 RID: 2949 RVA: 0x0000F344 File Offset: 0x0000D544
		public static Vector3 mousePosition
		{
			get
			{
				Vector3 result;
				Input.INTERNAL_get_mousePosition(out result);
				return result;
			}
		}

		// Token: 0x06000B86 RID: 2950
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_mousePosition(out Vector3 value);

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000B87 RID: 2951 RVA: 0x0000F35C File Offset: 0x0000D55C
		public static Vector2 mouseScrollDelta
		{
			get
			{
				Vector2 result;
				Input.INTERNAL_get_mouseScrollDelta(out result);
				return result;
			}
		}

		// Token: 0x06000B88 RID: 2952
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_mouseScrollDelta(out Vector2 value);

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000B89 RID: 2953
		public static extern bool mousePresent { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000B8A RID: 2954
		// (set) Token: 0x06000B8B RID: 2955
		public static extern bool simulateMouseWithTouches { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000B8C RID: 2956
		public static extern bool anyKey { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000B8D RID: 2957
		public static extern bool anyKeyDown { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000B8E RID: 2958
		public static extern string inputString { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000B8F RID: 2959 RVA: 0x0000F374 File Offset: 0x0000D574
		public static Vector3 acceleration
		{
			get
			{
				Vector3 result;
				Input.INTERNAL_get_acceleration(out result);
				return result;
			}
		}

		// Token: 0x06000B90 RID: 2960
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_acceleration(out Vector3 value);

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000B91 RID: 2961 RVA: 0x0000F38C File Offset: 0x0000D58C
		public static AccelerationEvent[] accelerationEvents
		{
			get
			{
				int accelerationEventCount = Input.accelerationEventCount;
				AccelerationEvent[] array = new AccelerationEvent[accelerationEventCount];
				for (int i = 0; i < accelerationEventCount; i++)
				{
					array[i] = Input.GetAccelerationEvent(i);
				}
				return array;
			}
		}

		// Token: 0x06000B92 RID: 2962
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern AccelerationEvent GetAccelerationEvent(int index);

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000B93 RID: 2963
		public static extern int accelerationEventCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x0000F3CC File Offset: 0x0000D5CC
		public static Touch[] touches
		{
			get
			{
				int touchCount = Input.touchCount;
				Touch[] array = new Touch[touchCount];
				for (int i = 0; i < touchCount; i++)
				{
					array[i] = Input.GetTouch(i);
				}
				return array;
			}
		}

		// Token: 0x06000B95 RID: 2965
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Touch GetTouch(int index);

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000B96 RID: 2966
		public static extern int touchCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000B97 RID: 2967
		// (set) Token: 0x06000B98 RID: 2968
		[Obsolete("eatKeyPressOnTextFieldFocus property is deprecated, and only provided to support legacy behavior.")]
		public static extern bool eatKeyPressOnTextFieldFocus { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000B99 RID: 2969
		public static extern bool touchPressureSupported { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000B9A RID: 2970
		public static extern bool stylusTouchSupported { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x0000F40C File Offset: 0x0000D60C
		public static bool touchSupported
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000B9C RID: 2972
		// (set) Token: 0x06000B9D RID: 2973
		public static extern bool multiTouchEnabled { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x0000F410 File Offset: 0x0000D610
		public static LocationService location
		{
			get
			{
				if (Input.locationServiceInstance == null)
				{
					Input.locationServiceInstance = new LocationService();
				}
				return Input.locationServiceInstance;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x0000F42C File Offset: 0x0000D62C
		public static Compass compass
		{
			get
			{
				if (Input.compassInstance == null)
				{
					Input.compassInstance = new Compass();
				}
				return Input.compassInstance;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000BA0 RID: 2976
		public static extern DeviceOrientation deviceOrientation { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0000F448 File Offset: 0x0000D648
		[Obsolete("Use ps3 move API instead", true)]
		public static Quaternion GetRotation(int deviceID)
		{
			return Quaternion.identity;
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0000F450 File Offset: 0x0000D650
		[Obsolete("Use ps3 move API instead", true)]
		public static Vector3 GetPosition(int deviceID)
		{
			return Vector3.zero;
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000BA3 RID: 2979
		// (set) Token: 0x06000BA4 RID: 2980
		public static extern IMECompositionMode imeCompositionMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000BA5 RID: 2981
		public static extern string compositionString { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000BA6 RID: 2982
		public static extern bool imeIsSelected { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000BA7 RID: 2983 RVA: 0x0000F458 File Offset: 0x0000D658
		// (set) Token: 0x06000BA8 RID: 2984 RVA: 0x0000F470 File Offset: 0x0000D670
		public static Vector2 compositionCursorPos
		{
			get
			{
				Vector2 result;
				Input.INTERNAL_get_compositionCursorPos(out result);
				return result;
			}
			set
			{
				Input.INTERNAL_set_compositionCursorPos(ref value);
			}
		}

		// Token: 0x06000BA9 RID: 2985
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_compositionCursorPos(out Vector2 value);

		// Token: 0x06000BAA RID: 2986
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_set_compositionCursorPos(ref Vector2 value);

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000BAB RID: 2987
		// (set) Token: 0x06000BAC RID: 2988
		public static extern bool backButtonLeavesApp { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x04000256 RID: 598
		private static Gyroscope m_MainGyro;

		// Token: 0x04000257 RID: 599
		private static LocationService locationServiceInstance;

		// Token: 0x04000258 RID: 600
		private static Compass compassInstance;
	}
}
