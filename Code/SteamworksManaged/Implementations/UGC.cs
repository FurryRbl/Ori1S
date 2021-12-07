using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ManagedSteam.CallbackStructures;
using ManagedSteam.SteamTypes;
using ManagedSteam.Utility;

namespace ManagedSteam.Implementations
{
	// Token: 0x02000051 RID: 81
	internal class UGC : SteamService, IUGC
	{
		// Token: 0x06000200 RID: 512 RVA: 0x000041E4 File Offset: 0x000023E4
		public UGC()
		{
			this.ugcQueryCompleted = new List<UGCQueryCompleted>();
			this.ugcRequestUGCDetailsResult = new List<SteamService.Result<UGCRequestUGCDetailsResult>>();
			SteamService.Callbacks[CallbackID.UGCQueryCompleted] = delegate(IntPtr data, int size)
			{
				this.ugcQueryCompleted.Add(ManagedSteam.CallbackStructures.UGCQueryCompleted.Create(data, size));
			};
			SteamService.Results[ResultID.UGCRequestUGCDetailsResult] = delegate(IntPtr data, int size, bool flag)
			{
				this.ugcRequestUGCDetailsResult.Add(new SteamService.Result<UGCRequestUGCDetailsResult>(ManagedSteam.CallbackStructures.UGCRequestUGCDetailsResult.Create(data, size), flag));
			};
		}

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06000201 RID: 513 RVA: 0x0000424C File Offset: 0x0000244C
		// (remove) Token: 0x06000202 RID: 514 RVA: 0x00004284 File Offset: 0x00002484
		public event CallbackEvent<UGCQueryCompleted> UGCQueryCompleted;

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06000203 RID: 515 RVA: 0x000042BC File Offset: 0x000024BC
		// (remove) Token: 0x06000204 RID: 516 RVA: 0x000042F4 File Offset: 0x000024F4
		public event ResultEvent<UGCRequestUGCDetailsResult> UGCRequestUGCDetailsResult;

