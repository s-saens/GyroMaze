using UnityEngine;

public class GameCompleteController : MonoBehaviour
{
    [SerializeField] private string completeViewIndex = "3";
    [SerializeField] private Event endPointCollisionEvent;
    [SerializeField] private Event viewToggleEvent;

    private void OnEnable()
    {
        endPointCollisionEvent.callback += GameComplete;
    }
    private void OnDisable()
    {
        endPointCollisionEvent.callback -= GameComplete;
    }


    private void GameComplete(string f)
    {
        if(UserData.databaseUser.stage - 1 == GameData.stageIndex.value)
        {
            UserData.databaseUser.SetStage(UserData.databaseUser.stage + 1);
        }
        PlayerPrefs.DeleteKey(KeyData.LAST_POSITION);
        viewToggleEvent.callback?.Invoke(completeViewIndex);
    }
}