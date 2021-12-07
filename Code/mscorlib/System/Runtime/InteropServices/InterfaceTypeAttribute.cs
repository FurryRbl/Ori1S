using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates whether a managed interface is dual, dispatch-only, or IUnknown -only when exposed to COM.</summary>
	// Token: 0x020003A6 RID: 934
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Interface, Inherited = false)]
	public sealed class InterfaceTypeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.InterfaceTypeAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.ComInterfaceType" /> enumeration member.</summary>
		/// <param name="interfaceType">One of the <see cref="T:System.Runtime.InteropServices.ComInterfaceType" /> values that describes how the interface should be exposed to COM clients. </param>
		// Token: 0x06002A93 RID: 10899 RVA: 0x00092964 File Offset: 0x00090B64
		public InterfaceTypeAttribute(ComInterfaceType interfaceType)
		{
			this.intType = interfaceType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.InterfaceTypeAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.ComInterfaceType" /> enumeration member.</summary>
		/// <param name="interfaceType">Describes how the interface should be exposed to COM clients. </param>
		// Token: 0x06002A94 RID: 10900 RVA: 0x00092974 File Offset: 0x00090B74
		public InterfaceTypeAttribute(short interfaceType)
		{
			this.intType = (ComInterfaceType)interfaceType;
		}

		/// <summary>Gets the <see cref="T:System.Runtime.InteropServices.ComInterfaceType" /> value that describes how the interface should be exposed to COM.</summary>
		/// <returns>The <see cref="T:System.Runtime.InteropServices.ComInterfaceType" /> value that describes how the interface should be exposed to COM.</returns>
		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06002A95 RID: 10901 RVA: 0x00092984 File Offset: 0x00090B84
		public ComInterfaceType Value
		{
			get
			{
				return this.intType;
			}
		}

		// Token: 0x04001151 RID: 4433
		private ComInterfaceType intType;
	}
}
