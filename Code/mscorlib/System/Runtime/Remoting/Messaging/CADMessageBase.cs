using System;
using System.Collections;
using System.Runtime.Remoting.Channels;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020004BC RID: 1212
	internal class CADMessageBase
	{
		// Token: 0x06003112 RID: 12562 RVA: 0x000A11A8 File Offset: 0x0009F3A8
		internal static int MarshalProperties(IDictionary dict, ref ArrayList args)
		{
			int num = 0;
			MethodDictionary methodDictionary = dict as MethodDictionary;
			if (methodDictionary != null)
			{
				if (methodDictionary.HasInternalProperties)
				{
					IDictionary internalProperties = methodDictionary.InternalProperties;
					if (internalProperties != null)
					{
						foreach (object obj in internalProperties)
						{
							DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
							if (args == null)
							{
								args = new ArrayList();
							}
							args.Add(dictionaryEntry);
							num++;
						}
					}
				}
			}
			else if (dict != null)
			{
				foreach (object obj2 in dict)
				{
					DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
					if (args == null)
					{
						args = new ArrayList();
					}
					args.Add(dictionaryEntry2);
					num++;
				}
			}
			return num;
		}

		// Token: 0x06003113 RID: 12563 RVA: 0x000A12E4 File Offset: 0x0009F4E4
		internal static void UnmarshalProperties(IDictionary dict, int count, ArrayList args)
		{
			for (int i = 0; i < count; i++)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)args[i];
				dict[dictionaryEntry.Key] = dictionaryEntry.Value;
			}
		}

		// Token: 0x06003114 RID: 12564 RVA: 0x000A1324 File Offset: 0x0009F524
		private static bool IsPossibleToIgnoreMarshal(object obj)
		{
			Type type = obj.GetType();
			return type.IsPrimitive || type == typeof(void) || (type.IsArray && type.GetElementType().IsPrimitive && ((Array)obj).Rank == 1) || (obj is string || obj is DateTime || obj is TimeSpan);
		}

		// Token: 0x06003115 RID: 12565 RVA: 0x000A13A8 File Offset: 0x0009F5A8
		protected object MarshalArgument(object arg, ref ArrayList args)
		{
			if (arg == null)
			{
				return null;
			}
			if (CADMessageBase.IsPossibleToIgnoreMarshal(arg))
			{
				return arg;
			}
			MarshalByRefObject marshalByRefObject = arg as MarshalByRefObject;
			if (marshalByRefObject != null)
			{
				if (!RemotingServices.IsTransparentProxy(marshalByRefObject))
				{
					ObjRef o = RemotingServices.Marshal(marshalByRefObject);
					return new CADObjRef(o, Thread.GetDomainID());
				}
			}
			if (args == null)
			{
				args = new ArrayList();
			}
			args.Add(arg);
			return new CADArgHolder(args.Count - 1);
		}

		// Token: 0x06003116 RID: 12566 RVA: 0x000A1420 File Offset: 0x0009F620
		protected object UnmarshalArgument(object arg, ArrayList args)
		{
			if (arg == null)
			{
				return null;
			}
			CADArgHolder cadargHolder = arg as CADArgHolder;
			if (cadargHolder != null)
			{
				return args[cadargHolder.index];
			}
			CADObjRef cadobjRef = arg as CADObjRef;
			if (cadobjRef != null)
			{
				string typeName = string.Copy(cadobjRef.TypeName);
				string uri = string.Copy(cadobjRef.URI);
				int sourceDomain = cadobjRef.SourceDomain;
				ChannelInfo cinfo = new ChannelInfo(new CrossAppDomainData(sourceDomain));
				ObjRef objectRef = new ObjRef(typeName, uri, cinfo);
				return RemotingServices.Unmarshal(objectRef);
			}
			if (arg is Array)
			{
				Array array = (Array)arg;
				Array array2;
				switch (Type.GetTypeCode(arg.GetType().GetElementType()))
				{
				case TypeCode.Boolean:
					array2 = new bool[array.Length];
					break;
				case TypeCode.Char:
					array2 = new char[array.Length];
					break;
				case TypeCode.SByte:
					array2 = new sbyte[array.Length];
					break;
				case TypeCode.Byte:
					array2 = new byte[array.Length];
					break;
				case TypeCode.Int16:
					array2 = new short[array.Length];
					break;
				case TypeCode.UInt16:
					array2 = new ushort[array.Length];
					break;
				case TypeCode.Int32:
					array2 = new int[array.Length];
					break;
				case TypeCode.UInt32:
					array2 = new uint[array.Length];
					break;
				case TypeCode.Int64:
					array2 = new long[array.Length];
					break;
				case TypeCode.UInt64:
					array2 = new ulong[array.Length];
					break;
				case TypeCode.Single:
					array2 = new float[array.Length];
					break;
				case TypeCode.Double:
					array2 = new double[array.Length];
					break;
				case TypeCode.Decimal:
					array2 = new decimal[array.Length];
					break;
				default:
					throw new NotSupportedException();
				}
				array.CopyTo(array2, 0);
				return array2;
			}
			switch (Type.GetTypeCode(arg.GetType()))
			{
			case TypeCode.Boolean:
				return (bool)arg;
			case TypeCode.Char:
				return (char)arg;
			case TypeCode.SByte:
				return (sbyte)arg;
			case TypeCode.Byte:
				return (byte)arg;
			case TypeCode.Int16:
				return (short)arg;
			case TypeCode.UInt16:
				return (ushort)arg;
			case TypeCode.Int32:
				return (int)arg;
			case TypeCode.UInt32:
				return (uint)arg;
			case TypeCode.Int64:
				return (long)arg;
			case TypeCode.UInt64:
				return (ulong)arg;
			case TypeCode.Single:
				return (float)arg;
			case TypeCode.Double:
				return (double)arg;
			case TypeCode.Decimal:
				return (decimal)arg;
			case TypeCode.DateTime:
				return new DateTime(((DateTime)arg).Ticks);
			case TypeCode.String:
				return string.Copy((string)arg);
			}
			if (arg is TimeSpan)
			{
				return new TimeSpan(((TimeSpan)arg).Ticks);
			}
			if (arg is IntPtr)
			{
				return (IntPtr)arg;
			}
			throw new NotSupportedException("Parameter of type " + arg.GetType() + " cannot be unmarshalled");
		}

		// Token: 0x06003117 RID: 12567 RVA: 0x000A1788 File Offset: 0x0009F988
		internal object[] MarshalArguments(object[] arguments, ref ArrayList args)
		{
			object[] array = new object[arguments.Length];
			int num = arguments.Length;
			for (int i = 0; i < num; i++)
			{
				array[i] = this.MarshalArgument(arguments[i], ref args);
			}
			return array;
		}

		// Token: 0x06003118 RID: 12568 RVA: 0x000A17C4 File Offset: 0x0009F9C4
		internal object[] UnmarshalArguments(object[] arguments, ArrayList args)
		{
			object[] array = new object[arguments.Length];
			int num = arguments.Length;
			for (int i = 0; i < num; i++)
			{
				array[i] = this.UnmarshalArgument(arguments[i], args);
			}
			return array;
		}

		// Token: 0x06003119 RID: 12569 RVA: 0x000A1800 File Offset: 0x0009FA00
		protected void SaveLogicalCallContext(IMethodMessage msg, ref ArrayList serializeList)
		{
			if (msg.LogicalCallContext != null && msg.LogicalCallContext.HasInfo)
			{
				if (serializeList == null)
				{
					serializeList = new ArrayList();
				}
				this._callContext = new CADArgHolder(serializeList.Count);
				serializeList.Add(msg.LogicalCallContext);
			}
		}

		// Token: 0x0600311A RID: 12570 RVA: 0x000A1858 File Offset: 0x0009FA58
		internal LogicalCallContext GetLogicalCallContext(ArrayList args)
		{
			if (this._callContext == null)
			{
				return null;
			}
			return (LogicalCallContext)args[this._callContext.index];
		}

		// Token: 0x040014C4 RID: 5316
		protected object[] _args;

		// Token: 0x040014C5 RID: 5317
		protected byte[] _serializedArgs;

		// Token: 0x040014C6 RID: 5318
		protected int _propertyCount;

		// Token: 0x040014C7 RID: 5319
		protected CADArgHolder _callContext;
	}
}
