using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Wraps objects the marshaler should marshal as a VT_CY.</summary>
	// Token: 0x02000383 RID: 899
	[ComVisible(true)]
	[Serializable]
	public sealed class CurrencyWrapper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.CurrencyWrapper" /> class with the Decimal to be wrapped and marshaled as type VT_CY.</summary>
		/// <param name="obj">The Decimal to be wrapped and marshaled as VT_CY. </param>
		// Token: 0x06002A45 RID: 10821 RVA: 0x000924F8 File Offset: 0x000906F8
		public CurrencyWrapper(decimal obj)
		{
			this.currency = obj;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.CurrencyWrapper" /> class with the object containing the Decimal to be wrapped and marshaled as type VT_CY.</summary>
		/// <param name="obj">The object containing the Decimal to be wrapped and marshaled as VT_CY. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="obj" /> parameter is not a <see cref="T:System.Decimal" /> type.</exception>
		// Token: 0x06002A46 RID: 10822 RVA: 0x00092508 File Offset: 0x00090708
		public CurrencyWrapper(object obj)
		{
			if (obj.GetType() != typeof(decimal))
			{
				throw new ArgumentException("obj has to be a Decimal type");
			}
			this.currency = (decimal)obj;
		}

		/// <summary>Gets the wrapped object to be marshaled as type VT_CY.</summary>
		/// <returns>The wrapped object to be marshaled as type VT_CY.</returns>
		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06002A47 RID: 10823 RVA: 0x00092548 File Offset: 0x00090748
		public decimal WrappedObject
		{
			get
			{
				return this.currency;
			}
		}

		// Token: 0x040010EB RID: 4331
		private decimal currency;
	}
}
