using System;
using System.Collections.Generic;
using System.Globalization;
using ManagedSteam.Exceptions;
using ManagedSteam.Implementations;
using ManagedSteam.SteamTypes;
using ManagedSteam.Utility;

namespace ManagedSteam
{
	// Token: 0x02000097 RID: 151
	public sealed class Steam
	{
		// Token: 0x06000481 RID: 1153 RVA: 0x0000828C File Offset: 0x0000648C
		private Steam(CultureInfo activeCulture)
		{
			this.Culture = activeCulture;
			this.bufferedExceptions = new List<Exception>();
			this.nativeCallbackHandle = new ManagedCallback(Steam.NativeCallbacks);
			this.nativeResultCallbackHandle = new ManagedResultCallback(Steam.NativeResultCallbacks);
			NativeMethods.Services_RegisterManagedCallbacks(this.nativeCallbackHandle, this.nativeResultCallbackHandle);
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x000082E6 File Offset: 0x000064E6
		// (set) Token: 0x06000483 RID: 1155 RVA: 0x000082EE File Offset: 0x000064EE
		internal Dictionary<CallbackID, NativeCallback> Callbacks { get; private set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x000082F7 File Offset: 0x000064F7
		// (set) Token: 0x06000485 RID: 1157 RVA: 0x000082FF File Offset: 0x000064FF
		internal Dictionary<ResultID, NativeResultCallback> ResultCallbacks { get; private set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x00008308 File Offset: 0x00006508
		// (set) Token: 0x06000487 RID: 1159 RVA: 0x00008310 File Offset: 0x00006510
		internal CultureInfo Culture { get; private set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x00008319 File Offset: 0x00006519
		// (set) Token: 0x06000489 RID: 1161 RVA: 0x00008320 File Offset: 0x00006520
		public static Steam Instance { get; private set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x00008328 File Offset: 0x00006528
		public bool IsAvailable
		{
			get
			{
				return Steam.Instance != null;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x00008335 File Offset: 0x00006535
		// (set) Token: 0x0600048C RID: 1164 RVA: 0x0000833D File Offset: 0x0000653D
		public AppID AppID { get; private set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x00008346 File Offset: 0x00006546
		public ICloud Cloud
		{
			get
			{
				return this.cloud;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x0000834E File Offset: 0x0000654E
		public IStats Stats
		{
			get
			{
				return this.stats;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x00008356 File Offset: 0x00006556
		public IUser User
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x0000835E File Offset: 0x0000655E
		public IFriends Friends
		{
			get
			{
				return this.friends;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x00008366 File Offset: 0x00006566
		public IMatchmaking Matchmaking
		{
			get
			{
				return this.matchmaking;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x0000836E File Offset: 0x0000656E
		public IMatchmakingServers MatchmakingServers
		{
			get
			{
				return this.matchmakingServers;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x00008376 File Offset: 0x00006576
		public INetworking Networking
		{
			get
			{
				return this.networking;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x0000837E File Offset: 0x0000657E
		public IUtils Utils
		{
			get
			{
				return this.utils;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x00008386 File Offset: 0x00006586
		public IApps Apps
		{
			get
			{
				return this.apps;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x0000838E File Offset: 0x0000658E
		public IHTTP HTTP
		{
			get
			{
				return this.http;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x00008396 File Offset: 0x00006596
		public IScreenshots Screenshots
		{
			get
			{
				return this.screenshots;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x0000839E File Offset: 0x0000659E
		public IUGC UGC
		{
			get
			{
				return this.ugc;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x000083A6 File Offset: 0x000065A6
		public ISteamController SteamController
		{
			get
			{
				return this.steamcontroller;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x000083AE File Offset: 0x000065AE
		public IHmd Hmd
		{
			get
			{
				return this.hmd;
			}
		}

		// Token: 0x14000051 RID: 81
		// (add) Token: 0x0600049B RID: 1179 RVA: 0x000083B8 File Offset: 0x000065B8
		// (remove) Token: 0x0600049C RID: 1180 RVA: 0x000083F0 File Offset: 0x000065F0
		public event Steam.ExceptionDelegate ExceptionThrown;

		// Token: 0x0600049D RID: 1181 RVA: 0x00008425 File Offset: 0x00006625
		public static Steam Initialize()
		{
			return Steam.Initialize(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00008434 File Offset: 0x00006634
		private static Steam Initialize(CultureInfo activeCulture)
		{
			if (activeCulture == null)
			{
				throw new ArgumentNullException("activeCulture", "activeCulture is null.");
			}
			if (Steam.Instance != null)
			{
				throw new InvalidOperationException(StringMap.GetString(StringID.OnlyOneInstance, new object[]
				{
					typeof(Steam).Name
				}));
			}
			Steam.Instance = new Steam(activeCulture);
			Steam.Instance.Startup();
			return Steam.Instance;
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00008600 File Offset: 0x00006800
		private void Startup()
		{
			if (NativeHelpers.Services_GetSteamLoadStatus() == LoadStatus.NotLoaded && !NativeMethods.Services_Startup(786U))
			{
				Steam.Instance = null;
				ErrorCodes errorCodes = NativeHelpers.Services_GetErrorCode();
				if (errorCodes == ErrorCodes.InvalidInterfaceVersion)
				{
					Error.ThrowError(ErrorCodes.InvalidInterfaceVersion, new object[]
					{
						NativeMethods.Services_GetInterfaceVersion(),
						786U
					});
				}
				else
				{
					Error.ThrowError(errorCodes, new object[0]);
				}
			}
			this.AppID = new AppID(NativeMethods.Services_GetAppID());
			this.serviceJobs = new JobManager();
			this.serviceJobs.AddJob(new DelegateJob(delegate()
			{
				this.RegisterManagedCallback();
			}, delegate()
			{
				this.RemoveManagedCallback();
			}));
			this.serviceJobs.AddJob(new DelegateJob(delegate()
			{
				this.cloud = new Cloud();
			}, delegate()
			{
				this.cloud.ReleaseManagedResources();
			}));
			this.serviceJobs.AddJob(new DelegateJob(delegate()
			{
				this.stats = new Stats();
			}, delegate()
			{
				this.stats.ReleaseManagedResources();
			}));
			this.serviceJobs.AddJob(new DelegateJob(delegate()
			{
				this.user = new User();
			}, delegate()
			{
				this.user.ReleaseManagedResources();
			}));
			this.serviceJobs.AddJob(new DelegateJob(delegate()
			{
				this.friends = new Friends();
			}, delegate()
			{
				this.friends.ReleaseManagedResources();
			}));
			this.serviceJobs.AddJob(new DelegateJob(delegate()
			{
				this.matchmaking = new MatchMaking();
			}, delegate()
			{
				this.matchmaking.ReleaseManagedResources();
			}));
			this.serviceJobs.AddJob(new DelegateJob(delegate()
			{
				this.matchmakingServers = new MatchmakingServers();
			}, delegate()
			{
				this.matchmakingServers.ReleaseManagedResources();
			}));
			this.serviceJobs.AddJob(new DelegateJob(delegate()
			{
				this.networking = new Networking();
			}, delegate()
			{
				this.networking.ReleaseManagedResources();
			}));
			this.serviceJobs.AddJob(new DelegateJob(delegate()
			{
				this.utils = new Utils();
			}, delegate()
			{
				this.utils.ReleaseManagedResources();
			}));
			this.serviceJobs.AddJob(new DelegateJob(delegate()
			{
				this.apps = new Apps();
			}, delegate()
			{
				this.apps.ReleaseManagedResources();
			}));
			this.serviceJobs.AddJob(new DelegateJob(delegate()
			{
				this.http = new HTTP();
			}, delegate()
			{
				this.http.ReleaseManagedResources();
			}));
			this.serviceJobs.AddJob(new DelegateJob(delegate()
			{
				this.screenshots = new Screenshots();
			}, delegate()
			{
				this.screenshots.ReleaseManagedResources();
			}));
			this.serviceJobs.AddJob(new DelegateJob(delegate()
			{
				this.ugc = new UGC();
			}, delegate()
			{
				this.ugc.ReleaseManagedResources();
			}));
			this.serviceJobs.AddJob(new DelegateJob(delegate()
			{
				this.steamcontroller = new SteamController();
			}, delegate()
			{
				this.steamcontroller.ReleaseManagedResources();
			}));
			this.hmd = new Hmd();
			this.serviceJobs.RunCreateJobs();
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x000088CF File Offset: 0x00006ACF
		public void Shutdown()
		{
			this.CheckIfUsable();
			this.ReleaseManagedResources();
			NativeMethods.Services_Shutdown();
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x000088E2 File Offset: 0x00006AE2
		public bool IsSteamRunning()
		{
			this.CheckIfUsable();
			return NativeMethods.Services_IsSteamRunning();
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x000088EF File Offset: 0x00006AEF
		public static bool RestartAppIfNecessary(uint ownAppID)
		{
			return NativeMethods.Services_RestartAppIfNecessary(ownAppID);
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x000088F7 File Offset: 0x00006AF7
		public bool RunCallbackSizeCheck()
		{
			return NativeMethods.Services_RunCallbackSizeCheck();
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00008900 File Offset: 0x00006B00
		public void Update()
		{
			this.CheckIfUsable();
			NativeMethods.Services_HandleCallbacks();
			this.ReportExceptions();
			this.matchmakingServers.ReportExceptions();
			this.cloud.InvokeEvents();
			this.stats.InvokeEvents();
			this.user.InvokeEvents();
			this.friends.InvokeEvents();
			this.matchmaking.InvokeEvents();
			this.matchmakingServers.InvokeEvents();
			this.networking.InvokeEvents();
			this.utils.InvokeEvents();
			this.apps.InvokeEvents();
			this.http.InvokeEvents();
			this.screenshots.InvokeEvents();
			this.ugc.InvokeEvents();
			this.steamcontroller.InvokeEvents();
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x000089B8 File Offset: 0x00006BB8
		private void ReportExceptions()
		{
			if (this.ExceptionThrown != null)
			{
				foreach (Exception e in this.bufferedExceptions)
				{
					this.ExceptionThrown(e);
				}
			}
			this.bufferedExceptions.Clear();
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00008A24 File Offset: 0x00006C24
		internal void ReportException(Exception e)
		{
			if (this.ExceptionThrown != null && e != null)
			{
				this.ExceptionThrown(e);
			}
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00008A3D File Offset: 0x00006C3D
		public void ReleaseManagedResources()
		{
			this.CheckIfUsable();
			this.serviceJobs.RunDestroyJobs();
			Steam.Instance = null;
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00008A58 File Offset: 0x00006C58
		private void RegisterManagedCallback()
		{
			this.Callbacks = new Dictionary<CallbackID, NativeCallback>();
			this.ResultCallbacks = new Dictionary<ResultID, NativeResultCallback>();
			this.bufferedExceptions = new List<Exception>();
			this.NativeCallbacksDelegate = new ManagedCallback(Steam.NativeCallbacks);
			this.NativeResultCallbacksDelegate = new ManagedResultCallback(Steam.NativeResultCallbacks);
			NativeMethods.Services_RegisterManagedCallbacks(this.NativeCallbacksDelegate, this.NativeResultCallbacksDelegate);
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00008ABB File Offset: 0x00006CBB
		private void RemoveManagedCallback()
		{
			NativeMethods.Services_RemoveManagedCallbacks();
			this.ResultCallbacks = null;
			this.Callbacks = null;
			this.nativeCallbackHandle = null;
			this.nativeResultCallbackHandle = null;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00008AE0 File Offset: 0x00006CE0
		private static void NativeCallbacks(int id, IntPtr dataPointer, int dataSize)
		{
			try
			{
				if (!Steam.Instance.Callbacks.ContainsKey((CallbackID)id))
				{
					throw new ManagedException(ErrorCodes.NoCallbackEvent, new object[]
					{
						id
					});
				}
				Steam.Instance.Callbacks[(CallbackID)id](dataPointer, dataSize);
			}
			catch (Exception item)
			{
				try
				{
					Steam.Instance.bufferedExceptions.Add(item);
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00008B6C File Offset: 0x00006D6C
		private static void NativeResultCallbacks(int id, IntPtr dataPointer, int dataSize, bool flag)
		{
			try
			{
				if (!Steam.Instance.ResultCallbacks.ContainsKey((ResultID)id))
				{
					throw new ManagedException(ErrorCodes.NoResultEvent, new object[]
					{
						id
					});
				}
				Steam.Instance.ResultCallbacks[(ResultID)id](dataPointer, dataSize, flag);
			}
			catch (Exception item)
			{
				try
				{
					Steam.Instance.bufferedExceptions.Add(item);
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00008BF8 File Offset: 0x00006DF8
		internal void CheckIfUsable()
		{
			if (!this.IsAvailable)
			{
				throw new InvalidOperationException(StringMap.GetString(ErrorCodes.UsageAfterAPIShutdown, new object[0]));
			}
		}

		// Token: 0x040002A5 RID: 677
		private JobManager serviceJobs;

		// Token: 0x040002A6 RID: 678
		private Cloud cloud;

		// Token: 0x040002A7 RID: 679
		private Stats stats;

		// Token: 0x040002A8 RID: 680
		private User user;

		// Token: 0x040002A9 RID: 681
		private Friends friends;

		// Token: 0x040002AA RID: 682
		private MatchMaking matchmaking;

		// Token: 0x040002AB RID: 683
		private MatchmakingServers matchmakingServers;

		// Token: 0x040002AC RID: 684
		private Networking networking;

		// Token: 0x040002AD RID: 685
		private Utils utils;

		// Token: 0x040002AE RID: 686
		private Apps apps;

		// Token: 0x040002AF RID: 687
		private HTTP http;

		// Token: 0x040002B0 RID: 688
		private Screenshots screenshots;

		// Token: 0x040002B1 RID: 689
		private UGC ugc;

		// Token: 0x040002B2 RID: 690
		private SteamController steamcontroller;

		// Token: 0x040002B3 RID: 691
		private Hmd hmd;

		// Token: 0x040002B4 RID: 692
		private List<Exception> bufferedExceptions;

		// Token: 0x040002B5 RID: 693
		private ManagedCallback nativeCallbackHandle;

		// Token: 0x040002B6 RID: 694
		private ManagedResultCallback nativeResultCallbackHandle;

		// Token: 0x040002B7 RID: 695
		private ManagedCallback NativeCallbacksDelegate;

		// Token: 0x040002B8 RID: 696
		private ManagedResultCallback NativeResultCallbacksDelegate;

		// Token: 0x02000098 RID: 152
		// (Invoke) Token: 0x060004CA RID: 1226
		public delegate void ExceptionDelegate(Exception e);
	}
}
