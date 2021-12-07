using System;
using UnityEngine;

namespace CatlikeCoding.TextBox
{
	// Token: 0x0200076B RID: 1899
	public sealed class TextMeshTorusRenderer : TextMeshRenderer
	{
		// Token: 0x06002C42 RID: 11330 RVA: 0x000BE410 File Offset: 0x000BC610
		public override void Apply()
		{
			if (!this.fetchedSettings)
			{
				this.fetchedSettings = true;
				this.settings = base.transform.parent.GetComponent<TextMeshTorusSettings>();
			}
			if (this.settings)
			{
				this.minorRadius = this.settings.minorRadius;
				this.majorRadius = this.settings.majorRadius;
				this.revolveAxis = this.settings.revolveAxis;
			}
			float num = -this.minorRadius;
			float num2 = (this.majorRadius != 0f) ? (-1f / this.majorRadius) : 0f;
			float num3 = (num != 0f) ? (1f / num) : 0f;
			if (this.revolveAxis == TorusRevolveAxis.X)
			{
				for (int i = 0; i < this.currentVertexIndex; i++)
				{
					Vector3 vector = this.vertices[i];
					float f = vector.y * num2;
					float f2 = vector.x * num3;
					float num4 = num * Mathf.Cos(f2) - this.majorRadius;
					vector.z = num4 * Mathf.Cos(f);
					vector.y = num4 * Mathf.Sin(f);
					vector.x = num * Mathf.Sin(f2);
					this.vertices[i] = vector;
				}
			}
			else
			{
				for (int j = 0; j < this.currentVertexIndex; j++)
				{
					Vector3 vector2 = this.vertices[j];
					float f3 = vector2.x * num2;
					float f4 = vector2.y * num3;
					float num5 = num * Mathf.Cos(f4) - this.majorRadius;
					vector2.z = num5 * Mathf.Cos(f3);
					vector2.x = num5 * Mathf.Sin(f3);
					vector2.y = num * Mathf.Sin(f4);
					this.vertices[j] = vector2;
				}
			}
			base.Apply();
		}

		// Token: 0x04002809 RID: 10249
		public TorusRevolveAxis revolveAxis;

		// Token: 0x0400280A RID: 10250
		public float minorRadius;

		// Token: 0x0400280B RID: 10251
		public float majorRadius;

		// Token: 0x0400280C RID: 10252
		private TextMeshTorusSettings settings;

		// Token: 0x0400280D RID: 10253
		private bool fetchedSettings;
	}
}
