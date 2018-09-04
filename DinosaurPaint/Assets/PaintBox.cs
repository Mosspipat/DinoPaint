using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBox : MonoBehaviour {

    gameManager _gameManager;
    // x = 100; y = 40;
    [SerializeField]
    int slotX, slotY;

    float width,height,boxPaintSizeX,boxPaintSizeY; 

    [SerializeField]
    GameObject boxPaint;

    [SerializeField]
    GameObject dinosaurParent;

    void Awake()
    {
        _gameManager = FindObjectOfType<gameManager>();
        _gameManager.width = width;
        _gameManager.height = height;
    }
        

    void Start () {
        CheckWidthHeight();
        GenerateBoxPaint();
    }

    void CheckWidthHeight()
    {
        width = this.GetComponent<RectTransform>().rect.width;
        height = this.GetComponent<RectTransform>().rect.height;

        boxPaintSizeX = width / (float)slotX;
        boxPaintSizeY = height / (float)slotY;

        Debug.Log("width and height : " + width + "and" + height);
        Debug.Log("sizeXBoxPaint and sizeYBoxPaint: " + boxPaintSizeX + "and" + boxPaintSizeY);
    }

    void GenerateBoxPaint()
    {
        //Instantiate(boxPaint.transform.position,) 
        Debug.Log("Test Generate");
        for (int i = 0; i < slotX; i++)
        {
            for (int j = 0; j < slotY; j++)
            {
                GameObject miniBoxPaint = Instantiate(boxPaint);
                miniBoxPaint.GetComponent<RectTransform>().sizeDelta = new Vector2(boxPaintSizeX, boxPaintSizeY);
                miniBoxPaint.transform.SetParent(dinosaurParent.transform);
                miniBoxPaint.transform.localScale = new Vector3(1, 1, 1);

                /*  miniBoxPaint.AddComponent<BoxCollider>().size = new Vector3(boxPaintSizeX,boxPaintSizeY,10f);
                miniBoxPaint.GetComponent<BoxCollider>().isTrigger = true;*/
                // set paintBox psotion                 [position of dinosaur picture]   [ back to conner of dinosaur picture]       [positionX+ middle of space paint's box X]  [PostionY + middle of space paint's box Y]
                miniBoxPaint.transform.position = dinosaurParent.transform.position - new Vector3(width/2,height/2) + new Vector3((boxPaintSizeX * i) + boxPaintSizeX/2 , (boxPaintSizeY * j) + boxPaintSizeY/2 ,0);

                //miniBoxPaint.transform.position = new Vector3((boxPaintSizeX * i) + (boxPaintSizeX / 2), (boxPaintSizeY * j) + (boxPaintSizeY / 2), 0);
            }
        } 
    }


}
