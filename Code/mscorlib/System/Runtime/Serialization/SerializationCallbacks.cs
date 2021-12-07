using System;
using System.Collections;
using System.Reflection;

namespace System.Runtime.Serialization
{
	// Token: 0x02000507 RID: 1287
	internal sealed class SerializationCallbacks
	{
		// Token: 0x06003336 RID: 13110 RVA: 0x000A5D94 File Offset: 0x000A3F94
		public SerializationCallbacks(Type type)
		{
			this.onSerializingList = SerializationCallbacks.GetMethodsByAttribute(type, typeof(OnSerializingAttribute));
			this.onSerializedList = SerializationCallbacks.GetMethodsByAttribute(type, typeof(OnSerializedAttribute));
			this.onDeserializingList = SerializationCallbacks.GetMethodsByAttribute(type, typeof(OnDeserializingAttribute));
			this.onDeserializedList = SerializationCallbacks.GetMethodsByAttribute(type, typeof(OnDeserializedAttribute));
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06003338 RID: 13112 RVA: 0x000A5E18 File Offset: 0x000A4018
		public bool HasSerializingCallbacks
		{
			get
			{
				return this.onSerializingList != null;
			}
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06003339 RID: 13113 RVA: 0x000A5E28 File Offset: 0x000A4028
		public bool HasSerializedCallbacks
		{
			get
			{
				return this.onSerializedList != null;
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x0600333A RID: 13114 RVA: 0x000A5E38 File Offset: 0x000A4038
		public bool HasDeserializingCallbacks
		{
			get
			{
				return this.onDeserializingList != null;
			}
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x0600333B RID: 13115 RVA: 0x000A5E48 File Offset: 0x000A4048
		public bool HasDeserializedCallbacks
		{
			get
			{
				return this.onDeserializedList != null;
			}
		}

		// Token: 0x0600333C RID: 13116 RVA: 0x000A5E58 File Offset: 0x000A4058
		private static ArrayList GetMethodsByAttribute(Type type, Type attr)
		{
			ArrayList arrayList = new ArrayList();
			for (Type type2 = type; type2 != typeof(object); type2 = type2.BaseType)
			{
				int num = 0;
				foreach (MethodInfo methodInfo in type2.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
				{
					if (methodInfo.IsDefined(attr, false))
					{
						arrayList.Add(methodInfo);
						num++;
					}
				}
				if (num > 1)
				{
					throw new TypeLoadException(string.Format("Type '{0}' has more than one method with the following attribute: '{1}'.", type.AssemblyQualifiedName, attr.FullName));
				}
			}
			return (arrayList.Count != 0) ? arrayList : null;
		}

		// Token: 0x0600333D RID: 13117 RVA: 0x000A5F04 File Offset: 0x000A4104
		private static void Invoke(ArrayList list, object target, StreamingContext context)
		{
			if (list == null)
			{
				return;
			}
			SerializationCallbacks.CallbackHandler callbackHandler = null;
			foreach (object obj in list)
			{
				MethodInfo method = (MethodInfo)obj;
				callbackHandler = (SerializationCallbacks.CallbackHandler)Delegate.Combine(Delegate.CreateDelegate(typeof(SerializationCallbacks.CallbackHandler), target, method), callbackHandler);
			}
			callbackHandler(context);
		}

		// Token: 0x0600333E RID: 13118 RVA: 0x000A5F94 File Offset: 0x000A4194
		public void RaiseOnSerializing(object target, StreamingContext contex)
		{
			SerializationCallbacks.Invoke(this.onSerializingList, target, contex);
		}

		// Token: 0x0600333F RID: 13119 RVA: 0x000A5FA4 File Offset: 0x000A41A4
		public void RaiseOnSerialized(object target, StreamingContext contex)
		{
			SerializationCallbacks.Invoke(this.onSerializedList, target, contex);
		}

		// Token: 0x06003340 RID: 13120 RVA: 0x000A5FB4 File Offset: 0x000A41B4
		public void RaiseOnDeserializing(object target, StreamingContext contex)
		{
			SerializationCallbacks.Invoke(this.onDeserializingList, target, contex);
		}

		// Token: 0x06003341 RID: 13121 RVA: 0x000A5FC4 File Offset: 0x000A41C4
		public void RaiseOnDeserialized(object target, StreamingContext contex)
		{
			SerializationCallbacks.Invoke(this.onDeserializedList, target, contex);
		}

		// Token: 0x06003342 RID: 13122 RVA: 0x000A5FD4 File Offset: 0x000A41D4
		public static SerializationCallbacks GetSerializationCallbacks(Type t)
		{
			SerializationCallbacks serializationCallbacks = (SerializationCallbacks)SerializationCallbacks.cache[t];
			if (serializationCallbacks != null)
			{
				return serializationCallbacks;
			}
			object obj = SerializationCallbacks.cache_lock;
			SerializationCallbacks result;
			lock (obj)
			{
				serializationCallbacks = (SerializationCallbacks)SerializationCallbacks.cache[t];
				if (serializationCallbacks == null)
				{
					Hashtable hashtable = (Hashtable)SerializationCallbacks.cache.Clone();
					serializationCallbacks = new SerializationCallbacks(t);
					hashtable[t] = serializationCallbacks;
					SerializationCallbacks.cache = hashtable;
				}
				result = serializationCallbacks;
			}
			return result;
		}

		// Token: 0x04001554 RID: 5460
		private const BindingFlags DefaultBindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x04001555 RID: 5461
		private readonly ArrayList onSerializingList;

		// Token: 0x04001556 RID: 5462
		private readonly ArrayList onSerializedList;

		// Token: 0x04001557 RID: 5463
		private readonly ArrayList onDeserializingList;

		// Token: 0x04001558 RID: 5464
		private readonly ArrayList onDeserializedList;

		// Token: 0x04001559 RID: 5465
		private static Hashtable cache = new Hashtable();

		// Token: 0x0400155A RID: 5466
		private static object cache_lock = new object();

		// Token: 0x020006E3 RID: 1763
		// (Invoke) Token: 0x06004380 RID: 17280
		public delegate void CallbackHandler(StreamingContext context);
	}
}
