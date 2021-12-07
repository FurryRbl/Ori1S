using System;
using System.Collections.Generic;
using System.IO;
using Core;
using UnityEngine;

// Token: 0x020004C3 RID: 1219
public class LeakedSceneObjectDetector : MonoBehaviour
{
	// Token: 0x1700059A RID: 1434
	// (get) Token: 0x0600210F RID: 8463 RVA: 0x00090E33 File Offset: 0x0008F033
	// (set) Token: 0x06002110 RID: 8464 RVA: 0x00090E40 File Offset: 0x0008F040
	public static bool Enabled
	{
		get
		{
			return LeakedSceneObjectDetector.m_instance != null;
		}
		set
		{
			if (LeakedSceneObjectDetector.m_instance && !value)
			{
				InstantiateUtility.Destroy(LeakedSceneObjectDetector.m_instance.gameObject);
			}
			if (LeakedSceneObjectDetector.m_instance == null && value)
			{
				GameObject gameObject = new GameObject("leakedSceneObjectDetector");
				gameObject.AddComponent<LeakedSceneObjectDetector>();
			}
		}
	}

	// Token: 0x06002111 RID: 8465 RVA: 0x00090E99 File Offset: 0x0008F099
	public void Awake()
	{
		LeakedSceneObjectDetector.m_instance = this;
	}

	// Token: 0x06002112 RID: 8466 RVA: 0x00090EA4 File Offset: 0x0008F0A4
	public bool IsValid(GameObject go)
	{
		string name = go.name;
		foreach (string b in this.m_ignoreList)
		{
			if (name == b)
			{
				return false;
			}
		}
		foreach (string value in this.m_ignoreStartsWithList)
		{
			if (name.StartsWith(value))
			{
				return false;
			}
		}
		if (this.IsPrefab(go))
		{
			return false;
		}
		if (go.transform.root.GetComponent<SceneRoot>())
		{
			return false;
		}
		if (go.transform.parent == null)
		{
			return true;
		}
		foreach (string a in this.m_rootsToCheck)
		{
			if (a == go.transform.root.name)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002113 RID: 8467 RVA: 0x00090FA8 File Offset: 0x0008F1A8
	public bool IsPrefab(GameObject go)
	{
		GameObject gameObject = go.transform.root.gameObject;
		return !gameObject.activeInHierarchy && gameObject.activeSelf;
	}

	// Token: 0x06002114 RID: 8468 RVA: 0x00090FDC File Offset: 0x0008F1DC
	public void UpdateLeakedItem()
	{
		Dictionary<int, LeakedSceneObjectDetector.ObjectData> previousData = this.m_previousData;
		previousData.Clear();
		foreach (GameObject gameObject in Resources.FindObjectsOfTypeAll<GameObject>())
		{
			if (gameObject.transform.parent == null && this.IsValid(gameObject))
			{
				int instanceID = gameObject.GetInstanceID();
				if (this.m_data.ContainsKey(instanceID))
				{
					previousData[instanceID] = this.m_data[instanceID];
				}
				else
				{
					previousData[instanceID] = new LeakedSceneObjectDetector.ObjectData(instanceID, gameObject);
				}
			}
		}
		this.m_previousData = this.m_data;
		this.m_data = previousData;
	}

	// Token: 0x06002115 RID: 8469 RVA: 0x0009108D File Offset: 0x0008F28D
	public void OnDisable()
	{
		this.Print();
	}

	// Token: 0x06002116 RID: 8470 RVA: 0x00091098 File Offset: 0x0008F298
	public void Print()
	{
		using (StreamWriter streamWriter = new StreamWriter(new FileStream(Path.Combine(OutputFolder.BuildOutputPath, "leakSceneObjectReport.txt"), FileMode.Create)))
		{
			List<LeakedSceneObjectDetector.ObjectData> list = new List<LeakedSceneObjectDetector.ObjectData>(this.m_data.Values);
			float time = Time.realtimeSinceStartup;
			list.RemoveAll((LeakedSceneObjectDetector.ObjectData a) => time - a.StartTime < 60f);
			list.Sort((LeakedSceneObjectDetector.ObjectData a, LeakedSceneObjectDetector.ObjectData b) => a.Name.CompareTo(b.Name));
			foreach (LeakedSceneObjectDetector.ObjectData objectData in list)
			{
				float num = time - objectData.StartTime;
				streamWriter.WriteLine(string.Concat(new object[]
				{
					objectData.Name,
					"\t",
					num,
					"\t",
					objectData.Scene
				}));
			}
		}
	}

	// Token: 0x06002117 RID: 8471 RVA: 0x000911C8 File Offset: 0x0008F3C8
	public void Update()
	{
		if (this.m_delay < 0f)
		{
			this.UpdateLeakedItem();
			this.m_delay = 60f;
		}
		this.m_delay -= Time.deltaTime;
	}

	// Token: 0x04001BFA RID: 7162
	private static LeakedSceneObjectDetector m_instance;

	// Token: 0x04001BFB RID: 7163
	private string[] m_ignoreList = new string[]
	{
		"audioObjectsParent",
		"menuScreenManager",
		"prefabWarm",
		"seinCharacter",
		"uberPoolFor",
		"warmGroups",
		"worldMapLogic",
		"systems",
		"seinHUD",
		"ori"
	};

	// Token: 0x04001BFC RID: 7164
	private string[] m_ignoreStartsWithList = new string[]
	{
		"uberPoolFor"
	};

	// Token: 0x04001BFD RID: 7165
	private string[] m_rootsToCheck = new string[]
	{
		"dynamicGameObjects",
		"audioObjectsParent"
	};

	// Token: 0x04001BFE RID: 7166
	private Dictionary<int, LeakedSceneObjectDetector.ObjectData> m_data = new Dictionary<int, LeakedSceneObjectDetector.ObjectData>();

	// Token: 0x04001BFF RID: 7167
	private Dictionary<int, LeakedSceneObjectDetector.ObjectData> m_previousData = new Dictionary<int, LeakedSceneObjectDetector.ObjectData>();

	// Token: 0x04001C00 RID: 7168
	private float m_delay;

	// Token: 0x0200097E RID: 2430
	public class ObjectData
	{
		// Token: 0x06003540 RID: 13632 RVA: 0x000DF2D0 File Offset: 0x000DD4D0
		public ObjectData(int id, GameObject gameObject)
		{
			this.ID = id;
			this.StartTime = Time.realtimeSinceStartup;
			this.Name = gameObject.name;
			RuntimeSceneMetaData currentScene = Scenes.Manager.CurrentScene;
			if (currentScene != null)
			{
				this.Scene = currentScene.Scene;
			}
			else
			{
				this.Scene = "Unkown";
			}
		}

		// Token: 0x04002FD9 RID: 12249
		public int ID;

		// Token: 0x04002FDA RID: 12250
		public float StartTime;

		// Token: 0x04002FDB RID: 12251
		public string Name;

		// Token: 0x04002FDC RID: 12252
		public string Scene;
	}
}
