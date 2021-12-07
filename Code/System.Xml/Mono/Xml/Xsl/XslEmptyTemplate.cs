using System;
using System.Collections;

namespace Mono.Xml.Xsl
{
	// Token: 0x0200009D RID: 157
	internal class XslEmptyTemplate : XslTemplate
	{
		// Token: 0x06000542 RID: 1346 RVA: 0x00021884 File Offset: 0x0001FA84
		private XslEmptyTemplate() : base(null)
		{
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x0002189C File Offset: 0x0001FA9C
		public static XslTemplate Instance
		{
			get
			{
				return XslEmptyTemplate.instance;
			}
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x000218A4 File Offset: 0x0001FAA4
		public override void Evaluate(XslTransformProcessor p, Hashtable withParams)
		{
		}

		// Token: 0x04000375 RID: 885
		private static XslEmptyTemplate instance = new XslEmptyTemplate();
	}
}
