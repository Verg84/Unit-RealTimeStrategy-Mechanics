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

    private void Awake()
    {
        cam = Camera.main;
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ToggleUnitVisuals(false);
            selectedUnits = new List<Unit>();

            SelectUnit(Input.mousePosition);
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
}
