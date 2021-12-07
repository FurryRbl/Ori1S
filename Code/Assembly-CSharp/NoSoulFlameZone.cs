using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200046F RID: 1135
[ExecuteInEditMode]
public class NoSoulFlameZone : MonoBehaviour
{
	// Token: 0x06001F4A RID: 8010 RVA: 0x0008A30E File Offset: 0x0008850E
	public void OnEnable()
	{
		NoSoulFlameZone.All.Add(this);
	}

	// Token: 0x06001F4B RID: 8011 RVA: 0x0008A31B File Offset: 0x0008851B
	public void OnDisable()
	{
		NoSoulFlameZone.All.Remove(this);
	}

	// Token: 0x06001F4C RID: 8012 RVA: 0x0008A32C File Offset: 0x0008852C
	public void Awake()
	{
		this.BoundingRect = new Rect
		{
			width = base.transform.lossyScale.x,
			height = base.transform.lossyScale.y,
			center = base.transform.position
		};
	}

	// Token: 0x17000575 RID: 1397
	// (get) Token: 0x06001F4D RID: 8013 RVA: 0x0008A393 File Offset: 0x00088593
	// (set) Token: 0x06001F4E RID: 8014 RVA: 0x0008A39B File Offset: 0x0008859B
	public Rect BoundingRect { get; set; }

	// Token: 0x04001B02 RID: 6914
	public static List<NoSoulFlameZone> All = new List<NoSoulFlameZone>();
}
