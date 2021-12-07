using System;
using System.Collections;

namespace System.ComponentModel
{
	/// <summary>Provides supplemental metadata to the <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
	// Token: 0x020001B2 RID: 434
	public abstract class TypeDescriptionProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> class.</summary>
		// Token: 0x06000F32 RID: 3890 RVA: 0x00026FFC File Offset: 0x000251FC
		protected TypeDescriptionProvider()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> class using a parent type description provider.</summary>
		/// <param name="parent">The parent type description provider.</param>
		// Token: 0x06000F33 RID: 3891 RVA: 0x00027004 File Offset: 0x00025204
		protected TypeDescriptionProvider(TypeDescriptionProvider parent)
		{
			this._parent = parent;
		}

		/// <summary>Creates an object that can substitute for another data type.</summary>
		/// <returns>The substitute <see cref="T:System.Object" />.</returns>
		/// <param name="provider">An optional service provider.</param>
		/// <param name="objectType">The type of object to create. This parameter is never null.</param>
		/// <param name="argTypes">An optional array of types that represent the parameter types to be passed to the object's constructor. This array can be null or of zero length.</param>
		/// <param name="args">An optional array of parameter values to pass to the object's constructor.</param>
		// Token: 0x06000F34 RID: 3892 RVA: 0x00027014 File Offset: 0x00025214
		public virtual object CreateInstance(IServiceProvider provider, Type objectType, Type[] argTypes, object[] args)
		{
			if (this._parent != null)
			{
				return this._parent.CreateInstance(provider, objectType, argTypes, args);
			}
			return Activator.CreateInstance(objectType, args);
		}

		/// <summary>Gets a per-object cache, accessed as an <see cref="T:System.Collections.IDictionary" /> of key/value pairs.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> if the provided object supports caching; otherwise, null.</returns>
		/// <param name="instance">The object for which to get the cache.</param>
		// Token: 0x06000F35 RID: 3893 RVA: 0x00027048 File Offset: 0x00025248
		public virtual IDictionary GetCache(object instance)
		{
			if (this._parent != null)
			{
				return this._parent.GetCache(instance);
			}
			return null;
		}

		/// <summary>Gets an extended custom type descriptor for the given object.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ICustomTypeDescriptor" /> that can provide extended metadata for the object.</returns>
		/// <param name="instance">The object for which to get the extended type descriptor.</param>
		// Token: 0x06000F36 RID: 3894 RVA: 0x00027064 File Offset: 0x00025264
		public virtual ICustomTypeDescriptor GetExtendedTypeDescriptor(object instance)
		{
			if (this._parent != null)
			{
				return this._parent.GetExtendedTypeDescriptor(instance);
			}
			if (this._emptyCustomTypeDescriptor == null)
			{
				this._emptyCustomTypeDescriptor = new TypeDescriptionProvider.EmptyCustomTypeDescriptor();
			}
			return this._emptyCustomTypeDescriptor;
		}

		/// <summary>Gets the name of the specified component, or null if the component has no name.</summary>
		/// <returns>The name of the specified component.</returns>
		/// <param name="component">The specified component.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null.</exception>
		// Token: 0x06000F37 RID: 3895 RVA: 0x000270A8 File Offset: 0x000252A8
		public virtual string GetFullComponentName(object component)
		{
			if (this._parent != null)
			{
				return this._parent.GetFullComponentName(component);
			}
			return this.GetTypeDescriptor(component).GetComponentName();
		}

		/// <summary>Performs normal reflection against the given object.</summary>
		/// <returns>A <see cref="T:System.Type" />.</returns>
		/// <param name="instance">An instance of the type (should not be null).</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is null.</exception>
		// Token: 0x06000F38 RID: 3896 RVA: 0x000270DC File Offset: 0x000252DC
		public Type GetReflectionType(object instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			return this.GetReflectionType(instance.GetType(), instance);
		}

		/// <summary>Performs normal reflection against a type.</summary>
		/// <returns>A <see cref="T:System.Type" />.</returns>
		/// <param name="objectType">The type of object for which to retrieve the <see cref="T:System.Reflection.IReflect" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="objectType" /> is null.</exception>
		// Token: 0x06000F39 RID: 3897 RVA: 0x000270FC File Offset: 0x000252FC
		public Type GetReflectionType(Type objectType)
		{
			return this.GetReflectionType(objectType, null);
		}

		/// <summary>Performs normal reflection against the given object with the given type.</summary>
		/// <returns>A <see cref="T:System.Type" />.</returns>
		/// <param name="objectType">The type of object for which to retrieve the <see cref="T:System.Reflection.IReflect" />.</param>
		/// <param name="instance">An instance of the type. Can be null.</param>
		// Token: 0x06000F3A RID: 3898 RVA: 0x00027108 File Offset: 0x00025308
		public virtual Type GetReflectionType(Type objectType, object instance)
		{
			if (this._parent != null)
			{
				return this._parent.GetReflectionType(objectType, instance);
			}
			return objectType;
		}

		/// <summary>Gets a custom type descriptor for the given object.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ICustomTypeDescriptor" /> that can provide metadata for the type.</returns>
		/// <param name="instance">An instance of the type. Can be null if no instance was passed to the <see cref="T:System.ComponentModel.TypeDescriptor" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is null.</exception>
		// Token: 0x06000F3B RID: 3899 RVA: 0x00027124 File Offset: 0x00025324
		public ICustomTypeDescriptor GetTypeDescriptor(object instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			return this.GetTypeDescriptor(instance.GetType(), instance);
		}

		/// <summary>Gets a custom type descriptor for the given type.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ICustomTypeDescriptor" /> that can provide metadata for the type.</returns>
		/// <param name="objectType">The type of object for which to retrieve the type descriptor.</param>
		// Token: 0x06000F3C RID: 3900 RVA: 0x00027144 File Offset: 0x00025344
		public ICustomTypeDescriptor GetTypeDescriptor(Type objectType)
		{
			return this.GetTypeDescriptor(objectType, null);
		}

		/// <summary>Gets a custom type descriptor for the given type and object.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ICustomTypeDescriptor" /> that can provide metadata for the type.</returns>
		/// <param name="objectType">The type of object for which to retrieve the type descriptor.</param>
		/// <param name="instance">An instance of the type. Can be null if no instance was passed to the <see cref="T:System.ComponentModel.TypeDescriptor" />.</param>
		// Token: 0x06000F3D RID: 3901 RVA: 0x00027150 File Offset: 0x00025350
		public virtual ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
		{
			if (this._parent != null)
			{
				return this._parent.GetTypeDescriptor(objectType, instance);
			}
			if (this._emptyCustomTypeDescriptor == null)
			{
				this._emptyCustomTypeDescriptor = new TypeDescriptionProvider.EmptyCustomTypeDescriptor();
			}
			return this._emptyCustomTypeDescriptor;
		}

		// Token: 0x04000447 RID: 1095
		private TypeDescriptionProvider.EmptyCustomTypeDescriptor _emptyCustomTypeDescriptor;

		// Token: 0x04000448 RID: 1096
		private TypeDescriptionProvider _parent;

		// Token: 0x020001B3 RID: 435
		private sealed class EmptyCustomTypeDescriptor : CustomTypeDescriptor
		{
		}
	}
}
