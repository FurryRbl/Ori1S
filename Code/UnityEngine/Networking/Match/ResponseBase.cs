using System;
using System.Collections.Generic;

namespace UnityEngine.Networking.Match
{
	// Token: 0x0200022E RID: 558
	public abstract class ResponseBase
	{
		// Token: 0x06002244 RID: 8772
		public abstract void Parse(object obj);

		// Token: 0x06002245 RID: 8773 RVA: 0x0002AD9C File Offset: 0x00028F9C
		internal string ParseJSONString(string name, object obj, IDictionary<string, object> dictJsonObj)
		{
			if (dictJsonObj.TryGetValue(name, out obj))
			{
				return obj as string;
			}
			throw new FormatException(name + " not found in JSON dictionary");
		}

		// Token: 0x06002246 RID: 8774 RVA: 0x0002ADC4 File Offset: 0x00028FC4
		internal short ParseJSONInt16(string name, object obj, IDictionary<string, object> dictJsonObj)
		{
			if (dictJsonObj.TryGetValue(name, out obj))
			{
				return Convert.ToInt16(obj);
			}
			throw new FormatException(name + " not found in JSON dictionary");
		}

		// Token: 0x06002247 RID: 8775 RVA: 0x0002ADEC File Offset: 0x00028FEC
		internal int ParseJSONInt32(string name, object obj, IDictionary<string, object> dictJsonObj)
		{
			if (dictJsonObj.TryGetValue(name, out obj))
			{
				return Convert.ToInt32(obj);
			}
			throw new FormatException(name + " not found in JSON dictionary");
		}

		// Token: 0x06002248 RID: 8776 RVA: 0x0002AE14 File Offset: 0x00029014
		internal long ParseJSONInt64(string name, object obj, IDictionary<string, object> dictJsonObj)
		{
			if (dictJsonObj.TryGetValue(name, out obj))
			{
				return Convert.ToInt64(obj);
			}
			throw new FormatException(name + " not found in JSON dictionary");
		}

		// Token: 0x06002249 RID: 8777 RVA: 0x0002AE3C File Offset: 0x0002903C
		internal ushort ParseJSONUInt16(string name, object obj, IDictionary<string, object> dictJsonObj)
		{
			if (dictJsonObj.TryGetValue(name, out obj))
			{
				return Convert.ToUInt16(obj);
			}
			throw new FormatException(name + " not found in JSON dictionary");
		}

		// Token: 0x0600224A RID: 8778 RVA: 0x0002AE64 File Offset: 0x00029064
		internal uint ParseJSONUInt32(string name, object obj, IDictionary<string, object> dictJsonObj)
		{
			if (dictJsonObj.TryGetValue(name, out obj))
			{
				return Convert.ToUInt32(obj);
			}
			throw new FormatException(name + " not found in JSON dictionary");
		}

		// Token: 0x0600224B RID: 8779 RVA: 0x0002AE8C File Offset: 0x0002908C
		internal ulong ParseJSONUInt64(string name, object obj, IDictionary<string, object> dictJsonObj)
		{
			if (dictJsonObj.TryGetValue(name, out obj))
			{
				return Convert.ToUInt64(obj);
			}
			throw new FormatException(name + " not found in JSON dictionary");
		}

		// Token: 0x0600224C RID: 8780 RVA: 0x0002AEB4 File Offset: 0x000290B4
		internal bool ParseJSONBool(string name, object obj, IDictionary<string, object> dictJsonObj)
		{
			if (dictJsonObj.TryGetValue(name, out obj))
			{
				return Convert.ToBoolean(obj);
			}
			throw new FormatException(name + " not found in JSON dictionary");
		}

		// Token: 0x0600224D RID: 8781 RVA: 0x0002AEDC File Offset: 0x000290DC
		internal DateTime ParseJSONDateTime(string name, object obj, IDictionary<string, object> dictJsonObj)
		{
			throw new FormatException(name + " DateTime not yet supported");
		}

		// Token: 0x0600224E RID: 8782 RVA: 0x0002AEF0 File Offset: 0x000290F0
		internal List<string> ParseJSONListOfStrings(string name, object obj, IDictionary<string, object> dictJsonObj)
		{
			if (dictJsonObj.TryGetValue(name, out obj))
			{
				List<object> list = obj as List<object>;
				if (list != null)
				{
					List<string> list2 = new List<string>();
					foreach (object obj2 in list)
					{
						IDictionary<string, object> dictionary = (IDictionary<string, object>)obj2;
						foreach (KeyValuePair<string, object> keyValuePair in dictionary)
						{
							string item = (string)keyValuePair.Value;
							list2.Add(item);
						}
					}
					return list2;
				}
			}
			throw new FormatException(name + " not found in JSON dictionary");
		}

		// Token: 0x0600224F RID: 8783 RVA: 0x0002AFE4 File Offset: 0x000291E4
		internal List<T> ParseJSONList<T>(string name, object obj, IDictionary<string, object> dictJsonObj) where T : ResponseBase, new()
		{
			if (dictJsonObj.TryGetValue(name, out obj))
			{
				List<object> list = obj as List<object>;
				if (list != null)
				{
					List<T> list2 = new List<T>();
					foreach (object obj2 in list)
					{
						IDictionary<string, object> obj3 = (IDictionary<string, object>)obj2;
						T item = Activator.CreateInstance<T>();
						item.Parse(obj3);
						list2.Add(item);
					}
					return list2;
				}
			}
			throw new FormatException(name + " not found in JSON dictionary");
		}
	}
}
