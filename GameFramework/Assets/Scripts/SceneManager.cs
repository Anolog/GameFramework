using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    //Variables for the scene manager. Seralized properties are for exposure to editor
    public static SceneManager Instance;

    [SerializeField]
    private GameObject m_LoadingScreen;
    private GameObject m_CurrentCanvas;

    [SerializeField]
    private AnimationCurve m_FadeInterpolation;

    [SerializeField]
    private CanvasGroup m_LoadingScreenCanvasGroup;

    [SerializeField]
    private CanvasGroup m_BlackCanvas;
    private string[] m_SceneNames;

    [SerializeField]
    private string m_FirstSceneToLoad;

    [SerializeField]
    private float m_InitialDuration;

    [SerializeField]
    private float m_InterpolationDuration;
    private float m_LoadingPercent;
    private bool m_IsLoading;

    // Use this for initialization
    void Start ()
    {
        m_IsLoading = false;
        m_LoadingPercent = 0.0f;
        
        //If it isn't initialized then instantiate the object and the loading/transition info for it.
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;

            if (m_LoadingScreen == null)
            {
                m_CurrentCanvas = Instantiate(m_LoadingScreen);
                m_CurrentCanvas.transform.SetParent(transform);
                m_CurrentCanvas.transform.position = Vector3.zero;
                m_LoadingScreenCanvasGroup = m_CurrentCanvas.GetComponent<CanvasGroup>();
                m_CurrentCanvas.GetComponent<Canvas>().sortingOrder = 100;
                DontDestroyOnLoad(m_LoadingScreen);
            }

            m_LoadingScreenCanvasGroup.GetComponent<Canvas>().sortingOrder = 99;
            m_InterpolationDuration = 2.0f;
            m_InitialDuration = 2.5f;

            //Start coroutine for loading scene

        }
        
        else
        {
            Debug.LogError("A SceneManager already exists in the current scene.");
            Destroy(this);
        }

    }

    //Set the canvas to be enabled or disabled
    public void SetCurrentCanvasActive(bool aActive)
    {
        m_BlackCanvas.gameObject.SetActive(aActive);

        if (m_LoadingScreenCanvasGroup != null)
        {
            m_CurrentCanvas.gameObject.SetActive(aActive);
            m_LoadingScreenCanvasGroup.gameObject.SetActive(aActive);
        }
    }

    //Takes a start value and an end value to interpolate between, and a fade target to change for it's alpha value
    public IEnumerator PlayFadeAnimation(float aStartValue, float aEndValue, CanvasGroup aFadeTarget)
    {
        float helperValue = 0.0f;

        m_IsLoading = true;

        while (helperValue <= 1.0f)
        {
            //if it is the starting scene, use initial values rather than duration
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0)
            {
                helperValue += Time.deltaTime / m_InitialDuration;
                m_LoadingPercent = helperValue / m_InitialDuration;
            }

            //if any other time loading
            else
            {
                helperValue += Time.deltaTime / m_InterpolationDuration;
                m_LoadingPercent = helperValue / m_InterpolationDuration;
            }

            //Set alpha to use the helper value to fade
            aFadeTarget.alpha = Mathf.Lerp(aStartValue, aEndValue, helperValue);
            yield return null;
        }

        m_IsLoading = false;
    }

    //This will be called in most cases. aDirection should be true for fade to black, and false for fade to black (Life it seems to fade away...)
    public IEnumerable PlayFadeAnimation(bool aDirection, CanvasGroup aFadeTarget)
    {
        if (aDirection == true)
        {
            yield return Instance.StartCoroutine(PlayFadeAnimation(0.0f, 1.0f, aFadeTarget));
        }

        else
        {
            yield return Instance.StartCoroutine(PlayFadeAnimation(1.0f, 0.0f, aFadeTarget));
        }
    }

	public IEnumerable LoadNextLevelWithFade()
    {
        //yield return StartCoroutine(LoadLevelWithFade(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1));
    }

    public IEnumerable LoadLevelWithFade(string aSceneName)
    {
        //This is temporary cause it is now accepting it for some reason
        object[] temp = { true, m_BlackCanvas };
        object[] temp2 = { false, m_BlackCanvas };


        SetCurrentCanvasActive(true);

        //For some reason it doesn't like this line...?
        //TODO: fix this vvvvv
        //yield return Instance.StartCoroutine("PlayFadeAnimation(true, m_BlackCanvas)");

        yield return Instance.StartCoroutine("PlayFadeAnimation", temp);

        var async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(aSceneName);
        //Waiting for the load to finish
        yield return async;
        yield return Instance.StartCoroutine("PlayFadeAnimation", temp2);
        //yield return Instance.StartCoroutine("PlayFadeAnimation(false, m_BlackCanvas)");

        SetCurrentCanvasActive(false);
    }
}
