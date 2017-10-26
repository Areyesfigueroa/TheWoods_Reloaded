using UnityEngine;
using System.Collections;

/// <summary>
/// Initializes events and 
/// </summary>
namespace TheWoods.Audio
{
    public class AudioEventSystem : MonoBehaviour
    {
        //Delegate
        public delegate void AudioEventHandler();
        //TODO:

        //Player Movement Events//Init done
        public static event AudioEventHandler onPlayerAmbience; //SetUp Done
        public static event AudioEventHandler onPlayerStep; //SetUp Done, Need Animation
        public static event AudioEventHandler onPlayerIdle; //SetUp Done, Need Animation
        public static event AudioEventHandler onPlayerJump; //SetUp Done, Need Animation
        public static event AudioEventHandler onPlayerFall; //SetUp Done, Need Animation
        public static event AudioEventHandler onPlayerDeath; //SetUp Done, Need Animation
        public static event AudioEventHandler onPlayerVictory; //SetUp Done, No Animation
        public static event AudioEventHandler onPlayerAttack; //SetUp Done, Need Animation

        //Init Done
        public static event AudioEventHandler onPlayerInivisibleOn; //SetUp Done
        public static event AudioEventHandler onPlayerInvisibleOff; //SetUp Done
        public static event AudioEventHandler onPlayerVisibleWarning; //ConeOfVision Script, init done
        public static event AudioEventHandler onPlayerLand; //Player Script, init done

        //Need to be implemented, Player Enviroment Interaction Events //Init done
        public static event AudioEventHandler onPlayerHide; //PlayerAbilities Script, Not implemented
        public static event AudioEventHandler onPlayerExitHide; //PlayerAbilities Script, Not implemented
        public static event AudioEventHandler onPlayerPowerUpPickUp; //PowerUpPickUp Script, Not implemented
        public static event AudioEventHandler onPlayerLadderClimb; //Ladders Script

        //NPC Events// Init Done
        public static event AudioEventHandler onEnemyStep; //EnemyMovement Script
        public static event AudioEventHandler onEnemyIdle; //EnemyMovement Script
        public static event AudioEventHandler onEnemyAlert; //ConeOfVision Script
        public static event AudioEventHandler onEnemyEscape; //EnemyMovement Script, Not Implemented
        public static event AudioEventHandler onEnemyCapture; //NOT IMPLEMENTED

        //Enviroment //Init Done
        public static event AudioEventHandler onCabinLightsOn; //Cabin Lights Script
        public static event AudioEventHandler onCabinLightsOff; //Cabin Lights Script
        public static event AudioEventHandler onMovingPlatform; //Moving Platform Script
        public static event AudioEventHandler onPowerUpIdle; //power up idle sound, PowerUpPickUp Script, Not Implemented
        public static event AudioEventHandler onLockedDoor; //LockedDoor Script, Not implemented

        //SoundTrack //init done
        public static event AudioEventHandler onInGameSoundTrack; //level manager Script, Not implemented, in game
        public static event AudioEventHandler onButtonPress; //MainMenu Script, not implemented, Main Menu
        public static event AudioEventHandler onMainMenuSoundTrack; //MainMenu Script, not implemented

        #region Init Player Controls Events

        //NOT ALL EVENTS CALLS ARE IMPLEMENTED. WAITING ON ANIMATION CLIPS TO TIME THE FRAMES
        //Init Player Movement Event Triggers

