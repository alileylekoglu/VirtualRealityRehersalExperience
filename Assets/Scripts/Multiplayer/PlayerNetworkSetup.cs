using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;
public class PlayerNetworkSetup : MonoBehaviourPunCallbacks
{

    public GameObject LocalXRRigGameobject;
    public GameObject LocalHandVisualizeGameObject;

    public GameObject MainAvatarGameobject;
    public GameObject AvatarHeadGameObject;
    public GameObject AvatarBodyGameObject;
   
    
    public GameObject[] AvatarModelPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
          //Local Player   
          LocalXRRigGameobject.SetActive(true);
          LocalHandVisualizeGameObject.SetActive(true);
          
          //Getting the avatar selection data so that the correct avatar model can be instantiated.
          object avatarSelectionNumber;
          if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(MultiplayerVRConstants.AVATAR_SELECTION_NUMBER, out avatarSelectionNumber))
          {
              Debug.Log("Avatar selection number: "+ (int)avatarSelectionNumber);
              photonView.RPC("InitializeSelectedAvatarModel", RpcTarget.AllBuffered, (int)avatarSelectionNumber);
          }
        
          
          
          SetLayerRecursively(AvatarHeadGameObject, 6);
          SetLayerRecursively(AvatarBodyGameObject, 7);
          
          TeleportationArea[] teleportationAreas = GameObject.FindObjectsOfType<TeleportationArea>();
          if (teleportationAreas.Length > 0)
          {
              Debug.Log("Found "+ teleportationAreas.Length+ " teleportation area. ");
              foreach (var item in teleportationAreas)
              {
                  item.teleportationProvider = LocalXRRigGameobject.GetComponent<TeleportationProvider>();
              }
          }
          MainAvatarGameobject.AddComponent<AudioListener>();
        }
        else
        {
            //Remote Player
            LocalXRRigGameobject.SetActive(false);
            LocalHandVisualizeGameObject.SetActive(false);
            
            SetLayerRecursively(AvatarHeadGameObject, 0);
            SetLayerRecursively(AvatarBodyGameObject, 0);
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void SetLayerRecursively(GameObject go, int layerNumber)
    {
        if (go == null) return;
        foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }
    [PunRPC]
    public void InitializeSelectedAvatarModel(int avatarSelectionNumber)
    {
        GameObject selectedAvatarGameobject = Instantiate(AvatarModelPrefabs[avatarSelectionNumber],LocalXRRigGameobject.transform);

        AvatarInputConverter avatarInputConverter = transform.GetComponent<AvatarInputConverter>();
        AvatarHolder avatarHolder = selectedAvatarGameobject.GetComponent<AvatarHolder>();
        SetUpAvatarGameobject(avatarHolder.HeadTransform,avatarInputConverter.AvatarHead);
        SetUpAvatarGameobject(avatarHolder.BodyTransform,avatarInputConverter.AvatarBody);
        SetUpAvatarGameobject(avatarHolder.HandLeftTransform, avatarInputConverter.AvatarHand_Left);
        SetUpAvatarGameobject(avatarHolder.HandRightTransform, avatarInputConverter.AvatarHand_Right);
    }

    void SetUpAvatarGameobject(Transform avatarModelTransform, Transform mainAvatarTransform)
    {
        avatarModelTransform.SetParent(mainAvatarTransform);
        avatarModelTransform.localPosition = Vector3.zero;
        avatarModelTransform.localRotation = Quaternion.identity;
    }
    
}
