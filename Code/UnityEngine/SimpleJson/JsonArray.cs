using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;

namespace SimpleJson
{
	// Token: 0x0200025C RID: 604
	[GeneratedCode("simple-json", "1.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal class JsonArray : List<object>
	{
		// Token: 0x0600241D RID: 9245 RVA: 0x0002DC84 File Offset: 0x0002BE84
		public JsonArray()
		{
		}

		// Token: 0x0600241E RID: 9246 RVA: 0x0002DC8C File Offset: 0x0002BE8C
		public JsonArray(int capacity) : base(capacity)
		{
		}

		// Token: 0x0600241F RID: 9247 RVA: 0x0002DC98 File Offset: 0x0002BE98
		public override string ToString()
		{
			return SimpleJson.SerializeObject(this) ?? string.Empty;
		}
	}
}
