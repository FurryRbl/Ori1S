using System;
using UnityEngine;

namespace Moon.EditorTools.SmartSelection
{
	// Token: 0x0200072E RID: 1838
	public class ArtGroupComponent : MonoBehaviour, IStrippable
	{
		// Token: 0x06002B3C RID: 11068 RVA: 0x000B94AC File Offset: 0x000B76AC
		public bool DoStrip()
		{
			return true;
		}

		// Token: 0x040026FF RID: 9983
		public int ArtGroupId;
	}
}
