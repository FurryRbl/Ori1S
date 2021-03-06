using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Provides methods for building signatures.</summary>
	// Token: 0x020002FA RID: 762
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_SignatureHelper))]
	public sealed class SignatureHelper : _SignatureHelper
	{
		// Token: 0x0600270B RID: 9995 RVA: 0x0008ABDC File Offset: 0x00088DDC
		internal SignatureHelper(ModuleBuilder module, SignatureHelper.SignatureHelperType type)
		{
			this.type = type;
			this.module = module;
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x0600270C RID: 9996 RVA: 0x0008ABF4 File Offset: 0x00088DF4
		void _SignatureHelper.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x0600270D RID: 9997 RVA: 0x0008ABFC File Offset: 0x00088DFC
		void _SignatureHelper.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x0600270E RID: 9998 RVA: 0x0008AC04 File Offset: 0x00088E04
		void _SignatureHelper.GetTypeInfoCount(out uint pcTInfo)
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
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x0600270F RID: 9999 RVA: 0x0008AC0C File Offset: 0x00088E0C
		void _SignatureHelper.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a signature helper for a field.</summary>
		/// <returns>The SignatureHelper object for a field.</returns>
		/// <param name="mod">The dynamic module that contains the field for which the SignatureHelper is requested. </param>
		// Token: 0x06002710 RID: 10000 RVA: 0x0008AC14 File Offset: 0x00088E14
		public static SignatureHelper GetFieldSigHelper(Module mod)
		{
			if (mod != null && !(mod is ModuleBuilder))
			{
				throw new ArgumentException("ModuleBuilder is expected");
			}
			return new SignatureHelper((ModuleBuilder)mod, SignatureHelper.SignatureHelperType.HELPER_FIELD);
		}

		/// <summary>Returns a signature helper for a local variable.</summary>
		/// <returns>The SignatureHelper object for a local variable.</returns>
		/// <param name="mod">The dynamic module that contains the local variable for which the SignatureHelper is requested. </param>
		// Token: 0x06002711 RID: 10001 RVA: 0x0008AC4C File Offset: 0x00088E4C
		public static SignatureHelper GetLocalVarSigHelper(Module mod)
		{
			if (mod != null && !(mod is ModuleBuilder))
			{
				throw new ArgumentException("ModuleBuilder is expected");
			}
			return new SignatureHelper((ModuleBuilder)mod, SignatureHelper.SignatureHelperType.HELPER_LOCAL);
		}

		/// <summary>Returns a signature helper for a local variable.</summary>
		/// <returns>A <see cref="T:System.Reflection.Emit.SignatureHelper" /> for a local variable.</returns>
		// Token: 0x06002712 RID: 10002 RVA: 0x0008AC84 File Offset: 0x00088E84
		public static SignatureHelper GetLocalVarSigHelper()
		{
			return new SignatureHelper(null, SignatureHelper.SignatureHelperType.HELPER_LOCAL);
		}

		/// <summary>Returns a signature helper for a method given the method's calling convention and return type.</summary>
		/// <returns>The SignatureHelper object for a method.</returns>
		/// <param name="callingConvention">The calling convention of the method. </param>
		/// <param name="returnType">The return type of the method, or null for a void return type (Sub procedure in Visual Basic). </param>
		// Token: 0x06002713 RID: 10003 RVA: 0x0008AC90 File Offset: 0x00088E90
		public static SignatureHelper GetMethodSigHelper(CallingConventions callingConvention, Type returnType)
		{
			return SignatureHelper.GetMethodSigHelper(null, callingConvention, (CallingConvention)0, returnType, null);
		}

		/// <summary>Returns a signature helper for a method given the method's unmanaged calling convention and return type.</summary>
		/// <returns>The SignatureHelper object for a method.</returns>
		/// <param name="unmanagedCallingConvention">The unmanaged calling convention of the method. </param>
		/// <param name="returnType">The return type of the method, or null for a void return type (Sub procedure in Visual Basic). </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="unmanagedCallConv" /> is an unknown unmanaged calling convention.</exception>
		// Token: 0x06002714 RID: 10004 RVA: 0x0008AC9C File Offset: 0x00088E9C
		public static SignatureHelper GetMethodSigHelper(CallingConvention unmanagedCallingConvention, Type returnType)
		{
			return SignatureHelper.GetMethodSigHelper(null, CallingConventions.Standard, unmanagedCallingConvention, returnType, null);
		}

		/// <summary>Returns a signature helper for a method given the method's module, calling convention, and return type.</summary>
		/// <returns>The SignatureHelper object for a method.</returns>
		/// <param name="mod">The <see cref="T:System.Reflection.Emit.ModuleBuilder" /> that contains the method for which the SignatureHelper is requested. </param>
		/// <param name="callingConvention">The calling convention of the method. </param>
		/// <param name="returnType">The return type of the method, or null for a void return type (Sub procedure in Visual Basic). </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="mod" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mod" /> is not a <see cref="T:System.Reflection.Emit.ModuleBuilder" />.</exception>
		// Token: 0x06002715 RID: 10005 RVA: 0x0008ACA8 File Offset: 0x00088EA8
		public static SignatureHelper GetMethodSigHelper(Module mod, CallingConventions callingConvention, Type returnType)
		{
			return SignatureHelper.GetMethodSigHelper(mod, callingConvention, (CallingConvention)0, returnType, null);
		}

		/// <summary>Returns a signature helper for a method given the method's module, unmanaged calling convention, and return type.</summary>
		/// <returns>The SignatureHelper object for a method.</returns>
		/// <param name="mod">The <see cref="T:System.Reflection.Emit.ModuleBuilder" /> that contains the method for which the SignatureHelper is requested. </param>
		/// <param name="unmanagedCallConv">The unmanaged calling convention of the method. </param>
		/// <param name="returnType">The return type of the method, or null for a void return type (Sub procedure in Visual Basic). </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="mod" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mod" /> is not a <see cref="T:System.Reflection.Emit.ModuleBuilder" />.-or-<paramref name="unmanagedCallConv" /> is an unknown unmanaged calling convention.</exception>
		// Token: 0x06002716 RID: 10006 RVA: 0x0008ACB4 File Offset: 0x00088EB4
		public static SignatureHelper GetMethodSigHelper(Module mod, CallingConvention unmanagedCallConv, Type returnType)
		{
			return SignatureHelper.GetMethodSigHelper(mod, CallingConventions.Standard, unmanagedCallConv, returnType, null);
		}

		/// <summary>Returns a signature helper for a method with a standard calling convention, given the method's module, return type, and argument types.</summary>
		/// <returns>The SignatureHelper object for a method.</returns>
		/// <param name="mod">The <see cref="T:System.Reflection.Emit.ModuleBuilder" /> that contains the method for which the SignatureHelper is requested. </param>
		/// <param name="returnType">The return type of the method, or null for a void return type (Sub procedure in Visual Basic). </param>
		/// <param name="parameterTypes">The types of the arguments of the method, or null if the method has no arguments. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="mod" /> is null.-or-An element of <paramref name="parameterTypes" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mod" /> is not a <see cref="T:System.Reflection.Emit.ModuleBuilder" />.</exception>
		// Token: 0x06002717 RID: 10007 RVA: 0x0008ACC0 File Offset: 0x00088EC0
		public static SignatureHelper GetMethodSigHelper(Module mod, Type returnType, Type[] parameterTypes)
		{
			return SignatureHelper.GetMethodSigHelper(mod, CallingConventions.Standard, (CallingConvention)0, returnType, parameterTypes);
		}

		/// <summary>Returns a signature helper for a property, given the dynamic module that contains the property, the property type, and the property arguments.</summary>
		/// <returns>A <see cref="T:System.Reflection.Emit.SignatureHelper" /> object for a property.</returns>
		/// <param name="mod">The <see cref="T:System.Reflection.Emit.ModuleBuilder" /> that contains the property for which the <see cref="T:System.Reflection.Emit.SignatureHelper" /> is requested.</param>
		/// <param name="returnType">The property type.</param>
		/// <param name="parameterTypes">The argument types, or null if the property has no arguments.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="mod" /> is null.-or-An element of <paramref name="parameterTypes" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mod" /> is not a <see cref="T:System.Reflection.Emit.ModuleBuilder" />.</exception>
		// Token: 0x06002718 RID: 10008 RVA: 0x0008ACCC File Offset: 0x00088ECC
		[MonoTODO("Not implemented")]
		public static SignatureHelper GetPropertySigHelper(Module mod, Type returnType, Type[] parameterTypes)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002719 RID: 10009 RVA: 0x0008ACD4 File Offset: 0x00088ED4
		private static int AppendArray(ref Type[] array, Type t)
		{
			if (array != null)
			{
				Type[] array2 = new Type[array.Length + 1];
				Array.Copy(array, array2, array.Length);
				array2[array.Length] = t;
				array = array2;
				return array.Length - 1;
			}
			array = new Type[1];
			array[0] = t;
			return 0;
		}

		// Token: 0x0600271A RID: 10010 RVA: 0x0008AD20 File Offset: 0x00088F20
		private static void AppendArrayAt(ref Type[][] array, Type[] t, int pos)
		{
			int num = Math.Max(pos, (array != null) ? array.Length : 0);
			Type[][] array2 = new Type[num + 1][];
			if (array != null)
			{
				Array.Copy(array, array2, num);
			}
			array2[pos] = t;
			array = array2;
		}

		// Token: 0x0600271B RID: 10011 RVA: 0x0008AD68 File Offset: 0x00088F68
		private static void ValidateParameterModifiers(string name, Type[] parameter_modifiers)
		{
			foreach (Type type in parameter_modifiers)
			{
				if (type == null)
				{
					throw new ArgumentNullException(name);
				}
				if (type.IsArray)
				{
					throw new ArgumentException(Locale.GetText("Array type not permitted"), name);
				}
				if (type.ContainsGenericParameters)
				{
					throw new ArgumentException(Locale.GetText("Open Generic Type not permitted"), name);
				}
			}
		}

		// Token: 0x0600271C RID: 10012 RVA: 0x0008ADD4 File Offset: 0x00088FD4
		private static void ValidateCustomModifier(int n, Type[][] custom_modifiers, string name)
		{
			if (custom_modifiers == null)
			{
				return;
			}
			if (custom_modifiers.Length != n)
			{
				throw new ArgumentException(Locale.GetText(string.Format("Custom modifiers length `{0}' does not match the size of the arguments", new object[0])));
			}
			foreach (Type[] array in custom_modifiers)
			{
				if (array != null)
				{
					SignatureHelper.ValidateParameterModifiers(name, array);
				}
			}
		}

		// Token: 0x0600271D RID: 10013 RVA: 0x0008AE38 File Offset: 0x00089038
		private static Exception MissingFeature()
		{
			throw new NotImplementedException("Mono does not currently support setting modOpt/modReq through SignatureHelper");
		}

		/// <summary>Adds a set of arguments to the signature, with the specified custom modifiers.</summary>
		/// <param name="arguments">The types of the arguments to be added.</param>
		/// <param name="requiredCustomModifiers">An array of arrays of types. Each array of types represents the required custom modifiers for the corresponding argument, such as <see cref="T:System.Runtime.CompilerServices.IsConst" /> or <see cref="T:System.Runtime.CompilerServices.IsBoxed" />. If a particular argument has no required custom modifiers, specify null instead of an array of types. If none of the arguments have required custom modifiers, specify null instead of an array of arrays.</param>
		/// <param name="optionalCustomModifiers">An array of arrays of types. Each array of types represents the optional custom modifiers for the corresponding argument, such as <see cref="T:System.Runtime.CompilerServices.IsConst" /> or <see cref="T:System.Runtime.CompilerServices.IsBoxed" />. If a particular argument has no optional custom modifiers, specify null instead of an array of types. If none of the arguments have optional custom modifiers, specify null instead of an array of arrays.</param>
		/// <exception cref="T:System.ArgumentNullException">An element of <paramref name="arguments" /> is null. -or-One of the specified custom modifiers is null. (However, null can be specified for the array of custom modifiers for any argument.)</exception>
		/// <exception cref="T:System.ArgumentException">The signature has already been finished. -or-One of the specified custom modifiers is an array type.-or-One of the specified custom modifiers is an open generic type. That is, the <see cref="P:System.Type.ContainsGenericParameters" /> property is true for the custom modifier. -or-The size of <paramref name="requiredCustomModifiers" /> or <paramref name="optionalCustomModifiers" /> does not equal the size of <paramref name="arguments" />.</exception>
		// Token: 0x0600271E RID: 10014 RVA: 0x0008AE44 File Offset: 0x00089044
		[MonoTODO("Currently we ignore requiredCustomModifiers and optionalCustomModifiers")]
		public void AddArguments(Type[] arguments, Type[][] requiredCustomModifiers, Type[][] optionalCustomModifiers)
		{
			if (arguments == null)
			{
				throw new ArgumentNullException("arguments");
			}
			if (requiredCustomModifiers != null || optionalCustomModifiers != null)
			{
				throw SignatureHelper.MissingFeature();
			}
			SignatureHelper.ValidateCustomModifier(arguments.Length, requiredCustomModifiers, "requiredCustomModifiers");
			SignatureHelper.ValidateCustomModifier(arguments.Length, optionalCustomModifiers, "optionalCustomModifiers");
			for (int i = 0; i < arguments.Length; i++)
			{
				this.AddArgument(arguments[i], (requiredCustomModifiers == null) ? null : requiredCustomModifiers[i], (optionalCustomModifiers == null) ? null : optionalCustomModifiers[i]);
			}
		}

		/// <summary>Adds an argument of the specified type to the signature, specifying whether the argument is pinned.</summary>
		/// <param name="argument">The argument type.</param>
		/// <param name="pinned">true if the argument is pinned; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="argument" /> is null.</exception>
		// Token: 0x0600271F RID: 10015 RVA: 0x0008AECC File Offset: 0x000890CC
		[MonoTODO("pinned is ignored")]
		public void AddArgument(Type argument, bool pinned)
		{
			this.AddArgument(argument);
		}

		/// <summary>Adds an argument to the signature, with the specified custom modifiers.</summary>
		/// <param name="argument">The argument type.</param>
		/// <param name="requiredCustomModifiers">An array of types representing the required custom modifiers for the argument, such as <see cref="T:System.Runtime.CompilerServices.IsConst" /> or <see cref="T:System.Runtime.CompilerServices.IsBoxed" />. If the argument has no required custom modifiers, specify null.</param>
		/// <param name="optionalCustomModifiers">An array of types representing the optional custom modifiers for the argument, such as <see cref="T:System.Runtime.CompilerServices.IsConst" /> or <see cref="T:System.Runtime.CompilerServices.IsBoxed" />. If the argument has no optional custom modifiers, specify null.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="argument" /> is null. -or-An element of <paramref name="requiredCustomModifiers" /> or <paramref name="optionalCustomModifiers" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The signature has already been finished. -or-One of the specified custom modifiers is an array type.-or-One of the specified custom modifiers is an open generic type. That is, the <see cref="P:System.Type.ContainsGenericParameters" /> property is true for the custom modifier.</exception>
		// Token: 0x06002720 RID: 10016 RVA: 0x0008AED8 File Offset: 0x000890D8
		public void AddArgument(Type argument, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers)
		{
			if (argument == null)
			{
				throw new ArgumentNullException("argument");
			}
			if (requiredCustomModifiers != null)
			{
				SignatureHelper.ValidateParameterModifiers("requiredCustomModifiers", requiredCustomModifiers);
			}
			if (optionalCustomModifiers != null)
			{
				SignatureHelper.ValidateParameterModifiers("optionalCustomModifiers", optionalCustomModifiers);
			}
			int pos = SignatureHelper.AppendArray(ref this.arguments, argument);
			if (requiredCustomModifiers != null)
			{
				SignatureHelper.AppendArrayAt(ref this.modreqs, requiredCustomModifiers, pos);
			}
			if (optionalCustomModifiers != null)
			{
				SignatureHelper.AppendArrayAt(ref this.modopts, optionalCustomModifiers, pos);
			}
		}

		/// <summary>Returns a signature helper for a property, given the dynamic module that contains the property, the property type, the property arguments, and custom modifiers for the return type and arguments.</summary>
		/// <returns>A <see cref="T:System.Reflection.Emit.SignatureHelper" /> object for a property.</returns>
		/// <param name="mod">The <see cref="T:System.Reflection.Emit.ModuleBuilder" /> that contains the property for which the <see cref="T:System.Reflection.Emit.SignatureHelper" /> is requested.</param>
		/// <param name="returnType">The property type.</param>
		/// <param name="requiredReturnTypeCustomModifiers">An array of types representing the required custom modifiers for the return type, such as <see cref="T:System.Runtime.CompilerServices.IsConst" /> or <see cref="T:System.Runtime.CompilerServices.IsBoxed" />. If the return type has no required custom modifiers, specify null.</param>
		/// <param name="optionalReturnTypeCustomModifiers">An array of types representing the optional custom modifiers for the return type, such as <see cref="T:System.Runtime.CompilerServices.IsConst" /> or <see cref="T:System.Runtime.CompilerServices.IsBoxed" />. If the return type has no optional custom modifiers, specify null.</param>
		/// <param name="parameterTypes">The types of the property's arguments, or null if the property has no arguments.</param>
		/// <param name="requiredParameterTypeCustomModifiers">An array of arrays of types. Each array of types represents the required custom modifiers for the corresponding argument of the property. If a particular argument has no required custom modifiers, specify null instead of an array of types. If the property has no arguments, or if none of the arguments have required custom modifiers, specify null instead of an array of arrays.</param>
		/// <param name="optionalParameterTypeCustomModifiers">An array of arrays of types. Each array of types represents the optional custom modifiers for the corresponding argument of the property. If a particular argument has no optional custom modifiers, specify null instead of an array of types. If the property has no arguments, or if none of the arguments have optional custom modifiers, specify null instead of an array of arrays.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="mod" /> is null.-or-An element of <paramref name="parameterTypes" /> is null. -or-One of the specified custom modifiers is null. (However, null can be specified for the array of custom modifiers for any argument.)</exception>
		/// <exception cref="T:System.ArgumentException">The signature has already been finished. -or-<paramref name="mod" /> is not a <see cref="T:System.Reflection.Emit.ModuleBuilder" />.-or-One of the specified custom modifiers is an array type.-or-One of the specified custom modifiers is an open generic type. That is, the <see cref="P:System.Type.ContainsGenericParameters" /> property is true for the custom modifier.-or-The size of <paramref name="requiredParameterTypeCustomModifiers" /> or <paramref name="optionalParameterTypeCustomModifiers" /> does not equal the size of <paramref name="parameterTypes" />.</exception>
		// Token: 0x06002721 RID: 10017 RVA: 0x0008AF4C File Offset: 0x0008914C
		[MonoTODO("Not implemented")]
		public static SignatureHelper GetPropertySigHelper(Module mod, Type returnType, Type[] requiredReturnTypeCustomModifiers, Type[] optionalReturnTypeCustomModifiers, Type[] parameterTypes, Type[][] requiredParameterTypeCustomModifiers, Type[][] optionalParameterTypeCustomModifiers)
		{
			throw new NotImplementedException();
		}

		/// <summary>Adds an argument to the signature.</summary>
		/// <param name="clsArgument">The type of the argument. </param>
		/// <exception cref="T:System.ArgumentException">The signature has already been finished. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="clsArgument" /> is null.</exception>
		// Token: 0x06002722 RID: 10018 RVA: 0x0008AF54 File Offset: 0x00089154
		public void AddArgument(Type clsArgument)
		{
			if (clsArgument == null)
			{
				throw new ArgumentNullException("clsArgument");
			}
			SignatureHelper.AppendArray(ref this.arguments, clsArgument);
		}

		/// <summary>Marks the end of a vararg fixed part. This is only used if the caller is creating a vararg signature call site.</summary>
		// Token: 0x06002723 RID: 10019 RVA: 0x0008AF74 File Offset: 0x00089174
		[MonoTODO("Not implemented")]
		public void AddSentinel()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002724 RID: 10020 RVA: 0x0008AF7C File Offset: 0x0008917C
		private static bool CompareOK(Type[][] one, Type[][] two)
		{
			if (one == null)
			{
				return two == null;
			}
			if (two == null)
			{
				return false;
			}
			if (one.Length != two.Length)
			{
				return false;
			}
			int i = 0;
			while (i < one.Length)
			{
				Type[] array = one[i];
				Type[] array2 = two[i];
				if (array == null)
				{
					if (array2 != null)
					{
						goto IL_52;
					}
				}
				else
				{
					if (array2 == null)
					{
						return false;
					}
					goto IL_52;
				}
				IL_AB:
				i++;
				continue;
				IL_52:
				if (array.Length != array2.Length)
				{
					return false;
				}
				for (int j = 0; j < array.Length; j++)
				{
					Type type = array[j];
					Type type2 = array2[j];
					if (type == null)
					{
						if (type2 != null)
						{
							return false;
						}
					}
					else
					{
						if (type2 == null)
						{
							return false;
						}
						if (!type.Equals(type2))
						{
							return false;
						}
					}
				}
				goto IL_AB;
			}
			return true;
		}

		/// <summary>Checks if this instance is equal to the given object.</summary>
		/// <returns>true if the given object is a SignatureHelper and represents the same signature; otherwise, false.</returns>
		/// <param name="obj">The object with which this instance should be compared. </param>
		// Token: 0x06002725 RID: 10021 RVA: 0x0008B044 File Offset: 0x00089244
		public override bool Equals(object obj)
		{
			SignatureHelper signatureHelper = obj as SignatureHelper;
			if (signatureHelper == null)
			{
				return false;
			}
			if (signatureHelper.module != this.module || signatureHelper.returnType != this.returnType || signatureHelper.callConv != this.callConv || signatureHelper.unmanagedCallConv != this.unmanagedCallConv)
			{
				return false;
			}
			if (this.arguments != null)
			{
				if (signatureHelper.arguments == null)
				{
					return false;
				}
				if (this.arguments.Length != signatureHelper.arguments.Length)
				{
					return false;
				}
				for (int i = 0; i < this.arguments.Length; i++)
				{
					if (!signatureHelper.arguments[i].Equals(this.arguments[i]))
					{
						return false;
					}
				}
			}
			else if (signatureHelper.arguments != null)
			{
				return false;
			}
			return SignatureHelper.CompareOK(signatureHelper.modreqs, this.modreqs) && SignatureHelper.CompareOK(signatureHelper.modopts, this.modopts);
		}

		/// <summary>Creates and returns a hash code for this instance.</summary>
		/// <returns>Returns the hash code based on the name.</returns>
		// Token: 0x06002726 RID: 10022 RVA: 0x0008B148 File Offset: 0x00089348
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x06002727 RID: 10023
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern byte[] get_signature_local();

		// Token: 0x06002728 RID: 10024
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern byte[] get_signature_field();

		/// <summary>Adds the end token to the signature and marks the signature as finished, so no further tokens can be added.</summary>
		/// <returns>Returns a byte array made up of the full signature.</returns>
		// Token: 0x06002729 RID: 10025 RVA: 0x0008B14C File Offset: 0x0008934C
		public byte[] GetSignature()
		{
			SignatureHelper.SignatureHelperType signatureHelperType = this.type;
			if (signatureHelperType == SignatureHelper.SignatureHelperType.HELPER_FIELD)
			{
				return this.get_signature_field();
			}
			if (signatureHelperType != SignatureHelper.SignatureHelperType.HELPER_LOCAL)
			{
				throw new NotImplementedException();
			}
			return this.get_signature_local();
		}

		/// <summary>Returns a string representing the signature arguments.</summary>
		/// <returns>Returns a string representing the arguments of this signature.</returns>
		// Token: 0x0600272A RID: 10026 RVA: 0x0008B188 File Offset: 0x00089388
		public override string ToString()
		{
			return "SignatureHelper";
		}

		// Token: 0x0600272B RID: 10027 RVA: 0x0008B190 File Offset: 0x00089390
		internal static SignatureHelper GetMethodSigHelper(Module mod, CallingConventions callingConvention, CallingConvention unmanagedCallingConvention, Type returnType, Type[] parameters)
		{
			if (mod != null && !(mod is ModuleBuilder))
			{
				throw new ArgumentException("ModuleBuilder is expected");
			}
			if (returnType == null)
			{
				returnType = typeof(void);
			}
			if (returnType.IsUserType)
			{
				throw new NotSupportedException("User defined subclasses of System.Type are not yet supported.");
			}
			if (parameters != null)
			{
				for (int i = 0; i < parameters.Length; i++)
				{
					if (parameters[i].IsUserType)
					{
						throw new NotSupportedException("User defined subclasses of System.Type are not yet supported.");
					}
				}
			}
			SignatureHelper signatureHelper = new SignatureHelper((ModuleBuilder)mod, SignatureHelper.SignatureHelperType.HELPER_METHOD);
			signatureHelper.returnType = returnType;
			signatureHelper.callConv = callingConvention;
			signatureHelper.unmanagedCallConv = unmanagedCallingConvention;
			if (parameters != null)
			{
				signatureHelper.arguments = new Type[parameters.Length];
				for (int j = 0; j < parameters.Length; j++)
				{
					signatureHelper.arguments[j] = parameters[j];
				}
			}
			return signatureHelper;
		}

		// Token: 0x04000FAE RID: 4014
		private ModuleBuilder module;

		// Token: 0x04000FAF RID: 4015
		private Type[] arguments;

		// Token: 0x04000FB0 RID: 4016
		private SignatureHelper.SignatureHelperType type;

		// Token: 0x04000FB1 RID: 4017
		private Type returnType;

		// Token: 0x04000FB2 RID: 4018
		private CallingConventions callConv;

		// Token: 0x04000FB3 RID: 4019
		private CallingConvention unmanagedCallConv;

		// Token: 0x04000FB4 RID: 4020
		private Type[][] modreqs;

		// Token: 0x04000FB5 RID: 4021
		private Type[][] modopts;

		// Token: 0x020002FB RID: 763
		internal enum SignatureHelperType
		{
			// Token: 0x04000FB7 RID: 4023
			HELPER_FIELD,
			// Token: 0x04000FB8 RID: 4024
			HELPER_LOCAL,
			// Token: 0x04000FB9 RID: 4025
			HELPER_METHOD,
			// Token: 0x04000FBA RID: 4026
			HELPER_PROPERTY
		}
	}
}
