using System;
using UnityEngine;

namespace Game
{
	// Token: 0x0200001B RID: 27
	public static class UI
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000184 RID: 388 RVA: 0x0000732B File Offset: 0x0000552B
		public static MessageControllerB MessageController
		{
			get
			{
				UI.LoadMessageController();
				return UI.m_messageController;
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00007337 File Offset: 0x00005537
		public static void LoadMessageController()
		{
			if (UI.m_messageController == null)
			{
				UI.m_messageController = (Resources.Load("MessageControllerB") as GameObject).GetComponent<MessageControllerB>();
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00007362 File Offset: 0x00005562
		// (set) Token: 0x06000187 RID: 391 RVA: 0x00007369 File Offset: 0x00005569
		public static MenuScreenManager Menu
		{
			get
			{
				return UI.m_sMenu;
			}
			set
			{
				UI.m_sMenu = value;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00007371 File Offset: 0x00005571
		public static bool MainMenuVisible
		{
			get
			{
				return UI.m_sMenu != null && (UI.m_sMenu.MainMenuVisible || UI.m_sMenu.ResumeScreenVisible);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000189 RID: 393 RVA: 0x000073A2 File Offset: 0x000055A2
		public static bool MainMenuExists
		{
			get
			{
				return UI.m_sMenu != null;
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000073AF File Offset: 0x000055AF
		public static bool IsInventoryVisible()
		{
			return UI.MainMenuVisible && UI.m_sMenu.IsInventoryVisible();
		}

		// Token: 0x04000131 RID: 305
		private static MessageControllerB m_messageController;

		// Token: 0x04000132 RID: 306
		public static FaderB Fader;

		// Token: 0x04000133 RID: 307
		public static SeinUI SeinUI;

		// Token: 0x04000134 RID: 308
		private static MenuScreenManager m_sMenu;

		// Token: 0x04000135 RID: 309
		public static Vignette Vignette;

		// Token: 0x0200001C RID: 28
		public static class Cameras
		{
			// Token: 0x04000136 RID: 310
			public static CameraSystem System;

			// Token: 0x04000137 RID: 311
			public static GameplayCamera Current;

			// Token: 0x04000138 RID: 312
			public static CameraManager Manager;
		}

		// Token: 0x020000F5 RID: 245
		public static class Hints
		{
			// Token: 0x17000212 RID: 530
			// (get) Token: 0x060009CC RID: 2508 RVA: 0x0002AFC3 File Offset: 0x000291C3
			public static Vector3 HintPosition
			{
				get
				{
					return OnScreenPositions.TopCenter;
				}
			}

			// Token: 0x060009CD RID: 2509 RVA: 0x0002AFCC File Offset: 0x000291CC
			public static void HideExistingHint()
			{
				if (UI.Hints.m_currentHint)
				{
					UI.Hints.m_currentHint.Visibility.HideMessageScreenImmediately();
					UI.Hints.m_currentHint = null;
				}
			}

			// Token: 0x060009CE RID: 2510 RVA: 0x0002AFFD File Offset: 0x000291FD
			private static bool LayerShouldShow(HintLayer layer)
			{
				return !UI.Hints.m_currentHint || layer >= UI.Hints.m_currentLayer;
			}

			// Token: 0x060009CF RID: 2511 RVA: 0x0002B01C File Offset: 0x0002921C
			public static MessageBox Show(MessageProvider messageProvider, HintLayer layer, float duration = 3f)
			{
				if (messageProvider == null)
				{
					return null;
				}
				if (UI.MessageController.AnyAbilityPickupStoryMessagesVisible)
				{
					return null;
				}
				if (UI.Hints.LayerShouldShow(layer))
				{
					UI.Hints.HideExistingHint();
					UI.Hints.m_currentLayer = layer;
					if (ShorterHintZone.IsInside)
					{
						duration = 1f;
					}
					UI.Hints.m_currentHint = UI.MessageController.ShowHintMessage(messageProvider, UI.Hints.HintPosition, duration);
					return UI.Hints.m_currentHint;
				}
				return null;
			}

			// Token: 0x17000213 RID: 531
			// (get) Token: 0x060009D0 RID: 2512 RVA: 0x0002B08C File Offset: 0x0002928C
			public static bool IsShowingHint
			{
				get
				{
					return UI.Hints.m_currentHint;
				}
			}

			// Token: 0x04000815 RID: 2069
			private static MessageBox m_currentHint;

			// Token: 0x04000816 RID: 2070
			private static HintLayer m_currentLayer;

			// Token: 0x04000817 RID: 2071
			private static bool m_showHints;
		}
	}
}
