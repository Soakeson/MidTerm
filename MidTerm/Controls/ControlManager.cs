using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using CS5410.Input;

namespace Yew
{
    public class ControlManager
    {
        private Dictionary<SceneContext, Dictionary<ControlContext, Control>> controls {get; set;} =
            new Dictionary<SceneContext, Dictionary<ControlContext, Control>>();
        private Dictionary<Keys, CommandEntry> delegates {get; set;} =
            new Dictionary<Keys, CommandEntry>();
        private DataManager dataManager;
        private KeyboardState statePrevious;

        /// <summary>
        /// Used to keep track of the details associated with a command
        /// </summary>
        private struct CommandEntry
        {
            public CommandEntry(bool keyPressOnly, IInputDevice.CommandDelegate callback)
            {
                this.keyPressOnly = keyPressOnly;
                this.callback = callback;
            }

            public bool keyPressOnly;
            public IInputDevice.CommandDelegate callback;
        }

        public ControlManager(DataManager dm) 
        {
            this.dataManager = dm;
            controls = dm.Load<Dictionary<SceneContext, Dictionary<ControlContext, Control>>>(controls);
        }

        public void RegisterControl(SceneContext sc, ControlContext cc, Keys key, bool keyPressOnly, IInputDevice.CommandDelegate d)
        {
            RegisterScene(sc);
            // If the control hasn't been loaded register it
            if (!controls[sc].ContainsKey(cc))
            {
                controls[sc].Add(cc, new Control(sc, cc, key));
            }
            Control con = controls[sc][cc];
            // Loaded control will override the register so it will only be registered if it wasn't able to load
            delegates.Add(con.key, new CommandEntry(keyPressOnly, d));
        }

        private void RegisterScene(SceneContext sc)
        {
            // If Scene hasn't been registered or wasn't loaded
            if (!controls.ContainsKey(sc))
            {
                controls.Add(sc, new Dictionary<ControlContext, Control>());
            }
        }
        
        /// <summary>
        /// Changes the key used for a registered control and changes what key references the delegate associated with it.
        /// <summary>
        public void ChangeKey(SceneContext sc, ControlContext cc, Keys key)
        {
            Keys old = controls[sc][cc].key;
            controls[sc][cc].key = key;
            CommandEntry ce = delegates[old];
            delegates.Remove(key);
            delegates.Add(key, ce);
            SaveKeys();
        }

        /// <summary>
        /// Returns a key based on the scene and the control context provided.
        /// <summary>
        public Keys GetKey(SceneContext sc, ControlContext cc)
        {
            return controls[sc][cc].key;
        }

        /// <summary>
        /// Saves the registered controls to file.
        /// <summary>
        private void SaveKeys()
        {
            dataManager.Save<Dictionary<SceneContext, Dictionary<ControlContext, Control>>>(controls);
        }

        /// <summary>
        /// Checks to see if a key was newly pressed.
        /// </summary>
        private bool KeyPressed(Keys key)
        {
            return (Keyboard.GetState().IsKeyDown(key) && !statePrevious.IsKeyDown(key));
        }
    }

}
