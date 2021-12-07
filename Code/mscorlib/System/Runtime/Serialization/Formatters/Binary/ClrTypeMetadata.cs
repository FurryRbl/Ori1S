using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000527 RID: 1319
	internal abstract class ClrTypeMetadata : TypeMetadata
	{
		// Token: 0x06003418 RID: 13336 RVA: 0x000A98E4 File Offset: 0x000A7AE4
		public ClrTypeMetadata(Type instanceType)
		{
			this.InstanceType = instanceType;
			this.InstanceTypeName = instanceType.FullName;
			this.TypeAssemblyName = instanceType.Assembly.FullName;
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06003419 RID: 13337 RVA: 0x000A991C File Offset: 0x000A7B1C
		public override bool RequiresTypes
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040015F6 RID: 5622
		public Type InstanceType;
	}
}
