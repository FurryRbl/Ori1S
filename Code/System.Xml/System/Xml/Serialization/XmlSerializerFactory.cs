using System;
using System.Collections;
using System.Security.Policy;

namespace System.Xml.Serialization
{
	/// <summary>Creates typed versions of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> for more efficient serialization.</summary>
	// Token: 0x020002B5 RID: 693
	public class XmlSerializerFactory
	{
		/// <summary>Returns a derivation of the <see cref="T:System.Xml.Serialization.XmlSerializerFactory" /> class that is used to serialize the specified type.</summary>
		/// <returns>A derivation of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class that is specifically created to serialize the specified type.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> to serialize.</param>
		// Token: 0x06001D2F RID: 7471 RVA: 0x0009B1D8 File Offset: 0x000993D8
		public XmlSerializer CreateSerializer(Type type)
		{
			return this.CreateSerializer(type, null, null, null, null);
		}

		/// <summary>Returns a derivation of the <see cref="T:System.Xml.Serialization.XmlSerializerFactory" /> class using an object that maps one type to another.</summary>
		/// <returns>A derivation of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class that is specifically created to serialize the mapped type.</returns>
		/// <param name="xmlTypeMapping">An <see cref="T:System.Xml.Serialization.XmlTypeMapping" /> that maps one type to another.</param>
		// Token: 0x06001D30 RID: 7472 RVA: 0x0009B1E8 File Offset: 0x000993E8
		public XmlSerializer CreateSerializer(XmlTypeMapping xmlTypeMapping)
		{
			Hashtable obj = XmlSerializerFactory.serializersBySource;
			XmlSerializer result;
			lock (obj)
			{
				XmlSerializer xmlSerializer = (XmlSerializer)XmlSerializerFactory.serializersBySource[xmlTypeMapping.Source];
				if (xmlSerializer == null)
				{
					xmlSerializer = new XmlSerializer(xmlTypeMapping);
					XmlSerializerFactory.serializersBySource[xmlTypeMapping.Source] = xmlSerializer;
				}
				result = xmlSerializer;
			}
			return result;
		}

		/// <summary>Returns a derivation of the <see cref="T:System.Xml.Serialization.XmlSerializerFactory" /> class that is used to serialize the specified type and namespace.</summary>
		/// <returns>A derivation of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class that is specifically created to serialize the specified type.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> to serialize.</param>
		/// <param name="defaultNamespace">The default namespace to use for all the XML elements. </param>
		// Token: 0x06001D31 RID: 7473 RVA: 0x0009B268 File Offset: 0x00099468
		public XmlSerializer CreateSerializer(Type type, string defaultNamespace)
		{
			return this.CreateSerializer(type, null, null, null, defaultNamespace);
		}

		/// <summary>Returns a derivation of the <see cref="T:System.Xml.Serialization.XmlSerializerFactory" /> class that is used to serialize the specified type. If a property or field returns an array, the <paramref name="extraTypes" /> parameter specifies objects that can be inserted into the array.</summary>
		/// <returns>A derivation of the <see cref="T:System.Xml.Serialization.XmlSerializer" />.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> to serialize.</param>
		/// <param name="extraTypes">A <see cref="T:System.Type" /> array of additional object types to serialize.</param>
		// Token: 0x06001D32 RID: 7474 RVA: 0x0009B278 File Offset: 0x00099478
		public XmlSerializer CreateSerializer(Type type, Type[] extraTypes)
		{
			return this.CreateSerializer(type, null, extraTypes, null, null);
		}

		/// <summary>Returns a derivation of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class that can serialize objects of the specified type into XML documents, and vice versa. Each object to be serialized can itself contain instances of classes, which this overload can override with other classes.</summary>
		/// <returns>A derivation of the <see cref="T:System.Xml.Serialization.XmlSerializer" />.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> to serialize.</param>
		/// <param name="overrides">An <see cref="T:System.Xml.Serialization.XmlAttributeOverrides" /> that contains fields that override the default serialization behavior.</param>
		// Token: 0x06001D33 RID: 7475 RVA: 0x0009B288 File Offset: 0x00099488
		public XmlSerializer CreateSerializer(Type type, XmlAttributeOverrides overrides)
		{
			return this.CreateSerializer(type, overrides, null, null, null);
		}

