using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;

namespace System.Runtime.Remoting
{
	/// <summary>Provides several methods for using and publishing remoted objects in SOAP format.</summary>
	// Token: 0x02000432 RID: 1074
	[ComVisible(true)]
	public class SoapServices
	{
		// Token: 0x06002DA6 RID: 11686 RVA: 0x00097F50 File Offset: 0x00096150
		private SoapServices()
		{
		}

		/// <summary>Gets the XML namespace prefix for common language runtime types.</summary>
		/// <returns>The XML namespace prefix for common language runtime types.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06002DA8 RID: 11688 RVA: 0x00097F98 File Offset: 0x00096198
		public static string XmlNsForClrType
		{
			get
			{
				return "http://schemas.microsoft.com/clr/";
			}
		}

		/// <summary>Gets the default XML namespace prefix that should be used for XML encoding of a common language runtime class that has an assembly, but no native namespace.</summary>
		/// <returns>The default XML namespace prefix that should be used for XML encoding of a common language runtime class that has an assembly, but no native namespace.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06002DA9 RID: 11689 RVA: 0x00097FA0 File Offset: 0x000961A0
		public static string XmlNsForClrTypeWithAssembly
		{
			get
			{
				return "http://schemas.microsoft.com/clr/assem/";
			}
		}

		/// <summary>Gets the XML namespace prefix that should be used for XML encoding of a common language runtime class that is part of the mscorlib.dll file.</summary>
		/// <returns>The XML namespace prefix that should be used for XML encoding of a common language runtime class that is part of the mscorlib.dll file.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06002DAA RID: 11690 RVA: 0x00097FA8 File Offset: 0x000961A8
		public static string XmlNsForClrTypeWithNs
		{
			get
			{
				return "http://schemas.microsoft.com/clr/ns/";
			}
		}

		/// <summary>Gets the default XML namespace prefix that should be used for XML encoding of a common language runtime class that has both a common language runtime namespace and an assembly.</summary>
		/// <returns>The default XML namespace prefix that should be used for XML encoding of a common language runtime class that has both a common language runtime namespace and an assembly.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06002DAB RID: 11691 RVA: 0x00097FB0 File Offset: 0x000961B0
		public static string XmlNsForClrTypeWithNsAndAssembly
		{
			get
			{
				return "http://schemas.microsoft.com/clr/nsassem/";
			}
		}

		/// <summary>Returns the common language runtime type namespace name from the provided namespace and assembly names.</summary>
		/// <returns>The common language runtime type namespace name from the provided namespace and assembly names.</returns>
		/// <param name="typeNamespace">The namespace that is to be coded. </param>
		/// <param name="assemblyName">The name of the assembly that is to be coded. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="assemblyName" /> and <paramref name="typeNamespace" /> parameters are both either null or empty. </exception>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002DAC RID: 11692 RVA: 0x00097FB8 File Offset: 0x000961B8
		public static string CodeXmlNamespaceForClrTypeNamespace(string typeNamespace, string assemblyName)
		{
			if (assemblyName == string.Empty)
			{
				return SoapServices.XmlNsForClrTypeWithNs + typeNamespace;
			}
			if (typeNamespace == string.Empty)
			{
				return SoapServices.EncodeNs(SoapServices.XmlNsForClrTypeWithAssembly + assemblyName);
			}
			return SoapServices.EncodeNs(SoapServices.XmlNsForClrTypeWithNsAndAssembly + typeNamespace + "/" + assemblyName);
		}

