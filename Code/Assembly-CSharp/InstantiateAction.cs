using System;
using Game;
using UnityEngine;

// Token: 0x020002EF RID: 751
[Category("General")]
public class InstantiateAction : ActionMethod
{
	// Token: 0x060016AE RID: 5806 RVA: 0x000632FC File Offset: 0x000614FC
	public override void Perform(IContext context)
	{
		GameObject gameObject = null;
		if (this.Position)
		{
			if (this.SpawnWhenNotOnScreen || UI.Cameras.Current.IsOnScreenPadded(this.Position.position, 5f))
			{
				gameObject = (GameObject)InstantiateUtility.Instantiate(this.Prefab, this.Position.position, this.Position.rotation);
			}
		}
		else
		{
			gameObject = (GameObject)InstantiateUtility.Instantiate(this.Prefab);
		}
		if (this.ShouldSetParent)
		{
			gameObject.transform.parent = this.Parent;
		}
	}

	// Token: 0x060016AF RID: 5807 RVA: 0x000633A0 File Offset: 0x000615A0
	public override string GetNiceName()
	{
		if (this.Position)
		{
			return "Instantiate " + ActionHelper.GetName(this.Prefab) + " at " + ActionHelper.GetName(this.Position);
		}
		return "Instantiate " + ActionHelper.GetName(this.Prefab);
	}

	// Token: 0x0400138C RID: 5004
	public Transform Position;

	// Token: 0x0400138D RID: 5005
	[NotNull]
	public GameObject Prefab;

	// Token: 0x0400138E RID: 5006
	public bool ShouldSetParent;

	// Token: 0x0400138F RID: 5007
	public Transform Parent;

	// Token: 0x04001390 RID: 5008
	public bool SpawnWhenNotOnScreen = true;
}
