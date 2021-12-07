using System;

namespace UnityEngine.Assertions
{
	// Token: 0x02000323 RID: 803
	public class AssertionException : Exception
	{
		// Token: 0x060027CA RID: 10186 RVA: 0x00038EAC File Offset: 0x000370AC
		public AssertionException(string message, string userMessage) : base(message)
		{
			this.m_UserMessage = userMessage;
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x060027CB RID: 10187 RVA: 0x00038EBC File Offset: 0x000370BC
		public override string Message
		{
			get
			{
				string text = base.Message;
				if (this.m_UserMessage != null)
				{
					text = text + '\n' + this.m_UserMessage;
				}
				return text;
			}
		}

		// Token: 0x04000C46 RID: 3142
		private string m_UserMessage;
	}
}
