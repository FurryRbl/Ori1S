using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000739 RID: 1849
public class LeaderboardTableUI : MonoBehaviour
{
	// Token: 0x170006EC RID: 1772
	// (get) Token: 0x06002B6F RID: 11119 RVA: 0x000BA706 File Offset: 0x000B8906
	public LeaderboardTableUI.LeaderboardMetaData CurrentMetaData
	{
		get
		{
			return this.MetaData.FirstOrDefault((LeaderboardTableUI.LeaderboardMetaData x) => x.Leaderboard == this.m_leaderboardType);
		}
	}

	// Token: 0x06002B70 RID: 11120 RVA: 0x000BA720 File Offset: 0x000B8920
	public LeaderboardRowUI GetRowByIndex(int index)
	{
		if (index < 0 || index >= this.m_leaderboardRows.Count)
		{
			return null;
		}
		return this.m_leaderboardRows[index];
	}

	// Token: 0x06002B71 RID: 11121 RVA: 0x000BA753 File Offset: 0x000B8953
	public void Awake()
	{
		this.GenerateTable();
	}

	// Token: 0x06002B72 RID: 11122 RVA: 0x000BA75C File Offset: 0x000B895C
	public void GenerateTable()
	{
		if (this.m_tableExists)
		{
			this.DestroyTable();
		}
		LeaderboardTableUI.LeaderboardMetaData currentMetaData = this.CurrentMetaData;
		this.m_header = UnityEngine.Object.Instantiate<GameObject>(currentMetaData.Header);
		this.m_header.transform.parent = this.HeaderParent;
		this.m_header.transform.localPosition = Vector3.zero;
		TransparencyAnimator.Register(this.m_header.transform);
		this.m_tableExists = true;
		for (int i = 0; i < this.RowCount; i++)
		{
			LeaderboardRowUI leaderboardRowUI = UnityEngine.Object.Instantiate<LeaderboardRowUI>(currentMetaData.Row);
			leaderboardRowUI.transform.parent = this.RowsParent;
			leaderboardRowUI.transform.localPosition = Vector3.down * (float)i * this.RowSpacing;
			this.m_leaderboardRows.Add(leaderboardRowUI);
			TransparencyAnimator.Register(leaderboardRowUI.transform);
		}
		for (int j = 1; j < this.RowCount; j += 2)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.LeaderboardRowBackground);
			gameObject.transform.parent = this.RowsParent;
			gameObject.transform.localPosition = Vector3.down * (float)j * this.RowSpacing;
			this.m_rowBackgrounds.Add(gameObject);
			TransparencyAnimator.Register(gameObject.transform);
		}
	}

	// Token: 0x06002B73 RID: 11123 RVA: 0x000BA8B4 File Offset: 0x000B8AB4
	public void DestroyTable()
	{
		foreach (LeaderboardRowUI leaderboardRowUI in this.m_leaderboardRows)
		{
			UnityEngine.Object.DestroyObject(leaderboardRowUI.gameObject);
		}
		foreach (GameObject obj in this.m_rowBackgrounds)
		{
			UnityEngine.Object.DestroyObject(obj);
		}
		UnityEngine.Object.DestroyObject(this.m_header);
		this.m_header = null;
		this.m_leaderboardRows.Clear();
		this.m_rowBackgrounds.Clear();
		this.m_tableExists = false;
	}

	// Token: 0x06002B74 RID: 11124 RVA: 0x000BA98C File Offset: 0x000B8B8C
	public void UpdateTable(LeaderboardData data)
	{
		if (data.Type != this.m_leaderboardType || !this.m_tableExists)
		{
			this.m_leaderboardType = data.Type;
			this.GenerateTable();
		}
		int num = Mathf.Min(data.Count, this.RowCount);
		int i;
		for (i = 0; i < num; i++)
		{
			LeaderboardData.Entry content = data[i];
			this.m_leaderboardRows[i].SetContent(content);
			this.m_leaderboardRows[i].Show();
		}
		while (i < this.RowCount)
		{
			this.m_leaderboardRows[i].Hide();
			i++;
		}
	}

	// Token: 0x06002B75 RID: 11125 RVA: 0x000BAA3C File Offset: 0x000B8C3C
	public void ClearTable()
	{
		this.DestroyTable();
	}

	// Token: 0x0400273C RID: 10044
	public GameObject LeaderboardRowBackground;

	// Token: 0x0400273D RID: 10045
	public float RowSpacing = 0.45f;

	// Token: 0x0400273E RID: 10046
	public int RowCount = 10;

	// Token: 0x0400273F RID: 10047
	public Transform RowsParent;

	// Token: 0x04002740 RID: 10048
	public Transform HeaderParent;

	// Token: 0x04002741 RID: 10049
	private readonly List<LeaderboardRowUI> m_leaderboardRows = new List<LeaderboardRowUI>();

	// Token: 0x04002742 RID: 10050
	private List<GameObject> m_rowBackgrounds = new List<GameObject>();

	// Token: 0x04002743 RID: 10051
	private GameObject m_header;

	// Token: 0x04002744 RID: 10052
	public LeaderboardTableUI.LeaderboardMetaData[] MetaData;

	// Token: 0x04002745 RID: 10053
	private Leaderboard m_leaderboardType;

	// Token: 0x04002746 RID: 10054
	private bool m_tableExists;

	// Token: 0x0200073A RID: 1850
	[Serializable]
	public class LeaderboardMetaData
	{
		// Token: 0x04002747 RID: 10055
		public GameObject Header;

		// Token: 0x04002748 RID: 10056
		public LeaderboardRowUI Row;

		// Token: 0x04002749 RID: 10057
		public Leaderboard Leaderboard;
	}
}
