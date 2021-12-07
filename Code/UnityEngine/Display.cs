using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000B6 RID: 182
	[UsedByNativeCode]
	public sealed class Display
	{
		// Token: 0x06000AF8 RID: 2808 RVA: 0x0000EE00 File Offset: 0x0000D000
		internal Display()
		{
			this.nativeDisplay = new IntPtr(0);
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0000EE14 File Offset: 0x0000D014
		internal Display(IntPtr nativeDisplay)
		{
			this.nativeDisplay = nativeDisplay;
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0000EE24 File Offset: 0x0000D024
		// Note: this type is marked as 'beforefieldinit'.
		static Display()
		{
			Display.onDisplaysUpdated = null;
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000AFB RID: 2811 RVA: 0x0000EE4C File Offset: 0x0000D04C
		// (remove) Token: 0x06000AFC RID: 2812 RVA: 0x0000EE64 File Offset: 0x0000D064
		public static event Display.DisplaysUpdatedDelegate onDisplaysUpdated;

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x0000EE7C File Offset: 0x0000D07C
		public int renderingWidth
		{
			get
			{
				int result = 0;
				int num = 0;
				Display.GetRenderingExtImpl(this.nativeDisplay, out result, out num);
				return result;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000AFE RID: 2814 RVA: 0x0000EEA0 File Offset: 0x0000D0A0
		public int renderingHeight
		{
			get
			{
				int num = 0;
				int result = 0;
				Display.GetRenderingExtImpl(this.nativeDisplay, out num, out result);
				return result;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x0000EEC4 File Offset: 0x0000D0C4
		public int systemWidth
		{
			get
			{
				int result = 0;
				int num = 0;
				Display.GetSystemExtImpl(this.nativeDisplay, out result, out num);
				return result;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000B00 RID: 2816 RVA: 0x0000EEE8 File Offset: 0x0000D0E8
		public int systemHeight
		{
			get
			{
				int num = 0;
				int result = 0;
				Display.GetSystemExtImpl(this.nativeDisplay, out num, out result);
				return result;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000B01 RID: 2817 RVA: 0x0000EF0C File Offset: 0x0000D10C
		public RenderBuffer colorBuffer
		{
			get
			{
				RenderBuffer result;
				RenderBuffer renderBuffer;
				Display.GetRenderingBuffersImpl(this.nativeDisplay, out result, out renderBuffer);
				return result;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000B02 RID: 2818 RVA: 0x0000EF2C File Offset: 0x0000D12C
		public RenderBuffer depthBuffer
		{
			get
			{
				RenderBuffer renderBuffer;
				RenderBuffer result;
				Display.GetRenderingBuffersImpl(this.nativeDisplay, out renderBuffer, out result);
				return result;
			}
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x0000EF4C File Offset: 0x0000D14C
		public void Activate()
		{
			Display.ActivateDisplayImpl(this.nativeDisplay, 0, 0, 60);
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x0000EF60 File Offset: 0x0000D160
		public void Activate(int width, int height, int refreshRate)
		{
			Display.ActivateDisplayImpl(this.nativeDisplay, width, height, refreshRate);
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x0000EF70 File Offset: 0x0000D170
		public void SetParams(int width, int height, int x, int y)
		{
			Display.SetParamsImpl(this.nativeDisplay, width, height, x, y);
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x0000EF84 File Offset: 0x0000D184
		public void SetRenderingResolution(int w, int h)
		{
			Display.SetRenderingResolutionImpl(this.nativeDisplay, w, h);
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x0000EF94 File Offset: 0x0000D194
		[Obsolete("MultiDisplayLicense has been deprecated.", false)]
		public static bool MultiDisplayLicense()
		{
			return true;
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0000EF98 File Offset: 0x0000D198
		public static Vector3 RelativeMouseAt(Vector3 inputMouseCoordinates)
		{
			int num = 0;
			int num2 = 0;
			int x = (int)inputMouseCoordinates.x;
			int y = (int)inputMouseCoordinates.y;
			Vector3 result;
			result.z = (float)Display.RelativeMouseAtImpl(x, y, out num, out num2);
			result.x = (float)num;
			result.y = (float)num2;
			return result;
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x0000EFE4 File Offset: 0x0000D1E4
		public static Display main
		{
			get
			{
				return Display._mainDisplay;
			}
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0000EFEC File Offset: 0x0000D1EC
		[RequiredByNativeCode]
		private static void RecreateDisplayList(IntPtr[] nativeDisplay)
		{
			Display.displays = new Display[nativeDisplay.Length];
			for (int i = 0; i < nativeDisplay.Length; i++)
			{
				Display.displays[i] = new Display(nativeDisplay[i]);
			}
			Display._mainDisplay = Display.displays[0];
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0000F038 File Offset: 0x0000D238
		[RequiredByNativeCode]
		private static void FireDisplaysUpdated()
		{
			if (Display.onDisplaysUpdated != null)
			{
				Display.onDisplaysUpdated();
			}
		}

		// Token: 0x06000B0C RID: 2828
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSystemExtImpl(IntPtr nativeDisplay, out int w, out int h);

		// Token: 0x06000B0D RID: 2829
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRenderingExtImpl(IntPtr nativeDisplay, out int w, out int h);

		// Token: 0x06000B0E RID: 2830
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRenderingBuffersImpl(IntPtr nativeDisplay, out RenderBuffer color, out RenderBuffer depth);

		// Token: 0x06000B0F RID: 2831
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRenderingResolutionImpl(IntPtr nativeDisplay, int w, int h);

		// Token: 0x06000B10 RID: 2832
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ActivateDisplayImpl(IntPtr nativeDisplay, int width, int height, int refreshRate);

		// Token: 0x06000B11 RID: 2833
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetParamsImpl(IntPtr nativeDisplay, int width, int height, int x, int y);

		// Token: 0x06000B12 RID: 2834
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int RelativeMouseAtImpl(int x, int y, out int rx, out int ry);

		// Token: 0x0400021E RID: 542
		internal IntPtr nativeDisplay;

		// Token: 0x0400021F RID: 543
		public static Display[] displays = new Display[]
		{
			new Display()
		};

		// Token: 0x04000220 RID: 544
		private static Display _mainDisplay = Display.displays[0];

		// Token: 0x02000343 RID: 835
		// (Invoke) Token: 0x06002866 RID: 10342
		public delegate void DisplaysUpdatedDelegate();
	}
}
