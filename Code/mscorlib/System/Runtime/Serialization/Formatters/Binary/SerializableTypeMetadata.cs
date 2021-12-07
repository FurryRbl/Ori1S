using System;
using System.IO;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000528 RID: 1320
	internal class SerializableTypeMetadata : TypeMetadata
	{
		// Token: 0x0600341A RID: 13338 RVA: 0x000A9920 File Offset: 0x000A7B20
		public SerializableTypeMetadata(Type itype, SerializationInfo info)
		{
			this.types = new Type[info.MemberCount];
			this.names = new string[info.MemberCount];
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			int num = 0;
			while (enumerator.MoveNext())
			{
				this.types[num] = enumerator.ObjectType;
				this.names[num] = enumerator.Name;
				num++;
			}
			this.TypeAssemblyName = info.AssemblyName;
			this.InstanceTypeName = info.FullTypeName;
		}

		// Token: 0x0600341B RID: 13339 RVA: 0x000A99A8 File Offset: 0x000A7BA8
		public override bool IsCompatible(TypeMetadata other)
		{
			if (!(other is SerializableTypeMetadata))
			{
				return false;
			}
			SerializableTypeMetadata serializableTypeMetadata = (SerializableTypeMetadata)other;
			if (this.types.Length != serializableTypeMetadata.types.Length)
			{
				return false;
			}
			if (this.TypeAssemblyName != serializableTypeMetadata.TypeAssemblyName)
			{
				return false;
			}
			if (this.InstanceTypeName != serializableTypeMetadata.InstanceTypeName)
			{
				return false;
			}
			for (int i = 0; i < this.types.Length; i++)
			{
				if (this.types[i] != serializableTypeMetadata.types[i])
				{
					return false;
				}
				if (this.names[i] != serializableTypeMetadata.names[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600341C RID: 13340 RVA: 0x000A9A60 File Offset: 0x000A7C60
		public override void WriteAssemblies(ObjectWriter ow, BinaryWriter writer)
		{
			foreach (Type type in this.types)
			{
				Type type2 = type;
				while (type2.IsArray)
				{
					type2 = type2.GetElementType();
				}
				ow.WriteAssembly(writer, type2.Assembly);
			}
		}

		// Token: 0x0600341D RID: 13341 RVA: 0x000A9AB4 File Offset: 0x000A7CB4
		public override void WriteTypeData(ObjectWriter ow, BinaryWriter writer, bool writeTypes)
		{
			writer.Write(this.types.Length);
			foreach (string value in this.names)
			{
				writer.Write(value);
			}
			foreach (Type type in this.types)
			{
				ObjectWriter.WriteTypeCode(writer, type);
			}
			foreach (Type type2 in this.types)
			{
				ow.WriteTypeSpec(writer, type2);
			}
		}

		// Token: 0x0600341E RID: 13342 RVA: 0x000A9B54 File Offset: 0x000A7D54
		public override void WriteObjectData(ObjectWriter ow, BinaryWriter writer, object data)
		{
			SerializationInfo serializationInfo = (SerializationInfo)data;
			SerializationInfoEnumerator enumerator = serializationInfo.GetEnumerator();
			while (enumerator.MoveNext())
			{
				ow.WriteValue(writer, enumerator.ObjectType, enumerator.Value);
			}
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x0600341F RID: 13343 RVA: 0x000A9B94 File Offset: 0x000A7D94
		public override bool RequiresTypes
		{
			get
			{
				return true;
			}
		}

		// Token: 0x040015F7 RID: 5623
		private Type[] types;

		// Token: 0x040015F8 RID: 5624
		private string[] names;
	}
}
