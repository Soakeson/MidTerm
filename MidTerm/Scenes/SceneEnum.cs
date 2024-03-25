using System;

namespace Yew
{
    [Flags]
    public enum SceneContext 
    {
        MainMenu,
        Game,
        Options,
        Scores,
        Credits,
        Exit,
    }
}
