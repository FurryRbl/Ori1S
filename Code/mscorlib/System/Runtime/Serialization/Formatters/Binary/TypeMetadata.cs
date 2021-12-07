using System;
using System.IO;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000526 RID: 1318
	internal abstract class TypeMetadata
	{
		// Token: 0x06003413 RID: 13331
		public abstract void WriteAssemblies(ObjectWriter ow, BinaryWriter writer);

		// Token: 0x06003414 RID: 13332
		public abstract void WriteTypeData(ObjectWriter ow, BinaryWriter writer, bool writeTypes);

		// Token: 0x06003415 RID: 13333
		public abstract void WriteObjectData(ObjectWriter ow, BinaryWriter writer, object data);

		// Token: 0x06003416 RID: 13334 RVA: 0x000A98E0 File Offset: 0x000A7AE0
		public virtual bool IsCompatible(TypeMetadata other)
		{
			return true;
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06003417 RID: 13335
		public abstract bool RequiresTypes { get; }

		// Token: 0x040015F4 RID: 5620
		public string TypeAssemblyName;

		// Token: 0x040015F5 RID: 5621
		public string InstanceTypeName;
	}
}
