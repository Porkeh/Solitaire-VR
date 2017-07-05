using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class Card
{
    private GameObject card;
    private Card child;
    private Vector3 prevZonePos;
    private Zone zone;
    private int id;
    private int cardNumber;
    private int colour;
    private bool faceUp = false;
    private enum suit
    {
        Spade, Heart, Diamond, Club
    };
    suit cardSuit;


    public GameObject GetCard()
    {
        return card;
    }

    public Vector3 GetPrevPos()
    {
        return prevZonePos;
    }
    public void SetCard(GameObject card)
    {
        this.card = card;
    }
    public void SetChild(Card child)
    {
        this.child = child;
    }
    public void SetZone(Zone z)
    {
        this.zone = z;
    }
    public Zone GetZone()
    {
        return zone; 
    }
    public int GetID()
    {
        return id;
    }
    public void SetID(int id)
    {
        this.id = id;
        Identify();
    }

    public Card GetChild()
    {
        return child;
    }

    public int GetNum()
    {
        return cardNumber;
    }
    public int GetSuit()
    {
        return (int)cardSuit;
    }
    public bool GetFaceUp()
    {
        return faceUp;
    }
    public int GetColour()
    {
        return colour;
    }

    private void Identify()
    {
        if(id >= 0 && id < 13)
        {
            cardSuit = suit.Spade;
            cardNumber = id + 1;

        }
        else if(id >= 13 && id < 26)
        {
            cardSuit = suit.Heart;
            cardNumber = (id + 1 - 13);

        }
        else if (id >=26 && id < 39)
        {
            cardSuit = suit.Diamond;
            cardNumber = (id + 1 - 13*2);

        }
        else if (id >= 39 && id < 52)
        {
            cardSuit = suit.Club;
            cardNumber = (id + 1 - 13*3) ;

        }

        if(GetSuit() == 0 || GetSuit() == 3)
        {
            colour = 0;
        }
        else
        {
            colour = 1;
        }
    }
    public void LogPosition()
    {
        prevZonePos = card.transform.position;
    }
    public void flip()
    {
        card.transform.Rotate(new Vector3(1, 0, 0), 180);
        faceUp = !faceUp;
    }
}


public class DeckController : MonoBehaviour {


    List<Card> deck = new List<Card>();
    List<Card> dealt = new List<Card>();
    public int SizeOfDeck;
    int heightCount = 0;
    int[] homeHeight = new int[4] { 0, 0, 0, 0 };
    private int[] cardID = new int[52]  {
            0,1,2,3,4,5,6,7,8,9,10,11,12,                   //Ace-King  Spades
            13,14,15,16,17,18,19,20,21,22,23,24,25,         //Ace-King  Hearts
            26,27,28,29,30,31,32,33,34,35,36,37,38,            //Ace-King  Diamonds
            39,40,41,42,43,44,45,46,47,48,49,50,51};     //Ace-King  Clubs
                                                         // A 02 03 04 05 06 07 08 09 10 J  Q   K

    public List<Card> cardList = new List<Card>();
        
               
    // Use this for initialization                                                        
    void Awake () {

        GenerateDeck(SizeOfDeck);
        ShuffleDeck(SizeOfDeck);
       
      


	}
	void Start ()
    {
        Deal();
    }
    void GenerateDeck(int size)
    {
        for (int i = 0; i < size; i++)
        {
            // GameObject newCard = Instantiate(CardBack, new Vector3(0.0f,0.8f+0.0005f*i,-0.35f),Quaternion.AngleAxis(-90,new Vector3(1,0,0))) as GameObject;

            StringBuilder sb = new StringBuilder();
            sb.Append("PlayingCards_");
            sb.Append(i);
           // Debug.Log(sb);
            string name = sb.ToString();
           // GameObject cardBack = GameObject.Find(name);
            GameObject newCard = Instantiate(Resources.Load<GameObject>(name), new Vector3(this.transform.position.x, this.transform.position.y + 0.0005f * i, this.transform.position.z), this.transform.rotation, this.transform) as GameObject;
            newCard.transform.Rotate(new Vector3(1, 0, 0), 180);
            newCard.tag = "InDeck";
            newCard.AddComponent<BoxCollider>();
            newCard.GetComponent<BoxCollider>().size = new Vector3(0.07f, 0.1f, 0.002f);
            newCard.AddComponent<Rigidbody>();
            newCard.GetComponent<Rigidbody>().isKinematic = true;
            newCard.AddComponent<CardController>();
  

            Card card = new Card();
            card.SetCard(newCard);
            card.SetID(i);
            cardList.Add(card);
            //  newCard.AddComponent<Rigidbody>();
            //newCard.transform.parent = this.transform;

        }

        deck = cardList;
    }

