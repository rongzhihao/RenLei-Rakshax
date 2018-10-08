using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using LeoLuz;
using System.Runtime.InteropServices;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace LeoLuz
{
    [RequireComponent(typeof(Rect))]
    public class UIButtonToButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {

        [DllImport("user32.dll", EntryPoint = "keybd_event")]
        public static extern void Keybd_event(
        byte bvk,//虚拟键值 ESC键对应的是27
        byte bScan,//0
        int dwFlags,//0为按下，1按住，2释放
        int dwExtraInfo//0
        );

        public byte keyboard;

        [InputAxesListDropdown]
        public string ButtonName;
        //public KeyCode keycode;
        private bool pressed;
#if UNITY_EDITOR
        private bool OrderOfScriptChanged;
#endif
        public void OnDrawGizmosSelected()
        {
            Input.Autoconfigure();
#if UNITY_EDITOR
            if (!OrderOfScriptChanged)
            {
                // Get the name of the script we want to change it's execution order
                string scriptName = typeof(UIButtonToButton).Name;

                // Iterate through all scripts (Might be a better way to do this?)
                foreach (MonoScript monoScript in MonoImporter.GetAllRuntimeMonoScripts())
                {
                    // If found our script
                    if (monoScript.name == scriptName)
                    {
                        MonoImporter.SetExecutionOrder(monoScript, -2000);
                    }
                }
                OrderOfScriptChanged = true;
            }
#endif
        }

        /*public void Update()
        {
            if (pressed)
                Input.PressButtonMobile(ButtonName);
        }
        public void FixedUpdate()
        {
            if (pressed)
                Input.PressButtonMobile(ButtonName);
        }
        public virtual void OnPointerDown(PointerEventData data)
        {
            // print("UI PointerEventData Button Pressed" + Time.time);
            Input.PressButtonDownMobile(ButtonName);
            pressed = true;
        }

        public virtual void OnPointerUp(PointerEventData data)
        {
            pressed = false;
            Input.PressButtonUpMobile(ButtonName);
        }*/


        public void Update()
        {
            if (pressed)
                Input.PressButtonMobile(ButtonName);
                //Keybd_event(keyboard, 0, 1, 0);
        }
        public void FixedUpdate()
        {
            if (pressed)
                Input.PressButtonMobile(ButtonName);
                //Keybd_event(keyboard, 0, 1, 0);
        }
        public virtual void OnPointerDown(PointerEventData data)
        {
            // print("UI PointerEventData Button Pressed" + Time.time);
            Input.PressButtonDownMobile(ButtonName);
            //Keybd_event(keyboard, 0, 0, 0);
            pressed = true;
        }

        public virtual void OnPointerUp(PointerEventData data)
        {
            pressed = false;
            Input.PressButtonUpMobile(ButtonName);
            //Keybd_event(keyboard, 0, 2, 0);
        }

    }

}