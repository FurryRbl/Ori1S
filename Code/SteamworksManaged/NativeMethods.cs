using System;
using System.CodeDom.Compiler;
using System.Runtime.InteropServices;

// Token: 0x02000123 RID: 291
[GeneratedCode("InteropSignatureToolkit", "")]
internal class NativeMethods
{
	// Token: 0x0600083A RID: 2106
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Cloud_Write([MarshalAs(UnmanagedType.LPStr)] [In] string fileName, IntPtr buffer, int bufferLength);

	// Token: 0x0600083B RID: 2107
	[DllImport("SteamworksNative")]
	internal static extern int Cloud_Read([MarshalAs(UnmanagedType.LPStr)] [In] string fileName, IntPtr buffer, int bufferLength);

	// Token: 0x0600083C RID: 2108
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Cloud_Forget([MarshalAs(UnmanagedType.LPStr)] [In] string fileName);

	// Token: 0x0600083D RID: 2109
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Cloud_Delete([MarshalAs(UnmanagedType.LPStr)] [In] string fileName);

	// Token: 0x0600083E RID: 2110
	[DllImport("SteamworksNative")]
	internal static extern void Cloud_Share([MarshalAs(UnmanagedType.LPStr)] [In] string fileName);

	// Token: 0x0600083F RID: 2111
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Cloud_SetSyncPlatforms([MarshalAs(UnmanagedType.LPStr)] [In] string fileName, int remoteStoragePlatform);

	// Token: 0x06000840 RID: 2112
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Cloud_Exists([MarshalAs(UnmanagedType.LPStr)] [In] string fileName);

	// Token: 0x06000841 RID: 2113
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Cloud_Persisted([MarshalAs(UnmanagedType.LPStr)] [In] string fileName);

	// Token: 0x06000842 RID: 2114
	[DllImport("SteamworksNative")]
	internal static extern int Cloud_GetSize([MarshalAs(UnmanagedType.LPStr)] [In] string fileName);

	// Token: 0x06000843 RID: 2115
	[DllImport("SteamworksNative")]
	internal static extern long Cloud_Timestamp([MarshalAs(UnmanagedType.LPStr)] [In] string fileName);

	// Token: 0x06000844 RID: 2116
	[DllImport("SteamworksNative")]
	internal static extern int Cloud_GetSyncPlatforms([MarshalAs(UnmanagedType.LPStr)] [In] string fileName);

	// Token: 0x06000845 RID: 2117
	[DllImport("SteamworksNative")]
	internal static extern int Cloud_GetFileCount();

	// Token: 0x06000846 RID: 2118
	[DllImport("SteamworksNative")]
	internal static extern IntPtr Cloud_GetFileNameAndSize(int fileID, ref int fileSize);

	// Token: 0x06000847 RID: 2119
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Cloud_GetQuota(ref int totalBytes, ref int availableBytes);

	// Token: 0x06000848 RID: 2120
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Cloud_IsEnabledForAccount();

	// Token: 0x06000849 RID: 2121
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Cloud_IsEnabledForApplication();

	// Token: 0x0600084A RID: 2122
	[DllImport("SteamworksNative")]
	internal static extern void Cloud_SetEnabledForApplication([MarshalAs(UnmanagedType.I1)] bool enabled);

	// Token: 0x0600084B RID: 2123
	[DllImport("SteamworksNative")]
	internal static extern void Cloud_UGCDownload(ulong handle, uint unPriority);

	// Token: 0x0600084C RID: 2124
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Cloud_GetUGCDownloadProgress(ulong handle, ref int bytesDownloaded, ref int bytesExpected);

	// Token: 0x0600084D RID: 2125
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Cloud_GetUGCDetails(ulong handle, ref uint appID, ref IntPtr name, ref int fileSize, ref ulong creator);

	// Token: 0x0600084E RID: 2126
	[DllImport("SteamworksNative")]
	internal static extern int Cloud_UGCRead(ulong handle, IntPtr buffer, int bufferLength, uint offset, int action);

	// Token: 0x0600084F RID: 2127
	[DllImport("SteamworksNative")]
	internal static extern int Cloud_GetCachedUGCCount();

	// Token: 0x06000850 RID: 2128
	[DllImport("SteamworksNative")]
	internal static extern ulong Cloud_GetUGCHandle(int handleID);

	// Token: 0x06000851 RID: 2129
	[DllImport("SteamworksNative")]
	internal static extern void Cloud_PublishWorkshopFile([MarshalAs(UnmanagedType.LPStr)] [In] string fileName, [MarshalAs(UnmanagedType.LPStr)] [In] string previewFile, uint consumerAppId, [MarshalAs(UnmanagedType.LPStr)] [In] string title, [MarshalAs(UnmanagedType.LPStr)] [In] string description, int visibility, IntPtr tags, int workshopFileType);

	// Token: 0x06000852 RID: 2130
	[DllImport("SteamworksNative")]
	internal static extern ulong Cloud_CreatePublishedFileUpdateRequest(ulong publishedFileId);

	// Token: 0x06000853 RID: 2131
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Cloud_UpdatePublishedFileFile(ulong updateHandle, [MarshalAs(UnmanagedType.LPStr)] [In] string file);

	// Token: 0x06000854 RID: 2132
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Cloud_UpdatePublishedFilePreviewFile(ulong updateHandle, [MarshalAs(UnmanagedType.LPStr)] [In] string previewFile);

	// Token: 0x06000855 RID: 2133
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Cloud_UpdatePublishedFileTitle(ulong updateHandle, [MarshalAs(UnmanagedType.LPStr)] [In] string title);

	// Token: 0x06000856 RID: 2134
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Cloud_UpdatePublishedFileDescription(ulong updateHandle, [MarshalAs(UnmanagedType.LPStr)] [In] string description);

	// Token: 0x06000857 RID: 2135
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Cloud_UpdatePublishedFileVisibility(ulong updateHandle, int visibility);

	// Token: 0x06000858 RID: 2136
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Cloud_UpdatePublishedFileTags(ulong updateHandle, IntPtr tags);

	// Token: 0x06000859 RID: 2137
	[DllImport("SteamworksNative")]
	internal static extern void Cloud_CommitPublishedFileUpdate(ulong updateHandle);

	// Token: 0x0600085A RID: 2138
	[DllImport("SteamworksNative")]
	internal static extern void Cloud_GetPublishedFileDetails(ulong publishedFileId, uint maxSecondsOld);

	// Token: 0x0600085B RID: 2139
	[DllImport("SteamworksNative")]
	internal static extern void Cloud_DeletePublishedFile(ulong publishedFileId);

	// Token: 0x0600085C RID: 2140
	[DllImport("SteamworksNative")]
	internal static extern void Cloud_EnumerateUserPublishedFiles(uint startIndex);

	// Token: 0x0600085D RID: 2141
	[DllImport("SteamworksNative")]
	internal static extern void Cloud_SubscribePublishedFile(ulong publishedFileId);

	// Token: 0x0600085E RID: 2142
	[DllImport("SteamworksNative")]
	internal static extern void Cloud_EnumerateUserSubscribedFiles(uint startIndex);

	// Token: 0x0600085F RID: 2143
	[DllImport("SteamworksNative")]
	internal static extern void Cloud_UnsubscribePublishedFile(ulong publishedFileId);

	// Token: 0x06000860 RID: 2144
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Cloud_UpdatePublishedFileSetChangeDescription(ulong updateHandle, [MarshalAs(UnmanagedType.LPStr)] [In] string changeDescription);

	// Token: 0x06000861 RID: 2145
	[DllImport("SteamworksNative")]
	internal static extern void Cloud_GetPublishedItemVoteDetails(ulong publishedFileId);

	// Token: 0x06000862 RID: 2146
	[DllImport("SteamworksNative")]
	internal static extern void Cloud_UpdateUserPublishedItemVote(ulong publishedFileId, [MarshalAs(UnmanagedType.I1)] bool voteUp);

	// Token: 0x06000863 RID: 2147
	[DllImport("SteamworksNative")]
	internal static extern void Cloud_GetUserPublishedItemVoteDetails(ulong publishedFileId);

	// Token: 0x06000864 RID: 2148
	[DllImport("SteamworksNative")]
	internal static extern void Cloud_EnumerateUserSharedWorkshopFiles(ulong steamId, uint startIndex, IntPtr requiredTags, IntPtr excludedTags);

	// Token: 0x06000865 RID: 2149
	[DllImport("SteamworksNative")]
	internal static extern void Cloud_PublishVideo(int videoProvider, [MarshalAs(UnmanagedType.LPStr)] [In] string videoAccount, [MarshalAs(UnmanagedType.LPStr)] [In] string videoIdentifier, [MarshalAs(UnmanagedType.LPStr)] [In] string videoPreview, uint consumerAppId, [MarshalAs(UnmanagedType.LPStr)] [In] string title, [MarshalAs(UnmanagedType.LPStr)] [In] string description, int visibility, IntPtr tags);

	// Token: 0x06000866 RID: 2150
	[DllImport("SteamworksNative")]
	internal static extern void Cloud_SetUserPublishedFileAction(ulong publishedFileId, int action);

	// Token: 0x06000867 RID: 2151
	[DllImport("SteamworksNative")]
	internal static extern void Cloud_EnumeratePublishedFilesByUserAction(int action, uint startIndex);

	// Token: 0x06000868 RID: 2152
	[DllImport("SteamworksNative")]
	internal static extern void Cloud_EnumeratePublishedWorkshopFiles(int enumerationType, uint startIndex, uint count, uint days, IntPtr tags, IntPtr userTags);

	// Token: 0x06000869 RID: 2153
	[DllImport("SteamworksNative")]
	internal static extern void Cloud_UGCDownloadToLocation(ulong content, [MarshalAs(UnmanagedType.LPStr)] [In] string location, uint priority);

	// Token: 0x0600086A RID: 2154
	[DllImport("SteamworksNative")]
	internal static extern uint Services_GetInterfaceVersion();

	// Token: 0x0600086B RID: 2155
	[DllImport("SteamworksNative")]
	internal static extern int Services_GetErrorCode();

	// Token: 0x0600086C RID: 2156
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Services_Startup(uint interfaceVersion);

	// Token: 0x0600086D RID: 2157
	[DllImport("SteamworksNative")]
	internal static extern void Services_Shutdown();

