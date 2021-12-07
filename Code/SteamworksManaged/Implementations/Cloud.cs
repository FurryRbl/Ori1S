using System;
using System.Collections.Generic;
using ManagedSteam.CallbackStructures;
using ManagedSteam.SteamTypes;
using ManagedSteam.Utility;

namespace ManagedSteam.Implementations
{
	// Token: 0x02000139 RID: 313
	internal class Cloud : SteamService, ICloud
	{
		// Token: 0x06000AFD RID: 2813 RVA: 0x0000E584 File Offset: 0x0000C784
		internal Cloud()
		{
			this.listCloudFileShareResult = new List<SteamService.Result<CloudFileShareResult>>();
			this.listCloudDownloadUGCResult = new List<SteamService.Result<CloudDownloadUGCResult>>();
			this.listCloudPublishFileResult = new List<SteamService.Result<CloudPublishFileResult>>();
			this.listCloudUpdatePublishedFileResult = new List<SteamService.Result<CloudUpdatePublishedFileResult>>();
			this.listCloudGetPublishedFileDetailsResult = new List<SteamService.Result<CloudGetPublishedFileDetailsResult>>();
			this.listCloudDeletePublishedFileResult = new List<SteamService.Result<CloudDeletePublishedFileResult>>();
			this.listCloudEnumerateUserPublishedFilesResult = new List<SteamService.Result<CloudEnumerateUserPublishedFilesResult>>();
			this.listCloudSubscribePublishedFileResult = new List<SteamService.Result<CloudSubscribePublishedFileResult>>();
			this.listCloudEnumerateUserSubscribedFilesResult = new List<SteamService.Result<CloudEnumerateUserSubscribedFilesResult>>();
			this.listCloudUnsubscribePublishedFileResult = new List<SteamService.Result<CloudUnsubscribePublishedFileResult>>();
			this.listCloudGetPublishedItemVoteDetailsResult = new List<SteamService.Result<CloudGetPublishedItemVoteDetailsResult>>();
			this.listCloudUpdateUserPublishedItemVoteResult = new List<SteamService.Result<CloudUpdateUserPublishedItemVoteResult>>();
			this.listCloudUserVoteDetailsResult = new List<SteamService.Result<CloudUserVoteDetails>>();
			this.listCloudEnumerateUserSharedWorkshopFilesResult = new List<SteamService.Result<CloudEnumerateUserSharedWorkshopFilesResult>>();
			this.listCloudSetUserPublishedFileActionResult = new List<SteamService.Result<CloudSetUserPublishedFileActionResult>>();
			this.listCloudEnumeratePublishedFilesByUserActionResult = new List<SteamService.Result<CloudEnumeratePublishedFilesByUserActionResult>>();
			this.listCloudEnumerateWorkshopFilesResult = new List<SteamService.Result<CloudEnumerateWorkshopFilesResult>>();
			this.listCloudPublishFileProgress = new List<CloudPublishFileProgress>();
			this.listCloudPublishedFileUpdated = new List<CloudPublishedFileUpdated>();
			SteamService.Results[ResultID.CloudFileShareResult] = delegate(IntPtr data, int size, bool flag)
			{
				this.listCloudFileShareResult.Add(new SteamService.Result<CloudFileShareResult>(ManagedSteam.CallbackStructures.CloudFileShareResult.Create(data, size), flag));
			};
			SteamService.Results[ResultID.CloudDownloadUGCResult] = delegate(IntPtr data, int size, bool flag)
			{
				this.listCloudDownloadUGCResult.Add(new SteamService.Result<CloudDownloadUGCResult>(ManagedSteam.CallbackStructures.CloudDownloadUGCResult.Create(data, size), flag));
			};
			SteamService.Results[ResultID.CloudPublishFileResult] = delegate(IntPtr data, int size, bool flag)
			{
				this.listCloudPublishFileResult.Add(new SteamService.Result<CloudPublishFileResult>(ManagedSteam.CallbackStructures.CloudPublishFileResult.Create(data, size), flag));
			};
			SteamService.Results[ResultID.CloudUpdatePublishedFileResult] = delegate(IntPtr data, int size, bool flag)
			{
				this.listCloudUpdatePublishedFileResult.Add(new SteamService.Result<CloudUpdatePublishedFileResult>(ManagedSteam.CallbackStructures.CloudUpdatePublishedFileResult.Create(data, size), flag));
			};
			SteamService.Results[ResultID.CloudGetPublishedFileDetailsResult] = delegate(IntPtr data, int size, bool flag)
			{
				this.listCloudGetPublishedFileDetailsResult.Add(new SteamService.Result<CloudGetPublishedFileDetailsResult>(ManagedSteam.CallbackStructures.CloudGetPublishedFileDetailsResult.Create(data, size), flag));
			};
			SteamService.Results[ResultID.CloudDeletePublishedFileResult] = delegate(IntPtr data, int size, bool flag)
			{
				this.listCloudDeletePublishedFileResult.Add(new SteamService.Result<CloudDeletePublishedFileResult>(ManagedSteam.CallbackStructures.CloudDeletePublishedFileResult.Create(data, size), flag));
			};
			SteamService.Results[ResultID.CloudEnumerateUserPublishedFilesResult] = delegate(IntPtr data, int size, bool flag)
			{
				this.listCloudEnumerateUserPublishedFilesResult.Add(new SteamService.Result<CloudEnumerateUserPublishedFilesResult>(ManagedSteam.CallbackStructures.CloudEnumerateUserPublishedFilesResult.Create(data, size), flag));
			};
			SteamService.Results[ResultID.CloudSubscribePublishedFileResult] = delegate(IntPtr data, int size, bool flag)
			{
				this.listCloudSubscribePublishedFileResult.Add(new SteamService.Result<CloudSubscribePublishedFileResult>(ManagedSteam.CallbackStructures.CloudSubscribePublishedFileResult.Create(data, size), flag));
			};
			SteamService.Results[ResultID.CloudEnumerateUserSubscribedFilesResult] = delegate(IntPtr data, int size, bool flag)
			{
				this.listCloudEnumerateUserSubscribedFilesResult.Add(new SteamService.Result<CloudEnumerateUserSubscribedFilesResult>(ManagedSteam.CallbackStructures.CloudEnumerateUserSubscribedFilesResult.Create(data, size), flag));
			};
			SteamService.Results[ResultID.CloudUnsubscribePublishedFileResult] = delegate(IntPtr data, int size, bool flag)
			{
				this.listCloudUnsubscribePublishedFileResult.Add(new SteamService.Result<CloudUnsubscribePublishedFileResult>(ManagedSteam.CallbackStructures.CloudUnsubscribePublishedFileResult.Create(data, size), flag));
			};
			SteamService.Results[ResultID.CloudGetPublishedItemVoteDetailsResult] = delegate(IntPtr data, int size, bool flag)
			{
				this.listCloudGetPublishedItemVoteDetailsResult.Add(new SteamService.Result<CloudGetPublishedItemVoteDetailsResult>(ManagedSteam.CallbackStructures.CloudGetPublishedItemVoteDetailsResult.Create(data, size), flag));
			};
			SteamService.Results[ResultID.CloudUpdateUserPublishedItemVoteResult] = delegate(IntPtr data, int size, bool flag)
			{
				this.listCloudUpdateUserPublishedItemVoteResult.Add(new SteamService.Result<CloudUpdateUserPublishedItemVoteResult>(ManagedSteam.CallbackStructures.CloudUpdateUserPublishedItemVoteResult.Create(data, size), flag));
			};
			SteamService.Results[ResultID.CloudUserVoteDetails] = delegate(IntPtr data, int size, bool flag)
			{
				this.listCloudUserVoteDetailsResult.Add(new SteamService.Result<CloudUserVoteDetails>(CloudUserVoteDetails.Create(data, size), flag));
			};
			SteamService.Results[ResultID.CloudEnumerateUserSharedWorkshopFilesResult] = delegate(IntPtr data, int size, bool flag)
			{
				this.listCloudEnumerateUserSharedWorkshopFilesResult.Add(new SteamService.Result<CloudEnumerateUserSharedWorkshopFilesResult>(ManagedSteam.CallbackStructures.CloudEnumerateUserSharedWorkshopFilesResult.Create(data, size), flag));
			};
			SteamService.Results[ResultID.CloudSetUserPublishedFileActionResult] = delegate(IntPtr data, int size, bool flag)
			{
				this.listCloudSetUserPublishedFileActionResult.Add(new SteamService.Result<CloudSetUserPublishedFileActionResult>(ManagedSteam.CallbackStructures.CloudSetUserPublishedFileActionResult.Create(data, size), flag));
			};
			SteamService.Results[ResultID.CloudEnumeratePublishedFilesByUserActionResult] = delegate(IntPtr data, int size, bool flag)
			{
				this.listCloudEnumeratePublishedFilesByUserActionResult.Add(new SteamService.Result<CloudEnumeratePublishedFilesByUserActionResult>(ManagedSteam.CallbackStructures.CloudEnumeratePublishedFilesByUserActionResult.Create(data, size), flag));
			};
			SteamService.Results[ResultID.CloudEnumerateWorkshopFilesResult] = delegate(IntPtr data, int size, bool flag)
			{
				this.listCloudEnumerateWorkshopFilesResult.Add(new SteamService.Result<CloudEnumerateWorkshopFilesResult>(ManagedSteam.CallbackStructures.CloudEnumerateWorkshopFilesResult.Create(data, size), flag));
			};
			SteamService.Callbacks[CallbackID.CloudPublishFileProgress] = delegate(IntPtr data, int size)
			{
				this.listCloudPublishFileProgress.Add(ManagedSteam.CallbackStructures.CloudPublishFileProgress.Create(data, size));
			};
			SteamService.Callbacks[CallbackID.CloudPublishedFileUpdated] = delegate(IntPtr data, int size)
			{
				this.listCloudPublishedFileUpdated.Add(ManagedSteam.CallbackStructures.CloudPublishedFileUpdated.Create(data, size));
			};
		}

