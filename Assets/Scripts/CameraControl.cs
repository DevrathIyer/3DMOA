    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    float speedH = 8f;
    float speedV = 4f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    [Header("MENUS")]
    public GameObject pauseMenu;
    public GameObject optionsMenu;

    public bool OpenMenu = false;
    static GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        OpenMenu = false;
        player = GameObject.Find("Player");
        speedH = PlayerPrefs.GetFloat("XSensitivity",8f);
        speedV = PlayerPrefs.GetFloat("YSensitivity",4f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!OpenMenu)
        {
            unPause();
            yaw += speedH * Input.GetAxisRaw("Mouse X");
            pitch -= speedV * Input.GetAxisRaw("Mouse Y");

            //Vector3 rotation = new Vector3( + pitch, player.transform.eulerAngles.y + yaw, 0.0f);
            Vector3 rotation = new Vector3(pitch, player.transform.eulerAngles.y, 0.0f);
            transform.eulerAngles = rotation;
        }

        //transform.forward = new Vector3(player.transform.forward.x+pitch, transform.forward.y, player.transform.forward.z);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!OpenMenu)
            {   
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                pauseMenu.SetActive(false);
                optionsMenu.SetActive(false);
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            OpenMenu = !OpenMenu;
        }
    }
    void OnGUI()
    {
        GUI.Box(new Rect((Screen.width / 2), (Screen.height / 2)-15, 5, 20), "");
        GUI.Box(new Rect((Screen.width / 2)-15, (Screen.height / 2), 20, 5), "");
        GUI.Box(new Rect((Screen.width / 2), (Screen.height / 2), 20, 5), "");
        GUI.Box(new Rect((Screen.width / 2), (Screen.height / 2), 5, 20), "");
    }
    public void unPause()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        OpenMenu = false;
    }
}
