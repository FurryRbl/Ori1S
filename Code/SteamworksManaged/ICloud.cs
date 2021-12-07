using System;
using System.Collections.Generic;
using ManagedSteam.CallbackStructures;
using ManagedSteam.SteamTypes;

namespace ManagedSteam
{
	// Token: 0x020000E1 RID: 225
	public interface ICloud
	{
		// Token: 0x14000076 RID: 118
		// (add) Token: 0x060005EF RID: 1519
		// (remove) Token: 0x060005F0 RID: 1520
		event ResultEvent<CloudFileShareResult> CloudFileShareResult;

		// Token: 0x14000077 RID: 119
		// (add) Token: 0x060005F1 RID: 1521
		// (remove) Token: 0x060005F2 RID: 1522
		event ResultEvent<CloudDownloadUGCResult> CloudDownloadUGCResult;

		// Token: 0x14000078 RID: 120
		// (add) Token: 0x060005F3 RID: 1523
		// (remove) Token: 0x060005F4 RID: 1524
		event ResultEvent<CloudPublishFileResult> CloudPublishFileResult;

		// Token: 0x14000079 RID: 121
		// (add) Token: 0x060005F5 RID: 1525
		// (remove) Token: 0x060005F6 RID: 1526
		event ResultEvent<CloudUpdatePublishedFileResult> CloudUpdatePublishedFileResult;

		// Token: 0x1400007A RID: 122
		// (add) Token: 0x060005F7 RID: 1527
		// (remove) Token: 0x060005F8 RID: 1528
		event ResultEvent<CloudGetPublishedFileDetailsResult> CloudGetPublishedFileDetailsResult;

		// Token: 0x1400007B RID: 123
		// (add) Token: 0x060005F9 RID: 1529
		// (remove) Token: 0x060005FA RID: 1530
		event ResultEvent<CloudDeletePublishedFileResult> CloudDeletePublishedFileResult;

		// Token: 0x1400007C RID: 124
		// (add) Token: 0x060005FB RID: 1531
		// (remove) Token: 0x060005FC RID: 1532
		event ResultEvent<CloudEnumerateUserPublishedFilesResult> CloudEnumerateUserPublishedFilesResult;

		// Token: 0x1400007D RID: 125
		// (add) Token: 0x060005FD RID: 1533
		// (remove) Token: 0x060005FE RID: 1534
		event ResultEvent<CloudSubscribePublishedFileResult> CloudSubscribePublishedFileResult;

		// Token: 0x1400007E RID: 126
		// (add) Token: 0x060005FF RID: 1535
		// (remove) Token: 0x06000600 RID: 1536
		event ResultEvent<CloudEnumerateUserSubscribedFilesResult> CloudEnumerateUserSubscribedFilesResult;

		// Token: 0x1400007F RID: 127
		// (add) Token: 0x06000601 RID: 1537
		// (remove) Token: 0x06000602 RID: 1538
		event ResultEvent<CloudUnsubscribePublishedFileResult> CloudUnsubscribePublishedFileResult;

		// Token: 0x14000080 RID: 128
		// (add) Token: 0x06000603 RID: 1539
		// (remove) Token: 0x06000604 RID: 1540
		event ResultEvent<CloudGetPublishedItemVoteDetailsResult> CloudGetPublishedItemVoteDetailsResult;

		// Token: 0x14000081 RID: 129
		// (add) Token: 0x06000605 RID: 1541
		// (remove) Token: 0x06000606 RID: 1542
		event ResultEvent<CloudUpdateUserPublishedItemVoteResult> CloudUpdateUserPublishedItemVoteResult;

		// Token: 0x14000082 RID: 130
		// (add) Token: 0x06000607 RID: 1543
		// (remove) Token: 0x06000608 RID: 1544
		event ResultEvent<CloudUserVoteDetails> CloudUserVoteDetailsResult;

		// Token: 0x14000083 RID: 131
		// (add) Token: 0x06000609 RID: 1545
		// (remove) Token: 0x0600060A RID: 1546
		event ResultEvent<CloudEnumerateUserSharedWorkshopFilesResult> CloudEnumerateUserSharedWorkshopFilesResult;

