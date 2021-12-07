using System;

// Token: 0x02000492 RID: 1170
[Serializable]
public class OverridableFloatProperty
{
	// Token: 0x06001FBA RID: 8122 RVA: 0x0008B535 File Offset: 0x00089735
	public OverridableFloatProperty()
	{
	}

	// Token: 0x06001FBB RID: 8123 RVA: 0x0008B540 File Offset: 0x00089740
	public OverridableFloatProperty(OverridableFloatProperty floatProperty)
	{
		this.Override = floatProperty.Override;
		this.Name = floatProperty.Name;
		this.Value = floatProperty.Value;
	}

	// Token: 0x06001FBC RID: 8124 RVA: 0x0008B577 File Offset: 0x00089777
	public void Apply(OverridableFloatProperty floatProperty)
	{
		this.Value = floatProperty.Value;
	}

	// Token: 0x04001B56 RID: 6998
	public bool Override;

	// Token: 0x04001B57 RID: 6999
	public string Name;

	// Token: 0x04001B58 RID: 7000
	public float Value;
}