	// Token: 0x0600086E RID: 2158
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Services_IsSteamRunning();

	// Token: 0x0600086F RID: 2159
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Services_RestartAppIfNecessary(uint ownAppID);

	// Token: 0x06000870 RID: 2160
	[DllImport("SteamworksNative")]
	internal static extern int Services_GetSteamLoadStatus();

	// Token: 0x06000871 RID: 2161
	[DllImport("SteamworksNative")]
	internal static extern void Services_HandleCallbacks();

	// Token: 0x06000872 RID: 2162
	[DllImport("SteamworksNative")]
	internal static extern uint Services_GetAppID();

	// Token: 0x06000873 RID: 2163
	[DllImport("SteamworksNative")]
	internal static extern void Services_RegisterManagedCallbacks(ManagedCallback callback, ManagedResultCallback resultCallback);

	// Token: 0x06000874 RID: 2164
	[DllImport("SteamworksNative")]
	internal static extern void Services_RemoveManagedCallbacks();

	// Token: 0x06000875 RID: 2165
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Services_RunCallbackSizeCheck();

	// Token: 0x06000876 RID: 2166
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Stats_RequestCurrentStats();

	// Token: 0x06000877 RID: 2167
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Stats_GetStatInt([MarshalAs(UnmanagedType.LPStr)] [In] string name, ref int data);

	// Token: 0x06000878 RID: 2168
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Stats_GetStatFloat([MarshalAs(UnmanagedType.LPStr)] [In] string name, ref float data);

	// Token: 0x06000879 RID: 2169
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Stats_SetStatInt([MarshalAs(UnmanagedType.LPStr)] [In] string name, int data);

	// Token: 0x0600087A RID: 2170
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Stats_SetStatFloat([MarshalAs(UnmanagedType.LPStr)] [In] string name, float data);

	// Token: 0x0600087B RID: 2171
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Stats_UpdateAverageRateStat([MarshalAs(UnmanagedType.LPStr)] [In] string name, float countThisSession, double sessionLength);

	// Token: 0x0600087C RID: 2172
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Stats_GetAchievement([MarshalAs(UnmanagedType.LPStr)] [In] string name, ref bool achieved);

	// Token: 0x0600087D RID: 2173
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Stats_SetAchievement([MarshalAs(UnmanagedType.LPStr)] [In] string name);

	// Token: 0x0600087E RID: 2174
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Stats_ClearAchievement([MarshalAs(UnmanagedType.LPStr)] [In] string name);

	// Token: 0x0600087F RID: 2175
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Stats_GetAchievementAndUnlockTime([MarshalAs(UnmanagedType.LPStr)] [In] string name, ref bool achieved, ref uint unlockTime);

	// Token: 0x06000880 RID: 2176
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Stats_StoreStats();

	// Token: 0x06000881 RID: 2177
	[DllImport("SteamworksNative")]
	internal static extern int Stats_GetAchievementIcon([MarshalAs(UnmanagedType.LPStr)] [In] string name);

	// Token: 0x06000882 RID: 2178
	[DllImport("SteamworksNative")]
	internal static extern IntPtr Stats_GetAchievementDisplayAttribute([MarshalAs(UnmanagedType.LPStr)] [In] string name, [MarshalAs(UnmanagedType.LPStr)] [In] string key);

	// Token: 0x06000883 RID: 2179
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Stats_IndicateAchievementProgress([MarshalAs(UnmanagedType.LPStr)] [In] string name, uint currentProgress, uint maxProgess);

	// Token: 0x06000884 RID: 2180
	[DllImport("SteamworksNative")]
	internal static extern void Stats_RequestUserStats(ulong steamID);

	// Token: 0x06000885 RID: 2181
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Stats_GetUserStatInt(ulong steamID, [MarshalAs(UnmanagedType.LPStr)] [In] string name, ref int data);

	// Token: 0x06000886 RID: 2182
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Stats_GetUserStatFloat(ulong steamID, [MarshalAs(UnmanagedType.LPStr)] [In] string name, ref float data);

	// Token: 0x06000887 RID: 2183
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Stats_GetUserAchievement(ulong steamID, [MarshalAs(UnmanagedType.LPStr)] [In] string name, ref bool achieved);

	// Token: 0x06000888 RID: 2184
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Stats_GetUserAchievementAndUnlockTime(ulong steamID, [MarshalAs(UnmanagedType.LPStr)] [In] string name, ref bool achieved, ref uint unlockTime);

	// Token: 0x06000889 RID: 2185
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Stats_ResetAllStats([MarshalAs(UnmanagedType.I1)] bool achievementsToo);

	// Token: 0x0600088A RID: 2186
	[DllImport("SteamworksNative")]
	internal static extern void Stats_FindOrCreateLeaderboard([MarshalAs(UnmanagedType.LPStr)] [In] string name, int sortMethod, int displayType);

	// Token: 0x0600088B RID: 2187
	[DllImport("SteamworksNative")]
	internal static extern void Stats_FindLeaderboard([MarshalAs(UnmanagedType.LPStr)] [In] string name);

	// Token: 0x0600088C RID: 2188
	[DllImport("SteamworksNative")]
	internal static extern IntPtr Stats_GetLeaderboardName(ulong handle);

	// Token: 0x0600088D RID: 2189
	[DllImport("SteamworksNative")]
	internal static extern int Stats_GetLeaderboardEntryCount(ulong handle);

	// Token: 0x0600088E RID: 2190
	[DllImport("SteamworksNative")]
	internal static extern int Stats_GetLeaderboardSortMethod(ulong handle);

	// Token: 0x0600088F RID: 2191
	[DllImport("SteamworksNative")]
	internal static extern int Stats_GetLeaderboardDisplayType(ulong handle);

	// Token: 0x06000890 RID: 2192
	[DllImport("SteamworksNative")]
	internal static extern void Stats_DownloadLeaderboardEntries(ulong handle, int dataRequest, int start, int end);

	// Token: 0x06000891 RID: 2193
	[DllImport("SteamworksNative")]
	internal static extern void Stats_DownloadLeaderboardEntriesForUsers(ulong handle, IntPtr users, int numberOfUsers);

	// Token: 0x06000892 RID: 2194
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Stats_GetDownloadedLeaderboardEntry(ulong entries, int index, IntPtr entry, IntPtr detailsBuffer, int maxDetails);

	// Token: 0x06000893 RID: 2195
	[DllImport("SteamworksNative")]
	internal static extern void Stats_UploadLeaderboardScore(ulong handle, int scoreMethod, int score, IntPtr details, int detailsCount);

	// Token: 0x06000894 RID: 2196
	[DllImport("SteamworksNative")]
	internal static extern void Stats_AttachLeaderboardUGC(ulong handle, ulong ugcHandle);

	// Token: 0x06000895 RID: 2197
	[DllImport("SteamworksNative")]
	internal static extern void Stats_GetNumberOfCurrentPlayers();

	// Token: 0x06000896 RID: 2198
	[DllImport("SteamworksNative")]
	internal static extern void Stats_RequestGlobalAchievementPercentages();

	// Token: 0x06000897 RID: 2199
	[DllImport("SteamworksNative")]
	internal static extern int Stats_GetMostAchievedAchievementInfo(IntPtr name, uint nameLength, ref float percent, ref bool achieved);

	// Token: 0x06000898 RID: 2200
	[DllImport("SteamworksNative")]
	internal static extern int Stats_GetNextMostAchievedAchievementInfo(int iterator, IntPtr name, uint nameLength, ref float percent, ref bool achieved);

	// Token: 0x06000899 RID: 2201
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Stats_GetAchievementAchievedPercent([MarshalAs(UnmanagedType.LPStr)] [In] string name, ref float percent);

	// Token: 0x0600089A RID: 2202
	[DllImport("SteamworksNative")]
	internal static extern void Stats_RequestGlobalStats(int historyDays);

	// Token: 0x0600089B RID: 2203
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Stats_GetGlobalStatInt([MarshalAs(UnmanagedType.LPStr)] [In] string name, ref long data);

	// Token: 0x0600089C RID: 2204
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Stats_GetGlobalStatDouble([MarshalAs(UnmanagedType.LPStr)] [In] string name, ref double data);

	// Token: 0x0600089D RID: 2205
	[DllImport("SteamworksNative")]
	internal static extern int Stats_GetGlobalStatHistoryInt([MarshalAs(UnmanagedType.LPStr)] [In] string name, IntPtr dataBuffer, uint bufferSize);

	// Token: 0x0600089E RID: 2206
	[DllImport("SteamworksNative")]
	internal static extern int Stats_GetGlobalStatHistoryDouble([MarshalAs(UnmanagedType.LPStr)] [In] string name, IntPtr dataBuffer, uint bufferSize);

	// Token: 0x0600089F RID: 2207
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool User_IsLoggedOn();

	// Token: 0x060008A0 RID: 2208
	[DllImport("SteamworksNative")]
	internal static extern ulong User_GetSteamID();

	// Token: 0x060008A1 RID: 2209
	[DllImport("SteamworksNative")]
	internal static extern int User_InitiateGameConnection(IntPtr authBlob, int maxAuthBlob, ulong steamIDGameServer, uint serverIP, ushort serverPort, bool secure);

	// Token: 0x060008A2 RID: 2210
	[DllImport("SteamworksNative")]
	internal static extern void User_TerminateGameConnection(uint serverIP, ushort serverPort);

	// Token: 0x060008A3 RID: 2211
	[DllImport("SteamworksNative")]
	internal static extern void User_TrackAppUsageEvent(ulong gameID, int appUsageEvent, [MarshalAs(UnmanagedType.LPStr)] [In] string extraInfo);

	// Token: 0x060008A4 RID: 2212
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool User_GetUserDataFolder(IntPtr buffer, int bufferLength);

	// Token: 0x060008A5 RID: 2213
	[DllImport("SteamworksNative")]
	internal static extern void User_StartVoiceRecording();

	// Token: 0x060008A6 RID: 2214
	[DllImport("SteamworksNative")]
	internal static extern void User_StopVoiceRecording();

	// Token: 0x060008A7 RID: 2215
	[DllImport("SteamworksNative")]
	internal static extern int User_GetAvailableVoice(ref uint compressed, ref uint uncompressed, uint uncompressedVoiceDesiredSampleRate);

