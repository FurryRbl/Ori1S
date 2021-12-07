using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000A0 RID: 160
public class CameraTarget
{
	// Token: 0x060006DF RID: 1759 RVA: 0x0001C344 File Offset: 0x0001A544
	public CameraTarget(GameplayCamera gameplayGameplayCamera)
	{
		this.GameplayCamera = gameplayGameplayCamera;
	}

	// Token: 0x17000192 RID: 402
	// (get) Token: 0x060006E0 RID: 1760 RVA: 0x0001C376 File Offset: 0x0001A576
	public CameraTarget.TargetLayer BaseTargetLayer
	{
		get
		{
			return this.m_targetLayers[0];
		}
	}

	// Token: 0x060006E1 RID: 1761 RVA: 0x0001C384 File Offset: 0x0001A584
	public CameraTarget.TargetLayer AddTargetLayer(Transform target, float weight, bool followX, bool followY, bool followZ)
	{
		CameraTarget.TargetLayer targetLayer = new CameraTarget.TargetLayer
		{
			Transform = target,
			Weight = weight,
			FollowTargetX = followX,
			FollowTargetY = followY,
			FollowTargetZ = followZ
		};
		this.m_targetLayers.Add(targetLayer);
		return targetLayer;
	}

	// Token: 0x060006E2 RID: 1762 RVA: 0x0001C3CB File Offset: 0x0001A5CB
	public void RemoveTargetLayer(CameraTarget.TargetLayer targetLayer)
	{
		this.m_targetLayers.Remove(targetLayer);
	}

	// Token: 0x060006E3 RID: 1763 RVA: 0x0001C3DA File Offset: 0x0001A5DA
	public void SetTargetPosition(Vector3 position)
	{
		this.GameplayCamera.CameraPositionForSampling = position;
		this.TargetPosition = position;
	}

	// Token: 0x060006E4 RID: 1764 RVA: 0x0001C3F0 File Offset: 0x0001A5F0
	public void UpdateTargetPosition()
	{
		Vector3 targetPosition = this.TargetPosition;
		for (int i = 0; i < this.m_targetLayers.Count; i++)
		{
			CameraTarget.TargetLayer targetLayer = this.m_targetLayers[i];
			if (targetLayer != null && targetLayer.Transform != null)
			{
				if (targetLayer.FollowTargetX)
				{
					targetPosition.x = Mathf.Lerp(targetPosition.x, targetLayer.Transform.position.x, targetLayer.Weight);
				}
				if (targetLayer.FollowTargetY)
				{
					targetPosition.y = Mathf.Lerp(targetPosition.y, targetLayer.Transform.position.y, targetLayer.Weight);
				}
				if (targetLayer.FollowTargetZ)
				{
					targetPosition.z = Mathf.Lerp(targetPosition.z, targetLayer.Transform.position.z, targetLayer.Weight);
				}
			}
		}
		this.GameplayCamera.CameraPositionForSampling = (this.TargetPosition = targetPosition);
	}

	// Token: 0x04000518 RID: 1304
	public GameplayCamera GameplayCamera;

	// Token: 0x04000519 RID: 1305
	private readonly List<CameraTarget.TargetLayer> m_targetLayers = new List<CameraTarget.TargetLayer>
	{
		new CameraTarget.TargetLayer()
	};

	// Token: 0x0400051A RID: 1306
	public Vector3 TargetPosition;

	// Token: 0x020003CC RID: 972
	public class TargetLayer
	{
		// Token: 0x0400174E RID: 5966
		public bool FollowTargetX = true;

		// Token: 0x0400174F RID: 5967
		public bool FollowTargetY = true;

		// Token: 0x04001750 RID: 5968
		public bool FollowTargetZ = true;

		// Token: 0x04001751 RID: 5969
		public Transform Transform;

		// Token: 0x04001752 RID: 5970
		public float Weight = 1f;
	}
}
