﻿using System;
using System.Collections.Generic;
using System.Linq;

// C# version of a binary heap as described here
// http://weblogs.asp.net/cumpsd/archive/2005/02/13/371719.aspx

namespace PathfindingFun
{
    /// <summary>
    /// Extra methods for helping with Binary Heaps
    /// </summary>
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

    /// <summary>
    /// A Generic Binary Heap implementation
    /// </summary>
    public class BinaryHeap<T> where T : IComparable, IComparable<T>
    {
        List<T> _heap;

        public BinaryHeap()
        {
            _heap = new List<T>
            {
                // Add something to the start of the heap to make indexing more efficent
                default
            };
        }

        ~BinaryHeap()
        {
            _heap.Clear();
        }

        public int Size() => _heap.Count - 1;
        
        public bool IsEmpty() => (_heap.Count - 1 == 0);
        
        public bool Contains(T t) => _heap.Contains(t);

        public T Peek() => _heap[1];

        public void Clear()
        {
            _heap.Clear();
            // Add something to the start of the heap to make indexing more efficent
            _heap.Add(default);
        }

        public void Insert(T t)
        {
            _heap.Add(t);
            int bubbleIndex = this.Size();

            while (bubbleIndex != 1)
            {
                Int32 parentIndex = bubbleIndex / 2;
                if (BinaryHeapExtension.IsLessThanOrEqual(_heap[bubbleIndex], _heap[parentIndex]))
                {
                    BinaryHeapExtension.Swap(_heap, parentIndex, bubbleIndex);
                    bubbleIndex = parentIndex;
                }
                else
                {
                    break;
                }
            }
        }

        public T PopMin()
        {
            if (_heap.Count - 1 == 0)
            {
                return default;
            }

            T smallest = _heap[1];
            BinaryHeapExtension.Swap(_heap, 1, this.Size());
            _heap.RemoveAt(this.Size()); // Pop back

            int swapItem = 1;
            int parent;
            do
            {
                parent = swapItem;
                if ((2 * parent + 1) <= this.Size())
                {
                    // Both children exist
                    if (BinaryHeapExtension.IsGreaterThanOrEqual(_heap[parent], _heap[2 * parent]))
                    {
                        swapItem = 2 * parent;
                    }
                    if (BinaryHeapExtension.IsGreaterThanOrEqual(_heap[swapItem], _heap[2 * parent + 1]))
                    {
                        swapItem = 2 * parent + 1;
                    }
                }
                else if ((2 * parent) <= this.Size())
                {
                    // Only 1 child exists
                    if (BinaryHeapExtension.IsGreaterThanOrEqual(_heap[parent], _heap[2 * parent]))
                    {
                        swapItem = 2 * parent;
                    }
                }

                // One of the parent's children are smaller or equal, swap them
                if (parent != swapItem)
                {
                    BinaryHeapExtension.Swap(_heap, parent, swapItem);
                }
            } while (parent != swapItem);

            return smallest;
        }

        public override string ToString()
        {
            return string.Join("\n", _heap.Skip(1).ToArray());
        }
    }
}