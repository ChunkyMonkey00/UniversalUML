using System;
using System.Diagnostics;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine.UI;
using System.Reflection.Emit;
using Debug = UnityEngine.Debug;
using System.ComponentModel;
using System.Windows.Forms;
using Screen = UnityEngine.Screen;

namespace UUML
{
    //Here is a template harmony patch :)
    /*
    [HarmonyPatch(typeof(ClassName), "MethodName")]
    public class PlayerControllerB_Start_Patch
    {
        private static bool Prefix()
        {
            return true;
        }
    }
    */
}