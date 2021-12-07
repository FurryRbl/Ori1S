using System;
using System.Collections.Generic;
using Game;

// Token: 0x0200031D RID: 797
public class TriggerByString : Trigger
{
	// Token: 0x06001767 RID: 5991 RVA: 0x00064E8B File Offset: 0x0006308B
	public static void Register(string s)
	{
		TriggerByString.m_stringTriggersList.Add(s);
	}

	// Token: 0x06001768 RID: 5992 RVA: 0x00064E98 File Offset: 0x00063098
	public static void Deregister(string s)
	{
		TriggerByString.m_stringTriggersList.Remove(s);
	}

	// Token: 0x06001769 RID: 5993 RVA: 0x00064EA8 File Offset: 0x000630A8
	public new void Awake()
	{
		base.Awake();
		Events.Scheduler.OnSceneRootEnabledAfterSerialize.Add(new Action<SceneRoot>(this.OnSceneRootEnabledAfterSerialize));
		if (this.Data.TriggerEvent == TriggerByString.TriggerEvent.Awake && TriggerByString.m_stringTriggersList.Contains(this.Data.String))
		{
			this.DoTrigger();
		}
	}

	// Token: 0x0600176A RID: 5994 RVA: 0x00064F06 File Offset: 0x00063106
	public void Start()
	{
		if (this.Data.TriggerEvent == TriggerByString.TriggerEvent.Start && TriggerByString.m_stringTriggersList.Contains(this.Data.String))
		{
			this.DoTrigger();
		}
	}

	// Token: 0x0600176B RID: 5995 RVA: 0x00064F39 File Offset: 0x00063139
	public static void OnGameReset()
	{
		TriggerByString.m_stringTriggersList.Clear();
	}

	// Token: 0x0600176C RID: 5996 RVA: 0x00064F45 File Offset: 0x00063145
	public void FixedUpdate()
	{
		if (this.Data.TriggerEvent == TriggerByString.TriggerEvent.Always && TriggerByString.m_stringTriggersList.Contains(this.Data.String))
		{
			this.DoTrigger();
		}
	}

	// Token: 0x0600176D RID: 5997 RVA: 0x00064F78 File Offset: 0x00063178
	public void DoTrigger()
	{
		if (SceneFPSTest.IsRunning())
		{
			return;
		}
		TriggerByString.Deregister(this.Data.String);
		base.DoTrigger(true);
	}

	// Token: 0x0600176E RID: 5998 RVA: 0x00064FA8 File Offset: 0x000631A8
	public static void SerializeStringTriggers(Archive ar)
	{
		if (ar.Writing)
		{
			ar.Serialize(TriggerByString.m_stringTriggersList.Count);
			foreach (string value in TriggerByString.m_stringTriggersList)
			{
				ar.Serialize(value);
			}
		}
		else if (ar.Reading)
		{
			int num = ar.Serialize(TriggerByString.m_stringTriggersList.Count);
			TriggerByString.m_stringTriggersList = new List<string>();
			for (int i = 0; i < num; i++)
			{
				TriggerByString.m_stringTriggersList.Add(ar.Serialize(string.Empty));
			}
		}
	}

	// Token: 0x0600176F RID: 5999 RVA: 0x00065070 File Offset: 0x00063270
	public new void OnDestroy()
	{
		base.OnDestroy();
		Events.Scheduler.OnSceneRootEnabledAfterSerialize.Remove(new Action<SceneRoot>(this.OnSceneRootEnabledAfterSerialize));
		base.OnDestroy();
	}

	// Token: 0x06001770 RID: 6000 RVA: 0x0006509C File Offset: 0x0006329C
	public void OnSceneRootEnabledAfterSerialize(SceneRoot sceneRoot)
	{
		SceneRoot sceneRoot2 = SceneRoot.FindFromTransform(base.transform);
		if (sceneRoot2 == null)
		{
			throw new Exception("scene root is null, you can't use this action on objects instantiated outside level root");
		}
		if (sceneRoot == sceneRoot2 && this.Data.TriggerEvent == TriggerByString.TriggerEvent.SceneEnabledAfterSerialize && TriggerByString.m_stringTriggersList.Contains(this.Data.String))
		{
			this.DoTrigger();
		}
	}

	// Token: 0x04001416 RID: 5142
	public TriggerByString.StringTriggerData Data;

	// Token: 0x04001417 RID: 5143
	private static List<string> m_stringTriggersList = new List<string>();

	// Token: 0x02000383 RID: 899
	[Serializable]
	public class StringTriggerData
	{
		// Token: 0x04001600 RID: 5632
		public string String = string.Empty;

		// Token: 0x04001601 RID: 5633
		public TriggerByString.TriggerEvent TriggerEvent;
	}

	// Token: 0x02000384 RID: 900
	public enum TriggerEvent
	{
		// Token: 0x04001603 RID: 5635
		Awake,
		// Token: 0x04001604 RID: 5636
		Start,
		// Token: 0x04001605 RID: 5637
		SceneEnabledAfterSerialize,
		// Token: 0x04001606 RID: 5638
		Always
	}
}
