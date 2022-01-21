using UnityEngine;

public enum ObjType
{
    TAP_OBJ, SWIPE_OBJ
}

[DefaultExecutionOrder(1)]
public class ChopObjectContainer : MonoBehaviour
{

    [Header("Objects Stats")]
    public int objectScore;
    public int objectHP;
    public ObjType objectType = ObjType.TAP_OBJ;

    public InputDetection inputDetection;
    private GameManager GM;

    void Awake()
    {
        GM = GameManager.Instance;
    }

    private void HitObject()
    {
        objectHP--;
        if(objectHP <= 0)
        {
            //Debug.Log("Destroy Object");

            GM.Score += objectScore;
            gameObject.SetActive(false);
            GM.SpawnNewObject();
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Player"))
        {
            if(objectType == ObjType.SWIPE_OBJ && inputDetection.touchState != TouchState.TAP)
            {
                HitObject();
            }else if(objectType == ObjType.TAP_OBJ && inputDetection.touchState == TouchState.TAP)
            {
                HitObject();
            }
        }else if(coll.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
            GM.SpawnNewObject();
        }
    }
    
}
