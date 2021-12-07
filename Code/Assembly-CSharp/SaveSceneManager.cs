using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

// Token: 0x0200009D RID: 157
public class SaveSceneManager : MonoBehaviour
{
	// Token: 0x06000679 RID: 1657 RVA: 0x000193DC File Offset: 0x000175DC
	public static SaveSceneManager FromTransform(Transform transform)
	{
		SceneRoot sceneRoot = SceneRoot.FindFromTransform(transform);
		if (sceneRoot)
		{
			return sceneRoot.SaveSceneManager;
		}
		return null;
	}

	// Token: 0x0600067A RID: 1658 RVA: 0x00019404 File Offset: 0x00017604
	public void ReleaseNullReferences()
	{
		for (int i = 0; i < this.SaveData.Count; i++)
		{
			SaveSceneManager.SaveId saveId = this.SaveData[i];
			if (saveId.SaveObject == null)
			{
				saveId.SaveObject = null;
			}
		}
	}

	// Token: 0x0600067B RID: 1659 RVA: 0x00019452 File Offset: 0x00017652
	[ContextMenu("Print info")]
	public void PrintInfo()
	{
	}

	// Token: 0x0600067C RID: 1660 RVA: 0x00019454 File Offset: 0x00017654
	public void RegisterGameObject(GameObject go)
	{
		go.GetComponentsInChildren<SaveSerialize>(SaveSceneManager.s_saveSerializeList);
		for (int i = 0; i < SaveSceneManager.s_saveSerializeList.Count; i++)
		{
			SaveSceneManager.s_saveSerializeList[i].RegisterToSaveSceneManager(this);
		}
		SaveSceneManager.s_saveSerializeList.Clear();
	}

	// Token: 0x0600067D RID: 1661 RVA: 0x000194A4 File Offset: 0x000176A4
	public void UnregisterGameObject(GameObject go)
	{
		go.GetComponentsInChildren<SaveSerialize>(SaveSceneManager.s_saveSerializeList);
		for (int i = 0; i < SaveSceneManager.s_saveSerializeList.Count; i++)
		{
			SaveSceneManager.s_saveSerializableHashSet.Add(SaveSceneManager.s_saveSerializeList[i]);
		}
		this.SaveData.RemoveAll((SaveSceneManager.SaveId a) => SaveSceneManager.s_saveSerializableHashSet.Contains(a.Save));
		SaveSceneManager.s_saveSerializeList.Clear();
		SaveSceneManager.s_saveSerializableHashSet.Clear();
	}

	// Token: 0x0600067E RID: 1662 RVA: 0x0001952C File Offset: 0x0001772C
	public ISerializable IdToSaveSerialize(MoonGuid id)
	{
		if (id == null)
		{
			return null;
		}
		for (int i = 0; i < this.SaveData.Count; i++)
		{
			SaveSceneManager.SaveId saveId = this.SaveData[i];
			if (saveId.Id == id)
			{
				return saveId.Save;
			}
		}
		return null;
	}

	// Token: 0x0600067F RID: 1663 RVA: 0x0001958C File Offset: 0x0001778C
	public MoonGuid SaveSerializeToId(ISerializable saveSerialize)
	{
		if (saveSerialize == null)
		{
			return null;
		}
		for (int i = 0; i < this.SaveData.Count; i++)
		{
			SaveSceneManager.SaveId saveId = this.SaveData[i];
			if (saveId.Save == saveSerialize)
			{
				return saveId.Id;
			}
		}
		return MoonGuid.Empty;
	}