	// Token: 0x060008A8 RID: 2216
	[DllImport("SteamworksNative")]
	internal static extern int User_GetVoice(bool wantCompressed, IntPtr destBuffer, uint destBufferSize, ref uint bytesWritten, bool wantUncompressed, IntPtr uncompressedDestBuffer, uint uncompressedDestBufferSize, ref uint uncompressedBytesWritten, uint uncompressedVoiceDesiredSampleRate);

	// Token: 0x060008A9 RID: 2217
	[DllImport("SteamworksNative")]
	internal static extern int User_DecompressVoice(IntPtr compressed, uint compressedSize, IntPtr destBuffer, uint destBufferSize, ref uint bytesWritten, uint desiredSampleRate);

	// Token: 0x060008AA RID: 2218
	[DllImport("SteamworksNative")]
	internal static extern uint User_GetVoiceOptimalSampleRate();

	// Token: 0x060008AB RID: 2219
	[DllImport("SteamworksNative")]
	internal static extern uint User_GetAuthSessionTicket(IntPtr ticket, int maxTicket, ref uint ticketLength);

	// Token: 0x060008AC RID: 2220
	[DllImport("SteamworksNative")]
	internal static extern int User_BeginAuthSession(IntPtr authTicket, int cbAuthTicket, ulong steamID);

	// Token: 0x060008AD RID: 2221
	[DllImport("SteamworksNative")]
	internal static extern void User_EndAuthSession(ulong steamID);

	// Token: 0x060008AE RID: 2222
	[DllImport("SteamworksNative")]
	internal static extern void User_CancelAuthTicket(uint authTicket);

	// Token: 0x060008AF RID: 2223
	[DllImport("SteamworksNative")]
	internal static extern int User_UserHasLicenseForApp(ulong steamID, uint appID);

	// Token: 0x060008B0 RID: 2224
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool User_IsBehindNAT();

	// Token: 0x060008B1 RID: 2225
	[DllImport("SteamworksNative")]
	internal static extern void User_AdvertiseGame(ulong steamIDGameServer, uint serverIP, ushort serverPort);

	// Token: 0x060008B2 RID: 2226
	[DllImport("SteamworksNative")]
	internal static extern void User_RequestEncryptedAppTicket(IntPtr dataToInclude, int cbDataToInclude);

	// Token: 0x060008B3 RID: 2227
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool User_GetEncryptedAppTicket(IntPtr ticket, int maxTicket, ref uint ticketLength);

	// Token: 0x060008B4 RID: 2228
	[DllImport("SteamworksNative")]
	internal static extern int User_GetGameBadgeLevel(int nSeries, [MarshalAs(UnmanagedType.I1)] bool bFoil);

	// Token: 0x060008B5 RID: 2229
	[DllImport("SteamworksNative")]
	internal static extern int User_GetPlayerSteamLevel();

	// Token: 0x060008B6 RID: 2230
	[DllImport("SteamworksNative")]
	internal static extern IntPtr Friends_GetPersonaName();

	// Token: 0x060008B7 RID: 2231
	[DllImport("SteamworksNative")]
	internal static extern void Friends_SetPersonaName(IntPtr personaName);

	// Token: 0x060008B8 RID: 2232
	[DllImport("SteamworksNative")]
	internal static extern int Friends_GetPersonaState();

	// Token: 0x060008B9 RID: 2233
	[DllImport("SteamworksNative")]
	internal static extern int Friends_GetFriendCount(int friendFlags);

	// Token: 0x060008BA RID: 2234
	[DllImport("SteamworksNative")]
	internal static extern ulong Friends_GetFriendByIndex(int friendIndex, int friendFlags);

	// Token: 0x060008BB RID: 2235
	[DllImport("SteamworksNative")]
	internal static extern int Friends_GetFriendRelationship(ulong steamIDFriend);

	// Token: 0x060008BC RID: 2236
	[DllImport("SteamworksNative")]
	internal static extern int Friends_GetFriendPersonaState(ulong steamIDFriend);

	// Token: 0x060008BD RID: 2237
	[DllImport("SteamworksNative")]
	internal static extern IntPtr Friends_GetFriendPersonaName(ulong steamIDFriend);

	// Token: 0x060008BE RID: 2238
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Friends_GetFriendGamePlayed(ulong steamIDFriend, IntPtr friendGameInfo);

	// Token: 0x060008BF RID: 2239
	[DllImport("SteamworksNative")]
	internal static extern int Friends_GetFriendGameInfoSize();

	// Token: 0x060008C0 RID: 2240
	[DllImport("SteamworksNative")]
	internal static extern IntPtr Friends_GetFriendPersonaNameHistory(ulong steamIDFriend, int personaName);

	// Token: 0x060008C1 RID: 2241
	[DllImport("SteamworksNative")]
	internal static extern IntPtr Friends_GetPlayerNickname(ulong steamIDPlayer);

	// Token: 0x060008C2 RID: 2242
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Friends_HasFriend(ulong steamIDFriend, int friendFlags);

	// Token: 0x060008C3 RID: 2243
	[DllImport("SteamworksNative")]
	internal static extern int Friends_GetClanCount();

	// Token: 0x060008C4 RID: 2244
	[DllImport("SteamworksNative")]
	internal static extern ulong Friends_GetClanByIndex(int clan);

	// Token: 0x060008C5 RID: 2245
	[DllImport("SteamworksNative")]
	internal static extern IntPtr Friends_GetClanName(ulong steamIDClan);

	// Token: 0x060008C6 RID: 2246
	[DllImport("SteamworksNative")]
	internal static extern IntPtr Friends_GetClanTag(ulong steamIDClan);

	// Token: 0x060008C7 RID: 2247
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Friends_GetClanActivityCounts(ulong steamIDClan, ref int online, ref int inGame, ref int chatting);

	// Token: 0x060008C8 RID: 2248
	[DllImport("SteamworksNative")]
	internal static extern void Friends_DownloadClanActivityCounts(IntPtr steamIDClans, int clansToRequest);

	// Token: 0x060008C9 RID: 2249
	[DllImport("SteamworksNative")]
	internal static extern int Friends_GetFriendCountFromSource(ulong steamIDSource);

	// Token: 0x060008CA RID: 2250
	[DllImport("SteamworksNative")]
	internal static extern ulong Friends_GetFriendFromSourceByIndex(ulong steamIDSource, int friendIndex);

	// Token: 0x060008CB RID: 2251
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Friends_IsUserInSource(ulong steamIDUser, ulong steamIDSource);

	// Token: 0x060008CC RID: 2252
	[DllImport("SteamworksNative")]
	internal static extern void Friends_SetInGameVoiceSpeaking(ulong steamIDUser, [MarshalAs(UnmanagedType.I1)] bool speaking);

	// Token: 0x060008CD RID: 2253
	[DllImport("SteamworksNative")]
	internal static extern void Friends_ActivateGameOverlay(int dialogType);

	// Token: 0x060008CE RID: 2254
	[DllImport("SteamworksNative")]
	internal static extern void Friends_ActivateGameOverlayToUser(int dialogType, ulong steamID);

	// Token: 0x060008CF RID: 2255
	[DllImport("SteamworksNative")]
	internal static extern void Friends_ActivateGameOverlayToWebPage([MarshalAs(UnmanagedType.LPStr)] [In] string url);

	// Token: 0x060008D0 RID: 2256
	[DllImport("SteamworksNative")]
	internal static extern void Friends_ActivateGameOverlayToStore(uint appId, int flag);

	// Token: 0x060008D1 RID: 2257
	[DllImport("SteamworksNative")]
	internal static extern void Friends_SetPlayedWith(ulong steamIDUserPlayedWith);

	// Token: 0x060008D2 RID: 2258
	[DllImport("SteamworksNative")]
	internal static extern void Friends_ActivateGameOverlayInviteDialog(ulong steamIDLobby);

	// Token: 0x060008D3 RID: 2259
	[DllImport("SteamworksNative")]
	internal static extern int Friends_GetSmallFriendAvatar(ulong steamIDFriend);

	// Token: 0x060008D4 RID: 2260
	[DllImport("SteamworksNative")]
	internal static extern int Friends_GetMediumFriendAvatar(ulong steamIDFriend);

	// Token: 0x060008D5 RID: 2261
	[DllImport("SteamworksNative")]
	internal static extern int Friends_GetLargeFriendAvatar(ulong steamIDFriend);

	// Token: 0x060008D6 RID: 2262
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Friends_RequestUserInformation(ulong steamIDUser, [MarshalAs(UnmanagedType.I1)] bool requireNameOnly);

	// Token: 0x060008D7 RID: 2263
	[DllImport("SteamworksNative")]
	internal static extern void Friends_RequestClanOfficerList(ulong steamIDClan);

	// Token: 0x060008D8 RID: 2264
	[DllImport("SteamworksNative")]
	internal static extern ulong Friends_GetClanOwner(ulong steamIDClan);

	// Token: 0x060008D9 RID: 2265
	[DllImport("SteamworksNative")]
	internal static extern int Friends_GetClanOfficerCount(ulong steamIDClan);

	// Token: 0x060008DA RID: 2266
	[DllImport("SteamworksNative")]
	internal static extern ulong Friends_GetClanOfficerByIndex(ulong steamIDClan, int officer);

	// Token: 0x060008DB RID: 2267
	[DllImport("SteamworksNative")]
	internal static extern uint Friends_GetUserRestrictions();

	// Token: 0x060008DC RID: 2268
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Friends_SetRichPresence([MarshalAs(UnmanagedType.LPStr)] [In] string key, IntPtr value);

	// Token: 0x060008DD RID: 2269
	[DllImport("SteamworksNative")]
	internal static extern void Friends_ClearRichPresence();

	// Token: 0x060008DE RID: 2270
	[DllImport("SteamworksNative")]
	internal static extern IntPtr Friends_GetFriendRichPresence(ulong steamIDFriend, [MarshalAs(UnmanagedType.LPStr)] [In] string key);

	// Token: 0x060008DF RID: 2271
	[DllImport("SteamworksNative")]
	internal static extern int Friends_GetFriendRichPresenceKeyCount(ulong steamIDFriend);

	// Token: 0x060008E0 RID: 2272
	[DllImport("SteamworksNative")]
	internal static extern IntPtr Friends_GetFriendRichPresenceKeyByIndex(ulong steamIDFriend, int key);

