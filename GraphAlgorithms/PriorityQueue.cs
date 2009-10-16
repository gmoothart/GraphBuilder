//**********************************************************
//* PriorityQueue                                          *
//* Copyright (c) Julian M Bucknall 2004                   *
//* All rights reserved.                                   *
//* This code can be used in your applications, providing  *
//*    that this copyright comment box remains as-is       *
//**********************************************************
//* .NET priority queue class (heap algorithm)             *
//**********************************************************
// 10/14/09 - Gabe Moothart
//            I updated PriorityQueue to be generic, and for simplicity removed
//            features I didn't need: serialization, etc.


using System;

namespace GraphAlgorithms {

    public struct HeapEntry<T, U> where U: IComparable<U>
    {
        private T item;
        private U priority;
        public HeapEntry(T item, U priority) {
            this.item = item;
            this.priority = priority;
        }
        public T Item {
            get {return item;}
        }
        public U Priority {
            get {return priority;}
            set { priority = value; }
        }
        public void Clear() {
            item = default(T);
            priority = default(U);
        }
    }

    public enum PriorityType
    {
      /// <summary>
      /// The default. Items with maximum priority are returned
      /// first.
      /// </summary>
      Max,

      /// <summary>
      /// Return items with minimum priority first
      /// </summary>
      Min
    }
    public class PriorityQueue<T,U> where U: IComparable<U>
    {
        private PriorityType priorityType;
        private int count;
        private int capacity;
        private int version;
        private HeapEntry<T,U>[] heap;

        private const string capacityName = "capacity";
        private const string countName = "count";
        private const string heapName = "heap";

        public PriorityQueue() : this(PriorityType.Max) {}

        public PriorityQueue(PriorityType pt)
        {
            capacity = 15; // 15 is equal to 4 complete levels
            heap = new HeapEntry<T,U>[capacity];

            priorityType = pt;
        }

        public T Dequeue() {
            if (count == 0) 
                throw new InvalidOperationException();
            
            T result = heap[0].Item;
            count--;
            trickleDown(0, heap[count]);
            heap[count].Clear();
            version++;
            return result;
        }

        public void Update(T item, U newPriority )
        {
          int index = Array.FindIndex(heap, he => he.Item.Equals(item));
          if (index < 0)
            throw new ArgumentException("item could not be found in queue");

          var h = heap[index];
          U oldPriority = h.Priority;
          h.Priority = newPriority;

          switch(Compare(newPriority, oldPriority)) {
            case -1: trickleDown(index, h);
                     break;
            case  1: bubbleUp(index, h);
                     break;
            default: break; /* do nothing if equal */
          }
        }

        public void Enqueue(T item, U priority) {
            if (priority == null) 
                throw new ArgumentNullException("priority");
            if (count == capacity)  
                growHeap();
            count++;
            bubbleUp(count - 1, new HeapEntry<T,U>(item, priority));
            version++;
        }

        public int Count {
            get {return count;}
        }

        private int Compare(HeapEntry<T,U> h1, HeapEntry<T,U> h2)
        {
          return Compare(h1.Priority, h2.Priority);
        }
        private int Compare(U priority1, U priority2)
        {
          switch(priorityType) {
            case PriorityType.Max: return priority1.CompareTo(priority2);
            case PriorityType.Min: return priority2.CompareTo(priority1);
            default:
              throw new ArgumentException("PriorityType not recognized!");
          }
        }

        private void bubbleUp(int index, HeapEntry<T,U> he) {
            int parent = getParent(index);
            // note: (index > 0) means there is a parent
            while ((index > 0) && Compare(heap[parent], he) < 0) {
                heap[index] = heap[parent];
                index = parent;
                parent = getParent(index);
            }
            heap[index] = he;
        }

        private int getLeftChild(int index) {
            return (index * 2) + 1;
        }

        private int getParent(int index) {
            return (index - 1) / 2;
        }

        private void growHeap() {
            capacity = (capacity * 2) + 1;
            HeapEntry<T,U>[] newHeap = new HeapEntry<T,U>[capacity];
            System.Array.Copy(heap, 0, newHeap, 0, count);
            heap = newHeap;
        }

        private void trickleDown(int index, HeapEntry<T,U> he) {
            int child = getLeftChild(index);
            while (child < count) {
                if ((child + 1) < count && 
                    Compare(heap[child], heap[child+1]) < 0) {
                    child++;
                }
                heap[index] = heap[child];
                index = child;
                child = getLeftChild(index);
            }
            bubbleUp(index, he);
        }
    }
}
