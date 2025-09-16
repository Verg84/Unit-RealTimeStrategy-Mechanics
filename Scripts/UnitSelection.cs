using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class UnitSelection : MonoBehaviour
{
    private Camera cam;
    private Player player;
    private List<Unit> selectedUnits = new List<Unit>();

    public LayerMask unitLayerMask;

    //Box Selection
    public RectTransform boxSelector;
    private Vector2 startPosition;

    private void Awake()
    {
        cam = Camera.main;
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;

            ToggleUnitVisuals(false);
            selectedUnits = new List<Unit>();

            SelectUnit(Input.mousePosition);
        }

        if(Input.GetMouseButton(0))
        {
            UpdateSelectionBox(Input.mousePosition);
        }

        if(Input.GetMouseButtonUp(0))
        {
            ReleaseSelectionBox();
        }
    }

    void SelectUnit(Vector2 screenPos)
    {
        
        Ray ray = cam.ScreenPointToRay(screenPos);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit,200,unitLayerMask))
        {

            Unit unit = hit.collider.GetComponent<Unit>();
            if(player.IsPlayerUnit(unit))
            {
                selectedUnits.Add(unit);
                unit.ToggleUnitSelectionVisual(true);
            }
        }
    }

    void ToggleUnitVisuals(bool selection)
    {
        foreach(Unit unit in selectedUnits)
        {
            unit.ToggleUnitSelectionVisual(selection);
        }
    }

    void UpdateSelectionBox(Vector2 curPosition)
    {
        if (!boxSelector.gameObject.activeInHierarchy)
            boxSelector.gameObject.SetActive(true);

        float width = curPosition.x - startPosition.x;
        float height = curPosition.y - startPosition.y;

        boxSelector.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        boxSelector.anchoredPosition = startPosition + new Vector2(width / 2, height / 2);
    }

    void ReleaseSelectionBox()
    {
        boxSelector.gameObject.SetActive(false);

        Vector2 min = boxSelector.anchoredPosition - boxSelector.sizeDelta / 2;
        Vector2 max = boxSelector.anchoredPosition + boxSelector.sizeDelta / 2;

        foreach(Unit unit in player.playerUnits)
        {
            Vector3 screenPos = cam.WorldToScreenPoint(unit.transform.position);
            if(screenPos.x>min.x && screenPos.x<max.x && screenPos.y>min.y && screenPos.y<max.y)
            {
                selectedUnits.Add(unit);
                unit.ToggleUnitSelectionVisual(true);
            }
        }
    }
}