		// Token: 0x06000205 RID: 517 RVA: 0x00004329 File Offset: 0x00002529
		internal override void CheckIfUsableInternal()
		{
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000432B File Offset: 0x0000252B
		internal override void InvokeEvents()
		{
			SteamService.InvokeEvents<UGCQueryCompleted>(this.ugcQueryCompleted, this.UGCQueryCompleted);
			SteamService.InvokeEvents<UGCRequestUGCDetailsResult>(this.ugcRequestUGCDetailsResult, this.UGCRequestUGCDetailsResult);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000434F File Offset: 0x0000254F
		internal override void ReleaseManagedResources()
		{
			this.ugcQueryCompleted = null;
			this.UGCQueryCompleted = null;
			this.ugcRequestUGCDetailsResult = null;
			this.UGCRequestUGCDetailsResult = null;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000436D File Offset: 0x0000256D
		public UGCQueryHandle CreateQueryUserUGCRequest(AccountID accountId, UserUGCList listType, EUGCMatchingUGCType matchingUGCType, EUserUGCListSortOrder sortOrder, AppID creatorAppID, AppID consumerAppID, uint page)
		{
			base.CheckIfUsable();
			return new UGCQueryHandle(NativeMethods.UGC_CreateQueryUserUGCRequest(accountId.AsUInt32, (int)listType, (int)matchingUGCType, (int)sortOrder, creatorAppID.AsUInt32, creatorAppID.AsUInt32, page));
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000439A File Offset: 0x0000259A
		public UGCQueryHandle CreateQueryAllUGCRequest(EUGCQuery queryType, EUGCMatchingUGCType matchingeMatchingUGCTypeFileType, AppID creatorAppID, AppID consumerAppID, uint page)
		{
			base.CheckIfUsable();
			return new UGCQueryHandle(NativeMethods.UGC_CreateQueryAllUGCRequest((int)queryType, (int)matchingeMatchingUGCTypeFileType, creatorAppID.AsUInt32, consumerAppID.AsUInt32, page));
		}

		// Token: 0x0600020A RID: 522 RVA: 0x000043BE File Offset: 0x000025BE
		public void SendQueryUGCRequest(UGCQueryHandle handle)
		{
			base.CheckIfUsable();
			NativeMethods.UGC_SendQueryUGCRequest(handle.AsUInt64);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000043D4 File Offset: 0x000025D4
		public bool GetQueryUGCResult(UGCQueryHandle handle, uint index, out UGCDetails details)
		{
			base.CheckIfUsable();
			bool result;
			using (NativeBuffer nativeBuffer = new NativeBuffer(Marshal.SizeOf(typeof(UGCDetails))))
			{
				bool flag = NativeMethods.UGC_GetQueryUGCResult(handle.AsUInt64, index, nativeBuffer.UnmanagedMemory);
				details = NativeHelpers.ConvertStruct<UGCDetails>(nativeBuffer.UnmanagedMemory, nativeBuffer.UnmanagedSize);
				result = flag;
			}
			return result;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00004448 File Offset: 0x00002648
		public UGCGetQueryUGCResultResult GetQueryUGCResult(UGCQueryHandle handle, uint index)
		{
			UGCGetQueryUGCResultResult result = default(UGCGetQueryUGCResultResult);
			result.Result = this.GetQueryUGCResult(handle, index, out result.Details);
			return result;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00004474 File Offset: 0x00002674
		public bool ReleaseQueryUGCRequest(UGCQueryHandle handle)
		{
			return NativeMethods.UGC_ReleaseQueryUGCRequest(handle.AsUInt64);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00004482 File Offset: 0x00002682
		public bool AddRequiredTag(UGCQueryHandle handle, string tagName)
		{
			return NativeMethods.UGC_AddRequiredTag(handle.AsUInt64, tagName);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00004491 File Offset: 0x00002691
		public bool AddExcludedTag(UGCQueryHandle handle, string tagName)
		{
			return NativeMethods.UGC_AddExcludedTag(handle.AsUInt64, tagName);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x000044A0 File Offset: 0x000026A0
		public bool SetReturnLongDescription(UGCQueryHandle handle, bool returnLongDescription)
		{
			return NativeMethods.UGC_SetReturnLongDescription(handle.AsUInt64, returnLongDescription);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000044AF File Offset: 0x000026AF
		public bool SetReturnTotalOnly(UGCQueryHandle handle, bool returnTotalOnly)
		{
			return NativeMethods.UGC_SetReturnTotalOnly(handle.AsUInt64, returnTotalOnly);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x000044BE File Offset: 0x000026BE
		public bool SetCloudFileNameFilter(UGCQueryHandle handle, string matchCloudFileName)
		{
			return NativeMethods.UGC_SetCloudFileNameFilter(handle.AsUInt64, matchCloudFileName);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000044CD File Offset: 0x000026CD
		public bool SetMatchAnyTag(UGCQueryHandle handle, bool matchAnyTag)
		{
			return NativeMethods.UGC_SetMatchAnyTag(handle.AsUInt64, matchAnyTag);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000044DC File Offset: 0x000026DC
		public bool SetSearchText(UGCQueryHandle handle, string searchText)
		{
			return NativeMethods.UGC_SetSearchText(handle.AsUInt64, searchText);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x000044EB File Offset: 0x000026EB
		public bool SetRankedByTrendDays(UGCQueryHandle handle, uint days)
		{
			return NativeMethods.UGC_SetRankedByTrendDays(handle.AsUInt64, days);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x000044FA File Offset: 0x000026FA
		public void RequestUGCDetails(PublishedFileId publishedFileId)
		{
			NativeMethods.UGC_RequestUGCDetails(publishedFileId.AsUInt64);
		}

		// Token: 0x0400017D RID: 381
		private List<UGCQueryCompleted> ugcQueryCompleted;

		// Token: 0x0400017E RID: 382
		private List<SteamService.Result<UGCRequestUGCDetailsResult>> ugcRequestUGCDetailsResult;
	}
}
