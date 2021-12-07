using System;
using Game;
using UnityEngine;

// Token: 0x02000462 RID: 1122
public class SpawnOnKill : MonoBehaviour, IKillReciever, IPooled
{
	// Token: 0x06001EDD RID: 7901 RVA: 0x00087E18 File Offset: 0x00086018
	public static GameObject GetSpawnOnKillObjectsParent()
	{
		if (SpawnOnKill.m_spawnOnKillObjectsParent == null)
		{
			SpawnOnKill.m_spawnOnKillObjectsParent = new GameObject("spawnOnKillObjectsParent");
			Utility.DontAssociateWithAnyScene(SpawnOnKill.m_spawnOnKillObjectsParent);
		}
		return SpawnOnKill.m_spawnOnKillObjectsParent;
	}

	// Token: 0x06001EDE RID: 7902 RVA: 0x00087E54 File Offset: 0x00086054
	public void OnKill()
	{
		if (this.ObjectToSpawn == null)
		{
			return;
		}
		Vector3 position = base.transform.position + this.PositionOffset;
		if (UI.Cameras.Current.IsOnScreenPadded(position, 7f))
		{
			GameObject gameObject = InstantiateUtility.Instantiate(this.ObjectToSpawn, position, Quaternion.identity) as GameObject;
			gameObject.transform.SetParentMaintainingLocalTransform(SpawnOnKill.GetSpawnOnKillObjectsParent().transform);
			gameObject.transform.localScale *= this.Scale;
		}
	}

	// Token: 0x06001EDF RID: 7903 RVA: 0x00087EE7 File Offset: 0x000860E7
	public void OnPoolSpawned()
	{
	}

	// Token: 0x04001AAE RID: 6830
	public GameObject ObjectToSpawn;

	// Token: 0x04001AAF RID: 6831
	public Vector3 PositionOffset;

	// Token: 0x04001AB0 RID: 6832
	public float Scale = 1f;

	// Token: 0x04001AB1 RID: 6833
	private static GameObject m_spawnOnKillObjectsParent;
}
