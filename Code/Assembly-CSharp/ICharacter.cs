using System;
using UnityEngine;

// Token: 0x0200000C RID: 12
public interface ICharacter
{
	// Token: 0x17000017 RID: 23
	// (get) Token: 0x06000061 RID: 97
	// (set) Token: 0x06000062 RID: 98
	Vector3 Position { get; set; }

	// Token: 0x06000063 RID: 99
	void Activate(bool active);

	// Token: 0x17000018 RID: 24
	// (get) Token: 0x06000064 RID: 100
	GameObject GameObject { get; }

	// Token: 0x17000019 RID: 25
	// (get) Token: 0x06000065 RID: 101
	// (set) Token: 0x06000066 RID: 102
	bool FaceLeft { get; set; }

	// Token: 0x1700001A RID: 26
	// (get) Token: 0x06000067 RID: 103
	// (set) Token: 0x06000068 RID: 104
	Vector3 Speed { get; set; }

	// Token: 0x1700001B RID: 27
	// (get) Token: 0x06000069 RID: 105
	Transform Transform { get; }

	// Token: 0x1700001C RID: 28
	// (get) Token: 0x0600006A RID: 106
	bool IsOnGround { get; }

	// Token: 0x0600006B RID: 107
	void PlaceOnGround();
}
