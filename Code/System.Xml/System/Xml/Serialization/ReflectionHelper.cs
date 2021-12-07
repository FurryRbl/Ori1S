using System;
using System.Collections;
using System.Reflection;

namespace System.Xml.Serialization
{
	// Token: 0x0200025B RID: 603
	internal class ReflectionHelper
	{
		// Token: 0x06001884 RID: 6276 RVA: 0x0007B984 File Offset: 0x00079B84
		public void RegisterSchemaType(XmlTypeMapping map, string xmlType, string ns)
		{
			string key = xmlType + "/" + ns;
			if (!this._schemaTypes.ContainsKey(key))
			{
				this._schemaTypes.Add(key, map);
			}
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x0007B9BC File Offset: 0x00079BBC
		public XmlTypeMapping GetRegisteredSchemaType(string xmlType, string ns)
		{
			string key = xmlType + "/" + ns;
			return this._schemaTypes[key] as XmlTypeMapping;
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x0007B9E8 File Offset: 0x00079BE8
		public void RegisterClrType(XmlTypeMapping map, Type type, string ns)
		{
			if (type == typeof(object))
			{
				ns = string.Empty;
			}
			string key = type.FullName + "/" + ns;
			if (!this._clrTypes.ContainsKey(key))
			{
				this._clrTypes.Add(key, map);
			}
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x0007BA3C File Offset: 0x00079C3C
		public XmlTypeMapping GetRegisteredClrType(Type type, string ns)
		{
			if (type == typeof(object))
			{
				ns = string.Empty;
			}
			string key = type.FullName + "/" + ns;
			return this._clrTypes[key] as XmlTypeMapping;
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x0007BA84 File Offset: 0x00079C84
		public Exception CreateError(XmlTypeMapping map, string message)
		{
			return new InvalidOperationException("There was an error reflecting '" + map.TypeFullName + "': " + message);
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x0007BAA4 File Offset: 0x00079CA4
		public static void CheckSerializableType(Type type, bool allowPrivateConstructors)
		{
			if (type.IsArray)
			{
				return;
			}
			if (!allowPrivateConstructors && type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, ReflectionHelper.empty_modifiers) == null && !type.IsAbstract && !type.IsValueType)
			{
				throw new InvalidOperationException(type.FullName + " cannot be serialized because it does not have a default public constructor");
			}
			if (type.IsInterface && !TypeTranslator.GetTypeData(type).IsListType)
			{
				throw new InvalidOperationException(type.FullName + " cannot be serialized because it is an interface");
			}
			Type type2 = type;
			while (type2.IsPublic || type2.IsNestedPublic)
			{
				Type type3 = type2;
				type2 = type2.DeclaringType;
				if (type2 == null || type2 == type3)
				{
					return;
				}
			}
			throw new InvalidOperationException(type.FullName + " is inaccessible due to its protection level. Only public types can be processed");
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x0007BB80 File Offset: 0x00079D80
		public static string BuildMapKey(Type type)
		{
			return type.FullName + "::";
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x0007BB94 File Offset: 0x00079D94
		public static string BuildMapKey(MethodInfo method, string tag)
		{
			string text = string.Concat(new string[]
			{
				method.DeclaringType.FullName,
				":",
				method.ReturnType.FullName,
				" ",
				method.Name,
				"("
			});
			ParameterInfo[] parameters = method.GetParameters();
			for (int i = 0; i < parameters.Length; i++)
			{
				if (i > 0)
				{
					text += ", ";
				}
				text += parameters[i].ParameterType.FullName;
			}
			text += ")";
			if (tag != null)
			{
				text = text + ":" + tag;
			}
			return text;
		}

		// Token: 0x04000A19 RID: 2585
		private Hashtable _clrTypes = new Hashtable();

		// Token: 0x04000A1A RID: 2586
		private Hashtable _schemaTypes = new Hashtable();

		// Token: 0x04000A1B RID: 2587
		private static readonly ParameterModifier[] empty_modifiers = new ParameterModifier[0];
	}
}
