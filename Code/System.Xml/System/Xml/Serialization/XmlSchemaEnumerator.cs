using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	/// <summary>Enables iteration over a collection of <see cref="T:System.Xml.Schema.XmlSchema" /> objects. </summary>
	// Token: 0x0200029D RID: 669
	[MonoTODO]
	public class XmlSchemaEnumerator : IEnumerator<XmlSchema>, IDisposable, IEnumerator
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSchemaEnumerator" /> class. </summary>
		/// <param name="list">The <see cref="T:System.Xml.Serialization.XmlSchemas" /> object you want to iterate over.</param>
		// Token: 0x06001B1E RID: 6942 RVA: 0x0008D818 File Offset: 0x0008BA18
		public XmlSchemaEnumerator(XmlSchemas list)
		{
			this.e = list.GetEnumerator();
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06001B1F RID: 6943 RVA: 0x0008D82C File Offset: 0x0008BA2C
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x0008D834 File Offset: 0x0008BA34
		void IEnumerator.Reset()
		{
			this.e.Reset();
		}

		/// <summary>Gets the current element in the collection.</summary>
		/// <returns>The current <see cref="T:System.Xml.Schema.XmlSchema" /> object in the collection.</returns>
		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06001B21 RID: 6945 RVA: 0x0008D844 File Offset: 0x0008BA44
		public XmlSchema Current
		{
			get
			{
				return (XmlSchema)this.e.Current;
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Xml.Serialization.XmlSchemaEnumerator" />.</summary>
		// Token: 0x06001B22 RID: 6946 RVA: 0x0008D858 File Offset: 0x0008BA58
		public void Dispose()
		{
		}

		/// <summary>Advances the enumerator to the next item in the collection.</summary>
		/// <returns>true if the move is successful; otherwise, false.</returns>
		// Token: 0x06001B23 RID: 6947 RVA: 0x0008D85C File Offset: 0x0008BA5C
		public bool MoveNext()
		{
			return this.e.MoveNext();
		}

		// Token: 0x04000B1E RID: 2846
		private IEnumerator e;
	}
}
