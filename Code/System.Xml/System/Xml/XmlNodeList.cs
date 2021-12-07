using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace System.Xml
{
	/// <summary>Represents an ordered collection of nodes.</summary>
	// Token: 0x02000107 RID: 263
	public abstract class XmlNodeList : IEnumerable
	{
		/// <summary>Gets the number of nodes in the XmlNodeList.</summary>
		/// <returns>The number of nodes.</returns>
		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000A92 RID: 2706
		public abstract int Count { get; }

		/// <summary>Retrieves a node at the given index.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> in the collection. If index is greater than or equal to the number of nodes in the list, this returns null.</returns>
		/// <param name="i">Zero-based index into the list of nodes. </param>
		// Token: 0x170002F9 RID: 761
		[IndexerName("ItemOf")]
		public virtual XmlNode this[int i]
		{
			get
			{
				return this.Item(i);
			}
		}

		/// <summary>Provides a simple "foreach" style iteration over the collection of nodes in the XmlNodeList.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" />.</returns>
		// Token: 0x06000A94 RID: 2708
		public abstract IEnumerator GetEnumerator();

		/// <summary>Retrieves a node at the given index.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> in the collection. If <paramref name="index" /> is greater than or equal to the number of nodes in the list, this returns null.</returns>
		/// <param name="index">Zero-based index into the list of nodes. </param>
		// Token: 0x06000A95 RID: 2709
		public abstract XmlNode Item(int index);
	}
}
