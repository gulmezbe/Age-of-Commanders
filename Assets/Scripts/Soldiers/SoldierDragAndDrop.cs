using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.EventSystems;

public class SoldierDragAndDrop : MonoBehaviourPunCallbacks, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    Vector2 defaultPosition;

    public GameObject lane1;
    public GameObject lane2;
    public GameObject lane3;
    public GameObject lane4;
    public GameObject lane5;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    // Start is called before the first frame update
    void Start()
    {
        lane1.SetActive(false);
        lane2.SetActive(false);
        lane3.SetActive(false);
        lane4.SetActive(false);
        lane5.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .6f;
        defaultPosition = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

        int draggingLane = FindLane(rectTransform.anchoredPosition);

        if (draggingLane == 1)
        {
            lane1.SetActive(true);
            lane2.SetActive(false);
            lane3.SetActive(false);
            lane4.SetActive(false);
            lane5.SetActive(false);
        }
        else if (draggingLane == 2)
        {
            lane1.SetActive(false);
            lane2.SetActive(true);
            lane3.SetActive(false);
            lane4.SetActive(false);
            lane5.SetActive(false);
        }
        else if (draggingLane == 3)
        {
            lane1.SetActive(false);
            lane2.SetActive(false);
            lane3.SetActive(true);
            lane4.SetActive(false);
            lane5.SetActive(false);
        }
        else if (draggingLane == 4)
        {
            lane1.SetActive(false);
            lane2.SetActive(false);
            lane3.SetActive(false);
            lane4.SetActive(true);
            lane5.SetActive(false);
        }
        else if (draggingLane == 5)
        {
            lane1.SetActive(false);
            lane2.SetActive(false);
            lane3.SetActive(false);
            lane4.SetActive(false);
            lane5.SetActive(true);
        }
        else
        {
            lane1.SetActive(false);
            lane2.SetActive(false);
            lane3.SetActive(false);
            lane4.SetActive(false);
            lane5.SetActive(false);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        lane1.SetActive(false);
        lane2.SetActive(false);
        lane3.SetActive(false);
        lane4.SetActive(false);
        lane5.SetActive(false);

        canvasGroup.alpha = 1f;

        int droppedLane = FindLane(rectTransform.anchoredPosition);

        if (droppedLane != 0)
        {

        }

        rectTransform.anchoredPosition = defaultPosition; 
    }

    public int FindLane(Vector2 position)
    {
        if (position.x > -740 && position.x < 740)
        {
            if (position.y > 324 && position.y < 540)
            {
                return 1;
            }
            else if (position.y > 108 && position.y < 324)
            {
                return 2;
            }
            else if (position.y > -108 && position.y < 108)
            {
                return 3;
            }
            else if (position.y > -324 && position.y < -108)
            {
                return 4;
            }
            else if (position.y > -540 && position.y < -324)
            {
                return 5;
            }
        }

        return 0;
    }
}
