using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Wraps objects the marshaler should marshal as a VT_UNKNOWN.</summary>
	// Token: 0x020003DD RID: 989
	[ComVisible(true)]
	[Serializable]
	public sealed class UnknownWrapper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.UnknownWrapper" /> class with the object to be wrapped.</summary>
		/// <param name="obj">The object being wrapped. </param>
		// Token: 0x06002C02 RID: 11266 RVA: 0x00093CE4 File Offset: 0x00091EE4
		public UnknownWrapper(object obj)
		{
			this.InternalObject = obj;
		}

		/// <summary>Gets the object contained by this wrapper.</summary>
		/// <returns>The wrapped object.</returns>
		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06002C03 RID: 11267 RVA: 0x00093CF4 File Offset: 0x00091EF4
		public object WrappedObject
		{
			get
			{
				return this.InternalObject;
			}
		}

		// Token: 0x04001217 RID: 4631
		private object InternalObject;
	}
}
