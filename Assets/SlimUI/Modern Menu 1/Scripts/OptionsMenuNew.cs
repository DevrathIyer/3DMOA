using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class OptionsMenuNew : MonoBehaviour {

    public AgentManagerControl agentManager;
    // toggle buttons
    [Header("VIDEO SETTINGS")]
    public GameObject fullscreentext;
    public GameObject ambientocclusiontext;
    public GameObject shadowofftext;
    public GameObject shadowofftextLINE;
    public GameObject shadowlowtext;
    public GameObject shadowlowtextLINE;
    public GameObject shadowhightext;
    public GameObject shadowhightextLINE;
    public GameObject aaofftext;
    public GameObject aaofftextLINE;
    public GameObject aa2xtext;
    public GameObject aa2xtextLINE;
    public GameObject aa4xtext;
    public GameObject aa4xtextLINE;
    public GameObject aa8xtext;
    public GameObject aa8xtextLINE;
    public GameObject vsynctext;
    public GameObject motionblurtext;
    public GameObject texturelowtext;
    public GameObject texturelowtextLINE;
    public GameObject texturemedtext;
    public GameObject texturemedtextLINE;
    public GameObject texturehightext;
    public GameObject texturehightextLINE;
    public GameObject cameraeffectstext;

    [Header("GAME SETTINGS")]
    public GameObject showhudtext;
    public GameObject tooltipstext;
    public GameObject difficultynormaltext;
    public GameObject difficultynormaltextLINE;
    public GameObject difficultyhardcoretext;
    public GameObject difficultyhardcoretextLINE;

    [Header("CONTROLS SETTINGS")]
    public GameObject invertmousetext;

    // sliders
    public GameObject agentSpeedSlider;
    public GameObject sensitivityYSlider;
    public GameObject sensitivityXSlider;
    public GameObject playerSpeedSlider;

    public GameObject agentLabel;
    public GameObject increaseAgentButton;
    public GameObject decreaseAgentButton;

    public GameObject ExperimentID;
    public GameObject Key;
    public GameObject Worker;

    private int agentCount = 0;
    private int maxAgents = 16;
    private int minAgents = 1;

    private string experimentIDvalue = "";
    private string keyvalue = "";
    private string workervalue = "";

    private float sliderValueAgentSpeed = 0.0f;
    private float sliderValueXSensitivity = 0.0f;
    private float sliderValueYSensitivity = 0.0f;
    private float sliderValueSmoothing = 0.0f;
    private float sliderValuePlayerSpeed = 0.0f;

    public GameObject camera;

    public void Start() {

        // check slider values
        agentCount = PlayerPrefs.GetInt("AgentCount", 6);
        agentLabel.GetComponent<TextMeshPro>().text = agentCount.ToString();

        //agentSpeedSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("AgentSpeed",2f);
        playerSpeedSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("PlayerSpeed");
        sensitivityXSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("XSensitivity");
        sensitivityYSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("YSensitivity");

        ExperimentID.GetComponent<TMP_Text>().text = PlayerPrefs.GetString("ExperimentID","NO EXPERIMENT");
        Key.GetComponent<TMP_Text>().text = PlayerPrefs.GetString("Key","NO KEY");
        Worker.GetComponent<TMP_Text>().text = PlayerPrefs.GetString("Worker", "NO WORKER");

        // check full screen
        if (Screen.fullScreen == true) {
            fullscreentext.GetComponent<TMP_Text>().text = "on";
        }
        else if (Screen.fullScreen == false) {
            fullscreentext.GetComponent<TMP_Text>().text = "off";
        }

        // check hud value
        if (PlayerPrefs.GetInt("ShowHUD") == 0) {
            showhudtext.GetComponent<TMP_Text>().text = "off";
        }
        else {
            showhudtext.GetComponent<TMP_Text>().text = "on";
        }

        // check tool tip value
        if (PlayerPrefs.GetInt("ToolTips") == 0) {
            tooltipstext.GetComponent<TMP_Text>().text = "off";
        }
        else {
            tooltipstext.GetComponent<TMP_Text>().text = "on";
        }

        // check shadow distance/enabled
        if (PlayerPrefs.GetInt("Shadows") == 0) {
            QualitySettings.shadowCascades = 0;
            QualitySettings.shadowDistance = 0;
            //shadowofftext.GetComponent<TMP_Text>().text = "OFF";
            //shadowlowtext.GetComponent<TMP_Text>().text = "low";
            //shadowhightext.GetComponent<TMP_Text>().text = "high";
            shadowofftextLINE.gameObject.SetActive(true);
            shadowlowtextLINE.gameObject.SetActive(false);
            shadowhightextLINE.gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Shadows") == 1) {
            QualitySettings.shadowCascades = 2;
            QualitySettings.shadowDistance = 75;
            //shadowofftext.GetComponent<TMP_Text>().text = "off";
            //shadowlowtext.GetComponent<TMP_Text>().text = "LOW";
            //shadowhightext.GetComponent<TMP_Text>().text = "high";
            shadowofftextLINE.gameObject.SetActive(false);
            shadowlowtextLINE.gameObject.SetActive(true);
            shadowhightextLINE.gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Shadows") == 2) {
            QualitySettings.shadowCascades = 4;
            QualitySettings.shadowDistance = 500;
            //shadowofftext.GetComponent<TMP_Text>().text = "off";
            //shadowlowtext.GetComponent<TMP_Text>().text = "low";
            //shadowhightext.GetComponent<TMP_Text>().text = "HIGH";
            shadowofftextLINE.gameObject.SetActive(false);
            shadowlowtextLINE.gameObject.SetActive(false);
            shadowhightextLINE.gameObject.SetActive(true);
        }

        // check vsync
        if (QualitySettings.vSyncCount == 0) {
            vsynctext.GetComponent<TMP_Text>().text = "off";
        }
        else if (QualitySettings.vSyncCount == 1) {
            vsynctext.GetComponent<TMP_Text>().text = "on";
        }

        // check mouse inverse
        if (PlayerPrefs.GetInt("Inverted") == 0) {
            invertmousetext.GetComponent<TMP_Text>().text = "off";
        }
        else if (PlayerPrefs.GetInt("Inverted") == 1) {
            invertmousetext.GetComponent<TMP_Text>().text = "on";
        }

        // check motion blur
        if (PlayerPrefs.GetInt("MotionBlur") == 0) {
            motionblurtext.GetComponent<TMP_Text>().text = "off";
        }
        else if (PlayerPrefs.GetInt("MotionBlur") == 1) {
            motionblurtext.GetComponent<TMP_Text>().text = "on";
        }

        // check ambient occlusion
        if (PlayerPrefs.GetInt("AmbientOcclusion") == 0) {
            ambientocclusiontext.GetComponent<TMP_Text>().text = "off";
        }
        else if (PlayerPrefs.GetInt("AmbientOcclusion") == 1) {
            ambientocclusiontext.GetComponent<TMP_Text>().text = "on";
        }

        // check texture quality
        if (PlayerPrefs.GetInt("Textures") == 0) {
            QualitySettings.masterTextureLimit = 2;
            //texturelowtext.GetComponent<TMP_Text>().text = "LOW";
            //texturemedtext.GetComponent<TMP_Text>().text = "med";
            //texturehightext.GetComponent<TMP_Text>().text = "high";
            texturelowtextLINE.gameObject.SetActive(true);
            texturemedtextLINE.gameObject.SetActive(false);
            texturehightextLINE.gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Textures") == 1) {
            QualitySettings.masterTextureLimit = 1;
            //texturelowtext.GetComponent<TMP_Text>().text = "low";
            //texturemedtext.GetComponent<TMP_Text>().text = "MED";
            //texturehightext.GetComponent<TMP_Text>().text = "high";
            texturelowtextLINE.gameObject.SetActive(false);
            texturemedtextLINE.gameObject.SetActive(true);
            texturehightextLINE.gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Textures") == 2) {
            QualitySettings.masterTextureLimit = 0;
            //texturelowtext.GetComponent<TMP_Text>().text = "low";
            //texturemedtext.GetComponent<TMP_Text>().text = "med";
            //texturehightext.GetComponent<TMP_Text>().text = "HIGH";
            texturelowtextLINE.gameObject.SetActive(false);
            texturemedtextLINE.gameObject.SetActive(false);
            texturehightextLINE.gameObject.SetActive(true);
        }
    }

    public void Update() {
        sliderValuePlayerSpeed = playerSpeedSlider.GetComponent<Slider>().value;
        sliderValueXSensitivity = sensitivityXSlider.GetComponent<Slider>().value;
        sliderValueYSensitivity = sensitivityYSlider.GetComponent<Slider>().value;
        sliderValueAgentSpeed = agentSpeedSlider.GetComponent<Slider>().value;

        keyvalue = Key.GetComponent<TMP_Text>().text;
        experimentIDvalue = ExperimentID.GetComponent<TMP_Text>().text;
        workervalue = Worker.GetComponent<TMP_Text>().text;
    }

    public void FullScreen() {
        Screen.fullScreen = !Screen.fullScreen;

        if (Screen.fullScreen == true) {
            fullscreentext.GetComponent<TMP_Text>().text = "on";
        }
        else if (Screen.fullScreen == false) {
            fullscreentext.GetComponent<TMP_Text>().text = "off";
        }
    }

    public void SpeedSlider() {
        PlayerPrefs.SetFloat("AgentSpeed", sliderValueAgentSpeed);
    }

    public void PlayerSpeedSlider()
    {
        PlayerPrefs.SetFloat("PlayerSpeed", sliderValuePlayerSpeed);
    }

    public void SensitivityXSlider() {
        PlayerPrefs.SetFloat("XSensitivity", sliderValueXSensitivity);
    }

    public void SensitivityYSlider() {
        PlayerPrefs.SetFloat("YSensitivity", sliderValueYSensitivity);
    }

    public void SensitivitySmoothing() {
        PlayerPrefs.SetFloat("MouseSmoothing", sliderValueSmoothing);
    }

    public void ExperimentIdDeselect()
    {
        PlayerPrefs.SetString("ExperimentID", experimentIDvalue);
    }

    public void WorkerDeselect()
    {
        PlayerPrefs.SetString("Worker", workervalue);
    }

    public void KeyDeselect()
    {
        PlayerPrefs.SetString("Key", keyvalue);
    }

    // the playerprefs variable that is checked to enable hud while in game
    public void  ShowHUD (){
		if(PlayerPrefs.GetInt("ShowHUD")==0){
			PlayerPrefs.SetInt("ShowHUD",1);
			showhudtext.GetComponent<TMP_Text>().text = "on";
		}
		else if(PlayerPrefs.GetInt("ShowHUD")==1){
			PlayerPrefs.SetInt("ShowHUD",0);
			showhudtext.GetComponent<TMP_Text>().text = "off";
		}
	}

	// show tool tips like: 'How to Play' control pop ups
	public void  ToolTips (){
		if(PlayerPrefs.GetInt("ToolTips")==0){
			PlayerPrefs.SetInt("ToolTips",1);
			tooltipstext.GetComponent<TMP_Text>().text = "on";
		}
		else if(PlayerPrefs.GetInt("ToolTips")==1){
			PlayerPrefs.SetInt("ToolTips",0);
			tooltipstext.GetComponent<TMP_Text>().text = "off";
		}
	}

	public void  NormalDifficulty (){
		difficultyhardcoretextLINE.gameObject.SetActive(false);
		difficultynormaltextLINE.gameObject.SetActive(true);
		PlayerPrefs.SetInt("NormalDifficulty",1);
		PlayerPrefs.SetInt("HardCoreDifficulty",0);
	}

	public void  HardcoreDifficulty (){
		difficultyhardcoretextLINE.gameObject.SetActive(true);
		difficultynormaltextLINE.gameObject.SetActive(false);
		PlayerPrefs.SetInt("NormalDifficulty",0);
		PlayerPrefs.SetInt("HardCoreDifficulty",1);
	}

	public void  ShadowsOff (){
		PlayerPrefs.SetInt("Shadows",0);
		QualitySettings.shadowCascades = 0;
		QualitySettings.shadowDistance = 0;
		//shadowofftext.GetComponent<TMP_Text>().text = "OFF";
		//shadowlowtext.GetComponent<TMP_Text>().text = "low";
		//shadowhightext.GetComponent<TMP_Text>().text = "high";
		shadowofftextLINE.gameObject.SetActive(true);
		shadowlowtextLINE.gameObject.SetActive(false);
		shadowhightextLINE.gameObject.SetActive(false);
	}

	public void  ShadowsLow (){
		PlayerPrefs.SetInt("Shadows",1);
		QualitySettings.shadowCascades = 2;
		QualitySettings.shadowDistance = 75;
		//shadowofftext.GetComponent<TMP_Text>().text = "off";
		//shadowlowtext.GetComponent<TMP_Text>().text = "LOW";
		//shadowhightext.GetComponent<TMP_Text>().text = "high";
		shadowofftextLINE.gameObject.SetActive(false);
		shadowlowtextLINE.gameObject.SetActive(true);
		shadowhightextLINE.gameObject.SetActive(false);
	}

	public void  ShadowsHigh (){
		PlayerPrefs.SetInt("Shadows",2);
		QualitySettings.shadowCascades = 4;
		QualitySettings.shadowDistance = 500;
		//shadowofftext.GetComponent<TMP_Text>().text = "off";
		//shadowlowtext.GetComponent<TMP_Text>().text = "low";
		//shadowhightext.GetComponent<TMP_Text>().text = "HIGH";
		shadowofftextLINE.gameObject.SetActive(false);
		shadowlowtextLINE.gameObject.SetActive(false);
		shadowhightextLINE.gameObject.SetActive(true);
	}

	public void  vsync (){
		if(QualitySettings.vSyncCount == 0){
			QualitySettings.vSyncCount = 1;
			vsynctext.GetComponent<TMP_Text>().text = "on";
		}
		else if(QualitySettings.vSyncCount == 1){
			QualitySettings.vSyncCount = 0;
			vsynctext.GetComponent<TMP_Text>().text = "off";
		}
	}

	public void  InvertMouse (){
		if(PlayerPrefs.GetInt("Inverted")==0){
			PlayerPrefs.SetInt("Inverted",1);
			invertmousetext.GetComponent<TMP_Text>().text = "on";
		}
		else if(PlayerPrefs.GetInt("Inverted")==1){
			PlayerPrefs.SetInt("Inverted",0);
			invertmousetext.GetComponent<TMP_Text>().text = "off";
		}
	}

	public void  MotionBlur (){
		if(PlayerPrefs.GetInt("MotionBlur")==0){
			PlayerPrefs.SetInt("MotionBlur",1);
			motionblurtext.GetComponent<TMP_Text>().text = "on";
		}
		else if(PlayerPrefs.GetInt("MotionBlur")==1){
			PlayerPrefs.SetInt("MotionBlur",0);
			motionblurtext.GetComponent<TMP_Text>().text = "off";
		}
	}

	public void  AmbientOcclusion (){
		if(PlayerPrefs.GetInt("AmbientOcclusion")==0){
			PlayerPrefs.SetInt("AmbientOcclusion",1);
			ambientocclusiontext.GetComponent<TMP_Text>().text = "on";
		}
		else if(PlayerPrefs.GetInt("AmbientOcclusion")==1){
			PlayerPrefs.SetInt("AmbientOcclusion",0);
			ambientocclusiontext.GetComponent<TMP_Text>().text = "off";
		}
	}

	public void  CameraEffects (){
		if(PlayerPrefs.GetInt("CameraEffects")==0){
			PlayerPrefs.SetInt("CameraEffects",1);
			cameraeffectstext.GetComponent<TMP_Text>().text = "on";
		}
		else if(PlayerPrefs.GetInt("CameraEffects")==1){
			PlayerPrefs.SetInt("CameraEffects",0);
			cameraeffectstext.GetComponent<TMP_Text>().text = "off";
		}
	}

	public void  TexturesLow (){
		PlayerPrefs.SetInt("Textures",0);
		QualitySettings.masterTextureLimit = 2;
		//texturelowtext.GetComponent<TMP_Text>().text = "LOW";
		//texturemedtext.GetComponent<TMP_Text>().text = "med";
		//texturehightext.GetComponent<TMP_Text>().text = "high";
		texturelowtextLINE.gameObject.SetActive(true);
		texturemedtextLINE.gameObject.SetActive(false);
		texturehightextLINE.gameObject.SetActive(false);
	}

	public void  TexturesMed (){
		PlayerPrefs.SetInt("Textures",1);
		QualitySettings.masterTextureLimit = 1;
		//texturelowtext.GetComponent<TMP_Text>().text = "low";
		//texturemedtext.GetComponent<TMP_Text>().text = "MED";
		//texturehightext.GetComponent<TMP_Text>().text = "high";
		texturelowtextLINE.gameObject.SetActive(false);
		texturemedtextLINE.gameObject.SetActive(true);
		texturehightextLINE.gameObject.SetActive(false);
	}

	public void  TexturesHigh (){
		PlayerPrefs.SetInt("Textures",2);
		QualitySettings.masterTextureLimit = 0;
		//texturelowtext.GetComponent<TMP_Text>().text = "low";
		//texturemedtext.GetComponent<TMP_Text>().text = "med";
		//texturehightext.GetComponent<TMP_Text>().text = "HIGH";
		texturelowtextLINE.gameObject.SetActive(false);
		texturemedtextLINE.gameObject.SetActive(false);
		texturehightextLINE.gameObject.SetActive(true);
	}
    public void IncreaseAgents()
    {
        agentCount += 1;
        agentLabel.GetComponent<TextMeshPro>().text = agentCount.ToString();
        if (agentCount == maxAgents)
        {
            increaseAgentButton.GetComponent<Button>().interactable = false;
        }
        decreaseAgentButton.GetComponent<Button>().interactable = true;
        PlayerPrefs.SetInt("AgentCount", agentCount);
    }
    public void DecreaseAgents()
    {
        agentCount -= 1;
        agentLabel.GetComponent<TextMeshPro>().text = agentCount.ToString();
        if (agentCount == minAgents)
        {
            decreaseAgentButton.GetComponent<Button>().interactable = false;
        }
        increaseAgentButton.GetComponent<Button>().interactable = true;
        PlayerPrefs.SetInt("AgentCount", agentCount);
    }

    public void unPause()
    {
        camera.GetComponent<CameraControl>().OpenMenu = false;
    }

}