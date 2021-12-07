using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Provides the managed definition of the IConnectionPoint interface.</summary>
	// Token: 0x020003F3 RID: 1011
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("b196b286-bab4-101a-b69c-00aa00341d07")]
	[ComImport]
	public interface IConnectionPoint
	{
		/// <summary>Returns the IID of the outgoing interface managed by this connection point.</summary>
		/// <param name="pIID">When this parameter returns, contains the IID of the outgoing interface managed by this connection point. This parameter is passed uninitialized.</param>
		// Token: 0x06002C12 RID: 11282
		void GetConnectionInterface(out Guid pIID);

		/// <summary>Retrieves the IConnectionPointContainer interface pointer to the connectable object that conceptually owns this connection point.</summary>
		/// <param name="ppCPC">When this parameter returns, contains the connectable object's IConnectionPointContainer interface. This parameter is passed uninitialized.</param>
		// Token: 0x06002C13 RID: 11283
		void GetConnectionPointContainer(out IConnectionPointContainer ppCPC);

		/// <summary>Establishes an advisory connection between the connection point and the caller's sink object.</summary>
		/// <param name="pUnkSink">A reference to the sink to receive calls for the outgoing interface managed by this connection point. </param>
		/// <param name="pdwCookie">When this method returns, contains the connection cookie. This parameter is passed uninitialized.</param>
		// Token: 0x06002C14 RID: 11284
		void Advise([MarshalAs(UnmanagedType.Interface)] object pUnkSink, out int pdwCookie);

		/// <summary>Terminates an advisory connection previously established through the <see cref="M:System.Runtime.InteropServices.ComTypes.IConnectionPoint.Advise(System.Object,System.Int32@)" /> method.</summary>
		/// <param name="dwCookie">The connection cookie previously returned from the <see cref="M:System.Runtime.InteropServices.ComTypes.IConnectionPoint.Advise(System.Object,System.Int32@)" /> method. </param>
		// Token: 0x06002C15 RID: 11285
		void Unadvise(int dwCookie);

		/// <summary>Creates an enumerator object for iteration through the connections that exist to this connection point.</summary>
		/// <param name="ppEnum">When this method returns, contains the newly created enumerator. This parameter is passed uninitialized.</param>
		// Token: 0x06002C16 RID: 11286
		void EnumConnections(out IEnumConnections ppEnum);
	}
}
