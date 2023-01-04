using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    Transform image;
    [SerializeField] Image imageChild;


    private void Start()
    {
        image = imageChild.transform.parent;
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
            {
                GameManager.Instance.Star(transform);
                holdingTarget.isTaken = true;
                GameManager.Instance.PlaySound();
            }
        }
        else if (isInFinal)
        {
            GameManager.Instance.counter++;
            isInFinal = false;
            transform.position = new Vector3(finalTransform.position.x, finalTransform.position.y, 0);
            GameManager.Instance.PlaySound();
        }
        else
            GameManager.Instance.GameOverEvent?.Invoke();
    }
    private void OnMouseDrag()
    {

        Vector3 v = Input.mousePosition;
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(v.x, v.y, 20));

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
        image.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        if (holdingTarget != null)
        {
            imageChild.fillAmount = 1 - holdingTarget.timer * 0.1f;
        }

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
        else if (other.gameObject.CompareTag("final"))
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
