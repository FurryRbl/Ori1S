using System;
using Game;
using UnityEngine;

// Token: 0x02000947 RID: 2375
public class KillCounterDisplay : MonoBehaviour
{
	// Token: 0x06003464 RID: 13412 RVA: 0x000DC1FC File Offset: 0x000DA3FC
	public void Update()
	{
		if (Characters.Sein && base.GetComponent<GUIText>().enabled)
		{
			int count = SeinDeathCounter.Count;
			if (count != this.m_kills)
			{
				this.m_kills = count;
				base.GetComponent<GUIText>().text = count.ToString();
			}
		}
	}

	// Token: 0x04002F45 RID: 12101
	private int m_kills = -1;
}
