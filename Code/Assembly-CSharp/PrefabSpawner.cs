using System;
using Game;
using UnityEngine;

// Token: 0x0200051A RID: 1306
public class PrefabSpawner : MonoBehaviour
{
	// Token: 0x060022CC RID: 8908 RVA: 0x000986DD File Offset: 0x000968DD
	public void Start()
	{
		if (this.SpawnAtStart)
		{
			this.Spawn(null);
		}
	}

	// Token: 0x060022CD RID: 8909 RVA: 0x000986F4 File Offset: 0x000968F4
	public GameObject Spawn(IContext context)
	{
		if (this.Prefab && (this.SpawnOffscreen || UI.Cameras.Current.IsOnScreenPadded(base.transform.position, this.OnScreenPadding)))
		{
			GameObject gameObject = this.Prefab.Prefab(context);
			if (gameObject != null)
			{
				GameObject gameObject2 = (GameObject)InstantiateUtility.Instantiate(gameObject, base.transform.position, Quaternion.identity);
				if (this.Parent)
				{
					gameObject2.transform.SetParentMaintainingRotationAndScale((!this.UseParentOfParent) ? this.Parent : this.Parent.parent);
				}
				if (this.UseRotation)
				{
					gameObject2.transform.rotation = base.transform.rotation;
				}
				Context.SendContextToGameObject(gameObject2, context);
				this.m_prefab = gameObject2;
				return gameObject2;
			}
		}
		return null;
	}

	// Token: 0x060022CE RID: 8910 RVA: 0x000987DF File Offset: 0x000969DF
	public void DestroyInstance()
	{
		if (this.m_prefab)
		{
			InstantiateUtility.Destroy(this.m_prefab);
		}
	}

	// Token: 0x04001D35 RID: 7477
	public PrefabProvider Prefab;

	// Token: 0x04001D36 RID: 7478
	public Transform Parent;

	// Token: 0x04001D37 RID: 7479
	public bool UseParentOfParent;

	// Token: 0x04001D38 RID: 7480
	public bool UseRotation;

	// Token: 0x04001D39 RID: 7481
	public bool SpawnAtStart;

	// Token: 0x04001D3A RID: 7482
	public bool SpawnOffscreen = true;

	// Token: 0x04001D3B RID: 7483
	public float OnScreenPadding = 5f;

	// Token: 0x04001D3C RID: 7484
	private GameObject m_prefab;
}
