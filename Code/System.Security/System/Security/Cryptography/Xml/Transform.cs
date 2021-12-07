using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the abstract base class from which all &lt;Transform&gt; elements that can be used in an XML digital signature derive.</summary>
	// Token: 0x02000053 RID: 83
	public abstract class Transform
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.Transform" /> class.</summary>
		// Token: 0x060002AC RID: 684 RVA: 0x0000CCF0 File Offset: 0x0000AEF0
		protected Transform()
		{
			if (SecurityManager.SecurityEnabled)
			{
				this.xmlResolver = new XmlSecureResolver(new XmlUrlResolver(), new Evidence());
			}
			else
			{
				this.xmlResolver = new XmlUrlResolver();
			}
		}

		/// <summary>Gets or sets the Uniform Resource Identifier (URI) that identifies the algorithm performed by the current transform.</summary>
		/// <returns>The URI that identifies the algorithm performed by the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object.</returns>
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000CD40 File Offset: 0x0000AF40
		// (set) Token: 0x060002AE RID: 686 RVA: 0x0000CD48 File Offset: 0x0000AF48
		public string Algorithm
		{
			get
			{
				return this.algo;
			}
			set
			{
				this.algo = value;
			}
		}

		/// <summary>When overridden in a derived class, gets an array of types that are valid inputs to the <see cref="M:System.Security.Cryptography.Xml.Transform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object.</summary>
		/// <returns>An array of valid input types for the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object; you can pass only objects of one of these types to the <see cref="M:System.Security.Cryptography.Xml.Transform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object.</returns>
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002AF RID: 687
		public abstract Type[] InputTypes { get; }

		/// <summary>When overridden in a derived class, gets an array of types that are possible outputs from the <see cref="M:System.Security.Cryptography.Xml.Transform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object.</summary>
		/// <returns>An array of valid output types for the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object; only objects of one of these types are returned from the <see cref="M:System.Security.Cryptography.Xml.Transform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object.</returns>
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002B0 RID: 688
		public abstract Type[] OutputTypes { get; }

		/// <summary>Sets the current <see cref="T:System.Xml.XmlResolver" /> object.</summary>
		/// <returns>The current <see cref="T:System.Xml.XmlResolver" /> object. This property defaults to an <see cref="T:System.Xml.XmlSecureResolver" /> object.</returns>
		// Token: 0x170000BA RID: 186
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x0000CD54 File Offset: 0x0000AF54
		[ComVisible(false)]
		public XmlResolver Resolver
		{
			set
			{
				this.xmlResolver = value;
			}
		}

		/// <summary>Gets or sets an <see cref="T:System.Xml.XmlElement" /> object that represents the document context under which the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object is running. </summary>
		/// <returns>An <see cref="T:System.Xml.XmlElement" /> object that represents the document context under which the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object is running.</returns>
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000CD60 File Offset: 0x0000AF60
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x0000CD68 File Offset: 0x0000AF68
		[ComVisible(false)]
		[MonoTODO]
		public XmlElement Context
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

		/// <summary>Gets or sets a <see cref="T:System.Collections.Hashtable" /> object that contains the namespaces that are propagated into the signature. </summary>
		/// <returns>A <see cref="T:System.Collections.Hashtable" /> object that contains the namespaces that are propagated into the signature.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Cryptography.Xml.Transform.PropagatedNamespaces" /> property was set to null.</exception>
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000CD70 File Offset: 0x0000AF70
		[ComVisible(false)]
		public Hashtable PropagatedNamespaces
		{
			get
			{
				return this.propagated_namespaces;
			}
		}

		/// <summary>When overridden in a derived class, returns the digest associated with a <see cref="T:System.Security.Cryptography.Xml.Transform" /> object. </summary>
		/// <returns>The digest associated with a <see cref="T:System.Security.Cryptography.Xml.Transform" /> object.</returns>
		/// <param name="hash">The <see cref="T:System.Security.Cryptography.HashAlgorithm" /> object used to create a digest.</param>
		// Token: 0x060002B5 RID: 693 RVA: 0x0000CD78 File Offset: 0x0000AF78
		[ComVisible(false)]
		public virtual byte[] GetDigestedOutput(HashAlgorithm hash)
		{
			return hash.ComputeHash((Stream)this.GetOutput(typeof(Stream)));
		}

		/// <summary>When overridden in a derived class, returns an XML representation of the parameters of the <see cref="T:System.Security.Cryptography.Xml.Transform" /> object that are suitable to be included as subelements of an XMLDSIG &lt;Transform&gt; element.</summary>
		/// <returns>A list of the XML nodes that represent the transform-specific content needed to describe the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object in an XMLDSIG &lt;Transform&gt; element.</returns>
		// Token: 0x060002B6 RID: 694
		protected abstract XmlNodeList GetInnerXml();

		/// <summary>When overridden in a derived class, returns the output of the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object.</summary>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object.</returns>
		// Token: 0x060002B7 RID: 695
		public abstract object GetOutput();

		/// <summary>When overridden in a derived class, returns the output of the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object of the specified type.</summary>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object as an object of the specified type.</returns>
		/// <param name="type">The type of the output to return. This must be one of the types in the <see cref="P:System.Security.Cryptography.Xml.Transform.OutputTypes" /> property. </param>
		// Token: 0x060002B8 RID: 696
		public abstract object GetOutput(Type type);

		/// <summary>Returns the XML representation of the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object.</summary>
		/// <returns>The XML representation of the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060002B9 RID: 697 RVA: 0x0000CD98 File Offset: 0x0000AF98
		public XmlElement GetXml()
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.XmlResolver = this.GetResolver();
			XmlElement xmlElement = xmlDocument.CreateElement("Transform", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement.SetAttribute("Algorithm", this.algo);
			XmlNodeList innerXml = this.GetInnerXml();
			if (innerXml != null)
			{
				foreach (object obj in innerXml)
				{
					XmlNode node = (XmlNode)obj;
					XmlNode newChild = xmlDocument.ImportNode(node, true);
					xmlElement.AppendChild(newChild);
				}
			}
			return xmlElement;
		}

		/// <summary>When overridden in a derived class, parses the specified <see cref="T:System.Xml.XmlNodeList" /> object as transform-specific content of a &lt;Transform&gt; element and configures the internal state of the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object to match the &lt;Transform&gt; element.</summary>
		/// <param name="nodeList">An <see cref="T:System.Xml.XmlNodeList" /> object that specifies transform-specific content for the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object. </param>
		// Token: 0x060002BA RID: 698
		public abstract void LoadInnerXml(XmlNodeList nodeList);

		/// <summary>When overridden in a derived class, loads the specified input into the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object.</summary>
		/// <param name="obj">The input to load into the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object. </param>
		// Token: 0x060002BB RID: 699
		public abstract void LoadInput(object obj);

		// Token: 0x060002BC RID: 700 RVA: 0x0000CE58 File Offset: 0x0000B058
		internal XmlResolver GetResolver()
		{
			return this.xmlResolver;
		}

		// Token: 0x0400012D RID: 301
		private string algo;

		// Token: 0x0400012E RID: 302
		private XmlResolver xmlResolver;

		// Token: 0x0400012F RID: 303
		private Hashtable propagated_namespaces = new Hashtable();
	}
}
