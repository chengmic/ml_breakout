using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public List<GameObject> current_powerups;
    
    public void newPowerupCreated(GameObject powerup){
        current_powerups.Add(powerup);
    }
    public void powerupDeleted(GameObject powerup){
        current_powerups.Remove(powerup);
    }

}
