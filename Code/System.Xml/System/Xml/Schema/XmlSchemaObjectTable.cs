using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Xml.Schema
{
	/// <summary>Provides the collections for contained elements in the <see cref="T:System.Xml.Schema.XmlSchema" /> class (for example, Attributes, AttributeGroups, Elements, and so on).</summary>
	// Token: 0x02000231 RID: 561
	public class XmlSchemaObjectTable
	{
		// Token: 0x06001637 RID: 5687 RVA: 0x000660CC File Offset: 0x000642CC
		internal XmlSchemaObjectTable()
		{
			this.table = new HybridDictionary();
		}

		/// <summary>Gets the number of items contained in the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" />.</summary>
		/// <returns>The number of items contained in the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" />.</returns>
		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06001638 RID: 5688 RVA: 0x000660E0 File Offset: 0x000642E0
		public int Count
		{
			get
			{
				return this.table.Count;
			}
		}

		/// <summary>Returns the element in the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" /> specified by qualified name.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaObject" /> of the element in the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" /> specified by qualified name.</returns>
		/// <param name="name">The <see cref="T:System.Xml.XmlQualifiedName" /> of the element to return.</param>
		// Token: 0x170006C9 RID: 1737
		public XmlSchemaObject this[XmlQualifiedName name]
		{
			get
			{
				return (XmlSchemaObject)this.table[name];
			}
		}

		/// <summary>Returns a collection of all the named elements in the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" />.</summary>
		/// <returns>A collection of all the named elements in the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" />.</returns>
		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x0600163A RID: 5690 RVA: 0x00066104 File Offset: 0x00064304
		public ICollection Names
		{
			get
			{
				return this.table.Keys;
			}
		}

		/// <summary>Returns a collection of all the values for all the elements in the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" />.</summary>
		/// <returns>A collection of all the values for all the elements in the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" />.</returns>
		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x0600163B RID: 5691 RVA: 0x00066114 File Offset: 0x00064314
		public ICollection Values
		{
			get
			{
				return this.table.Values;
			}
		}

		/// <summary>Determines if the qualified name specified exists in the collection.</summary>
		/// <returns>true if the qualified name specified exists in the collection; otherwise, false.</returns>
		/// <param name="name">The <see cref="T:System.Xml.XmlQualifiedName" />.</param>
		// Token: 0x0600163C RID: 5692 RVA: 0x00066124 File Offset: 0x00064324
		public bool Contains(XmlQualifiedName name)
		{
			return this.table.Contains(name);
		}

		/// <summary>Returns an enumerator that can iterate through the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> that can iterate through <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" />.</returns>
		// Token: 0x0600163D RID: 5693 RVA: 0x00066134 File Offset: 0x00064334
		public IDictionaryEnumerator GetEnumerator()
		{
			return new XmlSchemaObjectTable.XmlSchemaObjectTableEnumerator(this);
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x0006613C File Offset: 0x0006433C
		internal void Add(XmlQualifiedName name, XmlSchemaObject value)
		{
			this.table[name] = value;
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x0006614C File Offset: 0x0006434C
		internal void Clear()
		{
			this.table.Clear();
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x0006615C File Offset: 0x0006435C
		internal void Set(XmlQualifiedName name, XmlSchemaObject value)
		{
			this.table[name] = value;
		}

		// Token: 0x040008EB RID: 2283
		private HybridDictionary table;

		// Token: 0x02000232 RID: 562
		internal class XmlSchemaObjectTableEnumerator : IEnumerator, IDictionaryEnumerator
		{
			// Token: 0x06001641 RID: 5697 RVA: 0x0006616C File Offset: 0x0006436C
			internal XmlSchemaObjectTableEnumerator(XmlSchemaObjectTable table)
			{
				this.tmp = table.table;
				this.xenum = (IDictionaryEnumerator)this.tmp.GetEnumerator();
			}

			// Token: 0x06001642 RID: 5698 RVA: 0x000661A4 File Offset: 0x000643A4
			bool IEnumerator.MoveNext()
			{
				return this.xenum.MoveNext();
			}

			// Token: 0x06001643 RID: 5699 RVA: 0x000661B4 File Offset: 0x000643B4
			void IEnumerator.Reset()
			{
				this.xenum.Reset();
			}

			// Token: 0x170006CC RID: 1740
			// (get) Token: 0x06001644 RID: 5700 RVA: 0x000661C4 File Offset: 0x000643C4
			object IEnumerator.Current
			{
				get
				{
					return this.xenum.Entry;
				}
			}

			// Token: 0x170006CD RID: 1741
			// (get) Token: 0x06001645 RID: 5701 RVA: 0x000661D8 File Offset: 0x000643D8
			DictionaryEntry IDictionaryEnumerator.Entry
			{
				get
				{
					return this.xenum.Entry;
				}
			}

			// Token: 0x170006CE RID: 1742
			// (get) Token: 0x06001646 RID: 5702 RVA: 0x000661E8 File Offset: 0x000643E8
			object IDictionaryEnumerator.Key
			{
				get
				{
					return (XmlQualifiedName)this.xenum.Key;
				}
			}

			// Token: 0x170006CF RID: 1743
			// (get) Token: 0x06001647 RID: 5703 RVA: 0x000661FC File Offset: 0x000643FC
			object IDictionaryEnumerator.Value
			{
				get
				{
					return (XmlSchemaObject)this.xenum.Value;
				}
			}

			// Token: 0x170006D0 RID: 1744
			// (get) Token: 0x06001648 RID: 5704 RVA: 0x00066210 File Offset: 0x00064410
			public XmlSchemaObject Current
			{
				get
				{
					return (XmlSchemaObject)this.xenum.Value;
				}
			}

			// Token: 0x170006D1 RID: 1745
			// (get) Token: 0x06001649 RID: 5705 RVA: 0x00066224 File Offset: 0x00064424
			public DictionaryEntry Entry
			{
				get
				{
					return this.xenum.Entry;
				}
			}

			// Token: 0x170006D2 RID: 1746
			// (get) Token: 0x0600164A RID: 5706 RVA: 0x00066234 File Offset: 0x00064434
			public XmlQualifiedName Key
			{
				get
				{
					return (XmlQualifiedName)this.xenum.Key;
				}
			}

			// Token: 0x170006D3 RID: 1747
			// (get) Token: 0x0600164B RID: 5707 RVA: 0x00066248 File Offset: 0x00064448
			public XmlSchemaObject Value
			{
				get
				{
					return (XmlSchemaObject)this.xenum.Value;
				}
			}

			// Token: 0x0600164C RID: 5708 RVA: 0x0006625C File Offset: 0x0006445C
			public bool MoveNext()
			{
				return this.xenum.MoveNext();
			}

			// Token: 0x040008EC RID: 2284
			private IDictionaryEnumerator xenum;

			// Token: 0x040008ED RID: 2285
			private IEnumerable tmp;
		}
	}
}
