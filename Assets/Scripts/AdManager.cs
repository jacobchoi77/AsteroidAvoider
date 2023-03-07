using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener{
    public static AdManager Instance;

    [SerializeField]
    bool testMode = true;

    private GameOverHandler gameOverHandler;

#if UNITY_ANDROID
    private const string _gameId = "5194199";
#elif UNITY_IOS
    private const string _gameId = "5194198";
#endif

#if UNITY_IOS
    private const string _adUnitId = "RewardedVideo";
#elif UNITY_ANDROID
    private const string _adUnitId = "RewardedVideo";
#endif

    private void Awake(){
        if (Instance != null && Instance != this){
            Destroy(gameObject);
        }
        else{
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start(){
        if (Advertisement.isSupported){
            Debug.Log(Application.platform + " supported by Advertisement");
        }

        Advertisement.Initialize(_gameId, testMode, this);
    }

    public void OnInitializationComplete(){
        Debug.Log("Unity Ads initialization complete");
        Advertisement.Load(_adUnitId, this);
    }

    public void ShowAd(GameOverHandler gameOverHandler){
        this.gameOverHandler = gameOverHandler;
        Advertisement.Show(_adUnitId, this);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message){
        Debug.Log($"Unity initialization Failed: {error} - {message}");
    }

    public void OnUnityAdsAdLoaded(string placementId){
        Debug.Log($"Ad loaded: {placementId}");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message){
        Debug.Log($"Ad Failed to load: {placementId}: {error} - {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message){
        Debug.Log($"Ad Failed to show: {placementId}: {error} - {message}");
    }

    public void OnUnityAdsShowStart(string placementId){
    }

    public void OnUnityAdsShowClick(string placementId){
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState){
        switch (showCompletionState){
            case UnityAdsShowCompletionState.COMPLETED:
                gameOverHandler.ContinueGame();
                break;
            case UnityAdsShowCompletionState.SKIPPED:
                //add was skipped
                break;
            case UnityAdsShowCompletionState.UNKNOWN:
                Debug.LogWarning("Ad Failed");
                break;
        }
    }
}