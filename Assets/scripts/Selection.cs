using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selection : MonoBehaviour
{
    public Material highlightMaterial;
    public Material selectionMaterial;
    public Material originalWhiteMat;
    public Material originalBlackMat;

    public GameState GameState;

    private Material originalMaterialHighlight;
    private Material originalMaterialSelection;
    
    [SerializeField]
    private Transform highlight;
    [SerializeField]
    private Transform selection;
    private RaycastHit raycastHit;

    private Ray ray;

    void Update()
    {
        //ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (GameState.state == State.PieceSelection && GameState.is_p1_turn)
        {
            WhiteHighLightAndSelect();
        }
        else if (GameState.state == State.PieceSelection && !GameState.is_p1_turn)
        {
            BlackHighLightAndSelect();
        }

        if (GameState.state != State.PieceSelection && GameState.is_p1_turn)
        {
            if (selection)
            {
                selection.GetComponent<MeshRenderer>().material = originalWhiteMat;
                selection = null;
            }
        }
        else if (GameState.state != State.PieceSelection && !GameState.is_p1_turn)
        {
            if (selection)
            {
                selection.GetComponent<MeshRenderer>().material = originalBlackMat;
                selection = null;
            }
        }
    }

    public void WhiteHighLightAndSelect()
    {
        // Highlight
        if (highlight != null)
        {
            highlight.GetComponent<MeshRenderer>().sharedMaterial = originalMaterialHighlight;
            highlight = null;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit, Mathf.Infinity))
        {
            highlight = raycastHit.transform;
            if (highlight.name == "6" || highlight.name == "7" || highlight.name == "8" || highlight.name == "9" || highlight.name == "10" || highlight.name == "11" && highlight != selection)
            {
                
                if (highlight.GetComponent<MeshRenderer>().material != highlightMaterial)
                {
                    originalMaterialHighlight = highlight.GetComponent<MeshRenderer>().material;
                    highlight.GetComponent<MeshRenderer>().material = highlightMaterial;
                }
                
            }
            else
            {
                highlight = null;
            }
        }

        //Selection
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (highlight)
            {
                if (selection != null)
                {
                    selection.GetComponent<MeshRenderer>().material = originalMaterialSelection;
                }
                selection = raycastHit.transform;
                if (selection.GetComponent<MeshRenderer>().material != selectionMaterial)
                {
                    originalMaterialSelection = originalMaterialHighlight;
                    selection.GetComponent<MeshRenderer>().material = selectionMaterial;
                }
                highlight = null;
            }
            else
            {
                if (selection)
                {
                    selection.GetComponent<MeshRenderer>().material = originalMaterialSelection;
                    selection = null;
                }
            }
        }
    }

    public void BlackHighLightAndSelect()
    {
        // Highlight
        if (highlight != null)
        {
            highlight.GetComponent<MeshRenderer>().sharedMaterial = originalMaterialHighlight;
            highlight = null;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit, Mathf.Infinity))
        {
            highlight = raycastHit.transform;
            if (highlight.name == "0" || highlight.name == "1" || highlight.name == "2" || highlight.name == "3" || highlight.name == "4" || highlight.name == "5" && highlight != selection)
            {
                if (highlight.GetComponent<MeshRenderer>().material != highlightMaterial)
                {
                    originalMaterialHighlight = highlight.GetComponent<MeshRenderer>().material;
                    highlight.GetComponent<MeshRenderer>().material = highlightMaterial;
                }
            }
            else
            {
                highlight = null;
            }
        }

        //Selection
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (highlight)
            {
                if (selection != null)
                {
                    selection.GetComponent<MeshRenderer>().material = originalMaterialSelection;
                }
                selection = raycastHit.transform;
                if (selection.GetComponent<MeshRenderer>().material != selectionMaterial)
                {
                    originalMaterialSelection = originalMaterialHighlight;
                    selection.GetComponent<MeshRenderer>().material = selectionMaterial;
                }
                highlight = null;
            }
            else
            {
                if (selection)
                {
                    selection.GetComponent<MeshRenderer>().material = originalMaterialSelection;
                    selection = null;
                }
            }
        }
    }
}