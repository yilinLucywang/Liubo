using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovingTrajectory : MonoBehaviour
{
    private GameState gs;

    private LineRenderer lr;

    [SerializeField] private GameObject hologram;
    // Start is called before the first frame update
    void Start()
    {
        gs = FindObjectOfType<GameState>();
        gs.OnDestinationMouseEnterEvent.AddListener(ShowTrajectory);
        gs.OnDestinationMouseEnterEvent.AddListener(ShowHologram);
        gs.OnDestinationMouseExitEvent.AddListener(HideTrajectory);
        gs.OnDestinationMouseExitEvent.AddListener(HideHologram);
        gs.OnPieceStartMoving.AddListener(HideHologram);
        gs.OnPieceLand.AddListener(HideTrajectory);
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ShowTrajectory(List<int> path, bool _)
    {
        lr.positionCount = path.Count;
        var bd = gs.gameObject.GetComponent<board>();
        var vertices = path.Select(anchorIndex => bd.anchors[bd.index_2_anchor[anchorIndex]].transform.position).ToArray();
        lr.SetPositions(vertices);
    }

    private void ShowHologram(List<int> path, bool isTurningIntoOwl)
    {
        var bd = gs.gameObject.GetComponent<board>();
        var destNodeIndex = path[path.Count - 1];
        hologram.SetActive(true);
        hologram.transform.position = bd.GetTopPosition(destNodeIndex, isTurningIntoOwl);//   bd.anchors[bd.index_2_anchor[]
        hologram.transform.rotation = gs.GetPieceOrientation(destNodeIndex, isTurningIntoOwl);
    }

    private void HideHologram()
    {
        hologram.SetActive(false);
    }

    private void HideTrajectory()
    {
        lr.positionCount = 0;
    }
}
