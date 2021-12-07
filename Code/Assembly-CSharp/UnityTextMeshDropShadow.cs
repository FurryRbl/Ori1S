using System;
using UnityEngine;

// Token: 0x020009A8 RID: 2472
public class UnityTextMeshDropShadow : MonoBehaviour
{
	// Token: 0x060035D7 RID: 13783 RVA: 0x000E1FD4 File Offset: 0x000E01D4
	public void Awake()
	{
		this.m_transform = base.transform;
		this.m_textMesh = base.GetComponent<TextMesh>();
		this.m_renderer = base.GetComponent<Renderer>();
		this.m_shadowGameObject = new GameObject("shadow");
		this.m_shadowTextMesh = this.m_shadowGameObject.AddComponent<TextMesh>();
		if (this.m_shadowGameObject.GetComponent<Renderer>() == null)
		{
			this.m_shadowGameObject.AddComponent<MeshRenderer>();
		}
		this.m_shadowMeshRenderer = this.m_shadowGameObject.GetComponent<MeshRenderer>();
		this.m_shadowMeshRenderer.material = this.m_renderer.material;
		this.m_shadowGameObject.layer = base.gameObject.layer;
		this.m_shadowGameObject.transform.parent = this.m_transform;
		this.m_shadowGameObject.transform.position = this.m_transform.position + this.Offset;
		this.m_shadowGameObject.transform.localScale = Vector3.one;
		this.m_shadowGameObject.transform.localRotation = Quaternion.identity;
		this.m_shadowTextMesh.offsetZ = this.m_textMesh.offsetZ;
		this.m_shadowTextMesh.characterSize = this.m_textMesh.characterSize;
		this.m_shadowTextMesh.lineSpacing = this.m_textMesh.lineSpacing;
		this.m_shadowTextMesh.anchor = this.m_textMesh.anchor;
		this.m_shadowTextMesh.alignment = this.m_textMesh.alignment;
		this.m_shadowTextMesh.tabSize = this.m_textMesh.tabSize;
		this.m_shadowTextMesh.fontSize = this.m_textMesh.fontSize;
		this.m_shadowTextMesh.richText = this.m_textMesh.richText;
		this.m_shadowTextMesh.font = this.m_textMesh.font;
	}

	// Token: 0x060035D8 RID: 13784 RVA: 0x000E21B4 File Offset: 0x000E03B4
	public void Update()
	{
		this.m_shadowMeshRenderer.material.color = this.Color * this.m_renderer.material.color;
		this.m_shadowTextMesh.text = this.m_textMesh.text;
	}

	// Token: 0x0400306D RID: 12397
	public Vector3 Offset = new Vector3(0.02f, -0.02f, 0f);

	// Token: 0x0400306E RID: 12398
	public Color Color = Color.black;

	// Token: 0x0400306F RID: 12399
	private GameObject m_shadowGameObject;

	// Token: 0x04003070 RID: 12400
	private TextMesh m_shadowTextMesh;

	// Token: 0x04003071 RID: 12401
	private MeshRenderer m_shadowMeshRenderer;

	// Token: 0x04003072 RID: 12402
	private Transform m_transform;

	// Token: 0x04003073 RID: 12403
	private TextMesh m_textMesh;

	// Token: 0x04003074 RID: 12404
	private Renderer m_renderer;
}
