using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides information about the characteristics for a component, such as its attributes, properties, and events. This class cannot be inherited.</summary>
	// Token: 0x020001B5 RID: 437
	public sealed class TypeDescriptor
	{
		// Token: 0x06000F42 RID: 3906 RVA: 0x000271E0 File Offset: 0x000253E0
		private TypeDescriptor()
		{
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x000271E8 File Offset: 0x000253E8
		static TypeDescriptor()
		{
			TypeDescriptor.typeDescriptionProviders = new Dictionary<Type, System.Collections.Generic.LinkedList<TypeDescriptionProvider>>();
			TypeDescriptor.componentDescriptionProviders = new Dictionary<WeakObjectWrapper, System.Collections.Generic.LinkedList<TypeDescriptionProvider>>(new WeakObjectWrapperComparer());
		}

		/// <summary>Occurs when the cache for a component is cleared.</summary>
		// Token: 0x1400003A RID: 58
		// (add) Token: 0x06000F44 RID: 3908 RVA: 0x00027240 File Offset: 0x00025440
		// (remove) Token: 0x06000F45 RID: 3909 RVA: 0x00027258 File Offset: 0x00025458
		public static event RefreshEventHandler Refreshed;

		/// <summary>Gets the type of the Component Object Model (COM) object represented by the target component.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the COM object represented by this component, or null for non-COM objects.</returns>
		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000F46 RID: 3910 RVA: 0x00027270 File Offset: 0x00025470
		[MonoNotSupported("Mono does not support COM")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static Type ComObjectType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Adds class-level attributes to the target component instance.</summary>
		/// <returns>The newly created <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> that was used to add the specified attributes.</returns>
		/// <param name="instance">An instance of the target component.</param>
		/// <param name="attributes">An array of <see cref="T:System.Attribute" /> objects to add to the component's class.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters is null.</exception>
		// Token: 0x06000F47 RID: 3911 RVA: 0x00027278 File Offset: 0x00025478
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static TypeDescriptionProvider AddAttributes(object instance, params Attribute[] attributes)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			if (attributes == null)
			{
				throw new ArgumentNullException("attributes");
			}
			TypeDescriptor.AttributeProvider attributeProvider = new TypeDescriptor.AttributeProvider(attributes, TypeDescriptor.GetProvider(instance));
			TypeDescriptor.AddProvider(attributeProvider, instance);
			return attributeProvider;
		}

		/// <summary>Adds class-level attributes to the target component type.</summary>
		/// <returns>The newly created <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> that was used to add the specified attributes.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> of the target component.</param>
		/// <param name="attributes">An array of <see cref="T:System.Attribute" /> objects to add to the component's class.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters is null.</exception>
		// Token: 0x06000F48 RID: 3912 RVA: 0x000272BC File Offset: 0x000254BC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static TypeDescriptionProvider AddAttributes(Type type, params Attribute[] attributes)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (attributes == null)
			{
				throw new ArgumentNullException("attributes");
			}
			TypeDescriptor.AttributeProvider attributeProvider = new TypeDescriptor.AttributeProvider(attributes, TypeDescriptor.GetProvider(type));
			TypeDescriptor.AddProvider(attributeProvider, type);
			return attributeProvider;
		}

		/// <summary>Adds a type description provider for a single instance of a component.</summary>
		/// <param name="provider">The <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> to add.</param>
		/// <param name="instance">An instance of the target component.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are null.</exception>
		// Token: 0x06000F49 RID: 3913 RVA: 0x00027300 File Offset: 0x00025500
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static void AddProvider(TypeDescriptionProvider provider, object instance)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			object obj = TypeDescriptor.componentDescriptionProvidersLock;
			lock (obj)
			{
				WeakObjectWrapper key = new WeakObjectWrapper(instance);
				System.Collections.Generic.LinkedList<TypeDescriptionProvider> linkedList;
				if (!TypeDescriptor.componentDescriptionProviders.TryGetValue(key, out linkedList))
				{
					linkedList = new System.Collections.Generic.LinkedList<TypeDescriptionProvider>();
					TypeDescriptor.componentDescriptionProviders.Add(new WeakObjectWrapper(instance), linkedList);
				}
				linkedList.AddLast(provider);
				TypeDescriptor.Refresh(instance);
			}
		}

		/// <summary>Adds a type description provider for a component class.</summary>
		/// <param name="provider">The <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> to add.</param>
		/// <param name="type">The <see cref="T:System.Type" /> of the target component.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are null.</exception>
		// Token: 0x06000F4A RID: 3914 RVA: 0x000273A4 File Offset: 0x000255A4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static void AddProvider(TypeDescriptionProvider provider, Type type)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			object obj = TypeDescriptor.typeDescriptionProvidersLock;
			lock (obj)
			{
				System.Collections.Generic.LinkedList<TypeDescriptionProvider> linkedList;
				if (!TypeDescriptor.typeDescriptionProviders.TryGetValue(type, out linkedList))
				{
					linkedList = new System.Collections.Generic.LinkedList<TypeDescriptionProvider>();
					TypeDescriptor.typeDescriptionProviders.Add(type, linkedList);
				}
				linkedList.AddLast(provider);
				TypeDescriptor.Refresh(type);
			}
		}

		/// <summary>Creates an object that can substitute for another data type. </summary>
		/// <returns>An instance of the substitute data type if an associated <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> is found; otherwise, null.</returns>
		/// <param name="provider">The service provider that provides a <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> service. This parameter can be null.</param>
		/// <param name="objectType">The <see cref="T:System.Type" /> of object to create.</param>
		/// <param name="argTypes">An optional array of parameter types to be passed to the object's constructor. This parameter can be null or an array of zero length.</param>
		/// <param name="args">An optional array of parameter values to pass to the object's constructor. If not null, the number of elements must be the same as <paramref name="argTypes" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="objectType" /> is null, or <paramref name="args" /> is null when <paramref name="argTypes" /> is not null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="argTypes" /> and <paramref name="args" /> have different number of elements.</exception>
		// Token: 0x06000F4B RID: 3915 RVA: 0x0002743C File Offset: 0x0002563C
		[MonoTODO]
		public static object CreateInstance(IServiceProvider provider, Type objectType, Type[] argTypes, object[] args)
		{
			if (objectType == null)
			{
				throw new ArgumentNullException("objectType");
			}
			object obj = null;
			if (provider != null)
			{
				TypeDescriptionProvider typeDescriptionProvider = provider.GetService(typeof(TypeDescriptionProvider)) as TypeDescriptionProvider;
				if (typeDescriptionProvider != null)
				{
					obj = typeDescriptionProvider.CreateInstance(provider, objectType, argTypes, args);
				}
			}
			if (obj == null)
			{
				obj = Activator.CreateInstance(objectType, args);
			}
			return obj;
		}

		/// <summary>Adds an editor table for the given editor base type. </summary>
		/// <param name="editorBaseType">The editor base type to add the editor table for. If a table already exists for this type, this method will do nothing. </param>
		/// <param name="table">The <see cref="T:System.Collections.Hashtable" /> to add. </param>
		// Token: 0x06000F4C RID: 3916 RVA: 0x00027498 File Offset: 0x00025698
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static void AddEditorTable(Type editorBaseType, Hashtable table)
		{
			if (editorBaseType == null)
			{
				throw new ArgumentNullException("editorBaseType");
			}
			if (TypeDescriptor.editors == null)
			{
				TypeDescriptor.editors = new Hashtable();
			}
			if (!TypeDescriptor.editors.ContainsKey(editorBaseType))
			{
				TypeDescriptor.editors[editorBaseType] = table;
			}
		}

		/// <summary>Creates an instance of the designer associated with the specified component and of the specified type of designer.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.Design.IDesigner" /> that is an instance of the designer for the component, or null if no designer can be found.</returns>
		/// <param name="component">An <see cref="T:System.ComponentModel.IComponent" /> that specifies the component to associate with the designer. </param>
		/// <param name="designerBaseType">A <see cref="T:System.Type" /> that represents the type of designer to create. </param>
		// Token: 0x06000F4D RID: 3917 RVA: 0x000274E8 File Offset: 0x000256E8
		public static System.ComponentModel.Design.IDesigner CreateDesigner(IComponent component, Type designerBaseType)
		{
			string assemblyQualifiedName = designerBaseType.AssemblyQualifiedName;
			AttributeCollection attributes = TypeDescriptor.GetAttributes(component);
			foreach (object obj in attributes)
			{
				Attribute attribute = (Attribute)obj;
				DesignerAttribute designerAttribute = attribute as DesignerAttribute;
				if (designerAttribute != null && assemblyQualifiedName == designerAttribute.DesignerBaseTypeName)
				{
					Type typeFromName = TypeDescriptor.GetTypeFromName(component, designerAttribute.DesignerTypeName);
					if (typeFromName != null)
					{
						return (System.ComponentModel.Design.IDesigner)Activator.CreateInstance(typeFromName);
					}
				}
			}
			return null;
		}

		/// <summary>Creates a new event descriptor that is identical to an existing event descriptor by dynamically generating descriptor information from a specified event on a type.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptor" /> that is bound to a type.</returns>
		/// <param name="componentType">The type of the component the event lives on. </param>
		/// <param name="name">The name of the event. </param>
		/// <param name="type">The type of the delegate that handles the event. </param>
		/// <param name="attributes">The attributes for this event. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="TypeInformation, MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06000F4E RID: 3918 RVA: 0x000275AC File Offset: 0x000257AC
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"MemberAccess, TypeInformation\"/>\n</PermissionSet>\n")]
		public static EventDescriptor CreateEvent(Type componentType, string name, Type type, params Attribute[] attributes)
		{
			return new ReflectionEventDescriptor(componentType, name, type, attributes);
		}

		/// <summary>Creates a new event descriptor that is identical to an existing event descriptor, when passed the existing <see cref="T:System.ComponentModel.EventDescriptor" />.</summary>
		/// <returns>A new <see cref="T:System.ComponentModel.EventDescriptor" /> that has merged the specified metadata attributes with the existing metadata attributes.</returns>
		/// <param name="componentType">The type of the component for which to create the new event. </param>
		/// <param name="oldEventDescriptor">The existing event information. </param>
		/// <param name="attributes">The new attributes. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="TypeInformation, MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06000F4F RID: 3919 RVA: 0x000275B8 File Offset: 0x000257B8
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"MemberAccess, TypeInformation\"/>\n</PermissionSet>\n")]
		public static EventDescriptor CreateEvent(Type componentType, EventDescriptor oldEventDescriptor, params Attribute[] attributes)
		{
			return new ReflectionEventDescriptor(componentType, oldEventDescriptor, attributes);
		}

		/// <summary>Creates and dynamically binds a property descriptor to a type, using the specified property name, type, and attribute array.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that is bound to the specified type and that has the specified metadata attributes merged with the existing metadata attributes.</returns>
		/// <param name="componentType">The <see cref="T:System.Type" /> of the component that the property is a member of. </param>
		/// <param name="name">The name of the property. </param>
		/// <param name="type">The <see cref="T:System.Type" /> of the property. </param>
		/// <param name="attributes">The new attributes for this property. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="TypeInformation, MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06000F50 RID: 3920 RVA: 0x000275C4 File Offset: 0x000257C4
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"MemberAccess, TypeInformation\"/>\n</PermissionSet>\n")]
		public static PropertyDescriptor CreateProperty(Type componentType, string name, Type type, params Attribute[] attributes)
		{
			return new ReflectionPropertyDescriptor(componentType, name, type, attributes);
		}

		/// <summary>Creates a new property descriptor from an existing property descriptor, using the specified existing <see cref="T:System.ComponentModel.PropertyDescriptor" /> and attribute array.</summary>
		/// <returns>A new <see cref="T:System.ComponentModel.PropertyDescriptor" /> that has the specified metadata attributes merged with the existing metadata attributes.</returns>
		/// <param name="componentType">The <see cref="T:System.Type" /> of the component that the property is a member of. </param>
		/// <param name="oldPropertyDescriptor">The existing property descriptor. </param>
		/// <param name="attributes">The new attributes for this property. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="TypeInformation, MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06000F51 RID: 3921 RVA: 0x000275D0 File Offset: 0x000257D0
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"MemberAccess, TypeInformation\"/>\n</PermissionSet>\n")]
		public static PropertyDescriptor CreateProperty(Type componentType, PropertyDescriptor oldPropertyDescriptor, params Attribute[] attributes)
		{
			return new ReflectionPropertyDescriptor(componentType, oldPropertyDescriptor, attributes);
		}

		/// <summary>Returns a collection of attributes for the specified type of component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.AttributeCollection" /> with the attributes for the type of the component. If the component is null, this method returns an empty collection.</returns>
		/// <param name="componentType">The <see cref="T:System.Type" /> of the target component. </param>
		// Token: 0x06000F52 RID: 3922 RVA: 0x000275DC File Offset: 0x000257DC
		public static AttributeCollection GetAttributes(Type componentType)
		{
			if (componentType == null)
			{
				return AttributeCollection.Empty;
			}
			return TypeDescriptor.GetTypeInfo(componentType).GetAttributes();
		}

		/// <summary>Returns the collection of attributes for the specified component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.AttributeCollection" /> containing the attributes for the component. If <paramref name="component" /> is null, this method returns an empty collection.</returns>
		/// <param name="component">The component for which you want to get attributes. </param>
		// Token: 0x06000F53 RID: 3923 RVA: 0x000275F8 File Offset: 0x000257F8
		public static AttributeCollection GetAttributes(object component)
		{
			return TypeDescriptor.GetAttributes(component, false);
		}

		/// <summary>Returns a collection of attributes for the specified component and a Boolean indicating that a custom type descriptor has been created.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.AttributeCollection" /> with the attributes for the component. If the component is null, this method returns an empty collection.</returns>
		/// <param name="component">The component for which you want to get attributes. </param>
		/// <param name="noCustomTypeDesc">true to use a baseline set of attributes from the custom type descriptor if <paramref name="component" /> is of type <see cref="T:System.ComponentModel.ICustomTypeDescriptor" />; otherwise, false.</param>
		// Token: 0x06000F54 RID: 3924 RVA: 0x00027604 File Offset: 0x00025804
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static AttributeCollection GetAttributes(object component, bool noCustomTypeDesc)
		{
			if (component == null)
			{
				return AttributeCollection.Empty;
			}
			if (!noCustomTypeDesc && component is ICustomTypeDescriptor)
			{
				return ((ICustomTypeDescriptor)component).GetAttributes();
			}
			IComponent component2 = component as IComponent;
			if (component2 != null && component2.Site != null)
			{
				return TypeDescriptor.GetComponentInfo(component2).GetAttributes();
			}
			return TypeDescriptor.GetTypeInfo(component.GetType()).GetAttributes();
		}

		/// <summary>Returns the name of the class for the specified component using the default type descriptor.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the class for the specified component.</returns>
		/// <param name="component">The <see cref="T:System.Object" /> for which you want the class name. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null. </exception>
		// Token: 0x06000F55 RID: 3925 RVA: 0x00027670 File Offset: 0x00025870
		public static string GetClassName(object component)
		{
			return TypeDescriptor.GetClassName(component, false);
		}

		/// <summary>Returns the name of the class for the specified component using a custom type descriptor.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the class for the specified component.</returns>
		/// <param name="component">The <see cref="T:System.Object" /> for which you want the class name. </param>
		/// <param name="noCustomTypeDesc">true to consider custom type description information; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x06000F56 RID: 3926 RVA: 0x0002767C File Offset: 0x0002587C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static string GetClassName(object component, bool noCustomTypeDesc)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component", "component cannot be null");
			}
			if (!noCustomTypeDesc && component is ICustomTypeDescriptor)
			{
				string text = ((ICustomTypeDescriptor)component).GetClassName();
				if (text == null)
				{
					text = ((ICustomTypeDescriptor)component).GetComponentName();
				}
				if (text == null)
				{
					text = component.GetType().FullName;
				}
				return text;
			}
			return component.GetType().FullName;
		}

		/// <summary>Returns the name of the specified component using the default type descriptor.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the specified component, or null if there is no component name.</returns>
		/// <param name="component">The <see cref="T:System.Object" /> for which you want the class name. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x06000F57 RID: 3927 RVA: 0x000276F0 File Offset: 0x000258F0
		public static string GetComponentName(object component)
		{
			return TypeDescriptor.GetComponentName(component, false);
		}

		/// <summary>Returns the name of the specified component using a custom type descriptor.</summary>
		/// <returns>The name of the class for the specified component, or null if there is no component name.</returns>
		/// <param name="component">The <see cref="T:System.Object" /> for which you want the class name. </param>
		/// <param name="noCustomTypeDesc">true to consider custom type description information; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x06000F58 RID: 3928 RVA: 0x000276FC File Offset: 0x000258FC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static string GetComponentName(object component, bool noCustomTypeDesc)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component", "component cannot be null");
			}
			if (!noCustomTypeDesc && component is ICustomTypeDescriptor)
			{
				return ((ICustomTypeDescriptor)component).GetComponentName();
			}
			IComponent component2 = component as IComponent;
			if (component2 != null && component2.Site != null)
			{
				return component2.Site.Name;
			}
			return null;
		}

		/// <summary>Returns the fully qualified name of the component.</summary>
		/// <returns>The fully qualified name of the specified component, or null if the component has no name.</returns>
		/// <param name="component">The <see cref="T:System.ComponentModel.Component" /> to find the name for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null.</exception>
		// Token: 0x06000F59 RID: 3929 RVA: 0x00027764 File Offset: 0x00025964
		[MonoNotSupported("")]
		public static string GetFullComponentName(object component)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the name of the class for the specified type.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the class for the specified component type.</returns>
		/// <param name="componentType">The <see cref="T:System.Type" /> of the target component.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="componentType" /> is null.</exception>
		// Token: 0x06000F5A RID: 3930 RVA: 0x0002776C File Offset: 0x0002596C
		[MonoNotSupported("")]
		public static string GetClassName(Type componentType)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a type converter for the type of the specified component.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> for the specified component.</returns>
		/// <param name="component">A component to get the converter for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x06000F5B RID: 3931 RVA: 0x00027774 File Offset: 0x00025974
		public static TypeConverter GetConverter(object component)
		{
			return TypeDescriptor.GetConverter(component, false);
		}

		/// <summary>Returns a type converter for the type of the specified component with a custom type descriptor.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> for the specified component.</returns>
		/// <param name="component">A component to get the converter for. </param>
		/// <param name="noCustomTypeDesc">true to consider custom type description information; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x06000F5C RID: 3932 RVA: 0x00027780 File Offset: 0x00025980
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static TypeConverter GetConverter(object component, bool noCustomTypeDesc)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component", "component cannot be null");
			}
			if (!noCustomTypeDesc && component is ICustomTypeDescriptor)
			{
				return ((ICustomTypeDescriptor)component).GetConverter();
			}
			Type type = null;
			AttributeCollection attributes = TypeDescriptor.GetAttributes(component, false);
			TypeConverterAttribute typeConverterAttribute = (TypeConverterAttribute)attributes[typeof(TypeConverterAttribute)];
			if (typeConverterAttribute != null && typeConverterAttribute.ConverterTypeName.Length > 0)
			{
				type = TypeDescriptor.GetTypeFromName(component as IComponent, typeConverterAttribute.ConverterTypeName);
			}
			if (type == null)
			{
				type = TypeDescriptor.FindDefaultConverterType(component.GetType());
			}
			if (type == null)
			{
				return null;
			}
			ConstructorInfo constructor = type.GetConstructor(new Type[]
			{
				typeof(Type)
			});
			if (constructor != null)
			{
				return (TypeConverter)constructor.Invoke(new object[]
				{
					component.GetType()
				});
			}
			return (TypeConverter)Activator.CreateInstance(type);
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000F5D RID: 3933 RVA: 0x0002786C File Offset: 0x00025A6C
		private static ArrayList DefaultConverters
		{
			get
			{
				object obj = TypeDescriptor.creatingDefaultConverters;
				lock (obj)
				{
					if (TypeDescriptor.defaultConverters != null)
					{
						return TypeDescriptor.defaultConverters;
					}
					TypeDescriptor.defaultConverters = new ArrayList();
					TypeDescriptor.defaultConverters.Add(new DictionaryEntry(typeof(bool), typeof(BooleanConverter)));
					TypeDescriptor.defaultConverters.Add(new DictionaryEntry(typeof(byte), typeof(ByteConverter)));
					TypeDescriptor.defaultConverters.Add(new DictionaryEntry(typeof(sbyte), typeof(SByteConverter)));
					TypeDescriptor.defaultConverters.Add(new DictionaryEntry(typeof(string), typeof(StringConverter)));
					TypeDescriptor.defaultConverters.Add(new DictionaryEntry(typeof(char), typeof(CharConverter)));
					TypeDescriptor.defaultConverters.Add(new DictionaryEntry(typeof(short), typeof(Int16Converter)));
					TypeDescriptor.defaultConverters.Add(new DictionaryEntry(typeof(int), typeof(Int32Converter)));
					TypeDescriptor.defaultConverters.Add(new DictionaryEntry(typeof(long), typeof(Int64Converter)));
					TypeDescriptor.defaultConverters.Add(new DictionaryEntry(typeof(ushort), typeof(UInt16Converter)));
					TypeDescriptor.defaultConverters.Add(new DictionaryEntry(typeof(uint), typeof(UInt32Converter)));
					TypeDescriptor.defaultConverters.Add(new DictionaryEntry(typeof(ulong), typeof(UInt64Converter)));
					TypeDescriptor.defaultConverters.Add(new DictionaryEntry(typeof(float), typeof(SingleConverter)));
					TypeDescriptor.defaultConverters.Add(new DictionaryEntry(typeof(double), typeof(DoubleConverter)));
					TypeDescriptor.defaultConverters.Add(new DictionaryEntry(typeof(decimal), typeof(DecimalConverter)));
					TypeDescriptor.defaultConverters.Add(new DictionaryEntry(typeof(void), typeof(TypeConverter)));
					TypeDescriptor.defaultConverters.Add(new DictionaryEntry(typeof(Array), typeof(ArrayConverter)));
					TypeDescriptor.defaultConverters.Add(new DictionaryEntry(typeof(CultureInfo), typeof(CultureInfoConverter)));
					TypeDescriptor.defaultConverters.Add(new DictionaryEntry(typeof(DateTime), typeof(DateTimeConverter)));
					TypeDescriptor.defaultConverters.Add(new DictionaryEntry(typeof(Guid), typeof(GuidConverter)));
					TypeDescriptor.defaultConverters.Add(new DictionaryEntry(typeof(TimeSpan), typeof(TimeSpanConverter)));
					TypeDescriptor.defaultConverters.Add(new DictionaryEntry(typeof(ICollection), typeof(CollectionConverter)));
					TypeDescriptor.defaultConverters.Add(new DictionaryEntry(typeof(Enum), typeof(EnumConverter)));
				}
				return TypeDescriptor.defaultConverters;
			}
		}

		/// <summary>Returns a type converter for the specified type.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> for the specified type.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> of the target component. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null. </exception>
		// Token: 0x06000F5E RID: 3934 RVA: 0x00027C5C File Offset: 0x00025E5C
		public static TypeConverter GetConverter(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			Type type2 = null;
			AttributeCollection attributes = TypeDescriptor.GetAttributes(type);
			TypeConverterAttribute typeConverterAttribute = (TypeConverterAttribute)attributes[typeof(TypeConverterAttribute)];
			if (typeConverterAttribute != null && typeConverterAttribute.ConverterTypeName.Length > 0)
			{
				type2 = TypeDescriptor.GetTypeFromName(null, typeConverterAttribute.ConverterTypeName);
			}
			if (type2 == null)
			{
				type2 = TypeDescriptor.FindDefaultConverterType(type);
			}
			if (type2 == null)
			{
				return null;
			}
			ConstructorInfo constructor = type2.GetConstructor(new Type[]
			{
				typeof(Type)
			});
			if (constructor != null)
			{
				return (TypeConverter)constructor.Invoke(new object[]
				{
					type
				});
			}
			return (TypeConverter)Activator.CreateInstance(type2);
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x00027D14 File Offset: 0x00025F14
		private static Type FindDefaultConverterType(Type type)
		{
			Type type2 = null;
			if (type != null)
			{
				if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
				{
					return typeof(NullableConverter);
				}
				foreach (object obj in TypeDescriptor.DefaultConverters)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					if ((Type)dictionaryEntry.Key == type)
					{
						return (Type)dictionaryEntry.Value;
					}
				}
			}
			Type type3 = type;
			while (type3 != null && type3 != typeof(object))
			{
				foreach (object obj2 in TypeDescriptor.DefaultConverters)
				{
					DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
					Type type4 = (Type)dictionaryEntry2.Key;
					if (type4.IsAssignableFrom(type3))
					{
						type2 = (Type)dictionaryEntry2.Value;
						break;
					}
				}
				type3 = type3.BaseType;
			}
			if (type2 == null)
			{
				if (type != null && type.IsInterface)
				{
					type2 = typeof(ReferenceConverter);
				}
				else
				{
					type2 = typeof(TypeConverter);
				}
			}
			return type2;
		}

		/// <summary>Returns the default event for the specified type of component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptor" /> with the default event, or null if there are no events.</returns>
		/// <param name="componentType">The <see cref="T:System.Type" /> of the target component.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null. </exception>
		// Token: 0x06000F60 RID: 3936 RVA: 0x00027EBC File Offset: 0x000260BC
		public static EventDescriptor GetDefaultEvent(Type componentType)
		{
			return TypeDescriptor.GetTypeInfo(componentType).GetDefaultEvent();
		}

		/// <summary>Returns the default event for the specified component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptor" /> with the default event, or null if there are no events.</returns>
		/// <param name="component">The component to get the event for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x06000F61 RID: 3937 RVA: 0x00027ECC File Offset: 0x000260CC
		public static EventDescriptor GetDefaultEvent(object component)
		{
			return TypeDescriptor.GetDefaultEvent(component, false);
		}

		/// <summary>Returns the default event for a component with a custom type descriptor.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptor" /> with the default event, or null if there are no events.</returns>
		/// <param name="component">The component to get the event for. </param>
		/// <param name="noCustomTypeDesc">true to consider custom type description information; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x06000F62 RID: 3938 RVA: 0x00027ED8 File Offset: 0x000260D8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static EventDescriptor GetDefaultEvent(object component, bool noCustomTypeDesc)
		{
			if (!noCustomTypeDesc && component is ICustomTypeDescriptor)
			{
				return ((ICustomTypeDescriptor)component).GetDefaultEvent();
			}
			IComponent component2 = component as IComponent;
			if (component2 != null && component2.Site != null)
			{
				return TypeDescriptor.GetComponentInfo(component2).GetDefaultEvent();
			}
			return TypeDescriptor.GetTypeInfo(component.GetType()).GetDefaultEvent();
		}

		/// <summary>Returns the default property for the specified type of component.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> with the default property, or null if there are no properties.</returns>
		/// <param name="componentType">A <see cref="T:System.Type" /> that represents the class to get the property for. </param>
		// Token: 0x06000F63 RID: 3939 RVA: 0x00027F38 File Offset: 0x00026138
		public static PropertyDescriptor GetDefaultProperty(Type componentType)
		{
			return TypeDescriptor.GetTypeInfo(componentType).GetDefaultProperty();
		}

		/// <summary>Returns the default property for the specified component.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> with the default property, or null if there are no properties.</returns>
		/// <param name="component">The component to get the default property for. </param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x06000F64 RID: 3940 RVA: 0x00027F48 File Offset: 0x00026148
		public static PropertyDescriptor GetDefaultProperty(object component)
		{
			return TypeDescriptor.GetDefaultProperty(component, false);
		}

		/// <summary>Returns the default property for the specified component with a custom type descriptor.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> with the default property, or null if there are no properties.</returns>
		/// <param name="component">The component to get the default property for. </param>
		/// <param name="noCustomTypeDesc">true to consider custom type description information; otherwise, false.</param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x06000F65 RID: 3941 RVA: 0x00027F54 File Offset: 0x00026154
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static PropertyDescriptor GetDefaultProperty(object component, bool noCustomTypeDesc)
		{
			if (!noCustomTypeDesc && component is ICustomTypeDescriptor)
			{
				return ((ICustomTypeDescriptor)component).GetDefaultProperty();
			}
			IComponent component2 = component as IComponent;
			if (component2 != null && component2.Site != null)
			{
				return TypeDescriptor.GetComponentInfo(component2).GetDefaultProperty();
			}
			return TypeDescriptor.GetTypeInfo(component.GetType()).GetDefaultProperty();
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x00027FB4 File Offset: 0x000261B4
		internal static object CreateEditor(Type t, Type componentType)
		{
			if (t == null)
			{
				return null;
			}
			try
			{
				return Activator.CreateInstance(t);
			}
			catch
			{
			}
			try
			{
				return Activator.CreateInstance(t, new object[]
				{
					componentType
				});
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x00028038 File Offset: 0x00026238
		private static object FindEditorInTable(Type componentType, Type editorBaseType, Hashtable table)
		{
			object obj = null;
			object obj2 = null;
			if (componentType == null || editorBaseType == null || table == null)
			{
				return null;
			}
			for (Type type = componentType; type != null; type = type.BaseType)
			{
				obj = table[type];
				if (obj != null)
				{
					break;
				}
			}
			if (obj == null)
			{
				foreach (Type key in componentType.GetInterfaces())
				{
					obj = table[key];
					if (obj != null)
					{
						break;
					}
				}
			}
			if (obj == null)
			{
				return null;
			}
			if (obj is string)
			{
				obj2 = TypeDescriptor.CreateEditor(Type.GetType((string)obj), componentType);
			}
			else if (obj is Type)
			{
				obj2 = TypeDescriptor.CreateEditor((Type)obj, componentType);
			}
			else if (obj.GetType().IsSubclassOf(editorBaseType))
			{
				obj2 = obj;
			}
			if (obj2 != null)
			{
				table[componentType] = obj2;
			}
			return obj2;
		}

		/// <summary>Returns an editor with the specified base type for the specified type.</summary>
		/// <returns>An instance of the editor object that can be cast to the given base type, or null if no editor of the requested type can be found.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> of the target component.</param>
		/// <param name="editorBaseType">A <see cref="T:System.Type" /> that represents the base type of the editor you are trying to find. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> or <paramref name="editorBaseType" /> is null. </exception>
		// Token: 0x06000F68 RID: 3944 RVA: 0x00028130 File Offset: 0x00026330
		public static object GetEditor(Type componentType, Type editorBaseType)
		{
			Type type = null;
			object obj = null;
			object[] customAttributes = componentType.GetCustomAttributes(typeof(EditorAttribute), true);
			if (customAttributes != null && customAttributes.Length != 0)
			{
				foreach (EditorAttribute editorAttribute in customAttributes)
				{
					type = TypeDescriptor.GetTypeFromName(null, editorAttribute.EditorTypeName);
					if (type != null && type.IsSubclassOf(editorBaseType))
					{
						break;
					}
				}
			}
			if (type != null)
			{
				obj = TypeDescriptor.CreateEditor(type, componentType);
			}
			if (type == null || obj == null)
			{
				RuntimeHelpers.RunClassConstructor(editorBaseType.TypeHandle);
				if (TypeDescriptor.editors != null)
				{
					obj = TypeDescriptor.FindEditorInTable(componentType, editorBaseType, TypeDescriptor.editors[editorBaseType] as Hashtable);
				}
			}
			return obj;
		}

		/// <summary>Gets an editor with the specified base type for the specified component.</summary>
		/// <returns>An instance of the editor that can be cast to the specified editor type, or null if no editor of the requested type can be found.</returns>
		/// <param name="component">The component to get the editor for. </param>
		/// <param name="editorBaseType">A <see cref="T:System.Type" /> that represents the base type of the editor you want to find. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> or <paramref name="editorBaseType" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x06000F69 RID: 3945 RVA: 0x000281F4 File Offset: 0x000263F4
		public static object GetEditor(object component, Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(component, editorBaseType, false);
		}

		/// <summary>Returns an editor with the specified base type and with a custom type descriptor for the specified component.</summary>
		/// <returns>An instance of the editor that can be cast to the specified editor type, or null if no editor of the requested type can be found.</returns>
		/// <param name="component">The component to get the editor for. </param>
		/// <param name="editorBaseType">A <see cref="T:System.Type" /> that represents the base type of the editor you want to find. </param>
		/// <param name="noCustomTypeDesc">A flag indicating whether custom type description information should be considered.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> or <paramref name="editorBaseType" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x06000F6A RID: 3946 RVA: 0x00028200 File Offset: 0x00026400
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static object GetEditor(object component, Type editorBaseType, bool noCustomTypeDesc)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (editorBaseType == null)
			{
				throw new ArgumentNullException("editorBaseType");
			}
			if (!noCustomTypeDesc && component is ICustomTypeDescriptor)
			{
				return ((ICustomTypeDescriptor)component).GetEditor(editorBaseType);
			}
			object[] customAttributes = component.GetType().GetCustomAttributes(typeof(EditorAttribute), true);
			if (customAttributes.Length == 0)
			{
				return null;
			}
			string assemblyQualifiedName = editorBaseType.AssemblyQualifiedName;
			foreach (EditorAttribute editorAttribute in customAttributes)
			{
				if (editorAttribute.EditorBaseTypeName == assemblyQualifiedName)
				{
					Type type = Type.GetType(editorAttribute.EditorTypeName, true);
					return Activator.CreateInstance(type);
				}
			}
			return null;
		}

		/// <summary>Returns the collection of events for the specified component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> with the events for this component.</returns>
		/// <param name="component">A component to get the events for. </param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x06000F6B RID: 3947 RVA: 0x000282C4 File Offset: 0x000264C4
		public static EventDescriptorCollection GetEvents(object component)
		{
			return TypeDescriptor.GetEvents(component, false);
		}

		/// <summary>Returns the collection of events for a specified type of component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> with the events for this component.</returns>
		/// <param name="componentType">The <see cref="T:System.Type" /> of the target component.</param>
		// Token: 0x06000F6C RID: 3948 RVA: 0x000282D0 File Offset: 0x000264D0
		public static EventDescriptorCollection GetEvents(Type componentType)
		{
			return TypeDescriptor.GetEvents(componentType, null);
		}

		/// <summary>Returns the collection of events for a specified component using a specified array of attributes as a filter.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> with the events that match the specified attributes for this component.</returns>
		/// <param name="component">A component to get the events for. </param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that you can use as a filter. </param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x06000F6D RID: 3949 RVA: 0x000282DC File Offset: 0x000264DC
		public static EventDescriptorCollection GetEvents(object component, Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(component, attributes, false);
		}

		/// <summary>Returns the collection of events for a specified component with a custom type descriptor.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> with the events for this component.</returns>
		/// <param name="component">A component to get the events for. </param>
		/// <param name="noCustomTypeDesc">true to consider custom type description information; otherwise, false.</param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x06000F6E RID: 3950 RVA: 0x000282E8 File Offset: 0x000264E8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static EventDescriptorCollection GetEvents(object component, bool noCustomTypeDesc)
		{
			if (!noCustomTypeDesc && component is ICustomTypeDescriptor)
			{
				return ((ICustomTypeDescriptor)component).GetEvents();
			}
			IComponent component2 = component as IComponent;
			if (component2 != null && component2.Site != null)
			{
				return TypeDescriptor.GetComponentInfo(component2).GetEvents();
			}
			return TypeDescriptor.GetTypeInfo(component.GetType()).GetEvents();
		}

		/// <summary>Returns the collection of events for a specified type of component using a specified array of attributes as a filter.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> with the events that match the specified attributes for this component.</returns>
		/// <param name="componentType">The <see cref="T:System.Type" /> of the target component.</param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that you can use as a filter. </param>
		// Token: 0x06000F6F RID: 3951 RVA: 0x00028348 File Offset: 0x00026548
		public static EventDescriptorCollection GetEvents(Type componentType, Attribute[] attributes)
		{
			return TypeDescriptor.GetTypeInfo(componentType).GetEvents(attributes);
		}

		/// <summary>Returns the collection of events for a specified component using a specified array of attributes as a filter and using a custom type descriptor.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> with the events that match the specified attributes for this component.</returns>
		/// <param name="component">A component to get the events for. </param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> to use as a filter. </param>
		/// <param name="noCustomTypeDesc">true to consider custom type description information; otherwise, false.</param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x06000F70 RID: 3952 RVA: 0x00028358 File Offset: 0x00026558
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static EventDescriptorCollection GetEvents(object component, Attribute[] attributes, bool noCustomTypeDesc)
		{
			if (!noCustomTypeDesc && component is ICustomTypeDescriptor)
			{
				return ((ICustomTypeDescriptor)component).GetEvents(attributes);
			}
			IComponent component2 = component as IComponent;
			if (component2 != null && component2.Site != null)
			{
				return TypeDescriptor.GetComponentInfo(component2).GetEvents(attributes);
			}
			return TypeDescriptor.GetTypeInfo(component.GetType()).GetEvents(attributes);
		}

		/// <summary>Returns the collection of properties for a specified component.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties for the specified component.</returns>
		/// <param name="component">A component to get the properties for. </param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x06000F71 RID: 3953 RVA: 0x000283BC File Offset: 0x000265BC
		public static PropertyDescriptorCollection GetProperties(object component)
		{
			return TypeDescriptor.GetProperties(component, false);
		}

		/// <summary>Returns the collection of properties for a specified type of component.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties for a specified type of component.</returns>
		/// <param name="componentType">A <see cref="T:System.Type" /> that represents the component to get properties for.</param>
		// Token: 0x06000F72 RID: 3954 RVA: 0x000283C8 File Offset: 0x000265C8
		public static PropertyDescriptorCollection GetProperties(Type componentType)
		{
			return TypeDescriptor.GetProperties(componentType, null);
		}

		/// <summary>Returns the collection of properties for a specified component using a specified array of attributes as a filter.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that match the specified attributes for the specified component.</returns>
		/// <param name="component">A component to get the properties for. </param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> to use as a filter. </param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x06000F73 RID: 3955 RVA: 0x000283D4 File Offset: 0x000265D4
		public static PropertyDescriptorCollection GetProperties(object component, Attribute[] attributes)
		{
			return TypeDescriptor.GetProperties(component, attributes, false);
		}

		/// <summary>Returns the collection of properties for a specified component using a specified array of attributes as a filter and using a custom type descriptor.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the events that match the specified attributes for the specified component.</returns>
		/// <param name="component">A component to get the properties for. </param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> to use as a filter. </param>
		/// <param name="noCustomTypeDesc">true to not consider custom type description information; otherwise, false.</param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x06000F74 RID: 3956 RVA: 0x000283E0 File Offset: 0x000265E0
		public static PropertyDescriptorCollection GetProperties(object component, Attribute[] attributes, bool noCustomTypeDesc)
		{
			if (component == null)
			{
				return PropertyDescriptorCollection.Empty;
			}
			if (!noCustomTypeDesc && component is ICustomTypeDescriptor)
			{
				return ((ICustomTypeDescriptor)component).GetProperties(attributes);
			}
			IComponent component2 = component as IComponent;
			if (component2 != null && component2.Site != null)
			{
				return TypeDescriptor.GetComponentInfo(component2).GetProperties(attributes);
			}
			return TypeDescriptor.GetTypeInfo(component.GetType()).GetProperties(attributes);
		}

		/// <summary>Returns the collection of properties for a specified component using the default type descriptor.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties for a specified component.</returns>
		/// <param name="component">A component to get the properties for. </param>
		/// <param name="noCustomTypeDesc">true to not consider custom type description information; otherwise, false.</param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x06000F75 RID: 3957 RVA: 0x00028450 File Offset: 0x00026650
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static PropertyDescriptorCollection GetProperties(object component, bool noCustomTypeDesc)
		{
			if (component == null)
			{
				return PropertyDescriptorCollection.Empty;
			}
			if (!noCustomTypeDesc && component is ICustomTypeDescriptor)
			{
				return ((ICustomTypeDescriptor)component).GetProperties();
			}
			IComponent component2 = component as IComponent;
			if (component2 != null && component2.Site != null)
			{
				return TypeDescriptor.GetComponentInfo(component2).GetProperties();
			}
			return TypeDescriptor.GetTypeInfo(component.GetType()).GetProperties();
		}

		/// <summary>Returns the collection of properties for a specified type of component using a specified array of attributes as a filter.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that match the specified attributes for this type of component.</returns>
		/// <param name="componentType">The <see cref="T:System.Type" /> of the target component.</param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> to use as a filter. </param>
		// Token: 0x06000F76 RID: 3958 RVA: 0x000284BC File Offset: 0x000266BC
		public static PropertyDescriptorCollection GetProperties(Type componentType, Attribute[] attributes)
		{
			return TypeDescriptor.GetTypeInfo(componentType).GetProperties(attributes);
		}

		/// <summary>Returns the type description provider for the specified component.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> associated with the specified component.</returns>
		/// <param name="instance">An instance of the target component.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is null.</exception>
		// Token: 0x06000F77 RID: 3959 RVA: 0x000284CC File Offset: 0x000266CC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static TypeDescriptionProvider GetProvider(object instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			TypeDescriptionProvider typeDescriptionProvider = null;
			object obj = TypeDescriptor.componentDescriptionProvidersLock;
			lock (obj)
			{
				WeakObjectWrapper key = new WeakObjectWrapper(instance);
				System.Collections.Generic.LinkedList<TypeDescriptionProvider> linkedList;
				if (TypeDescriptor.componentDescriptionProviders.TryGetValue(key, out linkedList) && linkedList.Count > 0)
				{
					typeDescriptionProvider = linkedList.Last.Value;
				}
			}
			if (typeDescriptionProvider == null)
			{
				typeDescriptionProvider = TypeDescriptor.GetProvider(instance.GetType());
			}
			if (typeDescriptionProvider == null)
			{
				return new TypeDescriptor.DefaultTypeDescriptionProvider();
			}
			return new TypeDescriptor.WrappedTypeDescriptionProvider(typeDescriptionProvider);
		}

		/// <summary>Returns the type description provider for the specified type.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> associated with the specified type.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> of the target component.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		// Token: 0x06000F78 RID: 3960 RVA: 0x00028578 File Offset: 0x00026778
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static TypeDescriptionProvider GetProvider(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			TypeDescriptionProvider typeDescriptionProvider = null;
			object obj = TypeDescriptor.typeDescriptionProvidersLock;
			lock (obj)
			{
				System.Collections.Generic.LinkedList<TypeDescriptionProvider> linkedList;
				while (!TypeDescriptor.typeDescriptionProviders.TryGetValue(type, out linkedList))
				{
					linkedList = null;
					type = type.BaseType;
					if (type == null)
					{
						break;
					}
				}
				if (linkedList != null && linkedList.Count > 0)
				{
					typeDescriptionProvider = linkedList.Last.Value;
				}
			}
			if (typeDescriptionProvider == null)
			{
				return new TypeDescriptor.DefaultTypeDescriptionProvider();
			}
			return new TypeDescriptor.WrappedTypeDescriptionProvider(typeDescriptionProvider);
		}

		/// <summary>Returns a <see cref="T:System.Type" /> that can be used to perform reflection, given an object.</summary>
		/// <returns>A <see cref="T:System.Type" /> for the specified object.</returns>
		/// <param name="instance">An instance of the target component.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is null.</exception>
		// Token: 0x06000F79 RID: 3961 RVA: 0x00028628 File Offset: 0x00026828
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static Type GetReflectionType(object instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			return instance.GetType();
		}

		/// <summary>Returns a <see cref="T:System.Type" /> that can be used to perform reflection, given a class type.</summary>
		/// <returns>A <see cref="T:System.Type" /> of the specified class.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> of the target component.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		// Token: 0x06000F7A RID: 3962 RVA: 0x00028644 File Offset: 0x00026844
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static Type GetReflectionType(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return type;
		}

		/// <summary>Creates a primary-secondary association between two objects.</summary>
		/// <param name="primary">The primary <see cref="T:System.Object" />.</param>
		/// <param name="secondary">The secondary <see cref="T:System.Object" />.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="primary" /> is equal to <paramref name="secondary" />.</exception>
		// Token: 0x06000F7B RID: 3963 RVA: 0x00028658 File Offset: 0x00026858
		[MonoNotSupported("Associations not supported")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static void CreateAssociation(object primary, object secondary)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns an instance of the type associated with the specified primary object.</summary>
		/// <returns>An instance of the secondary type that has been associated with the primary object if an association exists; otherwise, <paramref name="primary" /> if no specified association exists.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> of the target component.</param>
		/// <param name="primary">The primary object of the association.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are null.</exception>
		// Token: 0x06000F7C RID: 3964 RVA: 0x00028660 File Offset: 0x00026860
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[MonoNotSupported("Associations not supported")]
		public static object GetAssociation(Type type, object primary)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes an association between two objects.</summary>
		/// <param name="primary">The primary <see cref="T:System.Object" />.</param>
		/// <param name="secondary">The secondary <see cref="T:System.Object" />.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are null.</exception>
		// Token: 0x06000F7D RID: 3965 RVA: 0x00028668 File Offset: 0x00026868
		[MonoNotSupported("Associations not supported")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static void RemoveAssociation(object primary, object secondary)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all associations for a primary object.</summary>
		/// <param name="primary">The primary <see cref="T:System.Object" /> in an association.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="primary" /> is null.</exception>
		// Token: 0x06000F7E RID: 3966 RVA: 0x00028670 File Offset: 0x00026870
		[MonoNotSupported("Associations not supported")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static void RemoveAssociations(object primary)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes a previously added type description provider that is associated with the specified object.</summary>
		/// <param name="provider">The <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> to remove.</param>
		/// <param name="instance">An instance of the target component.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are null.</exception>
		// Token: 0x06000F7F RID: 3967 RVA: 0x00028678 File Offset: 0x00026878
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static void RemoveProvider(TypeDescriptionProvider provider, object instance)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			object obj = TypeDescriptor.componentDescriptionProvidersLock;
			lock (obj)
			{
				WeakObjectWrapper key = new WeakObjectWrapper(instance);
				System.Collections.Generic.LinkedList<TypeDescriptionProvider> linkedList;
				if (TypeDescriptor.componentDescriptionProviders.TryGetValue(key, out linkedList) && linkedList.Count > 0)
				{
					TypeDescriptor.RemoveProvider(provider, linkedList);
				}
			}
			RefreshEventHandler refreshed = TypeDescriptor.Refreshed;
			if (refreshed != null)
			{
				refreshed(new RefreshEventArgs(instance));
			}
		}

		/// <summary>Removes a previously added type description provider that is associated with the specified type.</summary>
		/// <param name="provider">The <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> to remove.</param>
		/// <param name="type">The <see cref="T:System.Type" /> of the target component.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are null.</exception>
		// Token: 0x06000F80 RID: 3968 RVA: 0x00028724 File Offset: 0x00026924
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static void RemoveProvider(TypeDescriptionProvider provider, Type type)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			object obj = TypeDescriptor.typeDescriptionProvidersLock;
			lock (obj)
			{
				System.Collections.Generic.LinkedList<TypeDescriptionProvider> linkedList;
				if (TypeDescriptor.typeDescriptionProviders.TryGetValue(type, out linkedList) && linkedList.Count > 0)
				{
					TypeDescriptor.RemoveProvider(provider, linkedList);
				}
			}
			RefreshEventHandler refreshed = TypeDescriptor.Refreshed;
			if (refreshed != null)
			{
				refreshed(new RefreshEventArgs(type));
			}
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x000287C4 File Offset: 0x000269C4
		private static void RemoveProvider(TypeDescriptionProvider provider, System.Collections.Generic.LinkedList<TypeDescriptionProvider> plist)
		{
			System.Collections.Generic.LinkedListNode<TypeDescriptionProvider> linkedListNode = plist.Last;
			System.Collections.Generic.LinkedListNode<TypeDescriptionProvider> first = plist.First;
			for (;;)
			{
				TypeDescriptionProvider value = linkedListNode.Value;
				if (value == provider)
				{
					break;
				}
				if (linkedListNode == first)
				{
					return;
				}
				linkedListNode = linkedListNode.Previous;
			}
			plist.Remove(linkedListNode);
		}

		/// <summary>Sorts descriptors using the name of the descriptor.</summary>
		/// <param name="infos">An <see cref="T:System.Collections.IList" /> that contains the descriptors to sort. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="infos" /> is null.</exception>
		// Token: 0x06000F82 RID: 3970 RVA: 0x00028814 File Offset: 0x00026A14
		public static void SortDescriptorArray(IList infos)
		{
			string[] array = new string[infos.Count];
			object[] array2 = new object[infos.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = ((MemberDescriptor)infos[i]).Name;
				array2[i] = infos[i];
			}
			Array.Sort<string, object>(array, array2);
			infos.Clear();
			foreach (object value in array2)
			{
				infos.Add(value);
			}
		}

		/// <summary>Gets or sets the provider for the Component Object Model (COM) type information for the target component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.IComNativeDescriptorHandler" /> instance representing the COM type information provider.</returns>
		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000F83 RID: 3971 RVA: 0x000288A4 File Offset: 0x00026AA4
		// (set) Token: 0x06000F84 RID: 3972 RVA: 0x000288AC File Offset: 0x00026AAC
		[Obsolete("Use ComObjectType")]
		public static IComNativeDescriptorHandler ComNativeDescriptorHandler
		{
			[PermissionSet((SecurityAction)14, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"\nUnrestricted=\"true\"/>\n")]
			get
			{
				return TypeDescriptor.descriptorHandler;
			}
			[PermissionSet((SecurityAction)14, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"\nUnrestricted=\"true\"/>\n")]
			set
			{
				TypeDescriptor.descriptorHandler = value;
			}
		}

		/// <summary>Clears the properties and events for the specified assembly from the cache.</summary>
		/// <param name="assembly">The <see cref="T:System.Reflection.Assembly" /> that represents the assembly to refresh. Each <see cref="T:System.Type" /> in this assembly will be refreshed. </param>
		// Token: 0x06000F85 RID: 3973 RVA: 0x000288B4 File Offset: 0x00026AB4
		public static void Refresh(Assembly assembly)
		{
			foreach (Type type in assembly.GetTypes())
			{
				TypeDescriptor.Refresh(type);
			}
		}

		/// <summary>Clears the properties and events for the specified module from the cache.</summary>
		/// <param name="module">The <see cref="T:System.Reflection.Module" /> that represents the module to refresh. Each <see cref="T:System.Type" /> in this module will be refreshed. </param>
		// Token: 0x06000F86 RID: 3974 RVA: 0x000288E8 File Offset: 0x00026AE8
		public static void Refresh(Module module)
		{
			foreach (Type type in module.GetTypes())
			{
				TypeDescriptor.Refresh(type);
			}
		}

		/// <summary>Clears the properties and events for the specified component from the cache.</summary>
		/// <param name="component">A component for which the properties or events have changed. </param>
		// Token: 0x06000F87 RID: 3975 RVA: 0x0002891C File Offset: 0x00026B1C
		public static void Refresh(object component)
		{
			Hashtable obj = TypeDescriptor.componentTable;
			lock (obj)
			{
				TypeDescriptor.componentTable.Remove(component);
			}
			if (TypeDescriptor.Refreshed != null)
			{
				TypeDescriptor.Refreshed(new RefreshEventArgs(component));
			}
		}

		/// <summary>Clears the properties and events for the specified type of component from the cache.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the target component.</param>
		// Token: 0x06000F88 RID: 3976 RVA: 0x00028984 File Offset: 0x00026B84
		public static void Refresh(Type type)
		{
			Hashtable obj = TypeDescriptor.typeTable;
			lock (obj)
			{
				TypeDescriptor.typeTable.Remove(type);
			}
			if (TypeDescriptor.Refreshed != null)
			{
				TypeDescriptor.Refreshed(new RefreshEventArgs(type));
			}
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x000289EC File Offset: 0x00026BEC
		private static void OnComponentDisposed(object sender, EventArgs args)
		{
			Hashtable obj = TypeDescriptor.componentTable;
			lock (obj)
			{
				TypeDescriptor.componentTable.Remove(sender);
			}
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x00028A38 File Offset: 0x00026C38
		internal static ComponentInfo GetComponentInfo(IComponent com)
		{
			Hashtable obj = TypeDescriptor.componentTable;
			ComponentInfo result;
			lock (obj)
			{
				ComponentInfo componentInfo = (ComponentInfo)TypeDescriptor.componentTable[com];
				if (componentInfo == null)
				{
					if (TypeDescriptor.onDispose == null)
					{
						TypeDescriptor.onDispose = new EventHandler(TypeDescriptor.OnComponentDisposed);
					}
					com.Disposed += TypeDescriptor.onDispose;
					componentInfo = new ComponentInfo(com);
					TypeDescriptor.componentTable[com] = componentInfo;
				}
				result = componentInfo;
			}
			return result;
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x00028AD4 File Offset: 0x00026CD4
		internal static TypeInfo GetTypeInfo(Type type)
		{
			Hashtable obj = TypeDescriptor.typeTable;
			TypeInfo result;
			lock (obj)
			{
				TypeInfo typeInfo = (TypeInfo)TypeDescriptor.typeTable[type];
				if (typeInfo == null)
				{
					typeInfo = new TypeInfo(type);
					TypeDescriptor.typeTable[type] = typeInfo;
				}
				result = typeInfo;
			}
			return result;
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x00028B48 File Offset: 0x00026D48
		private static Type GetTypeFromName(IComponent component, string typeName)
		{
			Type type = null;
			if (component != null && component.Site != null)
			{
				System.ComponentModel.Design.ITypeResolutionService typeResolutionService = (System.ComponentModel.Design.ITypeResolutionService)component.Site.GetService(typeof(System.ComponentModel.Design.ITypeResolutionService));
				if (typeResolutionService != null)
				{
					type = typeResolutionService.GetType(typeName);
				}
			}
			if (type == null)
			{
				type = Type.GetType(typeName);
			}
			return type;
		}

		// Token: 0x0400044A RID: 1098
		private static readonly object creatingDefaultConverters = new object();

		// Token: 0x0400044B RID: 1099
		private static ArrayList defaultConverters;

		// Token: 0x0400044C RID: 1100
		private static IComNativeDescriptorHandler descriptorHandler;

		// Token: 0x0400044D RID: 1101
		private static Hashtable componentTable = new Hashtable();

		// Token: 0x0400044E RID: 1102
		private static Hashtable typeTable = new Hashtable();

		// Token: 0x0400044F RID: 1103
		private static Hashtable editors;

		// Token: 0x04000450 RID: 1104
		private static object typeDescriptionProvidersLock = new object();

		// Token: 0x04000451 RID: 1105
		private static Dictionary<Type, System.Collections.Generic.LinkedList<TypeDescriptionProvider>> typeDescriptionProviders;

		// Token: 0x04000452 RID: 1106
		private static object componentDescriptionProvidersLock = new object();

		// Token: 0x04000453 RID: 1107
		private static Dictionary<WeakObjectWrapper, System.Collections.Generic.LinkedList<TypeDescriptionProvider>> componentDescriptionProviders;

		// Token: 0x04000454 RID: 1108
		private static EventHandler onDispose;

		// Token: 0x020001B6 RID: 438
		private sealed class AttributeProvider : TypeDescriptionProvider
		{
			// Token: 0x06000F8D RID: 3981 RVA: 0x00028BA0 File Offset: 0x00026DA0
			public AttributeProvider(Attribute[] attributes, TypeDescriptionProvider parent) : base(parent)
			{
				this.attributes = attributes;
			}

			// Token: 0x06000F8E RID: 3982 RVA: 0x00028BB0 File Offset: 0x00026DB0
			public override ICustomTypeDescriptor GetTypeDescriptor(Type type, object instance)
			{
				return new TypeDescriptor.AttributeProvider.AttributeTypeDescriptor(base.GetTypeDescriptor(type, instance), this.attributes);
			}

			// Token: 0x04000456 RID: 1110
			private Attribute[] attributes;

			// Token: 0x020001B7 RID: 439
			private sealed class AttributeTypeDescriptor : CustomTypeDescriptor
			{
				// Token: 0x06000F8F RID: 3983 RVA: 0x00028BC8 File Offset: 0x00026DC8
				public AttributeTypeDescriptor(ICustomTypeDescriptor parent, Attribute[] attributes) : base(parent)
				{
					this.attributes = attributes;
				}

				// Token: 0x06000F90 RID: 3984 RVA: 0x00028BD8 File Offset: 0x00026DD8
				public override AttributeCollection GetAttributes()
				{
					AttributeCollection attributeCollection = base.GetAttributes();
					if (attributeCollection != null && attributeCollection.Count > 0)
					{
						return AttributeCollection.FromExisting(attributeCollection, this.attributes);
					}
					return new AttributeCollection(this.attributes);
				}

				// Token: 0x04000457 RID: 1111
				private Attribute[] attributes;
			}
		}

		// Token: 0x020001B8 RID: 440
		private sealed class WrappedTypeDescriptionProvider : TypeDescriptionProvider
		{
			// Token: 0x06000F91 RID: 3985 RVA: 0x00028C18 File Offset: 0x00026E18
			public WrappedTypeDescriptionProvider(TypeDescriptionProvider wrapped)
			{
				this.Wrapped = wrapped;
			}

			// Token: 0x17000380 RID: 896
			// (get) Token: 0x06000F92 RID: 3986 RVA: 0x00028C28 File Offset: 0x00026E28
			// (set) Token: 0x06000F93 RID: 3987 RVA: 0x00028C30 File Offset: 0x00026E30
			public TypeDescriptionProvider Wrapped { get; private set; }

			// Token: 0x06000F94 RID: 3988 RVA: 0x00028C3C File Offset: 0x00026E3C
			public override object CreateInstance(IServiceProvider provider, Type objectType, Type[] argTypes, object[] args)
			{
				TypeDescriptionProvider wrapped = this.Wrapped;
				if (wrapped == null)
				{
					return base.CreateInstance(provider, objectType, argTypes, args);
				}
				return wrapped.CreateInstance(provider, objectType, argTypes, args);
			}

			// Token: 0x06000F95 RID: 3989 RVA: 0x00028C70 File Offset: 0x00026E70
			public override IDictionary GetCache(object instance)
			{
				TypeDescriptionProvider wrapped = this.Wrapped;
				if (wrapped == null)
				{
					return base.GetCache(instance);
				}
				return wrapped.GetCache(instance);
			}

			// Token: 0x06000F96 RID: 3990 RVA: 0x00028C9C File Offset: 0x00026E9C
			public override ICustomTypeDescriptor GetExtendedTypeDescriptor(object instance)
			{
				return new TypeDescriptor.DefaultTypeDescriptor(this, null, instance);
			}

			// Token: 0x06000F97 RID: 3991 RVA: 0x00028CA8 File Offset: 0x00026EA8
			public override string GetFullComponentName(object component)
			{
				TypeDescriptionProvider wrapped = this.Wrapped;
				if (wrapped == null)
				{
					return base.GetFullComponentName(component);
				}
				return wrapped.GetFullComponentName(component);
			}

			// Token: 0x06000F98 RID: 3992 RVA: 0x00028CD4 File Offset: 0x00026ED4
			public override Type GetReflectionType(Type type, object instance)
			{
				TypeDescriptionProvider wrapped = this.Wrapped;
				if (wrapped == null)
				{
					return base.GetReflectionType(type, instance);
				}
				return wrapped.GetReflectionType(type, instance);
			}

			// Token: 0x06000F99 RID: 3993 RVA: 0x00028D00 File Offset: 0x00026F00
			public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
			{
				TypeDescriptionProvider wrapped = this.Wrapped;
				if (wrapped == null)
				{
					return new TypeDescriptor.DefaultTypeDescriptor(this, objectType, instance);
				}
				return wrapped.GetTypeDescriptor(objectType, instance);
			}
		}

		// Token: 0x020001B9 RID: 441
		private sealed class DefaultTypeDescriptor : CustomTypeDescriptor
		{
			// Token: 0x06000F9A RID: 3994 RVA: 0x00028D2C File Offset: 0x00026F2C
			public DefaultTypeDescriptor(TypeDescriptionProvider owner, Type objectType, object instance)
			{
				this.owner = owner;
				this.objectType = objectType;
				this.instance = instance;
			}

			// Token: 0x06000F9B RID: 3995 RVA: 0x00028D4C File Offset: 0x00026F4C
			public override AttributeCollection GetAttributes()
			{
				TypeDescriptor.WrappedTypeDescriptionProvider wrappedTypeDescriptionProvider = this.owner as TypeDescriptor.WrappedTypeDescriptionProvider;
				if (wrappedTypeDescriptionProvider != null)
				{
					return wrappedTypeDescriptionProvider.Wrapped.GetTypeDescriptor(this.objectType, this.instance).GetAttributes();
				}
				if (this.instance != null)
				{
					return TypeDescriptor.GetAttributes(this.instance, false);
				}
				if (this.objectType != null)
				{
					return TypeDescriptor.GetTypeInfo(this.objectType).GetAttributes();
				}
				return base.GetAttributes();
			}

			// Token: 0x06000F9C RID: 3996 RVA: 0x00028DC4 File Offset: 0x00026FC4
			public override string GetClassName()
			{
				TypeDescriptor.WrappedTypeDescriptionProvider wrappedTypeDescriptionProvider = this.owner as TypeDescriptor.WrappedTypeDescriptionProvider;
				if (wrappedTypeDescriptionProvider != null)
				{
					return wrappedTypeDescriptionProvider.Wrapped.GetTypeDescriptor(this.objectType, this.instance).GetClassName();
				}
				return base.GetClassName();
			}

			// Token: 0x06000F9D RID: 3997 RVA: 0x00028E08 File Offset: 0x00027008
			public override PropertyDescriptor GetDefaultProperty()
			{
				TypeDescriptor.WrappedTypeDescriptionProvider wrappedTypeDescriptionProvider = this.owner as TypeDescriptor.WrappedTypeDescriptionProvider;
				if (wrappedTypeDescriptionProvider != null)
				{
					return wrappedTypeDescriptionProvider.Wrapped.GetTypeDescriptor(this.objectType, this.instance).GetDefaultProperty();
				}
				PropertyDescriptor defaultProperty;
				if (this.objectType != null)
				{
					defaultProperty = TypeDescriptor.GetTypeInfo(this.objectType).GetDefaultProperty();
				}
				else if (this.instance != null)
				{
					defaultProperty = TypeDescriptor.GetTypeInfo(this.instance.GetType()).GetDefaultProperty();
				}
				else
				{
					defaultProperty = base.GetDefaultProperty();
				}
				return defaultProperty;
			}

			// Token: 0x06000F9E RID: 3998 RVA: 0x00028E94 File Offset: 0x00027094
			public override PropertyDescriptorCollection GetProperties()
			{
				TypeDescriptor.WrappedTypeDescriptionProvider wrappedTypeDescriptionProvider = this.owner as TypeDescriptor.WrappedTypeDescriptionProvider;
				if (wrappedTypeDescriptionProvider != null)
				{
					return wrappedTypeDescriptionProvider.Wrapped.GetTypeDescriptor(this.objectType, this.instance).GetProperties();
				}
				if (this.instance != null)
				{
					return TypeDescriptor.GetProperties(this.instance, null, false);
				}
				if (this.objectType != null)
				{
					return TypeDescriptor.GetTypeInfo(this.objectType).GetProperties(null);
				}
				return base.GetProperties();
			}

			// Token: 0x04000459 RID: 1113
			private TypeDescriptionProvider owner;

			// Token: 0x0400045A RID: 1114
			private Type objectType;

			// Token: 0x0400045B RID: 1115
			private object instance;
		}

		// Token: 0x020001BA RID: 442
		private sealed class DefaultTypeDescriptionProvider : TypeDescriptionProvider
		{
			// Token: 0x06000FA0 RID: 4000 RVA: 0x00028F14 File Offset: 0x00027114
			public override ICustomTypeDescriptor GetExtendedTypeDescriptor(object instance)
			{
				return new TypeDescriptor.DefaultTypeDescriptor(this, null, instance);
			}

			// Token: 0x06000FA1 RID: 4001 RVA: 0x00028F20 File Offset: 0x00027120
			public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
			{
				return new TypeDescriptor.DefaultTypeDescriptor(this, objectType, instance);
			}
		}
	}
}
