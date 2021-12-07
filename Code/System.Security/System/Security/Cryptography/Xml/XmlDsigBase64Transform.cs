using System;
using System.IO;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the Base64 decoding transform as defined in Section 6.6.2 of the XMLDSIG specification.</summary>
	// Token: 0x02000056 RID: 86
	public class XmlDsigBase64Transform : Transform
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> class.</summary>
		// Token: 0x060002CF RID: 719 RVA: 0x0000D380 File Offset: 0x0000B580
		public XmlDsigBase64Transform()
		{
			base.Algorithm = "http://www.w3.org/2000/09/xmldsig#base64";
		}

		/// <summary>Gets an array of types that are valid inputs to the <see cref="M:System.Security.Cryptography.Xml.XmlDsigBase64Transform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object.</summary>
		/// <returns>An array of valid input types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object; you can pass only objects of one of these types to the <see cref="M:System.Security.Cryptography.Xml.XmlDsigBase64Transform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object.</returns>
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000D394 File Offset: 0x0000B594
		public override Type[] InputTypes
		{
			get
			{
				if (this.input == null)
				{
					this.input = new Type[3];
					this.input[0] = typeof(Stream);
					this.input[1] = typeof(XmlDocument);
					this.input[2] = typeof(XmlNodeList);
				}
				return this.input;
			}
		}

		/// <summary>Gets an array of types that are possible outputs from the <see cref="M:System.Security.Cryptography.Xml.XmlDsigBase64Transform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object.</summary>
		/// <returns>An array of valid output types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object; only objects of one of these types are returned from the <see cref="M:System.Security.Cryptography.Xml.XmlDsigBase64Transform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object.</returns>
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000D3F4 File Offset: 0x0000B5F4
		public override Type[] OutputTypes
		{
			get
			{
				if (this.output == null)
				{
					this.output = new Type[1];
					this.output[0] = typeof(Stream);
				}
				return this.output;
			}
		}

		/// <summary>Returns an XML representation of the parameters of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object that are suitable to be included as subelements of an XMLDSIG &lt;Transform&gt; element.</summary>
		/// <returns>A list of the XML nodes that represent the transform-specific content needed to describe the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object in an XMLDSIG &lt;Transform&gt; element.</returns>
		// Token: 0x060002D2 RID: 722 RVA: 0x0000D428 File Offset: 0x0000B628
		protected override XmlNodeList GetInnerXml()
		{
			return null;
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object.</summary>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object.</returns>
		// Token: 0x060002D3 RID: 723 RVA: 0x0000D42C File Offset: 0x0000B62C
		public override object GetOutput()
		{
			return this.cs;
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object of type <see cref="T:System.IO.Stream" />.</summary>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object of type <see cref="T:System.IO.Stream" />.</returns>
		/// <param name="type">The type of the output to return. <see cref="T:System.IO.Stream" /> is the only valid type for this parameter. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="type" /> parameter is not a <see cref="T:System.IO.Stream" /> object. </exception>
		// Token: 0x060002D4 RID: 724 RVA: 0x0000D434 File Offset: 0x0000B634
		public override object GetOutput(Type type)
		{
			if (type != typeof(Stream))
			{
				throw new ArgumentException("type");
			}
			return this.GetOutput();
		}

		/// <summary>Parses the specified <see cref="T:System.Xml.XmlNodeList" /> object as transform-specific content of a &lt;Transform&gt; element; this method is not supported because the <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object has no inner XML elements.</summary>
		/// <param name="nodeList">An <see cref="T:System.Xml.XmlNodeList" /> object to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object. </param>
		// Token: 0x060002D5 RID: 725 RVA: 0x0000D458 File Offset: 0x0000B658
		public override void LoadInnerXml(XmlNodeList nodeList)
		{
		}

		/// <summary>Loads the specified input into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object.</summary>
		/// <param name="obj">The input to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="obj" /> parameter is a <see cref="T:System.IO.Stream" /> and it is null. </exception>
		// Token: 0x060002D6 RID: 726 RVA: 0x0000D45C File Offset: 0x0000B65C
		public override void LoadInput(object obj)
		{
			XmlNodeList xmlNodeList = null;
			Stream stream = null;
			if (obj is Stream)
			{
				stream = (obj as Stream);
			}
			else if (obj is XmlDocument)
			{
				xmlNodeList = (obj as XmlDocument).SelectNodes("//.");
			}
			else if (obj is XmlNodeList)
			{
				xmlNodeList = (XmlNodeList)obj;
			}
			if (xmlNodeList != null)
			{
				stream = new MemoryStream();
				StreamWriter streamWriter = new StreamWriter(stream);
				foreach (object obj2 in xmlNodeList)
				{
					XmlNode xmlNode = (XmlNode)obj2;
					XmlNodeType nodeType = xmlNode.NodeType;
					switch (nodeType)
					{
					case XmlNodeType.Attribute:
					case XmlNodeType.Text:
					case XmlNodeType.CDATA:
						break;
					default:
						if (nodeType != XmlNodeType.Whitespace && nodeType != XmlNodeType.SignificantWhitespace)
						{
							continue;
						}
						break;
					}
					streamWriter.Write(xmlNode.Value);
				}
				streamWriter.Flush();
				stream.Position = 0L;
			}
			if (stream != null)
			{
				this.cs = new CryptoStream(stream, new FromBase64Transform(), CryptoStreamMode.Read);
			}
		}

		// Token: 0x04000138 RID: 312
		private CryptoStream cs;

		// Token: 0x04000139 RID: 313
		private Type[] input;

		// Token: 0x0400013A RID: 314
		private Type[] output;
	}
}
