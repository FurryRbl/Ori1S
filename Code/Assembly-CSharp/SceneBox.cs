using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000716 RID: 1814
[ExecuteInEditMode]
public class SceneBox : MonoBehaviour
{
	// Token: 0x04002651 RID: 9809
	public Transform ScrollLockTop;

	// Token: 0x04002652 RID: 9810
	public Transform ScrollLockBottom;

	// Token: 0x04002653 RID: 9811
	public Transform ScrollLockLeft;

	// Token: 0x04002654 RID: 9812
	public Transform ScrollLockRight;

	// Token: 0x04002655 RID: 9813
	public List<Transform> MatchSizes = new List<Transform>();

	// Token: 0x04002656 RID: 9814
	private Vector3 m_lastPosition;

	// Token: 0x04002657 RID: 9815
	private Vector3 m_lastScale;
}
