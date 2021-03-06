using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Serialization.Formatters;
using System.Security;

namespace System.Runtime.Serialization
{
	/// <summary>Provides static methods to aid with the implementation of a <see cref="T:System.Runtime.Serialization.Formatter" /> for serialization. This class cannot be inherited.</summary>
	// Token: 0x020004F0 RID: 1264
	[ComVisible(true)]
	public sealed class FormatterServices
	{
		// Token: 0x060032D1 RID: 13009 RVA: 0x000A4988 File Offset: 0x000A2B88
		private FormatterServices()
		{
		}

		/// <summary>Extracts the data from the specified object and returns it as an array of objects.</summary>
		/// <returns>An array of <see cref="T:System.Object" /> that contains data stored in <paramref name="members" /> and associated with <paramref name="obj" />.</returns>
		/// <param name="obj">The object to write to the formatter. </param>
		/// <param name="members">The members to extract from the object. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> or <paramref name="members" /> parameter is null.An element of <paramref name="members" /> is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element of <paramref name="members" /> does not represent a field. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x060032D2 RID: 13010 RVA: 0x000A4990 File Offset: 0x000A2B90
		public static object[] GetObjectData(object obj, MemberInfo[] members)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (members == null)
			{
				throw new ArgumentNullException("members");
			}
			int num = members.Length;
			object[] array = new object[num];
			for (int i = 0; i < num; i++)
			{
				MemberInfo memberInfo = members[i];
				if (memberInfo == null)
				{
					throw new ArgumentNullException(string.Format("members[{0}]", i));
				}
				if (memberInfo.MemberType != MemberTypes.Field)
				{
					throw new SerializationException(string.Format("members [{0}] is not a field.", i));
				}
				FieldInfo fieldInfo = memberInfo as FieldInfo;
				array[i] = fieldInfo.GetValue(obj);
			}
			return array;
		}

		/// <summary>Gets all the serializable members for a class of the specified <see cref="T:System.Type" />.</summary>
		/// <returns>An array of type <see cref="T:System.Reflection.MemberInfo" /> of the non-transient, non-static members.</returns>
		/// <param name="type">The type being serialized. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x060032D3 RID: 13011 RVA: 0x000A4A34 File Offset: 0x000A2C34
		public static MemberInfo[] GetSerializableMembers(Type type)
		{
			StreamingContext context = new StreamingContext(StreamingContextStates.All);
			return FormatterServices.GetSerializableMembers(type, context);
		}

