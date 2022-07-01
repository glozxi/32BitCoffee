using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class PointsTest
{
    // Add network points from 0
    [UnityTest]
    public IEnumerator AddNetworkPointsFromZero()
    {
        GameObject obj = MonoBehaviour.Instantiate(new GameObject());
        Points points = obj.AddComponent<Points>();

        points.AddAnalysePoints();
        yield return null;
        Assert.AreEqual(5f, points.NetworkPoints);
    }

    // Check if NetworkPointsUpdated is invoked
    [UnityTest]
    public IEnumerator AddNetworkPointsInvokesNetworkPointsUpdated()
    {
        float val = 0;
        void Invoked(float amount)
        {
            val = amount;
        }

        GameObject obj = MonoBehaviour.Instantiate(new GameObject());
        Points points = obj.AddComponent<Points>();
        Points.NetworkPointsUpdated += Invoked;
        points.AddAnalysePoints();
        yield return null;

        Assert.AreEqual(5f, val);
    }
    
}
