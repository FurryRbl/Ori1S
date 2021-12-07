using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020000F3 RID: 243
public abstract class MessageProvider : ScriptableObject
{
	// Token: 0x060009B7 RID: 2487 RVA: 0x0002AB88 File Offset: 0x00028D88
	public override string ToString()
	{
		return this.GetMessages().First<MessageDescriptor>().Message;
	}

	// Token: 0x060009B8 RID: 2488
	public abstract IEnumerable<MessageDescriptor> GetMessages();
}
