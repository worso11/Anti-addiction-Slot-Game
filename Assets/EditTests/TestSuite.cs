using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Scene = UnityEditor.SearchService.Scene;

public class TestSuite
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestSuiteSimplePasses()
    {
        // Use the Assert class to test conditions
        var winningLine = GameObject.Find("WinningLine");
        var rightNumber = winningLine.transform.GetChild(0).gameObject;
        var rightNumberColorBefore = rightNumber.GetComponent<SpriteRenderer>().color;
        
        rightNumber.GetComponent<Number>().OnMouseOver();
        
        var rightNumberColorAfter = rightNumber.GetComponent<SpriteRenderer>().color;
        
        Assert.AreNotEqual(rightNumberColorBefore, rightNumberColorAfter);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestSuiteWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
