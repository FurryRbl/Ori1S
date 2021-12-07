using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UnityEngine.Serialization
{
	// Token: 0x02000331 RID: 817
	public class UnitySurrogateSelector : ISurrogateSelector
	{
		// Token: 0x06002832 RID: 10290 RVA: 0x00039720 File Offset: 0x00037920
		public ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector selector)
		{
			if (type.IsGenericType)
			{
				Type genericTypeDefinition = type.GetGenericTypeDefinition();
				if (genericTypeDefinition == typeof(List<>))
				{
					selector = this;
					return ListSerializationSurrogate.Default;
				}
				if (genericTypeDefinition == typeof(Dictionary<, >))
				{
					selector = this;
					Type type2 = typeof(DictionarySerializationSurrogate<, >).MakeGenericType(type.GetGenericArguments());
					return (ISerializationSurrogate)Activator.CreateInstance(type2);
				}
			}
			selector = null;
			return null;
		}

		// Token: 0x06002833 RID: 10291 RVA: 0x00039794 File Offset: 0x00037994
		public void ChainSelector(ISurrogateSelector selector)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002834 RID: 10292 RVA: 0x0003979C File Offset: 0x0003799C
		public ISurrogateSelector GetNextSelector()
		{
			throw new NotImplementedException();
		}
	}
}
