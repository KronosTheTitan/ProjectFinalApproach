using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
class Settings
{
    private static Settings _instance = new Settings();

    // Private constructor om te voorkomen dat anderen een instantie kunnen aanmaken.
    private Settings() { }

    // Via een static read-only property kan de instantie benaderd worden.
    public static Settings Instance
    {
        get
        {
            return _instance;
        }
    }

    public bool vsyncEnabled = false;

    public void ToggleFullscreen()
    {
        //Game.main.SetViewport();
    }
    public void ToggleVsync()
    {
        if (vsyncEnabled)
            Game.main.SetVSync(true);
        else
            Game.main.SetVSync(false);
    }
}
