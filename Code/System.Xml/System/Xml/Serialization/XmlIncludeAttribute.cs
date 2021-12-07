using System;

namespace System.Xml.Serialization
{
	/// <summary>Allows the <see cref="T:System.Xml.Serialization.XmlSerializer" /> to recognize a type when it serializes or deserializes an object.</summary>
	// Token: 0x02000291 RID: 657
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Interface, AllowMultiple = true)]
	public class XmlIncludeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlIncludeAttribute" /> class.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the object to include. </param>
		// Token: 0x06001AA6 RID: 6822 RVA: 0x0008AC2C File Offset: 0x00088E2C
		public XmlIncludeAttribute(Type type)
		{
			this.type = type;
		}

		/// <summary>Gets or sets the type of the object to include.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the object to include.</returns>
		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06001AA7 RID: 6823 RVA: 0x0008AC3C File Offset: 0x00088E3C
		// (set) Token: 0x06001AA8 RID: 6824 RVA: 0x0008AC44 File Offset: 0x00088E44
		public Type Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x04000AEA RID: 2794
		private Type type;
	}
}
