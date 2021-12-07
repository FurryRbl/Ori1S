using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020004BB RID: 1211
	internal class CADObjRef
	{
		// Token: 0x0600310E RID: 12558 RVA: 0x000A1164 File Offset: 0x0009F364
		public CADObjRef(ObjRef o, int sourceDomain)
		{
			this.objref = o;
			this.SourceDomain = sourceDomain;
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x0600310F RID: 12559 RVA: 0x000A117C File Offset: 0x0009F37C
		public string TypeName
		{
			get
			{
				return this.objref.TypeInfo.TypeName;
			}
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06003110 RID: 12560 RVA: 0x000A1190 File Offset: 0x0009F390
		public string URI
		{
			get
			{
				return this.objref.URI;
			}
		}

		// Token: 0x040014C2 RID: 5314
		private ObjRef objref;

		// Token: 0x040014C3 RID: 5315
		public int SourceDomain;
	}
}
