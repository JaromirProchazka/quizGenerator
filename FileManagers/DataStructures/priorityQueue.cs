using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace QuizPersistence.DataStructures
{

    /// <summary>
    /// A reversed priority queue with an item of general type and int priority. The LOWEST priority in queue is poped the first. Implements a asymptotically Logarithmic Enque() and Pop() functions.
    /// <example>
    /// For example, <c>q</c> being the empty <c>PriorityQueue</c>:
    /// <code>
    /// char[] alphabet = new char[] { 'a', 'b', 'c' };
    /// q.Enque(1, alphabet[1]);
    /// q.Enque(0, alphabet[0]);
    /// q.Enque(2, alphabet[2]);
    /// q.Pop() 
    /// </code>
    /// Last line returns char <c>a</c>, as it has priority <c>0</c>
    /// </example>
    /// </summary>
    public class PriorityQueue<T> where T : IComparable
    {

        private List<Item<T>> Heap;
        public int Count { get { return Heap.Count; } }

        public PriorityQueue() 
        {
            Heap = new List<Item<T>>();
        }

        public T Pop()
        {
            T poped = Heap.First().Value;
            switchPlaces(index1: Count - 1, index2: 0);
            Heap.RemoveAt(Count - 1);
            Heapify();

            return poped;

            void Heapify()
            {
                if (Heap.Count == 0) { return; }
                int currentIndex = 0;
                while (2 * currentIndex <= Count) 
                {
                    int leftChildIndex = this.leftChildIndex(currentIndex);
                    int rightChildIndex = this.leftChildIndex(currentIndex) + 1;
                    int indexOfMin = Min( currentIndex, Min(leftChildIndex, rightChildIndex) );

                    if (indexOfMin != currentIndex)
                    {
                        switchPlaces(indexOfMin, currentIndex);
                        currentIndex = indexOfMin;
                    } else
                    {
                        break;
                    }
                }
            }
        }

        public void Enqueue(int priority, T item)
        {
            Heap.Add(new Item<T>(priority, item));
            Heapify();

            void Heapify()
            {
                if (Heap.Count == 0) { return; }
                int currentIndex = Count - 1;
                
                while (currentIndex >= 0) 
                {
                    int parentIndex = ParentIndex(currentIndex);
                    if (parentIndex < 0) { break; }

                    if (Heap[parentIndex].Priority > Heap[currentIndex].Priority )
                    {
                        switchPlaces(parentIndex, currentIndex);
                        currentIndex = parentIndex;
                    } else 
                    { 
                        break; 
                    }
                }
            }
        }

        private void switchPlaces(int index1, int index2)
        {
            Item<T> placeholder = Heap[index2];
            Heap[index2] = Heap[index1];
            Heap[index1] = placeholder;
        }

        private int ParentIndex(int childIndex)
        {
            return (childIndex + 1) / 2 - 1;
        }

        private int leftChildIndex(int parentIndex)
        {
            return 2 * parentIndex + 1;
        }

        private int Min(int a, int b)
        {
            if ( ((a < Count) ? Heap[a].Priority : int.MaxValue) < ((b < Count) ? Heap[b].Priority : int.MaxValue) )
            {
                return a;
            } else
            {
                return b;
            }
        }

        private class Item<IT> where IT : T
        {
            public int Priority { get; } 
            public IT Value { get; }

            public Item(int _priority, IT _item)
            {
                Priority = _priority;
                Value = _item;
            }
        }
    }
}
