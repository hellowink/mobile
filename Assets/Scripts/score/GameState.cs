using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    ////public static int Points { get; private set; }

    //private static int coins = 0;

    //public static int Coins
    //{
    //    get => coins;
    //    set
    //    {
    //        coins = value;
    //        UIManager.Instance?.UpdateCoinsUI(coins); 
    //    }
    //}

    //public static void ResetCoins()
    //{
    //    coins = 0;
    //    UIManager.Instance?.UpdateCoinsUI(coins); 
    //}

    ////public static void ResetPoints()
    ////{
    ////    Points = 0;
    ////    UIManager.Instance?.UpdatePointsUI(0); 
    ////}

    ////public static void AddPoints(int baseAmount)
    ////{
    ////    int multiplier = Config.bonusEventActive ? 2 : 1;
    ////    int total = baseAmount * multiplier;
    ////    Points += total;

    ////    UIManager.Instance?.UpdatePointsUI(Points); 
    ////    Debug.Log($"+{total} puntos! Total: {Points}");
    ////}

    ////public static void CompleteSandwich()
    ////{
    ////    AddPoints((int)Config.pointsComplete);

    ////    // Guardar monedas acumuladas
    ////    Config.AddCoins(Points);
    ////}
}
