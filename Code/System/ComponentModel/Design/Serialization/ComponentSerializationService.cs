using System;
using System.Collections;
using System.IO;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides the base class for serializing a set of components or serializable objects into a serialization store.</summary>
	// Token: 0x02000128 RID: 296
	public abstract class ComponentSerializationService
	{
		/// <summary>Creates a new <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" />.</summary>
		/// <returns>A new <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" />.</returns>
		// Token: 0x06000B4B RID: 2891
		public abstract SerializationStore CreateStore();

		/// <summary>Deserializes the given store to produce a collection of objects.</summary>
		/// <returns>A collection of objects created according to the stored state.</returns>
		/// <param name="store">The <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> to deserialize.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="store" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="store" /> does not contain data in a format the serialization container can process.</exception>
		// Token: 0x06000B4C RID: 2892
		public abstract ICollection Deserialize(SerializationStore store);

		/// <summary>Deserializes the given store and populates the given <see cref="T:System.ComponentModel.IContainer" /> with deserialized <see cref="T:System.ComponentModel.IComponent" /> objects.</summary>
		/// <returns>A collection of objects created according to the stored state.</returns>
		/// <param name="store">The <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> to deserialize.</param>
		/// <param name="container">The <see cref="T:System.ComponentModel.IContainer" /> to which <see cref="T:System.ComponentModel.IComponent" /> objects will be added.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="store" /> or <paramref name="container" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="store" /> does not contain data in a format the serialization container can process.</exception>
		// Token: 0x06000B4D RID: 2893
		public abstract ICollection Deserialize(SerializationStore store, IContainer container);

		/// <summary>Loads a <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> from a stream.</summary>
		/// <returns>A new <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> instance.</returns>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> from which the store will be loaded.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="stream" /> does not contain data saved by a previous call to <see cref="M:System.ComponentModel.Design.Serialization.SerializationStore.Save(System.IO.Stream)" />.</exception>
		// Token: 0x06000B4E RID: 2894
		public abstract SerializationStore LoadStore(Stream stream);

		/// <summary>Serializes the given object to the given <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" />.</summary>
		/// <param name="store">The <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> to which the state of <paramref name="value" /> will be written.</param>
		/// <param name="value">The object to serialize.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="store" /> or <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="store" /> is closed, or <paramref name="store" /> is not a supported type of serialization store. Use a store returned by <see cref="M:System.ComponentModel.Design.Serialization.CodeDomComponentSerializationService.CreateStore" />.</exception>
		// Token: 0x06000B4F RID: 2895
		public abstract void Serialize(SerializationStore store, object value);

		/// <summary>Serializes the given object, accounting for default property values.</summary>
		/// <param name="store">The <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> to which the state of <paramref name="value" /> will be serialized.</param>
		/// <param name="value">The object to serialize.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="store" /> or <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="store" /> is closed, or <paramref name="store" /> is not a supported type of serialization store. Use a store returned by <see cref="M:System.ComponentModel.Design.Serialization.CodeDomComponentSerializationService.CreateStore" />.</exception>
		// Token: 0x06000B50 RID: 2896
		public abstract void SerializeAbsolute(SerializationStore store, object value);

		/// <summary>Serializes the given member on the given object. </summary>
		/// <param name="store">The <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> to which the state of <paramref name="member" /> will be serialized.</param>
		/// <param name="owningObject">The object to which <paramref name="member" /> is attached.</param>
		/// <param name="member">A <see cref="T:System.ComponentModel.MemberDescriptor" /> specifying the member to serialize.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="store" /> or <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="store" /> is closed, or <paramref name="store" /> is not a supported type of serialization store. Use a store returned by <see cref="M:System.ComponentModel.Design.Serialization.CodeDomComponentSerializationService.CreateStore" />.</exception>
		// Token: 0x06000B51 RID: 2897
		public abstract void SerializeMember(SerializationStore store, object owningObject, MemberDescriptor member);

		/// <summary>Serializes the given member on the given object, accounting for the default property value.</summary>
		/// <param name="store">The <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> to which the state of <paramref name="member" /> will be serialized.</param>
		/// <param name="owningObject">The object to which <paramref name="member" /> is attached.</param>
		/// <param name="member">The member to serialize.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="store" /> or <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="store" /> is closed, or <paramref name="store" /> is not a supported type of serialization store. Use a store returned by <see cref="M:System.ComponentModel.Design.Serialization.CodeDomComponentSerializationService.CreateStore" />.</exception>
		// Token: 0x06000B52 RID: 2898
		public abstract void SerializeMemberAbsolute(SerializationStore store, object owningObject, MemberDescriptor member);

		/// <summary>Deserializes the given <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> to the given container.</summary>
		/// <param name="store">The <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> to deserialize.</param>
		/// <param name="container">The container to which <see cref="T:System.ComponentModel.IComponent" /> objects will be added.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="store" /> or <paramref name="container" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="store" /> does not contain data in a format the serialization container can process.</exception>
		// Token: 0x06000B53 RID: 2899 RVA: 0x0001DEDC File Offset: 0x0001C0DC
		public void DeserializeTo(SerializationStore store, IContainer container)
		{
			this.DeserializeTo(store, container, true);
		}

		/// <summary>Deserializes the given <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> to the given container, optionally validating recycled types.</summary>
		/// <param name="store">The <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> to deserialize.</param>
		/// <param name="container">The container to which <see cref="T:System.ComponentModel.IComponent" /> objects will be added.</param>
		/// <param name="validateRecycledTypes">true to guarantee that the deserialization will only work if applied to an object of the same type.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="store" /> or <paramref name="container" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="store" /> does not contain data in a format the serialization container can process.</exception>
		// Token: 0x06000B54 RID: 2900 RVA: 0x0001DEE8 File Offset: 0x0001C0E8
		public void DeserializeTo(SerializationStore store, IContainer container, bool validateRecycledTypes)
		{
			this.DeserializeTo(store, container, validateRecycledTypes, true);
		}

		/// <summary>Deserializes the given <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> to the given container, optionally applying default property values.</summary>
		/// <param name="store">The <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> to deserialize.</param>
		/// <param name="container">The container to which <see cref="T:System.ComponentModel.IComponent" /> objects will be added.</param>
		/// <param name="validateRecycledTypes">true to guarantee that the deserialization will only work if applied to an object of the same type.</param>
		/// <param name="applyDefaults">true to indicate that the default property values should be applied.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="store" /> or <paramref name="container" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="store" /> does not contain data in a format the serialization container can process.</exception>
		// Token: 0x06000B55 RID: 2901
		public abstract void DeserializeTo(SerializationStore store, IContainer container, bool validateRecycledTypes, bool applyDefaults);
	}
}
