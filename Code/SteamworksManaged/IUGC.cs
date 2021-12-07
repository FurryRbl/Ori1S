using System;
using ManagedSteam.CallbackStructures;
using ManagedSteam.SteamTypes;

namespace ManagedSteam
{
	// Token: 0x02000046 RID: 70
	public interface IUGC
	{
		// Token: 0x14000026 RID: 38
		// (add) Token: 0x060001D4 RID: 468
		// (remove) Token: 0x060001D5 RID: 469
		event CallbackEvent<UGCQueryCompleted> UGCQueryCompleted;

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x060001D6 RID: 470
		// (remove) Token: 0x060001D7 RID: 471
		event ResultEvent<UGCRequestUGCDetailsResult> UGCRequestUGCDetailsResult;

		// Token: 0x060001D8 RID: 472
		UGCQueryHandle CreateQueryUserUGCRequest(AccountID accountId, UserUGCList listType, EUGCMatchingUGCType matchingUGCType, EUserUGCListSortOrder sortOrder, AppID creatorAppID, AppID consumerAppID, uint page);

		// Token: 0x060001D9 RID: 473
		UGCQueryHandle CreateQueryAllUGCRequest(EUGCQuery queryType, EUGCMatchingUGCType matchingeMatchingUGCTypeFileType, AppID creatorAppID, AppID consumerAppID, uint page);

		// Token: 0x060001DA RID: 474
		void SendQueryUGCRequest(UGCQueryHandle handle);

		// Token: 0x060001DB RID: 475
		bool GetQueryUGCResult(UGCQueryHandle handle, uint index, out UGCDetails details);

		// Token: 0x060001DC RID: 476
		UGCGetQueryUGCResultResult GetQueryUGCResult(UGCQueryHandle handle, uint index);

		// Token: 0x060001DD RID: 477
		bool ReleaseQueryUGCRequest(UGCQueryHandle handle);

		// Token: 0x060001DE RID: 478
		bool AddRequiredTag(UGCQueryHandle handle, string tagName);

		// Token: 0x060001DF RID: 479
		bool AddExcludedTag(UGCQueryHandle handle, string tagName);

		// Token: 0x060001E0 RID: 480
		bool SetReturnLongDescription(UGCQueryHandle handle, bool returnLongDescription);

		// Token: 0x060001E1 RID: 481
		bool SetReturnTotalOnly(UGCQueryHandle handle, bool returnTotalOnly);

		// Token: 0x060001E2 RID: 482
		bool SetCloudFileNameFilter(UGCQueryHandle handle, string matchCloudFileName);

		// Token: 0x060001E3 RID: 483
		bool SetMatchAnyTag(UGCQueryHandle handle, bool matchAnyTag);

		// Token: 0x060001E4 RID: 484
		bool SetSearchText(UGCQueryHandle handle, string searchText);

		// Token: 0x060001E5 RID: 485
		bool SetRankedByTrendDays(UGCQueryHandle handle, uint days);

		// Token: 0x060001E6 RID: 486
		void RequestUGCDetails(PublishedFileId publishedFileId);
	}
}
