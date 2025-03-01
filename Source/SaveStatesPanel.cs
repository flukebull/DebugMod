﻿using UnityEngine;
using InControl;
using System.Collections.Generic;

namespace DebugMod
{
    public static class SaveStatesPanel
    {
        private static CanvasPanel statePanel;

        public static void BuildMenu(GameObject canvas)
        {
            statePanel = new CanvasPanel(
                canvas,
                GUIController.Instance.images["BlankVertical"],
                new Vector2(720f, 40f),
                Vector2.zero,
                new Rect(
                    0f,
                    0f,
                    GUIController.Instance.images["BlankVertical"].width,
                    GUIController.Instance.images["BlankVertical"].height
                )
            );
            statePanel.AddText("CurrentFolder", "Page: "+(SaveStateManager.currentStateFolder + 1).ToString(), new Vector2(8,0), Vector2.zero, GUIController.Instance.arial, 15);
            statePanel.AddText("Mode", "mode: ", new Vector2(8, 20), Vector2.zero, GUIController.Instance.arial, 15);
            statePanel.AddText("currentmode", "-", new Vector2(60, 20), Vector2.zero, GUIController.Instance.arial, 15);
            
            for (int i = 0; i < SaveStateManager.maxSaveStates; i++) { 

                //Labels
                statePanel.AddText("Slot " + i, i.ToString(), new Vector2(10, i * 20 + 40), Vector2.zero, GUIController.Instance.arial, 15);
                
                //Values
                statePanel.AddText(i.ToString(), "", new Vector2(50, i * 20 + 40), Vector2.zero, GUIController.Instance.arial, 15);
            }
            /*
            statePanel.AddText("Slot1", "1", new Vector2(10f, 40f), Vector2.zero, GUIController.Instance.arial, 15);
            statePanel.AddText("Slot2", "2", new Vector2(10f, 60f), Vector2.zero, GUIController.Instance.arial, 15);
            statePanel.AddText("Slot3", "3", new Vector2(10f, 80f), Vector2.zero, GUIController.Instance.arial, 15);
            statePanel.AddText("Slot4", "4", new Vector2(10f, 100f), Vector2.zero, GUIController.Instance.arial, 15);
            statePanel.AddText("Slot5", "5", new Vector2(10f, 120f), Vector2.zero, GUIController.Instance.arial, 15);
            statePanel.AddText("Slot6", "6", new Vector2(10f, 140f), Vector2.zero, GUIController.Instance.arial, 15);
            statePanel.AddText("Slot7", "7", new Vector2(10f, 160f), Vector2.zero, GUIController.Instance.arial, 15);
            statePanel.AddText("Slot8", "8", new Vector2(10f, 180f), Vector2.zero, GUIController.Instance.arial, 15);
            statePanel.AddText("Slot9", "9", new Vector2(10f, 200f), Vector2.zero, GUIController.Instance.arial, 15);

            //Values
            statePanel.AddText("0", "", new Vector2(50f, 20f), Vector2.zero, GUIController.Instance.arial, 15);
            statePanel.AddText("1", "", new Vector2(50f, 40f), Vector2.zero, GUIController.Instance.arial, 15);
            statePanel.AddText("2", "", new Vector2(50f, 60f), Vector2.zero, GUIController.Instance.arial, 15);
            statePanel.AddText("3", "", new Vector2(50f, 80f), Vector2.zero, GUIController.Instance.arial, 15);
            statePanel.AddText("4", "", new Vector2(50f, 100f), Vector2.zero, GUIController.Instance.arial, 15);
            statePanel.AddText("5", "", new Vector2(50f, 120f), Vector2.zero, GUIController.Instance.arial, 15);
            statePanel.AddText("6", "", new Vector2(50f, 140f), Vector2.zero, GUIController.Instance.arial, 15);
            statePanel.AddText("7", "", new Vector2(50f, 160f), Vector2.zero, GUIController.Instance.arial, 15);
            statePanel.AddText("8", "", new Vector2(50f, 180f), Vector2.zero, GUIController.Instance.arial, 15);
            statePanel.AddText("9", "", new Vector2(50f, 200f), Vector2.zero, GUIController.Instance.arial, 15);
            */
        }
        public static void Update()
        {
            if (statePanel == null)
            {
                return;
            }

            if (DebugMod.GM.IsNonGameplayScene())
            {
                if (statePanel.active)
                {
                    statePanel.SetActive(false, true);
                }

                return;
            }

            if (DebugMod.settings.SaveStatePanelVisible && !statePanel.active)
            {
                statePanel.SetActive(true, false);
            }
            else if (!DebugMod.settings.SaveStatePanelVisible && statePanel.active)
            {
                statePanel.SetActive(false, true);
            }

            if (statePanel.active)
            {
                statePanel.GetText("currentmode").UpdateText(SaveStateManager.currentStateOperation);
                statePanel.GetText("CurrentFolder").UpdateText("Page: " + (SaveStateManager.currentStateFolder + 1).ToString()+"/"+SaveStateManager.savePages);
                for (int i = 0; i < SaveStateManager.maxSaveStates; i++)
                {
                    statePanel.GetText(i.ToString()).UpdateText("open");
                }

                foreach (KeyValuePair<int, string[]> entry in SaveStateManager.GetSaveStatesInfo())
                {
                    statePanel.GetText(entry.Key.ToString()).UpdateText(string.Format("{0} - {1}", entry.Value[0], entry.Value[1]));
                }
            }
        }

        private static string GetStringForBool(bool b)
        {
            return b ? "✓" : "X";
        }
    }
}
