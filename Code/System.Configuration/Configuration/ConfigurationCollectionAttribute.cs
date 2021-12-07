using System;

namespace System.Configuration
{
	/// <summary>Declaratively instructs the .NET Framework to create an instance of a configuration element collection. This class cannot be inherited.</summary>
	// Token: 0x0200001F RID: 31
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class ConfigurationCollectionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationCollectionAttribute" /> class.</summary>
		/// <param name="itemType">The type of the property collection to create.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="itemType" /> is null.</exception>
		// Token: 0x0600010D RID: 269 RVA: 0x00003C04 File Offset: 0x00001E04
		public ConfigurationCollectionAttribute(Type itemType)
		{
			this.itemType = itemType;
		}

		/// <summary>Gets or sets the name of the &lt;add&gt; configuration element.</summary>
		/// <returns>The name that substitutes the standard name "add" for the configuration item.</returns>
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00003C40 File Offset: 0x00001E40
		// (set) Token: 0x0600010F RID: 271 RVA: 0x00003C48 File Offset: 0x00001E48
		public string AddItemName
		{
			get
			{
				return this.addItemName;
			}
			set
			{
				this.addItemName = value;
			}
		}

		/// <summary>Gets or sets the name for the &lt;clear&gt; configuration element.</summary>
		/// <returns>The name that replaces the standard name "clear" for the configuration item.</returns>
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00003C54 File Offset: 0x00001E54
		// (set) Token: 0x06000111 RID: 273 RVA: 0x00003C5C File Offset: 0x00001E5C
		public string ClearItemsName
		{
			get
			{
				return this.clearItemsName;
			}
			set
			{
				this.clearItemsName = value;
			}
		}

		/// <summary>Gets or sets the name for the &lt;remove&gt; configuration element.</summary>
		/// <returns>The name that replaces the standard name "remove" for the configuration element.</returns>
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00003C68 File Offset: 0x00001E68
		// (set) Token: 0x06000113 RID: 275 RVA: 0x00003C70 File Offset: 0x00001E70
		public string RemoveItemName
		{
			get
			{
				return this.removeItemName;
			}
			set
			{
				this.removeItemName = value;
			}
		}

		/// <summary>Gets or sets the type of the <see cref="T:System.Configuration.ConfigurationCollectionAttribute" /> attribute.</summary>
		/// <returns>The type of the <see cref="T:System.Configuration.ConfigurationCollectionAttribute" />.</returns>
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00003C7C File Offset: 0x00001E7C
		// (set) Token: 0x06000115 RID: 277 RVA: 0x00003C84 File Offset: 0x00001E84
		public ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return this.collectionType;
			}
			set
			{
				this.collectionType = value;
			}
		}

		/// <summary>Gets the type of the collection element.</summary>
		/// <returns>The type of the collection element.</returns>
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00003C90 File Offset: 0x00001E90
		[MonoInternalNote("Do something with this in ConfigurationElementCollection")]
		public Type ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		// Token: 0x04000051 RID: 81
		private string addItemName = "add";

		// Token: 0x04000052 RID: 82
		private string clearItemsName = "clear";

		// Token: 0x04000053 RID: 83
		private string removeItemName = "remove";

		// Token: 0x04000054 RID: 84
		private ConfigurationElementCollectionType collectionType;

		// Token: 0x04000055 RID: 85
		private Type itemType;
	}
}
