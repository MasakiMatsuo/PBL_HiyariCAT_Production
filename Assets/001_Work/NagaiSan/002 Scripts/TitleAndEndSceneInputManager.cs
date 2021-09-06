using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleAndEndSceneInputManager : MonoBehaviour
{
    #region Player Values
    public GameObject playerRightController;
    public LineRenderer rayObject;
    #endregion

    void Update()
    {
        InitMyPlayerRay();
        Pointing();
    }

    public void InitMyPlayerRay()
    {
        #region Create Start Point of Ray (Player)
        // Create Vertex(0:Start, 1:End point)
        rayObject.SetVertexCount(2);

        // Set Vertex0 (Start point == position in RightController)
        rayObject.SetPosition(0, playerRightController.transform.position);
        #endregion
    }

    public void Pointing()
    {
        // Launch Ray
        if (OVRInput.Get(OVRInput.RawButton.RHandTrigger))
        {
            #region Set Vertex1 and With of Ray (Player)
            // Set Vertex1 (End point == position is 100m in front of RightController)
            rayObject.SetPosition(1, playerRightController.transform.position + playerRightController.transform.forward * 100.0f);

            // Set Width of Ray (This is 2 Demention)
            rayObject.SetWidth(0.01f, 0.01f);
            #endregion
            #region Create Ray, this is same scale to Line Renderer (Player)
            RaycastHit[] hits;
            hits = Physics.RaycastAll(playerRightController.transform.position, playerRightController.transform.forward * 100.0f);
            #endregion
            // Selecting
            if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
            {
                foreach (var hit in hits)
                {
                    string tagName = hit.collider.tag;

                    #region Scene Transition
                    if (tagName == "Tutorial")
                    {
                        SceneManager.LoadScene("002 Stage0");// Need to fix "scene.name" when Finalize
                    }
                    else if (tagName == "Play")
                    {
                        SceneManager.LoadScene("003 Stage1");// Need to fix "scene.name" when Finalize
                    }
                    else if (tagName == "Quit")
                    {
                        SceneManager.LoadScene("009 EndScene");// Need to fix "scene.name" when Finalize
                    }
                    else if (tagName == "ReturnTitle")
                    {
                        SceneManager.LoadScene("001 Title");// Need to fix "scene.name" when Finalize
                    }
                    #endregion

                    #region EndGame
                    else if (tagName == "End")
                    {
                        Application.Quit();
                    }
                    #endregion
                }
            }
        }
        else
        {
            // Remove the Echo of Ray (Player)
            rayObject.SetPosition(1, playerRightController.transform.position + playerRightController.transform.forward * 0.0f);
        }
    }

}
