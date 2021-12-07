using System;
using UnityEngine;

namespace Game
{
	// Token: 0x0200052F RID: 1327
	public static class Orbs
	{
		// Token: 0x02000530 RID: 1328
		public static class OrbDisplayText
		{
			// Token: 0x170005F8 RID: 1528
			// (get) Token: 0x0600231D RID: 8989 RVA: 0x00099ED5 File Offset: 0x000980D5
			public static GameObject OrbText
			{
				get
				{
					Orbs.OrbDisplayText.LoadOrbText();
					return Orbs.OrbDisplayText.m_orbText;
				}
			}

			// Token: 0x0600231E RID: 8990 RVA: 0x00099EE4 File Offset: 0x000980E4
			public static void LoadOrbText()
			{
				if (Orbs.OrbDisplayText.m_orbText == null)
				{
					Orbs.OrbDisplayText.m_orbText = (GameObject)Resources.Load("attacking/orbText", typeof(GameObject));
				}
			}

			// Token: 0x0600231F RID: 8991 RVA: 0x00099F20 File Offset: 0x00098120
			public static ExpText Create(Transform target, Vector3 offset, int value)
			{
				if (!GameSettings.Instance.DamageTextEnabled)
				{
					return null;
				}
				GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(Orbs.OrbDisplayText.OrbText, offset, Quaternion.identity);
				gameObject.transform.parent = target;
				gameObject.transform.position += target.position;
				ExpText component = gameObject.GetComponent<ExpText>();
				component.Amount = value;
				return component;
			}

			// Token: 0x04001D9A RID: 7578
			private static GameObject m_orbText;
		}
	}
}
