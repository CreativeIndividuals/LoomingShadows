using System;

public interface ICutsceneAction
{
    void Execute(Action onComplete);
}

