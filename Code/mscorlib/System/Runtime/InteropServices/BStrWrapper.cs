using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Marshals data of type VT_BSTR from managed to unmanaged code. This class cannot be inherited.</summary>
	// Token: 0x02000370 RID: 880
	[ComVisible(true)]
	[Serializable]
	public sealed class BStrWrapper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.BStrWrapper" /> class with the specified <see cref="T:System.String" /> object.</summary>
		/// <param name="value">The <see cref="T:System.String" /> object to wrap and marshal as VT_BSTR.</param>
		// Token: 0x06002A1B RID: 10779 RVA: 0x000921A8 File Offset: 0x000903A8
		public BStrWrapper(string value)
		{
			this._value = value;
		}

		/// <summary>Gets the wrapped <see cref="T:System.String" /> object to marshal as type VT_BSTR.</summary>
		/// <returns>The <see cref="T:System.String" /> object wrapped by <see cref="T:System.Runtime.InteropServices.BStrWrapper" />.</returns>
		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06002A1C RID: 10780 RVA: 0x000921B8 File Offset: 0x000903B8
		public string WrappedObject
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x040010BA RID: 4282
		private string _value;
	}
}
