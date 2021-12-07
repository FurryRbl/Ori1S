using System;
using Game;
using UnityEngine;

// Token: 0x020000AB RID: 171
public class CharacterFactory : SaveSerialize
{
	// Token: 0x0600077E RID: 1918 RVA: 0x0001F420 File Offset: 0x0001D620
	public override void Awake()
	{
		CharacterFactory.Instance = this;
		Events.Scheduler.OnSceneRootPreEnabled.Add(new Action<SceneRoot>(this.OnSceneRootPreEnabled));
	}

	// Token: 0x0600077F RID: 1919 RVA: 0x0001F443 File Offset: 0x0001D643
	public override void OnDestroy()
	{
		base.OnDestroy();
		Events.Scheduler.OnSceneRootPreEnabled.Remove(new Action<SceneRoot>(this.OnSceneRootPreEnabled));
	}

	// Token: 0x06000780 RID: 1920 RVA: 0x0001F468 File Offset: 0x0001D668
	public void OnSceneRootPreEnabled(SceneRoot sceneRoot)
	{
		if (this.m_nextCharacterSet)
		{
			if (this.m_nextCharacter == CharacterFactory.Characters.None)
			{
				this.DestroyCharacter();
			}
			else
			{
				this.SpawnCharacter(this.m_nextCharacter, null, Vector3.zero, null);
			}
			this.m_nextCharacterSet = false;
		}
	}

	// Token: 0x06000781 RID: 1921 RVA: 0x0001F4B4 File Offset: 0x0001D6B4
	public GameObject SpawnCharacter(CharacterFactory.Characters character, GameObject prefab, Vector3 position, Action afterLoad)
	{
		this.Current = character;
		this.m_nextCharacterSet = false;
		if (prefab == null)
		{
			prefab = this.GetCharacterPrefab(character);
		}
		if (prefab)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(prefab, position, Quaternion.identity);
			Utility.DontAssociateWithAnyScene(gameObject);
			gameObject.name = prefab.name;
			SaveSceneManager.Master.RegisterGameObject(gameObject);
			LoadFromMasterAtStart loadFromMasterAtStart = gameObject.AddComponent<LoadFromMasterAtStart>();
			if (afterLoad != null)
			{
				LoadFromMasterAtStart loadFromMasterAtStart2 = loadFromMasterAtStart;
				loadFromMasterAtStart2.AfterLoading = (Action)Delegate.Combine(loadFromMasterAtStart2.AfterLoading, afterLoad);
			}
			return gameObject;
		}
		return null;
	}

	// Token: 0x06000782 RID: 1922 RVA: 0x0001F548 File Offset: 0x0001D748
	public void DestroyCharacter()
	{
		if (Game.Characters.Current != null)
		{
			InstantiateUtility.Destroy(Game.Characters.Current.GameObject);
			Game.Characters.Current = null;
		}
		this.Current = CharacterFactory.Characters.None;
	}

	// Token: 0x06000783 RID: 1923 RVA: 0x0001F57C File Offset: 0x0001D77C
	private GameObject GetCharacterPrefab(CharacterFactory.Characters character)
	{
		switch (character)
		{
		case CharacterFactory.Characters.Sein:
			return Resources.Load<GameObject>("characters/seinCharacter");
		case CharacterFactory.Characters.SpiritTreeCutsceneSein:
			return Resources.Load<GameObject>("characters/cutsceneSeinCharacter");
		case CharacterFactory.Characters.BabySein:
			return Resources.Load<GameObject>("characters/babySein");
		case CharacterFactory.Characters.BabySeinWithBerries:
			return Resources.Load<GameObject>("characters/babySeinWithBerries");
		case CharacterFactory.Characters.Naru:
			return Resources.Load<GameObject>("characters/naru");
		case CharacterFactory.Characters.NaruWithSein:
			return Resources.Load<GameObject>("characters/naruWithSein");
		case CharacterFactory.Characters.NaruSadWalk:
			return Resources.Load<GameObject>("characters/naruSadWalk");
		default:
			return null;
		}
	}

	// Token: 0x06000784 RID: 1924 RVA: 0x0001F600 File Offset: 0x0001D800
	public override void Serialize(Archive ar)
	{
		if (ar.Reading)
		{
			this.m_nextCharacter = (CharacterFactory.Characters)ar.Serialize((int)this.m_nextCharacter);
			if (this.m_nextCharacter != this.Current)
			{
				this.m_nextCharacterSet = true;
			}
		}
		else
		{
			ar.Serialize((int)this.Current);
			if (Game.Characters.Current != null)
			{
				ar.Serialize(Game.Characters.Current.GameObject.activeSelf);
			}
			else
			{
				ar.Serialize(true);
			}
		}
	}

	// Token: 0x040005A4 RID: 1444
	public static CharacterFactory Instance;

	// Token: 0x040005A5 RID: 1445
	public CharacterFactory.Characters Current = CharacterFactory.Characters.None;

	// Token: 0x040005A6 RID: 1446
	private CharacterFactory.Characters m_nextCharacter = CharacterFactory.Characters.None;

	// Token: 0x040005A7 RID: 1447
	private bool m_nextCharacterSet;

	// Token: 0x020000AC RID: 172
	public enum Characters
	{
		// Token: 0x040005A9 RID: 1449
		Sein,
		// Token: 0x040005AA RID: 1450
		SpiritTreeCutsceneSein,
		// Token: 0x040005AB RID: 1451
		BabySein,
		// Token: 0x040005AC RID: 1452
		BabySeinWithBerries,
		// Token: 0x040005AD RID: 1453
		Naru,
		// Token: 0x040005AE RID: 1454
		NaruWithSein,
		// Token: 0x040005AF RID: 1455
		NaruSadWalk,
		// Token: 0x040005B0 RID: 1456
		None
	}
}
