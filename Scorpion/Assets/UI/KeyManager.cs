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
                KeyCode keyCode = (KeyCode) System.Enum.Parse(typeof (KeyCode), key);
                SetPlayerScriptKey (k, keyCode);
                keys.Add (keyCode);
            }
            else
            {
                if (k == "up1") {
                    PlayerPrefs.SetString(k, p1Movement.upButton.ToString ());
                    keys.Add(p1Movement.upButton);
                } 
                else if (k == "up2") {
                    PlayerPrefs.SetString(k, p2Movement.upButton.ToString ());
                    keys.Add(p2Movement.upButton);
                } 
                else if (k == "down1") {
                    PlayerPrefs.SetString(k, p1Movement.downButton.ToString ());
                    keys.Add(p1Movement.downButton);
                } 
                else if (k == "down2") {
                    PlayerPrefs.SetString(k, p2Movement.downButton.ToString ());
                    keys.Add(p2Movement.downButton);
                } 
                else if (k == "right1") {
                    PlayerPrefs.SetString(k, p1Movement.rightButton.ToString ());
                    keys.Add(p1Movement.rightButton);
                } 
                else if (k == "right2") {
                    PlayerPrefs.SetString(k, p2Movement.rightButton.ToString ());
                    keys.Add(p2Movement.rightButton);
                } 
                else if (k == "left1") {
                    PlayerPrefs.SetString(k, p1Movement.leftButton.ToString ());
                    keys.Add(p1Movement.leftButton);
                } 
                else if (k == "left2") {
                    PlayerPrefs.SetString(k, p2Movement.leftButton.ToString ());
                    keys.Add(p2Movement.leftButton);
                } 
                else if (k == "sword1") {
                    PlayerPrefs.SetString(k, p1SwordSwing.swingButton.ToString ());
                    keys.Add(p1SwordSwing.swingButton);
                } 
                else if (k == "sword2") {
                    PlayerPrefs.SetString(k, p2SwordSwing.swingButton.ToString ());
                    keys.Add(p2SwordSwing.swingButton);
                } 
                else if (k == "bow1") {
                    PlayerPrefs.SetString(k, p1ArrowShoot.shootButton.ToString ());
                    keys.Add(p1ArrowShoot.shootButton);
                } 
                else if (k == "bow2") {
                    PlayerPrefs.SetString(k, p2ArrowShoot.shootButton.ToString ());
                    keys.Add(p2ArrowShoot.shootButton);
                } 
                else if (k == "shield1") {
                    PlayerPrefs.SetString(k, p1ShieldSummon.shieldButton.ToString ());
                    keys.Add(p1ShieldSummon.shieldButton);
                } 
                else if (k == "shield2") {
                    PlayerPrefs.SetString(k, p2ShieldSummon.shieldButton.ToString ());
                    keys.Add(p2ShieldSummon.shieldButton);
                }
            }
        }

        // update UI to display key binds
        for (int i = 0; i < texts.Count; ++i)
        {
            texts[i].text = keys[i].ToString ();
        }

        keyCodes = (KeyCode[]) System.Enum.GetValues (typeof (KeyCode));
    }

    void Update()
    {
        if (selectedUIKey != -1)
        {
            foreach (KeyCode k in keyCodes)
            {
                if (Input.GetKey (k))
                {
                    // a key was pressed
                    if (keys.Contains (k))
                    {
                        // swap two keys
                        print ("swapping two keys");

                    }
                    else
                    {
                        // substitute one key
                        print ("substituting out one key");
                        keys[selectedUIKey] = k;
                        texts[selectedUIKey].text = k.ToString ();
                        PlayerPrefs.SetString (playerPrefKeys[selectedUIKey], k.ToString ());
                        SetPlayerScriptKey (playerPrefKeys[selectedUIKey], k);
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
}
