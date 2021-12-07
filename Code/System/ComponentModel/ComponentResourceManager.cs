using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace System.ComponentModel
{
	/// <summary>Provides simple functionality for enumerating resources for a component or object. The <see cref="T:System.ComponentModel.ComponentResourceManager" /> class is a <see cref="T:System.Resources.ResourceManager" />.</summary>
	// Token: 0x020000E0 RID: 224
	public class ComponentResourceManager : ResourceManager
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ComponentResourceManager" /> class with default values.</summary>
		// Token: 0x06000978 RID: 2424 RVA: 0x0001B76C File Offset: 0x0001996C
		public ComponentResourceManager()
		{
		}

		/// <summary>Creates a <see cref="T:System.ComponentModel.ComponentResourceManager" /> that looks up resources in satellite assemblies based on information from the specified <see cref="T:System.Type" />.</summary>
		/// <param name="t">A <see cref="T:System.Type" /> from which the <see cref="T:System.ComponentModel.ComponentResourceManager" /> derives all information for finding resource files. </param>
		// Token: 0x06000979 RID: 2425 RVA: 0x0001B774 File Offset: 0x00019974
		public ComponentResourceManager(Type t) : base(t)
		{
		}

		/// <summary>Applies a resource's value to the corresponding property of the object.</summary>
		/// <param name="value">An <see cref="T:System.Object" /> that contains the property value to be applied. </param>
		/// <param name="objectName">A <see cref="T:System.String" /> that contains the name of the object to look up in the resources. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> or <paramref name="objectName" /> is null.</exception>
		// Token: 0x0600097A RID: 2426 RVA: 0x0001B780 File Offset: 0x00019980
		public void ApplyResources(object value, string objectName)
		{
			this.ApplyResources(value, objectName, null);
		}

		/// <summary>Applies a resource's value to the corresponding property of the object.</summary>
		/// <param name="value">An <see cref="T:System.Object" /> that contains the property value to be applied. </param>
		/// <param name="objectName">A <see cref="T:System.String" /> that contains the name of the object to look up in the resources.</param>
		/// <param name="culture">The culture for which to apply resources.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> or <paramref name="objectName" /> is null.</exception>
		// Token: 0x0600097B RID: 2427 RVA: 0x0001B78C File Offset: 0x0001998C
		public virtual void ApplyResources(object value, string objectName, CultureInfo culture)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (objectName == null)
			{
				throw new ArgumentNullException("objectName");
			}
			if (culture == null)
			{
				culture = CultureInfo.CurrentUICulture;
			}
			Hashtable hashtable = (!this.IgnoreCase) ? new Hashtable() : System.Collections.Specialized.CollectionsUtil.CreateCaseInsensitiveHashtable();
			this.BuildResources(culture, hashtable);
			string text = objectName + ".";
			CompareOptions options = (!this.IgnoreCase) ? CompareOptions.None : CompareOptions.IgnoreCase;
			Type type = value.GetType();
			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;
			if (this.IgnoreCase)
			{
				bindingFlags |= BindingFlags.IgnoreCase;
			}
			foreach (object obj in hashtable)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string text2 = (string)dictionaryEntry.Key;
				if (culture.CompareInfo.IsPrefix(text2, text, options))
				{
					string name = text2.Substring(text.Length);
					PropertyInfo property = type.GetProperty(name, bindingFlags);
					if (property != null && property.CanWrite)
					{
						object value2 = dictionaryEntry.Value;
						if (value2 == null || property.PropertyType.IsInstanceOfType(value2))
						{
							property.SetValue(value, value2, null);
						}
					}
				}
			}
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0001B90C File Offset: 0x00019B0C
		private void BuildResources(CultureInfo culture, Hashtable resources)
		{
			if (culture != culture.Parent)
			{
				this.BuildResources(culture.Parent, resources);
			}
			ResourceSet resourceSet = this.GetResourceSet(culture, true, false);
			if (resourceSet != null)
			{
				foreach (object obj in resourceSet)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					resources[(string)dictionaryEntry.Key] = dictionaryEntry.Value;
				}
			}
		}
	}
}
