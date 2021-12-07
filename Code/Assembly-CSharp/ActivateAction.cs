using System;
using UnityEngine;

// Token: 0x020002B2 RID: 690
[Category("General")]
public class ActivateAction : ActionMethod
{
	// Token: 0x060015C1 RID: 5569 RVA: 0x000602E8 File Offset: 0x0005E4E8
	public void OnValidate()
	{
		if (this.Save && this.Target && this.Target.GetComponent<GameObjectActivator>())
		{
			this.Save = false;
		}
	}

	// Token: 0x060015C2 RID: 5570 RVA: 0x00060321 File Offset: 0x0005E521
	public override void Perform(IContext context)
	{
		this.Target.SetActive(this.Activate);
	}

	// Token: 0x060015C3 RID: 5571 RVA: 0x00060334 File Offset: 0x0005E534
	public override void Serialize(Archive ar)
	{
		if (this.Save)
		{
			if (ar.Reading)
			{
				bool active = ar.Serialize(true);
				if (this.Target)
				{
					this.Target.SetActive(active);
				}
			}
			if (ar.Writing)
			{
				if (this.Target == null)
				{
					ar.Serialize(false);
				}
				else
				{
					ar.Serialize(this.Target.activeSelf);
				}
			}
		}
	}

	// Token: 0x170003DC RID: 988
	// (get) Token: 0x060015C4 RID: 5572 RVA: 0x000603B8 File Offset: 0x0005E5B8
	private string TargetName
	{
		get
		{
			return (!(this.Target != null)) ? "unkown" : this.Target.name;
		}
	}

	// Token: 0x060015C5 RID: 5573 RVA: 0x000603EB File Offset: 0x0005E5EB
	public override string GetNiceName()
	{
		return ((!this.Activate) ? "Deactivate " : "Activate ") + this.TargetName;
	}

	// Token: 0x040012AC RID: 4780
	[NotNull]
	public GameObject Target;

	// Token: 0x040012AD RID: 4781
	public bool Activate = true;

	// Token: 0x040012AE RID: 4782
	public bool Save = true;
}