	// Token: 0x060008E1 RID: 2273
	[DllImport("SteamworksNative")]
	internal static extern void Friends_RequestFriendRichPresence(ulong steamIDFriend);

	// Token: 0x060008E2 RID: 2274
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Friends_InviteUserToGame(ulong steamIDFriend, [MarshalAs(UnmanagedType.LPStr)] [In] string connectString);

	// Token: 0x060008E3 RID: 2275
	[DllImport("SteamworksNative")]
	internal static extern int Friends_GetCoplayFriendCount();

	// Token: 0x060008E4 RID: 2276
	[DllImport("SteamworksNative")]
	internal static extern ulong Friends_GetCoplayFriend(int coplayFriend);

	// Token: 0x060008E5 RID: 2277
	[DllImport("SteamworksNative")]
	internal static extern int Friends_GetFriendCoplayTime(ulong steamIDFriend);

	// Token: 0x060008E6 RID: 2278
	[DllImport("SteamworksNative")]
	internal static extern uint Friends_GetFriendCoplayGame(ulong steamIDFriend);

	// Token: 0x060008E7 RID: 2279
	[DllImport("SteamworksNative")]
	internal static extern void Friends_JoinClanChatRoom(ulong steamIDClan);

	// Token: 0x060008E8 RID: 2280
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Friends_LeaveClanChatRoom(ulong steamIDClan);

	// Token: 0x060008E9 RID: 2281
	[DllImport("SteamworksNative")]
	internal static extern int Friends_GetClanChatMemberCount(ulong steamIDClan);

	// Token: 0x060008EA RID: 2282
	[DllImport("SteamworksNative")]
	internal static extern ulong Friends_GetChatMemberByIndex(ulong steamIDClan, int user);

	// Token: 0x060008EB RID: 2283
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Friends_SendClanChatMessage(ulong steamIDClanChat, IntPtr text);

	// Token: 0x060008EC RID: 2284
	[DllImport("SteamworksNative")]
	internal static extern int Friends_GetClanChatMessage(ulong steamIDClanChat, int message, IntPtr text, int textSize, ref int chatEntryType, ref ulong sender);

	// Token: 0x060008ED RID: 2285
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Friends_IsClanChatAdmin(ulong steamIDClanChat, ulong steamIDUser);

	// Token: 0x060008EE RID: 2286
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Friends_IsClanChatWindowOpenInSteam(ulong steamIDClanChat);

	// Token: 0x060008EF RID: 2287
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Friends_OpenClanChatWindowInSteam(ulong steamIDClanChat);

	// Token: 0x060008F0 RID: 2288
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Friends_CloseClanChatWindowInSteam(ulong steamIDClanChat);

	// Token: 0x060008F1 RID: 2289
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Friends_SetListenForFriendsMessages([MarshalAs(UnmanagedType.I1)] bool interceptEnabled);

	// Token: 0x060008F2 RID: 2290
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Friends_ReplyToFriendMessage(ulong steamIDFriend, IntPtr msgToSend);

	// Token: 0x060008F3 RID: 2291
	[DllImport("SteamworksNative")]
	internal static extern int Friends_GetFriendMessage(ulong steamIDFriend, int messageID, IntPtr text, int textSize, ref int chatEntryType);

	// Token: 0x060008F4 RID: 2292
	[DllImport("SteamworksNative")]
	internal static extern void Friends_GetFollowerCount(ulong steamID);

	// Token: 0x060008F5 RID: 2293
	[DllImport("SteamworksNative")]
	internal static extern void Friends_IsFollowing(ulong steamID);

	// Token: 0x060008F6 RID: 2294
	[DllImport("SteamworksNative")]
	internal static extern void Friends_EnumerateFollowingList(uint startIndex);

	// Token: 0x060008F7 RID: 2295
	[DllImport("SteamworksNative")]
	internal static extern int MatchMaking_GetFavoriteGameCount();

	// Token: 0x060008F8 RID: 2296
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool MatchMaking_GetFavoriteGame(int game, ref uint appID, ref uint ip, ref ushort connPort, ref ushort queryPort, ref uint flags, ref uint time32LastPlayedOnServer);

	// Token: 0x060008F9 RID: 2297
	[DllImport("SteamworksNative")]
	internal static extern int MatchMaking_AddFavoriteGame(uint appID, uint ip, ushort connPort, ushort queryPort, uint flags, uint time32LastPlayedOnServer);

	// Token: 0x060008FA RID: 2298
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool MatchMaking_RemoveFavoriteGame(uint appID, uint IP, ushort connPort, ushort queryPort, uint flags);

	// Token: 0x060008FB RID: 2299
	[DllImport("SteamworksNative")]
	internal static extern void MatchMaking_RequestLobbyList();

	// Token: 0x060008FC RID: 2300
	[DllImport("SteamworksNative")]
	internal static extern void MatchMaking_AddRequestLobbyListStringFilter([MarshalAs(UnmanagedType.LPStr)] [In] string keyToMatch, [MarshalAs(UnmanagedType.LPStr)] [In] string valueToMatch, int comparisonType);

	// Token: 0x060008FD RID: 2301
	[DllImport("SteamworksNative")]
	internal static extern void MatchMaking_AddRequestLobbyListNumericalFilter([MarshalAs(UnmanagedType.LPStr)] [In] string keyToMatch, int valueToMatch, int comparisonType);

	// Token: 0x060008FE RID: 2302
	[DllImport("SteamworksNative")]
	internal static extern void MatchMaking_AddRequestLobbyListNearValueFilter([MarshalAs(UnmanagedType.LPStr)] [In] string keyToMatch, int valueToBeCloseTo);

	// Token: 0x060008FF RID: 2303
	[DllImport("SteamworksNative")]
	internal static extern void MatchMaking_AddRequestLobbyListFilterSlotsAvailable(int SlotsAvailable);

	// Token: 0x06000900 RID: 2304
	[DllImport("SteamworksNative")]
	internal static extern void MatchMaking_AddRequestLobbyListDistanceFilter(int lobbyDistanceFilter);

	// Token: 0x06000901 RID: 2305
	[DllImport("SteamworksNative")]
	internal static extern void MatchMaking_AddRequestLobbyListResultCountFilter(int maxResults);

	// Token: 0x06000902 RID: 2306
	[DllImport("SteamworksNative")]
	internal static extern void MatchMaking_AddRequestLobbyListCompatibleMembersFilter(ulong steamIDLobby);

	// Token: 0x06000903 RID: 2307
	[DllImport("SteamworksNative")]
	internal static extern ulong MatchMaking_GetLobbyByIndex(int lobby);

	// Token: 0x06000904 RID: 2308
	[DllImport("SteamworksNative")]
	internal static extern void MatchMaking_CreateLobby(int lobbyType, int maxMembers);

	// Token: 0x06000905 RID: 2309
	[DllImport("SteamworksNative")]
	internal static extern void MatchMaking_JoinLobby(ulong steamIDLobby);

	// Token: 0x06000906 RID: 2310
	[DllImport("SteamworksNative")]
	internal static extern void MatchMaking_LeaveLobby(ulong steamIDLobby);

	// Token: 0x06000907 RID: 2311
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool MatchMaking_InviteUserToLobby(ulong steamIDLobby, ulong steamIDInvitee);

	// Token: 0x06000908 RID: 2312
	[DllImport("SteamworksNative")]
	internal static extern int MatchMaking_GetNumLobbyMembers(ulong steamIDLobby);

	// Token: 0x06000909 RID: 2313
	[DllImport("SteamworksNative")]
	internal static extern ulong MatchMaking_GetLobbyMemberByIndex(ulong steamIDLobby, int member);

	// Token: 0x0600090A RID: 2314
	[DllImport("SteamworksNative")]
	internal static extern IntPtr MatchMaking_GetLobbyData(ulong steamIDLobby, [MarshalAs(UnmanagedType.LPStr)] [In] string key);

	// Token: 0x0600090B RID: 2315
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool MatchMaking_SetLobbyData(ulong steamIDLobby, [MarshalAs(UnmanagedType.LPStr)] [In] string key, [MarshalAs(UnmanagedType.LPStr)] [In] string value);

	// Token: 0x0600090C RID: 2316
	[DllImport("SteamworksNative")]
	internal static extern int MatchMaking_GetLobbyDataCount(ulong steamIDLobby);

	// Token: 0x0600090D RID: 2317
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool MatchMaking_GetLobbyDataByIndex(ulong steamIDLobby, int lobbyData, IntPtr key, int keyBufferSize, IntPtr value, int valueBufferSize);

	// Token: 0x0600090E RID: 2318
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool MatchMaking_DeleteLobbyData(ulong steamIDLobby, [MarshalAs(UnmanagedType.LPStr)] [In] string key);

	// Token: 0x0600090F RID: 2319
	[DllImport("SteamworksNative")]
	internal static extern IntPtr MatchMaking_GetLobbyMemberData(ulong steamIDLobby, ulong SteamIDUser, [MarshalAs(UnmanagedType.LPStr)] [In] string key);

	// Token: 0x06000910 RID: 2320
	[DllImport("SteamworksNative")]
	internal static extern void MatchMaking_SetLobbyMemberData(ulong steamIDLobby, [MarshalAs(UnmanagedType.LPStr)] [In] string key, [MarshalAs(UnmanagedType.LPStr)] [In] string value);

	// Token: 0x06000911 RID: 2321
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool MatchMaking_SendLobbyChatMsg(ulong steamIDLobby, IntPtr msg, int cubMsg);

	// Token: 0x06000912 RID: 2322
	[DllImport("SteamworksNative")]
	internal static extern int MatchMaking_GetLobbyChatEntry(ulong steamIDLobby, int chatID, ref ulong steamIDUser, IntPtr data, int dataBufferSize, ref int chatEntryType);

	// Token: 0x06000913 RID: 2323
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool MatchMaking_RequestLobbyData(ulong steamIDLobby);

	// Token: 0x06000914 RID: 2324
	[DllImport("SteamworksNative")]
	internal static extern void MatchMaking_SetLobbyGameServer(ulong steamIDLobby, uint gameServerIP, ushort gameServerPort, ulong steamIDGameServer);

