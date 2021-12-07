using System;

// Token: 0x02000078 RID: 120
public abstract class SaveSerialize : GuidOwner, ISerializable, ISceneManagerRegisterReciever
{
	// Token: 0x06000529 RID: 1321 RVA: 0x0001476F File Offset: 0x0001296F
	public void RegisterToSaveSceneManager(SaveSceneManager saveSceneManager)
	{
		saveSceneManager.AddSaveObject(this, base.GetGuid());
	}

	// Token: 0x0600052A RID: 1322 RVA: 0x0001477E File Offset: 0x0001297E
	public virtual void Awake()
	{
	}

	// Token: 0x0600052B RID: 1323 RVA: 0x00014780 File Offset: 0x00012980
	public virtual void OnDestroy()
	{
	}

	// Token: 0x0600052C RID: 1324
	public abstract void Serialize(Archive ar);
}
