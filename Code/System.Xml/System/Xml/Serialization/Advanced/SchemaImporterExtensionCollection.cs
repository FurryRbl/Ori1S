using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Xml.Serialization.Advanced
{
	/// <summary>Represents a collection of <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtension" /> objects.</summary>
	// Token: 0x020002CD RID: 717
	public class SchemaImporterExtensionCollection : CollectionBase
	{
		/// <summary>Adds the specified importer extension to the collection.</summary>
		/// <returns>The index of the added extension.</returns>
		/// <param name="extension">The <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtensionCollection" /> to add.</param>
		// Token: 0x06001E22 RID: 7714 RVA: 0x0009D8F4 File Offset: 0x0009BAF4
		public int Add(SchemaImporterExtension extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			return base.List.Add(extension);
		}

		/// <summary>Adds the specified importer extension to the collection. The name parameter allows you to supply a custom name for the extension.</summary>
		/// <returns>The index of the newly added item.</returns>
		/// <param name="name">A custom name for the extension.</param>
		/// <param name="type">The <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtensionCollection" /> to add.</param>
		/// <exception cref="T:System.ArgumentException">The value of type does not inherit from <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtensionCollection" />.</exception>
		// Token: 0x06001E23 RID: 7715 RVA: 0x0009D914 File Offset: 0x0009BB14
		public int Add(string key, Type type)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!type.IsSubclassOf(typeof(SchemaImporterExtension)))
			{
				throw new ArgumentException("The type argument must be subclass of SchemaImporterExtension.");
			}
			SchemaImporterExtension schemaImporterExtension = (SchemaImporterExtension)Activator.CreateInstance(type);
			if (this.named_items.ContainsKey(key))
			{
				throw new InvalidOperationException(string.Format("A SchemaImporterExtension keyed by '{0}' already exists.", key));
			}
			int result = this.Add(schemaImporterExtension);
			this.named_items.Add(key, schemaImporterExtension);
			return result;
		}

		/// <summary>Clears the collection of importer extensions.</summary>
		// Token: 0x06001E24 RID: 7716 RVA: 0x0009D9A8 File Offset: 0x0009BBA8
		public new void Clear()
		{
			this.named_items.Clear();
			base.List.Clear();
		}

		/// <summary>Gets a value that indicates whether the specified importer extension exists in the collection.</summary>
		/// <returns>true if the extension is found; otherwise, false.</returns>
		/// <param name="extension">The <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtensionCollection" /> to search for.</param>
		// Token: 0x06001E25 RID: 7717 RVA: 0x0009D9C0 File Offset: 0x0009BBC0
		public bool Contains(SchemaImporterExtension extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			foreach (object obj in base.List)
			{
				SchemaImporterExtension obj2 = (SchemaImporterExtension)obj;
				if (extension.Equals(obj2))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Copies all the elements of the current <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtensionCollection" /> to the specified array of <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtension" /> objects at the specified index. </summary>
		/// <param name="array">The <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtension" /> to copy the current collection to.</param>
		/// <param name="index">The zero-based index at which the collection is added.</param>
		// Token: 0x06001E26 RID: 7718 RVA: 0x0009DA50 File Offset: 0x0009BC50
		public void CopyTo(SchemaImporterExtension[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Searches for the specified item and returns the zero-based index of the first occurrence within the collection.</summary>
		/// <returns>The index of the found item.</returns>
		/// <param name="extension">The <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtension" /> to search for.</param>
		// Token: 0x06001E27 RID: 7719 RVA: 0x0009DA60 File Offset: 0x0009BC60
		public int IndexOf(SchemaImporterExtension extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			int num = 0;
			foreach (object obj in base.List)
			{
				SchemaImporterExtension obj2 = (SchemaImporterExtension)obj;
				if (extension.Equals(obj2))
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		/// <summary>Inserts the specified <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtension" /> into the collection at the specified index.</summary>
		/// <param name="index">The zero-base index at which the <paramref name="extension" /> should be inserted.</param>
		/// <param name="extension">The <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtension" /> to insert.</param>
		// Token: 0x06001E28 RID: 7720 RVA: 0x0009DAF8 File Offset: 0x0009BCF8
		public void Insert(int index, SchemaImporterExtension extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			base.List.Insert(index, extension);
		}

		/// <summary>Removes the specified <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtension" /> from the collection.</summary>
		/// <param name="extension">The <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtension" /> to remove. </param>
		// Token: 0x06001E29 RID: 7721 RVA: 0x0009DB18 File Offset: 0x0009BD18
		public void Remove(SchemaImporterExtension extension)
		{
			int num = this.IndexOf(extension);
			if (num >= 0)
			{
				base.List.RemoveAt(num);
			}
		}

		/// <summary>Removes the <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtension" />, specified by name, from the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtension" /> to remove. The name is set using the <see cref="M:System.Xml.Serialization.Advanced.SchemaImporterExtensionCollection.Add(System.String,System.Type)" /> method.</param>
		// Token: 0x06001E2A RID: 7722 RVA: 0x0009DB40 File Offset: 0x0009BD40
		public void Remove(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (!this.named_items.ContainsKey(name))
			{
				return;
			}
			SchemaImporterExtension extension = this.named_items[name];
			this.Remove(extension);
			this.named_items.Remove(name);
		}

		/// <summary>Gets the <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtensionCollection" /> at the specified index.</summary>
		/// <returns>The <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtensionCollection" /> at the specified index.</returns>
		/// <param name="index">The index of the item to find.</param>
		// Token: 0x17000866 RID: 2150
		public SchemaImporterExtension this[int index]
		{
			get
			{
				return (SchemaImporterExtension)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x04000BF2 RID: 3058
		private Dictionary<string, SchemaImporterExtension> named_items = new Dictionary<string, SchemaImporterExtension>();
	}
}
