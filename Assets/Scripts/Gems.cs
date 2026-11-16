using System;
using UnityEngine;

public class Gems : MonoBehaviour,IITEM
{
    public static event Action<int> OnGemCollect;
    public int worth = 5;
    public bool collected = false;
    public void Collect()
    {
        if (collected) return;
        collected= true;
        OnGemCollect?.Invoke(worth);
        SoundManager.Play("Gems");
        Destroy(gameObject);
    }

    
}
