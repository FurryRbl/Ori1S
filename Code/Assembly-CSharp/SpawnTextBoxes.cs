using System;
using UnityEngine;

// Token: 0x02000271 RID: 625
public class SpawnTextBoxes : PerformingAction
{
	// Token: 0x060014E5 RID: 5349 RVA: 0x0005DD78 File Offset: 0x0005BF78
	public override void Perform(IContext context)
	{
		this.m_messageInstance = UnityEngine.Object.Instantiate<GameObject>(this.MessagePrefab);
		this.m_textBoxMessage = this.m_messageInstance.GetComponent<TextBoxMessage>();
		this.m_textBoxMessage.Initialize();
		foreach (string text in this.Strings)
		{
			this.m_textBoxMessage.AddLine(text);
		}
		this.m_textBoxMessage.ReadNextLine();
		this.m_textBoxMessage.StartWriting();
	}

	// Token: 0x060014E6 RID: 5350 RVA: 0x0005DDF3 File Offset: 0x0005BFF3
	public override void Stop()
	{
	}

	// Token: 0x170003B5 RID: 949
	// (get) Token: 0x060014E7 RID: 5351 RVA: 0x0005DDF5 File Offset: 0x0005BFF5
	public override bool IsPerforming
	{
		get
		{
			return !this.m_textBoxMessage.IsInactive;
		}
	}

	// Token: 0x04001220 RID: 4640
	public GameObject MessagePrefab;

	// Token: 0x04001221 RID: 4641
	private GameObject m_messageInstance;

	// Token: 0x04001222 RID: 4642
	public Vector3 MessageBoxOffset;

	// Token: 0x04001223 RID: 4643
	public Vector3 Position;

	// Token: 0x04001224 RID: 4644
	public string[] Strings;

	// Token: 0x04001225 RID: 4645
	private TextBoxMessage m_textBoxMessage;
}
