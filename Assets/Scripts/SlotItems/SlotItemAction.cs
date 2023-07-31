using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotItemAction
{
    SlotManager slotManager;

    public void Defend() {
      slotManager.gameManager.battleManager.Defend();
    }

    public void Attack() {
        slotManager.gameManager.battleManager.Attack();
    }

    public void Heal() {
        slotManager.gameManager.battleManager.Heal();
    }
}
