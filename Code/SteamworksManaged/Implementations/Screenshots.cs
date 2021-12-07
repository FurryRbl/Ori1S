using System;
using System.Collections.Generic;
using ManagedSteam.CallbackStructures;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.Implementations
{
	// Token: 0x02000072 RID: 114
	internal class Screenshots : SteamService, IScreenshots
	{
		// Token: 0x0600038E RID: 910 RVA: 0x00006E34 File Offset: 0x00005034
		public Screenshots()
		{
			this.screenshotReady = new List<ScreenshotReady>();
			this.screenshotRequested = new List<ScreenshotRequested>();
			SteamService.Callbacks[CallbackID.ScreenshotReady] = delegate(IntPtr data, int dataSize)
			{
				this.screenshotReady.Add(ManagedSteam.CallbackStructures.ScreenshotReady.Create(data, dataSize));
			};
			SteamService.Callbacks[CallbackID.ScreenshotRequested] = delegate(IntPtr data, int dataSize)
			{
				this.screenshotRequested.Add(ManagedSteam.CallbackStructures.ScreenshotRequested.Create(data, dataSize));
			};
		}

		// Token: 0x14000049 RID: 73
		// (add) Token: 0x0600038F RID: 911 RVA: 0x00006E9C File Offset: 0x0000509C
		// (remove) Token: 0x06000390 RID: 912 RVA: 0x00006ED4 File Offset: 0x000050D4
		public event CallbackEvent<ScreenshotReady> ScreenshotReady;

		// Token: 0x1400004A RID: 74
		// (add) Token: 0x06000391 RID: 913 RVA: 0x00006F0C File Offset: 0x0000510C
		// (remove) Token: 0x06000392 RID: 914 RVA: 0x00006F44 File Offset: 0x00005144
		public event CallbackEvent<ScreenshotRequested> ScreenshotRequested;

		// Token: 0x06000393 RID: 915 RVA: 0x00006F79 File Offset: 0x00005179
		internal override void CheckIfUsableInternal()
		{
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00006F7B File Offset: 0x0000517B
		internal override void ReleaseManagedResources()
		{
			this.screenshotReady = null;
			this.ScreenshotReady = null;
			this.screenshotRequested = null;
			this.ScreenshotRequested = null;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00006F99 File Offset: 0x00005199
		internal override void InvokeEvents()
		{
			SteamService.InvokeEvents<ScreenshotReady>(this.screenshotReady, this.ScreenshotReady);
			SteamService.InvokeEvents<ScreenshotRequested>(this.screenshotRequested, this.ScreenshotRequested);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00006FBD File Offset: 0x000051BD
		public ScreenshotHandle WriteScreenshot(IntPtr pubRGB, uint cubRGB, int width, int height)
		{
			return new ScreenshotHandle(NativeMethods.Screenshots_WriteScreenshot(pubRGB, cubRGB, width, height));
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00006FCE File Offset: 0x000051CE
		public ScreenshotHandle AddScreenshotToLibrary(string filename, string thumbnailFilename, int width, int height)
		{
			return new ScreenshotHandle(NativeMethods.Screenshots_AddScreenshotToLibrary(filename, thumbnailFilename, width, height));
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00006FDF File Offset: 0x000051DF
		public void TriggerScreenshot()
		{
			NativeMethods.Screenshots_TriggerScreenshot();
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00006FE6 File Offset: 0x000051E6
		public void HookScreenshots(bool hook)
		{
			NativeMethods.Screenshots_HookScreenshots(hook);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00006FEE File Offset: 0x000051EE
		public bool SetLocation(ScreenshotHandle screenshot, string location)
		{
			return NativeMethods.Screenshots_SetLocation(screenshot.AsUInt32, location);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00006FFD File Offset: 0x000051FD
		public bool TagUser(ScreenshotHandle screenshot, SteamID steamID)
		{
			return NativeMethods.Screenshots_TagUser(screenshot.AsUInt32, steamID.AsUInt64);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00007012 File Offset: 0x00005212
		public bool TagPublishedFile(ScreenshotHandle screenshot, PublishedFileId publishedFileID)
		{
			return NativeMethods.Screenshots_TagPublishedFile(screenshot.AsUInt32, publishedFileID.AsUInt64);
		}

		// Token: 0x040001FD RID: 509
		private List<ScreenshotReady> screenshotReady;

		// Token: 0x040001FE RID: 510
		private List<ScreenshotRequested> screenshotRequested;
	}
}