		/// <summary>Decodes the XML namespace and assembly names from the provided common language runtime namespace.</summary>
		/// <returns>true if the namespace and assembly names were successfully decoded; otherwise, false.</returns>
		/// <param name="inNamespace">The common language runtime namespace. </param>
		/// <param name="typeNamespace">When this method returns, contains a <see cref="T:System.String" /> that holds the decoded namespace name. This parameter is passed uninitialized. </param>
		/// <param name="assemblyName">When this method returns, contains a <see cref="T:System.String" /> that holds the decoded assembly name. This parameter is passed uninitialized. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="inNamespace" /> parameter is null or empty. </exception>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002DAD RID: 11693 RVA: 0x00098018 File Offset: 0x00096218
		public static bool DecodeXmlNamespaceForClrTypeNamespace(string inNamespace, out string typeNamespace, out string assemblyName)
		{
			if (inNamespace == null)
			{
				throw new ArgumentNullException("inNamespace");
			}
			inNamespace = SoapServices.DecodeNs(inNamespace);
			typeNamespace = null;
			assemblyName = null;
			if (inNamespace.StartsWith(SoapServices.XmlNsForClrTypeWithNsAndAssembly))
			{
				int length = SoapServices.XmlNsForClrTypeWithNsAndAssembly.Length;
				if (length >= inNamespace.Length)
				{
					return false;
				}
				int num = inNamespace.IndexOf('/', length + 1);
				if (num == -1)
				{
					return false;
				}
				typeNamespace = inNamespace.Substring(length, num - length);
				assemblyName = inNamespace.Substring(num + 1);
				return true;
			}
			else
			{
				if (inNamespace.StartsWith(SoapServices.XmlNsForClrTypeWithNs))
				{
					int length2 = SoapServices.XmlNsForClrTypeWithNs.Length;
					typeNamespace = inNamespace.Substring(length2);
					return true;
				}
				if (inNamespace.StartsWith(SoapServices.XmlNsForClrTypeWithAssembly))
				{
					int length3 = SoapServices.XmlNsForClrTypeWithAssembly.Length;
					assemblyName = inNamespace.Substring(length3);
					return true;
				}
				return false;
			}
		}

		/// <summary>Retrieves field type from XML attribute name, namespace, and the <see cref="T:System.Type" /> of the containing object.</summary>
		/// <param name="containingType">The <see cref="T:System.Type" /> of the object that contains the field. </param>
		/// <param name="xmlAttribute">The XML attribute name of the field type. </param>
		/// <param name="xmlNamespace">The XML namespace of the field type. </param>
		/// <param name="type">When this method returns, contains a <see cref="T:System.Type" /> of the field. This parameter is passed uninitialized. </param>
		/// <param name="name">When this method returns, contains a <see cref="T:System.String" /> that holds the name of the field. This parameter is passed uninitialized. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002DAE RID: 11694 RVA: 0x000980E8 File Offset: 0x000962E8
		public static void GetInteropFieldTypeAndNameFromXmlAttribute(Type containingType, string xmlAttribute, string xmlNamespace, out Type type, out string name)
		{
			SoapServices.TypeInfo typeInfo = (SoapServices.TypeInfo)SoapServices._typeInfos[containingType];
			Hashtable fields = (typeInfo == null) ? null : typeInfo.Attributes;
			SoapServices.GetInteropFieldInfo(fields, xmlAttribute, xmlNamespace, out type, out name);
		}

		/// <summary>Retrieves the <see cref="T:System.Type" /> and name of a field from the provided XML element name, namespace, and the containing type.</summary>
		/// <param name="containingType">The <see cref="T:System.Type" /> of the object that contains the field. </param>
		/// <param name="xmlElement">The XML element name of field. </param>
		/// <param name="xmlNamespace">The XML namespace of the field type. </param>
		/// <param name="type">When this method returns, contains a <see cref="T:System.Type" /> of the field. This parameter is passed uninitialized. </param>
		/// <param name="name">When this method returns, contains a <see cref="T:System.String" /> that holds the name of the field. This parameter is passed uninitialized. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002DAF RID: 11695 RVA: 0x00098124 File Offset: 0x00096324
		public static void GetInteropFieldTypeAndNameFromXmlElement(Type containingType, string xmlElement, string xmlNamespace, out Type type, out string name)
		{
			SoapServices.TypeInfo typeInfo = (SoapServices.TypeInfo)SoapServices._typeInfos[containingType];
			Hashtable fields = (typeInfo == null) ? null : typeInfo.Elements;
			SoapServices.GetInteropFieldInfo(fields, xmlElement, xmlNamespace, out type, out name);
		}

		// Token: 0x06002DB0 RID: 11696 RVA: 0x00098160 File Offset: 0x00096360
		private static void GetInteropFieldInfo(Hashtable fields, string xmlName, string xmlNamespace, out Type type, out string name)
		{
			if (fields != null)
			{
				FieldInfo fieldInfo = (FieldInfo)fields[SoapServices.GetNameKey(xmlName, xmlNamespace)];
				if (fieldInfo != null)
				{
					type = fieldInfo.FieldType;
					name = fieldInfo.Name;
					return;
				}
			}
			type = null;
			name = null;
		}

		// Token: 0x06002DB1 RID: 11697 RVA: 0x000981A8 File Offset: 0x000963A8
		private static string GetNameKey(string name, string namspace)
		{
			if (namspace == null)
			{
				return name;
			}
			return name + " " + namspace;
		}