		// Token: 0x140000C5 RID: 197
		// (add) Token: 0x06000AFE RID: 2814 RVA: 0x0000E8E8 File Offset: 0x0000CAE8
		// (remove) Token: 0x06000AFF RID: 2815 RVA: 0x0000E920 File Offset: 0x0000CB20
		public event ResultEvent<CloudFileShareResult> CloudFileShareResult;

		// Token: 0x140000C6 RID: 198
		// (add) Token: 0x06000B00 RID: 2816 RVA: 0x0000E958 File Offset: 0x0000CB58
		// (remove) Token: 0x06000B01 RID: 2817 RVA: 0x0000E990 File Offset: 0x0000CB90
		public event ResultEvent<CloudDownloadUGCResult> CloudDownloadUGCResult;

		// Token: 0x140000C7 RID: 199
		// (add) Token: 0x06000B02 RID: 2818 RVA: 0x0000E9C8 File Offset: 0x0000CBC8
		// (remove) Token: 0x06000B03 RID: 2819 RVA: 0x0000EA00 File Offset: 0x0000CC00
		public event ResultEvent<CloudPublishFileResult> CloudPublishFileResult;

		// Token: 0x140000C8 RID: 200
		// (add) Token: 0x06000B04 RID: 2820 RVA: 0x0000EA38 File Offset: 0x0000CC38
		// (remove) Token: 0x06000B05 RID: 2821 RVA: 0x0000EA70 File Offset: 0x0000CC70
		public event ResultEvent<CloudUpdatePublishedFileResult> CloudUpdatePublishedFileResult;

