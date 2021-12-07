using System;
using UnityEngine;

// Token: 0x02000692 RID: 1682
public class MoonDebugMessage
{
	// Token: 0x060028B2 RID: 10418 RVA: 0x000B0D74 File Offset: 0x000AEF74
	public MoonDebugMessage(MoonDebugMessageType typ, string text, UnityEngine.Object obj)
	{
		this.m_type = typ;
		this.m_text = text;
		this.m_object = obj;
	}

	// Token: 0x060028B3 RID: 10419 RVA: 0x000B0DA8 File Offset: 0x000AEFA8
	public override string ToString()
	{
		string text = string.Empty;
		if (this.m_object != null)
		{
			text = this.m_object.name;
		}
		return string.Concat(new object[]
		{
			this.m_type,
			", ",
			text,
			", ",
			this.m_text
		});
	}

	// Token: 0x0400245D RID: 9309
	private MoonDebugMessageType m_type;

	// Token: 0x0400245E RID: 9310
	private string m_text = string.Empty;

	// Token: 0x0400245F RID: 9311
	private UnityEngine.Object m_object;
}
