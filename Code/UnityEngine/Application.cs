using System;
using System.Collections;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using UnityEngine.Internal;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000AC RID: 172
	public sealed class Application
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060009A6 RID: 2470 RVA: 0x0000E0F8 File Offset: 0x0000C2F8
		// (remove) Token: 0x060009A7 RID: 2471 RVA: 0x0000E118 File Offset: 0x0000C318
		public static event Application.LogCallback logMessageReceived
		{
			add
			{
				Application.s_LogCallbackHandler = (Application.LogCallback)Delegate.Combine(Application.s_LogCallbackHandler, value);
				Application.SetLogCallbackDefined(true);
			}
			remove
			{
				Application.s_LogCallbackHandler = (Application.LogCallback)Delegate.Remove(Application.s_LogCallbackHandler, value);
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060009A8 RID: 2472 RVA: 0x0000E130 File Offset: 0x0000C330
		// (remove) Token: 0x060009A9 RID: 2473 RVA: 0x0000E150 File Offset: 0x0000C350
		public static event Application.LogCallback logMessageReceivedThreaded
		{
			add
			{
				Application.s_LogCallbackHandlerThreaded = (Application.LogCallback)Delegate.Combine(Application.s_LogCallbackHandlerThreaded, value);
				Application.SetLogCallbackDefined(true);
			}
			remove
			{
				Application.s_LogCallbackHandlerThreaded = (Application.LogCallback)Delegate.Remove(Application.s_LogCallbackHandlerThreaded, value);
			}
		}

		// Token: 0x060009AA RID: 2474
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Quit();

		// Token: 0x060009AB RID: 2475
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CancelQuit();

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060009AC RID: 2476
		[Obsolete("This property is deprecated, please use LoadLevelAsync to detect if a specific scene is currently loading.")]
		public static extern bool isLoadingLevel { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060009AD RID: 2477
		[Obsolete("Use SceneManager.sceneCountInBuildSettings")]
		public static extern int levelCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060009AE RID: 2478
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetStreamProgressForLevelByName(string levelName);

		// Token: 0x060009AF RID: 2479
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetStreamProgressForLevel(int levelIndex);

		// Token: 0x060009B0 RID: 2480 RVA: 0x0000E168 File Offset: 0x0000C368
		public static float GetStreamProgressForLevel(string levelName)
		{
			return Application.GetStreamProgressForLevelByName(levelName);
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060009B1 RID: 2481
		public static extern int streamedBytes { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060009B2 RID: 2482
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CanStreamedLevelBeLoadedByName(string levelName);

		// Token: 0x060009B3 RID: 2483
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool CanStreamedLevelBeLoaded(int levelIndex);

		// Token: 0x060009B4 RID: 2484 RVA: 0x0000E170 File Offset: 0x0000C370
		public static bool CanStreamedLevelBeLoaded(string levelName)
		{
			return Application.CanStreamedLevelBeLoadedByName(levelName);
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060009B5 RID: 2485
		public static extern bool isPlaying { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060009B6 RID: 2486
		public static extern bool isEditor { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060009B7 RID: 2487
		public static extern bool isWebPlayer { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060009B8 RID: 2488
		public static extern RuntimePlatform platform { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x0000E178 File Offset: 0x0000C378
		public static bool isMobilePlatform
		{
			get
			{
				switch (Application.platform)
				{
				case RuntimePlatform.IPhonePlayer:
				case RuntimePlatform.Android:
				case RuntimePlatform.MetroPlayerX86:
				case RuntimePlatform.MetroPlayerX64:
				case RuntimePlatform.MetroPlayerARM:
				case RuntimePlatform.WP8Player:
				case RuntimePlatform.BlackBerryPlayer:
				case RuntimePlatform.TizenPlayer:
					return true;
				}
				return false;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x0000E1DC File Offset: 0x0000C3DC
		public static bool isConsolePlatform
		{
			get
			{
				RuntimePlatform platform = Application.platform;
				return platform == RuntimePlatform.PS3 || platform == RuntimePlatform.PS4 || platform == RuntimePlatform.XBOX360 || platform == RuntimePlatform.XboxOne;
			}
		}

		// Token: 0x060009BB RID: 2491
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CaptureScreenshot(string filename, [DefaultValue("0")] int superSize);

		// Token: 0x060009BC RID: 2492 RVA: 0x0000E210 File Offset: 0x0000C410
		[ExcludeFromDocs]
		public static void CaptureScreenshot(string filename)
		{
			int superSize = 0;
			Application.CaptureScreenshot(filename, superSize);
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060009BD RID: 2493
		// (set) Token: 0x060009BE RID: 2494
		public static extern bool runInBackground { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x0000E228 File Offset: 0x0000C428
		[Obsolete("use Application.isEditor instead")]
		public static bool isPlayer
		{
			get
			{
				return !Application.isEditor;
			}
		}

		// Token: 0x060009C0 RID: 2496
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool HasProLicense();

		// Token: 0x060009C1 RID: 2497
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool HasAdvancedLicense();

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060009C2 RID: 2498
		internal static extern bool isBatchmode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060009C3 RID: 2499
		internal static extern bool isHumanControllingUs { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060009C4 RID: 2500
		internal static extern bool isRunningUnitTests { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060009C5 RID: 2501
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool HasARGV(string name);

		// Token: 0x060009C6 RID: 2502
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetValueForARGV(string name);

		// Token: 0x060009C7 RID: 2503
		[Obsolete("Use Object.DontDestroyOnLoad instead")]
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DontDestroyOnLoad(Object mono);

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060009C8 RID: 2504
		public static extern string dataPath { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060009C9 RID: 2505
		public static extern string streamingAssetsPath { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060009CA RID: 2506
		[SecurityCritical]
		public static extern string persistentDataPath { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060009CB RID: 2507
		public static extern string temporaryCachePath { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060009CC RID: 2508
		public static extern string srcValue { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060009CD RID: 2509
		public static extern string absoluteURL { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060009CE RID: 2510 RVA: 0x0000E234 File Offset: 0x0000C434
		private static string ObjectToJSString(object o)
		{
			if (o == null)
			{
				return "null";
			}
			if (o is string)
			{
				string text = o.ToString().Replace("\\", "\\\\");
				text = text.Replace("\"", "\\\"");
				text = text.Replace("\n", "\\n");
				text = text.Replace("\r", "\\r");
				text = text.Replace("\0", string.Empty);
				text = text.Replace("\u2028", string.Empty);
				text = text.Replace("\u2029", string.Empty);
				return '"' + text + '"';
			}
			if (o is int || o is short || o is uint || o is ushort || o is byte)
			{
				return o.ToString();
			}
			if (o is float)
			{
				NumberFormatInfo numberFormat = CultureInfo.InvariantCulture.NumberFormat;
				return ((float)o).ToString(numberFormat);
			}
			if (o is double)
			{
				NumberFormatInfo numberFormat2 = CultureInfo.InvariantCulture.NumberFormat;
				return ((double)o).ToString(numberFormat2);
			}
			if (o is char)
			{
				if ((char)o == '"')
				{
					return "\"\\\"\"";
				}
				return '"' + o.ToString() + '"';
			}
			else
			{
				if (o is IList)
				{
					IList list = (IList)o;
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append("new Array(");
					int count = list.Count;
					for (int i = 0; i < count; i++)
					{
						if (i != 0)
						{
							stringBuilder.Append(", ");
						}
						stringBuilder.Append(Application.ObjectToJSString(list[i]));
					}
					stringBuilder.Append(")");
					return stringBuilder.ToString();
				}
				return Application.ObjectToJSString(o.ToString());
			}
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0000E43C File Offset: 0x0000C63C
		public static void ExternalCall(string functionName, params object[] args)
		{
			Application.Internal_ExternalCall(Application.BuildInvocationForArguments(functionName, args));
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0000E44C File Offset: 0x0000C64C
		private static string BuildInvocationForArguments(string functionName, params object[] args)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(functionName);
			stringBuilder.Append('(');
			int num = args.Length;
			for (int i = 0; i < num; i++)
			{
				if (i != 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(Application.ObjectToJSString(args[i]));
			}
			stringBuilder.Append(')');
			stringBuilder.Append(';');
			return stringBuilder.ToString();
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0000E4C0 File Offset: 0x0000C6C0
		public static void ExternalEval(string script)
		{
			if (script.Length > 0 && script[script.Length - 1] != ';')
			{
				script += ';';
			}
			Application.Internal_ExternalCall(script);
		}

		// Token: 0x060009D2 RID: 2514
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_ExternalCall(string script);

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060009D3 RID: 2515
		public static extern string unityVersion { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060009D4 RID: 2516
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetBuildUnityVersion();

		// Token: 0x060009D5 RID: 2517
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetNumericUnityVersion(string version);

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060009D6 RID: 2518
		public static extern string version { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060009D7 RID: 2519
		public static extern string bundleIdentifier { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060009D8 RID: 2520
		public static extern ApplicationInstallMode installMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060009D9 RID: 2521
		public static extern ApplicationSandboxType sandboxType { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060009DA RID: 2522
		public static extern string productName { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060009DB RID: 2523
		public static extern string companyName { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060009DC RID: 2524
		public static extern string cloudProjectId { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060009DD RID: 2525 RVA: 0x0000E504 File Offset: 0x0000C704
		internal static void InvokeOnAdvertisingIdentifierCallback(string advertisingId, bool trackingEnabled)
		{
			if (Application.OnAdvertisingIdentifierCallback != null)
			{
				Application.OnAdvertisingIdentifierCallback(advertisingId, trackingEnabled, string.Empty);
			}
		}

		// Token: 0x060009DE RID: 2526
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool RequestAdvertisingIdentifierAsync(Application.AdvertisingIdentifierCallback delegateMethod);

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060009DF RID: 2527
		public static extern bool webSecurityEnabled { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060009E0 RID: 2528
		public static extern string webSecurityHostUrl { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060009E1 RID: 2529
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void OpenURL(string url);

		// Token: 0x060009E2 RID: 2530
		[Obsolete("For internal use only")]
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ForceCrash(int mode);

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060009E3 RID: 2531
		// (set) Token: 0x060009E4 RID: 2532
		public static extern int targetFrameRate { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060009E5 RID: 2533
		public static extern SystemLanguage systemLanguage { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060009E6 RID: 2534 RVA: 0x0000E524 File Offset: 0x0000C724
		[RequiredByNativeCode]
		private static void CallLogCallback(string logString, string stackTrace, LogType type, bool invokedOnMainThread)
		{
			if (invokedOnMainThread)
			{
				Application.LogCallback logCallback = Application.s_LogCallbackHandler;
				if (logCallback != null)
				{
					logCallback(logString, stackTrace, type);
				}
			}
			Application.LogCallback logCallback2 = Application.s_LogCallbackHandlerThreaded;
			if (logCallback2 != null)
			{
				logCallback2(logString, stackTrace, type);
			}
		}

		// Token: 0x060009E7 RID: 2535
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLogCallbackDefined(bool defined);

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060009E8 RID: 2536
		// (set) Token: 0x060009E9 RID: 2537
		public static extern StackTraceLogType stackTraceLogType { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060009EA RID: 2538
		// (set) Token: 0x060009EB RID: 2539
		public static extern ThreadPriority backgroundLoadingPriority { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060009EC RID: 2540
		public static extern NetworkReachability internetReachability { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060009ED RID: 2541
		public static extern bool genuine { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060009EE RID: 2542
		public static extern bool genuineCheckAvailable { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060009EF RID: 2543
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern AsyncOperation RequestUserAuthorization(UserAuthorization mode);

		// Token: 0x060009F0 RID: 2544
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool HasUserAuthorization(UserAuthorization mode);

		// Token: 0x060009F1 RID: 2545
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ReplyToUserAuthorizationRequest(bool reply, [DefaultValue("false")] bool remember);

		// Token: 0x060009F2 RID: 2546 RVA: 0x0000E564 File Offset: 0x0000C764
		[ExcludeFromDocs]
		internal static void ReplyToUserAuthorizationRequest(bool reply)
		{
			bool remember = false;
			Application.ReplyToUserAuthorizationRequest(reply, remember);
		}

		// Token: 0x060009F3 RID: 2547
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetUserAuthorizationRequestMode_Internal();

		// Token: 0x060009F4 RID: 2548 RVA: 0x0000E57C File Offset: 0x0000C77C
		internal static UserAuthorization GetUserAuthorizationRequestMode()
		{
			return (UserAuthorization)Application.GetUserAuthorizationRequestMode_Internal();
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060009F5 RID: 2549
		internal static extern bool submitAnalytics { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060009F6 RID: 2550
		public static extern bool isShowingSplashScreen { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060009F7 RID: 2551 RVA: 0x0000E584 File Offset: 0x0000C784
		[Obsolete("Application.RegisterLogCallback is deprecated. Use Application.logMessageReceived instead.")]
		public static void RegisterLogCallback(Application.LogCallback handler)
		{
			Application.RegisterLogCallback(handler, false);
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0000E590 File Offset: 0x0000C790
		[Obsolete("Application.RegisterLogCallbackThreaded is deprecated. Use Application.logMessageReceivedThreaded instead.")]
		public static void RegisterLogCallbackThreaded(Application.LogCallback handler)
		{
			Application.RegisterLogCallback(handler, true);
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0000E59C File Offset: 0x0000C79C
		private static void RegisterLogCallback(Application.LogCallback handler, bool threaded)
		{
			if (Application.s_RegisterLogCallbackDeprecated != null)
			{
				Application.logMessageReceived -= Application.s_RegisterLogCallbackDeprecated;
				Application.logMessageReceivedThreaded -= Application.s_RegisterLogCallbackDeprecated;
			}
			Application.s_RegisterLogCallbackDeprecated = handler;
			if (handler != null)
			{
				if (threaded)
				{
					Application.logMessageReceivedThreaded += handler;
				}
				else
				{
					Application.logMessageReceived += handler;
				}
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x0000E5F4 File Offset: 0x0000C7F4
		[Obsolete("Use SceneManager to determine what scenes have been loaded")]
		public static int loadedLevel
		{
			get
			{
				return SceneManager.GetActiveScene().buildIndex;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x0000E610 File Offset: 0x0000C810
		[Obsolete("Use SceneManager to determine what scenes have been loaded")]
		public static string loadedLevelName
		{
			get
			{
				return SceneManager.GetActiveScene().name;
			}
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0000E62C File Offset: 0x0000C82C
		[Obsolete("Use SceneManager.LoadScene")]
		public static void LoadLevel(int index)
		{
			SceneManager.LoadScene(index, LoadSceneMode.Single);
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0000E638 File Offset: 0x0000C838
		[Obsolete("Use SceneManager.LoadScene")]
		public static void LoadLevel(string name)
		{
			SceneManager.LoadScene(name, LoadSceneMode.Single);
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0000E644 File Offset: 0x0000C844
		[Obsolete("Use SceneManager.LoadScene")]
		public static void LoadLevelAdditive(int index)
		{
			SceneManager.LoadScene(index, LoadSceneMode.Additive);
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0000E650 File Offset: 0x0000C850
		[Obsolete("Use SceneManager.LoadScene")]
		public static void LoadLevelAdditive(string name)
		{
			SceneManager.LoadScene(name, LoadSceneMode.Additive);
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0000E65C File Offset: 0x0000C85C
		[Obsolete("Use SceneManager.LoadSceneAsync")]
		public static AsyncOperation LoadLevelAsync(int index)
		{
			return SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0000E668 File Offset: 0x0000C868
		[Obsolete("Use SceneManager.LoadSceneAsync")]
		public static AsyncOperation LoadLevelAsync(string levelName)
		{
			return SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0000E674 File Offset: 0x0000C874
		[Obsolete("Use SceneManager.LoadSceneAsync")]
		public static AsyncOperation LoadLevelAdditiveAsync(int index)
		{
			return SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0000E680 File Offset: 0x0000C880
		[Obsolete("Use SceneManager.LoadSceneAsync")]
		public static AsyncOperation LoadLevelAdditiveAsync(string levelName)
		{
			return SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0000E68C File Offset: 0x0000C88C
		[Obsolete("Use SceneManager.UnloadScene")]
		public static bool UnloadLevel(int index)
		{
			return SceneManager.UnloadScene(index);
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0000E694 File Offset: 0x0000C894
		[Obsolete("Use SceneManager.UnloadScene")]
		public static bool UnloadLevel(string scenePath)
		{
			return SceneManager.UnloadScene(scenePath);
		}

		// Token: 0x04000206 RID: 518
		internal static Application.AdvertisingIdentifierCallback OnAdvertisingIdentifierCallback;

		// Token: 0x04000207 RID: 519
		private static Application.LogCallback s_LogCallbackHandler;

		// Token: 0x04000208 RID: 520
		private static Application.LogCallback s_LogCallbackHandlerThreaded;

		// Token: 0x04000209 RID: 521
		private static volatile Application.LogCallback s_RegisterLogCallbackDeprecated;

		// Token: 0x02000340 RID: 832
		// (Invoke) Token: 0x0600285A RID: 10330
		public delegate void AdvertisingIdentifierCallback(string advertisingId, bool trackingEnabled, string errorMsg);

		// Token: 0x02000341 RID: 833
		// (Invoke) Token: 0x0600285E RID: 10334
		public delegate void LogCallback(string condition, string stackTrace, LogType type);
	}
}
