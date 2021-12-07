using System;

namespace Mono.Xml.XPath
{
	// Token: 0x02000006 RID: 6
	internal class Token
	{
		// Token: 0x04000014 RID: 20
		public const int ERROR = 257;

		// Token: 0x04000015 RID: 21
		public const int EOF = 258;

		// Token: 0x04000016 RID: 22
		public const int SLASH = 259;

		// Token: 0x04000017 RID: 23
		public const int SLASH2 = 260;

		// Token: 0x04000018 RID: 24
		public const int DOT = 262;

		// Token: 0x04000019 RID: 25
		public const int DOT2 = 263;

		// Token: 0x0400001A RID: 26
		public const int COLON2 = 265;

		// Token: 0x0400001B RID: 27
		public const int COMMA = 267;

		// Token: 0x0400001C RID: 28
		public const int AT = 268;

		// Token: 0x0400001D RID: 29
		public const int FUNCTION_NAME = 269;

		// Token: 0x0400001E RID: 30
		public const int BRACKET_OPEN = 270;

		// Token: 0x0400001F RID: 31
		public const int BRACKET_CLOSE = 271;

		// Token: 0x04000020 RID: 32
		public const int PAREN_OPEN = 272;

		// Token: 0x04000021 RID: 33
		public const int PAREN_CLOSE = 273;

		// Token: 0x04000022 RID: 34
		public const int AND = 274;

		// Token: 0x04000023 RID: 35
		public const int and = 275;

		// Token: 0x04000024 RID: 36
		public const int OR = 276;

		// Token: 0x04000025 RID: 37
		public const int or = 277;

		// Token: 0x04000026 RID: 38
		public const int DIV = 278;

		// Token: 0x04000027 RID: 39
		public const int div = 279;

		// Token: 0x04000028 RID: 40
		public const int MOD = 280;

		// Token: 0x04000029 RID: 41
		public const int mod = 281;

		// Token: 0x0400002A RID: 42
		public const int PLUS = 282;

		// Token: 0x0400002B RID: 43
		public const int MINUS = 283;

		// Token: 0x0400002C RID: 44
		public const int ASTERISK = 284;

		// Token: 0x0400002D RID: 45
		public const int DOLLAR = 285;

		// Token: 0x0400002E RID: 46
		public const int BAR = 286;

		// Token: 0x0400002F RID: 47
		public const int EQ = 287;

		// Token: 0x04000030 RID: 48
		public const int NE = 288;

		// Token: 0x04000031 RID: 49
		public const int LE = 290;

		// Token: 0x04000032 RID: 50
		public const int GE = 292;

		// Token: 0x04000033 RID: 51
		public const int LT = 294;

		// Token: 0x04000034 RID: 52
		public const int GT = 295;

		// Token: 0x04000035 RID: 53
		public const int ANCESTOR = 296;

		// Token: 0x04000036 RID: 54
		public const int ancestor = 297;

		// Token: 0x04000037 RID: 55
		public const int ANCESTOR_OR_SELF = 298;

		// Token: 0x04000038 RID: 56
		public const int ATTRIBUTE = 300;

		// Token: 0x04000039 RID: 57
		public const int attribute = 301;

		// Token: 0x0400003A RID: 58
		public const int CHILD = 302;

		// Token: 0x0400003B RID: 59
		public const int child = 303;

		// Token: 0x0400003C RID: 60
		public const int DESCENDANT = 304;

		// Token: 0x0400003D RID: 61
		public const int descendant = 305;

		// Token: 0x0400003E RID: 62
		public const int DESCENDANT_OR_SELF = 306;

		// Token: 0x0400003F RID: 63
		public const int FOLLOWING = 308;

		// Token: 0x04000040 RID: 64
		public const int following = 309;

		// Token: 0x04000041 RID: 65
		public const int FOLLOWING_SIBLING = 310;

		// Token: 0x04000042 RID: 66
		public const int sibling = 311;

		// Token: 0x04000043 RID: 67
		public const int NAMESPACE = 312;

		// Token: 0x04000044 RID: 68
		public const int NameSpace = 313;

		// Token: 0x04000045 RID: 69
		public const int PARENT = 314;

		// Token: 0x04000046 RID: 70
		public const int parent = 315;

		// Token: 0x04000047 RID: 71
		public const int PRECEDING = 316;

		// Token: 0x04000048 RID: 72
		public const int preceding = 317;

		// Token: 0x04000049 RID: 73
		public const int PRECEDING_SIBLING = 318;

		// Token: 0x0400004A RID: 74
		public const int SELF = 320;

		// Token: 0x0400004B RID: 75
		public const int self = 321;

		// Token: 0x0400004C RID: 76
		public const int COMMENT = 322;

		// Token: 0x0400004D RID: 77
		public const int comment = 323;

		// Token: 0x0400004E RID: 78
		public const int TEXT = 324;

		// Token: 0x0400004F RID: 79
		public const int text = 325;

		// Token: 0x04000050 RID: 80
		public const int PROCESSING_INSTRUCTION = 326;

		// Token: 0x04000051 RID: 81
		public const int NODE = 328;

		// Token: 0x04000052 RID: 82
		public const int node = 329;

		// Token: 0x04000053 RID: 83
		public const int MULTIPLY = 330;

		// Token: 0x04000054 RID: 84
		public const int NUMBER = 331;

		// Token: 0x04000055 RID: 85
		public const int LITERAL = 332;

		// Token: 0x04000056 RID: 86
		public const int QName = 333;

		// Token: 0x04000057 RID: 87
		public const int yyErrorCode = 256;
	}
}
