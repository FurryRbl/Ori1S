using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading;
using Microsoft.CSharp;

namespace System.Xml.Serialization
{
	/// <summary>Serializes and deserializes objects into and from XML documents. The <see cref="T:System.Xml.Serialization.XmlSerializer" /> enables you to control how objects are encoded into XML.</summary>
	// Token: 0x020002B1 RID: 689
	public class XmlSerializer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class.</summary>
		// Token: 0x06001CE8 RID: 7400 RVA: 0x00099F30 File Offset: 0x00098130
		protected XmlSerializer()
		{
			this.customSerializer = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class that can serialize objects of the specified type into XML documents, and deserialize XML documents into objects of the specified type.</summary>
		/// <param name="type">The type of the object that this <see cref="T:System.Xml.Serialization.XmlSerializer" /> can serialize. </param>
		// Token: 0x06001CE9 RID: 7401 RVA: 0x00099F40 File Offset: 0x00098140
		public XmlSerializer(Type type) : this(type, null, null, null, null)
		{
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class using an object that maps one type to another.</summary>
		/// <param name="xmlTypeMapping">An <see cref="T:System.Xml.Serialization.XmlTypeMapping" /> that maps one type to another. </param>
		// Token: 0x06001CEA RID: 7402 RVA: 0x00099F50 File Offset: 0x00098150
		public XmlSerializer(XmlTypeMapping xmlTypeMapping)
		{
			this.typeMapping = xmlTypeMapping;
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x00099F60 File Offset: 0x00098160
		internal XmlSerializer(XmlMapping mapping, XmlSerializer.SerializerData data)
		{
			this.typeMapping = mapping;
			this.serializerData = data;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class that can serialize objects of the specified type into XML documents, and deserialize XML documents into objects of the specified type. Specifies the default namespace for all the XML elements.</summary>
		/// <param name="type">The type of the object that this <see cref="T:System.Xml.Serialization.XmlSerializer" /> can serialize. </param>
		/// <param name="defaultNamespace">The default namespace to use for all the XML elements. </param>
		// Token: 0x06001CEC RID: 7404 RVA: 0x00099F78 File Offset: 0x00098178
		public XmlSerializer(Type type, string defaultNamespace) : this(type, null, null, null, defaultNamespace)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class that can serialize objects of the specified type into XML documents, and deserialize XML documents into object of a specified type. If a property or field returns an array, the <paramref name="extraTypes" /> parameter specifies objects that can be inserted into the array.</summary>
		/// <param name="type">The type of the object that this <see cref="T:System.Xml.Serialization.XmlSerializer" /> can serialize. </param>
		/// <param name="extraTypes">A <see cref="T:System.Type" /> array of additional object types to serialize. </param>
		// Token: 0x06001CED RID: 7405 RVA: 0x00099F88 File Offset: 0x00098188
		public XmlSerializer(Type type, Type[] extraTypes) : this(type, null, extraTypes, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class that can serialize objects of the specified type into XML documents, and deserialize XML documents into objects of the specified type. Each object to be serialized can itself contain instances of classes, which this overload can override with other classes.</summary>
		/// <param name="type">The type of the object to serialize. </param>
		/// <param name="overrides">An <see cref="T:System.Xml.Serialization.XmlAttributeOverrides" />. </param>
		// Token: 0x06001CEE RID: 7406 RVA: 0x00099F98 File Offset: 0x00098198
		public XmlSerializer(Type type, XmlAttributeOverrides overrides) : this(type, overrides, null, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class that can serialize objects of the specified type into XML documents, and deserialize an XML document into object of the specified type. It also specifies the class to use as the XML root element.</summary>
		/// <param name="type">The type of the object that this <see cref="T:System.Xml.Serialization.XmlSerializer" /> can serialize. </param>
		/// <param name="root">An <see cref="T:System.Xml.Serialization.XmlRootAttribute" /> that represents the XML root element. </param>
		// Token: 0x06001CEF RID: 7407 RVA: 0x00099FA8 File Offset: 0x000981A8
		public XmlSerializer(Type type, XmlRootAttribute root) : this(type, null, null, root, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class that can serialize objects of type <see cref="T:System.Object" /> into XML document instances, and deserialize XML document instances into objects of type <see cref="T:System.Object" />. Each object to be serialized can itself contain instances of classes, which this overload overrides with other classes. This overload also specifies the default namespace for all the XML elements and the class to use as the XML root element.</summary>
		/// <param name="type">The type of the object that this <see cref="T:System.Xml.Serialization.XmlSerializer" /> can serialize. </param>
		/// <param name="overrides">An <see cref="T:System.Xml.Serialization.XmlAttributeOverrides" /> that extends or overrides the behavior of the class specified in the <paramref name="type" /> parameter. </param>
		/// <param name="extraTypes">A <see cref="T:System.Type" /> array of additional object types to serialize. </param>
		/// <param name="root">An <see cref="T:System.Xml.Serialization.XmlRootAttribute" /> that defines the XML root element properties. </param>
		/// <param name="defaultNamespace">The default namespace of all XML elements in the XML document. </param>
		// Token: 0x06001CF0 RID: 7408 RVA: 0x00099FB8 File Offset: 0x000981B8
		public XmlSerializer(Type type, XmlAttributeOverrides overrides, Type[] extraTypes, XmlRootAttribute root, string defaultNamespace)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			XmlReflectionImporter xmlReflectionImporter = new XmlReflectionImporter(overrides, defaultNamespace);
			if (extraTypes != null)
			{
				foreach (Type type2 in extraTypes)
				{
					xmlReflectionImporter.IncludeType(type2);
				}
			}
			this.typeMapping = xmlReflectionImporter.ImportTypeMapping(type, root, defaultNamespace);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class that can serialize objects of the specified type into XML document instances, and deserialize XML document instances into objects of the specified type. This overload allows you to supply other types that can be encountered during a serialization or deserialization operation, as well as a default namespace for all XML elements, the class to use as the XML root element, its location, and credentials required for access.</summary>
		/// <param name="type">The type of the object that this <see cref="T:System.Xml.Serialization.XmlSerializer" /> can serialize.</param>
		/// <param name="overrides">An <see cref="T:System.Xml.Serialization.XmlAttributeOverrides" /> that extends or overrides the behavior of the class specified in the <paramref name="type" /> parameter.</param>
		/// <param name="extraTypes">A <see cref="T:System.Type" /> array of additional object types to serialize.</param>
		/// <param name="root">An <see cref="T:System.Xml.Serialization.XmlRootAttribute" /> that defines the XML root element properties.</param>
		/// <param name="defaultNamespace">The default namespace of all XML elements in the XML document.</param>
		/// <param name="location">The location of the types.</param>
		/// <param name="evidence">An instance of the <see cref="T:System.Security.Policy.Evidence" /> class that contains credentials required to access types.</param>
		// Token: 0x06001CF1 RID: 7409 RVA: 0x0009A020 File Offset: 0x00098220
		[MonoTODO]
		public XmlSerializer(Type type, XmlAttributeOverrides overrides, Type[] extraTypes, XmlRootAttribute root, string defaultNamespace, string location, Evidence evidence)
		{
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x0009A028 File Offset: 0x00098228
		static XmlSerializer()
		{
			string environmentVariable = Environment.GetEnvironmentVariable("MONO_XMLSERIALIZER_DEBUG");
			string text = Environment.GetEnvironmentVariable("MONO_XMLSERIALIZER_THS");
			if (text == null)
			{
				XmlSerializer.generationThreshold = 50;
				XmlSerializer.backgroundGeneration = true;
			}
			else
			{
				int num = text.IndexOf(',');
				if (num != -1)
				{
					if (text.Substring(num + 1) == "nofallback")
					{
						XmlSerializer.generatorFallback = false;
					}
					text = text.Substring(0, num);
				}
				if (text.ToLower(CultureInfo.InvariantCulture) == "no")
				{
					XmlSerializer.generationThreshold = -1;
				}
				else
				{
					XmlSerializer.generationThreshold = int.Parse(text, CultureInfo.InvariantCulture);
					XmlSerializer.backgroundGeneration = (XmlSerializer.generationThreshold != 0);
					if (XmlSerializer.generationThreshold < 1)
					{
						XmlSerializer.generationThreshold = 1;
					}
				}
			}
			XmlSerializer.deleteTempFiles = (environmentVariable == null || environmentVariable == "no");
			IDictionary dictionary = (IDictionary)ConfigurationSettings.GetConfig("system.diagnostics");
			if (dictionary != null)
			{
				dictionary = (IDictionary)dictionary["switches"];
				if (dictionary != null)
				{
					string a = (string)dictionary["XmlSerialization.Compilation"];
					if (a == "1")
					{
						XmlSerializer.deleteTempFiles = false;
					}
				}
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Xml.Serialization.XmlSerializer" /> encounters an XML attribute of unknown type during deserialization.</summary>
		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06001CF3 RID: 7411 RVA: 0x0009A17C File Offset: 0x0009837C
		// (remove) Token: 0x06001CF4 RID: 7412 RVA: 0x0009A198 File Offset: 0x00098398
		public event XmlAttributeEventHandler UnknownAttribute
		{
			add
			{
				this.onUnknownAttribute = (XmlAttributeEventHandler)Delegate.Combine(this.onUnknownAttribute, value);
			}
			remove
			{
				this.onUnknownAttribute = (XmlAttributeEventHandler)Delegate.Remove(this.onUnknownAttribute, value);
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Xml.Serialization.XmlSerializer" /> encounters an XML element of unknown type during deserialization.</summary>
		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06001CF5 RID: 7413 RVA: 0x0009A1B4 File Offset: 0x000983B4
		// (remove) Token: 0x06001CF6 RID: 7414 RVA: 0x0009A1D0 File Offset: 0x000983D0
		public event XmlElementEventHandler UnknownElement
		{
			add
			{
				this.onUnknownElement = (XmlElementEventHandler)Delegate.Combine(this.onUnknownElement, value);
			}
			remove
			{
				this.onUnknownElement = (XmlElementEventHandler)Delegate.Remove(this.onUnknownElement, value);
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Xml.Serialization.XmlSerializer" /> encounters an XML node of unknown type during deserialization.</summary>
		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06001CF7 RID: 7415 RVA: 0x0009A1EC File Offset: 0x000983EC
		// (remove) Token: 0x06001CF8 RID: 7416 RVA: 0x0009A208 File Offset: 0x00098408
		public event XmlNodeEventHandler UnknownNode
		{
			add
			{
				this.onUnknownNode = (XmlNodeEventHandler)Delegate.Combine(this.onUnknownNode, value);
			}
			remove
			{
				this.onUnknownNode = (XmlNodeEventHandler)Delegate.Remove(this.onUnknownNode, value);
			}
		}

		/// <summary>Occurs during deserialization of a SOAP-encoded XML stream, when the <see cref="T:System.Xml.Serialization.XmlSerializer" /> encounters a recognized type that is not used or is unreferenced.</summary>
		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06001CF9 RID: 7417 RVA: 0x0009A224 File Offset: 0x00098424
		// (remove) Token: 0x06001CFA RID: 7418 RVA: 0x0009A240 File Offset: 0x00098440
		public event UnreferencedObjectEventHandler UnreferencedObject
		{
			add
			{
				this.onUnreferencedObject = (UnreferencedObjectEventHandler)Delegate.Combine(this.onUnreferencedObject, value);
			}
			remove
			{
				this.onUnreferencedObject = (UnreferencedObjectEventHandler)Delegate.Remove(this.onUnreferencedObject, value);
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06001CFB RID: 7419 RVA: 0x0009A25C File Offset: 0x0009845C
		internal XmlMapping Mapping
		{
			get
			{
				return this.typeMapping;
			}
		}

		// Token: 0x06001CFC RID: 7420 RVA: 0x0009A264 File Offset: 0x00098464
		internal virtual void OnUnknownAttribute(XmlAttributeEventArgs e)
		{
			if (this.onUnknownAttribute != null)
			{
				this.onUnknownAttribute(this, e);
			}
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x0009A280 File Offset: 0x00098480
		internal virtual void OnUnknownElement(XmlElementEventArgs e)
		{
			if (this.onUnknownElement != null)
			{
				this.onUnknownElement(this, e);
			}
		}

		// Token: 0x06001CFE RID: 7422 RVA: 0x0009A29C File Offset: 0x0009849C
		internal virtual void OnUnknownNode(XmlNodeEventArgs e)
		{
			if (this.onUnknownNode != null)
			{
				this.onUnknownNode(this, e);
			}
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x0009A2B8 File Offset: 0x000984B8
		internal virtual void OnUnreferencedObject(UnreferencedObjectEventArgs e)
		{
			if (this.onUnreferencedObject != null)
			{
				this.onUnreferencedObject(this, e);
			}
		}

		/// <summary>Gets a value that indicates whether this <see cref="T:System.Xml.Serialization.XmlSerializer" /> can deserialize a specified XML document.</summary>
		/// <returns>true if this <see cref="T:System.Xml.Serialization.XmlSerializer" /> can deserialize the object that the <see cref="T:System.Xml.XmlReader" /> points to; otherwise, false.</returns>
		/// <param name="xmlReader">An <see cref="T:System.Xml.XmlReader" /> that points to the document to deserialize. </param>
		// Token: 0x06001D00 RID: 7424 RVA: 0x0009A2D4 File Offset: 0x000984D4
		public virtual bool CanDeserialize(XmlReader xmlReader)
		{
			xmlReader.MoveToContent();
			return this.typeMapping is XmlMembersMapping || ((XmlTypeMapping)this.typeMapping).ElementName == xmlReader.LocalName;
		}

		/// <summary>Returns an object used to read the XML document to be serialized.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlSerializationReader" /> used to read the XML document.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the method when the method is not overridden in a descendant class. </exception>
		// Token: 0x06001D01 RID: 7425 RVA: 0x0009A318 File Offset: 0x00098518
		protected virtual XmlSerializationReader CreateReader()
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, returns a writer used to serialize the object.</summary>
		/// <returns>An instance that implements the <see cref="T:System.Xml.Serialization.XmlSerializationWriter" /> class.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the method when the method is not overridden in a descendant class. </exception>
		// Token: 0x06001D02 RID: 7426 RVA: 0x0009A320 File Offset: 0x00098520
		protected virtual XmlSerializationWriter CreateWriter()
		{
			throw new NotImplementedException();
		}

		/// <summary>Deserializes the XML document contained by the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <returns>The <see cref="T:System.Object" /> being deserialized.</returns>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> that contains the XML document to deserialize. </param>
		// Token: 0x06001D03 RID: 7427 RVA: 0x0009A328 File Offset: 0x00098528
		public object Deserialize(Stream stream)
		{
			return this.Deserialize(new XmlTextReader(stream)
			{
				Normalization = true,
				WhitespaceHandling = WhitespaceHandling.Significant
			});
		}

		/// <summary>Deserializes the XML document contained by the specified <see cref="T:System.IO.TextReader" />.</summary>
		/// <returns>The <see cref="T:System.Object" /> being deserialized.</returns>
		/// <param name="textReader">The <see cref="T:System.IO.TextReader" /> that contains the XML document to deserialize. </param>
		/// <exception cref="T:System.InvalidOperationException">An error occurred during deserialization. The original exception is available using the <see cref="P:System.Exception.InnerException" /> property. </exception>
		// Token: 0x06001D04 RID: 7428 RVA: 0x0009A354 File Offset: 0x00098554
		public object Deserialize(TextReader textReader)
		{
			return this.Deserialize(new XmlTextReader(textReader)
			{
				Normalization = true,
				WhitespaceHandling = WhitespaceHandling.Significant
			});
		}

		/// <summary>Deserializes the XML document contained by the specified <see cref="T:System.xml.XmlReader" />.</summary>
		/// <returns>The <see cref="T:System.Object" /> being deserialized.</returns>
		/// <param name="xmlReader">The <see cref="T:System.xml.XmlReader" /> that contains the XML document to deserialize. </param>
		/// <exception cref="T:System.InvalidOperationException">An error occurred during deserialization. The original exception is available using the <see cref="P:System.Exception.InnerException" /> property. </exception>
		// Token: 0x06001D05 RID: 7429 RVA: 0x0009A380 File Offset: 0x00098580
		public object Deserialize(XmlReader xmlReader)
		{
			XmlSerializationReader xmlSerializationReader;
			if (this.customSerializer)
			{
				xmlSerializationReader = this.CreateReader();
			}
			else
			{
				xmlSerializationReader = this.CreateReader(this.typeMapping);
			}
			xmlSerializationReader.Initialize(xmlReader, this);
			return this.Deserialize(xmlSerializationReader);
		}

		/// <summary>Deserializes the XML document contained by the specified <see cref="T:System.Xml.Serialization.XmlSerializationReader" />.</summary>
		/// <returns>The deserialized object.</returns>
		/// <param name="reader">The <see cref="T:System.Xml.Serialization.XmlSerializationReader" /> that contains the XML document to deserialize. </param>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the method when the method is not overridden in a descendant class. </exception>
		// Token: 0x06001D06 RID: 7430 RVA: 0x0009A3C0 File Offset: 0x000985C0
		protected virtual object Deserialize(XmlSerializationReader reader)
		{
			if (this.customSerializer)
			{
				throw new NotImplementedException();
			}
			object result;
			try
			{
				if (reader is XmlSerializationReaderInterpreter)
				{
					result = ((XmlSerializationReaderInterpreter)reader).ReadRoot();
				}
				else
				{
					result = this.serializerData.ReaderMethod.Invoke(reader, null);
				}
			}
			catch (Exception ex)
			{
				if (ex is InvalidOperationException || ex is InvalidCastException)
				{
					throw new InvalidOperationException("There is an error in XML document.", ex);
				}
				throw;
			}
			return result;
		}

		/// <summary>Returns an array of <see cref="T:System.Xml.Serialization.XmlSerializer" /> objects created from an array of <see cref="T:System.Xml.Serialization.XmlTypeMapping" /> objects.</summary>
		/// <returns>An array of <see cref="T:System.Xml.Serialization.XmlSerializer" /> objects.</returns>
		/// <param name="mappings">An array of <see cref="T:System.Xml.Serialization.XmlTypeMapping" /> that maps one type to another. </param>
		// Token: 0x06001D07 RID: 7431 RVA: 0x0009A460 File Offset: 0x00098660
		public static XmlSerializer[] FromMappings(XmlMapping[] mappings)
		{
			XmlSerializer[] array = new XmlSerializer[mappings.Length];
			XmlSerializer.SerializerData[] array2 = new XmlSerializer.SerializerData[mappings.Length];
			XmlSerializer.GenerationBatch generationBatch = new XmlSerializer.GenerationBatch();
			generationBatch.Maps = mappings;
			generationBatch.Datas = array2;
			for (int i = 0; i < mappings.Length; i++)
			{
				if (mappings[i] != null)
				{
					XmlSerializer.SerializerData serializerData = new XmlSerializer.SerializerData();
					serializerData.Batch = generationBatch;
					array[i] = new XmlSerializer(mappings[i], serializerData);
					array2[i] = serializerData;
				}
			}
			return array;
		}

		/// <summary>Returns an array of <see cref="T:System.Xml.Serialization.XmlSerializer" /> objects created from an array of types.</summary>
		/// <returns>An array of <see cref="T:System.Xml.Serialization.XmlSerializer" /> objects.</returns>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects. </param>
		// Token: 0x06001D08 RID: 7432 RVA: 0x0009A4D4 File Offset: 0x000986D4
		public static XmlSerializer[] FromTypes(Type[] mappings)
		{
			XmlSerializer[] array = new XmlSerializer[mappings.Length];
			for (int i = 0; i < mappings.Length; i++)
			{
				array[i] = new XmlSerializer(mappings[i]);
			}
			return array;
		}

		/// <summary>Serializes the specified <see cref="T:System.Object" /> and writes the XML document to a file using the specified <see cref="T:System.Xml.Serialization.XmlSerializationWriter" />.</summary>
		/// <param name="o">The <see cref="T:System.Object" /> to serialize. </param>
		/// <param name="writer">The <see cref="T:System.Xml.Serialization.XmlSerializationWriter" /> used to write the XML document. </param>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the method when the method is not overridden in a descendant class. </exception>
		// Token: 0x06001D09 RID: 7433 RVA: 0x0009A50C File Offset: 0x0009870C
		protected virtual void Serialize(object o, XmlSerializationWriter writer)
		{
			if (this.customSerializer)
			{
				throw new NotImplementedException();
			}
			if (writer is XmlSerializationWriterInterpreter)
			{
				((XmlSerializationWriterInterpreter)writer).WriteRoot(o);
			}
			else
			{
				this.serializerData.WriterMethod.Invoke(writer, new object[]
				{
					o
				});
			}
		}

		/// <summary>Serializes the specified <see cref="T:System.Object" /> and writes the XML document to a file using the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> used to write the XML document. </param>
		/// <param name="o">The <see cref="T:System.Object" /> to serialize. </param>
		/// <exception cref="T:System.InvalidOperationException">An error occurred during serialization. The original exception is available using the <see cref="P:System.Exception.InnerException" /> property. </exception>
		// Token: 0x06001D0A RID: 7434 RVA: 0x0009A564 File Offset: 0x00098764
		public void Serialize(Stream stream, object o)
		{
			this.Serialize(new XmlTextWriter(stream, Encoding.Default)
			{
				Formatting = Formatting.Indented
			}, o, null);
		}

		/// <summary>Serializes the specified <see cref="T:System.Object" /> and writes the XML document to a file using the specified <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="textWriter">The <see cref="T:System.IO.TextWriter" /> used to write the XML document. </param>
		/// <param name="o">The <see cref="T:System.Object" /> to serialize. </param>
		// Token: 0x06001D0B RID: 7435 RVA: 0x0009A590 File Offset: 0x00098790
		public void Serialize(TextWriter textWriter, object o)
		{
			this.Serialize(new XmlTextWriter(textWriter)
			{
				Formatting = Formatting.Indented
			}, o, null);
		}

		/// <summary>Serializes the specified <see cref="T:System.Object" /> and writes the XML document to a file using the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="xmlWriter">The <see cref="T:System.xml.XmlWriter" /> used to write the XML document. </param>
		/// <param name="o">The <see cref="T:System.Object" /> to serialize. </param>
		/// <exception cref="T:System.InvalidOperationException">An error occurred during serialization. The original exception is available using the <see cref="P:System.Exception.InnerException" /> property. </exception>
		// Token: 0x06001D0C RID: 7436 RVA: 0x0009A5B4 File Offset: 0x000987B4
		public void Serialize(XmlWriter xmlWriter, object o)
		{
			this.Serialize(xmlWriter, o, null);
		}

		/// <summary>Serializes the specified <see cref="T:System.Object" /> and writes the XML document to a file using the specified <see cref="T:System.IO.Stream" />that references the specified namespaces.</summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> used to write the XML document. </param>
		/// <param name="o">The <see cref="T:System.Object" /> to serialize. </param>
		/// <param name="namespaces">The <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> referenced by the object. </param>
		/// <exception cref="T:System.InvalidOperationException">An error occurred during serialization. The original exception is available using the <see cref="P:System.Exception.InnerException" /> property. </exception>
		// Token: 0x06001D0D RID: 7437 RVA: 0x0009A5C0 File Offset: 0x000987C0
		public void Serialize(Stream stream, object o, XmlSerializerNamespaces namespaces)
		{
			this.Serialize(new XmlTextWriter(stream, Encoding.Default)
			{
				Formatting = Formatting.Indented
			}, o, namespaces);
		}

		/// <summary>Serializes the specified <see cref="T:System.Object" /> and writes the XML document to a file using the specified <see cref="T:System.IO.TextWriter" /> and references the specified namespaces.</summary>
		/// <param name="textWriter">The <see cref="T:System.IO.TextWriter" /> used to write the XML document. </param>
		/// <param name="o">The <see cref="T:System.Object" /> to serialize. </param>
		/// <param name="namespaces">The <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> that contains namespaces for the generated XML document. </param>
		/// <exception cref="T:System.InvalidOperationException">An error occurred during serialization. The original exception is available using the <see cref="P:System.Exception.InnerException" /> property. </exception>
		// Token: 0x06001D0E RID: 7438 RVA: 0x0009A5EC File Offset: 0x000987EC
		public void Serialize(TextWriter textWriter, object o, XmlSerializerNamespaces namespaces)
		{
			XmlTextWriter xmlTextWriter = new XmlTextWriter(textWriter);
			xmlTextWriter.Formatting = Formatting.Indented;
			this.Serialize(xmlTextWriter, o, namespaces);
			xmlTextWriter.Flush();
		}

		/// <summary>Serializes the specified <see cref="T:System.Object" /> and writes the XML document to a file using the specified <see cref="T:System.Xml.XmlWriter" /> and references the specified namespaces.</summary>
		/// <param name="xmlWriter">The <see cref="T:System.xml.XmlWriter" /> used to write the XML document. </param>
		/// <param name="o">The <see cref="T:System.Object" /> to serialize. </param>
		/// <param name="namespaces">The <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> referenced by the object. </param>
		/// <exception cref="T:System.InvalidOperationException">An error occurred during serialization. The original exception is available using the <see cref="P:System.Exception.InnerException" /> property. </exception>
		// Token: 0x06001D0F RID: 7439 RVA: 0x0009A618 File Offset: 0x00098818
		public void Serialize(XmlWriter writer, object o, XmlSerializerNamespaces namespaces)
		{
			try
			{
				XmlSerializationWriter xmlSerializationWriter;
				if (this.customSerializer)
				{
					xmlSerializationWriter = this.CreateWriter();
				}
				else
				{
					xmlSerializationWriter = this.CreateWriter(this.typeMapping);
				}
				if (namespaces == null || namespaces.Count == 0)
				{
					namespaces = new XmlSerializerNamespaces();
					namespaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
					namespaces.Add("xsd", "http://www.w3.org/2001/XMLSchema");
				}
				xmlSerializationWriter.Initialize(writer, namespaces);
				this.Serialize(o, xmlSerializationWriter);
				writer.Flush();
			}
			catch (Exception innerException)
			{
				if (innerException is TargetInvocationException)
				{
					innerException = innerException.InnerException;
				}
				if (innerException is InvalidOperationException || innerException is InvalidCastException)
				{
					throw new InvalidOperationException("There was an error generating the XML document.", innerException);
				}
				throw;
			}
		}

		/// <summary>Deserializes the object using the data contained by the specified <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <returns>The object being deserialized.</returns>
		/// <param name="xmlReader">An instance of the <see cref="T:System.Xml.XmlReader" /> class used to read the document.</param>
		/// <param name="encodingStyle">The encoding used.</param>
		/// <param name="events">An instance of the <see cref="T:System.Xml.Serialization.XmlDeserializationEvents" /> class. </param>
		// Token: 0x06001D10 RID: 7440 RVA: 0x0009A6F4 File Offset: 0x000988F4
		[MonoTODO]
		public object Deserialize(XmlReader xmlReader, string encodingStyle, XmlDeserializationEvents events)
		{
			throw new NotImplementedException();
		}

		/// <summary>Deserializes the XML document contained by the specified <see cref="T:System.xml.XmlReader" /> and encoding style.</summary>
		/// <returns>The deserialized object.</returns>
		/// <param name="xmlReader">The <see cref="T:System.xml.XmlReader" /> that contains the XML document to deserialize. </param>
		/// <param name="encodingStyle">The encoding style of the serialized XML. </param>
		/// <exception cref="T:System.InvalidOperationException">An error occurred during deserialization. The original exception is available using the <see cref="P:System.Exception.InnerException" /> property. </exception>
		// Token: 0x06001D11 RID: 7441 RVA: 0x0009A6FC File Offset: 0x000988FC
		[MonoTODO]
		public object Deserialize(XmlReader xmlReader, string encodingStyle)
		{
			throw new NotImplementedException();
		}

		/// <summary>Deserializes an XML document contained by the specified <see cref="T:System.Xml.XmlReader" /> and allows the overriding of events that occur during deserialization.</summary>
		/// <returns>The <see cref="T:System.Object" /> being deserialized.</returns>
		/// <param name="xmlReader">The <see cref="T:System.Xml.XmlReader" /> that contains the document to deserialize.</param>
		/// <param name="events">An instance of the <see cref="T:System.Xml.Serialization.XmlDeserializationEvents" /> class. </param>
		// Token: 0x06001D12 RID: 7442 RVA: 0x0009A704 File Offset: 0x00098904
		[MonoTODO]
		public object Deserialize(XmlReader xmlReader, XmlDeserializationEvents events)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns an instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class created from mappings of one XML type to another.</summary>
		/// <returns>An instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class.</returns>
		/// <param name="mappings">An array of <see cref="T:System.Xml.Serialization.XmlMapping" /> objects used to map one type to another.</param>
		/// <param name="evidence">An instance of the <see cref="T:System.Security.Policy.Evidence" /> class that contains host and assembly data presented to the common language runtime policy system.</param>
		// Token: 0x06001D13 RID: 7443 RVA: 0x0009A70C File Offset: 0x0009890C
		[MonoTODO]
		public static XmlSerializer[] FromMappings(XmlMapping[] mappings, Evidence evidence)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns an instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class from the specified mappings.</summary>
		/// <returns>An instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class.</returns>
		/// <param name="mappings">An array of <see cref="T:System.Xml.Serialization.XmlMapping" /> objects.</param>
		/// <param name="type">The <see cref="T:System.Type" /> of the deserialized object.</param>
		// Token: 0x06001D14 RID: 7444 RVA: 0x0009A714 File Offset: 0x00098914
		[MonoTODO]
		public static XmlSerializer[] FromMappings(XmlMapping[] mappings, Type type)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns an assembly that contains custom-made serializers used to serialize or deserialize the specified type or types, using the specified mappings.</summary>
		/// <returns>An <see cref="T:System.Reflection.Assembly" /> object that contains serializers for the supplied types and mappings.</returns>
		/// <param name="types">A collection of types.</param>
		/// <param name="mappings">A collection of <see cref="T:System.Xml.Serialization.XmlMapping" /> objects used to convert one type to another.</param>
		// Token: 0x06001D15 RID: 7445 RVA: 0x0009A71C File Offset: 0x0009891C
		public static Assembly GenerateSerializer(Type[] types, XmlMapping[] mappings)
		{
			return XmlSerializer.GenerateSerializer(types, mappings, null);
		}

		/// <summary>Returns an assembly that contains custom-made serializers used to serialize or deserialize the specified type or types, using the specified mappings and compiler settings and options. </summary>
		/// <returns>An <see cref="T:System.Reflection.Assembly" /> that contains special versions of the <see cref="T:System.Xml.Serialization.XmlSerializer" />.</returns>
		/// <param name="types">An array of type <see cref="T:System.Type" /> that contains objects used to serialize and deserialize data.</param>
		/// <param name="mappings">An array of type <see cref="T:System.Xml.Serialization.XmlMapping" /> that maps the XML data to the type data.</param>
		/// <param name="parameters">An instance of the <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> class that represents the parameters used to invoke a compiler.</param>
		// Token: 0x06001D16 RID: 7446 RVA: 0x0009A728 File Offset: 0x00098928
		[MonoTODO]
		public static Assembly GenerateSerializer(Type[] types, XmlMapping[] mappings, CompilerParameters parameters)
		{
			XmlSerializer.GenerationBatch generationBatch = new XmlSerializer.GenerationBatch();
			generationBatch.Maps = mappings;
			generationBatch.Datas = new XmlSerializer.SerializerData[mappings.Length];
			for (int i = 0; i < mappings.Length; i++)
			{
				XmlSerializer.SerializerData serializerData = new XmlSerializer.SerializerData();
				serializerData.Batch = generationBatch;
				generationBatch.Datas[i] = serializerData;
			}
			return XmlSerializer.GenerateSerializers(generationBatch, parameters);
		}

		/// <summary>Returns the name of the assembly that contains one or more versions of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> especially created to serialize or deserialize the specified type.</summary>
		/// <returns>The name of the assembly that contains an <see cref="T:System.Xml.Serialization.XmlSerializer" /> for the type.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> you are deserializing.</param>
		// Token: 0x06001D17 RID: 7447 RVA: 0x0009A784 File Offset: 0x00098984
		public static string GetXmlSerializerAssemblyName(Type type)
		{
			return type.Assembly.GetName().Name + ".XmlSerializers";
		}

		/// <summary>Returns the name of the assembly that contains the serializer for the specified type in the specified namespace.</summary>
		/// <returns>The name of the assembly that contains specially built serializers.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> you are interested in.</param>
		/// <param name="defaultNamespace">The namespace of the type.</param>
		// Token: 0x06001D18 RID: 7448 RVA: 0x0009A7AC File Offset: 0x000989AC
		public static string GetXmlSerializerAssemblyName(Type type, string defaultNamespace)
		{
			return XmlSerializer.GetXmlSerializerAssemblyName(type) + "." + defaultNamespace.GetHashCode();
		}

		/// <summary>Serializes the specified object and writes the XML document to a file using the specified <see cref="T:System.Xml.XmlWriter" /> and references the specified namespaces and encoding style.</summary>
		/// <param name="xmlWriter">The <see cref="T:System.xml.XmlWriter" /> used to write the XML document. </param>
		/// <param name="o">The object to serialize. </param>
		/// <param name="namespaces">The <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> referenced by the object. </param>
		/// <param name="encodingStyle">The encoding style of the serialized XML. </param>
		/// <exception cref="T:System.InvalidOperationException">An error occurred during serialization. The original exception is available using the <see cref="P:System.Exception.InnerException" /> property. </exception>
		// Token: 0x06001D19 RID: 7449 RVA: 0x0009A7CC File Offset: 0x000989CC
		[MonoTODO]
		public void Serialize(XmlWriter xmlWriter, object o, XmlSerializerNamespaces namespaces, string encodingStyle)
		{
			throw new NotImplementedException();
		}

		/// <summary>Serializes the specified <see cref="T:System.Object" /> and writes the XML document to a file using the specified <see cref="T:System.Xml.XmlWriter" />, XML namespaces, and encoding. </summary>
		/// <param name="xmlWriter">The <see cref="T:System.Xml.XmlWriter" /> used to write the XML document.</param>
		/// <param name="o">The object to serialize.</param>
		/// <param name="namespaces">An instance of the XmlSerializaerNamespaces that contains namespaces and prefixes to use.</param>
		/// <param name="encodingStyle">The encoding used in the document.</param>
		/// <param name="id">For SOAP encoded messages, the base used to generate id attributes. </param>
		// Token: 0x06001D1A RID: 7450 RVA: 0x0009A7D4 File Offset: 0x000989D4
		[MonoNotSupported("")]
		public void Serialize(XmlWriter xmlWriter, object o, XmlSerializerNamespaces namespaces, string encodingStyle, string id)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001D1B RID: 7451 RVA: 0x0009A7DC File Offset: 0x000989DC
		private XmlSerializationWriter CreateWriter(XmlMapping typeMapping)
		{
			lock (this)
			{
				if (this.serializerData != null)
				{
					XmlSerializer.SerializerData obj = this.serializerData;
					XmlSerializationWriter xmlSerializationWriter;
					lock (obj)
					{
						xmlSerializationWriter = this.serializerData.CreateWriter();
					}
					if (xmlSerializationWriter != null)
					{
						return xmlSerializationWriter;
					}
				}
			}
			if (!typeMapping.Source.CanBeGenerated || XmlSerializer.generationThreshold == -1)
			{
				return new XmlSerializationWriterInterpreter(typeMapping);
			}
			this.CheckGeneratedTypes(typeMapping);
			lock (this)
			{
				XmlSerializer.SerializerData obj2 = this.serializerData;
				XmlSerializationWriter xmlSerializationWriter;
				lock (obj2)
				{
					xmlSerializationWriter = this.serializerData.CreateWriter();
				}
				if (xmlSerializationWriter != null)
				{
					return xmlSerializationWriter;
				}
				if (!XmlSerializer.generatorFallback)
				{
					throw new InvalidOperationException("Error while generating serializer");
				}
			}
			return new XmlSerializationWriterInterpreter(typeMapping);
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x0009A930 File Offset: 0x00098B30
		private XmlSerializationReader CreateReader(XmlMapping typeMapping)
		{
			lock (this)
			{
				if (this.serializerData != null)
				{
					XmlSerializer.SerializerData obj = this.serializerData;
					XmlSerializationReader xmlSerializationReader;
					lock (obj)
					{
						xmlSerializationReader = this.serializerData.CreateReader();
					}
					if (xmlSerializationReader != null)
					{
						return xmlSerializationReader;
					}
				}
			}
			if (!typeMapping.Source.CanBeGenerated || XmlSerializer.generationThreshold == -1)
			{
				return new XmlSerializationReaderInterpreter(typeMapping);
			}
			this.CheckGeneratedTypes(typeMapping);
			lock (this)
			{
				XmlSerializer.SerializerData obj2 = this.serializerData;
				XmlSerializationReader xmlSerializationReader;
				lock (obj2)
				{
					xmlSerializationReader = this.serializerData.CreateReader();
				}
				if (xmlSerializationReader != null)
				{
					return xmlSerializationReader;
				}
				if (!XmlSerializer.generatorFallback)
				{
					throw new InvalidOperationException("Error while generating serializer");
				}
			}
			return new XmlSerializationReaderInterpreter(typeMapping);
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x0009AA84 File Offset: 0x00098C84
		private void CheckGeneratedTypes(XmlMapping typeMapping)
		{
			lock (this)
			{
				if (this.serializerData == null)
				{
					Hashtable obj = XmlSerializer.serializerTypes;
					lock (obj)
					{
						this.serializerData = (XmlSerializer.SerializerData)XmlSerializer.serializerTypes[typeMapping.Source];
						if (this.serializerData == null)
						{
							this.serializerData = new XmlSerializer.SerializerData();
							XmlSerializer.serializerTypes[typeMapping.Source] = this.serializerData;
						}
					}
				}
			}
			bool flag = false;
			XmlSerializer.SerializerData obj2 = this.serializerData;
			lock (obj2)
			{
				flag = (++this.serializerData.UsageCount == XmlSerializer.generationThreshold);
			}
			if (flag)
			{
				if (this.serializerData.Batch != null)
				{
					this.GenerateSerializersAsync(this.serializerData.Batch);
				}
				else
				{
					this.GenerateSerializersAsync(new XmlSerializer.GenerationBatch
					{
						Maps = new XmlMapping[]
						{
							typeMapping
						},
						Datas = new XmlSerializer.SerializerData[]
						{
							this.serializerData
						}
					});
				}
			}
		}

		// Token: 0x06001D1E RID: 7454 RVA: 0x0009ABF8 File Offset: 0x00098DF8
		private void GenerateSerializersAsync(XmlSerializer.GenerationBatch batch)
		{
			if (batch.Maps.Length != batch.Datas.Length)
			{
				throw new ArgumentException("batch");
			}
			lock (batch)
			{
				if (batch.Done)
				{
					return;
				}
				batch.Done = true;
			}
			if (XmlSerializer.backgroundGeneration)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.RunSerializerGeneration), batch);
			}
			else
			{
				this.RunSerializerGeneration(batch);
			}
		}

		// Token: 0x06001D1F RID: 7455 RVA: 0x0009AC98 File Offset: 0x00098E98
		private void RunSerializerGeneration(object obj)
		{
			try
			{
				XmlSerializer.GenerationBatch generationBatch = (XmlSerializer.GenerationBatch)obj;
				generationBatch = this.LoadFromSatelliteAssembly(generationBatch);
				if (generationBatch != null)
				{
					XmlSerializer.GenerateSerializers(generationBatch, null);
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
		}

		// Token: 0x06001D20 RID: 7456 RVA: 0x0009ACF0 File Offset: 0x00098EF0
		private static Assembly GenerateSerializers(XmlSerializer.GenerationBatch batch, CompilerParameters cp)
		{
			DateTime now = DateTime.Now;
			XmlMapping[] maps = batch.Maps;
			if (cp == null)
			{
				cp = new CompilerParameters();
				cp.IncludeDebugInformation = false;
				cp.GenerateInMemory = true;
				cp.TempFiles.KeepFiles = !XmlSerializer.deleteTempFiles;
			}
			string text = cp.TempFiles.AddExtension("cs");
			StreamWriter streamWriter = new StreamWriter(text);
			if (!XmlSerializer.deleteTempFiles)
			{
				Console.WriteLine("Generating " + text);
			}
			SerializationCodeGenerator serializationCodeGenerator = new SerializationCodeGenerator(maps);
			try
			{
				serializationCodeGenerator.GenerateSerializers(streamWriter);
			}
			catch (Exception value)
			{
				Console.WriteLine("Serializer could not be generated");
				Console.WriteLine(value);
				cp.TempFiles.Delete();
				return null;
			}
			streamWriter.Close();
			CSharpCodeProvider csharpCodeProvider = new CSharpCodeProvider();
			ICodeCompiler codeCompiler = csharpCodeProvider.CreateCompiler();
			cp.GenerateExecutable = false;
			foreach (object obj in serializationCodeGenerator.ReferencedTypes)
			{
				Type type = (Type)obj;
				string localPath = new Uri(type.Assembly.CodeBase).LocalPath;
				if (!cp.ReferencedAssemblies.Contains(localPath))
				{
					cp.ReferencedAssemblies.Add(localPath);
				}
			}
			if (!cp.ReferencedAssemblies.Contains("System.dll"))
			{
				cp.ReferencedAssemblies.Add("System.dll");
			}
			if (!cp.ReferencedAssemblies.Contains("System.Xml"))
			{
				cp.ReferencedAssemblies.Add("System.Xml");
			}
			if (!cp.ReferencedAssemblies.Contains("System.Data"))
			{
				cp.ReferencedAssemblies.Add("System.Data");
			}
			CompilerResults compilerResults = codeCompiler.CompileAssemblyFromFile(cp, text);
			if (compilerResults.Errors.HasErrors || compilerResults.CompiledAssembly == null)
			{
				Console.WriteLine("Error while compiling generated serializer");
				foreach (object obj2 in compilerResults.Errors)
				{
					CompilerError value2 = (CompilerError)obj2;
					Console.WriteLine(value2);
				}
				cp.TempFiles.Delete();
				return null;
			}
			GenerationResult[] generationResults = serializationCodeGenerator.GenerationResults;
			for (int i = 0; i < generationResults.Length; i++)
			{
				GenerationResult generationResult = generationResults[i];
				XmlSerializer.SerializerData serializerData = batch.Datas[i];
				XmlSerializer.SerializerData obj3 = serializerData;
				lock (obj3)
				{
					serializerData.WriterType = compilerResults.CompiledAssembly.GetType(generationResult.Namespace + "." + generationResult.WriterClassName);
					serializerData.ReaderType = compilerResults.CompiledAssembly.GetType(generationResult.Namespace + "." + generationResult.ReaderClassName);
					serializerData.WriterMethod = serializerData.WriterType.GetMethod(generationResult.WriteMethodName);
					serializerData.ReaderMethod = serializerData.ReaderType.GetMethod(generationResult.ReadMethodName);
					serializerData.Batch = null;
				}
			}
			cp.TempFiles.Delete();
			if (!XmlSerializer.deleteTempFiles)
			{
				Console.WriteLine("Generation finished - " + (DateTime.Now - now).TotalMilliseconds + " ms");
			}
			return compilerResults.CompiledAssembly;
		}

		// Token: 0x06001D21 RID: 7457 RVA: 0x0009B0D8 File Offset: 0x000992D8
		private XmlSerializer.GenerationBatch LoadFromSatelliteAssembly(XmlSerializer.GenerationBatch batch)
		{
			return batch;
		}

		// Token: 0x04000B81 RID: 2945
		internal const string WsdlNamespace = "http://schemas.xmlsoap.org/wsdl/";

		// Token: 0x04000B82 RID: 2946
		internal const string EncodingNamespace = "http://schemas.xmlsoap.org/soap/encoding/";

		// Token: 0x04000B83 RID: 2947
		internal const string WsdlTypesNamespace = "http://microsoft.com/wsdl/types/";

		// Token: 0x04000B84 RID: 2948
		private static int generationThreshold;

		// Token: 0x04000B85 RID: 2949
		private static bool backgroundGeneration = true;

		// Token: 0x04000B86 RID: 2950
		private static bool deleteTempFiles = true;

		// Token: 0x04000B87 RID: 2951
		private static bool generatorFallback = true;

		// Token: 0x04000B88 RID: 2952
		private bool customSerializer;

		// Token: 0x04000B89 RID: 2953
		private XmlMapping typeMapping;

		// Token: 0x04000B8A RID: 2954
		private XmlSerializer.SerializerData serializerData;

		// Token: 0x04000B8B RID: 2955
		private static Hashtable serializerTypes = new Hashtable();

		// Token: 0x04000B8C RID: 2956
		private XmlAttributeEventHandler onUnknownAttribute;

		// Token: 0x04000B8D RID: 2957
		private XmlElementEventHandler onUnknownElement;

		// Token: 0x04000B8E RID: 2958
		private XmlNodeEventHandler onUnknownNode;

		// Token: 0x04000B8F RID: 2959
		private UnreferencedObjectEventHandler onUnreferencedObject;

		// Token: 0x020002B2 RID: 690
		internal class SerializerData
		{
			// Token: 0x06001D23 RID: 7459 RVA: 0x0009B0E4 File Offset: 0x000992E4
			public XmlSerializationReader CreateReader()
			{
				if (this.ReaderType != null)
				{
					return (XmlSerializationReader)Activator.CreateInstance(this.ReaderType);
				}
				if (this.Implementation != null)
				{
					return this.Implementation.Reader;
				}
				return null;
			}

			// Token: 0x06001D24 RID: 7460 RVA: 0x0009B128 File Offset: 0x00099328
			public XmlSerializationWriter CreateWriter()
			{
				if (this.WriterType != null)
				{
					return (XmlSerializationWriter)Activator.CreateInstance(this.WriterType);
				}
				if (this.Implementation != null)
				{
					return this.Implementation.Writer;
				}
				return null;
			}

			// Token: 0x04000B90 RID: 2960
			public int UsageCount;

			// Token: 0x04000B91 RID: 2961
			public Type ReaderType;

			// Token: 0x04000B92 RID: 2962
			public MethodInfo ReaderMethod;

			// Token: 0x04000B93 RID: 2963
			public Type WriterType;

			// Token: 0x04000B94 RID: 2964
			public MethodInfo WriterMethod;

			// Token: 0x04000B95 RID: 2965
			public XmlSerializer.GenerationBatch Batch;

			// Token: 0x04000B96 RID: 2966
			public XmlSerializerImplementation Implementation;
		}

		// Token: 0x020002B3 RID: 691
		internal class GenerationBatch
		{
			// Token: 0x04000B97 RID: 2967
			public bool Done;

			// Token: 0x04000B98 RID: 2968
			public XmlMapping[] Maps;

			// Token: 0x04000B99 RID: 2969
			public XmlSerializer.SerializerData[] Datas;
		}
	}
}
