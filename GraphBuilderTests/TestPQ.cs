using System;
using GraphAlgorithms;
using MbUnit.Framework;

namespace GraphBuilderTests
{
  [TestFixture] 
  public class TestPQ {
    PriorityQueue<string, int> pq;

    [SetUp] public void SetUp() {
      pq = new PriorityQueue<string, int>();
    }

    [TearDown] public void TearDown() {
    }

    [Test] public void TestEnqueueing() {
      pq.Enqueue("item one", 12);
      Assert.AreEqual(1, pq.Count,
        "Enqueuing first item should set count to 1"
        );
      pq.Enqueue("item two", 5);
      Assert.AreEqual(2, pq.Count,
        "Enqueuing second item should set count to 2"
        );
    }

    [Test] public void TestDequeueing() {
      pq.Enqueue("item one", 12);
      pq.Enqueue("item two", 5);
      string s = pq.Dequeue();
      Assert.AreEqual(1, pq.Count,
        "Dequeuing item should set count to 1"
        );
      Assert.AreEqual("item one", s,
        "Dequeuing should retrieve highest priority item"
        );
      s = pq.Dequeue();
      Assert.AreEqual(0, pq.Count,
        "Dequeuing final item should set count to 0"
        );
      Assert.AreEqual("item two", s,
        "Dequeuing should retrieve next item"
        );
    }

    [ExpectedException(typeof(InvalidOperationException))]
    [Test] public void TestDequeueWithEmptyQueue() {
      pq.Dequeue();
    }

    [Test] public void TestGrowingQueue() {
      string s;
      string expectedStr;
      for (int i = 0; i < 15; i++) {
        pq.Enqueue("item: " + i.ToString(), i * 2);
      }
      Assert.AreEqual(15, pq.Count, "Enqueued 15 items, so there should be 15 there");
      pq.Enqueue("trigger", 15);
      Assert.AreEqual(16, pq.Count, "Enqueued another, so there should be 16 there");
      for (int i = 14; i > 7; i--) {
        s = (string) pq.Dequeue();
        expectedStr = "item: " + i.ToString();
        Assert.AreEqual(expectedStr, s, "Dequeueing problem");
      }
      s = (string) pq.Dequeue();
      Assert.AreEqual("trigger", s, "Dequeueing problem");
      for (int i = 7; i >= 0; i--) {
        s = (string) pq.Dequeue();
        expectedStr = "item: " + i.ToString();
        Assert.AreEqual(expectedStr, s, "Dequeing problem");
      }
    }

    [Test] public void TestPriorityType() {
      var pq = new PriorityQueue<string, int>(PriorityType.Min);
      pq.Enqueue("item one", 12);
      pq.Enqueue("item two", 5);
      string s = pq.Dequeue();
      Assert.AreEqual(1, pq.Count,
        "Dequeuing item should set count to 1"
        );
      Assert.AreEqual("item two", s,
        "Dequeuing should retrieve highest priority item"
        );
      s = pq.Dequeue();
      Assert.AreEqual(0, pq.Count,
        "Dequeuing final item should set count to 0"
        );
      Assert.AreEqual(
        "item one", s,
        "Dequeuing should retrieve next item");
    }

    [Test]
    public void TestPriorityPremote()
    {
      string item1 = "item1";
      pq.Enqueue(item1,   12);
      pq.Enqueue("item2", 1);
      pq.Enqueue("item3", 14);
      pq.Enqueue("item4", 2);
      pq.Enqueue("item5", 21);

      pq.Update(item1, 15);

      Assert.AreEqual("item5", pq.Dequeue());
      Assert.AreEqual(item1, pq.Dequeue());
      Assert.AreEqual("item3", pq.Dequeue());
    }

    [Test]
    public void TestPriorityDemote()
    {
      string item4 = "item4";
      pq.Enqueue("item1", 1);
      pq.Enqueue("item2", 2);
      pq.Enqueue("item3", 12);
      pq.Enqueue(item4,   14);
      pq.Enqueue("item5", 21);

      pq.Update(item4, 11);

      Assert.AreEqual("item5", pq.Dequeue());
      Assert.AreEqual("item3", pq.Dequeue());
      Assert.AreEqual(item4, pq.Dequeue());
    }

    [ExpectedException(typeof(ArgumentNullException))]
    [Test] public void TestEnqueueWithNullPriority() {
      PriorityQueue<string, string> pq2 = new PriorityQueue<string, string>();
      pq2.Enqueue("A", null);
    }
  }
}