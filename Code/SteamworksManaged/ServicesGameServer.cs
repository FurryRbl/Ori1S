using System;
using System.Collections.Generic;
using System.Globalization;
using ManagedSteam.Exceptions;
using ManagedSteam.Implementations;
using ManagedSteam.SteamTypes;
using ManagedSteam.Utility;

namespace ManagedSteam
{
	// Token: 0x02000157 RID: 343
	public sealed class ServicesGameServer
	{
		// Token: 0x06000BBA RID: 3002 RVA: 0x0000FD4B File Offset: 0x0000DF4B
		private ServicesGameServer(CultureInfo activeCulture)
		{
			this.Culture = activeCulture;
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x0000FD5A File Offset: 0x0000DF5A
		// (set) Token: 0x06000BBC RID: 3004 RVA: 0x0000FD62 File Offset: 0x0000DF62
		internal Dictionary<CallbackID, NativeCallback> Callbacks { get; private set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x0000FD6B File Offset: 0x0000DF6B
		// (set) Token: 0x06000BBE RID: 3006 RVA: 0x0000FD73 File Offset: 0x0000DF73
		internal Dictionary<ResultID, NativeResultCallback> ResultCallbacks { get; private set; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000BBF RID: 3007 RVA: 0x0000FD7C File Offset: 0x0000DF7C
		// (set) Token: 0x06000BC0 RID: 3008 RVA: 0x0000FD84 File Offset: 0x0000DF84
		internal CultureInfo Culture { get; private set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x0000FD8D File Offset: 0x0000DF8D
		// (set) Token: 0x06000BC2 RID: 3010 RVA: 0x0000FD94 File Offset: 0x0000DF94
		public static ServicesGameServer Instance { get; private set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000BC3 RID: 3011 RVA: 0x0000FD9C File Offset: 0x0000DF9C
		public bool IsAvailable
		{
			get
			{
				return ServicesGameServer.Instance != null;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x0000FDA9 File Offset: 0x0000DFA9
		public IGameServer GameServer
		{
			get
			{
				return this.gameServer;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x0000FDB1 File Offset: 0x0000DFB1
		public IGameServerStats GameServerStats
		{
			get
			{
				return this.gameServerStats;
			}
		}

		// Token: 0x140000D8 RID: 216
		// (add) Token: 0x06000BC6 RID: 3014 RVA: 0x0000FDBC File Offset: 0x0000DFBC
		// (remove) Token: 0x06000BC7 RID: 3015 RVA: 0x0000FDF4 File Offset: 0x0000DFF4
		public event ServicesGameServer.ExceptionDelegate ExceptionThrown;

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0000FE29 File Offset: 0x0000E029
		public static ServicesGameServer Initialize(uint ip, ushort steamPort, ushort gamePort, ushort queryPort, ServerMode serverMode, string versionString)
		{
			return ServicesGameServer.Initialize(CultureInfo.InvariantCulture, ip, steamPort, gamePort, queryPort, serverMode, versionString);
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0000FE40 File Offset: 0x0000E040
		private static ServicesGameServer Initialize(CultureInfo activeCulture, uint ip, ushort steamPort, ushort gamePort, ushort queryPort, ServerMode serverMode, string versionString)
		{
			if (activeCulture == null)
			{
				throw new ArgumentNullException("activeCulture", "activeCulture is null.");
			}
			if (ServicesGameServer.Instance != null)
			{
				throw new InvalidOperationException(StringMap.GetString(StringID.OnlyOneInstance, new object[]
				{
					typeof(ServicesGameServer).Name
				}));
			}
			ServicesGameServer.Instance = new ServicesGameServer(activeCulture);
			ServicesGameServer.Instance.Startup(ip, steamPort, gamePort, queryPort, serverMode, versionString);
			return ServicesGameServer.Instance;
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x0000FEF4 File Offset: 0x0000E0F4
		private void Startup(uint ip, ushort steamPort, ushort gamePort, ushort queryPort, ServerMode serverMode, string versionString)
		{
			if (NativeHelpers.ServicesGameServer_GetSteamLoadStatus() == LoadStatus.NotLoaded && !NativeMethods.ServicesGameServer_Startup(786U, ip, steamPort, gamePort, queryPort, (int)serverMode, versionString))
			{
				ServicesGameServer.Instance = null;
				ErrorCodes errorCodes = NativeHelpers.ServicesGameServer_GetErrorCode();
				if (errorCodes == ErrorCodes.InvalidInterfaceVersion)
				{
					Error.ThrowError(ErrorCodes.InvalidInterfaceVersion, new object[]
					{
						NativeMethods.ServicesGameServer_GetInterfaceVersion(),
						786U
					});
				}
				else
				{
					Error.ThrowError(errorCodes, new object[0]);
				}
			}
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
				this.gameServer = new GameServer();
			}, delegate()
			{
				this.gameServer.ReleaseManagedResources();
			}));
			this.serviceJobs.AddJob(new DelegateJob(delegate()
			{
				this.gameServerStats = new GameServerStats();
			}, delegate()
			{
				this.gameServerStats.ReleaseManagedResources();
			}));
			this.serviceJobs.RunCreateJobs();
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x0000FFF9 File Offset: 0x0000E1F9
		public void Shutdown()
		{
			this.CheckIfUsable();
			this.ReleaseManagedResources();
			NativeMethods.ServicesGameServer_Shutdown();
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x0001000C File Offset: 0x0000E20C
		public void Update()
		{
			this.CheckIfUsable();
			NativeMethods.ServicesGameServer_HandleCallbacks();
			this.ReportExceptions();
			this.gameServer.InvokeEvents();
			this.gameServerStats.InvokeEvents();
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x00010038 File Offset: 0x0000E238
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

		// Token: 0x06000BCE RID: 3022 RVA: 0x000100A4 File Offset: 0x0000E2A4
		public void ReleaseManagedResources()
		{
			this.CheckIfUsable();
			this.serviceJobs.RunDestroyJobs();
			ServicesGameServer.Instance = null;
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x000100C0 File Offset: 0x0000E2C0
		private void RegisterManagedCallback()
		{
			this.Callbacks = new Dictionary<CallbackID, NativeCallback>(16);
			this.ResultCallbacks = new Dictionary<ResultID, NativeResultCallback>(16);
			this.bufferedExceptions = new List<Exception>();
			NativeMethods.ServicesGameServer_RegisterManagedCallbacks(new ManagedCallback(ServicesGameServer.NativeCallbacks), new ManagedResultCallback(ServicesGameServer.NativeResultCallbacks));
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x0001010F File Offset: 0x0000E30F
		private void RemoveManagedCallback()
		{
			NativeMethods.ServicesGameServer_RemoveManagedCallbacks();
			this.ResultCallbacks = null;
			this.Callbacks = null;
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x00010124 File Offset: 0x0000E324
		private static void NativeCallbacks(int id, IntPtr dataPointer, int dataSize)
		{
			try
			{
				if (!ServicesGameServer.Instance.Callbacks.ContainsKey((CallbackID)id))
				{
					throw new ManagedException(ErrorCodes.NoCallbackEvent, new object[]
					{
						id
					});
				}
				ServicesGameServer.Instance.Callbacks[(CallbackID)id](dataPointer, dataSize);
			}
			catch (Exception item)
			{
				try
				{
					ServicesGameServer.Instance.bufferedExceptions.Add(item);
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x000101B0 File Offset: 0x0000E3B0
		private static void NativeResultCallbacks(int id, IntPtr dataPointer, int dataSize, bool flag)
		{
			try
			{
				if (!ServicesGameServer.Instance.ResultCallbacks.ContainsKey((ResultID)id))
				{
					throw new ManagedException(ErrorCodes.NoResultEvent, new object[]
					{
						id
					});
				}
				ServicesGameServer.Instance.ResultCallbacks[(ResultID)id](dataPointer, dataSize, flag);
			}
			catch (Exception item)
			{
				try
				{
					ServicesGameServer.Instance.bufferedExceptions.Add(item);
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x0001023C File Offset: 0x0000E43C
		internal void CheckIfUsable()
		{
			if (!this.IsAvailable)
			{
				throw new InvalidOperationException(StringMap.GetString(ErrorCodes.UsageAfterAPIShutdown, new object[0]));
			}
		}

		// Token: 0x0400061E RID: 1566
		private JobManager serviceJobs;

		// Token: 0x0400061F RID: 1567
		private GameServer gameServer;

		// Token: 0x04000620 RID: 1568
		private GameServerStats gameServerStats;

		// Token: 0x04000621 RID: 1569
		private List<Exception> bufferedExceptions;

		// Token: 0x02000158 RID: 344
		// (Invoke) Token: 0x06000BDB RID: 3035
		public delegate void ExceptionDelegate(Exception e);
	}
}
