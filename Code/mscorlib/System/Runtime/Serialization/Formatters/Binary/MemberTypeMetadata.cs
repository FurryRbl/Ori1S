using System;
using System.IO;
using System.Reflection;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000529 RID: 1321
	internal class MemberTypeMetadata : ClrTypeMetadata
	{
		// Token: 0x06003420 RID: 13344 RVA: 0x000A9B98 File Offset: 0x000A7D98
		public MemberTypeMetadata(Type type, StreamingContext context) : base(type)
		{
			this.members = FormatterServices.GetSerializableMembers(type, context);
		}

		// Token: 0x06003421 RID: 13345 RVA: 0x000A9BB0 File Offset: 0x000A7DB0
		public override void WriteAssemblies(ObjectWriter ow, BinaryWriter writer)
		{
			foreach (FieldInfo fieldInfo in this.members)
			{
				Type type = fieldInfo.FieldType;
				while (type.IsArray)
				{
					type = type.GetElementType();
				}
				ow.WriteAssembly(writer, type.Assembly);
			}
		}

		// Token: 0x06003422 RID: 13346 RVA: 0x000A9C10 File Offset: 0x000A7E10
		public override void WriteTypeData(ObjectWriter ow, BinaryWriter writer, bool writeTypes)
		{
			writer.Write(this.members.Length);
			foreach (FieldInfo fieldInfo in this.members)
			{
				writer.Write(fieldInfo.Name);
			}
			if (writeTypes)
			{
				foreach (FieldInfo fieldInfo2 in this.members)
				{
					ObjectWriter.WriteTypeCode(writer, fieldInfo2.FieldType);
				}
				foreach (FieldInfo fieldInfo3 in this.members)
				{
					ow.WriteTypeSpec(writer, fieldInfo3.FieldType);
				}
			}
		}

		// Token: 0x06003423 RID: 13347 RVA: 0x000A9CD4 File Offset: 0x000A7ED4
		public override void WriteObjectData(ObjectWriter ow, BinaryWriter writer, object data)
		{
			object[] objectData = FormatterServices.GetObjectData(data, this.members);
			for (int i = 0; i < objectData.Length; i++)
			{
				ow.WriteValue(writer, ((FieldInfo)this.members[i]).FieldType, objectData[i]);
			}
		}

		// Token: 0x040015F9 RID: 5625
		private MemberInfo[] members;
	}
}
