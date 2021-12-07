using System;
using System.Collections.Generic;
using System.IO;

// Token: 0x02000156 RID: 342
public class SaveGameData
{
	// Token: 0x06000DE4 RID: 3556 RVA: 0x00040D64 File Offset: 0x0003EF64
	public void SaveToWriter(BinaryWriter writer)
	{
		SaveGameData.CurrentSaveFileVersion = 1;
		writer.Write("SaveGameData");
		writer.Write(1);
		writer.Write(this.Scenes.Count);
		foreach (SaveScene saveScene in this.Scenes.Values)
		{
			writer.Write(saveScene.SceneGUID.ToByteArray());
			writer.Write(saveScene.SaveObjects.Count);
			foreach (SaveObject saveObject in saveScene.SaveObjects)
			{
				writer.Write(saveObject.Id.ToByteArray());
				saveObject.Data.WriteMemoryStreamToBinaryWriter(writer);
			}
		}
		((IDisposable)writer).Dispose();
	}

	// Token: 0x06000DE5 RID: 3557 RVA: 0x00040E70 File Offset: 0x0003F070
	public bool LoadFromReader(BinaryReader reader)
	{
		this.Scenes.Clear();
		this.PendingScenes.Clear();
		if (reader.ReadString() != "SaveGameData")
		{
			return false;
		}
		SaveGameData.CurrentSaveFileVersion = reader.ReadInt32();
		int num = reader.ReadInt32();
		for (int i = 0; i < num; i++)
		{
			SaveScene saveScene = new SaveScene();
			saveScene.SceneGUID = new MoonGuid(reader.ReadBytes(16));
			this.Scenes.Add(saveScene.SceneGUID, saveScene);
			int num2 = reader.ReadInt32();
			for (int j = 0; j < num2; j++)
			{
				SaveObject item = new SaveObject(new MoonGuid(reader.ReadBytes(16)));
				item.Data.ReadMemoryStreamFromBinaryReader(reader);
				saveScene.SaveObjects.Add(item);
			}
		}
		return true;
	}

	// Token: 0x170002A6 RID: 678
	// (get) Token: 0x06000DE6 RID: 3558 RVA: 0x00040F45 File Offset: 0x0003F145
	public SaveScene Master
	{
		get
		{
			return this.InsertScene(MoonGuid.Empty);
		}
	}

	// Token: 0x06000DE7 RID: 3559 RVA: 0x00040F54 File Offset: 0x0003F154
	public SaveScene GetScene(MoonGuid sceneGuid)
	{
		SaveScene result;
		if (this.Scenes.TryGetValue(sceneGuid, out result))
		{
			return result;
		}
		return null;
	}

	// Token: 0x06000DE8 RID: 3560 RVA: 0x00040F78 File Offset: 0x0003F178
	public SaveScene InsertScene(MoonGuid sceneGuid)
	{
		SaveScene saveScene;
		if (this.Scenes.TryGetValue(sceneGuid, out saveScene))
		{
			return saveScene;
		}
		saveScene = new SaveScene
		{
			SceneGUID = sceneGuid
		};
		this.Scenes.Add(saveScene.SceneGUID, saveScene);
		return saveScene;
	}

	// Token: 0x06000DE9 RID: 3561 RVA: 0x00040FBC File Offset: 0x0003F1BC
	public SaveScene InsertPendingScene(MoonGuid sceneGUID)
	{
		SaveScene saveScene;
		if (this.PendingScenes.TryGetValue(sceneGUID, out saveScene))
		{
			return saveScene;
		}
		saveScene = new SaveScene
		{
			SceneGUID = sceneGUID
		};
		this.PendingScenes.Add(saveScene.SceneGUID, saveScene);
		return saveScene;
	}

	// Token: 0x06000DEA RID: 3562 RVA: 0x00041000 File Offset: 0x0003F200
	public bool SceneExists(MoonGuid sceneGUID)
	{
		return this.Scenes.ContainsKey(sceneGUID);
	}

	// Token: 0x06000DEB RID: 3563 RVA: 0x00041010 File Offset: 0x0003F210
	public void ApplyPendingScenes()
	{
		foreach (SaveScene saveScene in this.PendingScenes.Values)
		{
			if (this.SceneExists(saveScene.SceneGUID))
			{
				this.Scenes.Remove(saveScene.SceneGUID);
			}
			this.Scenes.Add(saveScene.SceneGUID, saveScene);
		}
		this.ClearPendingScenes();
	}

	// Token: 0x06000DEC RID: 3564 RVA: 0x000410A4 File Offset: 0x0003F2A4
	public void ClearPendingScenes()
	{
		this.PendingScenes.Clear();
	}

	// Token: 0x06000DED RID: 3565 RVA: 0x000410B1 File Offset: 0x0003F2B1
	public void ClearAllData()
	{
		this.Scenes.Clear();
		this.PendingScenes.Clear();
	}

	// Token: 0x04000B48 RID: 2888
	public const int DATA_VERSION = 1;

	// Token: 0x04000B49 RID: 2889
	private const string FILE_FORMAT_STRING = "SaveGameData";

	// Token: 0x04000B4A RID: 2890
	public readonly Dictionary<MoonGuid, SaveScene> Scenes = new Dictionary<MoonGuid, SaveScene>();

	// Token: 0x04000B4B RID: 2891
	public readonly Dictionary<MoonGuid, SaveScene> PendingScenes = new Dictionary<MoonGuid, SaveScene>();

	// Token: 0x04000B4C RID: 2892
	public static int CurrentSaveFileVersion = -1;
}
