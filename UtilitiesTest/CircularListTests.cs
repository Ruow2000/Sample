using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilities;

namespace UtilitiesTest
{
	/// <summary>
	/// Tests Utilities.CircularList class
	/// </summary>

	public class CircularListTests
	{
		protected CircularList<int> _instance;
		protected int[] _numbers = { 243, 345, 889, 125, 7532, 43, 78, 976, 654, 787, 623, 21, 38, 94, 756, 489, 124, 359, 368, 9987, 6574, 124, 398, 364 };
		protected int _circularListSize = 11;

		[TestInitialize]
		public void TestInitialize()
		{
			_instance = new CircularList<int>(_circularListSize);
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_instance = null;
		}
	}

	[TestClass]
	public class Constructor : CircularListTests
	{
		[TestCategory("CircularListTests")]
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void InvalidSize()
		{
			int invalidSize = -5;
			CircularList<int> circularList = new CircularList<int>(invalidSize);
		}
	}

	[TestClass]
	public class Add : CircularListTests
	{
		[TestCategory("CircularListTests")]
		[TestMethod]
		public void Unfilled()
		{
			for (int i = 0; i < 3; i++)
			{
				_instance.Add(_numbers[i]);
			}

			Assert.AreEqual(_instance.Count, 3);
			Assert.AreEqual(_instance.Filled, false);
			Assert.AreEqual(_instance.First, _numbers[0]);
			Assert.AreEqual(_instance.Last, _numbers[2]);
			Assert.AreEqual(_instance.FirstIndex, 0);
			Assert.AreEqual(_instance.LastIndex, 2);
		}

		[TestCategory("CircularListTests")]
		[TestMethod]
		public void Filled()
		{
			foreach (int number in _numbers)
			{
				_instance.Add(number);
			}

			int firstIndex = _numbers.Length - _circularListSize;
			int lastIndex = _numbers.Length - 1;
			Assert.AreEqual(_instance.Count, _instance.Length);
			Assert.AreEqual(_instance.Filled, true);
			Assert.AreEqual(_instance.First, _numbers[firstIndex]);
			Assert.AreEqual(_instance.Last, _numbers[lastIndex]);
			Assert.AreEqual(_instance.FirstIndex, firstIndex);
			Assert.AreEqual(_instance.LastIndex, lastIndex);
		}

		[TestCategory("CircularListTests")]
		[TestMethod]
		public void Unfilled_Replaced()
		{
			int replaced = -1;
			for (int i = 0; i < 3; i++)
			{
				_instance.Add(_numbers[i], out replaced);
				Assert.AreEqual(replaced, default(int));
			}

			Assert.AreEqual(_instance.Count, 3);
			Assert.AreEqual(_instance.Filled, false);
			Assert.AreEqual(_instance.First, _numbers[0]);
			Assert.AreEqual(_instance.Last, _numbers[2]);
			Assert.AreEqual(_instance.FirstIndex, 0);
			Assert.AreEqual(_instance.LastIndex, 2);
		}

		[TestCategory("CircularListTests")]
		[TestMethod]
		public void Filled_Replaced()
		{
			int replaced = -1;
			for (int i = 0; i < _numbers.Length; i++)
			{
				_instance.Add(_numbers[i], out replaced);
				if (i < 11)
					Assert.AreEqual(replaced, default(int));
				else
					Assert.AreEqual(replaced, _numbers[i - 11]);
			}

			int firstIndex = _numbers.Length - _circularListSize;
			int lastIndex = _numbers.Length - 1;
			Assert.AreEqual(_instance.Count, _circularListSize);
			Assert.AreEqual(_instance.Filled, true);
			Assert.AreEqual(_instance.First, _numbers[firstIndex]);
			Assert.AreEqual(_instance.Last, _numbers[lastIndex]);
			Assert.AreEqual(_instance.FirstIndex, firstIndex);
			Assert.AreEqual(_instance.LastIndex, lastIndex);
		}
	}

	[TestClass]
	public class GetEnumerator : CircularListTests
	{
		[TestCategory("CircularListTests")]
		[TestMethod]
		public void AllNumbers()
		{
			foreach (int number in _numbers)
			{
				_instance.Add(number);
			}

			int index = 13;
			foreach (int value in _instance)
			{
				Assert.AreEqual(value, _instance[index]);
				index++;
			}
		}

		[TestCategory("CircularListTests")]
		[TestMethod]
		public void PartialNumbers()
		{
			for (int i = 0; i <= 18; i++)
			{
				_instance.Add(_numbers[i]);
			}

			int index = 8;
			foreach (int value in _instance)
			{
				Assert.AreEqual(value, _instance[index]);
				index++;
			}
		}

		[TestCategory("CircularListTests")]
		[TestMethod]
		public void CircListNotFull()
		{
			for (int i = 0; i <= 6; i++)
			{
				_instance.Add(_numbers[i]);
			}

			int index = 0;
			foreach (int value in _instance)
			{
				Assert.AreEqual(value, _instance[index]);
				index++;
			}
		}

		[TestCategory("CircularListTests")]
		[TestMethod]
		public void CircListEmpty()
		{
			foreach (int value in _instance)
			{
				Assert.Fail("CircularList is empty and shouldn't enumerate anything.");
			}
		}
	}

	[TestClass]
	public class Indexer : CircularListTests
	{
		[TestCategory("CircularListTests")]
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void Get_InvalidLowBoundIndex()
		{
			int tmpValue = _instance[int.MinValue];
		}

		[TestCategory("CircularListTests")]
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void Get_InvalidHighBoundIndex()
		{
			int tmpValue = _instance[int.MaxValue];
		}

		[TestCategory("CircularListTests")]
		[TestMethod]
		public void Get_ValidIndex()
		{
			_instance.Add(_numbers[0]);
			Assert.AreEqual(_instance[0], _numbers[0]);
		}

		[TestCategory("CircularListTests")]
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void Set_InvalidLowBoundIndex()
		{
			_instance[int.MinValue] = _numbers[0];
		}

		[TestCategory("CircularListTests")]
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void Set_InvalidHighBoundIndex()
		{
			_instance[int.MaxValue] = _numbers[0];
		}

		[TestCategory("CircularListTests")]
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void Set_ZeroIndexEmptyCircList()
		{
			_instance[0] = _numbers[0];
		}

		[TestCategory("CircularListTests")]
		[TestMethod]
		public void Set_ValidIndex()
		{
			for (int i = 0; i < 16; i++)
			{
				_instance.Add(_numbers[i]);
			}

			int lastIndex = _numbers.Length - 1;
			_instance[14] = _numbers[lastIndex];

			Assert.AreEqual(_instance[14], _numbers[lastIndex]);
		}
	}
}