    void ShuffleDeck(int size)
    {
        
        for (int i = 0; i < size; i++)
        {
            int j = Random.Range(0, size);
            Card temp = deck[i];
            deck[i] = deck[j];
            deck[j] = temp;

           
        }
      
    }


    public Card GetTop()
    {
        if (SizeOfDeck > 0)
        {
            Card top = deck[SizeOfDeck - 1];
            return top;
        }
        else
        {
            return null;
        }
    }
    
    public Card Draw()
    {
        Card top = GetTop();
        if(top == null)
        {
            return null;
        }
        top.GetCard().tag = "Card";
        //deck[cardID[SizeOfDeck - 1]] = null;
       // cards.RemoveAt(cardID[SizeOfDeck - 1]);
        SizeOfDeck--;
        Debug.Log("Draw");
        return top;

    }
    
    public void PlaceFromDeck()
    {
        TableController tc = GameObject.Find("MainTable").gameObject.GetComponent<TableController>();
        heightCount++;
        for (int i = 0; i < 3; i++)
        {
            Card toPlace = Draw();
            if (toPlace != null)
            {
                Zone z = tc.GetDealZone(i);
              //  if (z.GetCard() == null)
              //  {
                    z.SetCard(toPlace);
                    toPlace.GetCard().transform.position = z.GetPosition() + new Vector3(0,0.003f*heightCount,0);
              //  }
               // else
               // {
               //     toPlace.GetCard().transform.position = z.GetCard().GetCard().transform.position + new Vector3(0, 0.003f, 0);
               //     z.SetCard(toPlace);
               // }
                toPlace.flip();
                toPlace.SetZone(z);
                toPlace.GetCard().tag = "Dealt";
                toPlace.GetCard().transform.parent = null;
                dealt.Add(toPlace);
            }
            else
            {
               // ReloadDeck();
            }
        }
       
    }

    public bool CheckLegal(Card c, Vector2 index)
    {
        int xInd = (int)index.x;
        int yInd = (int)index.y-1;
        
        if (yInd <0 &&c.GetNum()%13 == 0)
        {
            return true;
        }
        TableController tc = GameObject.Find("MainTable").gameObject.GetComponent<TableController>();
        if (yInd >= 0)
        {
            Zone above = tc.GetZone(xInd, yInd);
            if (above.GetCard().GetColour() != c.GetColour())
            {
                if (c.GetNum() + 1 == above.GetCard().GetNum())
                {
                    return true;
                }
            }
        }

        return false;
        
    }
    public bool CheckLegalHome(Card c, Zone z)
    {

        if (c.GetChild() == null)
        {

            TableController tc = GameObject.Find("MainTable").gameObject.GetComponent<TableController>();
            if (!z.GetAvailable())
            {
                Card below = z.GetCard();
                if (below.GetColour() == c.GetColour())
                {
                    if (c.GetNum() - 1 == below.GetNum())
                    {
                        return true;
                    }
                }
            }
            else
            {
                if (c.GetNum() == 1)
                {
                    return true;
                }
            }
        }

        return false;

    }
    public void MoveChildrenZone(Card c)
    {
        if(c.GetChild() != null)
        {
            TableController tc = GameObject.Find("MainTable").gameObject.GetComponent<TableController>();
            Vector2 index = tc.GetZoneIndex(c.GetZone());
            int xInd = (int)index.x;
            int yInd = (int)index.y+1;
            Zone z = tc.GetZone(xInd, yInd);
            z.SetCard(c.GetChild());
            c.GetChild().GetZone().Clear();
            c.GetChild().SetZone(z);
            MoveChildrenZone(c.GetChild());
        }
    }
    public void Place(Card c, GameObject target)
    {
        if (target != null)
        {
            if(target.tag == "Zone")
            {
                TableController tc = GameObject.Find("MainTable").gameObject.GetComponent<TableController>();
                Vector2 index = tc.CalcZoneIndex(target.transform);
                Zone z = tc.GetZone((int)index.x, (int)index.y);

                

                if(CheckLegal(c,index))
                {
                    z.SetCard(c);
                    GameObject cardO = c.GetCard();



                    cardO.transform.position = z.GetPosition();
                   // cardO.transform.parent = null;


                    if (c.GetCard().tag == "Dealt")
                    {
                        RemoveFromDealZone(c);
                    }
                    else if (c.GetCard().tag == "Placed")
                    {
                        RemoveFromZone(c);
                  //      MoveChildrenZone(c, index);

                    }

                    c.GetCard().tag = "Placed";
                    c.SetZone(z);
                    MoveChildrenZone(c);
                    int parentX = (int)index.x;
                    int parentY = (int)index.y -1;
                    if (parentY >= 0)
                    {
                        Card parent = tc.GetZone(parentX, parentY).GetCard();
                        c.GetCard().transform.SetParent(parent.GetCard().transform);
                        parent.SetChild(c);
                    }

                    
                }
                else
                {
                    c.GetCard().transform.position = c.GetPrevPos();
                }
                
                
                
               
            }
            if (target.tag == "Home")
            {
                TableController tc = GameObject.Find("MainTable").gameObject.GetComponent<TableController>();
                int index = c.GetSuit();
                Zone z = tc.GetHomeZone(index);



                if (CheckLegalHome(c, z))
                {
                    z.SetCard(c);
                    GameObject cardO = c.GetCard();



                    cardO.transform.position = z.GetPosition() + new Vector3(0, 0.003f * homeHeight[c.GetSuit()],0) ;
                    homeHeight[c.GetSuit()]++;
                    cardO.transform.parent = null;
                    if (c.GetCard().tag == "Dealt")
                    {
                        RemoveFromDealZone(c);
                    }
                    else if(c.GetCard().tag == "Placed")
                    {
                        RemoveFromZone(c);

                    }
                    c.SetZone(z);
                  
                    c.GetCard().tag ="Home";
                }
                else
                {
                    c.GetCard().transform.position = c.GetPrevPos();
                }
            }

            if(target.tag == "Dealt")
            {
                c.GetCard().transform.position = c.GetPrevPos();
            }
            if (target.tag == "Deck")
            {
                c.GetCard().transform.position = c.GetPrevPos();
            }
        }
        else
        {
           
            c.GetCard().transform.position = c.GetPrevPos();
        }
    }
    public void RemoveFromZone(Card c)
    {
        TableController tc = GameObject.Find("MainTable").gameObject.GetComponent<TableController>();
        Vector2 index = tc.GetZoneIndex(c.GetZone());
        int xInd = (int)index.x;
        int yInd = (int)index.y-1;
        if (yInd >= 0)
        {
            Zone z = tc.GetZone(xInd, yInd);
            Card temp = z.GetCard();
            if (temp.GetFaceUp() == false)
            {
                temp.flip();
            }
        }
        c.GetZone().Clear();
        
        
    }
    public void RemoveFromDealZone(Card c)
    {
        Zone temp = c.GetZone();
        int i = dealt.Count - 4;
        if (i > 0)
        {


            temp.SetCard(dealt[i]);
        }
        else
        {
            temp.SetCard(null);
        }
        dealt.Remove(c);
    }
    public void ReloadDeck()
    {
        dealt.Reverse();
        deck = new List<Card>(dealt);
        dealt.Clear();
        for( int i = 0; i < deck.Count; i++)
        {
            GameObject card = deck[i].GetCard();
            card.transform.position = GameObject.Find("Deck").transform.position + new Vector3(0, 0.0005f * i,0);
            
            card.tag = "InDeck";
            deck[i].flip();
        
        }
        SizeOfDeck = deck.Count;
        GameObject.Find("MainTable").gameObject.GetComponent<TableController>().ClearZone();
        heightCount = 0;
        Debug.Log("Reload");
    }

