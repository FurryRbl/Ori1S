using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Marshals data of type VT_VARIANT | VT_BYREF from managed to unmanaged code. This class cannot be inherited.</summary>
	// Token: 0x020003E4 RID: 996
	[Serializable]
	public sealed class VariantWrapper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.VariantWrapper" /> class for the specified <see cref="T:System.Object" /> parameter.</summary>
		/// <param name="obj">The object to marshal. </param>
		// Token: 0x06002C06 RID: 11270 RVA: 0x00093D14 File Offset: 0x00091F14
		public VariantWrapper(object obj)
		{
			this._wrappedObject = obj;
		}

		/// <summary>Gets the object wrapped by the <see cref="T:System.Runtime.InteropServices.VariantWrapper" /> object.</summary>
		/// <returns>The object wrapped by the <see cref="T:System.Runtime.InteropServices.VariantWrapper" /> object.</returns>
		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06002C07 RID: 11271 RVA: 0x00093D24 File Offset: 0x00091F24
		public object WrappedObject
		{
			get
			{
				return this._wrappedObject;
			}
		}

		// Token: 0x04001283 RID: 4739
		private object _wrappedObject;
	}
}
