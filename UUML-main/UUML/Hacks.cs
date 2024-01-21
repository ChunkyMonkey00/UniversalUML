using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using UnityEngine;
using static GameObjectManager;
using System.Windows.Forms;
using static UnityEngine.GraphicsBuffer;
using System.Runtime.CompilerServices;
using Object = UnityEngine.Object;
using System.Runtime.InteropServices;
using UnityEngine.UIElements;
using Mono.CSharp;
using Enum = System.Enum;
using Event = UnityEngine.Event;
using Color = UnityEngine.Color;

namespace UUML
{

    internal class Hacks : MonoBehaviour
    {
        private static GUIStyle Style = null;
        private readonly SettingsData settingsData = Settings.Instance.settingsData;

        public static Hacks Instance
        {
            get
            {
                if (Hacks.instance == null)
                {
                    Hacks.instance = new Hacks();
                }
                return Hacks.instance;
            }
        }

        public void OnGUI()
        {
            if (!Settings.Instance.b_isMenuOpen && Event.current.type != EventType.Repaint)
                return;

            UI.Reset();

            Color darkBackground = new Color(23f / 255f, 23f / 255f, 23f / 255f, 1f);

            GUI.backgroundColor = darkBackground;
            GUI.contentColor = Color.white;

            Style = new GUIStyle(GUI.skin.label);
            Style.normal.textColor = Color.white;
            Style.fontStyle = FontStyle.Bold;

            GUI.color = settingsData.c_Theme;

            string Watermark = "Universal UML";
            Watermark += " | v" + settingsData.version;
            if (!Settings.Instance.b_isMenuOpen) Watermark += " | Press INSERT";

            Render.String(Style, 10f, 5f, 150f, Settings.TEXT_HEIGHT, Watermark, GUI.color);

            if (Settings.Instance.b_isMenuOpen)
            {
                Settings.Instance.windowRect = GUILayout.Window(0, Settings.Instance.windowRect, new GUI.WindowFunction(MenuContent), "Project Apparatus", Array.Empty<GUILayoutOption>());
            }
        }

        public static float Round(float value, int digits)
        {
            float mult = Mathf.Pow(10.0f, (float)digits);
            return Mathf.Round(value * mult) / mult;
        }

        private void MenuContent(int windowID)
        {

            GUILayout.BeginHorizontal();
            UI.Tab("Start", ref UI.nTab, UI.Tabs.Start);
            UI.Tab("Hacks", ref UI.nTab, UI.Tabs.Self);
            GUILayout.EndHorizontal();


            UI.TabContents("Start", UI.Tabs.Start, () =>
            {
                GUILayout.Label($"Welcome to UML! v{settingsData.version}!\n\n" +
                                $"If you have suggestions, please create a pull request in the repo or reply to the UC thread.\n" +
                                $"If you find bugs, please provide some steps on how to reproduce the problem and create an issue or pull request in the repo or reply to the UC thread");
                GUILayout.Space(20f);
                GUILayout.Label($"Changelog {settingsData.version}", new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold });
                scrollPos = GUILayout.BeginScrollView(scrollPos, GUILayout.Height(300f));
                GUILayout.TextArea(Settings.Changelog.changes.ToString(), GUILayout.ExpandHeight(true));
                GUILayout.EndScrollView();
                GUILayout.Space(20f);
                GUILayout.Label($"Credits", new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold });
                GUILayout.Label(Settings.Credits.credits.ToString());
            });



            UI.TabContents("Hacks", UI.Tabs.Self, () =>
            {
                UI.Checkbox(ref settingsData.b_GodMode, "Hack Placeholder", "Placeholder tooltip");
            });

        }

        private Vector2 scrollPos;
        private static Hacks instance;
    }
}