    public void Deal()
    {
        Debug.Log("Dealt");
        int columnSize = 1;
            for (int i = 0; i < 7; i++)
            {
  

         //   Debug.Log(columnSize);
            for (int j = 0; j < columnSize + i; j++)
            {
                TableController tc = GameObject.Find("MainTable").gameObject.GetComponent<TableController>();
                Zone z = tc.GetZone(i,j);
                Debug.Log(columnSize);

                Card card = Draw();
                z.SetCard(card);
                card.SetZone(z);
                GameObject cardO = card.GetCard();
                cardO.GetComponent<Rigidbody>().isKinematic = true;
               
                StringBuilder sb2 = new StringBuilder();
                sb2.Append(tc);
          
                Debug.Log(sb2);
                cardO.transform.position = z.GetPosition();
                cardO.transform.parent = null;
                cardO.tag = "Placed";
                if(j == columnSize+i - 1)
                {
                    card.flip();
                }
            }

          //  columnSize++;

            }

        Debug.Log("Dealt");
        
    }

    public Card GetFromDealZone()
    {
        Card c = dealt[dealt.Count-1];
        c.LogPosition();
        return c;
    }

    public Card GetFromZone(GameObject target, Transform hand)
    {
        TableController tc = GameObject.Find("MainTable").gameObject.GetComponent<TableController>();
        Vector2 index = tc.CalcZoneIndex(target.transform);
        int xInd = (int)index.x;

        int yInd = tc.IndexFromHand(xInd,hand.position.z);
        Debug.Log(yInd);
        Zone z = tc.GetZone(xInd, yInd);
        Card c = z.GetCard();
        if (c.GetFaceUp() == true)
        {
            c.LogPosition();
            return c;
        }
        return null;
    }

	// Update is called once per frame
	void Update () {

        
     
	
	}
}