	// Token: 0x06000915 RID: 2325
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool MatchMaking_GetLobbyGameServer(ulong steamIDLobby, ref uint gameServerIP, ref ushort gameServerPort, ref ulong steamIDGameServer);

	// Token: 0x06000916 RID: 2326
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool MatchMaking_SetLobbyMemberLimit(ulong steamIDLobby, int maxMembers);

	// Token: 0x06000917 RID: 2327
	[DllImport("SteamworksNative")]
	internal static extern int MatchMaking_GetLobbyMemberLimit(ulong steamIDlobby);

	// Token: 0x06000918 RID: 2328
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool MatchMaking_SetLobbyType(ulong steamIDLobby, int lobbyType);

	// Token: 0x06000919 RID: 2329
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool MatchMaking_SetLobbyJoinable(ulong steamIDLobby, [MarshalAs(UnmanagedType.I1)] bool lobbyJoinable);

	// Token: 0x0600091A RID: 2330
	[DllImport("SteamworksNative")]
	internal static extern ulong MatchMaking_GetLobbyOwner(ulong steamIDLobby);

	// Token: 0x0600091B RID: 2331
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool MatchMaking_SetLobbyOwner(ulong steamIDLobby, ulong steamIDNewOwner);

	// Token: 0x0600091C RID: 2332
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool MatchMaking_SetLinkedLobby(ulong steamIDLobby, ulong steamIDLobbyDependent);

	// Token: 0x0600091D RID: 2333
	[DllImport("SteamworksNative")]
	internal static extern IntPtr MatchmakingServerNetworkAddress_GetConnectionString(IntPtr instance);

	// Token: 0x0600091E RID: 2334
	[DllImport("SteamworksNative")]
	internal static extern IntPtr MatchmakingServerNetworkAddress_GetQueryString(IntPtr instance);

	// Token: 0x0600091F RID: 2335
	[DllImport("SteamworksNative")]
	internal static extern uint MatchmakingServers_RequestInternetServerList(uint appId, IntPtr filters, uint filterCount, uint requestServersResponse);

	// Token: 0x06000920 RID: 2336
	[DllImport("SteamworksNative")]
	internal static extern uint MatchmakingServers_RequestLANServerList(uint appId, uint requestServersResponse);

	// Token: 0x06000921 RID: 2337
	[DllImport("SteamworksNative")]
	internal static extern uint MatchmakingServers_RequestFriendsServerList(uint appId, IntPtr filters, uint filtersCount, uint requestServersResponse);

	// Token: 0x06000922 RID: 2338
	[DllImport("SteamworksNative")]
	internal static extern uint MatchmakingServers_RequestFavoritesServerList(uint appId, IntPtr filters, uint filterCount, uint requestServersResponse);

	// Token: 0x06000923 RID: 2339
	[DllImport("SteamworksNative")]
	internal static extern uint MatchmakingServers_RequestHistoryServerList(uint appId, IntPtr filters, uint filterCount, uint requestServersResponse);

	// Token: 0x06000924 RID: 2340
	[DllImport("SteamworksNative")]
	internal static extern uint MatchmakingServers_RequestSpectatorServerList(uint appId, IntPtr filters, uint filterCount, uint requestServersResponse);

	// Token: 0x06000925 RID: 2341
	[DllImport("SteamworksNative")]
	internal static extern void MatchmakingServers_ReleaseRequest(uint request);

	// Token: 0x06000926 RID: 2342
	[DllImport("SteamworksNative")]
	internal static extern IntPtr MatchmakingServers_GetServerDetails(uint request, int server);

	// Token: 0x06000927 RID: 2343
	[DllImport("SteamworksNative")]
	internal static extern int MatchmakingServers_GetGameServerItemSize();

	// Token: 0x06000928 RID: 2344
	[DllImport("SteamworksNative")]
	internal static extern void MatchmakingServers_CancelQuery(uint request);

	// Token: 0x06000929 RID: 2345
	[DllImport("SteamworksNative")]
	internal static extern void MatchmakingServers_RefreshQuery(uint request);

	// Token: 0x0600092A RID: 2346
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool MatchmakingServers_IsRefreshing(uint request);

	// Token: 0x0600092B RID: 2347
	[DllImport("SteamworksNative")]
	internal static extern int MatchmakingServers_GetServerCount(uint request);

	// Token: 0x0600092C RID: 2348
	[DllImport("SteamworksNative")]
	internal static extern void MatchmakingServers_RefreshServer(uint request, int server);

	// Token: 0x0600092D RID: 2349
	[DllImport("SteamworksNative")]
	internal static extern int MatchmakingServers_PingServer(uint ip, ushort port, uint requestServersResponse);

	// Token: 0x0600092E RID: 2350
	[DllImport("SteamworksNative")]
	internal static extern int MatchmakingServers_PlayerDetails(uint ip, ushort port, uint requestServersResponse);

	// Token: 0x0600092F RID: 2351
	[DllImport("SteamworksNative")]
	internal static extern int MatchmakingServers_ServerRules(uint ip, ushort port, uint requestServersResponse);

	// Token: 0x06000930 RID: 2352
	[DllImport("SteamworksNative")]
	internal static extern void MatchmakingServers_CancelServerQuery(int serverQuery);

	// Token: 0x06000931 RID: 2353
	[DllImport("SteamworksNative")]
	internal static extern uint MatchmakingServerListResponse_CreateObject();

	// Token: 0x06000932 RID: 2354
	[DllImport("SteamworksNative")]
	internal static extern void MatchmakingServerListResponse_DestroyObject(uint obj);

	// Token: 0x06000933 RID: 2355
	[DllImport("SteamworksNative")]
	internal static extern void MatchmakingServerListResponse_RegisterCallbacks(MatchmakingServerListResponse_ServerRespondedCallback serverResponded, MatchmakingServerListResponse_ServerFailedToRespond serverFailedToRespond, MatchmakingServerListResponse_RefreshComplete refreshComplete);

	// Token: 0x06000934 RID: 2356
	[DllImport("SteamworksNative")]
	internal static extern void MatchmakingServerListResponse_RemoveCallbacks();

	// Token: 0x06000935 RID: 2357
	[DllImport("SteamworksNative")]
	internal static extern uint MatchmakingPingResponse_CreateObject();

	// Token: 0x06000936 RID: 2358
	[DllImport("SteamworksNative")]
	internal static extern void MatchmakingPingResponse_DestroyObject(uint obj);

	// Token: 0x06000937 RID: 2359
	[DllImport("SteamworksNative")]
	internal static extern void MatchmakingPingResponse_RegisterCallbacks(MatchmakingPingResponse_ServerRespondedCallback serverResponded, MatchmakingPingResponse_ServerFailedToRespond serverFailedToRespond);

	// Token: 0x06000938 RID: 2360
	[DllImport("SteamworksNative")]
	internal static extern void MatchmakingPingResponse_RemoveCallbacks();

	// Token: 0x06000939 RID: 2361
	[DllImport("SteamworksNative")]
	internal static extern uint MatchmakingPlayersResponse_CreateObject();

	// Token: 0x0600093A RID: 2362
	[DllImport("SteamworksNative")]
	internal static extern void MatchmakingPlayersResponse_DestroyObject(uint obj);

	// Token: 0x0600093B RID: 2363
	[DllImport("SteamworksNative")]
	internal static extern void MatchmakingPlayersResponse_RegisterCallbacks(MatchmakingPlayersResponse_AddPlayerToList addPlayerToList, MatchmakingPlayersResponse_PlayersFailedToRespond playersFailedToRespond, MatchmakingPlayersResponse_PlayersRefreshComplete playersRefreshComplete);

	// Token: 0x0600093C RID: 2364
	[DllImport("SteamworksNative")]
	internal static extern void MatchmakingPlayersResponse_RemoveCallbacks();

	// Token: 0x0600093D RID: 2365
	[DllImport("SteamworksNative")]
	internal static extern uint MatchmakingRulesResponse_CreateObject();

	// Token: 0x0600093E RID: 2366
	[DllImport("SteamworksNative")]
	internal static extern void MatchmakingRulesResponse_DestroyObject(uint obj);

	// Token: 0x0600093F RID: 2367
	[DllImport("SteamworksNative")]
	internal static extern void MatchmakingRulesResponse_RegisterCallbacks(MatchmakingRulesResponse_RulesResponded rulesResponded, MatchmakingRulesResponse_RulesFailedToRespond rulesFailedToRespond, MatchmakingRulesResponse_RulesRefreshComplete rulesRefreshComplete);

	// Token: 0x06000940 RID: 2368
	[DllImport("SteamworksNative")]
	internal static extern void MatchmakingRulesResponse_RemoveCallbacks();

	// Token: 0x06000941 RID: 2369
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool GameServer_InitGameServer(uint ip, ushort gamePort, ushort queryPort, uint flags, uint gameAppId, [MarshalAs(UnmanagedType.LPStr)] [In] string versionString);

	// Token: 0x06000942 RID: 2370
	[DllImport("SteamworksNative")]
	internal static extern void GameServer_SetProduct([MarshalAs(UnmanagedType.LPStr)] [In] string product);

	// Token: 0x06000943 RID: 2371
	[DllImport("SteamworksNative")]
	internal static extern void GameServer_SetGameDescription([MarshalAs(UnmanagedType.LPStr)] [In] string gameDescription);

	// Token: 0x06000944 RID: 2372
	[DllImport("SteamworksNative")]
	internal static extern void GameServer_SetModDir([MarshalAs(UnmanagedType.LPStr)] [In] string modDir);

	// Token: 0x06000945 RID: 2373
	[DllImport("SteamworksNative")]
	internal static extern void GameServer_SetDedicatedServer([MarshalAs(UnmanagedType.I1)] bool dedicated);

	// Token: 0x06000946 RID: 2374
	[DllImport("SteamworksNative")]
	internal static extern void GameServer_LogOn([MarshalAs(UnmanagedType.LPStr)] [In] string accountName, [MarshalAs(UnmanagedType.LPStr)] [In] string password);

	// Token: 0x06000947 RID: 2375
	[DllImport("SteamworksNative")]
	internal static extern void GameServer_LogOnAnonymous();

	// Token: 0x06000948 RID: 2376
	[DllImport("SteamworksNative")]
	internal static extern void GameServer_LogOff();

	// Token: 0x06000949 RID: 2377
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool GameServer_LoggedOn();

