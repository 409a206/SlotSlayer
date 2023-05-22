using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : SlotItem
{
    public override void Activate() {
        AttackEnemy();
    }
    
    public void AttackEnemy() {
        slotManager.gameManager.battleManager.Attack();
    }
}
