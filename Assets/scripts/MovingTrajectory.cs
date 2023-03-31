using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovingTrajectory : MonoBehaviour
{
    private GameState gs;

    private LineRenderer lr;
    // Start is called before the first frame update
    void Start()
    {
        gs = FindObjectOfType<GameState>();
        gs.OnDestinationMouseEnterEvent.AddListener(ShowTrajectory);
        gs.OnDestinationMouseExitEvent.AddListener(HideTrajectory);
        gs.OnPieceLand.AddListener(HideTrajectory);
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ShowTrajectory(List<int> path)
    {
        lr.positionCount = path.Count;
        var bd = gs.gameObject.GetComponent<board>();
        var vertices = path.Select(anchorIndex => bd.anchors[bd.index_2_anchor[anchorIndex]].transform.position).ToArray();
        lr.SetPositions(vertices);
    }

    private void HideTrajectory()
    {
        lr.positionCount = 0;
    }
}
