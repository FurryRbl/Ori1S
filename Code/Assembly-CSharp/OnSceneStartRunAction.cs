using System;
using Game;
using UnityEngine;

// Token: 0x02000371 RID: 881
[AddComponentMenu("Event Framework/Trigger/On Scene Start Run Action")]
public class OnSceneStartRunAction : SaveSerialize
{
	// Token: 0x06001942 RID: 6466 RVA: 0x0006CC7E File Offset: 0x0006AE7E
	public void Start()
	{
	}

	// Token: 0x06001943 RID: 6467 RVA: 0x0006CC80 File Offset: 0x0006AE80
	public override void Awake()
	{
		Events.Scheduler.OnSceneStartLateAfterSerialize.Add(new Action<SceneRoot>(this.OnSceneStartLateAfterSerialize));
	}

	// Token: 0x06001944 RID: 6468 RVA: 0x0006CC9D File Offset: 0x0006AE9D
	public override void OnDestroy()
	{
		Events.Scheduler.OnSceneStartLateAfterSerialize.Remove(new Action<SceneRoot>(this.OnSceneStartLateAfterSerialize));
	}

	// Token: 0x06001945 RID: 6469 RVA: 0x0006CCBC File Offset: 0x0006AEBC
	public void OnSceneStartLateAfterSerialize(SceneRoot root)
	{
		if (SceneFPSTest.IsRunning())
		{
			return;
		}
		SceneRoot sceneRoot = SceneRoot.FindFromTransform(base.transform);
		if (sceneRoot == null)
		{
			return;
		}
		if (root == sceneRoot && ((this.TriggerOnce && !this.m_isTriggered) || !this.TriggerOnce) && (this.Condition == null || this.Condition.Validate(null)))
		{
			this.m_isTriggered = true;
			this.ActionToRun.Perform(null);
		}
	}

	// Token: 0x06001946 RID: 6470 RVA: 0x0006CD4F File Offset: 0x0006AF4F
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_isTriggered);
	}

	// Token: 0x040015B4 RID: 5556
	public ActionMethod ActionToRun;

	// Token: 0x040015B5 RID: 5557
	public Condition Condition;

	// Token: 0x040015B6 RID: 5558
	public bool TriggerOnce;

	// Token: 0x040015B7 RID: 5559
	private bool m_isTriggered;
}
