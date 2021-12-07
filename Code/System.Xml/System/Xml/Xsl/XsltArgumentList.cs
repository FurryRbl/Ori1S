using System;
using System.Collections;
using System.Security.Permissions;
using System.Xml.XPath;

namespace System.Xml.Xsl
{
	/// <summary>Contains a variable number of arguments which are either XSLT parameters or extension objects.</summary>
	// Token: 0x020001B2 RID: 434
	public class XsltArgumentList
	{
		/// <summary>Implements a new instance of the <see cref="T:System.Xml.Xsl.XsltArgumentList" />.</summary>
		// Token: 0x060011CF RID: 4559 RVA: 0x000513E8 File Offset: 0x0004F5E8
		public XsltArgumentList()
		{
			this.extensionObjects = new Hashtable();
			this.parameters = new Hashtable();
		}

		/// <summary>Occurs when a message is specified in the style sheet by the xsl:message element. </summary>
		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060011D0 RID: 4560 RVA: 0x00051408 File Offset: 0x0004F608
		// (remove) Token: 0x060011D1 RID: 4561 RVA: 0x00051424 File Offset: 0x0004F624
		public event XsltMessageEncounteredEventHandler XsltMessageEncountered;

		/// <summary>Adds a new object to the <see cref="T:System.Xml.Xsl.XsltArgumentList" /> and associates it with the namespace URI.</summary>
		/// <param name="namespaceUri">The namespace URI to associate with the object. To use the default namespace, specify an empty string. </param>
		/// <param name="extension">The object to add to the list. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="namespaceUri" /> is either null or http://www.w3.org/1999/XSL/Transform The <paramref name="namespaceUri" /> already has an extension object associated with it. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have sufficient permissions to call this method. </exception>
		// Token: 0x060011D2 RID: 4562 RVA: 0x00051440 File Offset: 0x0004F640
		[PermissionSet((SecurityAction)14, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"\nUnrestricted=\"true\"/>\n")]
		public void AddExtensionObject(string namespaceUri, object extension)
		{
			if (namespaceUri == null)
			{
				throw new ArgumentException("The namespaceUri is a null reference.");
			}
			if (namespaceUri == "http://www.w3.org/1999/XSL/Transform")
			{
				throw new ArgumentException("The namespaceUri is http://www.w3.org/1999/XSL/Transform.");
			}
			if (this.extensionObjects.Contains(namespaceUri))
			{
				throw new ArgumentException("The namespaceUri already has an extension object associated with it.");
			}
			this.extensionObjects[namespaceUri] = extension;
		}

		/// <summary>Adds a parameter to the <see cref="T:System.Xml.Xsl.XsltArgumentList" /> and associates it with the namespace qualified name.</summary>
		/// <param name="name">The name to associate with the parameter. </param>
		/// <param name="namespaceUri">The namespace URI to associate with the parameter. To use the default namespace, specify an empty string. </param>
		/// <param name="parameter">The parameter value or object to add to the list. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="namespaceUri" /> is either null or http://www.w3.org/1999/XSL/Transform.The <paramref name="name" /> is not a valid name according to the W3C XML specification.The <paramref name="namespaceUri" /> already has a parameter associated with it. </exception>
		// Token: 0x060011D3 RID: 4563 RVA: 0x000514A4 File Offset: 0x0004F6A4
		public void AddParam(string name, string namespaceUri, object parameter)
		{
			if (namespaceUri == null)
			{
				throw new ArgumentException("The namespaceUri is a null reference.");
			}
			if (namespaceUri == "http://www.w3.org/1999/XSL/Transform")
			{
				throw new ArgumentException("The namespaceUri is http://www.w3.org/1999/XSL/Transform.");
			}
			if (name == null)
			{
				throw new ArgumentException("The parameter name is a null reference.");
			}
			XmlQualifiedName key = new XmlQualifiedName(name, namespaceUri);
			if (this.parameters.Contains(key))
			{
				throw new ArgumentException("The namespaceUri already has a parameter associated with it.");
			}
			parameter = this.ValidateParam(parameter);
			this.parameters[key] = parameter;
		}

