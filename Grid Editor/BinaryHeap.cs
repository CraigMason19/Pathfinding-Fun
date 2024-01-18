using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// C# version of a binary heap as described here
// http://www.policyalmanac.org/games/binaryHeaps.htm
// http://weblogs.asp.net/cumpsd/archive/2005/02/13/371719.aspx

namespace PathfindingFun
{
    static class BinaryHeapExtension
    {
        // Data source, index a, index b
        public static void Swap<T>(List<T> data, int a, int b)
        {
            T tmp = data[a];
            data[a] = data[b];
            data[b] = tmp;
        }

        public static bool IsLessThanOrEqual<T>(T value, T other) where T : IComparable
        {
            return value.CompareTo(other) <= 0;
        }

        public static bool IsGreaterThanOrEqual<T>(T value, T other) where T : IComparable
        {
            return value.CompareTo(other) >= 0;
        }
    }

    public class BinaryHeap<T> where T : IComparable, IComparable<T>
    {
        List<T> _Heap;

        public BinaryHeap()
        {
            _Heap = new List<T>();
            // Add something to the start of the heap to make indexing more efficent
            _Heap.Add(default(T));
        }

        ~BinaryHeap()
        {
            _Heap.Clear();
        }

        public int Size()
        {
            return _Heap.Count - 1;
        }

        public bool IsEmpty()
        {
            return (_Heap.Count - 1 == 0);
        }

        public bool Contains(T t)
        {
            return _Heap.Contains(t);
        }

        public T Conatins2(T t)
        {
            return _Heap.FirstOrDefault(x => t.Equals(x));
        }

        public void Clear()
        {
            _Heap.Clear();
            // Add something to the start of the heap to make indexing more efficent
            _Heap.Add(default(T));
        }

        public void Insert(T t)
        {
            _Heap.Add(t);
            int bubbleIndex = this.Size();

            while (bubbleIndex != 1)
            {
                Int32 parentIndex = bubbleIndex / 2;
                if (BinaryHeapExtension.IsLessThanOrEqual(_Heap[bubbleIndex], _Heap[parentIndex]))
                {
                    BinaryHeapExtension.Swap(_Heap, parentIndex, bubbleIndex);
                    bubbleIndex = parentIndex;
                }
                else
                {
                    break;
                }
            }
        }

        public T Peek()
        {
            return _Heap[1];
        }

        public T PopMin()
        {
            if (_Heap.Count - 1 == 0)
            {
                return default(T);
            }

            T smallest = _Heap[1];
            BinaryHeapExtension.Swap(_Heap, 1, this.Size());
            _Heap.RemoveAt(this.Size()); // Pop back

            int swapItem = 1, parent = 1;
            do
            {
                parent = swapItem;
                if ((2 * parent + 1) <= this.Size())
                {
                    // Both children exist
                    if (BinaryHeapExtension.IsGreaterThanOrEqual(_Heap[parent], _Heap[2 * parent]))
                    {
                        swapItem = 2 * parent;
                    }
                    if (BinaryHeapExtension.IsGreaterThanOrEqual(_Heap[swapItem], _Heap[2 * parent + 1]))
                    {
                        swapItem = 2 * parent + 1;
                    }
                }
                else if ((2 * parent) <= this.Size())
                {
                    // Only 1 child exists
                    if (BinaryHeapExtension.IsGreaterThanOrEqual(_Heap[parent], _Heap[2 * parent]))
                    {
                        swapItem = 2 * parent;
                    }
                }

                // One if the parent's children are smaller or equal, swap them
                if (parent != swapItem)
                {
                    BinaryHeapExtension.Swap(_Heap, parent, swapItem);
                }
            } while (parent != swapItem);

            return smallest;
        }


        public override string ToString()
        {
            return string.Join("\n", _Heap.Skip(1).ToArray());
        }
    }

}