		/// <summary>Retrieves the <see cref="T:System.Type" /> that should be used during deserialization of an unrecognized object type with the given XML element name and namespace.</summary>
		/// <returns>The <see cref="T:System.Type" /> of object associated with the specified XML element name and namespace.</returns>
		/// <param name="xmlElement">The XML element name of the unknown object type. </param>
		/// <param name="xmlNamespace">The XML namespace of the unknown object type. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002DB2 RID: 11698 RVA: 0x000981C0 File Offset: 0x000963C0
		public static Type GetInteropTypeFromXmlElement(string xmlElement, string xmlNamespace)
		{
			object syncRoot = SoapServices._xmlElements.SyncRoot;
			Type result;
			lock (syncRoot)
			{
				result = (Type)SoapServices._xmlElements[xmlElement + " " + xmlNamespace];
			}
			return result;
		}

		/// <summary>Retrieves the object <see cref="T:System.Type" /> that should be used during deserialization of an unrecognized object type with the given XML type name and namespace.</summary>
		/// <returns>The <see cref="T:System.Type" /> of object associated with the specified XML type name and namespace.</returns>
		/// <param name="xmlType">The XML type of the unknown object type. </param>
		/// <param name="xmlTypeNamespace">The XML type namespace of the unknown object type. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002DB3 RID: 11699 RVA: 0x00098228 File Offset: 0x00096428
		public static Type GetInteropTypeFromXmlType(string xmlType, string xmlTypeNamespace)
		{
			object syncRoot = SoapServices._xmlTypes.SyncRoot;
			Type result;
			lock (syncRoot)
			{
				result = (Type)SoapServices._xmlTypes[xmlType + " " + xmlTypeNamespace];
			}
			return result;
		}

		// Token: 0x06002DB4 RID: 11700 RVA: 0x00098290 File Offset: 0x00096490
		private static string GetAssemblyName(MethodBase mb)
		{
			if (mb.DeclaringType.Assembly == typeof(object).Assembly)
			{
				return string.Empty;
			}
			return mb.DeclaringType.Assembly.GetName().Name;
		}

		/// <summary>Returns the SOAPAction value associated with the method specified in the given <see cref="T:System.Reflection.MethodBase" />.</summary>
		/// <returns>The SOAPAction value associated with the method specified in the given <see cref="T:System.Reflection.MethodBase" />.</returns>
		/// <param name="mb">The <see cref="T:System.Reflection.MethodBase" /> that contains the method for which a SOAPAction is requested. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002DB5 RID: 11701 RVA: 0x000982D8 File Offset: 0x000964D8
		public static string GetSoapActionFromMethodBase(MethodBase mb)
		{
			return SoapServices.InternalGetSoapAction(mb);
		}

		/// <summary>Determines the type and method name of the method associated with the specified SOAPAction value.</summary>
		/// <returns>true if the type and method name were successfully recovered; otherwise, false.</returns>
		/// <param name="soapAction">The SOAPAction of the method for which the type and method names were requested. </param>
		/// <param name="typeName">When this method returns, contains a <see cref="T:System.String" /> that holds the type name of the method in question. This parameter is passed uninitialized. </param>
		/// <param name="methodName">When this method returns, contains a <see cref="T:System.String" /> that holds the method name of the method in question. This parameter is passed uninitialized. </param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">The SOAPAction value does not start and end with quotes. </exception>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002DB6 RID: 11702 RVA: 0x000982E0 File Offset: 0x000964E0
		public static bool GetTypeAndMethodNameFromSoapAction(string soapAction, out string typeName, out string methodName)
		{
			object syncRoot = SoapServices._soapActions.SyncRoot;
			lock (syncRoot)
			{
				MethodBase methodBase = (MethodBase)SoapServices._soapActionsMethods[soapAction];
				if (methodBase != null)
				{
					typeName = methodBase.DeclaringType.AssemblyQualifiedName;
					methodName = methodBase.Name;
					return true;
				}
			}
			typeName = null;
			methodName = null;
			int num = soapAction.LastIndexOf('#');
			if (num == -1)
			{
				return false;
			}
			methodName = soapAction.Substring(num + 1);
			string str;
			string text;
			if (!SoapServices.DecodeXmlNamespaceForClrTypeNamespace(soapAction.Substring(0, num), out str, out text))
			{
				return false;
			}
			if (text == null)
			{
				typeName = str + ", " + typeof(object).Assembly.GetName().Name;
			}
			else
			{
				typeName = str + ", " + text;
			}
			return true;
		}

