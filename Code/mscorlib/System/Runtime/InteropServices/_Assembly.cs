﻿using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Policy;

namespace System.Runtime.InteropServices
{
	/// <summary>Exposes the public members of the <see cref="T:System.Reflection.Assembly" /> class to unmanaged code.</summary>
	// Token: 0x0200034F RID: 847
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	[TypeLibImportClass(typeof(Assembly))]
	[Guid("17156360-2F1A-384A-BC52-FDE93C215C5B")]
	[ComVisible(true)]
	[CLSCompliant(false)]
	public interface _Assembly
	{
		/// <summary>Provides COM objects with version-independent access to the <see cref="E:System.Reflection.Assembly.ModuleResolve" /> event.</summary>
		// Token: 0x14000016 RID: 22
		// (add) Token: 0x060028CB RID: 10443
		// (remove) Token: 0x060028CC RID: 10444
		event ModuleResolveEventHandler ModuleResolve;

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.ToString" /> method.</summary>
		/// <returns>The full name of the assembly, or the class name if the full name of the assembly cannot be determined.</returns>
		// Token: 0x060028CD RID: 10445
		string ToString();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Object.Equals(System.Object)" /> method.</summary>
		/// <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.</returns>
		/// <param name="other">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />.</param>
		// Token: 0x060028CE RID: 10446
		bool Equals(object other);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Object.GetHashCode" /> method.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
		// Token: 0x060028CF RID: 10447
		int GetHashCode();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Object.GetType" /> method.</summary>
		/// <returns>A <see cref="T:System.Type" /> object.</returns>
		// Token: 0x060028D0 RID: 10448
		Type GetType();

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.Assembly.CodeBase" /> property.</summary>
		/// <returns>The location of the assembly as specified originally.</returns>
		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x060028D1 RID: 10449
		string CodeBase { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.Assembly.EscapedCodeBase" /> property.</summary>
		/// <returns>A Uniform Resource Identifier (URI) with escape characters.</returns>
		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x060028D2 RID: 10450
		string EscapedCodeBase { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetName" /> method.</summary>
		/// <returns>An <see cref="T:System.Reflection.AssemblyName" /> for this assembly.</returns>
		// Token: 0x060028D3 RID: 10451
		AssemblyName GetName();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetName(System.Boolean)" /> method.</summary>
		/// <returns>An <see cref="T:System.Reflection.AssemblyName" /> for this assembly.</returns>
		/// <param name="copiedName">true to set the <see cref="P:System.Reflection.Assembly.CodeBase" /> to the location of the assembly after it was shadow copied; false to set <see cref="P:System.Reflection.Assembly.CodeBase" /> to the original location.</param>
		// Token: 0x060028D4 RID: 10452
		AssemblyName GetName(bool copiedName);

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.Assembly.FullName" /> property.</summary>
		/// <returns>The display name of the assembly.</returns>
		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x060028D5 RID: 10453
		string FullName { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.Assembly.EntryPoint" /> property.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object that represents the entry point of this assembly. If no entry point is found (for example, the assembly is a DLL), null is returned.</returns>
		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x060028D6 RID: 10454
		MethodInfo EntryPoint { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetType(System.String)" /> method.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the specified class, or null if the class is not found.</returns>
		/// <param name="name">The full name of the type.</param>
		// Token: 0x060028D7 RID: 10455
		Type GetType(string name);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetType(System.String,System.Boolean)" /> method.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the specified class.</returns>
		/// <param name="name">The full name of the type. </param>
		/// <param name="throwOnError">true to throw an exception if the type is not found; false to return null. </param>
		// Token: 0x060028D8 RID: 10456
		Type GetType(string name, bool throwOnError);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetExportedTypes" /> property.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects that represent the types defined in this assembly that are visible outside the assembly.</returns>
		// Token: 0x060028D9 RID: 10457
		Type[] GetExportedTypes();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetTypes" /> method.</summary>
		/// <returns>An array of type <see cref="T:System.Type" /> containing objects for all the types defined in this assembly.</returns>
		// Token: 0x060028DA RID: 10458
		Type[] GetTypes();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetManifestResourceStream(System.Type,System.String)" /> method.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> representing this manifest resource.</returns>
		/// <param name="type">The type whose namespace is used to scope the manifest resource name. </param>
		/// <param name="name">The case-sensitive name of the manifest resource being requested. </param>
		// Token: 0x060028DB RID: 10459
		Stream GetManifestResourceStream(Type type, string name);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetManifestResourceStream(System.String)" /> method.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> representing this manifest resource.</returns>
		/// <param name="name">The case-sensitive name of the manifest resource being requested.</param>
		// Token: 0x060028DC RID: 10460
		Stream GetManifestResourceStream(string name);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetFile(System.String)" /> method.</summary>
		/// <returns>A <see cref="T:System.IO.FileStream" /> for the specified file, or null if the file is not found.</returns>
		/// <param name="name">The name of the specified file. Do not include the path to the file.</param>
		// Token: 0x060028DD RID: 10461
		FileStream GetFile(string name);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetFiles" /> method.</summary>
		/// <returns>An array of <see cref="T:System.IO.FileStream" /> objects.</returns>
		// Token: 0x060028DE RID: 10462
		FileStream[] GetFiles();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetFiles(System.Boolean)" /> method.</summary>
		/// <returns>An array of <see cref="T:System.IO.FileStream" /> objects.</returns>
		/// <param name="getResourceModules">true to include resource modules; otherwise, false.</param>
		// Token: 0x060028DF RID: 10463
		FileStream[] GetFiles(bool getResourceModules);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetManifestResourceNames" /> method.</summary>
		/// <returns>An array of type String containing the names of all the resources.</returns>
		// Token: 0x060028E0 RID: 10464
		string[] GetManifestResourceNames();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetManifestResourceInfo(System.String)" /> method.</summary>
		/// <returns>A <see cref="T:System.Reflection.ManifestResourceInfo" /> object populated with information about the resource's topology, or null if the resource is not found.</returns>
		/// <param name="resourceName">The case-sensitive name of the resource.</param>
		// Token: 0x060028E1 RID: 10465
		ManifestResourceInfo GetManifestResourceInfo(string resourceName);

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.Assembly.Location" /> property.</summary>
		/// <returns>The location of the loaded file that contains the manifest. If the loaded file was shadow-copied, the location is that of the file after being shadow-copied. If the assembly is loaded from a byte array, such as when using the <see cref="M:System.Reflection.Assembly.Load(System.Byte[])" /> method overload, the value returned is an empty string ("").</returns>
		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x060028E2 RID: 10466
		string Location { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.Assembly.Evidence" /> property.</summary>
		/// <returns>An <see cref="T:System.Security.Policy.Evidence" /> object for this assembly.</returns>
		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x060028E3 RID: 10467
		Evidence Evidence { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetCustomAttributes(System.Type,System.Boolean)" /> method.</summary>
		/// <returns>An array of type <see cref="T:System.Object" /> containing the custom attributes for this assembly as specified by <paramref name="attributeType" />.</returns>
		/// <param name="attributeType">The <see cref="T:System.Type" /> for which the custom attributes are to be returned. </param>
		/// <param name="inherit">This argument is ignored for objects of type <see cref="T:System.Reflection.Assembly" />. </param>
		// Token: 0x060028E4 RID: 10468
		object[] GetCustomAttributes(Type attributeType, bool inherit);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetCustomAttributes(System.Boolean)" /> method.</summary>
		/// <returns>An array of type Object containing the custom attributes for this assembly.</returns>
		/// <param name="inherit">This argument is ignored for objects of type <see cref="T:System.Reflection.Assembly" />.</param>
		// Token: 0x060028E5 RID: 10469
		object[] GetCustomAttributes(bool inherit);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.IsDefined(System.Type,System.Boolean)" /> method.</summary>
		/// <returns>true if a custom attribute identified by the specified <see cref="T:System.Type" /> is defined; otherwise, false.</returns>
		/// <param name="attributeType">The <see cref="T:System.Type" /> of the custom attribute to be checked for this assembly. </param>
		/// <param name="inherit">This argument is ignored for objects of this type. </param>
		// Token: 0x060028E6 RID: 10470
		bool IsDefined(Type attributeType, bool inherit);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" /> method.</summary>
		/// <param name="info">The object to be populated with serialization information. </param>
		/// <param name="context">The destination context of the serialization. </param>
		// Token: 0x060028E7 RID: 10471
		void GetObjectData(SerializationInfo info, StreamingContext context);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetType(System.String,System.Boolean,System.Boolean)" /> method.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the specified class.</returns>
		/// <param name="name">The full name of the type. </param>
		/// <param name="throwOnError">true to throw an exception if the type is not found; false to return null. </param>
		/// <param name="ignoreCase">true to ignore the case of the type name; otherwise, false. </param>
		// Token: 0x060028E8 RID: 10472
		Type GetType(string name, bool throwOnError, bool ignoreCase);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetSatelliteAssembly(System.Globalization.CultureInfo)" /> method.</summary>
		/// <returns>The specified satellite assembly.</returns>
		/// <param name="culture">The specified culture.</param>
		// Token: 0x060028E9 RID: 10473
		Assembly GetSatelliteAssembly(CultureInfo culture);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetSatelliteAssembly(System.Globalization.CultureInfo,System.Version)" /> method.</summary>
		/// <returns>The specified satellite assembly.</returns>
		/// <param name="culture">The specified culture. </param>
		/// <param name="version">The version of the satellite assembly. </param>
		// Token: 0x060028EA RID: 10474
		Assembly GetSatelliteAssembly(CultureInfo culture, Version version);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.LoadModule(System.String,System.Byte[])" /> method.</summary>
		/// <returns>The loaded Module.</returns>
		/// <param name="moduleName">Name of the module. Must correspond to a file name in this assembly's manifest. </param>
		/// <param name="rawModule">A byte array that is a COFF-based image containing an emitted module, or a resource. </param>
		// Token: 0x060028EB RID: 10475
		Module LoadModule(string moduleName, byte[] rawModule);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.LoadModule(System.String,System.Byte[],System.Byte[])" /> method.</summary>
		/// <returns>The loaded module.</returns>
		/// <param name="moduleName">Name of the module. Must correspond to a file name in this assembly's manifest. </param>
		/// <param name="rawModule">A byte array that is a COFF-based image containing an emitted module, or a resource. </param>
		/// <param name="rawSymbolStore">A byte array containing the raw bytes representing the symbols for the module. Must be null if this is a resource file. </param>
		// Token: 0x060028EC RID: 10476
		Module LoadModule(string moduleName, byte[] rawModule, byte[] rawSymbolStore);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.CreateInstance(System.String)" /> method.</summary>
		/// <returns>An instance of <see cref="T:System.Object" /> representing the type, with culture, arguments, binder, and activation attributes set to null, and <see cref="T:System.Reflection.BindingFlags" /> set to Public or Instance, or null if <paramref name="typeName" /> is not found.</returns>
		/// <param name="typeName">The <see cref="P:System.Type.FullName" /> of the type to locate.</param>
		// Token: 0x060028ED RID: 10477
		object CreateInstance(string typeName);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.CreateInstance(System.String,System.Boolean)" /> method.</summary>
		/// <returns>An instance of <see cref="T:System.Object" /> representing the type, with culture, arguments, binder, and activation attributes set to null, and <see cref="T:System.Reflection.BindingFlags" /> set to Public or Instance, or null if <paramref name="typeName" /> is not found.</returns>
		/// <param name="typeName">The <see cref="P:System.Type.FullName" /> of the type to locate. </param>
		/// <param name="ignoreCase">true to ignore the case of the type name; otherwise, false. </param>
		// Token: 0x060028EE RID: 10478
		object CreateInstance(string typeName, bool ignoreCase);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.CreateInstance(System.String,System.Boolean,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object[],System.Globalization.CultureInfo,System.Object[])" /> method.</summary>
		/// <returns>An instance of Object representing the type and matching the specified criteria, or null if <paramref name="typeName" /> is not found.</returns>
		/// <param name="typeName">The <see cref="P:System.Type.FullName" /> of the type to locate. </param>
		/// <param name="ignoreCase">true to ignore the case of the type name; otherwise, false. </param>
		/// <param name="bindingAttr">A bitmask that affects how the search is conducted. The value is a combination of bit flags from <see cref="T:System.Reflection.BindingFlags" />. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of MemberInfo objects via reflection. If <paramref name="binder" /> is null, the default binder is used. </param>
		/// <param name="args">An array of type Object containing the arguments to be passed to the constructor. This array of arguments must match in number, order, and type the parameters of the constructor to be invoked. If the default constructor is desired, <paramref name="args" /> must be an empty array or null. </param>
		/// <param name="culture">An instance of CultureInfo used to govern the coercion of types. If this is null, the CultureInfo for the current thread is used. (This is necessary to convert a String that represents 1000 to a Double value, for example, since 1000 is represented differently by different cultures.) </param>
		/// <param name="activationAttributes">An array of type Object containing one or more activation attributes that can participate in the activation. An example of an activation attribute is: URLAttribute(http://hostname/appname/objectURI) </param>
		// Token: 0x060028EF RID: 10479
		object CreateInstance(string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetLoadedModules" /> method.</summary>
		/// <returns>An array of modules.</returns>
		// Token: 0x060028F0 RID: 10480
		Module[] GetLoadedModules();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetLoadedModules(System.Boolean)" /> method.</summary>
		/// <returns>An array of modules.</returns>
		/// <param name="getResourceModules">true to include resource modules; otherwise, false.</param>
		// Token: 0x060028F1 RID: 10481
		Module[] GetLoadedModules(bool getResourceModules);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetModules" /> method.</summary>
		/// <returns>An array of modules.</returns>
		// Token: 0x060028F2 RID: 10482
		Module[] GetModules();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetModules(System.Boolean)" /> method.</summary>
		/// <returns>An array of modules.</returns>
		/// <param name="getResourceModules">true to include resource modules; otherwise, false.</param>
		// Token: 0x060028F3 RID: 10483
		Module[] GetModules(bool getResourceModules);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetModule(System.String)" /> method.</summary>
		/// <returns>The module being requested, or null if the module is not found.</returns>
		/// <param name="name">The name of the module being requested.</param>
		// Token: 0x060028F4 RID: 10484
		Module GetModule(string name);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetReferencedAssemblies" /> method.</summary>
		/// <returns>An array of type <see cref="T:System.Reflection.AssemblyName" /> containing all the assemblies referenced by this assembly.</returns>
		// Token: 0x060028F5 RID: 10485
		AssemblyName[] GetReferencedAssemblies();

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.Assembly.GlobalAssemblyCache" /> property.</summary>
		/// <returns>true if the assembly was loaded from the global assembly cache; otherwise, false.</returns>
		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x060028F6 RID: 10486
		bool GlobalAssemblyCache { get; }
	}
}
