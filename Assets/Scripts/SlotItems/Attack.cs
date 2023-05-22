using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : SlotItem
{
    public override IEnumerator Activate() {
        AttackEnemy();
    }
    
    public IEnumerator AttackEnemy() {
        slotManager.gameManager.battleManager.Attack();
    }
}
