using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Controls the marshaling behavior of a delegate signature passed as an unmanaged function pointer to or from unmanaged code. This class cannot be inherited. </summary>
	// Token: 0x020003DE RID: 990
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Delegate, Inherited = false, AllowMultiple = false)]
	public sealed class UnmanagedFunctionPointerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute" /> class with the specified calling convention. </summary>
		/// <param name="callingConvention">The specified calling convention.</param>
		// Token: 0x06002C04 RID: 11268 RVA: 0x00093CFC File Offset: 0x00091EFC
		public UnmanagedFunctionPointerAttribute(CallingConvention callingConvention)
		{
			this.call_conv = callingConvention;
		}

		/// <summary>Gets the value of the calling convention.</summary>
		/// <returns>The value of the calling convention specified by the <see cref="M:System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute.#ctor(System.Runtime.InteropServices.CallingConvention)" /> constructor.</returns>
		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06002C05 RID: 11269 RVA: 0x00093D0C File Offset: 0x00091F0C
		public CallingConvention CallingConvention
		{
			get
			{
				return this.call_conv;
			}
		}

		// Token: 0x04001218 RID: 4632
		private CallingConvention call_conv;

		/// <summary>Indicates how to marshal string parameters to the method, and controls name mangling.</summary>
		// Token: 0x04001219 RID: 4633
		public CharSet CharSet;

		/// <summary>Indicates whether the callee calls the SetLastError Win32 API function before returning from the attributed method.</summary>
		// Token: 0x0400121A RID: 4634
		public bool SetLastError;

		/// <summary>Enables or disables best-fit mapping behavior when converting Unicode characters to ANSI characters.</summary>
		// Token: 0x0400121B RID: 4635
		public bool BestFitMapping;

		/// <summary>Enables or disables the throwing of an exception on an unmappable Unicode character that is converted to an ANSI "?" character.</summary>
		// Token: 0x0400121C RID: 4636
		public bool ThrowOnUnmappableChar;
	}
}
