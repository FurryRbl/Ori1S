using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;

namespace System.Configuration
{
	// Token: 0x02000022 RID: 34
	internal class ElementMap
	{
		// Token: 0x06000149 RID: 329 RVA: 0x00004D84 File Offset: 0x00002F84
		public ElementMap(Type t)
		{
			this.properties = new ConfigurationPropertyCollection();
			this.collectionAttribute = (Attribute.GetCustomAttribute(t, typeof(ConfigurationCollectionAttribute)) as ConfigurationCollectionAttribute);
			PropertyInfo[] array = t.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			foreach (PropertyInfo propertyInfo in array)
			{
				ConfigurationPropertyAttribute configurationPropertyAttribute = Attribute.GetCustomAttribute(propertyInfo, typeof(ConfigurationPropertyAttribute)) as ConfigurationPropertyAttribute;
				if (configurationPropertyAttribute != null)
				{
					string name = (configurationPropertyAttribute.Name == null) ? propertyInfo.Name : configurationPropertyAttribute.Name;
					ConfigurationValidatorAttribute configurationValidatorAttribute = Attribute.GetCustomAttribute(propertyInfo, typeof(ConfigurationValidatorAttribute)) as ConfigurationValidatorAttribute;
					ConfigurationValidatorBase validation = (configurationValidatorAttribute == null) ? null : configurationValidatorAttribute.ValidatorInstance;
					TypeConverterAttribute typeConverterAttribute = (TypeConverterAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(TypeConverterAttribute));
					TypeConverter converter = (typeConverterAttribute == null) ? null : ((TypeConverter)Activator.CreateInstance(Type.GetType(typeConverterAttribute.ConverterTypeName), true));
					ConfigurationProperty configurationProperty = new ConfigurationProperty(name, propertyInfo.PropertyType, configurationPropertyAttribute.DefaultValue, converter, validation, configurationPropertyAttribute.Options);
					configurationProperty.CollectionAttribute = (Attribute.GetCustomAttribute(propertyInfo, typeof(ConfigurationCollectionAttribute)) as ConfigurationCollectionAttribute);
					this.properties.Add(configurationProperty);
				}
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00004EEC File Offset: 0x000030EC
		public static ElementMap GetMap(Type t)
		{
			ElementMap elementMap = ElementMap.elementMaps[t] as ElementMap;
			if (elementMap != null)
			{
				return elementMap;
			}
			elementMap = new ElementMap(t);
			ElementMap.elementMaps[t] = elementMap;
			return elementMap;
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00004F28 File Offset: 0x00003128
		public ConfigurationCollectionAttribute CollectionAttribute
		{
			get
			{
				return this.collectionAttribute;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00004F30 File Offset: 0x00003130
		public bool HasProperties
		{
			get
			{
				return this.properties.Count > 0;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00004F40 File Offset: 0x00003140
		public ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04000064 RID: 100
		private static readonly Hashtable elementMaps = Hashtable.Synchronized(new Hashtable());

		// Token: 0x04000065 RID: 101
		private readonly ConfigurationPropertyCollection properties;

		// Token: 0x04000066 RID: 102
		private readonly ConfigurationCollectionAttribute collectionAttribute;
	}
}
