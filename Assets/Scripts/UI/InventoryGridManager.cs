using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Huntress.Items;
using Huntress.Characters.Player;

namespace Huntress.UI
{
    public class InventoryGridManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
    {
        [SerializeField] RectTransform iconTarget;
        UI_Inventory ui;
        PlayerInventory playerInventory;
        public ItemData data;
        public Image icon;
        public GameObject descriptionPanel;
        public TextMeshProUGUI description;
        Image dragImg;
        Canvas canvas;


        private void Start()
        {
            playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
            ui = GameObject.Find("Inventory Panel").GetComponent<UI_Inventory>();
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            dragImg = GameObject.Find("Draggable").GetComponent<Image>();
        }
        private void Update()
        {
            icon.sprite = data.icon;
            description = descriptionPanel.GetComponentInChildren<TextMeshProUGUI>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            description.text = $"{data.itemName}\n{data.description}" ;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            description.text = "";
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (data.itemName.Contains("bow"))
            {
                if (UnityEngine.InputSystem.Mouse.current.rightButton.wasPressedThisFrame)
                {
                    playerInventory.EquipItem(data);
                    if (ui.activeImage != null) ui.activeImage.color = Color.white;
                    Image active = GetComponent<Image>();
                    active.color = Color.yellow;
                    ui.activeImage = active;
                }
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            RectTransform rect = eventData.pointerDrag.GetComponent<RectTransform>();
            RectTransform img = dragImg.GetComponent<RectTransform>();
            dragImg.sprite = icon.sprite;
            icon.color = Color.white;
            icon.gameObject.SetActive(false);
            dragImg.gameObject.transform.SetParent(transform);
            img.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 20, 20);
            img.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 20, 20);
            img.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 20, 20);
            img.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 20, 20);
            img.anchorMin = new Vector2(0, 0);
            img.anchorMax = new Vector2(1, 1);
            img.pivot = new Vector2(0.5f, 0.5f);
            dragImg.enabled = true;
        }
        public void OnDrag(PointerEventData eventData)
        {
            RectTransform img = dragImg.GetComponent<RectTransform>();
            img.SetParent(canvas.transform);
            img.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            dragImg.enabled = false;
            icon.gameObject.SetActive(true);
        }
        public void OnDrop(PointerEventData eventData)
        {
            dragImg.enabled = false;
            icon.gameObject.SetActive(true);
        }
    }
}
