using UnityEngine;

public class JIBackgroundMusic : MonoBehaviour
{
    public static JIBackgroundMusic Instance;
    
    private void Awake()
    {
        if(!Instance) Instance = this;
        else Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }
}
