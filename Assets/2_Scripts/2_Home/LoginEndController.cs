using UnityEngine;

public class LoginEndController : MonoBehaviour
{
    [SerializeField] private Event loginEndEvent;

    [SerializeField] private Event viewToggleEvent;

    private void OnEnable()
    {
        loginEndEvent.callback += OnLoginEnd;
    }
    private void OnDisable()
    {
        loginEndEvent.callback -= OnLoginEnd;
    }

    private void OnLoginEnd(string param)
    {
        // 유저가 이미 있으면 Load해야 하고, 없으면 Save해야 함.
        FirebaseDBAccessor.GetValue<User>(
            FirebaseDBReference.user,
            (user) => { UserData.databaseUser.LoadFromDB(); },
            () => { UserData.databaseUser.SaveToDB(); }
        );
        viewToggleEvent.Invoke("1");
    }
}