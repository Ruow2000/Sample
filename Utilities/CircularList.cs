using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
	public class CircularList<T>
	{
		private const int _defaultSize = 100;
		int _internalCount { get; set; }
		private T[] _items { get; }

		#region Properties

		public int Length { get; }
		public int Count => Filled ? Length : _internalCount;
		public bool Filled => _internalCount > Length;
		public int LastIndex => (_internalCount > 0) ? _internalCount - 1 : 0;
		public int FirstIndex => (_internalCount > Length) ? _internalCount - Length : 0;

		public T Last
		{
			get { return this[LastIndex]; }
			set { this[LastIndex] = value; }
		}
		public T First
		{
			get { return this[FirstIndex]; }
			set { this[FirstIndex] = value; }
		}

		#endregion

		public CircularList(int size = _defaultSize)
		{
			if (size <= 0)
				throw new ArgumentOutOfRangeException($"Invalid size {size}.");

			_items = new T[size];
			Length = _items.Length;
			_internalCount = 0;
		}

		private void ValidateIndexRange(int index)
		{
			if (Filled && ((index >= _internalCount) || (index < _internalCount - Length)))
				throw new ArgumentOutOfRangeException($"Invalid index {index} specified.");

			if (!Filled && ((index >= _internalCount) || (index < 0)))
				throw new ArgumentOutOfRangeException($"Invalid index {index} specified.");
		}

		public void Add(T item)
		{
			_internalCount++;
			this[_internalCount - 1] = item;
		}

		public void Add(T item, out T replacedItem)
		{
			_internalCount++;
			replacedItem = this[_internalCount - 1];
			this[_internalCount - 1] = item;
		}

		public IEnumerator<T> GetEnumerator()
		{
			if (Count > 0)
			{
				for (int i = FirstIndex; i <= LastIndex; i++)
				{
					yield return this[i];
				}
			}
		}

		public T this[int index]
		{
			get
			{
				ValidateIndexRange(index);
				return _items[index % Length];
			}
			set
			{
				ValidateIndexRange(index);
				_items[index % Length] = value;
			}
		}
	}
}
