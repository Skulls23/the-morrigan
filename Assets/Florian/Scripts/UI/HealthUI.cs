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

    Texture2D texNormal;
    Texture2D texCorrupted;
    Texture2D texEmpty;

    private List<GameObject> imageList;

    // Start is called before the first frame update
    void Start()
    {
        texNormal = Resources.Load<Texture2D>("UI/heartNormal");
        texCorrupted = Resources.Load<Texture2D>("UI/heartCorrupted"); ;
        texEmpty = Resources.Load<Texture2D>("UI/heartEmpty");

        CreatingHealthContainerBar();
    }

    public List<GameObject> GetImageList()
    {
        return imageList;
    }

    public GameObject GetHeartImage(int num)
    {
        return imageList[num];
    }

    /// <summary>
    /// Create the UI.
    /// Called at first frame.
    /// </summary>
    private void CreatingHealthContainerBar()
    {
        xSpace = imageXSize;
        xPos = 0;
        yPos = transform.position.y;

        imageList = new List<GameObject>();

        for (int i = 0; i < jeuneCelte.GetComponent<Health>().GetHealthMax(); i++)
        {
            imageList.Add(new GameObject("Container " + i));

            RectTransform trans = imageList[i].AddComponent<RectTransform>();
            trans.sizeDelta = new Vector2(imageXSize, imageYSize);
            trans.anchoredPosition = new Vector2(0.5f, 0.5f);
            trans.localPosition = new Vector3((i * xSpace), 0, 0);
            trans.position = new Vector3(imageXSize / 2 + (i * xSpace), yPos * 2 - imageYSize / 2, 0);

            Image image = imageList[i].AddComponent<Image>();
            image.sprite = Sprite.Create(texNormal, new Rect(0, 0, texNormal.width, texNormal.height), new Vector2(0.5f, 0.5f));
            imageList[i].transform.SetParent(transform);
        }
    }

    /// <summary>
    /// Will recreate each heart box at each change.
    /// </summary>
    /// <param name="numNormal">The number of normal hearts</param>
    /// <param name="NumCorrupted">The number of corrupted hearts</param>
    /// <param name="numEmpty">The number of empty hearts</param>
    public void RefreshUI(int numNormal, int NumCorrupted, int numEmpty)
    {

        xSpace = imageXSize;
        xPos = 0;
        yPos = transform.position.y;

        imageList = new List<GameObject>();

        for (int i = 0; i < jeuneCelte.GetComponent<Health>().GetHealthMax(); i++)
        {
            imageList.Add(new GameObject("Container " + i));

            RectTransform trans = imageList[i].AddComponent<RectTransform>();
            trans.sizeDelta = new Vector2(imageXSize, imageYSize);
            trans.anchoredPosition = new Vector2(0.5f, 0.5f);
            trans.localPosition = new Vector3((i * xSpace), 0, 0);
            trans.position = new Vector3(imageXSize / 2 + (i * xSpace), yPos * 2 - imageYSize / 2, 0);

            Image image = imageList[i].AddComponent<Image>();
            if(numNormal-- > 0)
                image.sprite = Sprite.Create(texNormal, new Rect(0, 0, texNormal.width, texNormal.height), new Vector2(0.5f, 0.5f));
            else if(NumCorrupted-- > 0)
                image.sprite = Sprite.Create(texCorrupted, new Rect(0, 0, texCorrupted.width, texCorrupted.height), new Vector2(0.5f, 0.5f));
            else if(numEmpty-- > 0)
                image.sprite = Sprite.Create(texEmpty, new Rect(0, 0, texEmpty.width, texEmpty.height), new Vector2(0.5f, 0.5f));

            imageList[i].transform.SetParent(transform);
        }
    }
}
