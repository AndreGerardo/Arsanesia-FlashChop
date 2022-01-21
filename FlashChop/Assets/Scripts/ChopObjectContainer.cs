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
    private int defaultObjectHP;
    public ObjType objectType = ObjType.TAP_OBJ;

    public InputDetection inputDetection;
    private GameManager GM;

    void Awake()
    {
        GM = GameManager.Instance;
        defaultObjectHP = objectHP;
    }

    void Update()
{
    if (Input.GetMouseButtonDown(0) && objectType == ObjType.TAP_OBJ)
    {
        Vector3 raycast = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D raycastHit = Physics2D.Raycast(raycast, Camera.main.transform.forward);
        if (raycastHit.collider != null)
        {
            if (raycastHit.collider.CompareTag("Object"))
            {
                HitObject();
            }
        }
    }
}

    private void HitObject()
    {
        objectHP--;
        if(objectHP <= 0)
        {
            //Debug.Log("Destroy Object");

            GM.Score += objectScore;
            gameObject.SetActive(false);
            transform.position = Vector3.zero;
            objectHP = defaultObjectHP;
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
            }
        }else if(coll.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
            GM.SpawnNewObject();
        }
    }
    
}
