using System;
using UnityEngine;

// Token: 0x02000729 RID: 1833
public class Door : MonoBehaviour
{
	// Token: 0x170006E4 RID: 1764
	// (get) Token: 0x06002B2F RID: 11055 RVA: 0x000B9337 File Offset: 0x000B7537
	public SceneRoot SceneRoot
	{
		get
		{
			return SceneRoot.FindFromTransform(base.transform);
		}
	}

	// Token: 0x06002B30 RID: 11056 RVA: 0x000B9344 File Offset: 0x000B7544
	private void OnTriggerStay(Collider other)
	{
		SeinController component = other.GetComponent<SeinController>();
		if (component != null)
		{
			component.GetComponentInChildren<SeinDoorHandler>().OnDoorOverlap(this);
		}
	}

	// Token: 0x040026D7 RID: 9943
	public string OtherDoorName;

	// Token: 0x040026D8 RID: 9944
	public bool CreateCheckpoint;

	// Token: 0x040026D9 RID: 9945
	public float TransitionDelay;

	// Token: 0x040026DA RID: 9946
	public ActionMethod EnterDoorAction;

	// Token: 0x040026DB RID: 9947
	public ActionMethod ComeOutOfDoorAction;

	// Token: 0x040026DC RID: 9948
	public MessageProvider OverrideEnterDoorMessage;
}
