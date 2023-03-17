using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool CanMoveTo = true;
    public Dictionary<HeroManager.Direction, Tile> Tiles = new Dictionary<HeroManager.Direction, Tile>();

    private void Start()
    {
        RaycastHit Hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0,0,1)), out Hit, 1))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(0, 0, 1)) * Hit.distance, Color.yellow);
            if (Hit.collider.gameObject.GetComponent<Tile>() != null) Tiles.Add(HeroManager.Direction.North, Hit.collider.gameObject.GetComponent<Tile>());
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(1, 0, 0)), out Hit, 1))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(1, 0, 0)) * Hit.distance, Color.yellow);
            if (Hit.collider.gameObject.GetComponent<Tile>() != null) Tiles.Add(HeroManager.Direction.East, Hit.collider.gameObject.GetComponent<Tile>());
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0, 0, -1)), out Hit, 1))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(0, 0, -1)) * Hit.distance, Color.yellow);
            if (Hit.collider.gameObject.GetComponent<Tile>() != null) Tiles.Add(HeroManager.Direction.South, Hit.collider.gameObject.GetComponent<Tile>());
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(-1, 0, 0)), out Hit, 1))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(-1, 0, 0)) * Hit.distance, Color.yellow);
            if (Hit.collider.gameObject.GetComponent<Tile>() != null) Tiles.Add(HeroManager.Direction.West, Hit.collider.gameObject.GetComponent<Tile>());
        }
    }
}
