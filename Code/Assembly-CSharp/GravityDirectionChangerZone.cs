using System;
using UnityEngine;

// Token: 0x020008CD RID: 2253
public class GravityDirectionChangerZone : MonoBehaviour
{
	// Token: 0x0600322B RID: 12843 RVA: 0x000D4994 File Offset: 0x000D2B94
	private void OnTriggerEnter(Collider other)
	{
		SeinCharacter component = other.GetComponent<SeinCharacter>();
		if (component != null)
		{
			Vector2 worldSpeed = component.PlatformBehaviour.PlatformMovement.WorldSpeed;
			float z = base.transform.localRotation.eulerAngles.z;
			component.PlatformBehaviour.PlatformMovement.GravityAngle = (component.PlatformBehaviour.Gravity.BaseSettings.GravityAngle = z);
			component.PlatformBehaviour.PlatformMovement.transform.eulerAngles = new Vector3(0f, 0f, z);
			component.PlatformBehaviour.PlatformMovement.WorldSpeed = worldSpeed;
		}
	}
}
