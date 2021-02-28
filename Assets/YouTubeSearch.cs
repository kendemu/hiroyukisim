using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.IO;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Question {
    public string title;
    public string date;
    public string url;
}

public class YouTubeSearch : MonoBehaviour
{
    const string baseURL = "https://www.googleapis.com/youtube/v3";
    public string apiKey = "";
    private string pageToken = "";
    public string keywordPlaceholder = "ひろゆき+";
    public string keywords = "360+VR";
    public GameObject uiResultPrefab;
    public GameObject ListView;

    List<Question> questions = new List<Question>();
    List<GameObject> uiResults = new List<GameObject>();

    //List<VideoItem> videoItems = new List<VideoItem>();

    private int rowIndex = 0;
    private int videoIndex = 0;

    [SerializeField] private WebGLNativeInputField inputField;
    //[SerializeField] private InputField inputField;

    private bool m_GazeOver;
    
    void Start()
    {
    }

    IEnumerator QuerySearchList (string keywords)
    {

        // Pull down the JSON from YouTube
        string query = "";

        if (pageToken == "")
        {
            query = baseURL + "/search?part=snippet&maxResults=12&order=relevance&q=" + keywords + "&type=video&videoDefinition=high&key=" + apiKey;
        }
        else
        {
            query = baseURL + "/search?pageToken=" + pageToken + "&part=snippet&maxResults=12&order=relevance&q=" + keywords + "&type=video&videoDefinition=high&key=" + apiKey;
        }

        WWW w = new WWW(query);
        yield return w;

        ExtractSearchList(w.text);
    }

    void ExtractSearchList (string json)
    {
        print(json);

        // Create a JSON object from the text stream
        JSONObject jo = new JSONObject(json);

        pageToken = jo.list[2].str;

        // Go through the list of objects in our array
        foreach (JSONObject item in jo.list)
        {
            if (item.type == JSONObject.Type.ARRAY)
            {
                for (int i = 0; i < item.list.Count; i++)
                {
                    JSONObject videoInfo = item.list[i];

                    // Thumbnail
                    Debug.Log(videoInfo.list[3].list[4].list[2].list[0].str);


                    Question question = new Question();

                    question.title = videoInfo.list[3].list[2].str;
                    question.date = videoInfo.list[3].list[0].str.Replace("-", "/").Split('T')[0];
                    question.url = videoInfo.list[2].list[1].str;
                    questions.Add(question);

                    // Title
                    Debug.Log(videoInfo.list[3].list[2].str);
                    // Date
                    Debug.Log(videoInfo.list[3].list[0].str);
                    // URL
                    Debug.Log(videoInfo.list[2].list[1].str);
                    generateUIResult(question);
                }
            }
        }

        videoIndex += 12;
    }


    public void generateUIResult(Question question) {
        GameObject uiClone = Instantiate(uiResultPrefab, ListView.transform);
        uiClone.transform.SetParent(ListView.transform);
        uiClone.transform.Find("Title").gameObject.GetComponent<Text>().text = question.title;
        uiClone.transform.Find("Date").gameObject.GetComponent<Text>().text = question.date;
        uiClone.GetComponentInChildren<OpenVideo>().url = question.url;
        uiResults.Add(uiClone);
    }
   
    public void KeywordsEntered ()
    {
        //if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) {
            keywords = inputField.text;
            keywords = keywords.Replace(" ", "+");
            keywords = keywordPlaceholder + keywords;

            videoIndex = 0;
            rowIndex = 0;
            pageToken = "";

        questions = new List<Question>();

        for (int i = uiResults.Count - 1; i >= 0; i--) {
            Destroy(uiResults[i]);
        }
            /*
            for (int i = videoItems.Count - 1; i >= 0; i--)
            {
                Destroy(videoItems[i].gameObject);
            }

            videoItems.Clear();

            HideSearchBar();
            */

            StartCoroutine(QuerySearchList(keywords));
        //}
        
    }

    /*
    public void ReachedScrollbarEndpoint ()
    {
        StartCoroutine(QuerySearchList(keywords));
    }

    public void ShowSearchBar ()
    {
        Hashtable ht = new Hashtable();
        ht.Add("from", canvasGroup.alpha);
        ht.Add("to", 1f);
        ht.Add("time", .3f);
        ht.Add("onupdate", "TweenOpacity");

        iTween.ValueTo(gameObject, ht);

        inputField.Select();
        inputField.ActivateInputField();
    }

    public void HideSearchBar ()
    {
        Hashtable ht = new Hashtable();
        ht.Add("from", canvasGroup.alpha);
        ht.Add("to", 0f);
        ht.Add("time", .3f);
        ht.Add("onupdate", "TweenOpacity");

        iTween.ValueTo(gameObject, ht);
    }

    void TweenOpacity (float val)
    {
        canvasGroup.alpha = val;
        titleBarCanvasGroup.alpha = 1f - val;
    }

    */
}