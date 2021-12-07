using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	/// <summary>Represents a collection of attributes.</summary>
	// Token: 0x020000CB RID: 203
	[ComVisible(true)]
	public class AttributeCollection : ICollection, IEnumerable
	{
		// Token: 0x060008B0 RID: 2224 RVA: 0x00019B00 File Offset: 0x00017D00
		internal AttributeCollection(ArrayList attributes)
		{
			if (attributes != null)
			{
				this.attrList = attributes;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AttributeCollection" /> class.</summary>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that provides the attributes for this collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributes" /> is null.</exception>
		// Token: 0x060008B1 RID: 2225 RVA: 0x00019B20 File Offset: 0x00017D20
		public AttributeCollection(params Attribute[] attributes)
		{
			if (attributes != null)
			{
				for (int i = 0; i < attributes.Length; i++)
				{
					this.attrList.Add(attributes[i]);
				}
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />. </summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x060008B3 RID: 2227 RVA: 0x00019B78 File Offset: 0x00017D78
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread-safe).</summary>
		/// <returns>true if access to the collection is synchronized (thread-safe); otherwise, false.</returns>
		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x00019B80 File Offset: 0x00017D80
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.attrList.IsSynchronized;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x00019B90 File Offset: 0x00017D90
		object ICollection.SyncRoot
		{
			get
			{
				return this.attrList.SyncRoot;
			}
		}

		/// <summary>Gets the number of elements contained in the collection.</summary>
		/// <returns>The number of elements contained in the collection.</returns>
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x00019BA0 File Offset: 0x00017DA0
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		/// <summary>Creates a new <see cref="T:System.ComponentModel.AttributeCollection" /> from an existing <see cref="T:System.ComponentModel.AttributeCollection" />.</summary>
		/// <returns>A new <see cref="T:System.ComponentModel.AttributeCollection" /> that is a copy of <paramref name="existing" />.</returns>
		/// <param name="existing">An <see cref="T:System.ComponentModel.AttributeCollection" /> from which to create the copy.</param>
		/// <param name="newAttributes">An array of type <see cref="T:System.Attribute" /> that provides the attributes for this collection. Can be null.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="existing" /> is null.</exception>
		// Token: 0x060008B7 RID: 2231 RVA: 0x00019BA8 File Offset: 0x00017DA8
		public static AttributeCollection FromExisting(AttributeCollection existing, params Attribute[] newAttributes)
		{
			if (existing == null)
			{
				throw new ArgumentNullException("existing");
			}
			AttributeCollection attributeCollection = new AttributeCollection(new Attribute[0]);
			attributeCollection.attrList.AddRange(existing.attrList);
			if (newAttributes != null)
			{
				attributeCollection.attrList.AddRange(newAttributes);
			}
			return attributeCollection;
		}

		/// <summary>Determines whether this collection of attributes has the specified attribute.</summary>
		/// <returns>true if the collection contains the attribute or is the default attribute for the type of attribute; otherwise, false.</returns>
		/// <param name="attribute">An <see cref="T:System.Attribute" /> to find in the collection. </param>
		// Token: 0x060008B8 RID: 2232 RVA: 0x00019BF8 File Offset: 0x00017DF8
		public bool Contains(Attribute attr)
		{
			Attribute attribute = this[attr.GetType()];
			return attribute != null && attr.Equals(attribute);
		}

		/// <summary>Determines whether this attribute collection contains all the specified attributes in the attribute array.</summary>
		/// <returns>true if the collection contains all the attributes; otherwise, false.</returns>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> to find in the collection. </param>
		// Token: 0x060008B9 RID: 2233 RVA: 0x00019C24 File Offset: 0x00017E24
		public bool Contains(Attribute[] attributes)
		{
			if (attributes == null)
			{
				return true;
			}
			foreach (Attribute attr in attributes)
			{
				if (!this.Contains(attr))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Copies the collection to an array, starting at the specified index.</summary>
		/// <param name="array">The <see cref="T:System.Array" /> to copy the collection to. </param>
		/// <param name="index">The index to start from. </param>
		// Token: 0x060008BA RID: 2234 RVA: 0x00019C64 File Offset: 0x00017E64
		public void CopyTo(Array array, int index)
		{
			this.attrList.CopyTo(array, index);
		}

		/// <summary>Gets an enumerator for this collection.</summary>
		/// <returns>An enumerator of type <see cref="T:System.Collections.IEnumerator" />.</returns>
		// Token: 0x060008BB RID: 2235 RVA: 0x00019C74 File Offset: 0x00017E74
		public IEnumerator GetEnumerator()
		{
			return this.attrList.GetEnumerator();
		}

		/// <summary>Determines whether a specified attribute is the same as an attribute in the collection.</summary>
		/// <returns>true if the attribute is contained within the collection and has the same value as the attribute in the collection; otherwise, false.</returns>
		/// <param name="attribute">An instance of <see cref="T:System.Attribute" /> to compare with the attributes in this collection. </param>
		// Token: 0x060008BC RID: 2236 RVA: 0x00019C84 File Offset: 0x00017E84
		public bool Matches(Attribute attr)
		{
			foreach (object obj in this.attrList)
			{
				Attribute attribute = (Attribute)obj;
				if (attribute.Match(attr))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Determines whether the attributes in the specified array are the same as the attributes in the collection.</summary>
		/// <returns>true if all the attributes in the array are contained in the collection and have the same values as the attributes in the collection; otherwise, false.</returns>
		/// <param name="attributes">An array of <see cref="T:System.CodeDom.MemberAttributes" /> to compare with the attributes in this collection. </param>
		// Token: 0x060008BD RID: 2237 RVA: 0x00019D04 File Offset: 0x00017F04
		public bool Matches(Attribute[] attributes)
		{
			foreach (Attribute attr in attributes)
			{
				if (!this.Matches(attr))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Returns the default <see cref="T:System.Attribute" /> of a given <see cref="T:System.Type" />.</summary>
		/// <returns>An <see cref="T:System.Attribute" />.</returns>
		/// <param name="attributeType">The <see cref="T:System.Type" /> of the attribute to retrieve. </param>
		// Token: 0x060008BE RID: 2238 RVA: 0x00019D3C File Offset: 0x00017F3C
		protected Attribute GetDefaultAttribute(Type attributeType)
		{
			Attribute attribute = null;
			BindingFlags bindingAttr = BindingFlags.Static | BindingFlags.Public;
			FieldInfo field = attributeType.GetField("Default", bindingAttr);
			if (field == null)
			{
				ConstructorInfo constructor = attributeType.GetConstructor(Type.EmptyTypes);
				if (constructor != null)
				{
					attribute = (constructor.Invoke(null) as Attribute);
				}
				if (attribute != null && !attribute.IsDefaultAttribute())
				{
					attribute = null;
				}
			}
			else
			{
				attribute = (Attribute)field.GetValue(null);
			}
			return attribute;
		}

		/// <summary>Gets the number of attributes.</summary>
		/// <returns>The number of attributes.</returns>
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060008BF RID: 2239 RVA: 0x00019DA8 File Offset: 0x00017FA8
		public int Count
		{
			get
			{
				return (this.attrList == null) ? 0 : this.attrList.Count;
			}
		}

		/// <summary>Gets the attribute with the specified type.</summary>
		/// <returns>The <see cref="T:System.Attribute" /> with the specified type or, if the attribute does not exist, the default value for the attribute type.</returns>
		/// <param name="attributeType">The <see cref="T:System.Type" /> of the <see cref="T:System.Attribute" /> to get from the collection. </param>
		// Token: 0x170001EC RID: 492
		public virtual Attribute this[Type type]
		{
			get
			{
				Attribute attribute = null;
				if (this.attrList != null)
				{
					foreach (object obj in this.attrList)
					{
						Attribute attribute2 = (Attribute)obj;
						if (type.IsAssignableFrom(attribute2.GetType()))
						{
							attribute = attribute2;
							break;
						}
					}
				}
				if (attribute == null)
				{
					attribute = this.GetDefaultAttribute(type);
				}
				return attribute;
			}
		}

		/// <summary>Gets the attribute with the specified index number.</summary>
		/// <returns>The <see cref="T:System.Attribute" /> with the specified index number.</returns>
		/// <param name="index">The zero-based index of <see cref="T:System.ComponentModel.AttributeCollection" />. </param>
		// Token: 0x170001ED RID: 493
		public virtual Attribute this[int index]
		{
			get
			{
				return (Attribute)this.attrList[index];
			}
		}

		// Token: 0x04000242 RID: 578
		private ArrayList attrList = new ArrayList();

		/// <summary>Specifies an empty collection that you can use, rather than creating a new one. This field is read-only.</summary>
		// Token: 0x04000243 RID: 579
		public static readonly AttributeCollection Empty = new AttributeCollection(null);
	}
}
