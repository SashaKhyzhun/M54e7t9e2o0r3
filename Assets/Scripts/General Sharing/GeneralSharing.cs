using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;


public class GeneralSharing : MonoBehaviour
{
    IEnumerator SaveAndShare(Texture2D image, string text)
    {
        yield return new WaitForEndOfFrame();
        byte[] bytes = image.EncodeToPNG();
        string path = Application.persistentDataPath + "/MyImage.png";
        //string path = Application.dataPath + "/MyImage.png";
        File.WriteAllBytes(path, bytes);
		
        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
		
        intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
        intentObject.Call<AndroidJavaObject>("setType", "image/*");
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), "Stick!");
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TITLE"), "Stick!");
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), text);
		
        AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
        AndroidJavaClass fileClass = new AndroidJavaClass("java.io.File");
		
        AndroidJavaObject fileObject = new AndroidJavaObject("java.io.File", path);// Set Image Path Here
		
        AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("fromFile", fileObject);
		
        //			string uriPath =  uriObject.Call<string>("getPath");
        bool fileExist = fileObject.Call<bool>("exists");
        Debug.Log("File exist : " + fileExist);
        if (fileExist)
        {
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);

            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
            currentActivity.Call("startActivity", intentObject);
        }
    }

    public void OnShareTextWithImage(ShareInfo shI)
    {
        StartCoroutine(SaveAndShare(shI.image, shI.text));
    }
	
}