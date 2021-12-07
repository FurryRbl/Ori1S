using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.Reflection
{
	/// <summary>Performs reflection on a module.</summary>
	// Token: 0x020002A0 RID: 672
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_Module))]
	[Serializable]
	public class Module : ISerializable, ICustomAttributeProvider, _Module
	{
		// Token: 0x060021EC RID: 8684 RVA: 0x0007AD90 File Offset: 0x00078F90
		internal Module()
		{
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array that receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060021EE RID: 8686 RVA: 0x0007ADC8 File Offset: 0x00078FC8
		void _Module.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060021EF RID: 8687 RVA: 0x0007ADD0 File Offset: 0x00078FD0
		void _Module.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060021F0 RID: 8688 RVA: 0x0007ADD8 File Offset: 0x00078FD8
		void _Module.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides access to properties and methods exposed by an object.</summary>
		/// <param name="dispIdMember">Identifies the member.</param>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="lcid">The locale context in which to interpret arguments.</param>
		/// <param name="wFlags">Flags describing the context of the call.</param>
		/// <param name="pDispParams">Pointer to a structure containing an array of arguments, an array of argument DispIDs for named arguments, and counts for the number of elements in the arrays.</param>
		/// <param name="pVarResult">Pointer to the location where the result is to be stored.</param>
		/// <param name="pExcepInfo">Pointer to a structure that contains exception information.</param>
		/// <param name="puArgErr">The index of the first argument that has an error.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060021F1 RID: 8689 RVA: 0x0007ADE0 File Offset: 0x00078FE0
		void _Module.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the appropriate <see cref="T:System.Reflection.Assembly" /> for this instance of <see cref="T:System.Reflection.Module" />.</summary>
		/// <returns>An Assembly object.</returns>
		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x060021F2 RID: 8690 RVA: 0x0007ADE8 File Offset: 0x00078FE8
		public Assembly Assembly
		{
			get
			{
				return this.assembly;
			}
		}

		/// <summary>Gets a string representing the fully qualified name and path to this module.</summary>
		/// <returns>The fully qualified module name.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permissions. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x060021F3 RID: 8691 RVA: 0x0007ADF0 File Offset: 0x00078FF0
		public virtual string FullyQualifiedName
		{
			get
			{
				if (SecurityManager.SecurityEnabled)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.fqname).Demand();
				}
				return this.fqname;
			}
		}

		/// <summary>Gets a String representing the name of the module with the path removed.</summary>
		/// <returns>The module name with no path.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x060021F4 RID: 8692 RVA: 0x0007AE14 File Offset: 0x00079014
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets a string representing the name of the module.</summary>
		/// <returns>The module name.</returns>
		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x060021F5 RID: 8693 RVA: 0x0007AE1C File Offset: 0x0007901C
		public string ScopeName
		{
			get
			{
				return this.scopename;
			}
		}

		/// <summary>Gets a handle for the module.</summary>
		/// <returns>A <see cref="T:System.ModuleHandle" /> structure for the current module.</returns>
		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x060021F6 RID: 8694 RVA: 0x0007AE24 File Offset: 0x00079024
		public ModuleHandle ModuleHandle
		{
			get
			{
				return new ModuleHandle(this._impl);
			}
		}

		/// <summary>Gets a token that identifies the module in metadata.</summary>
		/// <returns>An integer token that identifies the current module in metadata.</returns>
		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x060021F7 RID: 8695
		public extern int MetadataToken { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		/// <summary>Gets the metadata stream version.</summary>
		/// <returns>A 32-bit integer representing the metadata stream version. The high-order two bytes represent the major version number, and the low-order two bytes represent the minor version number.</returns>
		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x060021F8 RID: 8696 RVA: 0x0007AE34 File Offset: 0x00079034
		public int MDStreamVersion
		{
			get
			{
				if (this._impl == IntPtr.Zero)
				{
					throw new NotSupportedException();
				}
				return Module.GetMDStreamVersion(this._impl);
			}
		}

		// Token: 0x060021F9 RID: 8697
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetMDStreamVersion(IntPtr module_handle);

		/// <summary>Returns an array of classes accepted by the given filter and filter criteria.</summary>
		/// <returns>An array of type Type containing classes that were accepted by the filter.</returns>
		/// <param name="filter">The delegate used to filter the classes. </param>
		/// <param name="filterCriteria">An Object used to filter the classes. </param>
		/// <exception cref="T:System.Reflection.ReflectionTypeLoadException">One or more classes in a module could not be loaded. </exception>
		// Token: 0x060021FA RID: 8698 RVA: 0x0007AE68 File Offset: 0x00079068
		public virtual Type[] FindTypes(TypeFilter filter, object filterCriteria)
		{
			ArrayList arrayList = new ArrayList();
			Type[] types = this.GetTypes();
			foreach (Type type in types)
			{
				if (filter(type, filterCriteria))
				{
					arrayList.Add(type);
				}
			}
			return (Type[])arrayList.ToArray(typeof(Type));
		}

		/// <summary>Returns all custom attributes.</summary>
		/// <returns>An array of type Object containing all custom attributes.</returns>
		/// <param name="inherit">This argument is ignored for objects of this type. </param>
		// Token: 0x060021FB RID: 8699 RVA: 0x0007AECC File Offset: 0x000790CC
		public virtual object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		/// <summary>Gets custom attributes of the specified type.</summary>
		/// <returns>An array of type Object containing all custom attributes of the specified type.</returns>
		/// <param name="attributeType">The type of attribute to get. </param>
		/// <param name="inherit">This argument is ignored for objects of this type. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not a <see cref="T:System.Type" /> object supplied by the runtime. For example, <paramref name="attributeType" /> is a <see cref="T:System.Reflection.Emit.TypeBuilder" /> object.</exception>
		// Token: 0x060021FC RID: 8700 RVA: 0x0007AED8 File Offset: 0x000790D8
		public virtual object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		/// <summary>Returns a field having the specified name.</summary>
		/// <returns>A FieldInfo object having the specified name, or null if the field does not exist.</returns>
		/// <param name="name">The field name. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		// Token: 0x060021FD RID: 8701 RVA: 0x0007AEE4 File Offset: 0x000790E4
		public FieldInfo GetField(string name)
		{
			if (this.IsResource())
			{
				return null;
			}
			Type globalType = this.GetGlobalType();
			return (globalType == null) ? null : globalType.GetField(name, BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>Returns a field having the specified name and binding attributes.</summary>
		/// <returns>A FieldInfo object having the specified name and binding attributes, or null if the field does not exist.</returns>
		/// <param name="name">The field name. </param>
		/// <param name="bindingAttr">One of the BindingFlags bit flags used to control the search. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		// Token: 0x060021FE RID: 8702 RVA: 0x0007AF1C File Offset: 0x0007911C
		public FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			if (this.IsResource())
			{
				return null;
			}
			Type globalType = this.GetGlobalType();
			return (globalType == null) ? null : globalType.GetField(name, bindingAttr);
		}

		/// <summary>Returns the global fields defined on the module.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.FieldInfo" /> objects representing the global fields defined on the module; if there are no global fields, an empty array is returned.</returns>
		// Token: 0x060021FF RID: 8703 RVA: 0x0007AF54 File Offset: 0x00079154
		public FieldInfo[] GetFields()
		{
			if (this.IsResource())
			{
				return new FieldInfo[0];
			}
			Type globalType = this.GetGlobalType();
			return (globalType == null) ? new FieldInfo[0] : globalType.GetFields(BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>Returns a method having the specified name.</summary>
		/// <returns>A MethodInfo object having the specified name, or null if the method does not exist.</returns>
		/// <param name="name">The method name. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		// Token: 0x06002200 RID: 8704 RVA: 0x0007AF94 File Offset: 0x00079194
		public MethodInfo GetMethod(string name)
		{
			if (this.IsResource())
			{
				return null;
			}
			Type globalType = this.GetGlobalType();
			return (globalType == null) ? null : globalType.GetMethod(name);
		}

		/// <summary>Returns a method having the specified name and parameter types.</summary>
		/// <returns>A MethodInfo object in accordance with the specified criteria, or null if the method does not exist.</returns>
		/// <param name="name">The method name. </param>
		/// <param name="types">The parameter types to search for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null, <paramref name="types" /> is null, or <paramref name="types" /> (i) is null. </exception>
		// Token: 0x06002201 RID: 8705 RVA: 0x0007AFC8 File Offset: 0x000791C8
		public MethodInfo GetMethod(string name, Type[] types)
		{
			return this.GetMethodImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, CallingConventions.Any, types, null);
		}

		/// <summary>Returns a method having the specified name, binding information, calling convention, and parameter types and modifiers.</summary>
		/// <returns>A MethodInfo object in accordance with the specified criteria, or null if the method does not exist.</returns>
		/// <param name="name">The method name. </param>
		/// <param name="bindingAttr">One of the BindingFlags bit flags used to control the search. </param>
		/// <param name="binder">An object that implements Binder, containing properties related to this method. </param>
		/// <param name="callConvention">The calling convention for the method. </param>
		/// <param name="types">The parameter types to search for. </param>
		/// <param name="modifiers">An array of parameter modifiers used to make binding work with parameter signatures in which the types have been modified. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null, <paramref name="types" /> is null, or <paramref name="types" /> (i) is null. </exception>
		// Token: 0x06002202 RID: 8706 RVA: 0x0007AFD8 File Offset: 0x000791D8
		public MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetMethodImpl(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		/// <summary>Returns the method implementation in accordance with the specified criteria.</summary>
		/// <returns>A MethodInfo object containing implementation information as specified, or null if the method does not exist.</returns>
		/// <param name="name">The method name. </param>
		/// <param name="bindingAttr">One of the BindingFlags bit flags used to control the search. </param>
		/// <param name="binder">An object that implements Binder, containing properties related to this method. </param>
		/// <param name="callConvention">The calling convention for the method. </param>
		/// <param name="types">The parameter types to search for. </param>
		/// <param name="modifiers">An array of parameter modifiers used to make binding work with parameter signatures in which the types have been modified. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">
		///   <paramref name="types" /> is null. </exception>
		// Token: 0x06002203 RID: 8707 RVA: 0x0007AFEC File Offset: 0x000791EC
		protected virtual MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (this.IsResource())
			{
				return null;
			}
			Type globalType = this.GetGlobalType();
			return (globalType == null) ? null : globalType.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		/// <summary>Returns the global methods defined on the module.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.MethodInfo" /> objects representing all the global methods defined on the module; if there are no global methods, an empty array is returned.</returns>
		// Token: 0x06002204 RID: 8708 RVA: 0x0007B028 File Offset: 0x00079228
		public MethodInfo[] GetMethods()
		{
			if (this.IsResource())
			{
				return new MethodInfo[0];
			}
			Type globalType = this.GetGlobalType();
			return (globalType == null) ? new MethodInfo[0] : globalType.GetMethods();
		}

		/// <summary>Returns the global methods defined on the module that match the specified binding flags.</summary>
		/// <returns>An array of type <see cref="T:System.Reflection.MethodInfo" /> representing the global methods defined on the module that match the specified binding flags; if no global methods match the binding flags, an empty array is returned.</returns>
		/// <param name="bindingFlags">A bitwise combination of <see cref="T:System.Reflection.BindingFlags" /> values that limit the search.</param>
		// Token: 0x06002205 RID: 8709 RVA: 0x0007B068 File Offset: 0x00079268
		public MethodInfo[] GetMethods(BindingFlags bindingFlags)
		{
			if (this.IsResource())
			{
				return new MethodInfo[0];
			}
			Type globalType = this.GetGlobalType();
			return (globalType == null) ? new MethodInfo[0] : globalType.GetMethods(bindingFlags);
		}

		/// <summary>Returns the global fields defined on the module that match the specified binding flags.</summary>
		/// <returns>An array of type <see cref="T:System.Reflection.FieldInfo" /> representing the global fields defined on the module that match the specified binding flags; if no global fields match the binding flags, an empty array is returned.</returns>
		/// <param name="bindingFlags">A bitwise combination of <see cref="T:System.Reflection.BindingFlags" /> values that limit the search.</param>
		// Token: 0x06002206 RID: 8710 RVA: 0x0007B0A8 File Offset: 0x000792A8
		public FieldInfo[] GetFields(BindingFlags bindingFlags)
		{
			if (this.IsResource())
			{
				return new FieldInfo[0];
			}
			Type globalType = this.GetGlobalType();
			return (globalType == null) ? new FieldInfo[0] : globalType.GetFields(bindingFlags);
		}

		/// <summary>Provides an <see cref="T:System.Runtime.Serialization.ISerializable" /> implementation for serialized objects.</summary>
		/// <param name="info">The information and data needed to serialize or deserialize an object. </param>
		/// <param name="context">The context for the serialization. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x06002207 RID: 8711 RVA: 0x0007B0E8 File Offset: 0x000792E8
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"SerializationFormatter\"/>\n</PermissionSet>\n")]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			UnitySerializationHolder.GetModuleData(this, info, context);
		}

		/// <summary>Returns an X509Certificate object corresponding to the certificate included in the Authenticode signature of the assembly which this module belongs to. If the assembly has not been Authenticode signed, null is returned.</summary>
		/// <returns>An X509Certificate object, or null if the assembly to which this module belongs has not been Authenticode signed.</returns>
		// Token: 0x06002208 RID: 8712 RVA: 0x0007B104 File Offset: 0x00079304
		public X509Certificate GetSignerCertificate()
		{
			X509Certificate result;
			try
			{
				result = X509Certificate.CreateFromSignedFile(this.assembly.Location);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		/// <summary>Returns the specified type, performing a case-sensitive search.</summary>
		/// <returns>A Type object representing the given type, if the type is in this module; otherwise, null.</returns>
		/// <param name="className">The name of the type to locate. The name must be fully qualified with the namespace. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="className" /> is null. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The class initializers are invoked and an exception is thrown. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="className" /> is a zero-length string. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="className" /> requires a dependent assembly that could not be found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="className" /> requires a dependent assembly that was found but could not be loaded.-or-The current assembly was loaded into the reflection-only context, and <paramref name="className" /> requires a dependent assembly that was not preloaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="className" /> requires a dependent assembly, but the file is not a valid assembly. -or-<paramref name="className" /> requires a dependent assembly which was compiled for a version of the runtime later than the currently loaded version.</exception>
		// Token: 0x06002209 RID: 8713 RVA: 0x0007B158 File Offset: 0x00079358
		[ComVisible(true)]
		public virtual Type GetType(string className)
		{
			return this.GetType(className, false, false);
		}

		/// <summary>Returns the specified type, searching the module with the specified case sensitivity.</summary>
		/// <returns>A Type object representing the given type, if the type is in this module; otherwise, null.</returns>
		/// <param name="className">The name of the type to locate. The name must be fully qualified with the namespace. </param>
		/// <param name="ignoreCase">true for case-insensitive search; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="className" /> is null. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The class initializers are invoked and an exception is thrown. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="className" /> is a zero-length string. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="className" /> requires a dependent assembly that could not be found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="className" /> requires a dependent assembly that was found but could not be loaded.-or-The current assembly was loaded into the reflection-only context, and <paramref name="className" /> requires a dependent assembly that was not preloaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="className" /> requires a dependent assembly, but the file is not a valid assembly. -or-<paramref name="className" /> requires a dependent assembly which was compiled for a version of the runtime later than the currently loaded version.</exception>
		// Token: 0x0600220A RID: 8714 RVA: 0x0007B164 File Offset: 0x00079364
		[ComVisible(true)]
		public virtual Type GetType(string className, bool ignoreCase)
		{
			return this.GetType(className, false, ignoreCase);
		}

		/// <summary>Returns the specified type, specifying whether to make a case-sensitive search of the module and whether to throw an exception if the type cannot be found.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the specified type, if the type is declared in this module; otherwise, null.</returns>
		/// <param name="className">The name of the type to locate. The name must be fully qualified with the namespace. </param>
		/// <param name="throwOnError">true to throw an exception if the type cannot be found; false to return null. </param>
		/// <param name="ignoreCase">true for case-insensitive search; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="className" /> is null. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The class initializers are invoked and an exception is thrown. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="className" /> is a zero-length string. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="throwOnError" /> is true, and the type cannot be found. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="className" /> requires a dependent assembly that could not be found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="className" /> requires a dependent assembly that was found but could not be loaded.-or-The current assembly was loaded into the reflection-only context, and <paramref name="className" /> requires a dependent assembly that was not preloaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="className" /> requires a dependent assembly, but the file is not a valid assembly. -or-<paramref name="className" /> requires a dependent assembly which was compiled for a version of the runtime later than the currently loaded version.</exception>
		// Token: 0x0600220B RID: 8715 RVA: 0x0007B170 File Offset: 0x00079370
		[ComVisible(true)]
		public virtual Type GetType(string className, bool throwOnError, bool ignoreCase)
		{
			if (className == null)
			{
				throw new ArgumentNullException("className");
			}
			if (className == string.Empty)
			{
				throw new ArgumentException("Type name can't be empty");
			}
			return this.assembly.InternalGetType(this, className, throwOnError, ignoreCase);
		}

		// Token: 0x0600220C RID: 8716
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Type[] InternalGetTypes();

		/// <summary>Returns all the types defined within this module.</summary>
		/// <returns>An array of type Type containing types defined within the module that is reflected by this instance.</returns>
		/// <exception cref="T:System.Reflection.ReflectionTypeLoadException">One or more classes in a module could not be loaded. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x0600220D RID: 8717 RVA: 0x0007B1B0 File Offset: 0x000793B0
		public virtual Type[] GetTypes()
		{
			return this.InternalGetTypes();
		}

		/// <summary>Determines if the specified <paramref name="attributeType" /> is defined on this module.</summary>
		/// <returns>true if one or more instance of <paramref name="attributeType" /> is defined on this module; otherwise, false.</returns>
		/// <param name="attributeType">The Type object to which the custom attribute is applied. </param>
		/// <param name="inherit">This argument is ignored for objects of this type. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not a <see cref="T:System.Type" /> object supplied by the runtime. For example, <paramref name="attributeType" /> is a <see cref="T:System.Reflection.Emit.TypeBuilder" /> object.</exception>
		// Token: 0x0600220E RID: 8718 RVA: 0x0007B1B8 File Offset: 0x000793B8
		public virtual bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		/// <summary>Gets a value indicating whether the object is a resource.</summary>
		/// <returns>true if the object is a resource; otherwise, false.</returns>
		// Token: 0x0600220F RID: 8719 RVA: 0x0007B1C4 File Offset: 0x000793C4
		public bool IsResource()
		{
			return this.is_resource;
		}

		/// <summary>Returns the name of the module.</summary>
		/// <returns>A String representing the name of this module.</returns>
		// Token: 0x06002210 RID: 8720 RVA: 0x0007B1CC File Offset: 0x000793CC
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06002211 RID: 8721 RVA: 0x0007B1D4 File Offset: 0x000793D4
		internal Guid MvId
		{
			get
			{
				return this.GetModuleVersionId();
			}
		}

		/// <summary>Gets a universally unique identifier (UUID) that can be used to distinguish between two versions of a module.</summary>
		/// <returns>A <see cref="T:System.Guid" /> that can be used to distinguish between two versions of a module.</returns>
		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06002212 RID: 8722 RVA: 0x0007B1DC File Offset: 0x000793DC
		public Guid ModuleVersionId
		{
			get
			{
				return this.GetModuleVersionId();
			}
		}

		/// <summary>Gets a pair of values indicating the nature of the code in a module and the platform targeted by the module.</summary>
		/// <param name="peKind">When this method returns, a combination of the <see cref="T:System.Reflection.PortableExecutableKinds" /> values indicating the nature of the code in the module.</param>
		/// <param name="machine">When this method returns, one of the <see cref="T:System.Reflection.ImageFileMachine" /> values indicating the platform targeted by the module.</param>
		// Token: 0x06002213 RID: 8723 RVA: 0x0007B1E4 File Offset: 0x000793E4
		public void GetPEKind(out PortableExecutableKinds peKind, out ImageFileMachine machine)
		{
			this.ModuleHandle.GetPEKind(out peKind, out machine);
		}

		// Token: 0x06002214 RID: 8724 RVA: 0x0007B204 File Offset: 0x00079404
		private Exception resolve_token_exception(int metadataToken, ResolveTokenError error, string tokenType)
		{
			if (error == ResolveTokenError.OutOfRange)
			{
				return new ArgumentOutOfRangeException("metadataToken", string.Format("Token 0x{0:x} is not valid in the scope of module {1}", metadataToken, this.name));
			}
			return new ArgumentException(string.Format("Token 0x{0:x} is not a valid {1} token in the scope of module {2}", metadataToken, tokenType, this.name), "metadataToken");
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x0007B25C File Offset: 0x0007945C
		private IntPtr[] ptrs_from_types(Type[] types)
		{
			if (types == null)
			{
				return null;
			}
			IntPtr[] array = new IntPtr[types.Length];
			for (int i = 0; i < types.Length; i++)
			{
				if (types[i] == null)
				{
					throw new ArgumentException();
				}
				array[i] = types[i].TypeHandle.Value;
			}
			return array;
		}

		/// <summary>Returns the field identified by the specified metadata token.</summary>
		/// <returns>A <see cref="T:System.Reflection.FieldInfo" /> object representing the field that is identified by the specified metadata token.</returns>
		/// <param name="metadataToken">A metadata token that identifies a field in the module.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="metadataToken" /> is not a token for a field in the scope of the current module.-or-<paramref name="metadataToken" /> identifies a field whose parent TypeSpec has a signature containing element type var (a type parameter of a generic type) or mvar (a type parameter of a generic method).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="metadataToken" /> is not a valid token in the scope of the current module.</exception>
		// Token: 0x06002216 RID: 8726 RVA: 0x0007B2BC File Offset: 0x000794BC
		public FieldInfo ResolveField(int metadataToken)
		{
			return this.ResolveField(metadataToken, null, null);
		}

		/// <summary>Returns the field identified by the specified metadata token, in the context defined by the specified generic type parameters.</summary>
		/// <returns>A <see cref="T:System.Reflection.FieldInfo" /> object representing the field that is identified by the specified metadata token.</returns>
		/// <param name="metadataToken">A metadata token that identifies a field in the module.</param>
		/// <param name="genericTypeArguments">An array of <see cref="T:System.Type" /> objects representing the generic type arguments of the type where the token is in scope, or null if that type is not generic. </param>
		/// <param name="genericMethodArguments">An array of <see cref="T:System.Type" /> objects representing the generic type arguments of the method where the token is in scope, or null if that method is not generic.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="metadataToken" /> is not a token for a field in the scope of the current module.-or-<paramref name="metadataToken" /> identifies a field whose parent TypeSpec has a signature containing element type var (a type parameter of a generic type) or mvar (a type parameter of a generic method), and the necessary generic type arguments were not supplied for either or both of <paramref name="genericTypeArguments" /> and <paramref name="genericMethodArguments" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="metadataToken" /> is not a valid token in the scope of the current module.</exception>
		// Token: 0x06002217 RID: 8727 RVA: 0x0007B2C8 File Offset: 0x000794C8
		public FieldInfo ResolveField(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			ResolveTokenError error;
			IntPtr intPtr = Module.ResolveFieldToken(this._impl, metadataToken, this.ptrs_from_types(genericTypeArguments), this.ptrs_from_types(genericMethodArguments), out error);
			if (intPtr == IntPtr.Zero)
			{
				throw this.resolve_token_exception(metadataToken, error, "Field");
			}
			return FieldInfo.GetFieldFromHandle(new RuntimeFieldHandle(intPtr));
		}

		/// <summary>Returns the type or member identified by the specified metadata token.</summary>
		/// <returns>A <see cref="T:System.Reflection.MemberInfo" /> object representing the type or member that is identified by the specified metadata token.</returns>
		/// <param name="metadataToken">A metadata token that identifies a type or member in the module.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="metadataToken" /> is not a token for a type or member in the scope of the current module.-or-<paramref name="metadataToken" /> is a MethodSpec or TypeSpec whose signature contains element type var (a type parameter of a generic type) or mvar (a type parameter of a generic method).-or-<paramref name="metadataToken" /> identifies a property or event.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="metadataToken" /> is not a valid token in the scope of the current module.</exception>
		// Token: 0x06002218 RID: 8728 RVA: 0x0007B31C File Offset: 0x0007951C
		public MemberInfo ResolveMember(int metadataToken)
		{
			return this.ResolveMember(metadataToken, null, null);
		}

		/// <summary>Returns the type or member identified by the specified metadata token, in the context defined by the specified generic type parameters.</summary>
		/// <returns>A <see cref="T:System.Reflection.MemberInfo" /> object representing the type or member that is identified by the specified metadata token.</returns>
		/// <param name="metadataToken">A metadata token that identifies a type or member in the module.</param>
		/// <param name="genericTypeArguments">An array of <see cref="T:System.Type" /> objects representing the generic type arguments of the type where the token is in scope, or null if that type is not generic. </param>
		/// <param name="genericMethodArguments">An array of <see cref="T:System.Type" /> objects representing the generic type arguments of the method where the token is in scope, or null if that method is not generic.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="metadataToken" /> is not a token for a type or member in the scope of the current module.-or-<paramref name="metadataToken" /> is a MethodSpec or TypeSpec whose signature contains element type var (a type parameter of a generic type) or mvar (a type parameter of a generic method), and the necessary generic type arguments were not supplied for either or both of <paramref name="genericTypeArguments" /> and <paramref name="genericMethodArguments" />.-or-<paramref name="metadataToken" /> identifies a property or event.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="metadataToken" /> is not a valid token in the scope of the current module.</exception>
		// Token: 0x06002219 RID: 8729 RVA: 0x0007B328 File Offset: 0x00079528
		public MemberInfo ResolveMember(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			ResolveTokenError error;
			MemberInfo memberInfo = Module.ResolveMemberToken(this._impl, metadataToken, this.ptrs_from_types(genericTypeArguments), this.ptrs_from_types(genericMethodArguments), out error);
			if (memberInfo == null)
			{
				throw this.resolve_token_exception(metadataToken, error, "MemberInfo");
			}
			return memberInfo;
		}

		/// <summary>Returns the method or constructor identified by the specified metadata token.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodBase" /> object representing the method or constructor that is identified by the specified metadata token.</returns>
		/// <param name="metadataToken">A metadata token that identifies a method or constructor in the module.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="metadataToken" /> is not a token for a method or constructor in the scope of the current module.-or-<paramref name="metadataToken" /> is a MethodSpec whose signature contains element type var (a type parameter of a generic type) or mvar (a type parameter of a generic method).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="metadataToken" /> is not a valid token in the scope of the current module.</exception>
		// Token: 0x0600221A RID: 8730 RVA: 0x0007B368 File Offset: 0x00079568
		public MethodBase ResolveMethod(int metadataToken)
		{
			return this.ResolveMethod(metadataToken, null, null);
		}

		/// <summary>Returns the method or constructor identified by the specified metadata token, in the context defined by the specified generic type parameters. </summary>
		/// <returns>A <see cref="T:System.Reflection.MethodBase" /> object representing the method that is identified by the specified metadata token.</returns>
		/// <param name="metadataToken">A metadata token that identifies a method or constructor in the module.</param>
		/// <param name="genericTypeArguments">An array of <see cref="T:System.Type" /> objects representing the generic type arguments of the type where the token is in scope, or null if that type is not generic. </param>
		/// <param name="genericMethodArguments">An array of <see cref="T:System.Type" /> objects representing the generic type arguments of the method where the token is in scope, or null if that method is not generic.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="metadataToken" /> is not a token for a method or constructor in the scope of the current module.-or-<paramref name="metadataToken" /> is a MethodSpec whose signature contains element type var (a type parameter of a generic type) or mvar (a type parameter of a generic method), and the necessary generic type arguments were not supplied for either or both of <paramref name="genericTypeArguments" /> and <paramref name="genericMethodArguments" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="metadataToken" /> is not a valid token in the scope of the current module.</exception>
		// Token: 0x0600221B RID: 8731 RVA: 0x0007B374 File Offset: 0x00079574
		public MethodBase ResolveMethod(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			ResolveTokenError error;
			IntPtr intPtr = Module.ResolveMethodToken(this._impl, metadataToken, this.ptrs_from_types(genericTypeArguments), this.ptrs_from_types(genericMethodArguments), out error);
			if (intPtr == IntPtr.Zero)
			{
				throw this.resolve_token_exception(metadataToken, error, "MethodBase");
			}
			return MethodBase.GetMethodFromHandleNoGenericCheck(new RuntimeMethodHandle(intPtr));
		}

		/// <summary>Returns the string identified by the specified metadata token.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a string value from the metadata string heap.</returns>
		/// <param name="metadataToken">A metadata token that identifies a string in the string heap of the module. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="metadataToken" /> is not a token for a string in the scope of the current module. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="metadataToken" /> is not a valid token in the scope of the current module.</exception>
		// Token: 0x0600221C RID: 8732 RVA: 0x0007B3C8 File Offset: 0x000795C8
		public string ResolveString(int metadataToken)
		{
			ResolveTokenError error;
			string text = Module.ResolveStringToken(this._impl, metadataToken, out error);
			if (text == null)
			{
				throw this.resolve_token_exception(metadataToken, error, "string");
			}
			return text;
		}

		/// <summary>Returns the type identified by the specified metadata token.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the type that is identified by the specified metadata token.</returns>
		/// <param name="metadataToken">A metadata token that identifies a type in the module.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="metadataToken" /> is not a token for a type in the scope of the current module.-or-<paramref name="metadataToken" /> is a TypeSpec whose signature contains element type var (a type parameter of a generic type) or mvar (a type parameter of a generic method). </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="metadataToken" /> is not a valid token in the scope of the current module.</exception>
		// Token: 0x0600221D RID: 8733 RVA: 0x0007B3FC File Offset: 0x000795FC
		public Type ResolveType(int metadataToken)
		{
			return this.ResolveType(metadataToken, null, null);
		}

		/// <summary>Returns the type identified by the specified metadata token, in the context defined by the specified generic type parameters.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the type that is identified by the specified metadata token.</returns>
		/// <param name="metadataToken">A metadata token that identifies a type in the module.</param>
		/// <param name="genericTypeArguments">An array of <see cref="T:System.Type" /> objects representing the generic type arguments of the type where the token is in scope, or null if that type is not generic. </param>
		/// <param name="genericMethodArguments">An array of <see cref="T:System.Type" /> objects representing the generic type arguments of the method where the token is in scope, or null if that method is not generic.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="metadataToken" /> is not a token for a type in the scope of the current module.-or-<paramref name="metadataToken" /> is a TypeSpec whose signature contains element type var (a type parameter of a generic type) or mvar (a type parameter of a generic method), and the necessary generic type arguments were not supplied for either or both of <paramref name="genericTypeArguments" /> and <paramref name="genericMethodArguments" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="metadataToken" /> is not a valid token in the scope of the current module.</exception>
		// Token: 0x0600221E RID: 8734 RVA: 0x0007B408 File Offset: 0x00079608
		public Type ResolveType(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			ResolveTokenError error;
			IntPtr intPtr = Module.ResolveTypeToken(this._impl, metadataToken, this.ptrs_from_types(genericTypeArguments), this.ptrs_from_types(genericMethodArguments), out error);
			if (intPtr == IntPtr.Zero)
			{
				throw this.resolve_token_exception(metadataToken, error, "Type");
			}
			return Type.GetTypeFromHandle(new RuntimeTypeHandle(intPtr));
		}

		/// <summary>Returns the signature blob identified by a metadata token.</summary>
		/// <returns>An array of bytes representing the signature blob.</returns>
		/// <param name="metadataToken">A metadata token that identifies a signature in the module.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="metadataToken" /> is not a valid MemberRef, MethodDef, TypeSpec, signature, or FieldDef token in the scope of the current module.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="metadataToken" /> is not a valid token in the scope of the current module.</exception>
		// Token: 0x0600221F RID: 8735 RVA: 0x0007B45C File Offset: 0x0007965C
		public byte[] ResolveSignature(int metadataToken)
		{
			ResolveTokenError error;
			byte[] array = Module.ResolveSignature(this._impl, metadataToken, out error);
			if (array == null)
			{
				throw this.resolve_token_exception(metadataToken, error, "signature");
			}
			return array;
		}

		// Token: 0x06002220 RID: 8736 RVA: 0x0007B490 File Offset: 0x00079690
		internal static Type MonoDebugger_ResolveType(Module module, int token)
		{
			ResolveTokenError resolveTokenError;
			IntPtr intPtr = Module.ResolveTypeToken(module._impl, token, null, null, out resolveTokenError);
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			return Type.GetTypeFromHandle(new RuntimeTypeHandle(intPtr));
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x0007B4CC File Offset: 0x000796CC
		internal static Guid Mono_GetGuid(Module module)
		{
			return module.GetModuleVersionId();
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x0007B4D4 File Offset: 0x000796D4
		internal virtual Guid GetModuleVersionId()
		{
			return new Guid(this.GetGuidInternal());
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x0007B4E4 File Offset: 0x000796E4
		private static bool filter_by_type_name(Type m, object filterCriteria)
		{
			string text = (string)filterCriteria;
			if (text.EndsWith("*"))
			{
				return m.Name.StartsWith(text.Substring(0, text.Length - 1));
			}
			return m.Name == text;
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x0007B530 File Offset: 0x00079730
		private static bool filter_by_type_name_ignore_case(Type m, object filterCriteria)
		{
			string text = (string)filterCriteria;
			if (text.EndsWith("*"))
			{
				return m.Name.ToLower().StartsWith(text.Substring(0, text.Length - 1).ToLower());
			}
			return string.Compare(m.Name, text, true) == 0;
		}

		// Token: 0x06002225 RID: 8741
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IntPtr GetHINSTANCE();

		// Token: 0x06002226 RID: 8742
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string GetGuidInternal();

		// Token: 0x06002227 RID: 8743
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Type GetGlobalType();

		// Token: 0x06002228 RID: 8744
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr ResolveTypeToken(IntPtr module, int token, IntPtr[] type_args, IntPtr[] method_args, out ResolveTokenError error);

		// Token: 0x06002229 RID: 8745
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr ResolveMethodToken(IntPtr module, int token, IntPtr[] type_args, IntPtr[] method_args, out ResolveTokenError error);

		// Token: 0x0600222A RID: 8746
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr ResolveFieldToken(IntPtr module, int token, IntPtr[] type_args, IntPtr[] method_args, out ResolveTokenError error);

		// Token: 0x0600222B RID: 8747
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string ResolveStringToken(IntPtr module, int token, out ResolveTokenError error);

		// Token: 0x0600222C RID: 8748
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MemberInfo ResolveMemberToken(IntPtr module, int token, IntPtr[] type_args, IntPtr[] method_args, out ResolveTokenError error);

		// Token: 0x0600222D RID: 8749
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern byte[] ResolveSignature(IntPtr module, int metadataToken, out ResolveTokenError error);

		// Token: 0x0600222E RID: 8750
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void GetPEKind(IntPtr module, out PortableExecutableKinds peKind, out ImageFileMachine machine);

		// Token: 0x04000CC7 RID: 3271
		private const BindingFlags defaultBindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;

		/// <summary>A TypeFilter object that filters the list of types defined in this module based upon the name. This field is case-sensitive and read-only.</summary>
		// Token: 0x04000CC8 RID: 3272
		public static readonly TypeFilter FilterTypeName = new TypeFilter(Module.filter_by_type_name);

		/// <summary>A TypeFilter object that filters the list of types defined in this module based upon the name. This field is case-insensitive and read-only.</summary>
		// Token: 0x04000CC9 RID: 3273
		public static readonly TypeFilter FilterTypeNameIgnoreCase = new TypeFilter(Module.filter_by_type_name_ignore_case);

		// Token: 0x04000CCA RID: 3274
		private IntPtr _impl;

		// Token: 0x04000CCB RID: 3275
		internal Assembly assembly;

		// Token: 0x04000CCC RID: 3276
		internal string fqname;

		// Token: 0x04000CCD RID: 3277
		internal string name;

		// Token: 0x04000CCE RID: 3278
		internal string scopename;

		// Token: 0x04000CCF RID: 3279
		internal bool is_resource;

		// Token: 0x04000CD0 RID: 3280
		internal int token;
	}
}
