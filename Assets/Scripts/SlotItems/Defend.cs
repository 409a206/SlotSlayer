using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defend : SlotItem
{
   public override void Activate() {
        DefendPlayer();
   }
   private void DefendPlayer() {
      slotManager.gameManager.battleManager.Defend();
   }
}
