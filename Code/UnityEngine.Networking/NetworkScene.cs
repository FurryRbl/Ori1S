using System;
using System.Collections.Generic;

namespace UnityEngine.Networking
{
	// Token: 0x02000050 RID: 80
	internal class NetworkScene
	{
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000371 RID: 881 RVA: 0x000120A0 File Offset: 0x000102A0
		internal Dictionary<NetworkInstanceId, NetworkIdentity> localObjects
		{
			get
			{
				return this.m_LocalObjects;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000372 RID: 882 RVA: 0x000120A8 File Offset: 0x000102A8
		internal static Dictionary<NetworkHash128, GameObject> guidToPrefab
		{
			get
			{
				return NetworkScene.s_GuidToPrefab;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000373 RID: 883 RVA: 0x000120B0 File Offset: 0x000102B0
		internal static Dictionary<NetworkHash128, SpawnDelegate> spawnHandlers
		{
			get
			{
				return NetworkScene.s_SpawnHandlers;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000374 RID: 884 RVA: 0x000120B8 File Offset: 0x000102B8
		internal static Dictionary<NetworkHash128, UnSpawnDelegate> unspawnHandlers
		{
			get
			{
				return NetworkScene.s_UnspawnHandlers;
			}
		}

		// Token: 0x06000375 RID: 885 RVA: 0x000120C0 File Offset: 0x000102C0
		internal void Shutdown()
		{
			this.ClearLocalObjects();
			NetworkScene.ClearSpawners();
		}

		// Token: 0x06000376 RID: 886 RVA: 0x000120D0 File Offset: 0x000102D0
		internal void SetLocalObject(NetworkInstanceId netId, GameObject obj, bool isClient, bool isServer)
		{
			if (LogFilter.logDev)
			{
				Debug.Log(string.Concat(new object[]
				{
					"SetLocalObject ",
					netId,
					" ",
					obj
				}));
			}
			if (obj == null)
			{
				this.m_LocalObjects[netId] = null;
				return;
			}
			NetworkIdentity networkIdentity = null;
			if (this.m_LocalObjects.ContainsKey(netId))
			{
				networkIdentity = this.m_LocalObjects[netId];
			}
			if (networkIdentity == null)
			{
				networkIdentity = obj.GetComponent<NetworkIdentity>();
				this.m_LocalObjects[netId] = networkIdentity;
			}
			networkIdentity.UpdateClientServer(isClient, isServer);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00012178 File Offset: 0x00010378
		internal GameObject FindLocalObject(NetworkInstanceId netId)
		{
			if (this.m_LocalObjects.ContainsKey(netId))
			{
				NetworkIdentity networkIdentity = this.m_LocalObjects[netId];
				if (networkIdentity != null)
				{
					return networkIdentity.gameObject;
				}
			}
			return null;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x000121B8 File Offset: 0x000103B8
		internal bool GetNetworkIdentity(NetworkInstanceId netId, out NetworkIdentity uv)
		{
			if (this.m_LocalObjects.ContainsKey(netId) && this.m_LocalObjects[netId] != null)
			{
				uv = this.m_LocalObjects[netId];
				return true;
			}
			uv = null;
			return false;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00012204 File Offset: 0x00010404
		internal bool RemoveLocalObject(NetworkInstanceId netId)
		{
			return this.m_LocalObjects.Remove(netId);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00012214 File Offset: 0x00010414
		internal bool RemoveLocalObjectAndDestroy(NetworkInstanceId netId)
		{
			if (this.m_LocalObjects.ContainsKey(netId))
			{
				NetworkIdentity networkIdentity = this.m_LocalObjects[netId];
				Object.Destroy(networkIdentity.gameObject);
				return this.m_LocalObjects.Remove(netId);
			}
			return false;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00012258 File Offset: 0x00010458
		internal void ClearLocalObjects()
		{
			this.m_LocalObjects.Clear();
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00012268 File Offset: 0x00010468
		internal static void RegisterPrefab(GameObject prefab, NetworkHash128 newAssetId)
		{
			NetworkIdentity component = prefab.GetComponent<NetworkIdentity>();
			if (component)
			{
				component.SetDynamicAssetId(newAssetId);
				if (LogFilter.logDebug)
				{
					Debug.Log(string.Concat(new object[]
					{
						"Registering prefab '",
						prefab.name,
						"' as asset:",
						component.assetId
					}));
				}
				NetworkScene.s_GuidToPrefab[component.assetId] = prefab;
			}
			else if (LogFilter.logError)
			{
				Debug.LogError("Could not register '" + prefab.name + "' since it contains no NetworkIdentity component");
			}
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0001230C File Offset: 0x0001050C
		internal static void RegisterPrefab(GameObject prefab)
		{
			NetworkIdentity component = prefab.GetComponent<NetworkIdentity>();
			if (component)
			{
				if (LogFilter.logDebug)
				{
					Debug.Log(string.Concat(new object[]
					{
						"Registering prefab '",
						prefab.name,
						"' as asset:",
						component.assetId
					}));
				}
				NetworkScene.s_GuidToPrefab[component.assetId] = prefab;
				NetworkIdentity[] componentsInChildren = prefab.GetComponentsInChildren<NetworkIdentity>();
				if (componentsInChildren.Length > 1 && LogFilter.logWarn)
				{
					Debug.LogWarning("The prefab '" + prefab.name + "' has multiple NetworkIdentity components. There can only be one NetworkIdentity on a prefab, and it must be on the root object.");
				}
			}
			else if (LogFilter.logError)
			{
				Debug.LogError("Could not register '" + prefab.name + "' since it contains no NetworkIdentity component");
			}
		}

		// Token: 0x0600037E RID: 894 RVA: 0x000123DC File Offset: 0x000105DC
		internal static bool GetPrefab(NetworkHash128 assetId, out GameObject prefab)
		{
			if (!assetId.IsValid())
			{
				prefab = null;
				return false;
			}
			if (NetworkScene.s_GuidToPrefab.ContainsKey(assetId) && NetworkScene.s_GuidToPrefab[assetId] != null)
			{
				prefab = NetworkScene.s_GuidToPrefab[assetId];
				return true;
			}
			prefab = null;
			return false;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00012434 File Offset: 0x00010634
		internal static void ClearSpawners()
		{
			NetworkScene.s_GuidToPrefab.Clear();
			NetworkScene.s_SpawnHandlers.Clear();
			NetworkScene.s_UnspawnHandlers.Clear();
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00012454 File Offset: 0x00010654
		public static void UnregisterSpawnHandler(NetworkHash128 assetId)
		{
			NetworkScene.s_SpawnHandlers.Remove(assetId);
			NetworkScene.s_UnspawnHandlers.Remove(assetId);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00012470 File Offset: 0x00010670
		internal static void RegisterSpawnHandler(NetworkHash128 assetId, SpawnDelegate spawnHandler, UnSpawnDelegate unspawnHandler)
		{
			if (spawnHandler == null || unspawnHandler == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("RegisterSpawnHandler custom spawn function null for " + assetId);
				}
				return;
			}
			if (LogFilter.logDebug)
			{
				Debug.Log(string.Concat(new object[]
				{
					"RegisterSpawnHandler asset '",
					assetId,
					"' ",
					spawnHandler.Method.Name,
					"/",
					unspawnHandler.Method.Name
				}));
			}
			NetworkScene.s_SpawnHandlers[assetId] = spawnHandler;
			NetworkScene.s_UnspawnHandlers[assetId] = unspawnHandler;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00012518 File Offset: 0x00010718
		internal static void UnregisterPrefab(GameObject prefab)
		{
			NetworkIdentity component = prefab.GetComponent<NetworkIdentity>();
			if (component == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("Could not unregister '" + prefab.name + "' since it contains no NetworkIdentity component");
				}
				return;
			}
			NetworkScene.s_SpawnHandlers.Remove(component.assetId);
			NetworkScene.s_UnspawnHandlers.Remove(component.assetId);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00012580 File Offset: 0x00010780
		internal static void RegisterPrefab(GameObject prefab, SpawnDelegate spawnHandler, UnSpawnDelegate unspawnHandler)
		{
			NetworkIdentity component = prefab.GetComponent<NetworkIdentity>();
			if (component == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("Could not register '" + prefab.name + "' since it contains no NetworkIdentity component");
				}
				return;
			}
			if (spawnHandler == null || unspawnHandler == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("RegisterPrefab custom spawn function null for " + component.assetId);
				}
				return;
			}
			if (!component.assetId.IsValid())
			{
				if (LogFilter.logError)
				{
					Debug.LogError("RegisterPrefab game object " + prefab.name + " has no prefab. Use RegisterSpawnHandler() instead?");
				}
				return;
			}
			if (LogFilter.logDebug)
			{
				Debug.Log(string.Concat(new object[]
				{
					"Registering custom prefab '",
					prefab.name,
					"' as asset:",
					component.assetId,
					" ",
					spawnHandler.Method.Name,
					"/",
					unspawnHandler.Method.Name
				}));
			}
			NetworkScene.s_SpawnHandlers[component.assetId] = spawnHandler;
			NetworkScene.s_UnspawnHandlers[component.assetId] = unspawnHandler;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x000126C0 File Offset: 0x000108C0
		internal static bool GetSpawnHandler(NetworkHash128 assetId, out SpawnDelegate handler)
		{
			if (NetworkScene.s_SpawnHandlers.ContainsKey(assetId))
			{
				handler = NetworkScene.s_SpawnHandlers[assetId];
				return true;
			}
			handler = null;
			return false;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x000126E8 File Offset: 0x000108E8
		internal static bool InvokeUnSpawnHandler(NetworkHash128 assetId, GameObject obj)
		{
			if (NetworkScene.s_UnspawnHandlers.ContainsKey(assetId) && NetworkScene.s_UnspawnHandlers[assetId] != null)
			{
				UnSpawnDelegate unSpawnDelegate = NetworkScene.s_UnspawnHandlers[assetId];
				unSpawnDelegate(obj);
				return true;
			}
			return false;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0001272C File Offset: 0x0001092C
		internal void DestroyAllClientObjects()
		{
			foreach (NetworkInstanceId key in this.m_LocalObjects.Keys)
			{
				NetworkIdentity networkIdentity = this.m_LocalObjects[key];
				if (networkIdentity != null && networkIdentity.gameObject != null)
				{
					if (networkIdentity.sceneId.IsEmpty())
					{
						Object.Destroy(networkIdentity.gameObject);
					}
					else
					{
						networkIdentity.gameObject.SetActive(false);
					}
				}
			}
			this.ClearLocalObjects();
		}

		// Token: 0x06000387 RID: 903 RVA: 0x000127F0 File Offset: 0x000109F0
		internal void DumpAllClientObjects()
		{
			foreach (NetworkInstanceId networkInstanceId in this.m_LocalObjects.Keys)
			{
				NetworkIdentity networkIdentity = this.m_LocalObjects[networkInstanceId];
				if (networkIdentity != null)
				{
					Debug.Log(string.Concat(new object[]
					{
						"ID:",
						networkInstanceId,
						" OBJ:",
						networkIdentity.gameObject,
						" AS:",
						networkIdentity.assetId
					}));
				}
				else
				{
					Debug.Log("ID:" + networkInstanceId + " OBJ: null");
				}
			}
		}

		// Token: 0x0400018C RID: 396
		private Dictionary<NetworkInstanceId, NetworkIdentity> m_LocalObjects = new Dictionary<NetworkInstanceId, NetworkIdentity>();

		// Token: 0x0400018D RID: 397
		private static Dictionary<NetworkHash128, GameObject> s_GuidToPrefab = new Dictionary<NetworkHash128, GameObject>();

		// Token: 0x0400018E RID: 398
		private static Dictionary<NetworkHash128, SpawnDelegate> s_SpawnHandlers = new Dictionary<NetworkHash128, SpawnDelegate>();

		// Token: 0x0400018F RID: 399
		private static Dictionary<NetworkHash128, UnSpawnDelegate> s_UnspawnHandlers = new Dictionary<NetworkHash128, UnSpawnDelegate>();
	}
}