		/// <summary>Returns a derivation of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class that can serialize objects of the specified type into XML documents, and vice versa. Specifies the object that represents the XML root element.</summary>
		/// <returns>A derivation of the <see cref="T:System.Xml.Serialization.XmlSerializer" />.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> to serialize.</param>
		/// <param name="root">An <see cref="T:System.Xml.Serialization.XmlRootAttribute" /> that represents the XML root element.</param>
		// Token: 0x06001D34 RID: 7476 RVA: 0x0009B298 File Offset: 0x00099498
		public XmlSerializer CreateSerializer(Type type, XmlRootAttribute root)
		{
			return this.CreateSerializer(type, null, null, root, null);
		}

		/// <summary>Returns a derivation of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class that can serialize objects of the specified type into XML document instances, and vice versa. Each object to be serialized can itself contain instances of classes, which this overload can override with other classes. This overload also specifies the default namespace for all the XML elements, and the class to use as the XML root element.</summary>
		/// <returns>A derivation of the <see cref="T:System.Xml.Serialization.XmlSerializer" />.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> to serialize.</param>
		/// <param name="overrides">An <see cref="T:System.Xml.Serialization.XmlAttributeOverrides" /> that contains fields that override the default serialization behavior.</param>
		/// <param name="extraTypes">A <see cref="T:System.Type" /> array of additional object types to serialize.</param>
		/// <param name="root">An <see cref="T:System.Xml.Serialization.XmlRootAttribute" /> that represents the XML root element.</param>
		/// <param name="defaultNamespace">The default namespace of all XML elements in the XML document. </param>
		// Token: 0x06001D35 RID: 7477 RVA: 0x0009B2A8 File Offset: 0x000994A8
		public XmlSerializer CreateSerializer(Type type, XmlAttributeOverrides overrides, Type[] extraTypes, XmlRootAttribute root, string defaultNamespace)
		{
			XmlTypeSerializationSource key = new XmlTypeSerializationSource(type, root, overrides, defaultNamespace, extraTypes);
			Hashtable obj = XmlSerializerFactory.serializersBySource;
			XmlSerializer result;
			lock (obj)
			{
				XmlSerializer xmlSerializer = (XmlSerializer)XmlSerializerFactory.serializersBySource[key];
				if (xmlSerializer == null)
				{
					xmlSerializer = new XmlSerializer(type, overrides, extraTypes, root, defaultNamespace);
					XmlSerializerFactory.serializersBySource[xmlSerializer.Mapping.Source] = xmlSerializer;
				}
				result = xmlSerializer;
			}
			return result;
		}

		/// <summary>Returns a derivation of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class that can serialize objects of the specified type into XML document instances, and vice versa. Each object to be serialized can itself contain instances of classes, which this overload can override with other classes. This overload also specifies the default namespace for all the XML elements, and the class to use as the XML root element.</summary>
		/// <returns>A derivation of the <see cref="T:System.Xml.Serialization.XmlSerializer" />.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> of the object that this <see cref="T:System.Xml.Serialization.XmlSerializer" /> can serialize.</param>
		/// <param name="overrides">An <see cref="T:System.Xml.Serialization.XmlAttributeOverrides" /> that extends or overrides the behavior of the class specified in the type parameter.</param>
		/// <param name="extraTypes">A <see cref="T:System.Type" /> array of additional object types to serialize.</param>
		/// <param name="root">An <see cref="T:System.Xml.Serialization.XmlRootAttribute" /> that defines the XML root element properties.</param>
		/// <param name="defaultNamespace">The default namespace of all XML elements in the XML document.</param>
		/// <param name="location">The path that specifies the location of the types.</param>
		/// <param name="evidence">An instance of the <see cref="T:System.Security.Policy.Evidence" /> class that contains credentials needed to access types.</param>
		// Token: 0x06001D36 RID: 7478 RVA: 0x0009B338 File Offset: 0x00099538
		[MonoTODO]
		public XmlSerializer CreateSerializer(Type type, XmlAttributeOverrides overrides, Type[] extraTypes, XmlRootAttribute root, string defaultNamespace, string location, Evidence evidence)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000B9C RID: 2972
		private static Hashtable serializersBySource = new Hashtable();
	}
}
