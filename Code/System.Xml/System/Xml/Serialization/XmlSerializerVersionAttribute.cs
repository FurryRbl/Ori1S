﻿using System;

namespace System.Xml.Serialization
{
	/// <summary>Signifies that the code was generated by the serialization infrastructure and can be reused for increased performance, when this attribute is applied to an assembly.</summary>
	// Token: 0x020002B8 RID: 696
	[AttributeUsage(AttributeTargets.Assembly)]
	public sealed class XmlSerializerVersionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializerVersionAttribute" /> class. </summary>
		// Token: 0x06001D47 RID: 7495 RVA: 0x0009B4F0 File Offset: 0x000996F0
		public XmlSerializerVersionAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializerVersionAttribute" /> class for the specified type.</summary>
		/// <param name="type">The type that is being serialized.</param>
		// Token: 0x06001D48 RID: 7496 RVA: 0x0009B4F8 File Offset: 0x000996F8
		public XmlSerializerVersionAttribute(Type type)
		{
			this._type = type;
		}

		/// <summary>Gets or sets the common language runtime (CLR) namespace of the assembly.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the common language runtime (CLR) namespace of the assembly.</returns>
		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06001D49 RID: 7497 RVA: 0x0009B508 File Offset: 0x00099708
		// (set) Token: 0x06001D4A RID: 7498 RVA: 0x0009B510 File Offset: 0x00099710
		public string Namespace
		{
			get
			{
				return this._namespace;
			}
			set
			{
				this._namespace = value;
			}
		}

		/// <summary>Gets or sets the identity of the parent assembly.</summary>
		/// <returns>The version of the parent assembly.</returns>
		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06001D4B RID: 7499 RVA: 0x0009B51C File Offset: 0x0009971C
		// (set) Token: 0x06001D4C RID: 7500 RVA: 0x0009B524 File Offset: 0x00099724
		public string ParentAssemblyId
		{
			get
			{
				return this._parentAssemblyId;
			}
			set
			{
				this._parentAssemblyId = value;
			}
		}

		/// <summary>Gets or sets the type that the serializer operates on.</summary>
		/// <returns>The <see cref="T:System.Type" /> to be serialized.</returns>
		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06001D4D RID: 7501 RVA: 0x0009B530 File Offset: 0x00099730
		// (set) Token: 0x06001D4E RID: 7502 RVA: 0x0009B538 File Offset: 0x00099738
		public Type Type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		/// <summary>Gets or sets the assembly's version number.</summary>
		/// <returns>The version of the assembly.</returns>
		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06001D4F RID: 7503 RVA: 0x0009B544 File Offset: 0x00099744
		// (set) Token: 0x06001D50 RID: 7504 RVA: 0x0009B54C File Offset: 0x0009974C
		public string Version
		{
			get
			{
				return this._version;
			}
			set
			{
				this._version = value;
			}
		}

		// Token: 0x04000B9E RID: 2974
		private string _namespace;

		// Token: 0x04000B9F RID: 2975
		private string _parentAssemblyId;

		// Token: 0x04000BA0 RID: 2976
		private Type _type;

		// Token: 0x04000BA1 RID: 2977
		private string _version;
	}
}