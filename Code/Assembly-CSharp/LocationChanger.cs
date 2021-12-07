using System;
using UnityEngine;

// Token: 0x020008D2 RID: 2258
[ExecuteInEditMode]
public class LocationChanger : MonoBehaviour, ILocationInformation
{
	// Token: 0x06003247 RID: 12871 RVA: 0x000D4E5C File Offset: 0x000D305C
	public void Start()
	{
		if (this.Scene == string.Empty)
		{
			GameObject gameObject = GameObject.Find(this.Target);
			if (gameObject != null)
			{
				this.m_target = gameObject.transform;
			}
		}
	}

	// Token: 0x170007FC RID: 2044
	// (get) Token: 0x06003248 RID: 12872 RVA: 0x000D4EA2 File Offset: 0x000D30A2
	public string SceneName
	{
		get
		{
			return this.Scene;
		}
	}

	// Token: 0x170007FD RID: 2045
	// (get) Token: 0x06003249 RID: 12873 RVA: 0x000D4EAA File Offset: 0x000D30AA
	public string TargetName
	{
		get
		{
			return this.Target;
		}
	}

	// Token: 0x170007FE RID: 2046
	// (get) Token: 0x0600324A RID: 12874 RVA: 0x000D4EB2 File Offset: 0x000D30B2
	public Transform TargetTransform
	{
		get
		{
			return this.m_target;
		}
	}

	// Token: 0x0600324B RID: 12875 RVA: 0x000D4EBC File Offset: 0x000D30BC
	public Vector3 TargetOffset(Transform other)
	{
		Vector3 vector = this.Offset;
		if (this.TargetName != string.Empty && this.MaintainOffsetOfPlayer)
		{
			vector += other.position - base.transform.position;
		}
		return vector;
	}

	// Token: 0x170007FF RID: 2047
	// (get) Token: 0x0600324C RID: 12876 RVA: 0x000D4F0E File Offset: 0x000D310E
	public bool UseFader
	{
		get
		{
			return this.UseFade;
		}
	}

	// Token: 0x04002D47 RID: 11591
	public string Target;

	// Token: 0x04002D48 RID: 11592
	public string Scene;

	// Token: 0x04002D49 RID: 11593
	public bool UseFade;

	// Token: 0x04002D4A RID: 11594
	public bool UsePostionZ;

	// Token: 0x04002D4B RID: 11595
	public bool MaintainOffsetOfPlayer;

	// Token: 0x04002D4C RID: 11596
	private Transform m_target;

	// Token: 0x04002D4D RID: 11597
	public Vector3 Offset;
}
