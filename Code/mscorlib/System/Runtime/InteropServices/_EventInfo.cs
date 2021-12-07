using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	/// <summary>Exposes the public members of the <see cref="T:System.Reflection.EventInfo" /> class to unmanaged code.</summary>
	// Token: 0x02000357 RID: 855
	[TypeLibImportClass(typeof(EventInfo))]
	[CLSCompliant(false)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("9DE59C64-D889-35A1-B897-587D74469E5B")]
	[ComVisible(true)]
	public interface _EventInfo
	{
		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.EventInfo.AddEventHandler(System.Object,System.Delegate)" /> method.</summary>
		/// <param name="target">The event source. </param>
		/// <param name="handler">A method or methods to be invoked when the event is raised by the target. </param>
		// Token: 0x06002934 RID: 10548
		void AddEventHandler(object target, Delegate handler);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Object.Equals(System.Object)" /> method.</summary>
		/// <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.</returns>
		/// <param name="other">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />.</param>
		// Token: 0x06002935 RID: 10549
		bool Equals(object other);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.EventInfo.GetAddMethod" /> method.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object representing the method used to add an event-handler delegate to the event source.</returns>
		// Token: 0x06002936 RID: 10550
		MethodInfo GetAddMethod();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.EventInfo.GetAddMethod(System.Boolean)" /> method.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object representing the method used to add an event-handler delegate to the event source.</returns>
		/// <param name="nonPublic">true to return non-public methods; otherwise, false.</param>
		// Token: 0x06002937 RID: 10551
		MethodInfo GetAddMethod(bool nonPublic);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Boolean)" /> method.</summary>
		/// <returns>An array that contains all the custom attributes, or an array with zero (0) elements if no attributes are defined.</returns>
		/// <param name="inherit">true to search a member's inheritance chain to find the attributes; otherwise, false.</param>
		// Token: 0x06002938 RID: 10552
		object[] GetCustomAttributes(bool inherit);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Type,System.Boolean)" /> method.</summary>
		/// <returns>An array of custom attributes applied to this member, or an array with zero (0) elements if no attributes have been applied.</returns>
		/// <param name="attributeType">The type of attribute to search for. Only attributes that are assignable to this type are returned. </param>
		/// <param name="inherit">true to search this member's inheritance chain to find the attributes; otherwise, false. </param>
		// Token: 0x06002939 RID: 10553
		object[] GetCustomAttributes(Type attributeType, bool inherit);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Object.GetHashCode" /> method.</summary>
		/// <returns>The hash code for the current instance.</returns>
		// Token: 0x0600293A RID: 10554
		int GetHashCode();

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">An array of names to be mapped.</param>
		/// <param name="cNames">The count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">An array allocated by the caller that receives the identifiers corresponding to the names.</param>
		// Token: 0x0600293B RID: 10555
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		/// <summary>Retrieves the type information for an object, which can be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">A pointer to the requested type information object.</param>
		// Token: 0x0600293C RID: 10556
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">When this method returns, contains a pointer to a location that receives the number of type information interfaces provided by the object. This parameter is passed uninitialized.</param>
		// Token: 0x0600293D RID: 10557
		void GetTypeInfoCount(out uint pcTInfo);

		/// <summary>Provides access to properties and methods exposed by an object.</summary>
		/// <param name="dispIdMember">An identifier for the member.</param>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="lcid">The locale context in which to interpret arguments.</param>
		/// <param name="wFlags">Flags describing the context of the call.</param>
		/// <param name="pDispParams">A pointer to a structure containing an array of arguments, an array of argument DISPIDs for named arguments, and counts for the number of elements in the arrays.</param>
		/// <param name="pVarResult">A pointer to the location where the result will be stored.</param>
		/// <param name="pExcepInfo">A pointer to a structure that contains exception information.</param>
		/// <param name="puArgErr">The index of the first argument that has an error.</param>
		// Token: 0x0600293E RID: 10558
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.EventInfo.GetRaiseMethod" /> method.</summary>
		/// <returns>The method that is called when the event is raised.</returns>
		// Token: 0x0600293F RID: 10559
		MethodInfo GetRaiseMethod();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.EventInfo.GetRaiseMethod(System.Boolean)" /> method.</summary>
		/// <returns>The <see cref="T:System.Reflection.MethodInfo" /> object that was called when the event was raised.</returns>
		/// <param name="nonPublic">true to return non-public methods; otherwise, false.</param>
		// Token: 0x06002940 RID: 10560
		MethodInfo GetRaiseMethod(bool nonPublic);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.EventInfo.GetRemoveMethod" /> method.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object representing the method used to remove an event-handler delegate from the event source.</returns>
		// Token: 0x06002941 RID: 10561
		MethodInfo GetRemoveMethod();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.EventInfo.GetRemoveMethod(System.Boolean)" /> method.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object representing the method used to remove an event-handler delegate from the event source.</returns>
		/// <param name="nonPublic">true to return non-public methods; otherwise, false.</param>
		// Token: 0x06002942 RID: 10562
		MethodInfo GetRemoveMethod(bool nonPublic);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Object.GetType" /> method.</summary>
		/// <returns>A <see cref="T:System.Type" /> object.</returns>
		// Token: 0x06002943 RID: 10563
		Type GetType();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.MemberInfo.IsDefined(System.Type,System.Boolean)" /> method.</summary>
		/// <returns>true if one or more instance of the <paramref name="attributeType" /> parameter is applied to this member; otherwise, false.</returns>
		/// <param name="attributeType">The Type object to which the custom attributes are applied. </param>
		/// <param name="inherit">true to search this member's inheritance chain to find the attributes; otherwise, false. </param>
		// Token: 0x06002944 RID: 10564
		bool IsDefined(Type attributeType, bool inherit);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.EventInfo.RemoveEventHandler(System.Object,System.Delegate)" /> method.</summary>
		/// <param name="target">The event source. </param>
		/// <param name="handler">The delegate to be disassociated from the events raised by target. </param>
		// Token: 0x06002945 RID: 10565
		void RemoveEventHandler(object target, Delegate handler);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Object.ToString" /> method.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Object" />.</returns>
		// Token: 0x06002946 RID: 10566
		string ToString();

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.EventInfo.Attributes" /> property.</summary>
		/// <returns>The read-only attributes for this event.</returns>
		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06002947 RID: 10567
		EventAttributes Attributes { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MemberInfo.DeclaringType" /> property.</summary>
		/// <returns>The Type object for the class that declares this member.</returns>
		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06002948 RID: 10568
		Type DeclaringType { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.EventInfo.EventHandlerType" /> property.</summary>
		/// <returns>A read-only <see cref="T:System.Type" /> object representing the delegate event handler.</returns>
		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06002949 RID: 10569
		Type EventHandlerType { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.EventInfo.IsMulticast" /> property.</summary>
		/// <returns>true if the delegate is an instance of a multicast delegate; otherwise, false.</returns>
		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x0600294A RID: 10570
		bool IsMulticast { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.EventInfo.IsSpecialName" /> property.</summary>
		/// <returns>true if this event has a special name; otherwise, false.</returns>
		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x0600294B RID: 10571
		bool IsSpecialName { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.EventInfo.MemberType" /> property.</summary>
		/// <returns>A <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is an event.</returns>
		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x0600294C RID: 10572
		MemberTypes MemberType { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MemberInfo.Name" /> property.</summary>
		/// <returns>A <see cref="T:System.String" /> object containing the name of this member.</returns>
		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x0600294D RID: 10573
		string Name { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MemberInfo.ReflectedType" /> property.</summary>
		/// <returns>The <see cref="T:System.Type" /> object that was used to obtain this object.</returns>
		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x0600294E RID: 10574
		Type ReflectedType { get; }
	}
}
