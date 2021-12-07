using System;
using System.Collections;
using System.Globalization;

namespace System.ComponentModel.Design
{
	/// <summary>Provides a base class for getting and setting option values for a designer.</summary>
	// Token: 0x020000FB RID: 251
	public abstract class DesignerOptionService : IDesignerOptionService
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerOptionService" /> class. </summary>
		// Token: 0x06000A2A RID: 2602 RVA: 0x0001CDB8 File Offset: 0x0001AFB8
		protected internal DesignerOptionService()
		{
		}

		/// <summary>Gets the value of an option defined in this package.</summary>
		/// <returns>The value of the option named <paramref name="valueName" />.</returns>
		/// <param name="pageName">The page to which the option is bound.</param>
		/// <param name="valueName">The name of the option value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pageName" /> or <paramref name="valueName" /> is null.</exception>
		// Token: 0x06000A2B RID: 2603 RVA: 0x0001CDC0 File Offset: 0x0001AFC0
		object IDesignerOptionService.GetOptionValue(string pageName, string valueName)
		{
			if (pageName == null)
			{
				throw new ArgumentNullException("pageName");
			}
			if (valueName == null)
			{
				throw new ArgumentNullException("valueName");
			}
			PropertyDescriptor optionProperty = this.GetOptionProperty(pageName, valueName);
			if (optionProperty != null)
			{
				return optionProperty.GetValue(null);
			}
			return null;
		}

		/// <summary>Sets the value of an option defined in this package.</summary>
		/// <param name="pageName">The page to which the option is bound</param>
		/// <param name="valueName">The name of the option value.</param>
		/// <param name="value">The value of the option.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pageName" /> or <paramref name="valueName" /> is null.</exception>
		// Token: 0x06000A2C RID: 2604 RVA: 0x0001CE08 File Offset: 0x0001B008
		void IDesignerOptionService.SetOptionValue(string pageName, string valueName, object value)
		{
			if (pageName == null)
			{
				throw new ArgumentNullException("pageName");
			}
			if (valueName == null)
			{
				throw new ArgumentNullException("valueName");
			}
			PropertyDescriptor optionProperty = this.GetOptionProperty(pageName, valueName);
			if (optionProperty != null)
			{
				optionProperty.SetValue(null, value);
			}
		}

