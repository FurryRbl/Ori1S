using System;
using UnityEngine;

// Token: 0x0200035E RID: 862
[RequireComponent(typeof(ListOfCollidedObjects))]
public class ColliderEnterExitTrigger : MonoBehaviour
{
	// Token: 0x0600189D RID: 6301 RVA: 0x000699E7 File Offset: 0x00067BE7
	private void Start()
	{
		this.m_listOfCollidedObjects = base.GetComponent<ListOfCollidedObjects>();
	}

	// Token: 0x0600189E RID: 6302 RVA: 0x000699F8 File Offset: 0x00067BF8
	public bool ValidObject(GameObject go)
	{
		if (this.GameObjectFilter)
		{
			return this.GameObjectFilter.Valid(go);
		}
		Collider component = go.GetComponent<Collider>();
		return !(component == null) && !component.isTrigger && !go.HasComponent<Projectile>() && !go.CompareTag("Player");
	}

	// Token: 0x0600189F RID: 6303 RVA: 0x00069A64 File Offset: 0x00067C64
	private void FixedUpdate()
	{
		int num = 0;
		for (int i = 0; i < this.m_listOfCollidedObjects.TriggerObjects.Count; i++)
		{
			GameObject go = this.m_listOfCollidedObjects.TriggerObjects[i];
			if (this.ValidObject(go))
			{
				num++;
			}
		}
		if (this.HasEntered)
		{
			if (num == 0)
			{
				this.EnterExitActionsExecutor.ExitTrigger();
				this.HasEntered = false;
			}
		}
		else if (num != 0)
		{
			this.EnterExitActionsExecutor.EnterTrigger();
			this.HasEntered = true;
		}
	}

	// Token: 0x0400152A RID: 5418
	public EnterExitActionsExecutor EnterExitActionsExecutor;

	// Token: 0x0400152B RID: 5419
	private ListOfCollidedObjects m_listOfCollidedObjects;

	// Token: 0x0400152C RID: 5420
	public GameObjectFilter GameObjectFilter;

	// Token: 0x0400152D RID: 5421
	public bool HasEntered;
}
