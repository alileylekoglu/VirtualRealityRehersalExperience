using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginUIManager : MonoBehaviour
{
    public GameObject ConnectWithNamePanelGameObject;
    public GameObject ConnectWithAnonymouslyPanelGameObject;

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        ConnectWithNamePanelGameObject.SetActive(false);
        ConnectWithAnonymouslyPanelGameObject.SetActive(true);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    #endregion
   
}
