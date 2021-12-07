using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007AE RID: 1966
[Serializable]
public class UberShaderDictionary<TK, TV> : IUberDictionary
{
	// Token: 0x06002D79 RID: 11641 RVA: 0x000C22D6 File Offset: 0x000C04D6
	public UberShaderDictionary()
	{
		this.m_dictionary = new Dictionary<TK, TV>();
	}

	// Token: 0x17000746 RID: 1862
	// (get) Token: 0x06002D7A RID: 11642 RVA: 0x000C22FF File Offset: 0x000C04FF
	public Dictionary<TK, TV>.ValueCollection Values
	{
		get
		{
			this.Init();
			return this.m_dictionary.Values;
		}
	}

	// Token: 0x17000747 RID: 1863
	// (get) Token: 0x06002D7B RID: 11643 RVA: 0x000C2312 File Offset: 0x000C0512
	public Dictionary<TK, TV>.KeyCollection Keys
	{
		get
		{
			this.Init();
			return this.m_dictionary.Keys;
		}
	}

	// Token: 0x17000748 RID: 1864
	// (get) Token: 0x06002D7C RID: 11644 RVA: 0x000C2325 File Offset: 0x000C0525
	public int Count
	{
		get
		{
			this.Init();
			return this.m_dictionary.Count;
		}
	}

	// Token: 0x06002D7D RID: 11645 RVA: 0x000C2338 File Offset: 0x000C0538
	public void Clear()
	{
		this.m_dictionary.Clear();
		this.m_keys.Clear();
		this.m_values.Clear();
	}

	// Token: 0x17000749 RID: 1865
	public TV this[TK index]
	{
		get
		{
			this.Init();
			return this.m_dictionary[index];
		}
		set
		{
			this.Init();
			this.m_dictionary[index] = value;
		}
	}

	// Token: 0x06002D80 RID: 11648 RVA: 0x000C2384 File Offset: 0x000C0584
	public void Init()
	{
		if (this.m_inited)
		{
			return;
		}
		this.m_dictionary = new Dictionary<TK, TV>();
		if (this.m_keys.Count != this.m_values.Count)
		{
			Debug.LogError("Dictionary out of sync! One of your types could not be serialized?");
			this.m_inited = true;
			this.m_keys.Clear();
			this.m_values.Clear();
			return;
		}
		for (int i = 0; i < this.m_keys.Count; i++)
		{
			if (this.m_keys[i] != null)
			{
				this.m_dictionary[this.m_keys[i]] = this.m_values[i];
			}
		}
		this.m_inited = true;
	}

	// Token: 0x06002D81 RID: 11649 RVA: 0x000C244C File Offset: 0x000C064C
	public bool ContainsKey(TK key)
	{
		this.Init();
		return this.m_dictionary.ContainsKey(key);
	}

	// Token: 0x06002D82 RID: 11650 RVA: 0x000C2460 File Offset: 0x000C0660
	public bool TryGetValue(TK key, out TV value)
	{
		this.Init();
		return this.m_dictionary.TryGetValue(key, out value);
	}

	// Token: 0x06002D83 RID: 11651 RVA: 0x000C2478 File Offset: 0x000C0678
	public void Save()
	{
		this.Init();
		this.m_keys.Clear();
		this.m_values.Clear();
		foreach (KeyValuePair<TK, TV> keyValuePair in this.m_dictionary)
		{
			this.m_keys.Add(keyValuePair.Key);
			this.m_values.Add(keyValuePair.Value);
		}
	}

	// Token: 0x06002D84 RID: 11652 RVA: 0x000C250C File Offset: 0x000C070C
	public void Add(TK key, TV value)
	{
		this.Init();
		if (!this.m_dictionary.ContainsKey(key))
		{
			this.m_dictionary.Add(key, value);
			this.m_keys.Add(key);
			this.m_values.Add(value);
		}
		else
		{
			Debug.LogError("Already contains key! " + key);
		}
	}

	// Token: 0x06002D85 RID: 11653 RVA: 0x000C2570 File Offset: 0x000C0770
	public void Remove(TK key)
	{
		this.Init();
		this.m_values.Remove(this.m_dictionary[key]);
		this.m_dictionary.Remove(key);
		this.m_keys.Remove(key);
	}

	// Token: 0x06002D86 RID: 11654 RVA: 0x000C25B8 File Offset: 0x000C07B8
	public void SetOrReplace(TK key, TV value)
	{
		this.Init();
		if (this.m_dictionary.ContainsKey(key))
		{
			this.m_dictionary[key] = value;
		}
		else
		{
			this.m_dictionary.Add(key, value);
		}
	}

	// Token: 0x04002904 RID: 10500
	protected Dictionary<TK, TV> m_dictionary;

	// Token: 0x04002905 RID: 10501
	[NonSerialized]
	private bool m_inited;

	// Token: 0x04002906 RID: 10502
	[SerializeField]
	protected List<TK> m_keys = new List<TK>();

	// Token: 0x04002907 RID: 10503
	[SerializeField]
	protected List<TV> m_values = new List<TV>();
}
