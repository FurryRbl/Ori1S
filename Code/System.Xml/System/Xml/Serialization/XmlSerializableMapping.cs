using System;
using System.Reflection;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x020002C7 RID: 711
	internal class XmlSerializableMapping : XmlTypeMapping
	{
		// Token: 0x06001DE2 RID: 7650 RVA: 0x0009C5B8 File Offset: 0x0009A7B8
		internal XmlSerializableMapping(string elementName, string ns, TypeData typeData, string xmlType, string xmlTypeNamespace) : base(elementName, ns, typeData, xmlType, xmlTypeNamespace)
		{
			XmlSchemaProviderAttribute xmlSchemaProviderAttribute = (XmlSchemaProviderAttribute)Attribute.GetCustomAttribute(typeData.Type, typeof(XmlSchemaProviderAttribute));
			if (xmlSchemaProviderAttribute != null)
			{
				string methodName = xmlSchemaProviderAttribute.MethodName;
				MethodInfo method = typeData.Type.GetMethod(methodName, BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
				if (method == null)
				{
					throw new InvalidOperationException(string.Format("Type '{0}' must implement public static method '{1}'", typeData.Type, methodName));
				}
				if (!typeof(XmlQualifiedName).IsAssignableFrom(method.ReturnType) && !typeof(XmlSchemaComplexType).IsAssignableFrom(method.ReturnType))
				{
					throw new InvalidOperationException(string.Format("Method '{0}' indicated by XmlSchemaProviderAttribute must have its return type as XmlQualifiedName", methodName));
				}
				XmlSchemaSet xmlSchemaSet = new XmlSchemaSet();
				object obj = method.Invoke(null, new object[]
				{
					xmlSchemaSet
				});
				this._schemaTypeName = XmlQualifiedName.Empty;
				if (obj == null)
				{
					return;
				}
				if (obj is XmlSchemaComplexType)
				{
					this._schemaType = (XmlSchemaComplexType)obj;
					if (!this._schemaType.QualifiedName.IsEmpty)
					{
						this._schemaTypeName = this._schemaType.QualifiedName;
					}
					else
					{
						this._schemaTypeName = new XmlQualifiedName(xmlType, xmlTypeNamespace);
					}
				}
				else
				{
					if (!(obj is XmlQualifiedName))
					{
						throw new InvalidOperationException(string.Format("Method {0}.{1}() specified by XmlSchemaProviderAttribute has invalid signature: return type must be compatible with System.Xml.XmlQualifiedName.", typeData.Type.Name, methodName));
					}
					this._schemaTypeName = (XmlQualifiedName)obj;
				}
				base.UpdateRoot(new XmlQualifiedName(this._schemaTypeName.Name, base.Namespace ?? this._schemaTypeName.Namespace));
				base.XmlTypeNamespace = this._schemaTypeName.Namespace;
				base.XmlType = this._schemaTypeName.Name;
				if (!this._schemaTypeName.IsEmpty && xmlSchemaSet.Count > 0)
				{
					XmlSchema[] array = new XmlSchema[xmlSchemaSet.Count];
					xmlSchemaSet.CopyTo(array, 0);
					this._schema = array[0];
				}
				return;
			}
			else
			{
				IXmlSerializable xmlSerializable = (IXmlSerializable)Activator.CreateInstance(typeData.Type, true);
				try
				{
					this._schema = xmlSerializable.GetSchema();
				}
				catch (Exception)
				{
				}
				if (this._schema != null && (this._schema.Id == null || this._schema.Id.Length == 0))
				{
					throw new InvalidOperationException("Schema Id is missing. The schema returned from " + typeData.Type.FullName + ".GetSchema() must have an Id.");
				}
				return;
			}
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06001DE3 RID: 7651 RVA: 0x0009C850 File Offset: 0x0009AA50
		internal XmlSchema Schema
		{
			get
			{
				return this._schema;
			}
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06001DE4 RID: 7652 RVA: 0x0009C858 File Offset: 0x0009AA58
		internal XmlSchemaType SchemaType
		{
			get
			{
				return this._schemaType;
			}
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06001DE5 RID: 7653 RVA: 0x0009C860 File Offset: 0x0009AA60
		internal XmlQualifiedName SchemaTypeName
		{
			get
			{
				return this._schemaTypeName;
			}
		}

		// Token: 0x04000BD2 RID: 3026
		private XmlSchema _schema;

		// Token: 0x04000BD3 RID: 3027
		private XmlSchemaComplexType _schemaType;

		// Token: 0x04000BD4 RID: 3028
		private XmlQualifiedName _schemaTypeName;
	}
}
