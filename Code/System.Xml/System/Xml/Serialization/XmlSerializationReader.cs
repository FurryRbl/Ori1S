using System;
using System.Collections;
using System.Globalization;
using System.Reflection;

namespace System.Xml.Serialization
{
	/// <summary>Controls deserialization by the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class. </summary>
	// Token: 0x020002A5 RID: 677
	[MonoTODO]
	public abstract class XmlSerializationReader : XmlSerializationGeneratedCode
	{
		// Token: 0x06001BCB RID: 7115 RVA: 0x00093B5C File Offset: 0x00091D5C
		internal void Initialize(XmlReader reader, XmlSerializer eventSource)
		{
			this.w3SchemaNS = reader.NameTable.Add("http://www.w3.org/2001/XMLSchema");
			this.w3InstanceNS = reader.NameTable.Add("http://www.w3.org/2001/XMLSchema-instance");
			this.w3InstanceNS2000 = reader.NameTable.Add("http://www.w3.org/2000/10/XMLSchema-instance");
			this.w3InstanceNS1999 = reader.NameTable.Add("http://www.w3.org/1999/XMLSchema-instance");
			this.soapNS = reader.NameTable.Add("http://schemas.xmlsoap.org/soap/encoding/");
			this.wsdlNS = reader.NameTable.Add("http://schemas.xmlsoap.org/wsdl/");
			this.nullX = reader.NameTable.Add("null");
			this.nil = reader.NameTable.Add("nil");
			this.typeX = reader.NameTable.Add("type");
			this.arrayType = reader.NameTable.Add("arrayType");
			this.reader = reader;
			this.eventSource = eventSource;
			this.arrayQName = new XmlQualifiedName("Array", this.soapNS);
			this.InitIDs();
		}

		// Token: 0x06001BCC RID: 7116 RVA: 0x00093C70 File Offset: 0x00091E70
		private ArrayList EnsureArrayList(ArrayList list)
		{
			if (list == null)
			{
				list = new ArrayList();
			}
			return list;
		}

		// Token: 0x06001BCD RID: 7117 RVA: 0x00093C80 File Offset: 0x00091E80
		private Hashtable EnsureHashtable(Hashtable hash)
		{
			if (hash == null)
			{
				hash = new Hashtable();
			}
			return hash;
		}