		/// <summary>Gets all the serializable members for a class of the specified <see cref="T:System.Type" /> and in the provided <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <returns>An array of type <see cref="T:System.Reflection.MemberInfo" /> of the non-transient, non-static members.</returns>
		/// <param name="type">The type being serialized or cloned. </param>
		/// <param name="context">The context where the serialization occurs. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x060032D4 RID: 13012 RVA: 0x000A4A54 File Offset: 0x000A2C54
		public static MemberInfo[] GetSerializableMembers(Type type, StreamingContext context)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			ArrayList arrayList = new ArrayList();
			for (Type type2 = type; type2 != null; type2 = type2.BaseType)
			{
				if (!type2.IsSerializable)
				{
					string message = string.Format("Type {0} in assembly {1} is not marked as serializable.", type2, type2.Assembly.FullName);
					throw new SerializationException(message);
				}
				FormatterServices.GetFields(type, type2, arrayList);
			}
			MemberInfo[] array = new MemberInfo[arrayList.Count];
			arrayList.CopyTo(array);
			return array;
		}

		// Token: 0x060032D5 RID: 13013 RVA: 0x000A4AD4 File Offset: 0x000A2CD4
		private static void GetFields(Type reflectedType, Type type, ArrayList fields)
		{
			FieldInfo[] fields2 = type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			foreach (FieldInfo fieldInfo in fields2)
			{
				if (!fieldInfo.IsNotSerialized)
				{
					MonoField monoField = fieldInfo as MonoField;
					if (monoField != null && reflectedType != type && !monoField.IsPublic)
					{
						string newName = type.Name + "+" + monoField.Name;
						fields.Add(monoField.Clone(newName));
					}
					else
					{
						fields.Add(fieldInfo);
					}
				}
			}
		}

		/// <summary>Looks up the <see cref="T:System.Type" /> of the specified object in the provided <see cref="T:System.Reflection.Assembly" />.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the object.</returns>
		/// <param name="assem">The assembly where you want to look up the object. </param>
		/// <param name="name">The name of the object. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="assem" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x060032D6 RID: 13014 RVA: 0x000A4B68 File Offset: 0x000A2D68
		public static Type GetTypeFromAssembly(Assembly assem, string name)
		{
			if (assem == null)
			{
				throw new ArgumentNullException("assem");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return assem.GetType(name);
		}

		/// <summary>Creates a new instance of the specified object type.</summary>
		/// <returns>A zeroed object of the specified type.</returns>
		/// <param name="type">The type of object to create. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x060032D7 RID: 13015 RVA: 0x000A4B94 File Offset: 0x000A2D94
		public static object GetUninitializedObject(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type == typeof(string))
			{
				throw new ArgumentException("Uninitialized Strings cannot be created.");
			}
			return ActivationServices.AllocateUninitializedClassInstance(type);
		}

		/// <summary>Populates the specified object with values for each field drawn from the data array of objects.</summary>
		/// <returns>The newly populated object.</returns>
		/// <param name="obj">The object to populate. </param>
		/// <param name="members">An array of <see cref="T:System.Reflection.MemberInfo" /> that describes which fields and properties to populate. </param>
		/// <param name="data">An array of <see cref="T:System.Object" /> that specifies the values for each field and property to populate. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" />, <paramref name="members" />, or <paramref name="data" /> parameter is null.An element of <paramref name="members" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="members" /> does not match the length of <paramref name="data" />. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element of <paramref name="members" /> is not an instance of <see cref="T:System.Reflection.FieldInfo" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x060032D8 RID: 13016 RVA: 0x000A4BD4 File Offset: 0x000A2DD4
		public static object PopulateObjectMembers(object obj, MemberInfo[] members, object[] data)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (members == null)
			{
				throw new ArgumentNullException("members");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			int num = members.Length;
			if (num != data.Length)
			{
				throw new ArgumentException("different length in members and data");
			}
			for (int i = 0; i < num; i++)
			{
				MemberInfo memberInfo = members[i];
				if (memberInfo == null)
				{
					throw new ArgumentNullException(string.Format("members[{0}]", i));
				}
				if (memberInfo.MemberType != MemberTypes.Field)
				{
					throw new SerializationException(string.Format("members [{0}] is not a field.", i));
				}
				FieldInfo fieldInfo = memberInfo as FieldInfo;
				fieldInfo.SetValue(obj, data[i]);
			}
			return obj;
		}

		/// <summary>Determines whether the specified <see cref="T:System.Type" /> can be deserialized with the <see cref="T:System.Runtime.Serialization.Formatters.TypeFilterLevel" /> property set to Low.</summary>
		/// <param name="t">The <see cref="T:System.Type" /> to check for the ability to deserialize. </param>
		/// <param name="securityLevel">The <see cref="T:System.Runtime.Serialization.Formatters.TypeFilterLevel" /> property value. </param>
		/// <exception cref="T:System.Security.SecurityException">The <paramref name="t" /> parameter is an advanced type and cannot be deserialized when the <see cref="T:System.Runtime.Serialization.Formatters.TypeFilterLevel" /> property is set to Low. </exception>
		// Token: 0x060032D9 RID: 13017 RVA: 0x000A4C94 File Offset: 0x000A2E94
		public static void CheckTypeSecurity(Type t, TypeFilterLevel securityLevel)
		{
			if (securityLevel == TypeFilterLevel.Full)
			{
				return;
			}
			FormatterServices.CheckNotAssignable(typeof(DelegateSerializationHolder), t);
			FormatterServices.CheckNotAssignable(typeof(ISponsor), t);
			FormatterServices.CheckNotAssignable(typeof(IEnvoyInfo), t);
			FormatterServices.CheckNotAssignable(typeof(ObjRef), t);
		}

		// Token: 0x060032DA RID: 13018 RVA: 0x000A4CEC File Offset: 0x000A2EEC
		private static void CheckNotAssignable(Type basetype, Type type)
		{
			if (basetype.IsAssignableFrom(type))
			{
				string text = "Type " + basetype + " and the types derived from it";
				string text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					" (such as ",
					type,
					") are not permitted to be deserialized at this security level"
				});
				throw new SecurityException(text);
			}
		}

		/// <summary>Creates a new instance of the specified object type.</summary>
		/// <returns>A zeroed object of the specified type.</returns>
		/// <param name="type">The type of object to create. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <paramref name="type" /> parameter is not a valid common language runtime type. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x060032DB RID: 13019 RVA: 0x000A4D44 File Offset: 0x000A2F44
		public static object GetSafeUninitializedObject(Type type)
		{
			return FormatterServices.GetUninitializedObject(type);
		}

		// Token: 0x0400152C RID: 5420
		private const BindingFlags fieldFlags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
	}
}
