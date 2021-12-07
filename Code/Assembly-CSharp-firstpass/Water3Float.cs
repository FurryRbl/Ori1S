using System;
using UnityEngine;

// Token: 0x02000016 RID: 22
public class Water3Float : MonoBehaviour
{
	// Token: 0x06000065 RID: 101 RVA: 0x000059E0 File Offset: 0x00003BE0
	private void Start()
	{
		if (!this.m_Water)
		{
			Debug.Log("Please assign a Water patch for bouyancy script on " + base.gameObject.name + " to work");
			base.enabled = false;
		}
	}

	// Token: 0x06000066 RID: 102 RVA: 0x00005A24 File Offset: 0x00003C24
	private void Update()
	{
		Vector3 heightOffsetAt = this.m_Water.GetHeightOffsetAt(base.transform.position);
		Vector3 normalAt = this.m_Water.GetNormalAt(base.transform.position, 1f);
		Quaternion identity = Quaternion.identity;
		identity.SetFromToRotation(Vector3.up, normalAt);
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, identity, Time.deltaTime * 4f);
		base.transform.position = Vector3.Lerp(base.transform.position, heightOffsetAt, Time.deltaTime * 4f);
	}

	// Token: 0x04000085 RID: 133
	public Water3 m_Water;
}
