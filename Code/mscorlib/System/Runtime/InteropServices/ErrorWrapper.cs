using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Wraps objects the marshaler should marshal as a VT_ERROR.</summary>
	// Token: 0x0200038A RID: 906
	[ComVisible(true)]
	[Serializable]
	public sealed class ErrorWrapper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ErrorWrapper" /> class with the HRESULT that corresponds to the exception supplied.</summary>
		/// <param name="e">The exception to be converted to an error code. </param>
		// Token: 0x06002A4C RID: 10828 RVA: 0x00092588 File Offset: 0x00090788
		public ErrorWrapper(Exception e)
		{
			this.errorCode = Marshal.GetHRForException(e);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ErrorWrapper" /> class with the HRESULT of the error.</summary>
		/// <param name="errorCode">The HRESULT of the error. </param>
		// Token: 0x06002A4D RID: 10829 RVA: 0x0009259C File Offset: 0x0009079C
		public ErrorWrapper(int errorCode)
		{
			this.errorCode = errorCode;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ErrorWrapper" /> class with an object containing the HRESULT of the error.</summary>
		/// <param name="errorCode">The object containing the HRESULT of the error. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="errorCode" /> parameter is not an <see cref="T:System.Int32" /> type.</exception>
		// Token: 0x06002A4E RID: 10830 RVA: 0x000925AC File Offset: 0x000907AC
		public ErrorWrapper(object errorCode)
		{
			if (errorCode.GetType() != typeof(int))
			{
				throw new ArgumentException("errorCode has to be an int type");
			}
			this.errorCode = (int)errorCode;
		}

		/// <summary>Gets the error code of the wrapper.</summary>
		/// <returns>The HRESULT of the error.</returns>
		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06002A4F RID: 10831 RVA: 0x000925EC File Offset: 0x000907EC
		public int ErrorCode
		{
			get
			{
				return this.errorCode;
			}
		}

		// Token: 0x040010FD RID: 4349
		private int errorCode;
	}
}