		// Token: 0x140000C9 RID: 201
		// (add) Token: 0x06000B06 RID: 2822 RVA: 0x0000EAA8 File Offset: 0x0000CCA8
		// (remove) Token: 0x06000B07 RID: 2823 RVA: 0x0000EAE0 File Offset: 0x0000CCE0
		public event ResultEvent<CloudGetPublishedFileDetailsResult> CloudGetPublishedFileDetailsResult;

		// Token: 0x140000CA RID: 202
		// (add) Token: 0x06000B08 RID: 2824 RVA: 0x0000EB18 File Offset: 0x0000CD18
		// (remove) Token: 0x06000B09 RID: 2825 RVA: 0x0000EB50 File Offset: 0x0000CD50
		public event ResultEvent<CloudDeletePublishedFileResult> CloudDeletePublishedFileResult;

		// Token: 0x140000CB RID: 203
		// (add) Token: 0x06000B0A RID: 2826 RVA: 0x0000EB88 File Offset: 0x0000CD88
		// (remove) Token: 0x06000B0B RID: 2827 RVA: 0x0000EBC0 File Offset: 0x0000CDC0
		public event ResultEvent<CloudEnumerateUserPublishedFilesResult> CloudEnumerateUserPublishedFilesResult;

		// Token: 0x140000CC RID: 204
		// (add) Token: 0x06000B0C RID: 2828 RVA: 0x0000EBF8 File Offset: 0x0000CDF8
		// (remove) Token: 0x06000B0D RID: 2829 RVA: 0x0000EC30 File Offset: 0x0000CE30
		public event ResultEvent<CloudSubscribePublishedFileResult> CloudSubscribePublishedFileResult;

		// Token: 0x140000CD RID: 205
		// (add) Token: 0x06000B0E RID: 2830 RVA: 0x0000EC68 File Offset: 0x0000CE68
		// (remove) Token: 0x06000B0F RID: 2831 RVA: 0x0000ECA0 File Offset: 0x0000CEA0
		public event ResultEvent<CloudEnumerateUserSubscribedFilesResult> CloudEnumerateUserSubscribedFilesResult;

		// Token: 0x140000CE RID: 206
		// (add) Token: 0x06000B10 RID: 2832 RVA: 0x0000ECD8 File Offset: 0x0000CED8
		// (remove) Token: 0x06000B11 RID: 2833 RVA: 0x0000ED10 File Offset: 0x0000CF10
		public event ResultEvent<CloudUnsubscribePublishedFileResult> CloudUnsubscribePublishedFileResult;

		// Token: 0x140000CF RID: 207
		// (add) Token: 0x06000B12 RID: 2834 RVA: 0x0000ED48 File Offset: 0x0000CF48
		// (remove) Token: 0x06000B13 RID: 2835 RVA: 0x0000ED80 File Offset: 0x0000CF80
		public event ResultEvent<CloudGetPublishedItemVoteDetailsResult> CloudGetPublishedItemVoteDetailsResult;

		// Token: 0x140000D0 RID: 208
		// (add) Token: 0x06000B14 RID: 2836 RVA: 0x0000EDB8 File Offset: 0x0000CFB8
		// (remove) Token: 0x06000B15 RID: 2837 RVA: 0x0000EDF0 File Offset: 0x0000CFF0
		public event ResultEvent<CloudUpdateUserPublishedItemVoteResult> CloudUpdateUserPublishedItemVoteResult;

		// Token: 0x140000D1 RID: 209
		// (add) Token: 0x06000B16 RID: 2838 RVA: 0x0000EE28 File Offset: 0x0000D028
		// (remove) Token: 0x06000B17 RID: 2839 RVA: 0x0000EE60 File Offset: 0x0000D060
		public event ResultEvent<CloudUserVoteDetails> CloudUserVoteDetailsResult;

		// Token: 0x140000D2 RID: 210
		// (add) Token: 0x06000B18 RID: 2840 RVA: 0x0000EE98 File Offset: 0x0000D098
		// (remove) Token: 0x06000B19 RID: 2841 RVA: 0x0000EED0 File Offset: 0x0000D0D0
		public event ResultEvent<CloudEnumerateUserSharedWorkshopFilesResult> CloudEnumerateUserSharedWorkshopFilesResult;

		// Token: 0x140000D3 RID: 211
		// (add) Token: 0x06000B1A RID: 2842 RVA: 0x0000EF08 File Offset: 0x0000D108
		// (remove) Token: 0x06000B1B RID: 2843 RVA: 0x0000EF40 File Offset: 0x0000D140
		public event ResultEvent<CloudSetUserPublishedFileActionResult> CloudSetUserPublishedFileActionResult;

