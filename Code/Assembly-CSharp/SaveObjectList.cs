using System;
using System.Collections.Generic;

// Token: 0x0200055B RID: 1371
public class SaveObjectList
{
	// Token: 0x060023C7 RID: 9159 RVA: 0x0009C834 File Offset: 0x0009AA34
	public void Convert(SaveSerialize[] saveSerializes, SaveSceneManager saveSceneManager)
	{
		this.m_saveSceneManager = saveSceneManager;
		this.SaveObjects.Clear();
		if (!(this.m_saveSceneManager == null))
		{
			foreach (SaveSerialize saveSerialize in saveSerializes)
			{
				SaveObject item = new SaveObject(this.m_saveSceneManager.SaveSerializeToId(saveSerialize));
				this.SaveObjects.Add(item);
			}
		}
	}

	// Token: 0x060023C8 RID: 9160 RVA: 0x0009C8A4 File Offset: 0x0009AAA4
	public void Save()
	{
		for (int i = 0; i < this.SaveObjects.Count; i++)
		{
			SaveObject saveObject = this.SaveObjects[i];
			ISerializable serializable = this.m_saveSceneManager.IdToSaveSerialize(saveObject.Id);
			if (serializable != null)
			{
				saveObject.Data.WriteMode();
				serializable.Serialize(saveObject.Data);
			}
		}
	}

	// Token: 0x060023C9 RID: 9161 RVA: 0x0009C90C File Offset: 0x0009AB0C
	public void Load()
	{
		foreach (SaveObject saveObject in this.SaveObjects)
		{
			ISerializable serializable = this.m_saveSceneManager.IdToSaveSerialize(saveObject.Id);
			if (serializable != null)
			{
				saveObject.Data.ReadMode();
				serializable.Serialize(saveObject.Data);
			}
		}
	}

	// Token: 0x04001DFA RID: 7674
	public List<SaveObject> SaveObjects = new List<SaveObject>();

	// Token: 0x04001DFB RID: 7675
	private SaveSceneManager m_saveSceneManager;
}
