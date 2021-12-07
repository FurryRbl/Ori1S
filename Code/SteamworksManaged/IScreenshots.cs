using System;
using ManagedSteam.CallbackStructures;
using ManagedSteam.SteamTypes;

namespace ManagedSteam
{
	// Token: 0x02000071 RID: 113
	public interface IScreenshots
	{
		// Token: 0x14000047 RID: 71
		// (add) Token: 0x06000383 RID: 899
		// (remove) Token: 0x06000384 RID: 900
		event CallbackEvent<ScreenshotReady> ScreenshotReady;

		// Token: 0x14000048 RID: 72
		// (add) Token: 0x06000385 RID: 901
		// (remove) Token: 0x06000386 RID: 902
		event CallbackEvent<ScreenshotRequested> ScreenshotRequested;

		// Token: 0x06000387 RID: 903
		ScreenshotHandle WriteScreenshot(IntPtr pubRGB, uint cubRGB, int width, int height);

		// Token: 0x06000388 RID: 904
		ScreenshotHandle AddScreenshotToLibrary(string filename, string thumbnailFilename, int width, int height);

		// Token: 0x06000389 RID: 905
		void TriggerScreenshot();

		// Token: 0x0600038A RID: 906
		void HookScreenshots(bool hook);

		// Token: 0x0600038B RID: 907
		bool SetLocation(ScreenshotHandle screenshot, string location);

		// Token: 0x0600038C RID: 908
		bool TagUser(ScreenshotHandle screenshot, SteamID steamID);

		// Token: 0x0600038D RID: 909
		bool TagPublishedFile(ScreenshotHandle screenshot, PublishedFileId publishedFileID);
	}
}
