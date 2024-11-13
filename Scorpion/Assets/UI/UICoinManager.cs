using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICoinManager : MonoBehaviour
{
    [SerializeField] private GameObject uiCoinPrefab;
    [SerializeField] float offset;
    [SerializeField] float startYPos = 2f * 128.2281f + 20f;

    private int numCoins = 0;
    private List<GameObject> uiCoins = new List<GameObject> ();

    public void SetCoins (int num)
    {
        int numCoinsToDisplay = (int) Mathf.Ceil (((float) num) / 5f);
        int numCoinsCurrentlyDisplayed = (int) Mathf.Ceil (((float) numCoins) / 5f);
        if (numCoinsToDisplay > numCoinsCurrentlyDisplayed)
        {
            print (numCoinsCurrentlyDisplayed);
            float y = startYPos + ((float) numCoinsCurrentlyDisplayed) * offset;
            for (int i = 0; i < numCoinsToDisplay - numCoinsCurrentlyDisplayed; ++i)
            {
                uiCoins.Add (Instantiate (uiCoinPrefab, new Vector3 (transform.position.x, y, 0f), Quaternion.identity, transform));
                y += offset;
            }
        }
        else
        {
            for (int i = numCoinsCurrentlyDisplayed; i > numCoinsToDisplay; --i)
            {
                GameObject uiCoin = uiCoins[i];
                uiCoins.RemoveAt (i);
                Destroy (uiCoin);
            }
        }
        numCoins = num;
    }
}
