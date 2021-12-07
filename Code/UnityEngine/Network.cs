using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x02000077 RID: 119
	public sealed class Network
	{
		// Token: 0x060006FD RID: 1789
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern NetworkConnectionError InitializeServer(int connections, int listenPort, bool useNat);

		// Token: 0x060006FE RID: 1790
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern NetworkConnectionError Internal_InitializeServerDeprecated(int connections, int listenPort);

		// Token: 0x060006FF RID: 1791 RVA: 0x0000A64C File Offset: 0x0000884C
		[Obsolete("Use the IntializeServer(connections, listenPort, useNat) function instead")]
		public static NetworkConnectionError InitializeServer(int connections, int listenPort)
		{
			return Network.Internal_InitializeServerDeprecated(connections, listenPort);
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000700 RID: 1792
		// (set) Token: 0x06000701 RID: 1793
		public static extern string incomingPassword { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000702 RID: 1794
		// (set) Token: 0x06000703 RID: 1795
		public static extern NetworkLogLevel logLevel { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000704 RID: 1796
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void InitializeSecurity();

		// Token: 0x06000705 RID: 1797
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern NetworkConnectionError Internal_ConnectToSingleIP(string IP, int remotePort, int localPort, [DefaultValue("\"\"")] string password);

		// Token: 0x06000706 RID: 1798 RVA: 0x0000A658 File Offset: 0x00008858
		[ExcludeFromDocs]
		private static NetworkConnectionError Internal_ConnectToSingleIP(string IP, int remotePort, int localPort)
		{
			string empty = string.Empty;
			return Network.Internal_ConnectToSingleIP(IP, remotePort, localPort, empty);
		}

		// Token: 0x06000707 RID: 1799
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern NetworkConnectionError Internal_ConnectToGuid(string guid, string password);

		// Token: 0x06000708 RID: 1800
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern NetworkConnectionError Internal_ConnectToIPs(string[] IP, int remotePort, int localPort, [DefaultValue("\"\"")] string password);

		// Token: 0x06000709 RID: 1801 RVA: 0x0000A674 File Offset: 0x00008874
		[ExcludeFromDocs]
		private static NetworkConnectionError Internal_ConnectToIPs(string[] IP, int remotePort, int localPort)
		{
			string empty = string.Empty;
			return Network.Internal_ConnectToIPs(IP, remotePort, localPort, empty);
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0000A690 File Offset: 0x00008890
		[ExcludeFromDocs]
		public static NetworkConnectionError Connect(string IP, int remotePort)
		{
			string empty = string.Empty;
			return Network.Connect(IP, remotePort, empty);
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0000A6AC File Offset: 0x000088AC
		public static NetworkConnectionError Connect(string IP, int remotePort, [DefaultValue("\"\"")] string password)
		{
			return Network.Internal_ConnectToSingleIP(IP, remotePort, 0, password);
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0000A6B8 File Offset: 0x000088B8
		[ExcludeFromDocs]
		public static NetworkConnectionError Connect(string[] IPs, int remotePort)
		{
			string empty = string.Empty;
			return Network.Connect(IPs, remotePort, empty);
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0000A6D4 File Offset: 0x000088D4
		public static NetworkConnectionError Connect(string[] IPs, int remotePort, [DefaultValue("\"\"")] string password)
		{
			return Network.Internal_ConnectToIPs(IPs, remotePort, 0, password);
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0000A6E0 File Offset: 0x000088E0
		[ExcludeFromDocs]
		public static NetworkConnectionError Connect(string GUID)
		{
			string empty = string.Empty;
			return Network.Connect(GUID, empty);
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0000A6FC File Offset: 0x000088FC
		public static NetworkConnectionError Connect(string GUID, [DefaultValue("\"\"")] string password)
		{
			return Network.Internal_ConnectToGuid(GUID, password);
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0000A708 File Offset: 0x00008908
		[ExcludeFromDocs]
		public static NetworkConnectionError Connect(HostData hostData)
		{
			string empty = string.Empty;
			return Network.Connect(hostData, empty);
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0000A724 File Offset: 0x00008924
		public static NetworkConnectionError Connect(HostData hostData, [DefaultValue("\"\"")] string password)
		{
			if (hostData == null)
			{
				throw new NullReferenceException();
			}
			if (hostData.guid.Length > 0 && hostData.useNat)
			{
				return Network.Connect(hostData.guid, password);
			}
			return Network.Connect(hostData.ip, hostData.port, password);
		}

		// Token: 0x06000712 RID: 1810
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Disconnect([DefaultValue("200")] int timeout);

		// Token: 0x06000713 RID: 1811 RVA: 0x0000A778 File Offset: 0x00008978
		[ExcludeFromDocs]
		public static void Disconnect()
		{
			int timeout = 200;
			Network.Disconnect(timeout);
		}

		// Token: 0x06000714 RID: 1812
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CloseConnection(NetworkPlayer target, bool sendDisconnectionNotification);

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000715 RID: 1813
		public static extern NetworkPlayer[] connections { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000716 RID: 1814
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetPlayer();

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x0000A794 File Offset: 0x00008994
		public static NetworkPlayer player
		{
			get
			{
				NetworkPlayer result;
				result.index = Network.Internal_GetPlayer();
				return result;
			}
		}

		// Token: 0x06000718 RID: 1816
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_AllocateViewID(out NetworkViewID viewID);

		// Token: 0x06000719 RID: 1817 RVA: 0x0000A7B0 File Offset: 0x000089B0
		public static NetworkViewID AllocateViewID()
		{
			NetworkViewID result;
			Network.Internal_AllocateViewID(out result);
			return result;
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0000A7C8 File Offset: 0x000089C8
		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object prefab, Vector3 position, Quaternion rotation, int group)
		{
			return Network.INTERNAL_CALL_Instantiate(prefab, ref position, ref rotation, group);
		}

		// Token: 0x0600071B RID: 1819
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Object INTERNAL_CALL_Instantiate(Object prefab, ref Vector3 position, ref Quaternion rotation, int group);

		// Token: 0x0600071C RID: 1820 RVA: 0x0000A7D8 File Offset: 0x000089D8
		public static void Destroy(NetworkViewID viewID)
		{
			Network.INTERNAL_CALL_Destroy(ref viewID);
		}

		// Token: 0x0600071D RID: 1821
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Destroy(ref NetworkViewID viewID);

		// Token: 0x0600071E RID: 1822 RVA: 0x0000A7E4 File Offset: 0x000089E4
		public static void Destroy(GameObject gameObject)
		{
			if (gameObject != null)
			{
				NetworkView component = gameObject.GetComponent<NetworkView>();
				if (component != null)
				{
					Network.Destroy(component.viewID);
				}
				else
				{
					Debug.LogError("Couldn't destroy game object because no network view is attached to it.", gameObject);
				}
			}
		}

		// Token: 0x0600071F RID: 1823
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DestroyPlayerObjects(NetworkPlayer playerID);

		// Token: 0x06000720 RID: 1824 RVA: 0x0000A82C File Offset: 0x00008A2C
		private static void Internal_RemoveRPCs(NetworkPlayer playerID, NetworkViewID viewID, uint channelMask)
		{
			Network.INTERNAL_CALL_Internal_RemoveRPCs(playerID, ref viewID, channelMask);
		}

		// Token: 0x06000721 RID: 1825
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_RemoveRPCs(NetworkPlayer playerID, ref NetworkViewID viewID, uint channelMask);

		// Token: 0x06000722 RID: 1826 RVA: 0x0000A838 File Offset: 0x00008A38
		public static void RemoveRPCs(NetworkPlayer playerID)
		{
			Network.Internal_RemoveRPCs(playerID, NetworkViewID.unassigned, uint.MaxValue);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0000A848 File Offset: 0x00008A48
		public static void RemoveRPCs(NetworkPlayer playerID, int group)
		{
			Network.Internal_RemoveRPCs(playerID, NetworkViewID.unassigned, 1U << group);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0000A85C File Offset: 0x00008A5C
		public static void RemoveRPCs(NetworkViewID viewID)
		{
			Network.Internal_RemoveRPCs(NetworkPlayer.unassigned, viewID, uint.MaxValue);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0000A86C File Offset: 0x00008A6C
		public static void RemoveRPCsInGroup(int group)
		{
			Network.Internal_RemoveRPCs(NetworkPlayer.unassigned, NetworkViewID.unassigned, 1U << group);
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000726 RID: 1830
		public static extern bool isClient { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000727 RID: 1831
		public static extern bool isServer { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000728 RID: 1832
		public static extern NetworkPeerType peerType { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000729 RID: 1833
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetLevelPrefix(int prefix);

		// Token: 0x0600072A RID: 1834
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetLastPing(NetworkPlayer player);

		// Token: 0x0600072B RID: 1835
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetAveragePing(NetworkPlayer player);

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600072C RID: 1836
		// (set) Token: 0x0600072D RID: 1837
		public static extern float sendRate { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600072E RID: 1838
		// (set) Token: 0x0600072F RID: 1839
		public static extern bool isMessageQueueRunning { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000730 RID: 1840
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetReceivingEnabled(NetworkPlayer player, int group, bool enabled);

		// Token: 0x06000731 RID: 1841
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetSendingGlobal(int group, bool enabled);

		// Token: 0x06000732 RID: 1842
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetSendingSpecific(NetworkPlayer player, int group, bool enabled);

		// Token: 0x06000733 RID: 1843 RVA: 0x0000A884 File Offset: 0x00008A84
		public static void SetSendingEnabled(int group, bool enabled)
		{
			Network.Internal_SetSendingGlobal(group, enabled);
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0000A890 File Offset: 0x00008A90
		public static void SetSendingEnabled(NetworkPlayer player, int group, bool enabled)
		{
			Network.Internal_SetSendingSpecific(player, group, enabled);
		}

		// Token: 0x06000735 RID: 1845
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetTime(out double t);

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x0000A89C File Offset: 0x00008A9C
		public static double time
		{
			get
			{
				double result;
				Network.Internal_GetTime(out result);
				return result;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000737 RID: 1847
		// (set) Token: 0x06000738 RID: 1848
		public static extern int minimumAllocatableViewIDs { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000739 RID: 1849
		// (set) Token: 0x0600073A RID: 1850
		[Obsolete("No longer needed. This is now explicitly set in the InitializeServer function call. It is implicitly set when calling Connect depending on if an IP/port combination is used (useNat=false) or a GUID is used(useNat=true).")]
		public static extern bool useNat { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x0600073B RID: 1851
		// (set) Token: 0x0600073C RID: 1852
		public static extern string natFacilitatorIP { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x0600073D RID: 1853
		// (set) Token: 0x0600073E RID: 1854
		public static extern int natFacilitatorPort { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600073F RID: 1855
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ConnectionTesterStatus TestConnection([DefaultValue("false")] bool forceTest);

		// Token: 0x06000740 RID: 1856 RVA: 0x0000A8B4 File Offset: 0x00008AB4
		[ExcludeFromDocs]
		public static ConnectionTesterStatus TestConnection()
		{
			bool forceTest = false;
			return Network.TestConnection(forceTest);
		}

		// Token: 0x06000741 RID: 1857
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ConnectionTesterStatus TestConnectionNAT([DefaultValue("false")] bool forceTest);

		// Token: 0x06000742 RID: 1858 RVA: 0x0000A8CC File Offset: 0x00008ACC
		[ExcludeFromDocs]
		public static ConnectionTesterStatus TestConnectionNAT()
		{
			bool forceTest = false;
			return Network.TestConnectionNAT(forceTest);
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000743 RID: 1859
		// (set) Token: 0x06000744 RID: 1860
		public static extern string connectionTesterIP { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000745 RID: 1861
		// (set) Token: 0x06000746 RID: 1862
		public static extern int connectionTesterPort { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000747 RID: 1863
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool HavePublicAddress();

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000748 RID: 1864
		// (set) Token: 0x06000749 RID: 1865
		public static extern int maxConnections { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600074A RID: 1866
		// (set) Token: 0x0600074B RID: 1867
		public static extern string proxyIP { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x0600074C RID: 1868
		// (set) Token: 0x0600074D RID: 1869
		public static extern int proxyPort { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600074E RID: 1870
		// (set) Token: 0x0600074F RID: 1871
		public static extern bool useProxy { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000750 RID: 1872
		// (set) Token: 0x06000751 RID: 1873
		public static extern string proxyPassword { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
