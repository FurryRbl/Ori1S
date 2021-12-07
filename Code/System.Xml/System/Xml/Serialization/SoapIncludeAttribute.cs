using System;

namespace System.Xml.Serialization
{
	/// <summary>Allows the <see cref="T:System.Xml.Serialization.XmlSerializer" /> to recognize a type when it serializes or deserializes an object as encoded SOAP XML.</summary>
	// Token: 0x02000273 RID: 627
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Interface, AllowMultiple = true)]
	public class SoapIncludeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.SoapIncludeAttribute" /> class using the specified type.</summary>
		/// <param name="type">The type of the object to include. </param>
		// Token: 0x06001950 RID: 6480 RVA: 0x00085168 File Offset: 0x00083368
		public SoapIncludeAttribute(Type type)
		{
			this.type = type;
		}

		/// <summary>Gets or sets the type of the object to use when serializing or deserializing an object.</summary>
		/// <returns>The type of the object to include.</returns>
		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06001951 RID: 6481 RVA: 0x00085178 File Offset: 0x00083378
		// (set) Token: 0x06001952 RID: 6482 RVA: 0x00085180 File Offset: 0x00083380
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

		// Token: 0x04000A84 RID: 2692
		private Type type;
	}
}
