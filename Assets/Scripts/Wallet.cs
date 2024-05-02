using UnityEngine;
using UnityEngine.Events;
using Zenject;
public class Wallet : MonoBehaviour
{
    public DataEvent GemsChange;
    private int _gems;

    private void Awake()
    {
        _gems = PlayerPrefs.GetInt("_gems");

        if (GemsChange == null)
            GemsChange = new DataEvent();
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("firstStart"))
        {
            SetGems(200);
            PlayerPrefs.SetInt("firstStart", 1);
        }
    }

    public int GetGems()
    {
        return _gems;
    }

    public void SetGems(int gem)
    {
        _gems += gem;
        PlayerPrefs.SetInt("_gems", _gems);
        GemsChange.Invoke(_gems);
    }

}

[System.Serializable]
public class DataEvent : UnityEvent<int>
{
}