	// Token: 0x0600094A RID: 2378
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool GameServer_Secure();

	// Token: 0x0600094B RID: 2379
	[DllImport("SteamworksNative")]
	internal static extern ulong GameServer_GetSteamID();

	// Token: 0x0600094C RID: 2380
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool GameServer_WasRestartRequested();

	// Token: 0x0600094D RID: 2381
	[DllImport("SteamworksNative")]
	internal static extern void GameServer_SetMaxPlayerCount(int playersMax);

	// Token: 0x0600094E RID: 2382
	[DllImport("SteamworksNative")]
	internal static extern void GameServer_SetBotPlayerCount(int botplayers);

	// Token: 0x0600094F RID: 2383
	[DllImport("SteamworksNative")]
	internal static extern void GameServer_SetServerName([MarshalAs(UnmanagedType.LPStr)] [In] string serverName);

	// Token: 0x06000950 RID: 2384
	[DllImport("SteamworksNative")]
	internal static extern void GameServer_SetMapName([MarshalAs(UnmanagedType.LPStr)] [In] string mapName);

	// Token: 0x06000951 RID: 2385
	[DllImport("SteamworksNative")]
	internal static extern void GameServer_SetPasswordProtected([MarshalAs(UnmanagedType.I1)] bool passwordProtected);

	// Token: 0x06000952 RID: 2386
	[DllImport("SteamworksNative")]
	internal static extern void GameServer_SetSpectatorPort(ushort spectatorPort);

	// Token: 0x06000953 RID: 2387
	[DllImport("SteamworksNative")]
	internal static extern void GameServer_SetSpectatorServerName([MarshalAs(UnmanagedType.LPStr)] [In] string spectatorServerName);

	// Token: 0x06000954 RID: 2388
	[DllImport("SteamworksNative")]
	internal static extern void GameServer_ClearAllKeyValues();

	// Token: 0x06000955 RID: 2389
	[DllImport("SteamworksNative")]
	internal static extern void GameServer_SetKeyValue([MarshalAs(UnmanagedType.LPStr)] [In] string key, [MarshalAs(UnmanagedType.LPStr)] [In] string value);

	// Token: 0x06000956 RID: 2390
	[DllImport("SteamworksNative")]
	internal static extern void GameServer_SetGameTags([MarshalAs(UnmanagedType.LPStr)] [In] string gameTags);

	// Token: 0x06000957 RID: 2391
	[DllImport("SteamworksNative")]
	internal static extern void GameServer_SetGameData([MarshalAs(UnmanagedType.LPStr)] [In] string gameData);

	// Token: 0x06000958 RID: 2392
	[DllImport("SteamworksNative")]
	internal static extern void GameServer_SetRegion([MarshalAs(UnmanagedType.LPStr)] [In] string region);

	// Token: 0x06000959 RID: 2393
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool GameServer_SendUserConnectAndAuthenticate(uint ipClient, IntPtr authBlob, uint authBlobSize, ref ulong steamIDUser);

	// Token: 0x0600095A RID: 2394
	[DllImport("SteamworksNative")]
	internal static extern ulong GameServer_CreateUnauthenticatedUserConnection();

	// Token: 0x0600095B RID: 2395
	[DllImport("SteamworksNative")]
	internal static extern void GameServer_SendUserDisconnect(ulong steamIDUser);

	// Token: 0x0600095C RID: 2396
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool GameServer_UpdateUserData(ulong steamIDUser, [MarshalAs(UnmanagedType.LPStr)] [In] string playerName, uint core);

	// Token: 0x0600095D RID: 2397
	[DllImport("SteamworksNative")]
	internal static extern uint GameServer_GetAuthSessionTicket(IntPtr ticket, int maxTicket, ref uint ticketSize);

	// Token: 0x0600095E RID: 2398
	[DllImport("SteamworksNative")]
	internal static extern int GameServer_BeginAuthSession(IntPtr authTicket, int authTicketSize, ulong steamID);

	// Token: 0x0600095F RID: 2399
	[DllImport("SteamworksNative")]
	internal static extern void GameServer_EndAuthSession(ulong steamID);

	// Token: 0x06000960 RID: 2400
	[DllImport("SteamworksNative")]
	internal static extern void GameServer_CancelAuthTicket(uint authTicket);

	// Token: 0x06000961 RID: 2401
	[DllImport("SteamworksNative")]
	internal static extern int GameServer_UserHasLicenseForApp(ulong steamID, uint appID);

	// Token: 0x06000962 RID: 2402
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool GameServer_RequestUserGroupStatus(ulong steamIDUser, ulong steamIDGroup);

	// Token: 0x06000963 RID: 2403
	[DllImport("SteamworksNative")]
	internal static extern void GameServer_GetGameplayStats();

	// Token: 0x06000964 RID: 2404
	[DllImport("SteamworksNative")]
	internal static extern void GameServer_GetServerReputation();

	// Token: 0x06000965 RID: 2405
	[DllImport("SteamworksNative")]
	internal static extern uint GameServer_GetPublicIP();

	// Token: 0x06000966 RID: 2406
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool GameServer_HandleIncomingPacket(IntPtr data, int dataSize, uint ip, ushort srcPort);

	// Token: 0x06000967 RID: 2407
	[DllImport("SteamworksNative")]
	internal static extern int GameServer_GetNextOutgoingPacket(IntPtr @out, int maxOut, ref uint netAdr, ref ushort port);

	// Token: 0x06000968 RID: 2408
	[DllImport("SteamworksNative")]
	internal static extern void GameServer_EnableHeartbeats([MarshalAs(UnmanagedType.I1)] bool active);

	// Token: 0x06000969 RID: 2409
	[DllImport("SteamworksNative")]
	internal static extern void GameServer_SetHeartbeatInterval(int heartbeatInterval);

	// Token: 0x0600096A RID: 2410
	[DllImport("SteamworksNative")]
	internal static extern void GameServer_ForceHeartbeat();

	// Token: 0x0600096B RID: 2411
	[DllImport("SteamworksNative")]
	internal static extern void GameServer_AssociateWithClan(ulong steamIDClan);

	// Token: 0x0600096C RID: 2412
	[DllImport("SteamworksNative")]
	internal static extern void GameServer_ComputeNewPlayerCompatibility(ulong steamIDNewPlayer);

	// Token: 0x0600096D RID: 2413
	[DllImport("SteamworksNative")]
	internal static extern void GameServerStats_RequestUserStats(ulong steamIDUser);

	// Token: 0x0600096E RID: 2414
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool GameServerStats_GetUserStatInt(ulong steamIDUser, [MarshalAs(UnmanagedType.LPStr)] [In] string name, ref int data);

	// Token: 0x0600096F RID: 2415
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool GameServerStats_GetUserStatFloat(ulong steamIDUser, [MarshalAs(UnmanagedType.LPStr)] [In] string name, ref float data);

	// Token: 0x06000970 RID: 2416
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool GameServerStats_GetUserAchievement(ulong steamIDUser, [MarshalAs(UnmanagedType.LPStr)] [In] string name, ref bool achieved);

	// Token: 0x06000971 RID: 2417
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool GameServerStats_SetUserStatInt(ulong steamIDUser, [MarshalAs(UnmanagedType.LPStr)] [In] string name, int data);

	// Token: 0x06000972 RID: 2418
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool GameServerStats_SetUserStatFloat(ulong steamIDUser, [MarshalAs(UnmanagedType.LPStr)] [In] string name, float data);

	// Token: 0x06000973 RID: 2419
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool GameServerStats_UpdateUserAvgRateStat(ulong steamIDUser, [MarshalAs(UnmanagedType.LPStr)] [In] string name, float countThisSession, double sessionLength);

	// Token: 0x06000974 RID: 2420
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool GameServerStats_SetUserAchievement(ulong steamIDUser, [MarshalAs(UnmanagedType.LPStr)] [In] string name);

	// Token: 0x06000975 RID: 2421
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool GameServerStats_ClearUserAchievement(ulong steamIDUser, [MarshalAs(UnmanagedType.LPStr)] [In] string name);

	// Token: 0x06000976 RID: 2422
	[DllImport("SteamworksNative")]
	internal static extern void GameServerStats_StoreUserStats(ulong steamIDUser);

	// Token: 0x06000977 RID: 2423
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Networking_SendP2PPacket(ulong steamIDRemote, IntPtr data, uint cubData, int p2pSendType, int channel);

	// Token: 0x06000978 RID: 2424
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Networking_SendP2PPacketOffset(ulong steamIDRemote, IntPtr data, uint cubData, uint dataOffset, int p2pSendType, int channel);

	// Token: 0x06000979 RID: 2425
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Networking_IsP2PPacketAvailable(ref uint msgSize, int channel);

	// Token: 0x0600097A RID: 2426
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Networking_ReadP2PPacket(IntPtr dest, uint cubDest, ref uint msgSize, ref ulong steamIDRemote, int channel);

	// Token: 0x0600097B RID: 2427
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Networking_AcceptP2PSessionWithUser(ulong steamIDRemote);

	// Token: 0x0600097C RID: 2428
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Networking_CloseP2PSessionWithUser(ulong steamIDRemote);

	// Token: 0x0600097D RID: 2429
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Networking_CloseP2PChannelWithUser(ulong steamIDRemote, int channel);

	// Token: 0x0600097E RID: 2430
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Networking_GetP2PSessionState(ulong steamIDRemote, IntPtr connectionState);

	// Token: 0x0600097F RID: 2431
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Networking_AllowP2PPacketRelay([MarshalAs(UnmanagedType.I1)] bool allow);

	// Token: 0x06000980 RID: 2432
	[DllImport("SteamworksNative")]
	internal static extern uint Networking_CreateListenSocket(int virtualP2PPort, uint ip, ushort port, [MarshalAs(UnmanagedType.I1)] bool allowUseOfPacketRelay);

	// Token: 0x06000981 RID: 2433
	[DllImport("SteamworksNative")]
	internal static extern uint Networking_CreateP2PConnectionSocket(ulong steamIDTarget, int virtualPort, int timeoutSec, [MarshalAs(UnmanagedType.I1)] bool allowUseOfPacketRelay);

	// Token: 0x06000982 RID: 2434
	[DllImport("SteamworksNative")]
	internal static extern uint Networking_CreateConnectionSocket(uint ip, ushort port, int timeoutSec);

