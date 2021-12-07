using System;
using System.IO;
using UnityEngine;

// Token: 0x020004BB RID: 1211
public class FixedUpdateSyncTracker : MonoBehaviour
{
	// Token: 0x060020D2 RID: 8402 RVA: 0x0008FB80 File Offset: 0x0008DD80
	public void FixedUpdate()
	{
		this.FixedUpdateCount++;
	}

	// Token: 0x060020D3 RID: 8403 RVA: 0x0008FB90 File Offset: 0x0008DD90
	public void Update()
	{
		if (!FixedUpdateSyncTracker.Enable)
		{
			return;
		}
		if (this.m_block == null)
		{
			this.m_block = new Texture2D(128, 128, TextureFormat.ARGB32, false, false);
		}
		if (Time.fixedDeltaTime > 0.016f)
		{
			int num = Mathf.Clamp(this.FixedUpdateCount, 0, 2);
			Color[] array = new Color[]
			{
				Color.blue,
				Color.white,
				Color.red
			};
			this.m_block.SetPixel(this.m_x, this.m_y, array[num]);
		}
		else
		{
			int num2 = Mathf.Clamp(this.FixedUpdateCount, 0, 4);
			Color[] array2 = new Color[]
			{
				Color.red,
				Color.magenta,
				Color.white,
				Color.green,
				Color.blue
			};
			this.m_block.SetPixel(this.m_x, this.m_y, array2[num2]);
		}
		this.FixedUpdateCount = 0;
		this.m_x++;
		if (this.m_x > this.m_block.width)
		{
			this.m_x = 0;
			this.m_y++;
		}
		if (this.m_y >= this.m_block.height)
		{
			this.Flush();
		}
	}

	// Token: 0x060020D4 RID: 8404 RVA: 0x0008FD3C File Offset: 0x0008DF3C
	public void Flush()
	{
		if (this.m_block)
		{
			byte[] array = this.m_block.EncodeToPNG();
			FileStream fileStream = File.Create("FixedUpdateSync_" + this.m_index + ".png");
			fileStream.Write(array, 0, array.Length);
			fileStream.Dispose();
			this.m_y = 0;
			this.m_index++;
			UnityEngine.Object.DestroyObject(this.m_block);
		}
	}

	// Token: 0x060020D5 RID: 8405 RVA: 0x0008FDB6 File Offset: 0x0008DFB6
	public void OnDestroy()
	{
		this.Flush();
	}

	// Token: 0x04001BC9 RID: 7113
	public int FixedUpdateCount;

	// Token: 0x04001BCA RID: 7114
	private Texture2D m_block;

	// Token: 0x04001BCB RID: 7115
	private int m_x;

	// Token: 0x04001BCC RID: 7116
	private int m_y;

	// Token: 0x04001BCD RID: 7117
	private int m_index;

	// Token: 0x04001BCE RID: 7118
	public static bool Enable;
}
