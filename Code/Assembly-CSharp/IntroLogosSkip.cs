using System;
using UnityEngine;

// Token: 0x02000002 RID: 2
public class IntroLogosSkip : MonoBehaviour
{
	// Token: 0x06000002 RID: 2 RVA: 0x00002304 File Offset: 0x00000504
	private void Update()
	{
		if ((UberShaderPrewarmer.IsLoaded || Application.isEditor) && GameStateMachine.Instance.CurrentState == GameStateMachine.State.Logos && this.TimelineRunning.IsRunning && Input.anyKey && Time.realtimeSinceStartup - this.m_lastInput > 0.3f)
		{
			this.SkipLogos();
			this.m_lastInput = Time.realtimeSinceStartup;
		}
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00002375 File Offset: 0x00000575
	private void SkipLogos()
	{
		this.EndSequence.Perform(null);
		this.TimelineRunning.IsRunning = false;
	}

	// Token: 0x04000001 RID: 1
	private const float c_inputRepeatDelay = 0.3f;

	// Token: 0x04000002 RID: 2
	private float m_lastInput;

	// Token: 0x04000003 RID: 3
	public ActionSequence EndSequence;

	// Token: 0x04000004 RID: 4
	public ActionSequence TimelineRunning;
}
