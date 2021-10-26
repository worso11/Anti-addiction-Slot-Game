using System;
using UnityEngine;

public class WinningController : MonoBehaviour
{
    public ParticleSystem stars;
    
    private int[,] _winningCoins;

    private void Start()
    {
        _winningCoins = new int[3, 5];
    }

    public void CheckForWin(int[][] coins)
    {
        for (var i = 0; i < coins.Length; i++)
        {
            for (var j = 1; j < coins[0].Length - 1; j++)
            {
                if (coins[i][j] != coins[i][j - 1] || coins[i][j] != coins[i][j + 1]) continue;
                for (var k = j - 1; k < j + 2; k++)
                {
                    _winningCoins[i,k] = 1;
                }
            }
        }
    }

    public void SpawnWinningStars()
    {
        for (var i = 0; i < _winningCoins.GetLength(0); i++)
        {
            for (var j = 0; j < _winningCoins.GetLength(1); j++)
            {
                if (_winningCoins[i,j] == 1)
                {
                    Instantiate(stars, new Vector3(j*2 - 4, i*2 - 2,0), Quaternion.identity);
                }
            } 
        }
        
        Array.Clear(_winningCoins, 0, _winningCoins.Length);
    }

    public static void DestroyStars()
    {
        var starsToDestroy = GameObject.FindGameObjectsWithTag("Stars");

        foreach (var star in starsToDestroy)
        {
            Destroy(star);
        }
    }
}