		/// <summary>Gets the XML document object into which the XML document is being deserialized. </summary>
		/// <returns>An <see cref="T:System.Xml.XmlDocument" /> that represents the deserialized <see cref="T:System.Xml.XmlDocument" /> data.</returns>
		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06001BCE RID: 7118 RVA: 0x00093C90 File Offset: 0x00091E90
		protected XmlDocument Document
		{
			get
			{
				if (this.document == null)
				{
					this.document = new XmlDocument(this.reader.NameTable);
				}
				return this.document;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlReader" /> object that is being used by <see cref="T:System.Xml.Serialization.XmlSerializationReader" />. </summary>
		/// <returns>The <see cref="T:System.Xml.XmlReader" /> that is being used by the <see cref="T:System.Xml.Serialization.XmlSerializationReader" />.</returns>
		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06001BCF RID: 7119 RVA: 0x00093CBC File Offset: 0x00091EBC
		protected XmlReader Reader
		{
			get
			{
				return this.reader;
			}
		}

		/// <summary>Gets or sets a value that should be true for a SOAP 1.1 return value.</summary>
		/// <returns>true, if the value is a return value. </returns>
		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06001BD0 RID: 7120 RVA: 0x00093CC4 File Offset: 0x00091EC4
		// (set) Token: 0x06001BD1 RID: 7121 RVA: 0x00093CCC File Offset: 0x00091ECC
		[MonoTODO]
		protected bool IsReturnValue
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the current count of the <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <returns>The current count of an <see cref="T:System.Xml.XmlReader" />.</returns>
		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06001BD2 RID: 7122 RVA: 0x00093CD4 File Offset: 0x00091ED4
		protected int ReaderCount
		{
			get
			{
				return this.readCount;
			}
		}

		/// <summary>Stores an object that contains a callback method that will be called, as necessary, to fill in .NET Framework collections or enumerations that map to SOAP-encoded arrays or SOAP-encoded, multi-referenced elements. </summary>
		/// <param name="fixup">A <see cref="T:System.Xml.Serialization.XmlSerializationCollectionFixupCallback" /> delegate and the callback method's input data.</param>
		// Token: 0x06001BD3 RID: 7123 RVA: 0x00093CDC File Offset: 0x00091EDC
		protected void AddFixup(XmlSerializationReader.CollectionFixup fixup)
		{
			this.collFixups = this.EnsureHashtable(this.collFixups);
			this.collFixups[fixup.Id] = fixup;
			if (this.delayedListFixups != null && this.delayedListFixups.ContainsKey(fixup.Id))
			{
				fixup.CollectionItems = this.delayedListFixups[fixup.Id];
				this.delayedListFixups.Remove(fixup.Id);
			}
		}

		/// <summary>Stores an object that contains a callback method instance that will be called, as necessary, to fill in the objects in a SOAP-encoded array. </summary>
		/// <param name="fixup">An <see cref="T:System.Xml.Serialization.XmlSerializationFixupCallback" /> delegate and the callback method's input data.</param>
		// Token: 0x06001BD4 RID: 7124 RVA: 0x00093D58 File Offset: 0x00091F58
		protected void AddFixup(XmlSerializationReader.Fixup fixup)
		{
			this.fixups = this.EnsureArrayList(this.fixups);
			this.fixups.Add(fixup);
		}

		// Token: 0x06001BD5 RID: 7125 RVA: 0x00093D7C File Offset: 0x00091F7C
		private void AddFixup(XmlSerializationReader.CollectionItemFixup fixup)
		{
			this.collItemFixups = this.EnsureArrayList(this.collItemFixups);
			this.collItemFixups.Add(fixup);
		}

		/// <summary>Stores an implementation of the <see cref="T:System.Xml.Serialization.XmlSerializationReadCallback" /> delegate and its input data for a later invocation. </summary>
		/// <param name="name">The name of the .NET Framework type that is being deserialized.</param>
		/// <param name="ns">The namespace of the .NET Framework type that is being deserialized.</param>
		/// <param name="type">The <see cref="T:System.Type" /> to be deserialized.</param>
		/// <param name="read">An <see cref="T:System.Xml.Serialization.XmlSerializationReadCallback" /> delegate.</param>
		// Token: 0x06001BD6 RID: 7126 RVA: 0x00093DA0 File Offset: 0x00091FA0
		protected void AddReadCallback(string name, string ns, Type type, XmlSerializationReadCallback read)
		{
			XmlSerializationReader.WriteCallbackInfo writeCallbackInfo = new XmlSerializationReader.WriteCallbackInfo();
			writeCallbackInfo.Type = type;
			writeCallbackInfo.TypeName = name;
			writeCallbackInfo.TypeNs = ns;
			writeCallbackInfo.Callback = read;
			this.typesCallbacks = this.EnsureHashtable(this.typesCallbacks);
			this.typesCallbacks.Add(new XmlQualifiedName(name, ns), writeCallbackInfo);
		}

		/// <summary>Stores an object that is being deserialized from a SOAP-encoded multiRef element for later access through the <see cref="M:System.Xml.Serialization.XmlSerializationReader.GetTarget(System.String)" /> method. </summary>
		/// <param name="id">The value of the id attribute of a multiRef element that identifies the element.</param>
		/// <param name="o">The object that is deserialized from the XML element.</param>
		// Token: 0x06001BD7 RID: 7127 RVA: 0x00093DF8 File Offset: 0x00091FF8
		protected void AddTarget(string id, object o)
		{
			if (id != null)
			{
				this.targets = this.EnsureHashtable(this.targets);
				if (this.targets[id] == null)
				{
					this.targets.Add(id, o);
				}
			}
			else
			{
				if (o != null)
				{
					return;
				}
				this.noIDTargets = this.EnsureArrayList(this.noIDTargets);
				this.noIDTargets.Add(o);
			}
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x00093E68 File Offset: 0x00092068
		private string CurrentTag()
		{
			XmlNodeType nodeType = this.reader.NodeType;
			switch (nodeType)
			{
			case XmlNodeType.Element:
				return string.Format("<{0} xmlns='{1}'>", this.reader.LocalName, this.reader.NamespaceURI);
			case XmlNodeType.Attribute:
				return this.reader.Value;
			case XmlNodeType.Text:
				return "CDATA";
			default:
				if (nodeType != XmlNodeType.EndElement)
				{
					return "(unknown)";
				}
				return ">";
			case XmlNodeType.Entity:
				return "<?";
			case XmlNodeType.ProcessingInstruction:
				return "<--";
			}
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates that an object being deserialized cannot be instantiated because the constructor throws a security exception.</summary>
		/// <returns>An <see cref="T:System.InvalidOperationException" /> exception.</returns>
		/// <param name="typeName">The name of the type.</param>
		// Token: 0x06001BD9 RID: 7129 RVA: 0x00093EFC File Offset: 0x000920FC
		protected Exception CreateCtorHasSecurityException(string typeName)
		{
			string message = string.Format("The type '{0}' cannot be serialized because its parameterless constructor is decorated with declarative security permission attributes. Consider using imperative asserts or demands in the constructor.", typeName);
			return new InvalidOperationException(message);
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates that an object being deserialized cannot be instantiated because there is no constructor available.</summary>
		/// <returns>An <see cref="T:System.InvalidOperationException" /> exception.</returns>
		/// <param name="typeName">The name of the type.</param>
		// Token: 0x06001BDA RID: 7130 RVA: 0x00093F1C File Offset: 0x0009211C
		protected Exception CreateInaccessibleConstructorException(string typeName)
		{
			string message = string.Format("{0} cannot be serialized because it does not have a default public constructor.", typeName);
			return new InvalidOperationException(message);
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates that an object being deserialized should be abstract. </summary>
		/// <returns>An <see cref="T:System.InvalidOperationException" /> exception.</returns>
		/// <param name="name">The name of the abstract type.</param>
		/// <param name="ns">The .NET Framework namespace of the abstract type.</param>
		// Token: 0x06001BDB RID: 7131 RVA: 0x00093F3C File Offset: 0x0009213C
		protected Exception CreateAbstractTypeException(string name, string ns)
		{
			string message = string.Concat(new string[]
			{
				"The specified type is abstrace: name='",
				name,
				"' namespace='",
				ns,
				"', at ",
				this.CurrentTag()
			});
			return new InvalidOperationException(message);
		}

		/// <summary>Creates an <see cref="T:System.InvalidCastException" /> that indicates that an explicit reference conversion failed.</summary>
		/// <returns>An <see cref="T:System.InvalidCastException" /> exception.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> that an object cannot be cast to. This type is incorporated into the exception message.</param>
		/// <param name="value">The object that cannot be cast. This object is incorporated into the exception message.</param>
		// Token: 0x06001BDC RID: 7132 RVA: 0x00093F84 File Offset: 0x00092184
		protected Exception CreateInvalidCastException(Type type, object value)
		{
			string message = string.Format(CultureInfo.InvariantCulture, "Cannot assign object of type {0} to an object of type {1}.", new object[]
			{
				value.GetType(),
				type
			});
			return new InvalidCastException(message);
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates that a SOAP-encoded collection type cannot be modified and its values cannot be filled in. </summary>
		/// <returns>An <see cref="T:System.InvalidOperationException" /> exception.</returns>
		/// <param name="name">The fully qualified name of the .NET Framework type for which there is a mapping.</param>
		// Token: 0x06001BDD RID: 7133 RVA: 0x00093FBC File Offset: 0x000921BC
		protected Exception CreateReadOnlyCollectionException(string name)
		{
			string message = string.Format("Could not serialize {0}. Default constructors are required for collections and enumerators.", name);
			return new InvalidOperationException(message);
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates that an enumeration value is not valid. </summary>
		/// <returns>An <see cref="T:System.InvalidOperationException" /> exception.</returns>
		/// <param name="value">The enumeration value that is not valid.</param>
		/// <param name="enumType">The enumeration type.</param>
		// Token: 0x06001BDE RID: 7134 RVA: 0x00093FDC File Offset: 0x000921DC
		protected Exception CreateUnknownConstantException(string value, Type enumType)
		{
			string message = string.Format("'{0}' is not a valid value for {1}.", value, enumType);
			return new InvalidOperationException(message);
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates that the current position of <see cref="T:System.Xml.XmlReader" /> represents an unknown XML node. </summary>
		/// <returns>An <see cref="T:System.InvalidOperationException" /> exception.</returns>
		// Token: 0x06001BDF RID: 7135 RVA: 0x00093FFC File Offset: 0x000921FC
		protected Exception CreateUnknownNodeException()
		{
			string message = this.CurrentTag() + " was not expected";
			return new InvalidOperationException(message);
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates that a type is unknown. </summary>
		/// <returns>An <see cref="T:System.InvalidOperationException" /> exception.</returns>
		/// <param name="type">An <see cref="T:System.Xml.XmlQualifiedName" /> that represents the name of the unknown type.</param>
		// Token: 0x06001BE0 RID: 7136 RVA: 0x00094020 File Offset: 0x00092220
		protected Exception CreateUnknownTypeException(XmlQualifiedName type)
		{
			string message = string.Concat(new string[]
			{
				"The specified type was not recognized: name='",
				type.Name,
				"' namespace='",
				type.Namespace,
				"', at ",
				this.CurrentTag()
			});
			return new InvalidOperationException(message);
		}

		/// <summary>Checks whether the deserializer has advanced.</summary>
		/// <param name="whileIterations">The current count in a while loop.</param>
		/// <param name="readerCount">The current <see cref="P:System.Xml.Serialization.XmlSerializationReader.ReaderCount" />. </param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Xml.Serialization.XmlSerializationReader.ReaderCount" /> has not advanced. </exception>
		// Token: 0x06001BE1 RID: 7137 RVA: 0x00094074 File Offset: 0x00092274
		protected void CheckReaderCount(ref int whileIterations, ref int readerCount)
		{
			whileIterations = this.whileIterationCount;
			readerCount = this.readCount;
		}

		/// <summary>Ensures that a given array, or a copy, is large enough to contain a specified index. </summary>
		/// <returns>The existing <see cref="T:System.Array" />, if it is already large enough; otherwise, a new, larger array that contains the original array's elements.</returns>
		/// <param name="a">The <see cref="T:System.Array" /> that is being checked.</param>
		/// <param name="index">The required index.</param>
		/// <param name="elementType">The <see cref="T:System.Type" /> of the array's elements.</param>
		// Token: 0x06001BE2 RID: 7138 RVA: 0x00094088 File Offset: 0x00092288
		protected Array EnsureArrayIndex(Array a, int index, Type elementType)
		{
			if (a != null && index < a.Length)
			{
				return a;
			}
			int length;
			if (a == null)
			{
				length = 32;
			}
			else
			{
				length = a.Length * 2;
			}
			Array array = Array.CreateInstance(elementType, length);
			if (a != null)
			{
				Array.Copy(a, array, index);
			}
			return array;
		}

		/// <summary>Fills in the values of a SOAP-encoded array whose data type maps to a .NET Framework reference type. </summary>
		/// <param name="fixup">An object that contains the array whose values are filled in.</param>
		// Token: 0x06001BE3 RID: 7139 RVA: 0x000940D8 File Offset: 0x000922D8
		[MonoTODO]
		protected void FixupArrayRefs(object fixup)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the length of the SOAP-encoded array where the <see cref="T:System.Xml.XmlReader" /> is currently positioned. </summary>
		/// <returns>The length of the SOAP array.</returns>
		/// <param name="name">The local name that the array should have.</param>
		/// <param name="ns">The namespace that the array should have.</param>
		// Token: 0x06001BE4 RID: 7140 RVA: 0x000940E0 File Offset: 0x000922E0
		[MonoTODO]
		protected int GetArrayLength(string name, string ns)
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines whether the XML element where the <see cref="T:System.Xml.XmlReader" /> is currently positioned has a null attribute set to the value true.</summary>
		/// <returns>true if <see cref="T:System.Xml.XmlReader" /> is currently positioned over a null attribute with the value true; otherwise, false.</returns>
		// Token: 0x06001BE5 RID: 7141 RVA: 0x000940E8 File Offset: 0x000922E8
		protected bool GetNullAttr()
		{
			string attribute = this.reader.GetAttribute(this.nullX, this.w3InstanceNS);
			if (attribute == null)
			{
				attribute = this.reader.GetAttribute(this.nil, this.w3InstanceNS);
				if (attribute == null)
				{
					attribute = this.reader.GetAttribute(this.nullX, this.w3InstanceNS2000);
					if (attribute == null)
					{
						attribute = this.reader.GetAttribute(this.nullX, this.w3InstanceNS1999);
					}
				}
			}
			return attribute != null;
		}

		/// <summary>Gets an object that is being deserialized from a SOAP-encoded multiRef element and that was stored earlier by <see cref="M:System.Xml.Serialization.XmlSerializationReader.AddTarget(System.String,System.Object)" />.  </summary>
		/// <returns>An object to be deserialized from a SOAP-encoded multiRef element.</returns>
		/// <param name="id">The value of the id attribute of a multiRef element that identifies the element.</param>
		// Token: 0x06001BE6 RID: 7142 RVA: 0x00094170 File Offset: 0x00092370
		protected object GetTarget(string id)
		{
			if (this.targets == null)
			{
				return null;
			}
			object obj = this.targets[id];
			if (obj != null)
			{
				if (this.referencedObjects == null)
				{
					this.referencedObjects = new Hashtable();
				}
				this.referencedObjects[obj] = obj;
			}
			return obj;
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x000941C4 File Offset: 0x000923C4
		private bool TargetReady(string id)
		{
			return this.targets != null && this.targets.ContainsKey(id);
		}

		/// <summary>Gets the value of the xsi:type attribute for the XML element at the current location of the <see cref="T:System.Xml.XmlReader" />. </summary>
		/// <returns>An XML qualified name that indicates the data type of an XML element.</returns>
		// Token: 0x06001BE8 RID: 7144 RVA: 0x000941E0 File Offset: 0x000923E0
		protected XmlQualifiedName GetXsiType()
		{
			string attribute = this.Reader.GetAttribute(this.typeX, "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute == string.Empty || attribute == null)
			{
				attribute = this.Reader.GetAttribute(this.typeX, this.w3InstanceNS1999);
				if (attribute == string.Empty || attribute == null)
				{
					attribute = this.Reader.GetAttribute(this.typeX, this.w3InstanceNS2000);
					if (attribute == string.Empty || attribute == null)
					{
						return null;
					}
				}
			}
			int num = attribute.IndexOf(":");
			if (num == -1)
			{
				return new XmlQualifiedName(attribute, this.Reader.NamespaceURI);
			}
			string prefix = attribute.Substring(0, num);
			string name = attribute.Substring(num + 1);
			return new XmlQualifiedName(name, this.Reader.LookupNamespace(prefix));
		}

		/// <summary>Initializes callback methods that populate objects that map to SOAP-encoded XML data. </summary>
		// Token: 0x06001BE9 RID: 7145
		protected abstract void InitCallbacks();

		/// <summary>Stores element and attribute names in a <see cref="T:System.Xml.NameTable" /> object. </summary>
		// Token: 0x06001BEA RID: 7146
		protected abstract void InitIDs();

		/// <summary>Determines whether an XML attribute name indicates an XML namespace. </summary>
		/// <returns>true if the XML attribute name indicates an XML namespace; otherwise, false.</returns>
		/// <param name="name">The name of an XML attribute.</param>
		// Token: 0x06001BEB RID: 7147 RVA: 0x000942C4 File Offset: 0x000924C4
		protected bool IsXmlnsAttribute(string name)
		{
			int length = name.Length;
			if (length < 5)
			{
				return false;
			}
			if (length == 5)
			{
				return name == "xmlns";
			}
			return name.StartsWith("xmlns:");
		}

		/// <summary>Sets the value of the XML attribute if it is of type arrayType from the Web Services Description Language (WSDL) namespace. </summary>
		/// <param name="attr">An <see cref="T:System.Xml.XmlAttribute" /> that may have the type wsdl:array.</param>
		// Token: 0x06001BEC RID: 7148 RVA: 0x00094300 File Offset: 0x00092500
		protected void ParseWsdlArrayType(XmlAttribute attr)
		{
			if (attr.NamespaceURI == this.wsdlNS && attr.LocalName == this.arrayType)
			{
				string text = string.Empty;
				string str;
				string str2;
				TypeTranslator.ParseArrayType(attr.Value, out str, out text, out str2);
				if (text != string.Empty)
				{
					text = this.Reader.LookupNamespace(text) + ":";
				}
				attr.Value = text + str + str2;
			}
		}

		/// <summary>Makes the <see cref="T:System.Xml.XmlReader" /> read the fully qualified name of the element where it is currently positioned. </summary>
		/// <returns>The fully qualified name of the current XML element.</returns>
		// Token: 0x06001BED RID: 7149 RVA: 0x00094388 File Offset: 0x00092588
		protected XmlQualifiedName ReadElementQualifiedName()
		{
			this.readCount++;
			if (this.reader.IsEmptyElement)
			{
				this.reader.Skip();
				return this.ToXmlQualifiedName(string.Empty);
			}
			this.reader.ReadStartElement();
			XmlQualifiedName result = this.ToXmlQualifiedName(this.reader.ReadString());
			this.reader.ReadEndElement();
			return result;
		}

		/// <summary>Makes the <see cref="T:System.Xml.XmlReader" /> read an XML end tag. </summary>
		// Token: 0x06001BEE RID: 7150 RVA: 0x000943F4 File Offset: 0x000925F4
		protected void ReadEndElement()
		{
			this.readCount++;
			while (this.reader.NodeType == XmlNodeType.Whitespace)
			{
				this.reader.Skip();
			}
			if (this.reader.NodeType != XmlNodeType.None)
			{
				this.reader.ReadEndElement();
			}
			else
			{
				this.reader.Skip();
			}
		}

		/// <summary>Instructs the <see cref="T:System.Xml.XmlReader" /> to read the current XML element if the element has a null attribute with the value true. </summary>
		/// <returns>true if the element has a null="true" attribute value and has been read; otherwise, false.</returns>
		// Token: 0x06001BEF RID: 7151 RVA: 0x0009445C File Offset: 0x0009265C
		protected bool ReadNull()
		{
			if (!this.GetNullAttr())
			{
				return false;
			}
			this.readCount++;
			if (this.reader.IsEmptyElement)
			{
				this.reader.Skip();
				return true;
			}
			this.reader.ReadStartElement();
			while (this.reader.NodeType != XmlNodeType.EndElement)
			{
				this.UnknownNode(null);
			}
			this.ReadEndElement();
			return true;
		}

		/// <summary>Instructs the <see cref="T:System.Xml.XmlReader" /> to read the fully qualified name of the element where it is currently positioned. </summary>
		/// <returns>A <see cref="T:System.Xml.XmlQualifiedName" /> that represents the fully qualified name of the current XML element; otherwise, null if a null="true" attribute value is present.</returns>
		// Token: 0x06001BF0 RID: 7152 RVA: 0x000944D4 File Offset: 0x000926D4
		protected XmlQualifiedName ReadNullableQualifiedName()
		{
			if (this.ReadNull())
			{
				return null;
			}
			return this.ReadElementQualifiedName();
		}

		/// <summary>Instructs the <see cref="T:System.Xml.XmlReader" /> to read a simple, text-only XML element that could be null. </summary>
		/// <returns>The string value; otherwise, null.</returns>
		// Token: 0x06001BF1 RID: 7153 RVA: 0x000944EC File Offset: 0x000926EC
		protected string ReadNullableString()
		{
			if (this.ReadNull())
			{
				return null;
			}
			this.readCount++;
			return this.reader.ReadElementString();
		}

		/// <summary>Reads the value of the href attribute (ref attribute for SOAP 1.2) that is used to refer to an XML element in SOAP encoding. </summary>
		/// <returns>true if the value was read; otherwise, false.</returns>
		/// <param name="fixupReference">An output string into which the href attribute value is read.</param>
		// Token: 0x06001BF2 RID: 7154 RVA: 0x00094520 File Offset: 0x00092720
		protected bool ReadReference(out string fixupReference)
		{
			string attribute = this.reader.GetAttribute("href");
			if (attribute == null)
			{
				fixupReference = null;
				return false;
			}
			if (attribute[0] != '#')
			{
				throw new InvalidOperationException("href not found: " + attribute);
			}
			fixupReference = attribute.Substring(1);
			this.readCount++;
			if (!this.reader.IsEmptyElement)
			{
				this.reader.ReadStartElement();
				this.ReadEndElement();
			}
			else
			{
				this.reader.Skip();
			}
			return true;
		}

		/// <summary>Deserializes an object from a SOAP-encoded multiRef XML element. </summary>
		/// <returns>The value of the referenced element in the document.</returns>
		// Token: 0x06001BF3 RID: 7155 RVA: 0x000945B4 File Offset: 0x000927B4
		protected object ReadReferencedElement()
		{
			return this.ReadReferencedElement(this.Reader.LocalName, this.Reader.NamespaceURI);
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x000945E0 File Offset: 0x000927E0
		private XmlSerializationReader.WriteCallbackInfo GetCallbackInfo(XmlQualifiedName qname)
		{
			if (this.typesCallbacks == null)
			{
				this.typesCallbacks = new Hashtable();
				this.InitCallbacks();
			}
			return (XmlSerializationReader.WriteCallbackInfo)this.typesCallbacks[qname];
		}

		/// <summary>Deserializes an object from a SOAP-encoded multiRef XML element. </summary>
		/// <returns>The value of the referenced element in the document.</returns>
		/// <param name="name">The local name of the element's XML Schema data type.</param>
		/// <param name="ns">The namespace of the element's XML Schema data type.</param>
		// Token: 0x06001BF5 RID: 7157 RVA: 0x00094610 File Offset: 0x00092810
		protected object ReadReferencedElement(string name, string ns)
		{
			XmlQualifiedName xmlQualifiedName = this.GetXsiType();
			if (xmlQualifiedName == null)
			{
				xmlQualifiedName = new XmlQualifiedName(name, ns);
			}
			string attribute = this.Reader.GetAttribute("id");
			string attribute2 = this.Reader.GetAttribute(this.arrayType, this.soapNS);
			object obj;
			if (xmlQualifiedName == this.arrayQName || (attribute2 != null && attribute2.Length > 0))
			{
				XmlSerializationReader.CollectionFixup collectionFixup = (this.collFixups == null) ? null : ((XmlSerializationReader.CollectionFixup)this.collFixups[attribute]);
				if (this.ReadList(out obj))
				{
					if (collectionFixup != null)
					{
						collectionFixup.Callback(collectionFixup.Collection, obj);
						this.collFixups.Remove(attribute);
						obj = collectionFixup.Collection;
					}
				}
				else if (collectionFixup != null)
				{
					collectionFixup.CollectionItems = (object[])obj;
					obj = collectionFixup.Collection;
				}
			}
			else
			{
				XmlSerializationReader.WriteCallbackInfo callbackInfo = this.GetCallbackInfo(xmlQualifiedName);
				if (callbackInfo == null)
				{
					obj = this.ReadTypedPrimitive(xmlQualifiedName, attribute != null);
				}
				else
				{
					obj = callbackInfo.Callback();
				}
			}
			this.AddTarget(attribute, obj);
			return obj;
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x00094744 File Offset: 0x00092944
		private bool ReadList(out object resultList)
		{
			string attribute = this.Reader.GetAttribute(this.arrayType, this.soapNS);
			if (attribute == null)
			{
				attribute = this.Reader.GetAttribute(this.arrayType, this.wsdlNS);
			}
			XmlQualifiedName xmlQualifiedName = this.ToXmlQualifiedName(attribute);
			int num = xmlQualifiedName.Name.LastIndexOf('[');
			string text = xmlQualifiedName.Name.Substring(num);
			string text2 = xmlQualifiedName.Name.Substring(0, num);
			int num2 = int.Parse(text.Substring(1, text.Length - 2), CultureInfo.InvariantCulture);
			num = text2.IndexOf('[');
			if (num == -1)
			{
				num = text2.Length;
			}
			string text3 = text2.Substring(0, num);
			string typeName;
			if (xmlQualifiedName.Namespace == this.w3SchemaNS)
			{
				typeName = TypeTranslator.GetPrimitiveTypeData(text3).Type.FullName + text2.Substring(num);
			}
			else
			{
				XmlSerializationReader.WriteCallbackInfo callbackInfo = this.GetCallbackInfo(new XmlQualifiedName(text3, xmlQualifiedName.Namespace));
				typeName = callbackInfo.Type.FullName + text2.Substring(num) + ", " + callbackInfo.Type.Assembly.FullName;
			}
			Array array = Array.CreateInstance(Type.GetType(typeName), num2);
			bool result = true;
			if (this.Reader.IsEmptyElement)
			{
				this.readCount++;
				this.Reader.Skip();
			}
			else
			{
				this.Reader.ReadStartElement();
				for (int i = 0; i < num2; i++)
				{
					this.whileIterationCount++;
					this.readCount++;
					this.Reader.MoveToContent();
					string text4;
					object value = this.ReadReferencingElement(text2, xmlQualifiedName.Namespace, out text4);
					if (text4 == null)
					{
						array.SetValue(value, i);
					}
					else
					{
						this.AddFixup(new XmlSerializationReader.CollectionItemFixup(array, i, text4));
						result = false;
					}
				}
				this.whileIterationCount = 0;
				this.Reader.ReadEndElement();
			}
			resultList = array;
			return result;
		}

		/// <summary>Deserializes objects from the SOAP-encoded multiRef elements in a SOAP message. </summary>
		// Token: 0x06001BF7 RID: 7159 RVA: 0x0009495C File Offset: 0x00092B5C
		protected void ReadReferencedElements()
		{
			this.reader.MoveToContent();
			XmlNodeType nodeType = this.reader.NodeType;
			while (nodeType != XmlNodeType.EndElement && nodeType != XmlNodeType.None)
			{
				this.whileIterationCount++;
				this.readCount++;
				this.ReadReferencedElement();
				this.reader.MoveToContent();
				nodeType = this.reader.NodeType;
			}
			this.whileIterationCount = 0;
			if (this.delayedListFixups != null)
			{
				foreach (object obj in this.delayedListFixups)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					this.AddTarget((string)dictionaryEntry.Key, dictionaryEntry.Value);
				}
			}
			if (this.collItemFixups != null)
			{
				foreach (object obj2 in this.collItemFixups)
				{
					XmlSerializationReader.CollectionItemFixup collectionItemFixup = (XmlSerializationReader.CollectionItemFixup)obj2;
					collectionItemFixup.Collection.SetValue(this.GetTarget(collectionItemFixup.Id), collectionItemFixup.Index);
				}
			}
			if (this.collFixups != null)
			{
				ICollection values = this.collFixups.Values;
				foreach (object obj3 in values)
				{
					XmlSerializationReader.CollectionFixup collectionFixup = (XmlSerializationReader.CollectionFixup)obj3;
					collectionFixup.Callback(collectionFixup.Collection, collectionFixup.CollectionItems);
				}
			}
			if (this.fixups != null)
			{
				foreach (object obj4 in this.fixups)
				{
					XmlSerializationReader.Fixup fixup = (XmlSerializationReader.Fixup)obj4;
					fixup.Callback(fixup);
				}
			}
			if (this.targets != null)
			{
				foreach (object obj5 in this.targets)
				{
					DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj5;
					if (dictionaryEntry2.Value != null && (this.referencedObjects == null || !this.referencedObjects.Contains(dictionaryEntry2.Value)))
					{
						this.UnreferencedObject((string)dictionaryEntry2.Key, dictionaryEntry2.Value);
					}
				}
			}
		}

		/// <summary>Deserializes an object from an XML element in a SOAP message that contains a reference to a multiRef element. </summary>
		/// <returns>The deserialized object.</returns>
		/// <param name="fixupReference">An output string into which the href attribute value is read.</param>
		// Token: 0x06001BF8 RID: 7160 RVA: 0x00094C94 File Offset: 0x00092E94
		protected object ReadReferencingElement(out string fixupReference)
		{
			return this.ReadReferencingElement(this.Reader.LocalName, this.Reader.NamespaceURI, false, out fixupReference);
		}

		/// <summary>Deserializes an object from an XML element in a SOAP message that contains a reference to a multiRef element. </summary>
		/// <returns>The deserialized object.</returns>
		/// <param name="name">The local name of the element's XML Schema data type.</param>
		/// <param name="ns">The namespace of the element's XML Schema data type.</param>
		/// <param name="fixupReference">An output string into which the href attribute value is read.</param>
		// Token: 0x06001BF9 RID: 7161 RVA: 0x00094CC0 File Offset: 0x00092EC0
		protected object ReadReferencingElement(string name, string ns, out string fixupReference)
		{
			return this.ReadReferencingElement(name, ns, false, out fixupReference);
		}

		/// <summary>Deserializes an object from an XML element in a SOAP message that contains a reference to a multiRef element.</summary>
		/// <returns>The deserialized object.</returns>
		/// <param name="name">The local name of the element's XML Schema data type.</param>
		/// <param name="ns">The namespace of the element's XML Schema data type.</param>
		/// <param name="elementCanBeType">true if the element name is also the XML Schema data type name; otherwise, false.</param>
		/// <param name="fixupReference">An output string into which the value of the href attribute is read.</param>
		// Token: 0x06001BFA RID: 7162 RVA: 0x00094CCC File Offset: 0x00092ECC
		protected object ReadReferencingElement(string name, string ns, bool elementCanBeType, out string fixupReference)
		{
			if (this.ReadNull())
			{
				fixupReference = null;
				return null;
			}
			string text = this.Reader.GetAttribute("href");
			if (text == string.Empty || text == null)
			{
				fixupReference = null;
				XmlQualifiedName xmlQualifiedName = this.GetXsiType();
				if (xmlQualifiedName == null)
				{
					xmlQualifiedName = new XmlQualifiedName(name, ns);
				}
				string attribute = this.Reader.GetAttribute(this.arrayType, this.soapNS);
				if (xmlQualifiedName == this.arrayQName || attribute != null)
				{
					this.delayedListFixups = this.EnsureHashtable(this.delayedListFixups);
					fixupReference = "__<" + this.delayedFixupId++ + ">";
					object value;
					this.ReadList(out value);
					this.delayedListFixups[fixupReference] = value;
					return null;
				}
				XmlSerializationReader.WriteCallbackInfo callbackInfo = this.GetCallbackInfo(xmlQualifiedName);
				if (callbackInfo == null)
				{
					return this.ReadTypedPrimitive(xmlQualifiedName, true);
				}
				return callbackInfo.Callback();
			}
			else
			{
				if (text.StartsWith("#"))
				{
					text = text.Substring(1);
				}
				this.readCount++;
				this.Reader.Skip();
				if (this.TargetReady(text))
				{
					fixupReference = null;
					return this.GetTarget(text);
				}
				fixupReference = text;
				return null;
			}
		}

		/// <summary>Populates an object from its XML representation at the current location of the <see cref="T:System.Xml.XmlReader" />. </summary>
		/// <returns>An object that implements the <see cref="T:System.Xml.Serialization.IXmlSerializable" /> interface with its members populated from the location of the <see cref="T:System.Xml.XmlReader" />.</returns>
		/// <param name="serializable">An <see cref="T:System.Xml.Serialization.IXmlSerializable" /> that corresponds to the current position of the <see cref="T:System.Xml.XmlReader" />.</param>
		// Token: 0x06001BFB RID: 7163 RVA: 0x00094E2C File Offset: 0x0009302C
		protected IXmlSerializable ReadSerializable(IXmlSerializable serializable)
		{
			if (this.ReadNull())
			{
				return null;
			}
			int depth = this.reader.Depth;
			this.readCount++;
			serializable.ReadXml(this.reader);
			this.Reader.MoveToContent();
			while (this.reader.Depth > depth)
			{
				this.reader.Skip();
			}
			if (this.reader.Depth == depth && this.reader.NodeType == XmlNodeType.EndElement)
			{
				this.reader.ReadEndElement();
			}
			return serializable;
		}

		/// <summary>Produces the result of a call to the <see cref="M:System.Xml.XmlReader.ReadString" /> method appended to the input value. </summary>
		/// <returns>The result of call to the <see cref="M:System.Xml.XmlReader.ReadString" /> method appended to the input value.</returns>
		/// <param name="value">A string to prefix to the result of a call to the <see cref="M:System.Xml.XmlReader.ReadString" /> method.</param>
		// Token: 0x06001BFC RID: 7164 RVA: 0x00094EC8 File Offset: 0x000930C8
		protected string ReadString(string value)
		{
			this.readCount++;
			if (value == null || value == string.Empty)
			{
				return this.reader.ReadString();
			}
			return value + this.reader.ReadString();
		}

		/// <summary>Gets the value of the XML node at which the <see cref="T:System.Xml.XmlReader" /> is currently positioned. </summary>
		/// <returns>The value of the node as a .NET Framework value type, if the value is a simple XML Schema data type.</returns>
		/// <param name="type">The <see cref="T:System.Xml.XmlQualifiedName" /> that represents the simple data type for the current location of the <see cref="T:System.Xml.XmlReader" />.</param>
		// Token: 0x06001BFD RID: 7165 RVA: 0x00094F18 File Offset: 0x00093118
		protected object ReadTypedPrimitive(XmlQualifiedName qname)
		{
			return this.ReadTypedPrimitive(qname, false);
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x00094F24 File Offset: 0x00093124
		private object ReadTypedPrimitive(XmlQualifiedName qname, bool reportUnknown)
		{
			if (qname == null)
			{
				qname = this.GetXsiType();
			}
			TypeData typeData = TypeTranslator.FindPrimitiveTypeData(qname.Name);
			if (typeData == null || typeData.SchemaType != SchemaTypes.Primitive)
			{
				this.readCount++;
				XmlNode xmlNode = this.Document.ReadNode(this.reader);
				if (reportUnknown)
				{
					this.OnUnknownNode(xmlNode, null, null);
				}
				if (xmlNode.ChildNodes.Count == 0 && xmlNode.Attributes.Count == 0)
				{
					return new object();
				}
				XmlElement xmlElement = xmlNode as XmlElement;
				if (xmlElement == null)
				{
					return new XmlNode[]
					{
						xmlNode
					};
				}
				XmlNode[] array = new XmlNode[xmlElement.Attributes.Count + xmlElement.ChildNodes.Count];
				int num = 0;
				foreach (object obj in xmlElement.Attributes)
				{
					XmlNode xmlNode2 = (XmlNode)obj;
					array[num++] = xmlNode2;
				}
				foreach (object obj2 in xmlElement.ChildNodes)
				{
					XmlNode xmlNode3 = (XmlNode)obj2;
					array[num++] = xmlNode3;
				}
				return array;
			}
			else
			{
				if (typeData.Type == typeof(XmlQualifiedName))
				{
					return this.ReadNullableQualifiedName();
				}
				this.readCount++;
				return XmlCustomFormatter.FromXmlString(typeData, this.Reader.ReadElementString());
			}
		}

		/// <summary>Instructs the <see cref="T:System.Xml.XmlReader" /> to read the XML node at its current position. </summary>
		/// <returns>An <see cref="T:System.Xml.XmlNode" /> that represents the XML node that has been read.</returns>
		/// <param name="wrapped">true to read content only after reading the element's start element; otherwise, false.</param>
		// Token: 0x06001BFF RID: 7167 RVA: 0x00095108 File Offset: 0x00093308
		protected XmlNode ReadXmlNode(bool wrapped)
		{
			this.readCount++;
			XmlNode xmlNode = this.Document.ReadNode(this.reader);
			if (wrapped)
			{
				return xmlNode.FirstChild;
			}
			return xmlNode;
		}

		/// <summary>Instructs the <see cref="T:System.Xml.XmlReader" /> to read an XML document root element at its current position.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlDocument" /> that contains the root element that has been read.</returns>
		/// <param name="wrapped">true if the method should read content only after reading the element's start element; otherwise, false.</param>
		// Token: 0x06001C00 RID: 7168 RVA: 0x00095144 File Offset: 0x00093344
		protected XmlDocument ReadXmlDocument(bool wrapped)
		{
			this.readCount++;
			if (wrapped)
			{
				this.reader.ReadStartElement();
			}
			this.reader.MoveToContent();
			XmlDocument xmlDocument = new XmlDocument();
			XmlNode newChild = xmlDocument.ReadNode(this.reader);
			xmlDocument.AppendChild(newChild);
			if (wrapped)
			{
				this.reader.ReadEndElement();
			}
			return xmlDocument;
		}

		/// <summary>Stores an object to be deserialized from a SOAP-encoded multiRef element.</summary>
		/// <param name="o">The object to be deserialized.</param>
		// Token: 0x06001C01 RID: 7169 RVA: 0x000951AC File Offset: 0x000933AC
		protected void Referenced(object o)
		{
			if (o != null)
			{
				if (this.referencedObjects == null)
				{
					this.referencedObjects = new Hashtable();
				}
				this.referencedObjects[o] = o;
			}
		}

		/// <summary>Ensures that a given array, or a copy, is no larger than a specified length. </summary>
		/// <returns>The existing <see cref="T:System.Array" />, if it is already small enough; otherwise, a new, smaller array that contains the original array's elements up to the size of<paramref name=" length" />.</returns>
		/// <param name="a">The array that is being checked.</param>
		/// <param name="length">The maximum length of the array.</param>
		/// <param name="elementType">The <see cref="T:System.Type" /> of the array's elements.</param>
		/// <param name="isNullable">true if null for the array, if present for the input array, can be returned; otherwise, a new, smaller array.</param>
		// Token: 0x06001C02 RID: 7170 RVA: 0x000951D8 File Offset: 0x000933D8
		protected Array ShrinkArray(Array a, int length, Type elementType, bool isNullable)
		{
			if (length == 0 && isNullable)
			{
				return null;
			}
			if (a == null)
			{
				return Array.CreateInstance(elementType, length);
			}
			if (a.Length == length)
			{
				return a;
			}
			Array array = Array.CreateInstance(elementType, length);
			Array.Copy(a, array, length);
			return array;
		}

		/// <summary>Instructs the <see cref="T:System.Xml.XmlReader" /> to read the string value at its current position and return it as a base-64 byte array.</summary>
		/// <returns>A base-64 byte array; otherwise, null if the value of the <paramref name="isNull" /> parameter is true.</returns>
		/// <param name="isNull">true to return null; false to return a base-64 byte array.</param>
		// Token: 0x06001C03 RID: 7171 RVA: 0x00095224 File Offset: 0x00093424
		protected byte[] ToByteArrayBase64(bool isNull)
		{
			this.readCount++;
			if (isNull)
			{
				this.Reader.ReadString();
				return null;
			}
			return XmlSerializationReader.ToByteArrayBase64(this.Reader.ReadString());
		}

		/// <summary>Produces a base-64 byte array from an input string. </summary>
		/// <returns>A base-64 byte array.</returns>
		/// <param name="value">A string to translate into a base-64 byte array.</param>
		// Token: 0x06001C04 RID: 7172 RVA: 0x00095264 File Offset: 0x00093464
		protected static byte[] ToByteArrayBase64(string value)
		{
			return Convert.FromBase64String(value);
		}

		/// <summary>Instructs the <see cref="T:System.Xml.XmlReader" /> to read the string value at its current position and return it as a hexadecimal byte array.</summary>
		/// <returns>A hexadecimal byte array; otherwise, null if the value of the <paramref name="isNull" /> parameter is true. </returns>
		/// <param name="isNull">true to return null; false to return a hexadecimal byte array.</param>
		// Token: 0x06001C05 RID: 7173 RVA: 0x0009526C File Offset: 0x0009346C
		protected byte[] ToByteArrayHex(bool isNull)
		{
			this.readCount++;
			if (isNull)
			{
				this.Reader.ReadString();
				return null;
			}
			return XmlSerializationReader.ToByteArrayHex(this.Reader.ReadString());
		}

		/// <summary>Produces a hexadecimal byte array from an input string.</summary>
		/// <returns>A hexadecimal byte array.</returns>
		/// <param name="value">A string to translate into a hexadecimal byte array.</param>
		// Token: 0x06001C06 RID: 7174 RVA: 0x000952AC File Offset: 0x000934AC
		protected static byte[] ToByteArrayHex(string value)
		{
			return XmlConvert.FromBinHexString(value);
		}

		/// <summary>Produces a <see cref="T:System.Char" /> object from an input string. </summary>
		/// <returns>A <see cref="T:System.Char" /> object.</returns>
		/// <param name="value">A string to translate into a <see cref="T:System.Char" /> object.</param>
		// Token: 0x06001C07 RID: 7175 RVA: 0x000952B4 File Offset: 0x000934B4
		protected static char ToChar(string value)
		{
			return XmlCustomFormatter.ToChar(value);
		}

		/// <summary>Produces a <see cref="T:System.DateTime" /> object from an input string. </summary>
		/// <returns>A <see cref="T:System.DateTime" />object.</returns>
		/// <param name="value">A string to translate into a <see cref="T:System.DateTime" /> class object.</param>
		// Token: 0x06001C08 RID: 7176 RVA: 0x000952BC File Offset: 0x000934BC
		protected static DateTime ToDate(string value)
		{
			return XmlCustomFormatter.ToDate(value);
		}

		/// <summary>Produces a <see cref="T:System.DateTime" /> object from an input string. </summary>
		/// <returns>A <see cref="T:System.DateTime" /> object.</returns>
		/// <param name="value">A string to translate into a <see cref="T:System.DateTime" /> object.</param>
		// Token: 0x06001C09 RID: 7177 RVA: 0x000952C4 File Offset: 0x000934C4
		protected static DateTime ToDateTime(string value)
		{
			return XmlCustomFormatter.ToDateTime(value);
		}

		/// <summary>Produces a numeric enumeration value from a string that consists of delimited identifiers that represent constants from the enumerator list. </summary>
		/// <returns>A long value that consists of the enumeration value as a series of bitwise OR operations.</returns>
		/// <param name="value">A string that consists of delimited identifiers where each identifier represents a constant from the set enumerator list.</param>
		/// <param name="h">A <see cref="T:System.Collections.Hashtable" /> that consists of the identifiers as keys and the constants as integral numbers.</param>
		/// <param name="typeName">The name of the enumeration type.</param>
		// Token: 0x06001C0A RID: 7178 RVA: 0x000952CC File Offset: 0x000934CC
		protected static long ToEnum(string value, Hashtable h, string typeName)
		{
			return XmlCustomFormatter.ToEnum(value, h, typeName, true);
		}

		/// <summary>Produces a <see cref="T:System.DateTime" /> from a string that represents the time. </summary>
		/// <returns>A <see cref="T:System.DateTime" /> object.</returns>
		/// <param name="value">A string to translate into a <see cref="T:System.DateTime" /> object.</param>
		// Token: 0x06001C0B RID: 7179 RVA: 0x000952D8 File Offset: 0x000934D8
		protected static DateTime ToTime(string value)
		{
			return XmlCustomFormatter.ToTime(value);
		}

		/// <summary>Decodes an XML name.</summary>
		/// <returns>A decoded string.</returns>
		/// <param name="value">An XML name to be decoded.</param>
		// Token: 0x06001C0C RID: 7180 RVA: 0x000952E0 File Offset: 0x000934E0
		protected static string ToXmlName(string value)
		{
			return XmlCustomFormatter.ToXmlName(value);
		}

		/// <summary>Decodes an XML name.</summary>
		/// <returns>A decoded string.</returns>
		/// <param name="value">An XML name to be decoded.</param>
		// Token: 0x06001C0D RID: 7181 RVA: 0x000952E8 File Offset: 0x000934E8
		protected static string ToXmlNCName(string value)
		{
			return XmlCustomFormatter.ToXmlNCName(value);
		}

		/// <summary>Decodes an XML name.</summary>
		/// <returns>A decoded string.</returns>
		/// <param name="value">An XML name to be decoded.</param>
		// Token: 0x06001C0E RID: 7182 RVA: 0x000952F0 File Offset: 0x000934F0
		protected static string ToXmlNmToken(string value)
		{
			return XmlCustomFormatter.ToXmlNmToken(value);
		}

		/// <summary>Decodes an XML name.</summary>
		/// <returns>A decoded string.</returns>
		/// <param name="value">An XML name to be decoded.</param>
		// Token: 0x06001C0F RID: 7183 RVA: 0x000952F8 File Offset: 0x000934F8
		protected static string ToXmlNmTokens(string value)
		{
			return XmlCustomFormatter.ToXmlNmTokens(value);
		}

		/// <summary>Obtains an <see cref="T:System.Xml.XmlQualifiedName" /> from a name that may contain a prefix. </summary>
		/// <returns>An <see cref="T:System.Xml.XmlQualifiedName" /> that represents a namespace-qualified XML name.</returns>
		/// <param name="value">A name that may contain a prefix.</param>
		// Token: 0x06001C10 RID: 7184 RVA: 0x00095300 File Offset: 0x00093500
		protected XmlQualifiedName ToXmlQualifiedName(string value)
		{
			int num = value.LastIndexOf(':');
			string name = XmlConvert.DecodeName(value);
			string name2;
			string text;
			if (num < 0)
			{
				name2 = this.reader.NameTable.Add(name);
				text = this.reader.LookupNamespace(string.Empty);
			}
			else
			{
				string text2 = value.Substring(0, num);
				text = this.reader.LookupNamespace(text2);
				if (text == null)
				{
					throw new InvalidOperationException("namespace " + text2 + " not defined");
				}
				name2 = this.reader.NameTable.Add(value.Substring(num + 1));
			}
			return new XmlQualifiedName(name2, text);
		}

		/// <summary>Raises an <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownAttribute" /> event for the current position of the <see cref="T:System.Xml.XmlReader" />. </summary>
		/// <param name="o">An object that the <see cref="T:System.Xml.Serialization.XmlSerializer" /> is attempting to deserialize, subsequently accessible through the <see cref="P:System.Xml.Serialization.XmlAttributeEventArgs.ObjectBeingDeserialized" /> property.</param>
		/// <param name="attr">An <see cref="T:System.Xml.XmlAttribute" /> that represents the attribute in question.</param>
		// Token: 0x06001C11 RID: 7185 RVA: 0x000953A4 File Offset: 0x000935A4
		protected void UnknownAttribute(object o, XmlAttribute attr)
		{
			this.UnknownAttribute(o, attr, null);
		}

		/// <summary>Raises an <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownAttribute" /> event for the current position of the <see cref="T:System.Xml.XmlReader" />. </summary>
		/// <param name="o">An object that the <see cref="T:System.Xml.Serialization.XmlSerializer" /> is attempting to deserialize, subsequently accessible through the <see cref="P:System.Xml.Serialization.XmlAttributeEventArgs.ObjectBeingDeserialized" /> property.</param>
		/// <param name="attr">A <see cref="T:System.Xml.XmlAttribute" /> that represents the attribute in question.</param>
		/// <param name="qnames">A comma-delimited list of XML qualified names.</param>
		// Token: 0x06001C12 RID: 7186 RVA: 0x000953B0 File Offset: 0x000935B0
		protected void UnknownAttribute(object o, XmlAttribute attr, string qnames)
		{
			int lineNum;
			int linePos;
			if (this.Reader is XmlTextReader)
			{
				lineNum = ((XmlTextReader)this.Reader).LineNumber;
				linePos = ((XmlTextReader)this.Reader).LinePosition;
			}
			else
			{
				lineNum = 0;
				linePos = 0;
			}
			XmlAttributeEventArgs xmlAttributeEventArgs = new XmlAttributeEventArgs(attr, lineNum, linePos, o);
			xmlAttributeEventArgs.ExpectedAttributes = qnames;
			if (this.eventSource != null)
			{
				this.eventSource.OnUnknownAttribute(xmlAttributeEventArgs);
			}
		}

		/// <summary>Raises an <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownElement" /> event for the current position of the <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="o">The <see cref="T:System.Object" /> that is being deserialized.</param>
		/// <param name="elem">The <see cref="T:System.Xml.XmlElement" /> for which an event is raised.</param>
		// Token: 0x06001C13 RID: 7187 RVA: 0x00095420 File Offset: 0x00093620
		protected void UnknownElement(object o, XmlElement elem)
		{
			this.UnknownElement(o, elem, null);
		}

		/// <summary>Raises an <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownElement" /> event for the current position of the <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="o">An object that the <see cref="T:System.Xml.Serialization.XmlSerializer" /> is attempting to deserialize, subsequently accessible through the <see cref="P:System.Xml.Serialization.XmlAttributeEventArgs.ObjectBeingDeserialized" /> property.</param>
		/// <param name="elem">The <see cref="T:System.Xml.XmlElement" /> for which an event is raised.</param>
		/// <param name="qnames">A comma-delimited list of XML qualified names.</param>
		// Token: 0x06001C14 RID: 7188 RVA: 0x0009542C File Offset: 0x0009362C
		protected void UnknownElement(object o, XmlElement elem, string qnames)
		{
			int lineNum;
			int linePos;
			if (this.Reader is XmlTextReader)
			{
				lineNum = ((XmlTextReader)this.Reader).LineNumber;
				linePos = ((XmlTextReader)this.Reader).LinePosition;
			}
			else
			{
				lineNum = 0;
				linePos = 0;
			}
			XmlElementEventArgs xmlElementEventArgs = new XmlElementEventArgs(elem, lineNum, linePos, o);
			xmlElementEventArgs.ExpectedElements = qnames;
			if (this.eventSource != null)
			{
				this.eventSource.OnUnknownElement(xmlElementEventArgs);
			}
		}

		/// <summary>Raises an <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownNode" /> event for the current position of the <see cref="T:System.Xml.XmlReader" />. </summary>
		/// <param name="o">The object that is being deserialized.</param>
		// Token: 0x06001C15 RID: 7189 RVA: 0x0009549C File Offset: 0x0009369C
		protected void UnknownNode(object o)
		{
			this.UnknownNode(o, null);
		}

		/// <summary>Raises an <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownNode" /> event for the current position of the <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="o">The object being deserialized.</param>
		/// <param name="qnames">A comma-delimited list of XML qualified names.</param>
		// Token: 0x06001C16 RID: 7190 RVA: 0x000954A8 File Offset: 0x000936A8
		protected void UnknownNode(object o, string qnames)
		{
			this.OnUnknownNode(this.ReadXmlNode(false), o, qnames);
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x000954BC File Offset: 0x000936BC
		private void OnUnknownNode(XmlNode node, object o, string qnames)
		{
			int linenumber;
			int lineposition;
			if (this.Reader is XmlTextReader)
			{
				linenumber = ((XmlTextReader)this.Reader).LineNumber;
				lineposition = ((XmlTextReader)this.Reader).LinePosition;
			}
			else
			{
				linenumber = 0;
				lineposition = 0;
			}
			if (node is XmlAttribute)
			{
				this.UnknownAttribute(o, (XmlAttribute)node, qnames);
				return;
			}
			if (node is XmlElement)
			{
				this.UnknownElement(o, (XmlElement)node, qnames);
				return;
			}
			if (this.eventSource != null)
			{
				this.eventSource.OnUnknownNode(new XmlNodeEventArgs(linenumber, lineposition, node.LocalName, node.Name, node.NamespaceURI, node.NodeType, o, node.Value));
			}
			if (this.Reader.ReadState == ReadState.EndOfFile)
			{
				throw new InvalidOperationException("End of document found");
			}
		}

		/// <summary>Raises an <see cref="E:System.Xml.Serialization.XmlSerializer.UnreferencedObject" /> event for the current position of the <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="id">A unique string that is used to identify the unreferenced object, subsequently accessible through the <see cref="P:System.Xml.Serialization.UnreferencedObjectEventArgs.UnreferencedId" /> property.</param>
		/// <param name="o">An object that the <see cref="T:System.Xml.Serialization.XmlSerializer" /> is attempting to deserialize, subsequently accessible through the <see cref="P:System.Xml.Serialization.UnreferencedObjectEventArgs.UnreferencedObject" /> property.</param>
		// Token: 0x06001C18 RID: 7192 RVA: 0x00095590 File Offset: 0x00093790
		protected void UnreferencedObject(string id, object o)
		{
			if (this.eventSource != null)
			{
				this.eventSource.OnUnreferencedObject(new UnreferencedObjectEventArgs(o, id));
			}
		}

		/// <summary>Gets or sets a value that determines whether XML strings are translated into valid .NET Framework type names.</summary>
		/// <returns>true if XML strings are decoded into valid .NET Framework type names; otherwise, false.</returns>
		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06001C19 RID: 7193 RVA: 0x000955B0 File Offset: 0x000937B0
		// (set) Token: 0x06001C1A RID: 7194 RVA: 0x000955B8 File Offset: 0x000937B8
		[MonoTODO]
		protected bool DecodeName
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Removes all occurrences of white space characters from the beginning and end of the specified string.</summary>
		/// <returns>The trimmed string.</returns>
		/// <param name="value">The string that will have its white space trimmed.</param>
		// Token: 0x06001C1B RID: 7195 RVA: 0x000955C0 File Offset: 0x000937C0
		[MonoTODO]
		protected string CollapseWhitespace(string value)
		{
			throw new NotImplementedException();
		}

		/// <summary>Populates an object from its XML representation at the current location of the <see cref="T:System.Xml.XmlReader" />, with an option to read the inner element.</summary>
		/// <returns>An <see cref="T:System.InvalidOperationException" /> exception.</returns>
		/// <param name="xsdDerived">The local name of the derived XML Schema data type.</param>
		/// <param name="nsDerived">The namespace of the derived XML Schema data type.</param>
		/// <param name="xsdBase">The local name of the base XML Schema data type.</param>
		/// <param name="nsBase">The namespace of the base XML Schema data type.</param>
		/// <param name="clrDerived">The namespace of the derived .NET Framework type.</param>
		/// <param name="clrBase">The name of the base .NET Framework type.</param>
		// Token: 0x06001C1C RID: 7196 RVA: 0x000955C8 File Offset: 0x000937C8
		[MonoTODO]
		protected Exception CreateBadDerivationException(string xsdDerived, string nsDerived, string xsdBase, string nsBase, string clrDerived, string clrBase)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates an <see cref="T:System.InvalidCastException" /> that indicates that an explicit reference conversion failed.</summary>
		/// <returns>An <see cref="T:System.InvalidCastException" /> exception.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> that an object cannot be cast to. This type is incorporated into the exception message.</param>
		/// <param name="value">The object that cannot be cast. This object is incorporated into the exception message.</param>
		/// <param name="id">A string identifier.</param>
		// Token: 0x06001C1D RID: 7197 RVA: 0x000955D0 File Offset: 0x000937D0
		[MonoTODO]
		protected Exception CreateInvalidCastException(Type type, object value, string id)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates that a derived type that is mapped to an XML Schema data type cannot be located.</summary>
		/// <returns>An <see cref="T:System.InvalidOperationException" /> exception.</returns>
		/// <param name="name">The local name of the XML Schema data type that is mapped to the unavailable derived type.</param>
		/// <param name="ns">The namespace of the XML Schema data type that is mapped to the unavailable derived type.</param>
		/// <param name="clrType">The full name of the .NET Framework base type for which a derived type cannot be located.</param>
		// Token: 0x06001C1E RID: 7198 RVA: 0x000955D8 File Offset: 0x000937D8
		[MonoTODO]
		protected Exception CreateMissingIXmlSerializableType(string name, string ns, string clrType)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the result of a call to the <see cref="M:System.Xml.XmlReader.ReadString" /> method of the <see cref="T:System.Xml.XmlReader" /> class, trimmed of white space if needed, and appended to the input value.</summary>
		/// <returns>The result of the read operation appended to the input value.</returns>
		/// <param name="value">A string that will be appended to.</param>
		/// <param name="trim">true if the result of the read operation should be trimmed; otherwise, false.</param>
		// Token: 0x06001C1F RID: 7199 RVA: 0x000955E0 File Offset: 0x000937E0
		[MonoTODO]
		protected string ReadString(string value, bool trim)
		{
			throw new NotImplementedException();
		}

		/// <summary>Reads an XML element that allows null values (xsi:nil = 'true') and returns a generic <see cref="T:System.Nullable`1" /> value. </summary>
		/// <returns>A generic <see cref="T:System.Nullable`1" /> that represents a null XML value.</returns>
		/// <param name="type">The <see cref="T:System.Xml.XmlQualifiedName" /> that represents the simple data type for the current location of the <see cref="T:System.Xml.XmlReader" />.</param>
		// Token: 0x06001C20 RID: 7200 RVA: 0x000955E8 File Offset: 0x000937E8
		[MonoTODO]
		protected object ReadTypedNull(XmlQualifiedName type)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a dynamically generated assembly by name.</summary>
		/// <returns>A dynamically generated <see cref="T:System.Reflection.Assembly" />.</returns>
		/// <param name="assemblyFullName">The full name of the assembly.</param>
		// Token: 0x06001C21 RID: 7201 RVA: 0x000955F0 File Offset: 0x000937F0
		[MonoTODO]
		protected static Assembly ResolveDynamicAssembly(string assemblyFullName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000B3E RID: 2878
		private XmlDocument document;

		// Token: 0x04000B3F RID: 2879
		private XmlReader reader;

		// Token: 0x04000B40 RID: 2880
		private ArrayList fixups;

		// Token: 0x04000B41 RID: 2881
		private Hashtable collFixups;

		// Token: 0x04000B42 RID: 2882
		private ArrayList collItemFixups;

		// Token: 0x04000B43 RID: 2883
		private Hashtable typesCallbacks;

		// Token: 0x04000B44 RID: 2884
		private ArrayList noIDTargets;

		// Token: 0x04000B45 RID: 2885
		private Hashtable targets;

		// Token: 0x04000B46 RID: 2886
		private Hashtable delayedListFixups;

		// Token: 0x04000B47 RID: 2887
		private XmlSerializer eventSource;

		// Token: 0x04000B48 RID: 2888
		private int delayedFixupId;

		// Token: 0x04000B49 RID: 2889
		private Hashtable referencedObjects;

		// Token: 0x04000B4A RID: 2890
		private int readCount;

		// Token: 0x04000B4B RID: 2891
		private int whileIterationCount;

		// Token: 0x04000B4C RID: 2892
		private string w3SchemaNS;

		// Token: 0x04000B4D RID: 2893
		private string w3InstanceNS;

		// Token: 0x04000B4E RID: 2894
		private string w3InstanceNS2000;

		// Token: 0x04000B4F RID: 2895
		private string w3InstanceNS1999;

		// Token: 0x04000B50 RID: 2896
		private string soapNS;

		// Token: 0x04000B51 RID: 2897
		private string wsdlNS;

		// Token: 0x04000B52 RID: 2898
		private string nullX;

		// Token: 0x04000B53 RID: 2899
		private string nil;

		// Token: 0x04000B54 RID: 2900
		private string typeX;

		// Token: 0x04000B55 RID: 2901
		private string arrayType;

		// Token: 0x04000B56 RID: 2902
		private XmlQualifiedName arrayQName;

		// Token: 0x020002A6 RID: 678
		private class WriteCallbackInfo
		{
			// Token: 0x04000B57 RID: 2903
			public Type Type;

			// Token: 0x04000B58 RID: 2904
			public string TypeName;

			// Token: 0x04000B59 RID: 2905
			public string TypeNs;

			// Token: 0x04000B5A RID: 2906
			public XmlSerializationReadCallback Callback;
		}

		/// <summary>Holds an <see cref="T:System.Xml.Serialization.XmlSerializationCollectionFixupCallback" /> delegate instance, plus the method's inputs; also supplies the method's parameters. </summary>
		// Token: 0x020002A7 RID: 679
		protected class CollectionFixup
		{
			// Token: 0x06001C23 RID: 7203 RVA: 0x00095600 File Offset: 0x00093800
			public CollectionFixup(object collection, XmlSerializationCollectionFixupCallback callback, string id)
			{
				this.callback = callback;
				this.collection = collection;
				this.id = id;
			}

			/// <summary>Gets the callback method that instantiates the <see cref="T:System.Xml.Serialization.XmlSerializationCollectionFixupCallback" /> delegate. </summary>
			/// <returns>The <see cref="T:System.Xml.Serialization.XmlSerializationCollectionFixupCallback" /> delegate that points to the callback method.</returns>
			// Token: 0x170007ED RID: 2029
			// (get) Token: 0x06001C24 RID: 7204 RVA: 0x00095620 File Offset: 0x00093820
			public XmlSerializationCollectionFixupCallback Callback
			{
				get
				{
					return this.callback;
				}
			}

			/// <summary>Gets the <paramref name="object collection" /> for the callback method. </summary>
			/// <returns>The collection that is used for the fixup.</returns>
			// Token: 0x170007EE RID: 2030
			// (get) Token: 0x06001C25 RID: 7205 RVA: 0x00095628 File Offset: 0x00093828
			public object Collection
			{
				get
				{
					return this.collection;
				}
			}

			// Token: 0x170007EF RID: 2031
			// (get) Token: 0x06001C26 RID: 7206 RVA: 0x00095630 File Offset: 0x00093830
			public object Id
			{
				get
				{
					return this.id;
				}
			}

			/// <summary>Gets the array into which the callback method copies a collection. </summary>
			/// <returns>The array into which the callback method copies a collection.</returns>
			// Token: 0x170007F0 RID: 2032
			// (get) Token: 0x06001C27 RID: 7207 RVA: 0x00095638 File Offset: 0x00093838
			// (set) Token: 0x06001C28 RID: 7208 RVA: 0x00095640 File Offset: 0x00093840
			internal object CollectionItems
			{
				get
				{
					return this.collectionItems;
				}
				set
				{
					this.collectionItems = value;
				}
			}

			// Token: 0x04000B5B RID: 2907
			private XmlSerializationCollectionFixupCallback callback;

			// Token: 0x04000B5C RID: 2908
			private object collection;

			// Token: 0x04000B5D RID: 2909
			private object collectionItems;

			// Token: 0x04000B5E RID: 2910
			private string id;
		}

		/// <summary>Holds an <see cref="T:System.Xml.Serialization.XmlSerializationFixupCallback" /> delegate instance, plus the method's inputs; also serves as the parameter for the method. </summary>
		// Token: 0x020002A8 RID: 680
		protected class Fixup
		{
			/// <summary>Receives the size of a string array to generate. </summary>
			/// <param name="o">The object that contains other objects whose values get filled in by the callback implementation.</param>
			/// <param name="callback">A method that instantiates the <see cref="T:System.Xml.Serialization.XmlSerializationFixupCallback" /> delegate.</param>
			/// <param name="count">The size of the string array obtained through the <see cref="P:System.Xml.Serialization.XmlSerializationReader.Fixup.Ids" /> property.</param>
			// Token: 0x06001C29 RID: 7209 RVA: 0x0009564C File Offset: 0x0009384C
			public Fixup(object o, XmlSerializationFixupCallback callback, int count)
			{
				this.source = o;
				this.callback = callback;
				this.ids = new string[count];
			}

			/// <summary>Receives a string array. </summary>
			/// <param name="o">The object that contains other objects whose values get filled in by the callback implementation.</param>
			/// <param name="callback">A method that instantiates the <see cref="T:System.Xml.Serialization.XmlSerializationFixupCallback" /> delegate.</param>
			/// <param name="ids">The string array obtained through the <see cref="P:System.Xml.Serialization.XmlSerializationReader.Fixup.Ids" /> property.</param>
			// Token: 0x06001C2A RID: 7210 RVA: 0x0009567C File Offset: 0x0009387C
			public Fixup(object o, XmlSerializationFixupCallback callback, string[] ids)
			{
				this.source = o;
				this.ids = ids;
				this.callback = callback;
			}

			/// <summary>Gets the callback method that creates an instance of the <see cref="T:System.Xml.Serialization.XmlSerializationFixupCallback" /> delegate. </summary>
			/// <returns>An <see cref="T:System.Xml.Serialization.XmlSerializationFixupCallback" />. </returns>
			// Token: 0x170007F1 RID: 2033
			// (get) Token: 0x06001C2B RID: 7211 RVA: 0x0009569C File Offset: 0x0009389C
			public XmlSerializationFixupCallback Callback
			{
				get
				{
					return this.callback;
				}
			}

			/// <summary>Gets or sets an array of keys for the objects that belong to the <see cref="P:System.Xml.Serialization.XmlSerializationReader.Fixup.Source" /> property whose values get filled in by the callback implementation. </summary>
			/// <returns>The array of keys.</returns>
			// Token: 0x170007F2 RID: 2034
			// (get) Token: 0x06001C2C RID: 7212 RVA: 0x000956A4 File Offset: 0x000938A4
			public string[] Ids
			{
				get
				{
					return this.ids;
				}
			}

			/// <summary>Gets or sets the object that contains other objects whose values get filled in by the callback implementation.</summary>
			/// <returns>The source containing objects with values to fill.</returns>
			// Token: 0x170007F3 RID: 2035
			// (get) Token: 0x06001C2D RID: 7213 RVA: 0x000956AC File Offset: 0x000938AC
			// (set) Token: 0x06001C2E RID: 7214 RVA: 0x000956B4 File Offset: 0x000938B4
			public object Source
			{
				get
				{
					return this.source;
				}
				set
				{
					this.source = value;
				}
			}

			// Token: 0x04000B5F RID: 2911
			private object source;

			// Token: 0x04000B60 RID: 2912
			private string[] ids;

			// Token: 0x04000B61 RID: 2913
			private XmlSerializationFixupCallback callback;
		}

		// Token: 0x020002A9 RID: 681
		protected class CollectionItemFixup
		{
			// Token: 0x06001C2F RID: 7215 RVA: 0x000956C0 File Offset: 0x000938C0
			public CollectionItemFixup(Array list, int index, string id)
			{
				this.list = list;
				this.index = index;
				this.id = id;
			}

			// Token: 0x170007F4 RID: 2036
			// (get) Token: 0x06001C30 RID: 7216 RVA: 0x000956E0 File Offset: 0x000938E0
			public Array Collection
			{
				get
				{
					return this.list;
				}
			}

			// Token: 0x170007F5 RID: 2037
			// (get) Token: 0x06001C31 RID: 7217 RVA: 0x000956E8 File Offset: 0x000938E8
			public int Index
			{
				get
				{
					return this.index;
				}
			}

			// Token: 0x170007F6 RID: 2038
			// (get) Token: 0x06001C32 RID: 7218 RVA: 0x000956F0 File Offset: 0x000938F0
			public string Id
			{
				get
				{
					return this.id;
				}
			}

			// Token: 0x04000B62 RID: 2914
			private Array list;

			// Token: 0x04000B63 RID: 2915
			private int index;

			// Token: 0x04000B64 RID: 2916
			private string id;
		}
	}
}