	// Token: 0x06000983 RID: 2435
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Networking_DestroySocket(uint socket, [MarshalAs(UnmanagedType.I1)] bool notifyRemoteEnd);

	// Token: 0x06000984 RID: 2436
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Networking_DestroyListenSocket(uint socket, [MarshalAs(UnmanagedType.I1)] bool notifyRemoteEnd);

	// Token: 0x06000985 RID: 2437
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Networking_SendDataOnSocket(uint socket, IntPtr data, uint cubData, [MarshalAs(UnmanagedType.I1)] bool reliable);

	// Token: 0x06000986 RID: 2438
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Networking_IsDataAvailableOnSocket(uint socket, ref uint msgSize);

	// Token: 0x06000987 RID: 2439
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Networking_RetrieveDataFromSocket(uint socket, IntPtr dest, uint cubDest, ref uint msgSize);

	// Token: 0x06000988 RID: 2440
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Networking_IsDataAvailable(uint listenSocket, ref uint msgSize, ref uint socket);

	// Token: 0x06000989 RID: 2441
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Networking_RetrieveData(uint listenSocket, IntPtr pubDest, uint cubDest, ref uint msgSize, ref uint socket);

	// Token: 0x0600098A RID: 2442
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Networking_GetSocketInfo(uint socket, ref ulong steamIDRemote, ref int socketStatus, ref uint ipRemote, ref ushort portRemote);

	// Token: 0x0600098B RID: 2443
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Networking_GetListenSocketInfo(uint listenSocket, ref uint ip, ref ushort port);

	// Token: 0x0600098C RID: 2444
	[DllImport("SteamworksNative")]
	internal static extern int Networking_GetSocketConnectionType(uint socket);

	// Token: 0x0600098D RID: 2445
	[DllImport("SteamworksNative")]
	internal static extern int Networking_GetMaxPacketSize(uint socket);

	// Token: 0x0600098E RID: 2446
	[DllImport("SteamworksNative")]
	internal static extern int Networking_GetP2PSessionStateSize();

	// Token: 0x0600098F RID: 2447
	[DllImport("SteamworksNative")]
	internal static extern uint ServicesGameServer_GetInterfaceVersion();

	// Token: 0x06000990 RID: 2448
	[DllImport("SteamworksNative")]
	internal static extern int ServicesGameServer_GetErrorCode();

	// Token: 0x06000991 RID: 2449
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool ServicesGameServer_Startup(uint interfaceVersion, uint ip, ushort steamPort, ushort gamePort, ushort queryPort, int serverMode, [MarshalAs(UnmanagedType.LPStr)] [In] string versionString);

	// Token: 0x06000992 RID: 2450
	[DllImport("SteamworksNative")]
	internal static extern void ServicesGameServer_Shutdown();

	// Token: 0x06000993 RID: 2451
	[DllImport("SteamworksNative")]
	internal static extern int ServicesGameServer_GetSteamLoadStatus();

	// Token: 0x06000994 RID: 2452
	[DllImport("SteamworksNative")]
	internal static extern void ServicesGameServer_HandleCallbacks();

	// Token: 0x06000995 RID: 2453
	[DllImport("SteamworksNative")]
	internal static extern void ServicesGameServer_RegisterManagedCallbacks(ManagedCallback callback, ManagedResultCallback resultCallback);

	// Token: 0x06000996 RID: 2454
	[DllImport("SteamworksNative")]
	internal static extern void ServicesGameServer_RemoveManagedCallbacks();

	// Token: 0x06000997 RID: 2455
	[DllImport("SteamworksNative")]
	internal static extern uint Utils_GetSecondsSinceAppActive();

	// Token: 0x06000998 RID: 2456
	[DllImport("SteamworksNative")]
	internal static extern uint Utils_GetSecondsSinceComputerActive();

	// Token: 0x06000999 RID: 2457
	[DllImport("SteamworksNative")]
	internal static extern int Utils_GetConnectedUniverse();

	// Token: 0x0600099A RID: 2458
	[DllImport("SteamworksNative")]
	internal static extern uint Utils_GetServerRealTime();

	// Token: 0x0600099B RID: 2459
	[DllImport("SteamworksNative")]
	internal static extern IntPtr Utils_GetIPCountry();

	// Token: 0x0600099C RID: 2460
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Utils_GetImageSize(int iImage, ref uint pnWidth, ref uint pnHeightr);

	// Token: 0x0600099D RID: 2461
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Utils_GetImageRGBA(int iImage, IntPtr pubDest, int nDestBufferSize);

	// Token: 0x0600099E RID: 2462
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Utils_GetCSERIPPort(ref uint unIP, ref ushort usPort);

	// Token: 0x0600099F RID: 2463
	[DllImport("SteamworksNative")]
	internal static extern byte Utils_GetCurrentBatteryPower();

	// Token: 0x060009A0 RID: 2464
	[DllImport("SteamworksNative")]
	internal static extern uint Utils_GetAppID();

	// Token: 0x060009A1 RID: 2465
	[DllImport("SteamworksNative")]
	internal static extern void Utils_SetOverlayNotificationPosition(int eNotificationPosition);

	// Token: 0x060009A2 RID: 2466
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Utils_IsAPICallCompleted(ulong hSteamAPICall, ref bool pbFailed);

	// Token: 0x060009A3 RID: 2467
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Utils_GetAPICallResult(ulong hSteamAPICall, IntPtr pCallback, int cubCallback, int iCallbackExpected, ref bool pbFailed);

	// Token: 0x060009A4 RID: 2468
	[DllImport("SteamworksNative")]
	internal static extern void Utils_RunFrame();

	// Token: 0x060009A5 RID: 2469
	[DllImport("SteamworksNative")]
	internal static extern uint Utils_GetIPCCallCount();

	// Token: 0x060009A6 RID: 2470
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Utils_IsOverlayEnabled();

	// Token: 0x060009A7 RID: 2471
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Utils_OverlayNeedsPresent();

	// Token: 0x060009A8 RID: 2472
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Utils_ShowGamepadTextInput(int inputMode, int lineInputMode, [MarshalAs(UnmanagedType.LPStr)] [In] string description, uint charMax);

	// Token: 0x060009A9 RID: 2473
	[DllImport("SteamworksNative")]
	internal static extern uint Utils_GetEnteredGamepadTextLength();

	// Token: 0x060009AA RID: 2474
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Utils_GetEnteredGamepadTextInput(IntPtr pchText, uint cchText);

	// Token: 0x060009AB RID: 2475
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Utils_IsSteamRunningInVR();

	// Token: 0x060009AC RID: 2476
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Apps_IsSubscribed();

	// Token: 0x060009AD RID: 2477
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Apps_IsLowViolence();

	// Token: 0x060009AE RID: 2478
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Apps_IsCybercafe();

	// Token: 0x060009AF RID: 2479
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Apps_IsVACBanned();

	// Token: 0x060009B0 RID: 2480
	[DllImport("SteamworksNative")]
	internal static extern IntPtr Apps_GetCurrentGameLanguage();

	// Token: 0x060009B1 RID: 2481
	[DllImport("SteamworksNative")]
	internal static extern IntPtr Apps_GetAvailableGameLanguages();

	// Token: 0x060009B2 RID: 2482
	[DllImport("SteamworksNative")]
	internal static extern bool Apps_IsSubscribedApp(uint appID);

	// Token: 0x060009B3 RID: 2483
	[DllImport("SteamworksNative")]
	internal static extern bool Apps_IsDlcInstalled(uint appID);

	// Token: 0x060009B4 RID: 2484
	[DllImport("SteamworksNative")]
	internal static extern uint Apps_GetEarliestPurchaseUnixTime(uint appID);

	// Token: 0x060009B5 RID: 2485
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Apps_IsSubscribedFromFreeWeekend();

	// Token: 0x060009B6 RID: 2486
	[DllImport("SteamworksNative")]
	internal static extern int Apps_GetDLCCount();

	// Token: 0x060009B7 RID: 2487
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Apps_GetDLCDataByIndex(int iDLC, ref uint pAppID, ref bool pbAvailable, IntPtr pchName, int cchNameBufferSize);

	// Token: 0x060009B8 RID: 2488
	[DllImport("SteamworksNative")]
	internal static extern void Apps_InstallDLC(uint appID);

	// Token: 0x060009B9 RID: 2489
	[DllImport("SteamworksNative")]
	internal static extern void Apps_UninstallDLC(uint appID);

	// Token: 0x060009BA RID: 2490
	[DllImport("SteamworksNative")]
	internal static extern void Apps_RequestAppProofOfPurchaseKey(uint appID);

	// Token: 0x060009BB RID: 2491
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Apps_GetCurrentBetaName(IntPtr pchName, int cchNameBufferSize);

	// Token: 0x060009BC RID: 2492
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Apps_MarkContentCorrupt([MarshalAs(UnmanagedType.I1)] bool bMissingFilesOnly);

	// Token: 0x060009BD RID: 2493
	[DllImport("SteamworksNative")]
	internal static extern uint Apps_GetInstalledDepots(uint appID, ref uint pDepots, uint maxDepots);

	// Token: 0x060009BE RID: 2494
	[DllImport("SteamworksNative")]
	internal static extern ulong Apps_GetAppOwner();

	// Token: 0x060009BF RID: 2495
	[DllImport("SteamworksNative")]
	internal static extern IntPtr Apps_GetLaunchQueryParam([MarshalAs(UnmanagedType.LPStr)] [In] string key);

	// Token: 0x060009C0 RID: 2496
	[DllImport("SteamworksNative")]
	internal static extern uint HTTP_CreateHTTPRequest(int eHTTPRequestMethod, IntPtr pchAbsoluteURL);

	// Token: 0x060009C1 RID: 2497
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool HTTP_SetHTTPRequestContextValue(uint hRequest, ulong ulContextValue);

	// Token: 0x060009C2 RID: 2498
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool HTTP_SetHTTPRequestNetworkActivityTimeout(uint hRequest, uint unTimeoutSeconds);

	// Token: 0x060009C3 RID: 2499
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool HTTP_SetHTTPRequestHeaderValue(uint hRequest, IntPtr pchHeaderName, IntPtr pchHeaderValue);

