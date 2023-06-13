using System;

public class UserAuthorizationData
{
    public string UserName { get; private set; }

    public event Action OnUserAuthorificated;

    public void SetUserName(string name)
    {
        UserName = name;

        if (!string.IsNullOrEmpty(UserName))
            OnUserAuthorificated?.Invoke();
    }
}