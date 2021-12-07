using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents a container for authentication module configuration elements. This class cannot be inherited.</summary>
	// Token: 0x020002C7 RID: 711
	[ConfigurationCollection(typeof(AuthenticationModuleElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class AuthenticationModuleElementCollection : ConfigurationElementCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.AuthenticationModuleElementCollection" /> class. </summary>
		// Token: 0x06001872 RID: 6258 RVA: 0x00043918 File Offset: 0x00041B18
		[MonoTODO]
		public AuthenticationModuleElementCollection()
		{
		}

		/// <summary>Gets or sets the element at the specified position in the collection.</summary>
		/// <returns>The <see cref="T:System.Net.Configuration.AuthenticationModuleElement" /> at the specified location.</returns>
		/// <param name="index">The zero-based index of the element.</param>
		// Token: 0x170005BE RID: 1470
		[MonoTODO]
		public AuthenticationModuleElement this[int index]
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
		/// <returns>The <see cref="T:System.Net.Configuration.AuthenticationModuleElement" /> with the specified key or null if there is no element with the specified key.</returns>
		/// <param name="name">The key for an element in the collection. </param>
		// Token: 0x170005BF RID: 1471
		[MonoTODO]
		public AuthenticationModuleElement this[string name]
		{
			get
			{
				return (AuthenticationModuleElement)base[name];
			}
			set
			{
				base[name] = value;
			}
		}

		/// <summary>Adds an element to the collection.</summary>
		/// <param name="element">The <see cref="T:System.Net.Configuration.AuthenticationModuleElement" /> to add to the collection.</param>
		// Token: 0x06001877 RID: 6263 RVA: 0x0004394C File Offset: 0x00041B4C
		public void Add(AuthenticationModuleElement element)
		{
			this.BaseAdd(element);
		}

		/// <summary>Removes all elements from the collection.</summary>
		// Token: 0x06001878 RID: 6264 RVA: 0x00043958 File Offset: 0x00041B58
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x00043960 File Offset: 0x00041B60
		protected override ConfigurationElement CreateNewElement()
		{
			return new AuthenticationModuleElement();
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x00043968 File Offset: 0x00041B68
		[MonoTODO("argument exception?")]
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (!(element is AuthenticationModuleElement))
			{
				throw new ArgumentException("element");
			}
			return ((AuthenticationModuleElement)element).Type;
		}

		/// <summary>Returns the index of the specified configuration element.</summary>
		/// <returns>The zero-based index of <paramref name="element" />.</returns>
		/// <param name="element">A <see cref="T:System.Net.Configuration.AuthenticationModuleElement" />.</param>
		// Token: 0x0600187B RID: 6267 RVA: 0x0004398C File Offset: 0x00041B8C
		public int IndexOf(AuthenticationModuleElement element)
		{
			return base.BaseIndexOf(element);
		}

		/// <summary>Removes the specified configuration element from the collection.</summary>
		/// <param name="element">The <see cref="T:System.Net.Configuration.AuthenticationModuleElement" /> to remove.</param>
		// Token: 0x0600187C RID: 6268 RVA: 0x00043998 File Offset: 0x00041B98
		public void Remove(AuthenticationModuleElement element)
		{
			base.BaseRemove(element);
		}

		/// <summary>Removes the element with the specified key.</summary>
		/// <param name="name">The key of the element to remove.</param>
		// Token: 0x0600187D RID: 6269 RVA: 0x000439A4 File Offset: 0x00041BA4
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		// Token: 0x0600187E RID: 6270 RVA: 0x000439B0 File Offset: 0x00041BB0
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}
	}
}
