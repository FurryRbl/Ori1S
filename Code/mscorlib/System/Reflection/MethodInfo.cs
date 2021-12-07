using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Discovers the attributes of a method and provides access to method metadata.</summary>
	// Token: 0x0200029D RID: 669
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_MethodInfo))]
	[ClassInterface(ClassInterfaceType.None)]
	[Serializable]
	public abstract class MethodInfo : MethodBase, _MethodInfo
	{
		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array that receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060021D9 RID: 8665 RVA: 0x0007AD10 File Offset: 0x00078F10
		void _MethodInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060021DA RID: 8666 RVA: 0x0007AD18 File Offset: 0x00078F18
		void _MethodInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060021DB RID: 8667 RVA: 0x0007AD20 File Offset: 0x00078F20
		void _MethodInfo.GetTypeInfoCount(out uint pcTInfo)
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
		// Token: 0x060021DC RID: 8668 RVA: 0x0007AD28 File Offset: 0x00078F28
		void _MethodInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, returns the MethodInfo object for the method on the direct or indirect base class in which the method represented by this instance was first declared.</summary>
		/// <returns>A MethodInfo object for the first implementation of this method.</returns>
		// Token: 0x060021DD RID: 8669
		public abstract MethodInfo GetBaseDefinition();

		/// <summary>Gets a <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is a method.</summary>
		/// <returns>A <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is a method.</returns>
		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x060021DE RID: 8670 RVA: 0x0007AD30 File Offset: 0x00078F30
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Method;
			}
		}

		/// <summary>Gets the return type of this method.</summary>
		/// <returns>The return type of this method.</returns>
		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x060021DF RID: 8671 RVA: 0x0007AD34 File Offset: 0x00078F34
		public virtual Type ReturnType
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the custom attributes for the return type.</summary>
		/// <returns>An ICustomAttributeProvider object representing the custom attributes for the return type.</returns>
		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x060021E0 RID: 8672
		public abstract ICustomAttributeProvider ReturnTypeCustomAttributes { get; }

		/// <summary>Returns a <see cref="T:System.Reflection.MethodInfo" /> object that represents a generic method definition from which the current method can be constructed.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object representing a generic method definition from which the current method can be constructed.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current method is not a generic method. That is, <see cref="P:System.Reflection.MethodInfo.IsGenericMethod" /> returns false. </exception>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x060021E1 RID: 8673 RVA: 0x0007AD38 File Offset: 0x00078F38
		[ComVisible(true)]
		public virtual MethodInfo GetGenericMethodDefinition()
		{
			throw new NotSupportedException();
		}

		/// <summary>Substitutes the elements of an array of types for the type parameters of the current generic method definition, and returns a <see cref="T:System.Reflection.MethodInfo" /> object representing the resulting constructed method.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object that represents the constructed method formed by substituting the elements of <paramref name="typeArguments" /> for the type parameters of the current generic method definition.</returns>
		/// <param name="typeArguments">An array of types to be substituted for the type parameters of the current generic method definition.</param>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Reflection.MethodInfo" /> does not represent a generic method definition. That is, <see cref="P:System.Reflection.MethodInfo.IsGenericMethodDefinition" /> returns false.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeArguments" /> is null.-or- Any element of <paramref name="typeArguments" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in <paramref name="typeArguments" /> is not the same as the number of type parameters of the current generic method definition.-or- An element of <paramref name="typeArguments" /> does not satisfy the constraints specified for the corresponding type parameter of the current generic method definition. </exception>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x060021E2 RID: 8674 RVA: 0x0007AD40 File Offset: 0x00078F40
		public virtual MethodInfo MakeGenericMethod(params Type[] typeArguments)
		{
			throw new NotSupportedException(base.GetType().ToString());
		}

		/// <summary>Returns an array of <see cref="T:System.Type" /> objects that represent the type arguments of a generic method or the type parameters of a generic method definition.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects that represent the type arguments of a generic method or the type parameters of a generic method definition. Returns an empty array if the current method is not a generic method.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x060021E3 RID: 8675 RVA: 0x0007AD54 File Offset: 0x00078F54
		[ComVisible(true)]
		public override Type[] GetGenericArguments()
		{
			return Type.EmptyTypes;
		}

		/// <summary>Gets a value indicating whether the current method is a generic method.</summary>
		/// <returns>true if the current method is a generic method; otherwise, false.</returns>
		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x060021E4 RID: 8676 RVA: 0x0007AD5C File Offset: 0x00078F5C
		public override bool IsGenericMethod
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Reflection.MethodInfo" /> represents the definition of a generic method.</summary>
		/// <returns>true if the <see cref="T:System.Reflection.MethodInfo" /> object represents the definition of a generic method; otherwise, false.</returns>
		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x060021E5 RID: 8677 RVA: 0x0007AD60 File Offset: 0x00078F60
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether a generic method contains unassigned generic type parameters.</summary>
		/// <returns>true if the current <see cref="T:System.Reflection.MethodInfo" /> contains unassigned generic type parameters; otherwise, false.</returns>
		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x060021E6 RID: 8678 RVA: 0x0007AD64 File Offset: 0x00078F64
		public override bool ContainsGenericParameters
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a <see cref="T:System.Reflection.ParameterInfo" /> object that contains information about the return type of the method, such as whether the return type has custom modifiers. </summary>
		/// <returns>A <see cref="T:System.Reflection.ParameterInfo" /> object that contains information about the return type.</returns>
		/// <exception cref="T:System.NotImplementedException">This method is not implemented.</exception>
		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x060021E7 RID: 8679 RVA: 0x0007AD68 File Offset: 0x00078F68
		public virtual ParameterInfo ReturnParameter
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Provides access to the <see cref="M:System.Object.GetType" /> method from COM.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Reflection.MethodInfo" /> type.</returns>
		// Token: 0x060021E8 RID: 8680 RVA: 0x0007AD70 File Offset: 0x00078F70
		virtual Type GetType()
		{
			return base.GetType();
		}
	}
}
