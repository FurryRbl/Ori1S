using System;
using UnityEngine;

// Token: 0x0200037D RID: 893
public class SeinHeadBumpTrigger : MonoBehaviour
{
	// Token: 0x06001972 RID: 6514 RVA: 0x0006D910 File Offset: 0x0006BB10
	public void OnTriggerEnter(Collider collider)
	{
		SeinCharacter component = collider.GetComponent<SeinCharacter>();
		if (component != null && component.PlatformBehaviour.PlatformMovement.Jumping)
		{
			this.Action.Perform(null);
		}
	}

	// Token: 0x06001973 RID: 6515 RVA: 0x0006D954 File Offset: 0x0006BB54
	public void OnCollisionEnter(Collision other)
	{
		SeinCharacter component = other.gameObject.GetComponent<SeinCharacter>();
		if (component != null && component.PlatformBehaviour.PlatformMovement.Ceiling.WasOn)
		{
			this.Action.Perform(null);
		}
	}

	// Token: 0x040015E3 RID: 5603
	public ActionMethod Action;
}
