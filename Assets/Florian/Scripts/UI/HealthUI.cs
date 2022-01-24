using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private GameObject jeuneCelte;
    [SerializeField] private int imageXSize;
    [SerializeField] private int imageYSize;

    private float xSpace;
    private float xPos;
    private float yPos;

    private List<GameObject> imageList;

    // Start is called before the first frame update
    void Start()
    {
        Texture2D tex = Resources.Load<Texture2D>("UI/heartNormal");

        xSpace = imageXSize;
        xPos = 0;
        yPos = transform.position.y;

        imageList = new List<GameObject>();

        for(int i = 0; i < jeuneCelte.GetComponent<Health>().GetHealthMax(); i++)
        {
            imageList.Add(new GameObject("Container " + i));


            RectTransform trans = imageList[i].AddComponent<RectTransform>();
            trans.sizeDelta = new Vector2(imageXSize, imageYSize);
            trans.anchoredPosition = new Vector2(0.5f, 0.5f);
            trans.localPosition = new Vector3((i * xSpace), 0, 0);
            trans.position = new Vector3(imageXSize/ 2 + (i * xSpace), yPos*2-imageYSize/2, 0);


            Image image = imageList[i].AddComponent<Image>();
            image.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            imageList[i].transform.SetParent(transform);

            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
