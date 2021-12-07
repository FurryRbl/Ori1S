using System;

namespace System.Xml.Schema
{
	/// <summary>An abstract class for schema content.</summary>
	// Token: 0x02000208 RID: 520
	public abstract class XmlSchemaContent : XmlSchemaAnnotated
	{
		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x060014EB RID: 5355 RVA: 0x0005D5C0 File Offset: 0x0005B7C0
		internal virtual bool IsExtension
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x0005D5C4 File Offset: 0x0005B7C4
		internal virtual XmlQualifiedName GetBaseTypeName()
		{
			return null;
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x0005D5C8 File Offset: 0x0005B7C8
		internal virtual XmlSchemaParticle GetParticle()
		{
			return null;
		}

		// Token: 0x040007FE RID: 2046
		internal object actualBaseSchemaType;
	}
}
