using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Defines and creates generic type parameters for dynamically defined generic types and methods. This class cannot be inherited. </summary>
	// Token: 0x020002DB RID: 731
	[ComVisible(true)]
	public sealed class GenericTypeParameterBuilder : Type
	{
		// Token: 0x0600252C RID: 9516 RVA: 0x00083680 File Offset: 0x00081880
		internal GenericTypeParameterBuilder(TypeBuilder tbuilder, MethodBuilder mbuilder, string name, int index)
		{
			this.tbuilder = tbuilder;
			this.mbuilder = mbuilder;
			this.name = name;
			this.index = index;
			this.initialize();
		}

		/// <summary>Sets the base type that a type must inherit in order to be substituted for the type parameter.</summary>
		/// <param name="baseTypeConstraint">The <see cref="T:System.Type" /> that must be inherited by any type that is to be substituted for the type parameter.</param>
		// Token: 0x0600252D RID: 9517 RVA: 0x000836AC File Offset: 0x000818AC
		public void SetBaseTypeConstraint(Type baseTypeConstraint)
		{
			this.base_type = (baseTypeConstraint ?? typeof(object));
		}

		/// <summary>Sets the interfaces a type must implement in order to be substituted for the type parameter. </summary>
		/// <param name="interfaceConstraints">An array of <see cref="T:System.Type" /> objects that represent the interfaces a type must implement in order to be substituted for the type parameter.</param>
		// Token: 0x0600252E RID: 9518 RVA: 0x000836C8 File Offset: 0x000818C8
		[ComVisible(true)]
		public void SetInterfaceConstraints(params Type[] interfaceConstraints)
		{
			this.iface_constraints = interfaceConstraints;
		}

		/// <summary>Sets the variance characteristics and special constraints of the generic parameter, such as the parameterless constructor constraint.</summary>
		/// <param name="genericParameterAttributes">A bitwise combination of <see cref="T:System.Reflection.GenericParameterAttributes" /> values that represent the variance characteristics and special constraints of the generic type parameter.</param>
		// Token: 0x0600252F RID: 9519 RVA: 0x000836D4 File Offset: 0x000818D4
		public void SetGenericParameterAttributes(GenericParameterAttributes genericParameterAttributes)
		{
			this.attrs = genericParameterAttributes;
		}

		// Token: 0x06002530 RID: 9520
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void initialize();

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="c">Not supported.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06002531 RID: 9521 RVA: 0x000836E0 File Offset: 0x000818E0
		[ComVisible(true)]
		public override bool IsSubclassOf(Type c)
		{
			if (!((ModuleBuilder)this.tbuilder.Module).assemblyb.IsCompilerContext)
			{
				throw this.not_supported();
			}
			return this.BaseType != null && (this.BaseType == c || this.BaseType.IsSubclassOf(c));
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x0008373C File Offset: 0x0008193C
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			if (((ModuleBuilder)this.tbuilder.Module).assemblyb.IsCompilerContext)
			{
				return TypeAttributes.Public;
			}
			throw this.not_supported();
		}

		// Token: 0x06002533 RID: 9523 RVA: 0x00083768 File Offset: 0x00081968
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="bindingAttr">Not supported.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases. </exception>
		// Token: 0x06002534 RID: 9524 RVA: 0x00083770 File Offset: 0x00081970
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="name">Not supported.</param>
		/// <param name="bindingAttr">Not supported. </param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06002535 RID: 9525 RVA: 0x00083778 File Offset: 0x00081978
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06002536 RID: 9526 RVA: 0x00083780 File Offset: 0x00081980
		public override EventInfo[] GetEvents()
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="bindingAttr">Not supported.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06002537 RID: 9527 RVA: 0x00083788 File Offset: 0x00081988
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="name">Not supported.</param>
		/// <param name="bindingAttr">Not supported.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06002538 RID: 9528 RVA: 0x00083790 File Offset: 0x00081990
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="bindingAttr">Not supported.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06002539 RID: 9529 RVA: 0x00083798 File Offset: 0x00081998
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="name">The name of the interface.</param>
		/// <param name="ignoreCase">true to search without regard for case; false to make a case-sensitive search.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x0600253A RID: 9530 RVA: 0x000837A0 File Offset: 0x000819A0
		public override Type GetInterface(string name, bool ignoreCase)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x0600253B RID: 9531 RVA: 0x000837A8 File Offset: 0x000819A8
		public override Type[] GetInterfaces()
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="bindingAttr">Not supported.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x0600253C RID: 9532 RVA: 0x000837B0 File Offset: 0x000819B0
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="name">Not supported.</param>
		/// <param name="type">Not supported.</param>
		/// <param name="bindingAttr">Not supported.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x0600253D RID: 9533 RVA: 0x000837B8 File Offset: 0x000819B8
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="bindingAttr">Not supported.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x0600253E RID: 9534 RVA: 0x000837C0 File Offset: 0x000819C0
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		// Token: 0x0600253F RID: 9535 RVA: 0x000837C8 File Offset: 0x000819C8
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="name">Not supported.</param>
		/// <param name="bindingAttr">Not supported.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06002540 RID: 9536 RVA: 0x000837D0 File Offset: 0x000819D0
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="bindingAttr">Not supported.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06002541 RID: 9537 RVA: 0x000837D8 File Offset: 0x000819D8
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="bindingAttr">Not supported.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06002542 RID: 9538 RVA: 0x000837E0 File Offset: 0x000819E0
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		// Token: 0x06002543 RID: 9539 RVA: 0x000837E8 File Offset: 0x000819E8
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw this.not_supported();
		}

		// Token: 0x06002544 RID: 9540 RVA: 0x000837F0 File Offset: 0x000819F0
		protected override bool HasElementTypeImpl()
		{
			return false;
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="c">Not supported.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06002545 RID: 9541 RVA: 0x000837F4 File Offset: 0x000819F4
		public override bool IsAssignableFrom(Type c)
		{
			throw this.not_supported();
		}

		// Token: 0x06002546 RID: 9542 RVA: 0x000837FC File Offset: 0x000819FC
		public override bool IsInstanceOfType(object o)
		{
			throw this.not_supported();
		}

		// Token: 0x06002547 RID: 9543 RVA: 0x00083804 File Offset: 0x00081A04
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06002548 RID: 9544 RVA: 0x00083808 File Offset: 0x00081A08
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x06002549 RID: 9545 RVA: 0x0008380C File Offset: 0x00081A0C
		protected override bool IsCOMObjectImpl()
		{
			return false;
		}

		// Token: 0x0600254A RID: 9546 RVA: 0x00083810 File Offset: 0x00081A10
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x0600254B RID: 9547 RVA: 0x00083814 File Offset: 0x00081A14
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x0600254C RID: 9548 RVA: 0x00083818 File Offset: 0x00081A18
		protected override bool IsValueTypeImpl()
		{
			return this.base_type != null && this.base_type.IsValueType;
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="name">Not supported. </param>
		/// <param name="invokeAttr">Not supported.</param>
		/// <param name="binder">Not supported.</param>
		/// <param name="target">Not supported.</param>
		/// <param name="args">Not supported.</param>
		/// <param name="modifiers">Not supported.</param>
		/// <param name="culture">Not supported.</param>
		/// <param name="namedParameters">Not supported.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x0600254D RID: 9549 RVA: 0x00083838 File Offset: 0x00081A38
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			throw this.not_supported();
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> in all cases. </summary>
		/// <returns>The type referred to by the current array type, pointer type, or ByRef type; or null if the current type is not an array type, is not a pointer type, and is not passed by reference.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x0600254E RID: 9550 RVA: 0x00083840 File Offset: 0x00081A40
		public override Type GetElementType()
		{
			throw this.not_supported();
		}

		/// <summary>Gets the current generic type parameter.</summary>
		/// <returns>The current <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> object.</returns>
		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x0600254F RID: 9551 RVA: 0x00083848 File Offset: 0x00081A48
		public override Type UnderlyingSystemType
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets an <see cref="T:System.Reflection.Assembly" /> object representing the dynamic assembly that contains the generic type definition the current type parameter belongs to.</summary>
		/// <returns>An <see cref="T:System.Reflection.Assembly" /> object representing the dynamic assembly that contains the generic type definition the current type parameter belongs to.</returns>
		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06002550 RID: 9552 RVA: 0x0008384C File Offset: 0x00081A4C
		public override Assembly Assembly
		{
			get
			{
				return this.tbuilder.Assembly;
			}
		}

		/// <summary>Gets null in all cases.</summary>
		/// <returns>A null reference (Nothing in Visual Basic) in all cases.</returns>
		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06002551 RID: 9553 RVA: 0x0008385C File Offset: 0x00081A5C
		public override string AssemblyQualifiedName
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the base type constraint of the current generic type parameter.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the base type constraint of the generic type parameter, or null if the type parameter has no base type constraint.</returns>
		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06002552 RID: 9554 RVA: 0x00083860 File Offset: 0x00081A60
		public override Type BaseType
		{
			get
			{
				return this.base_type;
			}
		}

		/// <summary>Gets null in all cases.</summary>
		/// <returns>A null reference (Nothing in Visual Basic) in all cases.</returns>
		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06002553 RID: 9555 RVA: 0x00083868 File Offset: 0x00081A68
		public override string FullName
		{
			get
			{
				return null;
			}
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases. </exception>
		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06002554 RID: 9556 RVA: 0x0008386C File Offset: 0x00081A6C
		public override Guid GUID
		{
			get
			{
				throw this.not_supported();
			}
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="attributeType">Not supported.</param>
		/// <param name="inherit">Not supported.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06002555 RID: 9557 RVA: 0x00083874 File Offset: 0x00081A74
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06002556 RID: 9558 RVA: 0x0008387C File Offset: 0x00081A7C
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="attributeType">The type of attribute to search for. Only attributes that are assignable to this type are returned.</param>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06002557 RID: 9559 RVA: 0x00083884 File Offset: 0x00081A84
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="interfaceType">A <see cref="T:System.Type" /> object that represents the interface type for which the mapping is to be retrieved.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06002558 RID: 9560 RVA: 0x0008388C File Offset: 0x00081A8C
		[ComVisible(true)]
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			throw this.not_supported();
		}

		/// <summary>Gets the name of the generic type parameter.</summary>
		/// <returns>The name of the generic type parameter.</returns>
		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06002559 RID: 9561 RVA: 0x00083894 File Offset: 0x00081A94
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets null in all cases.</summary>
		/// <returns>A null reference (Nothing in Visual Basic) in all cases.</returns>
		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x0600255A RID: 9562 RVA: 0x0008389C File Offset: 0x00081A9C
		public override string Namespace
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the dynamic module that contains the generic type parameter.</summary>
		/// <returns>A <see cref="T:System.Reflection.Module" /> object that represents the dynamic module that contains the generic type parameter.</returns>
		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x0600255B RID: 9563 RVA: 0x000838A0 File Offset: 0x00081AA0
		public override Module Module
		{
			get
			{
				return this.tbuilder.Module;
			}
		}

		/// <summary>Gets the generic type definition or generic method definition to which the generic type parameter belongs.</summary>
		/// <returns>If the type parameter belongs to a generic type, a <see cref="T:System.Type" /> object representing that generic type; if the type parameter belongs to a generic method, a <see cref="T:System.Type" /> object representing that type that declared that generic method.</returns>
		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x0600255C RID: 9564 RVA: 0x000838B0 File Offset: 0x00081AB0
		public override Type DeclaringType
		{
			get
			{
				return (this.mbuilder == null) ? this.tbuilder : this.mbuilder.DeclaringType;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> object that was used to obtain the <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" />.</summary>
		/// <returns>The <see cref="T:System.Type" /> object that was used to obtain the <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" />.</returns>
		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x0600255D RID: 9565 RVA: 0x000838D4 File Offset: 0x00081AD4
		public override Type ReflectedType
		{
			get
			{
				return this.DeclaringType;
			}
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x0600255E RID: 9566 RVA: 0x000838DC File Offset: 0x00081ADC
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw this.not_supported();
			}
		}

		/// <summary>Not valid for generic type parameters.</summary>
		/// <returns>Not valid for generic type parameters.</returns>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		// Token: 0x0600255F RID: 9567 RVA: 0x000838E4 File Offset: 0x00081AE4
		public override Type[] GetGenericArguments()
		{
			throw new InvalidOperationException();
		}

		/// <summary>Not valid for generic type parameters.</summary>
		/// <returns>Not valid for generic type parameters.</returns>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		// Token: 0x06002560 RID: 9568 RVA: 0x000838EC File Offset: 0x00081AEC
		public override Type GetGenericTypeDefinition()
		{
			throw new InvalidOperationException();
		}

		/// <summary>Gets true in all cases.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06002561 RID: 9569 RVA: 0x000838F4 File Offset: 0x00081AF4
		public override bool ContainsGenericParameters
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets true in all cases.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06002562 RID: 9570 RVA: 0x000838F8 File Offset: 0x00081AF8
		public override bool IsGenericParameter
		{
			get
			{
				return true;
			}
		}

		/// <summary>Returns false in all cases.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06002563 RID: 9571 RVA: 0x000838FC File Offset: 0x00081AFC
		public override bool IsGenericType
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets false in all cases.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06002564 RID: 9572 RVA: 0x00083900 File Offset: 0x00081B00
		public override bool IsGenericTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06002565 RID: 9573 RVA: 0x00083904 File Offset: 0x00081B04
		public override GenericParameterAttributes GenericParameterAttributes
		{
			get
			{
				if (((ModuleBuilder)this.tbuilder.Module).assemblyb.IsCompilerContext)
				{
					return this.attrs;
				}
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets the position of the type parameter in the type parameter list of the generic type or method that declared the parameter.</summary>
		/// <returns>The position of the type parameter in the type parameter list of the generic type or method that declared the parameter.</returns>
		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06002566 RID: 9574 RVA: 0x00083934 File Offset: 0x00081B34
		public override int GenericParameterPosition
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x06002567 RID: 9575 RVA: 0x0008393C File Offset: 0x00081B3C
		public override Type[] GetGenericParameterConstraints()
		{
			if (!((ModuleBuilder)this.tbuilder.Module).assemblyb.IsCompilerContext)
			{
				throw new InvalidOperationException();
			}
			if (this.base_type == null)
			{
				if (this.iface_constraints != null)
				{
					return this.iface_constraints;
				}
				return Type.EmptyTypes;
			}
			else
			{
				if (this.iface_constraints == null)
				{
					return new Type[]
					{
						this.base_type
					};
				}
				Type[] array = new Type[this.iface_constraints.Length + 1];
				array[0] = this.base_type;
				this.iface_constraints.CopyTo(array, 1);
				return array;
			}
		}

		/// <summary>Gets a <see cref="T:System.Reflection.MethodInfo" /> that represents the declaring method, if the current <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> represents a type parameter of a generic method.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> that represents the declaring method, if the current <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> represents a type parameter of a generic method; otherwise, null.</returns>
		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06002568 RID: 9576 RVA: 0x000839D4 File Offset: 0x00081BD4
		public override MethodBase DeclaringMethod
		{
			get
			{
				return this.mbuilder;
			}
		}

		/// <summary>Set a custom attribute using a custom attribute builder.</summary>
		/// <param name="customBuilder">An instance of a helper class that defines the custom attribute.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="customBuilder" /> is null.</exception>
		// Token: 0x06002569 RID: 9577 RVA: 0x000839DC File Offset: 0x00081BDC
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
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
		/// <param name="con">The constructor for the custom attribute.</param>
		/// <param name="binaryAttribute">A byte blob representing the attribute.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> is null.-or-<paramref name="binaryAttribute" /> is a null reference.</exception>
		// Token: 0x0600256A RID: 9578 RVA: 0x00083A50 File Offset: 0x00081C50
		[MonoTODO("unverified implementation")]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		// Token: 0x0600256B RID: 9579 RVA: 0x00083A60 File Offset: 0x00081C60
		private Exception not_supported()
		{
			return new NotSupportedException();
		}

		/// <summary>Returns a string representation of the current generic type parameter.</summary>
		/// <returns>A string that contains the name of the generic type parameter.</returns>
		// Token: 0x0600256C RID: 9580 RVA: 0x00083A68 File Offset: 0x00081C68
		public override string ToString()
		{
			return this.name;
		}

		/// <summary>Tests whether the given object is an instance of EventToken and is equal to the current instance.</summary>
		/// <returns>Returns true if <paramref name="o" /> is an instance of EventToken and equals the current instance; otherwise, false.</returns>
		/// <param name="o">The object to be compared with the current instance.</param>
		// Token: 0x0600256D RID: 9581 RVA: 0x00083A70 File Offset: 0x00081C70
		[MonoTODO]
		public override bool Equals(object o)
		{
			return base.Equals(o);
		}

		/// <summary>Returns a 32-bit integer hash code for the current instance.</summary>
		/// <returns>A 32-bit integer hash code.</returns>
		// Token: 0x0600256E RID: 9582 RVA: 0x00083A7C File Offset: 0x00081C7C
		[MonoTODO]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Returns the type of a one-dimensional array whose element type is the generic type parameter.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the type of a one-dimensional array whose element type is the generic type parameter.</returns>
		// Token: 0x0600256F RID: 9583 RVA: 0x00083A84 File Offset: 0x00081C84
		public override Type MakeArrayType()
		{
			return new ArrayType(this, 0);
		}

		/// <summary>Returns the type of an array whose element type is the generic type parameter, with the specified number of dimensions.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the type of an array whose element type is the generic type parameter, with the specified number of dimensions.</returns>
		/// <param name="rank">The number of dimensions for the array.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="rank" /> is not a valid number of dimensions. For example, its value is less than 1.</exception>
		// Token: 0x06002570 RID: 9584 RVA: 0x00083A90 File Offset: 0x00081C90
		public override Type MakeArrayType(int rank)
		{
			if (rank < 1)
			{
				throw new IndexOutOfRangeException();
			}
			return new ArrayType(this, rank);
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents the current generic type parameter when passed as a reference parameter.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the current generic type parameter when passed as a reference parameter.</returns>
		// Token: 0x06002571 RID: 9585 RVA: 0x00083AA8 File Offset: 0x00081CA8
		public override Type MakeByRefType()
		{
			return new ByRefType(this);
		}

		/// <summary>Not valid for incomplete generic type parameters.</summary>
		/// <returns>This method is invalid for incomplete generic type parameters.</returns>
		/// <param name="typeArguments">An array of type arguments.</param>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		// Token: 0x06002572 RID: 9586 RVA: 0x00083AB0 File Offset: 0x00081CB0
		[MonoTODO]
		public override Type MakeGenericType(params Type[] typeArguments)
		{
			return base.MakeGenericType(typeArguments);
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents a pointer to the current generic type parameter.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents a pointer to the current generic type parameter.</returns>
		// Token: 0x06002573 RID: 9587 RVA: 0x00083ABC File Offset: 0x00081CBC
		public override Type MakePointerType()
		{
			return new PointerType(this);
		}

		// Token: 0x04000DFD RID: 3581
		private TypeBuilder tbuilder;

		// Token: 0x04000DFE RID: 3582
		private MethodBuilder mbuilder;

		// Token: 0x04000DFF RID: 3583
		private string name;

		// Token: 0x04000E00 RID: 3584
		private int index;

		// Token: 0x04000E01 RID: 3585
		private Type base_type;

		// Token: 0x04000E02 RID: 3586
		private Type[] iface_constraints;

		// Token: 0x04000E03 RID: 3587
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x04000E04 RID: 3588
		private GenericParameterAttributes attrs;
	}
}