		// Token: 0x140000D4 RID: 212
		// (add) Token: 0x06000B1C RID: 2844 RVA: 0x0000EF78 File Offset: 0x0000D178
		// (remove) Token: 0x06000B1D RID: 2845 RVA: 0x0000EFB0 File Offset: 0x0000D1B0
		public event ResultEvent<CloudEnumeratePublishedFilesByUserActionResult> CloudEnumeratePublishedFilesByUserActionResult;

		// Token: 0x140000D5 RID: 213
		// (add) Token: 0x06000B1E RID: 2846 RVA: 0x0000EFE8 File Offset: 0x0000D1E8
		// (remove) Token: 0x06000B1F RID: 2847 RVA: 0x0000F020 File Offset: 0x0000D220
		public event ResultEvent<CloudEnumerateWorkshopFilesResult> CloudEnumerateWorkshopFilesResult;

		// Token: 0x140000D6 RID: 214
		// (add) Token: 0x06000B20 RID: 2848 RVA: 0x0000F058 File Offset: 0x0000D258
		// (remove) Token: 0x06000B21 RID: 2849 RVA: 0x0000F090 File Offset: 0x0000D290
		public event CallbackEvent<CloudPublishFileProgress> CloudPublishFileProgress;

		// Token: 0x140000D7 RID: 215
		// (add) Token: 0x06000B22 RID: 2850 RVA: 0x0000F0C8 File Offset: 0x0000D2C8
		// (remove) Token: 0x06000B23 RID: 2851 RVA: 0x0000F100 File Offset: 0x0000D300
		public event CallbackEvent<CloudPublishedFileUpdated> CloudPublishedFileUpdated;

