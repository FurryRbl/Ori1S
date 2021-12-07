using System;

namespace System.Collections.Generic
{
	// Token: 0x02000095 RID: 149
	internal class RBTree : IEnumerable, IEnumerable<RBTree.Node>
	{
		// Token: 0x0600062B RID: 1579 RVA: 0x00013004 File Offset: 0x00011204
		public RBTree(object hlp)
		{
			this.hlp = hlp;
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00013014 File Offset: 0x00011214
		IEnumerator<RBTree.Node> IEnumerable<RBTree.Node>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00013024 File Offset: 0x00011224
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00013034 File Offset: 0x00011234
		private static List<RBTree.Node> alloc_path()
		{
			if (RBTree.cached_path == null)
			{
				return new List<RBTree.Node>();
			}
			List<RBTree.Node> result = RBTree.cached_path;
			RBTree.cached_path = null;
			return result;
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00013060 File Offset: 0x00011260
		private static void release_path(List<RBTree.Node> path)
		{
			if (RBTree.cached_path == null || RBTree.cached_path.Capacity < path.Capacity)
			{
				path.Clear();
				RBTree.cached_path = path;
			}
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00013090 File Offset: 0x00011290
		public void Clear()
		{
			this.root = null;
			this.version += 1U;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x000130A8 File Offset: 0x000112A8
		public RBTree.Node Intern<T>(T key, RBTree.Node new_node)
		{
			if (this.root == null)
			{
				if (new_node == null)
				{
					new_node = ((RBTree.INodeHelper<T>)this.hlp).CreateNode(key);
				}
				this.root = new_node;
				this.root.IsBlack = true;
				this.version += 1U;
				return this.root;
			}
			List<RBTree.Node> list = RBTree.alloc_path();
			int in_tree_cmp = this.find_key<T>(key, list);
			RBTree.Node node = list[list.Count - 1];
			if (node == null)
			{
				if (new_node == null)
				{
					new_node = ((RBTree.INodeHelper<T>)this.hlp).CreateNode(key);
				}
				node = this.do_insert(in_tree_cmp, new_node, list);
			}
			RBTree.release_path(list);
			return node;
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00013150 File Offset: 0x00011350
		public RBTree.Node Remove<T>(T key)
		{
			if (this.root == null)
			{
				return null;
			}
			List<RBTree.Node> path = RBTree.alloc_path();
			int num = this.find_key<T>(key, path);
			RBTree.Node result = null;
			if (num == 0)
			{
				result = this.do_remove(path);
			}
			RBTree.release_path(path);
			return result;
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00013190 File Offset: 0x00011390
		public RBTree.Node Lookup<T>(T key)
		{
			RBTree.INodeHelper<T> nodeHelper = (RBTree.INodeHelper<T>)this.hlp;
			RBTree.Node node;
			int num;
			for (node = this.root; node != null; node = ((num >= 0) ? node.right : node.left))
			{
				num = nodeHelper.Compare(key, node);
				if (num == 0)
				{
					break;
				}
			}
			return node;
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x000131EC File Offset: 0x000113EC
		public int Count
		{
			get
			{
				return (int)((this.root != null) ? this.root.Size : 0U);
			}
		}

		// Token: 0x17000133 RID: 307
		public RBTree.Node this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new IndexOutOfRangeException("index");
				}
				RBTree.Node node = this.root;
				while (node != null)
				{
					int num = (int)((node.left != null) ? node.left.Size : 0U);
					if (index == num)
					{
						return node;
					}
					if (index < num)
					{
						node = node.left;
					}
					else
					{
						index -= num + 1;
						node = node.right;
					}
				}
				throw new SystemException("Internal Error: index calculation");
			}
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0001329C File Offset: 0x0001149C
		public RBTree.NodeEnumerator GetEnumerator()
		{
			return new RBTree.NodeEnumerator(this);
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x000132A4 File Offset: 0x000114A4
		private int find_key<T>(T key, List<RBTree.Node> path)
		{
			RBTree.INodeHelper<T> nodeHelper = (RBTree.INodeHelper<T>)this.hlp;
			int num = 0;
			RBTree.Node node = this.root;
			if (path != null)
			{
				path.Add(this.root);
			}
			while (node != null)
			{
				num = nodeHelper.Compare(key, node);
				if (num == 0)
				{
					return num;
				}
				RBTree.Node item;
				if (num < 0)
				{
					item = node.right;
					node = node.left;
				}
				else
				{
					item = node.left;
					node = node.right;
				}
				if (path != null)
				{
					path.Add(item);
					path.Add(node);
				}
			}
			return num;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00013334 File Offset: 0x00011534
		private RBTree.Node do_insert(int in_tree_cmp, RBTree.Node current, List<RBTree.Node> path)
		{
			path[path.Count - 1] = current;
			RBTree.Node node = path[path.Count - 3];
			if (in_tree_cmp < 0)
			{
				node.left = current;
			}
			else
			{
				node.right = current;
			}
			for (int i = 0; i < path.Count - 2; i += 2)
			{
				path[i].Size += 1U;
			}
			if (!node.IsBlack)
			{
				this.rebalance_insert(path);
			}
			if (!this.root.IsBlack)
			{
				throw new SystemException("Internal error: root is not black");
			}
			this.version += 1U;
			return current;
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x000133E4 File Offset: 0x000115E4
		private RBTree.Node do_remove(List<RBTree.Node> path)
		{
			int num = path.Count - 1;
			RBTree.Node node = path[num];
			if (node.left != null)
			{
				RBTree.Node node2 = RBTree.right_most(node.left, node.right, path);
				node.SwapValue(node2);
				if (node2.left != null)
				{
					RBTree.Node left = node2.left;
					path.Add(null);
					path.Add(left);
					node2.SwapValue(left);
				}
			}
			else if (node.right != null)
			{
				RBTree.Node right = node.right;
				path.Add(null);
				path.Add(right);
				node.SwapValue(right);
			}
			num = path.Count - 1;
			node = path[num];
			if (node.Size != 1U)
			{
				throw new SystemException("Internal Error: red-black violation somewhere");
			}
			path[num] = null;
			this.node_reparent((num != 0) ? path[num - 2] : null, node, 0U, null);
			for (int i = 0; i < path.Count - 2; i += 2)
			{
				path[i].Size -= 1U;
			}
			if (num != 0 && node.IsBlack)
			{
				this.rebalance_delete(path);
			}
			if (this.root != null && !this.root.IsBlack)
			{
				throw new SystemException("Internal Error: root is not black");
			}
			this.version += 1U;
			return node;
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0001354C File Offset: 0x0001174C
		private void rebalance_insert(List<RBTree.Node> path)
		{
			int num = path.Count - 1;
			while (path[num - 3] != null && !path[num - 3].IsBlack)
			{
				RBTree.Node node = path[num - 2];
				bool isBlack = true;
				path[num - 3].IsBlack = isBlack;
				node.IsBlack = isBlack;
				num -= 4;
				if (num == 0)
				{
					return;
				}
				path[num].IsBlack = false;
				if (path[num - 2].IsBlack)
				{
					return;
				}
			}
			this.rebalance_insert__rotate_final(num, path);
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x000135D8 File Offset: 0x000117D8
		private void rebalance_delete(List<RBTree.Node> path)
		{
			int num = path.Count - 1;
			for (;;)
			{
				RBTree.Node node = path[num - 1];
				if (!node.IsBlack)
				{
					num = this.ensure_sibling_black(num, path);
					node = path[num - 1];
				}
				if ((node.left != null && !node.left.IsBlack) || (node.right != null && !node.right.IsBlack))
				{
					break;
				}
				node.IsBlack = false;
				num -= 2;
				if (num == 0)
				{
					return;
				}
				if (!path[num].IsBlack)
				{
					goto Block_5;
				}
			}
			this.rebalance_delete__rotate_final(num, path);
			return;
			Block_5:
			path[num].IsBlack = true;
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00013688 File Offset: 0x00011888
		private void rebalance_insert__rotate_final(int curpos, List<RBTree.Node> path)
		{
			RBTree.Node node = path[curpos];
			RBTree.Node node2 = path[curpos - 2];
			RBTree.Node node3 = path[curpos - 4];
			uint size = node3.Size;
			bool flag = node2 == node3.left;
			bool flag2 = node == node2.left;
			RBTree.Node node4;
			if (flag && flag2)
			{
				node3.left = node2.right;
				node2.right = node3;
				node4 = node2;
			}
			else if (flag && !flag2)
			{
				node3.left = node.right;
				node.right = node3;
				node2.right = node.left;
				node.left = node2;
				node4 = node;
			}
			else if (!flag && flag2)
			{
				node3.right = node.left;
				node.left = node3;
				node2.left = node.right;
				node.right = node2;
				node4 = node;
			}
			else
			{
				node3.right = node2.left;
				node2.left = node3;
				node4 = node2;
			}
			node3.FixSize();
			node3.IsBlack = false;
			if (node4 != node2)
			{
				node2.FixSize();
			}
			node4.IsBlack = true;
			this.node_reparent((curpos != 4) ? path[curpos - 6] : null, node3, size, node4);
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x000137CC File Offset: 0x000119CC
		private void rebalance_delete__rotate_final(int curpos, List<RBTree.Node> path)
		{
			RBTree.Node node = path[curpos - 1];
			RBTree.Node node2 = path[curpos - 2];
			uint size = node2.Size;
			bool isBlack = node2.IsBlack;
			RBTree.Node node3;
			if (node2.right == node)
			{
				if (node.right == null || node.right.IsBlack)
				{
					RBTree.Node left = node.left;
					node2.right = left.left;
					left.left = node2;
					node.left = left.right;
					left.right = node;
					node3 = left;
				}
				else
				{
					node2.right = node.left;
					node.left = node2;
					node.right.IsBlack = true;
					node3 = node;
				}
			}
			else if (node.left == null || node.left.IsBlack)
			{
				RBTree.Node right = node.right;
				node2.left = right.right;
				right.right = node2;
				node.right = right.left;
				right.left = node;
				node3 = right;
			}
			else
			{
				node2.left = node.right;
				node.right = node2;
				node.left.IsBlack = true;
				node3 = node;
			}
			node2.FixSize();
			node2.IsBlack = true;
			if (node3 != node)
			{
				node.FixSize();
			}
			node3.IsBlack = isBlack;
			this.node_reparent((curpos != 2) ? path[curpos - 4] : null, node2, size, node3);
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00013944 File Offset: 0x00011B44
		private int ensure_sibling_black(int curpos, List<RBTree.Node> path)
		{
			RBTree.Node value = path[curpos];
			RBTree.Node node = path[curpos - 1];
			RBTree.Node node2 = path[curpos - 2];
			uint size = node2.Size;
			bool flag;
			if (node2.right == node)
			{
				node2.right = node.left;
				node.left = node2;
				flag = true;
			}
			else
			{
				node2.left = node.right;
				node.right = node2;
				flag = false;
			}
			node2.FixSize();
			node2.IsBlack = false;
			node.IsBlack = true;
			this.node_reparent((curpos != 2) ? path[curpos - 4] : null, node2, size, node);
			if (curpos + 1 == path.Count)
			{
				path.Add(null);
				path.Add(null);
			}
			path[curpos - 2] = node;
			path[curpos - 1] = ((!flag) ? node.left : node.right);
			path[curpos] = node2;
			path[curpos + 1] = ((!flag) ? node2.left : node2.right);
			path[curpos + 2] = value;
			return curpos + 2;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00013A60 File Offset: 0x00011C60
		private void node_reparent(RBTree.Node orig_parent, RBTree.Node orig, uint orig_size, RBTree.Node updated)
		{
			if (updated != null && updated.FixSize() != orig_size)
			{
				throw new SystemException("Internal error: rotation");
			}
			if (orig == this.root)
			{
				this.root = updated;
			}
			else if (orig == orig_parent.left)
			{
				orig_parent.left = updated;
			}
			else
			{
				if (orig != orig_parent.right)
				{
					throw new SystemException("Internal error: path error");
				}
				orig_parent.right = updated;
			}
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00013AE4 File Offset: 0x00011CE4
		private static RBTree.Node right_most(RBTree.Node current, RBTree.Node sibling, List<RBTree.Node> path)
		{
			for (;;)
			{
				path.Add(sibling);
				path.Add(current);
				if (current.right == null)
				{
					break;
				}
				sibling = current.left;
				current = current.right;
			}
			return current;
		}

		// Token: 0x040001B4 RID: 436
		private RBTree.Node root;

		// Token: 0x040001B5 RID: 437
		private object hlp;

		// Token: 0x040001B6 RID: 438
		private uint version;

		// Token: 0x040001B7 RID: 439
		[ThreadStatic]
		private static List<RBTree.Node> cached_path;

		// Token: 0x02000096 RID: 150
		public interface INodeHelper<T>
		{
			// Token: 0x06000641 RID: 1601
			int Compare(T key, RBTree.Node node);

			// Token: 0x06000642 RID: 1602
			RBTree.Node CreateNode(T key);
		}

		// Token: 0x02000097 RID: 151
		public abstract class Node
		{
			// Token: 0x06000643 RID: 1603 RVA: 0x00013B28 File Offset: 0x00011D28
			public Node()
			{
				this.size_black = 2U;
			}

			// Token: 0x17000134 RID: 308
			// (get) Token: 0x06000644 RID: 1604 RVA: 0x00013B38 File Offset: 0x00011D38
			// (set) Token: 0x06000645 RID: 1605 RVA: 0x00013B48 File Offset: 0x00011D48
			public bool IsBlack
			{
				get
				{
					return (this.size_black & 1U) == 1U;
				}
				set
				{
					this.size_black = ((!value) ? (this.size_black & 4294967294U) : (this.size_black | 1U));
				}
			}

			// Token: 0x17000135 RID: 309
			// (get) Token: 0x06000646 RID: 1606 RVA: 0x00013B78 File Offset: 0x00011D78
			// (set) Token: 0x06000647 RID: 1607 RVA: 0x00013B84 File Offset: 0x00011D84
			public uint Size
			{
				get
				{
					return this.size_black >> 1;
				}
				set
				{
					this.size_black = (value << 1 | (this.size_black & 1U));
				}
			}

			// Token: 0x06000648 RID: 1608 RVA: 0x00013B98 File Offset: 0x00011D98
			public uint FixSize()
			{
				this.Size = 1U;
				if (this.left != null)
				{
					this.Size += this.left.Size;
				}
				if (this.right != null)
				{
					this.Size += this.right.Size;
				}
				return this.Size;
			}

			// Token: 0x06000649 RID: 1609
			public abstract void SwapValue(RBTree.Node other);

			// Token: 0x040001B8 RID: 440
			private const uint black_mask = 1U;

			// Token: 0x040001B9 RID: 441
			private const int black_shift = 1;

			// Token: 0x040001BA RID: 442
			public RBTree.Node left;

			// Token: 0x040001BB RID: 443
			public RBTree.Node right;

			// Token: 0x040001BC RID: 444
			private uint size_black;
		}

		// Token: 0x02000098 RID: 152
		public struct NodeEnumerator : IEnumerator, IDisposable, IEnumerator<RBTree.Node>
		{
			// Token: 0x0600064A RID: 1610 RVA: 0x00013BF8 File Offset: 0x00011DF8
			internal NodeEnumerator(RBTree tree)
			{
				this.tree = tree;
				this.version = tree.version;
				this.pennants = null;
			}

			// Token: 0x17000136 RID: 310
			// (get) Token: 0x0600064B RID: 1611 RVA: 0x00013C14 File Offset: 0x00011E14
			object IEnumerator.Current
			{
				get
				{
					this.check_current();
					return this.Current;
				}
			}

			// Token: 0x0600064C RID: 1612 RVA: 0x00013C24 File Offset: 0x00011E24
			public void Reset()
			{
				this.check_version();
				this.pennants = null;
			}

			// Token: 0x17000137 RID: 311
			// (get) Token: 0x0600064D RID: 1613 RVA: 0x00013C34 File Offset: 0x00011E34
			public RBTree.Node Current
			{
				get
				{
					return this.pennants.Peek();
				}
			}

			// Token: 0x0600064E RID: 1614 RVA: 0x00013C44 File Offset: 0x00011E44
			public bool MoveNext()
			{
				this.check_version();
				RBTree.Node node;
				if (this.pennants == null)
				{
					if (this.tree.root == null)
					{
						return false;
					}
					this.pennants = new Stack<RBTree.Node>();
					node = this.tree.root;
				}
				else
				{
					if (this.pennants.Count == 0)
					{
						return false;
					}
					RBTree.Node node2 = this.pennants.Pop();
					node = node2.right;
				}
				while (node != null)
				{
					this.pennants.Push(node);
					node = node.left;
				}
				return this.pennants.Count != 0;
			}

			// Token: 0x0600064F RID: 1615 RVA: 0x00013CE4 File Offset: 0x00011EE4
			public void Dispose()
			{
				this.tree = null;
				this.pennants = null;
			}

			// Token: 0x06000650 RID: 1616 RVA: 0x00013CF4 File Offset: 0x00011EF4
			private void check_version()
			{
				if (this.tree == null)
				{
					throw new ObjectDisposedException("enumerator");
				}
				if (this.version != this.tree.version)
				{
					throw new InvalidOperationException("tree modified");
				}
			}

			// Token: 0x06000651 RID: 1617 RVA: 0x00013D30 File Offset: 0x00011F30
			internal void check_current()
			{
				this.check_version();
				if (this.pennants == null)
				{
					throw new InvalidOperationException("state invalid before the first MoveNext()");
				}
			}

			// Token: 0x040001BD RID: 445
			private RBTree tree;

			// Token: 0x040001BE RID: 446
			private uint version;

			// Token: 0x040001BF RID: 447
			private Stack<RBTree.Node> pennants;
		}
	}
}
