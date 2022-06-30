using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class PointsTest
{
    // Add cash from 0, order price 3.5, timer has bonus
    [UnityTest]
    public IEnumerator AddCashFromZeroWithBonus()
    {
        GameObject obj = MonoBehaviour.Instantiate(new GameObject());
        Points points = obj.AddComponent<Points>();

        points.AddCash(new OrderStub(true, 3.5f), new TimerWithBonusStub());
        yield return null;
        Assert.AreEqual(7f, points.Cash);
    }

    // Add cash from 0, order price 3.5, timer no bonus
    [UnityTest]
    public IEnumerator AddCashFromZeroNoBonus()
    {
        GameObject obj = MonoBehaviour.Instantiate(new GameObject());
        Points points = obj.AddComponent<Points>();

        points.AddCash(new OrderStub(true, 3.5f), new TimerNoBonusStub());
        yield return null;
        Assert.AreEqual(3.5f, points.Cash);
    }

    // Add cash from 0, order price 0, timer has bonus
    [UnityTest]
    public IEnumerator AddZeroCashFromZeroWithBonus()
    {
        GameObject obj = MonoBehaviour.Instantiate(new GameObject());
        Points points = obj.AddComponent<Points>();

        points.AddCash(new OrderStub(true, 0f), new TimerWithBonusStub());
        yield return null;
        Assert.AreEqual(0f, points.Cash);
    }

    // Add cash from 0, order price 0, timer has bonus
    [UnityTest]
    public IEnumerator AddZeroCashFromZeroNoBonus()
    {
        GameObject obj = MonoBehaviour.Instantiate(new GameObject());
        Points points = obj.AddComponent<Points>();

        points.AddCash(new OrderStub(true, 0f), new TimerNoBonusStub());
        yield return null;
        Assert.AreEqual(0f, points.Cash);
    }

    // Check if CashUpdated is invoked, timer with bonus
    [UnityTest]
    public IEnumerator AddCashInvokesCashUpdated()
    {
        float val = 0;
        void Invoked(float amount)
        {
            val = amount;
        }
        
        GameObject obj = MonoBehaviour.Instantiate(new GameObject());
        Points points = obj.AddComponent<Points>();
        Points.CashUpdated += Invoked;
        points.AddCash(new OrderStub(true, 3.5f), new TimerWithBonusStub());
        yield return null;
        
        Assert.AreEqual(7f, val);
    }
}
