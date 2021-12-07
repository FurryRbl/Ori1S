using System;

namespace System.Xml.Serialization
{
	/// <summary>Provides data for the known, but unreferenced, object found in an encoded SOAP XML stream during deserialization.</summary>
	// Token: 0x0200027C RID: 636
	public class UnreferencedObjectEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.UnreferencedObjectEventArgs" /> class.</summary>
		/// <param name="o">The unreferenced object. </param>
		/// <param name="id">A unique string used to identify the unreferenced object. </param>
		// Token: 0x060019BC RID: 6588 RVA: 0x00087F64 File Offset: 0x00086164
		public UnreferencedObjectEventArgs(object o, string id)
		{
			this.unreferencedObject = o;
			this.unreferencedId = id;
		}

		/// <summary>Gets the ID of the object.</summary>
		/// <returns>The ID of the object.</returns>
		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x060019BD RID: 6589 RVA: 0x00087F7C File Offset: 0x0008617C
		public string UnreferencedId
		{
			get
			{
				return this.unreferencedId;
			}
		}

		/// <summary>Gets the deserialized, but unreferenced, object.</summary>
		/// <returns>The deserialized, but unreferenced, object.</returns>
		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x060019BE RID: 6590 RVA: 0x00087F84 File Offset: 0x00086184
		public object UnreferencedObject
		{
			get
			{
				return this.unreferencedObject;
			}
		}

		// Token: 0x04000AA7 RID: 2727
		private object unreferencedObject;

		// Token: 0x04000AA8 RID: 2728
		private string unreferencedId;
	}
}
