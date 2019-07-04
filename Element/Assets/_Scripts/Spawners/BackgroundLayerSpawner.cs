using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class that handles creating an "infinitely scrolling background"
public class BackgroundLayerSpawner : MonoBehaviour, ExplicitInterface
{
    // Parameters
    public float imageWidth = 1080;
    public float imageHeight = 1920;
    public float pixelsPerUnit = 100;

    public float playerHorizontalLimit = 6;
    public float playerVerticalLimit = 6;

    // References
    public Transform player;
    public List<Transform> layers;

    // Lists and stuff.
    private List<Transform> curLayers = new List<Transform>();
    private List<Transform> layerPool = new List<Transform>();

    private float widthInUnits;
    private float heightInUnits;

    private Vector3 curPos;
    private Vector3 curScale;

    private float CurLayerX1
    {
        get
        {
            return this.curPos.x - (this.widthInUnits / 2);
        }
    }

    private float CurLayerX2
    {
        get
        {
            return this.curPos.x + (this.widthInUnits / 2);
        }
    }

    private float CurLayerY1
    {
        get
        {
            return this.curPos.y - (this.heightInUnits / 2);
        }
    }

    private float CurLayerY2
    {
        get
        {
            return this.curPos.y + (this.heightInUnits / 2);
        }
    }

    void Start()
    {
        this.widthInUnits = imageWidth * (1 / pixelsPerUnit);
        this.heightInUnits = imageHeight * (1 / pixelsPerUnit);

        this.curPos = this.layers[0].transform.position;
        this.curScale = this.layers[0].transform.localScale;

        for (int i = 0; i < this.layers.Count; i++)
        {
            this.layerPool.Add(this.layers[i]);
        }
    }

    void ReturnLayersToPool()
    {
        for (int i = 0; i < this.curLayers.Count; i++)
        {
            if (this.curLayers[i] != null)
            {
                this.curLayers[i].gameObject.SetActive(false);
                this.layerPool.Add(this.curLayers[i]);
            }
        }
        this.curLayers.Clear();
    }
    
    // Show or hide layers based on distance
    public void DoUpdate()
    {
        this.ReturnLayersToPool();
        this.showNewLayer(Vector3.zero);

        // Right
        bool showRight = this.player.transform.position.x + playerHorizontalLimit >= this.CurLayerX2;
        if (showRight)
        {
            this.showNewLayer(new Vector3(1, 0, 0));
        }

        bool showLeft = this.player.transform.position.x - playerHorizontalLimit <= this.CurLayerX1;
        if (showLeft)
        {
            this.showNewLayer(new Vector3(-1, 0, 0));
        }

        bool showTop = this.player.transform.position.y + playerVerticalLimit >= this.CurLayerY2;
        if (showTop)
        {
            this.showNewLayer(new Vector3(0, 1, 0));
        }

        bool showBottom = this.player.transform.position.y - playerVerticalLimit <= this.CurLayerY1;
        if (showBottom)
        {
            this.showNewLayer(new Vector3(0, -1, 0));
        }

        bool showTopLeft = showLeft && showTop;
        if (showTopLeft)
        {
            this.showNewLayer(new Vector3(-1, 1, 0));
        }

        bool showBottomLeft = showLeft && showBottom;
        if (showBottomLeft)
        {
            this.showNewLayer(new Vector3(-1, -1, 0));
        }

        bool showTopRight = showRight && showTop;
        if (showTopRight)
        {
            this.showNewLayer(new Vector3(1, 1, 0));
        }

        bool showBottomRight = showRight && showBottom;
        if (showBottomRight)
        {
            this.showNewLayer(new Vector3(1, -1, 0));
        }

        this.checkLimits();
    }
    
    // Gets a new layer from the pool
    private Transform GetLayerFromPool()
    {
        Transform newLayer = layerPool[layerPool.Count - 1];
        layerPool.RemoveAt(layerPool.Count - 1);
        return newLayer;
    }

    // Show the layer
    private void showNewLayer(Vector3 scale)
    {
        Transform newLayer = GetLayerFromPool();

        float newScaleX = scale.x != 0 ? -this.curScale.x : this.curScale.x;
        float newScaleY = scale.y != 0 ? -this.curScale.y : this.curScale.y;

        newLayer.localScale = new Vector3(newScaleX, newScaleY, newLayer.localScale.z);
        newLayer.position = this.curPos + new Vector3(this.widthInUnits * scale.x, this.heightInUnits * scale.y, 0);
        newLayer.gameObject.SetActive(true);
        this.curLayers.Add(newLayer);
    }
    
    // Modifies the "current pivot" based off of player location.
    private void checkLimits()
    {
        bool swapWithRight = this.player.transform.position.x > this.CurLayerX2;
        if (swapWithRight)
        {
            this.curPos = new Vector3(this.curPos.x + this.widthInUnits, this.curPos.y, this.curPos.z);
            this.curScale = new Vector3(-this.curScale.x, this.curScale.y, this.curScale.z);
        }

        bool swapWithLeft = this.player.transform.position.x < this.CurLayerX1;
        if (swapWithLeft)
        {
            this.curPos = new Vector3(this.curPos.x - this.widthInUnits, this.curPos.y, this.curPos.z);
            this.curScale = new Vector3(-this.curScale.x, this.curScale.y, this.curScale.z);
        }

        bool swapWithTop = this.player.transform.position.y > this.CurLayerY2;
        if (swapWithTop)
        {
            this.curPos = new Vector3(this.curPos.x, this.curPos.y + this.heightInUnits, this.curPos.z);
            this.curScale = new Vector3(this.curScale.x, -this.curScale.y, this.curScale.z);
        }

        bool swapWithBottom = this.player.transform.position.y < this.CurLayerY1;
        if (swapWithBottom)
        {
            this.curPos = new Vector3(this.curPos.x, this.curPos.y - this.heightInUnits, this.curPos.z);
            this.curScale = new Vector3(this.curScale.x, -this.curScale.y, this.curScale.z);
        }
    }
}