	// Token: 0x060009C4 RID: 2500
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool HTTP_SetHTTPRequestGetOrPostParameter(uint hRequest, IntPtr pchParamName, IntPtr pchParamValue);

	// Token: 0x060009C5 RID: 2501
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool HTTP_SendHTTPRequest(uint hRequest, ref ulong pCallHandle);

	// Token: 0x060009C6 RID: 2502
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool HTTP_SendHTTPRequestAndStreamResponse(uint hRequest, ref ulong pCallHandle);

	// Token: 0x060009C7 RID: 2503
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool HTTP_DeferHTTPRequest(uint hRequest);

	// Token: 0x060009C8 RID: 2504
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool HTTP_PrioritizeHTTPRequest(uint hRequest);

	// Token: 0x060009C9 RID: 2505
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool HTTP_GetHTTPResponseHeaderSize(uint hRequest, IntPtr pchHeaderName, ref uint unResponseHeaderSize);

	// Token: 0x060009CA RID: 2506
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool HTTP_GetHTTPResponseHeaderValue(uint hRequest, IntPtr pchHeaderName, IntPtr pHeaderValueBuffer, uint unBufferSize);

	// Token: 0x060009CB RID: 2507
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool HTTP_GetHTTPResponseBodySize(uint hRequest, ref uint unBodySize);

	// Token: 0x060009CC RID: 2508
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool HTTP_GetHTTPResponseBodyData(uint hRequest, IntPtr pBodyDataBuffer, uint unBufferSize);

	// Token: 0x060009CD RID: 2509
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool HTTP_GetHTTPStreamingResponseBodyData(uint hRequest, uint cOffset, IntPtr pBodyDataBuffer, uint unBufferSize);

	// Token: 0x060009CE RID: 2510
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool HTTP_ReleaseHTTPRequest(uint hRequest);

	// Token: 0x060009CF RID: 2511
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool HTTP_GetHTTPDownloadProgressPct(uint hRequest, ref float pflPercentOut);

	// Token: 0x060009D0 RID: 2512
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool HTTP_SetHTTPRequestRawPostBody(uint hRequest, IntPtr pchContentType, IntPtr pubBody, uint unBodyLen);

	// Token: 0x060009D1 RID: 2513
	[DllImport("SteamworksNative")]
	internal static extern uint Screenshots_WriteScreenshot(IntPtr pubRGB, uint cubRGB, int nWidth, int nHeight);

	// Token: 0x060009D2 RID: 2514
	[DllImport("SteamworksNative")]
	internal static extern uint Screenshots_AddScreenshotToLibrary([MarshalAs(UnmanagedType.LPStr)] [In] string pchFilename, [MarshalAs(UnmanagedType.LPStr)] [In] string pchThumbnailFilename, int nWidth, int nHeight);

	// Token: 0x060009D3 RID: 2515
	[DllImport("SteamworksNative")]
	internal static extern void Screenshots_TriggerScreenshot();

	// Token: 0x060009D4 RID: 2516
	[DllImport("SteamworksNative")]
	internal static extern void Screenshots_HookScreenshots(bool bHook);

	// Token: 0x060009D5 RID: 2517
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Screenshots_SetLocation(uint hScreenshot, [MarshalAs(UnmanagedType.LPStr)] [In] string pchLocation);

	// Token: 0x060009D6 RID: 2518
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Screenshots_TagUser(uint hScreenshot, ulong steamID);

	// Token: 0x060009D7 RID: 2519
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool Screenshots_TagPublishedFile(uint hScreenshot, ulong unPublishedFileID);

	// Token: 0x060009D8 RID: 2520
	[DllImport("SteamworksNative")]
	internal static extern ulong UGC_CreateQueryUserUGCRequest(uint unAccountID, int eListType, int eMatchingUGCType, int eSortOrder, uint nCreatorAppID, uint nConsumerAppID, uint unPage);

	// Token: 0x060009D9 RID: 2521
	[DllImport("SteamworksNative")]
	internal static extern ulong UGC_CreateQueryAllUGCRequest(int eQueryType, int eMatchingeMatchingUGCTypeFileType, uint nCreatorAppID, uint nConsumerAppID, uint unPage);

	// Token: 0x060009DA RID: 2522
	[DllImport("SteamworksNative")]
	internal static extern void UGC_SendQueryUGCRequest(ulong handle);

	// Token: 0x060009DB RID: 2523
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool UGC_GetQueryUGCResult(ulong handle, uint index, IntPtr pDetails);

	// Token: 0x060009DC RID: 2524
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool UGC_ReleaseQueryUGCRequest(ulong handle);

	// Token: 0x060009DD RID: 2525
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool UGC_AddRequiredTag(ulong handle, [MarshalAs(UnmanagedType.LPStr)] [In] string pTagName);

	// Token: 0x060009DE RID: 2526
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool UGC_AddExcludedTag(ulong handle, [MarshalAs(UnmanagedType.LPStr)] [In] string pTagName);

	// Token: 0x060009DF RID: 2527
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool UGC_SetReturnLongDescription(ulong handle, [MarshalAs(UnmanagedType.I1)] bool bReturnLongDescription);

	// Token: 0x060009E0 RID: 2528
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool UGC_SetReturnTotalOnly(ulong handle, [MarshalAs(UnmanagedType.I1)] bool bReturnTotalOnly);

	// Token: 0x060009E1 RID: 2529
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool UGC_SetCloudFileNameFilter(ulong handle, [MarshalAs(UnmanagedType.LPStr)] [In] string pMatchCloudFileName);

	// Token: 0x060009E2 RID: 2530
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool UGC_SetMatchAnyTag(ulong handle, [MarshalAs(UnmanagedType.I1)] bool bMatchAnyTag);

	// Token: 0x060009E3 RID: 2531
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool UGC_SetSearchText(ulong handle, [MarshalAs(UnmanagedType.LPStr)] [In] string pSearchText);

	// Token: 0x060009E4 RID: 2532
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool UGC_SetRankedByTrendDays(ulong handle, uint unDays);

	// Token: 0x060009E5 RID: 2533
	[DllImport("SteamworksNative")]
	internal static extern void UGC_RequestUGCDetails(ulong nPublishedFileID);

	// Token: 0x060009E6 RID: 2534
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool SteamController_Init([MarshalAs(UnmanagedType.LPStr)] [In] string absolutPathToControllerConfigVDF);

	// Token: 0x060009E7 RID: 2535
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool SteamController_Shutdown();

	// Token: 0x060009E8 RID: 2536
	[DllImport("SteamworksNative")]
	internal static extern void SteamController_RunFrame();

	// Token: 0x060009E9 RID: 2537
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool SteamController_GetControllerState(uint controllerIndex, IntPtr state);

	// Token: 0x060009EA RID: 2538
	[DllImport("SteamworksNative")]
	internal static extern void SteamController_TriggerHapticPulse(uint controllerIndex, int targetPad, ushort durationMicroSec);

	// Token: 0x060009EB RID: 2539
	[DllImport("SteamworksNative")]
	internal static extern void SteamController_SetOverrideMode([MarshalAs(UnmanagedType.LPStr)] [In] string mode);

	// Token: 0x060009EC RID: 2540
	[DllImport("SteamworksNative", EntryPoint = "VR_Hmd_Init")]
	internal static extern int VR_Init();

	// Token: 0x060009ED RID: 2541
	[DllImport("SteamworksNative", EntryPoint = "VR_Hmd_Shutdown")]
	internal static extern void VR_Shutdown();

	// Token: 0x060009EE RID: 2542
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool VR_Hmd_GetWindowBounds(ref int X, ref int Y, ref uint Width, ref uint Height);

	// Token: 0x060009EF RID: 2543
	[DllImport("SteamworksNative")]
	internal static extern void VR_Hmd_GetRecommendedRenderTargetSize(ref uint Width, ref uint Height);

	// Token: 0x060009F0 RID: 2544
	[DllImport("SteamworksNative")]
	internal static extern void VR_Hmd_GetEyeOutputViewport(int Eye, int APIType, ref uint X, ref uint Y, ref uint Width, ref uint Height);

	// Token: 0x060009F1 RID: 2545
	[DllImport("SteamworksNative")]
	internal static extern IntPtr VR_Hmd_GetProjectionMatrix(int Eye, float NearZ, float FarZ, int ProjType);

	// Token: 0x060009F2 RID: 2546
	[DllImport("SteamworksNative")]
	internal static extern void VR_Hmd_GetProjectionRaw(int Eye, ref float Left, ref float Right, ref float Top, ref float Bottom);

	// Token: 0x060009F3 RID: 2547
	[DllImport("SteamworksNative")]
	internal static extern IntPtr VR_Hmd_ComputeDistortion(int Eye, float U, float V);

	// Token: 0x060009F4 RID: 2548
	[DllImport("SteamworksNative")]
	internal static extern IntPtr VR_Hmd_GetEyeMatrix(int Eye);

	// Token: 0x060009F5 RID: 2549
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool VR_Hmd_GetViewMatrix(float SecondsFromNow, IntPtr MatLeftView, IntPtr MatRightView, ref int Result);

	// Token: 0x060009F6 RID: 2550
	[DllImport("SteamworksNative")]
	internal static extern int VR_Hmd_GetD3D9AdapterIndex();

	// Token: 0x060009F7 RID: 2551
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool VR_Hmd_GetWorldFromHeadPose(float PredictedSecondsFromNow, IntPtr Pose, ref int Result);

	// Token: 0x060009F8 RID: 2552
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool VR_Hmd_GetLastWorldFromHeadPose(IntPtr Pose);

	// Token: 0x060009F9 RID: 2553
	[DllImport("SteamworksNative")]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool VR_Hmd_WillDriftInYaw();

	// Token: 0x060009FA RID: 2554
	[DllImport("SteamworksNative")]
	internal static extern void VR_Hmd_ZeroTracker();

	// Token: 0x060009FB RID: 2555
	[DllImport("SteamworksNative")]
	internal static extern uint VR_Hmd_GetDriverId(IntPtr Buffer, uint BufferLen);

	// Token: 0x060009FC RID: 2556
	[DllImport("SteamworksNative")]
	internal static extern uint VR_Hmd_GetDisplayId(IntPtr Buffer, uint BufferLen);
}
