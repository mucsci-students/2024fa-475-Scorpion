using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement p1Movement;
    [SerializeField] private PlayerMovement p2Movement;
    [SerializeField] private SwordSwing p1SwordSwing;
    [SerializeField] private SwordSwing p2SwordSwing;
    [SerializeField] private ArrowShooter p1ArrowShoot;
    [SerializeField] private ArrowShooter p2ArrowShoot;
    [SerializeField] private ShieldSummon p1ShieldSummon;
    [SerializeField] private ShieldSummon p2ShieldSummon;
    [SerializeField] private List<TextMeshProUGUI> texts;

    private List<string> playerPrefKeys = new List<string> {"up1", "up2", "down1", "down2", "right1", "right2", "left1", "left2", "sword1", "sword2", "bow1", "bow2", "shield1", "shield2"};
    
    private List<KeyCode> keys = new List<KeyCode> ();
    private KeyCode[] keyCodes;
    private int selectedUIKey = -1;

    void Start()
    {
        // get default / user set key binds
        foreach (string k in playerPrefKeys)
        {
            if (PlayerPrefs.HasKey(k))
            {
                string key = PlayerPrefs.GetString(k);
                KeyCode keyCode = (KeyCode) System.Enum.Parse(typeof(KeyCode), key);
                SetPlayerScriptKey(k, keyCode);
                keys.Add(keyCode);
            }
            else
            {
                if (k == "up1") {
                    PlayerPrefs.SetString(k, StringOfKeyCode(p1Movement.upButton));
                    keys.Add(p1Movement.upButton);
                } 
                else if (k == "up2") {
                    PlayerPrefs.SetString(k, StringOfKeyCode(p2Movement.upButton));
                    keys.Add(p2Movement.upButton);
                } 
                else if (k == "down1") {
                    PlayerPrefs.SetString(k, StringOfKeyCode(p1Movement.downButton));
                    keys.Add(p1Movement.downButton);
                } 
                else if (k == "down2") {
                    PlayerPrefs.SetString(k, StringOfKeyCode(p2Movement.downButton));
                    keys.Add(p2Movement.downButton);
                } 
                else if (k == "right1") {
                    PlayerPrefs.SetString(k, StringOfKeyCode(p1Movement.rightButton));
                    keys.Add(p1Movement.rightButton);
                } 
                else if (k == "right2") {
                    PlayerPrefs.SetString(k, StringOfKeyCode(p2Movement.rightButton));
                    keys.Add(p2Movement.rightButton);
                } 
                else if (k == "left1") {
                    PlayerPrefs.SetString(k, StringOfKeyCode(p1Movement.leftButton));
                    keys.Add(p1Movement.leftButton);
                } 
                else if (k == "left2") {
                    PlayerPrefs.SetString(k, StringOfKeyCode(p2Movement.leftButton));
                    keys.Add(p2Movement.leftButton);
                } 
                else if (k == "sword1") {
                    PlayerPrefs.SetString(k, StringOfKeyCode(p1SwordSwing.swingButton));
                    keys.Add(p1SwordSwing.swingButton);
                } 
                else if (k == "sword2") {
                    PlayerPrefs.SetString(k, StringOfKeyCode(p2SwordSwing.swingButton));
                    keys.Add(p2SwordSwing.swingButton);
                } 
                else if (k == "bow1") {
                    PlayerPrefs.SetString(k, StringOfKeyCode(p1ArrowShoot.shootButton));
                    keys.Add(p1ArrowShoot.shootButton);
                } 
                else if (k == "bow2") {
                    PlayerPrefs.SetString(k, StringOfKeyCode(p2ArrowShoot.shootButton));
                    keys.Add(p2ArrowShoot.shootButton);
                } 
                else if (k == "shield1") {
                    PlayerPrefs.SetString(k, StringOfKeyCode(p1ShieldSummon.shieldButton));
                    keys.Add(p1ShieldSummon.shieldButton);
                } 
                else if (k == "shield2") {
                    PlayerPrefs.SetString(k, StringOfKeyCode(p2ShieldSummon.shieldButton));
                    keys.Add(p2ShieldSummon.shieldButton);
                }
            }
        }

        // update UI to display key binds
        for (int i = 0; i < texts.Count; ++i)
        {
            texts[i].text = StringOfKeyCode(keys[i]);
        }

        keyCodes = (KeyCode[]) System.Enum.GetValues(typeof(KeyCode));
    }

    void Update()
    {
        if (selectedUIKey != -1)
        {
            foreach (KeyCode k in keyCodes)
            {
                if (Input.GetKey(k))
                {
                    // a key was pressed
                    if (keys.Contains(k))
                    {
                        // swap two keys
                        print("swapping two keys");
                    }
                    else
                    {
                        // substitute one key
                        print("substituting out one key");
                        keys[selectedUIKey] = k;
                        texts[selectedUIKey].text = StringOfKeyCode(k);
                        PlayerPrefs.SetString(playerPrefKeys[selectedUIKey], StringOfKeyCode(k));
                        SetPlayerScriptKey(playerPrefKeys[selectedUIKey], k);
                    }
                    break;
                }
            }
        }
    }

    private void SetPlayerScriptKey (string playerPrefKey, KeyCode newKey)
    {
        if (playerPrefKey == "up1") {
            p1Movement.upButton = newKey;
        } 
        else if (playerPrefKey == "up2") {
            p2Movement.upButton = newKey;
        } 
        else if (playerPrefKey == "down1") {
            p1Movement.downButton = newKey;
        } 
        else if (playerPrefKey == "down2") {
            p2Movement.downButton = newKey;
        } 
        else if (playerPrefKey == "right1") {
            p1Movement.rightButton = newKey;
        } 
        else if (playerPrefKey == "right2") {
            p2Movement.rightButton = newKey;
        } 
        else if (playerPrefKey == "left1") {
            p1Movement.leftButton = newKey;
        } 
        else if (playerPrefKey == "left2") {
            p2Movement.leftButton = newKey;
        } 
        else if (playerPrefKey == "sword1") {
            p1SwordSwing.swingButton = newKey;
        } 
        else if (playerPrefKey == "sword2") {
            p2SwordSwing.swingButton = newKey;
        } 
        else if (playerPrefKey == "bow1") {
            p1ArrowShoot.shootButton = newKey;
        } 
        else if (playerPrefKey == "bow2") {
            p2ArrowShoot.shootButton = newKey;
        } 
        else if (playerPrefKey == "shield1") {
            p1ShieldSummon.shieldButton = newKey;
        } 
        else if (playerPrefKey == "shield2") {
            p2ShieldSummon.shieldButton = newKey;
        }
    }

    public string StringOfKeyCode (KeyCode key)
    {
        switch (key)
        {
            case KeyCode.None: return "Not assigned";
            case KeyCode.Backspace: return "◀";
            case KeyCode.Delete: return "Delete";
            case KeyCode.Tab: return "Tab";
            case KeyCode.Clear: return "Clear";
            case KeyCode.Return: return "Return";
            case KeyCode.Pause: return "Pause";
            case KeyCode.Escape: return "Esc";
            case KeyCode.Space: return "Space";
            case KeyCode.Keypad0: return "0";
            case KeyCode.Keypad1: return "1";
            case KeyCode.Keypad2: return "2";
            case KeyCode.Keypad3: return "3";
            case KeyCode.Keypad4: return "4";
            case KeyCode.Keypad5: return "5";
            case KeyCode.Keypad6: return "6";
            case KeyCode.Keypad7: return "7";
            case KeyCode.Keypad8: return "8";
            case KeyCode.Keypad9: return "9";
            case KeyCode.KeypadPeriod: return ".";
            case KeyCode.KeypadDivide: return "/";
            case KeyCode.KeypadMultiply: return "*";
            case KeyCode.KeypadMinus: return "-";
            case KeyCode.KeypadPlus: return "+";
            case KeyCode.KeypadEnter: return "Enter";
            case KeyCode.KeypadEquals: return "=";
            case KeyCode.UpArrow: return "↑";
            case KeyCode.DownArrow: return "↓";
            case KeyCode.RightArrow: return "→";
            case KeyCode.LeftArrow: return "←";
            case KeyCode.Insert: return "Insert";
            case KeyCode.Home: return "Home";
            case KeyCode.End: return "End";
            case KeyCode.PageUp: return "Page Up";
            case KeyCode.PageDown: return "Page Down";
            case KeyCode.F1: return "F1";
            case KeyCode.F2: return "F2";
            case KeyCode.F3: return "F3";
            case KeyCode.F4: return "F4";
            case KeyCode.F5: return "F5";
            case KeyCode.F6: return "F6";
            case KeyCode.F7: return "F7";
            case KeyCode.F8: return "F8";
            case KeyCode.F9: return "F9";
            case KeyCode.F10: return "F10";
            case KeyCode.F11: return "F11";
            case KeyCode.F12: return "F12";
            case KeyCode.Alpha0: return "0";
            case KeyCode.Alpha1: return "1";
            case KeyCode.Alpha2: return "2";
            case KeyCode.Alpha3: return "3";
            case KeyCode.Alpha4: return "4";
            case KeyCode.Alpha5: return "5";
            case KeyCode.Alpha6: return "6";
            case KeyCode.Alpha7: return "7";
            case KeyCode.Alpha8: return "8";
            case KeyCode.Alpha9: return "9";
            case KeyCode.Exclaim: return "!";
            case KeyCode.DoubleQuote: return "\"";
            case KeyCode.Hash: return "#";
            case KeyCode.Dollar: return "$";
            case KeyCode.Percent: return "%";
            case KeyCode.Ampersand: return "&";
            case KeyCode.Quote: return "'";
            case KeyCode.LeftParen: return "(";
            case KeyCode.RightParen: return ")";
            case KeyCode.Asterisk: return "*";
            case KeyCode.Plus: return "+";
            case KeyCode.Comma: return ",";
            case KeyCode.Minus: return "-";
            case KeyCode.Period: return ".";
            case KeyCode.Slash: return "/";
            case KeyCode.Colon: return ":";
            case KeyCode.Semicolon: return ";";
            case KeyCode.Less: return "<";
            case KeyCode.Equals: return "=";
            case KeyCode.Greater: return ">";
            case KeyCode.Question: return "?";
            case KeyCode.At: return "@";
            case KeyCode.LeftBracket: return "[";
            case KeyCode.Backslash: return "\\";
            case KeyCode.RightBracket: return "]";
            case KeyCode.Caret: return "^";
            case KeyCode.Underscore: return "_";
            case KeyCode.BackQuote: return "`";
            case KeyCode.A: return "a";
            case KeyCode.B: return "b";
            case KeyCode.C: return "c";
            case KeyCode.D: return "d";
            case KeyCode.E: return "e";
            case KeyCode.F: return "f";
            case KeyCode.G: return "g";
            case KeyCode.H: return "h";
            case KeyCode.I: return "i";
            case KeyCode.J: return "j";
            case KeyCode.K: return "k";
            case KeyCode.L: return "l";
            case KeyCode.M: return "m";
            case KeyCode.N: return "n";
            case KeyCode.O: return "o";
            case KeyCode.P: return "p";
            case KeyCode.Q: return "q";
            case KeyCode.R: return "r";
            case KeyCode.S: return "s";
            case KeyCode.T: return "t";
            case KeyCode.U: return "u";
            case KeyCode.V: return "v";
            case KeyCode.W: return "w";
            case KeyCode.X: return "x";
            case KeyCode.Y: return "y";
            case KeyCode.Z: return "z";
            case KeyCode.LeftCurlyBracket: return "{";
            case KeyCode.Pipe: return "|";
            case KeyCode.RightCurlyBracket: return "}";
            case KeyCode.Tilde: return "~";
            case KeyCode.Numlock: return "Numlock";
            case KeyCode.CapsLock: return "Caps Lock";
            case KeyCode.ScrollLock: return "Scroll Lock";
            case KeyCode.RightShift: return "Shift";
            case KeyCode.LeftShift: return "Shift";
            case KeyCode.RightControl: return "Ctrl";
            case KeyCode.LeftControl: return "Crtl";
            case KeyCode.RightAlt: return "Alt";
            case KeyCode.LeftAlt: return "Alt";
            case KeyCode.LeftMeta: return "Meta";
            case KeyCode.RightMeta: return "Meta";
            case KeyCode.Help: return "Help";
            case KeyCode.Print: return "Print";
            case KeyCode.Break: return "Break";
            case KeyCode.Menu: return "Menu";
            default: return "Unknown";
        }
    }
}
