using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents a container for the addresses of resources that bypass the proxy server. This class cannot be inherited.</summary>
	// Token: 0x020002CA RID: 714
	[ConfigurationCollection(typeof(BypassElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class BypassElementCollection : ConfigurationElementCollection
	{
		/// <summary>Gets or sets the element at the specified position in the collection.</summary>
		/// <returns>The <see cref="T:System.Net.Configuration.BypassElement" /> at the specified location.</returns>
		/// <param name="index">The zero-based index of the element.</param>
		// Token: 0x170005C4 RID: 1476
		[MonoTODO]
		public BypassElement this[int index]
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the element with the specified key.</summary>
		/// <returns>The <see cref="T:System.Net.Configuration.BypassElement" /> with the specified key, or null if there is no element with the specified key.</returns>
		/// <param name="name">The key for an element in the collection. </param>
		// Token: 0x170005C5 RID: 1477
		public BypassElement this[string name]
		{
			get
			{
				return (BypassElement)base[name];
			}
			set
			{
				base[name] = value;
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001890 RID: 6288 RVA: 0x00043AE8 File Offset: 0x00041CE8
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return false;
			}
		}

		/// <summary>Adds an element to the collection.</summary>
		/// <param name="element">The <see cref="T:System.Net.Configuration.BypassElement" /> to add to the collection.</param>
		// Token: 0x06001891 RID: 6289 RVA: 0x00043AEC File Offset: 0x00041CEC
		public void Add(BypassElement element)
		{
			this.BaseAdd(element);
		}

		/// <summary>Removes all elements from the collection.</summary>
		// Token: 0x06001892 RID: 6290 RVA: 0x00043AF8 File Offset: 0x00041CF8
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x00043B00 File Offset: 0x00041D00
		protected override ConfigurationElement CreateNewElement()
		{
			return new BypassElement();
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x00043B08 File Offset: 0x00041D08
		[MonoTODO("argument exception?")]
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (!(element is BypassElement))
			{
				throw new ArgumentException("element");
			}
			return ((BypassElement)element).Address;
		}

		/// <summary>Returns the index of the specified configuration element.</summary>
		/// <returns>The zero-based index of <paramref name="element" />.</returns>
		/// <param name="element">A <see cref="T:System.Net.Configuration.BypassElement" />.</param>
		// Token: 0x06001895 RID: 6293 RVA: 0x00043B2C File Offset: 0x00041D2C
		public int IndexOf(BypassElement element)
		{
			return base.BaseIndexOf(element);
		}

		/// <summary>Removes the specified configuration element from the collection.</summary>
		/// <param name="element">The <see cref="T:System.Net.Configuration.BypassElement" /> to remove.</param>
		// Token: 0x06001896 RID: 6294 RVA: 0x00043B38 File Offset: 0x00041D38
		public void Remove(BypassElement element)
		{
			base.BaseRemove(element);
		}

		/// <summary>Removes the element with the specified key.</summary>
		/// <param name="name">The key of the element to remove.</param>
		// Token: 0x06001897 RID: 6295 RVA: 0x00043B44 File Offset: 0x00041D44
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		// Token: 0x06001898 RID: 6296 RVA: 0x00043B50 File Offset: 0x00041D50
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}
	}
}
