using System;

// Token: 0x020000DC RID: 220
public abstract class ActionMethod : SaveSerialize, IAction
{
	// Token: 0x060008FC RID: 2300 RVA: 0x00026B33 File Offset: 0x00024D33
	public void Start()
	{
	}

	// Token: 0x060008FD RID: 2301 RVA: 0x00026B35 File Offset: 0x00024D35
	public override void Serialize(Archive ar)
	{
	}

	// Token: 0x060008FE RID: 2302 RVA: 0x00026B37 File Offset: 0x00024D37
	public virtual string GetNiceName()
	{
		return StringUtility.AddSpaces(base.GetType().Name);
	}

	// Token: 0x060008FF RID: 2303
	public abstract void Perform(IContext context);
}
