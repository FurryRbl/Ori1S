using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates which IDispatch implementation the common language runtime uses when exposing dual interfaces and dispinterfaces to COM.</summary>
	// Token: 0x02000399 RID: 921
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	[ComVisible(true)]
	[Obsolete]
	public sealed class IDispatchImplAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the IDispatchImplAttribute class with specified <see cref="T:System.Runtime.InteropServices.IDispatchImplType" /> value.</summary>
		/// <param name="implType">Indicates which <see cref="T:System.Runtime.InteropServices.IDispatchImplType" /> enumeration will be used. </param>
		// Token: 0x06002A7D RID: 10877 RVA: 0x00092924 File Offset: 0x00090B24
		public IDispatchImplAttribute(IDispatchImplType implType)
		{
			this.Impl = implType;
		}

		/// <summary>Initializes a new instance of the IDispatchImplAttribute class with specified <see cref="T:System.Runtime.InteropServices.IDispatchImplType" /> value.</summary>
		/// <param name="implType">Indicates which <see cref="T:System.Runtime.InteropServices.IDispatchImplType" /> enumeration will be used. </param>
		// Token: 0x06002A7E RID: 10878 RVA: 0x00092934 File Offset: 0x00090B34
		public IDispatchImplAttribute(short implType)
		{
			this.Impl = (IDispatchImplType)implType;
		}

		/// <summary>Gets the <see cref="T:System.Runtime.InteropServices.IDispatchImplType" /> value used by the class.</summary>
		/// <returns>The <see cref="T:System.Runtime.InteropServices.IDispatchImplType" /> value used by the class.</returns>
		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06002A7F RID: 10879 RVA: 0x00092944 File Offset: 0x00090B44
		public IDispatchImplType Value
		{
			get
			{
				return this.Impl;
			}
		}

		// Token: 0x04001135 RID: 4405
		private IDispatchImplType Impl;
	}
}