		// Token: 0x14000084 RID: 132
		// (add) Token: 0x0600060B RID: 1547
		// (remove) Token: 0x0600060C RID: 1548
		event ResultEvent<CloudSetUserPublishedFileActionResult> CloudSetUserPublishedFileActionResult;

		// Token: 0x14000085 RID: 133
		// (add) Token: 0x0600060D RID: 1549
		// (remove) Token: 0x0600060E RID: 1550
		event ResultEvent<CloudEnumeratePublishedFilesByUserActionResult> CloudEnumeratePublishedFilesByUserActionResult;

		// Token: 0x14000086 RID: 134
		// (add) Token: 0x0600060F RID: 1551
		// (remove) Token: 0x06000610 RID: 1552
		event ResultEvent<CloudEnumerateWorkshopFilesResult> CloudEnumerateWorkshopFilesResult;

		// Token: 0x14000087 RID: 135
		// (add) Token: 0x06000611 RID: 1553
		// (remove) Token: 0x06000612 RID: 1554
		event CallbackEvent<CloudPublishFileProgress> CloudPublishFileProgress;

		// Token: 0x14000088 RID: 136
		// (add) Token: 0x06000613 RID: 1555
		// (remove) Token: 0x06000614 RID: 1556
		event CallbackEvent<CloudPublishedFileUpdated> CloudPublishedFileUpdated;

		// Token: 0x06000615 RID: 1557
		bool Write(string fileName, IntPtr data, int dataSize);

		// Token: 0x06000616 RID: 1558
		int Read(string fileName, IntPtr data, int dataToRead);

		// Token: 0x06000617 RID: 1559
		bool Forget(string fileName);

		// Token: 0x06000618 RID: 1560
		bool Delete(string fileName);

		// Token: 0x06000619 RID: 1561
		void Share(string fileName);

		// Token: 0x0600061A RID: 1562
		bool SetSyncPlatforms(string fileName, RemoteStoragePlatform remoteStoragePlatform);

		// Token: 0x0600061B RID: 1563
		bool Exists(string fileName);

		// Token: 0x0600061C RID: 1564
		bool Persisted(string fileName);

		// Token: 0x0600061D RID: 1565
		int GetSize(string fileName);

		// Token: 0x0600061E RID: 1566
		long Timestamp(string fileName);

		// Token: 0x0600061F RID: 1567
		RemoteStoragePlatform GetSyncPlatforms(string fileName);

		// Token: 0x06000620 RID: 1568
		int GetFileCount();

		// Token: 0x06000621 RID: 1569
		string GetFileNameAndSize(int fileID, out int fileSize);

		// Token: 0x06000622 RID: 1570
		CloudGetFileNameAndSizeResult GetFileNameAndSize(int fileID);

		// Token: 0x06000623 RID: 1571
		bool GetQuota(out int totalBytes, out int availableBytes);

		// Token: 0x06000624 RID: 1572
		CloudGetQuotaResult GetQuota();

		// Token: 0x06000625 RID: 1573
		bool IsEnabledForAccount();

		// Token: 0x06000626 RID: 1574
		bool IsEnabledForApplication();

		// Token: 0x06000627 RID: 1575
		void SetEnabledForApplication(bool enabled);

		// Token: 0x06000628 RID: 1576
		void UGCDownload(UGCHandle handle, uint unPriority);

		// Token: 0x06000629 RID: 1577
		bool GetUGCDownloadProgress(UGCHandle handle, out int bytesDownloaded, out int bytesExpected);

		// Token: 0x0600062A RID: 1578
		CloudGetUGCDownloadProgressResult GetUGCDownloadProgress(UGCHandle handle);

		// Token: 0x0600062B RID: 1579
		bool GetUGCDetails(UGCHandle handle, out AppID appID, out string name, out int fileSize, out SteamID creator);

		// Token: 0x0600062C RID: 1580
		CloudGetUGCDetailsResult GetUGCDetails(UGCHandle handle);

		// Token: 0x0600062D RID: 1581
		int UGCRead(UGCHandle handle, byte[] data, uint offset, UGCReadAction action);

