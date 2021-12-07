using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000196 RID: 406
	public sealed class WebCamTexture : Texture
	{
		// Token: 0x06001921 RID: 6433 RVA: 0x00018938 File Offset: 0x00016B38
		public WebCamTexture(string deviceName, int requestedWidth, int requestedHeight, int requestedFPS)
		{
			WebCamTexture.Internal_CreateWebCamTexture(this, deviceName, requestedWidth, requestedHeight, requestedFPS);
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x0001894C File Offset: 0x00016B4C
		public WebCamTexture(string deviceName, int requestedWidth, int requestedHeight)
		{
			WebCamTexture.Internal_CreateWebCamTexture(this, deviceName, requestedWidth, requestedHeight, 0);
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x00018960 File Offset: 0x00016B60
		public WebCamTexture(string deviceName)
		{
			WebCamTexture.Internal_CreateWebCamTexture(this, deviceName, 0, 0, 0);
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x00018974 File Offset: 0x00016B74
		public WebCamTexture(int requestedWidth, int requestedHeight, int requestedFPS)
		{
			WebCamTexture.Internal_CreateWebCamTexture(this, string.Empty, requestedWidth, requestedHeight, requestedFPS);
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x0001898C File Offset: 0x00016B8C
		public WebCamTexture(int requestedWidth, int requestedHeight)
		{
			WebCamTexture.Internal_CreateWebCamTexture(this, string.Empty, requestedWidth, requestedHeight, 0);
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x000189A4 File Offset: 0x00016BA4
		public WebCamTexture()
		{
			WebCamTexture.Internal_CreateWebCamTexture(this, string.Empty, 0, 0, 0);
		}

		// Token: 0x06001927 RID: 6439
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateWebCamTexture([Writable] WebCamTexture self, string scriptingDevice, int requestedWidth, int requestedHeight, int maxFramerate);

		// Token: 0x06001928 RID: 6440 RVA: 0x000189BC File Offset: 0x00016BBC
		public void Play()
		{
			WebCamTexture.INTERNAL_CALL_Play(this);
		}

		// Token: 0x06001929 RID: 6441
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Play(WebCamTexture self);

		// Token: 0x0600192A RID: 6442 RVA: 0x000189C4 File Offset: 0x00016BC4
		public void Pause()
		{
			WebCamTexture.INTERNAL_CALL_Pause(this);
		}

		// Token: 0x0600192B RID: 6443
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Pause(WebCamTexture self);

		// Token: 0x0600192C RID: 6444 RVA: 0x000189CC File Offset: 0x00016BCC
		public void Stop()
		{
			WebCamTexture.INTERNAL_CALL_Stop(this);
		}

		// Token: 0x0600192D RID: 6445
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Stop(WebCamTexture self);

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x0600192E RID: 6446
		public extern bool isPlaying { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x0600192F RID: 6447
		// (set) Token: 0x06001930 RID: 6448
		public extern string deviceName { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001931 RID: 6449
		// (set) Token: 0x06001932 RID: 6450
		public extern float requestedFPS { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001933 RID: 6451
		// (set) Token: 0x06001934 RID: 6452
		public extern int requestedWidth { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001935 RID: 6453
		// (set) Token: 0x06001936 RID: 6454
		public extern int requestedHeight { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001937 RID: 6455
		public static extern WebCamDevice[] devices { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001938 RID: 6456 RVA: 0x000189D4 File Offset: 0x00016BD4
		public Color GetPixel(int x, int y)
		{
			Color result;
			WebCamTexture.INTERNAL_CALL_GetPixel(this, x, y, out result);
			return result;
		}

		// Token: 0x06001939 RID: 6457
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetPixel(WebCamTexture self, int x, int y, out Color value);

		// Token: 0x0600193A RID: 6458 RVA: 0x000189EC File Offset: 0x00016BEC
		public Color[] GetPixels()
		{
			return this.GetPixels(0, 0, this.width, this.height);
		}

		// Token: 0x0600193B RID: 6459
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color[] GetPixels(int x, int y, int blockWidth, int blockHeight);

		// Token: 0x0600193C RID: 6460
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color32[] GetPixels32([DefaultValue("null")] Color32[] colors);

		// Token: 0x0600193D RID: 6461 RVA: 0x00018A10 File Offset: 0x00016C10
		[ExcludeFromDocs]
		public Color32[] GetPixels32()
		{
			Color32[] colors = null;
			return this.GetPixels32(colors);
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x0600193E RID: 6462
		public extern int videoRotationAngle { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x0600193F RID: 6463
		public extern bool videoVerticallyMirrored { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06001940 RID: 6464
		public extern bool didUpdateThisFrame { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }
	}
}
