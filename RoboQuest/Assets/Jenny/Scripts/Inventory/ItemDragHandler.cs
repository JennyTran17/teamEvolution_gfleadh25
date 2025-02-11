using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform originalParent;
    CanvasGroup canvasGroup;


    Transform targetTransform;
    Transform playerTransform;
    public float minDropDistance = 2f;
    public float maxDropDistance = 3f;
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
       
    }

    void Update()
    {
       // targetTransform = GameObject.FindGameObjectWithTag("FixTarget")?.transform;
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        transform.SetParent(transform.root); //above other canvas
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.7f; //semi-transparent during drag
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; // follow the mouse
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; //enable raycast
        canvasGroup.alpha = 1f;

        Slot dropSlot = eventData.pointerEnter?.GetComponent<Slot>(); //slot where item dropped
        
        if (dropSlot == null)
        {
            GameObject dropItem = eventData.pointerEnter?.GetComponent<GameObject>();
            if(dropItem != null)
            {
                dropSlot = dropItem.GetComponentInParent<Slot>();
            }
        }

        Slot originalSlot = originalParent.GetComponent<Slot>();


        if (dropSlot != null)
        {
            if(dropSlot.currentItem != null)
            {
                //slot has an item - swap item
                dropSlot.currentItem.transform.SetParent(originalSlot.transform);
                originalSlot.currentItem = dropSlot.currentItem;
                dropSlot.currentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;


            }

            else
            {
                originalSlot.currentItem = null;
            }

            //move item into drop slot
            transform.SetParent(dropSlot.transform);
            dropSlot.currentItem = gameObject;
        }

        else
        {
            Transform closestTargetTransform = GetClosestTarget();
            
            if (!IsWithinInventory(eventData.position) && (Vector2.Distance(closestTargetTransform.position, playerTransform.position) < 2.5))
            {
                if (closestTargetTransform != null)
                {
                    targetTransform = closestTargetTransform;
                    DropItem(originalSlot);
                }
                else
                {
                    Debug.Log("No close target");
                }
            }

            else
            {
                //snap back to original slot
                transform.SetParent(originalParent);
            }
       
            
        }
        GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // center inside slot
    }



   bool IsWithinInventory(Vector2 mousePosition)
{
    RectTransform inventoryRect = originalParent.parent.GetComponent<RectTransform>();
    return RectTransformUtility.RectangleContainsScreenPoint(inventoryRect, mousePosition);
}

    void DropItem(Slot originalSlot)
    {
        originalSlot.currentItem = null;

        Vector2 dropPosition = targetTransform.position;
        Debug.Log(dropPosition);
        //Instantiate(gameObject, dropPosition, Quaternion.identity);

        //Instantiate drop item
        GameObject dropItem = Instantiate(gameObject, dropPosition, Quaternion.identity);
        Item itemComponent = dropItem.GetComponent<Item>();
        GameManager.Instance.SaveDroppedItem(itemComponent.ID, dropPosition);

        //Destroy UI one
        Destroy(gameObject);
        

    }

    Transform GetClosestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("FixTarget");
        Transform closest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject target in targets)
        {
            float distance = Vector2.Distance(target.transform.position, playerTransform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = target.transform;
            }
        }

        return closest;
    }


}
