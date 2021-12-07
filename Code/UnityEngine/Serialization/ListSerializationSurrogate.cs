﻿using System;
using System.Collections;
using System.Runtime.Serialization;

namespace UnityEngine.Serialization
{
	// Token: 0x02000332 RID: 818
	internal class ListSerializationSurrogate : ISerializationSurrogate
	{
		// Token: 0x06002837 RID: 10295 RVA: 0x000397B8 File Offset: 0x000379B8
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			IList list = (IList)obj;
			info.AddValue("_size", list.Count);
			info.AddValue("_items", ListSerializationSurrogate.ArrayFromGenericList(list));
			info.AddValue("_version", 0);
		}

		// Token: 0x06002838 RID: 10296 RVA: 0x000397FC File Offset: 0x000379FC
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			IList list = (IList)Activator.CreateInstance(obj.GetType());
			int @int = info.GetInt32("_size");
			if (@int == 0)
			{
				return list;
			}
			IEnumerator enumerator = ((IEnumerable)info.GetValue("_items", typeof(IEnumerable))).GetEnumerator();
			for (int i = 0; i < @int; i++)
			{
				if (!enumerator.MoveNext())
				{
					throw new InvalidOperationException();
				}
				list.Add(enumerator.Current);
			}
			return list;
		}

		// Token: 0x06002839 RID: 10297 RVA: 0x00039880 File Offset: 0x00037A80
		private static Array ArrayFromGenericList(IList list)
		{
			Array array = Array.CreateInstance(list.GetType().GetGenericArguments()[0], list.Count);
			list.CopyTo(array, 0);
			return array;
		}

		// Token: 0x04000C58 RID: 3160
		public static readonly ISerializationSurrogate Default = new ListSerializationSurrogate();
	}
}
