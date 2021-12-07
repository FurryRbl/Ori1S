using System;
using System.Collections.Generic;

namespace Mono.Xml
{
	// Token: 0x020000BF RID: 191
	internal class DictionaryBase : List<KeyValuePair<string, DTDNode>>
	{
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x00025C80 File Offset: 0x00023E80
		public IEnumerable<DTDNode> Values
		{
			get
			{
				foreach (KeyValuePair<string, DTDNode> p in this)
				{
					yield return p.Value;
				}
				yield break;
			}
		}
	}
}
