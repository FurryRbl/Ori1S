using System;
using System.Collections;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Defines and represents a module in a dynamic assembly.</summary>
	// Token: 0x020002EB RID: 747
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_ModuleBuilder))]
	[ClassInterface(ClassInterfaceType.None)]
	public class ModuleBuilder : Module, _ModuleBuilder
	{
		// Token: 0x06002642 RID: 9794 RVA: 0x00087250 File Offset: 0x00085450
		internal ModuleBuilder(AssemblyBuilder assb, string name, string fullyqname, bool emitSymbolInfo, bool transient)
		{
			this.scopename = name;
			this.name = name;
			this.fqname = fullyqname;
			this.assemblyb = assb;
			this.assembly = assb;
			this.transient = transient;
			this.guid = Guid.FastNewGuidArray();
			this.table_idx = this.get_next_table_index(this, 0, true);
			this.name_cache = new Hashtable();
			ModuleBuilder.basic_init(this);
			this.CreateGlobalType();
			if (assb.IsRun)
			{
				TypeBuilder typeBuilder = new TypeBuilder(this, TypeAttributes.Abstract, 16777215);
				Type ab = typeBuilder.CreateType();
				ModuleBuilder.set_wrappers_type(this, ab);
			}
			if (emitSymbolInfo)
			{
				Assembly assembly = Assembly.LoadWithPartialName("Mono.CompilerServices.SymbolWriter");
				if (assembly == null)
				{
					throw new ExecutionEngineException("The assembly for default symbol writer cannot be loaded");
				}
				Type type = assembly.GetType("Mono.CompilerServices.SymbolWriter.SymbolWriterImpl");
				if (type == null)
				{
					throw new ExecutionEngineException("The type that implements the default symbol writer interface cannot be found");
				}
				this.symbolWriter = (ISymbolWriter)Activator.CreateInstance(type, new object[]
				{
					this
				});
				string text = this.fqname;
				if (this.assemblyb.AssemblyDir != null)
				{
					text = Path.Combine(this.assemblyb.AssemblyDir, text);
				}
				this.symbolWriter.Initialize(IntPtr.Zero, text, true);
			}
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06002644 RID: 9796 RVA: 0x000873B4 File Offset: 0x000855B4
		void _ModuleBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06002645 RID: 9797 RVA: 0x000873BC File Offset: 0x000855BC
		void _ModuleBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06002646 RID: 9798 RVA: 0x000873C4 File Offset: 0x000855C4
		void _ModuleBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides access to properties and methods exposed by an object.</summary>
		/// <param name="dispIdMember">Identifies the member.</param>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="lcid">The locale context in which to interpret arguments.</param>
		/// <param name="wFlags">Flags describing the context of the call.</param>
		/// <param name="pDispParams">Pointer to a structure containing an array of arguments, an array of argument DISPIDs for named arguments, and counts for the number of elements in the arrays.</param>
		/// <param name="pVarResult">Pointer to the location where the result is to be stored.</param>
		/// <param name="pExcepInfo">Pointer to a structure that contains exception information.</param>
		/// <param name="puArgErr">The index of the first argument that has an error.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06002647 RID: 9799 RVA: 0x000873CC File Offset: 0x000855CC
		void _ModuleBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002648 RID: 9800
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void basic_init(ModuleBuilder ab);

		// Token: 0x06002649 RID: 9801
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_wrappers_type(ModuleBuilder mb, Type ab);

		/// <summary>Gets a String representing the fully-qualified name and path to this module.</summary>
		/// <returns>The fully-qualified module name.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x0600264A RID: 9802 RVA: 0x000873D4 File Offset: 0x000855D4
		public override string FullyQualifiedName
		{
			get
			{
				return this.fqname;
			}
		}

		/// <summary>Checks if this dynamic module is transient.</summary>
		/// <returns>Returns true if this dynamic module is transient; otherwise, false.</returns>
		// Token: 0x0600264B RID: 9803 RVA: 0x000873DC File Offset: 0x000855DC
		public bool IsTransient()
		{
			return this.transient;
		}

		/// <summary>Completes the global function definitions and global data definitions for this dynamic module.</summary>
		/// <exception cref="T:System.InvalidOperationException">This method was called previously. </exception>
		// Token: 0x0600264C RID: 9804 RVA: 0x000873E4 File Offset: 0x000855E4
		public void CreateGlobalFunctions()
		{
			if (this.global_type_created != null)
			{
				throw new InvalidOperationException("global methods already created");
			}
			if (this.global_type != null)
			{
				this.global_type_created = this.global_type.CreateType();
			}
		}

		/// <summary>Defines an initialized data field in the .sdata section of the portable executable (PE) file.</summary>
		/// <returns>A field to reference the data.</returns>
		/// <param name="name">The name used to refer to the data. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="data">The blob of data. </param>
		/// <param name="attributes">The attributes for the field. The default is Static. </param>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero.-or- The size of <paramref name="data" /> is less than or equal to zero or greater than or equal to 0x3f0000. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="data" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.ModuleBuilder.CreateGlobalFunctions" /> has been previously called. </exception>
		// Token: 0x0600264D RID: 9805 RVA: 0x00087424 File Offset: 0x00085624
		public FieldBuilder DefineInitializedData(string name, byte[] data, FieldAttributes attributes)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			FieldBuilder fieldBuilder = this.DefineUninitializedData(name, data.Length, attributes | FieldAttributes.HasFieldRVA);
			fieldBuilder.SetRVAData(data);
			return fieldBuilder;
		}

		/// <summary>Defines an uninitialized data field in the .sdata section of the portable executable (PE) file.</summary>
		/// <returns>A field to reference the data.</returns>
		/// <param name="name">The name used to refer to the data. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="size">The size of the data field. </param>
		/// <param name="attributes">The attributes for the field. </param>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero.-or- <paramref name="size" /> is less than or equal to zero or greater than or equal to 0x003f0000. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.ModuleBuilder.CreateGlobalFunctions" /> has been previously called. </exception>
		// Token: 0x0600264E RID: 9806 RVA: 0x0008745C File Offset: 0x0008565C
		public FieldBuilder DefineUninitializedData(string name, int size, FieldAttributes attributes)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this.global_type_created != null)
			{
				throw new InvalidOperationException("global fields already created");
			}
			if (size <= 0 || size > 4128768)
			{
				throw new ArgumentException("size", "Data size must be > 0 and < 0x3f0000");
			}
			this.CreateGlobalType();
			string text = "$ArrayType$" + size;
			Type type = this.GetType(text, false, false);
			if (type == null)
			{
				TypeBuilder typeBuilder = this.DefineType(text, TypeAttributes.Public | TypeAttributes.ExplicitLayout | TypeAttributes.Sealed, this.assemblyb.corlib_value_type, null, PackingSize.Size1, size);
				typeBuilder.CreateType();
				type = typeBuilder;
			}
			FieldBuilder fieldBuilder = this.global_type.DefineField(name, type, attributes | FieldAttributes.Static);
			if (this.global_fields != null)
			{
				FieldBuilder[] array = new FieldBuilder[this.global_fields.Length + 1];
				Array.Copy(this.global_fields, array, this.global_fields.Length);
				array[this.global_fields.Length] = fieldBuilder;
				this.global_fields = array;
			}
			else
			{
				this.global_fields = new FieldBuilder[1];
				this.global_fields[0] = fieldBuilder;
			}
			return fieldBuilder;
		}

		// Token: 0x0600264F RID: 9807 RVA: 0x00087570 File Offset: 0x00085770
		private void addGlobalMethod(MethodBuilder mb)
		{
			if (this.global_methods != null)
			{
				MethodBuilder[] array = new MethodBuilder[this.global_methods.Length + 1];
				Array.Copy(this.global_methods, array, this.global_methods.Length);
				array[this.global_methods.Length] = mb;
				this.global_methods = array;
			}
			else
			{
				this.global_methods = new MethodBuilder[1];
				this.global_methods[0] = mb;
			}
		}

		/// <summary>Defines a global method given its name, attributes, return type, and parameter types.</summary>
		/// <returns>Returns the defined global method.</returns>
		/// <param name="name">The name of the method. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="attributes">The attributes of the method. Must include <see cref="F:System.Reflection.MethodAttributes.Static" />. </param>
		/// <param name="returnType">The return type of the method. </param>
		/// <param name="parameterTypes">The types of the method's parameters. </param>
		/// <exception cref="T:System.ArgumentException">The method is not static. That is, <paramref name="attributes" /> does not include <see cref="F:System.Reflection.MethodAttributes.Static" />.-or- The length of <paramref name="name" /> is zero -or-An element in the <see cref="T:System.Type" /> array is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.ModuleBuilder.CreateGlobalFunctions" /> has been previously called. </exception>
		// Token: 0x06002650 RID: 9808 RVA: 0x000875D8 File Offset: 0x000857D8
		public MethodBuilder DefineGlobalMethod(string name, MethodAttributes attributes, Type returnType, Type[] parameterTypes)
		{
			return this.DefineGlobalMethod(name, attributes, CallingConventions.Standard, returnType, parameterTypes);
		}

		/// <summary>Defines a global method given its name, attributes, calling convention, return type, and parameter types.</summary>
		/// <returns>Returns the defined global method.</returns>
		/// <param name="name">The name of the method. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="attributes">The attributes of the method. Must include <see cref="F:System.Reflection.MethodAttributes.Static" />.</param>
		/// <param name="callingConvention">The calling convention for the method. </param>
		/// <param name="returnType">The return type of the method. </param>
		/// <param name="parameterTypes">The types of the method's parameters. </param>
		/// <exception cref="T:System.ArgumentException">The method is not static. That is, <paramref name="attributes" /> does not include <see cref="F:System.Reflection.MethodAttributes.Static" />.-or-An element in the <see cref="T:System.Type" /> array is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.ModuleBuilder.CreateGlobalFunctions" /> has been previously called. </exception>
		// Token: 0x06002651 RID: 9809 RVA: 0x000875E8 File Offset: 0x000857E8
		public MethodBuilder DefineGlobalMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
		{
			return this.DefineGlobalMethod(name, attributes, callingConvention, returnType, null, null, parameterTypes, null, null);
		}

		/// <summary>Defines a global method given its name, attributes, calling convention, return type, custom modifiers for the return type, parameter types, and custom modifiers for the parameter types.</summary>
		/// <returns>Returns the defined global method.</returns>
		/// <param name="name">The name of the method. <paramref name="name" /> cannot contain embedded null characters. </param>
		/// <param name="attributes">The attributes of the method. Must include <see cref="F:System.Reflection.MethodAttributes.Static" />.</param>
		/// <param name="callingConvention">The calling convention for the method. </param>
		/// <param name="returnType">The return type of the method. </param>
		/// <param name="requiredReturnTypeCustomModifiers">An array of types representing the required custom modifiers for the return type, such as <see cref="T:System.Runtime.CompilerServices.IsConst" /> or <see cref="T:System.Runtime.CompilerServices.IsBoxed" />. If the return type has no required custom modifiers, specify null. </param>
		/// <param name="optionalReturnTypeCustomModifiers">An array of types representing the optional custom modifiers for the return type, such as <see cref="T:System.Runtime.CompilerServices.IsConst" /> or <see cref="T:System.Runtime.CompilerServices.IsBoxed" />. If the return type has no optional custom modifiers, specify null. </param>
		/// <param name="parameterTypes">The types of the method's parameters. </param>
		/// <param name="requiredParameterTypeCustomModifiers">An array of arrays of types. Each array of types represents the required custom modifiers for the corresponding parameter of the global method. If a particular argument has no required custom modifiers, specify null instead of an array of types. If the global method has no arguments, or if none of the arguments have required custom modifiers, specify null instead of an array of arrays.</param>
		/// <param name="optionalParameterTypeCustomModifiers">An array of arrays of types. Each array of types represents the optional custom modifiers for the corresponding parameter. If a particular argument has no optional custom modifiers, specify null instead of an array of types. If the global method has no arguments, or if none of the arguments have optional custom modifiers, specify null instead of an array of arrays.</param>
		/// <exception cref="T:System.ArgumentException">The method is not static. That is, <paramref name="attributes" /> does not include <see cref="F:System.Reflection.MethodAttributes.Static" />.-or-An element in the <see cref="T:System.Type" /> array is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Reflection.Emit.ModuleBuilder.CreateGlobalFunctions" /> method has been previously called. </exception>
		// Token: 0x06002652 RID: 9810 RVA: 0x00087608 File Offset: 0x00085808
		public MethodBuilder DefineGlobalMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] requiredReturnTypeCustomModifiers, Type[] optionalReturnTypeCustomModifiers, Type[] parameterTypes, Type[][] requiredParameterTypeCustomModifiers, Type[][] optionalParameterTypeCustomModifiers)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if ((attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope)
			{
				throw new ArgumentException("global methods must be static");
			}
			if (this.global_type_created != null)
			{
				throw new InvalidOperationException("global methods already created");
			}
			this.CreateGlobalType();
			MethodBuilder methodBuilder = this.global_type.DefineMethod(name, attributes, callingConvention, returnType, requiredReturnTypeCustomModifiers, optionalReturnTypeCustomModifiers, parameterTypes, requiredParameterTypeCustomModifiers, optionalParameterTypeCustomModifiers);
			this.addGlobalMethod(methodBuilder);
			return methodBuilder;
		}

		/// <summary>Defines a PInvoke method given its name, the name of the DLL in which the method is defined, the attributes of the method, the calling convention of the method, the return type of the method, the types of the parameters of the method, and the PInvoke flags.</summary>
		/// <returns>The defined PInvoke method.</returns>
		/// <param name="name">The name of the PInvoke method. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="dllName">The name of the DLL in which the PInvoke method is defined. </param>
		/// <param name="attributes">The attributes of the method. </param>
		/// <param name="callingConvention">The method's calling convention. </param>
		/// <param name="returnType">The method's return type. </param>
		/// <param name="parameterTypes">The types of the method's parameters. </param>
		/// <param name="nativeCallConv">The native calling convention. </param>
		/// <param name="nativeCharSet">The method's native character set. </param>
		/// <exception cref="T:System.ArgumentException">The method is not static or if the containing type is an interface.-or- The method is abstract.-or- The method was previously defined. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="dllName" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /></exception>
		// Token: 0x06002653 RID: 9811 RVA: 0x0008767C File Offset: 0x0008587C
		public MethodBuilder DefinePInvokeMethod(string name, string dllName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, CallingConvention nativeCallConv, CharSet nativeCharSet)
		{
			return this.DefinePInvokeMethod(name, dllName, name, attributes, callingConvention, returnType, parameterTypes, nativeCallConv, nativeCharSet);
		}

		/// <summary>Defines a PInvoke method given its name, the name of the DLL in which the method is defined, the attributes of the method, the calling convention of the method, the return type of the method, the types of the parameters of the method, and the PInvoke flags.</summary>
		/// <returns>The defined PInvoke method.</returns>
		/// <param name="name">The name of the PInvoke method. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="dllName">The name of the DLL in which the PInvoke method is defined. </param>
		/// <param name="entryName">The name of the entry point in the DLL. </param>
		/// <param name="attributes">The attributes of the method. </param>
		/// <param name="callingConvention">The method's calling convention. </param>
		/// <param name="returnType">The method's return type. </param>
		/// <param name="parameterTypes">The types of the method's parameters. </param>
		/// <param name="nativeCallConv">The native calling convention. </param>
		/// <param name="nativeCharSet">The method's native character set. </param>
		/// <exception cref="T:System.ArgumentException">The method is not static or if the containing type is an interface or if the method is abstract of if the method was previously defined. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="dllName" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /></exception>
		// Token: 0x06002654 RID: 9812 RVA: 0x000876A0 File Offset: 0x000858A0
		public MethodBuilder DefinePInvokeMethod(string name, string dllName, string entryName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, CallingConvention nativeCallConv, CharSet nativeCharSet)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if ((attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope)
			{
				throw new ArgumentException("global methods must be static");
			}
			if (this.global_type_created != null)
			{
				throw new InvalidOperationException("global methods already created");
			}
			this.CreateGlobalType();
			MethodBuilder methodBuilder = this.global_type.DefinePInvokeMethod(name, dllName, entryName, attributes, callingConvention, returnType, parameterTypes, nativeCallConv, nativeCharSet);
			this.addGlobalMethod(methodBuilder);
			return methodBuilder;
		}

		/// <summary>Constructs a TypeBuilder for a type with the specified name.</summary>
		/// <returns>Returns the created TypeBuilder.</returns>
		/// <param name="name">The full path of the type. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <exception cref="T:System.ArgumentException">A type with the given name exists in the parent assembly of this module.-or- Nested type attributes are set on a type that is not nested. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		// Token: 0x06002655 RID: 9813 RVA: 0x00087714 File Offset: 0x00085914
		public TypeBuilder DefineType(string name)
		{
			return this.DefineType(name, TypeAttributes.NotPublic);
		}

		/// <summary>Constructs a TypeBuilder given the type name and the type attributes.</summary>
		/// <returns>Returns a TypeBuilder created with all of the requested attributes.</returns>
		/// <param name="name">The full path of the type. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="attr">The attributes of the defined type. </param>
		/// <exception cref="T:System.ArgumentException">A type with the given name exists in the parent assembly of this module.-or- Nested type attributes are set on a type that is not nested. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		// Token: 0x06002656 RID: 9814 RVA: 0x00087720 File Offset: 0x00085920
		public TypeBuilder DefineType(string name, TypeAttributes attr)
		{
			if ((attr & TypeAttributes.ClassSemanticsMask) != TypeAttributes.NotPublic)
			{
				return this.DefineType(name, attr, null, null);
			}
			return this.DefineType(name, attr, typeof(object), null);
		}

		/// <summary>Constructs a TypeBuilder given type name, its attributes, and the type that the defined type extends.</summary>
		/// <returns>Returns a TypeBuilder created with all of the requested attributes.</returns>
		/// <param name="name">The full path of the type. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="attr">The attribute to be associated with the type. </param>
		/// <param name="parent">The Type that the defined type extends. </param>
		/// <exception cref="T:System.ArgumentException">A type with the given name exists in the parent assembly of this module.-or- Nested type attributes are set on a type that is not nested. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		// Token: 0x06002657 RID: 9815 RVA: 0x00087754 File Offset: 0x00085954
		public TypeBuilder DefineType(string name, TypeAttributes attr, Type parent)
		{
			return this.DefineType(name, attr, parent, null);
		}

		// Token: 0x06002658 RID: 9816 RVA: 0x00087760 File Offset: 0x00085960
		private void AddType(TypeBuilder tb)
		{
			if (this.types != null)
			{
				if (this.types.Length == this.num_types)
				{
					TypeBuilder[] destinationArray = new TypeBuilder[this.types.Length * 2];
					Array.Copy(this.types, destinationArray, this.num_types);
					this.types = destinationArray;
				}
			}
			else
			{
				this.types = new TypeBuilder[1];
			}
			this.types[this.num_types] = tb;
			this.num_types++;
		}

		// Token: 0x06002659 RID: 9817 RVA: 0x000877E4 File Offset: 0x000859E4
		private TypeBuilder DefineType(string name, TypeAttributes attr, Type parent, Type[] interfaces, PackingSize packingSize, int typesize)
		{
			if (this.name_cache.ContainsKey(name))
			{
				throw new ArgumentException("Duplicate type name within an assembly.");
			}
			TypeBuilder typeBuilder = new TypeBuilder(this, name, attr, parent, interfaces, packingSize, typesize, null);
			this.AddType(typeBuilder);
			this.name_cache.Add(name, typeBuilder);
			return typeBuilder;
		}

		// Token: 0x0600265A RID: 9818 RVA: 0x00087834 File Offset: 0x00085A34
		internal void RegisterTypeName(TypeBuilder tb, string name)
		{
			this.name_cache.Add(name, tb);
		}

		// Token: 0x0600265B RID: 9819 RVA: 0x00087844 File Offset: 0x00085A44
		internal TypeBuilder GetRegisteredType(string name)
		{
			return (TypeBuilder)this.name_cache[name];
		}

		/// <summary>Constructs a TypeBuilder given the type name, attributes, the type that the defined type extends, and the interfaces that the defined type implements.</summary>
		/// <returns>Returns a TypeBuilder created with all of the requested attributes.</returns>
		/// <param name="name">The full path of the type. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="attr">The attributes to be associated with the type. </param>
		/// <param name="parent">The type that the defined type extends. </param>
		/// <param name="interfaces">The list of interfaces that the type implements. </param>
		/// <exception cref="T:System.ArgumentException">A type with the given name exists in the parent assembly of this module.-or- Nested type attributes are set on a type that is not nested. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		// Token: 0x0600265C RID: 9820 RVA: 0x00087858 File Offset: 0x00085A58
		[ComVisible(true)]
		public TypeBuilder DefineType(string name, TypeAttributes attr, Type parent, Type[] interfaces)
		{
			return this.DefineType(name, attr, parent, interfaces, PackingSize.Unspecified, 0);
		}

		/// <summary>Constructs a TypeBuilder given the type name, the attributes, the type that the defined type extends, and the total size of the type.</summary>
		/// <returns>Returns a TypeBuilder object.</returns>
		/// <param name="name">The full path of the type. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="attr">The attributes of the defined type. </param>
		/// <param name="parent">The Type that the defined type extends. </param>
		/// <param name="typesize">The total size of the type. </param>
		/// <exception cref="T:System.ArgumentException">A type with the given name exists in the parent assembly of this module.-or- Nested type attributes are set on a type that is not nested. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		// Token: 0x0600265D RID: 9821 RVA: 0x00087868 File Offset: 0x00085A68
		public TypeBuilder DefineType(string name, TypeAttributes attr, Type parent, int typesize)
		{
			return this.DefineType(name, attr, parent, null, PackingSize.Unspecified, 0);
		}

		/// <summary>Constructs a TypeBuilder given the type name, the attributes, the type that the defined type extends, and the packing size of the type.</summary>
		/// <returns>Returns a TypeBuilder object.</returns>
		/// <param name="name">The full path of the type. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="attr">The attributes of the defined type. </param>
		/// <param name="parent">The Type that the defined type extends. </param>
		/// <param name="packsize">The packing size of the type. </param>
		/// <exception cref="T:System.ArgumentException">A type with the given name exists in the parent assembly of this module.-or- Nested type attributes are set on a type that is not nested. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		// Token: 0x0600265E RID: 9822 RVA: 0x00087878 File Offset: 0x00085A78
		public TypeBuilder DefineType(string name, TypeAttributes attr, Type parent, PackingSize packsize)
		{
			return this.DefineType(name, attr, parent, null, packsize, 0);
		}

		/// <summary>Constructs a TypeBuilder given the type name, attributes, the type that the defined type extends, the packing size of the defined type, and the total size of the defined type.</summary>
		/// <returns>Returns a TypeBuilder created with all of the requested attributes.</returns>
		/// <param name="name">The full path of the type. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="attr">The attributes of the defined type. </param>
		/// <param name="parent">The type that the defined type extends. </param>
		/// <param name="packingSize">The packing size of the type. </param>
		/// <param name="typesize">The total size of the type. </param>
		/// <exception cref="T:System.ArgumentException">A type with the given name exists in the parent assembly of this module.-or- Nested type attributes are set on a type that is not nested. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		// Token: 0x0600265F RID: 9823 RVA: 0x00087888 File Offset: 0x00085A88
		public TypeBuilder DefineType(string name, TypeAttributes attr, Type parent, PackingSize packingSize, int typesize)
		{
			return this.DefineType(name, attr, parent, null, packingSize, typesize);
		}

		/// <summary>Returns the named method on an array class.</summary>
		/// <returns>The named method on an array class.</returns>
		/// <param name="arrayClass">An array class. </param>
		/// <param name="methodName">The name of a method on the array class. </param>
		/// <param name="callingConvention">The method's calling convention. </param>
		/// <param name="returnType">The return type of the method. </param>
		/// <param name="parameterTypes">The types of the method's parameters. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="arrayClass" /> is not an array. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="arrayClass" /> or <paramref name="methodName" /> is null. </exception>
		// Token: 0x06002660 RID: 9824 RVA: 0x00087898 File Offset: 0x00085A98
		public MethodInfo GetArrayMethod(Type arrayClass, string methodName, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
		{
			return new MonoArrayMethod(arrayClass, methodName, callingConvention, returnType, parameterTypes);
		}

		/// <summary>Defines an enumeration type that is a value type with a single non-static field called <paramref name="value__" /> of the specified type.</summary>
		/// <returns>Returns the defined enumeration.</returns>
		/// <param name="name">The full path of the enumeration type. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="visibility">The type attributes for the enumeration. The attributes are any bits defined by <see cref="F:System.Reflection.TypeAttributes.VisibilityMask" />. </param>
		/// <param name="underlyingType">The underlying type for the enumeration. This must be a built-in integer type. See .NET Framework Class Library Overview.</param>
		/// <exception cref="T:System.ArgumentException">Attributes other than visibility attributes are provided.-or- An enum with the given name exists in the parent assembly of this module.-or- When the visibility attributes are incorrect for the scope of the enum. For example, if <see cref="F:System.Reflection.TypeAttributes.NestedPublic" /> is specified as the <paramref name="visibility" /> but the enum is not a nested type. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		// Token: 0x06002661 RID: 9825 RVA: 0x000878A8 File Offset: 0x00085AA8
		public EnumBuilder DefineEnum(string name, TypeAttributes visibility, Type underlyingType)
		{
			if (this.name_cache.Contains(name))
			{
				throw new ArgumentException("Duplicate type name within an assembly.");
			}
			EnumBuilder enumBuilder = new EnumBuilder(this, name, visibility, underlyingType);
			TypeBuilder typeBuilder = enumBuilder.GetTypeBuilder();
			this.AddType(typeBuilder);
			this.name_cache.Add(name, typeBuilder);
			return enumBuilder;
		}

		/// <summary>Gets the named type defined in the module.</summary>
		/// <returns>The requested type. Returns null if the type is not found.</returns>
		/// <param name="className">The name of the <see cref="T:System.Type" /> to get. </param>
		/// <exception cref="T:System.ArgumentException">Length of <paramref name="className" /> is zero or if length of <paramref name="className" /> is greater than 1023. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="className" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The requested <see cref="T:System.Type" /> is non-public and the caller does not have <see cref="T:System.Security.Permissions.ReflectionPermission" /> to reflect non-public objects outside the current assembly. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A class initializer is invoked and throws an exception. </exception>
		/// <exception cref="T:System.TypeLoadException">An error is encountered while loading the <see cref="T:System.Type" />. </exception>
		// Token: 0x06002662 RID: 9826 RVA: 0x000878F8 File Offset: 0x00085AF8
		[ComVisible(true)]
		public override Type GetType(string className)
		{
			return this.GetType(className, false, false);
		}

		/// <summary>Gets the named type defined in the module optionally ignoring the case of the type name.</summary>
		/// <returns>The requested type. Returns null if the type is not found.</returns>
		/// <param name="className">The name of the <see cref="T:System.Type" /> to get. </param>
		/// <param name="ignoreCase">If true, the search is case-insensitive. If false, the search is case-sensitive. </param>
		/// <exception cref="T:System.ArgumentException">Length of <paramref name="className" /> is zero.-or- The length of <paramref name="className" /> is greater than 1023. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="className" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The requested <see cref="T:System.Type" /> is non-public and the caller does not have <see cref="T:System.Security.Permissions.ReflectionPermission" /> to reflect non-public objects outside the current assembly. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A class initializer is invoked and throws an exception. </exception>
		// Token: 0x06002663 RID: 9827 RVA: 0x00087904 File Offset: 0x00085B04
		[ComVisible(true)]
		public override Type GetType(string className, bool ignoreCase)
		{
			return this.GetType(className, false, ignoreCase);
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x00087910 File Offset: 0x00085B10
		private TypeBuilder search_in_array(TypeBuilder[] arr, int validElementsInArray, string className)
		{
			for (int i = 0; i < validElementsInArray; i++)
			{
				if (string.Compare(className, arr[i].FullName, true, CultureInfo.InvariantCulture) == 0)
				{
					return arr[i];
				}
			}
			return null;
		}

		// Token: 0x06002665 RID: 9829 RVA: 0x00087950 File Offset: 0x00085B50
		private TypeBuilder search_nested_in_array(TypeBuilder[] arr, int validElementsInArray, string className)
		{
			for (int i = 0; i < validElementsInArray; i++)
			{
				if (string.Compare(className, arr[i].Name, true, CultureInfo.InvariantCulture) == 0)
				{
					return arr[i];
				}
			}
			return null;
		}

		// Token: 0x06002666 RID: 9830
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Type create_modified_type(TypeBuilder tb, string modifiers);

		// Token: 0x06002667 RID: 9831 RVA: 0x00087990 File Offset: 0x00085B90
		private TypeBuilder GetMaybeNested(TypeBuilder t, string className)
		{
			int num = className.IndexOf('+');
			if (num >= 0)
			{
				if (t.subtypes != null)
				{
					string className2 = className.Substring(0, num);
					string className3 = className.Substring(num + 1);
					TypeBuilder typeBuilder = this.search_nested_in_array(t.subtypes, t.subtypes.Length, className2);
					if (typeBuilder != null)
					{
						return this.GetMaybeNested(typeBuilder, className3);
					}
				}
				return null;
			}
			if (t.subtypes != null)
			{
				return this.search_nested_in_array(t.subtypes, t.subtypes.Length, className);
			}
			return null;
		}

		/// <summary>Gets the named type defined in the module optionally ignoring the case of the type name. Optionally throws an exception if the type is not found.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the specified type, if the type is declared in this module; otherwise, null.</returns>
		/// <param name="className">The name of the <see cref="T:System.Type" /> to get. </param>
		/// <param name="throwOnError">true to throw an exception if the type cannot be found; false to return null. </param>
		/// <param name="ignoreCase">If true, the search is case-insensitive. If false, the search is case-sensitive. </param>
		/// <exception cref="T:System.ArgumentException">Length of <paramref name="className" /> is zero.-or- The length of <paramref name="className" /> is greater than 1023. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="className" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The requested <see cref="T:System.Type" /> is non-public and the caller does not have <see cref="T:System.Security.Permissions.ReflectionPermission" /> to reflect non-public objects outside the current assembly. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A class initializer is invoked and throws an exception. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="throwOnError" /> is true and the specified type is not found. </exception>
		// Token: 0x06002668 RID: 9832 RVA: 0x00087A14 File Offset: 0x00085C14
		[ComVisible(true)]
		public override Type GetType(string className, bool throwOnError, bool ignoreCase)
		{
			if (className == null)
			{
				throw new ArgumentNullException("className");
			}
			if (className.Length == 0)
			{
				throw new ArgumentException("className");
			}
			string message = className;
			TypeBuilder typeBuilder = null;
			if (this.types == null && throwOnError)
			{
				throw new TypeLoadException(className);
			}
			int num = className.IndexOfAny(ModuleBuilder.type_modifiers);
			string text;
			if (num >= 0)
			{
				text = className.Substring(num);
				className = className.Substring(0, num);
			}
			else
			{
				text = null;
			}
			if (!ignoreCase)
			{
				typeBuilder = (this.name_cache[className] as TypeBuilder);
			}
			else
			{
				num = className.IndexOf('+');
				if (num < 0)
				{
					if (this.types != null)
					{
						typeBuilder = this.search_in_array(this.types, this.num_types, className);
					}
				}
				else
				{
					string className2 = className.Substring(0, num);
					string className3 = className.Substring(num + 1);
					typeBuilder = this.search_in_array(this.types, this.num_types, className2);
					if (typeBuilder != null)
					{
						typeBuilder = this.GetMaybeNested(typeBuilder, className3);
					}
				}
			}
			if (typeBuilder == null && throwOnError)
			{
				throw new TypeLoadException(message);
			}
			if (typeBuilder != null && text != null)
			{
				Type type = ModuleBuilder.create_modified_type(typeBuilder, text);
				typeBuilder = (type as TypeBuilder);
				if (typeBuilder == null)
				{
					return type;
				}
			}
			if (typeBuilder != null && typeBuilder.is_created)
			{
				return typeBuilder.CreateType();
			}
			return typeBuilder;
		}

		// Token: 0x06002669 RID: 9833 RVA: 0x00087B70 File Offset: 0x00085D70
		internal int get_next_table_index(object obj, int table, bool inc)
		{
			if (this.table_indexes == null)
			{
				this.table_indexes = new int[64];
				for (int i = 0; i < 64; i++)
				{
					this.table_indexes[i] = 1;
				}
				this.table_indexes[2] = 2;
			}
			if (inc)
			{
				return this.table_indexes[table]++;
			}
			return this.table_indexes[table];
		}

		/// <summary>Set a custom attribute using a custom attribute builder.</summary>
		/// <param name="customBuilder">An instance of a helper class to define the custom attribute. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> is null. </exception>
		// Token: 0x0600266A RID: 9834 RVA: 0x00087BE0 File Offset: 0x00085DE0
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (this.cattrs != null)
			{
				CustomAttributeBuilder[] array = new CustomAttributeBuilder[this.cattrs.Length + 1];
				this.cattrs.CopyTo(array, 0);
				array[this.cattrs.Length] = customBuilder;
				this.cattrs = array;
			}
			else
			{
				this.cattrs = new CustomAttributeBuilder[1];
				this.cattrs[0] = customBuilder;
			}
		}

		/// <summary>Set a custom attribute using a specified custom attribute blob.</summary>
		/// <param name="con">The constructor for the custom attribute. </param>
		/// <param name="binaryAttribute">A byte blob representing the attributes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> or <paramref name="binaryAttribute" /> is null. </exception>
		// Token: 0x0600266B RID: 9835 RVA: 0x00087C44 File Offset: 0x00085E44
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		/// <summary>Returns the symbol writer associated with this dynamic module.</summary>
		/// <returns>Returns the symbol writer associated with this dynamic module.</returns>
		// Token: 0x0600266C RID: 9836 RVA: 0x00087C54 File Offset: 0x00085E54
		public ISymbolWriter GetSymWriter()
		{
			return this.symbolWriter;
		}

		/// <summary>Defines a document for source.</summary>
		/// <returns>An ISymbolDocumentWriter object representing the defined document.</returns>
		/// <param name="url">The URL for the document. </param>
		/// <param name="language">The GUID that identifies the document language. This can be <see cref="F:System.Guid.Empty" />. </param>
		/// <param name="languageVendor">The GUID that identifies the document language vendor. This can be <see cref="F:System.Guid.Empty" />. </param>
		/// <param name="documentType">The GUID that identifies the document type. This can be <see cref="F:System.Guid.Empty" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="url" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">This method is called on a dynamic module that is not a debug module. </exception>
		// Token: 0x0600266D RID: 9837 RVA: 0x00087C5C File Offset: 0x00085E5C
		public ISymbolDocumentWriter DefineDocument(string url, Guid language, Guid languageVendor, Guid documentType)
		{
			if (this.symbolWriter != null)
			{
				return this.symbolWriter.DefineDocument(url, language, languageVendor, documentType);
			}
			return null;
		}

		/// <summary>Returns all the classes defined within this module.</summary>
		/// <returns>An array of type Type containing classes defined within the module that is reflected by this instance.</returns>
		/// <exception cref="T:System.Reflection.ReflectionTypeLoadException">One or more classes in a module could not be loaded. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x0600266E RID: 9838 RVA: 0x00087C7C File Offset: 0x00085E7C
		public override Type[] GetTypes()
		{
			if (this.types == null)
			{
				return Type.EmptyTypes;
			}
			int num = this.num_types;
			Type[] array = new Type[num];
			Array.Copy(this.types, array, num);
			for (int i = 0; i < array.Length; i++)
			{
				if (this.types[i].is_created)
				{
					array[i] = this.types[i].CreateType();
				}
			}
			return array;
		}

		/// <summary>Defines the named managed embedded resource with the given attributes that is to be stored in this module.</summary>
		/// <returns>Returns a resource writer for the defined resource.</returns>
		/// <param name="name">The name of the resource. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="description">The description of the resource. </param>
		/// <param name="attribute">The resource attributes. </param>
		/// <exception cref="T:System.ArgumentException">Length of <paramref name="name" /> is zero. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">This module is transient.-or- The containing assembly is not persistable. </exception>
		// Token: 0x0600266F RID: 9839 RVA: 0x00087CEC File Offset: 0x00085EEC
		public IResourceWriter DefineResource(string name, string description, ResourceAttributes attribute)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name == string.Empty)
			{
				throw new ArgumentException("name cannot be empty");
			}
			if (this.transient)
			{
				throw new InvalidOperationException("The module is transient");
			}
			if (!this.assemblyb.IsSave)
			{
				throw new InvalidOperationException("The assembly is transient");
			}
			ResourceWriter resourceWriter = new ResourceWriter(new MemoryStream());
			if (this.resource_writers == null)
			{
				this.resource_writers = new Hashtable();
			}
			this.resource_writers[name] = resourceWriter;
			if (this.resources != null)
			{
				MonoResource[] destinationArray = new MonoResource[this.resources.Length + 1];
				Array.Copy(this.resources, destinationArray, this.resources.Length);
				this.resources = destinationArray;
			}
			else
			{
				this.resources = new MonoResource[1];
			}
			int num = this.resources.Length - 1;
			this.resources[num].name = name;
			this.resources[num].attrs = attribute;
			return resourceWriter;
		}

		/// <summary>Defines the named managed embedded resource to be stored in this module.</summary>
		/// <returns>Returns a resource writer for the defined resource.</returns>
		/// <param name="name">The name of the resource. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="description">The description of the resource. </param>
		/// <exception cref="T:System.ArgumentException">Length of <paramref name="name" /> is zero. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">This module is transient.-or- The containing assembly is not persistable. </exception>
		// Token: 0x06002670 RID: 9840 RVA: 0x00087DFC File Offset: 0x00085FFC
		public IResourceWriter DefineResource(string name, string description)
		{
			return this.DefineResource(name, description, ResourceAttributes.Public);
		}

		/// <summary>Defines an unmanaged embedded resource given an opaque blob of bytes.</summary>
		/// <param name="resource">An opaque blob that represents an unmanaged resource </param>
		/// <exception cref="T:System.ArgumentException">An unmanaged resource has already been defined in the module's assembly. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="resource" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002671 RID: 9841 RVA: 0x00087E08 File Offset: 0x00086008
		[MonoTODO]
		public void DefineUnmanagedResource(byte[] resource)
		{
			if (resource == null)
			{
				throw new ArgumentNullException("resource");
			}
			throw new NotImplementedException();
		}

		/// <summary>Defines an unmanaged resource given the name of Win32 resource file.</summary>
		/// <param name="resourceFileName">The name of the unmanaged resource file </param>
		/// <exception cref="T:System.ArgumentException">An unmanaged resource has already been defined in the module's assembly.-or- <paramref name="resourceFileName" /> is the empty string (""). </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="resourceFileName" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="resourceFileName" /> is not found -or- <paramref name="resourceFileName" /> is a directory </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002672 RID: 9842 RVA: 0x00087E20 File Offset: 0x00086020
		[MonoTODO]
		public void DefineUnmanagedResource(string resourceFileName)
		{
			if (resourceFileName == null)
			{
				throw new ArgumentNullException("resourceFileName");
			}
			if (resourceFileName == string.Empty)
			{
				throw new ArgumentException("resourceFileName");
			}
			if (!File.Exists(resourceFileName) || Directory.Exists(resourceFileName))
			{
				throw new FileNotFoundException("File '" + resourceFileName + "' does not exists or is a directory.");
			}
			throw new NotImplementedException();
		}

		/// <summary>Defines a manifest resource blob to be embedded in the dynamic assembly.</summary>
		/// <param name="name">The case-sensitive name for the resource.</param>
		/// <param name="stream">A stream that contains the bytes for the resource.</param>
		/// <param name="attribute">A <see cref="T:System.Reflection.ResourceAttributes" /> value that specifies whether the resource is public or private.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.-or-<paramref name="stream" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is a zero-length string.</exception>
		/// <exception cref="T:System.InvalidOperationException">The dynamic assembly that contains the current module is transient; that is, no file name was specified when <see cref="M:System.Reflection.Emit.AssemblyBuilder.DefineDynamicModule(System.String,System.String)" /> was called.</exception>
		// Token: 0x06002673 RID: 9843 RVA: 0x00087E8C File Offset: 0x0008608C
		public void DefineManifestResource(string name, Stream stream, ResourceAttributes attribute)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name == string.Empty)
			{
				throw new ArgumentException("name cannot be empty");
			}
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (this.transient)
			{
				throw new InvalidOperationException("The module is transient");
			}
			if (!this.assemblyb.IsSave)
			{
				throw new InvalidOperationException("The assembly is transient");
			}
			if (this.resources != null)
			{
				MonoResource[] destinationArray = new MonoResource[this.resources.Length + 1];
				Array.Copy(this.resources, destinationArray, this.resources.Length);
				this.resources = destinationArray;
			}
			else
			{
				this.resources = new MonoResource[1];
			}
			int num = this.resources.Length - 1;
			this.resources[num].name = name;
			this.resources[num].attrs = attribute;
			this.resources[num].stream = stream;
		}

		/// <summary>This method does nothing.</summary>
		/// <param name="name">The name of the custom attribute </param>
		/// <param name="data">An opaque blob of bytes that represents the value of the custom attribute. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="url" /> is null. </exception>
		// Token: 0x06002674 RID: 9844 RVA: 0x00087F90 File Offset: 0x00086190
		[MonoTODO]
		public void SetSymCustomAttribute(string name, byte[] data)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the user entry point.</summary>
		/// <param name="entryPoint">The user entry point. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="entryPoint" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">This method is called on a dynamic module that is not a debug module.-or- <paramref name="entryPoint" /> is not contained in this dynamic module. </exception>
		// Token: 0x06002675 RID: 9845 RVA: 0x00087F98 File Offset: 0x00086198
		[MonoTODO]
		public void SetUserEntryPoint(MethodInfo entryPoint)
		{
			if (entryPoint == null)
			{
				throw new ArgumentNullException("entryPoint");
			}
			if (entryPoint.DeclaringType.Module != this)
			{
				throw new InvalidOperationException("entryPoint is not contained in this module");
			}
			throw new NotImplementedException();
		}

		/// <summary>Returns the token used to identify the specified method within this module.</summary>
		/// <returns>Returns the token used to identify the method represented by method within this module.</returns>
		/// <param name="method">A MethodToken object representing the method to get a token for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="method" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The declaring type for the method is not in this module. </exception>
		// Token: 0x06002676 RID: 9846 RVA: 0x00087FD8 File Offset: 0x000861D8
		public MethodToken GetMethodToken(MethodInfo method)
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			if (method.DeclaringType.Module != this)
			{
				throw new InvalidOperationException("The method is not in this module");
			}
			return new MethodToken(this.GetToken(method));
		}

		/// <summary>Returns the token for the named method on an array class.</summary>
		/// <returns>The token for the named method on an array class.</returns>
		/// <param name="arrayClass">The Type object for the array. </param>
		/// <param name="methodName">A string containing the name of the method. </param>
		/// <param name="callingConvention">The calling convention for the method. </param>
		/// <param name="returnType">The return type of the method. </param>
		/// <param name="parameterTypes">The types of the parameters of the method. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="arrayClass" /> is not an array.-or- Length of <paramref name="methodName" /> is zero. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="arrayClass" /> or <paramref name="methodName" /> is null. </exception>
		// Token: 0x06002677 RID: 9847 RVA: 0x00088014 File Offset: 0x00086214
		public MethodToken GetArrayMethodToken(Type arrayClass, string methodName, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
		{
			return this.GetMethodToken(this.GetArrayMethod(arrayClass, methodName, callingConvention, returnType, parameterTypes));
		}

		/// <summary>Returns the token used to identify the specified constructor within this module.</summary>
		/// <returns>Returns the token used to identify the constructor represented by <paramref name="con" /> within this module.</returns>
		/// <param name="con">A ConstructorInfo object representing the constructor to get a token for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> is null. </exception>
		// Token: 0x06002678 RID: 9848 RVA: 0x0008802C File Offset: 0x0008622C
		[ComVisible(true)]
		public MethodToken GetConstructorToken(ConstructorInfo con)
		{
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			return new MethodToken(this.GetToken(con));
		}

		/// <summary>Returns the token used to identify the specified field within this module.</summary>
		/// <returns>Returns the token used to identify the field represented by <paramref name="con" /> within this module.</returns>
		/// <param name="field">A FieldInfo object representing the field to get a token for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> is null. </exception>
		// Token: 0x06002679 RID: 9849 RVA: 0x0008804C File Offset: 0x0008624C
		public FieldToken GetFieldToken(FieldInfo field)
		{
			if (field == null)
			{
				throw new ArgumentNullException("field");
			}
			if (field.DeclaringType.Module != this)
			{
				throw new InvalidOperationException("The method is not in this module");
			}
			return new FieldToken(this.GetToken(field));
		}

		/// <summary>Defines a signature token specified by the character array and signature length.</summary>
		/// <returns>A SignatureToken for the defined signature.</returns>
		/// <param name="sigBytes">The signature blob. </param>
		/// <param name="sigLength">The length of the signature blob. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sigBytes" /> is null. </exception>
		// Token: 0x0600267A RID: 9850 RVA: 0x00088088 File Offset: 0x00086288
		[MonoTODO]
		public SignatureToken GetSignatureToken(byte[] sigBytes, int sigLength)
		{
			throw new NotImplementedException();
		}

		/// <summary>Defines a signature token using the given SignatureHelper object.</summary>
		/// <returns>A SignatureToken for the defined signature.</returns>
		/// <param name="sigHelper">A reference to a SignatureHelper. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sigHelper" /> is null. </exception>
		// Token: 0x0600267B RID: 9851 RVA: 0x00088090 File Offset: 0x00086290
		public SignatureToken GetSignatureToken(SignatureHelper sigHelper)
		{
			if (sigHelper == null)
			{
				throw new ArgumentNullException("sigHelper");
			}
			return new SignatureToken(this.GetToken(sigHelper));
		}

		/// <summary>Returns the token of the given string in the module’s constant pool.</summary>
		/// <returns>Returns the StringToken of the string added to the constant pool.</returns>
		/// <param name="str">The string to add to the module's constant pool. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is null. </exception>
		// Token: 0x0600267C RID: 9852 RVA: 0x000880B0 File Offset: 0x000862B0
		public StringToken GetStringConstant(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			return new StringToken(this.GetToken(str));
		}

		/// <summary>Returns the token used to identify the specified type within this module.</summary>
		/// <returns>Returns the TypeToken used to identify the given type within this module.</returns>
		/// <param name="type">The type object that represents the class type. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> is a ByRef type. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">A non-transient module that references a transient module. </exception>
		// Token: 0x0600267D RID: 9853 RVA: 0x000880D0 File Offset: 0x000862D0
		public TypeToken GetTypeToken(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type.IsByRef)
			{
				throw new ArgumentException("type can't be a byref type", "type");
			}
			if (!this.IsTransient() && type.Module is ModuleBuilder && ((ModuleBuilder)type.Module).IsTransient())
			{
				throw new InvalidOperationException("a non-transient module can't reference a transient module");
			}
			return new TypeToken(this.GetToken(type));
		}

		/// <summary>Returns the token used to identify the type given its name.</summary>
		/// <returns>Returns the TypeToken used to identify the type given by name within this module.</returns>
		/// <param name="name">A string representing the name of the class, including the namespace. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is the empty string ("").-or-<paramref name="name" /> represents a ByRef type. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. -or-The type specified by <paramref name="name" /> could not be found.</exception>
		/// <exception cref="T:System.InvalidOperationException">This is a non-transient module that references a transient module. </exception>
		// Token: 0x0600267E RID: 9854 RVA: 0x00088150 File Offset: 0x00086350
		public TypeToken GetTypeToken(string name)
		{
			return this.GetTypeToken(this.GetType(name));
		}

		// Token: 0x0600267F RID: 9855
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int getUSIndex(ModuleBuilder mb, string str);

		// Token: 0x06002680 RID: 9856
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int getToken(ModuleBuilder mb, object obj);

		// Token: 0x06002681 RID: 9857
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int getMethodToken(ModuleBuilder mb, MethodInfo method, Type[] opt_param_types);

		// Token: 0x06002682 RID: 9858 RVA: 0x00088160 File Offset: 0x00086360
		internal int GetToken(string str)
		{
			if (this.us_string_cache.Contains(str))
			{
				return (int)this.us_string_cache[str];
			}
			int usindex = ModuleBuilder.getUSIndex(this, str);
			this.us_string_cache[str] = usindex;
			return usindex;
		}

		// Token: 0x06002683 RID: 9859 RVA: 0x000881AC File Offset: 0x000863AC
		internal int GetToken(MemberInfo member)
		{
			return ModuleBuilder.getToken(this, member);
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x000881B8 File Offset: 0x000863B8
		internal int GetToken(MethodInfo method, Type[] opt_param_types)
		{
			return ModuleBuilder.getMethodToken(this, method, opt_param_types);
		}

		// Token: 0x06002685 RID: 9861 RVA: 0x000881C4 File Offset: 0x000863C4
		internal int GetToken(SignatureHelper helper)
		{
			return ModuleBuilder.getToken(this, helper);
		}

		// Token: 0x06002686 RID: 9862
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void RegisterToken(object obj, int token);

		// Token: 0x06002687 RID: 9863 RVA: 0x000881D0 File Offset: 0x000863D0
		internal TokenGenerator GetTokenGenerator()
		{
			if (this.token_gen == null)
			{
				this.token_gen = new ModuleBuilderTokenGenerator(this);
			}
			return this.token_gen;
		}

		// Token: 0x06002688 RID: 9864
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void build_metadata(ModuleBuilder mb);

		// Token: 0x06002689 RID: 9865
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void WriteToFile(IntPtr handle);

		// Token: 0x0600268A RID: 9866 RVA: 0x000881F0 File Offset: 0x000863F0
		internal void Save()
		{
			if (this.transient && !this.is_main)
			{
				return;
			}
			if (this.types != null)
			{
				for (int i = 0; i < this.num_types; i++)
				{
					if (!this.types[i].is_created)
					{
						throw new NotSupportedException("Type '" + this.types[i].FullName + "' was not completed.");
					}
				}
			}
			if (this.global_type != null && this.global_type_created == null)
			{
				this.global_type_created = this.global_type.CreateType();
			}
			if (this.resources != null)
			{
				for (int j = 0; j < this.resources.Length; j++)
				{
					IResourceWriter resourceWriter;
					if (this.resource_writers != null && (resourceWriter = (this.resource_writers[this.resources[j].name] as IResourceWriter)) != null)
					{
						ResourceWriter resourceWriter2 = (ResourceWriter)resourceWriter;
						resourceWriter2.Generate();
						MemoryStream memoryStream = (MemoryStream)resourceWriter2.Stream;
						this.resources[j].data = new byte[memoryStream.Length];
						memoryStream.Seek(0L, SeekOrigin.Begin);
						memoryStream.Read(this.resources[j].data, 0, (int)memoryStream.Length);
					}
					else
					{
						Stream stream = this.resources[j].stream;
						if (stream != null)
						{
							try
							{
								long length = stream.Length;
								this.resources[j].data = new byte[length];
								stream.Seek(0L, SeekOrigin.Begin);
								stream.Read(this.resources[j].data, 0, (int)length);
							}
							catch
							{
							}
						}
					}
				}
			}
			ModuleBuilder.build_metadata(this);
			string text = this.fqname;
			if (this.assemblyb.AssemblyDir != null)
			{
				text = Path.Combine(this.assemblyb.AssemblyDir, text);
			}
			try
			{
				File.Delete(text);
			}
			catch
			{
			}
			using (FileStream fileStream = new FileStream(text, FileMode.Create, FileAccess.Write))
			{
				this.WriteToFile(fileStream.Handle);
			}
			File.SetAttributes(text, (FileAttributes)(-2147483648));
			if (this.types != null && this.symbolWriter != null)
			{
				for (int k = 0; k < this.num_types; k++)
				{
					this.types[k].GenerateDebugInfo(this.symbolWriter);
				}
				this.symbolWriter.Close();
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x0600268B RID: 9867 RVA: 0x000884DC File Offset: 0x000866DC
		internal string FileName
		{
			get
			{
				return this.fqname;
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (set) Token: 0x0600268C RID: 9868 RVA: 0x000884E4 File Offset: 0x000866E4
		internal bool IsMain
		{
			set
			{
				this.is_main = value;
			}
		}

		// Token: 0x0600268D RID: 9869 RVA: 0x000884F0 File Offset: 0x000866F0
		internal void CreateGlobalType()
		{
			if (this.global_type == null)
			{
				this.global_type = new TypeBuilder(this, TypeAttributes.NotPublic, 1);
			}
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x0008850C File Offset: 0x0008670C
		internal override Guid GetModuleVersionId()
		{
			return new Guid(this.guid);
		}

		// Token: 0x0600268F RID: 9871 RVA: 0x0008851C File Offset: 0x0008671C
		internal static Guid Mono_GetGuid(ModuleBuilder mb)
		{
			return mb.GetModuleVersionId();
		}

		// Token: 0x04000E62 RID: 3682
		private UIntPtr dynamic_image;

		// Token: 0x04000E63 RID: 3683
		private int num_types;

		// Token: 0x04000E64 RID: 3684
		private TypeBuilder[] types;

		// Token: 0x04000E65 RID: 3685
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x04000E66 RID: 3686
		private byte[] guid;

		// Token: 0x04000E67 RID: 3687
		private int table_idx;

		// Token: 0x04000E68 RID: 3688
		internal AssemblyBuilder assemblyb;

		// Token: 0x04000E69 RID: 3689
		private MethodBuilder[] global_methods;

		// Token: 0x04000E6A RID: 3690
		private FieldBuilder[] global_fields;

		// Token: 0x04000E6B RID: 3691
		private bool is_main;

		// Token: 0x04000E6C RID: 3692
		private MonoResource[] resources;

		// Token: 0x04000E6D RID: 3693
		private TypeBuilder global_type;

		// Token: 0x04000E6E RID: 3694
		private Type global_type_created;

		// Token: 0x04000E6F RID: 3695
		private Hashtable name_cache;

		// Token: 0x04000E70 RID: 3696
		private Hashtable us_string_cache = new Hashtable();

		// Token: 0x04000E71 RID: 3697
		private int[] table_indexes;

		// Token: 0x04000E72 RID: 3698
		private bool transient;

		// Token: 0x04000E73 RID: 3699
		private ModuleBuilderTokenGenerator token_gen;

		// Token: 0x04000E74 RID: 3700
		private Hashtable resource_writers;

		// Token: 0x04000E75 RID: 3701
		private ISymbolWriter symbolWriter;

		// Token: 0x04000E76 RID: 3702
		private static readonly char[] type_modifiers = new char[]
		{
			'&',
			'[',
			'*'
		};
	}
}