		/// <summary>Removes all parameters and extension objects from the <see cref="T:System.Xml.Xsl.XsltArgumentList" />.</summary>
		// Token: 0x060011D4 RID: 4564 RVA: 0x00051528 File Offset: 0x0004F728
		public void Clear()
		{
			this.extensionObjects.Clear();
			this.parameters.Clear();
		}

		/// <summary>Gets the object associated with the given namespace.</summary>
		/// <returns>The namespace URI object or null if one was not found.</returns>
		/// <param name="namespaceUri">The namespace URI of the object. </param>
		// Token: 0x060011D5 RID: 4565 RVA: 0x00051540 File Offset: 0x0004F740
		public object GetExtensionObject(string namespaceUri)
		{
			return this.extensionObjects[namespaceUri];
		}

		/// <summary>Gets the parameter associated with the namespace qualified name.</summary>
		/// <returns>The parameter object or null if one was not found.</returns>
		/// <param name="name">The name of the parameter. <see cref="T:System.Xml.Xsl.XsltArgumentList" /> does not check to ensure the name passed is a valid local name; however, the name cannot be null. </param>
		/// <param name="namespaceUri">The namespace URI associated with the parameter. </param>
		// Token: 0x060011D6 RID: 4566 RVA: 0x00051550 File Offset: 0x0004F750
		public object GetParam(string name, string namespaceUri)
		{
			if (name == null)
			{
				throw new ArgumentException("The parameter name is a null reference.");
			}
			XmlQualifiedName key = new XmlQualifiedName(name, namespaceUri);
			return this.parameters[key];
		}

		/// <summary>Removes the object with the namespace URI from the <see cref="T:System.Xml.Xsl.XsltArgumentList" />.</summary>
		/// <returns>The object with the namespace URI or null if one was not found.</returns>
		/// <param name="namespaceUri">The namespace URI associated with the object to remove. </param>
		// Token: 0x060011D7 RID: 4567 RVA: 0x00051584 File Offset: 0x0004F784
		public object RemoveExtensionObject(string namespaceUri)
		{
			object extensionObject = this.GetExtensionObject(namespaceUri);
			this.extensionObjects.Remove(namespaceUri);
			return extensionObject;
		}

		/// <summary>Removes the parameter from the <see cref="T:System.Xml.Xsl.XsltArgumentList" />.</summary>
		/// <returns>The parameter object or null if one was not found.</returns>
		/// <param name="name">The name of the parameter to remove. <see cref="T:System.Xml.Xsl.XsltArgumentList" /> does not check to ensure the name passed is a valid local name; however, the name cannot be null. </param>
		/// <param name="namespaceUri">The namespace URI of the parameter to remove. </param>
		// Token: 0x060011D8 RID: 4568 RVA: 0x000515A8 File Offset: 0x0004F7A8
		public object RemoveParam(string name, string namespaceUri)
		{
			XmlQualifiedName key = new XmlQualifiedName(name, namespaceUri);
			object param = this.GetParam(name, namespaceUri);
			this.parameters.Remove(key);
			return param;
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x000515D4 File Offset: 0x0004F7D4
		private object ValidateParam(object parameter)
		{
			if (parameter is string)
			{
				return parameter;
			}
			if (parameter is bool)
			{
				return parameter;
			}
			if (parameter is double)
			{
				return parameter;
			}
			if (parameter is XPathNavigator)
			{
				return parameter;
			}
			if (parameter is XPathNodeIterator)
			{
				return parameter;
			}
			if (parameter is short)
			{
				return (double)((short)parameter);
			}
			if (parameter is ushort)
			{
				return (double)((ushort)parameter);
			}
			if (parameter is int)
			{
				return (double)((int)parameter);
			}
			if (parameter is long)
			{
				return (double)((long)parameter);
			}
			if (parameter is ulong)
			{
				return (ulong)parameter;
			}
			if (parameter is float)
			{
				return (double)((float)parameter);
			}
			if (parameter is decimal)
			{
				return (double)((decimal)parameter);
			}
			return parameter.ToString();
		}

		// Token: 0x04000757 RID: 1879
		internal Hashtable extensionObjects;

		// Token: 0x04000758 RID: 1880
		internal Hashtable parameters;
	}
}
