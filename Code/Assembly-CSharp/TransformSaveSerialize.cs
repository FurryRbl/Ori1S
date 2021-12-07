using System;

// Token: 0x02000701 RID: 1793
public class TransformSaveSerialize : SaveSerialize
{
	// Token: 0x06002AA0 RID: 10912 RVA: 0x000B6BEC File Offset: 0x000B4DEC
	public override void Serialize(Archive ar)
	{
		base.transform.localPosition = ar.Serialize(base.transform.localPosition);
		base.transform.localRotation = ar.Serialize(base.transform.localRotation);
		base.transform.localScale = ar.Serialize(base.transform.localScale);
	}
}
