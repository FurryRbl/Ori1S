using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Defines events for a class.</summary>
	// Token: 0x020002D4 RID: 724
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_EventBuilder))]
	public sealed class EventBuilder : _EventBuilder
	{
		// Token: 0x060024D8 RID: 9432 RVA: 0x00082C64 File Offset: 0x00080E64
		internal EventBuilder(TypeBuilder tb, string eventName, EventAttributes eventAttrs, Type eventType)
		{
			this.name = eventName;
			this.attrs = eventAttrs;
			this.type = eventType;
			this.typeb = tb;
			this.table_idx = this.get_next_table_index(this, 20, true);
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x060024D9 RID: 9433 RVA: 0x00082C9C File Offset: 0x00080E9C
		void _EventBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x060024DA RID: 9434 RVA: 0x00082CA4 File Offset: 0x00080EA4
		void _EventBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x060024DB RID: 9435 RVA: 0x00082CAC File Offset: 0x00080EAC
		void _EventBuilder.GetTypeInfoCount(out uint pcTInfo)
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
		// Token: 0x060024DC RID: 9436 RVA: 0x00082CB4 File Offset: 0x00080EB4
		void _EventBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x00082CBC File Offset: 0x00080EBC
		internal int get_next_table_index(object obj, int table, bool inc)
		{
			return this.typeb.get_next_table_index(obj, table, inc);
		}

		/// <summary>Adds one of the "other" methods associated with this event. "Other" methods are methods other than the "on" and "raise" methods associated with an event. This function can be called many times to add as many "other" methods.</summary>
		/// <param name="mdBuilder">A MethodBuilder object that represents the other method. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="mdBuilder" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has been called on the enclosing type. </exception>
		// Token: 0x060024DE RID: 9438 RVA: 0x00082CCC File Offset: 0x00080ECC
		public void AddOtherMethod(MethodBuilder mdBuilder)
		{
			if (mdBuilder == null)
			{
				throw new ArgumentNullException("mdBuilder");
			}
			this.RejectIfCreated();
			if (this.other_methods != null)
			{
				MethodBuilder[] array = new MethodBuilder[this.other_methods.Length + 1];
				this.other_methods.CopyTo(array, 0);
				this.other_methods = array;
			}
			else
			{
				this.other_methods = new MethodBuilder[1];
			}
			this.other_methods[this.other_methods.Length - 1] = mdBuilder;
		}

		/// <summary>Returns the token for this event.</summary>
		/// <returns>Returns the EventToken for this event.</returns>
		// Token: 0x060024DF RID: 9439 RVA: 0x00082D44 File Offset: 0x00080F44
		public EventToken GetEventToken()
		{
			return new EventToken(335544320 | this.table_idx);
		}

		/// <summary>Sets the method used to subscribe to this event.</summary>
		/// <param name="mdBuilder">A MethodBuilder object that represents the method used to subscribe to this event. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="mdBuilder" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has been called on the enclosing type. </exception>
		// Token: 0x060024E0 RID: 9440 RVA: 0x00082D58 File Offset: 0x00080F58
		public void SetAddOnMethod(MethodBuilder mdBuilder)
		{
			if (mdBuilder == null)
			{
				throw new ArgumentNullException("mdBuilder");
			}
			this.RejectIfCreated();
			this.add_method = mdBuilder;
		}

		/// <summary>Sets the method used to raise this event.</summary>
		/// <param name="mdBuilder">A MethodBuilder object that represents the method used to raise this event. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="mdBuilder" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has been called on the enclosing type. </exception>
		// Token: 0x060024E1 RID: 9441 RVA: 0x00082D78 File Offset: 0x00080F78
		public void SetRaiseMethod(MethodBuilder mdBuilder)
		{
			if (mdBuilder == null)
			{
				throw new ArgumentNullException("mdBuilder");
			}
			this.RejectIfCreated();
			this.raise_method = mdBuilder;
		}

		/// <summary>Sets the method used to unsubscribe to this event.</summary>
		/// <param name="mdBuilder">A MethodBuilder object that represents the method used to unsubscribe to this event. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="mdBuilder" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has been called on the enclosing type. </exception>
		// Token: 0x060024E2 RID: 9442 RVA: 0x00082D98 File Offset: 0x00080F98
		public void SetRemoveOnMethod(MethodBuilder mdBuilder)
		{
			if (mdBuilder == null)
			{
				throw new ArgumentNullException("mdBuilder");
			}
			this.RejectIfCreated();
			this.remove_method = mdBuilder;
		}

		/// <summary>Sets a custom attribute using a custom attribute builder.</summary>
		/// <param name="customBuilder">An instance of a helper class to describe the custom attribute. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has been called on the enclosing type. </exception>
		// Token: 0x060024E3 RID: 9443 RVA: 0x00082DB8 File Offset: 0x00080FB8
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			this.RejectIfCreated();
			string fullName = customBuilder.Ctor.ReflectedType.FullName;
			if (fullName == "System.Runtime.CompilerServices.SpecialNameAttribute")
			{
				this.attrs |= EventAttributes.SpecialName;
				return;
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

		/// <summary>Set a custom attribute using a specified custom attribute blob.</summary>
		/// <param name="con">The constructor for the custom attribute. </param>
		/// <param name="binaryAttribute">A byte blob representing the attributes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> or <paramref name="binaryAttribute" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has been called on the enclosing type. </exception>
		// Token: 0x060024E4 RID: 9444 RVA: 0x00082E64 File Offset: 0x00081064
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

		// Token: 0x060024E5 RID: 9445 RVA: 0x00082E98 File Offset: 0x00081098
		private void RejectIfCreated()
		{
			if (this.typeb.is_created)
			{
				throw new InvalidOperationException("Type definition of the method is complete.");
			}
		}

		// Token: 0x04000DD4 RID: 3540
		internal string name;

		// Token: 0x04000DD5 RID: 3541
		private Type type;

		// Token: 0x04000DD6 RID: 3542
		private TypeBuilder typeb;

		// Token: 0x04000DD7 RID: 3543
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x04000DD8 RID: 3544
		internal MethodBuilder add_method;

		// Token: 0x04000DD9 RID: 3545
		internal MethodBuilder remove_method;

		// Token: 0x04000DDA RID: 3546
		internal MethodBuilder raise_method;

		// Token: 0x04000DDB RID: 3547
		internal MethodBuilder[] other_methods;

		// Token: 0x04000DDC RID: 3548
		internal EventAttributes attrs;

		// Token: 0x04000DDD RID: 3549
		private int table_idx;
	}
}
