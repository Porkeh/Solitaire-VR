using UnityEngine;
using Leap.Unity;
using System.Collections;

public class PinchControl : MonoBehaviour
{

    GameObject _target;
    GameObject holding;
    Card cHolding;
    bool empty = false;

    public void setTarget(GameObject target)
    {
        Debug.Log("Setting");

        if (_target == null)
        {
            if (target.tag == "Dealt")
            {
                GameObject go = GameObject.Find("DealTrigger");
                // Card c = go.GetComponent<DeckController>().GetFromDealZone();
                // if (c != null)
                // {
                //     _target = c.GetCard();
                // }

                _target = go;
            }

            if (target.tag == "Deck")
            {
                //  GameObject go = GameObject.Find("Deck");
                //  Card c = go.GetComponent<DeckController>().GetTop();
                //  if (c != null)
                //  {
                //      _target = c.GetCard();
                //  
                //  }
                //  else
                //  {
                //      empty = true;
                //  }

                GameObject go = GameObject.Find("Deck");
                _target = go;
            }
            if (target.tag == "Zone")
            {
                _target = target;
            }
            if (target.tag == "Home")
            {
                _target = target;
            }



        }

      
    }

    public void pickupTarget()
    {
        if (_target)
        {
            if (_target.tag == "Dealt")
            {
                //StartCoroutine(changeParent());

                //Rigidbody rb = _target.gameObject.GetComponent<Rigidbody>();
                //if (rb != null)
                //{
                //    rb.isKinematic = true;
                //}

                GameObject go = GameObject.Find("Deck");
                Card c = go.GetComponent<DeckController>().GetFromDealZone();
                if (c != null)
                {
                    cHolding = c;
                    holding = c.GetCard();
                }




                //  holding = _target;

            }
            if (_target.tag == "Deck")
            {
               //StartCoroutine(changeParent());
               //
               //Rigidbody rb = _target.gameObject.GetComponent<Rigidbody>();
               //if (rb != null)
               //{
               //    rb.isKinematic = true;
               //}

                if (!holding)
                {
                    GameObject go = GameObject.Find("Deck");
                    Card c = go.GetComponent<DeckController>().GetTop();
                     if (c != null)
                     {
                        go.GetComponent<DeckController>().PlaceFromDeck();

                    }
                     else
                     {
                        go.GetComponent<DeckController>().ReloadDeck();
                     }
                    
                }

               // holding = _target;
     

            }

            if(_target.tag == "Zone")
            {
                GameObject go = GameObject.Find("Deck");
                Card c = go.GetComponent<DeckController>().GetFromZone(_target, transform);
                if (c != null)
                {
                    cHolding = c;
                    holding = c.GetCard();
                }
            }

          // if (_target.tag == "Card")
          // {
          //     StartCoroutine(changeParent());
          //
          //     Rigidbody rb = _target.gameObject.GetComponent<Rigidbody>();
          //     if (rb != null)
          //     {
          //         rb.isKinematic = true;
          //     }
          //     //
          //    
          //
          //     holding = _target;
          //
          //
          // }
          //
           


        }
     

    }

    //Avoids object jumping when passing from hand to hand.
    IEnumerator changeParent()
    {
        yield return null;
        if (_target != null)
           
            _target.transform.parent = transform;
    }

    public void releaseTarget()
    {
        if (holding)
        {
            //if (holding.transform.parent == transform)
            //{ //Only reset if we are still the parent
            //    Rigidbody rb = holding.gameObject.GetComponent<Rigidbody>();
            //    if (rb != null)
            //    {
            //        rb.isKinematic = false;
            //    }
            //    holding.transform.parent = null;
            //}

            GameObject go = GameObject.Find("Deck");
           go.GetComponent<DeckController>().Place(cHolding, _target);



        }
        holding = null;
        cHolding = null;
    }

    public void clearTarget()
    {
        _target = null;
    }


    void Update()
    {
        
        if(holding)
        {

            holding.transform.position = transform.position;

        }

     //   Debug.Log(_target);
    }
}