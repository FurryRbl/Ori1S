using System;
using Game;
using UnityEngine;

// Token: 0x020008DE RID: 2270
public class InvisibleCheckpoint : MonoBehaviour
{
	// Token: 0x060032A3 RID: 12963 RVA: 0x000D5B34 File Offset: 0x000D3D34
	public void Awake()
	{
		this.m_bounds = new Rect
		{
			width = base.transform.lossyScale.x,
			height = base.transform.lossyScale.y,
			center = base.transform.position
		};
	}

	// Token: 0x060032A4 RID: 12964 RVA: 0x000D5B9B File Offset: 0x000D3D9B
	public virtual bool CanCreateCheckpoint()
	{
		return this.m_shouldAcceptRecievers && (!this.Condition || this.Condition.Validate(null));
	}

	// Token: 0x060032A5 RID: 12965 RVA: 0x000D5BD0 File Offset: 0x000D3DD0
	public void FixedUpdate()
	{
		if (Characters.Sein == null)
		{
			return;
		}
		if (this.m_bounds.Contains(Characters.Sein.Position))
		{
			if (this.CanCreateCheckpoint())
			{
				Characters.Sein.PickupHandler.OnEnterCheckpoint(this);
			}
		}
		else
		{
			this.m_shouldAcceptRecievers = true;
		}
	}

	// Token: 0x060032A6 RID: 12966 RVA: 0x000D5C2F File Offset: 0x000D3E2F
	public void OnCheckpointCreated()
	{
		this.m_shouldAcceptRecievers = false;
	}

	// Token: 0x04002D89 RID: 11657
	public Condition Condition;

	// Token: 0x04002D8A RID: 11658
	public Vector2 RespawnPosition = Vector2.zero;

	// Token: 0x04002D8B RID: 11659
	private bool m_shouldAcceptRecievers;

	// Token: 0x04002D8C RID: 11660
	private Rect m_bounds;
}
