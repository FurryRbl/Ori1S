using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020009DB RID: 2523
public class MistyWoodsKuroGameplayHideZone : SaveSerialize
{
	// Token: 0x060036EA RID: 14058 RVA: 0x000E681C File Offset: 0x000E4A1C
	public static bool PositionInside(Vector3 position)
	{
		return MistyWoodsKuroGameplayHideZone.All.Any((MistyWoodsKuroGameplayHideZone hideZone) => hideZone.Active && hideZone.Bounds.Contains(position));
	}

	// Token: 0x060036EB RID: 14059 RVA: 0x000E684C File Offset: 0x000E4A4C
	private void Start()
	{
		this.Bounds = new Bounds(base.transform.position, base.transform.localScale);
	}

	// Token: 0x060036EC RID: 14060 RVA: 0x000E687C File Offset: 0x000E4A7C
	public void FixedUpdate()
	{
		this.Bounds = new Bounds(base.transform.position, base.transform.localScale);
	}

	// Token: 0x060036ED RID: 14061 RVA: 0x000E68AA File Offset: 0x000E4AAA
	public void OnEnable()
	{
		MistyWoodsKuroGameplayHideZone.All.Add(this);
	}

	// Token: 0x060036EE RID: 14062 RVA: 0x000E68B7 File Offset: 0x000E4AB7
	public void OnDisable()
	{
		MistyWoodsKuroGameplayHideZone.All.Remove(this);
	}

	// Token: 0x060036EF RID: 14063 RVA: 0x000E68C5 File Offset: 0x000E4AC5
	public void OnDrawGizmos()
	{
		GizmoHelper.DrawSelectedTextFilled(base.transform, "Hide Zone", false);
	}

	// Token: 0x060036F0 RID: 14064 RVA: 0x000E68D8 File Offset: 0x000E4AD8
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.Active);
	}

	// Token: 0x040031D8 RID: 12760
	public Bounds Bounds;

	// Token: 0x040031D9 RID: 12761
	public bool Active = true;

	// Token: 0x040031DA RID: 12762
	public static List<MistyWoodsKuroGameplayHideZone> All = new List<MistyWoodsKuroGameplayHideZone>();
}
