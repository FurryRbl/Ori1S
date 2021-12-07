using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

// Token: 0x020005B3 RID: 1459
public class JumpShootSharkZone : MonoBehaviour
{
	// Token: 0x0600252D RID: 9517 RVA: 0x000A23EC File Offset: 0x000A05EC
	public void OnTriggerStay(Collider collider)
	{
		if (!collider.CompareTag("Player"))
		{
			return;
		}
		if (!Characters.Sein.IsOnGround)
		{
			return;
		}
		foreach (JumpShootSharkPlaceholder jumpShootSharkPlaceholder in this.JumpShootSharkPlaceholders)
		{
			if (!(jumpShootSharkPlaceholder == null))
			{
				JumpShootShark jumpShootShark = jumpShootSharkPlaceholder.CurrentEntity as JumpShootShark;
				if (jumpShootShark)
				{
					jumpShootShark.SetEmergeLocation(Characters.Sein.Position);
				}
			}
		}
	}

	// Token: 0x04001FB8 RID: 8120
	public List<JumpShootSharkPlaceholder> JumpShootSharkPlaceholders = new List<JumpShootSharkPlaceholder>();
}
