using System;
using System.Collections.Generic;
using System.IO;
using Game;

// Token: 0x02000184 RID: 388
public class CheckpointData : IFrameData
{
	// Token: 0x06000F4A RID: 3914 RVA: 0x00046108 File Offset: 0x00044308
	public CheckpointData(BinaryReader binaryReader)
	{
		this.Load(binaryReader);
	}

	// Token: 0x06000F4B RID: 3915 RVA: 0x00046130 File Offset: 0x00044330
	public CheckpointData(List<MoonGuid> scenes)
	{
		SaveGameData saveGameData = Game.Checkpoint.SaveGameData;
		SaveScene master = saveGameData.Master;
		List<SaveObject> list = new List<SaveObject>();
		List<SaveObject> saveObjects = master.SaveObjects;
		master.SaveObjects = list;
		SaveSceneManager.Master.Save(master);
		master.SaveObjects = saveObjects;
		foreach (SaveObject item in list)
		{
			this.m_globalSaveObjects.Add(item);
		}
		foreach (MoonGuid moonGuid in scenes)
		{
			SaveScene scene = saveGameData.GetScene(moonGuid);
			if (scene != null)
			{
				List<SaveObject> value = new List<SaveObject>(scene.SaveObjects);
				this.m_sceneSaveObjects.Add(new KeyValuePair<MoonGuid, List<SaveObject>>(moonGuid, value));
			}
		}
	}

	// Token: 0x06000F4C RID: 3916 RVA: 0x00046250 File Offset: 0x00044450
	public RecorderFrame.FrameDataTypes FrameType()
	{
		return RecorderFrame.FrameDataTypes.CheckpointData;
	}

	// Token: 0x170002C6 RID: 710
	// (get) Token: 0x06000F4D RID: 3917 RVA: 0x00046253 File Offset: 0x00044453
	public List<KeyValuePair<MoonGuid, List<SaveObject>>> SceneSaveObjects
	{
		get
		{
			return this.m_sceneSaveObjects;
		}
	}

	// Token: 0x170002C7 RID: 711
	// (get) Token: 0x06000F4E RID: 3918 RVA: 0x0004625B File Offset: 0x0004445B
	public List<SaveObject> GlobalSaveObjects
	{
		get
		{
			return this.m_globalSaveObjects;
		}
	}

	// Token: 0x06000F4F RID: 3919 RVA: 0x00046264 File Offset: 0x00044464
	public static void Record(BinaryWriter binaryWriter, List<MoonGuid> scenes)
	{
		List<KeyValuePair<MoonGuid, List<SaveObject>>> list = new List<KeyValuePair<MoonGuid, List<SaveObject>>>();
		List<SaveObject> list2 = new List<SaveObject>();
		SaveGameData saveGameData = Game.Checkpoint.SaveGameData;
		SaveScene master = saveGameData.Master;
		List<SaveObject> list3 = new List<SaveObject>();
		List<SaveObject> saveObjects = master.SaveObjects;
		master.SaveObjects = list3;
		SaveSceneManager.Master.Save(master);
		master.SaveObjects = saveObjects;
		foreach (SaveObject item in list3)
		{
			list2.Add(item);
		}
		foreach (MoonGuid moonGuid in scenes)
		{
			SaveScene scene = saveGameData.GetScene(moonGuid);
			if (scene != null)
			{
				List<SaveObject> value = new List<SaveObject>(scene.SaveObjects);
				list.Add(new KeyValuePair<MoonGuid, List<SaveObject>>(moonGuid, value));
			}
		}
		binaryWriter.Write(7);
		binaryWriter.Write(list2.Count);
		foreach (SaveObject saveObject in list2)
		{
			binaryWriter.Write(saveObject.Id.ToByteArray());
			saveObject.Data.WriteMemoryStreamToBinaryWriter(binaryWriter);
		}
		binaryWriter.Write(list.Count);
		foreach (KeyValuePair<MoonGuid, List<SaveObject>> keyValuePair in list)
		{
			binaryWriter.Write(keyValuePair.Key.ToByteArray());
			binaryWriter.Write(keyValuePair.Value.Count);
			foreach (SaveObject saveObject2 in keyValuePair.Value)
			{
				binaryWriter.Write(saveObject2.Id.ToByteArray());
				saveObject2.Data.WriteMemoryStreamToBinaryWriter(binaryWriter);
			}
		}
	}

	// Token: 0x06000F50 RID: 3920 RVA: 0x000464B8 File Offset: 0x000446B8
	public void Save(BinaryWriter binaryWriter)
	{
		binaryWriter.Write(this.m_globalSaveObjects.Count);
		foreach (SaveObject saveObject in this.m_globalSaveObjects)
		{
			binaryWriter.Write(saveObject.Id.ToByteArray());
			saveObject.Data.WriteMemoryStreamToBinaryWriter(binaryWriter);
		}
		binaryWriter.Write(this.m_sceneSaveObjects.Count);
		foreach (KeyValuePair<MoonGuid, List<SaveObject>> keyValuePair in this.m_sceneSaveObjects)
		{
			binaryWriter.Write(keyValuePair.Key.ToByteArray());
			binaryWriter.Write(keyValuePair.Value.Count);
			foreach (SaveObject saveObject2 in keyValuePair.Value)
			{
				binaryWriter.Write(saveObject2.Id.ToByteArray());
				saveObject2.Data.WriteMemoryStreamToBinaryWriter(binaryWriter);
			}
		}
	}

	// Token: 0x06000F51 RID: 3921 RVA: 0x00046618 File Offset: 0x00044818
	public void Load(BinaryReader binaryReader)
	{
		int num = binaryReader.ReadInt32();
		this.m_globalSaveObjects.Clear();
		for (int i = 0; i < num; i++)
		{
			SaveObject item = new SaveObject(new MoonGuid(binaryReader.ReadBytes(16)));
			item.Data.ReadMemoryStreamFromBinaryReader(binaryReader);
			this.m_globalSaveObjects.Add(item);
		}
		num = binaryReader.ReadInt32();
		this.m_sceneSaveObjects.Clear();
		for (int j = 0; j < num; j++)
		{
			MoonGuid key = new MoonGuid(binaryReader.ReadBytes(16));
			List<SaveObject> list = new List<SaveObject>();
			KeyValuePair<MoonGuid, List<SaveObject>> item2 = new KeyValuePair<MoonGuid, List<SaveObject>>(key, list);
			int num2 = binaryReader.ReadInt32();
			for (int k = 0; k < num2; k++)
			{
				SaveObject item3 = new SaveObject(new MoonGuid(binaryReader.ReadBytes(16)));
				item3.Data.ReadMemoryStreamFromBinaryReader(binaryReader);
				list.Add(item3);
			}
			this.m_sceneSaveObjects.Add(item2);
		}
	}

	// Token: 0x04000C22 RID: 3106
	private List<KeyValuePair<MoonGuid, List<SaveObject>>> m_sceneSaveObjects = new List<KeyValuePair<MoonGuid, List<SaveObject>>>();

	// Token: 0x04000C23 RID: 3107
	private List<SaveObject> m_globalSaveObjects = new List<SaveObject>();
}
