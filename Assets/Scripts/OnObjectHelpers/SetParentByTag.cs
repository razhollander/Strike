using UnityEngine;

public class SetParentByTag : MonoBehaviour
{
    [SerializeField] private string ParentTag;
    void Awake()
    {
        GameObject parent = GameObject.FindGameObjectWithTag(ParentTag);
        if (parent != null)
        {
            transform.SetParent(GameObject.FindGameObjectWithTag(ParentTag).transform);
        }
        else
        {
            Debug.LogError("No parent was found with tag: " + ParentTag);
        }
    }

}
