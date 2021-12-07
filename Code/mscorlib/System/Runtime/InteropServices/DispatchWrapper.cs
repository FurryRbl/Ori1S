﻿using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Wraps objects the marshaler should marshal as a VT_DISPATCH.</summary>
	// Token: 0x02000385 RID: 901
	[ComVisible(true)]
	[Serializable]
	public sealed class DispatchWrapper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.DispatchWrapper" /> class with the object being wrapped.</summary>
		/// <param name="obj">The object to be wrapped and converted to <see cref="F:System.Runtime.InteropServices.VarEnum.VT_DISPATCH" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="obj" /> is not a class or an array.-or- <paramref name="obj" /> does not support IDispatch. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="obj" /> parameter was marked with a <see cref="T:System.Runtime.InteropServices.ComVisibleAttribute" />  attribute that was passed a value of false.-or-The <paramref name="obj" /> parameter inherits from a type marked with a <see cref="T:System.Runtime.InteropServices.ComVisibleAttribute" />  attribute that was passed a value of false.</exception>
		// Token: 0x06002A48 RID: 10824 RVA: 0x00092550 File Offset: 0x00090750
		public DispatchWrapper(object obj)
		{
			Marshal.GetIDispatchForObject(obj);
			this.wrappedObject = obj;
		}

		/// <summary>Gets the object wrapped by the <see cref="T:System.Runtime.InteropServices.DispatchWrapper" />.</summary>
		/// <returns>The object wrapped by the <see cref="T:System.Runtime.InteropServices.DispatchWrapper" />.</returns>
		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06002A49 RID: 10825 RVA: 0x00092568 File Offset: 0x00090768
		public object WrappedObject
		{
			get
			{
				return this.wrappedObject;
			}
		}

		// Token: 0x040010F3 RID: 4339
		private object wrappedObject;
	}
}
