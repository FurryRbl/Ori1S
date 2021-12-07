using System;
using UnityEngine;

// Token: 0x02000461 RID: 1121
public class DisableOnKill : MonoBehaviour, IKillReciever
{
	// Token: 0x06001EDA RID: 7898 RVA: 0x00087DF5 File Offset: 0x00085FF5
	public void OnKill()
	{
		base.gameObject.SetActive(false);
	}
}
