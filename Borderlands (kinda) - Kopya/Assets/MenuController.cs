using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public int playLevelId = 2;
    //public int tutorialLevelId = 3;
    public Animator tutorialAnim;

    public TeleporterStatus status;

    public GameObject particleEffects;
    public Transform effectsTransform;

    int defQualityLevel;

    public UnityEngine.UI.Text options;

    public GameObject menu;
    public GameObject optionsMenu;
    public GameObject mainScreen;
    public float transitionSpeed = 1;

	// Use this for initialization
	void Start () {

        defQualityLevel = QualitySettings.GetQualityLevel();
        QualitySettings.SetQualityLevel(5, true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Play()
    {
        CommenceLevelLoad();
        SceneManager.LoadSceneAsync(playLevelId);
    }

    public void Tutorial()
    {
        tutorialAnim.SetBool("isOpen", true);
    }

    public void TutorialBack()
    {
        tutorialAnim.SetBool("isOpen", false);
    }

    void CommenceLevelLoad()
    {
        status.OpenLamp();
        Instantiate(particleEffects, effectsTransform.position, effectsTransform.rotation);
        QualitySettings.SetQualityLevel(defQualityLevel, true);
    }

    GameObject from;
    GameObject to;

    public void Options()
    {
        from = menu;
        to = optionsMenu;
        StartCoroutine("Transition");
    }

    public void OptionsBack()
    {
        from = optionsMenu;
        to = menu;
        StartCoroutine("Transition");
    }

    public void Exit()
    {
        Application.Quit();
    }

    void OnLevelWasLoaded(int level)
    {
        QualitySettings.SetQualityLevel(defQualityLevel, true);
    }

    IEnumerator Transition ()
    {
        RectTransform fRect = mainScreen.GetComponent<RectTransform>();

        float fromStarty = fRect.localScale.y;
        float fromStartx = fRect.localScale.x;
        while (fRect.localScale.y > 0.0001f)
        {
            fRect.localScale = new Vector3(Mathf.Lerp(fRect.localScale.x, 0.1f, transitionSpeed * Time.deltaTime), Mathf.Lerp(fRect.localScale.y, 0, transitionSpeed * Time.deltaTime), fRect.localScale.z);
            yield return 0;
        }

        fRect.localScale = new Vector3(fromStartx, fromStarty, fRect.localScale.z);

        from.SetActive(false);
        to.SetActive(true);


    }
}
