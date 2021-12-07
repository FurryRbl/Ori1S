using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Serialization;

namespace System.Configuration
{
	/// <summary>Represents a collection of related sections within a configuration file.</summary>
	// Token: 0x02000037 RID: 55
	[Serializable]
	public sealed class ConfigurationSectionCollection : NameObjectCollectionBase
	{
		// Token: 0x06000221 RID: 545 RVA: 0x00007658 File Offset: 0x00005858
		internal ConfigurationSectionCollection(Configuration config, SectionGroupInfo group) : base(StringComparer.Ordinal)
		{
			this.config = config;
			this.group = group;
		}

		/// <summary>Gets the keys to all <see cref="T:System.Configuration.ConfigurationSection" /> objects contained in this <see cref="T:System.Configuration.ConfigurationSectionCollection" /> object.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> object that contains the keys of all sections in this collection.</returns>
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00007674 File Offset: 0x00005874
		public override NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				return this.group.Sections.Keys;
			}
		}

		/// <summary>Gets the number of sections in this <see cref="T:System.Configuration.ConfigurationSectionCollection" /> object.</summary>
		/// <returns>An integer that represents the number of sections in the collection.</returns>
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00007688 File Offset: 0x00005888
		public override int Count
		{
			get
			{
				return this.group.Sections.Count;
			}
		}

		/// <summary>Gets the specified <see cref="T:System.Configuration.ConfigurationSection" /> object.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationSection" /> object with the specified name.</returns>
		/// <param name="name">The name of the <see cref="T:System.Configuration.ConfigurationSection" /> object to be returned. </param>
		// Token: 0x1700009D RID: 157
		public ConfigurationSection this[string name]
		{
			get
			{
				ConfigurationSection configurationSection = base.BaseGet(name) as ConfigurationSection;
				if (configurationSection == null)
				{
					SectionInfo sectionInfo = this.group.Sections[name] as SectionInfo;
					if (sectionInfo == null)
					{
						return null;
					}
					configurationSection = this.config.GetSectionInstance(sectionInfo, true);
					if (configurationSection == null)
					{
						return null;
					}
					base.BaseSet(name, configurationSection);
				}
				return configurationSection;
			}
		}

		/// <summary>Gets the specified <see cref="T:System.Configuration.ConfigurationSection" /> object.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationSection" /> object at the specified index.</returns>
		/// <param name="index">The index of the <see cref="T:System.Configuration.ConfigurationSection" /> object to be returned. </param>
		// Token: 0x1700009E RID: 158
		public ConfigurationSection this[int index]
		{
			get
			{
				return this[this.GetKey(index)];
			}
		}

		/// <summary>Adds a <see cref="T:System.Configuration.ConfigurationSection" /> object to the <see cref="T:System.Configuration.ConfigurationSectionCollection" /> object.</summary>
		/// <param name="name">The name of the section to be added.</param>
		/// <param name="section">The section to be added.</param>
		// Token: 0x06000226 RID: 550 RVA: 0x0000770C File Offset: 0x0000590C
		public void Add(string name, ConfigurationSection section)
		{
			this.config.CreateSection(this.group, name, section);
		}

		/// <summary>Clears this <see cref="T:System.Configuration.ConfigurationSectionCollection" /> object.</summary>
		// Token: 0x06000227 RID: 551 RVA: 0x00007724 File Offset: 0x00005924
		public void Clear()
		{
			if (this.group.Sections != null)
			{
				foreach (object obj in this.group.Sections)
				{
					ConfigInfo configInfo = (ConfigInfo)obj;
					this.config.RemoveConfigInfo(configInfo);
				}
			}
		}

		/// <summary>Copies this <see cref="T:System.Configuration.ConfigurationSectionCollection" /> object to an array.</summary>
		/// <param name="array">The array to copy the <see cref="T:System.Configuration.ConfigurationSectionCollection" /> object to.</param>
		/// <param name="index">The index location at which to begin copying.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="array" /> is less than the value of <see cref="P:System.Configuration.ConfigurationSectionCollection.Count" /> plus <paramref name="index" />.</exception>
		// Token: 0x06000228 RID: 552 RVA: 0x000077B0 File Offset: 0x000059B0
		public void CopyTo(ConfigurationSection[] array, int index)
		{
			for (int i = 0; i < this.group.Sections.Count; i++)
			{
				array[i + index] = this[i];
			}
		}

		/// <summary>Gets the specified <see cref="T:System.Configuration.ConfigurationSection" /> object contained in this <see cref="T:System.Configuration.ConfigurationSectionCollection" /> object.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationSection" /> object at the specified index.</returns>
		/// <param name="index">The index of the <see cref="T:System.Configuration.ConfigurationSection" /> object to be returned.</param>
		// Token: 0x06000229 RID: 553 RVA: 0x000077EC File Offset: 0x000059EC
		public ConfigurationSection Get(int index)
		{
			return this[index];
		}

		/// <summary>Gets the specified <see cref="T:System.Configuration.ConfigurationSection" /> object contained in this <see cref="T:System.Configuration.ConfigurationSectionCollection" /> object.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationSection" /> object with the specified name.</returns>
		/// <param name="name">The name of the <see cref="T:System.Configuration.ConfigurationSection" /> object to be returned.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is null or an empty string ("").</exception>
		// Token: 0x0600022A RID: 554 RVA: 0x000077F8 File Offset: 0x000059F8
		public ConfigurationSection Get(string name)
		{
			return this[name];
		}

		/// <summary>Gets an enumerator that can iterate through this <see cref="T:System.Configuration.ConfigurationSectionCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through this <see cref="T:System.Configuration.ConfigurationSectionCollection" /> object.</returns>
		// Token: 0x0600022B RID: 555 RVA: 0x00007804 File Offset: 0x00005A04
		public override IEnumerator GetEnumerator()
		{
			foreach (object obj in this.group.Sections.AllKeys)
			{
				string key = (string)obj;
				yield return this[key];
			}
			yield break;
		}

		/// <summary>Gets the key of the specified <see cref="T:System.Configuration.ConfigurationSection" /> object contained in this <see cref="T:System.Configuration.ConfigurationSectionCollection" /> object.</summary>
		/// <returns>The key of the <see cref="T:System.Configuration.ConfigurationSection" /> object at the specified index.</returns>
		/// <param name="index">The index of the <see cref="T:System.Configuration.ConfigurationSection" /> object whose key is to be returned. </param>
		// Token: 0x0600022C RID: 556 RVA: 0x00007820 File Offset: 0x00005A20
		public string GetKey(int index)
		{
			return this.group.Sections.GetKey(index);
		}

		/// <summary>Removes the specified <see cref="T:System.Configuration.ConfigurationSection" /> object from this <see cref="T:System.Configuration.ConfigurationSectionCollection" /> object.</summary>
		/// <param name="name">The name of the section to be removed. </param>
		// Token: 0x0600022D RID: 557 RVA: 0x00007834 File Offset: 0x00005A34
		public void Remove(string name)
		{
			SectionInfo sectionInfo = this.group.Sections[name] as SectionInfo;
			if (sectionInfo != null)
			{
				this.config.RemoveConfigInfo(sectionInfo);
			}
		}

		/// <summary>Removes the specified <see cref="T:System.Configuration.ConfigurationSection" /> object from this <see cref="T:System.Configuration.ConfigurationSectionCollection" /> object.</summary>
		/// <param name="index">The index of the section to be removed. </param>
		// Token: 0x0600022E RID: 558 RVA: 0x0000786C File Offset: 0x00005A6C
		public void RemoveAt(int index)
		{
			SectionInfo sectionInfo = this.group.Sections[index] as SectionInfo;
			this.config.RemoveConfigInfo(sectionInfo);
		}

		/// <summary>Used by the system during serialization.</summary>
		/// <param name="info">The applicable <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
		/// <param name="context">The applicable <see cref="T:System.Runtime.Serialization.StreamingContext" /> object.</param>
		// Token: 0x0600022F RID: 559 RVA: 0x0000789C File Offset: 0x00005A9C
		[MonoTODO]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040000B2 RID: 178
		private SectionGroupInfo group;

		// Token: 0x040000B3 RID: 179
		private Configuration config;
	}
}
