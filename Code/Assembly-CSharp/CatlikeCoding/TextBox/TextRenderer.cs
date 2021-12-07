using System;
using UnityEngine;

namespace CatlikeCoding.TextBox
{
	// Token: 0x02000680 RID: 1664
	public abstract class TextRenderer : MonoBehaviour
	{
		// Token: 0x06002873 RID: 10355 RVA: 0x000AF29F File Offset: 0x000AD49F
		public virtual void Prepare()
		{
		}

		// Token: 0x06002874 RID: 10356 RVA: 0x000AF2A1 File Offset: 0x000AD4A1
		public virtual void Add(CharMetaData meta, Vector2 offset)
		{
		}

		// Token: 0x06002875 RID: 10357 RVA: 0x000AF2A3 File Offset: 0x000AD4A3
		public virtual void Apply()
		{
		}

		// Token: 0x040023F2 RID: 9202
		public int renderedCharCount;
	}
}
