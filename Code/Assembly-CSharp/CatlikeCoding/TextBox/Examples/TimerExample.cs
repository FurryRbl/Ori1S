using System;
using System.Text;
using CatlikeCoding.Utilities;
using UnityEngine;

namespace CatlikeCoding.TextBox.Examples
{
	// Token: 0x02000762 RID: 1890
	public sealed class TimerExample : MonoBehaviour
	{
		// Token: 0x06002C22 RID: 11298 RVA: 0x000BD268 File Offset: 0x000BB468
		private void Start()
		{
			this.timeText = new StringBuilder(this.timerBox.DefaultText);
			this.textPrefixLength = this.timeText.Length;
		}

		// Token: 0x06002C23 RID: 11299 RVA: 0x000BD294 File Offset: 0x000BB494
		private void Update()
		{
			this.timeText.Length = this.textPrefixLength;
			StringBuilderUtility.AppendFloatGrouped(this.timeText, Time.time * 1000f, 2);
			this.timerBox.SetText(this.timeText);
			this.timerBox.RenderText();
		}

		// Token: 0x040027E7 RID: 10215
		public TextBox timerBox;

		// Token: 0x040027E8 RID: 10216
		private StringBuilder timeText;

		// Token: 0x040027E9 RID: 10217
		private int textPrefixLength;
	}
}
