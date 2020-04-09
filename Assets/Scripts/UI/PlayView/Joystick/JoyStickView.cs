using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickView : MonoBehaviour
{

    [SerializeField] PlayerTypeGameObjectDictionary joyStickDictionary;


    private void Start()
    {
        GameManager.Instance.GameStateManager.GetState<NormalPlayState>().OnEnter += SetJoyStick;
        SetJoyStick();
    }
    public void SetJoyStick()
    {
        ePlayerType playerType = GameManager.Instance.player.PlayerType;
        joyStickDictionary[playerType].SetActive(true);
    }
}
[System.Serializable] public class PlayerTypeGameObjectDictionary : SerializableDictionary<ePlayerType, GameObject> { }