		/// <summary>Creates a new <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" /> with the given name and adds it to the given parent. </summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" />. </returns>
		/// <param name="parent">The parent designer option collection. All collections have a parent except the root object collection.</param>
		/// <param name="name">The name of this collection.</param>
		/// <param name="value">The object providing properties for this collection. Can be null if the collection should not provide any properties.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="parent" /> or <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string.</exception>
		// Token: 0x06000A2D RID: 2605 RVA: 0x0001CE50 File Offset: 0x0001B050
		protected DesignerOptionService.DesignerOptionCollection CreateOptionCollection(DesignerOptionService.DesignerOptionCollection parent, string name, object value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}
			if (name == string.Empty)
			{
				throw new ArgumentException("name.Length == 0");
			}
			return new DesignerOptionService.DesignerOptionCollection(parent, name, value, this);
		}

		/// <summary>Shows the options dialog box for the given object.</summary>
		/// <returns>true if the dialog box is shown; otherwise, false.</returns>
		/// <param name="options">The options collection containing the object to be invoked.</param>
		/// <param name="optionObject">The actual options object.</param>
		// Token: 0x06000A2E RID: 2606 RVA: 0x0001CEA4 File Offset: 0x0001B0A4
		protected virtual bool ShowDialog(DesignerOptionService.DesignerOptionCollection options, object optionObject)
		{
			return false;
		}

		/// <summary>Populates a <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" />.</summary>
		/// <param name="options">The collection to populate.</param>
		// Token: 0x06000A2F RID: 2607 RVA: 0x0001CEA8 File Offset: 0x0001B0A8
		protected virtual void PopulateOptionCollection(DesignerOptionService.DesignerOptionCollection options)
		{
		}

		/// <summary>Gets the options collection for this service.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" /> populated with available designer options.</returns>
		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x0001CEAC File Offset: 0x0001B0AC
		public DesignerOptionService.DesignerOptionCollection Options
		{
			get
			{
				if (this._options == null)
				{
					this._options = new DesignerOptionService.DesignerOptionCollection(null, string.Empty, null, this);
				}
				return this._options;
			}
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0001CEE0 File Offset: 0x0001B0E0
		private PropertyDescriptor GetOptionProperty(string pageName, string valueName)
		{
			string[] array = pageName.Split(new char[]
			{
				'\\'
			});
			DesignerOptionService.DesignerOptionCollection designerOptionCollection = this.Options;
			foreach (string index in array)
			{
				designerOptionCollection = designerOptionCollection[index];
				if (designerOptionCollection == null)
				{
					return null;
				}
			}
			return designerOptionCollection.Properties[valueName];
		}

		// Token: 0x040002B8 RID: 696
		private DesignerOptionService.DesignerOptionCollection _options;

		/// <summary>Contains a collection of designer options. This class cannot be inherited.</summary>
		// Token: 0x020000FC RID: 252
		[TypeConverter(typeof(TypeConverter))]
		[Editor("", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[MonoTODO("implement own TypeConverter")]
		public sealed class DesignerOptionCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x06000A32 RID: 2610 RVA: 0x0001CF44 File Offset: 0x0001B144
			internal DesignerOptionCollection(DesignerOptionService.DesignerOptionCollection parent, string name, object propertiesProvider, DesignerOptionService service)
			{
				this._name = name;
				this._propertiesProvider = propertiesProvider;
				this._parent = parent;
				if (parent != null)
				{
					if (parent._children == null)
					{
						parent._children = new ArrayList();
					}
					parent._children.Add(this);
				}
				this._children = new ArrayList();
				this._optionService = service;
				service.PopulateOptionCollection(this);
			}

			/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
			/// <returns>true if the collection has a fixed size; otherwise, false.</returns>
			// Token: 0x17000250 RID: 592
			// (get) Token: 0x06000A33 RID: 2611 RVA: 0x0001CFB0 File Offset: 0x0001B1B0
			bool IList.IsFixedSize
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>true if the collection is read-only; otherwise, false.</returns>
			// Token: 0x17000251 RID: 593
			// (get) Token: 0x06000A34 RID: 2612 RVA: 0x0001CFB4 File Offset: 0x0001B1B4
			bool IList.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets or sets the element at the specified index.</summary>
			/// <returns>The element at the specified index.</returns>
			/// <param name="index">The zero-based index of the element to get or set.</param>
			// Token: 0x17000252 RID: 594
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			/// <summary>Gets a value indicating whether access to the collection is synchronized and, therefore, thread safe.</summary>
			/// <returns>true if the access to the collection is synchronized; otherwise, false.</returns>
			// Token: 0x17000253 RID: 595
			// (get) Token: 0x06000A37 RID: 2615 RVA: 0x0001CFCC File Offset: 0x0001B1CC
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
			/// <returns>An object that can be used to synchronize access to the collection.</returns>
			// Token: 0x17000254 RID: 596
			// (get) Token: 0x06000A38 RID: 2616 RVA: 0x0001CFD0 File Offset: 0x0001B1D0
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>Determines whether the collection contains a specific value.</summary>
			/// <returns>true if the <see cref="T:System.Object" /> is found in the collection; otherwise, false. </returns>
			/// <param name="value">The <see cref="T:System.Object" /> to locate in the collection</param>
			// Token: 0x06000A39 RID: 2617 RVA: 0x0001CFD4 File Offset: 0x0001B1D4
			bool IList.Contains(object item)
			{
				return this._children.Contains(item);
			}

			/// <summary>Determines the index of a specific item in the collection.</summary>
			/// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1.</returns>
			/// <param name="value">The <see cref="T:System.Object" /> to locate in the collection.</param>
			// Token: 0x06000A3A RID: 2618 RVA: 0x0001CFE4 File Offset: 0x0001B1E4
			int IList.IndexOf(object item)
			{
				return this._children.IndexOf(item);
			}

			/// <summary>Adds an item to the <see cref="T:System.Collections.IList" />.</summary>
			/// <returns>The position into which the new element was inserted.</returns>
			/// <param name="value">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
			// Token: 0x06000A3B RID: 2619 RVA: 0x0001CFF4 File Offset: 0x0001B1F4
			int IList.Add(object item)
			{
				throw new NotSupportedException();
			}

			/// <summary>Removes the first occurrence of a specific object from the collection.</summary>
			/// <param name="value">The <see cref="T:System.Object" /> to remove from the collection.</param>
			// Token: 0x06000A3C RID: 2620 RVA: 0x0001CFFC File Offset: 0x0001B1FC
			void IList.Remove(object item)
			{
				throw new NotSupportedException();
			}

			/// <summary>Removes the collection item at the specified index.</summary>
			/// <param name="index">The zero-based index of the item to remove.</param>
			// Token: 0x06000A3D RID: 2621 RVA: 0x0001D004 File Offset: 0x0001B204
			void IList.RemoveAt(int index)
			{
				throw new NotSupportedException();
			}

			/// <summary>Inserts an item into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
			/// <param name="value">The <see cref="T:System.Object" /> to insert into the collection.</param>
			// Token: 0x06000A3E RID: 2622 RVA: 0x0001D00C File Offset: 0x0001B20C
			void IList.Insert(int index, object item)
			{
				throw new NotSupportedException();
			}

			/// <summary>Removes all items from the collection.</summary>
			// Token: 0x06000A3F RID: 2623 RVA: 0x0001D014 File Offset: 0x0001B214
			void IList.Clear()
			{
				throw new NotSupportedException();
			}

			/// <summary>Displays a dialog box user interface (UI) with which the user can configure the options in this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" />.</summary>
			/// <returns>true if the dialog box can be displayed; otherwise, false.</returns>
			// Token: 0x06000A40 RID: 2624 RVA: 0x0001D01C File Offset: 0x0001B21C
			public bool ShowDialog()
			{
				return this._optionService.ShowDialog(this, this._propertiesProvider);
			}

			/// <summary>Gets the child collection at the given index.</summary>
			/// <returns>The child collection at the specified index.</returns>
			/// <param name="index">The zero-based index of the child collection to get.</param>
			// Token: 0x17000255 RID: 597
			public DesignerOptionService.DesignerOptionCollection this[int index]
			{
				get
				{
					return (DesignerOptionService.DesignerOptionCollection)this._children[index];
				}
			}

			/// <summary>Gets the child collection at the given name.</summary>
			/// <returns>The child collection with the name specified by the <paramref name="name" /> parameter, or null if the name is not found.</returns>
			/// <param name="name">The name of the child collection.</param>
			// Token: 0x17000256 RID: 598
			public DesignerOptionService.DesignerOptionCollection this[string index]
			{
				get
				{
					foreach (object obj in this._children)
					{
						DesignerOptionService.DesignerOptionCollection designerOptionCollection = (DesignerOptionService.DesignerOptionCollection)obj;
						if (string.Compare(designerOptionCollection.Name, index, true, CultureInfo.InvariantCulture) == 0)
						{
							return designerOptionCollection;
						}
					}
					return null;
				}
			}

			/// <summary>Gets the name of this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" />.</summary>
			/// <returns>The name of this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" />.</returns>
			// Token: 0x17000257 RID: 599
			// (get) Token: 0x06000A43 RID: 2627 RVA: 0x0001D0D0 File Offset: 0x0001B2D0
			public string Name
			{
				get
				{
					return this._name;
				}
			}

			/// <summary>Gets the number of child option collections this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" /> contains.</summary>
			/// <returns>The number of child option collections this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" /> contains.</returns>
			// Token: 0x17000258 RID: 600
			// (get) Token: 0x06000A44 RID: 2628 RVA: 0x0001D0D8 File Offset: 0x0001B2D8
			public int Count
			{
				get
				{
					if (this._children != null)
					{
						return this._children.Count;
					}
					return 0;
				}
			}

			/// <summary>Gets the parent collection object.</summary>
			/// <returns>The parent collection object, or null if there is no parent.</returns>
			// Token: 0x17000259 RID: 601
			// (get) Token: 0x06000A45 RID: 2629 RVA: 0x0001D0F4 File Offset: 0x0001B2F4
			public DesignerOptionService.DesignerOptionCollection Parent
			{
				get
				{
					return this._parent;
				}
			}

			/// <summary>Gets the collection of properties offered by this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" />, along with all of its children.</summary>
			/// <returns>The collection of properties offered by this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" />, along with all of its children.</returns>
			// Token: 0x1700025A RID: 602
			// (get) Token: 0x06000A46 RID: 2630 RVA: 0x0001D0FC File Offset: 0x0001B2FC
			public PropertyDescriptorCollection Properties
			{
				get
				{
					PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this._propertiesProvider);
					ArrayList arrayList = new ArrayList(properties.Count);
					foreach (object obj in properties)
					{
						PropertyDescriptor property = (PropertyDescriptor)obj;
						arrayList.Add(new DesignerOptionService.DesignerOptionCollection.WrappedPropertyDescriptor(property, this._propertiesProvider));
					}
					PropertyDescriptor[] properties2 = (PropertyDescriptor[])arrayList.ToArray(typeof(PropertyDescriptor));
					return new PropertyDescriptorCollection(properties2);
				}
			}

			/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate this collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate this collection.</returns>
			// Token: 0x06000A47 RID: 2631 RVA: 0x0001D1AC File Offset: 0x0001B3AC
			public IEnumerator GetEnumerator()
			{
				return this._children.GetEnumerator();
			}

			/// <summary>Returns the index of the first occurrence of a given value in a range of this collection.</summary>
			/// <returns>The index of the first occurrence of value within the entire collection, if found; otherwise, the lower bound of the collection minus 1.</returns>
			/// <param name="value">The object to locate in the collection.</param>
			// Token: 0x06000A48 RID: 2632 RVA: 0x0001D1BC File Offset: 0x0001B3BC
			public int IndexOf(DesignerOptionService.DesignerOptionCollection item)
			{
				return this._children.IndexOf(item);
			}

			/// <summary>Copies the entire collection to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
			/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the collection. The <paramref name="array" /> must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			// Token: 0x06000A49 RID: 2633 RVA: 0x0001D1CC File Offset: 0x0001B3CC
			public void CopyTo(Array array, int index)
			{
				this._children.CopyTo(array, index);
			}

			// Token: 0x040002B9 RID: 697
			private string _name;

			// Token: 0x040002BA RID: 698
			private object _propertiesProvider;

			// Token: 0x040002BB RID: 699
			private DesignerOptionService.DesignerOptionCollection _parent;

			// Token: 0x040002BC RID: 700
			private ArrayList _children;

			// Token: 0x040002BD RID: 701
			private DesignerOptionService _optionService;

			// Token: 0x020000FD RID: 253
			public sealed class WrappedPropertyDescriptor : PropertyDescriptor
			{
				// Token: 0x06000A4A RID: 2634 RVA: 0x0001D1DC File Offset: 0x0001B3DC
				public WrappedPropertyDescriptor(PropertyDescriptor property, object component) : base(property.Name, new Attribute[0])
				{
					this._property = property;
					this._component = component;
				}

				// Token: 0x06000A4B RID: 2635 RVA: 0x0001D20C File Offset: 0x0001B40C
				public override object GetValue(object ignored)
				{
					return this._property.GetValue(this._component);
				}

				// Token: 0x06000A4C RID: 2636 RVA: 0x0001D220 File Offset: 0x0001B420
				public override void SetValue(object ignored, object value)
				{
					this._property.SetValue(this._component, value);
				}

				// Token: 0x06000A4D RID: 2637 RVA: 0x0001D234 File Offset: 0x0001B434
				public override bool CanResetValue(object ignored)
				{
					return this._property.CanResetValue(this._component);
				}

				// Token: 0x06000A4E RID: 2638 RVA: 0x0001D248 File Offset: 0x0001B448
				public override void ResetValue(object ignored)
				{
					this._property.ResetValue(this._component);
				}

				// Token: 0x06000A4F RID: 2639 RVA: 0x0001D25C File Offset: 0x0001B45C
				public override bool ShouldSerializeValue(object ignored)
				{
					return this._property.ShouldSerializeValue(this._component);
				}

				// Token: 0x1700025B RID: 603
				// (get) Token: 0x06000A50 RID: 2640 RVA: 0x0001D270 File Offset: 0x0001B470
				public override AttributeCollection Attributes
				{
					get
					{
						return this._property.Attributes;
					}
				}

				// Token: 0x1700025C RID: 604
				// (get) Token: 0x06000A51 RID: 2641 RVA: 0x0001D280 File Offset: 0x0001B480
				public override bool IsReadOnly
				{
					get
					{
						return this._property.IsReadOnly;
					}
				}

				// Token: 0x1700025D RID: 605
				// (get) Token: 0x06000A52 RID: 2642 RVA: 0x0001D290 File Offset: 0x0001B490
				public override Type ComponentType
				{
					get
					{
						return this._property.ComponentType;
					}
				}

				// Token: 0x1700025E RID: 606
				// (get) Token: 0x06000A53 RID: 2643 RVA: 0x0001D2A0 File Offset: 0x0001B4A0
				public override Type PropertyType
				{
					get
					{
						return this._property.PropertyType;
					}
				}

				// Token: 0x040002BE RID: 702
				private PropertyDescriptor _property;

				// Token: 0x040002BF RID: 703
				private object _component;
			}
		}
	}
}
