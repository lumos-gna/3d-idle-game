using System.Numerics;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText;

    public void UpdateGoldText(BigInteger amount)
    {
        if (amount < 1000)
        {
            goldText.text = amount.ToString();
        }

        string[] units = { "", "k", "m", "b", "t", "q", "Q", "s", "S", "o", "n" };
        int unitIndex = 0;
        BigInteger divisor = 1000;

        while (amount >= divisor && unitIndex < units.Length - 1)
        {
            amount /= divisor;
            unitIndex++;
        }

        string formatString = unitIndex == 0 ? "{0}" : "{0:0.#}{1}";
        
        goldText.text = string.Format(formatString, amount, units[unitIndex]);
    }
}
