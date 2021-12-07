using System;
using System.Reflection;
using UnityEngine;

// Token: 0x02000628 RID: 1576
public class ValueInComponentProvider : FloatValueProvider
{
	// Token: 0x060026D9 RID: 9945 RVA: 0x000A9C79 File Offset: 0x000A7E79
	public void Awake()
	{
		this.m_valuefieldInfo = this.Component.GetType().GetField(this.ValueVariableName);
	}

	// Token: 0x060026DA RID: 9946 RVA: 0x000A9C97 File Offset: 0x000A7E97
	public override float GetFloatValue()
	{
		return (float)this.m_valuefieldInfo.GetValue(this.Component);
	}

	// Token: 0x04002170 RID: 8560
	public Component Component;

	// Token: 0x04002171 RID: 8561
	public string ValueVariableName;

	// Token: 0x04002172 RID: 8562
	private FieldInfo m_valuefieldInfo;
}