		/// <summary>Returns XML element information that should be used when serializing the given type.</summary>
		/// <returns>true if the requested values have been set flagged with <see cref="T:System.Runtime.Remoting.Metadata.SoapTypeAttribute" />; otherwise, false.</returns>
		/// <param name="type">The object <see cref="T:System.Type" /> for which the XML element and namespace names were requested. </param>
		/// <param name="xmlElement">When this method returns, contains a <see cref="T:System.String" /> that holds the XML element name of the specified object type. This parameter is passed uninitialized. </param>
		/// <param name="xmlNamespace">When this method returns, contains a <see cref="T:System.String" /> that holds the XML namespace name of the specified object type. This parameter is passed uninitialized. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002DB7 RID: 11703 RVA: 0x000983DC File Offset: 0x000965DC
		public static bool GetXmlElementForInteropType(Type type, out string xmlElement, out string xmlNamespace)
		{
			SoapTypeAttribute soapTypeAttribute = (SoapTypeAttribute)InternalRemotingServices.GetCachedSoapAttribute(type);
			if (!soapTypeAttribute.IsInteropXmlElement)
			{
				xmlElement = null;
				xmlNamespace = null;
				return false;
			}
			xmlElement = soapTypeAttribute.XmlElementName;
			xmlNamespace = soapTypeAttribute.XmlNamespace;
			return true;
		}

		/// <summary>Retrieves the XML namespace used during remote calls of the method specified in the given <see cref="T:System.Reflection.MethodBase" />.</summary>
		/// <returns>The XML namespace used during remote calls of the specified method.</returns>
		/// <param name="mb">The <see cref="T:System.Reflection.MethodBase" /> of the method for which the XML namespace was requested. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002DB8 RID: 11704 RVA: 0x0009841C File Offset: 0x0009661C
		public static string GetXmlNamespaceForMethodCall(MethodBase mb)
		{
			return SoapServices.CodeXmlNamespaceForClrTypeNamespace(mb.DeclaringType.FullName, SoapServices.GetAssemblyName(mb));
		}

		/// <summary>Retrieves the XML namespace used during the generation of responses to the remote call to the method specified in the given <see cref="T:System.Reflection.MethodBase" />.</summary>
		/// <returns>The XML namespace used during the generation of responses to a remote method call.</returns>
		/// <param name="mb">The <see cref="T:System.Reflection.MethodBase" /> of the method for which the XML namespace was requested. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002DB9 RID: 11705 RVA: 0x00098434 File Offset: 0x00096634
		public static string GetXmlNamespaceForMethodResponse(MethodBase mb)
		{
			return SoapServices.CodeXmlNamespaceForClrTypeNamespace(mb.DeclaringType.FullName, SoapServices.GetAssemblyName(mb));
		}

		/// <summary>Returns XML type information that should be used when serializing the given <see cref="T:System.Type" />.</summary>
		/// <returns>true if the requested values have been set flagged with <see cref="T:System.Runtime.Remoting.Metadata.SoapTypeAttribute" />; otherwise, false.</returns>
		/// <param name="type">The object <see cref="T:System.Type" /> for which the XML element and namespace names were requested. </param>
		/// <param name="xmlType">The XML type of the specified object <see cref="T:System.Type" />. </param>
		/// <param name="xmlTypeNamespace">The XML type namespace of the specified object <see cref="T:System.Type" />. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002DBA RID: 11706 RVA: 0x0009844C File Offset: 0x0009664C
		public static bool GetXmlTypeForInteropType(Type type, out string xmlType, out string xmlTypeNamespace)
		{
			SoapTypeAttribute soapTypeAttribute = (SoapTypeAttribute)InternalRemotingServices.GetCachedSoapAttribute(type);
			if (!soapTypeAttribute.IsInteropXmlType)
			{
				xmlType = null;
				xmlTypeNamespace = null;
				return false;
			}
			xmlType = soapTypeAttribute.XmlTypeName;
			xmlTypeNamespace = soapTypeAttribute.XmlTypeNamespace;
			return true;
		}

		/// <summary>Returns a Boolean value that indicates whether the specified namespace is native to the common language runtime.</summary>
		/// <returns>true if the given namespace is native to the common language runtime; otherwise, false.</returns>
		/// <param name="namespaceString">The namespace to check in the common language runtime. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002DBB RID: 11707 RVA: 0x0009848C File Offset: 0x0009668C
		public static bool IsClrTypeNamespace(string namespaceString)
		{
			return namespaceString.StartsWith(SoapServices.XmlNsForClrType);
		}

