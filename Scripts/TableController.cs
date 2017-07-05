using UnityEngine;
using System.Collections;
using System.Text;

public class Zone
{
    Vector3 position;
    bool available;
    Card card;

    public Zone(Vector3 pos)
    {
        this.position = pos;
        available = true;
    }

    public Vector3 GetPosition()
    {
        return position;
    }

    public void SetCard(Card card)
    {
        this.card = card;
        available = false;
    }
    public Card GetCard()
    {
         return this.card;
    }

    public void Clear()
    {
        this.card = null;
        available = true;
    }

    public bool GetAvailable()
    {
        return available;
    }
}



public class TableController : MonoBehaviour {

    private Zone[,] cardGrid = new Zone[7,13];
    private Zone[] dealGrid = new Zone[3];
    private Zone[] homeGrid = new Zone[4];

	// Use this for initialization

    void Awake()
    {
        float startX = -0.25f;
        float startY = 0.8f;
        float startZ = -0.2f;
        for (int i = 0; i < 7; i++)
        {
            startX += 0.07f;
            for (int j = 0; j < 13; j++)
            {
                float x = startX;
                float y = startY + 0.001f * j;
                float z = startZ + -0.03f * j;
                Zone temp = new Zone(new Vector3(x, y, z));
                cardGrid[i, j] = temp;
            }
        }


        startX = 0.1275f;
        startZ = -0.555f;

        for( int i = 0; i < 3; i++)
        {
            float x = startX - 0.03f * i;
            float y = startY + 0.001f * i;
            float z = startZ;
            Zone temp = new Zone(new Vector3(x, y, z));
            dealGrid[i] = temp;
        }

        startX = -0.25f;
        startZ = -0.3f;

        for (int i = 0; i < 4; i++)
        {
            float x = startX;
            float y = startY;
            float z = startZ + -0.10f * i;
            Zone temp = new Zone(new Vector3(x, y, z));
            homeGrid[i] = temp;
        }




    }
    void Start () {

       
      
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ClearZone()
    {
        for(int i = 0; i < 3; i++)
        {
            dealGrid[i].Clear();
        }
    }
    public Zone GetZone(int row, int column)
    {
        if(row == -1 || column == -1)
        {
            return null;
        }
        Debug.Log("getting");
        Zone z = cardGrid[row,column];
        StringBuilder sb2 = new StringBuilder();
        sb2.Append(cardGrid[row, column].GetPosition().x);
 
        //Debug.Log(sb2);
        return z;
    }
    public Vector2 GetZoneIndex(Zone z)
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 13; j++)
            {
                if(cardGrid[i,j] == z)
                {
                    return new Vector2(i, j);
                }
            }
        }

        return new Vector2(-1, -1);
    }
    public int IndexFromHand(int x, float handZ)
    {
        
        for (int i = 0; i < 13; i++)
        {
            if (-0.2 + -0.03*(i+1) < handZ)
            {
                if (cardGrid[x, i].GetCard() == null)
                {
                    int j = GetFirstAvailable(x);
                    return j - 1;
                }
                if (cardGrid[x, i].GetCard().GetFaceUp() == true)
                {
                    return i;
                }
               
            }
        }
        return -1;
    }
    public Zone GetDealZone(int pos)
    {
        Debug.Log("getting");
        Zone z = dealGrid[pos];
        StringBuilder sb2 = new StringBuilder();
        sb2.Append(dealGrid[pos].GetPosition().x);

        Debug.Log(sb2);
        return z;
    }

    public Zone GetHomeZone(int pos)
    {
        Debug.Log("getting");
        Zone z = homeGrid[pos];
       
        return z;
    }

    public Vector2 CalcZoneIndex(Transform t)
    {
        Vector2 index;
        float x = t.position.x + 0.18f;
        x = x / 7;
        x = x * 100;
        int xIndex = (int)x;
        Debug.Log(xIndex);
        
        int yIndex = GetFirstAvailable(xIndex);
        Debug.Log(yIndex);
        index = new Vector2(xIndex,yIndex);

        return index;
    }

    public Vector2 CalcZoneIndex(Vector3 t)
    {
        Vector2 index;
        float x = t.x + 0.18f;
        x = x / 7;
        x = x * 100;
        int xIndex = (int)x;
        Debug.Log(xIndex);

        int yIndex = GetAbove(xIndex);
        Debug.Log(yIndex);
        index = new Vector2(xIndex, yIndex);

        return index;
    }

    public int GetFirstAvailable(int x)
    {
        int index = -1;

        for(int i = 0; i < 13; i++)
        {
            if(cardGrid[x,i].GetAvailable())
            {
                index = i;
                return index;
            }
        }

        return index;
    }

    public int GetAbove(int x)
    {
        int index = -1;

        for (int i = 0; i < 13; i++)
        {
            if (cardGrid[x, i].GetAvailable())
            {
                index = i-2;
                return index;
            }
        }

        return index;
    }


}
