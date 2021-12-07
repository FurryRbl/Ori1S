using System;
using System.Collections;

namespace System.Xml.Schema
{
	/// <summary>Represents the enumerator for the <see cref="T:System.Xml.Schema.XmlSchemaObjectCollection" />.</summary>
	// Token: 0x02000230 RID: 560
	public class XmlSchemaObjectEnumerator : IEnumerator
	{
		// Token: 0x06001630 RID: 5680 RVA: 0x00066050 File Offset: 0x00064250
		internal XmlSchemaObjectEnumerator(IList list)
		{
			this.ienum = list.GetEnumerator();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Schema.XmlSchemaObjectEnumerator.MoveNext" />.</summary>
		// Token: 0x06001631 RID: 5681 RVA: 0x00066064 File Offset: 0x00064264
		bool IEnumerator.MoveNext()
		{
			return this.ienum.MoveNext();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Schema.XmlSchemaObjectEnumerator.Reset" />.</summary>
		// Token: 0x06001632 RID: 5682 RVA: 0x00066074 File Offset: 0x00064274
		void IEnumerator.Reset()
		{
			this.ienum.Reset();
		}

		/// <summary>For a description of this member, see <see cref="P:System.Xml.Schema.XmlSchemaObjectEnumerator.Current" />.</summary>
		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06001633 RID: 5683 RVA: 0x00066084 File Offset: 0x00064284
		object IEnumerator.Current
		{
			get
			{
				return (XmlSchemaObject)this.ienum.Current;
			}
		}

		/// <summary>Gets the current <see cref="T:System.Xml.Schema.XmlSchemaObject" /> in the collection.</summary>
		/// <returns>The current <see cref="T:System.Xml.Schema.XmlSchemaObject" />.</returns>
		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06001634 RID: 5684 RVA: 0x00066098 File Offset: 0x00064298
		public XmlSchemaObject Current
		{
			get
			{
				return (XmlSchemaObject)this.ienum.Current;
			}
		}

		/// <summary>Moves to the next item in the collection.</summary>
		/// <returns>false at the end of the collection.</returns>
		// Token: 0x06001635 RID: 5685 RVA: 0x000660AC File Offset: 0x000642AC
		public bool MoveNext()
		{
			return this.ienum.MoveNext();
		}

		/// <summary>Resets the enumerator to the start of the collection.</summary>
		// Token: 0x06001636 RID: 5686 RVA: 0x000660BC File Offset: 0x000642BC
		public void Reset()
		{
			this.ienum.Reset();
		}

		// Token: 0x040008EA RID: 2282
		private IEnumerator ienum;
	}
}
