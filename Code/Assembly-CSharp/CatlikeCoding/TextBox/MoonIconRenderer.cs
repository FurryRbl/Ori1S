using System;
using System.Collections.Generic;
using UnityEngine;

namespace CatlikeCoding.TextBox
{
	// Token: 0x02000764 RID: 1892
	public class MoonIconRenderer : TextRenderer
	{
		// Token: 0x06002C2F RID: 11311 RVA: 0x000BD6E0 File Offset: 0x000BB8E0
		public override void Prepare()
		{
			foreach (KeyValuePair<GameObject, List<GameObject>> keyValuePair in this.m_data)
			{
				foreach (GameObject gameObject in keyValuePair.Value)
				{
					if (gameObject)
					{
						gameObject.SetActive(false);
					}
				}
			}
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x000BD788 File Offset: 0x000BB988
		public override void Add(CharMetaData meta, Vector2 offset)
		{
			TextBoxIconsFontGenerator.IconData iconData = this.Icons.FindIcon((int)meta.id);
			if (iconData != null)
			{
				GameObject icon = iconData.Icon;
				if (icon)
				{
					List<GameObject> list = null;
					if (this.m_data.TryGetValue(icon, out list))
					{
						foreach (GameObject gameObject in list)
						{
							if (gameObject && !gameObject.activeSelf)
							{
								gameObject.SetActive(true);
								gameObject.transform.localScale = icon.transform.localScale * meta.scale;
								this.SetIconPosition(gameObject, meta, offset);
								return;
							}
						}
					}
					if (list == null)
					{
						list = new List<GameObject>();
						this.m_data.Add(icon, list);
					}
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(icon);
					gameObject2.transform.parent = base.transform;
					gameObject2.transform.localScale = icon.transform.localScale * meta.scale;
					gameObject2.transform.localPosition = icon.transform.localPosition;
					gameObject2.transform.localRotation = icon.transform.localRotation;
					this.SetIconPosition(gameObject2, meta, offset);
					list.Add(gameObject2);
					TransparencyAnimator.Register(gameObject2.transform);
				}
			}
		}

		// Token: 0x06002C31 RID: 11313 RVA: 0x000BD908 File Offset: 0x000BBB08
		private void SetIconPosition(GameObject icon, CharMetaData meta, Vector2 offset)
		{
			icon.transform.localPosition = meta.positionInBox + offset + this.IconOffset;
		}

		// Token: 0x06002C32 RID: 11314 RVA: 0x000BD934 File Offset: 0x000BBB34
		public override void Apply()
		{
			foreach (List<GameObject> list in this.m_data.Values)
			{
				foreach (GameObject gameObject in list)
				{
					if (gameObject && !gameObject.activeSelf)
					{
						if (Application.isPlaying)
						{
							InstantiateUtility.Destroy(gameObject);
						}
						else
						{
							UnityEngine.Object.DestroyImmediate(gameObject);
						}
					}
				}
				list.RemoveAll((GameObject a) => a == null);
			}
		}

		// Token: 0x040027ED RID: 10221
		public TextBoxIconsFontGenerator Icons;

		// Token: 0x040027EE RID: 10222
		public Vector2 IconOffset;

		// Token: 0x040027EF RID: 10223
		private readonly Dictionary<GameObject, List<GameObject>> m_data = new Dictionary<GameObject, List<GameObject>>();
	}
}
