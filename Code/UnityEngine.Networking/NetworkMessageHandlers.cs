using System;
using System.Collections.Generic;

namespace UnityEngine.Networking
{
	// Token: 0x02000048 RID: 72
	internal class NetworkMessageHandlers
	{
		// Token: 0x06000307 RID: 775 RVA: 0x0000F8F8 File Offset: 0x0000DAF8
		internal void RegisterHandlerSafe(short msgType, NetworkMessageDelegate handler)
		{
			if (handler == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("RegisterHandlerSafe id:" + msgType + " handler is null");
				}
				return;
			}
			if (LogFilter.logDebug)
			{
				Debug.Log(string.Concat(new object[]
				{
					"RegisterHandlerSafe id:",
					msgType,
					" handler:",
					handler.Method.Name
				}));
			}
			if (this.m_MsgHandlers.ContainsKey(msgType))
			{
				return;
			}
			this.m_MsgHandlers.Add(msgType, handler);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000F990 File Offset: 0x0000DB90
		public void RegisterHandler(short msgType, NetworkMessageDelegate handler)
		{
			if (handler == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("RegisterHandler id:" + msgType + " handler is null");
				}
				return;
			}
			if (msgType <= 31)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("RegisterHandler: Cannot replace system message handler " + msgType);
				}
				return;
			}
			if (this.m_MsgHandlers.ContainsKey(msgType))
			{
				if (LogFilter.logDebug)
				{
					Debug.Log("RegisterHandler replacing " + msgType);
				}
				this.m_MsgHandlers.Remove(msgType);
			}
			if (LogFilter.logDebug)
			{
				Debug.Log(string.Concat(new object[]
				{
					"RegisterHandler id:",
					msgType,
					" handler:",
					handler.Method.Name
				}));
			}
			this.m_MsgHandlers.Add(msgType, handler);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000FA7C File Offset: 0x0000DC7C
		public void UnregisterHandler(short msgType)
		{
			this.m_MsgHandlers.Remove(msgType);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000FA8C File Offset: 0x0000DC8C
		internal NetworkMessageDelegate GetHandler(short msgType)
		{
			if (this.m_MsgHandlers.ContainsKey(msgType))
			{
				return this.m_MsgHandlers[msgType];
			}
			return null;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000FAB0 File Offset: 0x0000DCB0
		internal Dictionary<short, NetworkMessageDelegate> GetHandlers()
		{
			return this.m_MsgHandlers;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000FAB8 File Offset: 0x0000DCB8
		internal void ClearMessageHandlers()
		{
			this.m_MsgHandlers.Clear();
		}

		// Token: 0x04000167 RID: 359
		private Dictionary<short, NetworkMessageDelegate> m_MsgHandlers = new Dictionary<short, NetworkMessageDelegate>();
	}
}