		// Token: 0x0600062E RID: 1582
		int GetCachedUGCCount();

		// Token: 0x0600062F RID: 1583
		UGCHandle GetUGCHandle(int handleID);

		// Token: 0x06000630 RID: 1584
		void PublishWorkshopFile(string fileName, string previewFile, AppID consumerAppId, string title, string description, RemoteStoragePublishedFileVisibility visibility, IList<string> tags, WorkshopFileType workshopFileType);

		// Token: 0x06000631 RID: 1585
		PublishedFileUpdateHandle CreatePublishedFileUpdateRequest(PublishedFileId publishedFileId);

		// Token: 0x06000632 RID: 1586
		bool UpdatePublishedFileFile(PublishedFileUpdateHandle updateHandle, string fileName);

		// Token: 0x06000633 RID: 1587
		bool UpdatePublishedFilePreviewFile(PublishedFileUpdateHandle updateHandle, string previewFile);

		// Token: 0x06000634 RID: 1588
		bool UpdatePublishedFileTitle(PublishedFileUpdateHandle updateHandle, string title);

		// Token: 0x06000635 RID: 1589
		bool UpdatePublishedFileDescription(PublishedFileUpdateHandle updateHandle, string description);

		// Token: 0x06000636 RID: 1590
		bool UpdatePublishedFileVisibility(PublishedFileUpdateHandle updateHandle, RemoteStoragePublishedFileVisibility visibility);

		// Token: 0x06000637 RID: 1591
		bool UpdatePublishedFileTags(PublishedFileUpdateHandle updateHandle, IList<string> tags);

		// Token: 0x06000638 RID: 1592
		void CommitPublishedFileUpdate(PublishedFileUpdateHandle updateHandle);

		// Token: 0x06000639 RID: 1593
		void GetPublishedFileDetails(PublishedFileId publishedFileId, uint maxSecondsOld);

		// Token: 0x0600063A RID: 1594
		void DeletePublishedFile(PublishedFileId publishedFileId);

		// Token: 0x0600063B RID: 1595
		void EnumerateUserPublishedFiles(int startIndex);

		// Token: 0x0600063C RID: 1596
		void SubscribePublishedFile(PublishedFileId publishedFileId);

		// Token: 0x0600063D RID: 1597
		void EnumerateUserSubscribedFiles(int startIndex);

		// Token: 0x0600063E RID: 1598
		void UnsubscribePublishedFile(PublishedFileId publishedFileId);

		// Token: 0x0600063F RID: 1599
		bool UpdatePublishedFileSetChangeDescription(PublishedFileUpdateHandle updateHandle, string changeDescription);

		// Token: 0x06000640 RID: 1600
		void GetPublishedItemVoteDetails(PublishedFileId publishedFileId);

		// Token: 0x06000641 RID: 1601
		void UpdateUserPublishedItemVote(PublishedFileId publishedFileId, bool voteUp);

		// Token: 0x06000642 RID: 1602
		void GetUserPublishedItemVoteDetails(PublishedFileId publishedFileId);

		// Token: 0x06000643 RID: 1603
		void EnumerateUserSharedWorkshopFiles(SteamID steamId, int startIndex, IList<string> requiredTags, IList<string> excludedTags);

		// Token: 0x06000644 RID: 1604
		void PublishVideo(WorkshopVideoProviders videoProviders, string videoAccount, string videoIdentifier, string previewFile, AppID comsumerAppId, string title, string description, RemoteStoragePublishedFileVisibility visibility, IList<string> tags);

		// Token: 0x06000645 RID: 1605
		void SetUserPublishedFileAction(PublishedFileId publishedFileId, WorkshopFileAction action);

		// Token: 0x06000646 RID: 1606
		void EnumeratePublishedFilesByUserAction(WorkshopFileAction action, int startIndex);

		// Token: 0x06000647 RID: 1607
		void EnumeratePublishedWorkshopFiles(WorkshopFileAction enumerationType, int startIndex, int count, int days, IList<string> tags, IList<string> userTags);

		// Token: 0x06000648 RID: 1608
		void UGCDownloadToLocation(ulong content, string location, uint priority);
	}
}
