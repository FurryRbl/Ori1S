using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200052A RID: 1322
	internal class ObjectWriter
	{
		// Token: 0x06003424 RID: 13348 RVA: 0x000A9D20 File Offset: 0x000A7F20
		public ObjectWriter(ISurrogateSelector surrogateSelector, StreamingContext context, FormatterAssemblyStyle assemblyFormat, FormatterTypeStyle typeFormat)
		{
			this._surrogateSelector = surrogateSelector;
			this._context = context;
			this._assemblyFormat = assemblyFormat;
			this._typeFormat = typeFormat;
			this._manager = new SerializationObjectManager(context);
		}

		// Token: 0x06003426 RID: 13350 RVA: 0x000A9DD8 File Offset: 0x000A7FD8
		public void WriteObjectGraph(BinaryWriter writer, object obj, Header[] headers)
		{
			this._pendingObjects.Clear();
			if (headers != null)
			{
				this.QueueObject(headers);
			}
			this.QueueObject(obj);
			this.WriteQueuedObjects(writer);
			ObjectWriter.WriteSerializationEnd(writer);
			this._manager.RaiseOnSerializedEvent();
		}

		// Token: 0x06003427 RID: 13351 RVA: 0x000A9E1C File Offset: 0x000A801C
		public void QueueObject(object obj)
		{
			this._pendingObjects.Enqueue(obj);
		}

		// Token: 0x06003428 RID: 13352 RVA: 0x000A9E2C File Offset: 0x000A802C
		public void WriteQueuedObjects(BinaryWriter writer)
		{
			while (this._pendingObjects.Count > 0)
			{
				this.WriteObjectInstance(writer, this._pendingObjects.Dequeue(), false);
			}
		}

		// Token: 0x06003429 RID: 13353 RVA: 0x000A9E64 File Offset: 0x000A8064
		public void WriteObjectInstance(BinaryWriter writer, object obj, bool isValueObject)
		{
			long id;
			if (isValueObject)
			{
				id = this._idGenerator.NextId;
			}
			else
			{
				bool flag;
				id = this._idGenerator.GetId(obj, out flag);
			}
			if (obj is string)
			{
				this.WriteString(writer, id, (string)obj);
			}
			else if (obj is Array)
			{
				this.WriteArray(writer, id, (Array)obj);
			}
			else
			{
				this.WriteObject(writer, id, obj);
			}
		}

		// Token: 0x0600342A RID: 13354 RVA: 0x000A9EDC File Offset: 0x000A80DC
		public static void WriteSerializationEnd(BinaryWriter writer)
		{
			writer.Write(11);
		}

		// Token: 0x0600342B RID: 13355 RVA: 0x000A9EE8 File Offset: 0x000A80E8
		private void WriteObject(BinaryWriter writer, long id, object obj)
		{
			TypeMetadata typeMetadata;
			object data;
			this.GetObjectData(obj, out typeMetadata, out data);
			ObjectWriter.MetadataReference metadataReference = (ObjectWriter.MetadataReference)this._cachedMetadata[typeMetadata.InstanceTypeName];
			if (metadataReference != null && typeMetadata.IsCompatible(metadataReference.Metadata))
			{
				writer.Write(1);
				writer.Write((int)id);
				writer.Write((int)metadataReference.ObjectID);
				typeMetadata.WriteObjectData(this, writer, data);
				return;
			}
			if (metadataReference == null)
			{
				metadataReference = new ObjectWriter.MetadataReference(typeMetadata, id);
				this._cachedMetadata[typeMetadata.InstanceTypeName] = metadataReference;
			}
			bool flag = typeMetadata.RequiresTypes || this._typeFormat == FormatterTypeStyle.TypesAlways;
			BinaryElement value;
			int num;
			if (typeMetadata.TypeAssemblyName == ObjectWriter.CorlibAssemblyName)
			{
				value = ((!flag) ? BinaryElement.UntypedRuntimeObject : BinaryElement.RuntimeObject);
				num = -1;
			}
			else
			{
				value = ((!flag) ? BinaryElement.UntypedExternalObject : BinaryElement.ExternalObject);
				num = this.WriteAssemblyName(writer, typeMetadata.TypeAssemblyName);
			}
			typeMetadata.WriteAssemblies(this, writer);
			writer.Write((byte)value);
			writer.Write((int)id);
			writer.Write(typeMetadata.InstanceTypeName);
			typeMetadata.WriteTypeData(this, writer, flag);
			if (num != -1)
			{
				writer.Write(num);
			}
			typeMetadata.WriteObjectData(this, writer, data);
		}

		// Token: 0x0600342C RID: 13356 RVA: 0x000AA01C File Offset: 0x000A821C
		private void GetObjectData(object obj, out TypeMetadata metadata, out object data)
		{
			Type type = obj.GetType();
			if (this._surrogateSelector != null)
			{
				ISurrogateSelector surrogateSelector;
				ISerializationSurrogate surrogate = this._surrogateSelector.GetSurrogate(type, this._context, out surrogateSelector);
				if (surrogate != null)
				{
					SerializationInfo serializationInfo = new SerializationInfo(type, new FormatterConverter());
					surrogate.GetObjectData(obj, serializationInfo, this._context);
					metadata = new SerializableTypeMetadata(type, serializationInfo);
					data = serializationInfo;
					return;
				}
			}
			BinaryCommon.CheckSerializable(type, this._surrogateSelector, this._context);
			this._manager.RegisterObject(obj);
			ISerializable serializable = obj as ISerializable;
			if (serializable != null)
			{
				SerializationInfo serializationInfo2 = new SerializationInfo(type, new FormatterConverter());
				serializable.GetObjectData(serializationInfo2, this._context);
				metadata = new SerializableTypeMetadata(type, serializationInfo2);
				data = serializationInfo2;
			}
			else
			{
				data = obj;
				if (this._context.Context != null)
				{
					metadata = new MemberTypeMetadata(type, this._context);
					return;
				}
				bool flag = false;
				Hashtable cachedTypes = ObjectWriter._cachedTypes;
				Hashtable hashtable;
				lock (cachedTypes)
				{
					hashtable = (Hashtable)ObjectWriter._cachedTypes[this._context.State];
					if (hashtable == null)
					{
						hashtable = new Hashtable();
						ObjectWriter._cachedTypes[this._context.State] = hashtable;
						flag = true;
					}
				}
				metadata = null;
				Hashtable obj2 = hashtable;
				lock (obj2)
				{
					if (!flag)
					{
						metadata = (TypeMetadata)hashtable[type];
					}
					if (metadata == null)
					{
						metadata = this.CreateMemberTypeMetadata(type);
					}
					hashtable[type] = metadata;
				}
			}
		}

		// Token: 0x0600342D RID: 13357 RVA: 0x000AA1E8 File Offset: 0x000A83E8
		private TypeMetadata CreateMemberTypeMetadata(Type type)
		{
			if (!BinaryCommon.UseReflectionSerialization)
			{
				Type type2 = CodeGenerator.GenerateMetadataType(type, this._context);
				return (TypeMetadata)Activator.CreateInstance(type2);
			}
			return new MemberTypeMetadata(type, this._context);
		}

		// Token: 0x0600342E RID: 13358 RVA: 0x000AA224 File Offset: 0x000A8424
		private void WriteArray(BinaryWriter writer, long id, Array array)
		{
			Type elementType = array.GetType().GetElementType();
			if (elementType == typeof(object) && array.Rank == 1)
			{
				this.WriteObjectArray(writer, id, array);
			}
			else if (elementType == typeof(string) && array.Rank == 1)
			{
				this.WriteStringArray(writer, id, array);
			}
			else if (BinaryCommon.IsPrimitive(elementType) && array.Rank == 1)
			{
				this.WritePrimitiveTypeArray(writer, id, array);
			}
			else
			{
				this.WriteGenericArray(writer, id, array);
			}
		}

		// Token: 0x0600342F RID: 13359 RVA: 0x000AA2C0 File Offset: 0x000A84C0
		private void WriteGenericArray(BinaryWriter writer, long id, Array array)
		{
			Type elementType = array.GetType().GetElementType();
			if (!elementType.IsArray)
			{
				this.WriteAssembly(writer, elementType.Assembly);
			}
			writer.Write(7);
			writer.Write((int)id);
			if (elementType.IsArray)
			{
				writer.Write(1);
			}
			else if (array.Rank == 1)
			{
				writer.Write(0);
			}
			else
			{
				writer.Write(2);
			}
			writer.Write(array.Rank);
			for (int i = 0; i < array.Rank; i++)
			{
				writer.Write(array.GetUpperBound(i) + 1);
			}
			ObjectWriter.WriteTypeCode(writer, elementType);
			this.WriteTypeSpec(writer, elementType);
			if (array.Rank == 1 && !elementType.IsValueType)
			{
				this.WriteSingleDimensionArrayElements(writer, array, elementType);
			}
			else
			{
				foreach (object val in array)
				{
					this.WriteValue(writer, elementType, val);
				}
			}
		}

		// Token: 0x06003430 RID: 13360 RVA: 0x000AA3FC File Offset: 0x000A85FC
		private void WriteObjectArray(BinaryWriter writer, long id, Array array)
		{
			writer.Write(16);
			writer.Write((int)id);
			writer.Write(array.Length);
			this.WriteSingleDimensionArrayElements(writer, array, typeof(object));
		}

		// Token: 0x06003431 RID: 13361 RVA: 0x000AA438 File Offset: 0x000A8638
		private void WriteStringArray(BinaryWriter writer, long id, Array array)
		{
			writer.Write(17);
			writer.Write((int)id);
			writer.Write(array.Length);
			this.WriteSingleDimensionArrayElements(writer, array, typeof(string));
		}

		// Token: 0x06003432 RID: 13362 RVA: 0x000AA474 File Offset: 0x000A8674
		private void WritePrimitiveTypeArray(BinaryWriter writer, long id, Array array)
		{
			writer.Write(15);
			writer.Write((int)id);
			writer.Write(array.Length);
			Type elementType = array.GetType().GetElementType();
			this.WriteTypeSpec(writer, elementType);
			switch (Type.GetTypeCode(elementType))
			{
			case TypeCode.Boolean:
				foreach (bool value in (bool[])array)
				{
					writer.Write(value);
				}
				return;
			case TypeCode.Char:
				writer.Write((char[])array);
				return;
			case TypeCode.SByte:
				if (array.Length > 2)
				{
					this.BlockWrite(writer, array, 1);
				}
				else
				{
					foreach (sbyte value2 in (sbyte[])array)
					{
						writer.Write(value2);
					}
				}
				return;
			case TypeCode.Byte:
				writer.Write((byte[])array);
				return;
			case TypeCode.Int16:
				if (array.Length > 2)
				{
					this.BlockWrite(writer, array, 2);
				}
				else
				{
					foreach (short value3 in (short[])array)
					{
						writer.Write(value3);
					}
				}
				return;
			case TypeCode.UInt16:
				if (array.Length > 2)
				{
					this.BlockWrite(writer, array, 2);
				}
				else
				{
					foreach (ushort value4 in (ushort[])array)
					{
						writer.Write(value4);
					}
				}
				return;
			case TypeCode.Int32:
				if (array.Length > 2)
				{
					this.BlockWrite(writer, array, 4);
				}
				else
				{
					foreach (int value5 in (int[])array)
					{
						writer.Write(value5);
					}
				}
				return;
			case TypeCode.UInt32:
				if (array.Length > 2)
				{
					this.BlockWrite(writer, array, 4);
				}
				else
				{
					foreach (uint value6 in (uint[])array)
					{
						writer.Write(value6);
					}
				}
				return;
			case TypeCode.Int64:
				if (array.Length > 2)
				{
					this.BlockWrite(writer, array, 8);
				}
				else
				{
					foreach (long value7 in (long[])array)
					{
						writer.Write(value7);
					}
				}
				return;
			case TypeCode.UInt64:
				if (array.Length > 2)
				{
					this.BlockWrite(writer, array, 8);
				}
				else
				{
					foreach (ulong value8 in (ulong[])array)
					{
						writer.Write(value8);
					}
				}
				return;
			case TypeCode.Single:
				if (array.Length > 2)
				{
					this.BlockWrite(writer, array, 4);
				}
				else
				{
					foreach (float value9 in (float[])array)
					{
						writer.Write(value9);
					}
				}
				return;
			case TypeCode.Double:
				if (array.Length > 2)
				{
					this.BlockWrite(writer, array, 8);
				}
				else
				{
					foreach (double value10 in (double[])array)
					{
						writer.Write(value10);
					}
				}
				return;
			case TypeCode.Decimal:
				foreach (decimal value11 in (decimal[])array)
				{
					writer.Write(value11);
				}
				return;
			case TypeCode.DateTime:
				foreach (DateTime dateTime in (DateTime[])array)
				{
					writer.Write(dateTime.ToBinary());
				}
				return;
			case TypeCode.String:
				foreach (string value12 in (string[])array)
				{
					writer.Write(value12);
				}
				return;
			}
			if (elementType != typeof(TimeSpan))
			{
				throw new NotSupportedException("Unsupported primitive type: " + elementType.FullName);
			}
			foreach (TimeSpan timeSpan in (TimeSpan[])array)
			{
				writer.Write(timeSpan.Ticks);
			}
		}

		// Token: 0x06003433 RID: 13363 RVA: 0x000AA944 File Offset: 0x000A8B44
		private void BlockWrite(BinaryWriter writer, Array array, int dataSize)
		{
			int i = Buffer.ByteLength(array);
			if (this.arrayBuffer == null || (i > this.arrayBuffer.Length && this.arrayBuffer.Length != this.ArrayBufferLength))
			{
				this.arrayBuffer = new byte[(i > this.ArrayBufferLength) ? this.ArrayBufferLength : i];
			}
			int num = 0;
			while (i > 0)
			{
				int num2 = (i >= this.arrayBuffer.Length) ? this.arrayBuffer.Length : i;
				Buffer.BlockCopy(array, num, this.arrayBuffer, 0, num2);
				if (!BitConverter.IsLittleEndian && dataSize > 1)
				{
					BinaryCommon.SwapBytes(this.arrayBuffer, num2, dataSize);
				}
				writer.Write(this.arrayBuffer, 0, num2);
				i -= num2;
				num += num2;
			}
		}

		// Token: 0x06003434 RID: 13364 RVA: 0x000AAA18 File Offset: 0x000A8C18
		private void WriteSingleDimensionArrayElements(BinaryWriter writer, Array array, Type elementType)
		{
			int num = 0;
			foreach (object obj in array)
			{
				if (obj != null && num > 0)
				{
					this.WriteNullFiller(writer, num);
					this.WriteValue(writer, elementType, obj);
					num = 0;
				}
				else if (obj == null)
				{
					num++;
				}
				else
				{
					this.WriteValue(writer, elementType, obj);
				}
			}
			if (num > 0)
			{
				this.WriteNullFiller(writer, num);
			}
		}

		// Token: 0x06003435 RID: 13365 RVA: 0x000AAAC4 File Offset: 0x000A8CC4
		private void WriteNullFiller(BinaryWriter writer, int numNulls)
		{
			if (numNulls == 1)
			{
				writer.Write(10);
			}
			else if (numNulls == 2)
			{
				writer.Write(10);
				writer.Write(10);
			}
			else if (numNulls <= 255)
			{
				writer.Write(13);
				writer.Write((byte)numNulls);
			}
			else
			{
				writer.Write(14);
				writer.Write(numNulls);
			}
		}

		// Token: 0x06003436 RID: 13366 RVA: 0x000AAB30 File Offset: 0x000A8D30
		private void WriteObjectReference(BinaryWriter writer, long id)
		{
			writer.Write(9);
			writer.Write((int)id);
		}

		// Token: 0x06003437 RID: 13367 RVA: 0x000AAB44 File Offset: 0x000A8D44
		public void WriteValue(BinaryWriter writer, Type valueType, object val)
		{
			if (val == null)
			{
				BinaryCommon.CheckSerializable(valueType, this._surrogateSelector, this._context);
				writer.Write(10);
			}
			else if (BinaryCommon.IsPrimitive(val.GetType()))
			{
				if (!BinaryCommon.IsPrimitive(valueType))
				{
					writer.Write(8);
					this.WriteTypeSpec(writer, val.GetType());
				}
				ObjectWriter.WritePrimitiveValue(writer, val);
			}
			else if (valueType.IsValueType)
			{
				this.WriteObjectInstance(writer, val, true);
			}
			else if (val is string)
			{
				bool flag;
				long id = this._idGenerator.GetId(val, out flag);
				if (flag)
				{
					this.WriteObjectInstance(writer, val, false);
				}
				else
				{
					this.WriteObjectReference(writer, id);
				}
			}
			else
			{
				bool flag2;
				long id2 = this._idGenerator.GetId(val, out flag2);
				if (flag2)
				{
					this._pendingObjects.Enqueue(val);
				}
				this.WriteObjectReference(writer, id2);
			}
		}

		// Token: 0x06003438 RID: 13368 RVA: 0x000AAC30 File Offset: 0x000A8E30
		private void WriteString(BinaryWriter writer, long id, string str)
		{
			writer.Write(6);
			writer.Write((int)id);
			writer.Write(str);
		}

		// Token: 0x06003439 RID: 13369 RVA: 0x000AAC54 File Offset: 0x000A8E54
		public int WriteAssembly(BinaryWriter writer, Assembly assembly)
		{
			return this.WriteAssemblyName(writer, assembly.FullName);
		}

		// Token: 0x0600343A RID: 13370 RVA: 0x000AAC64 File Offset: 0x000A8E64
		public int WriteAssemblyName(BinaryWriter writer, string assembly)
		{
			if (assembly == ObjectWriter.CorlibAssemblyName)
			{
				return -1;
			}
			bool flag;
			int num = this.RegisterAssembly(assembly, out flag);
			if (!flag)
			{
				return num;
			}
			writer.Write(12);
			writer.Write(num);
			if (this._assemblyFormat == FormatterAssemblyStyle.Full)
			{
				writer.Write(assembly);
			}
			else
			{
				int num2 = assembly.IndexOf(',');
				if (num2 != -1)
				{
					assembly = assembly.Substring(0, num2);
				}
				writer.Write(assembly);
			}
			return num;
		}

		// Token: 0x0600343B RID: 13371 RVA: 0x000AACE0 File Offset: 0x000A8EE0
		public int GetAssemblyId(Assembly assembly)
		{
			return this.GetAssemblyNameId(assembly.FullName);
		}

		// Token: 0x0600343C RID: 13372 RVA: 0x000AACF0 File Offset: 0x000A8EF0
		public int GetAssemblyNameId(string assembly)
		{
			return (int)this._assemblyCache[assembly];
		}

		// Token: 0x0600343D RID: 13373 RVA: 0x000AAD04 File Offset: 0x000A8F04
		private int RegisterAssembly(string assembly, out bool firstTime)
		{
			if (this._assemblyCache.ContainsKey(assembly))
			{
				firstTime = false;
				return (int)this._assemblyCache[assembly];
			}
			int num = (int)this._idGenerator.GetId(0, out firstTime);
			this._assemblyCache.Add(assembly, num);
			return num;
		}

		// Token: 0x0600343E RID: 13374 RVA: 0x000AAD60 File Offset: 0x000A8F60
		public static void WritePrimitiveValue(BinaryWriter writer, object value)
		{
			Type type = value.GetType();
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Boolean:
				writer.Write((bool)value);
				return;
			case TypeCode.Char:
				writer.Write((char)value);
				return;
			case TypeCode.SByte:
				writer.Write((sbyte)value);
				return;
			case TypeCode.Byte:
				writer.Write((byte)value);
				return;
			case TypeCode.Int16:
				writer.Write((short)value);
				return;
			case TypeCode.UInt16:
				writer.Write((ushort)value);
				return;
			case TypeCode.Int32:
				writer.Write((int)value);
				return;
			case TypeCode.UInt32:
				writer.Write((uint)value);
				return;
			case TypeCode.Int64:
				writer.Write((long)value);
				return;
			case TypeCode.UInt64:
				writer.Write((ulong)value);
				return;
			case TypeCode.Single:
				writer.Write((float)value);
				return;
			case TypeCode.Double:
				writer.Write((double)value);
				return;
			case TypeCode.Decimal:
				writer.Write(((decimal)value).ToString(CultureInfo.InvariantCulture));
				return;
			case TypeCode.DateTime:
				writer.Write(((DateTime)value).ToBinary());
				return;
			case TypeCode.String:
				writer.Write((string)value);
				return;
			}
			if (type != typeof(TimeSpan))
			{
				throw new NotSupportedException("Unsupported primitive type: " + value.GetType().FullName);
			}
			writer.Write(((TimeSpan)value).Ticks);
		}

		// Token: 0x0600343F RID: 13375 RVA: 0x000AAF28 File Offset: 0x000A9128
		public static void WriteTypeCode(BinaryWriter writer, Type type)
		{
			writer.Write((byte)ObjectWriter.GetTypeTag(type));
		}

		// Token: 0x06003440 RID: 13376 RVA: 0x000AAF38 File Offset: 0x000A9138
		public static TypeTag GetTypeTag(Type type)
		{
			if (type == typeof(string))
			{
				return TypeTag.String;
			}
			if (BinaryCommon.IsPrimitive(type))
			{
				return TypeTag.PrimitiveType;
			}
			if (type == typeof(object))
			{
				return TypeTag.ObjectType;
			}
			if (type.IsArray && type.GetArrayRank() == 1 && type.GetElementType() == typeof(object))
			{
				return TypeTag.ArrayOfObject;
			}
			if (type.IsArray && type.GetArrayRank() == 1 && type.GetElementType() == typeof(string))
			{
				return TypeTag.ArrayOfString;
			}
			if (type.IsArray && type.GetArrayRank() == 1 && BinaryCommon.IsPrimitive(type.GetElementType()))
			{
				return TypeTag.ArrayOfPrimitiveType;
			}
			if (type.Assembly == ObjectWriter.CorlibAssembly)
			{
				return TypeTag.RuntimeType;
			}
			return TypeTag.GenericType;
		}

		// Token: 0x06003441 RID: 13377 RVA: 0x000AB010 File Offset: 0x000A9210
		public void WriteTypeSpec(BinaryWriter writer, Type type)
		{
			switch (ObjectWriter.GetTypeTag(type))
			{
			case TypeTag.PrimitiveType:
				writer.Write(BinaryCommon.GetTypeCode(type));
				break;
			case TypeTag.RuntimeType:
			{
				string value = type.FullName;
				if (this._context.State == StreamingContextStates.Remoting)
				{
					if (type == typeof(MonoType))
					{
						value = "System.RuntimeType";
					}
					else if (type == typeof(MonoType[]))
					{
						value = "System.RuntimeType[]";
					}
				}
				writer.Write(value);
				break;
			}
			case TypeTag.GenericType:
				writer.Write(type.FullName);
				writer.Write(this.GetAssemblyId(type.Assembly));
				break;
			case TypeTag.ArrayOfPrimitiveType:
				writer.Write(BinaryCommon.GetTypeCode(type.GetElementType()));
				break;
			}
		}

		// Token: 0x040015FA RID: 5626
		private ObjectIDGenerator _idGenerator = new ObjectIDGenerator();

		// Token: 0x040015FB RID: 5627
		private Hashtable _cachedMetadata = new Hashtable();

		// Token: 0x040015FC RID: 5628
		private Queue _pendingObjects = new Queue();

		// Token: 0x040015FD RID: 5629
		private Hashtable _assemblyCache = new Hashtable();

		// Token: 0x040015FE RID: 5630
		private static Hashtable _cachedTypes = new Hashtable();

		// Token: 0x040015FF RID: 5631
		internal static Assembly CorlibAssembly = typeof(string).Assembly;

		// Token: 0x04001600 RID: 5632
		internal static string CorlibAssemblyName = typeof(string).Assembly.FullName;

		// Token: 0x04001601 RID: 5633
		private ISurrogateSelector _surrogateSelector;

		// Token: 0x04001602 RID: 5634
		private StreamingContext _context;

		// Token: 0x04001603 RID: 5635
		private FormatterAssemblyStyle _assemblyFormat;

		// Token: 0x04001604 RID: 5636
		private FormatterTypeStyle _typeFormat;

		// Token: 0x04001605 RID: 5637
		private byte[] arrayBuffer;

		// Token: 0x04001606 RID: 5638
		private int ArrayBufferLength = 4096;

		// Token: 0x04001607 RID: 5639
		private SerializationObjectManager _manager;

		// Token: 0x0200052B RID: 1323
		private class MetadataReference
		{
			// Token: 0x06003442 RID: 13378 RVA: 0x000AB0F4 File Offset: 0x000A92F4
			public MetadataReference(TypeMetadata metadata, long id)
			{
				this.Metadata = metadata;
				this.ObjectID = id;
			}

			// Token: 0x04001608 RID: 5640
			public TypeMetadata Metadata;

			// Token: 0x04001609 RID: 5641
			public long ObjectID;
		}
	}
}
