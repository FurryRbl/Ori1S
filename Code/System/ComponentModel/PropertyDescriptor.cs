using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	/// <summary>Provides an abstraction of a property on a class.</summary>
	// Token: 0x02000197 RID: 407
	[ComVisible(true)]
	public abstract class PropertyDescriptor : MemberDescriptor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> class with the name and attributes in the specified <see cref="T:System.ComponentModel.MemberDescriptor" />.</summary>
		/// <param name="descr">A <see cref="T:System.ComponentModel.MemberDescriptor" /> that contains the name of the property and its attributes. </param>
		// Token: 0x06000E53 RID: 3667 RVA: 0x00024C88 File Offset: 0x00022E88
		protected PropertyDescriptor(MemberDescriptor reference) : base(reference)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> class with the name in the specified <see cref="T:System.ComponentModel.MemberDescriptor" /> and the attributes in both the <see cref="T:System.ComponentModel.MemberDescriptor" /> and the <see cref="T:System.Attribute" /> array.</summary>
		/// <param name="descr">A <see cref="T:System.ComponentModel.MemberDescriptor" /> containing the name of the member and its attributes. </param>
		/// <param name="attrs">An <see cref="T:System.Attribute" /> array containing the attributes you want to associate with the property. </param>
		// Token: 0x06000E54 RID: 3668 RVA: 0x00024C94 File Offset: 0x00022E94
		protected PropertyDescriptor(MemberDescriptor reference, Attribute[] attrs) : base(reference, attrs)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> class with the specified name and attributes.</summary>
		/// <param name="name">The name of the property. </param>
		/// <param name="attrs">An array of type <see cref="T:System.Attribute" /> that contains the property attributes. </param>
		// Token: 0x06000E55 RID: 3669 RVA: 0x00024CA0 File Offset: 0x00022EA0
		protected PropertyDescriptor(string name, Attribute[] attrs) : base(name, attrs)
		{
		}

		/// <summary>When overridden in a derived class, gets the type of the component this property is bound to.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type of component this property is bound to. When the <see cref="M:System.ComponentModel.PropertyDescriptor.GetValue(System.Object)" /> or <see cref="M:System.ComponentModel.PropertyDescriptor.SetValue(System.Object,System.Object)" /> methods are invoked, the object specified might be an instance of this type.</returns>
		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000E56 RID: 3670
		public abstract Type ComponentType { get; }

		/// <summary>Gets the type converter for this property.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> that is used to convert the <see cref="T:System.Type" /> of this property.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000E57 RID: 3671 RVA: 0x00024CAC File Offset: 0x00022EAC
		public virtual TypeConverter Converter
		{
			get
			{
				if (this.converter == null && this.PropertyType != null)
				{
					TypeConverterAttribute typeConverterAttribute = (TypeConverterAttribute)this.Attributes[typeof(TypeConverterAttribute)];
					if (typeConverterAttribute != null && typeConverterAttribute != TypeConverterAttribute.Default)
					{
						Type typeFromName = this.GetTypeFromName(typeConverterAttribute.ConverterTypeName);
						if (typeFromName != null && typeof(TypeConverter).IsAssignableFrom(typeFromName))
						{
							this.converter = (TypeConverter)this.CreateInstance(typeFromName);
						}
					}
					if (this.converter == null)
					{
						this.converter = TypeDescriptor.GetConverter(this.PropertyType);
					}
				}
				return this.converter;
			}
		}

		/// <summary>Gets a value indicating whether this property should be localized, as specified in the <see cref="T:System.ComponentModel.LocalizableAttribute" />.</summary>
		/// <returns>true if the member is marked with the <see cref="T:System.ComponentModel.LocalizableAttribute" /> set to true; otherwise, false.</returns>
		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000E58 RID: 3672 RVA: 0x00024D58 File Offset: 0x00022F58
		public virtual bool IsLocalizable
		{
			get
			{
				foreach (Attribute attribute in this.AttributeArray)
				{
					if (attribute is LocalizableAttribute)
					{
						return ((LocalizableAttribute)attribute).IsLocalizable;
					}
				}
				return false;
			}
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether this property is read-only.</summary>
		/// <returns>true if the property is read-only; otherwise, false.</returns>
		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000E59 RID: 3673
		public abstract bool IsReadOnly { get; }

		/// <summary>When overridden in a derived class, gets the type of the property.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type of the property.</returns>
		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000E5A RID: 3674
		public abstract Type PropertyType { get; }

		/// <summary>Gets a value indicating whether value change notifications for this property may originate from outside the property descriptor.</summary>
		/// <returns>true if value change notifications may originate from outside the property descriptor; otherwise, false.</returns>
		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000E5B RID: 3675 RVA: 0x00024D9C File Offset: 0x00022F9C
		public virtual bool SupportsChangeEvents
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether this property should be serialized, as specified in the <see cref="T:System.ComponentModel.DesignerSerializationVisibilityAttribute" />.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.DesignerSerializationVisibility" /> enumeration values that specifies whether this property should be serialized.</returns>
		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000E5C RID: 3676 RVA: 0x00024DA0 File Offset: 0x00022FA0
		public DesignerSerializationVisibility SerializationVisibility
		{
			get
			{
				foreach (Attribute attribute in this.AttributeArray)
				{
					if (attribute is DesignerSerializationVisibilityAttribute)
					{
						DesignerSerializationVisibilityAttribute designerSerializationVisibilityAttribute = (DesignerSerializationVisibilityAttribute)attribute;
						return designerSerializationVisibilityAttribute.Visibility;
					}
				}
				return DesignerSerializationVisibility.Visible;
			}
		}

		/// <summary>Enables other objects to be notified when this property changes.</summary>
		/// <param name="component">The component to add the handler for. </param>
		/// <param name="handler">The delegate to add as a listener. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> or <paramref name="handler" /> is null.</exception>
		// Token: 0x06000E5D RID: 3677 RVA: 0x00024DE8 File Offset: 0x00022FE8
		public virtual void AddValueChanged(object component, EventHandler handler)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			if (this.notifiers == null)
			{
				this.notifiers = new Hashtable();
			}
			EventHandler eventHandler = (EventHandler)this.notifiers[component];
			if (eventHandler != null)
			{
				eventHandler = (EventHandler)Delegate.Combine(eventHandler, handler);
				this.notifiers[component] = eventHandler;
			}
			else
			{
				this.notifiers[component] = handler;
			}
		}

		/// <summary>Enables other objects to be notified when this property changes.</summary>
		/// <param name="component">The component to remove the handler for. </param>
		/// <param name="handler">The delegate to remove as a listener. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> or <paramref name="handler" /> is null.</exception>
		// Token: 0x06000E5E RID: 3678 RVA: 0x00024E74 File Offset: 0x00023074
		public virtual void RemoveValueChanged(object component, EventHandler handler)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			if (this.notifiers == null)
			{
				return;
			}
			EventHandler eventHandler = (EventHandler)this.notifiers[component];
			eventHandler = (EventHandler)Delegate.Remove(eventHandler, handler);
			if (eventHandler == null)
			{
				this.notifiers.Remove(component);
			}
			else
			{
				this.notifiers[component] = eventHandler;
			}
		}

		/// <summary>Adds the attributes of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to the specified list of attributes in the parent class.</summary>
		/// <param name="attributeList">An <see cref="T:System.Collections.IList" /> that lists the attributes in the parent class. Initially, this is empty.</param>
		// Token: 0x06000E5F RID: 3679 RVA: 0x00024EF4 File Offset: 0x000230F4
		protected override void FillAttributes(IList attributeList)
		{
			base.FillAttributes(attributeList);
		}

		/// <summary>This method returns the object that should be used during invocation of members.</summary>
		/// <returns>The <see cref="T:System.Object" /> that should be used during invocation of members.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> of the invocation target.</param>
		/// <param name="instance">The potential invocation target.</param>
		// Token: 0x06000E60 RID: 3680 RVA: 0x00024F00 File Offset: 0x00023100
		protected override object GetInvocationTarget(Type type, object instance)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			if (instance is CustomTypeDescriptor)
			{
				CustomTypeDescriptor customTypeDescriptor = (CustomTypeDescriptor)instance;
				return customTypeDescriptor.GetPropertyOwner(this);
			}
			return base.GetInvocationTarget(type, instance);
		}

		/// <summary>Retrieves the current set of ValueChanged event handlers for a specific component</summary>
		/// <returns>A combined multicast event handler, or null if no event handlers are currently assigned to <paramref name="component" />.</returns>
		/// <param name="component">The component for which to retrieve event handlers.</param>
		// Token: 0x06000E61 RID: 3681 RVA: 0x00024F54 File Offset: 0x00023154
		protected internal EventHandler GetValueChangedHandler(object component)
		{
			if (component == null || this.notifiers == null)
			{
				return null;
			}
			return (EventHandler)this.notifiers[component];
		}

		/// <summary>Raises the ValueChanged event that you implemented.</summary>
		/// <param name="component">The object that raises the event. </param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000E62 RID: 3682 RVA: 0x00024F88 File Offset: 0x00023188
		protected virtual void OnValueChanged(object component, EventArgs e)
		{
			if (this.notifiers == null)
			{
				return;
			}
			EventHandler eventHandler = (EventHandler)this.notifiers[component];
			if (eventHandler == null)
			{
				return;
			}
			eventHandler(component, e);
		}

		/// <summary>When overridden in a derived class, gets the current value of the property on a component.</summary>
		/// <returns>The value of a property for a given component.</returns>
		/// <param name="component">The component with the property for which to retrieve the value. </param>
		// Token: 0x06000E63 RID: 3683
		public abstract object GetValue(object component);

		/// <summary>When overridden in a derived class, sets the value of the component to a different value.</summary>
		/// <param name="component">The component with the property value that is to be set. </param>
		/// <param name="value">The new value. </param>
		// Token: 0x06000E64 RID: 3684
		public abstract void SetValue(object component, object value);

		/// <summary>When overridden in a derived class, resets the value for this property of the component to the default value.</summary>
		/// <param name="component">The component with the property value that is to be reset to the default value. </param>
		// Token: 0x06000E65 RID: 3685
		public abstract void ResetValue(object component);

		/// <summary>When overridden in a derived class, returns whether resetting an object changes its value.</summary>
		/// <returns>true if resetting the component changes its value; otherwise, false.</returns>
		/// <param name="component">The component to test for reset capability. </param>
		// Token: 0x06000E66 RID: 3686
		public abstract bool CanResetValue(object component);

		/// <summary>When overridden in a derived class, determines a value indicating whether the value of this property needs to be persisted.</summary>
		/// <returns>true if the property should be persisted; otherwise, false.</returns>
		/// <param name="component">The component with the property to be examined for persistence. </param>
		// Token: 0x06000E67 RID: 3687
		public abstract bool ShouldSerializeValue(object component);

		/// <summary>Creates an instance of the specified type.</summary>
		/// <returns>A new instance of the type.</returns>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type to create. </param>
		// Token: 0x06000E68 RID: 3688 RVA: 0x00024FC4 File Offset: 0x000231C4
		protected object CreateInstance(Type type)
		{
			if (type == null || this.PropertyType == null)
			{
				return null;
			}
			Type[] array = new Type[]
			{
				typeof(Type)
			};
			ConstructorInfo constructor = type.GetConstructor(array);
			object result;
			if (constructor != null)
			{
				object[] args = new object[]
				{
					this.PropertyType
				};
				result = TypeDescriptor.CreateInstance(null, type, array, args);
			}
			else
			{
				result = TypeDescriptor.CreateInstance(null, type, null, null);
			}
			return result;
		}

		/// <summary>Compares this to another object to see if they are equivalent.</summary>
		/// <returns>true if the values are equivalent; otherwise, false.</returns>
		/// <param name="obj">The object to compare to this <see cref="T:System.ComponentModel.PropertyDescriptor" />. </param>
		// Token: 0x06000E69 RID: 3689 RVA: 0x00025034 File Offset: 0x00023234
		public override bool Equals(object obj)
		{
			if (!base.Equals(obj))
			{
				return false;
			}
			PropertyDescriptor propertyDescriptor = obj as PropertyDescriptor;
			return propertyDescriptor != null && propertyDescriptor.PropertyType == this.PropertyType;
		}

		/// <summary>Returns the default <see cref="T:System.ComponentModel.PropertyDescriptorCollection" />.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E6A RID: 3690 RVA: 0x0002506C File Offset: 0x0002326C
		public PropertyDescriptorCollection GetChildProperties()
		{
			return this.GetChildProperties(null, null);
		}

		/// <summary>Returns a <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> for a given object.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties for the specified component.</returns>
		/// <param name="instance">A component to get the properties for. </param>
		// Token: 0x06000E6B RID: 3691 RVA: 0x00025078 File Offset: 0x00023278
		public PropertyDescriptorCollection GetChildProperties(object instance)
		{
			return this.GetChildProperties(instance, null);
		}

		/// <summary>Returns a <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> using a specified array of attributes as a filter.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that match the specified attributes.</returns>
		/// <param name="filter">An array of type <see cref="T:System.Attribute" /> to use as a filter. </param>
		// Token: 0x06000E6C RID: 3692 RVA: 0x00025084 File Offset: 0x00023284
		public PropertyDescriptorCollection GetChildProperties(Attribute[] filter)
		{
			return this.GetChildProperties(null, filter);
		}

		/// <summary>Returns the hash code for this object.</summary>
		/// <returns>The hash code for this object.</returns>
		// Token: 0x06000E6D RID: 3693 RVA: 0x00025090 File Offset: 0x00023290
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Returns a <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> for a given object using a specified array of attributes as a filter.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that match the specified attributes for the specified component.</returns>
		/// <param name="instance">A component to get the properties for. </param>
		/// <param name="filter">An array of type <see cref="T:System.Attribute" /> to use as a filter. </param>
		// Token: 0x06000E6E RID: 3694 RVA: 0x00025098 File Offset: 0x00023298
		public virtual PropertyDescriptorCollection GetChildProperties(object instance, Attribute[] filter)
		{
			return TypeDescriptor.GetProperties(instance, filter);
		}

		/// <summary>Gets an editor of the specified type.</summary>
		/// <returns>An instance of the requested editor type, or null if an editor cannot be found.</returns>
		/// <param name="editorBaseType">The base type of editor, which is used to differentiate between multiple editors that a property supports. </param>
		// Token: 0x06000E6F RID: 3695 RVA: 0x000250A4 File Offset: 0x000232A4
		public virtual object GetEditor(Type editorBaseType)
		{
			Type type = null;
			Attribute[] attributeArray = this.AttributeArray;
			if (attributeArray != null && attributeArray.Length != 0)
			{
				foreach (Attribute attribute in attributeArray)
				{
					EditorAttribute editorAttribute = attribute as EditorAttribute;
					if (editorAttribute != null)
					{
						type = this.GetTypeFromName(editorAttribute.EditorTypeName);
						if (type != null && type.IsSubclassOf(editorBaseType))
						{
							break;
						}
					}
				}
			}
			object obj = null;
			if (type != null)
			{
				obj = this.CreateInstance(type);
			}
			if (obj == null)
			{
				obj = TypeDescriptor.GetEditor(this.PropertyType, editorBaseType);
			}
			return obj;
		}

		/// <summary>Returns a type using its name.</summary>
		/// <returns>A <see cref="T:System.Type" /> that matches the given type name, or null if a match cannot be found.</returns>
		/// <param name="typeName">The assembly-qualified name of the type to retrieve. </param>
		// Token: 0x06000E70 RID: 3696 RVA: 0x0002514C File Offset: 0x0002334C
		protected Type GetTypeFromName(string typeName)
		{
			if (typeName == null || this.ComponentType == null || typeName.Trim().Length == 0)
			{
				return null;
			}
			Type type = Type.GetType(typeName);
			if (type == null)
			{
				int num = typeName.IndexOf(",");
				if (num != -1)
				{
					typeName = typeName.Substring(0, num);
				}
				type = this.ComponentType.Assembly.GetType(typeName);
			}
			return type;
		}

		// Token: 0x04000405 RID: 1029
		private TypeConverter converter;

		// Token: 0x04000406 RID: 1030
		private Hashtable notifiers;
	}
}
