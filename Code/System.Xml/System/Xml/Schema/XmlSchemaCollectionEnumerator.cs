using System;
using System.Collections;

namespace System.Xml.Schema
{
	/// <summary>Supports a simple iteration over a collection. This class cannot be inherited. </summary>
	// Token: 0x02000202 RID: 514
	public sealed class XmlSchemaCollectionEnumerator : IEnumerator
	{
		// Token: 0x06001491 RID: 5265 RVA: 0x00059E2C File Offset: 0x0005802C
		internal XmlSchemaCollectionEnumerator(ICollection col)
		{
			this.xenum = col.GetEnumerator();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Schema.XmlSchemaCollectionEnumerator.MoveNext" />.</summary>
		// Token: 0x06001492 RID: 5266 RVA: 0x00059E40 File Offset: 0x00058040
		bool IEnumerator.MoveNext()
		{
			return this.xenum.MoveNext();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Schema.XmlSchemaCollectionEnumerator.System.Collections.IEnumerator.Reset" />.</summary>
		// Token: 0x06001493 RID: 5267 RVA: 0x00059E50 File Offset: 0x00058050
		void IEnumerator.Reset()
		{
			this.xenum.Reset();
		}

		/// <summary>For a description of this member, see <see cref="P:System.Xml.Schema.XmlSchemaCollectionEnumerator.Current" />.</summary>
		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001494 RID: 5268 RVA: 0x00059E60 File Offset: 0x00058060
		object IEnumerator.Current
		{
			get
			{
				return this.xenum.Current;
			}
		}

		/// <summary>Gets the current <see cref="T:System.Xml.Schema.XmlSchema" /> in the collection.</summary>
		/// <returns>The current XmlSchema in the collection.</returns>
		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001495 RID: 5269 RVA: 0x00059E70 File Offset: 0x00058070
		public XmlSchema Current
		{
			get
			{
				return (XmlSchema)this.xenum.Current;
			}
		}

		/// <summary>Advances the enumerator to the next schema in the collection.</summary>
		/// <returns>true if the move was successful; false if the enumerator has passed the end of the collection.</returns>
		// Token: 0x06001496 RID: 5270 RVA: 0x00059E84 File Offset: 0x00058084
		public bool MoveNext()
		{
			return this.xenum.MoveNext();
		}

		// Token: 0x040007DD RID: 2013
		private IEnumerator xenum;
	}
}