        public static void PlayerAmbience()
        {
            if (onPlayerAmbience != null)
            {
                onPlayerAmbience();
            }
        }
        public static void PlayerStep()
        {
            if (onPlayerStep != null) //check if there are any subscribers, Player Animation Event, TO BE IMPLEMENTED
            {
                onPlayerStep();
            }
        }
        public static void PlayerIdle() //Player Animation Event, TO BE IMPLEMENTED
        {
            if (onPlayerIdle != null)
            {
                onPlayerIdle();
            }
        }
        public static void PlayerJump() //Player Script
        {
            if (onPlayerJump != null)
            {
                onPlayerJump();
            }
        }
        public static void PlayerFall() //Player Script
        {
            if (onPlayerFall != null)
            {
                onPlayerFall();
            }
        }
        public static void PlayerDeath() //Health Script, TO BE IMPLEMETED
        {
            if (onPlayerDeath != null)
            {
                onPlayerDeath();
            }
        }
        public static void PlayerVictory()
        {
            if (onPlayerVictory != null)
            {
                onPlayerVictory();
            }
        }
        public static void PlayerAttack() //Undetermined, TO BE IMPLEMENTED
        {
            if (onPlayerAttack != null)
            {
                onPlayerAttack();
            }
        }

        public static void PlayerInvisibleOn() //not subscribed yet
        {
            if (onPlayerInivisibleOn != null)
            {
                onPlayerInivisibleOn();
            }
        }

        public static void PlayerInvisibleOff() //TO BE IMPLEMENTED
        {
            if (onPlayerInvisibleOff != null)
            {
                onPlayerInvisibleOff();
            }
        }

        public static void PlayerVisibleWarning() //new
        {
            if (onPlayerVisibleWarning != null)
            {
                onPlayerVisibleWarning();
            }
        }

        public static void PlayerLand()
        {
            if (onPlayerLand != null)
            {
                onPlayerLand();
            }
        }


        #endregion

        #region Init Enemy Trigger events

        public static void EnemyStep()
        {
            if (onEnemyStep != null)
            {
                onEnemyStep();
            }
        }

        public static void EnemyIdle()
        {
            if (onEnemyIdle != null)
            {
                onEnemyIdle();
            }
        }

        public static void EnemyAlert()
        {
            if (onEnemyAlert != null)
            {
                onEnemyAlert();
            }
        }

        public static void EnemyEscape()
        {
            if (onEnemyEscape != null)
            {
                onEnemyEscape();
            }
        }

        public static void EnemyCapture()
        {
            if (onEnemyCapture != null)
            {
                onEnemyCapture();
            }
        }

        #endregion

        #region Init Player Enviroment Interaction event

        public static void PowerUpPickUp()  // not subscribed
        {
            if (onPlayerPowerUpPickUp != null)
            {
                onPlayerPowerUpPickUp();
            }
        }

        public static void PlayerHide()
        {
            if (onPlayerHide != null)
            {
                onPlayerHide();
            }
        }
        public static void PlayerExitHide()
        {
            if (onPlayerExitHide != null)
            {
                onPlayerExitHide();
            }
        }
        public static void PlayerLadderClimb()
        {
            if (onPlayerLadderClimb != null)
            {
                onPlayerLadderClimb();
            }
        }


        #endregion

        #region Init Enviroment events

        public static void PowerUpIdle() //loop audio
        {
            if (onPowerUpIdle != null)
            {
                onPowerUpIdle();
            }
        }

        public static void CabinLightsOn()
        {
            if (onCabinLightsOn != null)
            {
                onCabinLightsOn();
            }
        }

        public static void CabinLightsOff()
        {
            if (onCabinLightsOff != null)
            {
                onCabinLightsOff();
            }
        }

        public static void MovingPlatforms()
        {
            if (onMovingPlatform != null)
            {
                onMovingPlatform();
            }
        }

        public static void LockedDoor()
        {
            if (onLockedDoor != null)
            {
                onLockedDoor();
            }
        }

        #endregion

        #region Init Soundtracks

        public static void InGameSoundtrack()
        {
            if (onInGameSoundTrack != null)
            {
                onInGameSoundTrack();
            }
        }

        public static void MainMenuSoundtrack()
        {
            if (onMainMenuSoundTrack != null)
            {
                onMainMenuSoundTrack();
            }
        }

        #endregion

        #region Init UI Button

        public static void ButtonPress()
        {
            if (onButtonPress != null)
            {
                onButtonPress();
            }
        }

        #endregion
    }

}