		/// <summary>Determines if the specified SOAPAction is acceptable for a given <see cref="T:System.Reflection.MethodBase" />.</summary>
		/// <returns>true if the specified SOAPAction is acceptable for a given <see cref="T:System.Reflection.MethodBase" />; otherwise, false.</returns>
		/// <param name="soapAction">The SOAPAction to check against the given <see cref="T:System.Reflection.MethodBase" />. </param>
		/// <param name="mb">The <see cref="T:System.Reflection.MethodBase" /> the specified SOAPAction is checked against. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002DBC RID: 11708 RVA: 0x0009849C File Offset: 0x0009669C
		public static bool IsSoapActionValidForMethodBase(string soapAction, MethodBase mb)
		{
			string a;
			string a2;
			SoapServices.GetTypeAndMethodNameFromSoapAction(soapAction, out a, out a2);
			if (a2 != mb.Name)
			{
				return false;
			}
			string assemblyQualifiedName = mb.DeclaringType.AssemblyQualifiedName;
			return a == assemblyQualifiedName;
		}

		/// <summary>Preloads every <see cref="T:System.Type" /> found in the specified <see cref="T:System.Reflection.Assembly" /> from the information found in the <see cref="T:System.Runtime.Remoting.Metadata.SoapTypeAttribute" /> associated with each type.</summary>
		/// <param name="assembly">The <see cref="T:System.Reflection.Assembly" /> for each type of which to call <see cref="M:System.Runtime.Remoting.SoapServices.PreLoad(System.Type)" />. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002DBD RID: 11709 RVA: 0x000984DC File Offset: 0x000966DC
		public static void PreLoad(Assembly assembly)
		{
			foreach (Type type in assembly.GetTypes())
			{
				SoapServices.PreLoad(type);
			}
		}

		/// <summary>Preloads the given <see cref="T:System.Type" /> based on values set in a <see cref="T:System.Runtime.Remoting.Metadata.SoapTypeAttribute" /> on the type.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> to preload. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002DBE RID: 11710 RVA: 0x00098510 File Offset: 0x00096710
		public static void PreLoad(Type type)
		{
			SoapServices.TypeInfo typeInfo = SoapServices._typeInfos[type] as SoapServices.TypeInfo;
			if (typeInfo != null)
			{
				return;
			}
			string text;
			string text2;
			if (SoapServices.GetXmlTypeForInteropType(type, out text, out text2))
			{
				SoapServices.RegisterInteropXmlType(text, text2, type);
			}
			if (SoapServices.GetXmlElementForInteropType(type, out text, out text2))
			{
				SoapServices.RegisterInteropXmlElement(text, text2, type);
			}
			object syncRoot = SoapServices._typeInfos.SyncRoot;
			lock (syncRoot)
			{
				typeInfo = new SoapServices.TypeInfo();
				FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				foreach (FieldInfo fieldInfo in fields)
				{
					SoapFieldAttribute soapFieldAttribute = (SoapFieldAttribute)InternalRemotingServices.GetCachedSoapAttribute(fieldInfo);
					if (soapFieldAttribute.IsInteropXmlElement())
					{
						string nameKey = SoapServices.GetNameKey(soapFieldAttribute.XmlElementName, soapFieldAttribute.XmlNamespace);
						if (soapFieldAttribute.UseAttribute)
						{
							if (typeInfo.Attributes == null)
							{
								typeInfo.Attributes = new Hashtable();
							}
							typeInfo.Attributes[nameKey] = fieldInfo;
						}
						else
						{
							if (typeInfo.Elements == null)
							{
								typeInfo.Elements = new Hashtable();
							}
							typeInfo.Elements[nameKey] = fieldInfo;
						}
					}
				}
				SoapServices._typeInfos[type] = typeInfo;
			}
		}

		/// <summary>Associates the given XML element name and namespace with a run-time type that should be used for deserialization.</summary>
		/// <param name="xmlElement">The XML element name to use in deserialization. </param>
		/// <param name="xmlNamespace">The XML namespace to use in deserialization. </param>
		/// <param name="type">The run-time <see cref="T:System.Type" /> to use in deserialization. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002DBF RID: 11711 RVA: 0x0009866C File Offset: 0x0009686C
		public static void RegisterInteropXmlElement(string xmlElement, string xmlNamespace, Type type)
		{
			object syncRoot = SoapServices._xmlElements.SyncRoot;
			lock (syncRoot)
			{
				SoapServices._xmlElements[xmlElement + " " + xmlNamespace] = type;
			}
		}

