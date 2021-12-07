using System;

namespace System
{
	/// <summary>Specifies the standard keys on a console.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000114 RID: 276
	[Serializable]
	public enum ConsoleKey
	{
		/// <summary>The BACKSPACE key.</summary>
		// Token: 0x040003E3 RID: 995
		Backspace = 8,
		/// <summary>The TAB key.</summary>
		// Token: 0x040003E4 RID: 996
		Tab,
		/// <summary>The CLEAR key.</summary>
		// Token: 0x040003E5 RID: 997
		Clear = 12,
		/// <summary>The ENTER key.</summary>
		// Token: 0x040003E6 RID: 998
		Enter,
		/// <summary>The PAUSE key.</summary>
		// Token: 0x040003E7 RID: 999
		Pause = 19,
		/// <summary>The ESC (ESCAPE) key.</summary>
		// Token: 0x040003E8 RID: 1000
		Escape = 27,
		/// <summary>The SPACEBAR key.</summary>
		// Token: 0x040003E9 RID: 1001
		Spacebar = 32,
		/// <summary>The PAGE UP key.</summary>
		// Token: 0x040003EA RID: 1002
		PageUp,
		/// <summary>The PAGE DOWN key.</summary>
		// Token: 0x040003EB RID: 1003
		PageDown,
		/// <summary>The END key.</summary>
		// Token: 0x040003EC RID: 1004
		End,
		/// <summary>The HOME key.</summary>
		// Token: 0x040003ED RID: 1005
		Home,
		/// <summary>The LEFT ARROW key.</summary>
		// Token: 0x040003EE RID: 1006
		LeftArrow,
		/// <summary>The UP ARROW key.</summary>
		// Token: 0x040003EF RID: 1007
		UpArrow,
		/// <summary>The RIGHT ARROW key.</summary>
		// Token: 0x040003F0 RID: 1008
		RightArrow,
		/// <summary>The DOWN ARROW key.</summary>
		// Token: 0x040003F1 RID: 1009
		DownArrow,
		/// <summary>The SELECT key.</summary>
		// Token: 0x040003F2 RID: 1010
		Select,
		/// <summary>The PRINT key.</summary>
		// Token: 0x040003F3 RID: 1011
		Print,
		/// <summary>The EXECUTE key.</summary>
		// Token: 0x040003F4 RID: 1012
		Execute,
		/// <summary>The PRINT SCREEN key.</summary>
		// Token: 0x040003F5 RID: 1013
		PrintScreen,
		/// <summary>The INS (INSERT) key.</summary>
		// Token: 0x040003F6 RID: 1014
		Insert,
		/// <summary>The DEL (DELETE) key.</summary>
		// Token: 0x040003F7 RID: 1015
		Delete,
		/// <summary>The HELP key.</summary>
		// Token: 0x040003F8 RID: 1016
		Help,
		/// <summary>The 0 key.</summary>
		// Token: 0x040003F9 RID: 1017
		D0,
		/// <summary>The 1 key.</summary>
		// Token: 0x040003FA RID: 1018
		D1,
		/// <summary>The 2 key.</summary>
		// Token: 0x040003FB RID: 1019
		D2,
		/// <summary>The 3 key.</summary>
		// Token: 0x040003FC RID: 1020
		D3,
		/// <summary>The 4 key.</summary>
		// Token: 0x040003FD RID: 1021
		D4,
		/// <summary>The 5 key.</summary>
		// Token: 0x040003FE RID: 1022
		D5,
		/// <summary>The 6 key.</summary>
		// Token: 0x040003FF RID: 1023
		D6,
		/// <summary>The 7 key.</summary>
		// Token: 0x04000400 RID: 1024
		D7,
		/// <summary>The 8 key.</summary>
		// Token: 0x04000401 RID: 1025
		D8,
		/// <summary>The 9 key.</summary>
		// Token: 0x04000402 RID: 1026
		D9,
		/// <summary>The A key.</summary>
		// Token: 0x04000403 RID: 1027
		A = 65,
		/// <summary>The B key.</summary>
		// Token: 0x04000404 RID: 1028
		B,
		/// <summary>The C key.</summary>
		// Token: 0x04000405 RID: 1029
		C,
		/// <summary>The D key.</summary>
		// Token: 0x04000406 RID: 1030
		D,
		/// <summary>The E key.</summary>
		// Token: 0x04000407 RID: 1031
		E,
		/// <summary>The F key.</summary>
		// Token: 0x04000408 RID: 1032
		F,
		/// <summary>The G key.</summary>
		// Token: 0x04000409 RID: 1033
		G,
		/// <summary>The H key.</summary>
		// Token: 0x0400040A RID: 1034
		H,
		/// <summary>The I key.</summary>
		// Token: 0x0400040B RID: 1035
		I,
		/// <summary>The J key.</summary>
		// Token: 0x0400040C RID: 1036
		J,
		/// <summary>The K key.</summary>
		// Token: 0x0400040D RID: 1037
		K,
		/// <summary>The L key.</summary>
		// Token: 0x0400040E RID: 1038
		L,
		/// <summary>The M key.</summary>
		// Token: 0x0400040F RID: 1039
		M,
		/// <summary>The N key.</summary>
		// Token: 0x04000410 RID: 1040
		N,
		/// <summary>The O key.</summary>
		// Token: 0x04000411 RID: 1041
		O,
		/// <summary>The P key.</summary>
		// Token: 0x04000412 RID: 1042
		P,
		/// <summary>The Q key.</summary>
		// Token: 0x04000413 RID: 1043
		Q,
		/// <summary>The R key.</summary>
		// Token: 0x04000414 RID: 1044
		R,
		/// <summary>The S key.</summary>
		// Token: 0x04000415 RID: 1045
		S,
		/// <summary>The T key.</summary>
		// Token: 0x04000416 RID: 1046
		T,
		/// <summary>The U key.</summary>
		// Token: 0x04000417 RID: 1047
		U,
		/// <summary>The V key.</summary>
		// Token: 0x04000418 RID: 1048
		V,
		/// <summary>The W key.</summary>
		// Token: 0x04000419 RID: 1049
		W,
		/// <summary>The X key.</summary>
		// Token: 0x0400041A RID: 1050
		X,
		/// <summary>The Y key.</summary>
		// Token: 0x0400041B RID: 1051
		Y,
		/// <summary>The Z key.</summary>
		// Token: 0x0400041C RID: 1052
		Z,
		/// <summary>The left Windows logo key (Microsoft Natural Keyboard).</summary>
		// Token: 0x0400041D RID: 1053
		LeftWindows,
		/// <summary>The right Windows logo key (Microsoft Natural Keyboard).</summary>
		// Token: 0x0400041E RID: 1054
		RightWindows,
		/// <summary>The Application key (Microsoft Natural Keyboard).</summary>
		// Token: 0x0400041F RID: 1055
		Applications,
		/// <summary>The Computer Sleep key.</summary>
		// Token: 0x04000420 RID: 1056
		Sleep = 95,
		/// <summary>The 0 key on the numeric keypad.</summary>
		// Token: 0x04000421 RID: 1057
		NumPad0,
		/// <summary>The 1 key on the numeric keypad.</summary>
		// Token: 0x04000422 RID: 1058
		NumPad1,
		/// <summary>The 2 key on the numeric keypad.</summary>
		// Token: 0x04000423 RID: 1059
		NumPad2,
		/// <summary>The 3 key on the numeric keypad.</summary>
		// Token: 0x04000424 RID: 1060
		NumPad3,
		/// <summary>The 4 key on the numeric keypad.</summary>
		// Token: 0x04000425 RID: 1061
		NumPad4,
		/// <summary>The 5 key on the numeric keypad.</summary>
		// Token: 0x04000426 RID: 1062
		NumPad5,
		/// <summary>The 6 key on the numeric keypad.</summary>
		// Token: 0x04000427 RID: 1063
		NumPad6,
		/// <summary>The 7 key on the numeric keypad.</summary>
		// Token: 0x04000428 RID: 1064
		NumPad7,
		/// <summary>The 8 key on the numeric keypad.</summary>
		// Token: 0x04000429 RID: 1065
		NumPad8,
		/// <summary>The 9 key on the numeric keypad.</summary>
		// Token: 0x0400042A RID: 1066
		NumPad9,
		/// <summary>The Multiply key.</summary>
		// Token: 0x0400042B RID: 1067
		Multiply,
		/// <summary>The Add key.</summary>
		// Token: 0x0400042C RID: 1068
		Add,
		/// <summary>The Separator key.</summary>
		// Token: 0x0400042D RID: 1069
		Separator,
		/// <summary>The Subtract key.</summary>
		// Token: 0x0400042E RID: 1070
		Subtract,
		/// <summary>The Decimal key.</summary>
		// Token: 0x0400042F RID: 1071
		Decimal,
		/// <summary>The Divide key.</summary>
		// Token: 0x04000430 RID: 1072
		Divide,
		/// <summary>The F1 key.</summary>
		// Token: 0x04000431 RID: 1073
		F1,
		/// <summary>The F2 key.</summary>
		// Token: 0x04000432 RID: 1074
		F2,
		/// <summary>The F3 key.</summary>
		// Token: 0x04000433 RID: 1075
		F3,
		/// <summary>The F4 key.</summary>
		// Token: 0x04000434 RID: 1076
		F4,
		/// <summary>The F5 key.</summary>
		// Token: 0x04000435 RID: 1077
		F5,
		/// <summary>The F6 key.</summary>
		// Token: 0x04000436 RID: 1078
		F6,
		/// <summary>The F7 key.</summary>
		// Token: 0x04000437 RID: 1079
		F7,
		/// <summary>The F8 key.</summary>
		// Token: 0x04000438 RID: 1080
		F8,
		/// <summary>The F9 key.</summary>
		// Token: 0x04000439 RID: 1081
		F9,
		/// <summary>The F10 key.</summary>
		// Token: 0x0400043A RID: 1082
		F10,
		/// <summary>The F11 key.</summary>
		// Token: 0x0400043B RID: 1083
		F11,
		/// <summary>The F12 key.</summary>
		// Token: 0x0400043C RID: 1084
		F12,
		/// <summary>The F13 key.</summary>
		// Token: 0x0400043D RID: 1085
		F13,
		/// <summary>The F14 key.</summary>
		// Token: 0x0400043E RID: 1086
		F14,
		/// <summary>The F15 key.</summary>
		// Token: 0x0400043F RID: 1087
		F15,
		/// <summary>The F16 key.</summary>
		// Token: 0x04000440 RID: 1088
		F16,
		/// <summary>The F17 key.</summary>
		// Token: 0x04000441 RID: 1089
		F17,
		/// <summary>The F18 key.</summary>
		// Token: 0x04000442 RID: 1090
		F18,
		/// <summary>The F19 key.</summary>
		// Token: 0x04000443 RID: 1091
		F19,
		/// <summary>The F20 key.</summary>
		// Token: 0x04000444 RID: 1092
		F20,
		/// <summary>The F21 key.</summary>
		// Token: 0x04000445 RID: 1093
		F21,
		/// <summary>The F22 key.</summary>
		// Token: 0x04000446 RID: 1094
		F22,
		/// <summary>The F23 key.</summary>
		// Token: 0x04000447 RID: 1095
		F23,
		/// <summary>The F24 key.</summary>
		// Token: 0x04000448 RID: 1096
		F24,
		/// <summary>The Browser Back key (Windows 2000 or later).</summary>
		// Token: 0x04000449 RID: 1097
		BrowserBack = 166,
		/// <summary>The Browser Forward key (Windows 2000 or later).</summary>
		// Token: 0x0400044A RID: 1098
		BrowserForward,
		/// <summary>The Browser Refresh key (Windows 2000 or later).</summary>
		// Token: 0x0400044B RID: 1099
		BrowserRefresh,
		/// <summary>The Browser Stop key (Windows 2000 or later).</summary>
		// Token: 0x0400044C RID: 1100
		BrowserStop,
		/// <summary>The Browser Search key (Windows 2000 or later).</summary>
		// Token: 0x0400044D RID: 1101
		BrowserSearch,
		/// <summary>The Browser Favorites key (Windows 2000 or later).</summary>
		// Token: 0x0400044E RID: 1102
		BrowserFavorites,
		/// <summary>The Browser Home key (Windows 2000 or later).</summary>
		// Token: 0x0400044F RID: 1103
		BrowserHome,
		/// <summary>The Volume Mute key (Microsoft Natural Keyboard, Windows 2000 or later).</summary>
		// Token: 0x04000450 RID: 1104
		VolumeMute,
		/// <summary>The Volume Down key (Microsoft Natural Keyboard, Windows 2000 or later).</summary>
		// Token: 0x04000451 RID: 1105
		VolumeDown,
		/// <summary>The Volume Up key (Microsoft Natural Keyboard, Windows 2000 or later).</summary>
		// Token: 0x04000452 RID: 1106
		VolumeUp,
		/// <summary>The Media Next Track key (Windows 2000 or later).</summary>
		// Token: 0x04000453 RID: 1107
		MediaNext,
		/// <summary>The Media Previous Track key (Windows 2000 or later).</summary>
		// Token: 0x04000454 RID: 1108
		MediaPrevious,
		/// <summary>The Media Stop key (Windows 2000 or later).</summary>
		// Token: 0x04000455 RID: 1109
		MediaStop,
		/// <summary>The Media Play/Pause key (Windows 2000 or later).</summary>
		// Token: 0x04000456 RID: 1110
		MediaPlay,
		/// <summary>The Start Mail key (Microsoft Natural Keyboard, Windows 2000 or later).</summary>
		// Token: 0x04000457 RID: 1111
		LaunchMail,
		/// <summary>The Select Media key (Microsoft Natural Keyboard, Windows 2000 or later).</summary>
		// Token: 0x04000458 RID: 1112
		LaunchMediaSelect,
		/// <summary>The Start Application 1 key (Microsoft Natural Keyboard, Windows 2000 or later).</summary>
		// Token: 0x04000459 RID: 1113
		LaunchApp1,
		/// <summary>The Start Application 2 key (Microsoft Natural Keyboard, Windows 2000 or later).</summary>
		// Token: 0x0400045A RID: 1114
		LaunchApp2,
		/// <summary>The OEM 1 key (OEM specific).</summary>
		// Token: 0x0400045B RID: 1115
		Oem1 = 186,
		/// <summary>The OEM Plus key on any country/region keyboard (Windows 2000 or later).</summary>
		// Token: 0x0400045C RID: 1116
		OemPlus,
		/// <summary>The OEM Comma key on any country/region keyboard (Windows 2000 or later).</summary>
		// Token: 0x0400045D RID: 1117
		OemComma,
		/// <summary>The OEM Minus key on any country/region keyboard (Windows 2000 or later).</summary>
		// Token: 0x0400045E RID: 1118
		OemMinus,
		/// <summary>The OEM Period key on any country/region keyboard (Windows 2000 or later).</summary>
		// Token: 0x0400045F RID: 1119
		OemPeriod,
		/// <summary>The OEM 2 key (OEM specific).</summary>
		// Token: 0x04000460 RID: 1120
		Oem2,
		/// <summary>The OEM 3 key (OEM specific).</summary>
		// Token: 0x04000461 RID: 1121
		Oem3,
		/// <summary>The OEM 4 key (OEM specific).</summary>
		// Token: 0x04000462 RID: 1122
		Oem4 = 219,
		/// <summary>The OEM 5 (OEM specific).</summary>
		// Token: 0x04000463 RID: 1123
		Oem5,
		/// <summary>The OEM 6 key (OEM specific).</summary>
		// Token: 0x04000464 RID: 1124
		Oem6,
		/// <summary>The OEM 7 key (OEM specific).</summary>
		// Token: 0x04000465 RID: 1125
		Oem7,
		/// <summary>The OEM 8 key (OEM specific).</summary>
		// Token: 0x04000466 RID: 1126
		Oem8,
		/// <summary>The OEM 102 key (OEM specific).</summary>
		// Token: 0x04000467 RID: 1127
		Oem102 = 226,
		/// <summary>The IME PROCESS key.</summary>
		// Token: 0x04000468 RID: 1128
		Process = 229,
		/// <summary>The PACKET key (used to pass Unicode characters with keystrokes).</summary>
		// Token: 0x04000469 RID: 1129
		Packet = 231,
		/// <summary>The ATTN key.</summary>
		// Token: 0x0400046A RID: 1130
		Attention = 246,
		/// <summary>The CRSEL (CURSOR SELECT) key.</summary>
		// Token: 0x0400046B RID: 1131
		CrSel,
		/// <summary>The EXSEL (EXTEND SELECTION) key.</summary>
		// Token: 0x0400046C RID: 1132
		ExSel,
		/// <summary>The ERASE EOF key.</summary>
		// Token: 0x0400046D RID: 1133
		EraseEndOfFile,
		/// <summary>The PLAY key.</summary>
		// Token: 0x0400046E RID: 1134
		Play,
		/// <summary>The ZOOM key.</summary>
		// Token: 0x0400046F RID: 1135
		Zoom,
		/// <summary>A constant reserved for future use.</summary>
		// Token: 0x04000470 RID: 1136
		NoName,
		/// <summary>The PA1 key.</summary>
		// Token: 0x04000471 RID: 1137
		Pa1,
		/// <summary>The CLEAR key (OEM specific).</summary>
		// Token: 0x04000472 RID: 1138
		OemClear
	}
}
