using System;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.Reflection.Emit
{
	/// <summary>Defines and represents a method (or constructor) on a dynamic class.</summary>
	// Token: 0x020002E7 RID: 743
	[ComDefaultInterface(typeof(_MethodBuilder))]
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	public sealed class MethodBuilder : MethodInfo, _MethodBuilder
	{
		// Token: 0x060025DB RID: 9691 RVA: 0x00085F08 File Offset: 0x00084108
		internal MethodBuilder(TypeBuilder tb, string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnModReq, Type[] returnModOpt, Type[] parameterTypes, Type[][] paramModReq, Type[][] paramModOpt)
		{
			this.name = name;
			this.attrs = attributes;
			this.call_conv = callingConvention;
			this.rtype = returnType;
			this.returnModReq = returnModReq;
			this.returnModOpt = returnModOpt;
			this.paramModReq = paramModReq;
			this.paramModOpt = paramModOpt;
			if ((attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope)
			{
				this.call_conv |= CallingConventions.HasThis;
			}
			if (parameterTypes != null)
			{
				for (int i = 0; i < parameterTypes.Length; i++)
				{
					if (parameterTypes[i] == null)
					{
						throw new ArgumentException("Elements of the parameterTypes array cannot be null", "parameterTypes");
					}
				}
				this.parameters = new Type[parameterTypes.Length];
				Array.Copy(parameterTypes, this.parameters, parameterTypes.Length);
			}
			this.type = tb;
			this.table_idx = this.get_next_table_index(this, 6, true);
			((ModuleBuilder)tb.Module).RegisterToken(this, this.GetToken().Token);
		}

		// Token: 0x060025DC RID: 9692 RVA: 0x00086004 File Offset: 0x00084204
		internal MethodBuilder(TypeBuilder tb, string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnModReq, Type[] returnModOpt, Type[] parameterTypes, Type[][] paramModReq, Type[][] paramModOpt, string dllName, string entryName, CallingConvention nativeCConv, CharSet nativeCharset) : this(tb, name, attributes, callingConvention, returnType, returnModReq, returnModOpt, parameterTypes, paramModReq, paramModOpt)
		{
			this.pi_dll = dllName;
			this.pi_entry = entryName;
			this.native_cc = nativeCConv;
			this.charset = nativeCharset;
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array that receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060025DD RID: 9693 RVA: 0x00086048 File Offset: 0x00084248
		void _MethodBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060025DE RID: 9694 RVA: 0x00086050 File Offset: 0x00084250
		void _MethodBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060025DF RID: 9695 RVA: 0x00086058 File Offset: 0x00084258
		void _MethodBuilder.GetTypeInfoCount(out uint pcTInfo)
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
		// Token: 0x060025E0 RID: 9696 RVA: 0x00086060 File Offset: 0x00084260
		void _MethodBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Not supported for this type.</summary>
		/// <returns>Not supported.</returns>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class.</exception>
		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x060025E1 RID: 9697 RVA: 0x00086068 File Offset: 0x00084268
		public override bool ContainsGenericParameters
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets or sets a Boolean value that specifies whether the local variables in this method are zero initialized. The default value of this property is true.</summary>
		/// <returns>true if the local variables in this method should be zero initialized; otherwise false.</returns>
		/// <exception cref="T:System.InvalidOperationException">For the current method, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is true, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is false. (Get or set.)</exception>
		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x060025E2 RID: 9698 RVA: 0x00086070 File Offset: 0x00084270
		// (set) Token: 0x060025E3 RID: 9699 RVA: 0x00086078 File Offset: 0x00084278
		public bool InitLocals
		{
			get
			{
				return this.init_locals;
			}
			set
			{
				this.init_locals = value;
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x060025E4 RID: 9700 RVA: 0x00086084 File Offset: 0x00084284
		internal TypeBuilder TypeBuilder
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Retrieves the internal handle for the method. Use this handle to access the underlying metadata handle.</summary>
		/// <returns>Read-only. The internal handle for the method. Use this handle to access the underlying metadata handle.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. Retrieve the method using <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> and call <see cref="P:System.Reflection.MethodBase.MethodHandle" /> on the returned <see cref="T:System.Reflection.MethodInfo" />. </exception>
		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x060025E5 RID: 9701 RVA: 0x0008608C File Offset: 0x0008428C
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				throw this.NotSupported();
			}
		}

		/// <summary>Gets the return type of the method represented by this <see cref="T:System.Reflection.Emit.MethodBuilder" />.</summary>
		/// <returns>The return type of the method.</returns>
		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x060025E6 RID: 9702 RVA: 0x00086094 File Offset: 0x00084294
		public override Type ReturnType
		{
			get
			{
				return this.rtype;
			}
		}

		/// <summary>Retrieves the class that was used in reflection to obtain this object.</summary>
		/// <returns>Read-only. The type used to obtain this method.</returns>
		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x060025E7 RID: 9703 RVA: 0x0008609C File Offset: 0x0008429C
		public override Type ReflectedType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Returns the type that declares this method.</summary>
		/// <returns>Read-only. The type that declares this method.</returns>
		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x060025E8 RID: 9704 RVA: 0x000860A4 File Offset: 0x000842A4
		public override Type DeclaringType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Retrieves the name of this method.</summary>
		/// <returns>Read-only. Retrieves a string containing the simple name of this method.</returns>
		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x060025E9 RID: 9705 RVA: 0x000860AC File Offset: 0x000842AC
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Retrieves the attributes for this method.</summary>
		/// <returns>Read-only. Retrieves the MethodAttributes for this method.</returns>
		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x060025EA RID: 9706 RVA: 0x000860B4 File Offset: 0x000842B4
		public override MethodAttributes Attributes
		{
			get
			{
				return this.attrs;
			}
		}

		/// <summary>Returns the custom attributes of the method's return type.</summary>
		/// <returns>Read-only. The custom attributes of the method's return type.</returns>
		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x060025EB RID: 9707 RVA: 0x000860BC File Offset: 0x000842BC
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				return null;
			}
		}

		/// <summary>Returns the calling convention of the method.</summary>
		/// <returns>Read-only. The calling convention of the method.</returns>
		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x060025EC RID: 9708 RVA: 0x000860C0 File Offset: 0x000842C0
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.call_conv;
			}
		}

		/// <summary>Retrieves the signature of the method.</summary>
		/// <returns>Read-only. A String containing the signature of the method reflected by this MethodBase instance.</returns>
		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x060025ED RID: 9709 RVA: 0x000860C8 File Offset: 0x000842C8
		[MonoTODO("Not implemented")]
		public string Signature
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (set) Token: 0x060025EE RID: 9710 RVA: 0x000860D0 File Offset: 0x000842D0
		internal bool BestFitMapping
		{
			set
			{
				this.extra_flags = (uint)(((ulong)this.extra_flags & 18446744073709551567UL) | ((!value) ? 32UL : 16UL));
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (set) Token: 0x060025EF RID: 9711 RVA: 0x000860F8 File Offset: 0x000842F8
		internal bool ThrowOnUnmappableChar
		{
			set
			{
				this.extra_flags = (uint)(((ulong)this.extra_flags & 18446744073709539327UL) | ((!value) ? 8192UL : 4096UL));
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (set) Token: 0x060025F0 RID: 9712 RVA: 0x00086134 File Offset: 0x00084334
		internal bool ExactSpelling
		{
			set
			{
				this.extra_flags = (uint)(((ulong)this.extra_flags & 18446744073709551614UL) | ((!value) ? 0UL : 1UL));
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (set) Token: 0x060025F1 RID: 9713 RVA: 0x00086158 File Offset: 0x00084358
		internal bool SetLastError
		{
			set
			{
				this.extra_flags = (uint)(((ulong)this.extra_flags & 18446744073709551551UL) | ((!value) ? 0UL : 64UL));
			}
		}

		/// <summary>Returns the MethodToken that represents the token for this method.</summary>
		/// <returns>Returns the MethodToken of this method.</returns>
		// Token: 0x060025F2 RID: 9714 RVA: 0x00086188 File Offset: 0x00084388
		public MethodToken GetToken()
		{
			return new MethodToken(100663296 | this.table_idx);
		}

		/// <summary>Return the base implementation for a method.</summary>
		/// <returns>The base implementation of this method.</returns>
		// Token: 0x060025F3 RID: 9715 RVA: 0x0008619C File Offset: 0x0008439C
		public override MethodInfo GetBaseDefinition()
		{
			return this;
		}

		/// <summary>Returns the implementation flags for the method.</summary>
		/// <returns>Returns the implementation flags for the method.</returns>
		// Token: 0x060025F4 RID: 9716 RVA: 0x000861A0 File Offset: 0x000843A0
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.iattrs;
		}

		/// <summary>Returns the parameters of this method.</summary>
		/// <returns>An array of ParameterInfo objects that represent the parameters of the method.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. Retrieve the method using <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> and call GetParameters on the returned <see cref="T:System.Reflection.MethodInfo" />. </exception>
		// Token: 0x060025F5 RID: 9717 RVA: 0x000861A8 File Offset: 0x000843A8
		public override ParameterInfo[] GetParameters()
		{
			if (!this.type.is_created)
			{
				throw this.NotSupported();
			}
			if (this.parameters == null)
			{
				return null;
			}
			ParameterInfo[] array = new ParameterInfo[this.parameters.Length];
			for (int i = 0; i < this.parameters.Length; i++)
			{
				array[i] = new ParameterInfo((this.pinfo != null) ? this.pinfo[i + 1] : null, this.parameters[i], this, i + 1);
			}
			return array;
		}

		// Token: 0x060025F6 RID: 9718 RVA: 0x00086230 File Offset: 0x00084430
		internal override int GetParameterCount()
		{
			if (this.parameters == null)
			{
				return 0;
			}
			return this.parameters.Length;
		}

		/// <summary>Returns a reference to the module that contains this method.</summary>
		/// <returns>Returns a reference to the module that contains this method.</returns>
		// Token: 0x060025F7 RID: 9719 RVA: 0x00086248 File Offset: 0x00084448
		public Module GetModule()
		{
			return this.type.Module;
		}

		/// <summary>Creates the body of the method using a supplied byte array of Microsoft intermediate language (MSIL) instructions.</summary>
		/// <param name="il">An array containing valid MSIL instructions. If this parameter is null, the method's body is cleared. </param>
		/// <param name="count">The number of valid bytes in the MSIL array. This value is ignored if MSIL is null. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="count" /> is not within the range of indexes of the supplied MSIL instruction array and <paramref name="il" /> is not null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The containing type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or- This method was called previously on this MethodBuilder with an <paramref name="il" /> argument that was not null.-or-For the current method, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is true, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is false. </exception>
		// Token: 0x060025F8 RID: 9720 RVA: 0x00086258 File Offset: 0x00084458
		public void CreateMethodBody(byte[] il, int count)
		{
			if (il != null && (count < 0 || count > il.Length))
			{
				throw new ArgumentOutOfRangeException("Index was out of range.  Must be non-negative and less than the size of the collection.");
			}
			if (this.code != null || this.type.is_created)
			{
				throw new InvalidOperationException("Type definition of the method is complete.");
			}
			if (il == null)
			{
				this.code = null;
			}
			else
			{
				this.code = new byte[count];
				Array.Copy(il, this.code, count);
			}
		}

		/// <summary>Dynamically invokes the method reflected by this instance on the given object, passing along the specified parameters, and under the constraints of the given binder.</summary>
		/// <returns>Returns an object containing the return value of the invoked method.</returns>
		/// <param name="obj">The object on which to invoke the specified method. If the method is static, this parameter is ignored. </param>
		/// <param name="invokeAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> : InvokeMethod, NonPublic, and so on. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of MemberInfo objects via reflection. If binder is null, the default binder is used. For more details, see <see cref="T:System.Reflection.Binder" />. </param>
		/// <param name="parameters">An argument list. This is an array of arguments with the same number, order, and type as the parameters of the method to be invoked. If there are no parameters this should be null. </param>
		/// <param name="culture">An instance of <see cref="T:System.Globalization.CultureInfo" /> used to govern the coercion of types. If this is null, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used. (Note that this is necessary to, for example, convert a <see cref="T:System.String" /> that represents 1000 to a <see cref="T:System.Double" /> value, since 1000 is represented differently by different cultures.) </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. Retrieve the method using <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> and call <see cref="M:System.Type.InvokeMember(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object,System.Object[],System.Reflection.ParameterModifier[],System.Globalization.CultureInfo,System.String[])" /> on the returned <see cref="T:System.Reflection.MethodInfo" />. </exception>
		// Token: 0x060025F9 RID: 9721 RVA: 0x000862D8 File Offset: 0x000844D8
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw this.NotSupported();
		}

		/// <summary>Checks if the specified custom attribute type is defined.</summary>
		/// <returns>true if the specified custom attribute type is defined; otherwise, false.</returns>
		/// <param name="attributeType">The custom attribute type. </param>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the custom attributes. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. Retrieve the method using <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> and call <see cref="M:System.Reflection.MemberInfo.IsDefined(System.Type,System.Boolean)" /> on the returned <see cref="T:System.Reflection.MethodInfo" />. </exception>
		// Token: 0x060025FA RID: 9722 RVA: 0x000862E0 File Offset: 0x000844E0
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw this.NotSupported();
		}

		/// <summary>Returns all the custom attributes defined for this method.</summary>
		/// <returns>Returns an array of objects representing all the custom attributes of this method.</returns>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the custom attributes. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. Retrieve the method using <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> and call <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Boolean)" /> on the returned <see cref="T:System.Reflection.MethodInfo" />. </exception>
		// Token: 0x060025FB RID: 9723 RVA: 0x000862E8 File Offset: 0x000844E8
		public override object[] GetCustomAttributes(bool inherit)
		{
			if (this.type.is_created)
			{
				return MonoCustomAttrs.GetCustomAttributes(this, inherit);
			}
			throw this.NotSupported();
		}

		/// <summary>Returns the custom attributes identified by the given type.</summary>
		/// <returns>Returns an array of objects representing the attributes of this method that are of type <paramref name="attributeType" />.</returns>
		/// <param name="attributeType">The custom attribute type. </param>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the custom attributes. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. Retrieve the method using <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> and call <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Boolean)" /> on the returned <see cref="T:System.Reflection.MethodInfo" />. </exception>
		// Token: 0x060025FC RID: 9724 RVA: 0x00086308 File Offset: 0x00084508
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (this.type.is_created)
			{
				return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
			}
			throw this.NotSupported();
		}

		/// <summary>Returns an ILGenerator for this method with a default Microsoft intermediate language (MSIL) stream size of 64 bytes.</summary>
		/// <returns>Returns an ILGenerator object for this method.</returns>
		/// <exception cref="T:System.InvalidOperationException">The method should not have a body because of its <see cref="T:System.Reflection.MethodAttributes" /> or <see cref="T:System.Reflection.MethodImplAttributes" /> flags, for example because it has the <see cref="F:System.Reflection.MethodAttributes.PinvokeImpl" /> flag. -or-The method is a generic method, but not a generic method definition. That is, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is true, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is false. </exception>
		// Token: 0x060025FD RID: 9725 RVA: 0x0008632C File Offset: 0x0008452C
		public ILGenerator GetILGenerator()
		{
			return this.GetILGenerator(64);
		}

		/// <summary>Returns an ILGenerator for this method with the specified Microsoft intermediate language (MSIL) stream size.</summary>
		/// <returns>Returns an ILGenerator object for this method.</returns>
		/// <param name="size">The size of the MSIL stream, in bytes. </param>
		/// <exception cref="T:System.InvalidOperationException">The method should not have a body because of its <see cref="T:System.Reflection.MethodAttributes" /> or <see cref="T:System.Reflection.MethodImplAttributes" /> flags, for example because it has the <see cref="F:System.Reflection.MethodAttributes.PinvokeImpl" /> flag. -or-The method is a generic method, but not a generic method definition. That is, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is true, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is false.   </exception>
		// Token: 0x060025FE RID: 9726 RVA: 0x00086338 File Offset: 0x00084538
		public ILGenerator GetILGenerator(int size)
		{
			if ((this.iattrs & MethodImplAttributes.CodeTypeMask) != MethodImplAttributes.IL || (this.iattrs & MethodImplAttributes.ManagedMask) != MethodImplAttributes.IL)
			{
				throw new InvalidOperationException("Method body should not exist.");
			}
			if (this.ilgen != null)
			{
				return this.ilgen;
			}
			this.ilgen = new ILGenerator(this.type.Module, ((ModuleBuilder)this.type.Module).GetTokenGenerator(), size);
			return this.ilgen;
		}

		/// <summary>Sets the parameter attributes and the name of a parameter of this method, or of the return value of this method. Returns a ParameterBuilder that can be used to apply custom attributes.</summary>
		/// <returns>Returns a ParameterBuilder object that represents a parameter of this method or the return value of this method.</returns>
		/// <param name="position">The position of the parameter in the parameter list. Parameters are indexed beginning with the number 1 for the first parameter; the number 0 represents the return value of the method. </param>
		/// <param name="attributes">The parameter attributes of the parameter. </param>
		/// <param name="strParamName">The name of the parameter. The name can be the null string. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The method has no parameters.-or- <paramref name="position" /> is less than zero.-or- <paramref name="position" /> is greater than the number of the method's parameters. </exception>
		/// <exception cref="T:System.InvalidOperationException">The containing type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or-For the current method, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is true, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is false. </exception>
		// Token: 0x060025FF RID: 9727 RVA: 0x000863B0 File Offset: 0x000845B0
		public ParameterBuilder DefineParameter(int position, ParameterAttributes attributes, string strParamName)
		{
			this.RejectIfCreated();
			if (position < 0 || position > this.parameters.Length)
			{
				throw new ArgumentOutOfRangeException("position");
			}
			ParameterBuilder parameterBuilder = new ParameterBuilder(this, position, attributes, strParamName);
			if (this.pinfo == null)
			{
				this.pinfo = new ParameterBuilder[this.parameters.Length + 1];
			}
			this.pinfo[position] = parameterBuilder;
			return parameterBuilder;
		}

		// Token: 0x06002600 RID: 9728 RVA: 0x00086418 File Offset: 0x00084618
		internal void check_override()
		{
			if (this.override_method != null && this.override_method.IsVirtual && !this.IsVirtual)
			{
				throw new TypeLoadException(string.Format("Method '{0}' override '{1}' but it is not virtual", this.name, this.override_method));
			}
		}

		// Token: 0x06002601 RID: 9729 RVA: 0x00086468 File Offset: 0x00084668
		internal void fixup()
		{
			if ((this.attrs & (MethodAttributes.Abstract | MethodAttributes.PinvokeImpl)) == MethodAttributes.PrivateScope && (this.iattrs & (MethodImplAttributes)4099) == MethodImplAttributes.IL && (this.ilgen == null || ILGenerator.Mono_GetCurrentOffset(this.ilgen) == 0) && (this.code == null || this.code.Length == 0))
			{
				throw new InvalidOperationException(string.Format("Method '{0}.{1}' does not have a method body.", this.DeclaringType.FullName, this.Name));
			}
			if (this.ilgen != null)
			{
				this.ilgen.label_fixup();
			}
		}

		// Token: 0x06002602 RID: 9730 RVA: 0x00086504 File Offset: 0x00084704
		internal void GenerateDebugInfo(ISymbolWriter symbolWriter)
		{
			if (this.ilgen != null && this.ilgen.HasDebugInfo)
			{
				SymbolToken symbolToken = new SymbolToken(this.GetToken().Token);
				symbolWriter.OpenMethod(symbolToken);
				symbolWriter.SetSymAttribute(symbolToken, "__name", Encoding.UTF8.GetBytes(this.Name));
				this.ilgen.GenerateDebugInfo(symbolWriter);
				symbolWriter.CloseMethod();
			}
		}

		/// <summary>Sets a custom attribute using a custom attribute builder.</summary>
		/// <param name="customBuilder">An instance of a helper class to describe the custom attribute. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="customBuilder" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">For the current method, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is true, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is false.</exception>
		// Token: 0x06002603 RID: 9731 RVA: 0x00086578 File Offset: 0x00084778
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			string fullName = customBuilder.Ctor.ReflectedType.FullName;
			switch (fullName)
			{
			case "System.Runtime.CompilerServices.MethodImplAttribute":
			{
				byte[] data = customBuilder.Data;
				int num2 = (int)data[2];
				num2 |= (int)data[3] << 8;
				this.iattrs |= (MethodImplAttributes)num2;
				return;
			}
			case "System.Runtime.InteropServices.DllImportAttribute":
			{
				CustomAttributeBuilder.CustomAttributeInfo customAttributeInfo = CustomAttributeBuilder.decode_cattr(customBuilder);
				bool flag = true;
				this.pi_dll = (string)customAttributeInfo.ctorArgs[0];
				if (this.pi_dll == null || this.pi_dll.Length == 0)
				{
					throw new ArgumentException("DllName cannot be empty");
				}
				this.native_cc = System.Runtime.InteropServices.CallingConvention.Winapi;
				for (int i = 0; i < customAttributeInfo.namedParamNames.Length; i++)
				{
					string a = customAttributeInfo.namedParamNames[i];
					object obj = customAttributeInfo.namedParamValues[i];
					if (a == "CallingConvention")
					{
						this.native_cc = (CallingConvention)((int)obj);
					}
					else if (a == "CharSet")
					{
						this.charset = (CharSet)((int)obj);
					}
					else if (a == "EntryPoint")
					{
						this.pi_entry = (string)obj;
					}
					else if (a == "ExactSpelling")
					{
						this.ExactSpelling = (bool)obj;
					}
					else if (a == "SetLastError")
					{
						this.SetLastError = (bool)obj;
					}
					else if (a == "PreserveSig")
					{
						flag = (bool)obj;
					}
					else if (a == "BestFitMapping")
					{
						this.BestFitMapping = (bool)obj;
					}
					else if (a == "ThrowOnUnmappableChar")
					{
						this.ThrowOnUnmappableChar = (bool)obj;
					}
				}
				this.attrs |= MethodAttributes.PinvokeImpl;
				if (flag)
				{
					this.iattrs |= MethodImplAttributes.PreserveSig;
				}
				return;
			}
			case "System.Runtime.InteropServices.PreserveSigAttribute":
				this.iattrs |= MethodImplAttributes.PreserveSig;
				return;
			case "System.Runtime.CompilerServices.SpecialNameAttribute":
				this.attrs |= MethodAttributes.SpecialName;
				return;
			case "System.Security.SuppressUnmanagedCodeSecurityAttribute":
				this.attrs |= MethodAttributes.HasSecurity;
				break;
			}
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

		/// <summary>Sets a custom attribute using a specified custom attribute blob.</summary>
		/// <param name="con">The constructor for the custom attribute. </param>
		/// <param name="binaryAttribute">A byte blob representing the attributes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> or <paramref name="binaryAttribute" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">For the current method, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is true, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is false.</exception>
		// Token: 0x06002604 RID: 9732 RVA: 0x000868AC File Offset: 0x00084AAC
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (binaryAttribute == null)
			{
				throw new ArgumentNullException("binaryAttribute");
			}
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		/// <summary>Sets the implementation flags for this method.</summary>
		/// <param name="attributes">The implementation flags to set. </param>
		/// <exception cref="T:System.InvalidOperationException">The containing type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or-For the current method, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is true, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is false. </exception>
		// Token: 0x06002605 RID: 9733 RVA: 0x000868E0 File Offset: 0x00084AE0
		public void SetImplementationFlags(MethodImplAttributes attributes)
		{
			this.RejectIfCreated();
			this.iattrs = attributes;
		}

		/// <summary>Adds declarative security to this method.</summary>
		/// <param name="action">The security action to be taken (Demand, Assert, and so on). </param>
		/// <param name="pset">The set of permissions the action applies to. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="action" /> is invalid (RequestMinimum, RequestOptional, and RequestRefuse are invalid). </exception>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or-The permission set <paramref name="pset" /> contains an action that was added earlier by <see cref="M:System.Reflection.Emit.MethodBuilder.AddDeclarativeSecurity(System.Security.Permissions.SecurityAction,System.Security.PermissionSet)" />.-or-For the current method, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is true, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is false. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pset" /> is null. </exception>
		// Token: 0x06002606 RID: 9734 RVA: 0x000868F0 File Offset: 0x00084AF0
		public void AddDeclarativeSecurity(SecurityAction action, PermissionSet pset)
		{
			if (pset == null)
			{
				throw new ArgumentNullException("pset");
			}
			if (action == SecurityAction.RequestMinimum || action == SecurityAction.RequestOptional || action == SecurityAction.RequestRefuse)
			{
				throw new ArgumentOutOfRangeException("Request* values are not permitted", "action");
			}
			this.RejectIfCreated();
			if (this.permissions != null)
			{
				foreach (RefEmitPermissionSet refEmitPermissionSet in this.permissions)
				{
					if (refEmitPermissionSet.action == action)
					{
						throw new InvalidOperationException("Multiple permission sets specified with the same SecurityAction.");
					}
				}
				RefEmitPermissionSet[] array2 = new RefEmitPermissionSet[this.permissions.Length + 1];
				this.permissions.CopyTo(array2, 0);
				this.permissions = array2;
			}
			else
			{
				this.permissions = new RefEmitPermissionSet[1];
			}
			this.permissions[this.permissions.Length - 1] = new RefEmitPermissionSet(action, pset.ToXml().ToString());
			this.attrs |= MethodAttributes.HasSecurity;
		}

		/// <summary>Sets marshaling information for the return type of this method.</summary>
		/// <param name="unmanagedMarshal">Marshaling information for the return type of this method. </param>
		/// <exception cref="T:System.InvalidOperationException">The containing type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or-For the current method, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is true, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is false. </exception>
		// Token: 0x06002607 RID: 9735 RVA: 0x000869F8 File Offset: 0x00084BF8
		[Obsolete("An alternate API is available: Emit the MarshalAs custom attribute instead.")]
		public void SetMarshal(UnmanagedMarshal unmanagedMarshal)
		{
			this.RejectIfCreated();
			throw new NotImplementedException();
		}

		/// <summary>Set a symbolic custom attribute using a blob.</summary>
		/// <param name="name">The name of the symbolic custom attribute. </param>
		/// <param name="data">The byte blob that represents the value of the symbolic custom attribute. </param>
		/// <exception cref="T:System.InvalidOperationException">The containing type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or- The module that contains this method is not a debug module. -or-For the current method, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is true, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is false.</exception>
		// Token: 0x06002608 RID: 9736 RVA: 0x00086A08 File Offset: 0x00084C08
		[MonoTODO]
		public void SetSymCustomAttribute(string name, byte[] data)
		{
			this.RejectIfCreated();
			throw new NotImplementedException();
		}

		/// <summary>Returns this MethodBuilder instance as a string.</summary>
		/// <returns>Returns a string containing the name, attributes, method signature, exceptions, and local signature of this method followed by the current Microsoft intermediate language (MSIL) stream.</returns>
		// Token: 0x06002609 RID: 9737 RVA: 0x00086A18 File Offset: 0x00084C18
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"MethodBuilder [",
				this.type.Name,
				"::",
				this.name,
				"]"
			});
		}

		/// <summary>Determines whether the given object is equal to this instance.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of MethodBuilder and is equal to this object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with this MethodBuilder instance. </param>
		// Token: 0x0600260A RID: 9738 RVA: 0x00086A60 File Offset: 0x00084C60
		[MonoTODO]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		/// <summary>Gets the hash code for this method.</summary>
		/// <returns>The hash code for this method.</returns>
		// Token: 0x0600260B RID: 9739 RVA: 0x00086A6C File Offset: 0x00084C6C
		public override int GetHashCode()
		{
			return this.name.GetHashCode();
		}

		// Token: 0x0600260C RID: 9740 RVA: 0x00086A7C File Offset: 0x00084C7C
		internal override int get_next_table_index(object obj, int table, bool inc)
		{
			return this.type.get_next_table_index(obj, table, inc);
		}

		// Token: 0x0600260D RID: 9741 RVA: 0x00086A8C File Offset: 0x00084C8C
		internal void set_override(MethodInfo mdecl)
		{
			this.override_method = mdecl;
		}

		// Token: 0x0600260E RID: 9742 RVA: 0x00086A98 File Offset: 0x00084C98
		private void RejectIfCreated()
		{
			if (this.type.is_created)
			{
				throw new InvalidOperationException("Type definition of the method is complete.");
			}
		}

		// Token: 0x0600260F RID: 9743 RVA: 0x00086AB8 File Offset: 0x00084CB8
		private Exception NotSupported()
		{
			return new NotSupportedException("The invoked member is not supported in a dynamic module.");
		}

		/// <summary>Returns a generic method constructed from the current generic method definition using the specified generic type arguments.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> representing the generic method constructed from the current generic method definition using the specified generic type arguments.</returns>
		/// <param name="typeArguments">An array of <see cref="T:System.Type" /> objects that represent the type arguments for the generic method.</param>
		// Token: 0x06002610 RID: 9744
		[MethodImpl(MethodImplOptions.InternalCall)]
		public override extern MethodInfo MakeGenericMethod(params Type[] typeArguments);

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Reflection.Emit.MethodBuilder" /> object represents the definition of a generic method.</summary>
		/// <returns>true if the current <see cref="T:System.Reflection.Emit.MethodBuilder" /> object represents the definition of a generic method; otherwise, false.</returns>
		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06002611 RID: 9745 RVA: 0x00086AC4 File Offset: 0x00084CC4
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return this.generic_params != null;
			}
		}

		/// <summary>Gets a value indicating whether the method is a generic method.</summary>
		/// <returns>true if the method is generic; otherwise, false.</returns>
		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06002612 RID: 9746 RVA: 0x00086AD4 File Offset: 0x00084CD4
		public override bool IsGenericMethod
		{
			get
			{
				return this.generic_params != null;
			}
		}

		/// <summary>Returns this method.</summary>
		/// <returns>The current instance of <see cref="T:System.Reflection.Emit.MethodBuilder" />. </returns>
		/// <exception cref="T:System.InvalidOperationException">The current method is not generic. That is, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property returns false.</exception>
		// Token: 0x06002613 RID: 9747 RVA: 0x00086AE4 File Offset: 0x00084CE4
		public override MethodInfo GetGenericMethodDefinition()
		{
			if (!this.IsGenericMethodDefinition)
			{
				throw new InvalidOperationException();
			}
			return this;
		}

		/// <summary>Returns an array of <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> objects that represent the type parameters of the method, if it is generic.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> objects representing the type parameters, if the method is generic, or null if the method is not generic. </returns>
		// Token: 0x06002614 RID: 9748 RVA: 0x00086AF8 File Offset: 0x00084CF8
		public override Type[] GetGenericArguments()
		{
			if (this.generic_params == null)
			{
				return Type.EmptyTypes;
			}
			Type[] array = new Type[this.generic_params.Length];
			for (int i = 0; i < this.generic_params.Length; i++)
			{
				array[i] = this.generic_params[i];
			}
			return array;
		}

		/// <summary>Sets the number of generic type parameters for the current method, specifies their names, and returns an array of <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> objects that can be used to define their constraints.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> objects representing the type parameters of the generic method.</returns>
		/// <param name="names">An array of strings that represent the names of the generic type parameters.</param>
		/// <exception cref="T:System.InvalidOperationException">Generic type parameters have already been defined for this method.-or-The method has been completed already.-or-The <see cref="M:System.Reflection.Emit.MethodBuilder.SetImplementationFlags(System.Reflection.MethodImplAttributes)" /> method has been called for the current method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="names" /> is null.-or-An element of <paramref name="names" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="names" /> is an empty array.</exception>
		// Token: 0x06002615 RID: 9749 RVA: 0x00086B4C File Offset: 0x00084D4C
		public GenericTypeParameterBuilder[] DefineGenericParameters(params string[] names)
		{
			if (names == null)
			{
				throw new ArgumentNullException("names");
			}
			if (names.Length == 0)
			{
				throw new ArgumentException("names");
			}
			this.generic_params = new GenericTypeParameterBuilder[names.Length];
			for (int i = 0; i < names.Length; i++)
			{
				string text = names[i];
				if (text == null)
				{
					throw new ArgumentNullException("names");
				}
				this.generic_params[i] = new GenericTypeParameterBuilder(this.type, this, text, i);
			}
			return this.generic_params;
		}

		/// <summary>Sets the return type of the method.</summary>
		/// <param name="returnType">A <see cref="T:System.Type" /> object that represents the return type of the method.</param>
		/// <exception cref="T:System.InvalidOperationException">The current method is generic, but is not a generic method definition. That is, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is true, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is false.</exception>
		// Token: 0x06002616 RID: 9750 RVA: 0x00086BD0 File Offset: 0x00084DD0
		public void SetReturnType(Type returnType)
		{
			this.rtype = returnType;
		}

		/// <summary>Sets the number and types of parameters for a method. </summary>
		/// <param name="parameterTypes">An array of <see cref="T:System.Type" /> objects representing the parameter types.</param>
		/// <exception cref="T:System.InvalidOperationException">The current method is generic, but is not a generic method definition. That is, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is true, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is false.</exception>
		// Token: 0x06002617 RID: 9751 RVA: 0x00086BDC File Offset: 0x00084DDC
		public void SetParameters(params Type[] parameterTypes)
		{
			if (parameterTypes != null)
			{
				for (int i = 0; i < parameterTypes.Length; i++)
				{
					if (parameterTypes[i] == null)
					{
						throw new ArgumentException("Elements of the parameterTypes array cannot be null", "parameterTypes");
					}
				}
				this.parameters = new Type[parameterTypes.Length];
				Array.Copy(parameterTypes, this.parameters, parameterTypes.Length);
			}
		}

		/// <summary>Sets the method signature, including the return type, the parameter types, and the required and optional custom modifiers of the return type and parameter types.</summary>
		/// <param name="returnType">The return type of the method.</param>
		/// <param name="returnTypeRequiredCustomModifiers">An array of types representing the required custom modifiers, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />, for the return type of the method. If the return type has no required custom modifiers, specify null.</param>
		/// <param name="returnTypeOptionalCustomModifiers">An array of types representing the optional custom modifiers, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />, for the return type of the method. If the return type has no optional custom modifiers, specify null.</param>
		/// <param name="parameterTypes">The types of the parameters of the method.</param>
		/// <param name="parameterTypeRequiredCustomModifiers">An array of arrays of types. Each array of types represents the required custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no required custom modifiers, specify null instead of an array of types. If none of the parameters have required custom modifiers, specify null instead of an array of arrays.</param>
		/// <param name="parameterTypeOptionalCustomModifiers">An array of arrays of types. Each array of types represents the optional custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no optional custom modifiers, specify null instead of an array of types. If none of the parameters have optional custom modifiers, specify null instead of an array of arrays.</param>
		/// <exception cref="T:System.InvalidOperationException">The current method is generic, but is not a generic method definition. That is, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is true, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is false.</exception>
		// Token: 0x06002618 RID: 9752 RVA: 0x00086C38 File Offset: 0x00084E38
		public void SetSignature(Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
		{
			this.SetReturnType(returnType);
			this.SetParameters(parameterTypes);
			this.returnModReq = returnTypeRequiredCustomModifiers;
			this.returnModOpt = returnTypeOptionalCustomModifiers;
			this.paramModReq = parameterTypeRequiredCustomModifiers;
			this.paramModOpt = parameterTypeOptionalCustomModifiers;
		}

		/// <summary>Gets the module in which the current method is being defined.</summary>
		/// <returns>The <see cref="T:System.Reflection.Module" /> in which the member represented by the current <see cref="T:System.Reflection.MemberInfo" /> is being defined.</returns>
		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06002619 RID: 9753 RVA: 0x00086C68 File Offset: 0x00084E68
		public override Module Module
		{
			get
			{
				return base.Module;
			}
		}

		// Token: 0x04000E3E RID: 3646
		private RuntimeMethodHandle mhandle;

		// Token: 0x04000E3F RID: 3647
		private Type rtype;

		// Token: 0x04000E40 RID: 3648
		internal Type[] parameters;

		// Token: 0x04000E41 RID: 3649
		private MethodAttributes attrs;

		// Token: 0x04000E42 RID: 3650
		private MethodImplAttributes iattrs;

		// Token: 0x04000E43 RID: 3651
		private string name;

		// Token: 0x04000E44 RID: 3652
		private int table_idx;

		// Token: 0x04000E45 RID: 3653
		private byte[] code;

		// Token: 0x04000E46 RID: 3654
		private ILGenerator ilgen;

		// Token: 0x04000E47 RID: 3655
		private TypeBuilder type;

		// Token: 0x04000E48 RID: 3656
		internal ParameterBuilder[] pinfo;

		// Token: 0x04000E49 RID: 3657
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x04000E4A RID: 3658
		private MethodInfo override_method;

		// Token: 0x04000E4B RID: 3659
		private string pi_dll;

		// Token: 0x04000E4C RID: 3660
		private string pi_entry;

		// Token: 0x04000E4D RID: 3661
		private CharSet charset;

		// Token: 0x04000E4E RID: 3662
		private uint extra_flags;

		// Token: 0x04000E4F RID: 3663
		private CallingConvention native_cc;

		// Token: 0x04000E50 RID: 3664
		private CallingConventions call_conv;

		// Token: 0x04000E51 RID: 3665
		private bool init_locals = true;

		// Token: 0x04000E52 RID: 3666
		private IntPtr generic_container;

		// Token: 0x04000E53 RID: 3667
		internal GenericTypeParameterBuilder[] generic_params;

		// Token: 0x04000E54 RID: 3668
		private Type[] returnModReq;

		// Token: 0x04000E55 RID: 3669
		private Type[] returnModOpt;

		// Token: 0x04000E56 RID: 3670
		private Type[][] paramModReq;

		// Token: 0x04000E57 RID: 3671
		private Type[][] paramModOpt;

		// Token: 0x04000E58 RID: 3672
		private RefEmitPermissionSet[] permissions;
	}
}