		/// <summary>Associates the given XML type name and namespace with the run-time type that should be used for deserialization.</summary>
		/// <param name="xmlType">The XML type to use in deserialization. </param>
		/// <param name="xmlTypeNamespace">The XML namespace to use in deserialization. </param>
		/// <param name="type">The run-time <see cref="T:System.Type" /> to use in deserialization. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002DC0 RID: 11712 RVA: 0x000986CC File Offset: 0x000968CC
		public static void RegisterInteropXmlType(string xmlType, string xmlTypeNamespace, Type type)
		{
			object syncRoot = SoapServices._xmlTypes.SyncRoot;
			lock (syncRoot)
			{
				SoapServices._xmlTypes[xmlType + " " + xmlTypeNamespace] = type;
			}
		}

		/// <summary>Associates the specified <see cref="T:System.Reflection.MethodBase" /> with the SOAPAction cached with it.</summary>
		/// <param name="mb">The <see cref="T:System.Reflection.MethodBase" /> of the method to associate with the SOAPAction cached with it. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002DC1 RID: 11713 RVA: 0x0009872C File Offset: 0x0009692C
		public static void RegisterSoapActionForMethodBase(MethodBase mb)
		{
			SoapServices.InternalGetSoapAction(mb);
		}

		// Token: 0x06002DC2 RID: 11714 RVA: 0x00098738 File Offset: 0x00096938
		private static string InternalGetSoapAction(MethodBase mb)
		{
			object syncRoot = SoapServices._soapActions.SyncRoot;
			string result;
			lock (syncRoot)
			{
				string text = (string)SoapServices._soapActions[mb];
				if (text == null)
				{
					SoapMethodAttribute soapMethodAttribute = (SoapMethodAttribute)InternalRemotingServices.GetCachedSoapAttribute(mb);
					text = soapMethodAttribute.SoapAction;
					SoapServices._soapActions[mb] = text;
					SoapServices._soapActionsMethods[text] = mb;
				}
				result = text;
			}
			return result;
		}

		/// <summary>Associates the provided SOAPAction value with the given <see cref="T:System.Reflection.MethodBase" /> for use in channel sinks.</summary>
		/// <param name="mb">The <see cref="T:System.Reflection.MethodBase" /> to associate with the provided SOAPAction. </param>
		/// <param name="soapAction">The SOAPAction value to associate with the given <see cref="T:System.Reflection.MethodBase" />. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002DC3 RID: 11715 RVA: 0x000987C8 File Offset: 0x000969C8
		public static void RegisterSoapActionForMethodBase(MethodBase mb, string soapAction)
		{
			object syncRoot = SoapServices._soapActions.SyncRoot;
			lock (syncRoot)
			{
				SoapServices._soapActions[mb] = soapAction;
				SoapServices._soapActionsMethods[soapAction] = mb;
			}
		}

		// Token: 0x06002DC4 RID: 11716 RVA: 0x00098828 File Offset: 0x00096A28
		private static string EncodeNs(string ns)
		{
			ns = ns.Replace(",", "%2C");
			ns = ns.Replace(" ", "%20");
			return ns.Replace("=", "%3D");
		}

		// Token: 0x06002DC5 RID: 11717 RVA: 0x0009886C File Offset: 0x00096A6C
		private static string DecodeNs(string ns)
		{
			ns = ns.Replace("%2C", ",");
			ns = ns.Replace("%20", " ");
			return ns.Replace("%3D", "=");
		}

		// Token: 0x040013A4 RID: 5028
		private static Hashtable _xmlTypes = new Hashtable();

		// Token: 0x040013A5 RID: 5029
		private static Hashtable _xmlElements = new Hashtable();

		// Token: 0x040013A6 RID: 5030
		private static Hashtable _soapActions = new Hashtable();

		// Token: 0x040013A7 RID: 5031
		private static Hashtable _soapActionsMethods = new Hashtable();

		// Token: 0x040013A8 RID: 5032
		private static Hashtable _typeInfos = new Hashtable();

		// Token: 0x02000433 RID: 1075
		private class TypeInfo
		{
			// Token: 0x040013A9 RID: 5033
			public Hashtable Attributes;

			// Token: 0x040013AA RID: 5034
			public Hashtable Elements;
		}
	}
}
