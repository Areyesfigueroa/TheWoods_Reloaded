using UnityEngine;
using System.Collections;

/// <summary>
/// This script plays the audio files at the given events. 
/// Each function will be subscribed to their corresponding event. 
/// Triggers have already been placed, just excute audio on the functions given for the events
/// </summary>

namespace TheWoods.Audio
{
    public class AudioEventControl : MonoBehaviour
    {/*

    //Prevents audio override
    public bool playAudio = true;

    public static AudioEventControl Instance { get { return instance; } }
    static protected AudioEventControl instance;

    #region Engine Functions
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There is already an instance of InputManager, Deleting old and instantiating a new one");
            Destroy(AudioEventControl.Instance.gameObject);
            instance = null;
        }
        else
        {
            instance = this;
        }
    }

    void OnEnable()
    {
        AddSubscribers();

    }
    void OnDisable()
    {
        RemoveSubscribers();
    }

    #endregion

    #region Game Functions

    public void AddSubscribers()
    {
        //Player Events, Set Up Done, Waiting on Animations
        AudioEventSystem.onPlayerAmbience += this.PlayerAmbience;
        AudioEventSystem.onPlayerStep += this.PlayerStep;
        AudioEventSystem.onPlayerIdle += this.PlayerIdle;
        AudioEventSystem.onPlayerJump += this.PlayerJumping;
        AudioEventSystem.onPlayerFall += this.PlayerFall;
        AudioEventSystem.onPlayerDeath += this.PlayerDeath;
        AudioEventSystem.onPlayerVictory += this.PlayerVictory;
        AudioEventSystem.onPlayerAttack += this.PlayerAttack;
        AudioEventSystem.onPlayerInivisibleOn += this.PlayerInvisibleOn;
        AudioEventSystem.onPlayerInvisibleOff += this.PlayerInvisibleOff;
        AudioEventSystem.onPlayerVisibleWarning += this.PlayerVisibleWarning;
        AudioEventSystem.onPlayerLand += this.PlayerLand;

        //Player Enviroment 
        AudioEventSystem.onPlayerHide += this.PlayerHide;
        AudioEventSystem.onPlayerExitHide += this.PlayerExitHide;
        AudioEventSystem.onPlayerPowerUpPickUp += this.PlayerPowerUpPickUp;
        AudioEventSystem.onPlayerLadderClimb += this.PlayerLadderClimb;

        //Testing Enemy Events
        AudioEventSystem.onEnemyStep += this.EnemyStep;
        AudioEventSystem.onEnemyIdle += this.EnemyIdle;
        AudioEventSystem.onEnemyEscape += this.EnemyEscape;
        AudioEventSystem.onEnemyAlert += this.EnemyAlert;
        AudioEventSystem.onEnemyCapture += this.EnemyCapture;

        //Enviroment Events
        AudioEventSystem.onCabinLightsOn += this.CabinLightsOn;
        AudioEventSystem.onCabinLightsOff += this.CabinLightsOff;
        AudioEventSystem.onMovingPlatform += this.MovingPlatform;
        AudioEventSystem.onPowerUpIdle += this.PowerUpIdle;
        AudioEventSystem.onLockedDoor += this.LockedDoor;

        //Soundtrack events
        AudioEventSystem.onInGameSoundTrack += this.InGameSoundTrack;
        AudioEventSystem.onMainMenuSoundTrack += this.MainMenuSoundTrack;

        //Button Press Events
        AudioEventSystem.onButtonPress += this.ButtonPress;

    }

    public void RemoveSubscribers()
    {
        //Player Events, Set Up Done, Waiting on Animations
        AudioEventSystem.onPlayerAmbience -= this.PlayerAmbience;
        AudioEventSystem.onPlayerStep -= this.PlayerStep;
        AudioEventSystem.onPlayerIdle -= this.PlayerIdle;
        AudioEventSystem.onPlayerJump -= this.PlayerJumping;
        AudioEventSystem.onPlayerFall -= this.PlayerFall;
        AudioEventSystem.onPlayerDeath -= this.PlayerDeath;
        AudioEventSystem.onPlayerVictory -= this.PlayerVictory;
        AudioEventSystem.onPlayerAttack -= this.PlayerAttack;
        AudioEventSystem.onPlayerInivisibleOn -= this.PlayerInvisibleOn;
        AudioEventSystem.onPlayerInvisibleOff -= this.PlayerInvisibleOff;
        AudioEventSystem.onPlayerVisibleWarning -= this.PlayerVisibleWarning;
        AudioEventSystem.onPlayerLand -= this.PlayerLand;

        //Player Enviroment 
        AudioEventSystem.onPlayerHide -= this.PlayerHide;
        AudioEventSystem.onPlayerExitHide -= this.PlayerExitHide;
        AudioEventSystem.onPlayerPowerUpPickUp -= this.PlayerPowerUpPickUp;
        AudioEventSystem.onPlayerLadderClimb -= this.PlayerLadderClimb;

        //Testing Enemy Events
        AudioEventSystem.onEnemyStep -= this.EnemyStep;
        AudioEventSystem.onEnemyIdle -= this.EnemyIdle;
        AudioEventSystem.onEnemyEscape -= this.EnemyEscape;
        AudioEventSystem.onEnemyAlert -= this.EnemyAlert;
        AudioEventSystem.onEnemyCapture -= this.EnemyCapture;

        //Enviroment Events
        AudioEventSystem.onCabinLightsOn -= this.CabinLightsOn;
        AudioEventSystem.onCabinLightsOff -= this.CabinLightsOff;
        AudioEventSystem.onMovingPlatform -= this.MovingPlatform;
        AudioEventSystem.onPowerUpIdle -= this.PowerUpIdle;
        AudioEventSystem.onLockedDoor -= this.LockedDoor;

        //Soundtrack events
        AudioEventSystem.onInGameSoundTrack -= this.InGameSoundTrack;
        AudioEventSystem.onMainMenuSoundTrack -= this.MainMenuSoundTrack;

        //Button Press Events
        AudioEventSystem.onButtonPress -= this.ButtonPress;
    }
    //Audio Event From Wise sound
    //He will add AKSoundEngine.PostEvent(Calls my event EventName, gameObject);
    //Player Audio Event functions

    #region Player Events Functionality

    void PlayerAmbience()
    {
        Debug.Log("Player Ambience");
        AkSoundEngine.PostEvent ("Player_Ambience", gameObject);

    }
    void PlayerStep()
    {
        Debug.Log("Stepping Sound");
		AkSoundEngine.PostEvent ("Footsteps", gameObject);

    }
    void PlayerIdle()
    {
        Debug.Log("Idle Sound");
		AkSoundEngine.PostEvent ("Player_Idle", gameObject);

    }
    void PlayerJumping()
    {
        Debug.Log("Jumping Sound");
		AkSoundEngine.PostEvent ("Jumping", gameObject);
    }

    void PlayerFall()
    {
        Debug.Log("Fall sound");
		AkSoundEngine.PostEvent ("Landing", gameObject); //May not be included

    }

    void PlayerDeath()
    {
        Debug.Log("Death Sound");
		AkSoundEngine.PostEvent ("PlayerDefeat", gameObject);
    }

    void PlayerVictory()
    {
        Debug.Log("PlayerVictory Sound");
        AkSoundEngine.PostEvent("PlayerVictory", gameObject);
    }

    void PlayerAttack()
    {
        Debug.Log("Attacking Sound");
		AkSoundEngine.PostEvent ("Collection", gameObject);
    }

    void PlayerInvisibleOn() //Changing
    {
        Debug.Log("Invisible sound");
        AkSoundEngine.PostEvent("PowerUpToggleOn", gameObject);
    }

    void PlayerInvisibleOff() //add to Event system
    {
        Debug.Log("Invisible Off Sound");
        AkSoundEngine.PostEvent("PowerUpToggleOff", gameObject);
    }

    void PlayerVisibleWarning()
    {
        Debug.Log("Warning Visibility Sound");
        AkSoundEngine.PostEvent("NPCFound", gameObject);
    }

    void PlayerLand()
    {
        Debug.Log("Landing Sound");
        AkSoundEngine.PostEvent("Landing", gameObject); //May not be needed on in the banks
    }

    #endregion

    #region Enemy Events Functionality

    void EnemyStep()
    {
        Debug.Log("NPC Stepping Sound");
		AkSoundEngine.PostEvent ("NPC_Footsteps", gameObject);

    }

    void EnemyIdle() 
    {
        Debug.Log("Idle Sound");
		AkSoundEngine.PostEvent ("NPC_Ambience", gameObject);
    }

    void EnemyAlert()
    {
        Debug.Log("Alert Sound");
		AkSoundEngine.PostEvent ("NPCFound", gameObject);
    }

    void EnemyEscape()
    {
        Debug.Log("Enemy Escape");
    }

    void EnemyCapture()
    {
        Debug.Log("NPC Captured");
        AkSoundEngine.PostEvent("NPCCap", gameObject);
    }

    #endregion

    #region Player Enviroment Interactions Functionality

    void PlayerPowerUpPickUp()
    {
        Debug.Log("PowerUp PickUp Sound Functionality");
        AkSoundEngine.PostEvent("PowerUpCollection", gameObject);

    }

    void PlayerHide()
    {
        Debug.Log("Player Hide Sound");
        AkSoundEngine.PostEvent("Hide", gameObject);
    }

    void PlayerExitHide()
    {
        Debug.Log("Player Exit Sound");
        AkSoundEngine.PostEvent("ExitHide", gameObject);
    }

    void PlayerLadderClimb()
    {
        Debug.Log("Player Ladded Climb Sound");
        AkSoundEngine.PostEvent("Climbing", gameObject);
    }

    #endregion

    #region Enviroment Event Functionality

    void CabinLightsOn()
    {
        Debug.Log("Cabin Lights on Sound");
        AkSoundEngine.PostEvent("InLight", gameObject);
    }

    void CabinLightsOff()
    {
        Debug.Log("Cabin Lights Off Sound");
        AkSoundEngine.PostEvent("OutLight", gameObject);
    }

    void MovingPlatform()
    {
        Debug.Log("Moving Platform Sound");
    }

    void PowerUpIdle()
    {
        Debug.Log("PowerUpIdle Sound");
    }

    void LockedDoor()
    {
        Debug.Log("Locked");
    }

    #endregion

    #region SoundTracks Event Functionality

    void InGameSoundTrack()
    {
        Debug.Log("In Game Sound");
        AkSoundEngine.PostEvent("Night", gameObject);

    }

    void MainMenuSoundTrack()
    {
        Debug.Log("Main Menu Sound");
        AkSoundEngine.PostEvent("Game_Start", gameObject);
    }

    #endregion

    #region UI Button Functionality

    void ButtonPress()
    {
        Debug.Log("Button Pressed Sound");
        AkSoundEngine.PostEvent("Menu_Button", gameObject);
    }

    #endregion

    #region Helper Functions
    //Keeps audio from being overwritten
    IEnumerator PlayAudio(float audioLength)
    {
        playAudio = false; //keeps it from overriding
//        audioSource.Play();
        yield return new WaitForSeconds(audioLength + .03f); //Error margin
        playAudio = true;
          
    }

    #endregion

    #endregion
    */
    }
}