	// Token: 0x06000680 RID: 1664 RVA: 0x000195E4 File Offset: 0x000177E4
	public bool SaveSerializeIsRegistered(ISerializable serializable)
	{
		for (int i = 0; i < this.SaveData.Count; i++)
		{
			SaveSceneManager.SaveId saveId = this.SaveData[i];
			if (saveId.Save == serializable)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000681 RID: 1665 RVA: 0x0001962C File Offset: 0x0001782C
	public void AddSaveObject(ISerializable saveSerialize, MoonGuid guid)
	{
		SaveSceneManager.SaveId item = new SaveSceneManager.SaveId
		{
			Id = guid,
			Save = saveSerialize
		};
		this.SaveData.RemoveAll((SaveSceneManager.SaveId a) => a.Id == guid);
		this.SaveData.Add(item);
	}

	// Token: 0x06000682 RID: 1666 RVA: 0x00019688 File Offset: 0x00017888
	public static void RemoveSaveDataFromMaster(GameObject go)
	{
		go.GetComponentsInChildren<SaveSerialize>(SaveSceneManager.s_saveSerializeList);
		for (int i = 0; i < SaveSceneManager.s_saveSerializeList.Count; i++)
		{
			SaveSerialize saveSerialize = SaveSceneManager.s_saveSerializeList[i];
			MoonGuid moonGUID = MoonGuid.Empty;
			foreach (SaveSceneManager.SaveId saveId in SaveSceneManager.Master.SaveData)
			{
				if (saveId.Save == saveSerialize)
				{
					moonGUID = saveId.Id;
				}
			}
			if (moonGUID != MoonGuid.Empty)
			{
				Game.Checkpoint.SaveGameData.Master.SaveObjects.RemoveAll((SaveObject a) => a.Id == moonGUID);
			}
		}
		SaveSceneManager.s_saveSerializeList.Clear();
	}

	// Token: 0x06000683 RID: 1667 RVA: 0x00019780 File Offset: 0x00017980
	public void Save(SaveScene saveScene)
	{
		saveScene.SaveObjects.Clear();
		for (int i = 0; i < this.SaveData.Count; i++)
		{
			SaveSceneManager.SaveId saveId = this.SaveData[i];
			try
			{
				if (saveId.Save as Component != null)
				{
					SaveObject item = new SaveObject(saveId.Id);
					item.Data.WriteMode();
					saveId.Save.Serialize(item.Data);
					saveScene.SaveObjects.Add(item);
				}
			}
			catch (Exception ex)
			{
			}
		}
	}

	// Token: 0x06000684 RID: 1668 RVA: 0x0001982C File Offset: 0x00017A2C
	public void SaveWithoutClearing(SaveScene saveScene)
	{
		this.m_saveCache.Clear();
		for (int i = 0; i < saveScene.SaveObjects.Count; i++)
		{
			this.m_saveCache.Add(saveScene.SaveObjects[i].Id, saveScene.SaveObjects[i].Data);
		}
		for (int j = 0; j < this.SaveData.Count; j++)
		{
			SaveSceneManager.SaveId saveId = this.SaveData[j];
			try
			{
				if (saveId.Save as Component != null)
				{
					Archive archive;
					if (this.m_saveCache.TryGetValue(saveId.Id, out archive))
					{
						archive.WriteMode();
						saveId.Save.Serialize(archive);
					}
					else
					{
						SaveObject item = new SaveObject(saveId.Id);
						item.Data.WriteMode();
						saveId.Save.Serialize(item.Data);
						saveScene.SaveObjects.Add(item);
					}
				}
			}
			catch (Exception ex)
			{
			}
		}
		this.m_saveCache.Clear();
	}

	// Token: 0x06000685 RID: 1669 RVA: 0x00019964 File Offset: 0x00017B64
	public void Save(SaveScene saveScene, ISerializable serializable)
	{
		MoonGuid moonGuid = this.SaveSerializeToId(serializable);
		bool flag = false;
		for (int i = 0; i < saveScene.SaveObjects.Count; i++)
		{
			if (moonGuid == saveScene.SaveObjects[i].Id)
			{
				Archive data = saveScene.SaveObjects[i].Data;
				data.WriteMode();
				serializable.Serialize(data);
				flag = true;
			}
		}
		if (!flag)
		{
			SaveObject item = new SaveObject(moonGuid);
			saveScene.SaveObjects.Add(item);
			Archive data2 = item.Data;
			data2.WriteMode();
			serializable.Serialize(data2);
		}
	}

	// Token: 0x06000686 RID: 1670 RVA: 0x00019A10 File Offset: 0x00017C10
	public void Load(SaveScene saveScene, HashSet<SaveSerialize> objects)
	{
		for (int i = 0; i < saveScene.SaveObjects.Count; i++)
		{
			SaveObject saveObject = saveScene.SaveObjects[i];
			ISerializable serializable = this.IdToSaveSerialize(saveObject.Id);
			try
			{
				SaveSerialize saveSerialize = serializable as SaveSerialize;
				if (saveSerialize != null && objects.Contains(saveSerialize))
				{
					saveObject.Data.ReadMode();
					serializable.Serialize(saveObject.Data);
				}
			}
			catch (Exception ex)
			{
			}
		}
	}

	// Token: 0x06000687 RID: 1671 RVA: 0x00019AA8 File Offset: 0x00017CA8
	public void Load(SaveScene saveScene)
	{
		for (int i = 0; i < saveScene.SaveObjects.Count; i++)
		{
			SaveObject saveObject = saveScene.SaveObjects[i];
			ISerializable serializable = this.IdToSaveSerialize(saveObject.Id);
			try
			{
				if (serializable as Component)
				{
					saveObject.Data.ReadMode();
					serializable.Serialize(saveObject.Data);
				}
			}
			catch (Exception ex)
			{
			}
		}
	}

	// Token: 0x06000688 RID: 1672 RVA: 0x00019B30 File Offset: 0x00017D30
	public void AddChildSaveSerializables()
	{
		this.SaveData.Clear();
		try
		{
			this.RegisterGameObject(base.gameObject);
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x06000689 RID: 1673 RVA: 0x00019B70 File Offset: 0x00017D70
	public static void ClearSaveSlotForOneLife(SaveGameData data)
	{
		SaveObject item = default(SaveObject);
		if (SeinDeathsManager.Instance)
		{
			item = data.Master.SaveObjects.Find((SaveObject a) => a.Id == SeinDeathsManager.Instance.MoonGuid);
		}
		data.PendingScenes.Clear();
		data.Scenes.Clear();
		SaveScene master = data.Master;
		master.SaveObjects.Add(item);
	}

	// Token: 0x040004F6 RID: 1270
	public static SaveSceneManager Master;

	// Token: 0x040004F7 RID: 1271
	public List<SaveSceneManager.SaveId> SaveData = new List<SaveSceneManager.SaveId>();

	// Token: 0x040004F8 RID: 1272
	private static readonly List<SaveSerialize> s_saveSerializeList = new List<SaveSerialize>();

	// Token: 0x040004F9 RID: 1273
	private static readonly HashSet<ISerializable> s_saveSerializableHashSet = new HashSet<ISerializable>();

	// Token: 0x040004FA RID: 1274
	private Dictionary<MoonGuid, Archive> m_saveCache = new Dictionary<MoonGuid, Archive>();

	// Token: 0x020006FD RID: 1789
	[Serializable]
	public class SaveId
	{
		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06002A98 RID: 10904 RVA: 0x000B6B91 File Offset: 0x000B4D91
		// (set) Token: 0x06002A99 RID: 10905 RVA: 0x000B6B9E File Offset: 0x000B4D9E
		public ISerializable Save
		{
			get
			{
				return (ISerializable)this.SaveObject;
			}
			set
			{
				this.SaveObject = (UnityEngine.Object)value;
			}
		}

		// Token: 0x040025ED RID: 9709
		public MoonGuid Id;

		// Token: 0x040025EE RID: 9710
		public UnityEngine.Object SaveObject;
	}
}
