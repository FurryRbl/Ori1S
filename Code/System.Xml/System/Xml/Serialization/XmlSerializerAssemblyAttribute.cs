using System;

namespace System.Xml.Serialization
{
	/// <summary>Applied to a Web service client proxy, enables you to specify an assembly that contains custom-made serializers. </summary>
	// Token: 0x020002B4 RID: 692
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface)]
	public sealed class XmlSerializerAssemblyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializerAssemblyAttribute" /> class. </summary>
		// Token: 0x06001D26 RID: 7462 RVA: 0x0009B174 File Offset: 0x00099374
		public XmlSerializerAssemblyAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializerAssemblyAttribute" /> class with the specified assembly name.</summary>
		/// <param name="assemblyName">The simple, unencrypted name of the assembly. </param>
		// Token: 0x06001D27 RID: 7463 RVA: 0x0009B17C File Offset: 0x0009937C
		public XmlSerializerAssemblyAttribute(string assemblyName)
		{
			this._assemblyName = assemblyName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializerAssemblyAttribute" /> class with the specified assembly name and location of the assembly.</summary>
		/// <param name="assemblyName">The simple, unencrypted name of the assembly. </param>
		/// <param name="codeBase">A string that is the URL location of the assembly.</param>
		// Token: 0x06001D28 RID: 7464 RVA: 0x0009B18C File Offset: 0x0009938C
		public XmlSerializerAssemblyAttribute(string assemblyName, string codeBase) : this(assemblyName)
		{
			this._codeBase = codeBase;
		}

		/// <summary>Gets or sets the name of the assembly that contains serializers for a specific set of types.</summary>
		/// <returns>The simple, unencrypted name of the assembly. </returns>
		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06001D29 RID: 7465 RVA: 0x0009B19C File Offset: 0x0009939C
		// (set) Token: 0x06001D2A RID: 7466 RVA: 0x0009B1A4 File Offset: 0x000993A4
		public string AssemblyName
		{
			get
			{
				return this._assemblyName;
			}
			set
			{
				this._assemblyName = value;
			}
		}

		/// <summary>Gets or sets the location of the assembly that contains the serializers.</summary>
		/// <returns>A location, such as a path or URI, that points to the assembly.</returns>
		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06001D2B RID: 7467 RVA: 0x0009B1B0 File Offset: 0x000993B0
		// (set) Token: 0x06001D2C RID: 7468 RVA: 0x0009B1B8 File Offset: 0x000993B8
		public string CodeBase
		{
			get
			{
				return this._codeBase;
			}
			set
			{
				this._codeBase = value;
			}
		}

		// Token: 0x04000B9A RID: 2970
		private string _assemblyName;

		// Token: 0x04000B9B RID: 2971
		private string _codeBase;
	}
}
