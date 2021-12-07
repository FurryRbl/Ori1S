using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Discovers the attributes of a field and provides access to field metadata. </summary>
	// Token: 0x02000290 RID: 656
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_FieldInfo))]
	[ClassInterface(ClassInterfaceType.None)]
	[Serializable]
	public abstract class FieldInfo : MemberInfo, _FieldInfo
	{
		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06002170 RID: 8560 RVA: 0x0007A2CC File Offset: 0x000784CC
		void _FieldInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06002171 RID: 8561 RVA: 0x0007A2D4 File Offset: 0x000784D4
		void _FieldInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06002172 RID: 8562 RVA: 0x0007A2DC File Offset: 0x000784DC
		void _FieldInfo.GetTypeInfoCount(out uint pcTInfo)
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
		// Token: 0x06002173 RID: 8563 RVA: 0x0007A2E4 File Offset: 0x000784E4
		void _FieldInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the attributes associated with this field.</summary>
		/// <returns>The FieldAttributes for this field.</returns>
		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06002174 RID: 8564
		public abstract FieldAttributes Attributes { get; }

		/// <summary>Gets a RuntimeFieldHandle, which is a handle to the internal metadata representation of a field.</summary>
		/// <returns>A handle to the internal metadata representation of a field.</returns>
		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06002175 RID: 8565
		public abstract RuntimeFieldHandle FieldHandle { get; }

		/// <summary>Gets the type of this field object.</summary>
		/// <returns>The type of this field object.</returns>
		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06002176 RID: 8566
		public abstract Type FieldType { get; }

		/// <summary>When overridden in a derived class, returns the value of a field supported by a given object.</summary>
		/// <returns>An object containing the value of the field reflected by this instance.</returns>
		/// <param name="obj">The object whose field value will be returned. </param>
		/// <exception cref="T:System.Reflection.TargetException">The field is non-static and <paramref name="obj" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">A field is marked literal, but the field does not have one of the accepted literal types. </exception>
		/// <exception cref="T:System.FieldAccessException">The caller does not have permission to access this field. </exception>
		/// <exception cref="T:System.ArgumentException">The method is neither declared nor inherited by the class of <paramref name="obj" />. </exception>
		// Token: 0x06002177 RID: 8567
		public abstract object GetValue(object obj);

		/// <summary>Gets a <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is a field.</summary>
		/// <returns>A <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is a field.</returns>
		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06002178 RID: 8568 RVA: 0x0007A2EC File Offset: 0x000784EC
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Field;
			}
		}

		/// <summary>Gets a value indicating whether the value is written at compile time and cannot be changed.</summary>
		/// <returns>true if the field has the Literal attribute set; otherwise, false.</returns>
		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06002179 RID: 8569 RVA: 0x0007A2F0 File Offset: 0x000784F0
		public bool IsLiteral
		{
			get
			{
				return (this.Attributes & FieldAttributes.Literal) != FieldAttributes.PrivateScope;
			}
		}

		/// <summary>Gets a value indicating whether the field is static.</summary>
		/// <returns>true if this field is static; otherwise, false.</returns>
		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x0600217A RID: 8570 RVA: 0x0007A304 File Offset: 0x00078504
		public bool IsStatic
		{
			get
			{
				return (this.Attributes & FieldAttributes.Static) != FieldAttributes.PrivateScope;
			}
		}

		/// <summary>Gets a value indicating whether the field can only be set in the body of the constructor.</summary>
		/// <returns>true if the field has the InitOnly attribute set; otherwise, false.</returns>
		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x0600217B RID: 8571 RVA: 0x0007A318 File Offset: 0x00078518
		public bool IsInitOnly
		{
			get
			{
				return (this.Attributes & FieldAttributes.InitOnly) != FieldAttributes.PrivateScope;
			}
		}

		/// <summary>Gets a value indicating whether the field is public.</summary>
		/// <returns>true if this field is public; otherwise, false.</returns>
		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x0600217C RID: 8572 RVA: 0x0007A32C File Offset: 0x0007852C
		public bool IsPublic
		{
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Public;
			}
		}

		/// <summary>Gets a value indicating whether the field is private.</summary>
		/// <returns>true if the field is private; otherwise; false.</returns>
		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x0600217D RID: 8573 RVA: 0x0007A33C File Offset: 0x0007853C
		public bool IsPrivate
		{
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Private;
			}
		}

		/// <summary>Gets a value indicating whether the visibility of this field is described by <see cref="F:System.Reflection.FieldAttributes.Family" />; that is, the field is visible only within its class and derived classes.</summary>
		/// <returns>true if access to this field is exactly described by <see cref="F:System.Reflection.FieldAttributes.Family" />; otherwise, false.</returns>
		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x0600217E RID: 8574 RVA: 0x0007A34C File Offset: 0x0007854C
		public bool IsFamily
		{
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Family;
			}
		}

		/// <summary>Gets a value indicating whether the potential visibility of this field is described by <see cref="F:System.Reflection.FieldAttributes.Assembly" />; that is, the field is visible at most to other types in the same assembly, and is not visible to derived types outside the assembly.</summary>
		/// <returns>true if the visibility of this field is exactly described by <see cref="F:System.Reflection.FieldAttributes.Assembly" />; otherwise, false.</returns>
		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x0600217F RID: 8575 RVA: 0x0007A35C File Offset: 0x0007855C
		public bool IsAssembly
		{
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Assembly;
			}
		}

		/// <summary>Gets a value indicating whether the visibility of this field is described by <see cref="F:System.Reflection.FieldAttributes.FamANDAssem" />; that is, the field can be accessed from derived classes, but only if they are in the same assembly.</summary>
		/// <returns>true if access to this field is exactly described by <see cref="F:System.Reflection.FieldAttributes.FamANDAssem" />; otherwise, false.</returns>
		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06002180 RID: 8576 RVA: 0x0007A36C File Offset: 0x0007856C
		public bool IsFamilyAndAssembly
		{
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.FamANDAssem;
			}
		}

		/// <summary>Gets a value indicating whether the potential visibility of this field is described by <see cref="F:System.Reflection.FieldAttributes.FamORAssem" />; that is, the field can be accessed by derived classes wherever they are, and by classes in the same assembly.</summary>
		/// <returns>true if access to this field is exactly described by <see cref="F:System.Reflection.FieldAttributes.FamORAssem" />; otherwise, false.</returns>
		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06002181 RID: 8577 RVA: 0x0007A37C File Offset: 0x0007857C
		public bool IsFamilyOrAssembly
		{
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.FamORAssem;
			}
		}

		/// <summary>Gets a value indicating whether the corresponding PinvokeImpl attribute is set in <see cref="T:System.Reflection.FieldAttributes" />.</summary>
		/// <returns>true if the PinvokeImpl attribute is set in <see cref="T:System.Reflection.FieldAttributes" />; otherwise, false.</returns>
		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06002182 RID: 8578 RVA: 0x0007A38C File Offset: 0x0007858C
		public bool IsPinvokeImpl
		{
			get
			{
				return (this.Attributes & FieldAttributes.PinvokeImpl) == FieldAttributes.PinvokeImpl;
			}
		}

		/// <summary>Gets a value indicating whether the corresponding SpecialName attribute is set in the <see cref="T:System.Reflection.FieldAttributes" /> enumerator.</summary>
		/// <returns>true if the SpecialName attribute is set in <see cref="T:System.Reflection.FieldAttributes" />; otherwise, false.</returns>
		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06002183 RID: 8579 RVA: 0x0007A3A4 File Offset: 0x000785A4
		public bool IsSpecialName
		{
			get
			{
				return (this.Attributes & FieldAttributes.SpecialName) == FieldAttributes.SpecialName;
			}
		}

		/// <summary>Gets a value indicating whether this field has the NotSerialized attribute.</summary>
		/// <returns>true if the field has the NotSerialized attribute set; otherwise, false.</returns>
		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06002184 RID: 8580 RVA: 0x0007A3BC File Offset: 0x000785BC
		public bool IsNotSerialized
		{
			get
			{
				return (this.Attributes & FieldAttributes.NotSerialized) == FieldAttributes.NotSerialized;
			}
		}

		/// <summary>When overridden in a derived class, sets the value of the field supported by the given object.</summary>
		/// <param name="obj">The object whose field value will be set. </param>
		/// <param name="value">The value to assign to the field. </param>
		/// <param name="invokeAttr">A field of Binder that specifies the type of binding that is desired (for example, Binder.CreateInstance or Binder.ExactBinding). </param>
		/// <param name="binder">A set of properties that enables the binding, coercion of argument types, and invocation of members through reflection. If <paramref name="binder" /> is null, then Binder.DefaultBinding is used. </param>
		/// <param name="culture">The software preferences of a particular culture. </param>
		/// <exception cref="T:System.FieldAccessException">The caller does not have permission to access this field. </exception>
		/// <exception cref="T:System.Reflection.TargetException">The <paramref name="obj" /> parameter is null and the field is an instance field. </exception>
		/// <exception cref="T:System.ArgumentException">The field does not exist on the object.-or- The <paramref name="value" /> parameter cannot be converted and stored in the field. </exception>
		// Token: 0x06002185 RID: 8581
		public abstract void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture);

		/// <summary>Sets the value of the field supported by the given object.</summary>
		/// <param name="obj">The object whose field value will be set. </param>
		/// <param name="value">The value to assign to the field. </param>
		/// <exception cref="T:System.FieldAccessException">The caller does not have permission to access this field. </exception>
		/// <exception cref="T:System.Reflection.TargetException">The <paramref name="obj" /> parameter is null and the field is an instance field. </exception>
		/// <exception cref="T:System.ArgumentException">The field does not exist on the object.-or- The <paramref name="value" /> parameter cannot be converted and stored in the field. </exception>
		// Token: 0x06002186 RID: 8582 RVA: 0x0007A3D4 File Offset: 0x000785D4
		[DebuggerHidden]
		[DebuggerStepThrough]
		public void SetValue(object obj, object value)
		{
			this.SetValue(obj, value, BindingFlags.Default, null, null);
		}

		// Token: 0x06002187 RID: 8583
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern FieldInfo internal_from_handle_type(IntPtr field_handle, IntPtr type_handle);

		/// <summary>Gets a <see cref="T:System.Reflection.FieldInfo" /> for the field represented by the specified handle.</summary>
		/// <returns>A <see cref="T:System.Reflection.FieldInfo" /> object representing the field specified by <paramref name="handle" />.</returns>
		/// <param name="handle">A <see cref="T:System.RuntimeFieldHandle" /> structure that contains the handle to the internal metadata representation of a field. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="handle" /> is invalid.</exception>
		// Token: 0x06002188 RID: 8584 RVA: 0x0007A3E4 File Offset: 0x000785E4
		public static FieldInfo GetFieldFromHandle(RuntimeFieldHandle handle)
		{
			if (handle.Value == IntPtr.Zero)
			{
				throw new ArgumentException("The handle is invalid.");
			}
			return FieldInfo.internal_from_handle_type(handle.Value, IntPtr.Zero);
		}

		/// <summary>Gets a <see cref="T:System.Reflection.FieldInfo" /> for the field represented by the specified handle, for the specified generic type.</summary>
		/// <returns>A <see cref="T:System.Reflection.FieldInfo" /> object representing the field specified by <paramref name="handle" />, in the generic type specified by <paramref name="declaringType" />.</returns>
		/// <param name="handle">A <see cref="T:System.RuntimeFieldHandle" /> structure that contains the handle to the internal metadata representation of a field.</param>
		/// <param name="declaringType">A <see cref="T:System.RuntimeTypeHandle" /> structure that contains the handle to the generic type that defines the field.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="handle" /> is invalid.-or-<paramref name="declaringType" /> is not compatible with <paramref name="handle" />. For example, <paramref name="declaringType" /> is the runtime type handle of the generic type definition, and <paramref name="handle" /> comes from a constructed type. See Remarks. </exception>
		// Token: 0x06002189 RID: 8585 RVA: 0x0007A424 File Offset: 0x00078624
		[ComVisible(false)]
		public static FieldInfo GetFieldFromHandle(RuntimeFieldHandle handle, RuntimeTypeHandle declaringType)
		{
			if (handle.Value == IntPtr.Zero)
			{
				throw new ArgumentException("The handle is invalid.");
			}
			FieldInfo fieldInfo = FieldInfo.internal_from_handle_type(handle.Value, declaringType.Value);
			if (fieldInfo == null)
			{
				throw new ArgumentException("The field handle and the type handle are incompatible.");
			}
			return fieldInfo;
		}

		// Token: 0x0600218A RID: 8586 RVA: 0x0007A478 File Offset: 0x00078678
		internal virtual int GetFieldOffset()
		{
			throw new SystemException("This method should not be called");
		}

		/// <summary>Returns the value of a field supported by a given object.</summary>
		/// <returns>An Object containing a field value.</returns>
		/// <param name="obj">A <see cref="T:System.TypedReference" /> structure that encapsulates a managed pointer to a location and a runtime representation of the type that might be stored at that location. </param>
		/// <exception cref="T:System.NotSupportedException">The caller requires the Common Language Specification (CLS) alternative, but called this method instead. </exception>
		// Token: 0x0600218B RID: 8587 RVA: 0x0007A484 File Offset: 0x00078684
		[CLSCompliant(false)]
		[MonoTODO("Not implemented")]
		public virtual object GetValueDirect(TypedReference obj)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the value of the field supported by the given object.</summary>
		/// <param name="obj">A <see cref="T:System.TypedReference" /> structure that encapsulates a managed pointer to a location and a runtime representation of the type that can be stored at that location. </param>
		/// <param name="value">The value to assign to the field. </param>
		/// <exception cref="T:System.NotSupportedException">The caller requires the Common Language Specification (CLS) alternative, but called this method instead. </exception>
		// Token: 0x0600218C RID: 8588 RVA: 0x0007A48C File Offset: 0x0007868C
		[CLSCompliant(false)]
		[MonoTODO("Not implemented")]
		public virtual void SetValueDirect(TypedReference obj, object value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600218D RID: 8589
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern UnmanagedMarshal GetUnmanagedMarshal();

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x0600218E RID: 8590 RVA: 0x0007A494 File Offset: 0x00078694
		internal virtual UnmanagedMarshal UMarshal
		{
			get
			{
				return this.GetUnmanagedMarshal();
			}
		}

		// Token: 0x0600218F RID: 8591 RVA: 0x0007A49C File Offset: 0x0007869C
		internal object[] GetPseudoCustomAttributes()
		{
			int num = 0;
			if (this.IsNotSerialized)
			{
				num++;
			}
			if (this.DeclaringType.IsExplicitLayout)
			{
				num++;
			}
			UnmanagedMarshal umarshal = this.UMarshal;
			if (umarshal != null)
			{
				num++;
			}
			if (num == 0)
			{
				return null;
			}
			object[] array = new object[num];
			num = 0;
			if (this.IsNotSerialized)
			{
				array[num++] = new NonSerializedAttribute();
			}
			if (this.DeclaringType.IsExplicitLayout)
			{
				array[num++] = new FieldOffsetAttribute(this.GetFieldOffset());
			}
			if (umarshal != null)
			{
				array[num++] = umarshal.ToMarshalAsAttribute();
			}
			return array;
		}

		// Token: 0x06002190 RID: 8592
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Type[] GetTypeModifiers(bool optional);

		/// <summary>Gets an array of types that identify the optional custom modifiers of the field.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects that identify the optional custom modifiers of the current field, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />.</returns>
		// Token: 0x06002191 RID: 8593 RVA: 0x0007A540 File Offset: 0x00078740
		public virtual Type[] GetOptionalCustomModifiers()
		{
			Type[] typeModifiers = this.GetTypeModifiers(true);
			if (typeModifiers == null)
			{
				return Type.EmptyTypes;
			}
			return typeModifiers;
		}

		/// <summary>Gets an array of types that identify the required custom modifiers of the property.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects that identify the required custom modifiers of the current property, such as <see cref="T:System.Runtime.CompilerServices.IsConst" /> or <see cref="T:System.Runtime.CompilerServices.IsImplicitlyDereferenced" />.</returns>
		// Token: 0x06002192 RID: 8594 RVA: 0x0007A564 File Offset: 0x00078764
		public virtual Type[] GetRequiredCustomModifiers()
		{
			Type[] typeModifiers = this.GetTypeModifiers(false);
			if (typeModifiers == null)
			{
				return Type.EmptyTypes;
			}
			return typeModifiers;
		}

		/// <summary>Returns a literal value associated with the field by a compiler. </summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the literal value associated with the field. If the literal value is a class type with an element value of zero, the return value is null.</returns>
		/// <exception cref="T:System.InvalidOperationException">The Constant table in unmanaged metadata does not contain a constant value for the current property.</exception>
		/// <exception cref="T:System.FormatException">The type of the value is not one of the types permitted by the Common Language Specification (CLS). See the ECMA Partition II specification Metadata Logical Format: Other Structures, Element Types used in Signatures. </exception>
		// Token: 0x06002193 RID: 8595 RVA: 0x0007A588 File Offset: 0x00078788
		public virtual object GetRawConstantValue()
		{
			throw new NotSupportedException("This non-CLS method is not implemented.");
		}

		/// <summary>Gets a <see cref="T:System.Type" /> object representing the <see cref="T:System.Reflection.FieldInfo" /> type.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Reflection.FieldInfo" /> type.</returns>
		// Token: 0x06002194 RID: 8596 RVA: 0x0007A594 File Offset: 0x00078794
		virtual Type GetType()
		{
			return base.GetType();
		}
	}
}
