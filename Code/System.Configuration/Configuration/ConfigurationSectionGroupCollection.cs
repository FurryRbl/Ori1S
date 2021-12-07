using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Serialization;

namespace System.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Configuration.ConfigurationSectionGroup" /> objects.</summary>
	// Token: 0x02000039 RID: 57
	[Serializable]
	public sealed class ConfigurationSectionGroupCollection : NameObjectCollectionBase
	{
		// Token: 0x0600023E RID: 574 RVA: 0x000079E0 File Offset: 0x00005BE0
		internal ConfigurationSectionGroupCollection(Configuration config, SectionGroupInfo group) : base(StringComparer.Ordinal)
		{
			this.config = config;
			this.group = group;
		}

		/// <summary>Gets the keys to all <see cref="T:System.Configuration.ConfigurationSectionGroup" /> objects contained in this <see cref="T:System.Configuration.ConfigurationSectionGroupCollection" /> object.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> object that contains the names of all section groups in this collection.</returns>
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600023F RID: 575 RVA: 0x000079FC File Offset: 0x00005BFC
		public override NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				return this.group.Groups.Keys;
			}
		}

		/// <summary>Gets the number of section groups in the collection.</summary>
		/// <returns>An integer that represents the number of section groups in the collection.</returns>
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00007A10 File Offset: 0x00005C10
		public override int Count
		{
			get
			{
				return this.group.Groups.Count;
			}
		}

		/// <summary>Gets the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object whose name is specified from the collection.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object with the specified name.In C#, this property is the indexer for the <see cref="T:System.Configuration.ConfigurationSectionCollection" /> class. </returns>
		/// <param name="name">The name of the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object to be returned. </param>
		// Token: 0x170000A9 RID: 169
		public ConfigurationSectionGroup this[string name]
		{
			get
			{
				ConfigurationSectionGroup configurationSectionGroup = base.BaseGet(name) as ConfigurationSectionGroup;
				if (configurationSectionGroup == null)
				{
					SectionGroupInfo sectionGroupInfo = this.group.Groups[name] as SectionGroupInfo;
					if (sectionGroupInfo == null)
					{
						return null;
					}
					configurationSectionGroup = this.config.GetSectionGroupInstance(sectionGroupInfo);
					base.BaseSet(name, configurationSectionGroup);
				}
				return configurationSectionGroup;
			}
		}

		/// <summary>Gets the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object whose index is specified from the collection.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object at the specified index.In C#, this property is the indexer for the <see cref="T:System.Configuration.ConfigurationSectionCollection" /> class. </returns>
		/// <param name="index">The index of the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object to be returned. </param>
		// Token: 0x170000AA RID: 170
		public ConfigurationSectionGroup this[int index]
		{
			get
			{
				return this[this.GetKey(index)];
			}
		}

		/// <summary>Adds a <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object to this <see cref="T:System.Configuration.ConfigurationSectionGroupCollection" /> object.</summary>
		/// <param name="name">The name of the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object to be added.</param>
		/// <param name="sectionGroup">The <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object to be added.</param>
		// Token: 0x06000243 RID: 579 RVA: 0x00007A8C File Offset: 0x00005C8C
		public void Add(string name, ConfigurationSectionGroup sectionGroup)
		{
			this.config.CreateSectionGroup(this.group, name, sectionGroup);
		}

		/// <summary>Clears the collection.</summary>
		// Token: 0x06000244 RID: 580 RVA: 0x00007AA4 File Offset: 0x00005CA4
		public void Clear()
		{
			if (this.group.Groups != null)
			{
				foreach (object obj in this.group.Groups)
				{
					ConfigInfo configInfo = (ConfigInfo)obj;
					this.config.RemoveConfigInfo(configInfo);
				}
			}
		}

		/// <summary>Copies this <see cref="T:System.Configuration.ConfigurationSectionGroupCollection" /> object to an array.</summary>
		/// <param name="array">The array to copy the object to.</param>
		/// <param name="index">The index location at which to begin copying.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="array" /> is less than the value of <see cref="P:System.Configuration.ConfigurationSectionGroupCollection.Count" /> plus <paramref name="index" />.</exception>
		// Token: 0x06000245 RID: 581 RVA: 0x00007B30 File Offset: 0x00005D30
		public void CopyTo(ConfigurationSectionGroup[] array, int index)
		{
			for (int i = 0; i < this.group.Groups.Count; i++)
			{
				array[i + index] = this[i];
			}
		}

		/// <summary>Gets the specified <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object contained in the collection.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object at the specified index.</returns>
		/// <param name="index">The index of the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object to be returned. </param>
		// Token: 0x06000246 RID: 582 RVA: 0x00007B6C File Offset: 0x00005D6C
		public ConfigurationSectionGroup Get(int index)
		{
			return this[index];
		}

		/// <summary>Gets the specified <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object from the collection.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object with the specified name.</returns>
		/// <param name="name">The name of the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object to be returned. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is null or an empty string ("").</exception>
		// Token: 0x06000247 RID: 583 RVA: 0x00007B78 File Offset: 0x00005D78
		public ConfigurationSectionGroup Get(string name)
		{
			return this[name];
		}

		/// <summary>Gets an enumerator that can iterate through the <see cref="T:System.Configuration.ConfigurationSectionGroupCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the <see cref="T:System.Configuration.ConfigurationSectionGroupCollection" /> object.</returns>
		// Token: 0x06000248 RID: 584 RVA: 0x00007B84 File Offset: 0x00005D84
		public override IEnumerator GetEnumerator()
		{
			return this.group.Groups.AllKeys.GetEnumerator();
		}

		/// <summary>Gets the key of the specified <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object contained in this <see cref="T:System.Configuration.ConfigurationSectionGroupCollection" /> object.</summary>
		/// <returns>The key of the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object at the specified index.</returns>
		/// <param name="index">The index of the section group whose key is to be returned. </param>
		// Token: 0x06000249 RID: 585 RVA: 0x00007B9C File Offset: 0x00005D9C
		public string GetKey(int index)
		{
			return this.group.Groups.GetKey(index);
		}

		/// <summary>Removes the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object whose name is specified from this <see cref="T:System.Configuration.ConfigurationSectionGroupCollection" /> object.</summary>
		/// <param name="name">The name of the section group to be removed. </param>
		// Token: 0x0600024A RID: 586 RVA: 0x00007BB0 File Offset: 0x00005DB0
		public void Remove(string name)
		{
			SectionGroupInfo sectionGroupInfo = this.group.Groups[name] as SectionGroupInfo;
			if (sectionGroupInfo != null)
			{
				this.config.RemoveConfigInfo(sectionGroupInfo);
			}
		}

		/// <summary>Removes the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object whose index is specified from this <see cref="T:System.Configuration.ConfigurationSectionGroupCollection" /> object.</summary>
		/// <param name="index">The index of the section group to be removed. </param>
		// Token: 0x0600024B RID: 587 RVA: 0x00007BE8 File Offset: 0x00005DE8
		public void RemoveAt(int index)
		{
			SectionGroupInfo sectionGroupInfo = this.group.Groups[index] as SectionGroupInfo;
			this.config.RemoveConfigInfo(sectionGroupInfo);
		}

		/// <summary>Used by the system during serialization.</summary>
		/// <param name="info">The applicable <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
		/// <param name="context">The applicable <see cref="T:System.Runtime.Serialization.StreamingContext" /> object.</param>
		// Token: 0x0600024C RID: 588 RVA: 0x00007C18 File Offset: 0x00005E18
		[MonoTODO]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040000BD RID: 189
		private SectionGroupInfo group;

		// Token: 0x040000BE RID: 190
		private Configuration config;
	}
}