		// Token: 0x06000B24 RID: 2852 RVA: 0x0000F135 File Offset: 0x0000D335
		internal override void CheckIfUsableInternal()
		{
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x0000F138 File Offset: 0x0000D338
		internal override void InvokeEvents()
		{
			SteamService.InvokeEvents<CloudFileShareResult>(this.listCloudFileShareResult, this.CloudFileShareResult);
			SteamService.InvokeEvents<CloudDownloadUGCResult>(this.listCloudDownloadUGCResult, this.CloudDownloadUGCResult);
			SteamService.InvokeEvents<CloudPublishFileResult>(this.listCloudPublishFileResult, this.CloudPublishFileResult);
			SteamService.InvokeEvents<CloudUpdatePublishedFileResult>(this.listCloudUpdatePublishedFileResult, this.CloudUpdatePublishedFileResult);
			SteamService.InvokeEvents<CloudGetPublishedFileDetailsResult>(this.listCloudGetPublishedFileDetailsResult, this.CloudGetPublishedFileDetailsResult);
			SteamService.InvokeEvents<CloudDeletePublishedFileResult>(this.listCloudDeletePublishedFileResult, this.CloudDeletePublishedFileResult);
			SteamService.InvokeEvents<CloudEnumerateUserPublishedFilesResult>(this.listCloudEnumerateUserPublishedFilesResult, this.CloudEnumerateUserPublishedFilesResult);
			SteamService.InvokeEvents<CloudSubscribePublishedFileResult>(this.listCloudSubscribePublishedFileResult, this.CloudSubscribePublishedFileResult);
			SteamService.InvokeEvents<CloudEnumerateUserSubscribedFilesResult>(this.listCloudEnumerateUserSubscribedFilesResult, this.CloudEnumerateUserSubscribedFilesResult);
			SteamService.InvokeEvents<CloudUnsubscribePublishedFileResult>(this.listCloudUnsubscribePublishedFileResult, this.CloudUnsubscribePublishedFileResult);
			SteamService.InvokeEvents<CloudGetPublishedItemVoteDetailsResult>(this.listCloudGetPublishedItemVoteDetailsResult, this.CloudGetPublishedItemVoteDetailsResult);
			SteamService.InvokeEvents<CloudUpdateUserPublishedItemVoteResult>(this.listCloudUpdateUserPublishedItemVoteResult, this.CloudUpdateUserPublishedItemVoteResult);
			SteamService.InvokeEvents<CloudUserVoteDetails>(this.listCloudUserVoteDetailsResult, this.CloudUserVoteDetailsResult);
			SteamService.InvokeEvents<CloudEnumerateUserSharedWorkshopFilesResult>(this.listCloudEnumerateUserSharedWorkshopFilesResult, this.CloudEnumerateUserSharedWorkshopFilesResult);
			SteamService.InvokeEvents<CloudSetUserPublishedFileActionResult>(this.listCloudSetUserPublishedFileActionResult, this.CloudSetUserPublishedFileActionResult);
			SteamService.InvokeEvents<CloudEnumeratePublishedFilesByUserActionResult>(this.listCloudEnumeratePublishedFilesByUserActionResult, this.CloudEnumeratePublishedFilesByUserActionResult);
			SteamService.InvokeEvents<CloudEnumerateWorkshopFilesResult>(this.listCloudEnumerateWorkshopFilesResult, this.CloudEnumerateWorkshopFilesResult);
			SteamService.InvokeEvents<CloudPublishFileProgress>(this.listCloudPublishFileProgress, this.CloudPublishFileProgress);
			SteamService.InvokeEvents<CloudPublishedFileUpdated>(this.listCloudPublishedFileUpdated, this.CloudPublishedFileUpdated);
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0000F288 File Offset: 0x0000D488
		internal override void ReleaseManagedResources()
		{
			this.listCloudFileShareResult = null;
			this.listCloudDownloadUGCResult = null;
			this.listCloudPublishFileResult = null;
			this.listCloudUpdatePublishedFileResult = null;
			this.listCloudGetPublishedFileDetailsResult = null;
			this.listCloudDeletePublishedFileResult = null;
			this.listCloudEnumerateUserPublishedFilesResult = null;
			this.listCloudSubscribePublishedFileResult = null;
			this.listCloudEnumerateUserSubscribedFilesResult = null;
			this.listCloudUnsubscribePublishedFileResult = null;
			this.listCloudGetPublishedItemVoteDetailsResult = null;
			this.listCloudUpdateUserPublishedItemVoteResult = null;
			this.listCloudUserVoteDetailsResult = null;
			this.listCloudEnumerateUserSharedWorkshopFilesResult = null;
			this.listCloudSetUserPublishedFileActionResult = null;
			this.listCloudEnumeratePublishedFilesByUserActionResult = null;
			this.listCloudEnumerateWorkshopFilesResult = null;
			this.listCloudPublishFileProgress = null;
			this.listCloudPublishedFileUpdated = null;
			this.CloudFileShareResult = null;
			this.CloudDownloadUGCResult = null;
			this.CloudPublishFileResult = null;
			this.CloudUpdatePublishedFileResult = null;
			this.CloudGetPublishedFileDetailsResult = null;
			this.CloudDeletePublishedFileResult = null;
			this.CloudEnumerateUserPublishedFilesResult = null;
			this.CloudSubscribePublishedFileResult = null;
			this.CloudEnumerateUserSubscribedFilesResult = null;
			this.CloudUnsubscribePublishedFileResult = null;
			this.CloudGetPublishedItemVoteDetailsResult = null;
			this.CloudUpdateUserPublishedItemVoteResult = null;
			this.CloudUserVoteDetailsResult = null;
			this.CloudEnumerateUserSharedWorkshopFilesResult = null;
			this.CloudSetUserPublishedFileActionResult = null;
			this.CloudEnumeratePublishedFilesByUserActionResult = null;
			this.CloudEnumerateWorkshopFilesResult = null;
			this.CloudPublishFileProgress = null;
			this.CloudPublishedFileUpdated = null;
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0000F39F File Offset: 0x0000D59F
		public bool Write(string fileName, IntPtr data, int dataSize)
		{
			base.CheckIfUsable();
			return NativeMethods.Cloud_Write(fileName, data, dataSize);
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x0000F3AF File Offset: 0x0000D5AF
		public int Read(string fileName, IntPtr data, int dataToRead)
		{
			base.CheckIfUsable();
			return NativeMethods.Cloud_Read(fileName, data, dataToRead);
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0000F3BF File Offset: 0x0000D5BF
		public bool Forget(string fileName)
		{
			base.CheckIfUsable();
			return NativeMethods.Cloud_Forget(fileName);
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0000F3CD File Offset: 0x0000D5CD
		public bool Delete(string fileName)
		{
			base.CheckIfUsable();
			return NativeMethods.Cloud_Delete(fileName);
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0000F3DB File Offset: 0x0000D5DB
		public void Share(string fileName)
		{
			base.CheckIfUsable();
			NativeMethods.Cloud_Share(fileName);
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0000F3E9 File Offset: 0x0000D5E9
		public bool SetSyncPlatforms(string fileName, RemoteStoragePlatform remoteStoragePlatform)
		{
			base.CheckIfUsable();
			return NativeMethods.Cloud_SetSyncPlatforms(fileName, (int)remoteStoragePlatform);
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0000F3F8 File Offset: 0x0000D5F8
		public bool Exists(string fileName)
		{
			base.CheckIfUsable();
			return NativeMethods.Cloud_Exists(fileName);
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0000F406 File Offset: 0x0000D606
		public bool Persisted(string fileName)
		{
			base.CheckIfUsable();
			return NativeMethods.Cloud_Persisted(fileName);
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0000F414 File Offset: 0x0000D614
		public int GetSize(string fileName)
		{
			base.CheckIfUsable();
			return NativeMethods.Cloud_GetSize(fileName);
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0000F422 File Offset: 0x0000D622
		public long Timestamp(string fileName)
		{
			base.CheckIfUsable();
			return NativeMethods.Cloud_Timestamp(fileName);
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0000F430 File Offset: 0x0000D630
		public RemoteStoragePlatform GetSyncPlatforms(string fileName)
		{
			base.CheckIfUsable();
			return (RemoteStoragePlatform)NativeMethods.Cloud_GetSyncPlatforms(fileName);
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0000F43E File Offset: 0x0000D63E
		public int GetFileCount()
		{
			base.CheckIfUsable();
			return NativeMethods.Cloud_GetFileCount();
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0000F44C File Offset: 0x0000D64C
		public string GetFileNameAndSize(int fileID, out int fileSize)
		{
			base.CheckIfUsable();
			fileSize = 0;
			IntPtr pointer = NativeMethods.Cloud_GetFileNameAndSize(fileID, ref fileSize);
			return NativeHelpers.ToStringAnsi(pointer);
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0000F470 File Offset: 0x0000D670
		public CloudGetFileNameAndSizeResult GetFileNameAndSize(int fileID)
		{
			CloudGetFileNameAndSizeResult result = default(CloudGetFileNameAndSizeResult);
			result.result = this.GetFileNameAndSize(fileID, out result.sender);
			return result;
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0000F49B File Offset: 0x0000D69B
		public bool GetQuota(out int totalBytes, out int availableBytes)
		{
			base.CheckIfUsable();
			totalBytes = 0;
			availableBytes = 0;
			return NativeMethods.Cloud_GetQuota(ref totalBytes, ref availableBytes);
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0000F4B0 File Offset: 0x0000D6B0
		public CloudGetQuotaResult GetQuota()
		{
			CloudGetQuotaResult result = default(CloudGetQuotaResult);
			result.result = this.GetQuota(out result.totalBytesSender, out result.availableBytesSender);
			return result;
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x0000F4E1 File Offset: 0x0000D6E1
		public bool IsEnabledForAccount()
		{
			base.CheckIfUsable();
			return NativeMethods.Cloud_IsEnabledForAccount();
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x0000F4EE File Offset: 0x0000D6EE
		public bool IsEnabledForApplication()
		{
			base.CheckIfUsable();
			return NativeMethods.Cloud_IsEnabledForApplication();
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0000F4FB File Offset: 0x0000D6FB
		public void SetEnabledForApplication(bool enabled)
		{
			base.CheckIfUsable();
			NativeMethods.Cloud_SetEnabledForApplication(enabled);
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0000F509 File Offset: 0x0000D709
		public void UGCDownload(UGCHandle handle, uint unPriority)
		{
			base.CheckIfUsable();
			NativeMethods.Cloud_UGCDownload(handle.AsUInt64, unPriority);
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x0000F51E File Offset: 0x0000D71E
		public bool GetUGCDownloadProgress(UGCHandle handle, out int bytesDownloaded, out int bytesExpected)
		{
			base.CheckIfUsable();
			bytesDownloaded = 0;
			bytesExpected = 0;
			return NativeMethods.Cloud_GetUGCDownloadProgress(handle.AsUInt64, ref bytesDownloaded, ref bytesExpected);
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x0000F53C File Offset: 0x0000D73C
		public CloudGetUGCDownloadProgressResult GetUGCDownloadProgress(UGCHandle handle)
		{
			CloudGetUGCDownloadProgressResult result = default(CloudGetUGCDownloadProgressResult);
			result.result = this.GetUGCDownloadProgress(handle, out result.bytesDownloadedSender, out result.bytesExpectedSender);
			return result;
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x0000F570 File Offset: 0x0000D770
		public bool GetUGCDetails(UGCHandle handle, out AppID appID, out string name, out int fileSize, out SteamID creator)
		{
			base.CheckIfUsable();
			IntPtr zero = IntPtr.Zero;
			uint value = 0U;
			ulong value2 = 0UL;
			fileSize = 0;
			bool result = NativeMethods.Cloud_GetUGCDetails(handle.AsUInt64, ref value, ref zero, ref fileSize, ref value2);
			appID = new AppID(value);
			name = NativeHelpers.ToStringAnsi(zero);
			creator = new SteamID(value2);
			return result;
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0000F5CC File Offset: 0x0000D7CC
		public CloudGetUGCDetailsResult GetUGCDetails(UGCHandle handle)
		{
			CloudGetUGCDetailsResult result = default(CloudGetUGCDetailsResult);
			result.result = this.GetUGCDetails(handle, out result.appIDSender, out result.nameSender, out result.fileSizeSender, out result.creatorSender);
			return result;
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0000F60C File Offset: 0x0000D80C
		public int UGCRead(UGCHandle handle, byte[] data, uint offset, UGCReadAction action)
		{
			base.CheckIfUsable();
			int result;
			using (NativeBuffer nativeBuffer = new NativeBuffer(data))
			{
				int num = NativeMethods.Cloud_UGCRead(handle.AsUInt64, nativeBuffer.UnmanagedMemory, nativeBuffer.UnmanagedSize, offset, (int)action);
				nativeBuffer.ReadFromUnmanagedMemory(num);
				result = num;
			}
			return result;
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x0000F668 File Offset: 0x0000D868
		public int GetCachedUGCCount()
		{
			base.CheckIfUsable();
			return NativeMethods.Cloud_GetCachedUGCCount();
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0000F675 File Offset: 0x0000D875
		public UGCHandle GetUGCHandle(int handleID)
		{
			base.CheckIfUsable();
			return new UGCHandle(NativeMethods.Cloud_GetUGCHandle(handleID));
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0000F688 File Offset: 0x0000D888
		public void PublishWorkshopFile(string fileName, string previewFile, AppID consumerAppId, string title, string description, RemoteStoragePublishedFileVisibility visibility, IList<string> tags, WorkshopFileType workshopFileType)
		{
			base.CheckIfUsable();
			using (SteamParamStringArray steamParamStringArray = new SteamParamStringArray(tags))
			{
				NativeMethods.Cloud_PublishWorkshopFile(fileName, previewFile, consumerAppId.AsUInt32, title, description, (int)visibility, steamParamStringArray.UnmanagedMemory, (int)workshopFileType);
			}
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0000F6DC File Offset: 0x0000D8DC
		public PublishedFileUpdateHandle CreatePublishedFileUpdateRequest(PublishedFileId publishedFileId)
		{
			base.CheckIfUsable();
			return new PublishedFileUpdateHandle(NativeMethods.Cloud_CreatePublishedFileUpdateRequest(publishedFileId.AsUInt64));
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x0000F6F5 File Offset: 0x0000D8F5
		public bool UpdatePublishedFileFile(PublishedFileUpdateHandle updateHandle, string fileName)
		{
			base.CheckIfUsable();
			return NativeMethods.Cloud_UpdatePublishedFileFile(updateHandle.AsUInt64, fileName);
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0000F70A File Offset: 0x0000D90A
		public bool UpdatePublishedFilePreviewFile(PublishedFileUpdateHandle updateHandle, string previewFile)
		{
			base.CheckIfUsable();
			return NativeMethods.Cloud_UpdatePublishedFilePreviewFile(updateHandle.AsUInt64, previewFile);
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x0000F71F File Offset: 0x0000D91F
		public bool UpdatePublishedFileTitle(PublishedFileUpdateHandle updateHandle, string title)
		{
			base.CheckIfUsable();
			return NativeMethods.Cloud_UpdatePublishedFileTitle(updateHandle.AsUInt64, title);
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x0000F734 File Offset: 0x0000D934
		public bool UpdatePublishedFileDescription(PublishedFileUpdateHandle updateHandle, string description)
		{
			base.CheckIfUsable();
			return NativeMethods.Cloud_UpdatePublishedFileDescription(updateHandle.AsUInt64, description);
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x0000F749 File Offset: 0x0000D949
		public bool UpdatePublishedFileVisibility(PublishedFileUpdateHandle updateHandle, RemoteStoragePublishedFileVisibility visibility)
		{
			base.CheckIfUsable();
			return NativeMethods.Cloud_UpdatePublishedFileVisibility(updateHandle.AsUInt64, (int)visibility);
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x0000F760 File Offset: 0x0000D960
		public bool UpdatePublishedFileTags(PublishedFileUpdateHandle updateHandle, IList<string> tags)
		{
			base.CheckIfUsable();
			bool result;
			using (SteamParamStringArray steamParamStringArray = new SteamParamStringArray(tags))
			{
				result = NativeMethods.Cloud_UpdatePublishedFileTags(updateHandle.AsUInt64, steamParamStringArray.UnmanagedMemory);
			}
			return result;
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x0000F7AC File Offset: 0x0000D9AC
		public void CommitPublishedFileUpdate(PublishedFileUpdateHandle updateHandle)
		{
			base.CheckIfUsable();
			NativeMethods.Cloud_CommitPublishedFileUpdate(updateHandle.AsUInt64);
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x0000F7C0 File Offset: 0x0000D9C0
		public void GetPublishedFileDetails(PublishedFileId publishedFileId, uint maxSecondsOld)
		{
			base.CheckIfUsable();
			NativeMethods.Cloud_GetPublishedFileDetails(publishedFileId.AsUInt64, maxSecondsOld);
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0000F7D5 File Offset: 0x0000D9D5
		public void DeletePublishedFile(PublishedFileId publishedFileId)
		{
			base.CheckIfUsable();
			NativeMethods.Cloud_DeletePublishedFile(publishedFileId.AsUInt64);
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0000F7E9 File Offset: 0x0000D9E9
		public void EnumerateUserPublishedFiles(int startIndex)
		{
			base.CheckIfUsable();
			NativeMethods.Cloud_EnumerateUserPublishedFiles((uint)startIndex);
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0000F7F7 File Offset: 0x0000D9F7
		public void SubscribePublishedFile(PublishedFileId publishedFileId)
		{
			base.CheckIfUsable();
			NativeMethods.Cloud_SubscribePublishedFile(publishedFileId.AsUInt64);
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0000F80B File Offset: 0x0000DA0B
		public void EnumerateUserSubscribedFiles(int startIndex)
		{
			base.CheckIfUsable();
			NativeMethods.Cloud_EnumerateUserSubscribedFiles((uint)startIndex);
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x0000F819 File Offset: 0x0000DA19
		public void UnsubscribePublishedFile(PublishedFileId publishedFileId)
		{
			base.CheckIfUsable();
			NativeMethods.Cloud_UnsubscribePublishedFile(publishedFileId.AsUInt64);
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0000F82D File Offset: 0x0000DA2D
		public bool UpdatePublishedFileSetChangeDescription(PublishedFileUpdateHandle updateHandle, string changeDescription)
		{
			base.CheckIfUsable();
			return NativeMethods.Cloud_UpdatePublishedFileSetChangeDescription(updateHandle.AsUInt64, changeDescription);
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0000F842 File Offset: 0x0000DA42
		public void GetPublishedItemVoteDetails(PublishedFileId publishedFileId)
		{
			base.CheckIfUsable();
			NativeMethods.Cloud_GetPublishedItemVoteDetails(publishedFileId.AsUInt64);
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0000F856 File Offset: 0x0000DA56
		public void UpdateUserPublishedItemVote(PublishedFileId publishedFileId, bool voteUp)
		{
			base.CheckIfUsable();
			NativeMethods.Cloud_UpdateUserPublishedItemVote(publishedFileId.AsUInt64, voteUp);
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x0000F86B File Offset: 0x0000DA6B
		public void GetUserPublishedItemVoteDetails(PublishedFileId publishedFileId)
		{
			base.CheckIfUsable();
			NativeMethods.Cloud_GetUserPublishedItemVoteDetails(publishedFileId.AsUInt64);
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x0000F880 File Offset: 0x0000DA80
		public void EnumerateUserSharedWorkshopFiles(SteamID steamId, int startIndex, IList<string> requiredTags, IList<string> excludedTags)
		{
			base.CheckIfUsable();
			using (SteamParamStringArray steamParamStringArray = new SteamParamStringArray(requiredTags))
			{
				using (SteamParamStringArray steamParamStringArray2 = new SteamParamStringArray(excludedTags))
				{
					NativeMethods.Cloud_EnumerateUserSharedWorkshopFiles(steamId.AsUInt64, (uint)startIndex, steamParamStringArray.UnmanagedMemory, steamParamStringArray2.UnmanagedMemory);
				}
			}
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0000F8F0 File Offset: 0x0000DAF0
		public void PublishVideo(WorkshopVideoProviders videoProviders, string videoAccount, string videoIdentifier, string previewFile, AppID consumerAppId, string title, string description, RemoteStoragePublishedFileVisibility visibility, IList<string> tags)
		{
			base.CheckIfUsable();
			using (SteamParamStringArray steamParamStringArray = new SteamParamStringArray(tags))
			{
				NativeMethods.Cloud_PublishVideo((int)videoProviders, videoAccount, videoIdentifier, previewFile, consumerAppId.AsUInt32, title, description, (int)visibility, steamParamStringArray.UnmanagedMemory);
			}
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x0000F944 File Offset: 0x0000DB44
		public void SetUserPublishedFileAction(PublishedFileId publishedFileId, WorkshopFileAction action)
		{
			base.CheckIfUsable();
			NativeMethods.Cloud_SetUserPublishedFileAction(publishedFileId.AsUInt64, (int)action);
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x0000F959 File Offset: 0x0000DB59
		public void EnumeratePublishedFilesByUserAction(WorkshopFileAction action, int startIndex)
		{
			base.CheckIfUsable();
			NativeMethods.Cloud_EnumeratePublishedFilesByUserAction((int)action, (uint)startIndex);
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0000F968 File Offset: 0x0000DB68
		public void EnumeratePublishedWorkshopFiles(WorkshopFileAction enumerationType, int startIndex, int count, int days, IList<string> tags, IList<string> userTags)
		{
			base.CheckIfUsable();
			using (SteamParamStringArray steamParamStringArray = new SteamParamStringArray(tags))
			{
				using (SteamParamStringArray steamParamStringArray2 = new SteamParamStringArray(userTags))
				{
					NativeMethods.Cloud_EnumeratePublishedWorkshopFiles((int)enumerationType, (uint)startIndex, (uint)count, (uint)days, steamParamStringArray.UnmanagedMemory, steamParamStringArray2.UnmanagedMemory);
				}
			}
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0000F9D8 File Offset: 0x0000DBD8
		public void UGCDownloadToLocation(ulong content, string location, uint priority)
		{
			base.CheckIfUsable();
			NativeMethods.Cloud_UGCDownloadToLocation(content, location, priority);
		}

		// Token: 0x0400058B RID: 1419
		private List<SteamService.Result<CloudFileShareResult>> listCloudFileShareResult;

		// Token: 0x0400058C RID: 1420
		private List<SteamService.Result<CloudDownloadUGCResult>> listCloudDownloadUGCResult;

		// Token: 0x0400058D RID: 1421
		private List<SteamService.Result<CloudPublishFileResult>> listCloudPublishFileResult;

		// Token: 0x0400058E RID: 1422
		private List<SteamService.Result<CloudUpdatePublishedFileResult>> listCloudUpdatePublishedFileResult;

		// Token: 0x0400058F RID: 1423
		private List<SteamService.Result<CloudGetPublishedFileDetailsResult>> listCloudGetPublishedFileDetailsResult;

		// Token: 0x04000590 RID: 1424
		private List<SteamService.Result<CloudDeletePublishedFileResult>> listCloudDeletePublishedFileResult;

		// Token: 0x04000591 RID: 1425
		private List<SteamService.Result<CloudEnumerateUserPublishedFilesResult>> listCloudEnumerateUserPublishedFilesResult;

		// Token: 0x04000592 RID: 1426
		private List<SteamService.Result<CloudSubscribePublishedFileResult>> listCloudSubscribePublishedFileResult;

		// Token: 0x04000593 RID: 1427
		private List<SteamService.Result<CloudEnumerateUserSubscribedFilesResult>> listCloudEnumerateUserSubscribedFilesResult;

		// Token: 0x04000594 RID: 1428
		private List<SteamService.Result<CloudUnsubscribePublishedFileResult>> listCloudUnsubscribePublishedFileResult;

		// Token: 0x04000595 RID: 1429
		private List<SteamService.Result<CloudGetPublishedItemVoteDetailsResult>> listCloudGetPublishedItemVoteDetailsResult;

		// Token: 0x04000596 RID: 1430
		private List<SteamService.Result<CloudUpdateUserPublishedItemVoteResult>> listCloudUpdateUserPublishedItemVoteResult;

		// Token: 0x04000597 RID: 1431
		private List<SteamService.Result<CloudUserVoteDetails>> listCloudUserVoteDetailsResult;

		// Token: 0x04000598 RID: 1432
		private List<SteamService.Result<CloudEnumerateUserSharedWorkshopFilesResult>> listCloudEnumerateUserSharedWorkshopFilesResult;

		// Token: 0x04000599 RID: 1433
		private List<SteamService.Result<CloudSetUserPublishedFileActionResult>> listCloudSetUserPublishedFileActionResult;

		// Token: 0x0400059A RID: 1434
		private List<SteamService.Result<CloudEnumeratePublishedFilesByUserActionResult>> listCloudEnumeratePublishedFilesByUserActionResult;

		// Token: 0x0400059B RID: 1435
		private List<SteamService.Result<CloudEnumerateWorkshopFilesResult>> listCloudEnumerateWorkshopFilesResult;

		// Token: 0x0400059C RID: 1436
		private List<CloudPublishFileProgress> listCloudPublishFileProgress;

		// Token: 0x0400059D RID: 1437
		private List<CloudPublishedFileUpdated> listCloudPublishedFileUpdated;
	}
}
