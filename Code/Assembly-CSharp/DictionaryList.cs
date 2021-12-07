using System;
using System.Collections.Generic;

// Token: 0x020007C6 RID: 1990
[Serializable]
public class DictionaryList<Key, Value>
{
	// Token: 0x06002DCD RID: 11725 RVA: 0x000C3665 File Offset: 0x000C1865
	public void Clear()
	{
		this.Dictionary.Clear();
		this.List.Clear();
	}

	// Token: 0x06002DCE RID: 11726 RVA: 0x000C367D File Offset: 0x000C187D
	public void Add(Key key, Value value)
	{
		this.Dictionary.Add(key, value);
		this.List.Add(new DictionaryList<Key, Value>.KeyValuePair(key, value));
	}

	// Token: 0x17000757 RID: 1879
	// (get) Token: 0x06002DCF RID: 11727 RVA: 0x000C369E File Offset: 0x000C189E
	public int Count
	{
		get
		{
			return this.List.Count;
		}
	}

	// Token: 0x06002DD0 RID: 11728 RVA: 0x000C36AC File Offset: 0x000C18AC
	public void Remove(Key key)
	{
		this.Dictionary.Remove(key);
		this.List.RemoveAll((DictionaryList<Key, Value>.KeyValuePair a) => object.Equals(a.Key, key));
	}

	// Token: 0x04002923 RID: 10531
	public int Test;

	// Token: 0x04002924 RID: 10532
	public Dictionary<Key, Value> Dictionary;

	// Token: 0x04002925 RID: 10533
	public List<DictionaryList<Key, Value>.KeyValuePair> List;

	// Token: 0x020007C7 RID: 1991
	[Serializable]
	public class KeyValuePair
	{
		// Token: 0x06002DD1 RID: 11729 RVA: 0x000C36F7 File Offset: 0x000C18F7
		public KeyValuePair(Key key, Value value)
		{
			this.Key = key;
			this.Value = value;
		}

		// Token: 0x04002926 RID: 10534
		public Key Key;

		// Token: 0x04002927 RID: 10535
		public Value Value;
	}
}
