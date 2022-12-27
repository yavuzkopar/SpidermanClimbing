using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limb : MonoBehaviour
{
    public bool isHolding;
    public bool isIn = true;
    public bool isInFinal = false;
    public CikintiTimer holdingTarget;
    [SerializeField] float timer;
    [SerializeField] float boundDistance;

    Vector3 offset;
    Ray ray;
    [SerializeField] Transform allaign;
    [SerializeField] Vector2 bounds;
    bool hasHit;

    float x = 0;
    float y = 0;
    RaycastHit hit;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    private void OnMouseUp()
    {
        if (isIn)
        {
            holdingTarget.isCounting = true;
            transform.position = holdingTarget.transform.position;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            if (holdingTarget.isTaken)
            {
                GameManager.Instance.GameOverEvent?.Invoke();
            }
            else
                holdingTarget.isTaken = true;
        }
        else if (isInFinal)
        {
            GameManager.Instance.counter++;
            isInFinal = false;
            //  transform.position = new Vector3(transform.position.x, GameManager.Instance.final.position.y+0.5f,0);
            transform.position = new Vector3(finalTransform.position.x, finalTransform.position.y, 0);
        }
        else
            GameManager.Instance.GameOverEvent?.Invoke();
    }
    private void OnMouseDrag()
    {

        Vector3 v = Input.mousePosition;
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(v.x, v.y, 20));

                //transform.position = offset + hit.point;
                ////  transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                //x = transform.position.x;
                //y = transform.position.y;
                bounds = transform.position - allaign.position;
                bounds = Vector2.ClampMagnitude(bounds, boundDistance);
                x = Mathf.Clamp(x, allaign.position.x + bounds.x, allaign.position.x + bounds.x);
                y = Mathf.Clamp(y, allaign.position.y + bounds.y, allaign.position.y + bounds.y);

                transform.position = new Vector3(x, y, 0);

            
        
    }
    private void OnMouseDown()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hasHit = Physics.Raycast(ray, out hit, 1000f, GameManager.Instance.layerMask);
        offset = hit.transform.position - hit.point;
    }
    private void Update()
    {
        float z = transform.position.z - Camera.main.transform.position.z;
        Vector3 v = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y, 0));
        spriteRenderer.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(v.x, v.y, z * 0.75f));
        if (holdingTarget != null)
        {
            spriteRenderer.color = LerpedColor(holdingTarget.timer * 0.1f);
        }
       
    }
    Color LerpedColor(float t)
    {
        Color a = Color.Lerp(Color.black, Color.red, t);
        Color b = Color.Lerp(Color.red,Color.green, t);
        return Color.Lerp(a, b, t);
    }
    Transform finalTransform;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cikinti"))
        {
            isIn = true;
            holdingTarget = other.GetComponent<CikintiTimer>();
            holdingTarget.SetTextMeshEnable(true);
            
        }
        else if(other.gameObject.CompareTag("final"))
        {
            isInFinal = true;
            finalTransform = other.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Cikinti"))
        {
            isIn = false;
            holdingTarget.isCounting = false;
            holdingTarget.SetTextMeshEnable(false);
            holdingTarget.isTaken = false;
        }
    }
}
