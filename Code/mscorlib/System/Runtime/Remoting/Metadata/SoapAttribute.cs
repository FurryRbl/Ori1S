using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
	/// <summary>Provides default functionality for all SOAP attributes.</summary>
	// Token: 0x020004BF RID: 1215
	[ComVisible(true)]
	public class SoapAttribute : Attribute
	{
		/// <summary>Gets or sets a value indicating whether the type must be nested during SOAP serialization.</summary>
		/// <returns>true if the target object must be nested during SOAP serialization; otherwise, false.</returns>
		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x0600312B RID: 12587 RVA: 0x000A1D0C File Offset: 0x0009FF0C
		// (set) Token: 0x0600312C RID: 12588 RVA: 0x000A1D14 File Offset: 0x0009FF14
		public virtual bool Embedded
		{
			get
			{
				return this._nested;
			}
			set
			{
				this._nested = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the target of the current attribute will be serialized as an XML attribute instead of an XML field.</summary>
		/// <returns>true if the target object of the current attribute must be serialized as an XML attribute; false if the target object must be serialized as a subelement.</returns>
		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x0600312D RID: 12589 RVA: 0x000A1D20 File Offset: 0x0009FF20
		// (set) Token: 0x0600312E RID: 12590 RVA: 0x000A1D28 File Offset: 0x0009FF28
		public virtual bool UseAttribute
		{
			get
			{
				return this._useAttribute;
			}
			set
			{
				this._useAttribute = value;
			}
		}

		/// <summary>Gets or sets the XML namespace name.</summary>
		/// <returns>The XML namespace name under which the target of the current attribute is serialized.</returns>
		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x0600312F RID: 12591 RVA: 0x000A1D34 File Offset: 0x0009FF34
		// (set) Token: 0x06003130 RID: 12592 RVA: 0x000A1D3C File Offset: 0x0009FF3C
		public virtual string XmlNamespace
		{
			get
			{
				return this.ProtXmlNamespace;
			}
			set
			{
				this.ProtXmlNamespace = value;
			}
		}

		// Token: 0x06003131 RID: 12593 RVA: 0x000A1D48 File Offset: 0x0009FF48
		internal virtual void SetReflectionObject(object reflectionObject)
		{
			this.ReflectInfo = reflectionObject;
		}

		// Token: 0x040014CD RID: 5325
		private bool _nested;

		// Token: 0x040014CE RID: 5326
		private bool _useAttribute;

		/// <summary>The XML namespace to which the target of the current SOAP attribute is serialized.</summary>
		// Token: 0x040014CF RID: 5327
		protected string ProtXmlNamespace;

		/// <summary>A reflection object used by attribute classes derived from the <see cref="T:System.Runtime.Remoting.Metadata.SoapAttribute" /> class to set XML serialization information.</summary>
		// Token: 0x040014D0 RID: 5328
		protected object ReflectInfo;
	}
}
